<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlineFeePayment.aspx.cs" Inherits="OnlineFeePayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <link href="CSS1/Style.css" rel="stylesheet" type="text/css" />
    <link href="CSS1/maincss.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .mySlides {
            display: none;
        }

        textarea {
            font-size: 28px;
        }


        .modalPopup {
            background-color: #FFB106;
            border-radius: 5%;
            border-width: 3px;
            border-style: solid;
            border-color: white;
            padding: 3px;
            width: 460px;
        }

        [type="checkbox"] {
            vertical-align: middle;
        }

        login1 {
            float: right;
            display: block;
            padding-left: 5px;
            padding-right: 5px;
            border-color: white;
        }

        .modalPopup .header {
            background-color: #2FBDF1;
            height: 10px;
            color: White;
            line-height: 10px;
            text-align: center;
            font-weight: bold;
        }

        .modalPopup .body {
            min-height: 50px;
            line-height: 10px;
            text-align: center;
            padding: 5px;
        }

        .modalPopup .footer {
            padding: 3px;
        }

        .modalPopup .button {
            height: 30px;
            color: White;
            line-height: 10px;
            text-align: center;
            font-weight: bold;
            cursor: pointer;
            background-color: red;
            border: 1px solid #5C5C5C;
        }

        .modalPopup td {
            text-align: left;
        }
    </style>







    <script type="text/javascript">
        $(document).ready(function () {

            $('[id$=btnSave]').click(function () {
                $('[id$=txtLoginUserId]').css('border-color', 'red');
                $('[id$=txtMobileNo]').css('border-color', 'red');
                var a = '';
                if ($('[id$=txtLoginUserId]').val() == '') { a = 'false'; } else $('[id$=txtLoginUserId]').css('border-color', '');
                if ($('[id$=txtMobileNo]').val().length < 10) { a = "false" } else $('[id$=txtMobileNo]').css('border-color', '');
                if (a == "false") {
                    return false;
                }

            });
        });
    </script>
    <script type="text/javascript">
        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else
                event.returnValue = false;
        }

        function fun() {
            debugger
            alert($('[id$=lblMsg]').innerText);
            //$('[id$=lblMsg]').text='';
            // document.getElementById("lblMsg").value = "";
            alert("a");

        }
    </script>

</head>

<body>
    <%--<body style="background: url('images/TMU Image.jpg') no-repeat center center fixed; -moz-background-size: cover;-webkit-background-size: cover;-o-background-size: cover;background-size: cover;">--%>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table style="width: 100%">
            <tr>
                <td>

                    <div id="wrap1">
                        <div style="position: relative" id="template">

                            <div id="r" style="background-color: #FFB106">
                                <table cellpadding="0px" cellspacing="0px" width="100%">
                                    <tr>
                                        <td><a href="index.aspx">
                                            <br />
                                            <img src="images/t.jpg" style="border-width: 10px; float: left; width: 237px; height: 70px" /></a> </td>
                                        <td>
                                            <table cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <td align="right">
                                                        <div style="font-size: 40px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; तीर्थंकर महावीर विश्वविद्यालय   </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div style="font-size: 15px; color: #fff">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; An Ultimate Destination For World Class Education</div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>

                                        <td align="right">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/rightlogo.png" Width="65%" /></td>

                                    </tr>
                                </table>
                            </div>
                            <div id="loginwarpper">
                                <div id="col-left">
                                    <div id="modal-loginbox">
                                        <div id="contents" style="height: 380px">

                                            <div style="width: 90%; height: 70px; font-family: Century Schoolbook;">

                                                <br />
                                                <h1 style="white-space: pre; width: 80px; float: left; line-height: 35px; color: white; font-family: Century Schoolbook; padding-left: 13%; padding-top: 4%">View Details</h1>
                                                <span style="float: left; padding-top: 7%; padding-left: 6%; font-family: Century Schoolbook; color: white; font-weight: bold;"></span>
                                            </div>
                                            <br clear="all" />


                                            <div class="form-elements">
                                                <div class="label-txt">

                                                    <asp:Label ID="lblHeader" runat="server" Style="padding-left: 0%" Text="Student No." Font-Size="15pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                                    <label style="text-align: left">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserid" ErrorMessage="*" SetFocusOnError="true" BackColor="White" ValidationGroup="get"></asp:RequiredFieldValidator></label>
                                                </div>

                                                <input id="Text3" type="text" style="display: none" />

                                                <br />
                                                <br />
                                                <asp:TextBox ID="txtUserid" runat="server" Width="90%" Height="30px" Style="font-size: 20px; border-radius: 5%; text-transform: uppercase" MaxLength="20"></asp:TextBox>
                                            </div>
                                            <div class="form-elements">

                                                <div class="label-txt">

                                                    <asp:Label ID="Label1" runat="server" Text="Date of Birth" Style="padding-left: 0%" Font-Size="15pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                                    <label style="text-align: left">
                                                        <asp:RequiredFieldValidator ID="rfvtxtTillDate" runat="server" ControlToValidate="txtpassword" ErrorMessage="*" BackColor="White" ValidationGroup="get"></asp:RequiredFieldValidator>
                                                    </label>
                                                </div>
                                                <input id="Text4" type="text" style="display: none;" />
                                                <br />
                                                <br />
                                                <asp:TextBox ID="txtpassword" runat="server" Width="90%" Height="30px" Style="font-size: 20px; border-radius: 5%;" onkeydown="return false;"></asp:TextBox>
                                                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtpassword" Format="dd MMM yyyy">
                                </asp:CalendarExtender>


                                            </div>

                                            <br clear="all" />
                                            <br />



                                            <div style="padding-right: 10%;">
                                                <div style="text-align: center;">
                                                    <br />
                                                    <asp:Button ID="btnView" OnClick="btnView_Click" runat="server" Width="100%" BackColor="#990033" Height="40px" Font-Size="20px" ForeColor="White" ValidationGroup="get" Style="border-radius: 5%; font-family: Century Schoolbook;" Font-Bold="true" Text="View Details" />

                                                </div>
                                                <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
                                                <%--<asp:ImageButton ID="ImgBttn_Login"  runat="server" Width="130px" Height="40px" ImageUrl="~/images/login.jpg" onclick="ImgBttn_Login_Click" />
                                                --%>
                                            </div>
                                            <%--<a href="#" onclick="window.open('Forgotpassword.aspx','mywindow','width=400,height=385')"><b><font color="#FF0000"><img width="12px" height="12px" border="0" src="images/6anipt3c.gif"> Get Password ?</font></b></a> <br>--%>
                                            <br />


                                        </div>

                                    </div>

                                </div>

                                <div id="col-right">



                                    <div class="w3-content" style="max-width: 100%;height:500px;overflow:scroll" id="mainDiv" runat="server" visible="false">
                                        <div class="form-group clearfix" >
                                            <h1>Online Fee Payment</h1>
                                            <table>
                                                
                                                 <tr>
                                                    <td style="height:30px"></td>
                                                </tr>
                                                <tr>


                                                    <td valign="top">
                                                        <label id="lblsem" style="font-weight: bold;width:50px">Student Name:</label>
                                                    </td>
                                                    <td style="width: 4px"></td>
                                                    <td>
                                                        <asp:Label ID="lblStudentName" Width="200px" runat="server" >
                                                        </asp:Label>
                                                    </td>
                                                    <td style="width: 15px"></td>
                                                    <td valign="top">
                                                        <label id="lblExam" style="font-weight: bold;width:50px">Father's Name:</label>
                                                    </td>
                                                    <td style="width: 4px"></td>
                                                    <td>
                                                        <asp:Label ID="lblFather" Width="200px" runat="server" >
                                                            
                                                        </asp:Label>
                                                    </td>
                                                    <td style="width: 15px"></td>
                                                    </tr>
                                                <tr>
                                                    <td style="height:20px"></td>
                                                </tr>
                                                    <tr>
                                                    <td >
                                                        <label id="lblCou" style="font-weight: bold;width:50px" runat="server" >Course Name:</label>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Label ID="lblCourse" Width="200px" runat="server" >
                                                        </asp:Label>
                                                    </td>
                                                        <td style="width: 15px"></td>
                                                    <td valign="top">
                                                        <label id="lblCollege" style="font-weight: bold;width:40px">College Name:</label>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Label ID="lblCollege" Width="400px" runat="server" >
                                                            
                                                        </asp:Label>
                                                        <asp:HiddenField ID="hfSem" runat="server" />
                                                        <asp:HiddenField ID="hfyear" runat="server" />
                                                    </td>
                                                    <td style="width: 15px"></td>                                                
                                                   
                                                </tr>
                                                <tr>
                                                    <td style="height:20px"></td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        <label id="lblAmount" style="font-weight: bold;width:50px" runat="server" >Amount:</label>
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtamount" Text="0.00" onkeydown="return NumberOnly();" Width="200px" runat="server" >
                                                        </asp:TextBox>
                                                    </td>
                                                        <td style="width: 15px"></td>
                                                    <td valign="top">
                                                        
                                                    </td>
                                                    <td style="width: 6px"></td>
                                                    <td>
                                                        <asp:Button ID="btnPay" Text="Pay" OnClick="btnPay_Click"  Width="140px" runat="server" >
                                                            
                                                        </asp:Button>
                                                    </td>
                                                    <td style="width: 15px"></td>                                                
                                                   
                                                </tr>

                                            </table>


                                        </div>


                                    </div>
                                    <div runat="server" id="DivMsg" visible="false">

                                    </div>
                                   


                                </div>

                                <br />

                            </div>

                        </div>
                </td>
            </tr>

        </table>

        <p align="center">Copyright &copy; 2018 <span class="brown"><a href="http://tmu.ac.in/" target="_blank">TMU</a> </span>. All rights reserved. Powered by <a href="https://www.tmu.ac.in/" target="_blank">Teerthanker Mahaveer University </a></p>



    </form>
</body>
</html>
