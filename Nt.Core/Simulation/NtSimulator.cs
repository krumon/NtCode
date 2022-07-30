using System;
using System.Timers;

namespace NtCore
{
    public class NtSimulator
    {

        #region Events

        public event Action BarUpdated = () => { };

        #endregion

        #region Fields

        private int interval = 1000;
        private int speedFactor=0;
        
        protected Timer timer;
        protected Bar bar;

        #endregion

        #region Properties

        public bool PrintTimerOnConsole { get; set; } = true;
        public int Interval { get { return interval; } set { interval = value; if (timer != null) timer.Interval = interval; } } 
        public int SpeddFactor { get { return speedFactor; } set { speedFactor = value; } }
        public int Speed => Interval * SpeddFactor;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of <see cref="NtSimulator"/>.
        /// </summary>
        public NtSimulator()
        {
        }

        /// <summary>
        /// Create a new instance of <see cref="NtSimulator"/> with specific parameters.
        /// </summary>
        /// <param name="interval">The interval of time in milliseconds to update the bar.</param>
        /// <param name="speedFactor">The factor to multiply the spedd of the simulation timer.</param>
        public NtSimulator(int interval, int speedFactor)
        {
            this.interval = interval;
            this.speedFactor = speedFactor;
        }

        #endregion

        #region Public methods

        public void Start()
        {
            timer = new Timer()
            {
                Interval = interval,
            };

            bar = new Bar(1, 0, 0, 0, 0, 0, DateTime.Now);

            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;

        }

        public void Dispose()
        {
            timer.Elapsed -= Timer_Elapsed;
            timer.Enabled = false;
            timer.Dispose();
            timer.Close(); // Creo que no es necesario
        }

        #endregion

        #region Private methods

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {

            bar.Time += TimeSpan.FromMilliseconds(Speed);

            if (PrintTimerOnConsole)
            {
                PrintTimer();
            }

            // Call to listeners
            OnBarUpdate(bar);

            // Raise the event
            BarUpdated?.Invoke();
        }

        #endregion

        #region Virtual methods

        public virtual void OnBarUpdate(Bar currentBar)
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t\t-----------------------------------");
            Console.WriteLine("\t\t\t\t\t\t" + bar.Time.ToString());
            Console.WriteLine("\t\t\t\t\t-----------------------------------");
        }

        #endregion

        #region Helper methods

        private void PrintTimer()
        {

        }

        #endregion

    }
}
