namespace tetris.Add_classes
{
    public class Figure
    {
        private string structure;

        public Figure(string st)
        {
            structure = st;
        }

        public string Structure { get { return structure; } }
    }
}
