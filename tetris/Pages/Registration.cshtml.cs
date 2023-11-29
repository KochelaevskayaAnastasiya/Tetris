using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace tetris.Pages
{
    public class RegistrationModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private string login = "";
        private string password = "";

        public RegistrationModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        [HttpPost]
        public async void OnPost()
        {
            login = Request.Form["login"];
            password = Request.Form["pass1"];
            string connectionString = "Data source=.\\SQLEXPRESS;database=Tetris; Integrated security=true;";
            //string sqlExpression = "INSERT INTO [User] (ID_user,Login,Password) VALUES (1,'Tom', '18')";
            string sqlExpression = "INSERT INTO [User] (ID_user,Login,Password) VALUES (1,'"+login+"', '"+password+"')";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                Console.WriteLine("Добавлено объектов: {0}", number);
            }

            /*SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                // Открываем подключение
                await connection.OpenAsync();
                Console.WriteLine("Подключение открыто");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // если подключение открыто
                if (connection.State == ConnectionState.Open)
                {

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
                    {
                        dataAdapter.InsertCommand = new SqlCommand("INSERT [User] VALUES (1,'"+login+"','"+password+"' );", connection);
                        dataAdapter.InsertCommand = new SqlCommand("INSERT [User] VALUES (2,'12','12');", connection);

                    }
                    //connection.Open();
                    try
                    {
                        SqlCommand command = new SqlCommand();
                        command.CommandText = "INSERT [User] VALUES (1,'12','12');";
                        command.Connection = connection;


                        SqlCommand command2 = new SqlCommand();
                        command2.CommandText = "SELECT * FROM [User]";
                        command2.Connection = connection;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    // закрываем подключение
                    await connection.CloseAsync();
                    Console.WriteLine("Подключение закрыто...");
                }
            }*/

        }
    }
}
