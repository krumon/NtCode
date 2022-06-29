using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtCore.Time
{
    public class TimeTarget
    {

        #region Public properties

        /// <summary>
        /// Gets or sets the time zone info of the <see cref="TimeTarget"/>.
        /// </summary>
        TimeZoneInfo TimeZone { get; set; }

        /// <summary>
        /// Gets or sets the time of the <see cref="TimeTarget"/>.
        /// </summary>
        DateTime Time { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default instance of <see cref="TimeTarget"/> object.
        /// </summary>
        public TimeTarget()
        {
        }

        #endregion



    }
}
