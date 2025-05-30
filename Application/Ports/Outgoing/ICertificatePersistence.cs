namespace Domain.Ports.Outgoing
{
    public interface ICertificatePersistence
    {
        Task<long> InsertCertificateAsync(long dispositivoId, string stringKey, string stringIV);
    }
}
