namespace Eccomerce.Order.Rabbit.Consumer.WebApi.Order
{
    public interface IOrderAPI
    {
        Task<bool> CloseOrderSession(int orderSessionId, int statusId);
    }
}
