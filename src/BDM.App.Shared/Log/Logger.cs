using System;
using System.Collections.Generic;

namespace BDM.App.Shared.Log
{
    /// <summary>
    /// classe de base de tous les loggers.
    /// </summary>
    internal abstract class Logger : ILogger
    {
        
        public void Debug(IEnumerable<KeyValuePair<string, string>> data)
        {
            Write(LogLevel.Debug, data);
        }
        public void Info(IEnumerable<KeyValuePair<string, string>> data)
        {
            Write(LogLevel.Info, data);
        }
        public void Error(IEnumerable<KeyValuePair<string, string>> data)
        {
            Write(LogLevel.Error, data);
        }
        public void Fatal(IEnumerable<KeyValuePair<string, string>> data)
        {
            Write(LogLevel.Fatal, data);
        }
        public void Debug(string message)
        {
            Write(LogLevel.Debug, message);
        }
        public void Info(string message)
        {
            Write(LogLevel.Info, message);
        }
        public void Error(string message)
        {
            Write(LogLevel.Error, message);
        }
        public void Fatal(string message)
        {
            Write(LogLevel.Fatal, message);
        }
        public void Debug(Exception exception)
        {
            Write(LogLevel.Debug, "EXCEPTION: " + exception.Message + Environment.NewLine + exception.StackTrace);
        }
        public void Info(Exception exception)
        {
            Write(LogLevel.Info, "EXCEPTION: " + exception.Message + Environment.NewLine + exception.StackTrace);
        }
        public void Error(Exception exception)
        {
            Write(LogLevel.Error, "EXCEPTION: " + exception.Message + Environment.NewLine + exception.StackTrace);
        }
        public void Fatal(Exception exception)
        {
            Write(LogLevel.Fatal, "EXCEPTION: " + exception.Message + Environment.NewLine + exception.StackTrace);
        }

        public void Write(LogLevel logLevel, String message)
        {
            Write(logLevel, new[] { new KeyValuePair<string, string>("message", message) });
        }

        protected abstract void Write(LogLevel logLevel, IEnumerable<KeyValuePair<string, string>> data);

        protected IDictionary<string, string> GetParams(LogLevel logLevel, IEnumerable<KeyValuePair<string, string>> data)
        {
            var version = Windows.ApplicationModel.Package.Current.Id.Version;

            var result = new Dictionary<string, string>
            {
                { "assembly", LogManager.AssemblyName.ToString() },
                { "level", logLevel.ToString() },
                { "version", string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision) },
            };

            foreach (var kvp in data)
            {
                result[kvp.Key] = kvp.Value;
            }

            return result;
        }
    }
}
