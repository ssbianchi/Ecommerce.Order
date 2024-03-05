namespace Ecommerce.Order.Application.RabbitMq
{
    public interface IRabbitMessageService
    {
        void SendMessage<T> (T message);
    }
}
