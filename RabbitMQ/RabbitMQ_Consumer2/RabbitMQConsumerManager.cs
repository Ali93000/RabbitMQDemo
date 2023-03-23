using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_Consumer2
{
    public class RabbitMQConsumerManager
    {
        public static void ConsumeMessage()
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672"),
                ClientProvidedName = "Consumer",
            };

            var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            string exchangeName = "DemoExchange";
            string routingKey = "Demo-routing-key";
            string queueName = "Demo-Queue";

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true, false, null);
            channel.QueueDeclare(queueName, true, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);

            // used to set a limit number of unacknowledged messages
            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, args) =>
            {
                // get the body ( byte[] ) from memory it's readonly memory
                var body = args.Body.ToArray();
                // get the body as string
                var message = Encoding.UTF8.GetString(body);

                // Here we can processing the message (add to database or call Api or etc...)
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();  // temporary

                Console.WriteLine($"Message Received - {message}");

                // note the message that it's acknowledged
                channel.BasicAck(args.DeliveryTag, false);
            };


            string consumerTag = channel.BasicConsume(queueName, false, consumer);

            // we put it here to keep the consumer is always running and continue listinging and proccessing messages
            Console.ReadLine();

            // used to cancel or delete the content class consumer
            channel.BasicCancel(consumerTag);

            channel.Close();

            connection.Close();
        }
    }
}
