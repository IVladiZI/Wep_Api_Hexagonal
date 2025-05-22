using Domain.Entities;

namespace Domain.Ports.Incoming
{
    public interface IGenerateDeviceCertificate
    {
        Task<CertifiedPublic> GenerateDeviceCertificateAsync(long deviceId);
    }
}
