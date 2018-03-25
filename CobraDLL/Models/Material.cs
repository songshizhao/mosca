using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CobraDLL.Models
{


    ///材料定义
    public class Material
    {
        ///材料编号
        [XmlAttribute("Index")]
        public int Index { get; set; }
        ///材料名称
        [XmlAttribute("Name")]
        public string Name { get; set; }
        ///内置或者自定义
        [XmlAttribute("Type")]
        public string Type { get; set; }

        ///热导率
        [XmlElement("K")]
        public KCollection K { get; set; }
        ///比热容
        [XmlElement("Cp")]
        public CpCollection Cp { get; set; }
    }


    //热导率
    public class KCollection
    {
        [XmlAttribute("Value")]
        public double Value { get; set; }
        [XmlElement("Data")]
        public List<Data> KData { get; set; }

        public double Get(double T)
        {

            if (Value <= 0)
            {
                int x = 0;//比T小的最大序列
                int y = KData.Count - 1;//比T大的最小序列

                for (int i = 0; i < KData.Count; i++)
                {
                    if (T > KData[i].T && i >= x)
                    {
                        x = i;
                    }
                    if (T < KData[i].T && i <= y)
                    {
                        y = i;
                    }
                    if (KData[i].T == T)
                    {
                        return KData[i].Value;
                    }
                }
                double value = (T - KData[x].T) / (KData[y].T - KData[x].T) * (KData[y].Value - KData[x].Value) + KData[x].Value;
                return value;
            }
            else
            {
                return Value;
            }


        }

    }
    //比焓
    public class HCollection
    {
        [XmlAttribute("Value")]
        public double Value { get; set; }
        [XmlElement("Data")]
        public List<Data> HData { get; set; }
        public double Get(double T)
        {
            int x = 0;//比T小的最大序列
            int y = HData.Count;//比T大的最小序列
            for (int i = 0; i < HData.Count; i++)
            {
                if (HData[i].T < T && i >= x)
                {
                    x = i;
                }
                else if (HData[i].T > T && i <= y)
                {
                    y = i;
                }
                else if (HData[i].T == T)
                {
                    return HData[i].Value;
                }
            }
            double value = (T - HData[x].T) / (HData[y].T - HData[x].T) * (HData[y].Value - HData[x].Value) + HData[x].Value;
            return value;
        }
    }
    //比热容
    public class CpCollection
    {
        [XmlAttribute("Value")]
        public double Value { get; set; }
        [XmlElement("Data")]
        public List<Data> CpData { get; set; }
        public double Get(double T)
        {
            if (Value <= 0)
            {
                int x = 0;//比T小的最大序列
                int y = CpData.Count;//比T大的最小序列
                for (int i = 0; i < CpData.Count; i++)
                {
                    if (CpData[i].T < T && i >= x)
                    {
                        x = i;
                    }
                    else if (CpData[i].T > T && i <= y)
                    {
                        y = i;
                    }
                    else if (CpData[i].T == T)
                    {
                        return CpData[i].Value;
                    }
                }
                double value = (T - CpData[x].T) / (CpData[y].T - CpData[x].T) * (CpData[y].Value - CpData[x].Value) + CpData[x].Value;
                return value;
            }
            else
            {
                return Value;
            }
        }
    }
    //差值数据模型
    public class Data
    {
        ///温度
        [XmlAttribute("T")]
        public double T { get; set; }

        ///数据
        [XmlAttribute("Value")]
        public double Value { get; set; }
    }


}
