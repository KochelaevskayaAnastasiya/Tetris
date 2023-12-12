using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using tetris.Add_classes;

namespace tetris.Pages
{
    public class GameModel : PageModel
    {
        public Difficulty_level difficulty_level;

        public string[] figures_mas;
        public string[] figures_mas_with_col;
        public void OnGet()
        {
            List<Figure> figures = new List<Figure>();
            figures.Add(new Figure("1000000000000000"));
            figures.Add(new Figure("0000000000001111"));
            figures.Add(new Figure("1000100010001100"));
            figures.Add(new Figure("1000000000000000"));

            List<string> figures_str = new List<string>();
            foreach(Figure f in figures)
            {
                figures_str.Add(f.Structure);
            }

            difficulty_level = new Difficulty_level(new Glass(10, 20), figures, 1, 1, 10);

            figures_mas = figures_str.ToArray();

            FigureDop[] figureDops = CheckFigure.Delete_trush(figures_mas);
            List<string> figures_str_with_col = new List<string>();
            foreach (FigureDop f in figureDops)
            {
                figures_str_with_col.Add(f.structure+";"+f.col);
            }
            figures_mas_with_col = figures_str_with_col.ToArray();

        }
    }
}
