///----------------------------------------------------
//FormulaBase.cs基类
//基类，主程序全局函数，继承此类，使用到的全局函数加入此类中
//创建:2018-3-22 宋仕钊
//修改:2018-4-10 宋仕钊
///----------------------------------------------------
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using MoscaCore.Models;
using System.Diagnostics;
using System.Reflection;

namespace MoscaCore
{
    /// <summary>将一些通用 全局变量&通用函数 加入此类，主函数继承此类</summary>
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

        /// <summary> 什么也不做</summary>
        public static void DoNothing()
        {
            //什么也不做
        }


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
        internal double Q_Critical(double Xe, double d, double G, double h_f, double h_in, CHF_Formula_Types formlula)
        {
            double Qc = 0;
            switch (formlula)
            {
                case CHF_Formula_Types.LookUpTable:
                    Main.MsgCenter.ShowMessage("LookUpTable未完善需要补充");
                    break;
                case CHF_Formula_Types.BAW2:
                    Main.MsgCenter.ShowMessage("BAW2未完善需要补充");
                    break;
                case CHF_Formula_Types.W3:
                    Qc = W3_Formula(Xe, d, G, h_f, h_in);
                    break;
                case CHF_Formula_Types.EPRI:
                    Main.MsgCenter.ShowMessage("EPRI未完善需要补充");
                    break;
                case CHF_Formula_Types.Macbeth12:
                    Main.MsgCenter.ShowMessage("Macbeth12未完善需要补充");
                    break;
                case CHF_Formula_Types.Macbeth6:
                    Main.MsgCenter.ShowMessage("Macbeth6未完善需要补充");
                    break;
                case CHF_Formula_Types.Biasi:
                    Main.MsgCenter.ShowMessage("Biasi未完善需要补充");
                    break;
                case CHF_Formula_Types.Barnett:
                    Main.MsgCenter.ShowMessage("Barnett未完善需要补充");
                    break;
                default:
                    break;
            }
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
            CHF_Formula_Types chf_formula, double flowDirection = 1)
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
            ///计算临界热流密度
            //饱和液体比焓
            double Hf = coolent.GetHf(Pin);
            //饱和蒸汽比焓
            double Hg = coolent.GetHg(Pin);
            //热平衡寒气率
            double Xe = (Hout - Hf) / (Hg - Hf);
            //计算临界热流密度
            double Qc = Q_Critical(Xe, De, velocity * density, Hf, Hout, chf_formula);
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
#if DEBUG
            if (Hout < 0 | density < 0 | Pout < 0 | Pf < 0)
            {
                string MsgHeader = "-----[Fatal error]计算出现错误(出现负值),请查看下面的调试信息-----";
                string MsgBody = "";
                MsgBody += "入口数据\n";
                foreach (PropertyInfo item in income.GetType().GetProperties())
                {
                    MsgBody += item.Name + ":" + item.GetValue(income, null) + "\n";
                }
                MsgBody += "出口数据\n";
                foreach (PropertyInfo item in outcome.GetType().GetProperties())
                {
                    MsgBody += item.Name + ":" + item.GetValue(outcome, null) + "\n";
                }
                MsgBody += "通道数据\n";
                foreach (PropertyInfo item in channel.GetType().GetProperties())
                {
                    MsgBody += item.Name + ":" + item.GetValue(channel, null) + "\n";
                }
                MsgBody += "压降数据\n";
                MsgBody += "Ph：" + Ph + "\n";
                MsgBody += "Pf：" + Pf + "\n";
                MsgBody += "Pa：" + Pa + "\n";
                Debug.WriteLine(MsgHeader);
                Debug.WriteLine(MsgBody);
                //throw new Exception("计算出现错误(出现负值),请查看调试信息");
            }
#endif

#if !DEBUG
            if (Hout < 0 | density < 0 | Pout < 0 | Pf < 0)
            {
                string MsgHeader = "-----[Fatal error]计算出现错误(出现负值),请查看下面的调试信息-----";
                string MsgBody = "";
                MsgBody += "入口数据\n";
                foreach (PropertyInfo item in income.GetType().GetProperties())
                {
                    MsgBody+= item.Name+":"+ item.GetValue(income, null)+ "\n"; 
                }
                MsgBody += "出口数据\n";
                foreach (PropertyInfo item in outcome.GetType().GetProperties())
                {
                    MsgBody += item.Name + ":" + item.GetValue(outcome, null) + "\n";
                }
                MsgBody += "通道数据\n";
                foreach (PropertyInfo item in channel.GetType().GetProperties())
                {
                    MsgBody += item.Name + ":" + item.GetValue(channel, null) + "\n";
                }
                MsgBody += "压降数据\n";
                MsgBody += "Ph：" + Ph + "\n";
                MsgBody += "Pf：" + Pf + "\n";
                MsgBody += "Pa：" + Pa + "\n";
                Main.MsgCenter.ShowMessage(MsgHeader);
                Main.MsgCenter.ShowMessage(MsgBody);
            }
#endif
            return outcome;
        }

 
        internal List<ChannelFlow> GetNextTimeChannelsFlow(
            List<ChannelFlow> channelsFlow,
            List<Rod> rods,
            Fluid coolent,
            IterationTypes iteration,
            double powerFactor,
            double massFactor)
        {
            //根据功率变化指示调节功率
            for (int k = 0; k < rods.Count; k++)
            {
                for (int j = 0; j < rods[k].SubPowerCollection.Count; j++)
                {
                    //功率乘以因子
                    rods[k].SubPowerCollection[j].Value = rods[k].SubPowerCollection[j].Value * powerFactor;
                }

            }

            switch (iteration)
            {
                case IterationTypes.IOIteration:
                    break;
                case IterationTypes.NodeIteration:
                    break;
                case IterationTypes.FieldIteration:
                    break;
                default:
                    break;
            }

            List<ChannelFlow> NewChannelsFlow = new List<ChannelFlow>();
            for (int i = 0; i < channelsFlow.Count; i++)
            {
                //已知当前总流量，重新分配平均流量
            }

            return NewChannelsFlow;
        }






        /// <summary>
        /// 初始化开始节点
        /// </summary>
        /// <param name="Coolent">冷却剂</param>
        /// <param name="totalArea">总流通面积</param>
        /// <param name="channel">计算的通道</param>
        /// <param name="massFlow">质量流速</param>
        /// <param name="Acc">计算准确度</param>
        /// <returns></returns>
        public FluidData SetInitNode(Fluid coolent, double totalArea, Channel channel, MassFlow massFlow, Precision acc)
        {
            //当前子通道入口质量流速
            double massFlowRate = massFlow.MassVelocity * channel.FlowArea / totalArea;
            //入口的参数
            double T = massFlow.Temperature;
            //压力
            double P = massFlow.Pressure;
            //密度
            double density = coolent.GetDensity(T, P);
            //比焓
            double H = coolent.GetH(T, P);
            //流速
            double velocity = massFlowRate / channel.FlowArea / density;
            //等效水力直径
            double De = 4 * channel.FlowArea / channel.WetPerimeter;
            //运动粘度
            double Kv = coolent.GetKv(T, P);
            //雷诺数
            double Re = velocity * De / Kv;
            //普朗特数
            double Pr = coolent.GetPr(T, P);
            //导热系数
            double K = coolent.GetK(T, P);
            //对流换热系数
            double h = h_convect(Re, Pr, K, De);
            //饱和液体比焓
            double Hf = coolent.GetHf(P);
            //饱和蒸汽比焓
            double Hg = coolent.GetHg(P);
            //热平衡寒气率
            double Xe = (H - Hf) / (Hg - Hf);
            //新建节点对象
            FluidData InitNode = new FluidData(0, T, H, density, velocity, massFlowRate, P, h, Pr, Re, K, Kv, 0, Xe, acc);
            return InitNode;
        }

        /// <summary>
        /// 计算稳态子通道流量场，使用进出口压力迭代，即不考虑横向流动
        /// </summary>
        /// <param name="coolent">冷却剂</param>
        /// <param name="channels">通道集合</param>
        /// <param name="rods">燃料棒集合</param>
        /// <param name="massFlow">质量流速对象</param>
        /// <param name="Ni">通道数</param>
        /// <param name="Nj">轴向分段数</param>
        /// <param name="totalArea">总流通面积</param>
        /// <param name="options">计算选项</param>
        /// <returns></returns>
        public List<ChannelFlow> Caculate_Channels_Steady_IOIteration(
            Fluid coolent,
            List<Channel> channels,
            List<Rod> rods,
            MassFlow massFlow,
            int Ni, int Nj,
            double totalArea,
            Options options)
        {
            ///使用压力迭代方法确定稳态不同子通道之间的流量，此计算方法使用压力迭代确定
            ///一些局部变量
            ///

            //初始化输出结果
            List<ChannelFlow> channelsFlow = new List<ChannelFlow>();
            for (int i = 0; i < Ni; i++)
            {
                ChannelFlow channelFlow = new ChannelFlow
                {
                    //流动计算结果 编号与输入的子通道编号相对应
                    ChannelIndex = channels[i].Index,
                    FluidDatas = new List<FluidData>()
                };
                for (int j = 0; j < Nj + 1; j++)
                {
                    channelFlow.FluidDatas.Add(new FluidData());
                }
                channelsFlow.Add(channelFlow);
            }
            //迭代的限制参数
            Iteration iteration = options.Iteration;
            //功率的乘子
            PowerFactor powerFactor = options.PowerFactor;
            //计算的准确度
            Precision acc = options.Precision;
            //CHF公式选取
            CHF_Formula_Types chf_formula = options.DNBR_Formula;
            //流动方向(-1~1)
            double flow_direction = massFlow.Flow_Direction;
            //压降迭代,每个子通道的压降
            double[] DeltaP = new double[Ni];
            //压降迭代,每个子通道的压降
            double[] m = new double[Ni];
            //每个子通道的分段压降,Ni行xNj列初值为0
            Matrix<double> P_Local = Matrix<double>.Build.Dense(Nj + 1, Ni, 0);
            //压降迭代收敛因子
            double Sigma = 0;
            //迭代次数统计
            int iteration_times = 0;
            //迭代压降
            do
            {
                //遍历子通道
                for (int i = 0; i < Ni; i++)
                {
                    //初始化压降
                    DeltaP[i] = 0;
                    //初始化子通道入口数据节点
                    FluidData InitNode = SetInitNode(coolent, totalArea, channels[i], massFlow, acc);
                    //初始化子通道数据节点加入
                    channelsFlow[i].FluidDatas[0] = InitNode;
                    //局部压力场矩阵
                    P_Local[0, i] = InitNode.Pressure;
                    if (iteration_times <= 1)
                    {
                        //质量流速
                        m[i] = InitNode.MassFlowRate;
                    }
                    //每个通道数据节点（共Nj+1个,初始节点1个,循环Nj个）
                    for (int j = 1; j < Nj + 1; j++)
                    {
                        //计算当前段的长度j>=1
                        double Lj = rods[0].SubPowerCollection[j - 1].To - rods[0].SubPowerCollection[j - 1].From;
                        //前一个节点
                        FluidData pre = channelsFlow[i].FluidDatas[j - 1];
                        //当前子通道燃料棒功率输出
                        double SubPowerJ = 0;
                        //遍历所有燃料棒
                        foreach (Rod rod in rods)
                        {
                            //燃料棒所有接触的通道
                            foreach (var ContactedChannel in rod.ContactedChannel)
                            {
                                //如果与燃料棒接触的通道,如果是当前正在计算的通道
                                if (ContactedChannel.Index == channels[i].Index)
                                {
                                    SubPowerJ += rod.SubPowerCollection[j - 1].Value * ContactedChannel.Angle / 360;
                                }
                            }
                        }
                        //乘以功率因子
                        SubPowerJ = SubPowerJ * powerFactor.Multiplier;
                        //计算新节点,NodeToNext计算子通道节点
                        FluidData next = NodeToNext(
                            coolent,
                            pre,
                            channels[i],
                            Lj,
                            SubPowerJ,
                            m[i],
                            acc,
                            out double DeltaPij,
                            chf_formula,
                            flow_direction);
                        //存储计算结果
                        channelsFlow[i].FluidDatas[j] = next;
                        //i棒j段压降
                        P_Local[j, i] = next.Pressure;
                        //压降用于迭代
                        DeltaP[i] += DeltaPij;
                    }
                }
                //初始化迭代收敛因子Sigma
                Sigma = 0;
                //平均压降
                double AvgPressure = 0;
                for (int i = 0; i < Ni; i++)
                {
#if DEBUG
                    Main.MsgCenter.ShowMessage(String.Format("通道{0}压降:{1}", i, DeltaP[i]));
#endif
                    AvgPressure += DeltaP[i];
                }
                //平均压降
                AvgPressure = AvgPressure / Ni;
                //所有偏差之和
                for (int i = 0; i < Ni; i++)
                {
                    Sigma += Math.Abs(DeltaP[i] - AvgPressure);
                }
                double TotalM = 0;
                for (int i = 0; i < Ni; i++)
                {
                    //子通道i压降和平均压降的比值
                    double Factor = Math.Sqrt(AvgPressure / DeltaP[i]);
                    //重新分配压降
                    m[i] = Factor * m[i];// 所有偏差之和
                                         //计算平衡后的总质量流速
                    TotalM += m[i];
                }
                //计算平衡后与平衡前的比值
                double k = massFlow.MassVelocity / TotalM;
                //对质量流量进行修正,保持总质量流速不变
                for (int i = 0; i < Ni; i++)
                {
                    m[i] = k * m[i];
#if DEBUG
                    Main.MsgCenter.ShowMessage(String.Format("通道{0}质量流速:{1}", i, m[i]));
#endif
                }
                //输出迭代收敛因子
                Main.MsgCenter.ShowMessage(String.Format("Sigma->{0}", Sigma));
                //迭代次数+1
                iteration_times += 1;
                Main.MsgCenter.ShowMessage(String.Format("压力迭代次数：{0}", iteration_times));
                if (iteration_times > iteration.MaxIteration)
                {
                    Main.MsgCenter.ShowMessage(String.Format("超过最大迭代次数限制,最大迭代次数限制{0}", iteration.MaxIteration));
                    break;
                }
            }
            while (Sigma > iteration.Sigma);



            // MyIOManager.OutputData.SteadyResult.ChannelsFlow = ChannelsFlow;
            //信息输出
            Main.MsgCenter.ShowMessage("--------压力场预览--------");
            Main.MsgCenter.ShowMessage(P_Local.ToMatrixString(Nj + 1, Ni));

            Main.MsgCenter.ShowMessage("--------流量场预览--------");
            var MassFlowRate = Matrix<double>.Build.Dense(Nj + 1, Ni, (Mi, Mj) => m[Mj]);
            Main.MsgCenter.ShowMessage(MassFlowRate.ToMatrixString(Nj + 1, Ni));

            return channelsFlow;
        }





        /// <summary>
        /// 计算稳态子通道流量场，使用节点压力迭代，即用压力平衡思想考虑横向流动
        /// </summary>
        /// <param name="coolent">冷却剂</param>
        /// <param name="channels">通道集合</param>
        /// <param name="rods">燃料棒集合</param>
        /// <param name="massFlow">质量流速对象</param>
        /// <param name="Ni">通道数</param>
        /// <param name="Nj">轴向分段数</param>
        /// <param name="totalArea">总流通面积</param>
        /// <param name="options">计算选项</param>
        /// <returns></returns>
        public List<ChannelFlow> Caculate_Channels_Steady_NodeIteration(
            Fluid coolent,
            List<Channel> channels,
            List<Rod> rods,
            MassFlow massFlow,
            int Ni, int Nj,
            double totalArea,
            Options options)
        {
            ///使用压力迭代方法确定不同子通道之间的流量，此计算方法使用压力迭代确定
            ///
            Main.MsgCenter.ShowMessage("计算子通道数据,流量计算方式压力迭代：节点迭代...");
            //初始化输出结果
            List<ChannelFlow> channelsFlow = new List<ChannelFlow>();
            for (int i = 0; i < Ni; i++)
            {
                ChannelFlow channelFlow = new ChannelFlow
                {
                    //流动计算结果 编号与输入的子通道编号相对应
                    ChannelIndex = channels[i].Index,
                    FluidDatas = new List<FluidData>()
                };
                for (int j = 0; j < Nj + 1; j++)
                {
                    channelFlow.FluidDatas.Add(new FluidData());
                }
                channelsFlow.Add(channelFlow);
            }
            //迭代的限制参数
            Iteration iteration = options.Iteration;
            //功率的乘子
            PowerFactor powerFactor = options.PowerFactor;
            //计算的准确度
            Precision acc = options.Precision;
            //CHF公式选取
            CHF_Formula_Types chf_formula = options.DNBR_Formula;
            //流动方向(-1~1)
            double flow_direction = massFlow.Flow_Direction;

            //质量流量迭代
            var MassFlowRate = Matrix<double>.Build.Dense(Nj + 1, Ni, 0);
            //每个子通道的分段压降,Ni行xNj列初值为0
            var P_Local = Matrix<double>.Build.Dense(Nj + 1, Ni, 0);
            //压降迭代因子
            double Sigma = 0;
            //质量流速迭代中间变量
            double[] m = new double[Ni];
            //质量流速迭代中间变量
            double[] velocity = new double[Ni];
            //质量流速迭代中间变量
            double[] DeltaP = new double[Nj];
            //遍历所有子通道
            for (int i = 0; i < Ni; i++)
            {
                //初始化j=0入口节点
                FluidData InitNode = SetInitNode(coolent, totalArea, channels[i], massFlow, acc);
                //初始化子通道数据节点加入
                channelsFlow[i].FluidDatas[0] = InitNode;
                //局部压力场矩阵(Mpa)
                P_Local[0, i] = InitNode.Pressure;
                //质量流速kg/s 迭代用）
                m[i] = InitNode.MassFlowRate;
                //流速m/s（迭代用）
                velocity[i] = InitNode.Velocity;
                //质量流速矩阵（输出用）
                MassFlowRate[0, i] = m[i];

            }
            //轴向迭代
            for (int j = 1; j < Nj + 1; j++)
            {
                //迭代次数
                int iteration_times = 0;
                do
                {
                    //计算所有燃料棒j段（J=1~Nj-1）
                    for (int i = 0; i < Ni; i++)
                    {
                        //计算当前段的长度j>=1
                        double Lj = rods[0].SubPowerCollection[j - 1].To - rods[0].SubPowerCollection[j - 1].From;
                        //前一个节点
                        FluidData pre = channelsFlow[i].FluidDatas[j - 1];
                        //当前子通道燃料棒功率输出
                        double SubPowerJ = 0;
                        //遍历所有燃料棒
                        foreach (Rod rod in rods)
                        {
                            //燃料棒所有接触的通道
                            foreach (var ContactedChannel in rod.ContactedChannel)
                            {
                                //如果与燃料棒接触的通道,如果是当前正在计算的通道
                                if (ContactedChannel.Index == channels[i].Index)
                                {
                                    SubPowerJ += rod.SubPowerCollection[j - 1].Value * ContactedChannel.Angle / 360;
                                }
                            }
                        }
                        //乘以功率因子
                        SubPowerJ = SubPowerJ * powerFactor.Multiplier;
                        //计算新节点,NodeToNext计算子通道节点
                        FluidData next = NodeToNext(
                            coolent,
                            pre,
                            channels[i],
                            Lj,
                            SubPowerJ,
                            m[i],
                            acc,
                            out double DeltaPij,
                            chf_formula,
                            flow_direction);
                        //迭代中间变量赋值
                        DeltaP[i] = DeltaPij;
                        //存储计算结果
                        channelsFlow[i].FluidDatas[j] = next;
                        //i棒j段压降
                        P_Local[j, i] = next.Pressure;
                        //质量流速
                        MassFlowRate[j, i] = m[i];
                    }

                    //初始化迭代收敛因子
                    Sigma = 0;
                    //平均压降
                    double AvgPressure = 0;
                    for (int i = 0; i < Ni; i++)
                    {
                        AvgPressure += DeltaP[i];
                    }
                    //平均压降
                    AvgPressure = AvgPressure / Ni;
                    // 所有偏差之和
                    for (int i = 0; i < Ni; i++)
                    {
                        Sigma += Math.Abs(DeltaP[i] - AvgPressure);
                    }
                    //总质量流速
                    double TotalM = 0;
                    for (int i = 0; i < Ni; i++)
                    {

                        //子通道i压降和平均压降的比值
                        double Factor = (1 - DeltaP[i] / AvgPressure) * 0.1;
                        //重新分配压降
                        m[i] = m[i] + m[i] * Factor;

                        TotalM += m[i];

                        Debug.WriteLine(String.Format("因子{0}：", Factor));

                    }
                    //计算平衡后与平衡前的比值
                    double k = massFlow.MassVelocity / TotalM;
                    //保持总质量流速不变
                    for (int i = 0; i < Ni; i++)
                    {
                        //对质量流量进行修正
                        m[i] = k * m[i];
                        Debug.WriteLine(String.Format("通道{0}压降{1}Pa", i, DeltaP[i]));
                    }
                    Debug.WriteLine("=========================================");
                    //迭代压降
                    iteration_times += 1;
                    Main.MsgCenter.ShowMessage(String.Format("压力迭代次数：{0}", iteration_times));
                    if (iteration_times >iteration. MaxIteration)
                    {
                        Main.MsgCenter.ShowMessage(String.Format("超过最大迭代次数限制,最大迭代次数限制{0}",iteration.MaxIteration));
                        break;
                    }
                }
                while (Sigma > iteration.Sigma);
                //循环每个通道
                Main.MsgCenter.ShowMessage(String.Format("Sigma->{0}", Sigma));

            }
            //信息输出
            Main.MsgCenter.ShowMessage("--------压力场预览--------");
            Main.MsgCenter.ShowMessage(P_Local.ToMatrixString(Nj + 1, Ni));
            Main.MsgCenter.ShowMessage("--------流量场预览--------");
            Main.MsgCenter.ShowMessage(MassFlowRate.ToMatrixString(Nj + 1, Ni));

            return channelsFlow;
        }









        /// <summary>
        /// 计算燃料棒稳态温度场
        /// </summary>
        /// <param name="Nj">轴向分段数</param>
        /// <param name="Nk">燃料棒数</param>
        /// <param name="coolent">冷却剂</param>
        /// <param name="channels">通道结合</param>
        /// <param name="rods">燃料棒集合</param>
        /// <param name="rodTypes">燃料棒类型集合</param>
        /// <param name="materials">材料集合</param>
        /// <param name="channelsFlow">已经得到的子通道稳态流体计算结果</param>
        /// <param name="gasGap">气体间隙模型对象</param>
        /// <param name="options">计算选项集合</param>
        /// <returns></returns>
        public List<RodTemperature> Caculate_Rods_Temperature_Steady(
            int Nj,int Nk,
            Fluid coolent,
            List<Channel> channels,
            List<Rod> rods, 
            List<RodType> rodTypes, 
            List<Material> materials, 
            List<ChannelFlow> channelsFlow,
            GasGap gasGap,
            Options options)
        {

            Main.MsgCenter.ShowMessage("--------计算燃料棒温度场--------");
            //燃料棒温度集合 返回数据
            List<RodTemperature> RodsTemperature = new List<RodTemperature>();


            //包壳分段数
            var CladSegment = options.CladSegment;
            //芯块分段数
            var PelletSegment = options.PelletSegment;
            //功率因子
            var powerFactor = options.PowerFactor.Multiplier;
            //包壳功率份额
            var cladShare = options.PowerFactor.CladShare;
            //芯块功率份额
            var pelletShare = options.PowerFactor.PelletShare;
            //冷却剂中功率份额
            var fluidShare = options.PowerFactor.FluidShare;
            //计算结果准确度设置
            var acc = options.Precision;

            //燃料棒周围主流流体温度
            Matrix<double> Tf = Matrix<double>.Build.Dense(Nj, Nk, 0);
            //燃料棒周围主流流体对流换热系数
            Matrix<double> h = Matrix<double>.Build.Dense(Nj, Nk, 0);
            Matrix<double> massFlowDensity = Matrix<double>.Build.Dense(Nj, Nk, 0);
            //遍历所有燃料棒
            for (int k = 0; k < Nk; k++)
            {
                //新建一个用于存储一个燃料棒输出的对象
                RodTemperature RodkTemperature = new RodTemperature
                {
                    Index = rods[k].Index,
                    SubRods = new List<SubRodTemperature>(),
                };
                //径向温度节点数
                int size = CladSegment + PelletSegment + 2;
                //燃料棒k的温度场 矩阵
                Matrix<double> RodTField = Matrix<double>.Build.Dense(Nj, size, 0);
                //是否找到燃料类型
                bool isTypeFound = false;
                RodType rodType = new RodType();
                //找到燃料棒k的燃料棒类型
                foreach (RodType type in rodTypes)
                {
                    if (type.Index == rods[k].Type)
                    {
                        rodType = type;
                        //找到了燃料棒类型
                        isTypeFound = true;
                        break;
                    }
                }
                //如果未找到燃料棒类型
                if (!isTypeFound)
                {
                    Main.MsgCenter.ShowMessage(String.Format("燃料棒{0}未找到匹配的{1}燃料棒类型", k, rods[k].Type));
                }
                //寻找燃料棒固体材料数据
                Material Clad = new Material();
                Material Pellet = new Material();
                foreach (var material in materials)
                {
                    if (material.Index == rodType.CladMaterialIndex)
                    {
                        Clad = material;
                    }
                    else if (material.Index == rodType.PelletMaterialIndex)
                    {
                        Pellet = material;
                    }

                }
                //燃料棒直径
                double d_rod = rodType.Diameter;
                //燃料芯块直径
                double d_pellet = rodType.PelletDiameter;
                //燃料包壳厚度
                double clad_thickness = rodType.CladThickness;
                //气体间隙通过计算得出
                double gap_thickness = (d_rod - d_pellet) * 0.5 - clad_thickness;
                //分段计算燃料棒温度场
                for (int j = 0; j < Nj; j++)
                {
                    //分段长度
                    double Lj = rods[0].SubPowerCollection[j].To - rods[0].SubPowerCollection[j].From;
                    //接触的全部角度
                    double TotalAngle = 0;
                    double Xe = 0;
                    FluidData FluidJ;
                    //遍历与[燃料棒k] 接触的 子通道,找到流体外部边界条件
                    foreach (ContactedChannel EachContactedChannel in rods[k].ContactedChannel)
                    {
                        //与燃料棒k接触的所有子通道流体物性参数计算结果
                        ChannelFlow ChannelFlowOfContactChannel = new ChannelFlow();
                        //找到与燃料棒接触的子通道计算结果
                        foreach (ChannelFlow channelFlow in channelsFlow)
                        {
                            //通道数据 index和连接的通道 index相同
                            if (channelFlow.ChannelIndex == EachContactedChannel.Index)
                            {
                                ChannelFlowOfContactChannel = channelFlow;
                            }
                        }
                        FluidJ = ChannelFlowOfContactChannel.FluidDatas[j];
                        h[j, k] += FluidJ.h * EachContactedChannel.Angle;
                        Tf[j, k] += FluidJ.Temperature * EachContactedChannel.Angle;
                        Xe = FluidJ.Xe * EachContactedChannel.Angle;
                        //质量流密度
                        massFlowDensity[j, k] += FluidJ.Velocity * FluidJ.Density * EachContactedChannel.Angle;
                        //累加接触的角度份额
                        TotalAngle += EachContactedChannel.Angle;
                    }
                    //加权平均对流换热系数
                    h[j, k] = h[j, k] / TotalAngle;
                    //加权平均流体温度
                    Tf[j, k] = Tf[j, k] / TotalAngle;
                    //加权平均热平衡含气率
                    Xe = Xe / TotalAngle;
                    //加权平均质量流密度
                    massFlowDensity[j, k] = massFlowDensity[j, k] / TotalAngle;



                    //线性功率，单位W/M
                    double Linearpower = rods[k].SubPowerCollection[j].Value * powerFactor;
                    //体热源W/m3
                    double fi_pellet = Linearpower * pelletShare / (0.25 * PI * d_pellet * d_pellet);
                    //clad面积
                    double cladArea = 0.25 * PI * (d_rod * d_rod - (d_rod - 2 * clad_thickness) * (d_rod - 2 * clad_thickness));
                    //包壳 体热流密度
                    double fi_clad = Linearpower * cladShare / cladArea;
                    //包壳分段长度
                    double deltaR_clad = clad_thickness / CladSegment;
                    //芯块分段长度
                    double deltaR_pellet = d_pellet * 0.5 / PelletSegment;
                    //包壳外表面 - 热流密度
                    double q = Linearpower * (1 - fluidShare) / (PI * d_rod);
                    //包壳外表面 - 温度
                    double Tw = q / h[j, k] + Tf[j, k];
                    //内推温度场
                    RodTField[j, 0] = Tw;
                    //稳态,温度场由外向内内推
                    for (int layer = 0; layer < CladSegment; layer++)
                    {
                        //外径
                        double r_outside = 0.5 * d_rod - deltaR_clad * layer;
                        //内径
                        double r_inside = 0.5 * d_rod - deltaR_clad * (layer + 1);
                        //层平均半径
                        double r_av = (r_inside + r_outside) * 0.5;
                        //已经内推过的层面积
                        double layerArea = PI * (d_rod * d_rod * 0.25 - r_inside * r_inside);
                        //内推过的层发热线功率 
                        double layerHeat = layerArea * fi_clad;
                        //层导热热阻ln（d2/d1）/2π lamd l   Clad.K.Get(vectorT[layer]) 
                        double R_layer = Math.Log(r_outside / r_inside) / (2 * PI * Clad.GetK(RodTField[j, layer]) * Lj);
                        //内推节点      
                        RodTField[j, layer + 1] = (Linearpower - layerHeat) * Lj * R_layer + RodTField[j, layer];
                    }

                    //稳态下气体间隙传递的热流密度（导出热量等于芯块Pellet产热）
                    double q_gap = Linearpower * pelletShare / (PI * d_pellet);
                    //芯块外表面温度
                    RodTField[j, CladSegment + 1] = RodTField[j, CladSegment] + q_gap / gasGap.Get_h();
                    //计算燃料棒
                    for (int layer = 0; layer < PelletSegment; layer++)
                    {
                        //外径
                        double r_outside = 0.5 * d_pellet - deltaR_pellet * layer;
                        //内径
                        double r_inside = 0.5 * d_pellet - deltaR_pellet * (layer + 1);
                        //层平均半径
                        double r_av = (r_inside + r_outside) * 0.5;
                        //已经内推过的层面积
                        double layerArea = PI * (0.25 * d_pellet * d_pellet - r_inside * r_inside);
                        //层发热线功率
                        double layerHeat = layerArea * fi_pellet;
                        //层导热热阻ln（d2/d1）/2π*lamd*l   Clad.K.Get(vectorT[layer]) 
                        double R_layer = Math.Log(r_outside / r_inside) / (2 * PI * Clad.GetK(RodTField[j, layer]) * Lj);
                        RodTField[j, layer + CladSegment + 2] = (Linearpower * pelletShare - layerHeat) * Lj * R_layer + RodTField[j, layer + CladSegment + 1];

                    }
                    //芯块中心温度（根据有内热源传热方程）
                    RodTField[j, PelletSegment + CladSegment + 1] = RodTField[j, PelletSegment + CladSegment] + 0.25 * fi_pellet / Pellet.GetK(RodTField[j, PelletSegment + CladSegment]) * deltaR_pellet * deltaR_pellet;
                    //计算临界热流密度
                    double q_critical = Q_Critical(Xe, d_rod, massFlowDensity[j, k], coolent.GetHf(Tf[j, k]), coolent.GetH(Tf[j, k]), options.DNBR_Formula);
                    //设置输出对象（燃料棒k第j段）
                    SubRodTemperature subRodTemperature = new SubRodTemperature
                    {
                        Index = j,
                        //一些重要观测点温度
                        CladOutsideT = Math.Round(RodTField[j, 0], acc.T),
                        CladInsideT = Math.Round(RodTField[j, CladSegment], acc.T),
                        PelletOutsideT = Math.Round(RodTField[j, CladSegment + 1], acc.T),
                        PelletCenterT = Math.Round(RodTField[j, PelletSegment + CladSegment + 1], acc.T),
                        //对流换热系数
                        h = Math.Round(h[j, k], acc.h),
                        //热流密度
                        Q = Math.Round(q, 1),
                        //临界热流密度
                        Qc = Math.Round(q_critical, 1),
                        //DNBR
                        DNBR = Math.Round(q_critical/q, 3),
                        //温度向量
                        TemperatureVector = RodTField.Row(j),
                    };

                    //加入计算结果集合
                    RodkTemperature.SubRods.Add(subRodTemperature);

                }//---结束轴向J循环
                RodsTemperature.Add(RodkTemperature);
                //输出消息提示
                Main.MsgCenter.ShowMessage(String.Format("燃料棒{0}:", rods[k].Index));
                Main.MsgCenter.ShowMessage(RodTField.ToMatrixString(Nj, PelletSegment + CladSegment + 2));
            }//结束燃料棒k循环

            return RodsTemperature;
        }








        /// <summary>从稳态开始向后推算瞬态</summary>
        internal void Caculate_Transirent_From_Steady(List<InputTimer> InputTimers, List<ChannelFlow> channelsFlow)
        {
            //瞬态输出结果
            TransientResult transientResult = new TransientResult();
            //输出瞬态时间节点集合
            List<TransientTimer> transientTimers = new List<TransientTimer>();
            //根据时间节点计算
            for (int t = 0; t < InputTimers.Count; t++)
            {
                //质量流量乘子
                var massFlowMultiplier = InputTimers[t].MassFlowMultiplier;
                //功率乘子
                var powerMultiplier = InputTimers[t].PowerMultiplier;
                //绝对时间秒（非时间间隔）
                var time = InputTimers[t].Second;
                TransientTimer transientTimer = new TransientTimer
                {
                    Time = time,

                };

                //var ChannelsFlowNestTime = GetNextTimeChannelsFlow
                //    (
                //    channelsFlow,
                //    Rods,
                //    Coolent,
                //    Iteration.IterationType,
                //    powerMultiplier,
                //    massFlowMultiplier
                //    );



                //获得已经计算的初始时刻稳态计算结果
                //已知（上一个节点） 对流换热系数，燃料棒温度等。





            }









            //新的流量分配

            List<Matrix<double>> MatrixList = new List<Matrix<double>>();

            //int Nodes = CladNode + PelletNode;//总节点数
            ////芯块体内热源
            //double FI_Pellet = (Linearpower * PowerFactor* PelletShare)/(0.25*PI*d_pellet * d_pellet);
            //double d_CladInside = d_rod - CladThickness * 2;
            ////包壳体内热源
            //double FI_Clad = (Linearpower * PowerFactor *CladShare) / (0.25 * PI * (d_rod * d_rod- d_CladInside* d_CladInside));
            //double[] RightArray = new double[Nodes];//矩阵方程右侧向量
            //double[] ResultArray = new double[Nodes];//矩阵方程求解求解结果
            ////矩阵方程系数
            //double[,] Matrix = new double[Nodes, Nodes];
            //MatrixSolver Ms = new MatrixSolver(Matrix, RightArray);
            //ResultArray = Ms.Caculate();
        }
































        #endregion









    }
}
