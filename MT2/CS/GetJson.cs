using MT2.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace MT2.CS
{
    class GetJson
    {
        string JS_RequestUri;
        string JStext;
        public List<Yande_post_json> list;
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

        public   List<Yande_post_json> SaveJson( string Jsonstring)
        {  //Newtonsoft.Json 引用


               list = JsonConvert.DeserializeObject<List<Yande_post_json>>(Jsonstring);

              for (int a = list.Count-1; a>=0;a-- ) //动动py想一想都知道用减
            {
                if (list[a].rating == "q" )
                {
                    list.Remove(list[a]);                
                }
               else  if (list[a].rating == "e")
                {
                    list.Remove(list[a]);
                }

            }

            //foreach (var _list in list)
            //{
            //    if (_list.rating == "q"||_list.rating == "e")
            //    {
            //        list.Remove(_list);
            //    }
            //}

            return list;
        }

        private async Task<UICommandInvokedHandler> chonshiAsync()
        {
            await GetWebJsonStringAsync(JS_RequestUri);
            return null;
        }
    }
}
