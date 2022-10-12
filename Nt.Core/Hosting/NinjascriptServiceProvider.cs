using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Nt.Core.Hosting
{
    /// <summary>
    /// Default <see cref="INinjascriptServiceProvider"/>.
    /// </summary>
    public abstract class NinjascriptServiceProvider : INinjascriptServiceProvider, IDisposable, IAsyncDisposable
    {

        #region Private members

        private bool _dispose;

        private ConcurrentDictionary<Type, Func<Type,object>> _realizedServices;

        private object _createService;

        NinjascriptServiceDescriptor[] _descriptors;

        #endregion

        #region Implementation

        /// <summary>
        /// Gets the service object of the specified type.
        /// </summary>
        /// <param name="serviceType">The type of the service to get.</param>
        /// <returns>The service that was produced.</returns>
        public object GetService(Type serviceType)
        {
            if (_dispose)
                throw new ObjectDisposedException(nameof(NinjascriptServiceProvider));

            if (ValidateService(serviceType))
            {
                Func<Type,object> realizedService = _realizedServices.GetOrAdd(serviceType.GetType(), CreateService);
                var result = realizedService.Invoke(serviceType);
                return result;
            }

            return default;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            DisposeCore();
        }

        /// <inheritdoc/>
        public ValueTask DisposeAsync()
        {
            DisposeCore();
            return default;
        }

        public abstract object CreateService(Type serviceType);
        public abstract bool ValidateService(Type serviceType);

        #endregion

        #region Constructors

        public NinjascriptServiceProvider(ICollection<NinjascriptServiceDescriptor> descriptors)
        {
            _realizedServices = new ConcurrentDictionary<Type, Func<Type,object>>();
            ValidateDescriptors(descriptors);

        }

        #endregion

        #region Private methods

        private void DisposeCore()
        {
            _dispose = true;
        }

        private NinjascriptServiceDescriptor[] ValidateDescriptors(ICollection<NinjascriptServiceDescriptor> descriptors)
        {
            _descriptors = new NinjascriptServiceDescriptor[descriptors.Count];
            descriptors.CopyTo(_descriptors, 0);
            return _descriptors;
        }

        private object CreateServiceByDefault(Type serviceType)
        {
            try
            {
                if (!ValidateServiceByDefault())
                    return default;

                return _createService = Activator.CreateInstance(serviceType);
            }
            catch
            {
                Debugger.Break();
                return default;
            }
        }

        private bool ValidateServiceByDefault() => true;

        #endregion

    }
}
