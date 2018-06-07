using MT2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT2.CS
{
    class MTHub
    {
  //每日热门
        public static ObservableCollection<Yande_post_json> Hotitemvalue;
        public static ObservableCollection<Konachan_post_json> Hotitemvalue_Konachan;

        //热门的第一张图
#pragma warning disable CS0649 // 从未对字段“MTHub.Frist_IMG”赋值，字段将一直保持其默认值 null
        public static string  Frist_IMG;
#pragma warning restore CS0649 // 从未对字段“MTHub.Frist_IMG”赋值，字段将一直保持其默认值 null
    }
}
