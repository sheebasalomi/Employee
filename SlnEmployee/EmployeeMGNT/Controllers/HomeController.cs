using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMGNT.Models;
using EmployeeMGNT.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeMGNT.Controllers
{
    //[Route("Home")]
    public class HomeController : Controller
    {
        private IEmployeeRepository _employeeRepository;

        public IWebHostEnvironment _hostingEnvironment;

        public HomeController(IEmployeeRepository employeeRepository,
                                IWebHostEnvironment hostingEnvironment) // here employeeRepository is injected to the Home Controller through constructor, it is called constructor injection
        {
            _employeeRepository = employeeRepository;
            _hostingEnvironment = hostingEnvironment;
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
        [HttpGet]
        public ViewResult Details(int? id)
        {
            // id = (id == 0) ? 1 : id;

            Employee employee = _employeeRepository.GetEmployee(id.Value);
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }
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

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    employee.PhotoPath = ProcessUploadedFile(model);
                }

                _employeeRepository.Update(employee);
                return RedirectToAction("index");
            }
            return View();
        }

        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
                   
            }

            return uniqueFileName;
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);


                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };
                    
                _employeeRepository.Add(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }
            return View();
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
