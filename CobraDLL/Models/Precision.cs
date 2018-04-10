//////////////////////////////////////////////
//********精确度控制模型；
//********创建/2017-8-15/宋仕钊；
//********上次编辑/2018-3-28/宋仕钊；
//////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CobraDLL.Models
{
    /// <summary>
    /// 计算精确度
    /// </summary>
    public class Precision
    {
        private int T_Default = 3;//温度
        private int h_Default = 3;//对流换热系数
        private int H_Default = 3;//比焓
        private int Re_Default = 3;//雷诺
        private int Density_Default = 3;//密度
        private int Velocity_Default = 3;//速度
        private int Pressure_Default = 3;//压力
        private int MassFlowRate_Default = 3;//质量流速
        private int G_Default = 3;//质量流密度
        private int K_Default = 3;//导热系数
        private int Kv_Default = 3;//运动粘度
        private int Pr_Default = 3;//普朗特殊

        /// <summary>
        /// 温度计算精度
        /// </summary>
        [XmlAttribute("T")]
        public int T
        {
            get { return T_Default; }
            set { T_Default = value; }
        }

        /// <summary>
        /// 对流换热系数计算精度
        /// </summary>
        [XmlAttribute("h")]
        public int h
        {
            get { return h_Default; }
            set { h_Default = value; }
        }

        /// <summary>
        /// 比焓计算精度
        /// </summary>
        [XmlAttribute("H")]
        public int H
        {
            get { return H_Default; }
            set { H_Default = value; }
        }
        /// <summary>
        /// 雷诺数计算精度
        /// </summary>
        [XmlAttribute("Re")]
        public int Re
        {
            get { return Re_Default; }
            set { Re_Default = value; }
        }
        /// <summary>
        /// 密度计算精度
        /// </summary>
        [XmlAttribute("Density")]
        public int Density
        {
            get { return Density_Default; }
            set { Density_Default = value; }
        }
        /// <summary>
        /// 速度计算精度m/s
        /// </summary>
        [XmlAttribute("Velocity")]
        public int Velocity
        {
            get { return Velocity_Default; }
            set { Velocity_Default = value; }
        }
        /// <summary>
        /// 压力计算精度Pa
        /// </summary>
        [XmlAttribute("Pressure")]
        public int Pressure
        {
            get { return Pressure_Default; }
            set { Pressure_Default = value; }
        }
        /// <summary>
        /// 质量流苏计算精度
        /// </summary>
        [XmlAttribute("MassFlowRate")]
        public int MassFlowRate
        {
            get { return MassFlowRate_Default; }
            set { MassFlowRate_Default = value; }
        }
        /// <summary>
        /// 质量流密度计算精度
        /// </summary>
        [XmlAttribute("G")]
        public int G
        {
            get { return G_Default; }
            set { G_Default = value; }
        }
        /// <summary>
        /// 导热系数计算精度
        /// </summary>
        [XmlAttribute("K")]
        public int K
        {
            get { return K_Default; }
            set { K_Default = value; }
        }

        /// <summary>
        /// 粘度计算精度
        /// </summary>
        [XmlAttribute("Kv")]
        public int Kv
        {
            get { return Kv_Default; }
            set { Kv_Default = value; }
        }
        /// <summary>
        /// 普朗特数计算精度
        /// </summary>
        [XmlAttribute("Pr")]
        public int Pr
        {
            get { return Pr_Default; }
            set { Pr_Default = value; }
        }

    }


}
