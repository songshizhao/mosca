using MoscaPC.Properties;
using MoscaCore;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using MoscaCore.Helpper;
using System.Diagnostics;

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
        }



   

        public async Task ShowMessage(string msg)
        {
            //await Task.Run(() =>{ });
            DebugTextBox.AppendText("\n" + msg + "\n");
            
            //return null;
        }

        public async Task ShowProcess(double process)
        {
           // return null;
            //throw new NotImplementedException();
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