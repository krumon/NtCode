﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Nt.Core.Logging.Internal
{
    /// <summary>
    /// Default implementation of <see cref="IExternalScopeProvider"/>
    /// </summary>
    internal sealed class LoggerFactoryScopeProvider : IExternalScopeProvider
    {
        private readonly AsyncLocal<Scope> _currentScope = new AsyncLocal<Scope>();
        private readonly ActivityTrackingOptions _activityTrackingOption;

        public LoggerFactoryScopeProvider(ActivityTrackingOptions activityTrackingOption) => _activityTrackingOption = activityTrackingOption;

        public void ForEachScope<TState>(Action<object, TState> callback, TState state)
        {
            void Report(Scope current)
            {
                if (current == null)
                {
                    return;
                }
                Report(current.Parent);
                callback(current.State, state);
            }

            if (_activityTrackingOption != ActivityTrackingOptions.None)
            {
                Activity activity = Activity.Current;
                if (activity != null)
                {
                    const string propertyKey = "__ActivityLogScope__";

                    ActivityLogScope activityLogScope = activity.GetCustomProperty(propertyKey) as ActivityLogScope;
                    if (activityLogScope == null)
                    {
                        activityLogScope = new ActivityLogScope(activity, _activityTrackingOption);
                        activity.SetCustomProperty(propertyKey, activityLogScope);
                    }

                    callback(activityLogScope, state);

                    // Tags and baggage are opt-in and thus we assume that most of the time it will not be used.
                    if ((_activityTrackingOption & ActivityTrackingOptions.Tags) != 0
                        && activity.TagObjects.GetEnumerator().MoveNext())
                    {
                        // As TagObjects is a IEnumerable<KeyValuePair<string, object?>> this can be used directly as a scope.
                        // We do this to safe the allocation of a wrapper object.
                        callback(activity.TagObjects, state);
                    }

                    if ((_activityTrackingOption & ActivityTrackingOptions.Baggage) != 0)
                    {
                        // Only access activity.Baggage as every call leads to an allocation
                        IEnumerable<KeyValuePair<string, string>> baggage = activity.Baggage;
                        if (baggage.GetEnumerator().MoveNext())
                        {
                            // For the baggage a wrapper object is necessary because we need to be able to overwrite ToString().
                            // In contrast to the TagsObject, Baggage doesn't have one underlining type where we can do this overwrite.
                            ActivityBaggageLogScopeWrapper scope = GetOrCreateActivityBaggageLogScopeWrapper(activity, baggage);
                            callback(scope, state);
                        }
                    }
                }
            }

            Report(_currentScope.Value);
        }

        private static ActivityBaggageLogScopeWrapper GetOrCreateActivityBaggageLogScopeWrapper(Activity activity, IEnumerable<KeyValuePair<string, string>> items)
        {
            const string additionalItemsBaggagePropertyKey = "__ActivityBaggageItemsLogScope__";
            var activityBaggageLogScopeWrapper = activity.GetCustomProperty(additionalItemsBaggagePropertyKey) as ActivityBaggageLogScopeWrapper;
            if (activityBaggageLogScopeWrapper == null)
            {
                activityBaggageLogScopeWrapper = new ActivityBaggageLogScopeWrapper(items);
                activity.SetCustomProperty(additionalItemsBaggagePropertyKey, activityBaggageLogScopeWrapper);
            }

            return activityBaggageLogScopeWrapper;
        }

        public IDisposable Push(object state)
        {
            Scope parent = _currentScope.Value;
            var newScope = new Scope(this, state, parent);
            _currentScope.Value = newScope;

            return newScope;
        }

        private sealed class Scope : IDisposable
        {
            private readonly LoggerFactoryScopeProvider _provider;
            private bool _isDisposed;

            internal Scope(LoggerFactoryScopeProvider provider, object state, Scope parent)
            {
                _provider = provider;
                State = state;
                Parent = parent;
            }

            public Scope Parent { get; }

            public object State { get; }

            public override string ToString()
            {
                return State?.ToString();
            }

            public void Dispose()
            {
                if (!_isDisposed)
                {
                    _provider._currentScope.Value = Parent;
                    _isDisposed = true;
                }
            }
        }

        private sealed class ActivityLogScope : IReadOnlyList<KeyValuePair<string, object>>
        {
            private string _cachedToString;
            private const int MaxItems = 5;
            private KeyValuePair<string, object>[] _items = new KeyValuePair<string, object>[MaxItems];

            public ActivityLogScope(Activity activity, ActivityTrackingOptions activityTrackingOption)
            {
                System.Diagnostics.Debug.Assert(activity != null);
                System.Diagnostics.Debug.Assert(activityTrackingOption != ActivityTrackingOptions.None);

                int count = 0;
                if ((activityTrackingOption & ActivityTrackingOptions.SpanId) != 0)
                {
                    _items[count++] = new KeyValuePair<string, object>("SpanId", activity.GetSpanId());
                }

                if ((activityTrackingOption & ActivityTrackingOptions.TraceId) != 0)
                {
                    _items[count++] = new KeyValuePair<string, object>("TraceId", activity.GetTraceId());
                }

                if ((activityTrackingOption & ActivityTrackingOptions.ParentId) != 0)
                {
                    _items[count++] = new KeyValuePair<string, object>("ParentId", activity.GetParentId());
                }

                if ((activityTrackingOption & ActivityTrackingOptions.TraceState) != 0)
                {
                    _items[count++] = new KeyValuePair<string, object>("TraceState", activity.TraceStateString);
                }

                if ((activityTrackingOption & ActivityTrackingOptions.TraceFlags) != 0)
                {
                    _items[count++] = new KeyValuePair<string, object>("TraceFlags", activity.ActivityTraceFlags);
                }

                Count = count;
            }

            public int Count { get; }

            public KeyValuePair<string, object> this[int index]
            {
                get
                {
                    if (index >= Count)
                    {
                        throw new ArgumentOutOfRangeException(nameof(index));
                    }

                    return _items[index];
                }
            }

            public override string ToString()
            {
                if (_cachedToString == null)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append(_items[0].Key);
                    sb.Append(':');
                    sb.Append(_items[0].Value);

                    for (int i = 1; i < Count; i++)
                    {
                        sb.Append(", ");
                        sb.Append(_items[i].Key);
                        sb.Append(':');
                        sb.Append(_items[i].Value);
                    }
                    _cachedToString = sb.ToString();
                }

                return _cachedToString;
            }

            public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
            {
                for (int i = 0; i < Count; ++i)
                {
                    yield return this[i];
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private sealed class ActivityBaggageLogScopeWrapper : IEnumerable<KeyValuePair<string, string>>
        {
            private readonly IEnumerable<KeyValuePair<string, string>> _items;

            private StringBuilder _stringBuilder;

            public ActivityBaggageLogScopeWrapper(IEnumerable<KeyValuePair<string, string>> items)
            {
                _items = items;
            }

            public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
            {
                return _items.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return _items.GetEnumerator();
            }

            public override string ToString()
            {
                lock (this)
                {
                    IEnumerator<KeyValuePair<string, string>> enumerator = _items.GetEnumerator();
                    if (!enumerator.MoveNext())
                    {
                        return string.Empty;
                    }

                    if (_stringBuilder == null) _stringBuilder = new StringBuilder();
                    _stringBuilder.Append(enumerator.Current.Key);
                    _stringBuilder.Append(':');
                    _stringBuilder.Append(enumerator.Current.Value);

                    while (enumerator.MoveNext())
                    {
                        _stringBuilder.Append(", ");
                        _stringBuilder.Append(enumerator.Current.Key);
                        _stringBuilder.Append(':');
                        _stringBuilder.Append(enumerator.Current.Value);
                    }

                    string result = _stringBuilder.ToString();
                    _stringBuilder.Clear();
                    return result;
                }
            }
        }
    }

    internal static class ActivityExtensions
    {
        public static string GetSpanId(this Activity activity)
        {
            switch (activity.IdFormat)
            {
                case ActivityIdFormat.Hierarchical:
                    return activity.Id;
                case ActivityIdFormat.W3C:
                    return activity.SpanId.ToHexString();
                default:
                    return string.Empty;
            }
        }

        public static string GetTraceId(this Activity activity)
        {
            switch (activity.IdFormat)
            {
                case ActivityIdFormat.Hierarchical:
                    return activity.RootId;
                case ActivityIdFormat.W3C:
                    return activity.TraceId.ToHexString();
                default:
                    return string.Empty;
            }
        }

        public static string GetParentId(this Activity activity)
        {
            switch (activity.IdFormat)
            {
                case ActivityIdFormat.Hierarchical:
                    return activity.ParentId;
                case ActivityIdFormat.W3C:
                    return activity.ParentSpanId.ToHexString();
                default:
                    return string.Empty;
            }
        }
    }
}
