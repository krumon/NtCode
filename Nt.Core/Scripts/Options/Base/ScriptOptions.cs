using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// The script options
    /// </summary>
    public class ScriptOptions<T> : BaseOptions<T>
        where T : ScriptOptions<T>, new()
    {

        #region Private members / Default values

        /// <summary>
        /// Represents the ninjascript description.
        /// </summary>
        private string description = @"Script created by kRuMoN.";

        /// <summary>
        /// Represents the ninjascript name.
        /// </summary>
        private string name = "kRuMoN Script";

        /// <summary>
        /// Represents the ninjascript calculate mode.
        /// </summary>
        private Calculate calculate = Calculate.OnBarClose;

        #endregion

        #region Properties

        /// <summary>
        /// Represents the ninjascript description.
        /// </summary>
        public string Description { get { return description; } private set { description = value; } }

        /// <summary>
        /// Represents the ninjascript name.
        /// </summary>
        public string Name { get { return name; } private set { name = value; } }

        /// <summary>
        /// Represents the ninjascript calculate mode.
        /// </summary>
        public Calculate Calculate { get { return calculate; } private set { calculate = value; } }

        #endregion

        #region Override methods

        /// <summary>
        /// Copy options to ninjascript options
        /// </summary>
        /// <param name="options"></param>
        public override void CopyTo(T options)
        {
            // Copy the parent options...
            base.CopyTo(options);

            // Copy the new options
            options.Name = string.IsNullOrEmpty(Name) ? "kRuMoN Script" : Name;
            options.Description = string.IsNullOrEmpty(Description) ? @"Script created by kRuMoN." : Name;
            options.Calculate = calculate;

        }

        /// <summary>
        /// Copy options to ninjatrader properties
        /// </summary>
        /// <param name="ninjascript"></param>
        public override void CopyTo(NinjaScriptBase ninjascript)
        {
            // Copy the parent options
            base.CopyTo(ninjascript);

            // Copy the new options
            ninjascript.Name = Name;
            ninjascript.Calculate = Calculate;
        }

        #endregion

    }
}
