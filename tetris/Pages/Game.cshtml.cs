using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using tetris.Add_classes;
using System.Web.Services;
using static System.Net.Mime.MediaTypeNames;

namespace tetris.Pages
{
    public class GameModel : PageModel
    {
        private DataBase database = new DataBase();

        public Difficulty_level difficulty_level;

        public string[] figures_mas;
        public string[] figures_mas_with_col;

        public string login;
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
            login = RouteData.Values["login"].ToString();
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
        

        [HttpPost]
        public IActionResult OnPost()
        {
            string s = Request.Form["records"];
            s = s.Replace("\r", "");
            s = s.Replace("\t", "");
            string nnn = RouteData.Values["login"].ToString(); ;

            if (s != null)
            {
                string[] records = s.Split('\n');

            }
            return RedirectToPage("Game");


        }
    }
}
