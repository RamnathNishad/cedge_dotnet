
using Microsoft.Data.SqlClient;
using System.Data;

namespace DepartmentsAPI.Models
{
    public class DepartmentDataAccess : IDepartmentDataAccess
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        private readonly IConfiguration config;
        public DepartmentDataAccess(IConfiguration config)
        {
            con = new SqlConnection();
            con.ConnectionString = config.GetConnectionString("sqlconstr");
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from tbl_department";
            cmd.Connection = con;

            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "tbl_department");
        }
        public List<Department> GetDepts()
        {
            var lstDepts = new List<Department>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var dept = new Department
                {
                    Deptid = (int)row[0],
                    Dname = (string)row[1],
                    Dhead = (int)row[2]
                };
                lstDepts.Add(dept);
            }
            return lstDepts;
        }
    }
}
