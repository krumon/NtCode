using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Nt.Core.Logging.Internal
{
    /// <summary>
    /// Formatter to convert the named format items like {NamedformatItem} to <see cref="string.Format(IFormatProvider, string, object)"/> format.
    /// </summary>
    internal sealed class LogValuesSourceFormatter
    {
        private const string NullValue = "(null)";
        private readonly string _message;

        public LogValuesSourceFormatter(string message)
        {
            _message = message;
        }

        public string Format(object[] values)
        {
            object[] sourceValues = values;
            string origin = NullValue;
            string filePath = NullValue;
            int lineNumber = 0;
            string message = NullValue;
            if (values != null && values.Length > 0)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            origin = (string)sourceValues[i];
                            break;
                        case 1:
                            filePath = (string)sourceValues[i];
                            break;
                        case 2:
                            lineNumber = (int)sourceValues[i];
                            break;
                        case 3:
                            message = (string)sourceValues[i];
                            break;
                        default:
                            break;
                    }
                }
            }

            TextWriter message = new StringWriter();
            if (string.IsNullOrEmpty(message))
            // Format the message string
            return $"{message} [{Path.GetFileName(filePath)} > {origin}() > Line {lineNumber}]";
        }
    }
}
