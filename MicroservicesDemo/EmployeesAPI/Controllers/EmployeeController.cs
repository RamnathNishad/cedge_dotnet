using EmployeesAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
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


        [HttpPost]
        [Route("AddEmployee")]
        public IActionResult Post([FromBody]Employee employee)
        {
            dal.AddEmployee(employee);
            return Ok(employee);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            dal.DeleteEmployee(id);
            return Ok("Record deleted");
        }
        [HttpPut]
        [Route("UpdateEmp/{id}")]
        public IActionResult Put(int id,[FromBody]Employee employee)
        {
            dal.UpdateEmployee(employee); 
            return Ok(employee);
        }
    }
}
