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

        private string value = "";
        public string id = "";
        public void OnGet()
        {
            string queryString = "SELECT * FROM [Level] ORDER BY Speed;";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data1 = new List<string[]>();
            int jj = 1;
            while (reader.Read())
            {
                data1.Add(new string[7]);

                data1[data1.Count - 1][6] = reader[0].ToString();
                data1[data1.Count - 1][0] = jj.ToString();
                data1[data1.Count - 1][1] = reader[1].ToString();
                data1[data1.Count - 1][3] = reader[2].ToString();
                data1[data1.Count - 1][4] = reader[3].ToString();
                data1[data1.Count - 1][5] = reader[4].ToString();
                jj++;
            }
            reader.Close();
            level_count = data1.Count;
            for (int i = 0; i < level_count; i++)
            {
                queryString = $"SELECT SetOfShapes.Shape_Id FROM Level INNER JOIN SetOfShapes ON Level.Level_Id = SetOfShapes.Level_Id AND Level.Level_Id = {data1[i][6]}";
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
                for (int j = 0; j < 7; j++)
                {
                    str += data1[i][j] + ";";
                }
                lvls_str.Add(str);
            }
            database.closeConnection();
        }

        [HttpPost]
        public IActionResult OnPost()
        {   //string s1 = "<td class=\"td1\">";
            string s2 = "</td>";
            string s = Request.Form["kkk"];
            //int i1 = s.IndexOf(s1);
            int i2 = s.IndexOf(s2);
            if (i2 == -1)
            {
                return RedirectToPage("Levels");
            }
            else
            {
				string id2 = s.Substring(16, i2 - 16);

                string queryString = "SELECT Level_Id FROM [Level] ORDER BY Speed;";
                SqlCommand command = new SqlCommand(queryString, database.getConnection());
                database.openConnection();
                SqlDataReader reader = command.ExecuteReader();
                int jj = 1;
                while (jj != Convert.ToInt16(id2))
                {
                    reader.Read();
                    jj++;
                }
                reader.Read();
                id = reader[0].ToString();
                reader.Close();

                return RedirectToPage("EditLevel", new { id = this.id });
            }
        }
    }
}
