using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CobraDLL.Models
{
    public class RodCollection
    {
        //燃料棒分段数
        [XmlAttribute("Segment")]
        public int Segment { get; set; }
        //燃料棒长度
        [XmlAttribute("Length")]
        public double Length { get; set; }
        //燃料棒分定义
        [XmlElement(ElementName = "Rod")]
        public List<Rod> Rods { get; set; }

    }
}
