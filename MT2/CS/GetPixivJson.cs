using MT2.CS.Pixiv_api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;

namespace MT2.CS
{
    class GetPixivJson
    {
        public ObservableCollection<tuijian> Pixiv_list { get; set; }
        ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public   ObservableCollection<tuijian> SaveJson(string Jsonstring)
        {  //Newtonsoft.Json 引用
           //list = new ObservableCollection<Yande_post_json>();
      
                Pixiv_list  = JsonConvert.DeserializeObject<ObservableCollection<tuijian >>(Jsonstring);
                //try
                //{
                //    if ((int)localsettings.Values["_FuckSlider"] != 2) //2才是对的，暂时弄一个不可能的值以防开关误触发
                //    {
                //        for (int a = Pixiv_list .Count - 1; a >= 0; a--) //动动py想一想都知道用减   //最后我发现我还是不擅长动PY。。。
                //        {
                //            if (list[a].rating == "q")
                //            {
                //                list.Remove(list[a]);
                //            }
                //            else if (list[a].rating == "e")
                //            {
                //                list.Remove(list[a]);
                //            }

                //        }

                //    }

                //}
                //catch
                //{
                //    for (int a = list.Count - 1; a >= 0; a--)
                //    {
                //        if (list[a].rating == "q")
                //        {
                //            list.Remove(list[a]);
                //        }
                //        else if (list[a].rating == "e")
                //        {
                //            list.Remove(list[a]);
                //        }

                //    }
                //}

           
            return Pixiv_list ;
        }

    }
}
