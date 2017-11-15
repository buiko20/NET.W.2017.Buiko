using Logger.Implementation;

namespace Logger
{
    public static class LoggerFactory
    {
        public static ILogger GetNLogger(string className) 
            => new NLogger(className);
    }
}
