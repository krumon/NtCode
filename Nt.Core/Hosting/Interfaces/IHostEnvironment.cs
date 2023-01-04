using Nt.Core.FileProviders;

namespace Nt.Core.Hosting
{
    //
    // Resumen:
    //     Provides information about the hosting environment an application is running
    //     in.
    public interface IHostEnvironment
    {
        //
        // Resumen:
        //     Gets or sets the name of the environment. The host automatically sets this property
        //     to the value of the "environment" key as specified in configuration.
        string EnvironmentName { get; set; }

        //
        // Resumen:
        //     Gets or sets the name of the application. This property is automatically set
        //     by the host to the assembly containing the application entry point.
        string ApplicationName { get; set; }

        //
        // Resumen:
        //     Gets or sets the absolute path to the directory that contains the application
        //     content files.
        string ContentRootPath { get; set; }

        //
        // Resumen:
        //     Gets or sets an Microsoft.Extensions.FileProviders.IFileProvider pointing at
        //     Microsoft.Extensions.Hosting.IHostEnvironment.ContentRootPath.
        IFileProvider ContentRootFileProvider { get; set; }
    }
}
