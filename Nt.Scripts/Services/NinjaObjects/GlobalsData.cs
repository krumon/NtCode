using NinjaTrader.Core;
using Nt.Core.Logging;
using System;
using System.IO;

namespace Nt.Scripts.Services
{
    public class GlobalsData : IGlobalsData
    {
        private readonly ILogger _logger;
        private DateTime _maxDate;
        private DateTime _minDate;
        private TimeZoneInfo _userConfigureTimeZoneInfo;
        private string _userDataDir;
        private string _installDir;
        private string[] _customSubDirs;


        public DateTime MaxDate => Globals.MaxDate;
        public DateTime MinDate => Globals.MinDate;
        public TimeZoneInfo UserConfigureTimeZoneInfo => Globals.GeneralOptions.TimeZoneInfo;
        public string UserDataDir => Globals.UserDataDir;
        public string InstallDir => Globals.InstallDir;
        public string[] CustomSubDirs => Globals.CustomSubDirs;

        protected GlobalsData(ILogger<GlobalsData> logger, bool designMode = true)
        {
            _logger = logger;
            if (designMode)
                DesignConfigure();
            else
                Configure();
        }

        public GlobalsData(ILogger<GlobalsData> logger)
        {
            _logger = logger;
            Configure();
            PrintDirectories();
        }

        private void Configure()
        {
            _maxDate = Globals.MaxDate;
            _minDate = Globals.MinDate;
            _userConfigureTimeZoneInfo = Globals.GeneralOptions.TimeZoneInfo;
            _userDataDir = Globals.UserDataDir;
            _installDir = Globals.InstallDir;
            _customSubDirs = Globals.CustomSubDirs;
        }

        private void DesignConfigure()
        {
            _maxDate = DateTime.MaxValue;
            _minDate = DateTime.MinValue;
            _userConfigureTimeZoneInfo = Globals.GeneralOptions.TimeZoneInfo;
            _userDataDir = Path.Combine(Directory.GetCurrentDirectory(),"User");
            _installDir = Path.Combine(Directory.GetCurrentDirectory(),"Install");
            _customSubDirs = null;
        }

        private void PrintDirectories()
        {
            _logger.LogInformation(_maxDate.ToString());
            _logger.LogInformation(_minDate.ToString());
            _logger.LogInformation(_userConfigureTimeZoneInfo.ToString());
            _logger.LogInformation(_userDataDir.ToString());
            _logger.LogInformation(_installDir.ToString());
            _logger.LogInformation("Install Directory: {0}", InstallDir);
            _logger.LogInformation("User Directory: {0}", UserDataDir);
            if (CustomSubDirs != null && CustomSubDirs.Length > 0)
                _logger.LogInformation("Subdirectory[0]: {0}", CustomSubDirs[0].ToString());
        }
    }
}

