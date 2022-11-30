namespace Nt.Core.Data
{
    public class BarsPeriod
    {
        #region Private members

        private int _periodValue; 

        #endregion

        #region Public properties

        public PeriodType PeriodType { get; set; }

        public int PeriodValue 
        {
            get => _periodValue;
            set
            {
                if (_periodValue != value)
                {
                    if (value < 1)
                        return;

                    _periodValue = value;
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
            PeriodType = type;
            PeriodValue = period;
        }

        #endregion
    }
}
