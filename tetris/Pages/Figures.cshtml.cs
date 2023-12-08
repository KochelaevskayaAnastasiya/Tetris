using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using tetris.Add_classes;

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
    }
}
