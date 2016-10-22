using MT2.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MT2.CS
{
    class GetXml
    {
        
        public static async Task <string> GetWebString (string url,string formData)
        {
            string RequestUri =  url ;
            
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
                using (HttpClient httpclient = new HttpClient())
                {
                    using (HttpResponseMessage httpResponseMessage = await httpclient.GetAsync(new Uri(RequestUri))) //使用httpResponMessage接收uri返回的信息
                    {
                        httpResponseMessage.EnsureSuccessStatusCode(); 
                        var text = await httpResponseMessage.Content.ReadAsStringAsync(); //异步读取字符串
                        result = text;
                    }
                }
            }
            catch(Exception ec)
            {
                //后面加个弹窗(～o￣3￣)～
                var ectc =  ec.ToString();
                TextDialog ectctc = new TextDialog();
                ectctc.TopTextblocktext("异常酱");
                ectctc.Contenttextblocktext(ectc );
                result = null;

            }
            return result;
        } 
    }
}
