
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeesAPI.Models
{
    public class EmployeeDataAcccess : IEmployeDataAccess
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        private readonly IConfiguration config;
        public EmployeeDataAcccess(IConfiguration config)
        {
            con = new SqlConnection();
            con.ConnectionString = config.GetConnectionString("sqlconstr");
            cmd = new SqlCommand();
            cmd.CommandType= CommandType.Text;
            cmd.CommandText = "select * from employees";
            cmd.Connection = con;

            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "employees");
        }
        public List<Employee> GetAllEmps()
        {
            var lstEmps=new List<Employee>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var emp = new Employee
                {
                    Ecode = (int)row[0],
                    Ename = (string)row[1],
                    Salary = (int)row[2],
                    Deptid= (int)row[3],
                };
                lstEmps.Add(emp);
            }
            return lstEmps;
        }
    }
}
