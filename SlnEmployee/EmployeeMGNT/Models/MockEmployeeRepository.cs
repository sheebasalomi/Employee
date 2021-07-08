using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMGNT.Models
{
    public class MockEmployeeRepository : IEmployeeRepository // cntrl + .
    {
        private List<Employee> _employeeList;
   
        public MockEmployeeRepository() // ctor tab tab
        {
            _employeeList = new List<Employee> {
            new Employee() {Id =1, Name = "Jack", Email = "jack@gmail.com", Department = Dept.HR },
            new Employee() {Id =2, Name = "Mary", Email = "mary@gmail.com", Department = Dept.IT },
            new Employee() {Id =3, Name = "Sam", Email = "sam@gmail.com", Department = Dept.Payroll }
            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id = _employeeList.Max(e => e.Id + 1);
            _employeeList.Add(employee);
            return employee;
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == Id);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _employeeList;
        }
    }
}
