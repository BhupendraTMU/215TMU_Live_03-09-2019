<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="EnrollmentApproval.aspx.cs" Inherits="Faculty_EnrollmentApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ty" runat="server"></asp:ScriptManager>
    <fieldset>
        <div class="text-right" style="padding-left: 250px">

            <asp:Button ID="BtnSubmit" runat="server" Text="Approved" OnClick="BtnSubmit_Click" ForeColor="White" CssClass="btn" BackColor="#ff9900" />
            <asp:Button ID="BtnRejected" runat="server" Text="Rejected" ForeColor="White" OnClick="BtnRejected_Click" CssClass="btn" BackColor="#ff9900" />

        </div>
    </fieldset>
    <fieldset class="boxBodyInner">
        <br />
        <asp:GridView ID="grdenrollmentapprovallist" runat="server" DataKeyNames="Student_Number" AlternatingRowStyle-CssClass="danger" PageSize="10" 
            AllowPaging="true" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" Visible="true" OnPageIndexChanging="grdenrollmentapprovallist_PageIndexChanging" >
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle CssClass="csspager" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <%# Container.DataItemIndex +1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student Number" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblemployeecode" runat="server" Text='<%# Bind("Student_Number") %>'></asp:Label>
                        <asp:HiddenField ID="Hfemployeecode" Value='<%# Eval("Student_Number") %>' runat="server" />
                        <asp:HiddenField ID="Hfhodname" Value='<%# Eval("Student_Number") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="View" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbutton" OnClick="lnkbutton_Click" runat="server">View</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lblstudentName" runat="server" Text='<%# Eval("Student_Name") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Programme" ItemStyle-Width="3%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lblprogram" runat="server" Text='<%# Eval("Programee_Branch") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="College Name" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblStudentCategory" runat="server" Text='<%#Eval("Name_Of_College") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Academic Year" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblAcademicYear" runat="server" Text='<%#Eval("Year_Of_Admission") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remark" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:TextBox ID="txtRemark" runat="server" Enabled='<%# Eval("txtMarksEnableDesable").ToString().Equals("true") %>' Text='<%#Eval("RejectRemarks") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Principal Approval" ItemStyle-Width="2%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblprincipalapproval" runat="server" Text='<%# Eval("Pri_Approval") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Admission Approval" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblDirectorAdmission" runat="server" Text='<%#Eval("Director_Approval") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Enrollment Dept. Approval" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblRegistrar" runat="server" Text='<%#Eval("EnrollmentDept_Approval") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" HeaderText="Select" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:CheckBox ID="Chkemployee" Enabled='<%# Eval("txtMarksEnableDesable").ToString().Equals("true") %>' runat="server" AutoPostBack="true" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
        </asp:GridView>
        <div>
            <asp:Panel ID="pnlGridViewDetails" CssClass="modalPopup" Width="65%" runat="server" Style="display: none; overflow: scroll" Height="85%">

                <div class="header">
                    <b>
                        <asp:Label ID="lblNotification" runat="server" Text="Enrollment Form "></asp:Label></b>
                    <div class="close">
                        <asp:Button ID="btnclose" OnClick="btnclose_Click" runat="server" Text="X" />
                    </div>
                </div>
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

                                        <%--<div class="form-group">
                                <div class="col-md-12">
                                    <label style=" font-size:large; font:bold; color:black">(Please fill this form in Capital Letters only and S.No.4-7 as per High School Certificate)</label>
                                </div>
                                </div>--%>
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
                                                <label style="width: 200px">Father's Name</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtfathername" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
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
                                                <label style="width: 200px">Gender</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtgender" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>

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
                                                <label style="width: 200px">Nationality</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtnationality" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label style="width: 200px">Religion</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtreligion" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-2">
                                                <label style="width: 200px">Category</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtcategory" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                            </div>
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
                                                                <asp:Label ID="Label42" runat="server" Text="Course/Degree Name" CssClass="form-control" Enabled="false"></asp:Label>
                                                            </asp:TableHeaderCell>

                                                            <asp:TableHeaderCell Style="border: 1px solid">
                                                                <asp:Label ID="Label43" runat="server" CssClass="form-control" Text="Board/University" Enabled="false"></asp:Label>
                                                            </asp:TableHeaderCell>
                                                            <asp:TableCell Style="border: 1px solid">
                                                                <asp:Label ID="Label44" runat="server" CssClass="form-control" Font-Bold="true" Text="Year of Passing" Enabled="false"></asp:Label>
                                                            </asp:TableCell>
                                                            <asp:TableCell Style="border: 1px solid">
                                                                <asp:Label ID="Label45" runat="server" CssClass="form-control" Font-Bold="true" Text="Name of College/School/Institute" Enabled="false"></asp:Label>
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                        <asp:TableRow>
                                                            <asp:TableCell Style="border: 1px solid">
                                                                <asp:Label ID="Label51" runat="server" Text="10th" Font-Bold="true" CssClass="form-control" Enabled="false"></asp:Label>
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
                                                        </asp:TableRow>

                                                        <asp:TableRow>
                                                            <asp:TableCell Style="border: 1px solid">
                                                                <asp:Label ID="Label52" runat="server" Text="12th" Font-Bold="true" CssClass="form-control" Enabled="false"></asp:Label>
                                                            </asp:TableCell>
                                                            <asp:TableCell Style="border: 1px solid">
                                                                <asp:TextBox ID="txtboard12" runat="server" CssClass="form-control" BorderColor="Black"></asp:TextBox>
                                                            </asp:TableCell>
                                                            <asp:TableCell Style="border: 1px solid">
                                                                <asp:TextBox ID="txtyearofpassing12" runat="server" MaxLength="4" onkeypress="return numeric(event)" CssClass="form-control" BorderColor="Black"></asp:TextBox>
                                                            </asp:TableCell>
                                                            <asp:TableCell Style="border: 1px solid">
                                                                <asp:TextBox ID="txtnameofcollege12" runat="server" CssClass="form-control" BorderColor="Black"></asp:TextBox>
                                                            </asp:TableCell>
                                                        </asp:TableRow>

                                                        <asp:TableRow>
                                                            <asp:TableCell Style="border: 1px solid">
                                                                <asp:Label ID="Label53" runat="server" Text="Graduation" Font-Bold="true" CssClass="form-control" Enabled="false"></asp:Label>
                                                            </asp:TableCell>
                                                            <asp:TableCell Style="border: 1px solid">
                                                                <asp:TextBox ID="txtboardgraduation" runat="server" CssClass="form-control" BorderColor="Black"></asp:TextBox>
                                                            </asp:TableCell>
                                                            <asp:TableCell Style="border: 1px solid">
                                                                <asp:TextBox ID="txtyearofpassinggraduation" runat="server" MaxLength="4" onkeypress="return numeric(event)" CssClass="form-control" BorderColor="Black"></asp:TextBox>
                                                            </asp:TableCell>
                                                            <asp:TableCell Style="border: 1px solid">
                                                                <asp:TextBox ID="txtnameofcollegegraduation" runat="server" CssClass="form-control" BorderColor="Black"></asp:TextBox>
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                        <asp:TableRow>
                                                            <asp:TableCell Style="border: 1px solid">
                                                                <asp:Label ID="Label54" runat="server" Text="Post-Graduation" Font-Bold="true" CssClass="form-control" Enabled="false"></asp:Label>
                                                            </asp:TableCell>
                                                            <asp:TableCell Style="border: 1px solid">
                                                                <asp:TextBox ID="txtboardpost" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                                            </asp:TableCell>
                                                            <asp:TableCell Style="border: 1px solid">
                                                                <asp:TextBox ID="txtyearofpassingpost" runat="server" MaxLength="4" onkeypress="return numeric(event)" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                                            </asp:TableCell>
                                                            <asp:TableCell Style="border: 1px solid">
                                                                <asp:TextBox ID="txtnameofcollegepost" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                                            </asp:TableCell>

                                                        </asp:TableRow>
                                                        <asp:TableRow>
                                                            <asp:TableCell Style="border: 1px solid">
                                                                <asp:Label ID="Label55" runat="server" Text="Any Other" Font-Bold="true" CssClass="form-control" Enabled="false"></asp:Label>
                                                            </asp:TableCell>
                                                            <asp:TableCell Style="border: 1px solid">
                                                                <asp:TextBox ID="txtboardany" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                                            </asp:TableCell>
                                                            <asp:TableCell Style="border: 1px solid">
                                                                <asp:TextBox ID="txtyearofpassingany" runat="server" MaxLength="4" onkeypress="return numeric(event)" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                                            </asp:TableCell>
                                                            <asp:TableCell Style="border: 1px solid">
                                                                <asp:TextBox ID="txtnameofcollegeany" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>

                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        <label style="font-size: large">[In case of gap/discontinuity in education, photo copy of affidavit on Stamp Paper of Rs. 10/- is to be enclosed]</label>
                                                    </div>
                                                </div>
                                                <fieldset class="boxBody" style="text-align: center;">
                                                    <asp:Label ID="Label6" runat="server" Text=" Undertaking" Font-Size="15pt" ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                                </fieldset>
                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        <label style="font-size: large">
                                                            I
                                                <asp:Label ID="lblunderteking" runat="server" Font-Underline="true" Font-Bold="true" Text=""></asp:Label>
                                                            , hereby declare that the information furnished above are true. The attested copies of the mark sheets and Certificate/Degree are enclosed. I understand that if at any stage any of the documents, information furnished is found wrong/incorrect, my candidate and enrolment may be cancelled.</label>
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
                                                                                    </asp:TableHeaderCell>

                                                                                    <asp:TableHeaderCell Style="border: 1px solid">
                                                                                        <asp:Label ID="Label9" runat="server" Text="Receipt No." CssClass="form-control" Enabled="false"></asp:Label>
                                                                                    </asp:TableHeaderCell>
                                                                                    <asp:TableCell Style="border: 1px solid">
                                                                                        <asp:Label ID="Label10" runat="server" Text="Date." Font-Bold="true" CssClass="form-control" Enabled="false"></asp:Label>
                                                                                    </asp:TableCell>
                                                                                </asp:TableRow>
                                                                                <asp:TableRow>
                                                                                    <asp:TableCell Style="border: 1px solid">
                                                                                        <asp:TextBox ID="txtrs" runat="server" BorderColor="Black" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                                    </asp:TableCell>
                                                                                    <asp:TableCell Style="border: 1px solid">
                                                                                        <asp:TextBox ID="txtreceipt" runat="server" BorderColor="Black" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                                    </asp:TableCell>
                                                                                    <asp:TableCell Style="border: 1px solid">
                                                                                        <asp:TextBox ID="txtdateenrollmentfee" runat="server" BorderColor="Black" CssClass="form-control" Enabled="false"></asp:TextBox>

                                                                                    </asp:TableCell>
                                                                                </asp:TableRow>
                                                                            </asp:Table>
                                                                            <br />
                                                                            <div id="divGeneralBodyUploaddocument">
                                                                                <fieldset class="boxBodyInner">
                                                                                    <div class="form-horizontal">
                                                                                        <div class="box-body">
                                                                                            <div class="row">
                                                                                                <div class="col-md-12">
                                                                                                    <div class="form-group">
                                                                                                    <%--<div class="col-md-2">
                                                                                                <label style="width: 200px">Upload Document</label>
                                                                                            </div>--%>
                                                                                                    <%--  <div class="col-md-2">
                                                                                                <asp:UpdatePanel ID="updatePanel5" runat="server">
                                                                                                    <ContentTemplate>
                                                                                                        <asp:DropDownList ID="drpuploaddpocument" runat="server" AutoPostBack="true" CssClass="form-control">
                                                                                                            <asp:ListItem Text="---SELECT---" Value="0"></asp:ListItem>
                                                                                                            <asp:ListItem Text="10th Marksheet" Value="1"></asp:ListItem>
                                                                                                            <asp:ListItem Text="12th MArksheet" Value="2"></asp:ListItem>
                                                                                                            <asp:ListItem Text="Diploma Marksheet Final Year" Value="3"></asp:ListItem>
                                                                                                            <asp:ListItem Text="UG Marksheet Final Year" Value="4"></asp:ListItem>
                                                                                                            <asp:ListItem Text="Transfer Certificate Original" Value="5"></asp:ListItem>
                                                                                                            <asp:ListItem Text="Character Certificate Original" Value="6"></asp:ListItem>
                                                                                                            <asp:ListItem Text="Migration Original" Value="7"></asp:ListItem>
                                                                                                            <asp:ListItem Text="Gap Affidavit" Value="8"></asp:ListItem>
                                                                                                            <asp:ListItem Text="Domicile" Value="9"></asp:ListItem>
                                                                                                            <asp:ListItem Text="Student Aadhar" Value="10"></asp:ListItem>
                                                                                                            <asp:ListItem Text="Guardian Aadhar" Value="11"></asp:ListItem>
                                                                                                            <asp:ListItem Text="Admission Form Original" Value="12"></asp:ListItem>
                                                                                                        </asp:DropDownList>
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>
                                                                                            </div>--%>
                                                                                                    <%--<div class="col-md-2">
                                                                                                <%-- <div class="input-group" >--%>
                                                                                                    <%--<span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark" ></span></span>--%>
                                                                                                    <%-- <asp:FileUpload ID="FileUpload2" runat="server" />

                                                                                            </div>
                                                                                            <div class="col-md-1">
                                                                                            </div>
                                                                                            <div class="col-md-4">
                                                                                                <asp:Button ID="UploadBtn" Text="Upload File" runat="server" CssClass="form-control"  />
                                                                                            </div>--%>
                                                                                                    <div class="form-group">
                                                                                                        <div class="col-md-2" style="width: 200px">
                                                                                                            <label style="width: 200px"></label>
                                                                                                        </div>
                                                                                                        <div class="col-md-4" style="visibility: hidden">
                                                                                                            <asp:TextBox ID="txtID" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-md-4" style="visibility: hidden">
                                                                                                            <asp:TextBox ID="txtstudentnumber" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>

                                                                                                    </div>

                                                                                                </div>





                                                                                                <div class="form-group">
                                                                                                    <div class="col-md-10">
                                                                                                        <asp:GridView ID="grdAttachment" runat="server" BackColor="White" EmptyDataText="There are no data records to display." CssClass="myTableClass" AutoGenerateColumns="false" ShowFooter="true" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Horizontal">
                                                                                                            <Columns>
                                                                                                                <asp:TemplateField ControlStyle-BorderStyle="Solid">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:LinkButton ID="lbl10th" runat="server" Enabled='<%# (Eval("HighSchoolMarksheet").ToString() == "" ?  false : true) %>' Text="10th Marksheet" OnClick="lbl10th_Click" />
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField ControlStyle-BorderStyle="Solid">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:LinkButton ID="lbl12th" runat="server" Enabled='<%# (Eval("InterMarksheet").ToString() ==  "" ?  false : true) %>' Text="12th Marksheet" OnClick="lbl12th_Click" />
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField ControlStyle-BorderStyle="Solid">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:LinkButton ID="lbldipthe" runat="server" Enabled='<%# (Eval("Diploma_final_Year").ToString() ==  "" ?  false : true) %>' Text="Diploma Marksheet" OnClick="lbldipthe_Click" />
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>


                                                                                                                <asp:TemplateField ControlStyle-BorderStyle="Solid">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:LinkButton ID="lblUG" runat="server" Enabled='<%# (Eval("UG_Final_Year").ToString() ==  "" ?  false : true) %>' Text="UG Marksheet" OnClick="lblUG_Click" />
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField ControlStyle-BorderStyle="Solid">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:LinkButton ID="lblTran" runat="server" Enabled='<%# (Eval("Transfer_Certificate").ToString() ==  "" ?  false : true) %>' Text="Transfer Certificate" OnClick="lblTran_Click" />
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField ControlStyle-BorderStyle="Solid">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:LinkButton ID="lblCharacter" runat="server" Enabled='<%# (Eval("Character_Certificate").ToString() ==  "" ?  false : true) %>' Text="Character Certificate" OnClick="lblCharacter_Click" />
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField ControlStyle-BorderStyle="Solid">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:LinkButton ID="lblMigration" runat="server" Text="Migration" Enabled='<%# (Eval("Migration").ToString() ==  "" ?  false : true) %>' Height="42px" OnClick="lblMigration_Click" />
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField ControlStyle-BorderStyle="Solid">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:LinkButton ID="lblGap" runat="server" Text="Gap Affidavit" Enabled='<%# (Eval("Gap_Affidavit").ToString() ==  "" ?  false : true) %>' OnClick="lblGap_Click" />
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField ControlStyle-BorderStyle="Solid">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:LinkButton ID="lblDomicile" runat="server" Text="Domicile" Height="42px" Enabled='<%# (Eval("Domicile").ToString() ==  "" ?  false : true) %>' OnClick="lblDomicile_Click" />
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField ControlStyle-BorderStyle="Solid">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:LinkButton ID="lblAadhar" runat="server" Text="Aadhar" Height="42px" Enabled='<%# (Eval("Student_Aadhar").ToString() ==  "" ?  false : true ) %>' OnClick="lblAadhar_Click" />
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField ControlStyle-BorderStyle="Solid">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:LinkButton ID="lblGuardian" runat="server" Text="Guardian Aadhar" Enabled='<%# (Eval("Guardian_Aadhar").ToString() ==  "" ?  false : true) %>' OnClick="lblGuardian_Click" />
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <%--<asp:TemplateField ControlStyle-BorderStyle="Solid">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:LinkButton ID="lblAdmission" runat="server" Text="Admission Form" Enabled='<%# (Eval("Addmission_Form").ToString() ==  "" ?  false : true) %>' OnClick="lblAdmission_Click" />
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>--%>

                                                                                                            </Columns>
                                                                                                            <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />

                                                                                                        </asp:GridView>
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
        </div>
        </div>
                        </div>
                    </div>
    </fieldset>
    </div>

        </fieldset>


    </div>
                 
          </asp:Panel>  
         
         
        <asp:Button ID="btnDummy" runat="server" Style="display: none;" />
    <asp:ModalPopupExtender ID="GridViewDetails" runat="server" TargetControlID="btnDummy"
        PopupControlID="pnlGridViewDetails" BackgroundCssClass="modalBackground" />
    </div>
        

        </fieldset>
</asp:Content>

