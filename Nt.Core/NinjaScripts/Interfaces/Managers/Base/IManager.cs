using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;

namespace Nt.Core.Ninjascript
{
    /// <summary>
    /// The interfece for any ninjascripts manager.
    /// </summary>
    public interface IManager : INinjascript
    {

        /// <summary>
        /// Nijascripts collection
        /// </summary>
        List<INinjascript> Scripts { get; }

        /// <summary>
        /// Sets the ninjascripts collection from <see cref="IManagerBuilder"/> object.
        /// </summary>
        /// <param name="scripts"></param>
        /// <param name="methodName"></param>
        void SetScripts(List<INinjascript> scripts, [CallerMemberName] string methodName = null);

    }

}
