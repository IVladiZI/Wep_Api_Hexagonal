namespace Domain.Ports.Incoming
{
    public interface IGenerateGuid
    {
        string GenerateGuidWithHyphens();
        string GenerateGuidWithOutHyphens();
    }
}
