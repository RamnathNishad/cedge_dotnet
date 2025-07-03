using ADOLib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIPrj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesRepository dal;
        public EmployeesController(IEmployeesRepository dal)
        {
            this.dal = dal;
        }

        // GET: api/<EmployeesController>
        [HttpGet]
        [Route("GetAllEmps")]
        //[Route("GetEmps")]
        public IActionResult Get()
        {
            try
            {
                return Ok(dal.GetAllEmps());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<EmployeesController>/5
        [HttpGet]
        [Route("GetEmpById/{id:int}")]
        public IActionResult GetEmpById(int id)
        {
            try
            {
                return Ok(dal.GetEmployee(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetEmpsByDeptId/{id:int}")]
        public IActionResult GetEmpsByDeptId(int id)
        {
            try
            {
                return Ok(dal.GetAllEmps()
                          .Where(p => p.Deptid == id)
                          .ToList());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // POST api/<EmployeesController>
        [HttpPost]
        [Route("AddEmployee")]
        public IActionResult Post([FromBody] Employee emp)
        {
            try
            {
                dal.AddEmployee(emp);
                return Ok("Record inserted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<EmployeesController>/5
        [HttpPut]
        [Route("UpdateEmp/{id}")]
        public IActionResult Put(int id, [FromBody] Employee emp)
        {
            try
            {
                dal.UpdateEmployee(emp);
                return Ok("Record updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete]
        [Route("DeleteEmp/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                dal.DeleteEmployee(id);
                return Ok("Record deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("divide/{a}/{b}")]
        public IActionResult Divide(int a,int b)
        {
            var result = a / b;
            return Ok(result);
        }
    }
}
