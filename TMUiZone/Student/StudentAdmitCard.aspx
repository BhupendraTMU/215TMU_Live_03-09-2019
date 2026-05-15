<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentAdmitCard.aspx.cs" Inherits="Student_StudentAdmitCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
    .radRace input[type="radio"] {
        margin-left: 0px;
        margin-right: 300px;
    }
</style>
    
      <script type="text/javascript">



          function PrintDiv() {

              var divToPrint = document.getElementById('ContentPlaceHolder1_printarea');

              var popupWin = window.open('', '_blank', 'width=300px,height=400px,location=no,left=200px, margin:0mm');
              popupWin.document.open();
              popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
              popupWin.document.close();
          }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ty" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="fe" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

    <fieldset class="boxBodyInner">
        <div class="row tmu-form ml-5 mr-5" style="text-align:right">
              <asp:Button ID="btnprint" OnClientClick="PrintDiv();" runat="server" Visible="false"  Text="Print" />
        </div>
          <div class="row tmu-form ml-5 mr-5">
              <div class="col-sm-4 p-0">
    <asp:RadioButtonList ID="radRace"  runat="server" OnSelectedIndexChanged="radRace_SelectedIndexChanged" AutoPostBack="true"  CssClass="rbl">
    <asp:ListItem Text="Main" Value="0"  Selected="True"></asp:ListItem>
    <asp:ListItem Text="Re-Appear" Value="1" ></asp:ListItem>
    
</asp:RadioButtonList>
                  </div>
       <div class="col-sm-7 p-0">
            <div id="DivREp" runat="server" >
                        <label for="inputEmail3" class="col-form-label">Sem/Year</label>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="ddlSem" CssClass="form-control"  OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                
                            </asp:DropDownList>
                        </div>
            <div class="col-sm-2">
                <asp:Button ID="btnshow" runat="server" Text="Show" OnClick="btnshow_Click" CssClass="btn" />
               <asp:Label ID="lblRowNumber" runat="server" Visible="false" Text="1"  Height="10px"></asp:Label>

            </div>
              </div>      
        </div>
              </div>
   </fieldset>
           <%-- <fieldset>
                <br />--%>
               <%-- <asp:Panel ID="pnl1" runat="server">
                    <table>
                        <tr>
                            <td>
 <label style="width: 200px; font: bold; color: black; font-size: large" class="required">Mobile Number:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtmobile" CssClass="form-control" MaxLength="10" MinLength="10" Width="250px" onkeypress="return numeric(event)" BorderColor="Black" runat="server"></asp:TextBox>
                            </td>
                            <td>
 <asp:Button ID="btnsendOtp" runat="server" CssClass="auto-style7" Text="Send Mobile OTP" OnClick="btnsendOtp_Click" Height="35px" />
                            </td>
                        </tr>
                    </table>
                                       

                                    </asp:Panel>--%>
                                   <%-- <asp:Panel ID="pnl2" runat="server" Visible="false">
                                      <table>
                                          <tr>
                                              <td>
                                                   <label style="width: 200px; font: bold; color: black; font-size: large">Enter Your OTP:</label>
                                              </td>
                                              <td>
                                                   <asp:TextBox ID="txtverifyMobileNO" Height="30px" Width="150px" BorderColor="Black" runat="server"></asp:TextBox>
                                            
                                              </td>
                                              <td>
                                                   <asp:Button ID="btnverify" runat="server" Text="Verify" Height="35px" CssClass="auto-style7" OnClick="btnverify_Click" Width="120px" />
                                              </td>
                                          </tr>
                                          <tr>
                                              <td colspan="3">
                                                  <asp:Label ID="lblMSGOTP" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                              </td>
                                          </tr>
                                      </table>
                                           
                                        
                                      
                                    </asp:Panel>--%>
        <%--    </fieldset>--%>
            
            <fieldset class="boxBodyInner">
             
        <div id="printarea" runat="server" visible="false" >
           
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
                                                       <strong> <asp:Label ID="lblAC" runat="server" Text="ADMIT CARD"></asp:Label></strong>
                                                              <br />
                                                         
                                                             <strong><asp:Label ID="LblType" runat="server" Text=  '<%#Eval("ExaminationType") %>' Style="text-transform: uppercase;"></asp:Label> </strong>
                                                                                              <br />
                                                            <strong>SESSION: <asp:Label ID="TxtSession" runat="server" Text='<%#Eval("Description") %>' Style="text-transform: uppercase;"></asp:Label> </strong> 
                                              
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
                                                        <asp:Label ID="txtExaminationName" runat="server" Text='<%#Eval("Course Name") %>' BorderStyle="None"  ReadOnly="true" Style="text-transform: uppercase;font-size:small;"></asp:Label>
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
                                                        <asp:TextBox ID="TxtStudentName" runat="server" Text='<%#Eval("Student Name") %>' BorderStyle="None" Width="500px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                                    </strong>
                                                </td>
                                             </tr>
                                            <tr>
                                                <td style="width:50%">
                                                    <strong>
                                                       <asp:Label ID="LblHindiName" runat="server" Text=" छात्र/छात्रा का नाम"></asp:Label>
                                                    </strong>
                                                </td>
                                                <td style="width:50%;">
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
                                                <td style="width:60%">
                                                    <strong>
                                                        <asp:TextBox ID="TxtFathersName" Width="200px" runat="server" Text='<%#Eval("Fathers Name") %>' BorderStyle="None"  ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
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
                                                        <asp:TextBox ID="TxtMothersName" runat="server" Text='<%#Eval("Mothers Name") %>' BorderStyle="None"  Width="200px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
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
                                                        <asp:TextBox ID="TextAadhar" runat="server" Visible="false" BorderStyle="None"  ReadOnly="true" Text='<%#Eval("Aadhar No_") %>' Style="text-transform:uppercase;"></asp:TextBox>
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
                                                      <%--Text='<%#Eval("Exam") %>'--%>
                                                             <asp:TextBox ID="txtExaminationCentre" runat="server" BorderStyle="None"  ReadOnly="true" Text='<%#Eval("Exam") %>'  TextMode="MultiLine"  Style="text-transform:uppercase;resize:none;width:300px;"></asp:TextBox>
                                                   
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
                                                 
                                      <%--    <table style="margin-top:10px;">
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
                          <div style="width:99%">
       
                <asp:GridView ID="GrdSubjectDetail" runat="server" Visible="true" AutoGenerateColumns="false" RowStyle-Font-Size="Small"
                            CssClass="table table-striped table-bordered table-hover" Style="margin-bottom: 0px" AlternatingRowStyle-CssClass="danger" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# Container.DataItemIndex +1 %>
                        </ItemTemplate>
                    </asp:TemplateField>                   
                    <asp:TemplateField HeaderText="Course Code" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblCourseCode" runat="server" Text='<%# Eval("Subject Code") %>'  Style="text-transform: uppercase;"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course" ItemStyle-Width="30%">
                        <ItemTemplate>
                            <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("Description") %>' BorderStyle="None"  ReadOnly="true" Style="text-transform: uppercase;"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblCate" runat="server" Text='<%# Eval("Subject Classification") %>' BorderStyle="None"  ReadOnly="true" Style="text-transform: uppercase;"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Date" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="lbldate" runat="server" Text='<%# Eval("Date","{0:dd MMM yyyy}") %>' BorderStyle="None"  ReadOnly="true" Style="text-transform: uppercase;" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Shift" ItemStyle-Width="9%">
                        <ItemTemplate>
                            <asp:Label ID="lblshift" runat="server"  Text='<%# Eval("Shift") %>'  BorderStyle="None"  ReadOnly="true" Style="text-transform: uppercase;"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="From Time"  ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="lblCate" runat="server" Text='<%# Eval("[From Time]","{0: hh:mm tt}") %>' BorderStyle="None"  ReadOnly="true" Style="text-transform: uppercase;" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="To Time " ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="lbltime" runat="server" Text='<%# Eval("[To Time]","{0: hh:mm tt}") %>' BorderStyle="None"  ReadOnly="true" Style="text-transform: uppercase;"></asp:Label>
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
                    <asp:TemplateField HeaderText="Course Code" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblCourseCode1" runat="server" Text='<%# Eval("Subject Code") %>'   BorderStyle="None"  ReadOnly="true" Style="text-transform: uppercase;"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course" ItemStyle-Width="30%">
                        <ItemTemplate>
                            <asp:Label ID="lblCourse1" runat="server" Text='<%# Eval("Description") %>'   BorderStyle="None"  ReadOnly="true" Style="text-transform: uppercase;"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblCate1" runat="server" Text='<%# Eval("Subject Classification") %>'   BorderStyle="None"  ReadOnly="true" Style="text-transform: uppercase;"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="DATE" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="lbldate1" runat="server" Text='<%# Eval("Date","{0:dd MMM yyyy}") %>'   BorderStyle="None"  ReadOnly="true" Style="text-transform: uppercase;" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Shift" ItemStyle-Width="9%">
                        <ItemTemplate>
                            <asp:Label ID="lblshift1" runat="server"  Text='<%# Eval("Shift") %>'  BorderStyle="None"  ReadOnly="true" Style="text-transform: uppercase;" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="From Time"  ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="lblCate1" runat="server" Text='<%# Eval("[From Time]","{0: hh:mm tt}") %>'  BorderStyle="None"  ReadOnly="true" Style="text-transform: uppercase;" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="To Time " ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="lbltime1" runat="server" Text='<%# Eval("[To Time]","{0: hh:mm tt}") %>'   BorderStyle="None"  ReadOnly="true" Style="text-transform: uppercase;"></asp:Label>
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
                                        <td style="width: 50%">
                                            <table style="margin-top:78px;">
                                              
                                                <tr>
                                                    <td>Date:</td>
                                                    <td><asp:Label ID="date" runat="server" Text='<%# Eval("Date")  %>' ></asp:Label>

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
        <div style="page-break-before: always"></div>
    <br />
                </fieldset>
            </ContentTemplate>
        
         <Triggers>
         <asp:PostBackTrigger ControlID="btnshow" />
            </Triggers>

        </asp:UpdatePanel>
</asp:Content>

