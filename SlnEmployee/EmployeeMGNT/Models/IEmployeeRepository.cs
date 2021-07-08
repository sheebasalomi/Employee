using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMGNT.Models
{
    public interface IEmployeeRepository
    {
        // models in MVC : a set of calsses that represent data and the logic to manage the data
        Employee GetEmployee(int Id);
        IEnumerable<Employee> GetEmployees();
        Employee Add(Employee employee);
    }
}
