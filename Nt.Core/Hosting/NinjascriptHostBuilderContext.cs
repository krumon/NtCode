using System.Collections.Generic;

namespace Nt.Core.Hosting
{
    public class NinjascriptHostBuilderContext
    {

        #region Copy from microsoft

        public NinjascriptHostBuilderContext(IDictionary<object, object> properties)
        {
            Properties = properties ?? throw new System.ArgumentNullException(nameof(properties));
        }

        /// <summary>
        /// A central location for sharing state between components during the host building process.
        /// </summary>
        public IDictionary<object, object> Properties { get; }

        /// <summary>
        /// The <see cref="INinjascriptHostEnvironment" /> initialized by the <see cref="IHost" />.
        /// </summary>
        public INinjascriptHostEnvironment HostingEnvironment { get; set; }

        /// <summary>
        /// The <see cref="INinjascriptHostConfiguration" /> containing the merged configuration of the application and the <see cref="IHost" />.
        /// </summary>
        public INinjascriptHostConfiguration Configuration { get; set; }

        #endregion
    }
}
