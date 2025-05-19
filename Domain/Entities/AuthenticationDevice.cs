namespace Domain.Entities
{
    public class AuthenticationDevice : Device
    {
        public required string SendId { get; set; }
        public bool Certificate { get; set; }
        public required string EncryptedDevice { get; set; }
        public string? NotificationId { get; set; }
        public string? Reference { get; set; }
        public string? Product { get; set; }
        public int Event { get; set; }
        public string? CodeReferred { get; set; }
    }
}
