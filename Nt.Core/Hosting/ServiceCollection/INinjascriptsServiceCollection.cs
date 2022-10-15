using System.Collections;
using System.Collections.Generic;

namespace Nt.Core.Hosting
{
    /// <summary>
    /// Specifies the contract for a collection of ninjascript service descriptors.
    /// </summary>
    public interface INinjascriptsServiceCollection : 
        IList<NinjascriptServiceDescriptor>, 
        ICollection<NinjascriptServiceDescriptor>, 
        IEnumerable<NinjascriptServiceDescriptor>, 
        IEnumerable
    {
    }
}
