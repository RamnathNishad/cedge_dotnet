using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ADOLib
{
    //Connected Mode of ADO.NET
    public class EmployeeDataAccess : IEmployeesRepository
    {
        SqlConnection con;
        SqlCommand cmd;
        public EmployeeDataAccess()
        {
            con=new SqlConnection();
            con.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SampleDB;Integrated Security=True;";
        }

        public void AddEmployee(Employee employee)
        {
            //configure command for INSERT
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into employees(ecode,ename,salary,deptid) values(@ec,@en,@sal,@did)";
            //configure the parameters values
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ec", employee.Ecode);
            cmd.Parameters.AddWithValue("@en", employee.Ename);
            cmd.Parameters.AddWithValue("@sal", employee.Salary);
            cmd.Parameters.AddWithValue("@did", employee.Deptid);
            //attach connection 
            cmd.Connection = con;
            //open connection
            con.Open();
            //execute the command
            int recordsAffected = cmd.ExecuteNonQuery();
            if(recordsAffected==0)
            {
                throw new Exception("could not insert record");
            }
            
            //close connection
            con.Close();
        }

        public void DeleteEmployee(int ecode)
        {
            try
            {
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from employees where ecode=@ec";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ec", ecode);
                cmd.Connection = con;
                con.Open();
                int recordsAffected = cmd.ExecuteNonQuery();
                if (recordsAffected == 0)
                {
                    throw new Exception("Record not found,could not delete");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Employee> GetAllEmps()
        {
            //configure command for SELECT ALL
            cmd=new SqlCommand();
            cmd.CommandText = "select ecode,ename,salary,deptid from employees";
            cmd.CommandType = CommandType.Text;

            //attach connection with the command
            cmd.Connection = con;
            //open the connection
            con.Open();
            //execute the command
            SqlDataReader sdr = cmd.ExecuteReader();

            //traverse the records one by one and add to collection
            var lstEmps=new List<Employee>();
            while (sdr.Read())
            {
                var emp = new Employee
                {
                    Ecode = sdr.GetInt32(0),
                    Ename = sdr.GetString(1),
                    Salary=sdr.GetInt32(2),
                    Deptid=sdr.GetInt32(3)
                };
                //add to the collection
                lstEmps.Add(emp);
            }
            //close the connection
            con.Close();
            //return the result
            return lstEmps;
        }

        public Employee GetEmployee(int ecode)
        {
            cmd= new SqlCommand();
            cmd.CommandType= CommandType.Text;
            cmd.CommandText = "select * from employees where ecode=@ec";

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ec", ecode);

            cmd.Connection = con;

            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            if(sdr.Read())
            {
                var emp = new Employee
                {
                    Ecode = sdr.GetInt32(0),
                    Ename = sdr.GetString(1),
                    Salary = sdr.GetInt32(2),
                    Deptid = sdr.GetInt32(3)
                };
                con.Close();
                return emp;
            }
            else
            {
                throw new Exception("Record not found");
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            //configure command for UPDATE
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update employees set ename=@en,salary=@sal,deptid=@did where ecode=@ec";
            //configure the parameters values
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ec", employee.Ecode);
            cmd.Parameters.AddWithValue("@en", employee.Ename);
            cmd.Parameters.AddWithValue("@sal", employee.Salary);
            cmd.Parameters.AddWithValue("@did", employee.Deptid);
            //attach connection 
            cmd.Connection = con;
            //open connection
            con.Open();
            //execute the command
            int recordsAffected = cmd.ExecuteNonQuery();
            if (recordsAffected == 0)
            {
                throw new Exception("could not update the record");
            }

            //close connection
            con.Close();
        }
    }
}
