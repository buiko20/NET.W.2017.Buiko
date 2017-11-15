using System;
using NLog;

namespace Logger.Implementation
{
    internal class NLogger : ILogger
    {
        #region private fields

        private readonly NLog.Logger _logger;

        #endregion // !private fields.

        #region constructors

        public NLogger(string className)
        {
            _logger = LogManager.GetLogger(className);
        }

        #endregion // !constructors.

        #region interface implementation

        public void Trace(string message)
        {
            _logger.Trace(message);
        }

        public void Trace(Exception exception, string message)
        {
            _logger.Trace(exception, message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Debug(Exception exception, string message)
        {
            _logger.Debug(exception, message);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Info(Exception exception, string message)
        {
            _logger.Info(exception, message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Warn(Exception exception, string message)
        {
            _logger.Warn(exception, message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception exception, string message)
        {
            _logger.Error(exception, message);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(Exception exception, string message)
        {
            _logger.Fatal(exception, message);
        }

        #endregion // !interface implementation.
    }
}
