using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using tetris.Add_classes;
using static System.Net.Mime.MediaTypeNames;

namespace tetris.Pages
{
    public class FiguresModel : PageModel
    {
        private DataBase database = new DataBase();
        public List<Figure> figures = new List<Figure>();

        public int figure_count = 0;
        public int k = 0;

        public List<List<int>> fig = new List<List<int>>();
        public List<String> fig_str = new List<String>();

        public void OnGet()
        {
            string queryString = "SELECT Structure FROM [Shape];";
            //string queryString = "SELECT Structure FROM [Figures];";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                figures.Add(new Figure(reader[0].ToString()));
            }
            figure_count = figures.Count;
            reader.Close();
            database.closeConnection();

            for (int i = 0; i < figure_count; i++)
            {
                List<int> a = new List<int>();
                string str = figures[i].Structure;
                for (int j = 0; j < 16; j++)
                {
                    a.Add(int.Parse(Convert.ToString(str[j])));

                }
                fig.Add(a);
                fig_str.Add(str);
            }

        }

        public int CountFiguBD()
        {
            string queryString = "SELECT COUNT(*) FROM [Shape];";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            int l = 0;
            while (reader.Read())
            {
                l = int.Parse(reader[0].ToString());
            }
            reader.Close();
            database.closeConnection();
            return l;
        }

        [HttpPost]
        public IActionResult OnPost()
        {
            string s = Request.Form["kkk"];
            string[] figures_new = s.Split(',');
            List<String> fig_str2 = new List<String>();
            for(int i = 0; i < figures_new.Length; i++)
            {
                if (CheckFigure.CheckIntegrity(figures_new[i]))
                {
                    fig_str2.Add(figures_new[i]);
                }
            }
            int noIntegrity = figures_new.Length-fig_str2.Count;

            string[] res = CheckFigure.CheckFigures(fig_str2.ToArray());

            int noUnick = fig_str2.Count - res.Length;

            //---------------------------------
            int k = 0;
            int coun = CountFiguBD();
            if (coun < res.Length)
            {
                for (int i = 0; i < coun; i++)
                {
                    k = i + 1;
                    string queryString = "UPDATE Shape SET Structure = '" + res[i] + "' WHERE Shape_Id = '" + k + "';";
                    database.openConnection();
                    SqlCommand command_insert = new SqlCommand(queryString, database.getConnection());
                    int number = command_insert.ExecuteNonQuery();
                    Console.WriteLine("Изменено: {0}", number);
                    database.closeConnection();
                }

                for (int j = coun; j < res.Length; j++)
                {
                    string queryString = "INSERT INTO[Shape] VALUES('" + res[j] + "');";
                    database.openConnection();
                    SqlCommand command_insert = new SqlCommand(queryString, database.getConnection());
                    int number = command_insert.ExecuteNonQuery();
                    Console.WriteLine("Добавлено: {0}", number);
                    database.closeConnection();
                }
            }
            else
            {
                string queryString2 = "DELETE FROM Shape;";
                database.openConnection();
                SqlCommand command_insert2 = new SqlCommand(queryString2, database.getConnection());
                int number2 = command_insert2.ExecuteNonQuery();
                Console.WriteLine("Изменено: {0}", number2);
                database.closeConnection();

                for (int i = 0; i < res.Length; i++)
                {
                    string queryString = "INSERT INTO[Shape] VALUES('" + res[i] + "');";
                    database.openConnection();
                    SqlCommand command_insert = new SqlCommand(queryString, database.getConnection());
                    int number = command_insert.ExecuteNonQuery();
                    Console.WriteLine("Добавлено: {0}", number);
                    database.closeConnection();
                }
            }

            Console.WriteLine("Не целостны:"+ noIntegrity+"\nНе уникальны:"+ noUnick);
            return RedirectToPage("Figures");
        }
    }
}
