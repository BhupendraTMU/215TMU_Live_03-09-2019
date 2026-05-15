<%@ Page Title="" Language="C#" MasterPageFile="~/MasterContent.master" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Registration" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="js/jquery-1.11.0.min.js" type="text/javascript"></script>
        <script src="js/bootstrap.min.js" type="text/javascript"></script
        <script src="js/wow.min.js" type="text/javascript"></script>
    <link href="bootstrap/css/bootstrap.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table>
        <tr>
            <td>
                 <div class="loading" align="center">  Loading. Please wait.<br />  <br />  <img src="images/loader.gif" alt="" />
                 </div>
    <div style="width: 100%">
        <div class="container" style="padding-top: inherit; padding-left: 5px;">
            <br />
            <div class="row">
                <div class="col-lg-10" style="background-color: #337ab7" >
                     <div class="panel panel-info" style="border-color: #337ab7">
                        <div class="panel-heading" style="background-color: #337ab7">
                            <center>
                                <div class="panel-title" style="fit-position: center;">
                                    <b>
                                        <p style="color: white; font-size: 25px">Registration Form</p>
                                    </b>
                                </div>
                            </center>
                        </div>
                        <div class="panel panel-info" style="background-color: #ffd800">
                             <asp:RadioButtonList ID="RadioButtonList1" runat="server"  Width="500px" Style="padding-right: 20px" RepeatDirection="Horizontal">
                                 <asp:ListItem>Generate Enquiry No</asp:ListItem>
                                 <asp:ListItem>Fill Application Form</asp:ListItem>
                                 <asp:ListItem>Payment</asp:ListItem>
                             </asp:RadioButtonList>

                         </div>
                     </div>
                     <div >
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">   <ContentTemplate>
                     
                   
                            <div class="panel-body" id="body">
                                <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="lblEnquiryNo" style="line-height:30px" runat="server" Text="Label"><b>Enquiry No</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-left:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   <asp:TextBox ID="txtEnquiryNo"  runat="server" CssClass="form-control input-sm" placeholder="Enquiry No" ></asp:TextBox>
                                   <%--<asp:Button ID="btnValidate" runat="server" />--%>
                           </div>
                               </div>
                                    </div>
                                <div class="col-lg-2" style="padding-right:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label28" style="line-height:30px" runat="server" Text="Label"><b>Medium Instruction</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-right:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   <asp:DropDownList ID="DropDownList1" CssClass="form-control input-sm" placeholder="Course" runat="server"></asp:DropDownList>
                           </div>
                               </div>
                                    </div>

                                <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label2" style="line-height:30px" runat="server" Text="Label"><b>Academic Year</b></asp:Label> 
                                    </div>
                                     </div>
                                    <div class="col-lg-4" style="padding-left:0px;">
                                <div class="form-group">  
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>                                                           
                                    <asp:DropDownList ID="drpAcademicYear" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                                   </div>
                                    </div>
                           </div>
                                 <div class="col-lg-2" style="padding-right:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label1" style="line-height:30px" runat="server" Text="Label"><b>No.</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-right:0px;">
                           <div class="form-group">       
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>                      
                               <asp:Label ID="lblNo" CssClass="form-control input-sm" runat="server" Text="Label"></asp:Label>
                           </div>
                                    </div>
                                    </div>
                                <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label3" style="line-height:30px" runat="server" Text="Label"><b>Date of sale</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-left:0px;">
                            <div class="form-group"> 
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>
                               <asp:Label ID="lblDateOfSale" CssClass="form-control input-sm" runat="server" Text="Label"></asp:Label>                                  
                                </div> 
                                </div> 
                               </div>
                                <div class="col-lg-2" style="padding-right:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label4" style="line-height:30px" runat="server" Text="Label"><b>Citizenship</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-right:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>
                                   <asp:DropDownList ID="drpCitizen" CssClass="form-control input-sm" placeholder="Course" runat="server"></asp:DropDownList>
                           </div>
                               </div>
                                    </div>
                                <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label5" style="line-height:30px" runat="server" Text="Label"><b>Course Code</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-left:0px;">
                                 <div class="form-group" >    
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-globe"></span></span>
                                   <asp:DropDownList ID="drpCourse" CssClass="form-control input-sm" AutoPostBack="true" placeholder="Course" runat="server" >                                       
                                   </asp:DropDownList>
                           </div>
                               </div>
                                    </div>
                                <div class="col-lg-2" style="padding-right:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label6" style="line-height:30px" runat="server" Text="Label"><b>Prequalification</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-right:0px;">
                                 <div class="form-group" >       
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-copyright-mark"></span></span>
                                   <asp:DropDownList ID="drpPrequalification" CssClass="form-control input-sm" placeholder="Course" runat="server"></asp:DropDownList>
                           </div>
                               </div>
                                    </div>
                                <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label7" style="line-height:30px" runat="server" Text="Label"><b>Enquiry No</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-left:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   
                                   <asp:DropDownList ID="drpEnquiryNo" CssClass="form-control input-sm" placeholder="Course" runat="server" AutoPostBack="true" >
                                   </asp:DropDownList>
                                         
                           </div>                                    
                               </div>
                                    </div>
                                <div class="col-lg-2" style="padding-right:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label9" style="line-height:30px" runat="server" Text="Label"><b>Name of previous inst.</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-right:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   <asp:TextBox ID="txtPreviousInst" runat="server" CssClass="form-control input-sm" placeholder="Previous Institute"></asp:TextBox>
                           </div>
                               </div>
                                    </div>
                                 <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label8" style="line-height:30px" runat="server" Text="Label"><b>Applicant Name</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-left:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   <asp:TextBox ID="txtApplicantName" Name="txtApplicantName" runat="server" CssClass="form-control input-sm" placeholder="Applicant Name" ></asp:TextBox>
                           </div>
                               </div>
                                    </div>
                                <div class="col-lg-2" style="padding-right:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label12" style="line-height:30px" runat="server" Text="Label"><b>Medium Instruction</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-right:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   <asp:DropDownList ID="drpMedium" CssClass="form-control input-sm" placeholder="Course" runat="server"></asp:DropDownList>
                           </div>
                               </div>
                                    </div>
                                <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label13" style="line-height:30px" runat="server" Text="Label"><b>Gender</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-left:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   <asp:DropDownList ID="drpGender" CssClass="form-control input-sm" placeholder="Course" runat="server">
                                  
                                   </asp:DropDownList>
                           </div>
                               </div>
                                    </div>
                                <div class="col-lg-2" style="padding-right:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label14" style="line-height:30px" runat="server" Text="Label"><b>Hostel Acommodation</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-right:0px;">
                                 <div class="form-group" > 
                                   <asp:CheckBox ID="chkHostelAcommodation" CssClass="form-control input-sm" runat="server" />
                           
                               </div>
                                    </div>
                                <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label15" style="line-height:30px" runat="server" Text="Label"><b>Date of Birth</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-3" style="padding-left:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   <asp:TextBox ID="txtDOB" CssClass="form-control input-sm" runat="server" onkeypress="return false" onKeyDown="preventBackspace();"></asp:TextBox>
                                   
                           </div>
                               </div>
                                    </div>
                                <div class="col-lg-1" style="padding-left:0px;">
                                    <asp:Image src="Images/Calendar.png" runat="server" Height="30px" Width="25px" alt="" id="fdate" />
                                    <asp:CalendarExtender ID="CalendarExtender2" Format="dd MMM yyyy" runat="server"
                                        CssClass="cal_Theme1" PopupButtonID="fdate" Enabled="true" TargetControlID="txtDOB">
                                   </asp:CalendarExtender>
                                  </div>
                                <div class="col-lg-2" style="padding-right:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label16" style="line-height:30px" runat="server" Text="Label"><b>Quota</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-right:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   <asp:DropDownList ID="drpQuota" CssClass="form-control input-sm" placeholder="Course" runat="server" AutoPostBack="true" >
                                       
                                   </asp:DropDownList>
                           </div>
                               </div>
                                    </div>
                                <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label17" style="line-height:30px" runat="server" Text="Label"><b>Age</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-left:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   <asp:TextBox ID="txtAge" runat="server" CssClass="form-control input-sm" placeholder="Age"></asp:TextBox>
                           </div>
                               </div>
                                    </div>
                                <div class="col-lg-2" style="padding-right:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label18" style="line-height:30px" runat="server" Text="Label"><b>Application Cost</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-right:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   <asp:Label ID="lblApplicationCost" CssClass="form-control input-sm" runat="server" ></asp:Label>
                           </div>
                               </div>
                                    </div>
                                <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label19" style="line-height:30px" runat="server" Text="Label"><b>Months</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-left:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   <asp:TextBox ID="txtMonth" runat="server" CssClass="form-control input-sm" placeholder="Months"></asp:TextBox>
                           </div>
                               </div>
                                    </div>
                                <div class="col-lg-2" style="padding-right:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label20" style="line-height:30px" runat="server" Text="Label"><b>Mode of Payment</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-right:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   <asp:DropDownList ID="drpModeOfPayment" CssClass="form-control input-sm" placeholder="Course" AutoPostBack="true"  >
                                       <asp:ListItem Value="0">--Select--</asp:ListItem>
                                       <asp:ListItem Value="1">Online</asp:ListItem>
                                       <asp:ListItem Value="2">Offline</asp:ListItem>
                                   </asp:DropDownList>
                           </div>
                               </div>
                                    </div>
                                <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label21" style="line-height:30px" runat="server" Text="Label"><b>Father's Name</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-left:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control input-sm" placeholder="Father's Name"></asp:TextBox>
                           </div>
                               </div>
                                    </div>
                                <div class="col-lg-2" style="padding-right:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label22" style="line-height:30px" runat="server" Text="Label"><b>Cheque/ DD No.</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-right:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   <asp:TextBox ID="txtChequeNo" runat="server" CssClass="form-control input-sm" placeholder="Cheque/ DD No"></asp:TextBox>
                           </div>
                               </div>
                                    </div>
                                <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label23" style="line-height:30px" runat="server" Text="Label"><b>Mother's Name</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-left:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   <asp:TextBox ID="txtMotherName" runat="server" CssClass="form-control input-sm" placeholder="Mother's Name"></asp:TextBox>
                           </div>
                               </div>
                                    </div>
                                <div class="col-lg-2" style="padding-right:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label24" style="line-height:30px" runat="server" Text="Label"><b>Cheque/DD Date</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-3" style="padding-right:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   <asp:TextBox ID="txtChequeDate" CssClass="form-control input-sm" runat="server" onkeypress="return false" onKeyDown="preventBackspace();"></asp:TextBox>
                                   
                           </div>
                               </div>
                                    </div>
                                <div class="col-lg-1" style="padding-right:0px;">
                                    <asp:Image src="Images/Calendar.png" runat="server" Height="30px" Width="25px" alt="" id="fdate1" />
                                    <asp:CalendarExtender ID="CalendarExtender1" Format="dd MMM yyyy" runat="server"
                                        CssClass="cal_Theme1" PopupButtonID="fdate1" Enabled="true" TargetControlID="txtChequeDate">
                                   </asp:CalendarExtender>
                                  </div>
                                <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label10" style="line-height:30px" runat="server" Text="Label"><b>Aadhar Number</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-left:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   <asp:TextBox ID="txtAadharNo" runat="server" CssClass="form-control input-sm" placeholder="Aadhar Number"></asp:TextBox>
                           </div>
                               </div>
                                    </div>
                                <div class="col-lg-2" style="padding-right:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label25" style="line-height:30px" runat="server" Text="Label"><b>Bank Name</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-right:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control input-sm" placeholder="Bank Name"></asp:TextBox>
                           </div>
                               </div>
                                    </div>
                                
                                <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label11" style="line-height:30px" runat="server" Text="Label"><b>E-Mail Address</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-left:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="form-control input-sm" placeholder="E-Mail Address"></asp:TextBox>
                           </div>
                               </div>
                                    </div><div class="col-lg-2" style="padding-right:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label26" style="line-height:30px" runat="server" Text="Label"><b>Facebook ID</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-right:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   <asp:TextBox ID="txtFacebookId" runat="server" CssClass="form-control input-sm" placeholder="Facebook ID"></asp:TextBox>
                           </div>
                               </div>
                                    </div>
                                            <div class="form-group">
                                                 <div class="col-md-9 col-md-offset-3">
                                                    <div id="messages"></div>
                                            </div>
                                </div>
                           </div>
                                       
                            
             
                <div class="panel panel-info" style="border-color:#337ab7">
                    <div class="panel-heading" style="background-color:#337ab7; height:35px; cursor:pointer" id="head2" ><center>
                        <div class="panel-title" id="Div2"  ><b><p  style="color:white;font-size:15px"> Family Details</p></b></div> </center>
                        </div>   
                         
                                <div class="panel-body" id="body2">
                              
                                    <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label29" style="line-height:30px" runat="server" Text="Label"><b>Father Qualification</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-left:0px;">
                           <div class="form-group">       
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>                      
                               <asp:TextBox ID="txtFatherQualification" Name="txtFatherQualification" runat="server" CssClass="form-control input-sm" placeholder="Applicant Name" ></asp:TextBox>
                           </div>
                                    </div>
                                    </div>

                                    <div class="col-lg-2" style="padding-right:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label30" style="line-height:30px" runat="server" Text="Label"><b>Mother Qualification</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-right:0px;">
                           <div class="form-group">       
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>                      
                               <asp:TextBox ID="txtMotherQualification" Name="txtMotherQualification" runat="server" CssClass="form-control input-sm" placeholder="Applicant Name" ></asp:TextBox>
                           </div>
                                    </div>
                                    </div>

                                    <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label31" style="line-height:30px" runat="server" Text="Label"><b>Father Occupation</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-left:0px;">
                           <div class="form-group">       
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>                      
                               <asp:TextBox ID="txtFatherOccupation" Name="txtFatherOccupation" runat="server" CssClass="form-control input-sm" placeholder="Applicant Name" ></asp:TextBox>
                           </div>
                                    </div>
                                    </div>

                                    <div class="col-lg-2" style="padding-right:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label32" style="line-height:30px" runat="server" Text="Label"><b>Mother Occupation</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-right:0px;">
                           <div class="form-group">       
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>                      
                               <asp:TextBox ID="txtMotherOccupation" Name="txtMotherOccupation" runat="server" CssClass="form-control input-sm" placeholder="Applicant Name" ></asp:TextBox>
                           </div>
                                    </div>
                                    </div>

                                     <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label33" style="line-height:30px" runat="server" Text="Label"><b>Father Annual Income</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-left:0px;">
                           <div class="form-group">       
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>                      
                               <asp:TextBox ID="txtFatherAnnualIncome" Name="txtFatherAnnualIncome" runat="server" CssClass="form-control input-sm" placeholder="Applicant Name" ></asp:TextBox>
                                 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtFatherAnnualIncome" FilterType="Numbers, Custom"></asp:FilteredTextBoxExtender>
                           </div>
                                    </div>
                                    </div>

                                     <div class="col-lg-2" style="padding-right:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label34" style="line-height:30px" runat="server" Text="Label"><b>Mother Annual Income</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-right:0px;">
                           <div class="form-group">       
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>                      
                               <asp:TextBox ID="txtMotherAnnualIncome" Name="txtMotherAnnualIncome" runat="server" CssClass="form-control input-sm" placeholder="Applicant Name" ></asp:TextBox>
                                   <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMotherAnnualIncome" FilterType="Numbers, Custom"></asp:FilteredTextBoxExtender>
                           </div>
                                    </div>
                                    </div>
                                    </div>
                   
                                        </div>
                                <div class="panel panel-info" style="border-color:#337ab7">
                    <div class="panel-heading" style="background-color:#337ab7; height:35px; cursor:pointer"; id="head3" ><center>
                        <div class="panel-title" id="Div3"  ><b><p  style="color:white;font-size:15px"> Applicant Qualification</p></b></div> </center>
                        </div>   
                         
                                <div class="panel-body" id="body3">
                              
                                <table class="table">
                           
                                <tr>
                                    <th style="font-size:15px">Code&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</th>
                                    <th style="font-size:15px">Description</th>
                                    <th style="font-size:15px">Mark Obtained</th>
                                    <th style="font-size:15px">Maximum</th>
                                    <th style="font-size:15px">Month</th>
                                    <th style="font-size:15px">Year of Passing</th>
                                    <th style="font-size:15px">ADD</th>
                                    
                                </tr>
                            
                           
                                <tr>
                                    <td>
                                    <div class="col-lg-12" style="padding-left:0px;">
                                        
                               <asp:DropDownList ID="drpPrequalificationSubCode" CssClass="form-control input-sm"  runat="server" ></asp:DropDownList>
                                  
                                  </div>
                                        </td>
                                        <td>
                                         <div class="col-lg2" style="padding-left:0px;">
                                             <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control input-sm" placeholder="Description"></asp:TextBox>
                                                                                          
                                            </div>
                                    </td>
                                    <td>
                                         <div class="col-lg2" style="padding-left:0px;">
                                             <asp:TextBox ID="txtMarkObtained" runat="server" CssClass="form-control input-sm" MaxLength="3" placeholder="Mark Obtained"></asp:TextBox>
                                             <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtMarkObtained"  FilterType="Numbers, Custom"></asp:FilteredTextBoxExtender>
                                             
                                            </div>
                                    </td>
                                    <td>
                                         <div class="col-lg2" style="padding-left:0px;">
                                             <asp:TextBox ID="txtMaximum" runat="server" CssClass="form-control input-sm" MaxLength="3" placeholder="Maximum"></asp:TextBox>
                                             <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtMaximum" FilterType="Numbers, Custom"></asp:FilteredTextBoxExtender>
                                             
                                            </div>
                                    </td>
                                    <td>
                                         <div class="col-lg2" style="padding-left:0px;">
                                             <asp:TextBox ID="txtMonth1" runat="server" CssClass="form-control input-sm" MaxLength="2" placeholder="Month"></asp:TextBox>
                                             <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtMonth1" FilterType="Numbers, Custom"></asp:FilteredTextBoxExtender>
                                             
                                            </div>
                                    </td>
                                    <td>
                                         <div class="col-lg2" style="padding-left:0px;">
                                             <asp:TextBox ID="txtYearofPassing" runat="server"  CssClass="form-control input-sm" MaxLength="4" placeholder="Year of Passing"></asp:TextBox>
                                             <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtYearofPassing" FilterType="Numbers, Custom"></asp:FilteredTextBoxExtender>                                                                                          
                                             <asp:RegularExpressionValidator ID="RegularExpresphone2" ValidationGroup="g1" Display="Dynamic"
                                                    ControlToValidate="txtYearofPassing" runat="server" ForeColor="Red" ErrorMessage="Enter Valid Year!"
                                                    SetFocusOnError="True" ValidationExpression="^\d{4}$"></asp:RegularExpressionValidator>
                                            </div>
                                    </td>
                                     <td>
                                         <div class="col-lg2" style="padding-left:0px;">                                             
                                             <asp:Button ID="btnAdd" runat="server"  CssClass="form-control input-sm" Text="ADD" ValidationGroup="g1" />
                                            </div>
                                    </td>
                                </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:Label ID="lblerror" style="line-height:30px" ForeColor="Red" runat="server" Text=""></asp:Label> 
                                        </td>
                                    </tr>
                         
                        </table>
                                   
                                
                                  

                                     <div class="table-responsive">
                    <asp:GridView ID="grdQualification" runat="server" Width="100%" DataKeyNames="SubjectCode" CssClass="table table-striped table-bordered table-hover"   AutoGenerateColumns="False"  EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="10" >
                        <Columns>                            
                            <asp:BoundField DataField="SubjectCode" HeaderText="Code" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="SubDescription" HeaderText="Sub Description" SortExpression="CourseCode" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="MarksObtained" HeaderText="Marks Obtained" SortExpression="FathersName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="MaximumMarks" HeaderText="Maximum Marks" SortExpression="EnquiryType" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Month" HeaderText="Month" SortExpression="Country" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="YearOfPassing" HeaderText="Year Of Passing" SortExpression="DOB" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:ButtonField  CommandName="Select" Text="Delete" />
                        </Columns>                       
                        <AlternatingRowStyle CssClass="danger" />
                    </asp:GridView>
                                         </div>
                                    </div>
                   
                                        </div>
                                 </ContentTemplate>
                                       <Triggers>
                                           <%--<asp:AsyncPostBackTrigger ControlID="drpEnquiryNo"  EventName="SelectedIndexChanged" />
                                           <asp:AsyncPostBackTrigger ControlID="drpPrequalificationSubCode"  EventName="SelectedIndexChanged" />--%>
                                           
                                       </Triggers>
                                       </asp:UpdatePanel>
                         <div class="panel-footer" id="footer">
                             <center>
                                <asp:Button ID="Button1" runat="server"  ValidationGroup="g1" Text="Submit" CssClass="btn-lg btn-primary btn-block"  Height="43px" Width="93px" />
                                    </center>
                         </div>

                     </div>
                    
                </div>

            </div>
        </div>
    </div>
            </td>
        </tr>
    </table>
   
</asp:Content>


<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
           <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
     <div style="width: 100%">
        <div class="container" style="padding-top: inherit; padding-left: 5px;">
            <br />
            <asp:UpdatePanel runat="server" ID="updmain"><ContentTemplate>
            <div class="row">
              <div class="col-lg-10" style="background-color: #ffd800" >
                    <div class="panel-heading" style="background-color: #337ab7">
                            <center>
                                <div class="panel-title" style="fit-position: center;">
                                    <b>
                                        <p style="color: white; font-size: 25px">Registration Form</p>
                                    </b>
                                </div>
                            </center>
                        </div>

                  <div id="divMain" runat="server">
         <table>
             <tr>
                 <td>
                 </td>
                 <asp:RadioButtonList ID="RblRegistration" runat="server" RepeatLayout="Table" Width="500px" Style="padding-right:20px" RepeatDirection="Horizontal">
                     <asp:ListItem>Generate Enquiry No</asp:ListItem>
                     <asp:ListItem>Fill Application Form</asp:ListItem>                     
                     <asp:ListItem>Payment</asp:ListItem>
                 </asp:RadioButtonList>
             </tr>
         </table>
    
    <div id="divApplication" runat="server"    >
        <table>
            <tr>
                <td colspan="2">
                    
                </td>
            </tr>
            <tr>
                <td>Name</td>
                <td>
                     <asp:TextBox ID="txtName" runat="server" CssClass="form-control input-sm" placeholder="Name"></asp:TextBox>
                      </td>
            </tr>
            <tr>
                <td>User Password</td>
                <td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" /></td>
            </tr>
            <tr>
                <td>Confirm Password</td>
                <td><asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"/></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divPay" runat="server"   >
        </div>
        </div>
                </div>

 <div class="col-lg-8" style="background-color: whitesmoke" >
                     <div class="col-lg-2" style="padding-left: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="Label8" Style="line-height: 30px" runat="server" Text="Label"><b>Applicant Name</b></asp:Label>
                                        </div>
                                    </div>
                     <div class="col-lg-4" style="padding-left: 5px;">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                                <asp:TextBox ID="txtApplicantName" runat="server" CssClass="form-control input-sm" placeholder="Applicant Name" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                     <div class="col-lg-2" style="padding-right: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="Label4" Style="line-height: 30px" runat="server" Text="Label"><b>Course</b></asp:Label>
                                        </div>
                                    </div>
                     <div class="col-lg-4" style="padding-right: 5px;">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>
                                                <asp:DropDownList ID="ddlCourse" CssClass="form-control input-sm" placeholder="Course" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div
            
 
  </div></div>
            
            </ContentTemplate></asp:UpdatePanel>
        </div>
         </div>
</asp:Content>--%>

