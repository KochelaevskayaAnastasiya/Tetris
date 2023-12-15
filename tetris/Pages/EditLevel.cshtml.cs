using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Data.SqlClient;

namespace tetris.Pages
{
	public class EditLevelModel : PageModel
	{
		private DataBase database = new DataBase();

		public List<String> speeds = new List<String>() { "1", "2", "3", "4", "5" };

		public int glass_count = 0;

		public List<String> lvl_str = new List<String>();
		public List<String> gls_str = new List<String>();

		public string s = "";
		public int id = 0;

		public void OnGet()
		{
			s = RouteData.Values["id"].ToString();
			id = Convert.ToInt16(s);
			string queryString = $"SELECT * FROM Level WHERE Level_Id = {id}";

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
			queryString = $"SELECT * FROM [Glass] WHERE Glass_Id = {data1[0][1]}";

			SqlCommand command2 = new SqlCommand(queryString, database.getConnection());
			SqlDataReader reader2 = command2.ExecuteReader();
			List<string[]> data2 = new List<string[]>();
			while (reader2.Read())
			{
				data2.Add(new string[2]);

				data2[data2.Count - 1][0] = reader2[0].ToString();
				data2[data2.Count - 1][1] = reader2[1].ToString() + "x" + reader2[2].ToString();
			}
			reader2.Close();

			data1[0][1] = data2[0][1];

			for (int i = 0; i < data1.Count; i++)
			{
				//queryString = $"SELECT SetOfShapes.ID_figure FROM Level INNER JOIN SetOfShapes ON Level.Level_Id = SetOfShapes.Level_Id AND Level.Level_Id = {data1[i][0]}";

				queryString = $"SELECT SetOfShapes.Shape_Id FROM Level INNER JOIN SetOfShapes ON Level.Level_Id = SetOfShapes.Level_Id AND Level.Level_Id = {data1[i][0]}";
				SqlCommand command3 = new SqlCommand(queryString, database.getConnection());
				SqlDataReader reader3 = command3.ExecuteReader();
				String shapes = "";
				while (reader3.Read())
				{
					shapes += reader3[0].ToString() + ", ";
				}
				reader3.Close();
				shapes = shapes.Remove(shapes.Length - 2);
				data1[i][2] = shapes;
			}
			for (int i = 0; i < data1.Count; i++)
			{
				string str = "";
				for (int j = 0; j < 6; j++)
				{
					str += data1[i][j] + ";";
				}
				lvl_str.Add(str);
			}
			database.closeConnection();
			
			string queryString5 = $"SELECT * FROM [Glass];";

			SqlCommand command5 = new SqlCommand(queryString5, database.getConnection());
			database.openConnection();
			SqlDataReader reader5 = command5.ExecuteReader();
			List<string[]> data5 = new List<string[]>();
			while (reader5.Read())
			{
				data5.Add(new string[2]);

				data5[data5.Count - 1][0] = reader5[0].ToString();
				data5[data5.Count - 1][1] = reader5[1].ToString() + "x" + reader5[2].ToString();
			}
			reader5.Close();
			glass_count = data5.Count;
			for (int i = 0; i < glass_count; i++)
			{
				string str = "";
				str += data5[i][1];
				gls_str.Add(str);
			}
			database.closeConnection();
		}
        public PartialViewResult OnGetViewSetOfShape()
        {
            // this handler returns _ContactModalPartial
            return new PartialViewResult
            {
                ViewName = "_ViewSetOfShape",
                ViewData = new ViewDataDictionary<SetOfShapes>(ViewData, new SetOfShapes { })

            };
        }

        [HttpPost]
		public IActionResult OnPost()
		{
			string glass = Request.Form["k1"];
			string shapes = Request.Form["k2"];
			string speed = Request.Form["k3"];
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
			{
				return RedirectToPage("EditLevel", new { id = RouteData.Values["id"].ToString() });
			}
		}
	}
}
