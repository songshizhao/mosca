using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MoscaCore.Models
{
    public enum CHF_Formula_Types
    {
        ///DNBR临界热流密度关系式选用----
        //= 0，不进行CHF分析；
        //= 1，BA & W - 2关系式 *
        //= 2，W - 3关系式
        //= 3，EPRI关系式 *
        //= 4，Macbeth 关系式（12系数）*
        //= 5，Macbeth 关系式（6系数）*
        //= 6，Biasi 关系式*
        //= 7，改进Barnett关系式 *
        ///----------------------------
        [XmlEnum(Name = "Look-Up-Table")]
        LookUpTable = 0,
        [XmlEnum(Name = "BAW2")]
        BAW2 = 1,
        [XmlEnum(Name = "W3")]
        W3 = 2,
        [XmlEnum(Name = "EPRI")]
        EPRI = 3,
        [XmlEnum(Name = "Macbeth-12")]
        Macbeth12 = 4,
        [XmlEnum(Name = "Macbeth-6")]
        Macbeth6 = 5,
        [XmlEnum(Name = "Biasi")]
        Biasi = 6,
        [XmlEnum(Name = "Barnett")]
        Barnett = 7,
    }

    /// <summary>选项集合</summary>
    public class Options
    {
        /// <summary>堆芯分析还是子通道分析，0表示堆芯整体分析，1表示子通道分析</summary>
        [XmlElement(ElementName = "AnsysType")]
        public int AnsysType { get; set; }
        /// <summary>是否考虑不同通道之间的交混</summary>
        [XmlElement(ElementName = "IsOpenChannel")]
        public int IsOpenChannel { get; set; }

        //有默认值------------------------------------------------------------------------
        private CHF_Formula_Types dnbr_Formula = CHF_Formula_Types.LookUpTable;
        /// <summary>CHF公式选取</summary>
        [XmlElement(ElementName = "DNBR-Formula")]
        public CHF_Formula_Types DNBR_Formula
        {
            get { return dnbr_Formula; }
            set { dnbr_Formula = value; }
        }
        private int pelletSegment = 6;
        /// <summary>燃料芯块分段数,节点数=分段数+1</summary>
        [XmlElement(ElementName = "Pellet-Segment")]
        public int PelletSegment
        {
            get { return pelletSegment; }
            set { pelletSegment = value; }
        }

        private int cladSegment = 3;
        /// <summary>燃料包壳划分的分段数,节点数=分段数+1</summary>
        [XmlElement(ElementName = "Clad-Segment")]
        public int CladSegment
        {
            get { return cladSegment; }
            set { cladSegment = value; }
        }

        private Iteration iteration = new Iteration();
        /// <summary>燃料包壳划分的分段数,节点数=分段数+1</summary>
        [XmlElement(ElementName = "Iteration")]
        public Iteration Iteration
        {
            get { return iteration; }
            set { iteration = value; }
        }

        private PowerFactor powerFactor = new PowerFactor();
        /// <summary>功率因子，总功率乘子/燃料棒中份额/燃料包壳中份额/</summary>
        [XmlElement(ElementName = "PowerFactor")]
        public PowerFactor PowerFactor
        {
            get { return powerFactor; }
            set { powerFactor = value; }
        }

        private FluidFrictionFactor fff = new FluidFrictionFactor();
        /// <summary>计算摩擦因子FluidFrictionFactor</summary>
        [XmlElement(ElementName = "FluidFrictionFactor")]
        public FluidFrictionFactor FFF
        {
            get { return fff; }
            set { fff = value; }
        }

        private Precision precision = new Precision();
        /// <summary>计算精确度，小数点后保留位数</summary>
        [XmlElement(ElementName = "Precision")]
        public Precision Precision
        {
            get { return precision; }
            set { precision = value; }
        }

        private Transient transient = new Transient();
        /// <summary>瞬态关系定义</summary>
        [XmlElement(ElementName = "Transient")]
        public Transient Transient
        {
            get { return transient; }
            set { transient = value; }
        }


    }
}
