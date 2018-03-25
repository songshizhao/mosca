/////////////////////////////////////////////////////////////
//********COBRA MODEL CLASS
//********FIRST CREATED BY SONGSHIZHAO @ 2017年8月15日
//********已弃用
//********LASTEST EDITED BY SONGSHIZHAO @ 2017年10月12日10:00:36
//********END
/////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using CobraDLL.Models;



namespace CobraDLL
{

    public class FileIO
    {
        public static string InputXmlString;
        public static string OutputXmlString;
        /// 数据输入
        //public static string FileInput(string XmlString)
        //{
        //    string InputResult = "";
        //    using (StringReader reader = new StringReader(XmlString))
        //    {
        //        XmlSerializer xmlSearializer = new XmlSerializer(typeof(CobraInput));
        //        try
        //        {
        //            Main.InputData = (CobraInput)xmlSearializer.Deserialize(reader);
        //            InputResult = "Read XML Success";
        //        }
        //        catch (Exception ex)
        //        {
        //            InputResult = "反序列化Xml失败：" + ex.Message;
        //        }

        //    }
        //    return InputResult;
        //}

        //public static string FileOutput()
        //{



        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        var setting = new XmlWriterSettings()
        //        {
        //            Encoding = new UTF8Encoding(false),
        //            Indent = true,
        //        };

        //        using (XmlWriter writer = XmlWriter.Create(ms, setting))
        //        {

        //            XmlSerializer xmlSearializer = new XmlSerializer(typeof(CobraOutput));
        //            xmlSearializer.Serialize(writer, Main.OutputData);
        //            OutputXmlString = Encoding.UTF8.GetString(ms.ToArray());
        //        }

        //    }



        //    return OutputXmlString;
        //    //Debug.Write(OutputXmlString);
        //}

        public static string OutputDemoXml()
        {
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


                RodTypes =new List<RodType>()
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
                            Type="UserDefine",
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
                            Type="UserDefine",
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
                            Type="GetPropertyByName",
                            Name="LightWater",
                        },
                        new Material()
                        {
                            Index=4,
                            Type="FindPropertyByName",
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
                GridCollection=new GridCollection()
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
                    FluidMateralIndex = 5,
                    Flow_Direction = 1,
                },
                //计算选项
                Options = new OptionCollection()
                {
                    AnsysType = 1,
                    DNBR_Formula = 1,
                    IsOpenChannel = 1,
                },
            };
            //#.InputDemo.#

            //
            using (StringWriter writer = new StringWriter())
            {
                XmlSerializer xmlSearializer = new XmlSerializer(typeof(InputModel));
                xmlSearializer.Serialize(writer, DemoInput);
                OutputXmlString = writer.ToString();
            }

            return OutputXmlString;
        }




    }
}
