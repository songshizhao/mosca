/////////////////////////////////////////////////////////////////
//********输出结果存储类；                                       
//********  主程序输出结果存储
//********          创建/2018-3-22/宋仕钊
//********              上次编辑/2018-3-22/宋仕钊
/////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using System.Xml.Serialization;
using MoscaCore.Models;
using MathNet.Numerics.LinearAlgebra;

namespace MoscaCore.Models
{
    public class OutputModel
    {
        //标题
        [XmlElement(ElementName = "Title")]
        public Title Title { get; set; }
        //稳态计算结果
        [XmlElement(ElementName = "SteadyResult")]
        public Result SteadyResult { get; set; }
        //瞬态计算结果
        [XmlElement(ElementName = "TransientResult")]
        public TransientResult TransientResult { get; set; }
    }


    public class Result
    {
        //平均流数据
        [XmlElement("GeneralFlow")]
        public GeneralFlow GeneralFlow { get; set; }


        //子通道流动数据
        [XmlElement("ChannelsFlow")]
        public List<ChannelFlow> ChannelsFlow { get; set; }

        //燃料棒数据
        [XmlElement("RodsTemperature")]
        public List<RodTemperature> RodsTemperature { get; set; }
    }

    public class TransientResult
    {

        //瞬态计算数据
        [XmlElement("TransientResult")]
        public List<TransientTimer> TransientTimers { get; set; }
    }

    public class TransientTimer
    {
        //轴向序号
        [XmlAttribute("Time")]
        public double Time { get; set; }
        [XmlElement(ElementName = "Result")]
        public Result Result { get; set; }
    }

    //public class RodsTemperature
    //{
    //    [XmlElement("RodTemperature")]
    //    public List<RodTemperature> RodTemperature { get; set; }
    //}

    public class RodTemperature
    {
        //当前燃料棒的编号
        [XmlElement("Index")]
        public int Index { get; set; }
        //子芯块温度关键数据整理
        [XmlElement("SubRodTemperature")]
        public List<SubRodTemperature> SubRodTemperature { get; set; }
        //芯块温度场
        [XmlElement("TemperatureField")]
        public Matrix<double> TemperatureField { get; set; }
    }

    public class SubRodTemperature
    {
        /// <summary>轴向序号</summary>
        [XmlAttribute("Index")]
        public int Index { get; set; }
        /// <summary>包壳外表面温度</summary>
        [XmlAttribute("CladOutsideT")]
        public double CladOutsideT { get; set; }
        /// <summary>包壳内表面温度</summary>
        [XmlAttribute("CladInsideT")]
        public double CladInsideT { get; set; }

        /// <summary>芯块外表面温度</summary>
        [XmlAttribute("PelletOutsideT")]
        public double PelletOutsideT { get; set; }

        /// <summary>芯块内表面温度</summary>
        [XmlAttribute("PelletCenterT")]
        public double PelletCenterT { get; set; }

        /// <summary>权重平均换热系数</summary>
        [XmlAttribute("h")]
        public double h { get; set; }

        /// <summary>实际热流密度</summary>
        [XmlAttribute("q")]
        public double Q { get; set; }

        /// <summary> 临界热流密度</summary>
        [XmlAttribute("qc")]
        public double Qc { get; set; }

        /// <summary>临界泡核沸腾比-DNBR</summary>
        [XmlAttribute("DNBR")]
        public double DNBR { get; set; }

    }

    //public class TemperatureField
    //{
    //    //材料名称
    //    [XmlAttribute("Radius")]
    //    public double Radius { get; set; }
    //    //内置或者自定义
    //    [XmlAttribute("Temperature")]
    //    public double Temperature { get; set; }
    //}

    //平均流
    public class GeneralFlow
    {
        [XmlElement("FluidData")]
        public List<FluidData> FluidDatas { get; set; }
    }



    public class ChannelFlow
    {
        //材料名称
        [XmlAttribute("ChannelIndex")]
        public int ChannelIndex { get; set; }
        //内置或者自定义
        [XmlElement("FluidData")]
        public List<FluidData> FluidDatas { get; set; }
    }


}











////平均流数据
//[XmlElement("GeneralFlow")]
//public GeneralFlow GeneralFlow { get; set; }
////子通道数据
//[XmlElement("ChannelFlows")]
//public ChannelFlowCollection ChannelFlows { get; set; }
////燃料棒数据
//[XmlElement("RodsTemperature")]
//public RodsTemperature RodsTemperature { get; set; }