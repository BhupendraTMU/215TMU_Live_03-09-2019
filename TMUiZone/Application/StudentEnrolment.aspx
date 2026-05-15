<%@ Page Title="" Language="C#" MasterPageFile="~/Application/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentEnrolment.aspx.cs" Inherits="Student_StudentEnrolment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script runat="server">


    protected void lbl10th_Click1(object sender, EventArgs e)
    {

    }
</script>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"> </script>
    <script type="text/javascript">
        $(document).ready(function () {
            ShowTime();
        });
        function ShowTime() {
            var dt = new Date();
            document.getElementById("txtdate").innerHTML = dt.toLocaleTimeString();
            window.setTimeout("ShowTime()", 1000); // Here 1000(milliseconds) means one 1 Sec 
        }
    </script>
    <script type="text/javascript">
        function numeric(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && ((charCode >= 48 && charCode <= 57) || charCode == 46))
                return true;
            else {
                alert('Please Enter Number Only .');
                return false;
            }
        }
        function PrintDiv() {
            var divToPrint = document.getElementById('printarea');
            divToPrint.style.visibility = 'visible';
            var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
        }

    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody" style="text-align: center;">
        <div>
            <marquee>
                <asp:Label ID="Label15" Style="line-height: 30px" runat="server" Font-Bold="true" Font-Size="X-Large" ForeColor="Red" Text="Label"><b>
           “ Kindly submit the signed copy of duly filled Enrolment form at the office of assistant registrar of your respective college.”
</b></asp:Label></marquee>
        </div>

    </fieldset>
    <fieldset class="boxBody" style="text-align: center;">
        <asp:Label ID="lblPrincipal" runat="server" ForeColor="Red" Font-Size="Medium" Text="Principal:"></asp:Label>
        <asp:Label ID="lblhodstatus1" runat="server" ForeColor="Red" Font-Size="Medium" Text=""></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label13" runat="server" ForeColor="Red" Font-Size="Medium" Text="Addmission Director:"></asp:Label>
        <asp:Label ID="lblAddmissionDirector" runat="server" ForeColor="Red" Font-Size="Medium" Text=""></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblenrollment" runat="server" ForeColor="Red" Font-Size="Medium" Text="Enrollment Dept:"></asp:Label>
        <asp:Label ID="lblenrollmentdept" runat="server" ForeColor="Red" Font-Size="Medium" Text=""></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label14" runat="server" ForeColor="Red" Font-Size="Medium" Text="Rejection Reasons:"></asp:Label>
        <asp:Label ID="lblRejectionReasons" runat="server" ForeColor="Red" Font-Size="Medium" Text=""></asp:Label>
        <asp:Button ID="BtnPrint" OnClientClick="PrintDiv();" runat="server" Width="10%" Text="Print" Font-Bold="true" BorderColor="WhiteSmoke" />
    </fieldset>
    <fieldset class="boxBody">
        <div style="width: 100%; margin-bottom: 10px; margin-left: 1%; margin-right: 1%; margin-top: 5px;">

            <table>


                <tr>
                    <td style="width: 1%"></td>
                    <td style="width: 12%; text-align: left">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/UPDATEDLOGO.jpg" Width="55%" />

                    </td>
                    <td style="width: 65%; text-align: center">
                        <strong>
                            <asp:Label ID="lblCName" Font-Size="Large" Text="Teerthanker Mahaveer University, Moradabad" runat="server"></asp:Label></strong>
                        <br />
                        <strong>
                            <asp:Label ID="lblAC" runat="server" Text="(Established under Govt. of U. P. Act No. 30, 2008)"></asp:Label></strong>

                        <br />
                        <strong>
                            <asp:Label ID="LblType" runat="server" Text="Delhi Road,(146 Kms from Delhi on N.H. 24) Moradabad(U.P) India"></asp:Label>
                        </strong>
                        <br />
                        <strong>
                            <asp:Label ID="lbltel" runat="server" Text=" Tel.:+91-2360222 , 2360777"></asp:Label>
                        </strong>
                        <br />
                        <strong>
                            <asp:Label ID="lblemail" runat="server" Text="Email:university@tmu.ac.in;  hr@tmu.ac.in;  Website:www.tmu.ac.in"></asp:Label>
                        </strong>
                    </td>
                    <td style="width: 10%; text-align: center"></td>
                </tr>
            </table>
        </div>
    </fieldset>
    <fieldset class="boxBody" style="text-align: center; border-color: black; background-color: black;">
        <asp:Label ID="Label1" runat="server" Text="ENROLLMENT FORM" Font-Size="15pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <div id="divGeneralBodyenrollmentform">
        <fieldset class="boxBodyInner">
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">


                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="width: 200px">Programme/Branch</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtprogrambranch" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="col-md-2">
                                    <label style="width: 200px">Year of Admission</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtyearofaddmission" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div>
                                    <asp:UpdatePanel ID="pnlpic" runat="server">
                                        <ContentTemplate>
                                            <asp:Image ID="ImgPrv" Height="140px" Width="170px" BorderColor="Black" runat="server" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                            </div>
                            <div class="form-group" style="margin-top: -100px">
                                <div class="col-md-2">
                                    <label style="width: 200px">Name of College</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtnameofcollege" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px">Date of Birth</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtdateofbirth" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control" onkeydown="return false;" autocomplete="off" oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdateofbirth" Format="dd MMM yyyy"></asp:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtdateofbirth" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group" style="margin-top: -10px">
                                <div class="col-md-2">
                                    <label style="width: 200px">Student Name</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtstudentname" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px">Student Name(हिन्दी में)</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtstudendtnameHindi" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="width: 200px">Father's Name</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtfathername" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px">Father's Name(हिन्दी में)</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtFathernameHindi" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:Image ID="Image2" Height="40px" Width="170px" runat="server" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="width: 200px">Mother's Name</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtmothername" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px">Mother's Name(हिन्दी में)</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtMotherNameHindi" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="width: 200px">Gender</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtgender" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>

                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px">Nationality</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtnationality" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>
                            <div class="form-group">

                                <div class="col-md-2">
                                    <label style="width: 200px">Religion</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtreligion" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px">Category</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtcategory" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">

                                <div class="col-md-2">
                                    <label style="width: 200px">Minority Status(If Any)</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtminoritystatus" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <fieldset class="boxBody" style="text-align: left;">
                <asp:Label ID="Label5" runat="server" Text=" CORRESPONDENCE ADDRESS" Font-Size="15pt" ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
            </fieldset>
            <br />
            <div id="divGeneralBody">
                <fieldset class="boxBodyInner">
                    <div class="form-horizontal">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <div class="col-md-1">
                                            <label style="width: 200px">Address</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtaddress" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <label style="width: 200px">District</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtdistrict" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <label style="width: 200px">State</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtstate" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <label style="width: 200px">Country</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtcountry" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>
            <fieldset class="boxBody" style="text-align: left;">
                <asp:Label ID="Label2" runat="server" Text=" PERMANENT ADDRESS" Font-Size="15pt" ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
            </fieldset>
            <br />
            <div id="divGeneralBodyperadd">
                <fieldset class="boxBodyInner">
                    <div class="form-horizontal">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <div class="col-md-1">
                                            <label style="width: 200px">Address</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtperaddress" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <label style="width: 200px">District</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtperdistrict" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <label style="width: 200px">State</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtperstate" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <label style="width: 200px">Country</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtpercountry" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>
            <fieldset class="boxBody" style="text-align: left;">
                <asp:Label ID="Label3" runat="server" Text=" CONTACT DETAILS" Font-Size="15pt" ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
            </fieldset>
            <br />
            <div id="divGeneralBodycontactDetail">
                <fieldset class="boxBodyInner">
                    <div class="form-horizontal">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            <label style="width: 200px">Student Mob.</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtstudentmob" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <label style="width: 200px">E-mail ID</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtemailid" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="col-md-2">
                                            <label style="width: 250px">Parents Mob.</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtparentsmob" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>

            <fieldset class="boxBody" style="text-align: left;">
                <asp:Label ID="Label4" runat="server" Text=" EDUCATIONAL DETAIL -[Starting from 10th onward]" Font-Size="15pt" ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
            </fieldset>
            <div id="divGeneralBodyeducationdetail">
                <fieldset class="boxBodyInner">
                    <div class="form-horizontal">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Table class="boxBody" Width="1100px" Style="border: 1px solid" ID="Table1" runat="server">
                                            <asp:TableRow>
                                                <asp:TableHeaderCell Style="border: 1px solid">
                                                    <asp:Label ID="Label42" runat="server" Text="Course/Degree Name" Enabled="false"></asp:Label>
                                                </asp:TableHeaderCell>

                                                <asp:TableHeaderCell Style="border: 1px solid">
                                                    <asp:Label ID="Label43" runat="server" Text="Board/University" Enabled="false"></asp:Label>
                                                </asp:TableHeaderCell>
                                                <asp:TableCell Style="border: 1px solid">
                                                    <asp:Label ID="Label44" runat="server" Font-Bold="true" Text="Year of Passing" Enabled="false"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell Style="border: 1px solid">
                                                    <asp:Label ID="Label45" runat="server" Font-Bold="true" Text="Name of College/School/Institute" Enabled="false"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell Style="border: 1px solid">
                                                    <asp:Label ID="Label7" runat="server" Font-Bold="true" Text="Upload Document" Enabled="false"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell Style="border: 1px solid">
                                                    <asp:Label ID="Label12" runat="server" Font-Bold="true" Text="Action" Enabled="false"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell Style="border: 1px solid">
                                                    <asp:Label ID="Label51" runat="server" Text="10th" Font-Bold="true" Enabled="false"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell Style="border: 1px solid">
                                                    <asp:TextBox ID="txtboard10" runat="server" CssClass="form-control" BorderColor="Black"></asp:TextBox>
                                                </asp:TableCell>
                                                <asp:TableCell Style="border: 1px solid">
                                                    <asp:TextBox ID="txtyearofpassing10" runat="server" MaxLength="4" onkeypress="return numeric(event)" CssClass="form-control" BorderColor="Black"></asp:TextBox>
                                                </asp:TableCell>
                                                <asp:TableCell Style="border: 1px solid">
                                                    <asp:TextBox ID="txtnameofcollege10" runat="server" CssClass="form-control" BorderColor="Black"></asp:TextBox>
                                                </asp:TableCell>
                                                <asp:TableCell Style="border: 1px solid">
                                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" /><asp:Button ID="Button1" Text="Upload File" runat="server" BorderColor="Red" OnClick="Button1_Click" CssClass="form-control" />
                                                </asp:TableCell>
                                                <asp:TableCell Style="border: 1px solid">
                                                    <asp:LinkButton ID="lbl10th" runat="server" Enabled='<%# (Eval("HighSchoolMarksheet").ToString() == "" ?  false : true) %>' Text="Download" OnClick="lbl10th_Click" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell Style="border: 1px solid">
                                                    <asp:Label ID="Label52" runat="server" Text="12th" Font-Bold="true" Enabled="false"></asp:Label>
                                                </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                    <asp:TextBox ID="txtboard12" runat="server" CssClass="form-control" BorderColor="Black"></asp:TextBox>
                                                </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                    <asp:TextBox ID="txtyearofpassing12" runat="server" MaxLength="4" onkeypress="return numeric(event)" CssClass="form-control" BorderColor="Black"></asp:TextBox>
                                                </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                    <asp:TextBox ID="txtnameofcollege12" runat="server" CssClass="form-control" BorderColor="Black"></asp:TextBox>
                                                </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                    <asp:FileUpload ID="FileUpload3" runat="server" CssClass="form-control" /><asp:Button ID="Button2" Text="Upload File" runat="server" OnClick="Button2_Click" BorderColor="Red" CssClass="form-control" />
                                                </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                    <asp:LinkButton ID="lbl12th" runat="server" Enabled='<%# (Eval("InterMarksheet").ToString() ==  "" ?  false : true) %>' Text="Download" OnClick="lbl12th_Click" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell Style="border: 1px solid">
                                                    <asp:Label ID="Label53" runat="server" Text="Graduation" Font-Bold="true" Enabled="false"></asp:Label>
                                                </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                    <asp:TextBox ID="txtboardgraduation" runat="server" Enabled="false" CssClass="form-control" BorderColor="Black"></asp:TextBox>
                                                </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                    <asp:TextBox ID="txtyearofpassinggraduation" runat="server" MaxLength="4" onkeypress="return numeric(event)" CssClass="form-control" BorderColor="Black"></asp:TextBox>
                                                </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                    <asp:TextBox ID="txtnameofcollegegraduation" runat="server" CssClass="form-control" BorderColor="Black"></asp:TextBox>
                                                </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                    <asp:FileUpload ID="FileUpload4" runat="server" CssClass="form-control" /><asp:Button ID="Button3" Text="Upload File" runat="server" BorderColor="Red" OnClick="Button3_Click" CssClass="form-control" />
                                                </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                    <asp:LinkButton ID="lblUG" runat="server" Enabled='<%# (Eval("UG_Final_Year").ToString() ==  "" ?  false : true) %>' Text="Download" OnClick="lblUG_Click" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell Style="border: 1px solid">
                                                    <asp:Label ID="Label54" runat="server" Text="Post-Graduation" Font-Bold="true" Enabled="false"></asp:Label>
                                                </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                    <asp:TextBox ID="txtboardpost" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                                </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                    <asp:TextBox ID="txtyearofpassingpost" runat="server" MaxLength="4" onkeypress="return numeric(event)" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                                </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                    <asp:TextBox ID="txtnameofcollegepost" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                                </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                    <asp:FileUpload ID="FileUpload5" runat="server" CssClass="form-control" /><asp:Button ID="Button4" Text="Upload File" runat="server" BorderColor="Red" OnClick="Button4_Click" CssClass="form-control" />
                                                </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                    <asp:LinkButton ID="lbldipthe" runat="server" Enabled='<%# (Eval("Diploma_final_Year").ToString() ==  "" ?  false : true) %>' Text="Download" OnClick="lbldipthe_Click" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell Style="border: 1px solid">
                                                    <asp:Label ID="Label55" runat="server" Text="Transfer Certificate /Migration" Font-Bold="true" Enabled="false"></asp:Label>
                                                </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                    <asp:TextBox ID="txtboardany" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                                </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                    <asp:TextBox ID="txtyearofpassingany" runat="server" MaxLength="4" onkeypress="return numeric(event)" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                                </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                    <asp:TextBox ID="txtnameofcollegeany" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                                </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                    <asp:FileUpload ID="FileUpload6" runat="server" CssClass="form-control" /><asp:Button ID="Button5" Text="Upload File" runat="server" BorderColor="Red" OnClick="Button5_Click" CssClass="form-control" />
                                                </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                    <asp:LinkButton ID="lblTran" runat="server" Enabled='<%# (Eval("Transfer_Certificate").ToString() ==  "" ?  false : true) %>' Text="Download" OnClick="lblTran_Click" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </div>
                                    <fieldset class="boxBody" style="text-align: center;">
                                        <asp:Label ID="Label6" runat="server" Text=" Undertaking" Font-Size="15pt" ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                    </fieldset>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font-size: large">
                                                I
                                                <asp:Label ID="lblunderteking" runat="server" Font-Underline="true" Font-Bold="true" Text=""></asp:Label>, hereby declare that the information furnished above are true. The attested copies of the mark sheets and Certificate/Degree are enclosed. I understand that if at any stage any of the documents, information furnished is found wrong/incorrect, my candidate and enrolment may be cancelled.</label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-1">
                                            <label style="width: 200px; font-size: large">Date</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtdate" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-7" style="text-align: right;">

                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:Image ID="Image3" Height="40px" Width="170px" runat="server" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <label style="width: 200px; font-size: large;">Signature of Student</label>
                                        </div>
                                    </div>
                                    <div id="divGeneralBodyenrollfee">
                                        <fieldset class="boxBodyInner">
                                            <div class="form-horizontal">
                                                <div class="box-body">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <asp:Table class="boxBody" Width="1100px" Style="border: 1px solid" ID="Table2" runat="server">
                                                                    <asp:TableRow>
                                                                        <asp:TableHeaderCell Style="border: 1px solid" ColumnSpan="3">
                                                                            <asp:Label ID="Label11" runat="server" Text="Enrollment Fee Detail" CssClass="form-control" Enabled="false"></asp:Label>
                                                                        </asp:TableHeaderCell>
                                                                    </asp:TableRow>
                                                                    <asp:TableRow>
                                                                        <asp:TableHeaderCell Style="border: 1px solid">
                                                                            <asp:Label ID="Label8" runat="server" Text="Rs." CssClass="form-control" Enabled="false"></asp:Label>
                                                                        </asp:TableHeaderCell><asp:TableHeaderCell Style="border: 1px solid">
                                                                            <asp:Label ID="Label9" runat="server" Text="Receipt No." CssClass="form-control" Enabled="false"></asp:Label>
                                                                        </asp:TableHeaderCell><asp:TableCell Style="border: 1px solid">
                                                                            <asp:Label ID="Label10" runat="server" Text="Date." Font-Bold="true" CssClass="form-control" Enabled="false"></asp:Label>
                                                                        </asp:TableCell>
                                                                    </asp:TableRow>
                                                                    <asp:TableRow>
                                                                        <asp:TableCell Style="border: 1px solid">
                                                                            <asp:TextBox ID="txtrs" runat="server" BorderColor="Black" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                        </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                                            <asp:TextBox ID="txtreceipt" runat="server" BorderColor="Black" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                        </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                                            <asp:TextBox ID="txtdateenrollmentfee" runat="server" BorderColor="Black" CssClass="form-control" Enabled="false"></asp:TextBox>

                                                                        </asp:TableCell>
                                                                    </asp:TableRow>
                                                                </asp:Table>
                                                                <br />

                                                                <div class="form-group">
                                                                    <div class="col-md-2" style="width: 200px">
                                                                        <label style="width: 200px"></label>
                                                                    </div>
                                                                    <div class="col-md-4" style="visibility: hidden">
                                                                        <asp:TextBox ID="txtID" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="col-md-10">
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <asp:Button ID="btn_save" runat="server" Text="Save" BackColor="#ff9933" ForeColor="White" OnClick="btn_save_Click" CssClass="form-control" Height="30px" Width="100px" />
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
            </div>
        </fieldset>
    </div>
    <br />
    <br />
    <br />
    <%--style="visibility: hidden"--%>
    <div id="printarea">

        <div style="width: 100%; margin-bottom: 10px; margin-left: 1%; margin-right: 1%; margin-top: 5px;">

            <table>
                <tr>
                    <td style="width: 1%"></td>
                    <td style="width: 12%; text-align: left">
                        <asp:Image ID="Image4" runat="server" ImageUrl="~/images/UPDATEDLOGO.jpg" Width="55%" />

                    </td>
                    <td style="width: 65%; text-align: center">
                        <strong>
                            <asp:Label ID="Label16" Font-Size="Large" Text="Teerthanker Mahaveer University, Moradabad" runat="server"></asp:Label></strong><br />
                        <strong>
                            <asp:Label ID="Label17" runat="server" Text="(Established under Govt. of U. P. Act No. 30, 2008)"></asp:Label></strong><br />
                        <strong>
                            <asp:Label ID="Label18" runat="server" Text="Delhi Road,(146 Kms from Delhi on N.H. 24) Moradabad(U.P) India"></asp:Label></strong><br />
                        <strong>
                            <asp:Label ID="Label19" runat="server" Text=" Tel.:+91-2360222 , 2360777"></asp:Label></strong><br />
                        <strong>
                            <asp:Label ID="Label20" runat="server" Text="Email:university@tmu.ac.in;  hr@tmu.ac.in;  Website:www.tmu.ac.in"></asp:Label></strong></td>
                    <td style="width: 10%; text-align: center"></td>
                </tr>
            </table>
            <fieldset class="boxBody" style="text-align: center; border-color: black; background-color: black;">
                <asp:Label ID="Label21" runat="server" Text="ENROLLMENT FORM" Font-Size="15pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
            </fieldset>
            <div class="form-horizontal" style="width: 100%;">
                <div class="box-body" style="width: 100%;">
                    <br />

                    <table style="width: 100%;">
                        <tr>
                            <td style="vertical-align: top; width: 350px;">Programme/Branch : </td>
                            <td style="width: 900px; vertical-align: top">
                                <asp:Label ID="lblProgram" runat="server" BorderColor="Black"></asp:Label></td>

                            <td style="text-align: right; width: 500px;" rowspan="4">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:Image ID="Image5" Height="140px" Width="170px" BorderColor="Black" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 350px;">Year of Admission : </td>
                            <td style="width: 900px; vertical-align: top">
                                <asp:Label ID="lblAdmission" runat="server" BorderColor="Black"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="height: 20px"></td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 350px;">Name of College : </td>
                            <td style="width: 900px; vertical-align: top">
                                <asp:Label ID="lblCN" runat="server" BorderColor="Black"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="height: 20px"></td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 350px;">Date of Birth : </td>
                            <td style="width: 900px; vertical-align: top">
                                <asp:Label ID="lblDOB" runat="server"  BorderColor="Black"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="height: 20px"></td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 350px;">Student Name : </td>
                            <td style="width: 900px; vertical-align: top">
                                <asp:Label ID="lblSTName" runat="server" BorderColor="Black"></asp:Label></td>
                            <td style="vertical-align: top; width: 350px;">Student Name(हिन्दी में) : </td>
                            <td style="width: 700px; vertical-align: top">
                                <asp:Label ID="lblSTNameH" runat="server" BorderColor="Black"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="height: 20px"></td>
                        </tr>
                        <tr>

                            <td style="vertical-align: top; width: 350px;">Father's Name : </td>
                            <td style="width: 700px; vertical-align: top">
                                <asp:Label ID="lblFather" runat="server" BorderColor="Black"></asp:Label></td>
                            <td style="vertical-align: top; width: 350px;">Father's Name(हिन्दी में) : </td>
                            <td style="width: 700px; vertical-align: top">
                                <asp:Label ID="lblFatherH" runat="server" BorderColor="Black"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="height: 20px"></td>
                        </tr>
                        <tr>

                            <td style="vertical-align: top; width: 350px;">Mother's Name : </td>
                            <td style="width: 700px; vertical-align: top">
                                <asp:Label ID="lblMother" runat="server" BorderColor="Black"></asp:Label></td>
                            <td style="vertical-align: top; width: 350px;">Mother's Name(हिन्दी में) : </td>
                            <td style="width: 700px; vertical-align: top">
                                <asp:Label ID="lblMotherH" runat="server" BorderColor="Black"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="height: 20px"></td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 350px;">Gender : </td>
                            <td style="width: 700px; vertical-align: top">
                                <asp:Label ID="lblGender" runat="server" BorderColor="Black"></asp:Label></td>
                            <td style="vertical-align: top; width: 350px;">Nationality : </td>
                            <td style="width: 700px; vertical-align: top">
                                <asp:Label ID="lblNationality" runat="server" BorderColor="Black"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="height: 20px"></td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 350px;">Religion : </td>
                            <td style="width: 700px; vertical-align: top">
                                <asp:Label ID="lblReligion" runat="server" BorderColor="Black"></asp:Label></td>
                            <td style="vertical-align: top; width: 350px;">Category : </td>
                            <td style="width: 700px; vertical-align: top">
                                <asp:Label ID="lblCategory" runat="server" BorderColor="Black"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="height: 20px"></td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 360px;">Minority Status(If Any) : </td>
                            <td style="width: 700px; vertical-align: top">
                                <asp:Label ID="lblMinority" runat="server" BorderColor="Black"></asp:Label></td>
                            <td style="vertical-align: top; width: 350px;"></td>
                            <td style="width: 700px; vertical-align: top">
                                <asp:Label ID="Label31" runat="server" BorderColor="Black"></asp:Label></td>
                        </tr>

                    </table>
                    <fieldset class="boxBody" style="text-align: left;">
                        <asp:Label ID="Label32" runat="server" Text=" CORRESPONDENCE ADDRESS" Font-Size="15pt" ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                    </fieldset>
                    <table>
                        <tr>
                            <td style="vertical-align: top; width: 350px;">Address : </td>
                            <td style="width: 700px; vertical-align: top">
                                <asp:Label ID="lblAdressC" runat="server" BorderColor="Black"></asp:Label></td>
                            <td style="vertical-align: top; width: 350px;">District : </td>
                            <td style="width: 700px; vertical-align: top">
                                <asp:Label ID="lblDistrictC" runat="server" BorderColor="Black"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 350px;">State : </td>
                            <td style="width: 700px; vertical-align: top">
                                <asp:Label ID="lblStateC" runat="server" BorderColor="Black"></asp:Label></td>
                            <td style="vertical-align: top; width: 350px;">Country : </td>
                            <td style="width: 700px; vertical-align: top">
                                <asp:Label ID="lblCountryC" runat="server" BorderColor="Black"></asp:Label></td>
                        </tr>
                    </table>
                    <fieldset class="boxBody" style="text-align: left;">
                        <asp:Label ID="Label37" runat="server" Text=" PERMANENT ADDRESS" Font-Size="15pt" ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                    </fieldset>
                    <table>
                        <tr>
                            <td style="vertical-align: top; width: 350px;">Address : </td>
                            <td style="width: 700px; vertical-align: top">
                                <asp:Label ID="lblAddressP" runat="server" BorderColor="Black"></asp:Label></td>
                            <td style="vertical-align: top; width: 350px;">District : </td>
                            <td style="width: 700px; vertical-align: top">
                                <asp:Label ID="lblDistrictP" runat="server" BorderColor="Black"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 350px;">State : </td>
                            <td style="width: 700px; vertical-align: top">
                                <asp:Label ID="lblStateP" runat="server" BorderColor="Black"></asp:Label></td>
                            <td style="vertical-align: top; width: 350px;">Country : </td>
                            <td style="width: 700px; vertical-align: top">
                                <asp:Label ID="lblCountryP" runat="server" BorderColor="Black"></asp:Label></td>
                        </tr>
                    </table>
                    <fieldset class="boxBody" style="text-align: left;">
                        <asp:Label ID="Label46" runat="server" Text=" CONTACT DETAILS" Font-Size="15pt" ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                    </fieldset>
                    <table>
                        <tr>
                            <td style="vertical-align: top; width: 350px;">Student Mob. : </td>
                            <td style="width: 700px; vertical-align: top">
                                <asp:Label ID="lblStMobile" runat="server" BorderColor="Black"></asp:Label></td>
                            <td style="vertical-align: top; width: 350px;">E-mail ID : </td>
                            <td style="width: 700px; vertical-align: top">
                                <asp:Label ID="lblEmailP" runat="server" BorderColor="Black"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 700px;">Parents Mob. : </td>
                            <td style="width: 700px; vertical-align: top">
                                <asp:Label ID="lblParentsMobile" runat="server" BorderColor="Black"></asp:Label></td>

                        </tr>
                    </table>
                    <br />
                    <br />
                    <fieldset class="boxBody" style="text-align: left;">
                        <asp:Label ID="Label50" runat="server" Text=" EDUCATIONAL DETAIL -[Starting from 10th onward]" Font-Size="15pt" ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                    </fieldset>
                    <div id="divGeneralBodyeducationdetailP">
                        <fieldset class="boxBodyInner">
                            <div class="form-horizontal">
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Table class="boxBody" Width="99%" Style="border: 1px solid" ID="Table3" runat="server">
                                                    <asp:TableRow>
                                                        <asp:TableHeaderCell Style="border: 1px solid">
                                                            <asp:Label ID="Label56" runat="server" Text="Course/Degree Name" Enabled="false"></asp:Label>
                                                        </asp:TableHeaderCell>

                                                        <asp:TableHeaderCell Style="border: 1px solid">
                                                            <asp:Label ID="Label57" runat="server" Text="Board/University" Enabled="false"></asp:Label>
                                                        </asp:TableHeaderCell>
                                                        <asp:TableCell Style="border: 1px solid">
                                                            <asp:Label ID="Label58" runat="server" Font-Bold="true" Text="Year of Passing" Enabled="false"></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell Style="border: 1px solid">
                                                            <asp:Label ID="Label59" runat="server" Font-Bold="true" Text="Name of College/School/Institute" Enabled="false"></asp:Label>
                                                        </asp:TableCell>

                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell Style="border: 1px solid">
                                                            <asp:Label ID="Label62" runat="server" Text="10th" Font-Bold="true" Enabled="false"></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell Style="border: 1px solid">
                                                            <asp:Label ID="TextBox1" runat="server" CssClass="form-control" BorderColor="Black"></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell Style="border: 1px solid">
                                                            <asp:Label ID="TextBox2" runat="server" MaxLength="4" onkeypress="return numeric(event)" CssClass="form-control" BorderColor="Black"></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell Style="border: 1px solid">
                                                            <asp:Label ID="TextBox3" runat="server" CssClass="form-control" BorderColor="Black"></asp:Label>
                                                        </asp:TableCell>

                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell Style="border: 1px solid">
                                                            <asp:Label ID="Label63" runat="server" Text="12th" Font-Bold="true" Enabled="false"></asp:Label>
                                                        </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                            <asp:Label ID="TextBox4" runat="server" CssClass="form-control" BorderColor="Black"></asp:Label>
                                                        </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                            <asp:Label ID="TextBox5" runat="server" MaxLength="4" onkeypress="return numeric(event)" CssClass="form-control" BorderColor="Black"></asp:Label>
                                                        </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                            <asp:Label ID="TextBox6" runat="server" CssClass="form-control" BorderColor="Black"></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell Style="border: 1px solid">
                                                            <asp:Label ID="Label64" runat="server" Text="Graduation" Font-Bold="true" Enabled="false"></asp:Label>
                                                        </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                            <asp:Label ID="TextBox7" runat="server" Enabled="false" CssClass="form-control" BorderColor="Black"></asp:Label>
                                                        </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                            <asp:Label ID="TextBox8" runat="server" MaxLength="4" onkeypress="return numeric(event)" CssClass="form-control" BorderColor="Black"></asp:Label>
                                                        </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                            <asp:Label ID="TextBox9" runat="server" CssClass="form-control" BorderColor="Black"></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell Style="border: 1px solid">
                                                            <asp:Label ID="Label65" runat="server" Text="Post-Graduation" Font-Bold="true" Enabled="false"></asp:Label>
                                                        </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                            <asp:Label ID="TextBox10" runat="server" BorderColor="Black" CssClass="form-control"></asp:Label>
                                                        </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                            <asp:Label ID="TextBox11" runat="server" MaxLength="4" onkeypress="return numeric(event)" BorderColor="Black" CssClass="form-control"></asp:Label>
                                                        </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                            <asp:Label ID="TextBox12" runat="server" BorderColor="Black" CssClass="form-control"></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell Style="border: 1px solid">
                                                            <asp:Label ID="Label66" runat="server" Text="Transfer Certificate /Migration" Font-Bold="true" Enabled="false"></asp:Label>
                                                        </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                            <asp:Label ID="TextBox13" runat="server" BorderColor="Black" CssClass="form-control"></asp:Label>
                                                        </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                            <asp:Label ID="TextBox14" runat="server" MaxLength="4" onkeypress="return numeric(event)" BorderColor="Black" CssClass="form-control"></asp:Label>
                                                        </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                            <asp:Label ID="TextBox15" runat="server" BorderColor="Black" CssClass="form-control"></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </div>
                                            <fieldset class="boxBody" style="text-align: center;">
                                                <asp:Label ID="Label67" runat="server" Text=" Undertaking" Font-Size="15pt" ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                            </fieldset>
                                            <div class="form-group">
                                                <div class="col-md-12">
                                                    <label style="font-size: large">
                                                        I
                                    <asp:Label ID="Label68" runat="server" Font-Underline="true" Font-Bold="true" Text=""></asp:Label>, hereby declare that the information furnished above are true. The attested copies of the mark sheets and Certificate/Degree are enclosed. I understand that if at any stage any of the documents, information furnished is found wrong/incorrect, my candidate and enrolment may be cancelled.</label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-1">
                                                    <label style="width: 200px; font-size: large">Date</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:Label ID="lblDAte" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:Label>
                                                </div>
                                                <div class="col-md-7" style="text-align: right;">

                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Image ID="Image6" Height="40px" Width="170px" runat="server" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <label style="width: 200px; font-size: large;">Signature of Student</label>
                                                </div>
                                            </div>
                                            <div id="divGeneralBodyenrollfeeP">
                                                <fieldset class="boxBodyInner">
                                                    <div class="form-horizontal">
                                                        <div class="box-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="form-group">
                                                                        <asp:Table class="boxBody" Width="99%" Style="border: 1px solid" ID="Table4" runat="server">
                                                                            <asp:TableRow>
                                                                                <asp:TableHeaderCell Style="border: 1px solid" ColumnSpan="3">
                                                                                    <asp:Label ID="Label69" runat="server" Text="Enrollment Fee Detail" CssClass="form-control" Enabled="false"></asp:Label>
                                                                                </asp:TableHeaderCell>
                                                                            </asp:TableRow>
                                                                            <asp:TableRow>
                                                                                <asp:TableHeaderCell Style="border: 1px solid">
                                                                                    <asp:Label ID="Label70" runat="server" Text="Rs." CssClass="form-control" Enabled="false"></asp:Label>
                                                                                </asp:TableHeaderCell><asp:TableHeaderCell Style="border: 1px solid">
                                                                                    <asp:Label ID="Label71" runat="server" Text="Receipt No." CssClass="form-control" Enabled="false"></asp:Label>
                                                                                </asp:TableHeaderCell><asp:TableCell Style="border: 1px solid">
                                                                                    <asp:Label ID="Label72" runat="server" Text="Date." Font-Bold="true" CssClass="form-control" Enabled="false"></asp:Label>
                                                                                </asp:TableCell>
                                                                            </asp:TableRow>
                                                                            <asp:TableRow>
                                                                                <asp:TableCell Style="border: 1px solid">
                                                                                    <asp:Label ID="TextBox17" runat="server" BorderColor="Black" CssClass="form-control" Enabled="false"></asp:Label>
                                                                                </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                                                    <asp:Label ID="TextBox18" runat="server" BorderColor="Black" CssClass="form-control" Enabled="false"></asp:Label>
                                                                                </asp:TableCell><asp:TableCell Style="border: 1px solid">
                                                                                    <asp:Label ID="TextBox19" runat="server" BorderColor="Black" CssClass="form-control" Enabled="false"></asp:Label>

                                                                                </asp:TableCell>
                                                                            </asp:TableRow>
                                                                        </asp:Table>
                                                                        <br />

                                                                        <div class="form-group">
                                                                            <div class="col-md-2" style="width: 200px">
                                                                                <label style="width: 200px"></label>
                                                                            </div>
                                                                            <div class="col-md-4" style="visibility: hidden">
                                                                                <asp:Label ID="TextBox20" runat="server" CssClass="form-control"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="col-md-10">
                                                                        </div>

                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>

                    </div>
                </div>
            </div>
        </div>



    </div>

</asp:Content>
