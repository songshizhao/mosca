using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MoscaCore.Models
{
    public class Rod
    {
        //燃料棒编号
        [XmlAttribute("Index")]
        public int Index { get; set; }
        //燃料棒编号
        [XmlAttribute("Type")]
        public int Type { get; set; }
        //燃料棒功率分布
        [XmlElement("SubPower")]
        public List<SubPower> SubPowerCollection { get; set; }
        //接触的子通道
        [XmlElement("ContactedChannel")]
        public List<ContactedChannel> ContactedChannel { get; set; }

       
    }

    public class ContactedChannel
    {
        //接触的子通道编号
        [XmlAttribute("Index")]
        public int Index { get; set; }
        //接触的角度，0~360度
        [XmlAttribute("Angle")]
        public Double Angle { get; set; }
    }

    public class SubPower
    {
        //功率开始点
        [XmlAttribute("From")]
        public Double From { get; set; }
        //结束点
        [XmlAttribute("To")]
        public Double To { get; set; }
        //功率值
        [XmlAttribute("Value")]
        public Double Value { get; set; }
    }


}
