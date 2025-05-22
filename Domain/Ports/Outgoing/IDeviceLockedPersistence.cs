namespace Domain.Ports.Outgoing
{
    public interface IDeviceLockedPersistence
    {
        Task<long> InsertDeviceLocked(string deviceId, string deviceType, string encriptedDeviceId, string seedId);
    }
}
