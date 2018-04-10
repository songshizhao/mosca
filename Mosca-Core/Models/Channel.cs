using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MoscaCore.Models
{

    public class ChannelCollection 
    {

        [XmlElement("Channel")]
        public List<Channel> Channels { get; set; }
    
    }

    public class Channel
    {
        //通道编号
        [XmlAttribute("Index")]
        public int Index { get; set; }
        //总面积因子
        [XmlAttribute("AreaFactor")]
        public double AreaFactor { get; set; }
        //流通面积
        [XmlAttribute("FlowArea")]
        public double FlowArea { get; set; }
        //湿周
        [XmlAttribute("WetPerimeter")]
        public double WetPerimeter { get; set; }
        //热周
        [XmlAttribute("HotPerimeter")]
        public double HotPerimeter { get; set; }
        //连通的通道
        [XmlElement("ConnectedChannel")]
        public List<ConnectedChannel> ConnectedChannels { get; set; }
        ////接触的燃料棒及份额
        //[XmlElement("ContactedRod")]
        //public List<ContactedRod> ContactedRods { get; set; }
    }

    /// <summary>
    /// 连接的通道
    /// </summary>
    public class ConnectedChannel
    {
        //通道编号
        [XmlAttribute("Index")]
        public int Index { get; set; }
        //连通间隙
        [XmlAttribute("Gap")]
        public double Gap { get; set; }
        //两通道之间的质心距离
        [XmlAttribute("Distance")]
        public double Distance { get; set; }

    }
}
