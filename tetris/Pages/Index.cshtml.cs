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
        public string error_text = "";

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(string error_text2)
        {
            error_text = error_text2;
        }

        [HttpPost]
        public IActionResult OnPost()
        {
            login = Request.Form["login"];
            password = Request.Form["pass"];
            try
            {
                string queryString = "SELECT * FROM Users WHERE Login ='" + login + "' AND Password = '" + password + "';";

                SqlCommand command = new SqlCommand(queryString, database.getConnection());
                database.openConnection();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    database.closeConnection();
                    if (login == "Admin")
                    { return RedirectToPage("Menu_admin"); }
                    else
                    {
                        return RedirectToPage("Menu_user", new { login = this.login });
                    }
                }
                else
                {
                    reader.Close();
                    database.closeConnection();
                    return RedirectToPage("Index");
                }
            }
            catch (SqlException ex)
            {
                string message = "";

                int num = ex.Number;
                if (num == 53)
                {
                    message = "Нет доступа к базе данных.";
                }
                else
                {
                    if (num== 4060)
                    {
                        message = "База данный отсутствует.";
                    }
                    else
                    {
                        message = ex.Message;
                    }
                }

                return RedirectToPage("Index", new { error_text2 = message });
            }


        }
    }
}