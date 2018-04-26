using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MoscaCore.Models
{


    public enum IterationTypes
    {
        [XmlEnum(Name = "IOIteration")]
        IOIteration = 0,
        [XmlEnum(Name = "NodeIteration")]
        NodeIteration = 1,
        [XmlEnum(Name = "FieldIteration")]
        FieldIteration = 2,
    }


    public class Iteration
    {
        private IterationTypes iterationType = IterationTypes.IOIteration;//默认的迭代方法为进出口迭代
        [XmlAttribute("IterationType")]
        public IterationTypes IterationType
        {
            get { return iterationType; }
            set { iterationType = value; }
        }

        private int maxIteration = 20;
        [XmlAttribute("MaxIteration")]
        public int MaxIteration
        {
            get { return maxIteration; }
            set { maxIteration = value; }
        }

     
        private double sigma = 100;
        [XmlAttribute("Sigma")]
        public double Sigma
        {
            get { return sigma; }
            set { sigma = value; }
        }

    }
}
