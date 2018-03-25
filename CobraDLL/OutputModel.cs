using System.Collections.Generic;
using System.Xml.Serialization;
using CobraDLL.Models;

namespace CobraDLL.Models
{
    public class OutputModel
    {

        //标题
        [XmlElement(ElementName = "Title")]
        public string Title { get; set; }
        //说明
        [XmlElement(ElementName = "Info")]
        public List<string> Info { get; set; }

        //平均流数据
        [XmlElement("GeneralFlow")]
        public GeneralFlow GeneralFlow { get; set; }
        //子通道数据
        [XmlElement("ChannelFlows")]
        public ChannelFlowCollection ChannelFlows { get; set; }
        //燃料棒数据
        [XmlElement("RodsTemperature")]
        public RodsTemperature RodsTemperature { get; set; }

    }
    public class RodsTemperature
    {
        [XmlElement("RodTemperature")]
        public List<RodTemperature> RodTemperature { get; set; }
    }

    public class RodTemperature
    {
        [XmlElement("SubRodTemperature")]
        public List<SubRodTemperature> SubRodTemperature { get; set; }

    }

    public class SubRodTemperature
    {
        //轴向序号
        [XmlAttribute("Index")]
        public int Index { get; set; }
        //包壳外表面
        [XmlAttribute("CladOutside")]
        public double CladOutside { get; set; }
        //包壳内表面
        [XmlAttribute("CladInside")]
        public double CladInside { get; set; }
        //芯块温度场
        [XmlElement("PelletTemperature")]
        public List<PelletTemperature> PelletTemperatures { get; set; }
        
        //权重平均换热系数
        [XmlAttribute("h")]
        public double h { get; set; }
 
        //实际热流密度
        [XmlAttribute("q")]
        public double Q { get; set; }

        //临界热流密度
        [XmlAttribute("qc")]
        public double Qc { get; set; }

        //临界泡核沸腾比-DNBR
        [XmlAttribute("DNBR")]
        public double DNBR { get; set; }

    }

    public class PelletTemperature
    {
        //材料名称
        [XmlAttribute("Radius")]
        public double Radius { get; set; }
        //内置或者自定义
        [XmlAttribute("Temperature")]
        public double Temperature { get; set; }
    }

    //平均流
    public class GeneralFlow
    {
        [XmlElement("FluidData")]
        public List<FluidData> FluidDatas { get; set; }
    }

    public class ChannelFlowCollection
    {
        //内置或者自定义
        [XmlElement("ChannelFlow")]
        public List<ChannelFlow> ChannelFlow { get; set; }
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