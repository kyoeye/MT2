using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources.Core;

namespace MT2.CS
{
    public class LanguageSelection
    {
        public static   void SettingLanguage(string country)
        {
            if (country != null)
            {
                string AppLanguage = string.Empty;
                switch (country)
                {
                    case "China":
                        AppLanguage = "zh-Hans";
                        break;
                    case "USA":
                        AppLanguage = "en-US";
                        break;
                }
                //ResourceContext rs = ResourceContext.GetForCurrentView();
                ResourceContext rs = ResourceManager.Current.DefaultContext;
                rs.Languages = new List<string>(new string[] { AppLanguage });
            }
        }

    }
}
