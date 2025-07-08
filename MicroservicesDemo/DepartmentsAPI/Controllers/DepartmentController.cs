using DepartmentsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentDataAccess dal;
        public DepartmentController(IDepartmentDataAccess dal)
        {
            this.dal = dal;
        }

        [HttpGet]
        [Route("GetDepts")]
        public IActionResult GetDepartment()
        {
            return Ok(dal.GetDepts());
        }
    }
}
