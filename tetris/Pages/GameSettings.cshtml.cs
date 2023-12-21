using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Drawing;

namespace tetris.Pages
{
    public class GameSettingsModel : PageModel
    {
		private DataBase database = new DataBase();

		public int lvl_count = 0;

		public List<String> lvl_str = new List<String>();
		public void OnGet()
        {
			string queryString = "SELECT Level_Id FROM [Level];";

			SqlCommand command = new SqlCommand(queryString, database.getConnection());
			database.openConnection();
			SqlDataReader reader = command.ExecuteReader();
			List<string> data1 = new List<string>();
			while (reader.Read())
			{ 
				data1.Add(reader[0].ToString());
			}
			reader.Close();
			lvl_count = data1.Count;
			for (int i = 0; i < lvl_count; i++)
			{
				string str = "";
				str += data1[i];
				lvl_str.Add(str);
			}
			database.closeConnection();
		}
		[HttpPost]
		public IActionResult OnPost()
		{
			string stat = Request.Form["k1"];
			string level = Request.Form["k2"];
            string login2 = RouteData.Values["login"].ToString();
            string color = RouteData.Values["color"].ToString();
            string mus = RouteData.Values["mus"].ToString();
            string setka = RouteData.Values["setka"].ToString();
            string next_figu = RouteData.Values["next_figu"].ToString();
            string idd = level.Substring(8);

			string queryString = "SELECT Level_Id FROM [Level] ORDER BY Speed;";
			SqlCommand command = new SqlCommand(queryString, database.getConnection());
			database.openConnection();
			SqlDataReader reader = command.ExecuteReader();
			int jj = 1;
			while (jj != Convert.ToInt16(idd))
			{
				reader.Read();
				jj++;
			}
			reader.Read();
			string idd2 = reader[0].ToString();
			reader.Close();
			return RedirectToPage("Game", new { id = idd2 , state = stat, lvl = idd, login = login2, color = color, mus = mus, setka = setka, next_figu = next_figu });
			
		}
	}
}
