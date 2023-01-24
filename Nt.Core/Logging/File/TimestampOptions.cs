using System.IO;
using System.Text;

namespace Nt.Core.Logging.File
{
    public class TimestampOptions
    {
        public bool LogDate { get; set; }
        public bool LogTime { get; set; }  
        public bool LogMilliseconds { get; set; } 
        public bool UseUtcTimestamp { get; set; }   
        public string Timestampformat { get; set; }

        public TimestampOptions() : this(false)
        {
        }
        public TimestampOptions(bool useUtcTimestamp) : this(useUtcTimestamp,true,true)
        {
        }
        public TimestampOptions(bool useUtcTimestamp, bool logDate, bool logTime) : this(useUtcTimestamp,logDate,logTime,false)
        {
        }
        public TimestampOptions(bool useUtcTimestamp, bool logDate, bool logTime, bool logMilliseconds)
        {
            UseUtcTimestamp = useUtcTimestamp;
            LogDate = logDate;
            LogTime = logTime;
            LogMilliseconds = logMilliseconds;
            Timestampformat = GetTimestampFormat(logDate,logTime,logMilliseconds);
        }

        private string GetTimestampFormat(bool includeDate, bool includeTime, bool includeMilliseconds)
        {
            StringWriter format = new StringWriter();

            string dateFormat = "yyyy-MM-DD";
            string timeFormat = "HH:mm:ss";
            string millisecondsFormat = "ffff";

            if (includeDate)
            {
                if (includeTime)
                {
                    if (includeMilliseconds)
                    {
                        format.Write(dateFormat);
                        format.Write(" ");
                        format.Write(timeFormat);
                        format.Write(" ");
                        format.Write(millisecondsFormat);
                    }
                    else
                    {
                        format.Write(dateFormat);
                        format.Write(" ");
                        format.Write(timeFormat);
                    }
                }
                else
                {
                    format.Write(dateFormat);
                }
            }

            else
            {
                if (includeTime)
                {
                    if (includeMilliseconds)
                    {
                        format.Write(timeFormat);
                        format.Write(" ");
                        format.Write(millisecondsFormat);
                    }
                    else
                    {
                        format.Write(timeFormat);
                    }
                }
            }

            StringBuilder sb = format.GetStringBuilder();
            if (sb.Length > 0)
                return format.ToString();
            else
                return string.Empty;
        }
    }
}
