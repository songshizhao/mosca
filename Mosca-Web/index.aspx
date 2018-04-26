<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="MoscaWeb.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>MOSCA - 堆芯子通道热工分析程序</title>
    <link href="style.css" rel="stylesheet" />
    <link href="CSS/StyleSheet.css" rel="stylesheet" />
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
    <style>

        .item {
            color: white;
            float: left;
        }

            .item :hover {
                background-color: #151212;
            }

        .content1 {
            -webkit-transition: all .3s ease-out;
            height: 1000px;
            background-image: url("Assets/img/background.png");
            background-color: #000;
        }
        .content2 {
            -webkit-transition: all .3s ease-out;
            height: 1000px;
            background-image: url("Assets/img/bg2.png");
            /*background-color: #000;*/
        }
        .content3 {
            -webkit-transition: all .3s ease-out;
            height: 1000px;
            background-image: url("Assets/img/bg3.png");
            /*background-color: #000;*/
        }
        .pub {
            left: 50%;
            top: 346px;
            margin-left: 100px;
            position: absolute;
            -webkit-transition: all .3s ease-out;
            color: white;
        }

        #download-btn {
            width: 256px;
            height: 50px;
            background: url("Assets/img/btnskin.png") no-repeat 0 0;
            cursor: pointer;
        }
            #download-btn:hover{
                background: url("Assets/img/btnskin.png") no-repeat 0 -50px;
 
            }

    </style>



</head>
<body>

    <div id="header" style="width:100%;height:80px;background-color:black;color:white;">
        <a style="float:left;width:300px;height:40px;margin:20px;overflow:hidden">
      
                <img src="Assets/img/csp.png" style="width:38px;height:35px;margin-left:10px;float:left;"/>
                <span style="font-size:20px;font-weight:600;font-family:'Copperplate Gothic';line-height:40px;display:block;float:left;margin-left:25px;"> MOSCA</span>
        </a>


        <div id="items" style="height:80px;margin-left:100px;float:left;">

            <div class="item">
                <a href="软件详细设计文档.pdf">开发文档</a>
            </div>
            <div class="item">
                <a href="子通道分析程序-说明.pdf">
                    用户手册
                </a>

            </div>
            <div class="item">
                <a href="ftp://10.1.12.170/site/tutorial/examples/">教程</a>
            </div>
            <div class="item">
                  <a href="CoupleService.asmx">服务</a>
            </div>


        </div>

        <div class="item" style="float: right">
            <a href="WebApplication.aspx">在线使用</a>
        </div>
      
    </div>
    <div class="content1">
        <img src="Assets/img/scp.jpg" style="height: 292px; width: 555px; margin-left: 56px; margin-top: 278px" />

        <div class="pub">
            <p style="font-size: 30px;">
                反应堆堆芯子通道热工水力分析程序MOSCA
            </p>



            <span style="font-size: 40px; color: yellow;">Model Objectified Sub Channel Analysis</span>

            <br />

            <p style="font-size: 10px;">
                MOSCA使用C#语言以模块化开发方式开发,完全自主化,极大提高了软件的可维护性和耦合性, 使用xml输入和输出增强了可识别性和后处理能力
            </p>
            <a id="download-btn" href="../Mosca-PC\bin\Release\COBRA.exe">

            </a>
<%--            <a class="myButton" href="../Mosca-PC\bin\Release\COBRA.exe"></a>--%>
        </div>


    </div>
    
    <div class="content2">
        <img src="Assets/img/divide.png" style="margin-left: 120px; margin-top: 298px" />
        
    </div>

    <div class="content3">
        <img src="Assets/img/pwdistribution.png" style="margin-left: 71px; margin-top: 298px;width:461px; height:395px;" />
    </div>



</body>
</html>
