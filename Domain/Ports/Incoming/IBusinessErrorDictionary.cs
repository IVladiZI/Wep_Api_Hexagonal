namespace Domain.Ports.Incoming
{
    public interface IBusinessErrorDictionary
    {
        string GetErrorMessage(string errorCode);
    }
}
