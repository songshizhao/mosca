using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CobraDLL;

namespace WebCobra
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

           
        }


        protected void Btn_Start_Click(object sender, EventArgs e)
        {
            if (AspXmlUpload.HasFile)
            {
                //获取输入的xml文本
                byte[] buffer = AspXmlUpload.FileBytes;
                string str=System.Text.Encoding.UTF8.GetString(buffer);
                //string InputResult = m.FileInput(str);
                //使用service服务进行计算
                service MyService = new service();
                showinput.InnerText = MyService.All(str);


            }
            
        }
    }
}