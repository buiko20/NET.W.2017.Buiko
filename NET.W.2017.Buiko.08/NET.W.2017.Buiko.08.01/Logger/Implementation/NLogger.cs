using System;
using NLog;

namespace Logger.Implementation
{
    /// <inheritdoc />
    internal class NLogger : ILogger
    {
        #region private fields

        private readonly NLog.Logger _logger;

        #endregion // !private fields.

        #region constructors

        public NLogger(string className)
        {
            if (string.IsNullOrWhiteSpace(className))
            {
                throw new ArgumentException($"{nameof(className)} IsNullOrWhiteSpace", nameof(className));
            }

            _logger = LogManager.GetLogger(className);
        }

        #endregion // !constructors.

        #region interface implementation

        /// <inheritdoc />
        public void Trace(string message) =>
            _logger.Trace(message);

        /// <inheritdoc />
        public void Trace(string message, Exception exception) =>
            _logger.Trace(exception, message);

        /// <inheritdoc />
        public void Debug(string message) =>
            _logger.Debug(message);

        /// <inheritdoc />
        public void Debug(string message, Exception exception) =>
            _logger.Debug(exception, message);

        /// <inheritdoc />
        public void Info(string message) =>
            _logger.Info(message);

        /// <inheritdoc />
        public void Info(string message, Exception exception) =>
            _logger.Info(exception, message);

        /// <inheritdoc />
        public void Warn(string message) =>
            _logger.Warn(message);

        /// <inheritdoc />
        public void Warn(string message, Exception exception) =>
            _logger.Warn(exception, message);

        /// <inheritdoc />
        public void Error(string message) =>
            _logger.Error(message);

        /// <inheritdoc />
        public void Error(string message, Exception exception) =>
            _logger.Error(exception, message);

        /// <inheritdoc />
        public void Fatal(string message) =>
            _logger.Fatal(message);

        /// <inheritdoc />
        public void Fatal(string message, Exception exception) =>
            _logger.Fatal(exception, message);

        #endregion // !interface implementation.
    }
}
