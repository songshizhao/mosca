using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CobraDLL.Models
{
    //质量流量输入
    public class MassFlow
    {
        //质量流速
        [XmlAttribute("MassVelocity")]
        public double MassVelocity { get; set; }
        //入口温度
        [XmlAttribute("Temperature")]
        public double Temperature { get; set; }
        //入口压力
        [XmlAttribute("Pressure")]
        public double Pressure { get; set; }
        //流体材质
        [XmlAttribute("FluidMateralIndex")]
        public int FluidMateralIndex { get; set; }
        [XmlElement(ElementName = "Flow-Direction")]
        public int Flow_Direction { get; set; }
    }
}
