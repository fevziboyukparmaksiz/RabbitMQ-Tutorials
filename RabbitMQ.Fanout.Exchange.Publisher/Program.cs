using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://whlmzzhz:89x2AG2tGzVr0vc1y3F5MW9ndETSnHDP@rattlesnake.rmq.cloudamqp.com/whlmzzhz");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(
    exchange: "fanout-exchange-example",
    type: ExchangeType.Fanout);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");

    channel.BasicPublish(
    exchange: "fanout-exchange-example",
    routingKey: string.Empty,
    body: message);
}

Console.Read();
