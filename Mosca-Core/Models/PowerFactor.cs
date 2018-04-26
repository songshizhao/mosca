///---------------------------------------------------
//功率因子模型>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
//说明：该模型用于定义功率因子的输入,属性有默认值.
//创建于 2017-10-15 宋仕钊;上次编辑2018-3-25 宋仕钊
///--------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MoscaCore.Models
{
    public class PowerFactor
    {
        private double multiplier=1;
        private double pelletShare=1;
        private double cladShare = 0;
        private double fluidShare=0;
        /// <summary>
        /// 总功率乘子
        /// </summary>
        [XmlAttribute("Multiplier")]
        public double Multiplier
        {
            get { return multiplier; }
            set { multiplier = value; }
        }

        /// <summary>
        /// 芯块功率份额
        /// </summary>
        [XmlAttribute("PelletShare")]
        public double PelletShare
        {
            get { return pelletShare; }
            set { pelletShare = value; }
        }

        /// <summary>
        /// 包壳中功率份额
        /// </summary>
        [XmlAttribute("CladShare")]
        public double CladShare
        {
            get { return cladShare; }
            set { cladShare = value; }
        }

        /// <summary>
        /// 直接产生在冷却剂中慢化的功率份额
        /// </summary>
        [XmlAttribute("FluidShare")]
        public double FluidShare
        {
            get { return fluidShare; }
            set { fluidShare = value; }
        }

    }
}
