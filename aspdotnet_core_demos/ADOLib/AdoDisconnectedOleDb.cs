using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ADOLib
{
    public class AdoDisconnectedOleDb : IEmployeesRepository
    {
        OleDbConnection con;
        OleDbCommand cmd;
        OleDbDataAdapter da;
        DataSet ds;

        private readonly IConfiguration config;
        public AdoDisconnectedOleDb(IConfiguration config)
        {
            this.config = config;
            con = new OleDbConnection();
            //read the connection string from the appsettings.json file
            var conStr = config.GetConnectionString("sqloledbconstr");
            con.ConnectionString = conStr;

            //configure data adapter
            da = new OleDbDataAdapter("select ecode,ename,salary,deptid from employees", con);
            //create DataSet
            ds = new DataSet();
            //fill the dataset using DataAdpater
            da.Fill(ds, "employees");
            //add primary key constraint
            ds.Tables[0].Constraints.Add("pk1", ds.Tables[0].Columns[0], true);

        }
        public void AddEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmployee(int ecode)
        {
            throw new NotImplementedException();
        }

        public void FundsTransfer(int payee, int beneficiary, int amout)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAllEmps()
        {
            var lstEmps = new List<Employee>();
            //traverse each row of the table and add them into collection
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                //get the record from the row
                var emp = new Employee
                {
                    Ecode = (int)row[0],
                    Ename = row[1].ToString(),
                    Salary = (int)row[2],
                    Deptid = (int)row[3]
                };
                //add to the List collection
                lstEmps.Add(emp);
            }

            //return the result
            return lstEmps;
        }

        public Employee GetEmployee(int ecode)
        {
            throw new NotImplementedException();
        }

        public int GetTotalSalary()
        {
            var totalSalary = 0;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                totalSalary += (int)row[2];
            }
            return totalSalary;
        }

        public int PlaceOrder(int amount, int quantity)
        {
            throw new NotImplementedException();
        }

        public void UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployeeByEcodeAndDeptid(int ecode, int deptid)
        {
            throw new NotImplementedException();
        }
    }
}
