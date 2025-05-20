namespace Domain.Ports.Outgoing
{
    public interface IDevicePersistence
    {
        Task<long> InsertDeviceAsync(string deviceId, string deviceType);
    }
}
