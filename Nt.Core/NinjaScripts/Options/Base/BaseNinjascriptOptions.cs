﻿using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// The script options
    /// </summary>
    public abstract class BaseNinjascriptOptions<TOptions> : BaseOptions<TOptions>, INinjascriptOptions<TOptions>
        where TOptions : BaseNinjascriptOptions<TOptions>, new()
    {

        #region Private members / Default values

        /// <summary>
        /// Represents the ninjascript description.
        /// </summary>
        private string description;

        /// <summary>
        /// Represents the ninjascript name.
        /// </summary>
        private string name;

        /// <summary>
        /// Represents the ninjascript calculate mode.
        /// </summary>
        private Calculate calculate = Calculate.OnBarClose;

        /// <summary>
        /// Represents the minimum bars required to plot.
        /// </summary>
        private int barsRequiredToPlot;

        #endregion

        #region Properties

        /// <summary>
        /// Represents the ninjascript description.
        /// </summary>
        public string Description { get { return description; } set { description = value; } }

        /// <summary>
        /// Represents the ninjascript name.
        /// </summary>
        public string Name { get { return name; } set { name = value; } }

        /// <summary>
        /// Represents the ninjascript calculate mode.
        /// </summary>
        public Calculate Calculate { get { return calculate; } set { calculate = value; } }

        /// <summary>
        /// Represents the minimum bars required to plot.
        /// </summary>
        public int BarsRequiredToPlot { get { return barsRequiredToPlot; } set { barsRequiredToPlot = value; } }

        #endregion

        #region Public methods

        /// <summary>
        /// Copy options to ninjascript options
        /// </summary>
        /// <param name="options"></param>
        public override void CopyTo(TOptions options)
        {
            // Copy the new options
            options.Name = string.IsNullOrEmpty(Name) ? "kRuMoN Script" : Name;
            options.Description = string.IsNullOrEmpty(Description) ? @"Script created by kRuMoN." : Description;
            options.Calculate = Calculate;
            options.BarsRequiredToPlot = BarsRequiredToPlot;
        }

        /// <summary>
        /// Copy options to ninjascript options
        /// </summary>
        /// <param name="options"></param>
        public virtual void CopyTo<TScript>(TScript script, TOptions options)
            where TScript : BaseNinjascript<TScript, TOptions>, new()
        {
            // Copy the new options
            script.Options.Name = string.IsNullOrEmpty(Name) ? "kRuMoN Script" : Name;
            script.Options.Description = string.IsNullOrEmpty(Description) ? @"Script created by kRuMoN." : Description;
            script.Options.Calculate = Calculate;
            script.Options.BarsRequiredToPlot = BarsRequiredToPlot;
        }

        public void CopyToNinjascript(NinjaScriptBase ninjascript)
        {
            // Copy the new options
            ninjascript.Name = Name;
            ninjascript.Calculate = Calculate;
            ninjascript.BarsRequiredToPlot = BarsRequiredToPlot;
        }

        #endregion

    }

    /// <summary>
    /// The script options
    /// </summary>
    public class NinjascriptOptions : BaseNinjascriptOptions<NinjascriptOptions>, INinjascriptOptions
    {
        public void CopyTo(INinjascriptOptions options)
        {
            base.CopyTo((NinjascriptOptions)options);
        }
    }
}