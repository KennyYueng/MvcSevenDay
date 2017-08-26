using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcForms.Models;
using System.Web.Security;

namespace MvcForms.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Login()
        {
            Session.Clear();
            return View();
        }

        public ActionResult DoLogin(UserDetails u)
        {
            if(u.UserName=="Admin" && u.Password=="Admin")
            {
                FormsAuthentication.SetAuthCookie(u.UserName, false);
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                return View("Login");
            }

        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            //return RedirectToAction("Login");

            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.Cache.SetNoStore();
            Response.Write("<script>window.location.href='Login'</script>");
        }
    }
}