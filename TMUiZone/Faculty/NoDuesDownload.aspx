<%@ Page Title="" Language="C#" MasterPageFile="IndexMaster.master" AutoEventWireup="true" CodeFile="NoDuesDownload.aspx.cs" Inherits="NoDuesDownload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="Foundation/js/foundation.min.js" type="text/javascript"></script>
    <link href="Foundation/css/foundation.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function numeric(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && ((charCode >= 48 && charCode <= 57) || charCode == 46))
                return true;
            else {
                alert('Please Enter Number Only.');
                return false;
            }
        }
    </script>
    <style type="text/css">
        .required::after {
            content: "*";
            font-weight: bold;
            color: red;
        }
    </style>
    <style type="text/css">
        .custom-paragraph {
            line-height: 1.8; /* Adjust the value as needed */
        }
    </style>

    <style type="text/css">
        .auto-style1 {
            width: 1181px;
            height: 38px;
        }

        .auto-style3 {
            height: 27px;
            width: 1155px;
        }

        .auto-style4 {
            width: 757px;
        }

        @page {
            size: 210mm 297mm portrait;
            margin: 0 0 0 0;
            margin-left: 200px;
            overflow: hidden;
        }

        .Watermark {
            position: absolute;
            left: expression(this.offsetParent.scrollLeft + 380);
            top: expression(this.offsetParent.scrollTop + 320);
            z-index: 99;
        }

        body {
            background-image: url('watermark.jpg');
        }

        #btnPrint {
            display: none;
        }
    </style>


    <%--<style type="text/css">
        @media print {
            /* Set page size to A5 */
            @page {
                size: A4;
                
            }

            /* Adjust margins if needed */
            body {
              
                 margin:unset;
                margin-top: 100px;               
                widows: inherit;
                width:210mm;
                margin-left:-200px;
                position: absolute;
                left: expression(this.offsetParent.scrollLeft + 180);
                top: expression(this.offsetParent.scrollTop + 320);
                z-index: 99;
             
               padding-right:unset;
                
            }

            .Watermark {
                height: 35%;
                width: 50%;
                margin-left: 130px;
                opacity: 0.2;
            }
          
        }
      
        .auto-style7 {
            display: block;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
        }
      
        .auto-style8 {
            margin-left: 31px;
        }
      
    </style>--%>

    <style type="text/css">
        @media print {
            @page {
                size: A4;
                margin-left: 90px;
            }

            body {
                margin-top: 150px;
                widows: inherit;
                width: 210mm;
                /*margin-left:-80px;*/
                /* Set margins for the entire document */
            }

            .Watermark {
                height: 30%;
                width: 100%;
                margin-left: -50px;
                opacity: 0.2;
            }
        }
    </style>
    <script type="text/javascript">
        function printDiv() {
            //var divContents = document.getElementById("PrintPanel").innerHTML;
            //var a = window.open('', '', 'height=1000, width=500');
            ////var a = window.open('', '', 'margin-left:-200px , margin-top:-10px');
            //a.document.write('<html>');
            //a.document.write('<body >');
            //a.document.write(divContents);
            //a.document.write('</body></html>');
            var theImg = document.getElementById('Image2');

            theImg.width = 200;
            window.print();

            //a.document.close();
            //a.print();
        }
    </script>
    <script type="text/javascript">
        function printDiv(PrintPanel) {
            var theImg = document.getElementById('ContentPlaceHolder1_Image2');


            theImg.style.width = "500px";
            var printContents = document.getElementById(PrintPanel).innerHTML;
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = printContents;
            window.print();
            document.body.innerHTML = originalContents;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
   <div id="PrintPanel" style="text-align: justify">
        <div id="divnodues1" runat="server" visible="false">

            <fieldset class="boxBody" style="width: 800px; border: solid; margin-left: 80px">
                <br />
                <div style="margin-bottom: 10px; margin-left: 1%; margin-right: 1%; margin-top: 5px;" class="nav-justified">


                    <table style="width: 98%;">
                        <tr>

                            <td style="width: 30%; text-align: left;">
                                <asp:Image ID="Image3" runat="server" ImageUrl="~/images/tmulogo.png" Width="100%" Height="200px" />
                            </td>

                            <td style="width: 100%; text-align: center;">
                                <br />
                                <br />
                                <br />
                                <%--  <strong>
                            <asp:Label ID="Label9" runat="server" Font-Size="Large" Text="244001 - U.P."></asp:Label>
                        </strong>--%>

                                <strong>
                                    <asp:Label ID="Label2" Font-Size="X-Large" Text="TEERTHANKER MAHAVEER UNIVERSITY" Font-Names="Times New Roman" runat="server"></asp:Label></strong>
                                <br />
                                <br />
                                <strong>
                                    <asp:Label ID="Label27" Font-Size="Large" Text="(Established under Govt. of U. P. Act No. 30, 2008)" Font-Names="Times New Roman" runat="server"></asp:Label></strong>

                                <br />
                                <br />
                                <strong>
                                    <asp:Label ID="Label6" runat="server" Font-Size="Large" Font-Names="Times New Roman" Text="Delhi Road, Moradabad - 244001, U.P."></asp:Label></strong>

                            </td>

                            <td style="width: 10%; text-align: center"></td>
                        </tr>
                    </table>
                    <br />
                    <div style="font-size: large; text-align: justify">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <p style="font-weight: bold; font-size: large; text-align: left" class="auto-style3">
                 Ref. No.:
                 <asp:Label ID="lblrefno" runat="server" Text=""></asp:Label>&emsp;&emsp;&emsp;&emsp;
                 &emsp;&emsp;&emsp;&emsp;
                 &emsp;&emsp;&emsp;&emsp;
                 &emsp;&emsp;&nbsp;&nbsp;&nbsp;
                  
                
                                          Date:
                 <asp:Label ID="lbldate" runat="server" Text=""></asp:Label>
             </p>
                        <div style="margin-left: 170px">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/images/tmulogo.png" Width="20%" class="Watermark" Style="opacity: 0.1; filter: alpha(opacity=20);" />
                        </div>
                        <br />

                        <br />
                        <br />
                        <p style="text-align: center; font-size: xx-large; text-decoration: underline; font-family: 'Times New Roman'; font-weight: bold;">No Dues Form</p>
                        <br />
                        <br />
                        <div style="text-align: justify; font-size: x-large">
                            <p class="custom-paragraph">
                                This is to certify that 
                            <asp:Label ID="lblmr" Font-Bold="true" Text="Mr." Visible="false" runat="server"></asp:Label>

                                <asp:Label ID="lblms" Font-Bold="true" Text="Ms." Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="lblname" runat="server" Text="" Font-Bold="true" Font-Underline="false"></asp:Label>
                                <asp:Label ID="lblson" Text="son" Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="lbldaug" Font-Bold="true" Text="Daughter" Visible="false" runat="server"></asp:Label>
                                of 
                                <asp:Label ID="Label20" Font-Bold="true" Text="Mr." runat="server"></asp:Label>
                                <asp:Label ID="lblfathername" runat="server" Text="" Font-Bold="true" Font-Underline="false"></asp:Label>
                                having Enrollment No.
                            <asp:Label ID="lblenrollment" runat="server" Text="" Font-Bold="true" Font-Underline="false"></asp:Label>
                                has completed
                             <asp:Label ID="Label9" Text="his" Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="Label11" Text="her" Visible="false" runat="server"></asp:Label>

                                program
                            <asp:Label ID="lblprogram" runat="server" Text="" Font-Bold="true" Font-Underline="false"></asp:Label>
                                in the  Year 
    
                         <asp:Label ID="lblacedmicyear" runat="server" Text="" Font-Bold="true" Font-Underline="false"></asp:Label>.
                            </p>


                            <br />
                            <p class="custom-paragraph" style="font-size: x-large">
                                All the concerned departments of the University have given clearance for no dues against 
                             <asp:Label ID="Label13" Text="him." Visible="false" runat="server"></asp:Label>

                                <asp:Label ID="Label18" Text="her." Visible="false" runat="server"></asp:Label>


                            </p>
                        </div>
                        <br />
                        <%-- <p style="text-align:center"><asp:Image ID="Image2" runat="server" ImageUrl="~/images/UPDATEDLOGO.jpg" Width="20%" Height="100px" CssClass="auto-style5" /></p>--%>
                        <br />
                        <br />
                        <br />
                        <br />
                        <p class="custom-paragraph" style="text-align: right; font-size: large; font-weight: bold;">Controller of Examinations</p>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />

                        <div style="text-align: center">

                            <asp:Image ID="imgBarcode" runat="server" Height="140px" Width="140px" Visible="false" />
                        </div>
                        <br />
                        <br />

                        <p style="text-align: left; font-size: large; font-weight: bold" class="auto-style4">This is system-generated Certificate, No signature  required.</p>
                        <br />
                        <br />
                        <div style="text-align: right">

                            <asp:Label ID="Label19" Font-Bold="true" Text="Printed Date:" runat="server"></asp:Label>
                            <asp:Label ID="lblprinteddateandtime" Font-Bold="true" runat="server"></asp:Label>
                        </div>
                    </div>

                </div>




            </fieldset>
        </div>
    </div>


    <table class="auto-style1">
        <tr>
            <td style="width: 450px"></td>

            <td>

                <asp:Button ID="Button1" OnClientClick="printDiv('PrintPanel'); return false;" Visible="False" runat="server" Width="11%" Style="margin-top: 5px;" Text="Print" Font-Bold="true" BorderColor="WhiteSmoke" Height="44px" />
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>

