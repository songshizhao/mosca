using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CobraDLL.Models
{
    public class GridCollection
    {
        //朝向
        [XmlAttribute("Oriented")]
        public string Oriented { get; set; }
        //格架分定义
        [XmlElement(ElementName = "Grid")]
        public List<Grid> Grids { get; set; }
    }
}
