namespace Domain.Entities
{
    public abstract class Device
    {
        public required string DeviceId { get; set; }
        public required string DeviceType { get; set; }
    }
}
