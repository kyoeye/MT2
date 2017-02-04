using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT2.CS
{
  public   class ThemeColors
    {   
            public string ColorNames { get; set; }
            public string Colors { get; set; }
    }
    public class ThemeColorsAdd
    {
        public static List<ThemeColors> GetThemeColors()
        {
            var colors = new List<ThemeColors>();

            colors.Add(new ThemeColors { ColorNames = "纯洁白", Colors = "#FFFFFFFF" });
            colors.Add(new ThemeColors { ColorNames = "非洲黑", Colors = "#FF000000" });

            colors.Add(new ThemeColors { ColorNames = "萌豚蓝", Colors = "#FF0074B6" });
            colors.Add(new ThemeColors { ColorNames = "Miku绿", Colors = "#FF66FFCC" });
            colors.Add(new ThemeColors { ColorNames = "天依蓝", Colors = "#FF66CCFF " });
            colors.Add(new ThemeColors { ColorNames = "基佬紫", Colors = "#FFBE2AC6" });
            colors.Add(new ThemeColors { ColorNames = "亚里亚粉", Colors = "#FFE4A594" });
            colors.Add(new ThemeColors { ColorNames = "姨妈红", Colors = "#FFE21F1F" });

            return colors;
        }
    }
}
