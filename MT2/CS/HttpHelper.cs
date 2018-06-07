using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace MT2.CS
{
    class HttpHelper
    {
        ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        #region GetToken
        //(content="(.*)") (name="csrf-token")
        private async Task<string> GetToken(string uri)
        {
            string a = "";

            HttpBaseProtocolFilter HBPfilter = new HttpBaseProtocolFilter();
            HBPfilter.CacheControl.WriteBehavior = HttpCacheWriteBehavior.NoCache;
            HBPfilter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;
            HBPfilter.AllowUI = false;
            var httpc = new HttpClient(HBPfilter);
            var str = await httpc.GetStringAsync(new Uri(uri + "user/login"));
            if (uri.Contains("yande"))
            {
                string gz = "content=" + '"' + ".*" + '"'; //不懂正则只能用笨方法移除多余字符了。。
                Regex regex = new Regex(gz);
                MatchCollection match = regex.Matches(str);
                string value = match[2].Value;
                char[] stc = value.ToArray<char>();
                char[] stc2 = new char[value.Length];
                for (int i = 9; i < stc.Length - 1; i++)
                    a += stc[i];

            }
            else
            {
                string gz ="content=\"(.*)\" name=\"csrf-token\""; //在”前面加\原来可以让“不被转义
                Regex regex = new Regex(gz);
                Match match = regex.Match(str);
                  a =match.Groups[1].Value;
            }
            return a;
        }

        #endregion
        #region 登录 参考了prpr的思路
        public async Task<bool> LoginClient(string id, string pass, string uri)
        {
            try
            {
                string token = await GetToken(uri);
                if (token != "")
                {
                    string requestContent = $"authenticity_token={token}&url=&user%5Bname%5D={id}&user%5Bpassword%5D={pass}&commit=Login";
                    HttpClient httpClient = new HttpClient();
                    //   HttpStringContent httpStringContent = new HttpStringContent(requestContent);
                    var message = new HttpRequestMessage(new HttpMethod("POST"), new Uri(uri + "user/authenticate"))
                    {
                        Content = new HttpStringContent(requestContent)
                    };
                    message.Headers["Accept-Encoding"] = "gzip, deflate";
                    message.Content.Headers.ContentType = new Windows.Web.Http.Headers.HttpMediaTypeHeaderValue("application/x-www-form-urlencoded");

                    var res = await httpClient.SendRequestAsync(message);
                    var response = await res.Content.ReadAsStringAsync();
                    //还是大笨蛋的正则
                    string gzz = "<h2>(Hello )(.*)!</h2>";
                    Regex regex2 = new Regex(gzz);
                    Match match2 = regex2.Match(response);
                    string value2 = match2.Groups[1].Value;
                    if (value2 == "Hello ")
                    {
                        string gz = "show/[0-9]*";
                        Regex regex = new Regex(gz);
                        Match match = regex.Match(response);
                        string value = match.Value;
                        var user_id = value.Replace("show/", null);
                        if (uri.Contains("yande"))
                        {
                            SettingHelper.Username_Yande = match2.Groups[2].Value;
                            SettingHelper.Userid_Yande = user_id;
                        }
                        else
                        {
                            SettingHelper.Username_Konachan = match2.Groups[2].Value;
                            SettingHelper.Userid_Konachan = user_id;
                        }
                        #region 暂时没用的cookie
                        //   var _cooke = Getcooke(uri + "user/authenticate");
                        #endregion
                    }
                    else
                    {
                        await new MessageDialog("登录失败，请检查用户名和密码，或者多试几次，如仍失败，请反馈作者QAQ").ShowAsync();
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.ToString()).ShowAsync();
                return false;
            }
            return true;
        }
        #endregion
        public static string Hashpass(string hashpass)
        {
            var a = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha1);
            IBuffer buff = CryptographicBuffer.ConvertStringToBinary(hashpass, BinaryStringEncoding.Utf8);
            var hashed = a.HashData(buff);
            var res = CryptographicBuffer.EncodeToHexString(hashed);
            return res;
        }
        #region cookie

        public List<CookeModel> Getcooke(string uri)
        {
            List<CookeModel> cookes = new List<CookeModel>();
            HttpBaseProtocolFilter httpBaseProtocolFilter = new HttpBaseProtocolFilter();
            HttpCookieCollection cookieCollection = httpBaseProtocolFilter.CookieManager.GetCookies(new Uri(uri));
            foreach (HttpCookie item in cookieCollection)
            {
                cookes.Add(new CookeModel { name = item.Name, value = item.Value });
                //if (item.Name == "notice")
                //{
                //    await new MessageDialog(item.Value).ShowAsync();
                //}
            }
            return cookes;
        }
        public static string Getcookestring(string uri)
        {
            string responsecooke = "";
            HttpBaseProtocolFilter httpBaseProtocolFilter = new HttpBaseProtocolFilter();
            HttpCookieCollection cookieCollection = httpBaseProtocolFilter.CookieManager.GetCookies(new Uri(uri));
            foreach (HttpCookie item in cookieCollection)
            {
                responsecooke += item.Name + "=" + item.Value + "; ";
            }
            return responsecooke;
        }

        public class CookeModel
        {
            public string name { get; set; }
            public string value { get; set; }
        }
        #endregion

    }
}
