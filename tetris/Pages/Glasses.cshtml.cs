using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Data.SqlClient;
using tetris.Add_classes;

namespace tetris.Pages
{
    public class GlassesModel : PageModel
    {
        private DataBase database = new DataBase();
        public List<Glass> glasses = new List<Glass>();

        public int glass_count = 0;
        public int k = 0;

        public List<String> glasses_str = new List<String>();

        private readonly static List<EditGlass> Glasses = new List<EditGlass>();

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
        public void OnPost()
        {   //string s1 = "<td class=\"td1\">";
            string s2 = "</td>";
            string s = Request.Form["kkk"];
            //int i1 = s.IndexOf(s1);
            int i2 = s.IndexOf(s2);
            if (i2 == -1)
                id = "-1";
            else if (s == null)
                id = "0";
            else
                id = s.Substring(16, i2 - 16);
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
            }

            return new PartialViewResult
            {
                ViewName = "_ViewEditGlass",
                ViewData = new ViewDataDictionary<EditGlass>(ViewData, model)
            };
        }
    }
}
