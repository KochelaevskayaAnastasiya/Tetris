using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace tetris.Pages
{
    public class AboutSystem2Model : PageModel
    {
        public string textHTML;
        public int isError = 0;

        public async void OnGet()
        {
            string path = "file/AboutSystem2.txt";
            if (System.IO.File.Exists(path))
            {
                // асинхронное чтение
                using (StreamReader reader = new StreamReader(path))
                {
                    textHTML = await reader.ReadToEndAsync();
                }
            }
            else
            {
                isError = 1;
            }
        }
    }
}
