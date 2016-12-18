using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MT2.CS
{
    public class lcclass
    {
        public string[] previewurl;
        public string[] _id;
        public string[] authorname;
        public int b;
        public string lookimguri;//选中索引
        public int a = 0; // 数组索引
        public string[] sampleurl;
        public string[] ratings;
        public string[] jpegurl;
        public string[] thisname;
    }

    public class ItemGET
    {
        lcclass lc = new lcclass();
        public ItemGET()
        {
            
        }
        
        public void _lc (int a ) //a为数组容量
        {
            lc.previewurl = new string[a];
            lc._id = new string[a];
            lc.ratings = new string[a];
            lc.authorname = new string[a];
            lc.sampleurl = new string[a];
            lc.jpegurl = new string[a];
            lc.thisname = new string[a];
        }
        public void Toitem(string _mystring)
        {
            XElement xelement = XElement.Parse(_mystring);
            IEnumerable<XElement> elements = xelement.Elements();
            foreach (var element in elements)
            {
                if (element.Name == "post")
                {
                    IEnumerable<XAttribute> Items = element.Attributes();
                    foreach (var item in Items)
                    {
                        switch (item.Name.ToString())
                        {
                            case "id":
                                lc._id[lc.a] = item.ToString();
                                break;
                            case "preview_url":
                                lc.previewurl[lc.a] = (string)item;
                                break;
                            case "author":
                                lc.authorname[lc.a] = (string)item;
                                break;
                            case "sample_url":
                                lc.sampleurl[lc.a] = (string)item;
                                break;
                            case "jpeg_url":
                                lc.jpegurl[lc.a] = (string)item;

                                break;
                            case "rating":
                                lc.ratings[lc.a] = (string)item;
                                break;
                        }
                    }
                }
            }
        }
