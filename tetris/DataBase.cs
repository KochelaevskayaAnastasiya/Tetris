using System.Data.SqlClient;

namespace tetris
{
    public class DataBase
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-LIPBI05D;Initial Catalog=Tetris;Integrated Security=True");
        //SqlConnection con = new SqlConnection("Data source=.\\SQLEXPRESS;database=Tetris; Integrated security=true;");
        public void openConnection()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
        }

        public void closeConnection()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }

        public SqlConnection getConnection()
        {
            return con;
        }
    }
}
