using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CobraDLL.Models
{
    public class RodType
    {

        //燃料棒类型唯一编号
        [XmlAttribute("Index")]
        public int Index { get; set; }
        //包壳材料指定
        [XmlAttribute("CladMaterialIndex")]
        public int CladMaterialIndex { get; set; }
        //芯块材料指定
        [XmlAttribute("PelletMaterialIndex")]
        public int PelletMaterialIndex { get; set; }

        //燃料棒直径
        [XmlAttribute("Diameter")]
        public Double Diameter { get; set; }
        //芯块直径
        [XmlAttribute("PelletDiameter")]
        public Double PelletDiameter { get; set; }
        //包壳厚度
        [XmlAttribute("CladThickness")]
        public Double CladThickness { get; set; }

    }
}
