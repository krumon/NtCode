using System;
using System.Collections.Generic;
using System.Reflection;

namespace Nt.Core.Data
{
    /// <summary>
    /// Summary description for ServiceCallSite
    /// </summary>
    internal abstract class ServiceCallSite
    {
        protected ServiceCallSite()
        {
        }

        public abstract Type ServiceType { get; }
        public abstract Type ImplementationType { get; }
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
            Type serviceType, 
            ConstructorInfo constructorInfo) 
            : this(serviceType, constructorInfo, Array.Empty<ServiceCallSite>())
        {
        }

        public ConstructorCallSite(
            Type serviceType,
            ConstructorInfo constructorInfo,
            ServiceCallSite[] parameters) 
            : base()
        {
            if (!serviceType.IsAssignableFrom(constructorInfo.DeclaringType))
            {
                throw new ArgumentException("Implementation Type Cant Be Converted To Service Type, the constructor type is not assignable from serviceType");
            }

            ServiceType = serviceType;
            ConstructorInfo = constructorInfo;
            ParameterCallSites = parameters;
        }

        public override Type ServiceType { get; }
        public override Type ImplementationType => ConstructorInfo.DeclaringType;

    }
    internal sealed class IEnumerableCallSite : ServiceCallSite
    {
        internal Type ItemType { get; }
        internal ServiceCallSite[] ServiceCallSites { get; }

        public IEnumerableCallSite(
            Type itemType, 
            ServiceCallSite[] serviceCallSites) 
            : base()
        {
            ItemType = itemType;
            ServiceCallSites = serviceCallSites;
        }

        public override Type ServiceType => typeof(IEnumerable<>).MakeGenericType(ItemType);
        public override Type ImplementationType => ItemType.MakeArrayType();
    }
    internal sealed class ConstantCallSite : ServiceCallSite
    {
        private readonly Type _serviceType;
        internal object DefaultValue => Value;

        public ConstantCallSite(
            Type serviceType, 
            object defaultValue) 
            : base()
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
    }
    internal sealed class ServiceProviderCallSite : ServiceCallSite
    {
        public ServiceProviderCallSite() : base()
        {
        }

        public override Type ServiceType { get; } = typeof(IServiceProvider);
        public override Type ImplementationType { get; } = typeof(ServiceProvider);
    }
    internal sealed class FactoryCallSite : ServiceCallSite
    {
        public Func<IServiceProvider, object> Factory { get; }

        public FactoryCallSite(
            Type serviceType, 
            Func<IServiceProvider, object> factory) 
            : base()
        {
            Factory = factory;
            ServiceType = serviceType;
        }

        public override Type ServiceType { get; }
        public override Type ImplementationType => null;

    }
}