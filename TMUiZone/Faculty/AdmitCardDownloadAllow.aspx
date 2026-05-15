<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="AdmitCardDownloadAllow.aspx.cs" Inherits="Faculty_AdmitCardRelease" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ty" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="fe" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
         <div class="panel-heading" style="background-color: #2b5b69">
        <center>
            <div class="panel-title" style="fit-position: center;">
                <b>
                    <p style="color: white; font-size: 20px">
                        ADMIT CARD DOWNLOAD ALLOW 
                    </p>
                </b>
            </div>
        </center>
    </div>
    <br />
            <fieldset class="boxBodyInner">
               <div class="row tmu-form ml-5 mr-5">
                    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label">Exam Type</label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlReaapear" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlReaapear_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="Main"> </asp:ListItem>
                                    <asp:ListItem Value="1" Text="Re-Appear"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label">Academic Year</label>
                            <div class="col-sm-7">
                                <asp:DropDownList ID="ddlAcademicYear" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged1" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label">Course</label>
                            <div class="col-sm-7">
                                <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4 p-0">
                        <div class="form-group clearfix">
                            <div id="divSp" runat="server"  visible="false">
                            <label for="inputEmail3" style="margin-right:5px;" class="col-form-label">Special</label>
                    <asp:CheckBox ID="Chekap" runat="server"  OnCheckedChanged="Chekap_CheckedChanged" AutoPostBack="true" />
                             </div>
                            <label for="inputEmail3" class="col-form-label">Sem/Year</label>
                            <div class="col-sm-7">
 <asp:DropDownList ID="ddlSem" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                            </div>
                        </div>
                    </div>


                    <div class="col-sm-2 p-0 text-right">
                        <asp:Button ID="BtnShow" runat="server" Text="Show" CssClass="btn" OnClick="BtnShow_Click" />
                        <asp:Button ID="BtnSubmit" runat="server" Visible="false" Text="Submit" OnClick="BtnSubmit_Click" CssClass="btn" />
                    </div>
                </div>
            </fieldset>
            <fieldset class="boxBodyInner">
                <div class="text-center">
                    <asp:GridView ID="GrdExamList" runat="server" DataKeyNames="Enrollment No" AlternatingRowStyle-CssClass="danger" PageSize="20" 
                        PagerStyle-Font-Bold="true" PagerStyle-HorizontalAlign="Center" AllowPaging="true" OnPageIndexChanging="GrdExamList_PageIndexChanging"
                         AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" Visible="true">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%"  HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex +1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Enrolment No" ItemStyle-Width="3%"  HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                     <asp:Label ID="lblEnrollNo" runat="server"  Text='<%# Bind("[Enrollment No]") %>'></asp:Label>
                                       <asp:HiddenField ID="HfEnrollmentNo" Value='<%# Eval("[Enrollment No]") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                                  <asp:TemplateField HeaderText="View" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnview" runat="server"  OnClick="btnview_Click" Text="View"></asp:LinkButton>                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Name" ItemStyle-Width="4%"  HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-left">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Student Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Course" ItemStyle-Width="4%"  HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-left">
                        <ItemTemplate>
                            <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("Course") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                              <asp:TemplateField HeaderText="Course Name" ItemStyle-Width="5%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                <ItemTemplate>
                                    <asp:Label ID="lblcourseName" runat="server" Text='<%#Eval("Course Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                    <asp:TemplateField HeaderText="Semester/Year" ItemStyle-Width="5%"  HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("Semester") %>'></asp:Label>
                              <asp:HiddenField ID="HfSesterr" Value='<%# Eval("Semester") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="1%"  HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-center">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" Checked="true" OnCheckedChanged="chkAll_CheckedChanged" />
                                    Select All
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkStudent" runat="server" AutoPostBack="true" Checked="true" OnCheckedChanged="chkStudent_CheckedChanged" />
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
                                        
                                                <asp:HiddenField ID="HfEnrollment_No" runat="server" />
                                                <asp:HiddenField ID="HFsemester" runat="server" />
                                        <div style="width:99%">


                                             <div style="text-align: center; width: 100%" id="logoDiv">
                                                <table style="width: 80%; margin-left: 10%;">
                                                    <tr>
                                                        <td style="width: 200px;" align="left">
                                                            <img src="~/images/rightlogo.png" id="Image1" runat="server" width="100" height="102" visible="true" />
                                                        </td>
                                                        <td style="width: 80%; vertical-align: middle" align="left">
                                                            <asp:Label ID="LblTitle" runat="server" Text="Teerthanker Mahaveer University, Moradabad" Style="font-size: 25px;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>

                <asp:Repeater ID="RepAugust" runat="server" OnItemDataBound="RepAugust_ItemDataBound">
               
                    <ItemTemplate>

                         <div class="panel-heading" style="background-color: #2b5b69">
                                                <center>
                                                    <div class="panel-title" style="fit-position: center;">
                                                        <b>
                                                            <p style="color: white; font-size: 22px">
                                                                ADMIT CARD <asp:Label ID="LblType" runat="server" Text=  '<%#Eval("ExaminationType") %>'></asp:Label>
                                                            SESSION (<asp:Label ID="TxtSession" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                                                            </p>
                                                        </b>
                                                    </div>
                                                </center>
                                            </div>

                             <asp:HiddenField ID="hfCustomerId" runat="server" Value='<%# Eval("Enrollment No_") %>' />
                            <table id="table3" style="margin-left: 1%; margin-right: 3%; font: 8px;" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <table style="width: 90%">
                                            <tr><td>
                                                <table>
                                                        <tr>
                                                <td style="width: 35%">
                                                    <strong>
                                                        <asp:Label ID="lblExaminationName" runat="server" Text="Name of Examination"></asp:Label>
                                                    </strong>
                                                </td>
                                                <td style="width: 65%" colspan="2">
                                                    <strong>
                                                        <asp:Label ID="txtExaminationName" runat="server" Text='<%#Eval("Course Name") %>' BorderStyle="None"  ReadOnly="true" Style="text-transform: uppercase;"></asp:Label>
                                                    </strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <strong>
                                                        <asp:Label ID="lblSem" runat="server" Text="Program - Year / Sem. "></asp:Label>
                                                    </strong>
                                                </td>
                                                <td>
                                                    <strong>
                                                        <asp:TextBox ID="txtSemester" runat="server" Text='<%#Eval("Sem") %>' BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                                    </strong>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td style="width: 45%">
                                                    <strong>
                                                        <asp:Label ID="lblBranch" runat="server" Text="Program Code"></asp:Label>
                                                    </strong>
                                                </td>
                                                <td style="width: 55%">
                                                    <strong>
                                                        <asp:TextBox ID="TxtBranch" runat="server" Text='<%#Eval("Course Code") %>' BorderStyle="None" Width="300px" ReadOnly="true" CssClass="auto-style9" Style="text-transform: uppercase;"></asp:TextBox>
                                                    </strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 45%">
                                                    <strong>
                                                        <asp:Label ID="LblEnrollmentNo" runat="server" Text="Enrolment No."></asp:Label>
                                                    </strong>
                                                </td>
                                                <td style="width: 55%">
                                                    <strong>
                                                        <asp:TextBox ID="TxtEnrollmentNo" runat="server" Text='<%#Eval("Enrollment No_") %>' BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                                    </strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <strong>
                                                        <asp:Label ID="lblStudentName" runat="server" Text="Name of Student"></asp:Label>
                                                    </strong>
                                                </td>
                                                <td >
                                                    <strong>
                                                        <asp:TextBox ID="TxtStudentName" runat="server" Text='<%#Eval("Student Name") %>' BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                                    </strong>
                                                </td>
                                             </tr>
                                            <tr>
                                                <td>
                                                    <strong>
                                                       <asp:Label ID="LblHindiName" runat="server" Text=" छात्र/छात्रा का नाम"></asp:Label>
                                                    </strong>
                                                </td>
                                                <td >
                                                    <strong>
                                                           <asp:TextBox ID="TxtHindiName" runat="server" Text='<%#Eval("StudentHindiName") %>' BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                                    </strong>
                                                </td>
                                             </tr>
                                            <tr>
                                                <td >
                                                    <strong>
                                                        <asp:Label ID="lblFathersName" runat="server" Text="Father's Name "></asp:Label>
                                                    </strong>
                                                </td>
                                                <td >
                                                    <strong>
                                                        <asp:TextBox ID="TxtFathersName" runat="server" Text='<%#Eval("Fathers Name") %>' BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                                    </strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td >
                                                    <strong>
                                                          <asp:Label ID="LblHindiFatherName" runat="server" Text=" पिता का नाम "></asp:Label>
                                                    </strong>
                                                </td>
                                                <td >
                                                    <strong>
                                                         <asp:TextBox ID="TxtHindiFathersName" runat="server" Text='<%#Eval("FatherHindiName") %>' BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                                    </strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <strong>
                                                        <asp:Label ID="lblMothersName" runat="server" Text="Mother’s Name "></asp:Label>
                                                    </strong>
                                                </td>
                                                <td >
                                                    <strong>
                                                        <asp:TextBox ID="TxtMothersName" runat="server" Text='<%#Eval("Mothers Name") %>' BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                                    </strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td >
                                                    <strong>
                                                        <asp:Label ID="LblHindiMothersName" runat="server" Text=" माता का नाम "></asp:Label>
                                                    </strong>
                                                </td>
                                                <td >
                                                    <strong>
                                                        <asp:TextBox ID="TxtHindiMothersName" runat="server" Text='<%#Eval("MotherHindiName") %>' BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                                    </strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td >
                                                    <strong>
                                                        <asp:Label ID="lblAadhar" runat="server" Text=" Aadhar No:"></asp:Label>
                                                    </strong>
                                                </td>
                                                <td>
                                                    <strong>
                                                        <asp:TextBox ID="TextAadhar" runat="server" Visible="false" BorderStyle="None" Width="300px" ReadOnly="true" Text='<%#Eval("Aadhar No_") %>' Style="text-transform: uppercase;"></asp:TextBox>
                                                    </strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td >
                                                    <strong>
                                                        <asp:Label ID="lblExaminationCentre" runat="server" Text="Examination Centre:"></asp:Label>
                                                    </strong>
                                                </td>
                                                <td>
                                                    <strong>
                                                        <asp:TextBox ID="txtExaminationCentre" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" Text='<%#Eval("Exam") %>' TextMode="MultiLine" Style="text-transform: uppercase;resize:none"></asp:TextBox>
                                                    </strong>
                                                </td>
                                            </tr>

                                                </table>


                                                </td>
                                                <td>
                                                    <table>
                                                                                                         <td rowspan="9">
                                        <div style="text-align: left; margin-right: 10%;height:100px; width:90px">
                                            <img src='data:CarImages/png;base64,<%# Eval("Student Image") != System.DBNull.Value ? Convert.ToBase64String((byte[])Eval("Student Image")) : string.Empty %>' alt="image" height="150" width="120" style="border: 1px solid black" />
                                        </div>
                                    <%--                     <table style="margin-top:10px;">
                                <tr>
                                    <td style="text-align:left;">Contact No.</td>
                                    <td>                                       
                               <asp:TextBox ID="TxtContactNo" runat="server" Text='<%#Eval("Mobile Number") %>' BorderStyle="None" style="margin-top: 2px;width:auto;height:12px;" ReadOnly="true"></asp:TextBox>
                                      
                                    </td>
                                </tr>
                            </table>--%>
                                    </td>

                                                    </table>


                                                </td>
                                            </tr>
                                        
                                          
                                      </table>

                                    </td>
                                       
                                </tr>
                            </table>
                            <p style="text-align: center;font-size:small"><b>You are permitted to appear in the following Course(s):</b></p>
                          <div style="width:99%">
       
              <asp:GridView ID="GrdSubjectDetail"  runat="server"  Visible="true" AutoGenerateColumns="false"
                CssClass="table table-striped table-bordered table-hover" style="margin-bottom:0px;" AlternatingRowStyle-CssClass="danger" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="8%">
                        <ItemTemplate>
                            <%# Container.DataItemIndex +1 %>
                        </ItemTemplate>
                    </asp:TemplateField>                   
                    <asp:TemplateField HeaderText="Course Code" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblCourseCode" runat="server" Text='<%# Eval("Subject Code") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course" ItemStyle-Width="30%">
                        <ItemTemplate>
                            <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblCate" runat="server" Text='<%# Eval("Subject Classification") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Date" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="lbldate" runat="server" Text='<%# Eval("Date","{0:dd MMM yyyy}") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Shift" ItemStyle-Width="9%">
                        <ItemTemplate>
                            <asp:Label ID="lblshift" runat="server"  Text='<%# Eval("Shift") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="From Time"  ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="lblCate" runat="server" Text='<%# Eval("[From Time]","{0: hh:mm tt}") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="To Time " ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="lbltime" runat="server" Text='<%# Eval("[To Time]","{0: hh:mm tt}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                                          
                      
    
                </Columns>
            </asp:GridView>
                              <div id ="Extrasubject" runat="server" visible="true">
          <div  style="page-break-before: always"></div>
                    <asp:GridView ID="GrdSubjectDetail1"  runat="server"  ShowHeader="False" Visible="true" AutoGenerateColumns="false"
                CssClass="table table-striped table-bordered table-hover" AlternatingRowStyle-CssClass="danger">
                <Columns>
                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="6%">
                        <ItemTemplate>
                            <%# Container.DataItemIndex +11 %>
                        </ItemTemplate>
                    </asp:TemplateField>                   
                    <asp:TemplateField HeaderText="Course Code" ItemStyle-Width="8%">
                        <ItemTemplate>
                            <asp:Label ID="lblCourseCode1" runat="server" Text='<%# Eval("Subject Code") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course" ItemStyle-Width="30%">
                        <ItemTemplate>
                            <asp:Label ID="lblCourse1" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblCate1" runat="server" Text='<%# Eval("Subject Classification") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="DATE" ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="lbldate1" runat="server" Text='<%# Eval("Date","{0:dd MMM yyyy}") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Shift" ItemStyle-Width="9%">
                        <ItemTemplate>
                            <asp:Label ID="lblshift1" runat="server"  Text='<%# Eval("Shift") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="From Time"  ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="lblCate1" runat="server" Text='<%# Eval("[From Time]","{0: hh:mm tt}") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="To Time " ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="lbltime1" runat="server" Text='<%# Eval("[To Time]","{0: hh:mm tt}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                                   </Columns>
            </asp:GridView>
                     </div>                               
             
                                  
                                   </div>
                                <div style="width: 100%";>
                         <br /> 
                            <p><b>Note 1:</b> Practical/Viva/Lab exam will be conducted as per Practical/Viva/Lab date sheet.</p>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 50%">
                                            <table>
                                                <tr>
                                                    <td>Date:</td>
                                                    <td><asp:Label ID="date" runat="server" Text='<%# Eval("Date")  %>'  ></asp:Label>

                                                    </td>
                                                </tr>

                                            </table>

                                        </td>
                                       <td style="width:50%;text-align:center;">
                                        <div class="col-lg-12" style="text-align:center; margin-bottom:12px">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/images/sign.jpg" ImageAlign="Middle" Width="30%"  />                                                                                                                
                                        </div>
                                             Controller of Examinations
                                            </td>
                                    </tr>

                                </table>

                            </div>
                        <br />

                            <div style="margin-left: 1%; margin-right:3%; font:4px; margin-right: 10%">
                                <b>Note: 2</b>
                                  <p style="font-size:small">(I) Students are  advised to check the accuracy in the spelling of their own name as well as of their parents (both in Hindi & English) and Aadhar No. mentioned in the admit card. If any discrepancy is noticed this may be informed to the Admission cell through Director / Principal of the college within 05 days of the last theory examination held. This information shall be used for preparing the Marksheet, Consolidated Marksheet and the Degree. Examination Division shall not be responsible for any spelling mistake identified/informed lateron.
                                  </p>
                            </div>
                              <div style="page-break-before: always"></div>  

                        <br />
                         
                      
                    </ItemTemplate>
                   
                    <FooterTemplate>
                    </FooterTemplate>
                       
                </asp:Repeater>
                           
            </div>
                                    </asp:Panel>
                                </asp:Panel>
                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

