<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="ScholarshipApproval.aspx.cs" Inherits="Faculty_ScholarshipApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     
    <script type="text/javascript">

        function txtHSpercentage() {
            var Sub1 = 0, Sub2 = 0, Sub3 = 0, Sub4 = 0;

            if (document.getElementById("ContentPlaceHolder1_TextBox2").value == "") {
                Sub1 = 0;
            }
            else {
                Sub1 = document.getElementById("ContentPlaceHolder1_TextBox2").value;
            }

            if (document.getElementById("ContentPlaceHolder1_TextBox6").value == "") {
                Sub2 = 0;
            }
            else {
                Sub2 = document.getElementById("ContentPlaceHolder1_TextBox6").value;
            }

            if (document.getElementById("ContentPlaceHolder1_TextBox10").value == "") {
                Sub3 = 0;
            }
            else {
                Sub3 = document.getElementById("ContentPlaceHolder1_TextBox10").value;
            }

            if (document.getElementById("ContentPlaceHolder1_TextBox14").value == "") {
                Sub4 = 0;
            }
            else {
                Sub4 = document.getElementById("ContentPlaceHolder1_TextBox14").value;
            }
            document.getElementById("ContentPlaceHolder1_TextBox4").value = parseFloat(Sub1) + parseFloat(Sub2) + parseFloat(Sub3) + parseFloat(Sub4);

            var Sub7 = 0, Sub8 = 0, Sub9 = 0, Sub10 = 0;


            if (document.getElementById("ContentPlaceHolder1_TextBox3").value == "") {
                Sub7 = 0;
            }
            else {
                Sub7 = document.getElementById("ContentPlaceHolder1_TextBox3").value;
            }

            if (document.getElementById("ContentPlaceHolder1_TextBox7").value == "") {
                Sub8 = 0;
            }
            else {
                Sub8 = document.getElementById("ContentPlaceHolder1_TextBox7").value;
            }

            if (document.getElementById("ContentPlaceHolder1_TextBox11").value == "") {
                Sub9 = 0;
            }
            else {
                Sub9 = document.getElementById("ContentPlaceHolder1_TextBox11").value;
            }

            if (document.getElementById("ContentPlaceHolder1_TextBox15").value == "") {
                Sub10 = 0;
            }
            else {
                Sub10 = document.getElementById("ContentPlaceHolder1_TextBox15").value;
            }


            document.getElementById("ContentPlaceHolder1_TextBox8").value = parseFloat(Sub7) + parseFloat(Sub8) + parseFloat(Sub9) + parseFloat(Sub10);

            D1 = $('[id$=TextBox4]').val();
            D2 = $('[id$=TextBox8]').val();
            D3 = (D1 / D2) * 100;
            document.getElementById("ContentPlaceHolder1_TextBox12").value = (D3.toFixed(2));




        }
       
            </script>
    <script type="text/javascript">

        function txtHSpercentage1() {
            M4 = $('[id$=txtfee]').val();
            M5 = $('[id$=txtdiscountper]').val();
            M6 = (M4 * M5) / 100;
            document.getElementById("ContentPlaceHolder1_txtscholarshipamount").value = (M6.toFixed(2));
        }
        
         </script>
    <script type="text/javascript">
        function multiplyBy() {
            num1 = document.getElementById("txtfee").value;
            alert("Hello")
            num2 = document.getElementById("txtdiscountper").value;
            document.getElementById("txtscholarshipamount").innerHTML = (num1 * num2) / 100;
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
    </script>

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ty" runat="server"></asp:ScriptManager>
      <fieldset>
        <div class="text-right"  style="padding-left:250px">

            <asp:Button ID="BtnSubmit" runat="server" Text="Approved" OnClick="BtnSubmit_Click" ForeColor="White" CssClass="btn"  BackColor="#ff9900" />
            <asp:Button ID="BtnRejected" runat="server" Text="Rejected" OnClick="BtnRejected_Click" ForeColor="White" CssClass="btn"   BackColor="#ff9900"/>

        </div>
    </fieldset>
    <fieldset class="boxBodyInner">
        <br />
        <asp:GridView ID="grdscholarshipapprovallist" runat="server" DataKeyNames="Enrollment_No"   AlternatingRowStyle-CssClass="danger" PageSize="50"
            AllowPaging="true" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" Visible="true">
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle CssClass="csspager" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <%# Container.DataItemIndex +1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Enrollment No" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblemployeecode" runat="server" Text='<%# Bind("Enrollment_No") %>'></asp:Label>
                        <asp:HiddenField ID="Hfemployeecode" Value='<%# Eval("Enrollment_No") %>' runat="server" />
                        <asp:HiddenField ID="Hfhodname" Value='<%# Eval("Enrollment_No") %>' runat="server" />
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
                        <asp:Label ID="lblprogram" runat="server" Text='<%# Eval("Programme") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               <%-- <asp:TemplateField HeaderText="Student Category" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblStudentCategory" runat="server" Text='<%#Eval("Student_category") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
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
                 <asp:TemplateField HeaderText="Registrar Approval" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblRegistrar" runat="server" Text='<%#Eval("Registrar_Approval") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Account Approval" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblAccount" runat="server" Text='<%#Eval("Account_Approval") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                   <asp:TemplateField HeaderText="Remark" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:TextBox ID="txtRemark" runat="server" Enabled='<%# Eval("txtMarksEnableDesable").ToString().Equals("true") %>' Text='<%#Eval("Reject_Remarks") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" HeaderText="Select" ItemStyle-CssClass="text-center">                   
                    <ItemTemplate>
                        <asp:CheckBox ID="Chkemployee" Enabled='<%# Eval("txtMarksEnableDesable").ToString().Equals("true") %>'   runat="server" AutoPostBack="true"  />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
        </asp:GridView>
        
         <asp:Panel ID="pnlGridViewDetails" CssClass="modalPopup" Width="65%" runat="server" Style="display: none;">

            <div class="header">
                <b>

                    <asp:Label ID="lblNotification" runat="server" Text="Scholarship Declaration Form "></asp:Label></b>
                <div class="close">
                    <asp:Button ID="btnclose" OnClick="btnclose_Click"  runat="server" Text="X" />
                </div>           
            </div>
              <div id="Div1" runat="server" style="max-height: 900px; overflow: auto;">
            <fieldset class="boxBody">     
        <div  style="width: 100%; margin-bottom: 10px; margin-left: 1%; margin-right: 1%; margin-top: 5px;">

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
        <asp:Label ID="Label1" runat="server" Text="Scholarship Declaration Form Session(2023-2024)" Font-Size="15pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <div id="divGeneralBodyenrollmentform">
        <fieldset id="Print" class="boxBodyInner" >
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                              <div class="form-group">
                                  <div class="col-md-1">
                                   
                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px; font-size:medium">Student's Name</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtstudentname" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="col-md-2">
                                    <label style="width: 200px; font-size:medium">Father's Name</label>
                                </div>
                                <div class="col-md-3">
                                      <asp:TextBox ID="txtfathername" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                  </div>  
                              <div class="form-group">
                                   <div class="col-md-1">
                                   
                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px;font-size:medium">Student No.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtStudentNo" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="col-md-2">
                                    <label style="width: 200px; font-size:medium">Programme</label>
                                </div>
                                <div class="col-md-3">
                                      <asp:TextBox ID="txtprogramme" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                  </div>
                              <%--<div class="form-group">
                                     <div class="col-md-1">
                                   
                                    </div>
                                  <div class="col-md-3">
                                       <label style="width: 200px; font-size:large; font:bold">Nature of Scholarship</label>
                                      </div>
                                  </div> --%>                               
                            </div>
                        </div>
                    </div>
                </div>
             <%--<asp:UpdatePanel ID="pnlpic" runat="server">
                       <ContentTemplate>--%>
             <fieldset class="boxBody" style="text-align: left;"">
                 <div class="form-group">
                                  <div class="col-md-3">
                                     <asp:Label ID="Label5" runat="server" Text=" Nature of Scholarship" Font-Size="15pt" ToolTip="Tick The Applicable"   ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                </div>
               <%-- <label style="width: 250px; font:bold; color:red">(Tick as Applicable)</label>--%>
                    
                  <div class="col-md-4">
                 <asp:Textbox ID="txtnatureofscholarship" runat="server" OnTextChanged="txtnatureofscholarship_TextChanged" Enabled="true" AutoPostBack="true" CssClass="form-control">
                 </asp:Textbox>
                   </div>
                     <div class="col-md-4">
                      <asp:TextBox ID="txttypeofexam" runat="server" BorderColor="Black" placeholder="Exam Scholarship is claimed" Visible="false" style='text-transform:uppercase'  CssClass="form-control"></asp:TextBox>
                      </div>
                     </div>
                  
                     
             </fieldset>
            <div id="maxmarks" runat="server" visible="false">
            <fieldset class="boxBodyInner">
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                             <div class="form-group">
                                 <div class="col-md-2">
                                   
                                </div>
                                    <div class="col-md-2">
                                   <label style="font-size:medium; font:bold" class="required">Subject</label>
                                </div>

                                  <div class="col-md-2">
                                   <label style="font-size:medium; font:bold" class="required">Obtained Marks</label>
                                </div>
                                 <div class="col-md-2">
                                   <label style="font-size:medium; font:bold" class="required">Max Marks</label>
                                </div>
                                 <div class="col-md-2">
                                   <label style="font-size:medium; font:bold" class="required">Percentage (%)</label>
                                </div>
                                 </div>
                                  

                             <div class="form-group">
                                 <div class="col-md-2">
                                   
                                </div>
                                    <div class="col-md-2">
                                  <asp:TextBox ID="txtsubject1" runat="server" BorderColor="Black" style='text-transform:uppercase' Enabled="false" class="required" CssClass="form-control"></asp:TextBox>
                                </div>

                                  <div class="col-md-2">
                                  <asp:TextBox ID="txtobtsubject1" runat="server" BorderColor="Black" onchange="txtHSpercentage()" Enabled="false" onkeypress="return numeric(event)"   CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txtmaxsubject1" runat="server" BorderColor="Black" onchange="txtHSpercentage()" Enabled="false" onkeypress="return numeric(event)" CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txtpersubject1" runat="server" BorderColor="Black" Enabled="false"  CssClass="form-control"></asp:TextBox>
                                </div>
                                 </div>

                              <div class="form-group">
                                 <div class="col-md-2">
                                   
                                </div>
                                    <div class="col-md-2">
                                  <asp:TextBox ID="txtsubject2" runat="server" BorderColor="Black" style='text-transform:uppercase' Enabled="false"  CssClass="form-control"></asp:TextBox>
                                </div>

                                  <div class="col-md-2">
                                  <asp:TextBox ID="txtobtsubject2" runat="server" BorderColor="Black" onchange="txtHSpercentage()" Enabled="false" onkeypress="return numeric(event)"  CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txtmaxsubject2" runat="server" BorderColor="Black" onchange="txtHSpercentage()" Enabled="false" onkeypress="return numeric(event)" CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txtpersubject2" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                 </div>

                              <div class="form-group">
                                 <div class="col-md-2">
                                   
                                </div>
                                    <div class="col-md-2">
                                  <asp:TextBox ID="txtsubject3" runat="server" BorderColor="Black" style='text-transform:uppercase' Enabled="false"  CssClass="form-control"></asp:TextBox>
                                </div>

                                  <div class="col-md-2">
                                  <asp:TextBox ID="txtobtsubject3" runat="server" BorderColor="Black" onchange="txtHSpercentage()" Enabled="false" onkeypress="return numeric(event)"   CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txtmaxsubject3" runat="server" BorderColor="Black" onchange="txtHSpercentage()" Enabled="false" onkeypress="return numeric(event)"   CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txtpersubject3" runat="server" BorderColor="Black" Enabled="false"  CssClass="form-control"></asp:TextBox>
                                </div>
                                 </div>
                              
                              <div class="form-group">
                                 <div class="col-md-2">
                                   
                                </div>
                                    <div class="col-md-2">
                                  <asp:TextBox ID="txtsubject4" runat="server" BorderColor="Black" style='text-transform:uppercase' Enabled="false"  CssClass="form-control"></asp:TextBox>
                                </div>

                                  <div class="col-md-2">
                                  <asp:TextBox ID="txtobtsubject4" runat="server" BorderColor="Black" onchange="txtHSpercentage()" Enabled="false"  onkeypress="return numeric(event)"  CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txtmaxsubject4" runat="server" BorderColor="Black" onchange="txtHSpercentage()" Enabled="false"  onkeypress="return numeric(event)"   CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txtpersubject4" runat="server" BorderColor="Black" Enabled="false"  CssClass="form-control"></asp:TextBox>
                                </div>
                                 </div>

                             <div id="subject5" class="form-group">
                                 <div class="col-md-2">
                                   
                                </div>
                                    <div  class="col-md-2">
                                  <asp:TextBox ID="txtsubject5" runat="server" BorderColor="Black" style='text-transform:uppercase' Enabled="false"  CssClass="form-control"></asp:TextBox>
                                </div>

                                  <div class="col-md-2">
                                  <asp:TextBox ID="txtobtsubject5" runat="server" BorderColor="Black"   onchange="txtHSpercentage()" Enabled="false" onkeypress="return numeric(event)" CssClass="form-control"></asp:TextBox>
                                    
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txtmaxsubject5" runat="server" BorderColor="Black"   onchange="txtHSpercentage()" Enabled="false"  onkeypress="return numeric(event)" CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txtpersubject5" runat="server" BorderColor="Black" Enabled="false"  CssClass="form-control"></asp:TextBox>
                                </div>
                                 </div>

                               <div id="subject6" class="form-group" runat="server" style="visibility:hidden">
                                 <div class="col-md-2">
                                   
                                </div>
                                    <div class="col-md-2">
                                  <asp:TextBox ID="txtsubject6" runat="server" BorderColor="Black" style='text-transform:uppercase' Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>

                                  <div class="col-md-2">
                                  <asp:TextBox ID="txtobtsubject6" runat="server"  BorderColor="Black"  onchange="txtHSpercentage()" Enabled="false" onkeypress="return numeric(event)" CssClass="form-control">
                                      
                                  </asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txtmaxsubject6" runat="server" BorderColor="Black"   onchange="txtHSpercentage()"  Enabled="false" onkeypress="return numeric(event)" CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txtpersubject6" runat="server" BorderColor="Black" Enabled="false"  CssClass="form-control"></asp:TextBox>
                                </div>
                                 </div>

                             <div class="form-group">
                                 <div class="col-md-2">
                                   
                                </div>
                                    <div class="col-md-2">
                                 
                                </div>

                                  <div class="col-md-2">
                                  <asp:TextBox ID="txtobtainedmarks" runat="server" BorderColor="Black"   Enabled="false"  CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txttotalmarks" runat="server" BorderColor="Black" Enabled="false"  CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txtpercent" runat="server" BorderColor="Black" CssClass="form-control" Enabled="false" AutoPostBack="true"></asp:TextBox>
                                </div>
                                 </div>
                                   <div class="form-group">  
                                           <div class="col-md-6">                                   
                                </div>                            
                             <div class="col-md-2">
                          <asp:Button ID="btn_Changesubject" runat="server" Text=" Fill Main Four Subject" OnClick="btn_Changesubject_Click" Height="30px"  CssClass="form-control" BackColor="#ff9933" ForeColor="White" />
                                        </div>
                                       </div>
                            </div>
                        </div>
                    </div>
                </div>
                </fieldset>
                </div>
            <div id="Mainfoursubject" runat="server" visible="false">
            <fieldset class="boxBodyInner">
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                             <div class="form-group">
                                 <div class="col-md-3">
                                   
                                </div>
                                    <div class="col-md-2">
                                   <label style="font-size:medium; font:bold" class="required">Subject</label>
                                </div>

                                  <div class="col-md-2">
                                   <label style="font-size:medium; font:bold" class="required">Obtained Marks</label>
                                </div>
                                 <div class="col-md-2">
                                   <label style="font-size:medium; font:bold" class="required">Max Marks</label>
                                </div>
                             
                                 </div>
                                  

                             <div class="form-group">
                                 <div class="col-md-3">
                                   
                                </div>
                                    <div class="col-md-2">
                                  <asp:TextBox ID="TextBox1" runat="server" BorderColor="Black" style='text-transform:uppercase' class="required" CssClass="form-control"></asp:TextBox>
                                </div>

                                  <div class="col-md-2">
                                  <asp:TextBox ID="TextBox2" runat="server" BorderColor="Black" onchange="txtHSpercentage()" onkeypress="return numeric(event)"   CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="TextBox3" runat="server" BorderColor="Black" onchange="txtHSpercentage()" onkeypress="return numeric(event)" CssClass="form-control"></asp:TextBox>
                                </div>
                                
                                 </div>

                              <div class="form-group">
                                 <div class="col-md-3">
                                   
                                </div>
                                    <div class="col-md-2">
                                  <asp:TextBox ID="TextBox5" runat="server" BorderColor="Black" style='text-transform:uppercase'  CssClass="form-control"></asp:TextBox>
                                </div>

                                  <div class="col-md-2">
                                  <asp:TextBox ID="TextBox6" runat="server" BorderColor="Black" onchange="txtHSpercentage()" onkeypress="return numeric(event)"  CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="TextBox7" runat="server" BorderColor="Black" onchange="txtHSpercentage()" onkeypress="return numeric(event)" CssClass="form-control"></asp:TextBox>
                                </div>
                                
                                 </div>

                              <div class="form-group">
                                 <div class="col-md-3">
                                   
                                </div>
                                    <div class="col-md-2">
                                  <asp:TextBox ID="TextBox9" runat="server" BorderColor="Black" style='text-transform:uppercase'  CssClass="form-control"></asp:TextBox>
                                </div>

                                  <div class="col-md-2">
                                  <asp:TextBox ID="TextBox10" runat="server" BorderColor="Black" onchange="txtHSpercentage()" onkeypress="return numeric(event)"   CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="TextBox11" runat="server" BorderColor="Black" onchange="txtHSpercentage()" onkeypress="return numeric(event)"   CssClass="form-control"></asp:TextBox>
                                </div>
                             
                                 </div>
                               <div class="form-group">
                                 <div class="col-md-3">
                                   
                                </div>
                                    <div class="col-md-2">
                                  <asp:TextBox ID="TextBox13" runat="server" BorderColor="Black" style='text-transform:uppercase'  CssClass="form-control"></asp:TextBox>
                                </div>

                                  <div class="col-md-2">
                                  <asp:TextBox ID="TextBox14" runat="server" BorderColor="Black" onchange="txtHSpercentage()" onkeypress="return numeric(event)"   CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="TextBox15" runat="server" BorderColor="Black" onchange="txtHSpercentage()" onkeypress="return numeric(event)"   CssClass="form-control"></asp:TextBox>
                                </div>
                              
                                 </div>

                            <div class="form-group">
                                 <div class="col-md-3">                                  
                                </div>
                                    <div class="col-md-2">
                                   <label style="font-size:medium; font:bold">Obtained Marks</label>
                                </div>
                                  <div class="col-md-2">
                                   <label style="font-size:medium; font:bold">Total Marks</label>
                                </div>
                                 <div class="col-md-2">
                                   <label style="font-size:medium; font:bold" class="required">Percentage</label>
                                </div>                             
                                 </div>
                             <div class="form-group">
                                 <div class="col-md-3">
                                   
                                </div>
                                    <div class="col-md-2">
                                 <asp:TextBox ID="TextBox4" runat="server" BorderColor="Black" onchange="txtHSpercentage()" Enabled="false" onkeypress="return numeric(event)"   CssClass="form-control"></asp:TextBox>
                                </div>

                                  <div class="col-md-2">
                                   <asp:TextBox ID="TextBox8" runat="server" BorderColor="Black" onchange="txtHSpercentage()" Enabled="false" onkeypress="return numeric(event)"   CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                  <asp:TextBox ID="TextBox12" runat="server" BorderColor="Black" onchange="txtHSpercentage()" Enabled="false" onkeypress="return numeric(event)"   CssClass="form-control"></asp:TextBox>
                                </div>
                             
                                 </div>                          
                              </div>
                        </div>
                    </div>
                </div>
                </fieldset>
                </div>            
            <div id="divchancellorscholarship" runat="server" visible="false">
            <fieldset class="boxBodyInner">
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-16">
                             <div class="form-group">   
                                 <div class="col-md-1">
                                   
                                </div>                           
                                    <div class="col-md-2">
                                   <label style="font-size:medium; font:bold" class="required">Employee Name:</label>
                                </div>

                                  <div class="col-md-3">
                                      <asp:TextBox ID="txtemployeename" runat="server" BorderColor="Black" Enabled="false" style='text-transform:uppercase' CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <label style="font-size:medium; font:bold" class="required">Employee Code:</label>
                                </div>
                                 <div class="col-md-3">
                                  <asp:TextBox ID="txtemployeecode" runat="server" BorderColor="Black" style='text-transform:uppercase' Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                 </div>
                              <div class="form-group">
                                   <div class="col-md-1">
                                   
                                </div>
                                   <div class="col-md-2">
                                   <label style="font-size:medium; font:bold" class="required">Designation:</label>
                                </div>
                                 <div class="col-md-3">
                                  <asp:TextBox ID="txtdesignation" runat="server" BorderColor="Black" style='text-transform:uppercase' Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                   <div class="col-md-2">
                                   <label style="font-size:medium; font:bold" class="required">Aadhar No:</label>
                                </div>
                                 <div class="col-md-3">
                                  <asp:TextBox ID="txtaadharno" runat="server" BorderColor="Black" style='text-transform:uppercase' Enabled="false" onkeypress="return numeric(event)" MaxLength="12" MinLength="12" CssClass="form-control"></asp:TextBox>
                                </div>
                                 </div>                            

                            </div>
                        </div>
                    </div>
                </div>
                </fieldset>
                </div>

             <div id="divjainscholarship" runat="server" visible="false">
            &nbsp;<fieldset class="boxBodyInner">
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                             <div class="form-group">   
                                 <div class="col-md-1">
                                   
                                </div>                           
                                    <div class="col-md-2">
                                   <label style="font-size:medium ;width: 200px; font:bold">Student Name:</label>
                                </div>

                                  <div class="col-md-2">
                                      <asp:TextBox ID="txtstudentnamejain" runat="server" BorderColor="Black" style='text-transform:uppercase' Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <label id="lblreligion" style="font-size:medium; font:bold" runat="server" visible="false">Religion</label>
                                </div>
                                 <div class="col-md-2">
                                    <asp:TextBox ID="txtreligion1" runat="server" BorderColor="Black" style='text-transform:uppercase' Enabled="false" visible="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                  
                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" Visible="false" Width="200px" />
                                &nbsp;<asp:LinkButton ID="lnkCertificate" runat="server" Enabled='<%# (Eval("[Jain Certificate]").ToString() ==  "" ?  false : true) %>' OnClick="lnkCertificate_Click" Text="View Jain Certificate"  Visible="false" Font-Size="Larger" Font-Bold="true"  />
                               &nbsp;
                                </div>                                                                 
                                 </div>  
                              <div class="col-md-2">
                               </div>                            
                                 
                                 <div class="form-group">   
                                  <div class="col-md-1">                                  
                                     <asp:TextBox ID="txtreligion" runat="server" BorderColor="Black" style='text-transform:uppercase' Visible="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                 </div>
                             
                            

                            </div>
                        </div>
                    </div>
                </div>
                </fieldset>
                </div>
            
            </fieldset>
          <div id="div3" runat="server">
            &nbsp;<fieldset class="boxBodyInner">
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                             <asp:UpdatePanel ID="pnlpic" runat="server">
                       <ContentTemplate>
                             <div class="form-group">
                                     <div class="col-md-1">
                                   
                                </div>
                                     <div class="col-md-2">
                                   <label style="width: 600px; font-size:medium; font:bold">Applicable Scholarship</label>
                                    </div>
                                 <div class="col-md-2">
                                    <asp:DropDownList ID="drpapplicablescholarship" runat="server" OnSelectedIndexChanged="drpapplicablescholarship_SelectedIndexChanged" AutoPostBack="true" BorderColor="Black" CssClass="form-control">
                                        <asp:ListItem Text="SELECT" Value="0"></asp:ListItem>
                                          <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                                          <asp:ListItem Text="NO" Value="2"></asp:ListItem>                                        
                                    </asp:DropDownList>
                                    </div>
                                 <div class="col-md-2" style="text-align:center">
                                   <label id="lblfee" runat="server" style="font-size:medium; font:bold" visible="false">Yearly Tuition Fee</label>
                                    </div>
                                  <div class="col-md-2">
                                    <asp:TextBox ID="txtfee" runat="server"  BorderColor="Black"  onchange="txtHSpercentage1()"  onkeypress="return numeric(event)" Visible="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                  <div class="col-md-1" style="text-align:right">
                                   <label id="lbldis" runat="server" style="font-size:medium; font:bold" visible="false">Dis.Per(%)</label>
                                    </div>
                                  <div class="col-md-2">
                                    <asp:TextBox ID="txtdiscountper" runat="server" onchange="txtHSpercentage1()" onkeypress="return numeric(event)"  BorderColor="Black" Visible="false" CssClass="form-control"></asp:TextBox>
                                    </div>

                        </div>
                              <div class="form-group">
                                    <div class="col-md-1">
                                   
                                </div>
                                     <div class="col-md-2">
                                   <label id="lblscamount" runat="server" style="width: 600px; font-size:medium; font:bold" visible="false">Scholarship Amount</label>
                                    </div>
                                 <div class="col-md-2">
                                    <asp:TextBox ID="txtscholarshipamount" runat="server" onkeypress="return numeric(event)"   Visible="false"  BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                    </div>
                        </div>
                          
                           </ContentTemplate>
                                  </asp:UpdatePanel>
                              <div class="form-group">
                                    <div class="col-md-1">
                                   
                                </div>
                                     <div class="col-md-11">
                                   <label style=" font-size:medium; font:bold">Undertaking from Student/parents I/We understand that scholarship amount granted allowed hereby is provisional and is applicable for the session 2023-2024
                                       only.If any discrepancy is found later on,due to which the amount of scholarship availed gets changed, the amount of scholarship so changed after taking into consideration the discrepancy, shall be acceptable
                                       to me as final and binding.
                                   </label>
                                    </div>
                                  
                                   <div class="col-md-2">
                                                                                            </div>
                                                                                              
                                 
                        </div>
                            
                                  <div class="form-group">
                                      </div>

                                 <div class="form-group">
                                      <div class="col-md-4">
                                          </div>

                                 <div class="col-md-6">
                                <asp:LinkButton ID="lblTran" runat="server" Enabled='<%# (Eval("Pre_Education").ToString() ==  "" ?  false : true) %>'  Text=" Dowload Pre_Qualification Marksheet" Font-Size="X-Large" Font-Bold="true"  OnClick="lblTran_Click" />
                                  </div>
                                     </div>
                            <br />   
                            <div class="form-group">
                       <div class="col-md-5">
                      </div>
                                                   
                <div class="col-md-1">
                    <asp:Button ID="btn_Save" runat="server" Text="Save"   CssClass="form-control" BackColor="#ff9933" Visible="false"  OnClick="btn_Save_Click" ForeColor="White" Width="80px" Height="30px" />
                    </div>
                         
                                  <div class="col-md-1">
                    <asp:Button ID="btn_reset" runat="server" Text="Reset"   CssClass="form-control" BackColor="#ff9933" Visible="false"  ForeColor="White" Width="80px" Height="30px" />
                    </div>
                                  </div>
                                 <div class="col-md-3" style="visibility:hidden">
                                    <asp:TextBox ID="txtenrollmentnumber" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                             <div class="col-md-3" style="visibility:hidden">
                                    <asp:TextBox ID="txtid" runat="server" BorderColor="Black" Enabled="false"  CssClass="form-control"></asp:TextBox>
                                </div>
                                 
                              </div>  
                
                        
            </div>
                 </div>
                </div>
            </fieldset>
              </div>
        
        </div>
                    </div>
           
       
          </asp:Panel>  
        <asp:Button ID="btnDummy" runat="server" Style="display: none;" /> 
        <asp:ModalPopupExtender ID="GridViewDetails" runat="server" TargetControlID="btnDummy"
            PopupControlID="pnlGridViewDetails" BackgroundCssClass="modalBackground" />
       
            </fieldset>
       
       
    

</asp:Content>

