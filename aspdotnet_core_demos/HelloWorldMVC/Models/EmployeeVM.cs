using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HelloWorldMVC.Models
{
    public class EmployeeVM
    {
        [Required]
        [RegularExpression(@"\d{3,3}")]
        public int Ecode {  get; set; }

        [Required]
        [StringLength(10,MinimumLength =3)]
        public string Ename { get; set; }

        [Required]
        [Range(1000,20000)]
        public int Salary {  get; set; }

        [Required]
        public int Deptid { get; set; }

        
        public List<SelectListItem>? DeptIds { get; set; }
    }
}
