using System;

namespace NtCore
{

    /// <summary>
    /// Helper methods of <see cref="InstrumentCode"/> enum.
    /// </summary>
    public static class InstrumentCodeHelpers
    {
        /// <summary>
        /// Method to convert the <see cref="InstrumentCode"/> to description.
        /// </summary>
        /// <param name="type">The instrument code.</param>
        /// <returns>The instrument description.</returns>
        public static string ToName(this InstrumentCode type)
        {

            switch (type)
            {
                case (InstrumentCode.MES):
                    return "MICRO E-MINI S&P 500 INDEX FUTURES";
                default:
                    throw new Exception("The instrument code doesn't exists.");

            }

        }
    }
}
