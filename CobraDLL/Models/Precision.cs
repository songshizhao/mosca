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
        private int Pr_Default = 3;//普朗特殊


        [XmlAttribute("T")]
        public int T
        {
            get { return T_Default; }
            set { T_Default = value; }
        }


        [XmlAttribute("h")]
        public int h
        {
            get { return h_Default; }
            set { h_Default = value; }
        }


        [XmlAttribute("H")]
        public int H
        {
            get { return H_Default; }
            set { H_Default = value; }
        }

        [XmlAttribute("Re")]
        public int Re
        {
            get { return Re_Default; }
            set { Re_Default = value; }
        }

        [XmlAttribute("Density")]
        public int Density
        {
            get { return Density_Default; }
            set { Density_Default = value; }
        }

        [XmlAttribute("Velocity")]
        public int Velocity
        {
            get { return Velocity_Default; }
            set { Velocity_Default = value; }
        }

        [XmlAttribute("Pressure")]
        public int Pressure
        {
            get { return Pressure_Default; }
            set { Pressure_Default = value; }
        }

        [XmlAttribute("MassFlowRate")]
        public int MassFlowRate
        {
            get { return MassFlowRate_Default; }
            set { MassFlowRate_Default = value; }
        }

        [XmlAttribute("G")]
        public int G
        {
            get { return G_Default; }
            set { G_Default = value; }
        }

        [XmlAttribute("K")]
        public int K
        {
            get { return K_Default; }
            set { K_Default = value; }
        }

        [XmlAttribute("Pr")]
        public int Pr
        {
            get { return Pr_Default; }
            set { Pr_Default = value; }
        }

    }


}
