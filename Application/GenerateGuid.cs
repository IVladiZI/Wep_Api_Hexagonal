using Domain.Ports.Incoming;

namespace Application
{
    public class GenerateGuid : IGenerateGuid
    {
        public Guid GenerateGuidWithHyphens()
        {
            return Guid.NewGuid();
        }

        public string GenerateGuidWithOutHyphens()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
