using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CobraDLL.Models
{
    public class Fluid
    {
        //材料名称
        [XmlAttribute("Name")]
        public string Name { get; set; }
        //内置或者自定义
        [XmlAttribute("Type")]
        public string Type { get; set; }
        //用户自定义流质
        [XmlElement(ElementName = "FluidProperty")]
        public List<FluidProperty> FluidPropertys { get; set; }
        /// <summary>饱和液体比焓,只有使用自定义冷却剂模型时需要</summary>
        [XmlAttribute("hf")]
        public double HL { get; set; }
        /// <summary>饱和气体比焓,只有使用自定义冷却剂模型时需要</summary>
        [XmlAttribute("hg")]
        public double HG { get; set; }
        //可选使用if97或者if67
        private long Range = 97;


        /// <summary>根据温度返回比焓</summary>
        public double GetH(double T, double P = 0)
        {

            double ReturnH = 0;
            //如果为自定义类型
            if (Type == "UserDefine")
            {
                //比T小的最大序列
                int x = 0;
                //比T大的最小序列
                int y = FluidPropertys.Count;

                for (int i = 0; i < FluidPropertys.Count; i++)
                {
                    if (FluidPropertys[i].T < T && i >= x)
                    {
                        x = i;
                    }
                    else if (FluidPropertys[i].T > T && i <= y)
                    {
                        y = i;
                    }
                    else if (FluidPropertys[i].T == T)
                    {
                        return FluidPropertys[i].H;
                    }
                }
                ReturnH = (T - FluidPropertys[x].T) / (FluidPropertys[y].T - FluidPropertys[x].T) * (FluidPropertys[y].H - FluidPropertys[x].H) + FluidPropertys[x].H;
            }
            //如果类型为默认类型
            else if (Type == "GetPropertyByName")
            {

                WaterProperty.PT2H(P, T, out ReturnH, out Range);

            }

            return ReturnH;

        }

        /// <summary>根据温度返回热导率</summary>
        public double GetK(double T, double P = 0)
        {
            double ReturnValue = 0;
            //如果为自定义类型
            if (Type == "UserDefine")
            {

                //比T小的最大序列
                int x = 0;
                //比T大的最小序列
                int y = FluidPropertys.Count;

                for (int i = 0; i < FluidPropertys.Count; i++)
                {
                    if (FluidPropertys[i].T < T && i >= x)
                    {
                        x = i;
                    }
                    else if (FluidPropertys[i].T > T && i <= y)
                    {
                        y = i;
                    }
                    else if (FluidPropertys[i].T == T)
                    {
                        return FluidPropertys[i].K;
                    }
                }
                ReturnValue = (T - FluidPropertys[x].T) / (FluidPropertys[y].T - FluidPropertys[x].T) * (FluidPropertys[y].K - FluidPropertys[x].K) + FluidPropertys[x].K;
            }
            //如果类型为默认类型
            else if (Type == "GetPropertyByName")
            {

                WaterProperty.PT2RAMD(P, T, out ReturnValue, out Range);

            }

            return ReturnValue;
        }

        /// <summary>根据温度返回普朗特数</summary>
        public double GetPr(double T, double P = 0)
        {

            double ReturnValue = 0;
            //如果为自定义类型
            if (Type == "UserDefine")
            {

                //比T小的最大序列
                int x = 0;
                //比T大的最小序列
                int y = FluidPropertys.Count;

                for (int i = 0; i < FluidPropertys.Count; i++)
                {
                    if (FluidPropertys[i].T < T && i >= x)
                    {
                        x = i;
                    }
                    else if (FluidPropertys[i].T > T && i <= y)
                    {
                        y = i;
                    }
                    else if (FluidPropertys[i].T == T)
                    {
                        return FluidPropertys[i].Pr;
                    }
                }
                ReturnValue = (T - FluidPropertys[x].T) / (FluidPropertys[y].T - FluidPropertys[x].T) * (FluidPropertys[y].Pr - FluidPropertys[x].Pr) + FluidPropertys[x].Pr;
            }
            //如果类型为默认类型
            else if (Type == "GetPropertyByName")
            {

                WaterProperty.PT2PRN(P, T, out ReturnValue, out Range);

            }

            return ReturnValue;
        }

        /// <summary>根据温度返回运动粘度</summary>
        public double GetK_Viscosity(double T, double P = 0)
        {

            double ReturnValue = 0;
            //如果为自定义类型
            if (Type == "UserDefine")
            {

                //比T小的最大序列
                int x = 0;
                //比T大的最小序列
                int y = FluidPropertys.Count;

                for (int i = 0; i < FluidPropertys.Count; i++)
                {
                    if (FluidPropertys[i].T < T && i >= x)
                    {
                        x = i;
                    }
                    else if (FluidPropertys[i].T > T && i <= y)
                    {
                        y = i;
                    }
                    else if (FluidPropertys[i].T == T)
                    {
                        return FluidPropertys[i].Kv;
                    }
                }
                ReturnValue = (T - FluidPropertys[x].T) / (FluidPropertys[y].T - FluidPropertys[x].T) * (FluidPropertys[y].Kv - FluidPropertys[x].Kv) + FluidPropertys[x].Kv;
            }
            //如果类型为默认类型
            else if (Type == "GetPropertyByName")
            {
                WaterProperty.PT2U(P, T, out ReturnValue, out Range);
            }

            return ReturnValue;
        }




        public double GetDensity(double T, double P = 0)
        {

            double ReturnValue = 0;
            //如果为自定义类型
            if (Type == "UserDefine")
            {

                //比T小的最大序列
                int x = 0;
                //比T大的最小序列
                int y = FluidPropertys.Count;

                for (int i = 0; i < FluidPropertys.Count; i++)
                {
                    if (FluidPropertys[i].T < T && i >= x)
                    {
                        x = i;
                    }
                    else if (FluidPropertys[i].T > T && i <= y)
                    {
                        y = i;
                    }
                    else if (FluidPropertys[i].T == T)
                    {
                        return FluidPropertys[i].Density;
                    }
                }
                ReturnValue = (T - FluidPropertys[x].T) / (FluidPropertys[y].T - FluidPropertys[x].T) * (FluidPropertys[y].Density - FluidPropertys[x].Density) + FluidPropertys[x].Density;
            }
            //如果类型为默认类型
            else if (Type == "GetPropertyByName")
            {
                WaterProperty.PT2V(P, T, out ReturnValue, out Range);

                ReturnValue = 1 / ReturnValue;
            }

            return ReturnValue;
        }




        public double GetHf(double P)
        {
            double result = 0;
            if (Type == "UserDefine")
            {
                //当使用自定义模型的时候，返回输入的hf
                result = HL;
            }
            else
            {
                WaterProperty.P2HL(P, out result, out Range);
            }
            return result;
        }

        public double GetHg(double P)
        {
            double result = 0;
            if (Type == "UserDefine")
            {
                //当使用自定义模型的时候，返回输入的hg
                result = HG;
            }
            else
            {
                WaterProperty.P2HG(P, out result, out Range);
            }
            return result;
        }

        public double GetT(double H, double P = 0)
        {

            double ReturnValue = 0;
            //如果为自定义类型
            if (Type == "UserDefine")
            {

                //比T小的最大序列
                int x = 0;
                //比T大的最小序列
                int y = FluidPropertys.Count;

                for (int i = 0; i < FluidPropertys.Count; i++)
                {
                    if (FluidPropertys[i].H < H && i >= x)
                    {
                        x = i;
                    }
                    else if (FluidPropertys[i].H > H && i <= y)
                    {
                        y = i;
                    }
                    else if (FluidPropertys[i].H == H)
                    {
                        return FluidPropertys[i].Density;
                    }
                }
                ReturnValue = (H - FluidPropertys[x].H) / (FluidPropertys[y].H - FluidPropertys[x].H) * (FluidPropertys[y].T - FluidPropertys[x].T) + FluidPropertys[x].T;
            }
            //如果类型为默认类型
            else if (Type == "GetPropertyByName")
            {
                WaterProperty.PH2T(P, H, out ReturnValue, out Range);


            }

            return ReturnValue;
        }

    }
}
