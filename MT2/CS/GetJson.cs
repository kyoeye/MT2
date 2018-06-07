using MT2.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;

namespace MT2.CS
{
    class GetJson
    {
        string JS_RequestUri;
#pragma warning disable CS0169 // 从不使用字段“GetJson.JStext”
        string JStext;
#pragma warning restore CS0169 // 从不使用字段“GetJson.JStext”
        ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        //Yande的子站的api是相同的
        //public List<Yande_post_json> list;
        #region 通用获取json返回
        public async Task<string> GetWebJsonStringAsync(string uri)
        {
            JS_RequestUri = uri;
            string jsstring;
            try
            {
                using (Windows.Web.Http.HttpClient httpclient = new Windows.Web.Http.HttpClient())
                {
                    using (Windows.Web.Http.HttpResponseMessage httpResponseMessage = await httpclient.GetAsync(new Uri(JS_RequestUri)))
                    {
                        httpResponseMessage.EnsureSuccessStatusCode();
                        jsstring = await httpResponseMessage.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                var s = ex.Message.ToString();
                var messagedialog = new MessageDialog(s);
                messagedialog.Commands.Add(new UICommand("重试", cmd => { }, commandId: 0));
                messagedialog.DefaultCommandIndex = 0;
                var a = await messagedialog.ShowAsync();

                a.Invoked += await chonshiAsync();
                jsstring = null;

                throw;
            }

            return jsstring;
        }

        #endregion
        public ObservableCollection<Yande_post_json> list { get; set; }
        public ObservableCollection<Konachan_post_json>list_konachan { get; set; }
        public ObservableCollection<Model.Yande_Tage> tages_yande { get; set; }

        public ObservableCollection<Model.Yande_Tage> SaveJson_Tag_yande(string Jsonstring)
        {
                tages_yande = JsonConvert.DeserializeObject<ObservableCollection<Model.Yande_Tage >>(Jsonstring);
                return tages_yande;
        }
        public ObservableCollection<Yande_post_json> SaveJson(string Jsonstring)
        {  //Newtonsoft.Json 引用
    
           //list = new ObservableCollection<Yande_post_json>();
           try
            {
                list = JsonConvert.DeserializeObject<ObservableCollection<Yande_post_json >>(Jsonstring);
  
                try
                {
                    if ((int)localsettings.Values["_FuckSlider"] != 2) //2才是对的，暂时弄一个不可能的值以防开关误触发
                    {
                        for (int a = list.Count - 1; a >= 0; a--) //动动py想一想都知道用减   //最后我发现我还是不擅长动PY。。。
                        {
                            if (list[a].rating == "q")
                            {
                                list.Remove(list[a]);
                            }
                            else if (list[a].rating == "e")
                            {
                                list.Remove(list[a]);
                            }
                        }
                    }
                }
                catch
                {
                    for (int a = list.Count - 1; a >= 0; a--)  
                    {
                        if (list[a].rating == "q")
                        {
                            list.Remove(list[a]);
                        }
                        else if (list[a].rating == "e")
                        {
                            list.Remove(list[a]);
                        }    
                    }
                }
            }
          catch
            {

            }
            return list;
        }
        public ObservableCollection<Konachan_post_json> SaveJson_konachan(string Jsonstring)
        {  //Newtonsoft.Json 引用
           //list = new ObservableCollection<Yande_post_json>();
            try
            {
                list_konachan  = JsonConvert.DeserializeObject<ObservableCollection<Konachan_post_json >>(Jsonstring);
                try
                {
                    if ((int)localsettings.Values["_FuckSlider"] != 2) //2才是对的，暂时弄一个不可能的值以防开关误触发
                    {
                        for (int a = list_konachan.Count - 1; a >= 0; a--) //动动py想一想都知道用减   //最后我发现我还是不擅长动PY。。。
                        {
                            if (list_konachan[a].rating == "q")
                            {
                                list_konachan.Remove(list_konachan[a]);
                            }
                            else if (list_konachan[a].rating == "e")
                            {
                                list_konachan.Remove(list_konachan[a]);
                            }
                        }
                    }
                }
                catch
                {
                    for (int a = list_konachan.Count - 1; a >= 0; a--)
                    {
                        if (list_konachan[a].rating == "q")
                        {
                            list_konachan.Remove(list_konachan[a]);
                        }
                        else if (list_konachan[a].rating == "e")
                        {
                            list_konachan.Remove(list_konachan[a]);
                        }
                    }
                }
            }
            catch
            {  }
            return list_konachan;
        }
        public    bool Loadingitem( string Jsonstring, int limit )
        {
            try
            {    
            var listcount = list.Count;
            var list2 = JsonConvert.DeserializeObject<List<Yande_post_json>>(Jsonstring);
            int z = 0;
            for (int i = listcount; i < listcount + limit; i++)
            {
                list.Add(list2[z]);
                z++;
            }
            //list.Add(list2[1].d)
            try
            {
                if ((int)localsettings.Values["_FuckSlider"] != 2)
                {
                    for (int a = list.Count - 1; a >= 0; a--)  
                    {
                        if (list[a].rating == "q")
                        {
                            list.Remove(list[a]);
                        }
                        else if (list[a].rating == "e")
                        {
                            list.Remove(list[a]);
                        }

                    }

                }
            }
            catch
            {          
                    for (int a = list.Count - 1; a >= 0; a--) 
                    {
                        if (list[a].rating == "q")
                        {
                            list.Remove(list[a]);
                        }
                        else if (list[a].rating == "e")
                        {
                            list.Remove(list[a]);
                        }
                    }
            }
            }
            catch 
            {
  
            }
            return true;
        }
        //河蟹
        public  void NoH()
        {
            if ((int)localsettings.Values["_FuckSlider"] != 2)
            {
                for (int a = list.Count - 1; a >= 0; a--)
                {
                    if (list[a].rating == "q")
                    {
                        list.Remove(list[a]);
                    }
                    else if (list[a].rating == "e")
                    {
                        list.Remove(list[a]);
                    }

                }
            }
        }
        public async void Loadingitem_konachan(string Jsonstring, int limit)
        {
            try
            {
                var listcount = list_konachan.Count;
                var list2 = JsonConvert.DeserializeObject<List<Konachan_post_json>>(Jsonstring);
                int z = 0;
                for (int i = listcount; i < listcount + limit; i++)
                {
                    list_konachan.Add(list2[z]);
                    z++;
                }
                //list.Add(list2[1].d)
                try
                {
                    if ((int)localsettings.Values["_FuckSlider"] != 2)
                    {
                        for (int a = list_konachan.Count - 1; a >= 0; a--)
                        {
                            if (list_konachan[a].rating == "q")
                            {
                                list_konachan.Remove(list_konachan[a]);
                            }
                            else if (list_konachan[a].rating == "e")
                            {
                                list_konachan.Remove(list_konachan[a]);
                            }

                        }

                    }
                }
                catch
                {
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.ToString()).ShowAsync();
            }
        }

        private async Task<UICommandInvokedHandler> chonshiAsync()
        {
            await GetWebJsonStringAsync(JS_RequestUri);
            return null;
        }
    }
}
