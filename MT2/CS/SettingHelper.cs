using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MT2.CS
{
    class SettingHelper
    {
       static  ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        #region yande
        public static string Username_Yande
        {
            get { return localsettings.Values["Username_Yande"].ToString(); }
            set { localsettings.Values["Username_Yande"] = value; }
        }
        public static string Userid_Yande
        {
            get { return localsettings.Values["Userid_Yande"].ToString(); }
            set { localsettings.Values["Userid_Yande"] = value; }
        }

        //存明文，哈希后面再做
        public static string UserPass_Yande
        {
            get { return localsettings.Values["Userpass_Yande"].ToString(); }
            set { localsettings.Values["Userpass_Yande"] = value; }
        }
        //是否登陆
        public static string IsLoagin_Yande
        {
            get { try { return localsettings.Values["IsLoagin_Yande"].ToString(); } catch { return "0"; } }
            set { localsettings.Values["IsLoagin_Yande"] = value; }
        }
        #endregion
        #region konachan
        //存明文，哈希后面再做
        public static string UserPass_Konachan
        {
            get { return localsettings.Values["Userpass_Konachan"].ToString(); }
            set { localsettings.Values["Userpass_Konachan"] = value; }
        }
        public static string Username_Konachan
        {
            get { return localsettings.Values["Username_Konachan"].ToString(); }
            set { localsettings.Values["Username_Konachan"] = value; }
        }
        public static string Userid_Konachan
        {
            get { return localsettings.Values["Userid_Konachan"].ToString(); }
            set { localsettings.Values["Userid_Konachan"] = value; }
        }
        //是否登陆
        public static string IsLoagin_Konachan
        {
            get { try { return localsettings.Values["IsLoagin_Konachan"].ToString(); } catch {return "0"; } }
            set { localsettings.Values["IsLoagin_Konachan"] = value; }
        }
        #endregion
    }
}
