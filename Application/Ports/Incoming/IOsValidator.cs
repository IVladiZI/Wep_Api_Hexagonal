namespace Domain.Ports.Incoming
{
    public interface IOsValidator
    {
        bool ValidateDeviceOs(string deviceType);
    }
}
