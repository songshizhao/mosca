<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebApplication.aspx.cs" Inherits="MoscaWeb.WebApplication" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>MOSCA - 堆芯子通道热工分析程序</title>
    <link href="CSS/StyleSheet.css" rel="stylesheet" />
    <%--Jquery以及Jquery.UI--%>
    <script src="Scripts/jquery-1.6.4.js"></script>
    <script src="Scripts/jquery-ui.js"></script>
    <link href="Scripts/jquery-ui.css" rel="stylesheet" />
    <%--SIGNALR以及代理--%>
    <script src="Scripts/jquery.signalR-2.2.3.js"></script>
    <script src="signalr/hubs"></script>
    <style>
        .msg {
            margin-left:10px;font-size:14px;font-weight:300;
        }
    </style>
</head>
<body>
     <div id="header" style="width:100%;height:80px;background-color:black;color:white;">
        <a style="float:left;width:300px;height:40px;margin:20px;overflow:hidden">
      
                <img src="Assets/img/csp.png" style="width:38px;height:35px;margin-left:10px;float:left;"/>
                <span style="font-size:20px;font-weight:600;font-family:'Copperplate Gothic';line-height:40px;display:block;float:left;margin-left:25px;"> MOSCA</span>
        </a>


         <div class="item" style="float:right;">
             <a href="index.aspx">返回主页</a>
         </div>
      
    </div>
    <form id="form1" runat="server" style="margin:0;padding:0;">

        <div class="content-white">
            <h4 style="padding-left:20px;padding-top:20px;margin:0;">选择输入文件</h4>

            <input class="upload" style="margin-left:20px;" type="file" name="file" id="upload" />
            <input type="button" class="button" value="开始计算" onclick="BeginCaculate()" />

            <div id="msg-container" style="user-select:all;height:800px;overflow-y:scroll">
            </div>
        </div>
        
 
          
       
   
    </form>


    <script type="text/javascript">
        var reply;
        $(function () {
            // Declare a proxy to reference the hub. 
            reply = $.connection.replyHub;
            // Create a function that the hub can call to broadcast messages.
            reply.client.sendMessage = function (msg) {
                // Html encode display name and message. 
                var encodedMsg = $('<div />').text(msg).html();
                encodedMsg.search("\r\n",);
                encodedMsg = encodedMsg.replace(/\r\n/g,"<br />");
                $('#msg-container').append("<div class='msg'>" + encodedMsg + "</div>");
            };
            reply.client.arrival = function (msg) {
                // Html encode display name and message. 
                var encodedMsg = $('<div />').text(msg).html();
                // Add the message to the page. 
                $('#msg-container').append("<div class='msg'>" + encodedMsg + "</div>");
            };
            reply.client.showXmlResult = function (xmlstring) {

                $('#msg-container').append("<xmp style='color:white;background:black;'>" + xmlstring + "</xmp>");

            };




            // 
            $.connection.hub.start().done(function () {
                //告知服务器已经登陆，并从服务器获得登陆确认消息
                reply.server.arrival();

                //$('#sendmessage').click(function () {
                //    // Call the Send method on the hub. 
                //    chat.server.send($('#displayname').val(), $('#message').val());
                //    // Clear text box and reset focus for next comment. 
                //    $('#message').val('').focus();
                //});
            });
        });
    </script>

    <script>

        function BeginCaculate() {
            var f = document.getElementById('upload');
            var file = f.files[0];
            console.log(file)
            var reader = new FileReader();
            reader.readAsText(file);
            reader.onload = function(e) {
                //设置输入文件
                var input_string = e.target.result;
                //alert(input_string);
                if (input_string != "") {

                    reply.server.caculate(input_string);
                }
            }
                
           

           

        }

    </script>








</body>
</html>
