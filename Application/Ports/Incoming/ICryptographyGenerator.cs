namespace Domain.Ports.Incoming
{
    public interface ICryptographyGenerator
    {
        string GenerateRandomIV(int length);
        string GenerateHashSha256(string text, int length);
    }
}
