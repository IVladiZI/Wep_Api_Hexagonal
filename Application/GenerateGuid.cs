using Domain.Ports.Incoming;

namespace Application
{
    public class GenerateGuid : IGenerateGuid
    {
        public string GenerateGuidWithHyphens()
        {
            return Guid.NewGuid().ToString();
        }

        public string GenerateGuidWithOutHyphens()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
