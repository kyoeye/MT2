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
        string JStext;
        ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        //public List<Yande_post_json> list;
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
        public ObservableCollection<Yande_post_json> list { get; set; }

        public ObservableCollection<Yande_post_json> SaveJson(string Jsonstring)
        {  //Newtonsoft.Json 引用
           //list = new ObservableCollection<Yande_post_json>();
           try
            {
                list = JsonConvert.DeserializeObject<ObservableCollection<Yande_post_json>>(Jsonstring);
                try
                {
                    if ((int)localsettings.Values["_FuckSlider"] != 20) //2才是对的，暂时弄一个不可能的值以防开关误触发
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

        public async void Loadingitem(string Jsonstring, int limit)
        {
            try
            {    
            var listcount = list.Count;
            var list2 = JsonConvert.DeserializeObject<List<Yande_post_json>>(Jsonstring);
            #region MyRegion
            //ObservableCollection<Yande_post_json> lc =( ObservableCollection < Yande_post_json >) list.Concat(list2); ;
            //var lc =  list.Concat(list2);
            //  var s = lc.Count();
            //  list =(ObservableCollection<Yande_post_json >) lc;
            //var list3 = lc.ToList<Yande_post_json>();

            //var gg =  lc.AsQueryable();
            //var cc = gg.AsEnumerable<Yande_post_json>();
            //var kk = cc.ToList<ObservableCollection<Yande_post_json>>();
            //ObservableCollection <Yande_post_json > query = (ObservableCollection<Yande_post_json>)list.Select(Yande_post_json => Yande_post_json  ).Concat(list2.Select(Yande_post_json => Yande_post_json));

            #endregion
            int z = 0;
            for (int i = listcount; i < listcount + limit; i++)
            {
                list.Add(list2[z]);
                z++;
            }
            //list.Add(list2[1].d)
            try
            {
                if ((int)localsettings.Values["_FuckSlider"] != 20)
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
            catch(Exception ex)
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
