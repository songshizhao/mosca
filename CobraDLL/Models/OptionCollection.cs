using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CobraDLL.Models
{
    /// <summary>选项集合</summary>
    public class OptionCollection
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
        /// <summary>燃料芯块划分的节点数</summary>
        [XmlElement(ElementName = "Pellet-Node")]
        public int PelletNode { get; set; }
        /// <summary>燃料包壳划分的节点数</summary>
        [XmlElement(ElementName = "Clad-Node")]
        public int CladNode { get; set; }

        /// <summary>功率因子，总功率乘子/燃料棒中份额/燃料包壳中份额/</summary>
        [XmlElement(ElementName = "PowerFactor")]
        public PowerFactor PowerFactor { get; set; }

        /// <summary>计算摩擦因子</summary>
        [XmlElement(ElementName = "FluidFrictionFactor")]
        public FluidFrictionFactor FFF { get; set; }


        /// <summary>计算精确度，小数点后保留位数</summary>
        [XmlElement(ElementName = "Precision")]
        public Precision Precision { get; set; }


        /// <summary>瞬态关系定义</summary>
        [XmlElement(ElementName = "Transient")]
        public Transient Transient { get; set; }


    }
}
