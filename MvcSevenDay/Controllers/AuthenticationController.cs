using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSevenDay.ViewModels;
using System.Web.Security;
using MvcSevenDay.Models;

namespace MvcSevenDay.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Login()
        {
            return View();
        }

        //為什麼添加HttpPost屬性？
        //該屬性可使得DoLogin方法打開Post請求。如果有人嘗試獲取DoLogin，將不會起作用。
        [HttpPost]
        public ActionResult DoLogin(UserDetails u)
        {
            if (ModelState.IsValid)
            {
                EmployeeBusinessLayer bal = new EmployeeBusinessLayer();
                UserStatus status = bal.GetUserValidity(u);
                bool IsAdmin = false;
                if(status==UserStatus.AuthenticatedAdmin)
                {
                    IsAdmin = true;
                }
                else if (status==UserStatus.AuthentucatedUser)
                {
                    IsAdmin = false;
                }
                else
                {
                    ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                    return View("Login");
                }
                FormsAuthentication.SetAuthCookie(u.UserName, false);
                Session["IsAdmin"] = IsAdmin;
                return RedirectToAction("Index", "Employee");


                //if (bal.IsValidUser(u))
                //{
                //    //創建認證Cookie
                //    FormsAuthentication.SetAuthCookie(u.UserName, false);
                //    return RedirectToAction("Index", "Employee");
                //}
                //else
                //{
                //    ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                //    return View("Login");
                //}
            }
            else
            {
                return View("Login");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }

}