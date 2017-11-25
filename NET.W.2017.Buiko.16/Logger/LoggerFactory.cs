using Logger.Implementation;

namespace Logger
{
    /// <summary>
    /// Class representing a static factory for loggers.
    /// </summary>
    public static class LoggerFactory
    {
        /// <summary>
        /// Returns the logger.
        /// </summary>
        /// <param name="className">name of the class 
        /// for which the logger is created</param>
        /// <returns>Logger.</returns>
        public static ILogger GetLogger(string className) 
            => new NLogger(className);
    }
}
