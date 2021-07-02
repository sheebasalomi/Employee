using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeMGNT.Controllers
{
    [Route("[controller]")] //routing by token
    public class DepartmentController : Controller
    {
        // GET: /<controller>/
        [Route("[action]")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("[action]")]
        public string List()
        {
            return "List of Department";
        }
        [Route("[action]/{id?}")]
        public string Details()
        {
            return "Details of Department";
        }
    }
}
