using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOLib
{
    public interface IEmployeesRepository
    {
        void AddEmployee(Employee employee);
        void DeleteEmployee(int ecode);
        void UpdateEmployee(Employee employee);

        List<Employee> GetAllEmps();
        Employee GetEmployee(int ecode);
        Employee GetEmployeeByEcodeAndDeptid(int ecode, int deptid);

        int PlaceOrder(int amount, int quantity);

        void FundsTransfer(int payee,int beneficiary,int amout);

        int GetTotalSalary();

    }
}
