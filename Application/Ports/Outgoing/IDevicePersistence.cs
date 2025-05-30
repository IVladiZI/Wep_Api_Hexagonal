using Domain.Enums;

namespace Domain.Ports.Outgoing
{
    public interface IDevicePersistence
    {
        Task<long> InsertDeviceAsync(string deviceId, string deviceType);
        Task<long> GetIdDevice(string deviceId,string deviceType);
    }
}
