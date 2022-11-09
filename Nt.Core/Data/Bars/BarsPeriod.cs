namespace Nt.Core.Data
{
    public class BarsPeriod
    {
        #region Private members

        private int _period; 

        #endregion

        #region Public properties

        public PeriodType Type { get; set; }

        public int Period 
        {
            get => _period;
            set
            {
                if (_period != value)
                {
                    if (value < 1)
                        return;

                    _period = value;
                }
            }
        }

        #endregion

        #region Constructors

        public BarsPeriod() : this(PeriodType.Minute,1)
        {
        }

        public BarsPeriod(PeriodType type) : this(type,1)
        {
        }

        public BarsPeriod(int period) : this(PeriodType.Minute,period)
        {
        }

        public BarsPeriod(PeriodType type, int period)
        {
            Type = type;
            Period = period;
        }

        #endregion
    }
}
