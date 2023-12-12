using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using tetris.Add_classes;

namespace tetris.Pages
{
    public class LevelsModel : PageModel
    {
        private DataBase database = new DataBase();
        public List<Difficulty_level> levels = new List<Difficulty_level>();

        public int level_count = 0;
        public int k = 0;

        public List<String> lvls_str = new List<String>();
        public void OnGet()
        {
            string queryString = "SELECT * FROM [Level];";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data1 = new List<string[]>();
            while (reader.Read())
            {
                data1.Add(new string[6]);

                data1[data1.Count - 1][0] = reader[0].ToString();
                data1[data1.Count - 1][1] = reader[1].ToString();
                data1[data1.Count - 1][3] = reader[2].ToString();
                data1[data1.Count - 1][4] = reader[3].ToString();
                data1[data1.Count - 1][5] = reader[4].ToString();
            }
            reader.Close();
            level_count = data1.Count;
            for (int i = 0; i < level_count; i++)
            {
                queryString = $"SELECT SetOfShapes.Shape_Id FROM Level INNER JOIN SetOfShapes ON Level.Level_Id = SetOfShapes.Level_Id AND Level.Level_Id = {data1[i][0]}";
                SqlCommand command2 = new SqlCommand(queryString, database.getConnection());
                SqlDataReader reader2 = command2.ExecuteReader();
                String shapes = "";
                while (reader2.Read())
                {
                    shapes += reader2[0].ToString() + ", ";
                }
                reader2.Close();
                shapes = shapes.Remove(shapes.Length - 2);
                data1[i][2] = shapes;
            }
            for (int i = 0; i < level_count; i++)
            {
                string str = "";
                for (int j = 0; j < 6; j++)
                {
                    str += data1[i][j] + ";";
                }
                lvls_str.Add(str);
            }
            database.closeConnection();
        }
    }
}
