using System.Web;
using System.Web.Mvc;

namespace ThanhToanTrucTuyen
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
