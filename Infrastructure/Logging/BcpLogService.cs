using Domain.Ports.Outgoing;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using static Domain.Enums.Logging;
namespace Infrastructure.Logging
{
    public class BcpLogService : ILogService
    {
        private readonly ILogger _logger;

        public BcpLogService(IConfiguration configuration)
        {
            _logger = LoggerFactory.CreateLogger(configuration);
        }

        public void Log(
            string request,
            string response,
            Exception exception,
            string correlationId,
            LogLevel level,
            string method = ""
        )
        {
            var logData = new
            {
                Date = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                Time = DateTime.UtcNow.ToString("HH:mm:ss.fff"),
                Level = level.ToString(),
                CorrelationId = correlationId,
                Method = method,
                Request = request,
                Response = response,
                Exception = exception?.ToString()
            };

            switch (level)
            {
                case LogLevel.VERBOSE:
                    _logger.Verbose(logData);
                    break;
                case LogLevel.DEBUG:
                    _logger.Debug(logData);
                    break;
                case LogLevel.INFORMATION:
                    _logger.Information(logData);
                    break;
                case LogLevel.WARNING:
                    _logger.Warning(logData);
                    break;
                case LogLevel.ERROR:
                    _logger.Error(logData);
                    break;
                case LogLevel.FATAL:
                    _logger.Fatal(logData);
                    break;
            }
        }

        public void Log(string request, string response, Exception exception, string correlationId, Domain.Enums.Logging.LogLevel level, [CallerMemberName] string method = "")
        {
            throw new NotImplementedException();
        }
    }
}