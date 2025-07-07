using ADOLib;
using AutoMapper;
using Newtonsoft.Json;

using Newtonsoft.Json;
using NuGet.Common;
using System.Text.Json;

namespace ADODemoMVC.Models
{
    public class ApiConsumer
    {      

        static string baseUrl = "http://localhost:5142/api/Employees/";
        
        public string Authenticate(string uname,string pwd,string userRole)
        {
            //call the login api authenticate method
            string baseAddress = "http://localhost:5142/api/Account/authenticate";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                var response=client.PostAsJsonAsync("", new { username = uname, password = pwd ,role=userRole});
                response.Wait();
                
                var result=response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsStringAsync();
                    data.Wait();
                    var token = data.Result;
                    return token;
                }
                else
                {
                    return null;
                }
            }
        }
        public  List<DtoEmployee> GetAllEmps(string token)
        { 
            var dtoLst = new List<DtoEmployee>();
            //call the web api GetAll using HttpClient
            using (var http=new HttpClient())
            {
                http.BaseAddress = new Uri(baseUrl);

                //attach bearer token to the Http authorization header
                http.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
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
                    dtoLst=System.Text.Json.JsonSerializer.Deserialize<List<DtoEmployee>>(dataStr);
 
                    ////configure and create mapper object
                    //MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<DtoEmployee,Employee>());
                    ////create the mapper using configuration
                    //IMapper mapper=config.CreateMapper();
                    ////map source to destination using mapper
                    //list=_mapper.Map<List<Employee>>(dtoLst);
                }
                else
                {
                    var dataErr = responseResult.Content.ReadAsStringAsync();
                    dataErr.Wait();
                    //get the data records
                    var dataErrStr = dataErr.Result;
                    throw new Exception("some server error:"+ dataErrStr);
                }
            }

            return dtoLst;
        }

        public bool AddEmployee(Employee employee,string token)
        {
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(baseUrl);
                //attach bearer token to the Http authorization header
                http.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var encryptedEmployee = new AesEncryptionHelper().Encrypt(JsonConvert.SerializeObject(employee));
                var response = http.PostAsJsonAsync("AddEmployee", encryptedEmployee);
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
                    dto = System.Text.Json.JsonSerializer.Deserialize<DtoEmployee>(dataStr);
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
