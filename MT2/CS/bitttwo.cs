using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MT2.CS
{
    class bitttwo
    {
        public static bool HttpDownloadFile(string url, string path)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream responseStream = null;
            Stream stream = null;
            try
            {
                // 设置参数
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ContinueTimeout = 10000;
                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                responseStream = response.GetResponseStream();

                //创建本地文件写入流
                stream = new FileStream(path, FileMode.OpenOrCreate);

                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    stream.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                stream.Close();
                responseStream.Close();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                try
                {
                    if (response != null) response.Close();
                    if (responseStream != null) responseStream.Dispose();
                    if (stream != null) stream.Dispose();
                }
                catch { }

            }
        }
    }
}
