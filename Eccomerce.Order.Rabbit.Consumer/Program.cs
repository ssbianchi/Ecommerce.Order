using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using Eccomerce.Order.Rabbit.Consumer.WebApi.Order;
using System.Text;
using System.Text.Json;
using Ecommerce.Order.CrossCutting.Rabbit;

namespace Eccomerce.Order.Rabbit.Consumer
{
    public class Program
    {
        private static IOrderAPI _orderAPI;
        static void Main(string[] args)
        {
            if (_orderAPI == null)
                _orderAPI = new OrderAPI();

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "paymentQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var json = Encoding.UTF8.GetString(body);

                    if (json == "true")
                        return;

                    RabbitMessageConsumer message = JsonSerializer.Deserialize<RabbitMessageConsumer>(json);

                    System.Threading.Thread.Sleep(1000);

                    var api = _orderAPI.CloseOrderSession(message.OrderSessionId, message.OrderSessionStatusId);

                    Console.WriteLine($"OrderSessionId: {message.OrderSessionId}; OrderSessionStatusId={message.OrderSessionStatusId}");
                };
                channel.BasicConsume(queue: "paymentQueue",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}

