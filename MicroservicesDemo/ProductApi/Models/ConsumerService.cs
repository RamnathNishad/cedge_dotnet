
using Confluent.Kafka;
using System.Text.Json;

namespace ProductApi.Models
{
    public class ConsumerService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                ReadMessage("topic3");
            });
        }

        public void ReadMessage(string topic)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                ClientId = "my-app",
                GroupId="my-group",
                BrokerAddressFamily = BrokerAddressFamily.V4
            };

            var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

            consumer.Subscribe(topic);
            try
            {
                while (true)
                {
                    var consumeResult = consumer.Consume();
                    var order = JsonSerializer.Deserialize<OrderDTO>(consumeResult.Message.Value);
                    //reduce the Qty from the Product inventory as per order qty
                    //i.e. Product.qty-=order.qty  ---TODO in DB
                   
                }
            }
            catch (Exception ex)
            {
                //return error msg or log the error
            }
            finally
            {
                consumer.Close();
            }
        }
    }
}
