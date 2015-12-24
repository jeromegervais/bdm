namespace BDM.App.Shared.Log
{
    /// <summary>
    /// Classe utilitaire pour configurer / creer des loggers.
    /// Puis creer des loggers en utilisant LogManager.CreateLogger()
    /// </summary>
    public static class LogManager
    {
        /// <summary>
        /// Le nom de l'assembly qui a créé le logger.
        /// </summary>
        public static string AssemblyName { get; set; }

        /// <summary>
        /// niveau a partir du quel les demandes de Log seront prises en compte.
        /// Par defaut, Debug.
        /// </summary>
        public static LogLevel MinLevel { get; set; } = LogLevel.Debug;
        
        public static ILogger CreateLogglyLogger(string token)
        {
            return new LogglyLogger(token);
        }

        public static ILogger CreateDefaultLooger()
        {
            return new DefaultLogger();
        }
    }
}
