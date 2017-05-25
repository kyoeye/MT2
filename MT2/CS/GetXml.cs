using MT2.Control;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace MT2.CS
{



    class GetAPIstring
    {

        //private CancellationTokenSource cts = new CancellationTokenSource();
        string RequestUri;

        public   async Task<string> GetWebString(string url ) 
        {
               RequestUri = url;


            string result;

            try
            {



                using (Windows.Web.Http.HttpClient httpclient = new Windows.Web.Http.HttpClient())
                {
                    using (Windows.Web.Http.HttpResponseMessage httpResponseMessage = await httpclient.GetAsync(new Uri(RequestUri)))
                    {
                        //var inputstream = await httpResponseMessage.Content.ReadAsStreamAsync();
                        httpResponseMessage.EnsureSuccessStatusCode();
                        result = await httpResponseMessage.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ec)
            {

                var s = ec.Message.ToString();
                var messagedialog = new MessageDialog(s);
                messagedialog.Commands.Add(new UICommand("重试", cmd =>{ },commandId:0));
                messagedialog.DefaultCommandIndex = 0;
               var a= await messagedialog.ShowAsync();
               
                a.Invoked += await chonshi();
                result = null;

            }
            return result;
            
        }
        //重试
        private async Task< UICommandInvokedHandler> chonshi()
        {
            await GetWebString(RequestUri);
            return null;
        }

        //public static async Task<string> GetWebString(string url, string a)
        //{
        //    Windows.Web.Http.HttpStatusCode statecode = new Windows.Web.Http.HttpStatusCode();
        //    string webString = null;
        //    try
        //    {
        //        using (Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient())
        //        {
        //            using (Windows.Web.Http.HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(new Uri(url)))
        //            {
        //                statecode = httpResponseMessage.StatusCode;
        //                string dialog = "";
        //                switch (statecode.ToString())
        //                {
        //                    case "200":

        //                        break;
        //                    case "403":
        //                        dialog = "403：错误，拒绝访问";
        //                        showText(dialog);
        //                        break;
        //                    case "404":
        //                        dialog = "404：错误，没有找到";
        //                        showText(dialog);
        //                        break;
        //                    case "420":
        //                        dialog = "420：错误，无效记录，记录无法保存";
        //                        showText(dialog);
        //                        break;
        //                    case "421":
        //                        dialog = "421：错误，当日访问次数上限";
        //                        showText(dialog);
        //                        break;
        //                    case "422":
        //                        dialog = "422：错误，当前资源被锁定无法修改";
        //                        showText(dialog);
        //                        break;
        //                    case "423":
        //                        dialog = "423：错误，资源已经存在";
        //                        showText(dialog);
        //                        break;
        //                    case "424":
        //                        dialog = "424：错误，无效参数，给定参数无效";
        //                        showText(dialog);
        //                        break;
        //                    case "500":
        //                        dialog = "500：错误，服务器内部错误";
        //                        showText(dialog);
        //                        break;
        //                    case "503":
        //                        dialog = "503：错误，服务器无法处理当前请求";
        //                        showText(dialog);
        //                        break;

        //                }
        //                httpResponseMessage.EnsureSuccessStatusCode();
        //                string text = await httpResponseMessage.Content.ReadAsStringAsync();
        //                webString = text;
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        webString = null;

        //    }
        //    return webString;
        //}

        private  void getStatuscode(string s)
        {
            string dialog = "";
            switch (s)
            {
                case "200":

                    break;
                case "403":
                    dialog = s + "：错误，拒绝访问";
                    showText(dialog);
                    break;
                case "404":
                    dialog = s + "：错误，没有找到";
                    showText(dialog);
                    break;
                case "420":
                    dialog = s + "：错误，无效记录，记录无法保存";
                    showText(dialog);
                    break;
                case "421":
                    dialog = s + "：错误，当日访问次数上限";
                    showText(dialog);
                    break;
                case "422":
                    dialog = s + "：错误，当前资源被锁定无法修改";
                    showText(dialog);
                    break;
                case "423":
                    dialog = s + "：错误，资源已经存在";
                    showText(dialog);
                    break;
                case "424":
                    dialog = s + "：错误，无效参数，给定参数无效";
                    showText(dialog);
                    break;
                case "500":
                    dialog = s + "：错误，服务器内部错误";
                    showText(dialog);
                    break;
                case "503":
                    dialog = s + "：错误，服务器无法处理当前请求";
                    showText(dialog);
                    break;


            }
        }

        public async  void showText(string a)
        {
            await new MessageDialog(a).ShowAsync();
        }
    }
}
