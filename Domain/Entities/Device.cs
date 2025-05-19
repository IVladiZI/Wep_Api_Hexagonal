namespace Domain.Entities
{
    abstract class Device
    {
        public required string DeviceId { get; set; }
        public required string DeviceType { get; set; }
    }
}
