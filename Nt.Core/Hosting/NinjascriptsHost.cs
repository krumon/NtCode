namespace Nt.Core.Hosting
{
    public static class NinjascriptsHost
    {

        public static INinjascriptHostBuilder CreateNinjascriptHostBuilder()
        {
            return new NinjascriptHostBuilder();
        }
    }
}
