//***************************************************************
//********用于传输消息；                                       *
//********  计算中出现的消息传递
//********      如有消息需要提示调用此类,使用方式为接口委托
//********          创建/2017-12-22/宋仕钊
//********              上次编辑/2018-4-3/宋仕钊
//****************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace CobraDLL
{
    /// <summary>
    /// 接口：用来向使用者报告正在进行计算和分析的详细信息
    /// </summary>
    public interface IMsgCenter
    {
        Task ShowMessage(string msg);
        Task ShowProcess(double process);

       // delegate Sertionardfy;
    }


    public class TempMsgCenter : IMsgCenter
    {
        public async Task ShowMessage(string msg)
        {
           
            //return null;
        }

        public async Task ShowProcess(double process)
        {
           // return null;
        }
    }

}
