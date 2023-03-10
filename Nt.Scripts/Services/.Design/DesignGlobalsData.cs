using NinjaTrader.Core;
using Nt.Core.Logging;
using System;
using System.IO;

namespace Nt.Scripts.Services.Design
{
    public class DesignGlobalsData : GlobalsData
    {
        public DesignGlobalsData(ILogger<GlobalsData> logger) : base(logger)
        {
        }

        public override void Configure()
        {
            MaxDate = DateTime.MaxValue;
            MinDate = DateTime.MinValue;
            UserConfigureTimeZoneInfo = TimeZoneInfo.Local;
            UserDataDir = Path.Combine(Directory.GetCurrentDirectory(), "User");
            InstallDir = Path.Combine(Directory.GetCurrentDirectory(), "Install");
            CustomSubDirs = null;
        }
    }
}
