using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Nt.Core.Services.Internal
{
    /// <summary>
    /// Summary description for ServiceCallSite
    /// </summary>
    internal abstract class ServiceCallSite
    {
        protected ServiceCallSite(ResultCache cache)
        {
            Cache = cache;
        }

        public abstract Type ServiceType { get; }
        public abstract Type ImplementationType { get; }
        public abstract CallSiteKind Kind { get; }
        public ResultCache Cache { get; }
        public object Value { get; set; }

        public bool CaptureDisposable =>
            ImplementationType == null ||
            typeof(IDisposable).IsAssignableFrom(ImplementationType) ||
            typeof(IAsyncDisposable).IsAssignableFrom(ImplementationType);
    }

    internal sealed class ConstructorCallSite : ServiceCallSite
    {
        internal ConstructorInfo ConstructorInfo { get; }
        internal ServiceCallSite[] ParameterCallSites { get; }

        public ConstructorCallSite(
            ResultCache cache, 
            Type serviceType, 
            ConstructorInfo constructorInfo) 
            : this(cache, serviceType, constructorInfo, Array.Empty<ServiceCallSite>())
        {
        }

        public ConstructorCallSite(
            ResultCache cache, 
            Type serviceType,
            ConstructorInfo constructorInfo, 
            ServiceCallSite[] parameterCallSites) 
            : base(cache)
        {
            if (!serviceType.IsAssignableFrom(constructorInfo.DeclaringType))
            {
                throw new ArgumentException("Implementation Type Cant Be Converted To Service Type, the constructor type is not assignable from serviceType");
            }

            ServiceType = serviceType;
            ConstructorInfo = constructorInfo;
            ParameterCallSites = parameterCallSites;
        }

        public override Type ServiceType { get; }
        public override Type ImplementationType => ConstructorInfo.DeclaringType;
        public override CallSiteKind Kind { get; } = CallSiteKind.Constructor;

    }

    internal sealed class IEnumerableCallSite : ServiceCallSite
    {
        internal Type ItemType { get; }
        internal ServiceCallSite[] ServiceCallSites { get; }

        public IEnumerableCallSite(
            ResultCache cache, 
            Type itemType, 
            ServiceCallSite[] serviceCallSites) 
            : base(cache)
        {
            ItemType = itemType;
            ServiceCallSites = serviceCallSites;
        }

        public override Type ServiceType => typeof(IEnumerable<>).MakeGenericType(ItemType);
        public override Type ImplementationType => ItemType.MakeArrayType();
        public override CallSiteKind Kind { get; } = CallSiteKind.IEnumerable;
    }

    internal sealed class ConstantCallSite : ServiceCallSite
    {
        private readonly Type _serviceType;
        internal object DefaultValue => Value;

        public ConstantCallSite(
            Type serviceType, 
            object defaultValue) 
            : base(ResultCache.None)
        {
            _serviceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));
            if (defaultValue != null && !serviceType.IsInstanceOfType(defaultValue))
            {
                throw new ArgumentException("Constant Cant Be Converted To Service Type, defaultValue.GetType(), serviceType");
            }

            Value = defaultValue;
        }

        public override Type ServiceType => _serviceType;
        public override Type ImplementationType => DefaultValue?.GetType() ?? _serviceType;
        public override CallSiteKind Kind { get; } = CallSiteKind.Constant;
    }

    internal sealed class ServiceProviderCallSite : ServiceCallSite
    {
        public ServiceProviderCallSite() : base(ResultCache.None)
        {
        }

        public override Type ServiceType { get; } = typeof(INinjascriptServiceProvider);
        public override Type ImplementationType { get; } = typeof(NinjascriptServiceProvider);
        public override CallSiteKind Kind { get; } = CallSiteKind.ServiceProvider;
    }

    internal sealed class FactoryCallSite : ServiceCallSite
    {
        public Func<INinjascriptServiceProvider, object> Factory { get; }

        public FactoryCallSite(
            ResultCache cache, 
            Type serviceType, 
            Func<INinjascriptServiceProvider, object> factory) 
            : base(cache)
        {
            Factory = factory;
            ServiceType = serviceType;
        }

        public override Type ServiceType { get; }
        public override Type ImplementationType => null;

        public override CallSiteKind Kind { get; } = CallSiteKind.Factory;
    }

    internal enum CallSiteKind
    {
        Factory,
        Constructor,
        Constant,
        IEnumerable,
        ServiceProvider,
        Scope,
        Transient,
        Singleton
    }

    internal struct ResultCache
    {
        public ServiceCacheKey Key { get; set; }
        public CallSiteResultCacheLocation Location { get; set; }

        internal ResultCache(CallSiteResultCacheLocation lifetime, ServiceCacheKey cacheKey)
        {
            Location = lifetime;
            Key = cacheKey;
        }

        public ResultCache(ServiceLifetime lifetime, Type type, int slot)
        {
            Debug.Assert(lifetime == ServiceLifetime.Transient || type != null);

            switch (lifetime)
            {
                case ServiceLifetime.Singleton:
                    Location = CallSiteResultCacheLocation.Root;
                    break;
                case ServiceLifetime.Scoped:
                    Location = CallSiteResultCacheLocation.Scope;
                    break;
                case ServiceLifetime.Transient:
                    Location = CallSiteResultCacheLocation.Dispose;
                    break;
                default:
                    Location = CallSiteResultCacheLocation.None;
                    break;
            }
            Key = new ServiceCacheKey(type, slot);
        }

        public static ResultCache None { get; } = new ResultCache(CallSiteResultCacheLocation.None, ServiceCacheKey.Empty);

    }

}