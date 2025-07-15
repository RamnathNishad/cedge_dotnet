using EFDatabaseFirstApproachDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFDatabaseFirstApproachDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly SampleDbContext _context;
        public EmployeesController(SampleDbContext context)
        {
            this._context = context;
        }
        // GET: api/<EmployeesController>
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return _context.Employees.ToList();
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            //using stored procedure            
            var result = _context.Database.SqlQuery<Employee>(FormattableStringFactory.Create("EXEC GetEmployee {0}", id))
                                          .AsEnumerable<Employee>()
                                          .SingleOrDefault();
            return result;   
            
            //return _context.Employees.Find(id);
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public void Post([FromBody] Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        // PUT api/<EmployeesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Employee emp)
        {
            var record=_context.Employees.Find(id);
            if(record != null)
            {
                record.Ename = emp.Ename;
                record.Salary = emp.Salary;
                record.Deptid = emp.Deptid;
                _context.SaveChanges();
            }
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var record = _context.Employees.Find(id);
            if (record != null)
            {
                _context.Remove(record);
                _context.SaveChanges();
            }
        }
    }
}
