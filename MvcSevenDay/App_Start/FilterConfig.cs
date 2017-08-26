using System.Web;
using System.Web.Mvc;
using MvcSevenDay.Logger;

namespace MvcSevenDay
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new EmployeeExceptionFilter());
            //filters.Add(new AuthorizeAttribute());
        }
    }
}
