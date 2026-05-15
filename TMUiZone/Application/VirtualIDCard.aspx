<%@ Page Title="" Language="C#" MasterPageFile="~/Application/IndexMaster.master" AutoEventWireup="true" CodeFile="VirtualIDCard.aspx.cs" Inherits="Application_VirtualIDCard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<script src="https://cdn.jsdelivr.net/npm/es6-promise@4/dist/es6-promise.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/es6-promise@4/dist/es6-promise.auto.min.js"></script>
    <script src="https://html2canvas.hertzen.com/dist/html2canvas.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://html2canvas.hertzen.com/dist/html2canvas.min.js"></script>
    <script type="text/javascript">

        function ConvertToImage(btnExport) {


           

         
          

            html2canvas($("#Div1")[0], {
                width: 1024,
                height: 1024,
                scale: 1
            }
                )


                .then(function (canvas) {
                var base64 = canvas.toDataURL();
                $("[id*=hfImageData]").val(base64);
                __doPostBack(btnExport.name, "");
            });
            return false;
        }


       

        function PrintDiv() {

            var divToPrint = document.getElementById('Div1');
            //document.getElementById('PanelQuotation').style.visibility = true;
            //document.getElementById('PanelQuotation').style.visibility = 'Visible';

            document.getElementById('<%=lblVID.ClientID %>').style.fontSize = "25px";
            document.getElementById('<%=PanelQuotation.ClientID %>').style.display = 'block'
            var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px, margin:0mm');
            popupWin.document.open();
            popupWin.document.write('<html><body style="border-left: solid;border-right:solid;border-top:solid;border-bottom:solid;height:425px; border-width: 2px" onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
        }
    </script>
    <style type="text/css">
        .upCase {
            text-transform: uppercase;
        }

        .myTableClass tr th {
            padding: 1px;
        }

        tr td {
            padding: 1px;
        }



        .style1 {
            height: 800px;
        }

        a.greenButton {
            color: #000;
            text-decoration: none;
            margin: 20px;
            padding: 10px 20px 10px 20px;
            display: inline-block;
        }

            a.greenButton:hover {
                background-color: #5078B3;
            }

        .modalbackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .modalPopupWhite {
            background-color: #ffffff;
            padding: 3px;
            width: 550px;
            height: 400px;
        }

        .modalprogress {
            opacity: 0.7;
            filter: alpha(opacity=60);
            background-color: #ededed;
        }

        .btnaddmaincattt {
            color: #fff;
            text-decoration: none;
            padding: 10px 20px 10px 20px;
            display: inline-block;
            font-weight: bold;
            background-color: #5078B3;
            cursor: pointer;
            border-radius: 10px;
        }

        .hidden {
            display: none;
        }

        .block1 {
            visibility: visible;
        }

        .redBorder {
            border: 1px solid red;

        }

        .loader {
            position: fixed;
            left: 45%;
            top: 45%;
            width: 100px;
            height: 100px;
            z-index: 9999;
            background: url('../images/loader.gif') 50% 50% no-repeat rgb(249,249,249);
        }

        tr.spaceUnder {
            padding-bottom: 5px;
        }

        .example-print {
            display: none;
        }

        @media print {
            .example-screen {
                display: none;
            }

            .example-print {
                display: block;
            }
        }


        .example-print1 {
            display: block;
        }

        @media print1 {
            .example-screen {
                display: block;
            }

            .example-print1 {
                display: none;
            }
        }

        .auto-style1 {
            height: 17px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="View Virtual ID Card" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>

   


        <fieldset id="fsParent" class="boxBody" runat="server">
            <table id="tblParent" runat="server">
                  <tr>
                    <td colspan="4">
                        <asp:Label ID="Label14" runat="server" ForeColor="Blue" Font-Underline="true"  Height="30px" Width="550px" Font-Bold="true" Font-Size="Large" Text="Father Mobile Number Verification"></asp:Label>

                    </td>
                </tr>
                <tr>
                    <td style="width: 150px">
                        <asp:Label ID="Label7" runat="server" Height="30px" Width="150px" Font-Bold="true" Font-Size="Medium" Text="Confirm OTP :"></asp:Label>

                    </td>

                    <td style="width: 250px; vertical-align: top">
                        <asp:TextBox ID="TextBox1" runat="server" Height="30px" Width="80px"></asp:TextBox>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                     <asp:Button ID="Button2" runat="server" Text="Confirm" Height="30px" Width="100px" Font-Bold="true" BackColor="Blue" ForeColor="White" Font-Size="Medium" OnClick="Button2_Click"></asp:Button>
                    </td>
                    <td style="width: 100px">
                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Generate OTP" ForeColor="Green" Height="30px" Width="150px" Font-Bold="true" Font-Size="Medium" OnClick="LinkButton1_Click"></asp:LinkButton>
                    </td>
                    <td style="width: 100px">
                        <asp:Label ID="Label11" runat="server" Text="" ForeColor="Green" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="Label13" runat="server" ForeColor="Red" Visible="false"></asp:Label>

                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset id="fsStudent" class="boxBody"  runat="server" >
        <table>
             <tr>
                    <td colspan="4">
                        <asp:Label ID="Label15" runat="server" ForeColor="Blue" Font-Underline="true"  Height="30px" Width="550px" Font-Bold="true" Font-Size="Large" Text="Student Mobile Number Verification"></asp:Label>

                    </td>
                </tr>
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="lblotp" runat="server" Height="30px" Width="150px" Font-Bold="true" Font-Size="Medium" Text="Confirm OTP :"></asp:Label>

                </td>

                <td style="width: 250px">
                    <asp:TextBox ID="txtOTP" runat="server" Height="30px" Width="80px"></asp:TextBox>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                     <asp:Button ID="btnSubmit" runat="server" Text="Confirm" Height="30px" Width="100px" Font-Bold="true" BackColor="Blue" ForeColor="White" Font-Size="Medium" OnClick="btnSubmit_Click"></asp:Button>
                </td>
                <td style="width: 100px">
                    <asp:LinkButton ID="Label2" runat="server" Text="Generate OTP" ForeColor="Green" Height="30px" Width="150px" Font-Bold="true" Font-Size="Medium" OnClick="Label2_Click"></asp:LinkButton>
                </td>
                <td style="width: 100px">
                    <asp:Label ID="lblMSG" runat="server" Text="" ForeColor="Green" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblMSGOTP" runat="server" ForeColor="Red" Visible="false"></asp:Label>

                </td>
            </tr>
        </table>


    </fieldset>

    <fieldset class="boxBody">
           <span style="text-align:left"><h1 style="color:red">Note:- It’s mandatory to save the virtual ID on your mobile phone for future use.</h1></span><br />
        <div style="width: 1200px; padding-left: 300px">
       
            <asp:Panel runat="server" ID="PanelQuotation" CssClass="modalPopupWhite" Visible="false">
                <div id="Div1" style="border-left: solid;border-right:solid;border-top:solid;border-bottom:solid; border-width: 1px">
                    <div id="divmsg" runat="server" style="color: white; margin-bottom: 0px; height: 110px; background-color: orange">
                        <table style="border-bottom:1px solid">
                            <tr style="text-align: right">
                                <td>
                                    <asp:Image ID="ImgPrv" Height="105px" Width="110px" BorderColor="Black" runat="server" CssClass="imgcircle" />
                                </td>

                                <td style="vertical-align: middle; height: 100px; width: 500px" align="center">
                                    <asp:Label ID="lblVID" runat="server" Text="Teerthanker Mahaveer University (Moradabad)" ForeColor="White" Font-Bold="true" Font-Size="Large"></asp:Label>

                                </td>
                                <td align="right">
                                    <asp:Image ID="Image2" Height="109px" Width="120px" ImageUrl="~/images/TMU Logo.jpg" BorderColor="Black" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <table>
                            <tr>
                                <td style="text-align: center; width: 700px;padding-top:10px;" >
                                    <asp:Label ID="Label3" Font-Bold="true" ForeColor="Green" Text="(VIRTUAL ID CARD)" runat="server"></asp:Label>
                                </td>

                            </tr>
                        </table>
                    </div>
                    <br />
                    <div>
                        <table style="padding-left: -50px">
                            <tr style="margin-left: -200px">
                                <td style="width: 120px; height: 30px">&nbsp&nbsp&nbsp
                                    <asp:Label ID="Label4" Font-Bold="true" ForeColor="Black" Text="Student Name" runat="server"></asp:Label>
                                </td>
                                <td style="width: 280px; height: 30px">
                                    <asp:Label ID="lblStuName" Font-Bold="true" ForeColor="Black" runat="server"></asp:Label>

                                </td>

                                <td rowspan="6">
                                    <asp:Image ID="Image1" Height="140px" Width="140px" BorderColor="Black" runat="server" /><br />
                                    <br />
                                    <asp:Label ID="lblM" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>

                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp&nbsp&nbsp
                                    <asp:Label ID="Label6" Font-Bold="true" ForeColor="Black" Text="Father's Name" runat="server"></asp:Label>
                                </td>
                                <td style="width: 280px; height: 30px">
                                    <asp:Label ID="lblFather" Font-Bold="true" ForeColor="Black" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr style="margin-left: 200px">
                                <td>&nbsp&nbsp&nbsp
                                    <asp:Label ID="Label8" Font-Bold="true" ForeColor="Black" Text="Student No." runat="server"></asp:Label>
                                </td>
                                <td style="width: 280px; height: 30px">
                                    <asp:Label ID="lblStudent" Font-Bold="true" ForeColor="Black" runat="server"></asp:Label>
                                </td>

                            </tr>
                            <tr style="margin-left: 200px">
                                <td>&nbsp&nbsp&nbsp
                                    <asp:Label ID="Label5" Font-Bold="true" ForeColor="Black" Text="College Code" runat="server"></asp:Label>
                                </td>
                                <td style="width: 280px; height: 30px">
                                    <asp:Label ID="lblCollege" Font-Bold="true" ForeColor="Black" runat="server"></asp:Label>
                                </td>

                            </tr>
                            <tr style="margin-left: 200px">
                                <td>&nbsp&nbsp&nbsp 
                                    <asp:Label ID="Label10" Font-Bold="true" ForeColor="Black" Text="Course Name" runat="server"></asp:Label>
                                </td>
                                <td style="width: 300px; height: 30px;">
                                    <asp:Label ID="lblCourse" Font-Bold="true" ForeColor="Black" runat="server"></asp:Label>
                                </td>

                            </tr>
                            <tr style="margin-left: 200px">
                                <td>&nbsp&nbsp&nbsp
                                    <asp:Label ID="Label12" Font-Bold="true" ForeColor="Black" Text="Admitted Year" runat="server"></asp:Label>
                                </td>
                                <td style="width: 280px; height: 30px">
                                    <asp:Label ID="lblAdmittedYear" Font-Bold="true" ForeColor="Black" runat="server"></asp:Label>
                                </td>

                            </tr>
                            <%-- <tr style="margin-left: 200px">
                                <td>&nbsp&nbsp&nbsp
                                    <asp:Label ID="Label7" Font-Bold="true" ForeColor="Black" Text="Student Mobile" runat="server"></asp:Label>
                                </td>
                                <td style="width: 280px; height: 30px">
                                    <asp:Label ID="lblStMobile" Font-Bold="true" ForeColor="Black" runat="server"></asp:Label>
                                </td>

                            </tr>--%>
                            <tr style="margin-left: 200px">
                                <td style="width: 250px">&nbsp&nbsp&nbsp
                                    <asp:Label ID="Label9" Font-Bold="true" ForeColor="Black" Text="Parent's Mobile" runat="server"></asp:Label>
                                </td>
                                <td style="width: 280px; height: 30px">
                                    <asp:Label ID="lblPMobile" Font-Bold="true" ForeColor="Black" runat="server"></asp:Label>
                                </td>

                            </tr>
                            <%-- <tr style="margin-left:200px">
                        <td>
                           &nbsp&nbsp&nbsp <asp:Label ID="Label14" Font-Bold="true"  ForeColor="Black" Text="Mobile No." runat="server"></asp:Label>
                        </td>
                        <td  style="width:240px;height:30px">
                           <asp:Label ID="lblMobile" Font-Bold="true"  ForeColor="Black" Text="BHUPENDRA SINGH YADAV" runat="server"></asp:Label>
                        </td>
                        
                    </tr>--%>
                        </table>
                    </div>
                </div>
                <div style="text-align: center">
                    <asp:HiddenField ID="hfImageData" runat="server" />
                    <asp:Button ID="btncancelpopup" Text="Close" BackColor="Blue" ForeColor="White" Font-Bold="true" Height="30px" Width="50px" OnClick="btncancelpopup_Click" runat="server" />
                   <%--                    <asp:Button ID="Button1" Text="Print" Height="30px" BackColor="Blue" ForeColor="White" Font-Bold="true" Width="50px" runat="server" OnClientClick="PrintDiv()" />--%>
                   <asp:Button ID="Button3" Text="Download" Height="30px" BackColor="Blue" ForeColor="White" Font-Bold="true" Width="80px" runat="server" UseSubmitBehavior="false" OnClick="ExportToImage" OnClientClick="return ConvertToImage(this)" />


                     
       

                </div>
            </asp:Panel>
        </div>
    </fieldset>




   
</asp:Content>

