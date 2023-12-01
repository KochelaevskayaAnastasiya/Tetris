using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace tetris
{
    public class Warn
    {
        string messege;
        public Warn(string warn)
        {
            messege=warn;
        }
        public HtmlString CreateList()
        {
            return new HtmlString(messege);
        }
    }
}
