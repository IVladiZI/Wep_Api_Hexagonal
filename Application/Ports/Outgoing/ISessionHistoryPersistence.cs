using Domain.Entities;

namespace Domain.Ports.Outgoing
{
    public interface ISessionHistoryPersistence
    {
        Task InsertSessionHistory(SessionHistory sessionHistory);
    }
}
