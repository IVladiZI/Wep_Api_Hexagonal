namespace Domain.Ports.Incoming
{
    public interface IDeviceValidator
    {
        bool ValidateEncryptedDevice(string deviceId, string sendId, string encryptedDevice);
    }
}
