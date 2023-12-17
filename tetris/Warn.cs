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
    }
}
