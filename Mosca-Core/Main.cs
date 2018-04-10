//计算主程序>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
//说明：该函数方法必须使用IOManager输入后才可进行和计算
//创建于 2017-8-15;上次编辑2018-3-25
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
using MoscaCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using MoscaCore.Models;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace MoscaCore
{
    public class Main:FormulaBase
    {
        
        //管理输入和输出
        public IOManager MyIOManager = new IOManager();
        //消息输出
        public static IMsgCenter MsgCenter { get; set; }
        public Main()
        {
            //设置一个无消息输出的默认值
            MsgCenter = new TempMsgCenter();
        }
        public void RunAllSteps()
        {
            //读取输入参数
            BeginRecognize();
            //设置输出参数
            SetOutput();
            //计算平均通道
            CaculateGeneralFlow();
            //计算子通道
            CaculateChannelFlow();
            //计算稳态燃料棒温度场
            CaculateRodsTemperature();
            //计算瞬态
            if (MyIOManager.InputData.Options.Transient.Use == true)
            {
                RunTransirentFromSteady();
            }

        }






        #region Gloable全局变量
        //子通道数
        private int Ni;
        //燃料棒数
        private int Nk;
        //轴向分段数
        private int Nj;
        //总功率因子
        private double PowerFactor;
        //燃料芯块份额
        private double PelletShare;
        //包壳份额
        private double CladShare;
        //冷却剂份额
        private double FluidShare;
        //总的流通面积
        private double TotalArea =0;
        //总湿周 
        private double TotalLw =0;
        //总热周 
        private double TotalLh =0;
        //总等效水力直径
        private double TotalDi;
        //最大的压力迭代次数限制，默认20
        private int MaxIteration = 20;
        //燃料芯块结点划分
        private int PelletSegment;
        //燃料包壳结点划分
        private int CladSegment;
        //流体流动方向
        private double flow_direction;
        //CHF计算方式指示器
        private int chf_indicator;
        /////////////////////////////
        //输入的冷却剂模型
        private Fluid Coolent = new Fluid();
        //输入的充气间隙模型
        private GasGap GasGap;
        //输入的固体材料模型 集合
        private List<Material> Materials = new List<Material>();
        private List<RodType> RodTypes = new List<RodType>();
        //输入的通道数据模型 集合
        private List<Channel> Channels = new List<Channel>();
        //燃料棒数据模型 集合
        private List<Rod> Rods;
        //定位格架数据模型 集合
        private List<Grid> Grids;
        //计算的精确度
        private Precision Acc = new Precision();
        //入口流量模型
        private MassFlow MassFlow;
        //瞬态计算模型
        private Transient Transient;
        //输入的计算选项
        private Options options;
        //存储的稳态计算温度场(用于初始化瞬态计算)
        private List<Matrix<double>> RodsTField=new List<Matrix<double>>();
        #endregion

        #region 计算主程序
        /// <summary>
        /// 将输入数据和程序中变量做对应，需要存在输入文件后再调用
        /// 全局变量赋值，并简化表示方法，初步计算
        /// </summary>        
        public void BeginRecognize()
        {
            ////////////////////////////////////////////////////////////
            //主要对输入数据的表示方法进行简化处理,被赋值的变量均为[全局变量]
            //同时可对输入数据进行[简单的]计算
            //
            InputModel InputData = MyIOManager.InputData;
            //子通道对象
            Channels = InputData.ChannelCollection.Channels;
            //燃料棒对象
            Rods = InputData.RodCollection.Rods;
            //燃料棒类型集合
            RodTypes = InputData.RodTypes;
            //子通道数
            Ni = Channels.Count;
            //轴向分段数
            Nj = InputData.RodCollection.Segment;
            //燃料棒个数
            Nk = Rods.Count;
            //最大迭代次数
            MaxIteration = InputData.Options.MaxIteration;
            //功率因子
            PowerFactor = InputData.Options.PowerFactor.Multiplier;
            //燃料芯块功率份额
            PelletShare = InputData.Options.PowerFactor.PelletShare;
            //燃料包壳功率份额
            CladShare = InputData.Options.PowerFactor.CladShare;
            //流体中慢化功率份额
            FluidShare = InputData.Options.PowerFactor.FluidShare;
            //临界热流密度CHF计算公式选用
            chf_indicator = InputData.Options.DNBR_Formula;
            //冷却剂模型
            Coolent = InputData.MaterialCollection.Fluid;
            //气体间隙
            GasGap = InputData.MaterialCollection.GasGap;
            //固体材料集合
            Materials = InputData.MaterialCollection.Materials;
            //初始的流量数据模型
            MassFlow = InputData.MassFlow;
            //流体流动方向与垂直方向的夹角cosθ(向上为正;-1~1)
            flow_direction = MassFlow.Flow_Direction;
            //定位格架模型
            Grids = InputData.GridCollection.Grids;
            foreach (Channel Ch in Channels)
            {
                //计算总流通面积
                TotalArea += Ch.FlowArea;
                //计算总湿周
                TotalLw += Ch.WetPerimeter;
                TotalLh += Ch.HotPerimeter;
            }
            //计算总的等效直径
            TotalDi = 4 * TotalArea / TotalLw;
            //获取参数计算要求的精确度 i.e.参数小数点后保留位数
            Acc = InputData.Options.Precision;
            //读取瞬态设置
            Transient = InputData.Options.Transient;
            //计算选项
            options = InputData.Options;
            //包壳分段数(计算温度)
            CladSegment = options.CladSegment;
            //芯块分段数(计算温度)
            PelletSegment = options.PelletSegment;

            MsgCenter.ShowMessage("子通道数:" + Ni);
            MsgCenter.ShowMessage("轴向分段:" + Nj);
            MsgCenter.ShowMessage("燃料棒数:" + Nk);
            MsgCenter.ShowMessage("流动方向:" + flow_direction);
            MsgCenter.ShowMessage("是否计算瞬态:" + MyIOManager.InputData.Options.Transient.Use.ToString());

            //foreach (System.Reflection.PropertyInfo p in options.GetType().GetProperties())
            //{
            //    Console.WriteLine("Name:{0} Value:{1}", p.Name, p.GetValue(options));
            //}
        }

        /// <summary>设置输出对象，实例化</summary>
        public void SetOutput()
        {
            MsgCenter.ShowMessage("设置输出格式");
            MyIOManager.OutputData = new OutputModel
            {
                Title = MyIOManager.InputData.Title,
                SteadyResult = new Result
                {
                    //平均流
                    GeneralFlow = new GeneralFlow
                    {
                        FluidDatas = new List<FluidData>(),
                    },
                    //子通道流
                    ChannelsFlow = new List<ChannelFlow>(),
                    //燃料棒温度场
                    RodsTemperature = new List<RodTemperature>()
                    {

                    },
                }
            };
            if (MyIOManager.InputData.Options.Transient.Use==true)
            {
                //如果输入文件定义了瞬态价算
                MyIOManager.OutputData.TransientResult = new TransientResult
                {
                    TransientTimers=new List<TransientTimer>(),
                };
            }
            //子通道轴向迭代

        }



        /// <summary>计算稳态平均通道(单通道)</summary>
        public void CaculateGeneralFlow()
        {
            MsgCenter.ShowMessage("计算基本流动数据...");
            //存放计算结果的集合
            List<FluidData> fluid_datas = new List<FluidData>();
            
            //计算平均流,Nj为轴向分段数,节点为Nj+1个
            for (int j = 0; j < Nj+1; j++)
            {
                if (j == 0)
                {
                    //j=0为一个初始节点,即入口节点
                    FluidData init = new FluidData
                    {
                        //入口温度
                        Temperature = MassFlow.Temperature,
                        //入口位置
                        Position = 0.000,
                        //入口压力
                        Pressure = MassFlow.Pressure,
                        //入口质量流量
                        MassFlowRate = MassFlow.MassVelocity,
                    };
                    //入口比焓,根据入口温度和压力返回入口流体比焓
                    init.Enthalphy = Coolent.GetH(init.Temperature, init.Pressure);
                    init.Enthalphy = Math.Round(init.Enthalphy, Acc.H);
                    //入口冷却剂密度,根据温度和压力
                    init.Density = Coolent.GetDensity(init.Temperature, init.Pressure);
                    init.Density = Math.Round(init.Density, Acc.Density);
                    //入口冷却剂流速(m/s) V=m/(A·ρ)
                    init.Velocity = init.MassFlowRate / TotalArea / init.Density;
                    init.Velocity = Math.Round(init.Velocity, Acc.Velocity);
                    //导热系数
                    init.K = Coolent.GetK(init.Temperature, init.Pressure);
                    init.K = Math.Round(init.K, Acc.K);
                    //运动粘度Kv=μ/ρ
                    init.Kv = Coolent.GetKv(init.Temperature, init.Pressure);
                    init.Kv = Math.Round(init.Kv, Acc.Kv);
                    //雷诺数
                    init.Re = init.Velocity * TotalDi / init.Kv;
                    init.Re = Math.Round(init.Re, Acc.Re);
                    //普朗特数                                              
                    init.Pr = Coolent.GetPr(init.Temperature, init.Pressure);
                    init.Pr = Math.Round(init.Pr, Acc.Pr);
                    //对流换热系数
                    init.h = h_convect(init.Re, init.Pr, init.K, TotalDi);
                    init.h = Math.Round(init.h, Acc.h);
                    //将数据添加到计算结果集合
                    fluid_datas.Add(init);
                }
                //如果不是入口节点j!=0
                else
                {
                    //获得上一个节点数据Previous
                    FluidData pre = fluid_datas[j-1];
                    //计算第j个节点的数据(第j段,共J段),根据入口温度求出比焓
                    double Hin = Coolent.GetH(pre.Temperature, pre.Pressure);
                    //总线功率,累加所有燃料棒功率份额
                    double TotalSubPower = 0;
                    //遍历所有燃料棒
                    foreach (Rod rod in Rods)
                    {
                        //遍历所有与此燃料棒接触的子通道
                        foreach (var contact_chl in rod.ContactedChannel)
                        {
                            //总的线功率+=燃料功率*功率份额(因存在对称边界半根燃料棒计算的算例)
                            TotalSubPower += rod.SubPowerCollection[j-1].Value * contact_chl.Angle / 360;
                        }
                    }
                    //总线功率 X 功率因子
                    TotalSubPower = TotalSubPower * PowerFactor;
                    //获取当前段的长度,选择一个燃料棒进行计算
                    double Lj = Rods[0].SubPowerCollection[j-1].To - Rods[0].SubPowerCollection[j-1].From;
                    //新建-出口节点数据
                    FluidData next = new FluidData();
                    //出口比焓,根据功率计算出口焓值,因为比焓单位是kJ/kg,所以power(W/m)除以1000
                    double Hout = Hin + TotalSubPower / 1000 * Lj / pre.MassFlowRate;
                    next.Enthalphy = Math.Round(Hout, 3);
                    //出口位置百分比
                    next.Position = Math.Round((double)j/ Nj, 3);
                    //计算出口温度
                    double Tout = Coolent.GetT(next.Enthalphy, next.Pressure);
                    next.Temperature = Math.Round(Tout, Acc.T);
                    //质量流速
                    double massVelocity = MyIOManager.InputData.MassFlow.MassVelocity;
                    next.MassFlowRate= Math.Round(massVelocity, Acc.MassFlowRate);
                    //密度
                    next.Density = Math.Round(Coolent.GetDensity(next.Temperature, next.Pressure), Acc.Density);
                    //出口流速m/s
                    next.Velocity = Math.Round(next.MassFlowRate / TotalArea / next.Density, Acc.Velocity);
                    //求运动粘度
                    next.Kv = Coolent.GetKv(next.Temperature, next.Pressure);
                    next.Kv= Math.Round(next.Kv, Acc.Kv);
                    //雷诺数              
                    next.Re = next.Velocity * TotalDi / next.Kv;
                    next.Re = Math.Round(next.Re, Acc.Re);
                    //普朗特数
                    next.Pr = Coolent.GetPr(next.Temperature, next.Pressure);
                    next.Pr = Math.Round(next.Pr, Acc.Pr);
                    //导热系数
                    next.K = Coolent.GetK(next.Temperature, next.Pressure);
                    next.K = Math.Round(next.K, Acc.K);
                    //重力压降Pa
                    double Ph = next.Density * G * Lj * flow_direction;
                    //摩擦压降Pa
                    double Pf = FrictionFactor(next.Re) * Lj / TotalDi * next.Density * next.Velocity * next.Velocity / 2;
                    //加速压降Pa
                    double Pa = next.Density * (next.Velocity + pre.Velocity) / 2 * (next.Velocity - pre.Velocity);
                    //总压降 - Mpa
                    double deltaP = (Ph + Pf + Pa) * 0.000001;
                    //压降 - Mpa
                    next.Pressure = pre.Pressure + deltaP;
                    next.Pressure = Math.Round(next.Pressure,Acc.Pressure+6);
                    ///计算临界热流密度
                    //饱和液体比焓
                    double Hf = Coolent.GetHf(pre.Pressure);
                    //饱和蒸汽比焓
                    double Hg = Coolent.GetHg(pre.Pressure);
                    //热平衡寒气率
                    double Xe = (next.Enthalphy- Hf) / (Hg - Hf);
                    //计算临界热流密度
                    double Qc = Q_Critical(Xe, TotalDi, next.MassFlowRate, Hf, next.Enthalphy,chf_indicator);
                    //本地临界热流密度
                    double Ql = TotalSubPower / TotalLh;
                    //DNBR默认保留三位小数点
                    next.DNBR = Math.Round(Qc / Ql, 3);
                    //对流换热系数
                    next.h = h_convect(next.Re, next.Pr, next.K, TotalDi);
                    next.h = Math.Round(next.Re, Acc.h);

                    fluid_datas.Add(next);
                }

                MyIOManager.OutputData.SteadyResult.GeneralFlow = new GeneralFlow
                {
                    FluidDatas = fluid_datas,
                };
            }
        }

        ///<summary>计算子通道流场稳态,所说的稳态*不能*是已经发生膜态沸腾的状态</summary>
        public void CaculateChannelFlow()
        {
            ///使用压力迭代方法确定不同子通道之间的流量，此计算方法使用压力迭代确定
            ///一些局部变量
            MsgCenter.ShowMessage("计算子通道数据,流量计算方式压力迭代：进出口迭代...");
            //质量流量迭代
            double[] m = new double[Ni];
            //压降迭代,每个子通道的压降
            double[] DeltaP = new double[Ni];
            //每个子通道的分段压降,Ni行xNj列初值为0
            Matrix<double> P_Local = Matrix<double>.Build.Dense(Ni,Nj+1,0);
            //压降迭代因子
            double Sigma = 0;
            //新建输出对象
            List<ChannelFlow> ChannelFlows = new List<ChannelFlow>();
            //遍历所有子通道
            for (int i = 0; i < Ni; i++)
            {
                ChannelFlow channelFlow = new ChannelFlow
                {
                    //流动计算结果 编号与输入的子通道编号相对应
                    ChannelIndex = Channels[i].Index,
                    FluidDatas = new List<FluidData>()
                };
                for (int j = 0; j < Nj+1; j++)
                {
                    channelFlow.FluidDatas.Add(new FluidData());
                }
                ChannelFlows.Add(channelFlow);
                //质量流量 分配初值
                m[i] = MassFlow.MassVelocity * Channels[i].FlowArea / TotalArea;
            }

            
            int Iteration = 0;
            //迭代压降
            do
            {
                Iteration += 1;
                MsgCenter.ShowMessage(String.Format("压力迭代次数：{0}, 最大迭代次数限制{1}", Iteration, MaxIteration));
                if (Iteration > MaxIteration)
                {
                    MsgCenter.ShowMessage("超过最大迭代次数限制");
                    break;
                }

                //所有子通道
                for (int i = 0; i < Ni; i++)
                {
                    //初始化压降
                    DeltaP[i] = 0;
                    //新建子通道对象
                    ChannelFlow ChannelFlow = new ChannelFlow
                    {

                        ChannelIndex = Channels[i].Index,
                        FluidDatas = new List<FluidData>(),
                    };
                    //每个通道数据节点（0~Nj，共Nj+1个）
                    for (int j = 0; j < Nj + 1; j++)
                    {
                        //给入口节点赋初值
                        if (j == 0)
                        {
                            //第i个子通道
                            Channel channel = Channels[i];
                            //初始节点
                            FluidData InitNode = new FluidData
                            {
                                //入口温度
                                Temperature = MassFlow.Temperature,
                                //位置
                                Position = 0.000,
                                //入口压力
                                Pressure =MyIOManager.InputData.MassFlow.Pressure,
                                //入口质量流量(初始迭代根据面积平均分配)
                                MassFlowRate = m[i],
                            };
                            //比焓
                            InitNode.Enthalphy = Coolent.GetH(InitNode.Temperature, InitNode.Pressure);
                            InitNode.Enthalphy = Math.Round(InitNode.Enthalphy, Acc.H);
                            //密度
                            InitNode.Density = Coolent.GetDensity(InitNode.Temperature, InitNode.Pressure);
                            InitNode.Density = Math.Round(InitNode.Density, Acc.Density);
                            //流速m/s
                            InitNode.Velocity = Math.Round(InitNode.MassFlowRate / channel.FlowArea / InitNode.Density, Acc.Velocity);
                            //等效水力直径
                            double De = 4 * channel.FlowArea / channel.WetPerimeter;
                            //运动粘度
                            double Kv = Coolent.GetKv(InitNode.Temperature, InitNode.Pressure);
                            //雷诺数
                            InitNode.Re = InitNode.Velocity * De / Kv;
                            InitNode.Re = Math.Round(InitNode.Re, Acc.Re);
                            //普朗特数
                            InitNode.Pr = Coolent.GetPr(InitNode.Temperature, InitNode.Pressure);
                            InitNode.Pr = Math.Round(InitNode.Pr, Acc.Pr);
                            //导热系数
                            InitNode.K = Coolent.GetK(InitNode.Temperature, InitNode.Pressure);
                            InitNode.K = Math.Round(InitNode.K, Acc.K);
                            //对流换热系数
                            InitNode.h = h_convect(InitNode.Re, InitNode.Pr, InitNode.K, De);
                            InitNode.h = Math.Round(InitNode.h, Acc.h);
                            //初始化子通道数据节点加入
                            ChannelFlows[i].FluidDatas[j] = InitNode;
                            P_Local[i, j] = InitNode.Pressure;
                        }
                        //j!=0
                        else
                        {
                            //计算当前段的长度j>=1
                            double Lj = Rods[0].SubPowerCollection[j - 1].To - Rods[0].SubPowerCollection[j - 1].From;
                            //当前计算的子通道对象
                            Channel channel = Channels[i];
                            //前一个节点
                            FluidData pre = ChannelFlows[i].FluidDatas[j - 1];
                            //当前子通道燃料棒功率输出
                            double SubPowerJ = 0;
                            //遍历所有燃料棒
                            foreach (Rod rod in Rods)
                            {
                                //燃料棒所有接触的通道
                                foreach (var ContactedChannel in rod.ContactedChannel)
                                {
                                    //如果与燃料棒接触的通道,如果是当前正在计算的通道
                                    if (ContactedChannel.Index == channel.Index)
                                    {
                                        SubPowerJ += rod.SubPowerCollection[j - 1].Value * ContactedChannel.Angle / 360;
                                    }
                                }

                            }
                            //乘以功率因子
                            SubPowerJ = SubPowerJ * PowerFactor;
                            
                            //计算新节点,NodeToNext计算子通道节点
                            FluidData next = NodeToNext
                                (
                                Coolent, 
                                pre, 
                                channel, 
                                Lj, 
                                SubPowerJ, 
                                m[i], 
                                Acc,
                                out double DeltaPij,
                                chf_indicator,
                                flow_direction
                                );
                         
                            //存储计算结果
                            ChannelFlows[i].FluidDatas[j] = next;
                            //i棒j段压降
                            P_Local[i, j]= next.Pressure;
                            //压降用于迭代
                            DeltaP[i] += DeltaPij;
                            //Debug.WriteLine("通道压降:" + DeltaP[i]);
                        }

                    }
                }


                //初始化Sigma
                Sigma = 0;
                //平均压降
                double AvgPressure = 0;
                for (int i = 0; i < Ni; i++)
                {
                    //MsgCenter.ShowMessage(String.Format("通道{0}压降:{1}", i, DeltaP[i]));
                    AvgPressure += DeltaP[i];
                }
                //平均压降
                AvgPressure = AvgPressure / Ni;

                for (int i = 0; i < Ni; i++)
                {
                    Sigma += Math.Abs(DeltaP[i] - AvgPressure);// 所有偏差之和
                }
                double TotalM = 0;
                for (int i = 0; i < Ni; i++)
                {
                    //子通道i压降和平均压降的比值
                    double Factor = Math.Sqrt(AvgPressure/DeltaP[i]);
                    //重新分配压降
                    m[i] = Factor * m[i];// 所有偏差之和
                    //计算平衡后的总质量流速
                    TotalM += m[i];
                }
                //计算平衡后与平衡前的比值
                double k = MassFlow.MassVelocity / TotalM;
                //纠正后的总质量流速
                double TotalM2 = 0;
                //保持总质量流速不变
                for (int i = 0; i < Ni; i++)
                {
                    //对质量流量进行修正
                    m[i] = k * m[i];
                    TotalM2 += m[i];
                    MsgCenter.ShowMessage(String.Format("通道{0}质量流速:{1}", i, m[i]));
                }

                MsgCenter.ShowMessage(String.Format("Sigma->{0}", Sigma));
            }
            while (Sigma > 100);


            MyIOManager.OutputData.SteadyResult.ChannelsFlow = ChannelFlows;

            MsgCenter.ShowMessage("--------压力场预览--------");
            MsgCenter.ShowMessage(P_Local.ToMatrixString());//P_Local.ToMatrixString(5,15)

        }




        ///<summary>计算燃料棒温度场,需要先计算流量场数据</summary>
        public void CaculateRodsTemperature()
        {
            MsgCenter.ShowMessage("计算燃料棒温度场...");
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
                    Index=Rods[k].Index,
                    SubRodTemperature = new List<SubRodTemperature>(),
                };
                //径向温度节点数
                int size = CladSegment + PelletSegment + 2;
                //燃料棒k的温度场 矩阵
                Matrix<double> RodTField = Matrix<double>.Build.Dense(Nj, size, 0);
                //List<Vector> RodTfield = new List<Vector>();
                bool isTypeFound = false;
                RodType rodType = new RodType();
                //找到燃料棒k的燃料棒类型
                foreach (RodType type in RodTypes)
                {
                    if (type.Index == Rods[k].Type)
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
                    MsgCenter.ShowMessage(String.Format("燃料棒{0}未找到匹配的{1}燃料棒类型", k, Rods[k].Type));
                }
                //寻找燃料棒固体材料数据
                Material Clad = new Material();
                Material Pellet = new Material();
                foreach (var material in Materials)
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
                    //燃料k，节点j温度场向量
                    //var vectorT = new DenseVector(new double[size]);
                    //轴向分段燃料棒温度场
                    SubRodTemperature subRodTemperature = new SubRodTemperature();
                    //分段长度
                    double Lj = Rods[0].SubPowerCollection[j].To - Rods[0].SubPowerCollection[j].From;
                    //接触的全部角度
                    double TotalAngle = 0;
                    double Xe=0;
                    FluidData FluidJ;
                    //遍历与[燃料棒k] 接触的 子通道,找到流体外部边界条件
                    foreach (ContactedChannel EachContactedChannel in Rods[k].ContactedChannel)
                    {
                        //与燃料棒k接触的所有子通道流体物性参数计算结果
                        ChannelFlow ChannelFlowOfContactChannel = new ChannelFlow();
                        //找到与燃料棒接触的子通道计算结果
                        foreach (ChannelFlow channelFlow in MyIOManager.OutputData.SteadyResult.ChannelsFlow)
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
                        Xe= FluidJ.Xe * EachContactedChannel.Angle;
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
                    double Linearpower = Rods[k].SubPowerCollection[j].Value * PowerFactor;
                    //体热源W/m3
                    double fi_pellet = Linearpower * PelletShare / (0.25 * PI * d_pellet * d_pellet);
                    //clad面积
                    double cladArea = 0.25 * PI * (d_rod * d_rod - (d_rod - 2 * clad_thickness) * (d_rod - 2 * clad_thickness));
                    //包壳 体热流密度
                    double fi_clad = Linearpower * CladShare / cladArea;
                    //包壳分段长度
                    double deltaR_clad = clad_thickness / CladSegment;
                    //芯块分段长度
                    double deltaR_pellet = d_pellet * 0.5 / PelletSegment;
                    //包壳外表面 - 热流密度
                    double q = Linearpower * (1 - FluidShare) / (PI * d_rod);
                    //包壳外表面 - 温度
                    double Tw = q / h[j, k] + Tf[j, k];
                    //·····内推温度场

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
                        double R_layer = Math.Log(r_outside / r_inside) / (2 * PI * Clad.GetK(RodTField[j,layer]) * Lj);
                        //内推节点      
                        RodTField[j,layer + 1] = (Linearpower - layerHeat) * Lj * R_layer + RodTField[j, layer];
                    }
                    subRodTemperature.CladInsideT = RodTField[j, CladSegment];
                    //稳态下气体间隙传递的热流密度（导出热量等于芯块Pellet产热）
                    double q_gap = Linearpower * PelletShare / (PI * d_pellet);
                    //芯块外表面温度
                    RodTField[j, CladSegment +1] = RodTField[j, CladSegment] + q_gap / GasGap.Get_h();
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
                        double R_layer = Math.Log(r_outside / r_inside) / (2 * PI * Clad.GetK(RodTField[j,layer]) * Lj);
                        RodTField[j, layer + CladSegment + 2] = (Linearpower * PelletShare - layerHeat) * Lj * R_layer + RodTField[j, layer + CladSegment + 1];

                    }
                    //芯块中心温度（根据有内热源传热方程）
                    RodTField[j, PelletSegment + CladSegment + 1] = RodTField[j, PelletSegment + CladSegment] + 0.25 * fi_pellet / Pellet.GetK(RodTField[j, PelletSegment + CladSegment]) * deltaR_pellet * deltaR_pellet;
                    //设置输出对象
                    subRodTemperature.Index = j;
                    subRodTemperature.CladOutsideT = RodTField[j, 0];
                    subRodTemperature.CladInsideT = RodTField[j, CladSegment];
                    subRodTemperature.PelletOutsideT= RodTField[j, CladSegment + 1];
                    subRodTemperature.PelletCenterT= RodTField[j, PelletSegment + CladSegment + 1];
                    subRodTemperature.h = h[j, k];
                    subRodTemperature.Index = Rods[k].Index;
                    subRodTemperature.Q = q;
                    //临界热流密度
                    subRodTemperature.Qc = Q_Critical(Xe, d_rod, massFlowDensity[j, k], Coolent.GetHf(Tf[j, k]), Coolent.GetH(Tf[j, k]), chf_indicator);
                    //DNBR
                    subRodTemperature.DNBR = Math.Round(q / subRodTemperature.Qc, 3);
                    //加入计算结果集合
                    RodkTemperature.SubRodTemperature.Add(subRodTemperature);

                }//---结束轴向J循环

                //温度场矩阵加入输出对象//燃料棒k的温度场 矩阵Matrix<double> RodTField = Matrix<double>.Build.DenseOfColumnVectors(RodTfield);
                RodkTemperature.TemperatureField = RodTField;
                MyIOManager.OutputData.SteadyResult.RodsTemperature.Add(RodkTemperature);

                //输出消息提示
                MsgCenter.ShowMessage(String.Format("-----燃料棒{0}的温度场-----",Rods[k].Index));
                MsgCenter.ShowMessage(RodTField.ToMatrixString(Nj, PelletSegment + CladSegment + 2));
                //MsgCenter.ShowMessage(h.ToMatrixString());
                //MsgCenter.ShowMessage(Tf.ToMatrixString());
                //MsgCenter.ShowMessage(massFlowDensity.ToMatrixString());

            }//结束燃料棒k循环



        }




        private void RunTransirentFromSteady()
        {

        }

        #endregion




    }
}



















































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