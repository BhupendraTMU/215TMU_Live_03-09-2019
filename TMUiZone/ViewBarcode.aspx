<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewBarcode.aspx.cs" Inherits="ViewBarcode" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        .auto-style1 {
            width: 153px;
        }
        .auto-style2 {
            width: 99px;
        }
        .auto-style3 {
            width: 133px;
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
function numberMobile(e){
    e.target.value = e.target.value.replace(/[^\d]/g,'');
    return false;
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
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table style="width: 100%">
            <tr>
                <td>

                 
                        <div style="position: relative" id="template">

                            <div id="header" style="background-color: #FFB106">
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

                                <div >



                                    <div class="w3-content" style="max-width: 100%;height:100%px;padding-left:40px">
                                        <table>
                                            <tr>
                                                <td style="vertical-align:top"><asp:Label ID="lblName" Text="Name" Font-Bold="true" Font-Size="40px" runat="server" Width="150px"></asp:Label> </td>
                                                 <td style="vertical-align:top"> <asp:Label ID="Label2" Text=":" Font-Bold="true" Font-Size="40px" runat="server" ></asp:Label></td>
                                                
                                                <td >
                                                  <asp:TextBox ID="txtName" runat="server" BackColor="White" TextMode="MultiLine" ForeColor="Blue" Font-Bold="true" Font-Size="40px" Width="680px" Enabled="false" Height="100px"></asp:TextBox>
                                                               </td>
                                                </tr>
                                           <tr>
                                               <td style="height:40px"></td>
                                           </tr>
                                                <tr>
                                                 <td style="vertical-align:top"><asp:Label ID="Label1" Text="Outsourse Agency" Font-Bold="true" Font-Size="40px" Width="300px" runat="server"></asp:Label></td>
                                                     <td style="vertical-align:top"> <asp:Label ID="Label5" Text=":" Font-Bold="true" Font-Size="40px" runat="server" ></asp:Label></td>
                                                    <td>
                                                    <asp:TextBox ID="txtOutSource" runat="server" BackColor="White" ForeColor="Blue" Font-Bold="true" Font-Size="40px" Width="680px" TextMode="MultiLine" Enabled="false" Height="100px"></asp:TextBox>
                                                               </td>
                                            </tr>
                                             <tr>
                                               <td style="height:40px"></td>
                                           </tr>
                                            <tr>

                                                <td style="vertical-align:top" ><asp:Label ID="Label3" Text="Category" Font-Bold="true" Font-Size="40px" runat="server"></asp:Label> </td>
                                                 <td style="vertical-align:top"> <asp:Label ID="Label4" Text=":" Font-Bold="true" Font-Size="40px" runat="server" ></asp:Label></td>
                                                <td>
                                                       <asp:TextBox ID="txtCategory" runat="server" BackColor="White" ForeColor="Blue" Font-Bold="true" Font-Size="40px" Width="680px" Enabled="false" Height="60px"></asp:TextBox>
                                                               </td>
                                                </tr>
                                             <tr>
                                               <td style="height:40px"></td>
                                           </tr>
                                              <tr>

                                                <td style="vertical-align:top" ><asp:Label ID="Label9" Text="Code" Font-Bold="true" Font-Size="40px" runat="server"></asp:Label> </td>
                                                 <td style="vertical-align:top"> <asp:Label ID="Label10" Text=":" Font-Bold="true" Font-Size="40px" runat="server" ></asp:Label></td>
                                                <td>
                                                       <asp:TextBox ID="txtCode" runat="server" BackColor="White" ForeColor="Blue" Font-Bold="true" Font-Size="40px" Width="680px" Enabled="false" Height="60px"></asp:TextBox>
                                                               </td>
                                                </tr>
                                             <tr>
                                               <td style="height:40px"></td>
                                           </tr>
                                                <tr>

                                                <td style="vertical-align:top"><asp:Label ID="lblPatch" Text="Date" Font-Bold="true" Font-Size="40px" runat="server"></asp:Label> </td>
                                                 <td style="vertical-align:top"> <asp:Label ID="Label7" Text=":" Font-Bold="true" Font-Size="40px" runat="server" ></asp:Label></td>
                                                <td>
                                                       <asp:TextBox ID="txtPatch" runat="server" MaxLength="5" BackColor="White" ForeColor="Blue" Font-Bold="true" Font-Size="40px" Width="680px" Enabled="false" Height="60px"></asp:TextBox>
                                                               </td>
                                                </tr>
                                             <tr>
                                               <td style="height:40px"></td>
                                           </tr>

                                             <tr>

                                                <td style="vertical-align:top"><asp:Label ID="Label6" Text="Quantity" Font-Bold="true" Font-Size="40px" runat="server"></asp:Label> </td>
                                                 <td style="vertical-align:top"> <asp:Label ID="Label8" Text=":" Font-Bold="true" Font-Size="40px" runat="server" ></asp:Label></td>
                                                <td>
                                                       <asp:TextBox ID="txtQTY" runat="server" placeholder="0.00" BackColor="White"  ForeColor="Blue" Font-Bold="true" Font-Size="40px" Width="680px" onkeyup="numberMobile(event);"  Height="60px"></asp:TextBox>
                                                               </td>
                                                </tr>

                                           <tr>
                                               <td style="height:40px"></td>
                                           </tr>


                                            <tr>
                                                <td ></td>
                                                 <td> </td>
                                                <td align="right">
                                                       <asp:Button ID="btnSubmit" Text="LOCK" Font-Bold="true" Font-Size="40px" BackColor="Green" runat="server" Height="70px"  Width="200px" OnClick="btnSubmit_Click"></asp:Button>
                                                         
                                                               </td>
                                                
                                                     </tr>   
                                                
                                                    
                                                               
                                        </table>
                                     
                                    </div>
                                    

                                  
                            </div>
                           
                            <br />

                        </div>
                       
                    </div>
                </td>
            </tr>

        </table>
      
        <div>
          
           
        </div>

       
    </form>
</body>
</html>
