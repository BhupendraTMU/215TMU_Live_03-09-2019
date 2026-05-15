<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS1/Style.css" rel="stylesheet" type="text/css" />
    <link href="CSS1/maincss.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .button-style {
            display: inline-block;
            padding: 10px 20px;
            background-color: #007bff; /* Blue background */
            color: white; /* White text */
            text-align: center;
            text-decoration: none; /* Remove underline */
            border-radius: 5px; /* Rounded corners */
            border: 1px solid #007bff;
            cursor: pointer; /* Indicate clickable */
        }

            .button-style:hover {
                background-color: #0056b3; /* Darker blue on hover */
                border-color: #0056b3;
            }


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

                            <div id="header" style="background-color: #FFB106">
                                <table cellpadding="0px" cellspacing="0px" width="100%">
                                    <tr>
                                        <td><a href="index.aspx">
                                            <br />
                                            <img src="images/t.jpg" alt="TMU i-Zone Logo" style="border-width: 10px; float: left; width: 237px; height: 70px" /></a> </td>
                                        <td>
                                            <table cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <td align="right">
                                                        <div style="font-size: 40px; color: white;">&nbsp TEERTHANKER MAHAVEER UNIVERSITY &nbsp</div>
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
                                            <asp:Image ID="Image1" AlternateText="TMU logo" runat="server" ImageUrl="~/images/rightlogo.png" Width="150px" Height="120px" /></td>

                                    </tr>
                                </table>
                            </div>
                            <div id="loginwarpper">
                                <div id="col-left">
                                    <div id="modal-loginbox">
                                        <div id="contents" style="height: 380px">

                                            <div style="width: 90%; height: 70px; font-family: Century Schoolbook;">

                                                <br />
                                                <h1 style="white-space: pre; width: 80px; float: left; line-height: 35px; color: white; font-family: Century Schoolbook; padding-left: 13%; padding-top: 4%">Login</h1>
                                                <span style="float: left; padding-top: 7%; padding-left: 6%; font-family: Century Schoolbook; color: white; font-weight: bold;">Student | Faculty</span>
                                            </div>
                                            <br clear="all" />


                                            <div class="form-elements">
                                                <div class="label-txt" style="width: 500px">

                                                    <asp:Label ID="lblHeader" runat="server" Style="width: 450px" Text="User Name/Enrollment No" Font-Size="15pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
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

                                                    <asp:Label ID="Label1" runat="server" Text="Password" Font-Size="15pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                                    <label style="text-align: left">
                                                        <asp:RequiredFieldValidator ID="rfvtxtTillDate" runat="server" ControlToValidate="txtpassword" ErrorMessage="*" BackColor="White" ValidationGroup="get"></asp:RequiredFieldValidator>
                                                    </label>
                                                </div>
                                                <input id="Text4" type="text" style="display: none;" />
                                                <br />
                                                <br />
                                                <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" Width="90%" Height="30px" Style="font-size: 20px; border-radius: 5%;"></asp:TextBox>
                                                <input id="Text6" type="text" style="display: none" />


                                            </div>

                                            <br clear="all" />
                                            <br />
                                            <b>
                                                <asp:CheckBox ID="chkrem" runat="server" value="Remember Me" name="Remember Me" />
                                                <asp:Label ID="Label2" runat="server" Text="Remember Me" Font-Size="12pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                                            </b>

                                            <div style="float: right; padding-right: 10%">

                                                <asp:LinkButton ID="LnkGetPwd" runat="server"><b><font color="#FF0000"><img width="12px" height="12px" border="0" src="images/6anipt3c.gif"> Get Password ?</font></b></asp:LinkButton>
                                            </div>

                                            <div style="padding-right: 10%;">
                                                <div style="text-align: center;">
                                                    <br />
                                                    <asp:Button ID="ImgBttn_Login" runat="server" Width="100%" BackColor="#990033" Height="40px" Font-Size="20px" ForeColor="White" ValidationGroup="get" Style="border-radius: 5%; font-family: Century Schoolbook;" Font-Bold="true" Text="Login" OnClick="ImgBttn_Login_Click1" />
                                                </div>
                                                <asp:LinkButton ID="lnkPay" runat="server" ForeColor="Red" OnClick="lnkPay_Click" Visible="false" Text="Please Click here to pay fees without Enrollment No."></asp:LinkButton>

                                                <asp:LinkButton ID="lnkMedicalLogin" runat="server" ForeColor="Red" PostBackUrl="http://14.139.238.132:90/" Text="Click here to Login Medical/Dental Academic Portal"></asp:LinkButton>



                                                <%--<asp:ImageButton ID="ImgBttn_Login"  runat="server" Width="130px" Height="40px" ImageUrl="~/images/login.jpg" onclick="ImgBttn_Login_Click" />
                                                --%>
                                            </div>
                                            <%--<a href="#" onclick="window.open('Forgotpassword.aspx','mywindow','width=400,height=385')"><b><font color="#FF0000"><img width="12px" height="12px" border="0" src="images/6anipt3c.gif"> Get Password ?</font></b></a> <br>--%>
                                            <br />


                                        </div>
                                        <div>
                                            <asp:Label ID="lblHRUserId" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <%--<div id="amizonemobile"> comment by veerendra
<div id="top-mobile"></div>
<div id="contents-mobile">
<div id="innercontents" style="height:275px">
    <asp:Image ID="Image3" runat="server" Height="275px" Width="334px" ImageUrl="~/images/add.png"/>
</div>





</div>
<div id="bottom-mobile"></div>

</div>--%>

                                    <!-- <div id="copyright">
Copyright ©2011 <span class="brown">Amity</span>. All rights reserved. 
<br />
Help : <a href="mailto:amizone@amity.edu" class="brown">amizone@amity.edu</a>
<br />
Powered by AKC Data Systems (India) Pvt. Ltd.
</div> -->
                                </div>

                                <div id="col-right">



                                    <div class="w3-content" style="max-width: 100%">
                                        <div class="mySlides w3-container w3-red">
                                            <%--<h1><b>TMU UNIVERSITY, Moradabad Campus...</b></h1>
      <br />--%>
                                            <asp:Image ID="Image6" runat="server" BorderWidth="4px" AlternateText="TMU ERP Login Portal" BorderColor="White" ImageUrl="~/images/TMU Image.jpg" Width="100%" Style="border-radius: 5%" Height="420px" />
                                            <%--<h1><b>Did You Know?</b></h1>
    <h1><i>We plan to sell trips to the moon in the 2020s</i></h1>--%>
                                        </div>

                                        <asp:Image ID="Image4" class="mySlides" runat="server" BorderWidth="4px" AlternateText="TMU ERP Login Portal" BorderColor="White" ImageUrl="~/images/tmu-camp.jpg" Width="100%" Style="border-radius: 5%" Height="420px" />


                                        <div class="mySlides w3-container w3-xlarge w3-white w3-card-8">
                                            <%--<h1><b>TMU UNIVERSITY, Welcome</b></h1>
      <br />--%>
                                            <asp:Image ID="Image3" runat="server" BorderWidth="4px" AlternateText="TMU ERP Login Portal" BorderColor="White" ImageUrl="~/images/TMU Image.jpg" Width="100%" Style="border-radius: 5%" Height="420px" />

                                        </div>
                                        <asp:Image ID="Image2" class="mySlides" runat="server" BorderWidth="4px" AlternateText="TMU ERP Login Portal" BorderColor="White" ImageUrl="~/images/TMU1-img.jpg" Width="100%" Style="border-radius: 5%" Height="420px" />
                                        <%--<img class="mySlides" src="img_manarola.jpg" style="width:100%">--%>
                                    </div>
                                    <script>
                                        var slideIndex = 0;
                                        carousel();

                                        function carousel() {
                                            var i;
                                            var x = document.getElementsByClassName("mySlides");
                                            for (i = 0; i < x.length; i++) {
                                                x[i].style.display = "none";
                                            }
                                            slideIndex++;
                                            if (slideIndex > x.length) { slideIndex = 1 }
                                            x[slideIndex - 1].style.display = "block";
                                            setTimeout(carousel, 2000);
                                        }
                                    </script>

                                    <%--    <table> comment by veerendra
        <tr>
            <td><asp:Image ID="Image2" runat="server" ImageUrl="~/images/TMU1-img.jpg" Width="50%" Height="420px"/><asp:Image ID="Image4" runat="server" ImageUrl="~/images/tmu-camp.jpg" Width="50%" Height="420px"/></td>
        </tr>
        <%--<tr>
            <td><asp:Image ID="Image4" runat="server" ImageUrl="~/images/tmu-camp.jpg" Width="530px" Height="275px"/></td>
        </tr>
    </table>--%>
                                    <%--<asp:Image ID="Image2" runat="server" ImageUrl="~/images/TMU Newss.png" Width="430px" Height="550px"/>--%>
                                </div>
                                <%--<img width="100%" height="437" style="margin:20px 0 0 15px;" alt="" src="images/footer copy.jpg"> comment for below space
    <br />    
	<p align="center">Copyright &copy; 2015 <span class="brown"><a href="http://tmu.ac.in/" target="_blank"> TMU</a> </span>. All rights reserved.<br /> Powered by <a href="http://www.corporateserve.com/" target="_blank">  Corporateserve Solutions Pvt Ltd </a></p>  
                                --%>
                            </div>
                            <table style="text-align: center; width: 100%">
                                <tr>
                                    <td>Follow TMU@
                                    </td>
                                    <td>
                                        <a href="https://www.youtube.com/@TeerthankerMahaveerUniversity" target="_blank">
                                            <asp:Image ID="lnkYout" runat="server" ImageUrl="~/images/Youtube_logo.png" Width="45px" Height="35px" />


                                        </a>
                                    </td>
                                    <td>
                                        <a href="https://www.facebook.com/tmumbd/" target="_blank">
                                            <asp:Image ID="Image5" runat="server" ImageUrl="~/images/fb logo.jpg" Width="45px" Height="35px" />


                                        </a>
                                    </td>
                                    <td>
                                        <a href="https://www.instagram.com/tmu_mbd/" target="_blank">
                                            <asp:Image ID="Image7" runat="server" ImageUrl="~/images/Instagram_logo.png" Width="45px" Height="35px" />


                                        </a>
                                    </td>
                                    <td>
                                        <a href="https://twitter.com/Tmumbd" target="_blank">
                                            <asp:Image ID="Image8" runat="server" ImageUrl="~/images/x.png" Width="45px" Height="35px" />


                                        </a>
                                    </td>
                                    <td>
                                        <a href="https://in.pinterest.com/tmumbd/" target="_blank">
                                            <asp:Image ID="Image9" runat="server" ImageUrl="~/images/Pinterest-logo.png" Width="45px" Height="35px" />


                                        </a>
                                    </td>
                                    <td>
                                        <a href="https://www.quora.com/profile/Teerthanker-Mahaveer-University-2" target="_blank">
                                            <asp:Image ID="Image10" runat="server" ImageUrl="~/images/quora logo.png" Width="45px" Height="35px" />


                                        </a>
                                    </td>
                                    <td>
                                        <a href="https://www.threads.net/@tmu_mbd" target="_blank">
                                            <asp:Image ID="Image11" runat="server" ImageUrl="~/images/threads logo.png" Width="45px" Height="35px" />


                                        </a>
                                    </td>
                                    <td>
                                        <a href="https://whatsapp.com/channel/0029Va5aFcvIyPtT9FN1AW2W" target="_blank">
                                            <asp:Image ID="Image12" runat="server" ImageUrl="~/images/WhatsApp logo.png" Width="45px" Height="35px" />


                                        </a>
                                    </td>
                                    <%-- <td>
                                        <a href="https://chat.whatsapp.com/C43Cb215JgJ995NpPrzZzo" target="_blank">
                                            <asp:Image ID="Image13" runat="server" ImageUrl="~/images/WhatsApp logo.png" Width="35px" Height="25px" />


                                        </a>
                                    </td>--%>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:HyperLink ID="lnkbarcode" runat="server" CssClass="button-style" Text="SCAN BAR CODE" NavigateUrl="Scanner.aspx"></asp:HyperLink>
                                    </td>
                                </tr>
                            </table>


                            <img width="96%" height="437" style="margin: 20px 0 0 15px;" alt="TMU Awards & Recognition Banner" src="images/footer copy.jpg">
                            <br />
                            <p align="center">
                                Copyright &copy; 2021 <span class="brown"><a href="http://tmu.ac.in/" target="_blank">TMU</a> </span>. All rights reserved.<br />
                                Powered by <a href="https://www.tmu.ac.in/" target="_blank">Teerthanker Mahaveer University </a>
                            </p>

                        </div>
                        <%--<div id="footer"></div> --%>
                    </div>
                </td>
            </tr>

        </table>
        <%-- <asp:UpdatePanel runat="server" ID="updmain">
             <ContentTemplate>--%>
        <div>
            <%--<asp:Button text="" id="lnkFollowUP" runat="server" />--%>
            <asp:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="LnkGetPwd"
                CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="pnlPopup">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                <div class="body">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center">
                                <div><b>Password Recovery</b></div>
                            </td>
                            <td align="right" width="30%">
                                <div style="text-align: right">
                                    <asp:Button ID="btnClose" runat="server" Text="X" Width="30px" CssClass="button" OnClientClick="javascript:return fun();" />
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <div style="text-align: center">
                                    <asp:Label runat="server" ID="lblMsg" ForeColor="Red" Font-Bold="true"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%"><b><b>User Id : </b></td>
                            <td style="width: 70%" align="left">
                                <asp:TextBox ID="txtLoginUserId" runat="server" MaxLength="20" />
                                <asp:RequiredFieldValidator ControlToValidate="txtLoginUserId" ID="rfvtxtLoginUserId" SetFocusOnError="true" ErrorMessage="*" ForeColor="RED" runat="server" ValidationGroup="vgGetPwd"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr style="height: 5px">
                            <td style="width: 20%" colspan="2"></td>
                        </tr>
                        <tr>
                            <td style="width: 20%" colspan="2">
                                <br />
                                <b>
                                    <asp:Label ID="lblmob" runat="server" ForeColor="#ff9900" Text="10 Digit Registered Mobile No Without Prefix ( +91 OR 0 )" /></b>
                                <br />
                            </td>
                        </tr>
                        <tr style="height: 5px">
                            <td style="width: 20%" colspan="2"></td>
                        </tr>
                        <tr>
                            <td style="width: 30%"><b><b>Mobile No : </b></td>
                            <td style="width: 70%" align="left">
                                <asp:TextBox ID="txtMobileNo" runat="server" MaxLength="10" onkeypress="return NumberOnly()"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtMobileNo" ID="rvftxtMobileNo" SetFocusOnError="true" ErrorMessage="*" ForeColor="RED" runat="server" ValidationGroup="vgGetPwd"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rfvMobileNo" runat="server" SetFocusOnError="true" ControlToValidate="txtMobileNo" ErrorMessage="*" ForeColor="RED" ValidationGroup="vgGetPwd"
                                    ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td align="left">
                                <asp:Button ID="btnSave" runat="server" Text="Get Password" Height="35px" Width="93px" ValidationGroup="vgGetPwd" OnClick="btnSave_Click" /></td>
                        </tr>
                    </table>

                </div>

            </asp:Panel>
        </div>

        <div id="link" runat="server" visible="true" style="background-color: white;">
            <%--//veerendra--%>
            <asp:Button Text="" ID="btnLog" runat="server" />
            <asp:ModalPopupExtender ID="mpeLog" runat="server" PopupControlID="pnlPopLog" TargetControlID="btnLog"
                CancelControlID="btnClosee" Drag="true" PopupDragHandleControlID="pnlPopLog">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlPopLog" runat="server" CssClass="modalPopup" Style="display: none">
                <div class="body">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center">
                                <div style="text-align: center">
                                    <center><span style="float: left; padding-left: 180px; padding-top: 13px; color: #990033; font-weight: bold; font-size: x-large">College</span>  </center>
                                </div>
                                <br />
                            </td>
                            <td align="right" width="20%">
                                <div style="text-align: right">
                                    <asp:Button ID="btnClosee" runat="server" Text="X" Width="20px" CssClass="button" OnClientClick="javascript:return fun();" />
                                </div>

                            </td>
                        </tr>
                        <tr style="height: 5px">
                            <td style="width: 20%" colspan="2"></td>
                        </tr>
                        <tr>
                            <td style="width: 30%" align="center" colspan="2"><b></b></td>
                        </tr>
                        <tr>
                            <td style="width: 70%" align="left" colspan="2">
                                <asp:DropDownList ID="ddlCollege" runat="server" MaxLength="20" Height="35px" />
                                <asp:RequiredFieldValidator ControlToValidate="ddlCollege" ID="rfvddlCollege" SetFocusOnError="true" ErrorMessage="*" ForeColor="RED" runat="server"
                                    ValidationGroup="vgCollege"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr style="height: 5px">
                            <td style="width: 20%" colspan="2"></td>
                        </tr>
                        <tr align="right">
                            <td align="right" colspan="2">
                                <div style="text-align: right">
                                    <asp:ImageButton ID="btnCollege" runat="server" ImageUrl="~/images/login.jpg" ValidationGroup="vgCollege" OnClick="btnCollege_Click" />
                                    <%--<asp:Button ID="btnCollege" runat="server" Text="Login" Height="35px" Width="93px" ValidationGroup="vgCollege" OnClick="btnCollege_Click" />--%>
                                </div>
                            </td>
                        </tr>
                    </table>

                </div>

            </asp:Panel>
        </div>
    </form>
</body>
</html>
