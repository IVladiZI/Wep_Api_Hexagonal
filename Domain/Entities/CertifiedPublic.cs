namespace Domain.Entities
{
    public class CertifiedPublic : PublicToken
    {
        public required string Key { get; set; }
        public required string IV { get; set; }
        public long Id { get; set; }
    }
}
