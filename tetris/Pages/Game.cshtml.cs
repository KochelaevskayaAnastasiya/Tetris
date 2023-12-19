using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using tetris.Add_classes;
using System.Web.Services;
using static System.Net.Mime.MediaTypeNames;
using System.Data.SqlTypes;

namespace tetris.Pages
{
    public class GameModel : PageModel
    {
        private DataBase database = new DataBase();

        public Difficulty_level difficulty_level;

        public string[] figures_mas;
        public string[] figures_mas_with_col;

        public int point_mode;

        public Figure GetFigure(int id_figure)
        {
            string queryString = "SELECT Structure FROM [Shape] WHERE [Shape_Id] =" + id_figure + ";";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            Figure figure = null;
            while (reader.Read())
            {
                figure = new Figure(reader[0].ToString());
            }
            reader.Close();
            database.closeConnection();

            return figure;
        }

        public List<Figure> GetFigures(int id_level)
        {
            List<Figure> figures = new List<Figure>();

            string queryString = "SELECT [Shape_Id] FROM [SetOfShapes] WHERE [Level_Id] = " + id_level + ";";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            List<string> data = new List<string>();

            while (reader.Read())
            {
                data.Add(reader[0].ToString());
            }
            reader.Close();
            database.closeConnection();
            for (int i = 0; i < data.Count; i++)
            {
                Figure figure = GetFigure(Int32.Parse(data[i].ToString()));
                figures.Add(figure);
            }

            return figures;
        }
        public Glass GetGlass(int id)
        {
            string queryString = "SELECT [Length],[Width] FROM [Glass] WHERE [Glass_Id] = " + id + ";";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            string[] data = new string[2];

            while (reader.Read())
            {

                data[0] = reader[0].ToString();
                data[1] = reader[1].ToString();
            }
            reader.Close();
            database.closeConnection();
            Glass glass = new Glass(Int32.Parse(data[1]), Int32.Parse(data[0]));
            return glass;
        }

        public Difficulty_level GetDifficulty_Level(int id)
        {

            string queryString = "SELECT [Glass_Id],[Speed] ,[PointsForRow],[PointsToNextLevel] FROM [Level] WHERE Level_Id = " + id + ";";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            string[] data = new string[4];

            while (reader.Read())
            {

                data[0] = reader[0].ToString();
                data[1] = reader[1].ToString();
                data[2] = reader[2].ToString();
                data[3] = reader[3].ToString();
            }
            reader.Close();
            database.closeConnection();

            Difficulty_level difficulty_Level = new Difficulty_level(GetGlass(Int32.Parse(data[0])), GetFigures(id), Int32.Parse(data[1]), Int32.Parse(data[2]), Int32.Parse(data[3]));
            return difficulty_Level;
        }
        public void OnGet()
        {
            string point_mode_str = RouteData.Values["state"].ToString();
            if (point_mode_str == "not")
            {
                point_mode = 0;
            }
            else
            {
                if (point_mode_str == "time")
                {
                    point_mode = 1;
                }
                else
                {
                    point_mode = 2;
                }
            }

            difficulty_level = GetDifficulty_Level(Convert.ToInt16(RouteData.Values["id"]));

            List<Figure> figures = difficulty_level.figures;

            List<string> figures_str = new List<string>();
            foreach (Figure f in figures)
            {
                figures_str.Add(f.Structure);
            }

            figures_mas = figures_str.ToArray();

            FigureDop[] figureDops = CheckFigure.Chen_mas(CheckFigure.Delete_trush(figures_mas));
            List<string> figures_str_with_col = new List<string>();
            foreach (FigureDop f in figureDops)
            {
                figures_str_with_col.Add(f.structure + ";" + f.col);
            }
            figures_mas_with_col = figures_str_with_col.ToArray();

        }
        public int[] GetRecordsBDTime(int id)
        {
            List<int> records = new List<int>();
            string queryString = "SELECT [StTime] FROM [StatisticsTime] WHERE [ID_user]=" + id+";";
            //string queryString = "SELECT [StTime] FROM [StatisticsTime] WHERE [User_Id]=" + id + ";";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string data = reader[0].ToString();
                string[] strings = data.Split(':');
                int k = Int32.Parse(strings[0]);
                int k2 = Int32.Parse(strings[1]);
                records.Add(k * 60 + k2);
            }
            reader.Close();
            database.closeConnection();

            return records.ToArray();
        }

        public int[] GetRecordsBDPoint(int id)
        {
            List<int> records = new List<int>();
            //string queryString = "SELECT [Points] FROM [StatisticsPoints] WHERE [ID_user]=" + id + ";";
            string queryString = "SELECT [Points] FROM [StatisticsPoints] WHERE [User_Id]=" + id + ";";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string data = reader[0].ToString();
                records.Add(Int32.Parse(data));
            }
            reader.Close();
            database.closeConnection();

            return records.ToArray();
        }

        public void SetRecordsBDPoint(int[] record, int id)
        {
            //string queryString = "DELETE FROM [StatisticsPoints] WHERE [ID_user]=" + id + ";";
            string queryString = "DELETE FROM [StatisticsPoints] WHERE [User_Id]=" + id + ";";
            database.openConnection();
            SqlCommand command_insert2 = new SqlCommand(queryString, database.getConnection());
            int number2 = command_insert2.ExecuteNonQuery();
            Console.WriteLine("delete: {0}", number2);
            database.closeConnection();

            for (int i = 0; i < record.Length; i++) {
                string queryString2 = "INSERT INTO StatisticsPoints VALUES (" + id + ", " + record[i] +");";
                database.openConnection();
                SqlCommand command_insert = new SqlCommand(queryString2, database.getConnection());
                int number = command_insert.ExecuteNonQuery();
                Console.WriteLine("Вставлено: {0}", number);
                database.closeConnection();
            }
        }

        public void SetRecordsBDTime(int[] record, int id)
        {
            string queryString = "DELETE FROM [StatisticsTime] WHERE [ID_user]=" + id + ";";
            //string queryString = "DELETE FROM [StatisticsTime] WHERE [User_Id]=" + id + ";";
            database.openConnection();
            SqlCommand command_insert2 = new SqlCommand(queryString, database.getConnection());
            int number2 = command_insert2.ExecuteNonQuery();
            Console.WriteLine("delete: {0}", number2);
            database.closeConnection();

            for (int i = 0; i < record.Length; i++)
            {
                int k = record[i]/60;
                int k2 = record[i]%60;
                string s = "00:"+k + ":" + k2;
                string queryString2 = "INSERT INTO StatisticsTime VALUES (" + id + ", '" + s + "');";
                database.openConnection();
                SqlCommand command_insert = new SqlCommand(queryString2, database.getConnection());
                command_insert.ExecuteNonQuery();
                database.closeConnection();
            }
        }

        public int GetIdOnLogin(string login)
        {
            string queryString = "SELECT [ID_user] FROM [Users] WHERE [Login] = '"+ login+ "';";
            //string queryString = "SELECT [User_Id] FROM [Users] WHERE [Login] = '" + login + "';";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            string data = null;
            while (reader.Read())
            {
                data = reader[0].ToString();
            }
            reader.Close();
            database.closeConnection();

            return Int32.Parse(data);
        }
        [HttpPost]
        public IActionResult OnPost()
        {
            
            string s = Request.Form["records"];
            s = s.Replace("\r", "");
            s = s.Replace("\t", "");
            string login = RouteData.Values["login"].ToString();
            int id = GetIdOnLogin(login);

            List<int> rec = new List<int>();
            if (s != null)
            {
                string[] records = s.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

                if (records[0].IndexOf(":")==-1)
                {
                    //point
                    int[] records1 = GetRecordsBDPoint(id);
                    List<int> ints = new List<int>();
                    for (int i=0;i< records1.Length; i++)
                    {
                        ints.Add(records1[i]);
                    }

                    for (int i = 0; i < records.Length; i++)
                    {
                        ints.Add(Int32.Parse(records[i]));
                    }

                    ints.Sort();

                    int[] intArray;
                    if (ints.Count <= 10)
                    {
                        intArray = new int[ints.Count];
                        ints.CopyTo(intArray);
                    }
                    else
                    {
                        intArray = new int[10];
                        ints.CopyTo(ints.Count - 10, intArray, 0, 10);

                    }
                    SetRecordsBDPoint(intArray, id);

                }
                else
                {
                    //time
                    int[] records2 = GetRecordsBDTime(id);

                    List<int> ints = new List<int>();
                    
                    for (int i=0; i < records.Length; i++)
                    {
                        string[] strings = records[i].Split(':');
                        int k = Int32.Parse(strings[0]);
                        int k2 = Int32.Parse(strings[1]);
                        ints.Add(k * 60 + k2);
                    }

                    for (int i = 0; i < records2.Length; i++)
                    {
                        ints.Add(records2[i]);
                    }
                    ints.Sort();

                    int[] intArray;
                    if (ints.Count <= 10)
                    {
                        intArray = new int[ints.Count];
                        ints.CopyTo(intArray);
                    }
                    else
                    {
                        intArray = new int[10];
                        ints.CopyTo(ints.Count - 10, intArray, 0, 10);
                    }
                    SetRecordsBDTime(intArray, id);
                }
            }
            return RedirectToPage("Game");
        }
    }
}
