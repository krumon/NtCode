namespace Nt.Core.Services
{
    internal class NinjascriptHostEnvironment : INinjascriptHostEnvironment
    {
        public string EnvironmentName { get; set; }

        public string ApplicationName { get; set; }

        public string ContentRootPath { get; set; }

        //public IFileProvider ContentRootFileProvider { get; set; }

    }
}
