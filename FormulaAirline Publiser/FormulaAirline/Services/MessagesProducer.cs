using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace FormulaAirline.API.Services
{
    public class MessagesProducer : IMessagesProducer
    {
        private IModel channel;
        private readonly object instance_Locker = new object();

        internal object GetLocker()
        {
            return instance_Locker;
        }

        public void SendMessage<T>(T message)
        {

            lock (GetLocker())
            {
                if (channel == null)
                {
                    var factory = new ConnectionFactory()
                    {
                        HostName = "localhost",
                        UserName = "guest",
                        Password = "guest",
                        VirtualHost = "/"
                    };
                    var conn = factory.CreateConnection();

                    var channel = conn.CreateModel();

                    channel.QueueDeclare("bookings123", durable: false, exclusive: true, autoDelete: false, arguments: null);

                    var jsonString = JsonSerializer.Serialize(message);
                    var body = Encoding.UTF8.GetBytes(jsonString);

                    channel.BasicPublish("", "bookings123", body: body);
                }
            }
           

        }
    }
}
