using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcForms.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        //public ActionResult Index()
        //{
        //    return View();
        //}

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.Cache.SetNoStore();            
            base.OnActionExecuted(filterContext);
        }
    }
}