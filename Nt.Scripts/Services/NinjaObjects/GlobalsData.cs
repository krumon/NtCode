using NinjaTrader.Core;
using Nt.Core.Logging;
using System;
using System.IO;

namespace Nt.Scripts.Services
{
    public class GlobalsData : IGlobalsData
    {
        private readonly ILogger _logger;
        private readonly bool _isInDesingnMode = false;

        public DateTime MaxDate {get;protected set;}
        public DateTime MinDate { get; protected set; }
        public TimeZoneInfo UserConfigureTimeZoneInfo { get; protected set; }
        public string UserDataDir { get; protected set; }
        public string InstallDir { get; protected set; }
        public string[] CustomSubDirs { get; protected set; }

        public bool IsConfigured => throw new NotImplementedException();

        public GlobalsData(ILogger<GlobalsData> logger)
        {
            _logger = logger;
            Initialize();
            // TODO: Delete. Is a testing method.
            PrintDirectories();
        }

        public virtual void Initialize()
        {
            MaxDate = Globals.MaxDate;
            MinDate = Globals.MinDate;
            UserConfigureTimeZoneInfo = Globals.GeneralOptions.TimeZoneInfo;
            UserDataDir = Globals.UserDataDir;
            InstallDir = Globals.InstallDir;
            CustomSubDirs = Globals.CustomSubDirs;
        }

        protected void PrintDirectories()
        {
            _logger.LogInformation("Install Directory: {0}", InstallDir);
            _logger.LogInformation("User Directory: {0}", UserDataDir);
            if (CustomSubDirs != null && CustomSubDirs.Length > 0)
                _logger.LogInformation("Subdirectory[0]: {0}", CustomSubDirs[0].ToString());
        }
    }
}

