using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Nt.Core.Hosting
{
    //public class NinjascriptHosting
    //{
    //    /// <summary>
    //    /// Provides convenience for creating instances of <see cref="IHostBuilder"/> with preconfigured defaults.
    //    /// </summary>
    //    private INinjascriptHosting ninjascriptHost;

    //    public string displayText;

    //    //public IServiceProvider Services => ninjascriptHost.Services;

    //    /// <summary>
    //    /// Initialize a new instance of the <see cref="HostBuilder"/> class with preconfigured default.
    //    /// </summary>
    //    /// <returns></returns>
    //    private IHostBuilder CreateDefaultBuilder(params string[] args)
    //    {
    //        return Host
    //            .CreateDefaultBuilder(args);
    //            //.UseEnvironment("Development")
    //            //.ConfigureServices(services =>
    //            //{
    //            //    services.AddHostedService<NinjascriptLifetimeHostedService>();
    //            //})
    //            //.ConfigureLogging((builder) =>
    //            //{
    //            //    builder.ClearProviders()
    //            //    .AddNinjascriptConsoleLogger(configuration =>
    //            //    {
    //            //        // Replace warning value from appsettings.json of "Cyan"
    //            //        configuration.LogLevelToColorMap[LogLevel.Warning] = ConsoleColor.Magenta;
    //            //        // Replace warning value from appsettings.jsno of "Red"
    //            //        configuration.LogLevelToColorMap[LogLevel.Error] = ConsoleColor.DarkRed;
    //            //    });
    //            //});
    //    }

    //    /// <summary>
    //    /// Run the given actions to initialize the host. This can only be called once.
    //    /// </summary>
    //    /// <returns>An initialized host.</returns>
    //    private INinjascriptHosting Build(params string[] args)
    //    {
    //        displayText = "The ninjascript host is building...";
    //        return (INinjascriptHosting)CreateDefaultBuilder(args).Build();

    //    }

    //    /// <summary>
    //    /// Run the given actions to initialize the host. This can only be called once.
    //    /// </summary>
    //    /// <returns>An initialized host.</returns>
    //    public void Build()
    //    {
    //        displayText = "The ninjascript host is building...";
    //        ninjascriptHost = Build(null);

    //    }

    //    /// <summary>
    //    /// Runs the <see cref="NinjascriptHosting"/> and returns a <see cref="Task"/> that only completes when 
    //    /// </summary>
    //    /// <returns></returns>
    //    public async Task RunAsync(CancellationToken startToken = default) 
    //    { 
    //        if (!startToken.IsCancellationRequested)
    //        {
    //            displayText = "The ninjascript host is running...";
    //            ninjascriptHost?.RunAsync(startToken);
    //            displayText = "The ninjascript host started.";
    //        }

    //        await Task.CompletedTask;
    //    }

    //    /// <summary>
    //    /// Start the ninjascript host.
    //    /// </summary>
    //    /// <returns>A <see cref="Task"/> that will be completed when the <see cref="NinjascriptHosting"/> starts.</returns>
    //    //public async Task StartAsync() => await ninjascriptHost?.StartAsync();

    //    /// <summary>
    //    /// Attempts to gracefully stop the <see cref="NinjascriptHosting"/>.
    //    /// </summary>
    //    /// <returns>A <see cref="Task"/> that will be completed when the <see cref="NinjascriptHosting"/> stops.</returns>
    //    //public async Task StopAsync() 
    //    //{
    //    //    displayText = "The ninjascript host is stopping...";
    //    //    await ninjascriptHost?.StopAsync();
    //    //    displayText = "The ninjascript host stopped.";
    //    //}

    //    public async Task StartAsync(CancellationToken cancellationToken = default)
    //    {
    //        displayText = "The ninjascript host is stopping...";

    //        if (!cancellationToken.IsCancellationRequested)
    //            await ninjascriptHost?.StartAsync(cancellationToken);

    //    }

    //    public Task StopAsync(CancellationToken cancellationToken = default)
    //    {
    //        displayText = "The ninjascript host is stopping...";

    //        if (!cancellationToken.IsCancellationRequested)
    //            ninjascriptHost?.StopAsync(cancellationToken);

    //        displayText = "The ninjascript host stopped.";

    //        return Task.CompletedTask;
    //    }

    //    /// <summary>
    //    /// Do tasks defined by the user to free the unmanagment resources.
    //    /// </summary>
    //    public void Dispose()
    //    {
    //        displayText = "The ninjascript host is disposing...";
    //        ninjascriptHost?.Dispose();
    //        displayText = "The ninjascript host disposed.";
    //    }
    //}
}
