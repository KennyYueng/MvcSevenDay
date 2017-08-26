using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSevenDay.Models;
using MvcSevenDay.ViewModels;
using MvcSevenDay.Filters;

namespace MvcSevenDay.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Test
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public string GetString()
        {
            return "Hello World is old now.It's time for wassup bro";
        }


        [Authorize]
        [HeaderFooterFilter]
        public ActionResult Index()
        {
            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();
            //employeeListViewModel.UserName = User.Identity.Name;

            EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
            List<Employee> employees = empBal.GetEmployees();

            List<EmployeeViewModel> empViewModels = new List<EmployeeViewModel>();

            foreach(Employee emp in employees)
            {
                EmployeeViewModel empViewModel = new EmployeeViewModel();
                empViewModel.EmployeeName = emp.FirstName + " " + emp.LastName;
                //empViewModel.Salary = emp.Salary.ToString("C");                
                empViewModel.Salary = emp.Salary.Value.ToString("C");
                if(emp.Salary>15000)
                {
                    empViewModel.SalaryColor = "yellow";
                }
                else
                {
                    empViewModel.SalaryColor = "green";
                }
                empViewModels.Add(empViewModel);
            }
            employeeListViewModel.Employees = empViewModels;

            //employeeListViewModel.FooterData = new FooterViewModel();
            //employeeListViewModel.FooterData.CompanyName = "Kenny Lab";
            //employeeListViewModel.FooterData.Year = DateTime.Now.Year.ToString();
            return View("Index", employeeListViewModel);
        }

        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult AddNew()
        {
            CreateEmployeeViewModel employeeListViewModel = new CreateEmployeeViewModel();
            //employeeListViewModel.FooterData = new FooterViewModel();
            //employeeListViewModel.FooterData.CompanyName = "Kenny Lab";
            //employeeListViewModel.FooterData.Year = DateTime.Now.Year.ToString();
            //employeeListViewModel.UserName = User.Identity.Name;
            return View("CreateEmployee",employeeListViewModel);
        }

        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult SaveEmployee(Employee e,string BtnSubmit)
        {
            switch (BtnSubmit)
            { 
                case "Save Employee":
                    if (ModelState.IsValid)
                    { 
                      EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
                      empBal.SaveEmployee(e);
                      return RedirectToAction("Index");
                    }
                    else
                    {
                        CreateEmployeeViewModel vm = new CreateEmployeeViewModel();
                        vm.FirstName = e.FirstName;
                        vm.LastName = e.LastName;
                        if(e.Salary.HasValue)
                        {
                            vm.Salary = e.Salary.ToString();
                        }
                        else
                        {
                            vm.Salary = ModelState["Salary"].Value.AttemptedValue;
                        }
                        //vm.FooterData = new FooterViewModel();
                        //vm.FooterData.CompanyName = "Kenny Lab";
                        //vm.FooterData.Year = DateTime.Now.Year.ToString();
                        //vm.UserName = User.Identity.Name;
                        return View("CreateEmployee",vm);
                    }
                case "Cancel":
                    return RedirectToAction("Index");
            }
            return new EmptyResult();            
        }

        public ActionResult GetAddnewLink()
        {
            if (Convert.ToBoolean(Session["IsAdmin"]))
            {
                return PartialView("AddNewLink");
            }
            else
            {
                return new EmptyResult();
            }
        }
    }
}