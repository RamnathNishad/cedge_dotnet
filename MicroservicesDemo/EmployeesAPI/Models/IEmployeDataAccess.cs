namespace EmployeesAPI.Models
{
    public interface IEmployeDataAccess
    {
        List<Employee> GetAllEmps();
        void AddEmployee(Employee emp);
        void DeleteEmployee(int ecode);
        void UpdateEmployee(Employee emp);
    }
}
