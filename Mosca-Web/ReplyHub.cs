using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using MoscaCore;
using MoscaCore.Helpper;

namespace MoscaWeb
{
    public class ReplyHub : Hub,IMsgCenter
    {
        public void Arrival()
        {
            Clients.Caller.arrival("已成功连接WEBSOCKET");
        }



        public void SendMessage(string msg,string caller)
        {
            Clients.Caller.sendMessage(msg);
        }

        public void Caculate(string xmlstring)
        {
            //获得计算对象
            Main MainCaculation = new Main();
            //给与计算输入
            MainCaculation.MyIOManager.Input(xmlstring);
            //使用集线器进行消息输入和输出
            Main.MsgCenter = this;
            //开始计算
            MainCaculation.RunAllSteps();
            //获得输出字符串
            string r= MainCaculation.MyIOManager.Output();
            //计算结果输出
            Clients.Caller.sendMessage(r);
        }

        public Task ShowMessage(string msg)
        {
            SendMessage(msg,"");
            return null;
        }

        public Task ShowProcess(double process)
        {
            SendMessage(process.ToString(), "");
            return null;
        }
    }
}