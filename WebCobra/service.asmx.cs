using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using CobraDLL;
namespace WebCobra
{
    /// <summary>
    /// service 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class service : System.Web.Services.WebService
    {

        
        public Main m = new Main();


        [WebMethod]
        public string All(string XmlString)
        {
            m.FileInput(XmlString);


            

            m.BeginRecognize();

            m.SetOutput();

            m.CaculateGeneralFlow();



            m.CaculateChannelFlow();



            m.CaculateRodsTemperature();

            return m.FileOutput();
        }



        [WebMethod]
        public string LoadInputXml(string XmlString)
        {
            
            string InputResult = m.FileInput(XmlString);
            return "Hello World" + InputResult;
        }



        [WebMethod]
        public void Caculate()
        {
            
            m.RunAllSteps();
           
        }


        [WebMethod]
        public string ReturnOutputString()
        {
            return m.FileOutput();
        }







    }
}
