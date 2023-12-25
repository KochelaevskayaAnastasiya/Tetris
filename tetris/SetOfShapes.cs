using System.Data.SqlClient;
using tetris.Add_classes;

namespace tetris
{
    public class SetOfShapes
    {
        private DataBase database = new DataBase();

        public string str = "УРАА";

        public string[] figu;

        public string[] setFiguLevel;

        public string shapes;

        public string AAAAA { get; set; }

        public int id { get; set; }

        public SetOfShapes()
        {
            List<Figure> figures = GetFigures();

            List<string> figures_str = new List<string>();
            foreach (Figure figure in figures)
            {
                figures_str.Add(figure.Structure);
            }
            figu = figures_str.ToArray();

            setFiguLevel = GetSetFiguresLevels();
        }
        public string[] GetSetFiguresLevels()
        {
            string queryString = "SELECT [Level_Id], [Shape_Id] FROM [SetOfShapes];";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            List<string> data = new List<string>();

            while (reader.Read())
            {
                string lvl = reader[0].ToString();
                string figu = reader[1].ToString();
                LevelFigu level = new LevelFigu(Int32.Parse(lvl), Int32.Parse(figu));
                data.Add(level.ToString());
            }
            reader.Close();
            database.closeConnection();

            return data.ToArray();
        }
        public List<Figure> GetFigures()
        {
            List<Figure> figures = new List<Figure>();

            string queryString = "SELECT Structure FROM[Shape];";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            List<string> data = new List<string>();

            while (reader.Read())
            {
                data.Add(reader[0].ToString());
            }
            reader.Close();
            database.closeConnection();
            for (int i = 0; i < data.Count; i++)
            {
                figures.Add(new Figure(data[i].ToString()));
            }

            return figures;
        }
    }
}
