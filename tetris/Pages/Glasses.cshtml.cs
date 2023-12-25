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

        public string w ="";
        public string h = "";

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

        [HttpPost]
        public IActionResult OnPost()
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
                {
                    id = s.Substring(16, i2 - 16);
                    string queryString5 = $"SELECT Width, Length FROM Glass WHERE Glass_Id = {Convert.ToInt32(id)}";
                    database.openConnection();
                    SqlCommand command = new SqlCommand(queryString5, database.getConnection());
                    SqlDataReader reader5 = command.ExecuteReader();
                    if (reader5.Read())
                    {
                        w = reader5[0].ToString();
                        h = reader5[1].ToString();
                    }
                    reader5.Close();
                    database.closeConnection();
                }
            }
            return RedirectToPage("Glasses");
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
                string queryString2 = $"UPDATE Glass SET Length = {Convert.ToInt32(Glasses[Glasses.Count-1].Height)}, Width = {Convert.ToInt32(Glasses[Glasses.Count - 1].Width)} WHERE Glass_Id = {Convert.ToInt32(Glasses[Glasses.Count - 1].Id)}";
                database.openConnection();
                string queryString = $"SELECT Glass_Id FROM Glass WHERE Width = {Convert.ToInt32(Glasses[Glasses.Count - 1].Width)} AND Length = {Convert.ToInt32(Glasses[Glasses.Count - 1].Height)} AND Glass_Id != {Convert.ToInt32(Glasses[Glasses.Count - 1].Id)}";
                SqlCommand command3 = new SqlCommand(queryString2, database.getConnection());
                SqlCommand command = new SqlCommand(queryString, database.getConnection());
                SqlDataReader reader5 = command.ExecuteReader();
                if (!reader5.Read())
                {
                    reader5.Close();
                    command3.ExecuteNonQuery();
                }
                else
                {
                    reader5.Close();
                }
                
                database.closeConnection();
            }
            RedirectToPage();

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
                //RedirectToPage("Glasses");
            }

            return new PartialViewResult
            {
                ViewName = "_ViewNewGlass",
                ViewData = new ViewDataDictionary<NewGlass>(ViewData, model)
            };
        }
    }
}
