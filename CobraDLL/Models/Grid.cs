using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CobraDLL.Models
{
    public class Grid
    {
        //格架位置，z/H
        [XmlAttribute("Position")]
        public double Position { get; set; }
        //待定义😰😄
        [XmlAttribute("r")]
        public double r { get; set; }
        //待定义😰
        [XmlAttribute("k")]
        public double k { get; set; }
        //待定义😰
        [XmlAttribute("c")]
        public double c { get; set; }
    }

}
