using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RMQPublisher.Core.Business;
using RMQPublisher.Core.Common;
using RMQPublisher.Core.Domain;

namespace RMQ.Worker.Consumer
{
    public class RMQ_Consumer
    {
        public static ConnectionFactory _connectionFactory;
        public static IModel _model;
        public static IConnection _connection;


        public RMQ_Consumer()
        {
            CreateConnection();


            var consumer = new EventingBasicConsumer(_model);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var payment = body.FromByteArray<Payment>();
                Console.WriteLine($" [{payment.RowNum}] Received {payment.FirstName} {payment.LastName} {payment.Value}");
            };
            _model.BasicConsume(queue: Varriables.Keyname,
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

        }


        public static void CreateConnection()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                Port = Protocols.DefaultProtocol.DefaultPort
            };
            _connection = _connectionFactory.CreateConnection();
            _model = _connection.CreateModel();
            _model.QueueDeclare(Varriables.Keyname, true, false, false, null);
        }
    }
}
