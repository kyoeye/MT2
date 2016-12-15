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
    class GetXml
    {

        private CancellationTokenSource cts = new CancellationTokenSource();

        public static async Task<string> GetWebString(string url, string formData)
        {
            string RequestUri = url;

            //string requestUri = "";
            //if (formData.Contains("="))
            //{
            //    requestUri = string.Concat(new object[]
            //    {
            //        url,
            //        "?",
            //        formData,
            //        "r=",
            //        DateTime.Now.Ticks
            //    });
            //}
            //else
            //{
            //    if (string.IsNullOrEmpty(formData))
            //    {
            //        requestUri = url + "?r=" + DateTime.Now.Ticks;
            //    }
            //    else
            //    {
            //        requestUri = url + formData;
            //    }
            //}

            string result;

            try
            {

                IProgress<Windows.Web.Http.HttpProgress> progress = new Progress<Windows.Web.Http.HttpProgress>();

                HttpClient httpclient = new HttpClient();

                HttpResponseMessage httpResponseMessage = await httpclient.GetAsync(new Uri(RequestUri)); //使用httpResponMessage接收uri返回的信息
                                                                                                          //  HttpStatusCode      statuscode =   httpResponseMessage.StatusCode; //这是一个枚举//可能是不必要的
             

                httpResponseMessage.EnsureSuccessStatusCode();
                var text = await httpResponseMessage.Content.ReadAsStringAsync(); //异步读取字符串
                result = text;
                //var inputstream = await httpResponseMessage.Content.ReadAsStreamAsync();
                //Stream stream = inputstream.AsRandomAccessStream();
                //using (StreamReader reader = new StreamReader(stream))
                //{
                //    string result = reader.ReadToEnd();
                //    wv.NavigateToString(result);
                //}


            }
            catch (Exception ec)
            {
                var s = ec.Message.ToString();
                await new MessageDialog(s ).ShowAsync();
                //var s = httpResponseMessage.StatusCode.ToString();
                //getStatuscode(s);

                //var ectc = ec.ToString();
                //TextDialog ectctc = new TextDialog();
                //ectctc.TopTextblocktext("怒刷存在的异常酱");
                //ectctc.Contenttextblocktext(ectc);
                result = null;

            }
            return result;
        }

        private static void getStatuscode(string s)
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

        public async static void showText(string a)
        {
            await new MessageDialog(a).ShowAsync();
        }
    }
}
