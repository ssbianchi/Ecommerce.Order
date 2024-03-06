namespace Eccomerce.Order.Rabbit.Consumer.WebApi.Exceptions
{
    public sealed class HttpRequestException : Exception
    {
        public HttpRequestException() { }
        public HttpRequestException(string errorMessage) : base(errorMessage) { }
        public HttpRequestException(string errorMessage, Exception innerException) : base(errorMessage, innerException) { }
    }
}
