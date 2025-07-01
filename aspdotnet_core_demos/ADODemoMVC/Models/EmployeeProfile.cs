using ADOLib;
using AutoMapper;

namespace ADODemoMVC.Models
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<DtoEmployee, Employee>();
        }
    }
}
