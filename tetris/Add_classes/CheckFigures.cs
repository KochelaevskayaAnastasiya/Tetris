namespace tetris.Add_classes
{
    public class CheckFigures
    {
        HashSet<String> figu = new HashSet<String>();

        public CheckFigures(string[] figures_new)
        {
            for(int i=0;i<figures_new.Length;i++)
            {
                figu.Add(figures_new[i]);
            }
        }
    }
}
