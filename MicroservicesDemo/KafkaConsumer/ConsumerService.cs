using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsumer
{
    internal class ConsumerService
    {
        public void ReadMessage()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers="localhost:9092",
                AutoOffsetReset=AutoOffsetReset.Earliest,
                ClientId="my-app",
                GroupId="my-group",
                BrokerAddressFamily=BrokerAddressFamily.V4
            };

            var consumer = new ConsumerBuilder<Null, string>(config).Build();
            consumer.Subscribe("topic3");
            try
            {
                while (true)
                {
                    var consumeResult = consumer.Consume();
                    Console.WriteLine($"Message received from:{consumeResult.Message.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                consumer.Close();
            }
        }
    }
}
