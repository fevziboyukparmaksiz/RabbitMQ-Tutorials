
using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://whlmzzhz:89x2AG2tGzVr0vc1y3F5MW9ndETSnHDP@rattlesnake.rmq.cloudamqp.com/whlmzzhz");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "direct-exhange-example", type: ExchangeType.Direct);

while (true)
{
    Console.Write("Mesaj: ");
    string message = Console.ReadLine();
    byte[] byteMessage = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(
        exchange: "direct-exhange-example",
        routingKey: "direct-queue-example",
        body: byteMessage);

}



Console.Read();


