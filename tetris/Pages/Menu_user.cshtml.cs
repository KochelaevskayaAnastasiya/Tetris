using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Reflection;

namespace tetris.Pages
{
    public class Menu_userModel : PageModel
    {
        public void OnGet()
        {
        }
        public PartialViewResult OnGetViewModalInfo()
        {
            // this handler returns _ContactModalPartial
            return new PartialViewResult
            {
                ViewName = "_ViewModalInfo",
                ViewData = new ViewDataDictionary<InfoAboutUs>(ViewData, new InfoAboutUs { })
            
            };
        }
    }
}
