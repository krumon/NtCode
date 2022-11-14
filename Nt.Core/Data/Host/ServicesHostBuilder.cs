using System;
using System.Collections.Generic;

namespace Nt.Core.Data
{
    /// <summary>
    /// Default services host builder.
    /// </summary>
    public class ServicesHostBuilder : IServicesHostBuilder
    {
        public IDictionary<object, object> Properties => throw new NotImplementedException();

        public IServicesHost Build()
        {
            throw new NotImplementedException();
        }

        public IServicesHostBuilder ConfigureServices(Action<IServiceCollection<IServiceDescriptor>> configureDelegate)
        {
            throw new NotImplementedException();
        }
    }
}
