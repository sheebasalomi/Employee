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

        //public JsonResult Details()
        //{
        //    Employee model = _employeeRepository.GetEmployee(1);
        //    return Json(model);
        //}

        //public ObjectResult Details()
        //{
        //    Employee model = _employeeRepository.GetEmployee(1);
        //    return new ObjectResult(model);
        //}

        public ViewResult Details()
        {
            Employee model = _employeeRepository.GetEmployee(1);
            ViewData["Employee"] = model;
            ViewData["PageTitle"] = "Employee Details(ViewData Demo)";//ViewData use dinamic key and no compile time checking and intellisense

            ViewBag.PageTitle = "Employee Details(ViewBag Demo)";
            ViewBag.Employee = _employeeRepository.GetEmployee(2);//ViewBag use dynamic property and no compile time checking and intellisense

            // preffered approch to pass data from controller to view is StronglyTyped View
            
            
            
            return  View();

            // when we return the View it will look for the Details.cshtml file in the folder View/Home
            // we can load a view with different name : View("Test", model) this will look for file Test.cshtml in View/Home
            //View("Demo/Test.cshtml", model) this will look for file Test.cshtml in root/Demo folder, /Demo/Test, ~/Demo/Test can also be used
            // view("../Demo/Test",model) this will look for file Test.cshtml in Views/Demo as MVC first look in Views/Home
            // we can give absolute or relative path, in absolute path .cshtml extension should be included
        }
        
}
}
