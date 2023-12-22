using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace tetris.Pages
{
    public class Menu_adminModel : PageModel 
    {
        public void OnGet()
        {
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
