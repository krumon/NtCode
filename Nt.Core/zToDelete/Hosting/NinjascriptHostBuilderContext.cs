using Nt.Core.Configuration;
using Nt.Core.Services;
using System.Collections.Generic;

namespace Nt.Core.Hosting
{
    /// <summary>
    /// Context containing the common services on the <see cref="INinjascriptHost" />. Some properties may be null until set by the <see cref="IHost" />.
    /// </summary>
    public class NinjascriptHostBuilderContext
    {

        public NinjascriptHostBuilderContext(IDictionary<object, object> properties)
        {
            Properties = properties ?? throw new System.ArgumentNullException(nameof(properties));
        }

        /// <summary>
        /// A central location for sharing state between components during the host building process.
        /// </summary>
        public IDictionary<object, object> Properties { get; }

        /// <summary>
        /// The <see cref="INinjascriptEnvironment" /> initialized by the <see cref="IHost" />.
        /// </summary>
        public INinjascriptHostEnvironment HostingEnvironment { get; set; }

        /// <summary>
        /// The <see cref="INinjascriptConfiguration" /> containing the merged configuration of the application and the <see cref="IHost" />.
        /// </summary>
        public INinjascriptConfiguration Configuration { get; set; }

    }
}
