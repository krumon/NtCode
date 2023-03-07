namespace Nt.Core.Ninjascripts.Indicators
{
    public class IndicatorsProvider : INinjascriptsProvider
    {
        public INinjascripts CreateNinjascript(string categoryName)
        {
            return new Indicators();
        }

        public void Dispose()
        {
            
        }
    }
}
