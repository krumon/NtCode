using System;

namespace Nt.Core
{

    /// <summary>
    /// Interface for any manager builder.
    /// </summary>
    public interface IManagerBuilder : IBuilder
    {

        IManagerBuilder Add<Script, Options>(Action<Options> options)
            where Script : INinjascript;

    }

}
