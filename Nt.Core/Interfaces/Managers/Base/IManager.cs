﻿using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;

namespace Nt.Core
{
    /// <summary>
    /// The interfece for any ninjascripts manager.
    /// </summary>
    public interface IManager : INinjascript
    {
        /// <summary>
        /// Sets the ninjascripts collection from <see cref="IManagerBuilder"/> object.
        /// </summary>
        /// <param name="scripts"></param>
        /// <param name="methodName"></param>
        void SetScripts(List<INinjascript> scripts, [CallerMemberName] string methodName = null);

    }

}
