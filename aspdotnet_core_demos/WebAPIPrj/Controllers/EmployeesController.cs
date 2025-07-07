using ADOLib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebAPIPrj.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIPrj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="admin")]
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
       //[Authorize(Roles = "admin,guest")]
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
        //[Authorize(Roles = "admin,guest")]
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
        //[Authorize(Roles = "admin,guest")]
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
        //[Authorize(Roles = "admin")]
        public IActionResult Post([FromBody] string data)
        {
            AesEncryptionHelper helper = new AesEncryptionHelper();
            string empStr=helper.Decrypt(data);


            Employee emp = JsonSerializer.Deserialize<Employee>(empStr); ;
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
        //[Authorize(Roles = "admin")]
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
        //[Authorize(Roles = "admin")]
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
