using System;
using System.Collections.Generic;
using System.Linq;

namespace BDM.App.Shared.Log
{
    /// <summary>
    /// Logger de debug, utilisant System.Diagnostics.Debug
    /// </summary>
    internal class DefaultLogger : Logger
    {
        protected override void Write(LogLevel logLevel, IEnumerable<KeyValuePair<string, string>> data)
        {
            if (logLevel < LogManager.MinLevel)
                return;

            System.Diagnostics.Debug.WriteLine(string.Join(Environment.NewLine, GetParams(logLevel, data).Select(x => x.Key + ":" + x.Value)));
        }
    }
}
