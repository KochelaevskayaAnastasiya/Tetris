using System.Xml.Linq;

namespace tetris.Add_classes
{
    public class LevelFigu
    {
        public int level;
        public int figu;
        public LevelFigu(int l, int f)
        {
            level = l;
            figu = f;
        }

        public override string ToString()
        {
            return level+","+figu;
        }
    }
}
