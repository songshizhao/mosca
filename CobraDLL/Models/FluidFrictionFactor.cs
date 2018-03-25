using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CobraDLL.Models
{
    public class FluidFrictionFactor
    {


        private double a = 0.32;
        /// <summary>系数a,默认0.32</summary>
        [XmlAttribute("a")]
        public double A
        {
            get { return a; }
            set { a = value; }
        }

        /// <summary>系数b,默认-0.25</summary>
        private double b = -0.25;
        [XmlAttribute("b")]
        public double B
        {
            get { return b; }
            set { b = value; }
        }

        /// <summary>系数c,默认0</summary>
        private double c = 0;
        [XmlAttribute("c")]
        public double C
        {
            get { return c; }
            set { c = value; }
        }


    }
}
