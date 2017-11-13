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
    
       //string hoturl = "https://yande.re/post/popular_recent.xml";
        string xmltext;
        string tohotimg;
        ItemGET hotitemget2 = new ItemGET();

        public HotimageHub()
        {       
            Gethotxml();
        }

        public  async void Gethotxml()
        {
            GetAPIstring getxml2 = new GetAPIstring();
            xmltext = await getxml2.GetWebString(@"https://yande.re/post/popular_recent.xml");
            Gethotimg();

        }
        public void Gethotimg()
        {
         
            hotitemget2.Toitem(xmltext);
            hotitemget2.getlistitems(false);
            Tophotimg = hotitemget2.Listapiitems[1].sample_url;
        }

        public string Tophotimg { get { return tohotimg; } set {  tohotimg = value ; } }
        //建多个模型，多个模型集合存储到同一个xml
        public void SaveXml_Totext()
        {
            
        }

    }
  
}
