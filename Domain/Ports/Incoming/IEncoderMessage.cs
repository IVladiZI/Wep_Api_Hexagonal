namespace Domain.Ports.Incoming
{
    public interface IEncoderMessage
    {
        string EncodeByBytes(string message);
    }
}
