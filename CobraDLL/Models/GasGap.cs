using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CobraDLL.Models
{
    //充气间隙模型
    public class GasGap
    {
        ///材料编号
        [XmlAttribute("h")]
        public double h { get; set; }

        //以下只有使用气体计算模型时才采用
        ///常温填充气体压力
        [XmlAttribute("FPRESS")]
        public double FPRESS { get; set; }
        ///气腔的体积
        [XmlAttribute("VPLEN")]
        public double VPLEN { get; set; }
        ///填充气体中He所占的摩尔体积份额
        [XmlAttribute("FRAMOL1")]
        public double FRAMOL1 { get; set; }
        ///填充气体中Xe所占的摩尔体积份额
        [XmlAttribute("FRAMOL2")]
        public double FRAMOL2 { get; set; }
        ///填充气体中Ar所占的摩尔体积份额
        [XmlAttribute("FRAMOL3")]
        public double FRAMOL3 { get; set; }
        ///填充气体中Kr所占的摩尔体积份额
        [XmlAttribute("FRAMOL4")]
        public double FRAMOL4 { get; set; }
        ///填充气体中H所占的摩尔体积份额
        [XmlAttribute("FRAMOL5")]
        public double FRAMOL5 { get; set; }
        ///填充气体中N所占的摩尔体积份额
        [XmlAttribute("FRAMOL6")]
        public double FRAMOL6 { get; set; }

        public double Get()
        {
            if (h >= 0)
            {
                return h;
            }
            else
            {
                return h;//使用模型
            }

        }

    }

}
