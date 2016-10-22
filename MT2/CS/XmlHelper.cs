using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MT2.CS
{
    class XmlHelper
    {
        string[] id = new string[30];
        string[] uri = new string[30];
        //注意，这里暂时还不可行，需要先确定目标，不然这样访问会很乱。。。
        public async void getacg()
        {
            string url = "https://yande.re/post.xml?limit=30";
          
            var mystring = await GetXml.GetWebString(url, null);


            XElement root = XElement.Parse(mystring);
            IEnumerable<XElement> elements = root.Elements();
            foreach (var element in elements)
            {
                if (element.Name == "post")
                {
                    IEnumerable<XAttribute> Items = element.Attributes();
                    foreach (var item in Items)
                    {
                        if (item.Name == "id")
                        {

                            id[0] = (string)item;
                            textb.Text = id[0];

                        }
                        else if (item.Name == "file_url")
                        {
                            uri[0] = (string)item;
                            mybutton.Content = uri[0];


                        }
                        else if (item.Name == "rating")
                        {
                            bool fc = (item.Value != "e");
                            if (fc == false)
                            {
                                break;
                            }
                            break;
                        }

                    }
                }
                break;
            }

            //Windows.Data.Xml.Dom.XmlDocument doc = new Windows.Data.Xml.Dom.XmlDocument();
            //doc.LoadXml (mystring);

            //Windows.Data.Xml.Dom.XmlNodeList schoolNodeList = doc.SelectNodes("/posts");

            //if (schoolNodeList != null)
            //{
            //    foreach (XmlNode schoolNode in schoolNodeList)
            //    {

            //    }
            //}




            //Windows.Data.Xml.Dom.XmlDocument doc = new Windows.Data.Xml.Dom.XmlDocument();
            //doc.LoadXml(mystring);
            //Windows.Data.Xml.Dom.XmlNodeList postsNodeList = doc.SelectNodes("/posts");//一级节点
            //if (postsNodeList != null)
            //{
            //    foreach (Windows.Data.Xml.Dom.XmlAttribute yandeNode in postsNodeList) //循环
            //    {

            //        Windows.Data.Xml.Dom.XmlAttribute gradesNode = yandeNode.NodeName ;
            //        {
            //            System.Xml.XmlNodeList gradeNodeList = gradesNode.ChildNodes;

            //            if (gradeNodeList != null)
            //            {
            //                foreach (XmlNode gradeNode in gradeNodeList)
            //                {
            //                    textb.Text = yandeNode.Attributes["jpeg_url"].Value;

            //                }
            //            }

            //        }


            //    }
            //}
            //else
            //{
            //    textb.Text = "一个坏消息，无法找到posts";
            //}
        }


    }
}
