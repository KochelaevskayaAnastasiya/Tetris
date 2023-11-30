using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;


namespace tetris.Pages
{
    public class RegistrationModel : PageModel
    {
        private DataBase database = new DataBase();
        private readonly ILogger<IndexModel> _logger;

        private string login = "";
        private string password = "";
        private string password2 = "";

        public RegistrationModel(ILogger<IndexModel> logger)
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
            password = Request.Form["pass1"];
            password2 = Request.Form["pass2"];
            if (string.Compare(password, password2)==0)
            {
                string queryString = "SELECT * FROM Users WHERE Login ='"+login+"';";

                SqlCommand command = new SqlCommand(queryString, database.getConnection());
                database.openConnection();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                }
                else
                {
                    reader.Close();
                    string sqlExpression = "INSERT INTO Users VALUES ('" + login + "', '" + password + "')";
                    SqlCommand command_insert = new SqlCommand(sqlExpression, database.getConnection());
                    int number = command_insert.ExecuteNonQuery();
                    Console.WriteLine("Добавлен пользователь: {0}", number);
                }
                database.closeConnection();
                return RedirectToPage("Index");

            }
            else
            {
                return RedirectToPage("Registration");
                //webBrowser1.Document.GetElementById("body").InnerText = "text";

            }

        }
    }
}
