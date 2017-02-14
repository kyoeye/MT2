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
            colors.Add(new ThemeColors { ColorNames = "高坂穗乃果", Colors = "#FFFFB11B" });
            colors.Add(new ThemeColors { ColorNames = "南小鸟", Colors = "#FF808080" });
            colors.Add(new ThemeColors { ColorNames = "园田海未", Colors = "#FF0000FF" });
            colors.Add(new ThemeColors { ColorNames = "星空凛", Colors = "#FFFFFF00" });
            colors.Add(new ThemeColors { ColorNames = "小泉花阳", Colors = "#FFADFF2F" });
            colors.Add(new ThemeColors { ColorNames = "西木野真姬", Colors = "#FFFF0000" });
            colors.Add(new ThemeColors { ColorNames = "绚濑绘里", Colors = "#FF00FFFF" });
            colors.Add(new ThemeColors { ColorNames = "东条希", Colors = "#FF800080" });
            colors.Add(new ThemeColors { ColorNames = "矢泽妮可", Colors = "#FFFFC0CB" });
            colors.Add(new ThemeColors { ColorNames = "奇迹", Colors = "#FFFD6600" });
            colors.Add(new ThemeColors { ColorNames = "高海千歌", Colors = "#FFF08300" });
            colors.Add(new ThemeColors { ColorNames = "渡边曜", Colors = "#FF68D1FF" });
            colors.Add(new ThemeColors { ColorNames = "樱内梨子", Colors = "#FFFF7575" });
            colors.Add(new ThemeColors { ColorNames = "国木田花丸", Colors = "#FFE2C800" });
            colors.Add(new ThemeColors { ColorNames = "黑泽露比", Colors = "#FFF273C4" });
            colors.Add(new ThemeColors { ColorNames = "津岛善子", Colors = "#FFC3C3C3" });
            colors.Add(new ThemeColors { ColorNames = "黑泽黛雅", Colors = "#FFFF2A2A" });
            colors.Add(new ThemeColors { ColorNames = "小原鞠莉", Colors = "#FFB761FF" });
            colors.Add(new ThemeColors { ColorNames = "松浦果南", Colors = "#FF1BD5A8" });
            colors.Add(new ThemeColors { ColorNames = "希望色", Colors = "#FF00A1E9" });




            return colors;
        }
    }
}
