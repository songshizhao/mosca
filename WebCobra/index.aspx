<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="MoscaWeb.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="style.css" rel="stylesheet" />
    <title>Cobra远程调用</title>
</head>
<body>

    <div id="message" runat="server" style="width:100%;height:30px;background-color:black;text-align:center;color:white;display:none;" onclick="HideMessage()">

    </div>
    <form id="form1" runat="server">

    <div style="width:800px;border:solid red 1px;margin:0 auto;padding:10px;">
        <a href="userguid.aspx">文档</a>
        <a href="ftp://10.1.12.170/site/tutorial/examples/">示例</a>
        <a href="service.asmx">服务</a>


        <h1>子通道跑程序远程手动调用测试</h1>
        <h4>手动上传输入文件</h4>
         <asp:FileUpload ID="AspXmlUpload" runat="server" ToolTip="上传输入文件"/>
        <asp:Button ID="Btn_Start" runat="server" OnClick="Btn_Start_Click" Text="开始上传" OnClientClick="uploadbegin"/>
        <div style="overflow:scroll;display:block;height:1000px;width:100%;" runat="server" id="showinput"></div>

        <%--<input id="htmlupload" type="file" title="上传输入文件" onchange="uploadchanged(this.files)"/>--%>
    </div>
    </form>


    <script>

        var msgbar = document.getElementById('message');
        var aspupload = document.getElementById('AspXmlUpload');

        function ShowMessage(msg) {
            msgbar.innerText = msg;
            msgbar.style.display = 'block';
        }

        function HideMessage() {
            msgbar.style.display = 'none';
        }

        function uploadbegin() {
            ShowMessage('正在上传...');
        }

    </script>
</body>
</html>
