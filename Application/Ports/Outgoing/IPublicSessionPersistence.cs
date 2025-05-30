namespace Domain.Ports.Outgoing
{
    public interface IPublicSessionPersistence
    {
        Task<long> InsertPublicSessionAsync(long deviceId, Guid publicToken);
    }
}
