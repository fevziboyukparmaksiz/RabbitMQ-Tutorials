using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://whlmzzhz:89x2AG2tGzVr0vc1y3F5MW9ndETSnHDP@rattlesnake.rmq.cloudamqp.com/whlmzzhz");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


//1. Adım : Publisher'da ki exchange ile birebir aynı isim ve type'a sahip bir exchange tanımlanmalıdır!
channel.ExchangeDeclare(exchange: "direct-exhange-example", type: ExchangeType.Direct);

//2. Adım : Publisher tarafından routing key'de bulunan değerdeki kuyruğa gönderilen mesajları, kendi oluşturduğumuz kuyruğa yönlendirerek tüketmemiz gerekmektedir.Bunun için öncelikle bir kuyruk oluşturulmalıdır!
string queueName = channel.QueueDeclare().QueueName;

//3. Adım 
channel.QueueBind(
    queue: queueName,
    exchange: "direct-exhange-example",
    routingKey: "direct-queue-example");

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: queueName,
    autoAck: true,
    consumer: consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};


Console.Read();


