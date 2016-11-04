using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT2.CS.apiset
{
    public class Listapiset
    {
        public string id { get; set; } //作者id
        public string name { get; set; } //作者名字
        public string imguri { get; set; } //图片uri


        public string tags { get; set;} // 标签，这个实现的方式有点特殊
        public string created_at { get; set; }//创建者
        public string approver_id { get;  set; }//审核人
    }
}
