using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using tetris.Add_classes;

namespace tetris.Pages
{
    public class GlassesModel : PageModel
    {
        private DataBase database = new DataBase();
        public List<Glass> glasses = new List<Glass>();

        public int glass_count = 0;
        public int k = 0;

        public string w;
        public string h;

        public List<String> glasses_str = new List<String>();

        private readonly static List<EditGlass> Glasses = new List<EditGlass>();
        private readonly static List<NewGlass> NewGlasses = new List<NewGlass>();

        public string id = "";
        public void OnGet()
        {
            string queryString = "SELECT * FROM [Glass];";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data1 = new List<string[]>();
            while (reader.Read())
            {
                data1.Add(new string[2]);

                data1[data1.Count - 1][0] = reader[0].ToString();
                data1[data1.Count - 1][1] = reader[1].ToString() + "x" + reader[2].ToString();
            }
            reader.Close();
            glass_count = data1.Count;
            for (int i = 0; i < glass_count; i++)
            {
                string str = "";
                for (int j = 0; j < 2; j++)
                {
                    str += data1[i][j] + ";";
                }
                glasses_str.Add(str);
            }
            database.closeConnection();
        }

        public string getGl(string kkk)
        {
            string s2 = "</td>";
            string s = kkk;
            //int i1 = s.IndexOf(s1);
            int i2 = s.IndexOf(s2);
            if (i2 == -1)
                id = "-1";
            else if (s == null)
                id = "0";
            else
                id = s.Substring(16, i2 - 16);

            string str = "";
            if (id != "-1" && id != "0"){
                string queryString = $"SELECT Length, Width FROM Glass WHERE Glass_Id='{id}'";

                SqlCommand command = new SqlCommand(queryString, database.getConnection());
                database.openConnection();
                SqlDataReader reader = command.ExecuteReader();
                string[] data1 = new string[2];
                if (reader.Read())
                {
                    data1[0] = reader[0].ToString();
                    data1[1] = reader[1].ToString();
                    str = data1[0] + " " + data1[1];
                }
                reader.Close();
                database.closeConnection();
            }
            return str;
        }

        [HttpPost]
        public void OnPost()
        {   //string s1 = "<td class=\"td1\">";
            string s2 = "</td>";
            string s = Request.Form["kkk"];
            //int i1 = s.IndexOf(s1);
            int i2;
            if (s == null)
            {
                id = "0";
            }
            else
            {
                i2 = s.IndexOf(s2);
                if (i2 == -1)
                    id = "-1";
                else
                    id = s.Substring(16, i2 - 16);
            }
        }
        public PartialViewResult OnGetViewEditGlass()
        {
            // this handler returns _ContactModalPartial 
            return new PartialViewResult
            {
                ViewName = "_ViewEditGlass",
                ViewData = new ViewDataDictionary<EditGlass>(ViewData, new EditGlass { })

            };
        }

        public PartialViewResult OnPostViewEditGlass(EditGlass model)
        {
            if (ModelState.IsValid)
            {
                Glasses.Add(model);
                string queryString2 = $"INSERT INTO Glass (Length, Width) VALUES ({Convert.ToInt32(Glasses[Glasses.Count-1].Height)}, {Convert.ToInt32(Glasses[Glasses.Count - 1].Width)})";
                database.openConnection();
                string queryString = $"SELECT Glass_Id FROM Glass WHERE Width = {Convert.ToInt32(Glasses[Glasses.Count - 1].Width)} AND Length = {Convert.ToInt32(Glasses[Glasses.Count - 1].Height)}";
                SqlCommand command3 = new SqlCommand(queryString2, database.getConnection());
                SqlCommand command = new SqlCommand(queryString, database.getConnection());
                SqlDataReader reader5 = command.ExecuteReader();
                if (!reader5.Read())
                {
                    reader5.Close();
                    command3.ExecuteNonQuery();
                }
                reader5.Close();
                database.closeConnection();
                RedirectToPage("Glasses");
            }

            return new PartialViewResult
            {
                ViewName = "_ViewEditGlass",
                ViewData = new ViewDataDictionary<EditGlass>(ViewData, model)
            };
        }

        public PartialViewResult OnGetViewNewGlass()
        {
            // this handler returns _ContactModalPartial 
            return new PartialViewResult
            {
                ViewName = "_ViewNewGlass",
                ViewData = new ViewDataDictionary<NewGlass>(ViewData, new NewGlass { })

            };
        }

        public PartialViewResult OnPostViewNewGlass(NewGlass model)
        {
            if (ModelState.IsValid)
            {
                NewGlasses.Add(model);
                string queryString2 = $"INSERT INTO Glass (Length, Width) VALUES ({Convert.ToInt32(NewGlasses[NewGlasses.Count - 1].Height)}, {Convert.ToInt32(NewGlasses[NewGlasses.Count - 1].Width)})";
                database.openConnection();
                string queryString = $"SELECT Glass_Id FROM Glass WHERE Width = {Convert.ToInt32(NewGlasses[NewGlasses.Count - 1].Width)} AND Length = {Convert.ToInt32(NewGlasses[NewGlasses.Count - 1].Height)}";
                SqlCommand command2 = new SqlCommand(queryString2, database.getConnection());
                SqlCommand command = new SqlCommand(queryString, database.getConnection());
                SqlDataReader reader = command.ExecuteReader();
                if (!reader.Read())
                {
                    reader.Close();
                    command2.ExecuteNonQuery();
                }
                reader.Close();
                database.closeConnection();
                RedirectToPage("Glasses");
            }

            return new PartialViewResult
            {
                ViewName = "_ViewNewGlass",
                ViewData = new ViewDataDictionary<NewGlass>(ViewData, model)
            };
        }
    }
}
