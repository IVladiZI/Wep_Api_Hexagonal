namespace Domain.Ports.Incoming
{
    public interface IPublicToken
    {
        bool ValidateEncryptedDecvice();
        Task GetTokenAsync();
    }
}
