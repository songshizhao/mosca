using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CobraDLL.Models
{
    public class PowerFactor
    {
        //整体因子
        [XmlAttribute("Value")]
        public double Value { get; set; }

        //芯块中份额
        [XmlAttribute("PelletShare")]
        public double PelletShare { get; set; }

        //包壳中份额
        [XmlAttribute("CladShare")]
        public double CladShare { get; set; }

        //直接 产生 在冷却剂中的份额
        [XmlAttribute("FluidShare")]
        public double FluidShare { get; set; }

    }
}
