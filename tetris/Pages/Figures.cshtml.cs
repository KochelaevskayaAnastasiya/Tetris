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

        public int figure_count=0;
        public int k=0;

        public List<List<int>> fig = new List<List<int>>();
        public List<String> fig_str = new List<String>();

        public void OnGet()
        {
            string queryString = "SELECT Structure FROM [Figures];";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                figures.Add(new Figure(reader[0].ToString()));
            }
            figure_count=figures.Count;
            reader.Close();
            database.closeConnection();

            for(int i = 0; i < figure_count; i++)
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
        public bool CheckFigure(string fi)
        {
            /*for(int k=0;k< fig_str.Count; k++)
            {
                string queryString = "SELECT * FROM Figures WHERE Structure ='" + fi + "';";

                SqlCommand command = new SqlCommand(queryString, database.getConnection());
                database.openConnection();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    database.closeConnection();
                }
                else
                {
                    reader.Close();
                    string sqlExpression = "INSERT INTO Users VALUES ('" + login + "', '" + password + "')";
                    SqlCommand command_insert = new SqlCommand(sqlExpression, database.getConnection());
                    int number = command_insert.ExecuteNonQuery();
                    Console.WriteLine("Добавлен пользователь: {0}", number);
                    database.closeConnection();
                }
            }*/
            return true;
        }
        public int CountFiguBD()
        {
            string queryString = "SELECT COUNT(*) FROM [Figures];";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            int l=0;
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
            int k = 0;
            int coun = CountFiguBD();
            for (int i=0;i<coun;i++)
            {
                k = i + 1;
                string queryString = "UPDATE Figures SET Structure = '"+figures_new[i]+ "' WHERE ID_figure = '" + k + "';";
                database.openConnection();
                SqlCommand command_insert = new SqlCommand(queryString, database.getConnection());
                int number = command_insert.ExecuteNonQuery();
                Console.WriteLine("Изменено: {0}", number);
                database.closeConnection();
            }

            for(int j = coun; j < figures_new.Length; j++) 
            {
                string queryString = "INSERT INTO[Figures] VALUES('" + figures_new[j] + "');";
                database.openConnection();
                SqlCommand command_insert = new SqlCommand(queryString, database.getConnection());
                int number = command_insert.ExecuteNonQuery();
                Console.WriteLine("Добавлено: {0}", number);
                database.closeConnection();
            }

            Console.WriteLine();
            return RedirectToPage("Figures");
        }
    }
}
