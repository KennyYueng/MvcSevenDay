using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcForms.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        [Authorize]
        public ActionResult Index()
        {
            if(Request.IsAuthenticated)
            { 
                return View();
            }
            else
            {
                return new EmptyResult();
            }
        }
    }
}