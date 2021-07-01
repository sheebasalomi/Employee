using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMGNT.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeMGNT.Controllers
{
    public class HomeController : Controller
    {
        private IEmployeeRepository _employeeRepository;
        public HomeController(IEmployeeRepository employeeRepository) // here employeeRepository is injected to the Home Controller through constructor, it is called constructor injection
        {
            _employeeRepository = employeeRepository;
        }     

        // GET: /<controller>/
        //public JsonResult Index()
        //{
        //    return Json(new {id = 1, name = "Jack" });
        //}

        public string Index()
        {
            return _employeeRepository.GetEmployee(1).Name;
        }
    }
}
