using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.BackgroundTransfer;

namespace MT2.CS
{
    public class TransferModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //下载的操作对象
        public DownloadOperation DownloadOperation { get; set; }
        //ID
        public string ID { get; set; }
        //缩略图
        public string SampUri { get; set; }
        //源
        public string Source { get; set; }
        //下载文件本地地址
         public string Destination { get; set; }

        private int _Progress;
        public int Progress { get { return _Progress; } set { _Progress = value; ralsepropertychanged("Progress");  } }

        //一共需要传输多少字节
        private ulong _totalBytesToReceive;
        public ulong TotalBytesToReceive { get { return _totalBytesToReceive; } set { _totalBytesToReceive = value; ralsepropertychanged("TotalBytesToReceive"); } }

        //已接受到的字节
        private ulong _bytesReceived;
        public ulong BytesReceived { get { return _bytesReceived; } set { _bytesReceived = value; ralsepropertychanged("BytesReceived"); } }



        private void ralsepropertychanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name)); //??
            }
        }
    }
}
