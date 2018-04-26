///-----------------------------------------------------
//MsgCenter.cs
//消息输出功能接口，用于传输消息
//创建:2017-12-22 宋仕钊
//修改:2018-4-3 宋仕钊
///------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MoscaCore.Helpper
{
    /// <summary>
    /// 接口：用来向使用者报告正在进行计算和分析的详细信息，非必须
    /// </summary>
    public interface IMsgCenter
    {
        Task ShowMessage(string msg);
        Task ShowProcess(double process);
    }

    /// <summary>
    /// 没有外部调用时留空的使用类
    /// </summary>
    public class TempMsgCenter : IMsgCenter
    {


        public async Task ShowMessage(string msg)
        {
        }

        public async Task ShowProcess(double process)
        {
        }
    }

}
