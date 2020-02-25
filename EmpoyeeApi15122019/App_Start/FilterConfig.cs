using System.Web;
using System.Web.Mvc;

namespace EmpoyeeApi15122019
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
