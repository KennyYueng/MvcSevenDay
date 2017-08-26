using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcSevenDay.Models;
using MvcSevenDay.DataAccessLayer;

namespace MvcSevenDay.ViewModels
{
    public class EmployeeBusinessLayer
    {
        public List<Employee> GetEmployees()
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            return salesDal.Employees.ToList();
        }

        public Employee SaveEmployee(Employee e)
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            salesDal.Employees.Add(e);
            salesDal.SaveChanges();
            return e;
        }

        public bool IsValidUser(UserDetails u)
        {
            if(u.UserName=="Admin" && u.Password=="Admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public UserStatus GetUserValidity(UserDetails u)
        {
            if(u.UserName=="Admin" && u.Password=="Admin")
            {
                return UserStatus.AuthenticatedAdmin;
            }
            else if (u.UserName=="Sukesh" && u.Password=="Sukesh")
            {
                return UserStatus.AuthentucatedUser;
            }
            else
            {
                return UserStatus.NonAuthenticatedUser;
            }
        }

        public void UploadEmployees(List<Employee> employees)
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            salesDal.Employees.AddRange(employees);
            salesDal.SaveChanges();
        }
    }
}