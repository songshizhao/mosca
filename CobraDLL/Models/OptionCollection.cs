using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CobraDLL.Models
{
    /// <summary>选项集合</summary>
    public class Options
    {
        /// <summary>堆芯分析还是子通道分析，0表示堆芯整体分析，1表示子通道分析</summary>
        [XmlElement(ElementName = "AnsysType")]
        public int AnsysType { get; set; }
        /// <summary>是否考虑不同通道之间的交混</summary>
        [XmlElement(ElementName = "IsOpenChannel")]
        public int IsOpenChannel { get; set; }
        /// <summary>堆芯分析还是子通道分析，0表示堆芯整体分析，1表示子通道分析</summary>
        [XmlElement(ElementName = "DNBR-Formula")]
        public int DNBR_Formula { get; set; }
        
        //[XmlElement(ElementName = "Pellet-Node")]
        //public int PelletNode { get; set; }
      
        //[XmlElement(ElementName = "Clad-Node")]
        //public int CladNode { get; set; }


        //有默认值/////////////////////////////////////////////////////////////////////////////////////////////////////////


        private int pelletSegment = 6;
        /// <summary>燃料芯块分段数,节点数=分段数+1</summary>
        [XmlElement(ElementName = "Pellet-Segment")]
        public int PelletSegment
        {
            get { return pelletSegment; }
            set { pelletSegment = value; }
        }


        private int cladSegment = 3;
        ///// <summary>燃料包壳划分的分段数,节点数=分段数+1</summary>
        [XmlElement(ElementName = "Clad-Segment")]
        public int CladSegment
        {
            get { return cladSegment; }
            set { cladSegment = value; }
        }






        private int maxIteration = 20;
        [XmlElement(ElementName = "Max-Iteration")]
        public int MaxIteration
        {
            get { return maxIteration; }
            set { maxIteration = value; }
        }

        private PowerFactor powerFactor=new PowerFactor();
        /// <summary>功率因子，总功率乘子/燃料棒中份额/燃料包壳中份额/</summary>
        [XmlElement(ElementName = "PowerFactor")]
        public PowerFactor PowerFactor
        {
            get { return powerFactor; }
            set { powerFactor = value; }
        }

        private FluidFrictionFactor fff=new FluidFrictionFactor();
        /// <summary>计算摩擦因子FluidFrictionFactor</summary>
        [XmlElement(ElementName = "FluidFrictionFactor")]
        public FluidFrictionFactor FFF
        {
            get { return fff; }
            set { fff = value; }
        }


        private Precision precision=new Precision();
        /// <summary>计算精确度，小数点后保留位数</summary>
        [XmlElement(ElementName = "Precision")]
        public Precision Precision
        {
            get { return precision; }
            set { precision = value; }
        }

        private Transient transient=new Transient();
        /// <summary>瞬态关系定义</summary>
        [XmlElement(ElementName = "Transient")]
        public Transient Transient
        {
            get { return transient; }
            set { transient = value; }
        }
    }
}
