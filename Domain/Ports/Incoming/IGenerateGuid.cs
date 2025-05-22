namespace Domain.Ports.Incoming
{
    public interface IGenerateGuid
    {
        Guid GenerateGuidWithHyphens();
        string GenerateGuidWithOutHyphens();
    }
}
