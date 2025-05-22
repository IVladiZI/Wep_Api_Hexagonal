namespace Domain.Entities
{
    public class SessionHistory
    {
        public string? PublicToken { get; set; }
        public string? PrivateToken { get; set; }
        public int TypeRequestId { get; set; }
        public string? Request { get; set; }
        public string? Response { get; set; }
        public DateTime StartTimeRequest { get; set; }
        public DateTime EndTimeRequest { get; set; }
        public int StateResponse { get; set; }
    }
}
