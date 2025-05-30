using Domain.Entities;
using Domain.Exceptions;
using Domain.Ports.Incoming;
using Domain.Ports.Outgoing;
using System.Runtime.InteropServices;

namespace Application.Interactor
{
    public class GeneratePublicToken : IGeneratePublicToken
    {
        private readonly IBusinessErrorDictionary _businessErrorDictionary;
        private readonly IGenerateDeviceCertificate _generateDeviceCertificate;
        private readonly IDeviceValidator _deviceValidator;
        private readonly IOsValidator _osValidator;
        private readonly IDevicePersistence _devicePersistence;
        private readonly ISeedPersistence _seedPersistence;
        private readonly ICertificatePersistence _certificatePersistence;
        private readonly IPublicSessionPersistence _publicSessionPersistence;
        private readonly IDeviceLockedPersistence _deviceLockedPersistence;
        private readonly IGenerateGuid _generateGuid;
        private readonly ICryptographyGenerator _cryptographyGenerator;
        private long DeviceId = 0;
        private CertifiedPublic CertifiedPublic = new();
        private Guid TokenGuid = Guid.Empty;

        public GeneratePublicToken(IBusinessErrorDictionary businessErrorDictionary, IGenerateDeviceCertificate generateDeviceCertificate, IDeviceValidator deviceValidator, IOsValidator osValidator, IDevicePersistence devicePersistence, ISeedPersistence seedPersistence, ICertificatePersistence certificatePersistence, IPublicSessionPersistence publicSessionPersistence, IDeviceLockedPersistence deviceLockedPersistence, IGenerateGuid generateGuid, ICryptographyGenerator cryptographyGenerator)
        {
            _businessErrorDictionary = businessErrorDictionary;
            _generateDeviceCertificate = generateDeviceCertificate;
            _deviceValidator = deviceValidator;
            _osValidator = osValidator;
            _devicePersistence = devicePersistence;
            _seedPersistence = seedPersistence;
            _certificatePersistence = certificatePersistence;
            _publicSessionPersistence = publicSessionPersistence;
            _deviceLockedPersistence = deviceLockedPersistence;
            _generateGuid = generateGuid;
            _cryptographyGenerator = cryptographyGenerator;
        }

        public async Task<ApiResponse> GenerateTokenAsync(AuthenticationDevice authenticationDevice)
        {
            try
            {
                if (!_deviceValidator.ValidateEncryptedDevice(authenticationDevice.DeviceId, authenticationDevice.SendId, authenticationDevice.EncryptedDevice))
                {
                    await _deviceLockedPersistence.InsertDeviceLocked(authenticationDevice.DeviceId, authenticationDevice.DeviceType, authenticationDevice.EncryptedDevice, authenticationDevice.SendId);
                    return ApiResponseFactory.ControlledError(
                        _businessErrorDictionary.GetErrorMessage("DEVICE_LOCKED"));
                }

                if (!_osValidator.ValidateDeviceOs(authenticationDevice.DeviceType))
                    return ApiResponseFactory.ControlledError(
                        _businessErrorDictionary.GetErrorMessage("DEVICE_NO_ACCES"));

                if(authenticationDevice.Certificate)
                {
                    DeviceId = await _devicePersistence.InsertDeviceAsync(authenticationDevice.DeviceId,authenticationDevice.DeviceType);
                    
                    await _seedPersistence.GetSemillaBySendIdAsync(authenticationDevice.SendId);

                    CertifiedPublic = await _generateDeviceCertificate.GenerateDeviceCertificateAsync(DeviceId);

                    TokenGuid = _generateGuid.GenerateGuidWithHyphens();

                    await _publicSessionPersistence.InsertPublicSessionAsync(DeviceId, TokenGuid);

                    return ApiResponseFactory.Success(CertifiedPublic);
                }

                DeviceId = await _devicePersistence.GetIdDevice(authenticationDevice.DeviceId, authenticationDevice.DeviceType);
                TokenGuid = _generateGuid.GenerateGuidWithHyphens();
                CertifiedPublic = new CertifiedPublic
                {
                    AuthToken = TokenGuid.ToString()
                };
                await _publicSessionPersistence.InsertPublicSessionAsync(DeviceId, TokenGuid);

                return ApiResponseFactory.Success(CertifiedPublic);
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.ControlledError(
                    _businessErrorDictionary.GetErrorMessage("GENERIC_ERROR"),ex);
            }
        }
    }
}
