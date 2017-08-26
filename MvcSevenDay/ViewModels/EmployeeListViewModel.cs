using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSevenDay.ViewModels
{
    public class EmployeeListViewModel:BaseViewModel
    {
        public List<EmployeeViewModel> Employees { get; set; }
    }
}