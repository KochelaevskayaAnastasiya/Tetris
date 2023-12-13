using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;
using tetris.Add_classes;

namespace tetris.Pages
{
    public class GameModel : PageModel
    {
        private DataBase database = new DataBase();

        public Difficulty_level difficulty_level;

        public string[] figures_mas;
        public string[] figures_mas_with_col;

        public List<Figure> GetFigures()
        {
            List<Figure> figures = new List<Figure>();


            //string queryString = "SELECT Structure FROM [Shape];";
            string queryString = "SELECT Structure FROM [Figures];";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                figures.Add(new Figure(reader[0].ToString()));
            }
            reader.Close();
            database.closeConnection();

            return figures;
        }
        public void OnGet()
        {
            List<Figure> figures = GetFigures();

            List<string> figures_str = new List<string>();
            foreach(Figure f in figures)
            {
                figures_str.Add(f.Structure);
            }

            difficulty_level = new Difficulty_level(new Glass(10, 20), figures, 1, 1, 10);

            figures_mas = figures_str.ToArray();

            FigureDop[] figureDops = CheckFigure.Chen_mas(CheckFigure.Delete_trush(figures_mas));
            List<string> figures_str_with_col = new List<string>();
            foreach (FigureDop f in figureDops)
            {
                figures_str_with_col.Add(f.structure+";"+f.col);
            }
            figures_mas_with_col = figures_str_with_col.ToArray();

        }
    }
}
