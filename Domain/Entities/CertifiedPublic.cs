namespace Domain.Entities
{
    public class CertifiedPublic : PublicToken
    {
        public string? Key { get; set; }
        public string? IV { get; set; }
        public long Id { get; set; }
    }
}
