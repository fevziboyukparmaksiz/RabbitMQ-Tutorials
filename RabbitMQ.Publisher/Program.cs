
using RabbitMQ.Client;
using System.Text;

//1.Adım Bağlantı Oluşturma
ConnectionFactory factory = new();
factory.Uri = new("amqps://whlmzzhz:89x2AG2tGzVr0vc1y3F5MW9ndETSnHDP@rattlesnake.rmq.cloudamqp.com/whlmzzhz");

//2.Adım Bağlantıyı Aktifleştirme ve Kanal Açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//3.Adım Queue Oluşturma
channel.QueueDeclare(queue: "example-queue", exclusive: false, durable: true);

//4.Adım Queue'ya Mesaj Gönderme

//RabbitMQ kuyrağa atacağı mesajları byte türünden kabul etmektedir. Mesajları byte dönüştürmemiz gerekecektir.
//byte[] message = Encoding.UTF8.GetBytes("Merhaba");
//channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);

IBasicProperties properties = channel.CreateBasicProperties();
properties.Persistent = true;

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes("Merhaba " + i);
    channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message, basicProperties: properties);
}

Console.Read();


