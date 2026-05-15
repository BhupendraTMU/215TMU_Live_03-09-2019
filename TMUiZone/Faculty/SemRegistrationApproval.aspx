<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="SemRegistrationApproval.aspx.cs" Inherits="Faculty_SemRegistrationApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <style>
  
        #confirmModalB .modal-dialog.modalPopup {
            width: 95%;
        }

        table thead tr th:first-child, .table > tbody > tr > th:first-child {
            border-left: 1px solid #60594f;
            padding: 5px 8px;
        }
    </style>


           <script type="text/javascript">
               function PrintDiv() {
                   var divToPrint = document.getElementById('printarea');
                   var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px');
                   popupWin.document.open();
                   popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
                   popupWin.document.close();
               }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <fieldset class="boxBodyInner">
       <h2> Semester Regisration Approval</h2>
        <table cellpadding="0px" cellspacing="0px">
            <caption>
                <br />

                <tr>
                    <td>Academic Year  </td>
                    <td style="width: 10px"></td>
                    <td>
                        <asp:DropDownList ID="drpAcademicYear" Width="150px" Height="20px" runat="server" OnSelectedIndexChanged="drpAcademicYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                    <td style="width: 20px"></td>
                    <td>Program<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpCourse" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator></td>
                    <td style="width: 10px"></td>
                    <td>
                        <asp:DropDownList ID="drpCourse" runat="server" AutoPostBack="true" Height="20px" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged" Width="200px"></asp:DropDownList>

                    </td>
                    <td style="width: 20px"></td>
                    <td>Semester/Year
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpSemester" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 10px"></td>
                    <td>
                        <asp:DropDownList ID="drpSemester" runat="server" AutoPostBack="true" Height="20px" Width="120px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 10px"></td>





                    <td style="width: 10px"></td>
                    <td>
                        <asp:Button ID="btnShow" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" OnClick="btnShow_Click" Height="30px" Width="90px" Text="SHOW" />
                       
                    </td>

                    <td style="width: 10px"></td>
                    <td>
                      <asp:Button ID="BtnSubmit" runat="server" Visible="false" Text="Submit" OnClick="BtnSubmit_Click" CssClass="btn" />   </td>

                </tr>
        </table>




    </fieldset>
    <fieldset class="boxBodyInner">
        <div class="text-center">
             <asp:HiddenField ID="HfEnrollment_No" runat="server" />
                                                <asp:HiddenField ID="HFsemester" runat="server" />
            <%--  OnPageIndexChanging="GrdExamList_PageIndexChanging"--%>
            <asp:GridView ID="GrdExamList" runat="server" DataKeyNames="Enrollment No" AlternatingRowStyle-CssClass="danger" PageSize="20"
                PagerStyle-Font-Bold="true" PagerStyle-HorizontalAlign="Center" AllowPaging="true" OnPageIndexChanging="GrdExamList_PageIndexChanging"
                AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" Visible="true">
                <Columns>
                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <%# Container.DataItemIndex +1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Enrolment No" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lblEnrollNo" runat="server" Text='<%# Bind("[Enrollment No]") %>'></asp:Label>
                            <asp:HiddenField ID="HfEnrollmentNo" Value='<%# Eval("[Enrollment No]") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="View" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnview" runat="server" Text="View" OnClick="btnview_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--OnClick="btnview_Click"--%>
                    <asp:TemplateField HeaderText="Name" ItemStyle-Width="4%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-left">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Student Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course" ItemStyle-Width="4%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-left">
                        <ItemTemplate>
                            <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("Course") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course Name" ItemStyle-Width="5%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                        <ItemTemplate>
                            <asp:Label ID="lblcourseName" runat="server" Text='<%#Eval("Course Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Semester/Year" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("Semester") %>'></asp:Label>
                            <asp:HiddenField ID="HfSesterr" Value='<%# Eval("Semester") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-center">
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true"  OnCheckedChanged="chkAll_CheckedChanged" Checked="true" />
                            Select All
                                   
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkStudent" runat="server"   OnCheckedChanged="chkStudent_CheckedChanged" AutoPostBack="true" Checked="true" />
                          
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
            </asp:GridView>
        </div>
    </fieldset>
     <div id="confirmModalB" class="modal fade confirm-modal" role="dialog">

                <div class="modal-dialog modalPopup border-box">
                    <div class="modal-content">
                     <div class="modal-header">
                     <button type="button" name="btn_close" id="Button1" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="clearfix">
                            <div class="col-sm-12">
                                <asp:Panel runat="server" ID="Panel2">
                                    <asp:Panel ID="PnlMain" runat="server">
                                        <%-- <asp:textbox id="TextBox1"  runat="server" style="border: 1px solid black; height: 125px; margin-left: auto; width: 550px;" textmode="MultiLine"></asp:textbox>   --%>
                                        <div style="text-align: right; width: 100%; margin-bottom: -30px;">
                                            <asp:Button ID="BtnPrint" OnClientClick="PrintDiv();" runat="server" Width="10%" Style="margin-top: 5px;" Text="Print" Font-Bold="true" BorderColor="WhiteSmoke" />
                                        </div>
                <div id="printarea" style="border-style: none">
                    <div style="text-align: center; width: 100%" id="logoDiv">
                        <table style="width: 80%; margin-left: 10%;">
                            <tr>
                                <td style="width: 200px;" align="left">
                                    <img src="~/images/rightlogo.png" id="Image1" runat="server" width="100" height="102" visible="true" />
                                </td>
                                <td style="width: 80%; vertical-align: middle" align="left">
                                    <asp:Label ID="LblTitle" runat="server" Text="Teerthanker Mahaveer University,Moradabad" Style="font-size: 25px;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="panel-heading" style="background-color: #2b5b69">
                        <center>
                            <div class="panel-title" style="fit-position: center;">
                                <b>
                                    <p style="color: white; font-size: 22px">
                                        Student Semester Registration Form  &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                    </p>
                                </b>
                            </div>
                        </center>
                    </div>
                    <fieldset class="boxBodyInner">
                        <table id="table3" style="margin-left: 5%; margin-right: 10%; font: 14px; font-family: Times New Roman;" width="100%">
                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="lblProgram" runat="server" Text="1. Programme Enrolled for"></asp:Label>
                                    </strong>
                                </td>
                                <td style="width: 20%;">
                                    <strong>
                                        <asp:TextBox ID="txtProgramName" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                    </strong>
                                </td>
                                <td style="width: 15%" rowspan="7">
                                    <strong>

                                        <asp:Image ID="ImgStudent" runat="server" Style="border: 1px solid #d4d7d8; width: 150px; height: 150px;" ImageUrl='data:CarImages/png;base64,<%# Eval("Student Image") != System.DBNull.Value ? Convert.ToBase64String((byte[])Eval("Student Image")) : string.Empty %>'></asp:Image>

                                    </strong>
                                    <table style="margin-top: 10px;">
                                        <tr>
                                            <td><strong>Contact No.</strong></td>
                                            <td>
                                                <strong>
                                                    <asp:TextBox ID="TxtContactNo" runat="server" BorderStyle="None" Style="margin-top: 2px; width: 90px; height: 12px;" ReadOnly="true"></asp:TextBox>
                                                </strong>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="lblAcademic" runat="server" Text="2. Academic Session "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>

                                        <asp:TextBox ID="txtAcademic" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                    </strong>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="lblAcategory" runat="server" Text="3. Admission Category"></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="TxtACategory" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" CssClass="auto-style9" Style="text-transform: uppercase;"></asp:TextBox>
                                    </strong>
                                </td>


                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="LblStudentName" runat="server" Text="4. Full Name of the Student"></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="TxtStudentFName" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="lblFatherName" runat="server" Text="5. Father's Name"></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="TxtFatherName" runat="server" BorderStyle="None" Width="300px" Style="text-transform: uppercase;"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="LblFatherMobile" runat="server" Text="6. Father's Mobile No"></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="TxtFatherMobile" runat="server" BorderStyle="None" Width="300px"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">

                                    <strong>

                                        <asp:Label ID="lblFathersOcc" runat="server" Text="7. Father's Occupation "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="TxtFathersOcc" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">

                                    <strong>

                                        <asp:Label ID="LblMotherName" runat="server" Text="8. Mother's Name "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="TxtMotherName" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                    </strong>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="lblMothersMobile" runat="server" Text="9.  Mother’s Mobile No "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="TxtMothersMobile" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">

                                    <strong>

                                        <asp:Label ID="LblHindiMothersOcc" runat="server" Text="10. Mother's Occupation "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="TxtHindiMotherOcc" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="lblNationality" runat="server" Text="11. Nationality"></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="TxtNationality" runat="server" BorderStyle="None" MaxLength="12" Width="300px" ReadOnly="true"></asp:TextBox>
                                    </strong>
                                </td>
                                <td style="width: 25%" rowspan="1"></td>
                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="lblDOB" runat="server" Text="12. Date of Birth"></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="TxtDOB" runat="server" BorderStyle="None" Width="300px" Style="text-transform: uppercase; resize: none" ReadOnly="true"></asp:TextBox>
                                    </strong>
                                </td>
                                <td style="visibility: hidden;"></td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="lblGender" runat="server" Text="13.  Gender "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="TxtGender" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase; resize: none"></asp:TextBox>
                                    </strong>
                                </td>
                                <td style="visibility: hidden"></td>
                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="Label1" runat="server" Text="14.  Category "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtCategory" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase; resize: none"></asp:TextBox>
                                    </strong>
                                </td>
                                <td style="visibility: hidden"></td>
                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="Label2" runat="server" Text="15.  Emergency Contact No "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtEmergencyContact" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase; resize: none"></asp:TextBox>
                                    </strong>
                                </td>
                                <td style="visibility: hidden"></td>
                            </tr>
                        </table>
                        <br />

                        <fieldset class="boxBodyHeader">
                            <asp:Label ID="lblCorres" runat="server"
                                Text="Correspondence Address " Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                        </fieldset>
                        <br />
                        <table style="margin-left: 5%; margin-right: 10%; font: 14px; font-family: Times New Roman;" width="100%">
                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="Label3" runat="server" Text="1. Address"></asp:Label>
                                    </strong>
                                </td>
                                <td style="width: 50%;">
                                    <strong>
                                        <asp:TextBox ID="txtAddress" runat="server"  Width="300px" Style="text-transform: uppercase;"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="Label4" runat="server" Text="2. City"></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtCity" runat="server"  Width="300px"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">

                                    <strong>

                                        <asp:Label ID="Label5" runat="server" Text="3. State "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtState" runat="server"  Width="300px"  Style="text-transform: uppercase;"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">

                                    <strong>

                                        <asp:Label ID="Label6" runat="server" Text="4. Country "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtCountry" runat="server"  Width="300px"  Style="text-transform: uppercase;"></asp:TextBox>
                                    </strong>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="Label7" runat="server" Text="5. Pin "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtPin" runat="server"  Width="300px"  Style="text-transform: uppercase;"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">

                                    <strong>

                                        <asp:Label ID="Label8" runat="server" Text="6. Tel "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtTel" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">

                                    <strong>

                                        <asp:Label ID="Label9" runat="server" Text="7. Fax "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtFax" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                    </strong>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="Label10" runat="server" Text="8.  Mobile "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtMobile" runat="server"  Width="300px"  Style="text-transform: uppercase;"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">

                                    <strong>

                                        <asp:Label ID="Label11" runat="server" Text="9. Email "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtEmail" runat="server"  Width="300px"  Style="text-transform: uppercase;"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                        </table>
                        <br />

                        <fieldset class="boxBodyHeader">
                            <asp:Label ID="Label12" runat="server"
                                Text="Permanent Address [Address of Parents]" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                        </fieldset>
                        <br />
                        <table style="margin-left: 5%; margin-right: 10%; font: 14px; font-family: Times New Roman;" width="100%">
                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="Label13" runat="server" Text="1. Address"></asp:Label>
                                    </strong>
                                </td>
                                <td style="width: 50%;">
                                    <strong>
                                        <asp:TextBox ID="txtPAddress" runat="server" BorderStyle="None" Width="300px" Style="text-transform: uppercase;"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="Label14" runat="server" Text="2. City"></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtPCity" runat="server" BorderStyle="None" Width="300px"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">

                                    <strong>

                                        <asp:Label ID="Label15" runat="server" Text="3. State "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtPState" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">

                                    <strong>

                                        <asp:Label ID="Label16" runat="server" Text="4. Country "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtPCountry" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                    </strong>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="Label17" runat="server" Text="5.  Pin "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtPPin" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">

                                    <strong>

                                        <asp:Label ID="Label18" runat="server" Text="6. Tel "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtPTel" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">

                                    <strong>

                                        <asp:Label ID="Label19" runat="server" Text="7. Fax "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtPFax" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                    </strong>
                                </td>
                            </tr>

                        </table>

                        <br />

                        <fieldset class="boxBodyHeader">
                            <asp:Label ID="Label20" runat="server"
                                Text="Local Guardian(s) to be contacted in emergency" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                        </fieldset>
                        <br />
                        <table style="margin-left: 5%; margin-right: 10%; font: 14px; font-family: Times New Roman;" width="100%">
                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="Label30" runat="server" Text="1. Full LG Name"></asp:Label>
                                    </strong>
                                </td>
                                <td style="width: 50%;">
                                    <strong>
                                        <asp:TextBox ID="txtLGName" runat="server" CssClass="underlined" Width="300px"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="Label21" runat="server" Text="2. Address"></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtLGAddress" runat="server" CssClass="underlined" Width="300px"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="Label22" runat="server" Text="3. City"></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtLGCity" runat="server" CssClass="underlined" Width="300px"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">

                                    <strong>

                                        <asp:Label ID="Label23" runat="server" Text="4. State "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtLGState" runat="server" CssClass="underlined" Width="300px"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">

                                    <strong>

                                        <asp:Label ID="Label24" runat="server" Text="5. Country "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtLGCountry" runat="server" CssClass="underlined" Width="300px"></asp:TextBox>
                                    </strong>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="Label25" runat="server" Text="6.  Pin "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtLGPin" runat="server" CssClass="underlined" Width="300px"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">

                                    <strong>

                                        <asp:Label ID="Label26" runat="server" Text="7. Tel "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtLGTel" runat="server" CssClass="underlined" Width="300px"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>

                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="Label28" runat="server" Text="8.  Mobile "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtLGMobile" runat="server" CssClass="underlined" Width="300px"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">

                                    <strong>

                                        <asp:Label ID="Label29" runat="server" Text="9. Email "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtLGEmail" runat="server" CssClass="underlined" Width="300px"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                        </table>
                        <br />

                        <fieldset class="boxBodyHeader">
                            <asp:Label ID="Label27" runat="server"
                                Text="Place of stay during this Semester(Non-Hostellers/Hosteller)" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                        </fieldset>
                        <br />
                        <table style="margin-left: 5%; margin-right: 10%; font: 14px; font-family: Times New Roman;" width="100%">
                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="Label31" runat="server" Text="1. Address"></asp:Label>
                                    </strong>
                                </td>
                                <td style="width: 50%;">
                                    <strong>
                                        <asp:TextBox ID="txtHAddress" runat="server" CssClass="underlined" Width="300px"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="Label32" runat="server" Text="2. City"></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtHCity" runat="server" CssClass="underlined" Width="300px"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="Label33" runat="server" Text="3. Pin"></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtHPin" runat="server" CssClass="underlined" Width="300px"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: middle">

                                    <strong>

                                        <asp:Label ID="Label34" runat="server" Text="4. Telephone "></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="txtHTel" runat="server" CssClass="underlined" Width="300px"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>

                        </table>
                        <br />
                        <fieldset class="boxBodyHeader">
                            <asp:Label ID="Label35" runat="server"
                                Text="Details of Courses Selected for Current Semester: Session" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                            <asp:Label ID="lblSession" runat="server" Text="2020-21" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                            <asp:Label ID="Label36" runat="server" Text="Semester/Year" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                            <asp:Label ID="Label37" runat="server" Text="III" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                        </fieldset>
                        <br />
                        <br />
                        <table style="margin-left: 5%; margin-right: 10%; font: 14px; font-family: Times New Roman;" width="100%">
                            <tr>
                                <td>
                                    <asp:GridView ID="GrdAppliedExamination" runat="server" Style="width: 84%" Visible="true" AutoGenerateColumns="false"
                                        CssClass="table table-striped table-bordered table-hover" AlternatingRowStyle-CssClass="danger" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex +1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Course Code ">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblPracticalCode" runat="server" Text='<%# Bind("[SubjectCode]") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Course Name ">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblPracticalName" runat="server" Text='<%# Bind("[SubName]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Semester">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblSemester" runat="server" Text='<%# Bind("[Semester]") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Core">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblCore" runat="server" Text='<%# Bind("[Core]") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Discipline Elective">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblDE" runat="server" Text='<%# Bind("[Subject Type]") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="VAC">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblVAC" runat="server" Text='<%# Bind("[VAC]") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Open Elective">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblOpenElective" runat="server" Text='<%# Bind("[OpenElect]") %>'></asp:Label>
                                                    <asp:HiddenField ID="hdfMaxSubject" runat="server" Value='<%# Bind("[MaximumSubject]") %>' />
                                                    <asp:HiddenField ID="HdfTotalCreditPoint" runat="server" Value='<%# Bind("[TotalCredit]") %>' />
                                                    <asp:HiddenField ID="HdfCreditPoint" runat="server" Value='<%# Bind("[Credit]") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <%--<asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" />--%>
                                                    Select 
                                                </HeaderTemplate>
                                                <ItemTemplate>

                                                    <asp:CheckBox ID="chkStudent" runat="server" AutoPostBack="true" Checked='<%# Eval("Checked").ToString().Equals("True") %>' Enabled='<%# Eval("Checked").ToString().Equals("False") %>' OnCheckedChanged="chkStudent_CheckedChanged" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
                                    </asp:GridView>

                                </td>
                            </tr>
                        </table>


                        <br />
                        <fieldset class="boxBodyHeader">
                            <table runat="server" id="tblArrangement">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label38" runat="server"
                                            Text="Any type of sickness that you are prone to and the line of treatment :" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>


                                    </td>
                                    <td style="width: 70px">
                                        <asp:CheckBox ID="chkYes" runat="server" AutoPostBack="true" Text="Yes" ></asp:CheckBox>&nbsp&nbsp

                                    </td>
                                    <td style="width: 60px">
                                        <asp:CheckBox ID="chkNo" runat="server" AutoPostBack="true" Checked="true" Text="No" />

                                    </td>
                                    <td style="width: 10px"></td>
                                    <td></td>

                                </tr>
                            </table>





                        </fieldset>
                        <br />
                        <div id="DivDocDetail" runat="server" visible="false">
                            <fieldset class="boxBodyHeader">
                                <asp:Label ID="Label39" runat="server"
                                    Text="Any Particular Doctor to be contacted in case of your sickness:" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                            </fieldset>
                            <br />
                            <table style="margin-left: 5%; margin-right: 10%; font: 14px; font-family: Times New Roman;" width="100%">
                                <tr>
                                    <td style="width: 15%; vertical-align: middle">
                                        <strong>
                                            <asp:Label ID="Label40" runat="server" Text="1. Full DR Name"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 50%;">
                                        <strong>
                                            <asp:TextBox ID="txtDrName" runat="server" CssClass="underlined" Width="300px"></asp:TextBox>
                                        </strong>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="width: 15%; vertical-align: middle">
                                        <strong>
                                            <asp:Label ID="Label41" runat="server" Text="2. Address"></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        <strong>
                                            <asp:TextBox ID="txtDrAddress" runat="server" CssClass="underlined" Width="300px"></asp:TextBox>
                                        </strong>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="width: 15%; vertical-align: middle">
                                        <strong>
                                            <asp:Label ID="Label42" runat="server" Text="3. City"></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        <strong>
                                            <asp:TextBox ID="txtDrCity" runat="server" CssClass="underlined" Width="300px"></asp:TextBox>
                                        </strong>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="width: 15%; vertical-align: middle">

                                        <strong>

                                            <asp:Label ID="Label43" runat="server" Text="4. State "></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        <strong>
                                            <asp:TextBox ID="txtDRState" runat="server" CssClass="underlined" Width="300px"></asp:TextBox>
                                        </strong>
                                    </td>

                                </tr>

                                <tr>
                                    <td style="width: 15%; vertical-align: middle">
                                        <strong>
                                            <asp:Label ID="Label45" runat="server" Text="5.  Pin "></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        <strong>
                                            <asp:TextBox ID="txtDRPin" runat="server" CssClass="underlined" Width="300px"></asp:TextBox>
                                        </strong>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="width: 15%; vertical-align: middle">

                                        <strong>

                                            <asp:Label ID="Label46" runat="server" Text="6. Tel "></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        <strong>
                                            <asp:TextBox ID="txtDrTel" runat="server" CssClass="underlined" Width="300px"></asp:TextBox>
                                        </strong>
                                    </td>

                                </tr>

                                <tr>
                                    <td style="width: 15%; vertical-align: middle">
                                        <strong>
                                            <asp:Label ID="Label47" runat="server" Text="7.  Mobile "></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        <strong>
                                            <asp:TextBox ID="txtDRMobile" runat="server" CssClass="underlined" Width="300px"></asp:TextBox>
                                        </strong>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="width: 15%; vertical-align: middle">

                                        <strong>

                                            <asp:Label ID="Label48" runat="server" Text="8. Email "></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        <strong>
                                            <asp:TextBox ID="txtDREmail" runat="server" CssClass="underlined" Width="300px"></asp:TextBox>
                                        </strong>
                                    </td>

                                </tr>

                                <tr>
                                    <td style="width: 15%; vertical-align: middle">

                                        <strong>

                                            <asp:Label ID="Label44" runat="server" Text="9. Your Blood Group "></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        <strong>
                                            <asp:TextBox ID="txtBloodGroup" runat="server" CssClass="underlined" Width="300px"></asp:TextBox>
                                        </strong>
                                    </td>

                                </tr>
                            </table>




                        </div>


                        <fieldset class="boxBodyHeader">
                            <asp:Label ID="Label49" runat="server"
                                Text="UNDERTAKING" Font-Size="12pt" ForeColor="#093A62" Font-Bold="true" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                        </fieldset>
                        <br />
                        <br />
                        <table style="margin-left: 5%; margin-right: 10%; font-family: Times New Roman; width: 87%;">
                            <tr>
                                <td style="font-size: large; font-family: Arial">I hereby solemnly affrim and declare that the information made and furnished by me in the Semester Registration Form is true and correct to the best of my knowledge and belief.Further,I am being promoted to the next semester of above stated programme entirely based on promotion criteria laid down by University.I agree to abide by all the rule and regulationsof the Institution/University which I have read and understood.In the event of suppression or distortion of any fact like educational qualification,nationality etc. made in the Semester Registration Form , I understand that my admission is liable for cancellation.
                                    <br />
                                    <br />
                                    I have full knowledge of the fact that in casemy attendance in any subject falls below 75%,I shall not alloed to appear in the End Term Examinations.
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <br />
                        <br />

                        <table style="margin-left: 5%; margin-right: 10%; font: 14px; font-family: Times New Roman;" width="100%">
                            <tr>
                                <td style="width: 5%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="Label50" runat="server" Text="Date"></asp:Label>
                                    </strong>
                                </td>
                                <td style="width: 50%;">
                                    <strong>
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="underlined" Width="200px"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="height: 50px"></td>
                            </tr>
                            <tr>
                                <td style="width: 5%; vertical-align: middle">
                                    <strong>
                                        <asp:Label ID="Label51" runat="server" Text="Place"></asp:Label>
                                    </strong>
                                </td>
                                <td style="width: 50%;">
                                    <strong>
                                        <asp:TextBox ID="txtPlace" runat="server" CssClass="underlined" Width="200px"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="height: 30px"></td>

                                <td></td>
                            </tr>
                            <tr align="right">
                                <td style="width: 5%;">
                                    <strong>
                                        <asp:Label ID="Label52" runat="server" Text=""></asp:Label>
                                    </strong>
                                </td>
                                <td style="width: 28%"></td>
                                <td style="width: 25%">
                                    <strong>
                                        <asp:Label ID="Label53" runat="server" Text="(Signature of Student)"></asp:Label>
                                    </strong>
                                </td>
                                <td style="width: 20px"></td>

                            </tr>
                            <tr>
                                <td style="height: 20px;"></td>
                            </tr>
                            <tr>
                                <td style="width: 7%;">
                                    <strong>
                                        <asp:Label ID="Label54" runat="server" Text="Office Seal"></asp:Label>
                                    </strong>
                                </td>
                                <td style="width: 30%"></td>
                                <td style="width: 25%">
                                    <strong>
                                        <asp:Label ID="Label55" runat="server"></asp:Label>
                                    </strong>
                                </td>
                                <td style="width: 10px"></td>

                            </tr>
                            <tr>
                                <td style="height: 30px;"></td>
                            </tr>
                            <tr align="right">
                                <td style="width: 5%;">
                                    <strong>
                                        <asp:Label ID="Label56" runat="server" Text=""></asp:Label>
                                    </strong>
                                </td>
                                <td style="width: 38%"></td>
                                <td style="width: 25%">
                                    <strong>
                                        <asp:Label ID="Label57" runat="server" Text="(Name & Signature of the Verifying Faculty)"></asp:Label>
                                    </strong>
                                </td>
                                <td style="width: 20px"></td>

                            </tr>
                            <tr>
                                <td style="height: 20px;"></td>
                            </tr>
                            <tr>
                                <td style="width: 7%; vertical-align: bottom">
                                    <strong>
                                        <asp:Label ID="Label58" runat="server" Text="Date"></asp:Label>
                                    </strong>
                                </td>

                                <td style="width: 25%">
                                    <strong>
                                        <asp:TextBox ID="TxtDDAte" runat="server" CssClass="underlined" Width="200px"></asp:TextBox>
                                    </strong>
                                </td>
                                <td style="width: 30%"></td>
                                <td style="width: 10px"></td>

                            </tr>
                        </table>

                        <br />
                        <br />
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <strong>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="underlined" Width="1150px"></asp:TextBox>
                                    </strong>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <br />
                        <table style="margin-left: 5%; margin-right: 10%; font: 14px; font-family: Times New Roman;" width="100%">
                            <tr align="center">

                                <td style="width: 300px">
                                    <asp:Label ID="Label61" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="width: 300px" align="left">
                                    <strong>

                                        <asp:Label ID="Label59" runat="server" Text="For Official Use"></asp:Label>
                                    </strong>
                                </td>
                                <td style="width: 300px"></td>
                            </tr>
                            <tr>
                                <td style="height: 30px"></td>
                            </tr>
                            <tr align="left">

                                <td style="width: 300px">
                                    <asp:Label ID="Label60" runat="server" Text="Promotion to Next Semester Granted / Not Granted"></asp:Label>
                                </td>
                                <td style="width: 300px">
                                    <strong></strong>
                                </td>
                                <td style="width: 100px"></td>
                            </tr>

                        </table>

                        <table style="margin-left: 5%; margin-right: 10%; font: 14px; font-family: Times New Roman;" width="100%">


                            <tr>
                                <td style="height: 30px"></td>
                            </tr>

                            <tr align="left">
                                <td style="width: 70px; vertical-align: bottom">
                                    <asp:Label ID="lbl29" runat="server" Text=" Date "></asp:Label>
                                </td>
                                <td style="width: 300px">
                                    <strong>
                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="underlined" Width="200px"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>
                            <tr>
                                <td style="height: 30px"></td>
                            </tr>

                            <tr align="left">
                                <td style="width: 70px; vertical-align: bottom">
                                    <asp:Label ID="Label62" runat="server" Text=" Place "></asp:Label>
                                </td>
                                <td style="width: 300px">
                                    <strong>
                                        <asp:TextBox ID="TextBox3" runat="server" CssClass="underlined" Width="200px"></asp:TextBox>
                                    </strong>
                                </td>

                            </tr>

                            <tr>
                                <td style="height: 30px"></td>
                            </tr>

                            <tr align="right">
                                <td style="width: 5%;">
                                    <strong>
                                        <asp:Label ID="Label65" runat="server" Text=""></asp:Label>
                                    </strong>
                                </td>
                                <td style="width: 57%"></td>
                                <td style="width: 25%; text-align: right">
                                    <strong>
                                        <asp:Label ID="Label66" runat="server" Text="(Signature of Authorized Officer)"></asp:Label>
                                    </strong>
                                </td>
                               
                                    <td style="width: 20px"></td>
                            </tr>


                        


                        </table>
                    

                    </fieldset>
                </div>
                                        </asp:Panel>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                   </div>
                
            </div>

</asp:Content>

