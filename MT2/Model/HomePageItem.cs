using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT2.Model
{
    public class HomePageItem
    {

        public string id { get; set; } //作者id
        public string name { get; set; } //作者名字
        public string imguri { get; set; } //图片uri


    }

    public class HomeManager
    {
        public static void GetNews(string pictureid , ObservableCollection<HomePageItem> homeimgitems )
        {

        }

        public static List<HomePageItem> getNewsItems()
        {
            var retNewsItems = new List<HomePageItem>();
             
            retNewsItems.Add(new HomePageItem() { id = "等下调用数组" });

            return retNewsItems;
        }
    }
}
