//***************************************************************
//********InputModel.cs；
//********输入文件模型定义；
//********创建/2017-8-15/宋仕钊；
//********上次编辑/2018-3-22/宋仕钊；
//****************************************************************
using MoscaCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using MoscaCore.Models;

namespace MoscaCore.Models
{


    /// <summary>输入模型</summary>
    [XmlRoot("Root", Namespace= "http://songshizhao.com/MyXMLSchema.xsd")]
    public class InputModel
    {
        /// <summary>标题，有且仅有一个</summary>
        [XmlElement(ElementName = "Title")]
        public Title Title{ get; set; }

        /// <summary>材料描述，包含芯块、气体间隙、包壳、流体等</summary>
        [XmlElement(ElementName = "Materials")]
        public MaterialCollection MaterialCollection{ get; set; }  
 
        /// <summary>燃料棒类型描述，描述莫一种类型燃料棒的结构尺寸</summary>
        [XmlElement(ElementName = "RodTypes")]
        public List<RodType> RodTypes{ get; set; }  
 
        /// <summary>燃料棒集合，分别描述每个燃料棒</summary>
        [XmlElement(ElementName = "Rods")]
        public RodCollection RodCollection { get; set; }  

 
        /// <summary>定位格架，定义定位格架的阻力系数</summary>
        [XmlElement(ElementName = "Grids")]
        public GridCollection GridCollection { get; set; }


        //[XmlElement(ElementName = "Channels")]
        //public List<Channel> Channels { get; set; }
        /// <summary>分别描述每一个子通道</summary>
        [XmlElement(ElementName = "Channels")]
        public ChannelCollection ChannelCollection { get; set; }  
 
        ///<summary>描述质量流量输入情况</summary>
        [XmlElement(ElementName = "MassFlow")]
        public MassFlow MassFlow { get; set; }

        ///<summary>计算选项描述</summary>
        [XmlElement(ElementName = "Option")]
        public Options Options { get; set; }

        
    }





















 






    



}
