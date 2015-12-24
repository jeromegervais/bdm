using System;
using System.Collections.Generic;

namespace BDM.App.Shared.Log
{
    /// <summary>
    /// interface commune pour les loggers
    /// </summary>
    public interface ILogger
    {
        void Debug(IEnumerable<KeyValuePair<string, string>> data);
        void Info(IEnumerable<KeyValuePair<string, string>> data);
        void Error(IEnumerable<KeyValuePair<string, string>> data);
        void Fatal(IEnumerable<KeyValuePair<string, string>> data);
        void Debug(string message);
        void Info(string message);
        void Error(string message);
        void Fatal(string message);
        void Debug(Exception exception);
        void Info(Exception exception);
        void Error(Exception exception);
        void Fatal(Exception exception);
    }
}
