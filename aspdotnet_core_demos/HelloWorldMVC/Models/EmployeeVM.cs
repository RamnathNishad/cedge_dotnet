using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelloWorldMVC.Models
{
    public class EmployeeVM
    {
        public int Ecode {  get; set; }
        public string Ename { get; set; }
        public int Salary {  get; set; }
        public int Deptid { get; set; }

        public List<SelectListItem> DeptIds { get; set; }
    }
}
