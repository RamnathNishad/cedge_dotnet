using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HelloWorldMVC.Models
{
    public class EmployeeVM : IValidatableObject
    {
        [Required]
        [RegularExpression(@"\d{3,3}",ErrorMessage ="ecode must be exactly 3-digits")]
        public int Ecode { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3,ErrorMessage ="employee name must have min 3 char and max 10 char")]
        public string Ename { get; set; }

        [Required]
        //[Range(1000, 20000)]
        //[Remote("CheckSalaryRange","Home",ErrorMessage ="salary must be between 1000 and 2000")]

        [CheckSalary(ErrorMessage ="Salary must be between 1000 and 2000")]
        public int Salary { get; set; }

        [Required]
        public int Deptid { get; set; }

        [Required] 
        public string Password {  get; set; }

        [Required]
        public string ConfirmPassword {  get; set; }

        public List<SelectListItem>? DeptIds { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Password!=ConfirmPassword)
            {
                yield return new ValidationResult("Both the password must be same");
            }
        }
    }


    public class CheckSalaryAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var salary = (int)value;
            if(salary<1000 || salary >2000)
            {
                return false;
            }
            return true;
        }
    }
}

