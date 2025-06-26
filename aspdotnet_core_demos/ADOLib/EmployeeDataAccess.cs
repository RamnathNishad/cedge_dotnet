using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace ADOLib
{
    //Connected Mode of ADO.NET
    public class EmployeeDataAccess : IEmployeesRepository
    {
        SqlConnection con;
        SqlCommand cmd;
        private readonly IConfiguration config;

        public EmployeeDataAccess(IConfiguration config)
        {           
            this.config = config;
            con=new SqlConnection();
            //read the connection string from the appsettings.json file
            var conStr = config.GetConnectionString("sqlconstr");
            con.ConnectionString = conStr;
            //var uname = config["credential:username"];
            //var pwd = config["credential:password"];
        }

        public void AddEmployee(Employee employee)
        {
            //configure command for INSERT
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure; //mandatory for stored procedure
            cmd.CommandText = "sp_insert";
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
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_delete";
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
            cmd.CommandText = "sp_getemps";
            cmd.CommandType = CommandType.StoredProcedure;

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
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.CommandText = "sp_getbyid";

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
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_update";
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

        public int PlaceOrder(int amount, int quantity)
        {
            cmd = new SqlCommand();
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.CommandText = "sp_place_order";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@qty", quantity);
            cmd.Parameters.AddWithValue("@order_id", 0);
            //specify the direction of the parameter order_id
            cmd.Parameters[2].Direction = ParameterDirection.Output;

            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            //get the value of the parameter
            var orderid = (int)cmd.Parameters[2].Value;
           //close the connection only after retrieving the value
            con.Close();

            return orderid;
        }

        public void FundsTransfer(int payee, int beneficiary, int amount)
        {
            SqlTransaction T = null;            
            try
            {
                SqlCommand cmd1 = new SqlCommand();
                cmd1.CommandText = "update account set balance=balance-@amount where account_no=@accno";
                cmd1.CommandType = CommandType.Text;
                cmd1.Parameters.Clear();
                cmd1.Parameters.AddWithValue("@accno", payee);
                cmd1.Parameters.AddWithValue("@amount", amount);


                SqlCommand cmd2 = new SqlCommand();
                cmd2.CommandText = "update account set balance=balance+@amount where account_no=@accno";
                cmd2.CommandType = CommandType.Text;
                cmd2.Parameters.Clear();
                cmd2.Parameters.AddWithValue("@accno", beneficiary);
                cmd2.Parameters.AddWithValue("@amount", amount);

                cmd1.Connection = con; ;
                cmd2.Connection = con;

                con.Open();
                //initiate the transaction once connection is opened
                T=con.BeginTransaction();
                //group the Sql Commands within the transaction
                cmd1.Transaction = T;
                cmd2.Transaction = T;

                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();

                //commit the transaction
                T.Commit();
            }
            catch (Exception ex)
            {
                //rollback the transaction
                T.Rollback();
                throw new Exception("Transaction rolled back");
            }
            finally
            {
                con.Close();
            }
        }
    }
}
