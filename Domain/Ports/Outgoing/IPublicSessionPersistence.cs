namespace Domain.Ports.Outgoing
{
    public interface IPublicSessionPersistence
    {
        Task InsertPublicSessionAsync(Guid sessionId, string publicToken);
    }
}
