using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;

namespace tetris.Pages
{
    public class SettingsModel : PageModel
    {
        public string color = "";
        public string mus = "";
        public string setka = "";
        public string next_figu = "";
        public string login = "";
        public void OnGet()
        {
            color = RouteData.Values["color"].ToString();
            mus = RouteData.Values["mus"].ToString();
            setka = RouteData.Values["setka"].ToString();
            next_figu = RouteData.Values["next_figu"].ToString();
            login = RouteData.Values["login"].ToString();
        }
        [HttpPost]
        public IActionResult OnPost()
        {
            login = RouteData.Values["login"].ToString();
            color = Request.Form["radio"];

            mus = Request.Form["checkbox_mus"];
            setka = Request.Form["checkbox_setka"];
            next_figu = Request.Form["checkbox_next"];
            if (mus == null) mus = "null";
            if (setka == null) setka = "null";
            if (next_figu == null) next_figu = "null";
            return RedirectToPage("Menu_user", new {login = login, color= color, mus= mus, setka = setka , next_figu= next_figu });
        }
    }
}
