using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MT2.CS
{
    //public class lcclass
    //{
    //    public string[] previewurl;
    //    public string[] _id;
    //    public string[] authorname;
    //    public int b;
    //    public string lookimguri;//选中索引
    //    public int a = 0; // 数组索引
    //    public string[] sampleurl;
    //    public string[] ratings;
    //    public string[] jpegurl;
    //    public string[] thisname;
    //}

    public class _itemsave
    {
        public string previewurl { get; set; }
        public string id { get; set; }
        public string authorname { get; set; }
        public string lookimguri { get; set; }
        public string sampleurl { get; set; }
        public string ratings { get; set; }
        public string jpegurl { get; set; }
        public string thisname { get; set; }
    }

    public class ItemGET
    {
        public string previewurl;
        public string id;
        public string authorname;
        public string lookimguri;
        public string sampleurl;
        public string ratings;
        public string jpegurl;
        public string thisname;
        //lcclass lc = new lcclass();
        public ItemGET()
        {
        }

        ObservableCollection<_itemsave >itemlist  { get; set; }


        _itemsave itemsave = new _itemsave();
        public void Toitem(string _mystring)
        {


            XElement xelement = XElement.Parse(_mystring);
            IEnumerable<XElement> elements = xelement.Elements();
            foreach (var element in elements)
            {
                if (element.Name == "post")
                {
                    for ( int a = 0; a<1000;a++)
                    {
                        itemlist.Add(new CS._itemsave
                        { id = this.id ,
                            jpegurl = this .jpegurl ,
                            authorname = this.authorname ,
                            ratings = this .ratings ,
                            lookimguri = this .lookimguri ,
                            previewurl =this .previewurl ,
                            sampleurl =this .sampleurl ,
                            thisname = this.thisname });
                    }
                    IEnumerable<XAttribute> Items = element.Attributes();
                    foreach (var item in Items)
                    {
                        switch (item.Name.ToString())
                        {
                            case "id":
                               this. id  = item.ToString();
                                break;
                            case "preview_url":
                               previewurl = (string)item;
                                break;
                            case "author":
                              authorname  = (string)item;
                                break;
                            case "sample_url":
                               sampleurl  = (string)item;
                                break;
                            case "jpeg_url":
                                jpegurl = (string)item;

                                break;
                            case "rating":
                               ratings  = (string)item;
                                break;
                        }
                    }
                }
            }
        }
    }
}

