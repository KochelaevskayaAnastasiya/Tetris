using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace tetris.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private DataBase database = new DataBase();

        private string login = "";
        private string password = "";

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        [HttpPost]
        public IActionResult OnPost()
        {
            login = Request.Form["login"];
            password = Request.Form["pass"];
            string queryString = "SELECT * FROM Users WHERE Login ='" + login + "' AND Password = '"+password+"';";
            
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                database.closeConnection();
                if (login == "Admin")
                { return RedirectToPage("Menu_admin"); }
                else { return RedirectToPage("Menu_user"); }
            }
            else
            {
                reader.Close();
                database.closeConnection();
                return RedirectToPage("Index");
            }


        }
    }
}