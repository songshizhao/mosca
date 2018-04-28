using MoscaPC.Properties;
using MoscaCore;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using MoscaCore.Helpper;
using System.Diagnostics;
using ExcelHandler;
using System.Collections.Generic;
using MoscaCore.Models;

namespace MoscaPC
{
    public partial class Form1 : Form,IMsgCenter
    {
        public Form1()
        {
            InitializeComponent();

            //Debug.WriteLine(FileIO.OutputDemoXml());

            //读取历史数据（输入输出路径）
            if (Settings.Default.InputFileName!="")
            {
                textBox1.Text = Settings.Default.InputFileName;
            }
            if (Settings.Default.OutputFileName != "")
            {
                textBox2.Text = Settings.Default.OutputFileName;
            }
            //测试字符串输出
            //Debug.WriteLine(IOManager.OutputDemoInputXml());
        }
        //选择输入文件
        private void InputOpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            textBox1.Text = InputOpenFileDialog.FileName;
            //保存设置
            Settings.Default.InputFileName = textBox1.Text;
            Settings.Default.Save();
        }
        //开始选择输入文件
        private void SelectInput_Click(object sender, EventArgs e)
        {
            InputOpenFileDialog.Title = "选取输入文件";
            InputOpenFileDialog.FileName = "...";
            InputOpenFileDialog.Filter = "xml文件(*.xml)|*.xml|所有类型 (*.*)|*.*";// "excel文件(*.xlsx)|*.xlsx|旧版excel文件(*.xls)|*.xls|所有类型 (*.*)|*.*";
            InputOpenFileDialog.RestoreDirectory = true;
            InputOpenFileDialog.ShowDialog();
        }

        //选择输出文件
        private void OutputFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            textBox2.Text = OutputFileDialog.FileName;
            //保存设置
            Settings.Default.OutputFileName = textBox2.Text;
            Settings.Default.Save();
        }
        //开始选择输出文件
        private void SelectOutput(object sender, EventArgs e)
        {
            OutputFileDialog.Title = "选取输出文件";
            OutputFileDialog.FileName = "Output.xml";
            //OutputFileDialog.Filter = "xml文件(*.xml)|*.xml|所有类型 (*.*)|*.*";// "excel文件(*.xlsx)|*.xlsx|旧版excel文件(*.xls)|*.xls|所有类型 (*.*)|*.*";
            OutputFileDialog.RestoreDirectory = true;
            OutputFileDialog.ShowDialog();
        }
        //点击开始计算按钮
        private void Run_Click(object sender, EventArgs e)
        {
            //Dll主程序实例化
            Main m = new Main();
            //设置消息提示
            Main.MsgCenter = this;
            //输入对接
            using (StreamReader reader = new StreamReader(textBox1.Text))
            {

                string XmlString = reader.ReadToEnd();
                //给计算程序输入数据
                m.MyIOManager.Input(XmlString);
            }
            //开始计算
            m.RunAllSteps();

            string XmlOutput = m.MyIOManager.Output();
            using (StreamWriter writer = new StreamWriter(textBox2.Text))
            {
                writer.Write(XmlOutput);
            }

         
            using (FolderBrowserDialog select_folder = new FolderBrowserDialog())
            {
                

                select_folder.Description = "选择excel文件输出路径";
                select_folder.ShowDialog();
                if (select_folder.SelectedPath!="")
                {
                    string full_filename = select_folder.SelectedPath + "/output.xlsx";
                    Output_Result_To_Excel(m.MyIOManager.OutputData, full_filename);
                    // @"C:\Users\Administrator\Desktop\text.xlsx"
                }

            }

        }



   

        public Task ShowMessage(string msg)
        {
            //await Task.Run(() =>{ });
            DebugTextBox.AppendText("\n" + msg + "\n");
            
            return null;
        }

        public Task ShowProcess(double process)
        {
           return null;
            //throw new NotImplementedException();
        }




        void Output_Result_To_Excel(OutputModel outputModel, string path)
        {
            int currentRow = 1;
            int currentColumn = 1;
            //新建一个excel 程序
            var ExcelObject = new ExcelInstance();
            List<string> SheetsNameList = new List<string> {
                "基本数据",
                 "进阶数据",
                 "其他数据",
            };
            //创建一个Excel 文件
            ExcelObject.CreatNewExcel(path, SheetsNameList, false);
            //写标题
            ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, 1, "标题:" + outputModel.Title.Value);
            //写说明
            foreach (string info in outputModel.Title.Infos)
            {
                currentRow += 1;
                ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn, "说明:" + info);

            }
            currentRow += 1;
            //写一个表头标题
            ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn, "平均通道计算结果");
            ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 1, "位置");
            ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 2, "温度℃");
            ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 3, "压力Mpa");
            ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 4, "密度");
            ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 5, "比焓kJ/kg");
            ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 6, "流速m/s");
            ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 7, "质量流苏kg/s");
            ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 8, "对流换热系数W/m2");
            ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 9, "导热系数");
            ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 10, "运动粘度");
            ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 11, "雷诺数");
            ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 12, "热平衡含气率");
            ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 13, "DNBR");
            
            foreach (var fluidData in outputModel.SteadyResult.GeneralFlow.FluidDatas)
            {
                currentRow += 1;
                ExcelObject.Output_On_Cell<double>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 1, fluidData.Position);
                ExcelObject.Output_On_Cell<double>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 2, fluidData.Temperature);
                ExcelObject.Output_On_Cell<double>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 3, fluidData.Pressure);
                ExcelObject.Output_On_Cell<double>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 4, fluidData.Density);
                ExcelObject.Output_On_Cell<double>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 5, fluidData.Enthalphy);
                ExcelObject.Output_On_Cell<double>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 6, fluidData.Velocity);
                ExcelObject.Output_On_Cell<double>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 7, fluidData.MassFlowRate);
                ExcelObject.Output_On_Cell<double>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 8, fluidData.h);
                ExcelObject.Output_On_Cell<double>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 9, fluidData.K);
                ExcelObject.Output_On_Cell<double>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 10, fluidData.Kv);
                ExcelObject.Output_On_Cell<double>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 11, fluidData.Re);
                ExcelObject.Output_On_Cell<double>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 12, fluidData.Xe);
                ExcelObject.Output_On_Cell<double>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 13, fluidData.DNBR);
            }
            currentRow += 1;
            foreach (var channeFlow in outputModel.SteadyResult.ChannelsFlow)
            {
                
                ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn, "子通道"+ channeFlow.ChannelIndex + "计算结果");
                ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 1, "位置");
                ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 2, "温度℃");
                ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 3, "压力Mpa");
                ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 4, "密度");
                ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 5, "比焓kJ/kg");
                ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 6, "流速m/s");
                ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 7, "质量流苏kg/s");
                ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 8, "对流换热系数W/m2");
                ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 9, "导热系数");
                ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 10, "运动粘度");
                ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 11, "雷诺数");
                ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 12, "热平衡含气率");
                ExcelObject.Output_On_Cell<string>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 13, "DNBR");

                currentRow += 1;
                foreach (var fluidData in channeFlow.FluidDatas)
                {
                    
                    ExcelObject.Output_On_Cell<double>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 1, fluidData.Position);
                    ExcelObject.Output_On_Cell<double>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 2, fluidData.Temperature);
                    ExcelObject.Output_On_Cell<double>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 3, fluidData.Pressure);
                    ExcelObject.Output_On_Cell<double>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 4, fluidData.Density);
                    ExcelObject.Output_On_Cell<double>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 5, fluidData.Enthalphy);
                    ExcelObject.Output_On_Cell<double>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 6, fluidData.Velocity);
                    ExcelObject.Output_On_Cell<double>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 7, fluidData.MassFlowRate);
                    ExcelObject.Output_On_Cell<double>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 8, fluidData.h);
                    ExcelObject.Output_On_Cell<double>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 9, fluidData.K);
                    ExcelObject.Output_On_Cell<double>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 10, fluidData.Kv);
                    ExcelObject.Output_On_Cell<double>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 11, fluidData.Re);
                    ExcelObject.Output_On_Cell<double>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 12, fluidData.Xe);
                    ExcelObject.Output_On_Cell<double>(ExcelObject.ExcelSheets[0], currentRow, currentColumn + 13, fluidData.DNBR);
                    currentRow += 1;
                }
            }






            //保存一个Excel 文件
            ExcelObject.SaveExcel(@"C:\Users\Administrator\Desktop\text.xlsx", true);


        }
        

    }
}










