namespace RouletteBetsApi.Exceptions
{
    public class NotauthorizedAccessException : Exception
    {
        public NotauthorizedAccessException(string message) : base(message)
        { }
    }
}
