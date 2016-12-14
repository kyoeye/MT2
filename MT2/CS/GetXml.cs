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
                        var s = httpResponseMessage.StatusCode.ToString();
                        switch ( s )
                {
                    case "200":
                       
                        break;
                    case "421":

                        break;   

                }
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
                //后面加个弹窗(～o￣3￣)～
                var ectc = ec.ToString();
                TextDialog ectctc = new TextDialog();
                ectctc.TopTextblocktext("怒刷存在的异常酱");
                ectctc.Contenttextblocktext(ectc);
                result = null;

            }
            return result;
        }
    }
}
