using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CobraDLL.Models
{


    public class FluidProperty
    {
        //温度
        [XmlAttribute("T")]
        public double T { get; set; }
        //热导率
        [XmlAttribute("K")]
        public double K { get; set; }
        //比焓
        [XmlAttribute("H")]
        public double H { get; set; }
        //密度
        [XmlAttribute("Density")]
        public double Density { get; set; }
        //运动粘度
        [XmlAttribute("Kv")]
        public double Kv { get; set; }
        //普朗特数
        [XmlAttribute("Pr")]
        public double Pr { get; set; }
    }
}
