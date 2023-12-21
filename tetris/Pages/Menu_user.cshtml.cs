using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NuGet.Protocol.Plugins;
using System.Reflection;

namespace tetris.Pages
{
    public class Menu_userModel : PageModel
    {
        public string color = "1";
        public string mus = "null";
        public string setka = "null";
        public string next_figu = "null";

        public string login = "";

        public void OnGet()
        {
            color = RouteData.Values["color"].ToString();
            mus = RouteData.Values["mus"].ToString();
            setka = RouteData.Values["setka"].ToString();
            next_figu = RouteData.Values["next_figu"].ToString();
            login = RouteData.Values["login"].ToString();
        }
        public PartialViewResult OnGetViewModalInfo()
        {
            int k = 5;
            // this handler returns _ContactModalPartial
            return new PartialViewResult
            {
                ViewName = "_ViewModalInfo",
                ViewData = new ViewDataDictionary<InfoAboutUs>(ViewData, new InfoAboutUs { })
            
            };
        }
    }
}
