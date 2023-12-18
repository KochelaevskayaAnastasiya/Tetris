namespace tetris.Add_classes
{
    public class Record
    {
        public int id;
        public int record;
        public Record(int id, string record)
        {
            this.id = id;
            string[] strings = record.Split(':');
            int k = Int32.Parse(strings[0]);
            int k2 = Int32.Parse(strings[1]);
            this.record = k * 60 + k2;  
        }

        public Record(int id, int record)
        {
            this.id = id;
            this.record = record;
        }
    }
}
