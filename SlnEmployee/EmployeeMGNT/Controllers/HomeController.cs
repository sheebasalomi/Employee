using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMGNT.Models;
using EmployeeMGNT.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeMGNT.Controllers
{
    //[Route("Home")]
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

        //[Route("~/")]
        //[Route("Index")]
        public ViewResult Index()
        {
            return View(_employeeRepository.GetEmployees());
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

        public ViewResult EmpDetails()
        {
            // when we return the View it will look for the Details.cshtml file in the folder View/Home
            // we can load a view with different name : View("Test", model) this will look for file Test.cshtml in View/Home
            //View("Demo/Test.cshtml", model) this will look for file Test.cshtml in root/Demo folder, /Demo/Test, ~/Demo/Test can also be used
            // view("../Demo/Test",model) this will look for file Test.cshtml in Views/Demo as MVC first look in Views/Home
            // we can give absolute or relative path, in absolute path .cshtml extension should be included

            //ViewData["Employee"] =  _employeeRepository.GetEmployee(1);
            //ViewData["PageTitle"] = "Employee Details(ViewData Demo)";//ViewData use dinamic key and no compile time checking and intellisense
            //return View();


            //ViewBag.PageTitle = "Employee Details(ViewBag Demo)";
            //ViewBag.Employee = _employeeRepository.GetEmployee(2);//ViewBag use dynamic property and no compile time checking and intellisense
            //return View();

            // preffered approch to pass data from controller to view is StronglyTyped View

            //ViewBag.PageTitle = "Employee Details";
            //Employee model = _employeeRepository.GetEmployee(1);
            //return View(model);

            //---ViewModel

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployee(1),
                PageTitle = "Employee Details"
            };

            return View(homeDetailsViewModel);

            }
        //[Route("Details/{id?}")]
        public ViewResult Details(int? id)
        {
           // id = (id == 0) ? 1 : id;
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployee(id??1),
                PageTitle = "Employee Details"
            };

            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public RedirectToActionResult Create(Employee employee)
        {
           Employee NewEmployee =  _employeeRepository.Add(employee);
            return RedirectToAction("details",new { id = NewEmployee.Id});
        }
    }




    //<h3>@ViewData["PageTitle"]</h3><!--only string need no casting-->
    //@{
    //    var employee = ViewData["Employee"] as EmployeeMGNT.Models.Employee;
    //}

    //<div>
    //    Name : @employee.Name
    //</div>
    //<div>
    //    Email : @employee.Email
    //</div>
    //<div>
    //    Department : @employee.Department
    //</div>


    //<h3>@ViewBag.PageTitle</h3><!--no need of casting-->

    //<div>
    //    Name : @ViewBag.Employee.Name
    //</div>
    //<div>
    //    Email : @ViewBag.Employee.Email
    //</div>
    //<div>
    //    Department : @ViewBag.Employee.Department
    //</div>

    //@model EmployeeMGNT.Models.Employee
    // <h3>@ViewBag.PageTitle</h3><!--no need of casting-->

    //<div>
    //    Name : @Model.Name
    //</div>
    //<div>
    //    Email :  @Model.Email
    //</div>
    //<div>
    //    Department :  @Model.Department
    //</div>


}
