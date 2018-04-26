using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MoscaCore.Models
{
    public class MaterialCollection
    {
        [XmlElement(ElementName = "Material")]
        public List<Material> Materials { get; set; }

        [XmlElement(ElementName = "Fluid")]
        public Fluid Fluid { get; set; }

        [XmlElement(ElementName = "GasGap")]
        public GasGap GasGap { get; set; }

    }
}
