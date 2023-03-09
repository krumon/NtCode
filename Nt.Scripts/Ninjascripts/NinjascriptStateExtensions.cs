namespace Nt.Scripts.Ninjascripts
{
    /// <summary>
    /// Extension methods for convert values 'from' or 'to' <see cref="NinjascriptState"/> enum.
    /// </summary>
    public static class NinjascriptStateExtensions
    {

        public static NinjascriptLevel ToNinjascriptLevel(this NinjascriptState state)
        {
            switch (state)
            {
                case NinjascriptState.SetDefaults:
                case NinjascriptState.Configure:
                case NinjascriptState.Active:
                case NinjascriptState.DataLoaded:
                    return NinjascriptLevel.Active;
                case NinjascriptState.Historical:
                case NinjascriptState.Transition:
                    return NinjascriptLevel.Historical;
                default:
                    return NinjascriptLevel.Disable;
            }
        }
    }
}
