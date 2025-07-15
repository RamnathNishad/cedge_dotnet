using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersAPI.Models;
using System.Text.Json;

namespace OrdersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        [HttpPost]
        public IActionResult Post([FromBody]Order order)
        {
            //create order ---TODO in DB Insert

            //produce message in topic
            CreateMessage("topic3", new Message<int, string>
            {
                Key=order.Id,
                Value=JsonSerializer.Serialize(order)
            }).Wait();

            return Ok("Order created successfully");
        }

        async Task CreateMessage(string topic,Message<int,string> message)
        {
            var config = new ProducerConfig
            {
                BootstrapServers="localhost:9092",
                ClientId="my-app",
                BrokerAddressFamily=BrokerAddressFamily.V4
            };

            var producer = new ProducerBuilder<int, string>(config).Build();
            await producer.ProduceAsync(topic, message);
        }
        
    }
}
