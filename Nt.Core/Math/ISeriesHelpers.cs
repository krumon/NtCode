using NinjaTrader.NinjaScript;
using System;

namespace Nt.Core
{
    /// <summary>
    /// Extension or helper methods of <see cref="ISeries{T}"/> class.
    /// </summary>
    public static class ISeriesHelpers
    {

        public static double Sum(this ISeries<double> serie)
        {
            double sum = 0.0;

            for (int i = 0; i<serie.Count; i++)
            {
                sum += serie.GetValueAt(i);
            }
            
            return sum;
        }

        public static double Average(this ISeries<double> serie)
        {
            return serie.Sum()/serie.Count;
        }
        
        public static double GetStandardDesviation(this ISeries<double> serie)
        {
            double average = serie.Average();
            double cuadraticsDifferenceSum = 0.0;

            for (int i = 0; i<serie.Count; i++)
            {
                cuadraticsDifferenceSum += Math.Pow((serie.GetValueAt(i)-average),2);
            }
           
            return Math.Sqrt(cuadraticsDifferenceSum);
        }        
    }
}
