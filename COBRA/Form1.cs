using COBRA.Properties;
using CobraDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COBRA
{
    public partial class Form1 : Form
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
            string r = FileIO.OutputDemoXml();
            Debug.WriteLine(r);
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
        private void button1_Click(object sender, EventArgs e)
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
        private void button2_Click(object sender, EventArgs e)
        {
            OutputFileDialog.Title = "选取输出文件";
            OutputFileDialog.FileName = "Output.xml";
            //OutputFileDialog.Filter = "xml文件(*.xml)|*.xml|所有类型 (*.*)|*.*";// "excel文件(*.xlsx)|*.xlsx|旧版excel文件(*.xls)|*.xls|所有类型 (*.*)|*.*";
            OutputFileDialog.RestoreDirectory = true;
            OutputFileDialog.ShowDialog();
        }
        //点击开始计算按钮
        private void button3_Click(object sender, EventArgs e)
        {


            Main m = new Main();



            using (StreamReader reader = new StreamReader(textBox1.Text))
            {

                string XmlString = reader.ReadToEnd();
                //给计算程序输入数据
                string InputResult = m.FileInput(XmlString);
                if (InputResult != "Read XML Success")
                {
                    DebugTextBox.AppendText("\n Error：F0001:" + InputResult + "\n");
                    return;//放弃计算
                }
                else
                {
                    //识别成功
                    DebugTextBox.AppendText("\n" + InputResult + "...\n");

                }
            }
            m.SetOutput();

            //在这里认为进行了5%的工作
            progressBar1.Value = 5;


            progressBar1.Value = 10;

            m.BeginRecognize();
            DebugTextBox.AppendText("\n" + "Recognize Input" + "...\n");
            progressBar1.Value = 15;


            m.CaculateGeneralFlow();
            DebugTextBox.AppendText("\n" + "Caculate General Flow" + "...\n");
            progressBar1.Value = 25;


            m.CaculateChannelFlow();
            DebugTextBox.AppendText("\n" + "Caculate Channel Flow" + "...\n");
            progressBar1.Value = 45;


            m.CaculateRodsTemperature();
            DebugTextBox.AppendText("\n" + "Caculate Rods Temperature" + "...\n");
            progressBar1.Value = 100;




            //设置输出
            try
            {
                m.SetOutput();
            }
            catch (Exception ex)
            {

                DebugTextBox.AppendText("\n Error：F0003:" + ex.Message + "\n");
                return;//放弃计算
            }
            //在这里认为进行了10%的工作
            progressBar1.Value = 10;
            //输入数据处理



            try
            {
                m.BeginRecognize();
                DebugTextBox.AppendText("\n" + "Recognize Input" + "...\n");
            }
            catch (Exception ex)
            {

                DebugTextBox.AppendText("\n" + ex.Message + "\n");
                return;//放弃计算
            }
            //在这里认为进行了15%的工作
            progressBar1.Value = 15;
            try
            {
                m.CaculateGeneralFlow();
                DebugTextBox.AppendText("\n" + "Caculate General Flow" + "...\n");
            }
            catch (Exception ex)
            {

                DebugTextBox.AppendText("\n" + ex.Message + "\n");
                return;//放弃计算
            }
            //在这里认为进行了25%的工作
            progressBar1.Value = 25;
            try
            {
                m.CaculateChannelFlow();
                DebugTextBox.AppendText("\n" + "Caculate Channel Flow" + "...\n");
            }
            catch (Exception ex)
            {

                DebugTextBox.AppendText("\n" + ex.Message + "\n");
                return;//放弃计算
            }

            //在这里认为进行了35%的工作
            progressBar1.Value = 35;
            try
            {
                m.CaculateRodsTemperature();
                DebugTextBox.AppendText("\n" + "Caculate Rods Temperature" + "...\n");
            }
            catch (Exception ex)
            {

                DebugTextBox.AppendText("\n" + ex.Message + "\n");
                return;//放弃计算
            }
            //在这里认为进行了45%的工作
            progressBar1.Value = 45;

            //开始输出计算的结果



            try
            {
                string XmlOutput = m.FileOutput();
                using (StreamWriter writer = new StreamWriter(textBox2.Text, false, Encoding.UTF8))
                {
                    //writer.Encoding = ;
                    writer.Write(XmlOutput);
                }
                DebugTextBox.AppendText("\n" + "Completed！" + "...\n");
            }
            catch (Exception ex)
            {
                DebugTextBox.AppendText("\n" + "F0002" + ex.Message + "\n");
                return;
            }

            //在这里认为进行了100%的工作
            progressBar1.Value = 100;
        }
    }
}










//try
//{
//    string XmlString = reader.ReadToEnd();
//    //给计算程序输入数据
//    string InputResult = CobraDLL.FileIO.FileInput(XmlString);
//    if (InputResult!= "Read XML Success")
//    {
//        DebugTextBox.AppendText("\n致命错误:" + InputResult+"\n");
//        return;//放弃计算
//    }
//    else
//    {
//        DebugTextBox.AppendText("\n" + InputResult + "...\n");
//    }
//    //发出开始计算命令...

//    //获得计算结果并输出
//    Main m = new Main();
//    m.Caculate();

//    string XmlOutput=FileIO.FileOutput();
//    Debug.Write(XmlOutput);
//    using (StreamWriter writer=new StreamWriter(textBox2.Text))
//    {
//        writer.Write(XmlOutput);
//    }
//}
//catch (Exception ex)
//{
//    DebugTextBox.AppendText("\n读取文件失败,致命:" + ex.Message);
//    return;//放弃计算
//}