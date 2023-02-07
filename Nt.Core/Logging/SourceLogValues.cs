using Nt.Core.Logging.Internal;

namespace Nt.Core.Logging
{
    public class SourceLogValues
    {

        private const string NullFormat = "[null]";
        private readonly LogValuesSourceFormatter _formatter;

        internal LogValuesSourceFormatter Formatter => _formatter;

        public SourceLogValues(string memberName, string filePath, int lineNumber)
        {
            _formatter = new LogValuesSourceFormatter(memberName, filePath, lineNumber);
        }

        public override string ToString()
        {
            if (_formatter == null)
            {
                return NullFormat;
            }

            return _formatter.Format();
        }

    }
}
