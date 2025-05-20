namespace Domain.Exceptions
{
    public class GenericError : Exception
    {
        public GenericError(string message) : base(message) { }
        public GenericError(string message, Exception inner) : base(message, inner) { }
    }
}
