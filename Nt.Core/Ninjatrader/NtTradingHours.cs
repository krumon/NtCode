using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Nt.Core
{
    /// <summary>
    /// Represents the Trading Hours information returned from the current bars series. 
    /// </summary>
    public class NtTradingHours
    {

        /// <summary>
        /// A serializable collection of full holidays configured for a Trading Hours template. Holidays are days which fall outside of the regular trading schedule.
        /// </summary>
        public NtPartialHoliday[] HolidaysSerializable { get; set; }

        /// <summary>
        /// A serializable collection of partial holidays which are configured for a Trading Hours template. 
        /// Holidays are days which fall outside of the normal trading schedule, on which data will be excluded. 
        /// For more information please see the "Understanding trading holidays" section of the Using the Trading Hours window.
        /// </summary>
        public NtPartialHoliday[] PartialHolidaysSerializable { get; set; }

        /// <summary>
        /// Represents the trading hours version
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Represents the name of the specific trading hours.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A collection of session definitions of the configured Trading Hours template.
        /// </summary>
        public Collection<NsSession> Sessions { get; set; }

        /// <summary>
        /// Indicates a time zone that is configured by a Trading Hours template 
        /// </summary>
        public TimeZoneInfo TimeZoneInfo { get; }

        /// <summary>
        /// A collection of full holidays configured for a Trading Hours template. Holidays are days which fall outside of the regular trading schedule.
        /// </summary>
        [XmlIgnore]
        public Dictionary<DateTime, string> Holidays { get; }

        /// <summary>
        /// A collection of partial holidays which are configured for a Trading Hours template. 
        /// Holidays are days which fall outside of the normal trading schedule, on which data will be excluded. 
        /// For more information please see the "Understanding trading holidays" section of the Using the Trading Hours window.
        /// </summary>
        [XmlIgnore]
        public Dictionary<DateTime, NtPartialHoliday> PartialHolidays { get; }

        /// <summary>
        /// Indicates a time zone display name that is configured by a Trading Hour template
        /// </summary>
        [XmlIgnore]
        public string TimeZoneDisplayName { get; set; }

        /// <summary>
        /// Return string format of trading hours object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
