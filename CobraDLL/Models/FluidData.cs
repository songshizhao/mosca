using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CobraDLL.Models
{
    public class FluidData
    {
        //位置,z/H
        [XmlAttribute("Position")]
        public double Position { get; set; }
        //温度,℃
        [XmlAttribute("Temperature")]
        public double Temperature { get; set; }
        //比焓
        [XmlAttribute("Enthalphy")]
        public double Enthalphy { get; set; }
        //密度
        [XmlAttribute("Density")]
        public double Density { get; set; }
        //流速
        [XmlAttribute("Velocity")]
        public double Velocity { get; set; }
        //质量流速
        [XmlAttribute("MassFlowRate")]
        public double MassFlowRate { get; set; }
        //压力
        [XmlAttribute("Pressure")]
        public double Pressure { get; set; }
        //对流换热系数
        [XmlAttribute("h")]
        public double h { get; set; }
        //普朗特数
        [XmlAttribute("Pr")]
        public double Pr { get; set; }
        //雷诺数
        [XmlAttribute("Re")]
        public double Re { get; set; }
        //导热系数
        [XmlAttribute("K")]
        public double K { get; set; }
        //临界泡核沸腾比-DNBR
        [XmlAttribute("DNBR")]
        public double DNBR { get; set; }
    }
}
