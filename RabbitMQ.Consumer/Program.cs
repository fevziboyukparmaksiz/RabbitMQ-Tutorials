
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//1.Adım Bağlantı Oluşturma
ConnectionFactory factory = new();
factory.Uri = new("amqps://whlmzzhz:89x2AG2tGzVr0vc1y3F5MW9ndETSnHDP@rattlesnake.rmq.cloudamqp.com/whlmzzhz");

//2.Adım Bağlantıyı Aktifleştirme ve Kanal Açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//3.Adım Queue Oluşturma
channel.QueueDeclare(queue: "example-queue", exclusive: false, durable: true); //Consumer'da da kuyruk publisher'daki ile birebir aynı yapılandırma tanımlanmalıdır !

//4.Adım Queue'dan Mesaj Okuma
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example-queue", autoAck: false, consumer);
channel.BasicQos(0, 1, true);
consumer.Received += (sender, e) =>
{
    // Kuyruğa gelen mesajın işlendiği yerdir.
    //e.body : Kuyruktaki mesajın verisini bütünsel olarak getirecektir.
    //e.Body.Span veya e.Body.ToArray(): kuyruktaki mesajun byte verisini getirecektir.
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));

    channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
};

Console.Read();