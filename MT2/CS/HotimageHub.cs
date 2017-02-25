using MT2.page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT2.CS
{
  public class HotimageHub 
    {
    
       public  string hoturl = "https://yande.re/post/popular_recent.xml";
        string xmltext;
        string tohotimg;
       
        //public HotimageHub()
        //{         
        //    Gethotxml();
        //}
      
        public async void Gethotxml()
        {
            GetXml getxml = new GetXml();
            xmltext = await getxml.GetWebString(hoturl);
          //Gethotimg();
          
        }
        public void Gethotimg()
        {
            ItemGET hotitemget2 = new ItemGET();
            hotitemget2.Toitem(xmltext);
            hotitemget2.getlistitems(false);
            Tophotimg = hotitemget2.Listapiitems[1].sample_url;
        }

        public string Tophotimg { get { return tohotimg; } set { value = tohotimg; } }
    }
}
