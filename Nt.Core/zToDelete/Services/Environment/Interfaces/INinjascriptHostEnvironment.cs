namespace Nt.Core.Services
{
    public interface INinjascriptHostEnvironment
    {

        string EnvironmentName { get; set; }

        string ApplicationName { get; set; }

        string ContentRootPath { get; set; }

        //IFileProvider ContentRootFileProvider { get; set; }

    }
}