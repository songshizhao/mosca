///-------------------------------------------
//********流体参数模型；
//********创建/2017-8-15/宋仕钊；
//********上次编辑/2018-3-28/宋仕钊；
///-------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MoscaCore.Models
{
    public class FluidData
    {

        public FluidData(){}
        /// <summary>
        /// 初始化重载
        /// </summary>
        /// <param name="z">轴向距离</param>
        /// <param name="T">温度</param>
        /// <param name="H">比焓</param>
        /// <param name="rou">密度</param>
        /// <param name="V">速度</param>
        /// <param name="MassRate">质量流速</param>
        /// <param name="P">压力</param>
        /// <param name="h">对流换热系数</param>
        /// <param name="Pr">普朗特数</param>
        /// <param name="Re">雷诺数</param>
        /// <param name="K">导热系数</param>
        /// <param name="Kv">动力粘度</param>
        /// <param name="DNB">偏离沸腾比</param>
        /// <param name="Xe">热平衡含气率</param>
        public FluidData
            (double z,double T, double H, double rou, double V, 
            double MassRate, double P, double _h, double _Pr, 
            double _Re, double _K, double _Kv, double _DNB, double _Xe,Precision acc=null)
        {

            if (acc==null) acc = new Precision();
            
            Position = Math.Round(z,3); 
            Temperature = Math.Round(T, acc.T);
            Enthalphy = Math.Round(H, acc.H);
            Density = Math.Round(rou, acc.Density);
            Velocity = Math.Round(V, acc.Velocity);
            MassFlowRate = Math.Round(MassRate, acc.MassFlowRate);
            Pressure = Math.Round(P, acc.Pressure);
            h = Math.Round(_h, acc.h);
            Pr = Math.Round(_Pr, acc.Pr);
            Re = Math.Round(_Re, acc.Re);
            K = Math.Round(_K, acc.K);
            Kv = Math.Round(_Kv, acc.Kv);
            DNBR = Math.Round(_DNB, 3);
            Xe = Math.Round(_Xe, 3);
        }


        //位置,z/H
        [XmlAttribute("z")]
        public double Position { get; set; }
        //温度,℃
        [XmlAttribute("T")]
        public double Temperature { get; set; }
        //比焓
        [XmlAttribute("H")]
        public double Enthalphy { get; set; }
        //密度
        [XmlAttribute("rou")]
        public double Density { get; set; }
        //流速
        [XmlAttribute("V")]
        public double Velocity { get; set; }
        //质量流速
        [XmlAttribute("M")]
        public double MassFlowRate { get; set; }
        //压力
        [XmlAttribute("P")]
        public double Pressure { get; set; }
        /// <summary>对流换热系数</summary>
        [XmlAttribute("h")]
        public double h { get; set; }
        /// <summary>普朗特数</summary>
        [XmlAttribute("Pr")]
        public double Pr { get; set; }
        //雷诺数
        [XmlAttribute("Re")]
        public double Re { get; set; }
        /// <summary>导热系数</summary>
        [XmlAttribute("K")]
        public double K { get; set; }
        /// <summary>运动粘度</summary>
        [XmlAttribute("Kv")]
        public double Kv { get; set; }
        /// <summary>临界泡核沸腾比-DNBR</summary>
        [XmlAttribute("DNBR")]
        public double DNBR { get; set; }


        /// <summary>热平衡含气率Xe</summary>
        [XmlAttribute("Xe")]
        public double Xe { get; set; }

    }
}
