using System;
using System.Runtime.ExceptionServices;

namespace Nt.Core.DependencyInjection.Internal
{
    internal class CallSiteRuntimeResolver
    {

        public static CallSiteRuntimeResolver Instance { get; } = new CallSiteRuntimeResolver();

        private CallSiteRuntimeResolver()
        {
        }

        public object Resolve(ServiceCallSite callSite, ServiceProvider serviceProvider)
        {
            // Fast path to avoid virtual calls if we already have the cached value in the root scope
            if (callSite.Value is object cached)
            {
                return cached;
            }

            return VisitCallSite(callSite, serviceProvider);
        }

        protected object VisitCallSite(ServiceCallSite callSite, ServiceProvider serviceProvider)
        {
            switch (callSite.Kind)
            {
                case CallSiteKind.Factory:
                    return VisitFactory((FactoryCallSite)callSite, serviceProvider);
                case CallSiteKind.IEnumerable:
                    return VisitIEnumerable((IEnumerableCallSite)callSite, serviceProvider);
                case CallSiteKind.Constructor:
                    return VisitConstructor((ConstructorCallSite)callSite, serviceProvider);
                case CallSiteKind.Constant:
                    return VisitConstant((ConstantCallSite)callSite, serviceProvider);
                default:
                    throw new NotSupportedException($"CallSiteTypeNotSupported ({callSite.GetType()})");
            }
        }

        protected object VisitConstructor(ConstructorCallSite constructorCallSite, ServiceProvider context)
        {
            object[] parameterValues;
            if (constructorCallSite.ParameterCallSites.Length == 0)
            {
                parameterValues = Array.Empty<object>();
            }
            else
            {
                parameterValues = new object[constructorCallSite.ParameterCallSites.Length];
                for (int index = 0; index < parameterValues.Length; index++)
                {
                    parameterValues[index] = VisitCallSite(constructorCallSite.ParameterCallSites[index], context);
                }
            }

            try
            {
                return constructorCallSite.ConstructorInfo.Invoke(parameterValues);
            }
            catch (Exception ex) when (ex.InnerException != null)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                // The above line will always throw, but the compiler requires we throw explicitly.
                throw;
            }
        }

        protected object VisitConstant(ConstantCallSite constantCallSite, ServiceProvider context)
        {
            return constantCallSite.DefaultValue;
        }

        protected object VisitIEnumerable(IEnumerableCallSite enumerableCallSite, ServiceProvider context)
        {
            var array = Array.CreateInstance(
                enumerableCallSite.ItemType,
                enumerableCallSite.ServiceCallSites.Length);

            for (int index = 0; index < enumerableCallSite.ServiceCallSites.Length; index++)
            {
                object value = VisitCallSite(enumerableCallSite.ServiceCallSites[index], context);
                array.SetValue(value, index);
            }
            return array;
        }

        protected object VisitFactory(FactoryCallSite factoryCallSite, ServiceProvider context)
        {
            return factoryCallSite.Factory(context);
        }

    }
}
