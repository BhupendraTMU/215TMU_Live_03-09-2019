<%@ Page Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="AdmitCard.aspx.cs" EnableEventValidation="false" Inherits="AdmitCard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
   
      <script type="text/javascript">
        
        

          function PrintDiv() {

              var divToPrint = document.getElementById('printarea');

              var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px, margin:0mm');
              popupWin.document.open();
              popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
              popupWin.document.close();
          }
    </script>
        <script type="text/javascript">

            function onUpdating() {
                // get the divImage
               
                var panelProg = $get('divImage');
                // set it to visible
                panelProg.style.display = '';

                // hide label if visible     
                var lbl = $get('<%= this.lblText.ClientID %>');
                lbl.innerHTML = '';
                
            }
    </script>
    
 
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
    <asp:ScriptManager ID="ty" runat="server"></asp:ScriptManager>
   <asp:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1"
        TargetControlID="fe" runat="server">
        <Animations>
            <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script="onUpdating();" />
                    <EnableAction AnimationTarget="ddlReaapear" Enabled="false" /> 
                </Parallel>
            </OnUpdating>
             </Animations>
        </asp:UpdatePanelAnimationExtender> 
    <asp:UpdatePanel ID="fe" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
             
         <panel id="updateheading" runat="server" visible="true">
             <div class="panel-heading" style="background-color: #2b5b69">
        <center>
            <div class="panel-title" style="fit-position: center;">
                <b>
                    <p style="color: white; font-size: 20px">
                        ADMIT CARD 
                    </p>
                </b>
            </div>
        </center>
    </div>
             </panel>
            <br />
   <fieldset class="boxBodyInner">
       <panel id="PnlFilter" runat="server" visible="true">
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
                    <div class="col-sm-4 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label">Academic Year</label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlAcademicYear" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged1" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label">Course</label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label">Sem/Year</label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlSem" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" AutoPostBack="true">
                                  
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2 p-0 text-right">
                        <asp:Button ID="BtnShow" runat="server" Text="Show" CssClass="btn" OnClick="BtnShow_Click" />
                        <id id="divImage" style="display:none;" class="fa fa-spinner fa-pulse fa-2x fa-fw"></id>
                        <span class="sr-only">Loading...</span>
                        <asp:Label ID="lblText" runat="server" Text="" Height="10px"></asp:Label>
                         <asp:Label ID="lblRowNumber" runat="server" Visible="false"  Height="10px"></asp:Label>
                        <%--<asp:Label ID="Label1" runat="server" Text="0" Height="10px"></asp:Label>--%>
                         
                      
                        <%--<asp:Button ID="BtnSubmit" runat="server" Visible="false" Text="Submit" OnClick="BtnSubmit_Click" CssClass="btn" />--%>
                    </div>
                </div>                
      </panel>
            </fieldset>
            <asp:HiddenField ID="HFsemester"  runat="server" />
            <asp:GridView ID="GrdAppliedExamination" DataKeyNames="Enrollment No" AlternatingRowStyle-CssClass="danger" PageSize="20" PagerStyle-Font-Bold="true" PagerStyle-HorizontalAlign="Center"
                 AllowPaging="true" OnPageIndexChanging="GrdAppliedExamination_PageIndexChanging" 
                 runat="server"  Visible="true" AutoGenerateColumns="false" 
                CssClass="table table-striped table-bordered table-hover" Style="width: 90%; margin-left:5%;margin-right:5%"   ShowHeaderWhenEmpty="false">
                <Columns>
                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%"  HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <%# Container.DataItemIndex +1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Enrolment No" ItemStyle-Width="15%"  HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lblEnrollNo" runat="server" Text='<%#Eval("Enrollment No") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name" ItemStyle-Width="15%"  HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-left">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Student Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course" ItemStyle-Width="15%"  HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-left">
                        <ItemTemplate>
                            <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("Course") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course Name" ItemStyle-Width="20%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                        <ItemTemplate>
                            <asp:Label ID="lblcourseName" runat="server" Text='<%#Eval("Course Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Semester/Year" ItemStyle-Width="10%"  HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("Semester") %>'></asp:Label>
                            <asp:HiddenField ID="HfSesterr" Value='<%# Eval("Semester") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="15%"  HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-center">
                        <HeaderTemplate>
                            <asp:CheckBox ID="ChlAll" OnCheckedChanged="ChlAll_CheckedChanged"  Checked="true" AutoPostBack="true" runat="server" />
                            <asp:LinkButton ID="DownloadAll" runat="server" OnClick="DownloadAll_Click"  Text="View AdmitCard"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="ChkDownload" runat="server" Checked="true"  AutoPostBack="true" OnCheckedChanged="ChkDownload_CheckedChanged" />

                        </ItemTemplate>
                    </asp:TemplateField>
                    
                </Columns>
                 <EmptyDataTemplate>No Record To Display</EmptyDataTemplate> 
            </asp:GridView>
            <div style="text-align: right; width: 80%">
                <asp:Button ID="btntest" OnClientClick="PrintDiv();" runat="server" OnClick="btntest_Click" Visible="false" Text="Print" />
                <asp:Button ID="btnback" runat="server" Text="Back" Visible="false" OnClick="btnback_Click" />
                <asp:Label ID="lbltotalrepeater" runat="server"></asp:Label>
             <%--   <asp:Button ID="btnSubmit" runat="server" Text="Release" Visible="false" OnClick="btnSubmit_Click" />--%>

            </div>
            <div id="printarea">
                <asp:Repeater ID="RepAugust" runat="server" OnItemDataBound="RepAugust_ItemDataBound">
               
                    <ItemTemplate>
                       
                            <div class="panel-heading">
                                <center>
                                    <div class="panel-title" style="fit-position:left;">
                                        <b>
                                            <table style="width: 100%; text-align: left">
                                                <tr>
                                                    <td style="width: 15%; text-align:center">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/rightlogo.png" Width="55%" />
                                                    </td>
                                                    <td style="width: 75%; text-align:center">
                                                     <strong><asp:Label ID="lblCName" Text="Teerthanker Mahaveer University, Moradabad" runat="server"></asp:Label></strong> 
                                                                  <br />
                                                          <strong><asp:Label ID="lblAC" runat="server" Text="ADMIT CARD"></asp:Label></strong>
                                                              
                                                         <br />
                                                             <strong><asp:Label ID="LblType" runat="server" Text=  '<%#Eval("ExaminationType") %>' Style="text-transform: uppercase;"></asp:Label> </strong>                             <br />
                                                           <strong> SESSION: <asp:Label ID="TxtSession" runat="server" Text='<%#Eval("Description") %>' Style="text-transform: uppercase;"></asp:Label></strong>
                                              
                                                    </td>
                                                    <td style="width: 6%; text-align:center">


                                                    </td>
                                                </tr>

                                            </table>
                                        </b>
                                    </div>
                                </center>
                            </div>
                             <asp:HiddenField ID="hfCustomerId" runat="server" Value='<%# Eval("Enrollment No_") %>' />
                         
                             <asp:HiddenField ID="HfSem" runat="server" Value='<%# Eval("Semester") %>' />
                         
                           
                               
<table id="table3" style="margin-left: 1%; margin-right: 1%; font: 8px;" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <table style="width: 99%">
                                            <tr><td style="width:78%">
                                                <table>
                                                        <tr>
                                                <td style="width:50%">
                                                    <strong>
                                                        <asp:Label ID="lblExaminationName" runat="server" Text="Name of Examination"></asp:Label>
                                                    </strong>
                                                </td>
                                                <td style="width:50%">
                                                    <strong>
                                                        <asp:Label ID="txtExaminationName" runat="server" Text='<%#Eval("Course Name") %>' BorderStyle="None"  ReadOnly="true" Style="text-transform: uppercase; font-size:small;"></asp:Label>
                                                    </strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width:50%">
                                                    <strong>
                                                        <asp:Label ID="lblSem" runat="server" Text="Program - Year / Sem. "></asp:Label>
                                                    </strong>
                                                </td>
                                                <td style="width:50%">
                                                    <strong>
                                                        <asp:TextBox ID="txtSemester" runat="server" Text='<%#Eval("Sem") %>' BorderStyle="None"  ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                                    </strong>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td style="width:50%">
                                                    <strong>
                                                        <asp:Label ID="lblBranch" runat="server" Text="Program Code"></asp:Label>
                                                    </strong>
                                                </td>
                                                <td style="width:50%">
                                                    <strong>
                                                        <asp:TextBox ID="TxtBranch" runat="server" Text='<%#Eval("Course Code") %>' BorderStyle="None"  ReadOnly="true" CssClass="auto-style9" Style="text-transform: uppercase;"></asp:TextBox>
                                                    </strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width:50%">
                                                    <strong>
                                                        <asp:Label ID="LblEnrollmentNo" runat="server" Text="Enrolment No."></asp:Label>
                                                    </strong>
                                                </td>
                                                <td style="width:50%">
                                                    <strong>
                                                        <asp:TextBox ID="TxtEnrollmentNo" runat="server" Text='<%#Eval("Enrollment No_") %>' BorderStyle="None"  ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                                    </strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width:50%">
                                                    <strong>
                                                        <asp:Label ID="lblStudentName" runat="server" Text="Name of Student"></asp:Label>
                                                    </strong>
                                                </td>
                                                <td style="width:50%">
                                                    <strong>
                                                        <asp:TextBox ID="TxtStudentName" runat="server" Text='<%#Eval("Student Name") %>' BorderStyle="None"  ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                                    </strong>
                                                </td>
                                             </tr>
                                            <tr>
                                                <td style="width:50%">
                                                    <strong>
                                                       <asp:Label ID="LblHindiName" runat="server" Text=" छात्र/छात्रा का नाम"></asp:Label>
                                                    </strong>
                                                </td>
                                                <td style="width:50%">
                                                    <strong>
                                                           <asp:TextBox ID="TxtHindiName" runat="server" Text='<%#Eval("StudentHindiName") %>' BorderStyle="None"  ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                                    </strong>
                                                </td>
                                             </tr>
                                            <tr>
                                                <td style="width:50%">
                                                    <strong>
                                                        <asp:Label ID="lblFathersName" runat="server" Text="Father's Name "></asp:Label>
                                                    </strong>
                                                </td>
                                                <td style="width:50%">
                                                    <strong>
                                                        <asp:TextBox ID="TxtFathersName" runat="server" Text='<%#Eval("Fathers Name") %>' BorderStyle="None"  ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                                    </strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width:50%">
                                                    <strong>
                                                          <asp:Label ID="LblHindiFatherName" runat="server" Text=" पिता का नाम "></asp:Label>
                                                    </strong>
                                                </td>
                                                <td style="width:50%">
                                                    <strong>
                                                         <asp:TextBox ID="TxtHindiFathersName" runat="server" Text='<%#Eval("FatherHindiName") %>' BorderStyle="None"  ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                                    </strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width:50%">
                                                    <strong>
                                                        <asp:Label ID="lblMothersName" runat="server" Text="Mother’s Name "></asp:Label>
                                                    </strong>
                                                </td>
                                                <td style="width:50%">
                                                    <strong>
                                                        <asp:TextBox ID="TxtMothersName" runat="server" Text='<%#Eval("Mothers Name") %>' BorderStyle="None"  ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                                    </strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width:50%">
                                                    <strong>
                                                        <asp:Label ID="LblHindiMothersName" runat="server" Text=" माता का नाम "></asp:Label>
                                                    </strong>
                                                </td>
                                                <td style="width:50%">
                                                    <strong>
                                                        <asp:TextBox ID="TxtHindiMothersName" runat="server" Text='<%#Eval("MotherHindiName") %>' BorderStyle="None"  ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                                    </strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width:50%">
                                                    <strong>
                                                        <asp:Label ID="lblAadhar" runat="server" Text=" Aadhar No:"></asp:Label>
                                                    </strong>
                                                </td>
                                                <td style="width:50%">
                                                    <strong>
                                                        <asp:TextBox ID="TextAadhar" runat="server" Visible="false" BorderStyle="None"  ReadOnly="true" Text='<%#Eval("Aadhar No_") %>' Style="text-transform: uppercase;"></asp:TextBox>
                                                    </strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width:50%">
                                                    <strong>
                                                        <asp:Label ID="lblExaminationCentre" runat="server" Text="Examination Centre:"></asp:Label>
                                                    </strong>
                                                </td>
                                                <td style="width:50%">
                                                    <strong>
                                                        <asp:TextBox ID="txtExaminationCentre" runat="server" BorderStyle="None"  ReadOnly="true" Text='<%#Eval("Exam") %>' TextMode="MultiLine"  Style="text-transform: uppercase;resize:none;width:300px;"></asp:TextBox>
                                                    </strong>
                                                </td>
                                            </tr>

                                                </table>


                                                </td>
                                                <td style="width:22%">
                                                    <table>
                                                    <td rowspan="7">
                                        <div style="text-align:right; margin-right: 1%;height:100px; width:90px">
                                            <img src='data:CarImages/png;base64,<%# Eval("Student Image") != System.DBNull.Value ? Convert.ToBase64String((byte[])Eval("Student Image")) : string.Empty %>' alt="image" height="100px" width="90px" style="border: 1px solid black" />
                                        </div>
                                                 
                                    <%--      <table style="margin-top:10px;">
                                <tr>
                                    <td style="text-align:left; resize:none"><asp:Label ID="Label1" runat="server" Text="ContactNo."   Enabled="false"></asp:Label></td>
                                    <td style="text-align:left;">                                       
                               <asp:TextBox ID="TxtContactNo" runat="server" Text='<%#Eval("Mobile Number") %>' BorderStyle="None" style="margin-top:2px;width:auto;height:12px;" ReadOnly="true"></asp:TextBox>
                                      
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
                          <div style="width:99%" >
      
                        <asp:GridView ID="GrdSubjectDetail" runat="server" Visible="true" AutoGenerateColumns="false" RowStyle-Font-Size="Small"
                            CssClass="table table-striped table-bordered table-hover" Style="margin-bottom: 0px" AlternatingRowStyle-CssClass="danger" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# Container.DataItemIndex +1 %>
                        </ItemTemplate>
                    </asp:TemplateField>                   
                    <asp:TemplateField HeaderText="Course Code" ItemStyle-Width="12%">
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
                      <asp:TemplateField HeaderText="Date" ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="lbldate" runat="server" Text='<%# Eval("Date","{0:dd MMM yyyy}") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Shift" ItemStyle-Width="8%">
                        <ItemTemplate>
                            <asp:Label ID="lblshift" runat="server"  Text='<%# Eval("Shift") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="From Time"  ItemStyle-Width="11%">
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
                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# Container.DataItemIndex +11 %>
                        </ItemTemplate>
                    </asp:TemplateField>                   
                    <asp:TemplateField HeaderText="Course Code" ItemStyle-Width="12%">
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
                      <asp:TemplateField HeaderText="Shift" ItemStyle-Width="8%">
                        <ItemTemplate>
                            <asp:Label ID="lblshift1" runat="server"  Text='<%# Eval("Shift") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="From Time"  ItemStyle-Width="11%">
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
                          
                     <p><b>Note 1:</b> Practical/Viva/Lab exam will be conducted as per Practical/Viva/Lab date sheet.</p>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width:50%">
                                            <table style="margin-top:78px;">
                                               
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
                            

                     
                        
                   
  <asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible="<%# (Container.ItemIndex+1) < Convert.ToInt32(lblRowNumber.Text) %>">
    <tr><td>
         <div  style="page-break-before: always"></div>
        </td></tr>
  </asp:PlaceHolder>  
  <!-- "Else" -->
  <asp:PlaceHolder ID="PlaceHolder2" runat="server" Visible="<%# (Container.ItemIndex+1)>=Convert.ToInt32(lblRowNumber.Text) %>">
    <tr><td>
         <div  style="page-break-before: auto"></div>
        </td></tr>
  </asp:PlaceHolder>
                    </ItemTemplate>
                  
                      <FooterTemplate>
                         
                    </FooterTemplate>
                      
                </asp:Repeater>
                           
            </div>
              
        </ContentTemplate>
         
    </asp:UpdatePanel>
</asp:Content>
