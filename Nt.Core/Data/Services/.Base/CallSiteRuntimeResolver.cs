namespace Nt.Core.Data
{
    public sealed class CallSiteRuntimeResolver
    {

        public static CallSiteRuntimeResolver Instance { get; } = new();

        private CallSiteRuntimeResolver()
        {
        }

        public object Resolve(ServiceCallSite callSite, ServiceProviderEngineScope scope)
        {
            // Fast path to avoid virtual calls if we already have the cached value in the root scope
            if (scope.IsRootScope && callSite.Value is object cached)
            {
                return cached;
            }

            return VisitCallSite(callSite, new RuntimeResolverContext
            {
                Scope = scope
            });
        }

        protected override object VisitDisposeCache(ServiceCallSite transientCallSite, RuntimeResolverContext context)
        {
            return context.Scope.CaptureDisposable(VisitCallSiteMain(transientCallSite, context));
        }

        protected override object VisitConstructor(ConstructorCallSite constructorCallSite, RuntimeResolverContext context)
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

#if NETFRAMEWORK || NETSTANDARD2_0
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
#else
            return constructorCallSite.ConstructorInfo.Invoke(BindingFlags.DoNotWrapExceptions, binder: null, parameters: parameterValues, culture: null);
#endif
        }

        protected override object VisitConstant(ConstantCallSite constantCallSite, RuntimeResolverContext context)
        {
            return constantCallSite.DefaultValue;
        }

        protected override object VisitIEnumerable(IEnumerableCallSite enumerableCallSite, RuntimeResolverContext context)
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

        protected override object VisitFactory(FactoryCallSite factoryCallSite, RuntimeResolverContext context)
        {
            return factoryCallSite.Factory(context.Scope);
        }
    }
}
