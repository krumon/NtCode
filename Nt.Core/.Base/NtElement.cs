namespace NtCore
{
    /// <summary>
    /// Base ninjatrader element
    /// </summary>
    public abstract class NtElement
    {
        /// <summary>
        /// The element id or index
        /// </summary>
        public int Idx { get; set; }

        /// <summary>
        /// Fre the unmagnement memory of any element.
        /// </summary>
        public virtual void Dispose() { }

    }
}
