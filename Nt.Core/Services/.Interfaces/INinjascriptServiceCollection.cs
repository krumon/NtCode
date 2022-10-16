using System.Collections;
using System.Collections.Generic;

namespace Nt.Core.Services
{
    /// <summary>
    /// Specifies the contract for a collection of ninjascript service descriptors.
    /// </summary>
    public interface INinjascriptServiceCollection : 
        IList<NinjascriptServiceDescriptor>, 
        ICollection<NinjascriptServiceDescriptor>, 
        IEnumerable<NinjascriptServiceDescriptor>, 
        IEnumerable
    {
    }
}
