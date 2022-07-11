using System;
using System.Linq;

namespace NtCore
{

    /// <summary>
    /// Helper methods of <see cref="SpecificSessionHours"/> enum.
    /// </summary>
    public static class SessionHoursHelpers
    {
        /// <summary>
        /// Method to convert the <see cref="SpecificSessionHours"/> to initial <see cref="SessionTime"/>.
        /// </summary>
        /// <param name="specificSessionHours"></param>
        /// <returns>Initial <see cref="SessionTime"/> of the <see cref="SpecificSessionHours"/>.</returns>
        public static SessionTime ToBeginSessionTime(this SpecificSessionHours specificSessionHours, InstrumentCode instrumentCode = InstrumentCode.Default)
        {

            switch (instrumentCode)
            {
                case (InstrumentCode.Default):
                case (InstrumentCode.MES):
                    {
                        switch (specificSessionHours)
                        {

                            case (SpecificSessionHours.Electronic):
                                return SpecificSessionTime.Electronic_Open.ToSessionTime(instrumentCode);
                            case (SpecificSessionHours.Electronic_IB):
                                return SpecificSessionTime.Electronic_IB_Open.ToSessionTime(instrumentCode);
                            case (SpecificSessionHours.Electronic_FB):
                                return SpecificSessionTime.Electronic_FB_Open.ToSessionTime(instrumentCode);

                            case (SpecificSessionHours.DAY):
                                return SpecificSessionTime.DAY_Open.ToSessionTime(instrumentCode);
                            case (SpecificSessionHours.DAY_IB):
                                return SpecificSessionTime.DAY_IB_Open.ToSessionTime(instrumentCode);
                            case (SpecificSessionHours.DAY_FB):
                                return SpecificSessionTime.DAY_FB_Open.ToSessionTime(instrumentCode);

                            case (SpecificSessionHours.Regular):
                                return SpecificSessionTime.Regular_Open.ToSessionTime(instrumentCode);
                            case (SpecificSessionHours.Regular_IB):
                                return SpecificSessionTime.Regular_IB_Open.ToSessionTime(instrumentCode);
                            case (SpecificSessionHours.Regular_FB):
                                return SpecificSessionTime.Regular_FB_Open.ToSessionTime(instrumentCode);

                            case (SpecificSessionHours.OVN):
                                return SpecificSessionTime.OVN_Open.ToSessionTime(instrumentCode);
                            case (SpecificSessionHours.OVN_IB):
                                return SpecificSessionTime.OVN_IB_Open.ToSessionTime(instrumentCode);
                            case (SpecificSessionHours.OVN_FB):
                                return SpecificSessionTime.Regular_FB_Open.ToSessionTime(instrumentCode);

                            case (SpecificSessionHours.American):
                                return SpecificSessionTime.American_Open.ToSessionTime();
                            case (SpecificSessionHours.American_IB):
                                return SpecificSessionTime.American_IB_Open.ToSessionTime();
                            case (SpecificSessionHours.American_FB):
                                return SpecificSessionTime.American_FB_Open.ToSessionTime();

                            case (SpecificSessionHours.AmericanAndEuropean):
                                return SpecificSessionTime.AmericanAndEuropean_Open.ToSessionTime();
                            case (SpecificSessionHours.AmericanAndEuropean_IB):
                                return SpecificSessionTime.AmericanAndEuropean_IB_Open.ToSessionTime();
                            case (SpecificSessionHours.AmericanAndEuropean_FB):
                                return SpecificSessionTime.AmericanAndEuropean_FB_Open.ToSessionTime();

                            case (SpecificSessionHours.Asian):
                                return SpecificSessionTime.Asian_Open.ToSessionTime();
                            case (SpecificSessionHours.Asian_IB):
                                return SpecificSessionTime.Asian_IB_Open.ToSessionTime();
                            case (SpecificSessionHours.Asian_FB):
                                return SpecificSessionTime.Asian_FB_Open.ToSessionTime();

                            case (SpecificSessionHours.European):
                                return SpecificSessionTime.European_Open.ToSessionTime();
                            case (SpecificSessionHours.European_IB):
                                return SpecificSessionTime.European_IB_Open.ToSessionTime();
                            case (SpecificSessionHours.European_FB):
                                return SpecificSessionTime.European_FB_Open.ToSessionTime();

                            default:
                                throw new Exception("The specific trading hours doesn't exists.");

                        }
                    }

                default:
                    throw new Exception("The instrument code doesn't exists.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="SpecificSessionHours"/> to final <see cref="SessionTime"/>.
        /// </summary>
        /// <param name="specificSessionHours"></param>
        /// <returns>Final <see cref="SessionTime"/> of the <see cref="SpecificSessionHours"/>.</returns>
        public static SessionTime ToEndSessionTime(this SpecificSessionHours specificSessionHours, InstrumentCode instrumentCode = InstrumentCode.Default)
        {

            switch (instrumentCode)
            {
                case (InstrumentCode.Default):
                case (InstrumentCode.MES):
                    {
                        switch (specificSessionHours)
                        {

                            case (SpecificSessionHours.Electronic):
                                return SpecificSessionTime.Electronic_Close.ToSessionTime(instrumentCode);
                            case (SpecificSessionHours.Electronic_IB):
                                return SpecificSessionTime.Electronic_IB_Close.ToSessionTime(instrumentCode);
                            case (SpecificSessionHours.Electronic_FB):
                                return SpecificSessionTime.Electronic_FB_Close.ToSessionTime(instrumentCode);

                            case (SpecificSessionHours.DAY):
                                return SpecificSessionTime.DAY_Close.ToSessionTime(instrumentCode);
                            case (SpecificSessionHours.DAY_IB):
                                return SpecificSessionTime.DAY_IB_Close.ToSessionTime(instrumentCode);
                            case (SpecificSessionHours.DAY_FB):
                                return SpecificSessionTime.DAY_FB_Close.ToSessionTime(instrumentCode);

                            case (SpecificSessionHours.Regular):
                                return SpecificSessionTime.Regular_Close.ToSessionTime(instrumentCode);
                            case (SpecificSessionHours.Regular_IB):
                                return SpecificSessionTime.Regular_IB_Close.ToSessionTime(instrumentCode);
                            case (SpecificSessionHours.Regular_FB):
                                return SpecificSessionTime.Regular_FB_Close.ToSessionTime(instrumentCode);

                            case (SpecificSessionHours.OVN):
                                return SpecificSessionTime.OVN_Close.ToSessionTime(instrumentCode);
                            case (SpecificSessionHours.OVN_IB):
                                return SpecificSessionTime.OVN_IB_Close.ToSessionTime(instrumentCode);
                            case (SpecificSessionHours.OVN_FB):
                                return SpecificSessionTime.Regular_FB_Close.ToSessionTime(instrumentCode);

                            case (SpecificSessionHours.American):
                                return SpecificSessionTime.American_Close.ToSessionTime();
                            case (SpecificSessionHours.American_IB):
                                return SpecificSessionTime.American_IB_Close.ToSessionTime();
                            case (SpecificSessionHours.American_FB):
                                return SpecificSessionTime.American_FB_Close.ToSessionTime();

                            case (SpecificSessionHours.AmericanAndEuropean):
                                return SpecificSessionTime.AmericanAndEuropean_Close.ToSessionTime();
                            case (SpecificSessionHours.AmericanAndEuropean_IB):
                                return SpecificSessionTime.AmericanAndEuropean_IB_Close.ToSessionTime();
                            case (SpecificSessionHours.AmericanAndEuropean_FB):
                                return SpecificSessionTime.AmericanAndEuropean_FB_Close.ToSessionTime();

                            case (SpecificSessionHours.Asian):
                                return SpecificSessionTime.Asian_Close.ToSessionTime();
                            case (SpecificSessionHours.Asian_IB):
                                return SpecificSessionTime.Asian_IB_Close.ToSessionTime();
                            case (SpecificSessionHours.Asian_FB):
                                return SpecificSessionTime.Asian_FB_Close.ToSessionTime();

                            case (SpecificSessionHours.European):
                                return SpecificSessionTime.European_Close.ToSessionTime();
                            case (SpecificSessionHours.European_IB):
                                return SpecificSessionTime.European_IB_Close.ToSessionTime();
                            case (SpecificSessionHours.European_FB):
                                return SpecificSessionTime.European_FB_Close.ToSessionTime();

                            default:
                                throw new Exception("The specific trading hours doesn't exists.");

                        }
                    }

                default:
                    throw new Exception("The instrument code doesn't exists.");
            }

        }

        public static SessionHours ToSessionHours(this SpecificSessionHours type)
        {
            return SessionHours.CreateSessionHoursByType(type);
        }

        public static SpecificSessionHours[] ToArray(this SpecificSessionHours type)
        {
            return Enum.GetValues(typeof(SpecificSessionHours)).Cast<SpecificSessionHours>().ToArray();
        }
    }
}
