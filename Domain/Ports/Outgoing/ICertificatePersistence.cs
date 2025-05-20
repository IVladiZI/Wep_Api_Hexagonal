namespace Domain.Ports.Outgoing
{
    public interface ICertificatePersistence
    {
        Task<long> InsertCertificateAsync(int deviceId, string publicKey, string iv);
    }
}
