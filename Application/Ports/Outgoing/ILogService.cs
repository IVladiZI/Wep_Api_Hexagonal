using System.Runtime.CompilerServices;
using static Domain.Enums.Logging;

namespace Domain.Ports.Outgoing
{
    public interface ILogService
    {
        void Log(
            string request,
            string response,
            Exception exception,
            string correlationId,
            LogLevel level,
            [CallerMemberName] string method = ""
        );
    }
}
