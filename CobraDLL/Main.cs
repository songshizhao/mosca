//计算主程序>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
//说明：该函数方法必须反序列化InputModel InputData对象后
//作为输入，才可以进行实际计算。
//FIRST CREATED BY SONGSHIZHAO @ 2017年8月15日
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
using CobraDLL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using CobraDLL.Models;


namespace CobraDLL
{
    public class Main:Formula
    {
        public Main() { }

        /// <summary>输入文件模型 </summary>
        public InputModel InputData { get; set; }
        /// <summary>输出文件模型</summary>
        public OutputModel OutputData { get; set; }
        /// <summary>输入文件xml文本</summary>
        public string InputXmlString { get; set; }
        /// <summary>输出文件xml文本</summary>
        public string OutputXmlString { get; set; }

        /// 数据输入,反序列化xml为对象
        public string FileInput(string XmlString)
        {
            string InputResult = "";
            using (MemoryStream MS = new MemoryStream(Encoding.UTF8.GetBytes(XmlString)))
            {
                using (XmlReader xr = XmlReader.Create(MS))
                {
                    XmlSerializer xmlSearializer = new XmlSerializer(typeof(InputModel));

                    InputData = (InputModel)xmlSearializer.Deserialize(xr);
                    InputResult = "Read XML Success";
                }
            }

            return InputResult;            
        }


        /// 数据输出，序列化xml文本 
        public string FileOutput()
        {

            using (MemoryStream ms = new MemoryStream())
            {
                var setting = new XmlWriterSettings()
                {
                    Encoding = new UTF8Encoding(false),
                    Indent = true,
                };

                using (XmlWriter writer = XmlWriter.Create(ms, setting))
                {

                    XmlSerializer xmlSearializer = new XmlSerializer(typeof(OutputModel));
                    xmlSearializer.Serialize(writer, OutputData);
                    OutputXmlString = Encoding.UTF8.GetString(ms.ToArray());
                }

            }

            return OutputXmlString;
        }






        #region Gloable全局

        double G = 9.8;//重力加速度
        int Ni;//子通道数
        int Nj;//轴向分段数</summary>
        int Nk;//燃料棒数
        const double PI = 3.1416;//圆周率π=3.1416
        public static long Range = 97;//默认水物性参数 IF97 或者 IF67 默认=97

        double PowerFactor = 1;//总功率因子
        double PelletShare = 1;//燃料芯块份额
        double CladShare = 0;//包壳份额
        double FluidShare = 0;//冷却剂份额


        double TotalArea = 0;//总的流通面积
        double TotalLw = 0;//总等效湿周 
        double Di;//等效水力直径

 
        List<Channel> Channels = new List<Channel>();//输入的通道数据模型
        Fluid Coolent = new Fluid();//输入的冷却剂
        GasGap GasGap = new GasGap();//输入的充气间隙模型
        List<Material> Materials = new List<Material>();//
        List<RodType> RodTypes = new List<RodType>();//


        Precision acc=new Precision();
        #endregion









        /// <summary>将输入数据和程序中变量做进一步对应，数据简单分析，并简化表示方法，初步计算</summary>
        public void BeginRecognize()
        {
            //************************************************************
            //在这个方法中主要进行数据的识别和初步匹配                     *
            //不要在此方法中 声明任何变量或者对象                          *
            //被赋值的所有变量都属于全局变量                              *
            //这些变量或者对象已经在外部进行了定义和赋值                    *
            //可以在此方法中对数据进行简单的整理                          *
            //可以使用循环，但不应该存在*大量*数据运算                     *
            //************************************************************

            Channels = InputData.ChannelCollection.Channels;//子通道对象
            Ni = Channels.Count; //子通道数
            Nj = InputData.RodCollection.Segment; //轴向分段数
            Nk = InputData.RodCollection.Rods.Count;//燃料棒个数
 
            PowerFactor = InputData.Options.PowerFactor.Value;//功率因子
            PelletShare = InputData.Options.PowerFactor.PelletShare;//燃料芯块功率份额
            CladShare = InputData.Options.PowerFactor.CladShare;//燃料包壳功率份额
            FluidShare = InputData.Options.PowerFactor.FluidShare;//流体中慢化功率份额

            Coolent = InputData.MaterialCollection.Fluid;//冷却剂
            GasGap = InputData.MaterialCollection.GasGap;//气体间隙
            Materials = InputData.MaterialCollection.Materials;//固体材料结合
            RodTypes = InputData.RodTypes;//燃料棒类型集合

 
            foreach (Channel CH in Channels)
            {
                TotalArea += CH.FlowArea;
                TotalLw += CH.WetPerimeter;
            }
            Di = 4 * TotalArea / TotalLw; //总的等效直径

            Precision acc = InputData.Options.Precision;//计算要求的精确度

        }



        /// <summary>设置输出对象，实例化</summary>
        public void SetOutput()
        {
            this.OutputData = new OutputModel
            {
                Title = InputData.Title.Value,
                //Info = InputData.Infos,
                //平均流
                GeneralFlow = new GeneralFlow
                {
                    FluidDatas = new List<FluidData>(),
                },
                //子通道流
                ChannelFlows = new ChannelFlowCollection
                {
                    ChannelFlow = new List<ChannelFlow>(),
                },
                //燃料棒温度场
                RodsTemperature = new RodsTemperature {
                    RodTemperature = new List<RodTemperature>(),
                },
            };
        }



        /// <summary>计算平均流-稳态</summary>
        public void CaculateGeneralFlow()
        {
            //计算平均流
            for (int j = 0; j < Nj; j++)
            {
                if (j == 0)
                {
                    //初始化入口节点数据，只有j=0时才进行
                    FluidData Initial = new FluidData
                    {
                        Temperature = InputData.MassFlow.Temperature,//入口温度
                        Position = 0.000,
                        Pressure = InputData.MassFlow.Pressure,//入口压力
                        MassFlowRate = InputData.MassFlow.MassVelocity,

                    };
                    //入口比焓
                    Initial.Enthalphy = Coolent.GetH(Initial.Temperature, Initial.Pressure);
                    Initial.Enthalphy = Math.Round(Initial.Enthalphy, acc.H);
                    //入口冷却剂密度
                    Initial.Density = Coolent.GetDensity(Initial.Temperature, Initial.Pressure);
                    Initial.Density = Math.Round(Initial.Density, acc.Density);
                    //入口冷却剂流速
                    Initial.Velocity = Initial.MassFlowRate / TotalArea / Initial.Density;
                    Initial.Velocity = Math.Round(Initial.Velocity, acc.Velocity);
                    //导热系数
                    Initial.K = Coolent.GetK(Initial.Temperature, Initial.Pressure);
                    Initial.K = Math.Round(Initial.K, acc.K);
                    //动力粘度
                    double Kv = Coolent.GetK_Viscosity(Initial.Temperature, Initial.Pressure);
                    //雷诺数
                    Initial.Re = Initial.Velocity * Di / Kv;
                    Initial.Re = Math.Round(Initial.Re, acc.Re);
                    //普朗特数                                              
                    Initial.Pr = Coolent.GetPr(Initial.Temperature, Initial.Pressure);
                    Initial.Pr = Math.Round(Initial.Pr, acc.Pr);
                    //对流换热系数
                    Initial.h = h_convect(Initial.Re, Initial.Pr, Initial.K, Di);
                    Initial.h = Math.Round(Initial.h, acc.h);
                    //将入口初始数据添加到输出
                    OutputData.GeneralFlow.FluidDatas.Add(Initial);
                }
                //获得上一个节点数据               
                FluidData Previous = OutputData.GeneralFlow.FluidDatas[j];
                //根据入口温度求出比焓
                double Hin = Coolent.GetH(Previous.Temperature, Previous.Pressure);

                //总分段的-线功率
                double TotalSubPower = 0;
                foreach (Rod rod in InputData.RodCollection.Rods)
                {
                    foreach (var ContactedChannel in rod.ContactedChannel)
                    {
                        TotalSubPower += rod.SubPowerCollection[j].Value * ContactedChannel.Angle / 360;
                    }
                }
                //总线功率 X 功率因子
                TotalSubPower = TotalSubPower * PowerFactor;
                //获取当前段的长度
                double Ln = InputData.RodCollection.Rods[0].SubPowerCollection[j].To - InputData.RodCollection.Rods[0].SubPowerCollection[j].From;//计算当前段的高度

                //新建-出口节点数据
                FluidData Next = new FluidData();
                //根据功率计算出口焓值
                double Hout = Hin + TotalSubPower / 1000 * Ln / Previous.MassFlowRate;
                //出口比焓
                Next.Enthalphy = Math.Round(Hout, 3);
                //出口位置百分比
                Next.Position = Math.Round((double)(j + 1) / Nj, 3);
                // 先预定为上一个节点的压力
                Next.Pressure = Previous.Pressure;
                //计算出口温度
                double Tout = Coolent.GetT(Next.Enthalphy, Next.Pressure);

                //出口温度
                Next.Temperature = Math.Round(Tout, acc.T);

                //质量流速
                Next.MassFlowRate = InputData.MassFlow.MassVelocity;
                //密度

                Next.Density = Math.Round(Coolent.GetDensity(Next.Temperature, Next.Pressure), acc.Density);

                //出口流速
                Next.Velocity = Math.Round(Next.MassFlowRate / TotalArea / Next.Density, acc.Velocity);
                //重力压降
                double Ph = Next.Density * G * Ln * InputData.MassFlow.Flow_Direction;
                //求运动粘度
                double U = Coolent.GetK_Viscosity(Next.Temperature, Next.Pressure);

                //雷诺数              
                Next.Re = Next.Velocity * Di / U;
                Next.Re = Math.Round(Next.Re, 0);

                Next.Pr = Coolent.GetPr(Next.Temperature, Next.Pressure);
                Next.Pr = Math.Round(Next.Pr, acc.Pr);

                Next.K = Coolent.GetK(Next.Temperature, Next.Pressure);
                Next.K = Math.Round(Next.K, acc.K);

                Next.h = h_convect(Next.Re, Next.Pr, Next.K, Di);
                Next.h = Math.Round(Next.Re, acc.h);

                Debug.WriteLine("h->" + Next.h);


                //摩擦压降 - Pa
                double Pf = FrictionFactor(Next.Re) * Ln / Di * Next.Density * Next.Velocity * Next.Velocity / 2;
                //加速压降 - Pa
                double Pa = Next.Density * (Next.Velocity + Previous.Velocity) / 2 * (Next.Velocity - Previous.Velocity);
                //总压降 - Mpa
                double deltaP = (Ph + Pf + Pa) * 0.000001;
                //压降 - Mpa
                Next.Pressure = Previous.Pressure + deltaP;
                Next.Pressure = Math.Round(Next.Pressure, 6);

                OutputData.GeneralFlow.FluidDatas.Add(Next);

            }
        }


#if DEBUG
#endif


        ///<summary>计算子通道流场-稳态</summary>
        public void CaculateChannelFlow()
        {
            //质量流量迭代
            double[] m = new double[Ni];
            //压降迭代
            double[] DeltaP = new double[Ni];

            for (int i = 0; i < Ni; i++)//新建输出对象
            {
                ChannelFlow SubChannel = new ChannelFlow();//新建子通道对象
                SubChannel.ChannelIndex = InputData.ChannelCollection.Channels[i].Index;
                OutputData.ChannelFlows.ChannelFlow.Add(SubChannel);
                OutputData.ChannelFlows.ChannelFlow[i].FluidDatas = new List<FluidData>();
                for (int j = 0; j < Nj+1; j++)
                {
                    OutputData.ChannelFlows.ChannelFlow[i].FluidDatas.Add(new FluidData());
                }
            }

            //新建output输出燃料棒对象
      
            for (int k = 0; k < Nk; k++)
            {

                RodTemperature rt = new RodTemperature
                {
                    SubRodTemperature = new List<SubRodTemperature>()
                };
                OutputData.RodsTemperature.RodTemperature.Add(rt);
            }
 
            //子通道轴向迭代
            for (int j = 0; j < Nj; j++)
            {

                double Sigma = 0;
                //给入口节点初值
                if (j == 0)
                {
                    for (int i = 0; i < Ni; i++)
                    {
                        Channel ThisChannel = Channels[i];
                        m[i] = InputData.MassFlow.MassVelocity * ThisChannel.FlowArea / TotalArea;
                        FluidData Initial = new FluidData
                        {
                            Temperature = InputData.MassFlow.Temperature,//入口温度
                            Position = 0.000,
                            Pressure = InputData.MassFlow.Pressure,//入口压力
                            MassFlowRate = m[i],
                        };

                        Initial.Enthalphy = Coolent.GetH( Initial.Temperature,Initial.Pressure);
                        Initial.Enthalphy = Math.Round(Initial.Enthalphy, acc.H);
                         
                        Initial.Density = Coolent.GetDensity(Initial.Temperature,Initial.Pressure);
                        Initial.Density= Math.Round(Initial.Density, acc.Density);


                        Initial.Velocity = Math.Round(Initial.MassFlowRate /ThisChannel.FlowArea/ Initial.Density, acc.Velocity);
                        //计算对流换热系数
                        double De = 4 * ThisChannel.FlowArea / ThisChannel.WetPerimeter;//等效水力直径

                        double U = Coolent.GetK_Viscosity(Initial.Temperature, Initial.Pressure);
                        //雷诺数
                        Initial.Re = Initial.Velocity * De / U;
                        Initial.Re = Math.Round(Initial.Re,acc.Re);

                        Initial.Pr = Coolent.GetPr(Initial.Temperature, Initial.Pressure);
                        Initial.Pr = Math.Round(Initial.Pr, acc.Pr);
 
                        Initial.K= Coolent.GetK(Initial.Temperature, Initial.Pressure);
                        Initial.K = Math.Round(Initial.K,acc.K);

                        Initial.h = h_convect(Initial.Re, Initial.Pr, Initial.K, De);
                        Initial.h = Math.Round(Initial.h,acc.h);
 
                        OutputData.ChannelFlows.ChannelFlow[i].FluidDatas[j]=Initial;//初始化子通道数据
                    }
                }
                double Ln = InputData.RodCollection.Rods[0].SubPowerCollection[j].To - InputData.RodCollection.Rods[0].SubPowerCollection[j].From;//计算当前段的高度

                do
                {
                    //子通道计算
                    for (int i = 0; i < Ni; i++)
                    {
                        Channel ThisChannel =Channels[i];//当前计算的子通道对象
                        FluidData Previous = OutputData.ChannelFlows.ChannelFlow[i].FluidDatas[j];//获得上一个节点数据
 
                        double Hin = Coolent.GetH(Previous.Temperature, Previous.Pressure);

                        double SubPower = 0;
                        //当前子通道燃料棒功率输出
                        foreach (Rod rod in InputData.RodCollection.Rods)
                        {
                            foreach (var ContactedChannel in rod.ContactedChannel)
                            {
                                if (ContactedChannel.Index == ThisChannel.Index)
                                {
                                    SubPower += rod.SubPowerCollection[j].Value * ContactedChannel.Angle / 360;
                                }
                            }

                        }


                        double De = 4 * ThisChannel.FlowArea / ThisChannel.WetPerimeter;//等效水力直径
                        //新建-出口节点数据
                        FluidData Next = new FluidData();
                        //根据功率计算出口焓值
                        double Hout = Hin + SubPower * 0.001 * Ln / Previous.MassFlowRate;
                        //出口比焓
                        Next.Enthalphy = Math.Round(Hout,acc.H);
                        //出口位置百分比
                        Next.Position = Math.Round((double)(j + 1) / Nj, 3);
                        // 先预定为上一个节点的压力
                        Next.Pressure = Previous.Pressure;
                        
                        //出口温度
                        Next.Temperature = Coolent.GetT(Next.Enthalphy, Next.Pressure);
                        Next.Temperature = Math.Round(Next.Temperature, acc.T);
                        
                        //质量流速
                        Next.MassFlowRate = m[i];//  Math.Round(m[i], PrecisionMassFlowRate);//
 
                        Next.Density = Math.Round(Coolent.GetDensity(Next.Temperature, Next.Pressure),acc.Density); //密度
                        Next.Velocity = Math.Round( Next.MassFlowRate / ThisChannel.FlowArea / Next.Density, acc.Velocity); //出口流速

                        double Ph = Next.Density * G * Ln * InputData.MassFlow.Flow_Direction;//重力压降
                         
                        double U = Coolent.GetK_Viscosity(Next.Temperature, Next.Pressure);
                        //雷诺数
                        Next.Re= Next.Velocity * De / U;
                        Next.Re = Math.Round(Next.Re,acc.Re);
                        
                        Next.Pr = Coolent.GetPr( Next.Temperature, Next.Pressure);
                        Next.Pr = Math.Round(Next.Pr, acc.Pr);

                        Next.K= Coolent.GetK(Next.Temperature, Next.Pressure);
                        Next.K = Math.Round(Next.K,acc.K);

                        Next.h = h_convect(Next.Re, Next.Pr, Next.K, De);
                        Next.h = Math.Round(Next.h,acc.h);


                        double Pf = FrictionFactor(Next.Re) * Ln / De * Next.Density * Next.Velocity * Next.Velocity / 2;
                        double Pa = Next.Density * (Next.Velocity + Previous.Velocity) / 2 * (Next.Velocity-Previous.Velocity);
                        double P = (Ph + Pf + Pa) ;
                        Next.Pressure = Previous.Pressure + P * 0.000001;
                        Next.Pressure = Math.Round(Next.Pressure,6);

                        OutputData.ChannelFlows.ChannelFlow[i].FluidDatas[j+1]=Next;

                        DeltaP[i]= P;
                    }

                    //=====================================分段压力迭代
                    Sigma = 0;
                    double AvgPressure = 0;//平均压力
                    for (int i = 0; i < Ni; i++)
                    {
                        AvgPressure += DeltaP[i];// 所有偏差之和
                        //Debug.WriteLine("子通道"+ i+"压降："+DeltaP[i]);
                    }
                    AvgPressure = AvgPressure / Ni;
                    //Debug.WriteLine(AvgPressure + "平均压降");
                    for (int i = 0; i < Ni; i++)
                    {
                        Sigma += Math.Abs(DeltaP[i] - AvgPressure);// 所有偏差之和
                    }
                    double TotalM = 0;
                    for (int i = 0; i < Ni; i++)
                    {
                        double Factor = (AvgPressure - DeltaP[i]) / AvgPressure*0.01;

                        m[i] =m[i] + Factor * m[i];// 所有偏差之和

                        TotalM += m[i];
                    }
                    
                    double k = InputData.MassFlow.MassVelocity/ TotalM;
                    for (int i = 0; i < Ni; i++)
                    {
                        m[i] = k * m[i];//对质量流量进行修正
                        Debug.WriteLine( "子通道" + i+"流量"+m[i]);
                    }
                    Debug.WriteLine("Sigma->" + Sigma);
                }
                while (Sigma>1000);
            }


            
        }




        ///<summary>计算燃料棒温度场-稳态</summary>
        public void CaculateRodsTemperature() 
        {
            //燃料棒温度场计算
            for (int j = 0; j < Nj; j++)
            {
                double Ln = InputData.RodCollection.Rods[0].SubPowerCollection[j].To - InputData.RodCollection.Rods[0].SubPowerCollection[j].From;//计算当前段的高度
                //i是燃料棒
                for (int i = 0; i < InputData.RodCollection.Rods.Count; i++)
                {
                    double h = 0;//燃料棒i周围的，加权平均对流换热系数
                    double Tf = 0;
                    double TotalAngle = 0;
                    double G1 = 0;
                    //传热计算
                    int rodtype=InputData.RodCollection.Rods[i].Type;
                    

                    double d_rod = InputData.RodTypes[rodtype].Diameter;
                    double d_pellet = InputData.RodTypes[rodtype].PelletDiameter;
                    double CladThickness = InputData.RodTypes[rodtype].CladThickness;
                    double GapThickness = (d_rod - d_pellet) * 0.5 - CladThickness;


                    int CladMaterialIndex = InputData.RodTypes[rodtype].CladMaterialIndex;
                    int PelletMaterialIndex = InputData.RodTypes[rodtype].PelletMaterialIndex;

                    Material Clad = InputData.MaterialCollection.Materials[CladMaterialIndex];
                    Material Pellet = InputData.MaterialCollection.Materials[PelletMaterialIndex];

                    //遍历 与 [燃料棒i] 接触的 子通道
                    foreach (ContactedChannel item in InputData.RodCollection.Rods[i].ContactedChannel)
                    {
                        ChannelFlow OneContactChannel = new ChannelFlow();
                        foreach (ChannelFlow cf in OutputData.ChannelFlows.ChannelFlow)
                        {
                            if (cf.ChannelIndex == item.Index)
                            {
                                OneContactChannel = cf;
                            }
                        }

                        double h_channel = OneContactChannel.FluidDatas[j].h;

                        h += h_channel * item.Angle;
                        Tf += OneContactChannel.FluidDatas[j].Temperature * item.Angle;
                        TotalAngle += item.Angle;//累加接触的角度份额
                        G1 += OneContactChannel.FluidDatas[j].Velocity * OneContactChannel.FluidDatas[j].Density * item.Angle;
                    }

                    h = h / TotalAngle;
                    Tf = Tf / TotalAngle;
                    G1 = G1 / TotalAngle;

                    Debug.WriteLine("G1->" + G1);

                    //线性功率，单位W/M
                    double Linearpower = InputData.RodCollection.Rods[i].SubPowerCollection[j].Value;
                    Linearpower = Linearpower * PowerFactor;
                    //包壳外表面 - 温度
                    double Tw = Linearpower * (1 - FluidShare) / PI / d_rod / h + Tf;

                    //燃料包壳节点数
                    int CladNode = InputData.Options.CladNode;
                    int PelletNode = InputData.Options.PelletNode;



                    double[] T_Clad = new double[CladNode];
                    double[] T_Pellet = new double[PelletNode];

                    T_Clad[0] = Tw;
                    //包壳 -稳态 -控制体积- 模型 -没考虑- 产热
                    for (int n = 1; n < CladNode; n++)
                    {
                        double r1 = 0.5 * d_rod - (n - 1) * CladThickness / (CladNode - 1);
                        double r2 = 0.5 * d_rod - n * CladThickness / (CladNode - 1);

                        double R = Math.Log(r1 / r2) / (2 * PI * Clad.K.Get(T_Clad[n - 1]) * Ln);
                        T_Clad[n] = Linearpower * Ln * R + T_Clad[n - 1];
                    }

                    double h_gasGap = InputData.MaterialCollection.GasGap.h;
                    double T_pellet_outside = Linearpower / h_gasGap / (2 * PI) + T_Clad[CladNode - 1];

                    T_Pellet[0] = T_pellet_outside;
                    //芯块 -稳态 -控制体积- 模型 
                    for (int n = 1; n < PelletNode - 1; n++)
                    {
                        //环外径 - 占比
                        double d1 = 1 - (double)(n - 1) / (PelletNode - 1);
                        //Debug.WriteLine("d1->" + d1);
                        //环内径 - 占比
                        double d2 = 1 - (double)n / (PelletNode - 1);
                        //Debug.WriteLine("d2->" + d2);
                        //热阻
                        double R = Math.Log(d1 / d2) / (2 * PI * Pellet.K.Get(T_Pellet[n - 1]) * Ln);
                        //平均半径
                        double d_avg = (d1 + d2) * 0.5;
                        //环内部产热
                        double InsideHeat = Linearpower * PelletShare * Ln * (d_avg * d_avg);

                        //Debug.WriteLine("GenerateHeat->"+ InsideHeat);

                        T_Pellet[n] = T_Pellet[n - 1] + InsideHeat * R;
                    }
                    T_Pellet[PelletNode - 1] = T_Pellet[PelletNode - 2] + 0.25 * Linearpower / Pellet.K.Get(T_Pellet[PelletNode - 2]) * (1.0 / (PelletNode - 1) / (PelletNode - 1));

                    //燃料棒温度场

                    //燃料棒节点
                    SubRodTemperature srt = new SubRodTemperature
                    {
                        Index = j,
                        CladOutside = Math.Round(T_Clad[0], 3),
                        CladInside = Math.Round(T_Clad[CladNode - 1], 3),
                        PelletTemperatures = new List<PelletTemperature>(),
                        h =Math.Round( h,acc.h),
                        Q = Math.Round(Linearpower / d_rod / PI, 3),
                    };

                    double P = OutputData.ChannelFlows.ChannelFlow[i].FluidDatas[j].Pressure;
                    double H = OutputData.ChannelFlows.ChannelFlow[i].FluidDatas[j].Enthalphy;
                    double MassVelocity = OutputData.ChannelFlows.ChannelFlow[i].FluidDatas[j].MassFlowRate;
                    //double G1 = MassVelocity / InputData.ChannelCollection.Channels[i].FlowArea;

                    double Hf = Coolent.GetHf(P);
                    double Hg = Coolent.GetHg(P);

                    double Xe = (H - Hf) / (Hg - Hf);
                    //Debug.WriteLine("Xe->" + Xe);
               
                    double qc = Q_Critical(Xe, d_rod, G1, Hf, H, InputData.Options.DNBR_Formula);
                    srt.Qc = Math.Round(qc, 3);
                    srt.DNBR = Math.Round(srt.Qc / srt.Q, 3);


                    for (int n = 0; n < PelletNode; n++)
                    {
                        //燃料芯块温度场
                        PelletTemperature pt = new PelletTemperature
                        {
                            Radius = Math.Round(1 - (double)n / (PelletNode - 1), 4),
                            Temperature = Math.Round(T_Pellet[n], acc.T),
                        };
                        srt.PelletTemperatures.Add(pt);
                    }
                    OutputData.RodsTemperature.RodTemperature[i].SubRodTemperature.Add(srt);
                }
            }
        }






        public void RunAllSteps()
        {
            BeginRecognize();
            SetOutput();
            CaculateGeneralFlow();
            CaculateChannelFlow();
            CaculateRodsTemperature();
        }



































        //public double FrictionFactor(double Re)
        //{
        //    throw new NotImplementedException();
        //}

        //public double Nu(double Re, double Pr)
        //{
        //    throw new NotImplementedException();
        //}

        //public double h_convect(double Re, double Pr, double Lamd, double de)
        //{
        //    throw new NotImplementedException();
        //}

        //public double Q_Critical(double Xe, double d, double G, double h_f, double h_in, int formlula)
        //{
        //    throw new NotImplementedException();
        //}

        //public double W3_Formula(double Xe, double d, double G, double h_f, double h_in)
        //{
        //    throw new NotImplementedException();
        //}

        //public double EPRI_Formula(double Xe, double d, double G, double h_f, double h_in)
        //{
        //    throw new NotImplementedException();
        //}

        //public double Biasi_Formula(double Xe, double d, double G, double h_f, double h_in)
        //{
        //    throw new NotImplementedException();
        //}

        //public void df()
        //{
        //    throw new NotImplementedException();
        //}
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