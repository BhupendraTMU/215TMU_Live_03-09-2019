<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentNoDuesTMMC.aspx.cs" Inherits="Student_StudentNoDuesTMMC" %>

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
    <div id="divnodues" runat="server" visible="true">
        <fieldset class="boxBody">
            <div style="width: 100%; margin-bottom: 10px; margin-left: 1%; margin-right: 1%; margin-top: 5px;">
                <table style="width: 98%;">
                    <tr>
                        <td style="width: 1%"></td>
                        <td style="width: 12%; text-align: left">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/tmulogo.png" Width="50%" />
                        </td>
                        <td style="width: 70%; text-align: center">
                            <strong>
                                <asp:Label ID="Label28" Font-Size="X-Large" Text="TEERTHANKER MAHAVEER MEDICAL COLLEGE & RESEARCH CENTER" Font-Names="Times New Roman" runat="server"></asp:Label></strong>
                            <br />
                            <br />
                            <strong>
                                <asp:Label ID="Label29" Font-Size="Large" Text="(Established under Govt. of U. P. Act No. 30, 2008)" Font-Names="Times New Roman" runat="server"></asp:Label></strong>

                            <br />
                            <br />
                            <strong>
                                <asp:Label ID="Label30" runat="server" Font-Size="Large" Font-Names="Times New Roman" Text="Delhi Road, Moradabad - 244001, (U.P.)"></asp:Label></strong>

                        </td>

                        <td style="width: 10%; text-align: center"></td>
                    </tr>
                </table>
            </div>
        </fieldset>
        <fieldset class="boxBody" style="text-align: center; border-color: black; background-color: black;">
            <asp:Label ID="Label1" runat="server" Text="No Dues Certificate (Interns)" Font-Size="15pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
        </fieldset>


        <div id="divGeneralBodyenrollmentform">
            <fieldset class="boxBodyInner">
                <div class="form-horizontal">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="col-md-2">
                                        <label style="width: 200px; font: bold; color: black; font-size: large">Enrollement No:</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtenrollmentno" runat="server" Enabled="false" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label style="width: 200px; font: bold; color: black; font-size: large">ST.No</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtstno" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label style="width: 200px; font: bold; color: black; font-size: large">Student Name:</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtstudentName" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-2">
                                        <label style="width: 200px; font: bold; color: black; font-size: large">Father's Name:</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtfathername" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label style="width: 200px; font: bold; color: black; font-size: large">College/Dept:</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtcollegedept" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label style="width: 200px; font: bold; color: black; font-size: large">Programme:</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtPrograme" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-2">
                                        <label style="width: 200px; font: bold; color: black; font-size: large">No Dues Id:</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtnoduesid" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label style="width: 200px; font: bold; color: black; font-size: large">Gender:</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtgender" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </fieldset>
        </div>
        <div id="divGeneralBodyenrollmentform1">
            <fieldset class="boxBodyInner">
                <div class="form-horizontal">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">


                                <div class="form-group">
                                    <asp:Panel ID="pnl1" runat="server">
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large" class="required">Mobile Number:</label>

                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtmobile" CssClass="form-control" MaxLength="10" MinLength="10" onkeypress="return numeric(event)" BorderColor="Black" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Button ID="btnsendOtp" runat="server" CssClass="auto-style7" Text="Send Mobile OTP" OnClick="btnsendOtp_Click" Height="35px" />

                                        </div>

                                    </asp:Panel>
                                    <asp:Panel ID="pnl2" runat="server" Visible="false">
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">Enter Your OTP:</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtverifyMobileNO" CssClass="form-control" BorderColor="Black" runat="server"></asp:TextBox>
                                            <asp:Label ID="lblMSGOTP" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                        </div>
                                        <div class="col-md-2">

                                            <asp:Button ID="btnverify" runat="server" Text="Verify" Height="35px" CssClass="auto-style7" OnClick="btnverify_Click" Width="120px" />
                                        </div>
                                    </asp:Panel>

                                </div>
                                <div class="form-group">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large" class="required">Email-ID:</label>

                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtemailid" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtemailid" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Button ID="Button2" runat="server" CssClass="form-control" Visible="false" Height="35px" OnClick="Button2_Click" Text="Send Email Id OTP" />

                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel2" runat="server" Visible="false">
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">Enter Your OTP:</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtverifyEmail" CssClass="form-control" BorderColor="Black" runat="server"></asp:TextBox>
                                            <asp:Label ID="lblMSGOTPemail" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Button ID="Btn_verifyEmail" runat="server" Text="Verify" OnClick="Btn_verifyEmail_Click" Height="35px" Width="120px" CssClass="form-control" />
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </fieldset>
        </div>

        <asp:Table class="boxBody" Width="1113px" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center; margin-left: 19px;" ID="tblData" runat="server" CssClass="auto-style8" Height="323px">
            <asp:TableRow>
                <asp:TableHeaderCell Style="border: 1px solid; text-align: center" Width="193px">
                    <asp:Label ID="Label7" runat="server" Font-Bold="true" Style="text-align: center" Text="S.NO."></asp:Label>
                </asp:TableHeaderCell>
                <asp:TableHeaderCell Style="border: 1px solid; text-align: center" Width="320px">
                    <asp:Label ID="Label8" runat="server" Font-Bold="true" Style="text-align: center" Text="PARTICULARS"></asp:Label>
                </asp:TableHeaderCell>

                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="Label10" runat="server" Font-Bold="true" Style="text-align: center" Text="NAME"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="Label3" runat="server" Font-Bold="true" Style="text-align: center" Text="DESIGNATION"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid" Width="320px">
                    <asp:Label ID="Label4" runat="server" Font-Bold="true" Style="text-align: center" Text="REMARK"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid" Width="320px">
                    <asp:Label ID="Label23" runat="server" Font-Bold="true" Style="text-align: center" Text="PENDING AMOUNT"></asp:Label>
                </asp:TableCell>
                <%-- <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="Label31" runat="server" Visible="false" Text="ACTION"></asp:Label>
            </asp:TableCell>--%>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">1.</asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:Label ID="Label24" runat="server" Font-Bold="true" Text="Pending Fee"></asp:Label>&nbsp
                    <asp:LinkButton ID="lnkbutton" runat="server" ForeColor="Red" OnClick="lnkbutton_Click" Font-Bold="true">[View]</asp:LinkButton>

                    <asp:TextBox ID="TextBox15" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtaccountemployeecode" runat="server" Visible="false"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="Label25" runat="server" Enabled="false" Text=""></asp:Label>
                    <asp:Label ID="lblaccountname" Font-Bold="true" runat="server" Enabled="false" Text=""></asp:Label>

                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="Label26" runat="server" Enabled="false" Text=""></asp:Label>
                    <asp:Label ID="lblaccountdeg" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid">

                    <asp:TextBox ID="TextBox17" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox18" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>


                <asp:TableCell>
                    <div>
                        <asp:TextBox ID="txtifotherfeeid" runat="server" Width="50px" Visible="false"></asp:TextBox>
                    </div>

                </asp:TableCell>


            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">2.</asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:Label ID="Label5" runat="server" Font-Bold="true" Text="Central Library"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="lblcentlibname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                     <asp:Label ID="lblcentrallibrarycode" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="lblcentlibdeg" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="txtremarkcentrallib" Font-Bold="true" runat="server" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox2" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <%--  <asp:TableCell Style="border: 1px solid">
                <asp:Button ID="btn_Centrallib" runat="server" Enabled="false" Text="Submit" Height="25px" Visible="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
            </asp:TableCell>--%>
            </asp:TableRow>
            



            <asp:TableRow>
                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">3.</asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:Label ID="Label16" runat="server" Font-Bold="true" Text="Hostel"></asp:Label>
                </asp:TableCell>



                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="lblhostelsname" runat="server" Enabled="false" Font-Bold="true" Text=""></asp:Label>
                    <asp:Label ID="lblhostelcode" runat="server" Visible="false" Font-Bold="true" Text=""></asp:Label>
                    <asp:Label ID="Label14" runat="server" Font-Bold="true" Visible="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="lblhosteldeg" Font-Bold="true" runat="server" Enabled="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="txtremarkhostel" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox5" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <%--<asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Btn_Hostel" runat="server" Enabled="false" Text="Submit" Visible="false"  Height="25px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
            </asp:TableCell>--%>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">4.</asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Hostel Mess"></asp:Label>
                </asp:TableCell>



                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="Label6" runat="server" Enabled="false" Font-Bold="true" Text=""></asp:Label>
                    <asp:Label ID="lblhostelmasname" runat="server" Visible="false" Font-Bold="true" Text=""></asp:Label>
                    <asp:Label ID="lblhostelmescode" runat="server" Font-Bold="true" Visible="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="Label12" Font-Bold="true" runat="server" Enabled="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox1" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox3" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <%--<asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Btn_Hostel" runat="server" Enabled="false" Text="Submit" Visible="false"  Height="25px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
            </asp:TableCell>--%>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">5.</asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:Label ID="Label17" runat="server" Font-Bold="true" Text="Washer Man"></asp:Label>
                </asp:TableCell>



                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="lblwashermanname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="lblwashermandeg" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="txtremarkwasherman" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox6" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>

                <%--<asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Btn_Washerman" runat="server" Enabled="false" Text="Submit" Height="25px" Visible="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
            </asp:TableCell>--%>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">6.</asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:Label ID="Label21" runat="server" Font-Bold="true" Text="Sports"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="lblsportname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                    <asp:Label ID="lblsportnamecode" runat="server" Font-Bold="true" Visible="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="lblsportdeg" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="txtremarksportic" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox8" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <%--<asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button14" runat="server" Enabled="false" Text="Submit" Height="25px" Visible="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
            </asp:TableCell>--%>
            </asp:TableRow>
            <asp:TableRow>
    <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">7.</asp:TableCell>
    <asp:TableCell Style="border: 1px solid">
        <asp:Label ID="Label921" runat="server" Font-Bold="true" Text="IT Department"></asp:Label>
    </asp:TableCell>
    <asp:TableCell Style="border: 1px solid" Visible="false">
        <asp:Label ID="lblITname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
        <asp:Label ID="lblITnamecode" runat="server" Font-Bold="true" Visible="false" Text=""></asp:Label>
    </asp:TableCell>

    <asp:TableCell Style="border: 1px solid" Visible="false">
        <asp:Label ID="lblITdeg" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
    </asp:TableCell>

    <asp:TableCell Style="border: 1px solid">
        <asp:TextBox ID="txtremarkIT" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
    </asp:TableCell>
    <asp:TableCell Style="border: 1px solid">
        <asp:TextBox ID="TextBox98" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
    </asp:TableCell>
    <%--<asp:TableCell Style="border: 1px solid">
    <asp:Button ID="Button914" runat="server" Enabled="false" Text="Submit" Height="25px" Visible="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
</asp:TableCell>--%>
</asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">8.</asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:Label ID="Label13" runat="server" Font-Bold="true" Text="Community Medicine"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="lblCommunityMedicinename" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                    <asp:Label ID="lblCommunityMedicinecode" runat="server" Font-Bold="true" Visible="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="Label19" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox4" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox7" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <%--<asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button14" runat="server" Enabled="false" Text="Submit" Height="25px" Visible="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
            </asp:TableCell>--%>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">9.</asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:Label ID="Label20" runat="server" Font-Bold="true" Text="General Medicine"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="lblGeneralMedicinename" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                    <asp:Label ID="lblGeneralMedicicode" runat="server" Font-Bold="true" Visible="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="Label32" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox9" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox10" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <%--<asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button14" runat="server" Enabled="false" Text="Submit" Height="25px" Visible="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
            </asp:TableCell>--%>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">10.</asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:Label ID="Label33" runat="server" Font-Bold="true" Text="Psychiatry"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="lblPsychiatryname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                    <asp:Label ID="lblPsychiatrycode" runat="server" Font-Bold="true" Visible="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="Label36" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox11" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox12" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <%--<asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button14" runat="server" Enabled="false" Text="Submit" Height="25px" Visible="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
            </asp:TableCell>--%>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">11.</asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:Label ID="Label37" runat="server" Font-Bold="true" Text="General Surgery"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="lblGeneralSurgeryname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                    <asp:Label ID="lblGeneralSurgerycode" runat="server" Font-Bold="true" Visible="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="Label40" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox13" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox14" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <%--<asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button14" runat="server" Enabled="false" Text="Submit" Height="25px" Visible="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
            </asp:TableCell>--%>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">12.</asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:Label ID="Label41" runat="server" Font-Bold="true" Text="Anesthisia"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="lblAnesthisianame" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                    <asp:Label ID="lblAnesthisiacode" runat="server" Font-Bold="true" Visible="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="Label44" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox16" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox19" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <%--<asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button14" runat="server" Enabled="false" Text="Submit" Height="25px" Visible="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
            </asp:TableCell>--%>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">13.</asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:Label ID="Label45" runat="server" Font-Bold="true" Text="Obs & Gyane"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="lblObsGyanename" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                    <asp:Label ID="lblObsGyanecode" runat="server" Font-Bold="true" Visible="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="Label48" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox20" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox21" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <%--<asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button14" runat="server" Enabled="false" Text="Submit" Height="25px" Visible="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
            </asp:TableCell>--%>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">14.</asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:Label ID="Label49" runat="server" Font-Bold="true" Text="Pediatrics"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="lblPediatricsname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                    <asp:Label ID="lblPediatricscode" runat="server" Font-Bold="true" Visible="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="Label52" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox22" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox23" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <%--<asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button14" runat="server" Enabled="false" Text="Submit" Height="25px" Visible="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
            </asp:TableCell>--%>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">15.</asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:Label ID="Label53" runat="server" Font-Bold="true" Text="Orthopedics"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="lblOrthopedicsname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                    <asp:Label ID="lblOrthopedicscode" runat="server" Font-Bold="true" Visible="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="Label56" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox24" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox25" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <%--<asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button14" runat="server" Enabled="false" Text="Submit" Height="25px" Visible="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
            </asp:TableCell>--%>
            </asp:TableRow>


            <asp:TableRow>
                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">16.</asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:Label ID="Label57" runat="server" Font-Bold="true" Text="Ent"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="lblEntname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                    <asp:Label ID="lblEntcode" runat="server" Font-Bold="true" Visible="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="Label60" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox26" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox27" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <%--<asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button14" runat="server" Enabled="false" Text="Submit" Height="25px" Visible="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
            </asp:TableCell>--%>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">17.</asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:Label ID="Label61" runat="server" Font-Bold="true" Text="Ophthalmology"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="lblOphthalmologyname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                    <asp:Label ID="lblOphthalmologycode" runat="server" Font-Bold="true" Visible="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="Label64" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox28" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox29" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <%--<asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button14" runat="server" Enabled="false" Text="Submit" Height="25px" Visible="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
            </asp:TableCell>--%>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">18.</asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:Label ID="Label65" runat="server" Font-Bold="true" Text="Casualty"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="lblCasualtyname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                    <asp:Label ID="lblCasualtycode" runat="server" Font-Bold="true" Visible="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="Label68" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox30" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox31" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <%--<asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button14" runat="server" Enabled="false" Text="Submit" Height="25px" Visible="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
            </asp:TableCell>--%>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">19.</asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:Label ID="Label69" runat="server" Font-Bold="true" Text="Dermatology"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="lblDermatologyname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                    <asp:Label ID="lblDermatologycode" runat="server" Font-Bold="true" Visible="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="Label72" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox32" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox33" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <%--<asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button14" runat="server" Enabled="false" Text="Submit" Height="25px" Visible="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
            </asp:TableCell>--%>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">20.</asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:Label ID="Label73" runat="server" Font-Bold="true" Text="Tb & Chest (Pulmonary Medicine)"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="lblTbChestname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                    <asp:Label ID="lblTbChestcode" runat="server" Font-Bold="true" Visible="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="Label76" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox34" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox35" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <%--<asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button14" runat="server" Enabled="false" Text="Submit" Height="25px" Visible="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
            </asp:TableCell>--%>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">21.</asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:Label ID="Label77" runat="server" Font-Bold="true" Text="Radiology"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="lblRadiologyname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                    <asp:Label ID="lblRadiologycode" runat="server" Font-Bold="true" Visible="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="Label80" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox36" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox37" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <%--<asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button14" runat="server" Enabled="false" Text="Submit" Height="25px" Visible="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
            </asp:TableCell>--%>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">22.</asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:Label ID="Label81" runat="server" Font-Bold="true" Text="Pathlogy(Blood Bank)"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="lblPathlogyname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                    <asp:Label ID="lblPathlogycode" runat="server" Font-Bold="true" Visible="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="Label84" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox38" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox39" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <%--<asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button14" runat="server" Enabled="false" Text="Submit" Height="25px" Visible="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
            </asp:TableCell>--%>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">23.</asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:Label ID="Label85" runat="server" Font-Bold="true" Text="Forensic Medicine"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="lblForensicMedicinename" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                    <asp:Label ID="lblForensicMedicinecode" runat="server" Font-Bold="true" Visible="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid" Visible="false">
                    <asp:Label ID="Label88" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                </asp:TableCell>

                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox40" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid">
                    <asp:TextBox ID="TextBox41" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <%--<asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button14" runat="server" Enabled="false" Text="Submit" Height="25px" Visible="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
            </asp:TableCell>--%>
            </asp:TableRow>


            <asp:TableFooterRow HorizontalAlign="Right">
                <asp:TableCell Style="border: 1px solid; text-align: right" ColumnSpan="3" Font-Bold="true">
                    &nbsp Payment Status :
                    <asp:Label ID="PayStatus" runat="server" ForeColor="Red" Font-Bold="true" Text="OPEN"></asp:Label>&nbsp&nbsp&nbsp&nbsp   Total Amount :
                <%--<asp:TextBox ID="TextBox15" runat="server" Text="bfjsfbjsfbss" Enabled="false"></asp:TextBox>--%>
                </asp:TableCell>
                <asp:TableCell Style="border: 1px solid; text-align: left" ColumnSpan="1">
                    &nbsp 
                    <asp:Label ID="lblTotal" runat="server" Font-Bold="true" Enabled="false"></asp:Label>
                    &nbsp&nbsp

                <asp:Button ID="lnkPay" Text="Pay Amount" ForeColor="white" BackColor="Green" Font-Size="Large" runat="server" Visible="false" OnClick="lnkPay_Click" Height="40px"></asp:Button>

                </asp:TableCell>
            </asp:TableFooterRow>
        </asp:Table>

       
        <br />
        <br />
        <br />
        <fieldset>
            <%-- <div class="form-group">
                <div class="auto-style6">
                    <label>
                        Certified that Mr./Ms&nbsp;&nbsp;<asp:Label ID="lblcertifiedname" Font-Bold="true" Font-Underline="true" runat="server"></asp:Label>
                        has been  bona fide student of the college and has completed the studies in the Year&nbsp;&nbsp;<asp:Label ID="lblYear" runat="server" Font-Bold="true" Font-Underline="true"></asp:Label>
                        .
                    </label>
                </div>
           <%-- </div>--%>
            <%--    <div class="form-group">
                <div class="col-md-12">
                    <label>The "NO DUES CERTIFICATE" is issued to him/her and forwarded to Exam. Branch for final declaration of exam results.</label>
                </div>
            </div>--%>
            <br />
            <br />
            <div class="form-group" runat="server" visible="false">
                <div class="col-md-1">
                    <label>Date:</label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtdatedirectorprincipaldate" runat="server" BorderColor="Black" Width="200px" onkeydown="return false;" Enabled="false" autocomplete="off" oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" AutoPostBack="True"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtdatedirectorprincipaldate" Format="dd MMM yyyy"></asp:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtdatedirectorprincipaldate" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="aspx"></asp:RequiredFieldValidator>

                </div>
            </div>
        </fieldset>
        <fieldset>

            <div class="form-group">
                <div class="col-md-5">
                    <label></label>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btn_Save" runat="server" OnClick="btn_Save_Click" Width="100px" Visible="false" Height="40px" Font-Bold="true" ForeColor="white" BackColor="Green" Text="Apply" CssClass="form-control" />
                </div>

                <%--<asp:TextBox ID="txtBarcode" runat="server"></asp:TextBox>--%>
                <%-- <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click"/>--%>
            </div>


        </fieldset>

        <asp:Panel ID="pnlGridViewdata" CssClass="modalPopup" Width="50%" runat="server" ScrollBars="Vertical" Height="700px" Style="display: none;">
            <div class="close">
                <asp:Button ID="btnclose" runat="server" Text="X" ForeColor="Red" BackColor="White" Height="35px" />
            </div>
            <fieldset class="boxBody" style="text-align: center; border-color: black; background-color: green; height: 35px">
                <asp:Label ID="Label31" runat="server" Text="Student  Fee Ledger" Font-Size="12pt" ForeColor="white" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

            </fieldset>
            <br />
            <asp:GridView ID="GriedviewStudentfeeLedger" runat="server" AutoGenerateColumns="false" AlternatingRowStyle-CssClass="danger" OnPageIndexChanging="GriedviewStudentfeeLedger_PageIndexChanging1" PageSize="30" CssClass="table table-striped table-bordered table-hover" AllowPaging="true" ShowFooter="true">
                <Columns>
                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <%# Container.DataItemIndex +1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="customerNo" HeaderText="ST. No" ItemStyle-Width="60" />
                    <asp:BoundField DataField="StudentName" HeaderText="Student Name" ItemStyle-Width="100" />
                    <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Width="100" />
                    <asp:BoundField DataField="PendingAmount" HeaderText="Pending Amount" FooterStyle-ForeColor="Red" ItemStyle-Width="60" FooterStyle-Font-Bold="true" DataFormatString="{0:N4}" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="PostingDate" HeaderText="Posting Date" ItemStyle-Width="100" />

                </Columns>
                <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
            </asp:GridView>
        </asp:Panel>
        <asp:Button ID="btnDummy" runat="server" Style="display: none;" />
        <asp:ModalPopupExtender ID="GridViewdata" runat="server" TargetControlID="btnDummy"
            PopupControlID="pnlGridViewdata" BackgroundCssClass="modalBackground" />

    </div>

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
                                    <asp:Label ID="Label9" Font-Size="X-Large" Text="TEERTHANKER MAHAVEER UNIVERSITY" Font-Names="Times New Roman" runat="server"></asp:Label></strong>
                                <br />
                                <br />
                                <strong>
                                    <asp:Label ID="Label27" Font-Size="Large" Text="(Established under Govt. of U. P. Act No. 30, 2008)" Font-Names="Times New Roman" runat="server"></asp:Label></strong>

                                <br />
                                <br />
                                <strong>
                                    <asp:Label ID="Label11" runat="server" Font-Size="Large" Font-Names="Times New Roman" Text="Delhi Road, Moradabad - 244001, U.P."></asp:Label></strong>

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
                        <p style="text-align: center; font-size: xx-large; text-decoration: underline; font-family: 'Times New Roman'; font-weight: bold;">No Dues Certificate</p>
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
                                <asp:Label ID="Label15" Font-Bold="true" Text="Mr." runat="server"></asp:Label>
                                <asp:Label ID="lblfathername" runat="server" Text="" Font-Bold="true" Font-Underline="false"></asp:Label>
                                having Enrollment No.
                            <asp:Label ID="lblenrollment" runat="server" Text="" Font-Bold="true" Font-Underline="false"></asp:Label>
                                has completed
                             <asp:Label ID="Label18" Text="his" Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="Label22" Text="her" Visible="false" runat="server"></asp:Label>

                                program
                            <asp:Label ID="lblprogram" runat="server" Text="" Font-Bold="true" Font-Underline="false"></asp:Label>
                                in the  Year 
    
                         <asp:Label ID="lblacedmicyear" runat="server" Text="" Font-Bold="true" Font-Underline="false"></asp:Label>.
                            </p>


                            <br />
                            <p class="custom-paragraph" style="font-size: x-large">
                                All the concerned departments of the University have given clearance for no dues against 
                             <asp:Label ID="Label34" Text="him." Visible="false" runat="server"></asp:Label>

                                <asp:Label ID="Label35" Text="her." Visible="false" runat="server"></asp:Label>


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

                            <asp:Image ID="imgBarcode" runat="server" Height="140px" Width="140px" Visible="true" />
                        </div>
                        <br />
                        <br />

                        <p style="text-align: left; font-size: large; font-weight: bold" class="auto-style4">This is system-generated Certificate, No signature  required.</p>
                        <br />
                        <br />
                        <div style="text-align: right">

                            <asp:Label ID="Label38" Font-Bold="true" Text="Printed Date:" runat="server"></asp:Label>
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

