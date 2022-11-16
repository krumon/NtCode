﻿using System.Collections.Generic;
using System.Text;
using System;
using Kr.Core.Helpers;

namespace Nt.Core.Services.Internal
{
    internal sealed class CallSiteChain
    {

        private readonly Dictionary<Type, ChainItemInfo> _callSiteChain;

        public CallSiteChain()
        {
            _callSiteChain = new Dictionary<Type, ChainItemInfo>();
        }

        public void CheckCircularDependency(Type serviceType)
        {
            if (_callSiteChain.ContainsKey(serviceType))
            {
                throw new InvalidOperationException(CreateCircularDependencyExceptionMessage(serviceType));
            }
        }

        public void Remove(Type serviceType)
        {
            _callSiteChain.Remove(serviceType);
        }

        public void Add(Type serviceType, Type implementationType = null)
        {
            _callSiteChain[serviceType] = new ChainItemInfo(_callSiteChain.Count, implementationType);
        }

        private string CreateCircularDependencyExceptionMessage(Type type)
        {
            var messageBuilder = new StringBuilder();
            messageBuilder.Append("CircularDependencyException");
            messageBuilder.AppendLine();

            AppendResolutionPath(messageBuilder, type);

            return messageBuilder.ToString();
        }

        private void AppendResolutionPath(StringBuilder builder, Type currentlyResolving)
        {
            var ordered = new List<KeyValuePair<Type, ChainItemInfo>>(_callSiteChain);
            ordered.Sort((a, b) => a.Value.Order.CompareTo(b.Value.Order));

            foreach (KeyValuePair<Type, ChainItemInfo> pair in ordered)
            {
                Type serviceType = pair.Key;
                Type implementationType = pair.Value.ImplementationType;
                if (implementationType == null || serviceType == implementationType)
                {
                    builder.Append(TypeNameHelper.GetTypeDisplayName(serviceType));
                }
                else
                {
                    builder.AppendFormat("{0}({1})",
                        TypeNameHelper.GetTypeDisplayName(serviceType),
                        TypeNameHelper.GetTypeDisplayName(implementationType));
                }

                builder.Append(" -> ");
            }

            builder.Append(TypeNameHelper.GetTypeDisplayName(currentlyResolving));
        }

        private readonly struct ChainItemInfo
        {
            public int Order { get; }
            public Type ImplementationType { get; }

            public ChainItemInfo(int order, Type implementationType)
            {
                Order = order;
                ImplementationType = implementationType;
            }
        }
    }
}