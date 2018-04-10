using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CobraDLL.Models
{
    public class Title
    {
        /// <summary>标题内容</summary>
        [XmlAttribute("Value")]
        public string Value { get; set; }

        private List<string> infos=new List<string>();
        /// <summary>信息说明</summary>
        public List<string> Infos
        {
            get { return infos; }
            set { infos= value; }
        }


        //[XmlElement(ElementName = "Info")]
        //public List<string> Infos { get; set; }


    }
}
