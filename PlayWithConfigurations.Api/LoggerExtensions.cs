namespace PlayWithConfigurations.Api
{
	/// <summary>
	/// Improving logging performance with source generators
	/// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/logging/loggermessage?view=aspnetcore-7.0
	/// </summary>
	public static class LoggerExtensions
    {
        /*
		 * The purpose of the static LoggerMessage.Define<T> method:
		 * 1. Encapsulate the if statement to allow performant logging
		 * 2. Enforce the correct strongly-typed parameters are passed when logging the message
		 * 3. Ensure the log message contains the correct number of placeholders for parameters
		 */

        private static readonly Action<ILogger, DateTime, string, Exception?> _logInfo
            = LoggerMessage.Define<DateTime, string>(
                logLevel: LogLevel.Information,
                eventId: new(0, nameof(LogInfo)),
                formatString: "[{Time}]: {Message}"
                //new LogDefineOptions() { SkipEnabledCheck = true }
                );

        // LoggerMessage.Define: https://github.com/aspnet/Logging/blob/50bc4c097986eafe3b4f3be8c36a79c832820af0/src/Microsoft.Extensions.Logging.Abstractions/LoggerMessage.cs

        private static Func<ILogger, string, IDisposable> _createScope
            = LoggerMessage.DefineScope<string>(
                 "Tracking Ref: {Reference}"
                );

        public static void LogInfo(this ILogger logger, string message)
        {
            _logInfo(logger, DateTime.Now, message, null);
        }

        public static IDisposable CreateScope(this ILogger logger, string reference)
        {
            return _createScope(logger, reference);
        }

        /*
		 * We can also do something like this:
		 * 
		 * public partial class SomeController
		 * {
		 *		[LoggerMessage(0, LogLevel.Information, "Hi-Perf Log: {Message}")]
		 *		partial void LogInfo(string message);
		 *		
		 *		// [LoggerMessage(0, LogLevel.Information, "Hi-Perf Log: {Message}")]
		 *		// static partial void LogInfo(string message);
		 *		
		 *		// [LoggerMessage(Message = "Hi-Perf Log: {Message}")]
		 *	    // partial void LogInfo(LogLevel logLevel, String message); 
		 * }
		 */
    }
}
