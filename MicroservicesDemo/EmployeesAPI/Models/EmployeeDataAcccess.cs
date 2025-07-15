
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
            ds.Tables[0].Constraints.Add("pk1", ds.Tables[0].Columns[0], true);
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

        public void AddEmployee(Employee emp)
        {
            var record = ds.Tables[0].NewRow();
            record[0] = emp.Ecode;
            record[1]= emp.Ename;
            record[2]= emp.Salary;
            record[3]= emp.Deptid;
            ds.Tables[0].Rows.Add(record);

            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Update(ds, "employees");
        }

        public void DeleteEmployee(int ecode)
        {
            var record = ds.Tables[0].Rows.Find(ecode);
            if (record != null)
            {
                record.Delete();
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Update(ds, "employees");
            }
        }

        public void UpdateEmployee(Employee emp)
        {
            var record = ds.Tables[0].Rows.Find(emp.Ecode);
            if (record != null)
            {
                record[1] = emp.Ename;
                record[2] = emp.Salary;
                record[3]=emp.Deptid;

                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Update(ds, "employees");
            }
        }
    }
}
