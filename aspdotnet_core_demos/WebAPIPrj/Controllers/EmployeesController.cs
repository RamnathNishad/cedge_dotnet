using ADOLib;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIPrj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public IEnumerable<Employee> Get()
        {
            return dal.GetAllEmps();
        }

        // GET api/<EmployeesController>/5
        [HttpGet]
        [Route("GetEmpById/{id:int}")]
        public Employee GetEmpById(int id)
        {
            return dal.GetEmployee(id);
        }

        [HttpGet]
        [Route("GetEmpsByDeptId/{id:int}")]
        public List<Employee> GetEmpsByDeptId(int id)
        {
            return dal.GetAllEmps()
                      .Where(p=>p.Deptid==id)
                      .ToList();
        }


        // POST api/<EmployeesController>
        [HttpPost]
        [Route("AddEmployee")]
        public void Post([FromBody] Employee emp)
        {
            dal.AddEmployee(emp);
        }

        // PUT api/<EmployeesController>/5
        [HttpPut]
        [Route("UpdateEmp/{id}")]
        public void Put(int id, [FromBody] Employee emp)
        {
            dal.UpdateEmployee(emp);
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete]
        [Route("DeleteEmp/{id}")]
        public void Delete(int id)
        {
            dal.DeleteEmployee(id);
        }
    }
}
