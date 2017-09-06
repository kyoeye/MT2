using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT2.CS
{

    class Languages
    {
        LanguageMode lm = new LanguageMode();
        public  static List<LanguageMode> GetLanguages()
        {
            var llm = new List<LanguageMode>();
            llm.Add(new LanguageMode { LanguageIMG_Uri = "ms-appx:///Assets/icon/China.png" });
            llm.Add(new LanguageMode { LanguageIMG_Uri = "ms-appx:///Assets/icon/USA.png" });
            return llm;
        }
    }

   public class LanguageMode
    {
        public string LanguageIMG_Uri { get; set; }
    }
}
