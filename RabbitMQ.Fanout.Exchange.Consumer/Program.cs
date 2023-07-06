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

//Kuyruk adı kullanıcıdan alındı.
Console.Write("Kuyruk adını giriniz : ");
string queueName = Console.ReadLine();

//Kuyruk declare edildi.
channel.QueueDeclare(
    queue: queueName,
    exclusive: false);

//Exchange kuyruğa bind edildi.
channel.QueueBind(
    queue: queueName,
    exchange: "fanout-exchange-example",
    routingKey: string.Empty);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: queueName,
    autoAck: true,
    consumer: consumer);

consumer.Received+= (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};


Console.Read();
