using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoscaCore;

namespace MoscaWeb
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

           
        }


        //protected void Btn_Start_Click(object sender, EventArgs e)
        //{
        //    if (AspXmlUpload.HasFile)
        //    {
        //        //获取输入的xml文本
        //        byte[] buffer = AspXmlUpload.FileBytes;
        //        string str=System.Text.Encoding.UTF8.GetString(buffer);
        //        //string InputResult = m.FileInput(str);
        //        //使用service服务进行计算
        //        CoupleService MyService = new CoupleService();
        //        showinput.InnerText = MyService.Caculate_Return_Xml_String(str);

        //    }
            
        //}

        //public Task ShowMessage(string msg)
        //{
        //    //throw new NotImplementedException();
        //}

        //public Task ShowProcess(double process)
        //{
        //    //throw new NotImplementedException();
        //}
    }
}