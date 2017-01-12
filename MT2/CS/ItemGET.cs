using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MT2.CS
{
    public class listClass
    {
        public int a;
        public List<string> previewurl = new List<string>();
        public List<string> id = new List<string>();
        public List<string> authorname = new List<string>();
        public List<string> lookimguri = new List<string>();
        public List<string> sampleurl = new List<string>();
        public List<string> ratings = new List<string>();
        public List<string> jpegurl = new List<string>();
        public List<string> thisname = new List<string>();
    }
    public class listsave
    {
        public int _a { get; set; }
        public string id { get; set; } //作品id
        public string _name { get; set; } //作者名字
        public string imguri { get; set; } //图片uri
        public string preview_url { get; set; }//瀑布流预览图
        public string tags { get; set; } // 标签，这个实现的方式有点特殊
        public string created_at { get; set; }//创建者
        public string approver_id { get; set; }//审核人
        public string sample_url { get; set; }//二级预览
        public string rating { get; set; }//安全等级

    }



    public class ItemGET
    {

        public ObservableCollection<listsave> Listapiitems { get; set; }

        listClass listclass = new listClass ();
        public ItemGET()
        {
        }

       public void getlistitems()
        {
            Listapiitems = new ObservableCollection<listsave >();

            for (int i = 0; i < listclass .id .Count ; i++) // 50为一次瀑布流显示的所有数量
            {
                if (listclass.ratings[listclass .a] != "q")
                {
                    if (listclass.ratings[listclass.a] != "e")
                    {
                        Listapiitems.Add(new CS.listsave
                        {
                            _name = "作者：" + listclass .authorname[listclass .a],
                            rating = listclass .ratings[listclass .a],
                            preview_url = listclass.previewurl[listclass.a],
                            sample_url = listclass.sampleurl[listclass.a],
                            _a = listclass.a,
                            id = listclass.id[listclass.a]
                        });
                    }
                    else
                    {
                        listclass.a++;
                        continue;
                    }
                }
                else
                {
                    listclass.a++;
                    continue;
                }
                listclass.a++;
            }
         
        }
        public void Toitem(string _mystring)
        {
           
            //listclass = new CS.listClass();
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
                                    listclass.id.Add((string)item);                             
                                break;
                            case "preview_url":
                                listclass.previewurl.Add((string)item);
                                break;
                            case "author":
                                listclass.authorname.Add((string)item);
                                break;
                            case "sample_url":
                                listclass.sampleurl.Add((string)item);
                                break;
                            case "jpeg_url":
                                listclass.jpegurl.Add((string)item);
                                break;
                            case "rating":
                                listclass.ratings.Add((string)item);
                                break;
                        }
                    }
                }
            }
        }
    }
}

