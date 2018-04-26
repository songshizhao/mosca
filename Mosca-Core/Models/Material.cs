using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MoscaCore.Models
{
    public enum MaterialTypes
    {
        [XmlEnum(Name = "GetPropertyByName")]
        GetPropertyByName,
        [XmlEnum(Name = "UserDefine")]
        UserDefine,
    }

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
        public MaterialTypes Type { get; set; }


        private KCollection k=new KCollection();
        ///热导率
        [XmlElement("K")]
        public KCollection K
        {
            get { return k; }
            set { k = value; }
        }

        private CpCollection cp=new CpCollection();
        ///比热容
        [XmlElement("Cp")]
        public CpCollection Cp
        {
            get { return cp; }
            set { cp= value; }
        }

        /// <summary>
        /// 根据温度返回 导热系数 lamd
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        internal double GetK(double T)
        {
            double Value = K.Value;
            List<Data> KData = K.KData;
            if (Type == MaterialTypes.GetPropertyByName)
            {
                switch (Name)
                {
                    case "UO2":
                        KData=KernelMaterial.UO2.K.KData;
                        break;
                    case "Zr4":
                        KData = KernelMaterial.Zr4.K.KData;
                        //Debug.WriteLine("Kdata 数据" + KData.Count);
                        //Main.MsgCenter.ShowMessage("Kdata 数据"+ KData.Count);
                        break;
                    case "Zr2":
                        KData = KernelMaterial.Zr2.K.KData;
                        break;
                    default:
                        break;
                }
            }
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
        /// <summary>
        /// 根据温度返回 比热容 Cp
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        internal double Get(double T)
        {
            double Value = K.Value;
            List<Data> CpData = Cp.CpData;
            if (Type == MaterialTypes.GetPropertyByName)
            {
                switch (Name)
                {
                    case "UO2":
                        CpData = KernelMaterial.UO2.Cp.CpData;
                        break;
                    case "Zr4":
                        CpData = KernelMaterial.Zr4.Cp.CpData;
                        break;
                    case "Zr2":
                        CpData = KernelMaterial.Zr2.Cp.CpData;
                        break;
                    default:
                        break;
                }
            }
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


    //热导率
    public class KCollection
    {
        private double _value=0;
        [XmlAttribute("Value")]
        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }
        private List<Data> kData = new List<Data>();
        [XmlElement("Data")]
        public List<Data> KData
        {
            get { return kData; }
            set { kData = value; }
        }


    }
    //比焓
    public class HCollection
    {
        [XmlAttribute("Value")]
        public double Value { get; set; }
        [XmlElement("Data")]
        public List<Data> HData { get; set; }



        internal double Get(double T)
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
