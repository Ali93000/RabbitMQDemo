using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_Sender
{
    public static class RabbitMQSender
    {
        public static void PublishMessage(string msg)
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672"),
                ClientProvidedName = "RabbitMQ Senedr"
            };

            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            string exchangeName = "DemoExchange";
            string routingKey = "Demo-routing-key";
            string queueName = "Demo-Queue";

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true, false, null);
            channel.QueueDeclare(queueName, true, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);

            var body = Encoding.UTF8.GetBytes(msg);

            channel.BasicPublish(exchangeName, routingKey, null, body);
            channel.Close();
            connection.Close();
        }
    }
}
