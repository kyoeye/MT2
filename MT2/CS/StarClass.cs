using MT2.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace MT2.CS
{
    class StarClass
    {
        #region 源账号收藏
        
        public  static  async Task Setfavourite(int imgid ,string username ,string userpass , int vote,string host)
        {
            try
            {
                string request = $"login={username}&password_hash={userpass}&id={imgid }&score={vote}";
                Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();
                var requestMessage = new Windows.Web.Http.HttpRequestMessage(new Windows.Web.Http.HttpMethod("POST"), new Uri($"{host}post/vote.xml"))
                {
                    Content = new HttpStringContent(request)
                };
                requestMessage.Headers["Accept-Encoding"] = "gzip, deflate, br";
                requestMessage.Content.Headers.ContentType = new Windows.Web.Http.Headers.HttpMediaTypeHeaderValue("application/x-www-form-urlencoded");
                var res = await httpClient.SendRequestAsync(requestMessage);
                var response = await res.Content.ReadAsStringAsync();
            }
            catch(Exception ex)
            {
                await new MessageDialog(ex.ToString()).ShowAsync();
            }
        }
        public static async Task< bool> Isfavourite(int imgid, string username,string userpass,string host)
        {
            bool isfavourite = false;

            try
            {
                string request = $"login={username}&password_hash={userpass}&id={imgid }";
                Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();
                var requestMessage = new Windows.Web.Http.HttpRequestMessage(new Windows.Web.Http.HttpMethod("POST"), new Uri($"{host}post/vote.xml"))
                {
                    Content = new HttpStringContent(request)
                };
                requestMessage.Headers["Accept-Encoding"] = "gzip, deflate, br";
                requestMessage.Content.Headers.ContentType = new Windows.Web.Http.Headers.HttpMediaTypeHeaderValue("application/x-www-form-urlencoded");
                var res = await httpClient.SendRequestAsync(requestMessage);
                var response = await res.Content.ReadAsStringAsync();
                isfavourite = response.Contains("3") ? true : false;
                //if (response.Contains("3"))
                //    isfavourite = true;
                //else
                //    isfavourite = false;
                return isfavourite;

            }
            catch
            {
                return false;
            }

        }


        #endregion
        #region 本地收藏图片
        //本地收藏实现  暂时放弃
        StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
        StorageFile file;
        //收藏图片
        public StarClass(Yande_post_json content)
        {

        }
        private async void savedata()
        {
            var dataFolder = await local.CreateFolderAsync("StarData", CreationCollisionOption.OpenIfExists);
            var file = await dataFolder.CreateFileAsync("ImageStar", CreationCollisionOption.OpenIfExists);
        }
        //收藏tag
        public StarClass(string content)
        {

        }

        #region 读取文件
        private async Task<string> ReadText()
        {
            string textcontent = "";
            var dataFloder = await local.CreateFolderAsync("PrPr", CreationCollisionOption.OpenIfExists);
            file = await dataFloder.CreateFileAsync("PrData", CreationCollisionOption.OpenIfExists);
            //   myreadtextbox3.Text = await FileIO.ReadTextAsync(file);
            //  string  filecontent = await FileIO.ReadTextAsync(file);
            var filestream = await file.OpenReadAsync();
            try
            {
                using (StreamReader streamReader = new StreamReader(filestream.AsStreamForRead((int)filestream.Size)))
                {
                    textcontent = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.ToString()).ShowAsync();
            }
            return textcontent;
        }
        #endregion
        #region 写入文件
        private async void Writetext(string content)
        {
            await FileIO.WriteTextAsync(file, content);
        }
        #endregion
        #endregion


    }
}
