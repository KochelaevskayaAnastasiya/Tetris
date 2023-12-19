using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace tetris.Pages
{
    public class StaticticsModel : PageModel
    {
        private DataBase database = new DataBase();
        public int[] point;
        public string[] time;
        public string[] GetRecordsBDTime(int id)
        {
            List<string> records = new List<string>();
            //string queryString = "SELECT [StTime] FROM [StatisticsTime] WHERE [ID_user]=" + id + ";";
            string queryString = "SELECT [StTime] FROM [StatisticsTime] WHERE [User_id]=" + id + ";";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string data = reader[0].ToString();
                records.Add(data);
            }
            reader.Close();
            database.closeConnection();

            return records.ToArray();
        }

        public int[] GetRecordsBDPoint(int id)
        {
            List<int> records = new List<int>();
            //string queryString = "SELECT [Points] FROM [StatisticsPoints] WHERE [ID_user]=" + id + ";";
            string queryString = "SELECT [Points] FROM [StatisticsPoints] WHERE [User_id]=" + id + ";";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string data = reader[0].ToString();
                records.Add(Int32.Parse(data));
            }
            reader.Close();
            database.closeConnection();

            return records.ToArray();
        }
        public int GetIdOnLogin(string login)
        {
            //string queryString = "SELECT [ID_user] FROM [Users] WHERE [Login] = '" + login + "';";
            string queryString = "SELECT [User_Id] FROM [Users] WHERE [Login] = '" + login + "';";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            string data = null;
            while (reader.Read())
            {
                data = reader[0].ToString();
            }
            reader.Close();
            database.closeConnection();

            return Int32.Parse(data);
        }
        public void OnGet()
        {
            string login = RouteData.Values["login"].ToString();
            int id = GetIdOnLogin(login);
            point = GetRecordsBDPoint(id);
            time = GetRecordsBDTime(id);
            Array.Reverse(point);
            Array.Reverse(time);
        }
    }
}
