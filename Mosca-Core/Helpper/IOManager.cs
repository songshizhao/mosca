///----------------------------------------------------------
//输入输出控制类:IOManager
//说明：使用该对象处理输入输出文件
//创建于 2017-8-15 宋仕钊;上次编辑2018-3-25 宋仕钊
///----------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using MoscaCore.Models;



namespace MoscaCore.Helpper
{
    public class IOManager
    {
        /// <summary>输入文件模型 </summary>
        public InputModel InputData { get; set; }

        private OutputModel outputData;
        /// <summary>输出文件模型</summary>
        public OutputModel OutputData
        {
            get { return outputData; }
            set { outputData = value; }
        }
        /// <summary>输入文件xml文本</summary>
        public string InputXmlString { get; set; }
        /// <summary>输出文件xml文本</summary>
        public string OutputXmlString { get; set; }

        /// <summary>数据输入反序列化XML文本</summary>
        public void Input(string XmlString)
        {
            Main.MsgCenter.ShowMessage("正在读取输入信息...");
            //将输入字符串变量赋值
            InputXmlString = XmlString;
            using (MemoryStream MS = new MemoryStream(Encoding.UTF8.GetBytes(InputXmlString)))
            {
                using (XmlReader xr = XmlReader.Create(MS))
                {
                    XmlSerializer xmlSearializer = new XmlSerializer(typeof(InputModel));
                    InputData = (InputModel)xmlSearializer.Deserialize(xr);
                    
                }
            }
        }


        /// <summary>数据输出序列化XML文本,计算后调用,需要原始对象直接则获取OutputModel属性</summary>
        public string Output()
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

        /// <summary>
        /// *仅测试用
        /// </summary>
        public static string OutputDemoInputXml()
        {
            string Result;
            //#.InputDemo.#
            InputModel DemoInput = new InputModel()
            {
                Title = new Title
                {
                    Value = "Input demo",
                    Infos = new List<string>
                    {
                        "dsd",
                    },

                },


                RodTypes = new List<RodType>()
                {
                    new RodType(){
                        Index=1,
                        CladMaterialIndex=1,
                        PelletMaterialIndex=2,
                        Diameter=0.008,
                        PelletDiameter=0.006,
                        CladThickness=0.001,
                    },
                },
                //材料集合
                MaterialCollection = new MaterialCollection()
                {
                    Fluid = new Fluid()
                    {
                        Name = "LightWater",
                        Type = "GetPropertyByName",
                    },
                    Materials = new List<Material>()
                    {
                        new Material()
                        {
                            Index=1,
                            Type=MaterialTypes.UserDefine,
                            Name="UO2",
                            K=new KCollection()
                            {
                                Value=0,
                                KData=new List<Data>()
                                {
                                    new Data(){Value=2,T=100,},
                                    new Data(){Value=2,T=200,},
                                    new Data(){Value=2,T=300,},
                                },
                            },
                            Cp=new CpCollection()
                            {
                                Value=0,
                                CpData=new List<Data>()
                                {
                                    new Data(){Value=2,T=100,},
                                    new Data(){Value=2,T=200,},
                                    new Data(){Value=2,T=300,},
                                },
                            },

                        },
                        new Material()
                        {
                            Index=2,
                            Type=MaterialTypes.UserDefine,
                            Name="ZirClad",
                            K=new KCollection()
                            {
                                Value=0,
                                KData=new List<Data>()
                                {
                                    new Data(){Value=2,T=100,},
                                    new Data(){Value=2,T=200,},
                                    new Data(){Value=2,T=300,},
                                }
                            },
                            Cp=new CpCollection()
                            {
                                Value=0,
                                CpData=new List<Data>()
                                {
                                    new Data(){Value=2,T=100,},
                                    new Data(){Value=2,T=200,},
                                    new Data(){Value=2,T=300,},
                                },
                            },
                        },
                        new Material()
                        {
                            Index=3,
                            Type=MaterialTypes.GetPropertyByName,
                            Name="LightWater",
                        },
                        new Material()
                        {
                            Index=4,
                            Type=MaterialTypes.GetPropertyByName,
                            Name="GasGap",
                        },
                    },

                },
                //通道集合
                ChannelCollection = new ChannelCollection()
                {
                    Channels = new List<Channel>()
                    {
                        new Channel()
                        {
                            AreaFactor=1,
                            FlowArea=0.0000277434,
                            Index=1,
                            WetPerimeter = 0.0178024,
                            HotPerimeter = 0.0104720,
                            ConnectedChannels=new List<ConnectedChannel>()
                            {
                                new ConnectedChannel()
                                {
                                    Index=2,
                                    Gap=0.0024,
                                    Distance = 0.0086,
                                }
                            },

                        },
                        new Channel()
                        {
                            Index=2,
                            AreaFactor=1,
                            FlowArea=0.0000557380,
                            WetPerimeter = 0.0251328,
                            HotPerimeter = 0.0251328,
                            ConnectedChannels=new List<ConnectedChannel>()
                            {
                                new ConnectedChannel()
                                {
                                    Index=3,
                                    Gap=0.0029,
                                    Distance = 0.0069,
                                },
                                new ConnectedChannel()
                                {
                                    Index=4,
                                    Gap=0.0024,
                                    Distance = 0.0090,
                                },
                            },

                        },
                        new Channel()
                        {
                            Index=3,
                            AreaFactor=1,
                            FlowArea=0.0000122937,
                            WetPerimeter = 0.0062832,
                            HotPerimeter = 0.0062832,
                            ConnectedChannels=new List<ConnectedChannel>()
                            {
                                new ConnectedChannel()
                                {
                                    Index=5,
                                    Gap=0.0012,
                                    Distance = 0.0072,
                                }
                            },

                        },
                        new Channel()
                        {
                            Index=4,
                            AreaFactor=1,
                            FlowArea=0.0000381208,
                            WetPerimeter = 0.0263940,
                            HotPerimeter = 0.0139627,
                            ConnectedChannels=new List<ConnectedChannel>()
                            {
                                new ConnectedChannel()
                                {
                                    Index=5,
                                    Gap=0.0015,
                                    Distance = 0.0095,
                                }
                            },

                        },
                        new Channel()
                        {
                            Index=5,
                            AreaFactor=1,
                            FlowArea=0.00001190604,
                            WetPerimeter = 0.0131970,
                            HotPerimeter = 0.0069813,
                        },
                    },

                },
                //燃料棒集合
                RodCollection = new RodCollection()
                {
                    Segment = 100,
                    Length = 1.4,
                    Rods = new List<Rod>()
                    {
                        new Rod()
                        {
                           Index=1,
                           Type=1,
                           SubPowerCollection=new List<SubPower>()
                           {
                               new SubPower()
                               {
                                   From=0,
                                   To=0.1,
                                   Value=11950,
                               },
                               new SubPower()
                               {
                                   From=0.1,
                                   To=0.2,
                                   Value=17390,
                               },
                               new SubPower()
                               {
                                   From=0.2,
                                   To=0.3,
                                   Value=21250,
                               },
                               new SubPower()
                               {
                                   From=0.3,
                                   To=0.4,
                                   Value=24380,
                               },
                               new SubPower()
                               {
                                   From=0.4,
                                   To=0.5,
                                   Value=21640,
                               },
                               new SubPower()
                               {
                                   From=0.5,
                                   To=0.6,
                                   Value=20690,
                               },
                               new SubPower()
                               {
                                   From=0.6,
                                   To=0.7,
                                   Value=20690,
                               },
                               new SubPower()
                               {
                                   From=0.7,
                                   To=0.8,
                                   Value=17260,
                               },
                               new SubPower()
                               {
                                   From=0.8,
                                   To=0.9,
                                   Value=14430,
                               },
                               new SubPower()
                               {
                                   From=0.9,
                                   To=1.0,
                                   Value=9851,
                               },
                               new SubPower()
                               {
                                   From=1.0,
                                   To=1.1,
                                   Value=8200,
                               },
                               new SubPower()
                               {
                                   From=1.1,
                                   To=1.2,
                                   Value=6196,
                               },
                               new SubPower()
                               {
                                   From=1.2,
                                   To=1.3,
                                   Value=4581,
                               },
                               new SubPower()
                               {
                                   From=1.3,
                                   To=1.4,
                                   Value=2653,
                               },
                           },
                           ContactedChannel=new List<ContactedChannel>()
                           {
                               new ContactedChannel()
                               {
                                   Index=1,
                                   Angle=75,
                               },
                               new ContactedChannel()
                               {
                                   Index=2,
                                   Angle=105,
                               },
                           },
                        },
                        new Rod()
                        {
                           Index=2,
                           Type=1,
                           SubPowerCollection=new List<SubPower>()
                           {
                               new SubPower()
                               {
                                   From=0,
                                   To=0.1,
                                   Value=11950,
                               },
                               new SubPower()
                               {
                                   From=0.1,
                                   To=0.2,
                                   Value=17390,
                               },
                               new SubPower()
                               {
                                   From=0.2,
                                   To=0.3,
                                   Value=21250,
                               },
                               new SubPower()
                               {
                                   From=0.3,
                                   To=0.4,
                                   Value=24380,
                               },
                               new SubPower()
                               {
                                   From=0.4,
                                   To=0.5,
                                   Value=21640,
                               },
                               new SubPower()
                               {
                                   From=0.5,
                                   To=0.6,
                                   Value=20690,
                               },
                               new SubPower()
                               {
                                   From=0.6,
                                   To=0.7,
                                   Value=20690,
                               },
                               new SubPower()
                               {
                                   From=0.7,
                                   To=0.8,
                                   Value=17260,
                               },
                               new SubPower()
                               {
                                   From=0.8,
                                   To=0.9,
                                   Value=14430,
                               },
                               new SubPower()
                               {
                                   From=0.9,
                                   To=1.0,
                                   Value=9851,
                               },
                               new SubPower()
                               {
                                   From=1.0,
                                   To=1.1,
                                   Value=8200,
                               },
                               new SubPower()
                               {
                                   From=1.1,
                                   To=1.2,
                                   Value=6196,
                               },
                               new SubPower()
                               {
                                   From=1.2,
                                   To=1.3,
                                   Value=4581,
                               },
                               new SubPower()
                               {
                                   From=1.3,
                                   To=1.4,
                                   Value=2653,
                               },
                           },
                           ContactedChannel=new List<ContactedChannel>()
                           {
                               new ContactedChannel()
                               {
                                   Index=1,
                                   Angle=75,
                               },
                               new ContactedChannel()
                               {
                                   Index=2,
                                   Angle=76.4,
                               },
                               new ContactedChannel()
                               {
                                   Index=3,
                                   Angle=28.6,
                               },
                           },
                        },
                        new Rod()
                        {
                           Index=3,
                           Type=1,
                           SubPowerCollection=new List<SubPower>()
                           {
                               new SubPower()
                               {
                                   From=0,
                                   To=0.1,
                                   Value=11950,
                               },
                               new SubPower()
                               {
                                   From=0.1,
                                   To=0.2,
                                   Value=17390,
                               },
                               new SubPower()
                               {
                                   From=0.2,
                                   To=0.3,
                                   Value=21250,
                               },
                               new SubPower()
                               {
                                   From=0.3,
                                   To=0.4,
                                   Value=24380,
                               },
                               new SubPower()
                               {
                                   From=0.4,
                                   To=0.5,
                                   Value=21640,
                               },
                               new SubPower()
                               {
                                   From=0.5,
                                   To=0.6,
                                   Value=20690,
                               },
                               new SubPower()
                               {
                                   From=0.6,
                                   To=0.7,
                                   Value=20690,
                               },
                               new SubPower()
                               {
                                   From=0.7,
                                   To=0.8,
                                   Value=17260,
                               },
                               new SubPower()
                               {
                                   From=0.8,
                                   To=0.9,
                                   Value=14430,
                               },
                               new SubPower()
                               {
                                   From=0.9,
                                   To=1.0,
                                   Value=9851,
                               },
                               new SubPower()
                               {
                                   From=1.0,
                                   To=1.1,
                                   Value=8200,
                               },
                               new SubPower()
                               {
                                   From=1.1,
                                   To=1.2,
                                   Value=6196,
                               },
                               new SubPower()
                               {
                                   From=1.2,
                                   To=1.3,
                                   Value=4581,
                               },
                               new SubPower()
                               {
                                   From=1.3,
                                   To=1.4,
                                   Value=2653,
                               },
                           },
                           ContactedChannel=new List<ContactedChannel>()
                           {
                               new ContactedChannel()
                               {
                                   Index=2,
                                   Angle=80,
                               },
                               new ContactedChannel()
                               {
                                   Index=4,
                                   Angle=100,
                               },
                           },
                        },
                        new Rod()
                        {
                           Index=4,
                           Type=1,
                           SubPowerCollection=new List<SubPower>()
                           {
                               new SubPower()
                               {
                                   From=0,
                                   To=0.1,
                                   Value=11950,
                               },
                               new SubPower()
                               {
                                   From=0.1,
                                   To=0.2,
                                   Value=17390,
                               },
                               new SubPower()
                               {
                                   From=0.2,
                                   To=0.3,
                                   Value=21250,
                               },
                               new SubPower()
                               {
                                   From=0.3,
                                   To=0.4,
                                   Value=24380,
                               },
                               new SubPower()
                               {
                                   From=0.4,
                                   To=0.5,
                                   Value=21640,
                               },
                               new SubPower()
                               {
                                   From=0.5,
                                   To=0.6,
                                   Value=20690,
                               },
                               new SubPower()
                               {
                                   From=0.6,
                                   To=0.7,
                                   Value=20690,
                               },
                               new SubPower()
                               {
                                   From=0.7,
                                   To=0.8,
                                   Value=17260,
                               },
                               new SubPower()
                               {
                                   From=0.8,
                                   To=0.9,
                                   Value=14430,
                               },
                               new SubPower()
                               {
                                   From=0.9,
                                   To=1.0,
                                   Value=9851,
                               },
                               new SubPower()
                               {
                                   From=1.0,
                                   To=1.1,
                                   Value=8200,
                               },
                               new SubPower()
                               {
                                   From=1.1,
                                   To=1.2,
                                   Value=6196,
                               },
                               new SubPower()
                               {
                                   From=1.2,
                                   To=1.3,
                                   Value=4581,
                               },
                               new SubPower()
                               {
                                   From=1.3,
                                   To=1.4,
                                   Value=2653,
                               },
                           },
                           ContactedChannel=new List<ContactedChannel>()
                           {
                               new ContactedChannel()
                               {
                                   Index=2,
                                   Angle=98.604,
                               },
                               new ContactedChannel()
                               {
                                   Index=3,
                                   Angle=61.416,
                               },
                               new ContactedChannel()
                               {
                                   Index=4,
                                   Angle=100.008,
                               },
                               new ContactedChannel()
                               {
                                   Index=5,
                                   Angle=100.008,
                               },
                           },
                        },
                    },
                },
                //格架定义
                GridCollection = new GridCollection()
                {
                    Grids = new List<Grid>()
                    {
                       new Grid{ },
                       new Grid{ },
                       new Grid{ },
                       new Grid{ },
                    },
                },
                //质量流量输入
                MassFlow = new MassFlow()
                {
                    MassVelocity = 2,
                    Temperature = 240,
                    Pressure = 14,
                    //FluidMateralIndex = 5,
                    Flow_Direction = 1,
                },
                //计算选项
                Options = new Options()
                {
                    AnsysType = 1,
                    DNBR_Formula = CHF_Formula_Types.W3,
                    IsOpenChannel = 1,
                    Iteration = new Iteration {
                        IterationType = IterationTypes.NodeIteration,
                        MaxIteration = 1,
                        Sigma = 100,
                    },
                },
            };
            //#.InputDemo.#

            //
            using (StringWriter writer = new StringWriter())
            {
                XmlSerializer xmlSearializer = new XmlSerializer(typeof(InputModel));
                xmlSearializer.Serialize(writer, DemoInput);
                Result = writer.ToString();
            }

            return Result;
        }
    }
}
