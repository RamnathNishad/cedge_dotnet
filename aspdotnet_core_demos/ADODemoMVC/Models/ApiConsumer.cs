using ADOLib;
using AutoMapper;
using System.Text.Json;

namespace ADODemoMVC.Models
{
    public class ApiConsumer
    {      

        static string baseUrl = "http://localhost:5142/api/Employees/";
        public  List<DtoEmployee> GetAllEmps()
        { 
            var dtoLst = new List<DtoEmployee>();
            //call the web api GetAll using HttpClient
            using (var http=new HttpClient())
            {
                http.BaseAddress = new Uri(baseUrl);
                var response = http.GetAsync("GetAllEmps");
                response.Wait();

                //get the result from the response
                var responseResult=response.Result;
                //if the response is success
                if(responseResult.IsSuccessStatusCode)
                {
                    //read the content of the response
                    var data=responseResult.Content.ReadAsStringAsync();
                    data.Wait();
                    //get the data records
                    var dataStr = data.Result;
                    //convert the json string into object
                    dtoLst=JsonSerializer.Deserialize<List<DtoEmployee>>(dataStr);
                    
                    
                    ////map the dto to employee
                    //foreach (var item in dtoLst)
                    //{
                    //    var emp = new Employee
                    //    {
                    //        Ecode = item.ecode,
                    //        Ename = item.ename,
                    //        Salary = item.salary,
                    //        Deptid = item.deptid
                    //    };
                    //    list.Add(emp);
                    //}

                    ////configure and create mapper object
                    //MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<DtoEmployee,Employee>());
                    ////create the mapper using configuration
                    //IMapper mapper=config.CreateMapper();
                    ////map source to destination using mapper
                    //list=_mapper.Map<List<Employee>>(dtoLst);
                }
                else
                {
                    throw new Exception("some server error");
                }
            }

            return dtoLst;
        }

        public bool AddEmployee(Employee employee)
        {
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(baseUrl);
                var response = http.PostAsJsonAsync("AddEmployee", employee);
                response.Wait();
                var responseResult = response.Result;
                if (responseResult.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public string DeleteEmp(int ecode)
        {
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(baseUrl);
                var response = http.DeleteAsync($"DeleteEmp/{ecode}");
                response.Wait();
                var responseResult = response.Result;
                if (responseResult.IsSuccessStatusCode)
                {
                    return "Record deleted";
                }
                else
                {
                    return "could not delete record";
                }
            }
        }

        public string UpdateEmp(Employee emp)
        {
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(baseUrl);
                var response=http.PutAsJsonAsync($"UpdateEmp/{emp.Ecode}",emp);
                response.Wait();
                var responseResult = response.Result;
                if (responseResult.IsSuccessStatusCode)
                {
                    return "Record updated";
                }
                else
                {
                    return "Could not update the record";
                }
            }
        }

        public DtoEmployee GetEmpById(int ecode)
        {
            var dto = new DtoEmployee();
            //call the web api GetAll using HttpClient
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(baseUrl);
                var response = http.GetAsync($"GetEmpById/{ecode}");
                response.Wait();

                //get the result from the response
                var responseResult = response.Result;
                //if the response is success
                if (responseResult.IsSuccessStatusCode)
                {
                    //read the content of the response
                    var data = responseResult.Content.ReadAsStringAsync();
                    data.Wait();
                    //get the data record
                    var dataStr = data.Result;
                    //convert the json string into object
                    dto = JsonSerializer.Deserialize<DtoEmployee>(dataStr);
                }
                else
                {
                    throw new Exception("some server error");
                }
            }

            return dto;
        }
    }
}
