namespace Nt.Core.Services
{
    internal class NinjascriptsHostEnvironment : INinjascriptsHostEnvironment
    {
        public string EnvironmentName { get; set; }

        public string ApplicationName { get; set; }

        public string ContentRootPath { get; set; }

        //public IFileProvider ContentRootFileProvider { get; set; }

    }
}
