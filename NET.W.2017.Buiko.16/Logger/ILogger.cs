using System;

namespace Logger
{
    /// <summary>
    /// Logger contract.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Writes the diagnostic message to the Trace level. 
        /// </summary>
        /// <param name="message">diagnostic message</param>
        void Trace(string message);

        /// <summary>
        /// Writes the diagnostic message to the Trace level. 
        /// </summary>
        /// <param name="message">diagnostic message</param>
        /// <param name="exception">error info</param>
        void Trace(string message, Exception exception);

        /// <summary>
        /// Writes the diagnostic message to the Debug level. 
        /// </summary>
        /// <param name="message">diagnostic message</param>
        void Debug(string message);

        /// <summary>
        /// Writes the diagnostic message to the Debug level. 
        /// </summary>
        /// <param name="message">diagnostic message</param>
        /// <param name="exception">error info</param>
        void Debug(string message, Exception exception);

        /// <summary>
        /// Writes the diagnostic message to the Info level. 
        /// </summary>
        /// <param name="message">diagnostic message</param>
        void Info(string message);

        /// <summary>
        /// Writes the diagnostic message to the Info level. 
        /// </summary>
        /// <param name="message">diagnostic message</param>
        /// <param name="exception">error info</param>
        void Info(string message, Exception exception);

        /// <summary>
        /// Writes the diagnostic message to the Warn level. 
        /// </summary>
        /// <param name="message">diagnostic message</param>
        void Warn(string message);

        /// <summary>
        /// Writes the diagnostic message to the Warn level. 
        /// </summary>
        /// <param name="message">diagnostic message</param>
        /// <param name="exception">error info</param>
        void Warn(string message, Exception exception);

        /// <summary>
        /// Writes the diagnostic message to the Error level. 
        /// </summary>
        /// <param name="message">diagnostic message</param>
        void Error(string message);

        /// <summary>
        /// Writes the diagnostic message to the Error level. 
        /// </summary>
        /// <param name="message">diagnostic message</param>
        /// <param name="exception">error info</param>
        void Error(string message, Exception exception);

        /// <summary>
        /// Writes the diagnostic message to the Fatal level. 
        /// </summary>
        /// <param name="message">diagnostic message</param>
        void Fatal(string message);

        /// <summary>
        /// Writes the diagnostic message to the Fatal level. 
        /// </summary>
        /// <param name="message">diagnostic message</param>
        /// <param name="exception">error info</param>
        void Fatal(string message, Exception exception);
    }
}
