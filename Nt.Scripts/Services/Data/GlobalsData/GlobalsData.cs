using NinjaTrader.Core;
using Nt.Core.Logging;
using System;

namespace Nt.Scripts.Services
{
    public abstract class GlobalsData : IGlobalsData
    {

        public DateTime MaxDate => Globals.MaxDate;
        public DateTime MinDate => Globals.MinDate;
        public TimeZoneInfo UserConfigureTimeZoneInfo => Globals.GeneralOptions.TimeZoneInfo;
        public string UserDataDir => Globals.UserDataDir;
        public string InstallDir => Globals.InstallDir;
        public string[] CustomSubDirs => Globals.CustomSubDirs;

        public GlobalsData(ILogger<GlobalsData> logger)
        {
            logger.LogInformation("Install Directory: {0}",InstallDir);
            logger.LogInformation("User Directory: {0}",UserDataDir);
            if (CustomSubDirs != null && CustomSubDirs.Length>0)
                logger.LogInformation("Subdirectory[0]: {0}", CustomSubDirs[0].ToString());
        }

    }
}
