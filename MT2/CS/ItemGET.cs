//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Linq;

//namespace MT2.CS
//{
//    public class ItemGET
//    {
//        public ItemGET()
//        {
           
//        }
        
//        public void Toitem( string _mystring)
//        {
//            XElement xelement = XElement.Parse(_mystring);
//            IEnumerable<XElement> elements = xelement.Elements ();
//            foreach (var element in elements )
//            {
//                if (element.Name == "post")
//                {
//                    IEnumerable<XAttribute> Items = element.Attributes();
//                    foreach (var item in Items)
//                    {

//                        if (item.Name == "id")
//                        {
//                            lookit._id[lookit.a] = (string)item;
//                        }
//                        else if (item.Name == "preview_url")
//                        {
//                            lookit.previewurl[lookit.a] = (string)item;
//                        }
//                        else if (item.Name == "author")
//                        {
//                            lookit.authorname[lookit.a] = (string)item;
//                        }
//                        else if (item.Name == "sample_url")
//                        {
//                            lookit.sampleurl[lookit.a] = (string)item;
//                        }
//                        else if (item.Name == "jpeg_url")
//                        {
//                            lookit.jpegurl[lookit.a] = (string)item;
//                        }
//                        else if (item.Name == "rating") // 这个判断需要重新写11.5留
//                        {
//                            lookit.ratings[lookit.a] = (string)item;
//                        }
//                    }
//        }
//    }
//}
