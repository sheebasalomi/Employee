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
            new Employee() {Id =1, Name = "Jack", Email = "jack@gmail.com", Department = "Marketing" },
            new Employee() {Id =2, Name = "Mary", Email = "mary@gmail.com", Department = "Sales" },
            new Employee() {Id =3, Name = "Sam", Email = "sam@gmail.com", Department = "IT" }
            };
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == Id);
        }
    }
}
