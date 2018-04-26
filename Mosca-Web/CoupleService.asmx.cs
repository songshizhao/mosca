using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MoscaCore;
using MoscaCore.Models;

namespace MoscaWeb
{
    /// <summary>
    /// service 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://falseuri.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class CoupleService : System.Web.Services.WebService
    {
        /// <summary>
        /// 输入xml字符串，返回xml字符串
        /// </summary>
        /// <param name="XmlString"></param>
        /// <returns></returns>
        [WebMethod]
        public string Caculate_Return_Xml_String(string XmlString)
        {
            Main m = new Main();
            m.MyIOManager.Input(XmlString);
            m.RunAllSteps();

            return m.MyIOManager.Output();
        }
        /// <summary>
        /// 输入xml字符串，返回Model对象（xml）
        /// </summary>
        /// <param name="XmlString"></param>
        /// <returns></returns>
        [WebMethod]
        public OutputModel Caculate_Return_Object_Model(string XmlString)
        {
            Main m = new Main();
            m.MyIOManager.Input(XmlString);
            m.RunAllSteps();
            return m.MyIOManager.OutputData;
        }

        /// <summary>
        /// 输入对象，返回对象
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [WebMethod]
        public OutputModel Object_To_Object_Couple(InputModel inputModel)
        {
            Main m = new Main();
            m.MyIOManager.InputData= inputModel;
            m.RunAllSteps();
            return m.MyIOManager.OutputData;
        }





    }
}
