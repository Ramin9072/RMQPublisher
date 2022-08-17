using RabbitMQ.Client;
using RMQPublisher.Core.Business;
using RMQPublisher.Core.Common;
using RMQPublisher.Core.Domain;

namespace RMQ.Worker.Publisher
{
    public class RMQ_Publisher
    {
        public static ConnectionFactory _connectionFactory;
        public static IConnection _connection;
        public static IModel _model;

        public const string QueueName = "Queue1";
        public RMQ_Publisher()
        {
            CreateConnection();

            string key;
            do
            {
                Console.WriteLine("How many package you want to send?"); 
                var paymentCount = Console.ReadLine();

                SendPayments(int.Parse(paymentCount));
                Console.WriteLine("".PadLeft(100, '-'));
                Console.WriteLine();

                Console.WriteLine("do you want to send message:y/any thing else ");
                key = Console.ReadLine();
                Console.WriteLine();

            } while (key.ToLower() == "y");
        }
        private static void SendPayments(int paymentCount)
        {
            for (int i = 0; i < paymentCount; i++)
            {
                Payment payment = new Payment
                {
                    FirstName = $"FiratName {i}",
                    LastName = $"LastName {i}",
                    CardNumber = $"1111-2222-3333-{i.ToString().PadLeft(4, '0')}",
                    Value = Math.Pow(i / 3, 5)
                };
                _model.BasicPublish("", Varriables.Keyname, null, payment.ToByteArray());
                Console.WriteLine($"Message Send for {i}");
            }
        }

        public static void CreateConnection()
        {
            // create connection to RabbitMQ
            _connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                Port = Protocols.DefaultProtocol.DefaultPort
            };
            // make connection 
            _connection = _connectionFactory.CreateConnection();
            // create channel در صورتی که چنتا کانکشن باشد این بین همه آنها مشترک است
            _model= _connection.CreateModel();
            _model.QueueDeclare(QueueName,true,false,false,null);
        }
    }
}
