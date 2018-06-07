using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT2.CS
{
    class Toasttext
    {
        string text="";
        List<string> jvzi = new List<string>();
        public Toasttext  ()
        {
            //未来做成服务器发送的，我能有更多的控制权
            jvzi.Add("这里有个橘子……");
            jvzi.Add("汪汪汪……");
            jvzi.Add("经常打开应用的人运气不会太差喵~");
            jvzi.Add("命运石只会对经常打开应用的人敞开喵~");
            jvzi.Add("少女探索中……");
            jvzi.Add("少女祈祷中……");
            jvzi.Add("(●ˇ∀ˇ●)……");
            jvzi.Add("加载什么的好累……");
            jvzi.Add("年更的辣鸡应用说的就是我吧……");
            jvzi.Add("真是元气满满的一天啊……");
            jvzi.Add("呐，主人又是什么样的friend呢……");
            jvzi.Add("花生教主是个女孩子……");
            jvzi.Add("wtmsb……"); 
           // jvzi.Add("MT娘真的存在……");
            jvzi.Add("Q群531234373是兄弟就来van♂……");
            jvzi.Add("因为大家都懂的原因访问会有些慢哦……");
            jvzi.Add("快来群531234373里和大家van♂啊……");
            jvzi.Add("少女不想被放置play……");
            jvzi.Add("如果是darling的话……也..不是不可以哦……");
            jvzi.Add("八嘎hentai无路赛QAQ……");
            jvzi.Add("咕咕咕……");
            jvzi.Add("蛤……");
            jvzi.Add("在关于里能找到群号喵……");

        }
        public string  GetToasttext()
        {
            Random rd = new Random();
            var  a = rd.Next(0, jvzi.Count);
            text = jvzi[a];
            return text;
        }
    }
}
