namespace tetris.Add_classes
{
    public class Difficulty_level
    {
        private Glass glass;
        private List<Figure> figures;
        private double speed;
        private int points_row;
        private int points_next_level;

        public Difficulty_level(Glass glass, List<Figure> figures, double speed, int points_row, int points_next_level)
        {
            this.glass = glass;
            this.figures = figures;
            this.speed = speed;
            this.points_row = points_row;
            this.points_next_level = points_next_level;
        }

        public Glass Glass { get { return glass; } }
        public List<Figure> Falls { get { return figures; } }
        public double Speed { get { return speed; } }
        public int Points_row { get { return points_row; } }
        public int Players_next_level { get { return points_next_level; } }

    }
}
