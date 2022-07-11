using System;
using System.Linq;

namespace NtCore
{

    /// <summary>
    /// Helper methods of <see cref="SpecificTradingHours"/> enum.
    /// </summary>
    public static class TradingHoursHelpers
    {
        /// <summary>
        /// Method to convert the <see cref="SpecificTradingHours"/> to initial <see cref="TradingTime"/>.
        /// </summary>
        /// <param name="specificTradingHours"></param>
        /// <returns>Initial <see cref="TradingTime"/> of the <see cref="SpecificTradingHours"/>.</returns>
        public static TradingTime ToBeginTradingTime(this SpecificTradingHours specificTradingHours, InstrumentCode instrumentCode = InstrumentCode.Default)
        {

            switch (instrumentCode)
            {
                case (InstrumentCode.Default):
                case (InstrumentCode.MES):
                    {
                        switch (specificTradingHours)
                        {

                            case (SpecificTradingHours.Electronic):
                                return SpecificTradingTime.Electronic_Open.ToTradingTime(instrumentCode);
                            case (SpecificTradingHours.Electronic_IB):
                                return SpecificTradingTime.Electronic_IB_Open.ToTradingTime(instrumentCode);
                            case (SpecificTradingHours.Electronic_FB):
                                return SpecificTradingTime.Electronic_FB_Open.ToTradingTime(instrumentCode);

                            case (SpecificTradingHours.DAY):
                                return SpecificTradingTime.DAY_Open.ToTradingTime(instrumentCode);
                            case (SpecificTradingHours.DAY_IB):
                                return SpecificTradingTime.DAY_IB_Open.ToTradingTime(instrumentCode);
                            case (SpecificTradingHours.DAY_FB):
                                return SpecificTradingTime.DAY_FB_Open.ToTradingTime(instrumentCode);

                            case (SpecificTradingHours.Regular):
                                return SpecificTradingTime.Regular_Open.ToTradingTime(instrumentCode);
                            case (SpecificTradingHours.Regular_IB):
                                return SpecificTradingTime.Regular_IB_Open.ToTradingTime(instrumentCode);
                            case (SpecificTradingHours.Regular_FB):
                                return SpecificTradingTime.Regular_FB_Open.ToTradingTime(instrumentCode);

                            case (SpecificTradingHours.OVN):
                                return SpecificTradingTime.OVN_Open.ToTradingTime(instrumentCode);
                            case (SpecificTradingHours.OVN_IB):
                                return SpecificTradingTime.OVN_IB_Open.ToTradingTime(instrumentCode);
                            case (SpecificTradingHours.OVN_FB):
                                return SpecificTradingTime.Regular_FB_Open.ToTradingTime(instrumentCode);

                            case (SpecificTradingHours.American):
                                return SpecificTradingTime.American_Open.ToTradingTime();
                            case (SpecificTradingHours.American_IB):
                                return SpecificTradingTime.American_IB_Open.ToTradingTime();
                            case (SpecificTradingHours.American_FB):
                                return SpecificTradingTime.American_FB_Open.ToTradingTime();

                            case (SpecificTradingHours.AmericanAndEuropean):
                                return SpecificTradingTime.AmericanAndEuropean_Open.ToTradingTime();
                            case (SpecificTradingHours.AmericanAndEuropean_IB):
                                return SpecificTradingTime.AmericanAndEuropean_IB_Open.ToTradingTime();
                            case (SpecificTradingHours.AmericanAndEuropean_FB):
                                return SpecificTradingTime.AmericanAndEuropean_FB_Open.ToTradingTime();

                            case (SpecificTradingHours.Asian):
                                return SpecificTradingTime.Asian_Open.ToTradingTime();
                            case (SpecificTradingHours.Asian_IB):
                                return SpecificTradingTime.Asian_IB_Open.ToTradingTime();
                            case (SpecificTradingHours.Asian_FB):
                                return SpecificTradingTime.Asian_FB_Open.ToTradingTime();

                            case (SpecificTradingHours.European):
                                return SpecificTradingTime.European_Open.ToTradingTime();
                            case (SpecificTradingHours.European_IB):
                                return SpecificTradingTime.European_IB_Open.ToTradingTime();
                            case (SpecificTradingHours.European_FB):
                                return SpecificTradingTime.European_FB_Open.ToTradingTime();

                            default:
                                throw new Exception("The specific trading hours doesn't exists.");

                        }
                    }

                default:
                    throw new Exception("The instrument code doesn't exists.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="SpecificTradingHours"/> to final <see cref="TradingTime"/>.
        /// </summary>
        /// <param name="specificTradingHours"></param>
        /// <returns>Final <see cref="TradingTime"/> of the <see cref="SpecificTradingHours"/>.</returns>
        public static TradingTime ToEndTradingTime(this SpecificTradingHours specificTradingHours, InstrumentCode instrumentCode = InstrumentCode.Default)
        {

            switch (instrumentCode)
            {
                case (InstrumentCode.Default):
                case (InstrumentCode.MES):
                    {
                        switch (specificTradingHours)
                        {

                            case (SpecificTradingHours.Electronic):
                                return SpecificTradingTime.Electronic_Close.ToTradingTime(instrumentCode);
                            case (SpecificTradingHours.Electronic_IB):
                                return SpecificTradingTime.Electronic_IB_Close.ToTradingTime(instrumentCode);
                            case (SpecificTradingHours.Electronic_FB):
                                return SpecificTradingTime.Electronic_FB_Close.ToTradingTime(instrumentCode);

                            case (SpecificTradingHours.DAY):
                                return SpecificTradingTime.DAY_Close.ToTradingTime(instrumentCode);
                            case (SpecificTradingHours.DAY_IB):
                                return SpecificTradingTime.DAY_IB_Close.ToTradingTime(instrumentCode);
                            case (SpecificTradingHours.DAY_FB):
                                return SpecificTradingTime.DAY_FB_Close.ToTradingTime(instrumentCode);

                            case (SpecificTradingHours.Regular):
                                return SpecificTradingTime.Regular_Close.ToTradingTime(instrumentCode);
                            case (SpecificTradingHours.Regular_IB):
                                return SpecificTradingTime.Regular_IB_Close.ToTradingTime(instrumentCode);
                            case (SpecificTradingHours.Regular_FB):
                                return SpecificTradingTime.Regular_FB_Close.ToTradingTime(instrumentCode);

                            case (SpecificTradingHours.OVN):
                                return SpecificTradingTime.OVN_Close.ToTradingTime(instrumentCode);
                            case (SpecificTradingHours.OVN_IB):
                                return SpecificTradingTime.OVN_IB_Close.ToTradingTime(instrumentCode);
                            case (SpecificTradingHours.OVN_FB):
                                return SpecificTradingTime.Regular_FB_Close.ToTradingTime(instrumentCode);

                            case (SpecificTradingHours.American):
                                return SpecificTradingTime.American_Close.ToTradingTime();
                            case (SpecificTradingHours.American_IB):
                                return SpecificTradingTime.American_IB_Close.ToTradingTime();
                            case (SpecificTradingHours.American_FB):
                                return SpecificTradingTime.American_FB_Close.ToTradingTime();

                            case (SpecificTradingHours.AmericanAndEuropean):
                                return SpecificTradingTime.AmericanAndEuropean_Close.ToTradingTime();
                            case (SpecificTradingHours.AmericanAndEuropean_IB):
                                return SpecificTradingTime.AmericanAndEuropean_IB_Close.ToTradingTime();
                            case (SpecificTradingHours.AmericanAndEuropean_FB):
                                return SpecificTradingTime.AmericanAndEuropean_FB_Close.ToTradingTime();

                            case (SpecificTradingHours.Asian):
                                return SpecificTradingTime.Asian_Close.ToTradingTime();
                            case (SpecificTradingHours.Asian_IB):
                                return SpecificTradingTime.Asian_IB_Close.ToTradingTime();
                            case (SpecificTradingHours.Asian_FB):
                                return SpecificTradingTime.Asian_FB_Close.ToTradingTime();

                            case (SpecificTradingHours.European):
                                return SpecificTradingTime.European_Close.ToTradingTime();
                            case (SpecificTradingHours.European_IB):
                                return SpecificTradingTime.European_IB_Close.ToTradingTime();
                            case (SpecificTradingHours.European_FB):
                                return SpecificTradingTime.European_FB_Close.ToTradingTime();

                            default:
                                throw new Exception("The specific trading hours doesn't exists.");

                        }
                    }

                default:
                    throw new Exception("The instrument code doesn't exists.");
            }

        }

        public static TradingHours ToTradingHours(this SpecificTradingHours type)
        {
            return TradingHours.CreateTradingHoursByType(type);
        }

        public static SpecificTradingHours[] ToArray(this SpecificTradingHours type)
        {
            return Enum.GetValues(typeof(SpecificTradingHours)).Cast<SpecificTradingHours>().ToArray();
        }
    }
}
