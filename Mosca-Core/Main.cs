///--------------------------------------------------------
//Main.cs
//主计算流程
//创建:2017-8-15 宋仕钊
//修改:2018-4-13 宋仕钊
///---------------------------------------------------------
using System;
using System.Collections.Generic;
using MoscaCore.Helpper;
using MoscaCore.Models;
using MathNet.Numerics.LinearAlgebra;
using System.Diagnostics;

namespace MoscaCore
{
    /// <summary>主程序计算流程，该类继承FormulaBase的函数和变量,使用IOManager输入输出，使用MsgCenter消息输出</summary>
    public class Main : FormulaBase                                                                 
    {
        #region Gloable 全局变量
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
        private double TotalArea = 0;
        //总湿周 
        private double TotalLw = 0;
        //总热周 
        private double TotalLh = 0;
        //总等效水力直径
        private double TotalDi;
        //燃料芯块控制体积划分
        private int PelletSegment;
        //燃料包壳控制体积划分
        private int CladSegment;
        //流体流动方向
        private double Flow_Direction;
        //CHF计算方式指示器
        private CHF_Formula_Types CHF_formula;
        //迭代方式
        private Iteration Iteration;
        //最大的压力迭代次数限制，默认20
        private int MaxIteration = 20;
        //迭代收敛因子限制
        private double SigmaLimit=100;
        //***********************************
        //输入的冷却剂模型
        private Fluid Coolent = new Fluid();
        //输入的充气间隙模型
        private GasGap GasGap;
        //输入的固体材料模型 集合
        private List<Material> Materials;
        private List<RodType> RodTypes;
        //输入的通道数据模型 集合
        private List<Channel> Channels;
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
        private Options Options;
        //子通道输出数据对象
        List<ChannelFlow> ChannelsFlow = new List<ChannelFlow>();
        #endregion

        #region Property 属性
        //管理输入和输出
        public IOManager MyIOManager = new IOManager();
        //消息输出
        public static IMsgCenter MsgCenter { get; set; }
        #endregion

        #region MainProcess 主程序计算流程

        public Main()
        {
            //MsgCenter属性设置为一个无消息输出的默认值
            MsgCenter = new TempMsgCenter();
        }

        public void RunAllSteps()
        {
            //读取输入参数
            Recognize();
            //设置输出参数
            SetOutput();
            //计算平均通道
            Caculate_General_Flow();
            //计算稳态子通道
            Caculate_Channels_Steady();
            //计算稳态燃料棒温度场
            Caculate_Rods_Temperature_Steady();
            //计算瞬态
            Caculate_Transirent();
        }
 
        /// <summary>将输入数据和程序中变量做对应，需要存在输入文件后再调用,全局变量赋值，并简化表示方法，初步计算</summary>        
        public void Recognize()
        {
            ///························································
            //主要对输入数据的表示方法进行简化处理,被赋值的变量均为[全局变量]
            //同时可对输入数据进行[简单的]计算
            ///·························································
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
            //功率因子
            PowerFactor = InputData.Options.PowerFactor.Multiplier;
            //燃料芯块功率份额
            PelletShare = InputData.Options.PowerFactor.PelletShare;
            //燃料包壳功率份额
            CladShare = InputData.Options.PowerFactor.CladShare;
            //流体中慢化功率份额
            FluidShare = InputData.Options.PowerFactor.FluidShare;
            //临界热流密度CHF计算公式选用
            CHF_formula = InputData.Options.DNBR_Formula;
            //冷却剂模型
            Coolent = InputData.MaterialCollection.Fluid;
            //气体间隙
            GasGap = InputData.MaterialCollection.GasGap;
            //固体材料集合
            Materials = InputData.MaterialCollection.Materials;
            //初始的流量数据模型
            MassFlow = InputData.MassFlow;
            //流体流动方向与垂直方向的夹角cosθ(向上为正;-1~1)
            Flow_Direction = MassFlow.Flow_Direction;
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
            Options = InputData.Options;
            //迭代控制
            Iteration = Options.Iteration;
            //最大迭代次数
            MaxIteration = Iteration.MaxIteration;
            //迭代收敛因子
            SigmaLimit = Iteration.Sigma;
            //包壳分段数(计算温度)
            CladSegment = Options.CladSegment;
            //芯块分段数(计算温度)
            PelletSegment = Options.PelletSegment;
            //消息输出
            MsgCenter.ShowMessage("子通道数:" + Ni);
            MsgCenter.ShowMessage("轴向分段:" + Nj);
            MsgCenter.ShowMessage("燃料棒数:" + Nk);
            MsgCenter.ShowMessage("流动方向:" + Flow_Direction);
            MsgCenter.ShowMessage("迭代方式:" + Iteration.IterationType.ToString());
            MsgCenter.ShowMessage("是否计算瞬态:" + MyIOManager.InputData.Options.Transient.Use.ToString());
        }

        ///<summary>实例化输出对象</summary>
        public void SetOutput()
        {
            MsgCenter.ShowMessage("设置输出格式");
            MyIOManager.OutputData = new OutputModel
            {
                Title = MyIOManager.InputData.Title,
                SteadyResult = new Result(),

            };

            if (MyIOManager.InputData.Options.Transient.Use == true)
            {
                //如果输入中定义了瞬态,实例化瞬态计算输出对象
                MyIOManager.OutputData.TransientResult = new TransientResult();
            }

        }
 
        /// <summary>计算稳态平均通道(单通道)</summary>
        public void Caculate_General_Flow()
        {
            MsgCenter.ShowMessage("计算基本流动数据...");
            //存放计算结果的集合
            List<FluidData> fluid_datas = new List<FluidData>();
            //平均通道
            Channel ch = new Channel
            {
                FlowArea = TotalArea,
                HotPerimeter = TotalLh,
                WetPerimeter = TotalLw,
            };
            //初始化子通道入口数据节点
            FluidData InitNode = SetInitNode(Coolent, TotalArea, ch, MassFlow, Acc);
            //初始节点添加到计算结果集合
            fluid_datas.Add(InitNode);
            //计算平均流,Nj为轴向分段数,循环1~Nj个节点（外加初始节点1个共Nj+1个）
            for (int j = 1; j < Nj + 1; j++)
            {
                //获得上一个节点数据Previous
                FluidData pre = fluid_datas[j - 1];
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
                        TotalSubPower += rod.SubPowerCollection[j - 1].Value * contact_chl.Angle / 360;
                    }
                }
                //总线功率 X 功率因子
                TotalSubPower = TotalSubPower * PowerFactor;
                //获取当前段的长度,选择一个燃料棒进行计算
                double Lj = Rods[0].SubPowerCollection[j - 1].To - Rods[0].SubPowerCollection[j - 1].From;
                //推算下一个节点
                FluidData next = NodeToNext(
                    Coolent,
                    pre,
                    ch,
                    Lj,
                    TotalSubPower,
                    pre.MassFlowRate,
                    Acc,
                    out double P,
                    CHF_formula,
                    Flow_Direction);
                //节点添加到集合
                fluid_datas.Add(next);
                //将集合给MyIOManager
                MyIOManager.OutputData.SteadyResult.GeneralFlow = new GeneralFlow
                {
                    FluidDatas = fluid_datas,
                };
            }
        }

        ///<summary>计算子通道流场稳态,压力迭代方式为进出口迭代</summary>
        public void Caculate_Channels_Steady()
        {
            var channelsFlow = new List<ChannelFlow>();
            //根据迭代方式调用不同的方法计算子通道数据
            switch (Iteration.IterationType)
            {
                case IterationTypes.IOIteration:
                    //获得通道流体信息集合
                    channelsFlow = Caculate_Channels_Steady_IOIteration(Coolent,Channels,Rods,MassFlow,Ni,Nj,TotalArea,Options);
                    //添加到输出对象
                    MyIOManager.OutputData.SteadyResult.ChannelsFlow = channelsFlow;
                    break;
                case IterationTypes.NodeIteration:
                    //获得通道流体信息集合
                    channelsFlow = Caculate_Channels_Steady_NodeIteration(Coolent, Channels, Rods, MassFlow, Ni, Nj, TotalArea, Options);
                    //添加到输出对象
                    MyIOManager.OutputData.SteadyResult.ChannelsFlow = channelsFlow;
                    break;
                case IterationTypes.FieldIteration:
                    //暂时留空
                    DoNothing();
                    break;
                default:
                    //CaculateChannelFlow();
                    break;
            }





           
        }
 
        ///<summary>计算燃料棒温度场,需要先计算流量场数据</summary>
        public void Caculate_Rods_Temperature_Steady()
        {
            //燃料棒温度集合
            var rodsTemperature=Caculate_Rods_Temperature_Steady(
                Nj,Nk,
                Coolent,
                Channels,
                Rods,
                RodTypes,
                Materials,
                MyIOManager.OutputData.SteadyResult.ChannelsFlow,
                GasGap,
                Options);
            MyIOManager.OutputData.SteadyResult.RodTemperature = rodsTemperature;
        }
 
        /// <summary>从稳态开始向后推算瞬态</summary>
        private void Caculate_Transirent()
        {

            if (MyIOManager.InputData.Options.Transient.Use == true)
            {
                //输入的瞬态时间变化设置
                var timers = MyIOManager.InputData.Options.Transient.Timers;
                //稳态流体计算结果
                var steady_Channels_Flow = MyIOManager.OutputData.SteadyResult.ChannelsFlow;
                //从稳态开始计算瞬态
                Caculate_Transirent_From_Steady(timers, steady_Channels_Flow);
            }
            else
            {
                DoNothing();
            }
        }

        #endregion

    }
}


