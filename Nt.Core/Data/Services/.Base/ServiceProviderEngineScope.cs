using Nt.Core.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Nt.Core.Data
{
    public class ServiceProviderEngineScope : IServiceProvider, IServiceScope, IAsyncDisposable
    {

        #region Private members

        private bool _disposed;
        private List<object> _disposables;

        // For testing only
        internal IList<object> Disposables => _disposables ?? (IList<object>)Array.Empty<object>();
        // This lock protects state on the scope, in particular, for the root scope, it protects
        // the list of disposable entries only, since ResolvedServices are cached on CallSites
        // For other scopes, it protects ResolvedServices and the list of disposables
        internal object Sync => ResolvedServices;
        internal ServiceProvider RootProvider { get; }
        internal Dictionary<Type, object> ResolvedServices { get; }

        #endregion

        #region Public properties

        public bool IsRootScope { get; }
        public IServiceProvider ServiceProvider => this;

        #endregion

        #region Constructor

        public ServiceProviderEngineScope(ServiceProvider provider, bool isRootScope)
        {
            ResolvedServices = new Dictionary<Type, object>();
            RootProvider = provider;
            IsRootScope = isRootScope;
        }

        #endregion

        #region Public methods

        public object GetService(Type serviceType)
        {
            if (_disposed)
            {
                ThrowHelper.ThrowObjectDisposedException();
            }

            return RootProvider.GetService(serviceType, this);
        }

        public IServiceScope CreateScope() => RootProvider.CreateScope();

        public void Dispose()
        {
            List<object> toDispose = BeginDispose();

            if (toDispose != null)
            {
                for (int i = toDispose.Count - 1; i >= 0; i--)
                {
                    if (toDispose[i] is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                    else
                    {
                        throw new InvalidOperationException(toDispose[i].ToString());
                    }
                }
            }
        }

        public ValueTask DisposeAsync()
        {
            List<object> toDispose = BeginDispose();

            if (toDispose != null)
            {
                try
                {
                    for (int i = toDispose.Count - 1; i >= 0; i--)
                    {
                        object disposable = toDispose[i];
                        if (disposable is IAsyncDisposable asyncDisposable)
                        {
                            ValueTask vt = asyncDisposable.DisposeAsync();
                            if (!vt.IsCompletedSuccessfully)
                            {
                                return Await(i, vt, toDispose);
                            }

                            // If its a IValueTaskSource backed ValueTask,
                            // inform it its result has been read so it can reset
                            vt.GetAwaiter().GetResult();
                        }
                        else
                        {
                            ((IDisposable)disposable).Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    return new ValueTask(Task.FromException(ex));
                }
            }

            return default;

            async ValueTask Await(int i, ValueTask vt, List<object> toDispos)
            {
                await vt.ConfigureAwait(false);
                // vt is acting on the disposable at index i,
                // decrement it and move to the next iteration
                i--;

                for (; i >= 0; i--)
                {
                    object disposable = toDispos[i];
                    if (disposable is IAsyncDisposable asyncDisposable)
                    {
                        await asyncDisposable.DisposeAsync().ConfigureAwait(false);
                    }
                    else
                    {
                        ((IDisposable)disposable).Dispose();
                    }
                }
            }
        }

        #endregion

        #region Private methods

        internal object CaptureDisposable(object service)
        {
            if (ReferenceEquals(this, service) || !(service is IDisposable || service is IAsyncDisposable))
            {
                return service;
            }

            bool disposed = false;
            lock (Sync)
            {
                if (_disposed)
                {
                    disposed = true;
                }
                else
                {
                    if (_disposables == null)
                        _disposables = new List<object>();

                    _disposables.Add(service);
                }
            }

            // Don't run customer code under the lock
            if (disposed)
            {
                if (service is IDisposable disposable)
                {
                    disposable.Dispose();
                }
                else
                {
                    // sync over async, for the rare case that an object only implements IAsyncDisposable and may end up starving the thread pool.
                    Task.Run(() => ((IAsyncDisposable)service).DisposeAsync().AsTask()).GetAwaiter().GetResult();
                }

                ThrowHelper.ThrowObjectDisposedException();
            }

            return service;
        }

        private List<object> BeginDispose()
        {
            lock (Sync)
            {
                if (_disposed)
                {
                    return null;
                }

                // Track statistics about the scope (number of disposable objects and number of disposed services)
                string logText = $"{RootProvider.GetHashCode()} : Resolved services: {ResolvedServices.Count} : Disposables: {_disposables?.Count ?? 0}";

                // We've transitioned to the disposed state, so future calls to
                // CaptureDisposable will immediately dispose the object.
                // No further changes to _state.Disposables, are allowed.
                _disposed = true;

                // ResolvedServices is never cleared for singletons because there might be a compilation running in background
                // trying to get a cached singleton service. If it doesn't find it
                // it will try to create a new one which will result in an ObjectDisposedException.

                return _disposables;
            }
        }

        #endregion
    }
}
