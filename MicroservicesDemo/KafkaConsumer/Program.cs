namespace KafkaConsumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var consumer = new ConsumerService();
            consumer.ReadMessage();
        }
    }
}
