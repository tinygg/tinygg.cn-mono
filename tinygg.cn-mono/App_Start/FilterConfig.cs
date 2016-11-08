using System.Web;
using System.Web.Mvc;

namespace tinygg.cn_mono
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}