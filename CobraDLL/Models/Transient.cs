using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CobraDLL.Models
{
    public class Transient
    {


       // public bool Use { get; set; } = false;

        //是否为瞬态计算
        private bool _use=false;
        [XmlAttribute("use")]
        public bool Use
        {
            get { return _use=false; }
            set { _use = value; }
        }

        public List<Timer> Timer { get; set; }

    }


    public class Timer
    {
        [XmlAttribute("Second")]
        public double Second { get; set; }
        [XmlAttribute("PowerMultiplier")]
        public double PowerMultiplier { get; set; }
        [XmlAttribute("MassFlowMultiplier")]
        public double MassFlowMultiplier { get; set; }
    }







}
