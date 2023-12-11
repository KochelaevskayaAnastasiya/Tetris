namespace tetris.Add_classes
{
    public class FigureDop
    {
        public string structure;
        public int col;
        public FigureDop(string structure, int col)
        {
            this.structure = structure;
            this.col = col;
        }

        public override string ToString()
        {
            return structure + " " + col;
        }
    }
}
