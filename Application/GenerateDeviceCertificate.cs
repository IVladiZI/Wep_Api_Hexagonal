using Domain.Entities;
using Domain.Ports.Incoming;
using Domain.Ports.Outgoing;

namespace Application
{
    public class GenerateDeviceCertificate : IGenerateDeviceCertificate
    {
        private readonly IGenerateGuid _generateGuid;
        private readonly ICryptographyGenerator _cryptographyGenerator;
        private readonly IEncoderMessage _encoderMessage;
        private readonly ICertificatePersistence _certificatePersistence;
        public GenerateDeviceCertificate(IGenerateGuid generateGuid, ICryptographyGenerator cryptographyGenerator, IEncoderMessage encoderMessage, ICertificatePersistence certificatePersistence)
        {
            _generateGuid = generateGuid;
            _cryptographyGenerator = cryptographyGenerator;
            _encoderMessage = encoderMessage;
            _certificatePersistence = certificatePersistence;
        }
        public async Task<CertifiedPublic> GenerateDeviceCertificateAsync(long deviceId)
        {
            var guid = _generateGuid.GenerateGuidWithOutHyphens();
            var iv = _cryptographyGenerator.GenerateRandomIV(16);
            var key = _cryptographyGenerator.GenerateHashSha256(guid, 31);
            var stringKey = _encoderMessage.EncodeByBytes(key);
            var StringIV = _encoderMessage.EncodeByBytes(iv);

            long id = await _certificatePersistence.InsertCertificateAsync(deviceId, stringKey, StringIV);

            var certifiedPublic = new CertifiedPublic
            {
                Id = id,
                Key = stringKey,
                IV = StringIV
            };

            return certifiedPublic;
        }
    }
}
