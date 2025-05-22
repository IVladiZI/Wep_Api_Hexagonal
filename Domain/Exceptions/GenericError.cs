namespace Domain.Exceptions
{
    public class GenericError : Exception
    {
        public string? ErrorCode { get; }
        public GenericError(string message) : base(message) { }
        public GenericError(string message, Exception inner) : base(message, inner) { }
        public GenericError(string errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
