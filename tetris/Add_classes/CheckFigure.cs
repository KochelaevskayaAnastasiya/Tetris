using System.Text;

namespace tetris.Add_classes
{
    public static class CheckFigure
    {
        public static FigureDop TurnRi(FigureDop st)
        {
            int row = st.structure.Length / st.col;
            string str = "";
            for (int i = 0; i < st.col; i++)
            {
                for (int j = row - 1; j >= 0; j--)
                {
                    str += st.structure[j * st.col + i];
                }
            }
            return new FigureDop(str, row);
        }
        public static bool CheckUnickOnce(FigureDop[] figures_new2, FigureDop fig)
        {
            int k = 0;
            for (int i = 0; i < figures_new2.Length; i++)
            {
                if (fig.col == figures_new2[i].col)
                {
                    if (string.Compare(fig.structure, figures_new2[i].structure) == 0)
                    {
                        k = 1;
                        break;
                    }
                }
            }
            if (k == 0) { return true; }
            else { return false; }
        }
        public static bool CheckUnick(string fi, List<string> list)
        {
            FigureDop[] figures_new2 = Delete_trush(list.ToArray());
            FigureDop fig = Del(fi);
            bool k = CheckUnickOnce(figures_new2, fig);
            FigureDop ss = TurnRi(fig);
            bool k2 = CheckUnickOnce(figures_new2, ss);
            ss = TurnRi(ss);
            bool k3 = CheckUnickOnce(figures_new2, ss);
            ss = TurnRi(ss);
            bool k4 = CheckUnickOnce(figures_new2, ss);

            return k && k2 && k3 && k4;
        }

        public static FigureDop Del(string f)
        {
            if (f != "0000000000000000")
            {
                //по строкам
                string s = f.Substring(0, 4);
                while (s == "0000") { f = f.Substring(4); s = f.Substring(0, 4); }
                string s2 = f.Substring(f.Length - 4, 4);
                while (s2 == "0000") { f = f.Substring(0, f.Length - 4); s2 = f.Substring(f.Length - 4, 4); }

                //по столбам
                int col = 4;
                int i = 0;
                int k = 0;
                string s3 = "";
                while (k < f.Length)
                {
                    s3 += f[k];
                    k += 4;
                }
                while (Convert.ToInt16(s3) == 0)
                {
                    int y = 3;
                    while (y * col >= f.Length)
                    {
                        y--;
                    }
                    for (int l = y; l >= 0; l--)
                    {
                        f = f.Remove(col * l, 1);
                    }
                    i++;
                    k = 0;
                    s3 = "";
                    col--;
                    while (k < f.Length)
                    {
                        s3 += f[k];
                        k += col;
                    }
                }
                k = col - 1;
                s3 = "";
                while (k < f.Length)
                {
                    s3 += f[k];
                    k += col;
                }
                i = col - 1;
                while (Convert.ToInt16(s3) == 0 && i >= 0)
                {
                    int z = 3;
                    while (i + z * col >= f.Length)
                    {
                        z--;
                    }
                    for (int l = z; l >= 0; l--)
                    {
                        f = f.Remove(i + col * l, 1);
                    }
                    i--;
                    s3 = "";
                    col--;
                    k = col - 1;

                    while (k < f.Length)
                    {
                        s3 += f[k];
                        k += col;
                    }
                }
                return new FigureDop(f, col);
            }
            else { return null; }
        }

        public static FigureDop[] Delete_trush(string[] fi)
        {
            FigureDop[] figureDops = new FigureDop[fi.Length];
            for (int i = 0; i < fi.Length; i++)
            {
                figureDops[i] = Del(fi[i]);
            }
            return figureDops;
        }

        public static string[] CheckFigures(string[] fi)
        {
            List<string> new_list_fi = new List<string>();
            for (int i = 0; i < fi.Length; i++)
            {
                if (fi[i] != null && fi[i] != "0000000000000000")
                {
                    if (new_list_fi.Count == 0) { new_list_fi.Add(fi[i]); }
                    else
                    {
                        if (CheckUnick(fi[i], new_list_fi)) new_list_fi.Add(fi[i]);
                    }
                }

            }
            return new_list_fi.ToArray();
        }
        public static string Paint_1(string check, int i)
        {
            StringBuilder sb = new StringBuilder(check);
            sb[i] = '1';
            return sb.ToString();
        }

        public static string Check1(string fi, int i, string check)
        {
            if (i < fi.Length - 1)
            {
                if (fi[i + 1] == '1' && check[i + 1] == '0')
                {
                    check = Paint_1(check, i + 1);
                    check = Check1(fi, i + 1, check);
                }
            }
            if (i > 0)
            {
                if (fi[i - 1] == '1' && check[i - 1] == '0')
                {
                    check = Paint_1(check, i - 1);
                    check = Check1(fi, i - 1, check);
                }
            }
            if (i > 3)
            {
                if (fi[i - 4] == '1' && check[i - 4] == '0')
                {
                    check = Paint_1(check, i - 4);
                    check = Check1(fi, i - 4, check);
                }
            }
            if (i < fi.Length - 4)
            {
                if (fi[i + 4] == '1' && check[i + 4] == '0')
                {
                    check = Paint_1(check, i + 4);
                    check = Check1(fi, i + 4, check);
                }
            }
            return check;
        }
        public static bool CheckIntegrity(string fi)
        {
            string check = "00000000000000000000";
            int k = 0;
            for (int i = 0; i < fi.Length; i++)
            {
                if (check[i] == '0')
                {
                    check = Paint_1(check, i);
                    if (fi[i] == '1') { k++; check = Check1(fi, i, check); }
                }
            }
            if (k > 1) { return false; }
            else { return true; }
        }
        public static FigureDop Chen(FigureDop str)
        {
            int col = str.col;
            int row = str.structure.Length / col;
            string struc = str.structure;
            FigureDop res = str;
            if (col != row)
            {
                if (col > row)
                {
                    int k = col - row;
                    for (int i = 0; i < k * col; i++)
                    {
                        struc += "0";
                    }
                    res = new FigureDop(struc, col);
                }
                else
                {
                    str = TurnRi(str);
                    res = Chen(str);
                }
            }
            return res;
        }

        public static FigureDop[] Chen_mas(FigureDop[] mas)
        {
            for (int i = 0; i < mas.Length; i++)
            {
                mas[i] = Chen(mas[i]);
            }
            return mas;
        }
    }
}
