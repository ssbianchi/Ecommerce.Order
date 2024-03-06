using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Ecommerce.Order.Application.RabbitRequest
{
    public class RabbitRequestService : IRabbitRequestService
    {
        public void SendMessage<T>(T message, string queue)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queue, //"orderQueue"
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: queue, //"orderQueue"
                                 basicProperties: null,
                                 body: body);

        }
    }
}
