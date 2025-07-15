using KafkaProducer;

namespace KafkaProducerAndConsumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var producerService = new ProducerService();
            producerService.CreateMessage().Wait();

        }
    }
}
