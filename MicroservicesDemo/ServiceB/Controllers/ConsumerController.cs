using Consul;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Text.Json;

namespace ServiceB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        [HttpGet]
        [Route("GetEmps")]
        public async Task<IActionResult> GetEmps()
        {
            var lstEmps =new  List<Employee>();
            //call EmpService 
            var discovery = new ConsulServiceDiscovery("http://localhost:8500");
            var uri = await discovery.GetServiceUri("EmpService");
            if(uri != null)
            {
                var httpClient=new HttpClient();
                var result =await httpClient.GetStringAsync(uri+"api/Employee/GetEmps");
                lstEmps=JsonSerializer.Deserialize<List<Employee>>(result);
            }

            return Ok(lstEmps);
        }

    }
    public class ConsulServiceDiscovery
    {
        private readonly ConsulClient consulClient;
        public ConsulServiceDiscovery(string consulAddress)
        {
            consulClient = new ConsulClient(c => c.Address = new Uri(consulAddress));
        }

        public async Task<Uri> GetServiceUri(string serviceName)
        {
            var services = await consulClient.Health.Service(serviceName, "", true);
            var service = services.Response.FirstOrDefault();
            if (service == null)
            {
                return null;
            }

            var address = service.Service.Address;
            var port = service.Service.Port;
            return new Uri($"http://{address}:{port}");

        }
    }
}
