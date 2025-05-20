using Domain.Ports.Incoming;
using System.Text;

namespace Application
{
    public class EncoderMessage : IEncoderMessage
    {
        public string EncodeByBytes(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            return string.Join("|", bytes);
        }
    }
}
