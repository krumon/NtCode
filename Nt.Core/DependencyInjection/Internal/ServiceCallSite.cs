﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace Nt.Core.DependencyInjection.Internal
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
        public abstract CallSiteKind Kind { get;}
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
                throw new ArgumentException("Implementation PeriodType Cant Be Converted To Service PeriodType, the constructor type is not assignable from serviceType");
            }

            ServiceType = serviceType;
            ConstructorInfo = constructorInfo;
            ParameterCallSites = parameters;
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
            Type itemType, 
            ServiceCallSite[] serviceCallSites) 
            : base()
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
            : base()
        {
            _serviceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));
            if (defaultValue != null && !serviceType.IsInstanceOfType(defaultValue))
            {
                throw new ArgumentException("Constant Cant Be Converted To Service PeriodType, defaultValue.GetType(), serviceType");
            }

            Value = defaultValue;
        }

        public override Type ServiceType => _serviceType;
        public override Type ImplementationType => DefaultValue?.GetType() ?? _serviceType;
        public override CallSiteKind Kind { get; } = CallSiteKind.Constant;
    }
    internal sealed class ServiceProviderCallSite : ServiceCallSite
    {
        public ServiceProviderCallSite() : base()
        {
        }

        public override Type ServiceType { get; } = typeof(IServiceProvider);
        public override Type ImplementationType { get; } = typeof(ServiceProvider);
        public override CallSiteKind Kind { get; } = CallSiteKind.ServiceProvider;
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
        public override CallSiteKind Kind { get; } = CallSiteKind.Factory;
    }
}