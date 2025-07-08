using EmployeesAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeDataAccess dal;
        public EmployeeController(IEmployeDataAccess dal)
        {
            this.dal = dal;
        }

        [HttpGet]
        [Route("GetEmps")]
        public IActionResult GetEmps()
        {
            return Ok(dal.GetAllEmps());
        }
    }
}
