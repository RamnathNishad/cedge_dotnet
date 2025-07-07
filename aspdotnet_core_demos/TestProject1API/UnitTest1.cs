using ADOLib;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPIPrj.Controllers;


namespace TestProject1API
{    
    public class UnitTestEmployeeWebAPI
    {
        [Fact]
        //[Theory]
        //[InlineData(10,20)]
        //[InlineData(100,200)]
        //[InlineData(50,60)]
        public void Test1_Divide_Numbers()
        {
            //Arrange
            Mock<IEmployeesRepository> mock = new Mock<IEmployeesRepository>();
            var empController = new EmployeesController(mock.Object);
            int a = 20, b = 10;
            var expectedResult = 2;

            //Act
            var actualResult = (OkObjectResult)empController.Divide(a, b);
            var actualValue = (int)actualResult.Value;

            //Assert
            Assert.Equal(expectedResult, actualValue);
        }

        [Fact]
        public void Test_Get_Employee_By_Id()
        {
            //Arrange
            Mock<IEmployeesRepository> mock = new Mock<IEmployeesRepository>();
            int ecode = 101;
            var expectedResultFromDB = new Employee
            {
                Ecode=101,
                Ename="Ravi",
                Salary=1111,
                Deptid=201
            };

            EmployeesController controller = new EmployeesController(mock.Object);
            mock.Setup(m => m.GetEmployee(ecode)).Returns(expectedResultFromDB);


            //Act
            var actualResult = (Employee)((OkObjectResult)controller.GetEmpById(ecode)).Value;

            //Assert
            Assert.Equal(expectedResultFromDB.Ecode, actualResult.Ecode);
            Assert.Equal(expectedResultFromDB.Ename, actualResult.Ename);
            Assert.Equal(expectedResultFromDB.Salary, actualResult.Salary);
            Assert.Equal(expectedResultFromDB.Deptid, actualResult.Deptid);
        }
    }
}