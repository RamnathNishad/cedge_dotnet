using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Security.Cryptography;

namespace ADOLib
{
    public class AdoDisconnected : IEmployeesRepository
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;

        private readonly IConfiguration config;
        public AdoDisconnected(IConfiguration config)
        {
            this.config = config;
            con = new SqlConnection();
            //read the connection string from the appsettings.json file
            var conStr = config.GetConnectionString("sqlconstr");
            con.ConnectionString = conStr;

            //configure data adapter
            da = new SqlDataAdapter("select ecode,ename,salary,deptid from employees", con);
            //create DataSet
            ds=new DataSet();
            //fill the dataset using DataAdpater
            da.Fill(ds, "employees");
            //add primary key constraint
            ds.Tables[0].Constraints.Add("pk1", ds.Tables[0].Columns[0],true);
        }
        public void AddEmployee(Employee employee)
        {
            //a) create a new record from the DataSet table
            DataRow row = ds.Tables[0].NewRow();
            //b) specify the column values for the new row
            row[0] = employee.Ecode;
            row[1] = employee.Ename;
            row[2] = employee.Salary;
            row[3] = employee.Deptid;
            //c) add this row to the dataset table rows collection
            ds.Tables[0].Rows.Add(row);
            //d) save changes database from DataSet using DataAdapter Update method:
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Update(ds, "employees"); //this step only make changes permanent in DB
        }

        public void DeleteEmployee(int ecode)
        {
            //delete records from DataSet
            //find the record to be deleted
            var row = ds.Tables[0].Rows.Find(ecode);
            if (row != null)
            {
                //delete and save changes to DB
                row.Delete();
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Update(ds, "employees");
            }
            else
            {
                throw new Exception("Record not found");
            }
        }

        public void FundsTransfer(int payee, int beneficiary, int amout)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAllEmps()
        {
            var lstEmps=new List<Employee>();
            //traverse each row of the table and add them into collection
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                //get the record from the row
                var emp = new Employee
                {
                    Ecode = (int)row[0],
                    Ename = row[1].ToString(),
                    Salary=(int)row[2],
                    Deptid=(int)row[3]
                };
                //add to the List collection
                lstEmps.Add(emp);
            }

            //return the result
            return lstEmps;
        }

        public Employee GetEmployee(int ecode)
        {
            //find the record in the dataset table rows
            //filtering based on Select criterie, can be used for all
            //types of filtering whether key column or non-key column
            //DataRow[] rows=ds.Tables[0].Select("ecode=" + ecode);
            //var emp = new Employee
            //{
            //    Ecode =(int)rows[0][0],
            //    Ename = rows[0][1].ToString(),
            //    Salary =(int)rows[0][2],
            //    Deptid =(int)rows[0][3]
            //};


            //if the filtering is based on Primary Key or Unique Key column
            DataRow row = ds.Tables[0].Rows.Find(ecode);
            var emp = new Employee
            {
                Ecode = (int)row[0],
                Ename = row[1].ToString(),
                Salary = (int)row[2],
                Deptid = (int)row[3]
            };

            return emp;
        }

        public int GetTotalSalary()
        {
            var totalSalary = 0;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                totalSalary+= (int)row[2];
            }
            return totalSalary;
        }

        public int PlaceOrder(int amount, int quantity)
        {
            throw new NotImplementedException();
        }

        public void UpdateEmployee(Employee employee)
        {
            //find the record for update
            var record = ds.Tables[0].Rows.Find(employee.Ecode);
            if (record != null)
            {
                //update the values 
                record[1] = employee.Ename;
                record[2]=employee.Salary;
                record[3]=employee.Deptid;
                //save changes using DA
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Update(ds, "employees");
            }
            else
            {
                throw new Exception("Record not found");
            }
        }

        public Employee GetEmployeeByEcodeAndDeptid(int ecode, int deptid)
        {
            throw new NotImplementedException();
        }
    }
}
