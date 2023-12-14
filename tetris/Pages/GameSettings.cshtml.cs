using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

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
			/*string speed = Request.Form["k3"];
			string sc1 = Request.Form["k4"];
			string sc2 = Request.Form["k5"];
			int i1, i2;
			int spd;
			int.TryParse(speed, out spd);
			bool b = true;
			if (!int.TryParse(sc2, out i2) || !int.TryParse(sc1, out i1) || shapes == "")
			{
				b = false;
			}
			if (b)
			{
				int.TryParse(sc2, out i2); int.TryParse(sc1, out i1);
				if (i2 < 1 || i1 < 1)
					b = false;
			}
			if (b)
			{
				string[] gl = glass.Split('x');
				string query = $"SELECT Glass_Id FROM Glass WHERE Length = {Convert.ToInt32(gl[0])} AND Width = {Convert.ToInt32(gl[1])}";
				SqlCommand command = new SqlCommand(query, database.getConnection());
				database.openConnection();
				string GlassId = command.ExecuteScalar().ToString();
				int.TryParse(sc2, out i2); int.TryParse(sc1, out i1);
				string queryString = $"UPDATE Level SET Glass_Id = {Convert.ToInt32(GlassId)}, Speed = {spd}, PointsForRow = {i1}, PointsToNextLevel = {i2} WHERE Level_Id = {Convert.ToInt16(RouteData.Values["id"].ToString())}";
				SqlCommand command2 = new SqlCommand(queryString, database.getConnection());
				command2.ExecuteNonQuery();
				database.closeConnection();
				return RedirectToPage("Levels");
			}
			else
			{*/
				return RedirectToPage("EditLevel", new { id = RouteData.Values["id"].ToString() });
			
		}
	}
}
