

using RabbitMQ_Sender;

Console.WriteLine("Send Message Start");
for (int i = 0; i < 100; i++)
{
    RabbitMQSender.PublishMessage($"Message ID #{i}");
}
Console.WriteLine("Send Message 100 Finished");

Console.ReadLine();