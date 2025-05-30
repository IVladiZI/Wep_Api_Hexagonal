using Domain.Ports.Incoming;

namespace Application.Interactor
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
