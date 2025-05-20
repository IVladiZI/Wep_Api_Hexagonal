namespace Domain.Ports.Incoming
{
    public interface IPublicTokenGenerator
    {
        string GenerateToken();
        Task<string> GenerateTokenAsync();
    }
}
