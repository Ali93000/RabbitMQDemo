using RabbitMQ.Client;

namespace RabbitMQ.Configurations
{
    public class RabbitMQManager
    {
        public static void CreateExchangAndQueueWithBind()
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672"),
                ClientProvidedName = "Rabbit MQ Configuration"
            };

            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            string exchangeName = "DemoExchange";
            string routingKey = "Demo-routing-key";
            string queueName = "Demo-Queue";

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);



        }

    }
}