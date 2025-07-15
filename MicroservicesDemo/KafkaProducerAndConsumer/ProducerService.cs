using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaProducer
{
    internal class ProducerService
    {
        public async Task CreateMessage()
        {
            var config = new ProducerConfig
            {
                BootstrapServers="localhost:9092",
                ClientId="my-app",
                BrokerAddressFamily=BrokerAddressFamily.V4
            };


            var producer = new ProducerBuilder<Null, string>(config).Build();
            Console.WriteLine("Please enter the messagae your want to send");
            while(true)
            {
                var input = Console.ReadLine();
                var message = new Message<Null, string>
                {
                    Value=input
                };

                var deliveryReport =await producer.ProduceAsync("topic3", message);
                Console.WriteLine($"Message delivered to {deliveryReport.TopicPartitionOffset}");
            }
        }
    }
}
