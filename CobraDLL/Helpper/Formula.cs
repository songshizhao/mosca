//·····························································
//********Formula CLASS；                                       *
//********  主程序全局函数，继承此类
//********      使用到的全局函数加入此类中
//********          创建/2018-3-22/宋仕钊
//********              上次编辑/2018-4-10/宋仕钊
//将一些通用 全局变量&通用函数 加入此类，主函数继承此类
//·····························································

using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CobraDLL.Models;
using System.Diagnostics;

namespace CobraDLL
{
    public class FormulaBase
    {

        #region 全局变量
        //圆周率π=3.1416
        public const double PI = 3.1416;
        //重力加速度
        public double G = 9.8;
        //默认水物性参数 IF97 或者 IF67 默认=97,对应UEWASP.dll调用
        public static long Range = 97;
        #endregion




        #region 全局方法(持续完善)
        /// <summary>
        /// 计算摩擦因子
        /// </summary>
        /// <param name="Re">雷诺数</param>
        /// <param name="a">缺省0.32</param>
        /// <param name="b">缺省-0.25</param>
        /// <param name="c">缺省0</param>
        /// <returns></returns>
        internal double FrictionFactor(double Re, double a = 0.32, double b = -0.25, double c = 0)
        {
            double r = a * Math.Pow(Re, b) + c;
            return r;
        }

        /// <summary>
        /// 经验公式，计算努塞尔数
        /// </summary>
        /// <param name="Re">雷诺数</param>
        /// <param name="Pr">普朗特数</param>
        /// <returns>努塞尔数</returns>
        internal double Nu(double Re, double Pr, double a = 0.023, double b = 0.8, double c = 0.4)
        {
            return a * Math.Pow(Re, b) * Math.Pow(Pr, c);
        }
        /// <summary>
        /// 计算对流换热系数
        /// </summary>
        /// <param name="Re">雷诺数</param>
        /// <param name="Pr">普朗特数</param>
        /// <param name="Lamd">导热系数</param>
        /// <param name="de">水力直径</param>
        /// <param name="DNBR">DNBR值只有在DNB小于1时产生作用</param>
        /// <returns>对流换热系数</returns>
        internal double h_convect(double Re, double Pr, double Lamd, double de, double DNBR = 5)
        {
            //未完善需要补充
            //如定义了完整的沸腾曲线，对流换热系数根据完整的沸腾曲线得出
            return Nu(Re, Pr) * Lamd / de;
        }

        /// <summary>
        /// 计算临界热流密度
        /// </summary>
        /// <param name="Xe">热平衡寒气率</param>
        /// <param name="d">等效水力直径</param>
        /// <param name="G">质量流密度</param>
        /// <param name="h_f">饱和比焓</param>
        /// <param name="h_in">入口比焓</param>
        /// <param name="formlula">公式选取1~n</param>
        /// <returns></returns>
        internal double Q_Critical(double Xe, double d, double G, double h_f, double h_in, int formlula)
        {

            //未完善需要补充
            double Qc = 0;

            switch (formlula)
            {
                case 0:

                    break;
                case 1:

                    break;
                case 2:

                    Qc = W3_Formula(Xe, d, G, h_f, h_in);

                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;

                default:
                    break;
            }

            //            DNBR临界热流密度关系式选用
            //= 0，不进行CHF分析；
            //= 1，BA & W - 2关系式 *
            //     = 2，W - 3关系式
            //        = 3，EPRI关系式 *
            //         = 4，Macbeth 关系式（12系数）*
            //          = 5，Macbeth 关系式（6系数）*
            //           = 6，Biasi 关系式*
            //            = 7，改进Barnett关系式 *


            return Qc;
        }


        //[W3临界热流密度关系式]
        internal double W3_Formula(double Xe, double d, double G, double h_f, double h_in)
        {
            if (Xe < -0.15) { Xe = -0.15; }
            double Qc = 3154600 * (2.022 - 6.24 * 0.148 + (0.1722 - 1.43 * 0.148) * Math.Exp((18.177 - 5.99 * 1.48) * Xe)) * ((0.1484 - 1.596 * Xe + 0.1729 * Math.Abs(Xe) * Xe) * 7.374 / 10000 * G + 1.037) * (1.157 - 0.869 * Xe) * (0.2664 + 0.8357 * Math.Exp(-124.1 * d)) * (0.8258 + 3.41 / 10000000 * (h_f - h_in));
            return Qc;
        }



        //[EPRI临界热流密度关系式]
        internal double EPRI_Formula(double Xe, double d, double G, double h_f, double h_in)
        {
            if (Xe < -0.15) { Xe = -0.15; }
            double Qc = 3154600 * (2.022 - 6.24 * 0.148 + (0.1722 - 1.43 * 0.148) * Math.Exp((18.177 - 5.99 * 1.48) * Xe)) * ((0.1484 - 1.596 * Xe + 0.1729 * Math.Abs(Xe) * Xe) * 7.374 / 10000 * G + 1.037) * (1.157 - 0.869 * Xe) * (0.2664 + 0.8357 * Math.Exp(-124.1 * d)) * (0.8258 + 3.41 / 10000000 * (h_f - h_in));
            return Qc;
        }



        //[Biasi临界热流密度关系式]
        internal double Biasi_Formula(double Xe, double d, double G, double h_f, double h_in)
        {
            if (Xe < -0.15) { Xe = -0.15; }
            double Qc = 3154600 * (2.022 - 6.24 * 0.148 + (0.1722 - 1.43 * 0.148) * Math.Exp((18.177 - 5.99 * 1.48) * Xe)) * ((0.1484 - 1.596 * Xe + 0.1729 * Math.Abs(Xe) * Xe) * 7.374 / 10000 * G + 1.037) * (1.157 - 0.869 * Xe) * (0.2664 + 0.8357 * Math.Exp(-124.1 * d)) * (0.8258 + 3.41 / 10000000 * (h_f - h_in));
            return Qc;
        }


        /// <summary>
        /// 计算下一个节点的数据
        /// </summary>
        /// <param name="coolent">冷却剂 对象模型</param>
        /// <param name="income">入口流动节点 上一个节点的数据</param>
        /// <param name="channel">所要计算的通道模型</param>
        /// <param name="Lj">长度m</param>
        /// <param name="power">线功率。W/m</param>
        /// <param name="massVelocity">质量流速(kg/s)</param>
        /// <param name="deltaP">压降Pa *这是个返回值*</param>
        /// <param name="flowDirection">流动方向（-1~1）</param>
        /// <returns></returns>
        internal FluidData NodeToNext(
            Fluid coolent, FluidData income, Channel channel, double Lj,
            double power, double massVelocity, Precision Acc, out double deltaP,
            int chf_indicator = 0, double flowDirection = 1)
        {
            ///****根据根据流体比焓和功率关系，从一个节点数据，计算下一个节点。
            ///****输出模型中，压力单位是Mpa，压降计算中，压力单位为Pa，注意单位转换

            //等效水力直径
            double De = 4 * channel.FlowArea / channel.WetPerimeter;
            //入口比焓
            double Hin = coolent.GetH(income.Temperature, income.Pressure);
            //计算出口比焓
            double Hout = Hin + power * 0.001 * Lj / income.MassFlowRate;
            //入口压力(压力显示单位为Mpa,计算单位使用pa)
            double Pin = income.Pressure;
            //出口压力
            double Pout = Pin;
            //入口位置
            double pos_in = income.Position;
            //出口位置
            double pos_out = pos_in + Lj;
            //入口温度
            double Tin = income.Temperature;
            //出口温度
            double Tout = coolent.GetT(Hout, Pout);
            //出口节点流体密度
            double density = coolent.GetDensity(Tout, Pout);
            //出口流速
            double velocity = massVelocity / channel.FlowArea / density;
            //运动粘度
            double Kv = coolent.GetKv(Tout, Pout);
            //雷诺数
            double Re = velocity * De / Kv;
            //普朗特数
            double Pr = coolent.GetPr(Tout, Pout);
            //导热系数
            double K = coolent.GetK(Tout, Pout);
            //重力压降
            double Ph = density * G * Lj * flowDirection;
            //摩擦压降
            double Pf = FrictionFactor(Re) * Lj / De * density * velocity * velocity * 0.5;
            //加速压降
            double Pa = density * (velocity + income.Velocity) * (velocity - income.Velocity) * 0.5;
            //总压降
            deltaP = (Ph + Pf + Pa);
            //出口节点压力,显示单位是Mpa
            Pout = Pin - deltaP * 0.000001;
            //Debug.WriteLine("Pf:" + Pf);
            //Debug.WriteLine("流速：" + velocity);
            //Debug.WriteLine("雷诺数：" + Re);
            //Debug.WriteLine("雷诺数1：" + FrictionFactor(Re));
#if DEBUG
            if (Hout < 0 | density < 0 | Pout < 0)
            {
                Debug.WriteLine("入口温度" + Tin);
                Debug.WriteLine("流通面积" + channel.FlowArea);
                Debug.WriteLine("密度：" + density);
                Debug.WriteLine("质量流速：" + massVelocity);
                Debug.WriteLine("流速：" + velocity);
                Debug.WriteLine("雷诺数：" + Re);
                Debug.WriteLine("Ph:" + Ph);
                Debug.WriteLine("Pf:" + Pf);
                Debug.WriteLine("Pa:" + Pa);
                Debug.WriteLine("△P:" + deltaP + "pa");
                Debug.WriteLine("压力：" + Pout + "Mpa");
                throw new Exception("计算出现错误(出现负值),请查看调试信息");
            }
#endif
            ///计算临界热流密度
            //饱和液体比焓
            double Hf = coolent.GetHf(Pin);
            //饱和蒸汽比焓
            double Hg = coolent.GetHg(Pin);
            //热平衡寒气率
            double Xe = (Hout - Hf) / (Hg - Hf);
            //计算临界热流密度
            double Qc = Q_Critical(Xe, De, velocity * density, Hf, Hout, chf_indicator);
            //本地临界热流密度
            double Ql = power / channel.HotPerimeter;
            //DNBR默认保留三位小数点
            double DNBR = Qc / Ql;
            //对流换热系数
            double h = h_convect(Re, Pr, K, De, DNBR);
            FluidData outcome = new FluidData
            {
                //出口比焓
                Enthalphy = Math.Round(Hout, Acc.H),
                //先预定压力为入口节点的压力
                Pressure = Math.Round(Pout, Acc.Pressure + 6),
                //出口温度
                Temperature = Math.Round(Tout, Acc.T),
                //节点位置
                Position = pos_out,
                //质量流速
                MassFlowRate = Math.Round(massVelocity, Acc.MassFlowRate),
                //流体密度
                Density = Math.Round(density, Acc.Density),
                //出口流速
                Velocity = Math.Round(velocity, Acc.Velocity),
                //运动粘度
                Kv = Math.Round(Kv, Acc.Kv),
                //雷诺数
                Re = Math.Round(Re, Acc.Re),
                //普朗特数
                Pr = Math.Round(Pr, Acc.Pr),
                //导热系数
                K = Math.Round(K, Acc.K),
                //烧毁比
                DNBR = Math.Round(DNBR, 3),
                //对流换热系数
                h = Math.Round(h, Acc.h),
                //热平衡含气率 -1~1     ·
                Xe = Math.Round(Xe, 3),
            };
            return outcome;
        }












        //public void sd(Rod rod,RodType rodType, Material pellet, Material clad,GasGap gasgap,double power)
        //{

        //    //燃料棒i的加权平均对流换热系数
        //    double h = 0;
        //    //燃料棒i的周围主流流体温度
        //    double Tf = 0;
        //    //燃料棒i的
        //    double TotalAngle = 0;
        //    //质量密度
        //    double massFlowDensity = 0;


        //    //燃料棒直径
        //    double d_rod = rodType.Diameter;
        //    //燃料芯块直径
        //    double d_pellet = rodType.PelletDiameter;
        //    //燃料包壳厚度
        //    double CladThickness = rodType.CladThickness;
        //    //气体间隙通过计算得出
        //    double GapThickness = (d_rod - d_pellet) * 0.5 - CladThickness;

        //    //遍历 与 [燃料棒i] 接触的 子通道
        //    foreach (ContactedChannel item in Rods[i].ContactedChannel)
        //    {
        //        ChannelFlow OneContactChannel = new ChannelFlow();
        //        foreach (ChannelFlow cf in OutputData.SteadyResult.ChannelFlows.ChannelFlow)
        //        {
        //            if (cf.ChannelIndex == item.Index)
        //            {
        //                OneContactChannel = cf;
        //            }
        //        }

        //        double h_channel = OneContactChannel.FluidDatas[j].h;

        //        h += h_channel * item.Angle;
        //        Tf += OneContactChannel.FluidDatas[j].Temperature * item.Angle;
        //        TotalAngle += item.Angle;//累加接触的角度份额
        //        G1 += OneContactChannel.FluidDatas[j].Velocity * OneContactChannel.FluidDatas[j].Density * item.Angle;
        //    }

        //    h = h / TotalAngle;
        //    Tf = Tf / TotalAngle;
        //    G1 = G1 / TotalAngle;

        //    Debug.WriteLine("G1->" + G1);

        //    //线性功率，单位W/M
        //    double Linearpower = InputData.RodCollection.Rods[i].SubPowerCollection[j].Value;
        //    Linearpower = Linearpower * PowerFactor;
        //    //包壳外表面 - 温度
        //    double Tw = Linearpower * (1 - FluidShare) / PI / d_rod / h + Tf;

        //    //燃料包壳节点数
        //    int CladNode = InputData.Options.CladNode;
        //    int PelletNode = InputData.Options.PelletNode;



        //    double[] T_Clad = new double[CladNode];
        //    double[] T_Pellet = new double[PelletNode];

        //    T_Clad[0] = Tw;
        //    //包壳 -稳态 -控制体积- 模型 -没考虑- 产热
        //    for (int n = 1; n < CladNode; n++)
        //    {
        //        double r1 = 0.5 * d_rod - (n - 1) * CladThickness / (CladNode - 1);
        //        double r2 = 0.5 * d_rod - n * CladThickness / (CladNode - 1);

        //        double R = Math.Log(r1 / r2) / (2 * PI * Clad.K.Get(T_Clad[n - 1]) * Ln);
        //        T_Clad[n] = Linearpower * Lj * R + T_Clad[n - 1];
        //    }

        //    double h_gasGap = InputData.MaterialCollection.GasGap.h;
        //    double T_pellet_outside = Linearpower / h_gasGap / (2 * PI) + T_Clad[CladNode - 1];

        //    T_Pellet[0] = T_pellet_outside;
        //    //芯块 -稳态 -控制体积- 模型 
        //    for (int n = 1; n < PelletNode - 1; n++)
        //    {
        //        //环外径 - 占比
        //        double d1 = 1 - (double)(n - 1) / (PelletNode - 1);
        //        //Debug.WriteLine("d1->" + d1);
        //        //环内径 - 占比
        //        double d2 = 1 - (double)n / (PelletNode - 1);
        //        //Debug.WriteLine("d2->" + d2);
        //        //热阻
        //        double R = Math.Log(d1 / d2) / (2 * PI * Pellet.K.Get(T_Pellet[n - 1]) * Ln);
        //        //平均半径
        //        double d_avg = (d1 + d2) * 0.5;
        //        //环内部产热
        //        double InsideHeat = Linearpower * PelletShare * Ln * (d_avg * d_avg);

        //        //Debug.WriteLine("GenerateHeat->"+ InsideHeat);

        //        T_Pellet[n] = T_Pellet[n - 1] + InsideHeat * R;
        //    }
        //    T_Pellet[PelletNode - 1] = T_Pellet[PelletNode - 2] + 0.25 * Linearpower / Pellet.K.Get(T_Pellet[PelletNode - 2]) * (1.0 / (PelletNode - 1) / (PelletNode - 1));

        //    //燃料棒温度场

        //    //燃料棒节点
        //    SubRodTemperature srt = new SubRodTemperature
        //    {
        //        Index = j,
        //        CladOutside = Math.Round(T_Clad[0], 3),
        //        CladInside = Math.Round(T_Clad[CladNode - 1], 3),
        //        PelletTemperatures = new List<PelletTemperature>(),
        //        h = Math.Round(h, Acc.h),
        //        Q = Math.Round(Linearpower / d_rod / PI, 3),
        //    };

        //    double P = OutputData.ChannelFlows.ChannelFlow[i].FluidDatas[j].Pressure;
        //    double H = OutputData.ChannelFlows.ChannelFlow[i].FluidDatas[j].Enthalphy;
        //    double MassVelocity = OutputData.ChannelFlows.ChannelFlow[i].FluidDatas[j].MassFlowRate;
        //    //double G1 = MassVelocity / InputData.ChannelCollection.Channels[i].FlowArea;

        //    double Hf = Coolent.GetHf(P);
        //    double Hg = Coolent.GetHg(P);

        //    double Xe = (H - Hf) / (Hg - Hf);
        //    //Debug.WriteLine("Xe->" + Xe);

        //    double qc = Q_Critical(Xe, d_rod, G1, Hf, H, InputData.Options.DNBR_Formula);
        //    srt.Qc = Math.Round(qc, 3);
        //    srt.DNBR = Math.Round(srt.Qc / srt.Q, 3);


        //    for (int n = 0; n < PelletNode; n++)
        //    {
        //        //燃料芯块温度场
        //        PelletTemperature pt = new PelletTemperature
        //        {
        //            Radius = Math.Round(1 - (double)n / (PelletNode - 1), 4),
        //            Temperature = Math.Round(T_Pellet[n], Acc.T),
        //        };
        //        srt.PelletTemperatures.Add(pt);
        //    }
        //    OutputData.RodsTemperature.RodTemperature[i].SubRodTemperature.Add(srt);
        //}





        #endregion









    }
}
