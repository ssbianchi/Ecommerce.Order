namespace Ecommerce.Order.Application.RabbitRequest
{
    public interface IRabbitRequestService
    {
        void SendMessage<T> (T message, string queue);
    }
}
