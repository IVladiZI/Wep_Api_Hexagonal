namespace Domain.Ports.Outgoing
{
    public interface ISeedPersistence
    {
        Task<string> GetSemillaBySendIdAsync(string sendId);
    }
}
