using Domain.Enums;
using Domain.Ports.Incoming;

namespace Application
{
    public class OsValidator : IOsValidator
    {
        public bool ValidateDeviceOs(string deviceType)
        {
            if (Enum.TryParse<DeviceType>(deviceType, true, out var parsedDeviceType))
            {
                return parsedDeviceType == DeviceType.android
                    || parsedDeviceType == DeviceType.ios;
            }
            return false;
        }
    }
}
