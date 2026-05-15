<%@ Page Title="" Language="C#" MasterPageFile="~/MasterContent.master" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Test" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <link href="bootstrap/css/bootstrap.css" rel="stylesheet" />
    <script src="bootstrap/js/bootstrap.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <script src="Scripts/alertify/alertify.js"></script>
    <script src="Scripts/alertify/alertify.min.js"></script>
    <link href="packages/alertify.0.3.11/content/Content/alertify/alertify.core.css" rel="stylesheet" />
    <link href="packages/alertify.0.3.11/content/Content/alertify/alertify.default.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div id="dvAppDetails" class="panel panel-info" runat="server" style="border-color:#337ab7">
                             <div class="panel-body" id="body" >
                                 <div class="col-lg-6" >
                                    <div class="col-lg-4" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="lblApplicantName" style="line-height:30px" runat="server" Text="Label"><b>Applicant Name</b></asp:Label> 
                                    </div>
                                     </div>
                                    <div class="col-lg-8" style="padding-left:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   <asp:TextBox ID="txtApplicantName" Name="txtApplicantName" runat="server" CssClass="form-control input-sm" placeholder="Applicant Name" ></asp:TextBox>
                           </div>
                               </div>
                                    </div>
                                    <div class="col-lg-4" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label2" style="line-height:30px" runat="server" Text="Label"><b>Academic Year</b></asp:Label> 
                                    </div>
                                     </div>
                                    <div class="col-lg-8" style="padding-left:0px;">
                                 <div class="form-group">  
                                 <div class="input-group">
                                  <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>                                                           
                                    <asp:DropDownList ID="drpAcademicYear" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                                   </div>
                                    </div>
                           </div>
                                    <div class="col-lg-4" style="padding-left:0px; font-size:12px; ">
                                        <div class="form-group">
                                    <asp:Label ID="Label5" style="line-height:30px" runat="server" Text="Label"><b>Course Code</b></asp:Label> 
                                        </div>
                                         </div>
                                    <div class="col-lg-8" style="padding-left:0px;">
                                     <div class="form-group" >    
                                   <div class="input-group">
                                   <span class="input-group-addon"><span class="glyphicon glyphicon-globe"></span></span>
                                       <asp:DropDownList ID="drpCourse" CssClass="form-control input-sm" placeholder="Course" runat="server" >                                       
                                       </asp:DropDownList>
                               </div>
                                   </div>
                                        </div>
                                 </div>
                                     <div class="col-lg-6" style="" >
                                     <div class="col-lg-12" style="" >
                                    <asp:ImageMap ID="imgApplicant"  runat="server" Width="100px" Height="125px" ImageAlign="Right" ></asp:ImageMap> 
                                       
                                     </div>
                                         <div class="col-lg-12" style="" >
                                    <asp:label ID="ImageMap1"  runat="server" Width="100px" ></asp:label> 
                                       
                                     </div>
                                        
                                     </div>                            
                                                                 
                                    <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                        <div class="form-group">
                                    <asp:Label ID="Label6" style="line-height:30px" runat="server" Text="Label"><b>Prequalification</b></asp:Label> 
                                        </div>
                                         </div>
                                    <div class="col-lg-4" style="padding-left:0px;">
                                     <div class="form-group" >       
                                   <div class="input-group">
                                   <span class="input-group-addon"><span class="glyphicon glyphicon-copyright-mark"></span></span>
                                       <asp:DropDownList ID="drpPrequalification" CssClass="form-control input-sm" placeholder="Course" runat="server"></asp:DropDownList>
                               </div>
                                   </div>
                                        </div>
                                  <div class="col-lg-2" style="padding-right:0px; font-size:12px; ">
                                        <div class="form-group">
                                    <asp:Label ID="Label1" style="line-height:30px" runat="server" Text="Label"><b>Upload</b></asp:Label> 
                                        </div>
                                         </div>
                                    <div class="col-lg-4" style="padding-right:0px;">
                                     <div class="form-group" >       
                                   <div class="input-group">
                                   <span class="input-group-addon"><span class="glyphicon glyphicon-copyright-mark"></span></span>
                                       <asp:DropDownList ID="DropDownList1" CssClass="form-control input-sm" placeholder="Course" runat="server"></asp:DropDownList>
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
                                <asp:Label ID="Label4" style="line-height:30px" runat="server" Text="Label"><b>Citizenship</b></asp:Label> 
                                    </div>
                                     </div>
                                 <div class="col-lg-4" style="padding-right:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>
                                   <asp:DropDownList ID="drpCitizen" CssClass="form-control input-sm" placeholder="CitizenShip" runat="server"></asp:DropDownList>
                           </div>
                               </div>
                                    </div>                               

                                   <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label12" style="line-height:30px" runat="server" Text="Label"><b>Medium Instruction</b></asp:Label> 
                                    </div>
                                     </div>
                                   <div class="col-lg-4" style="padding-left:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   <asp:DropDownList ID="drpMedium" CssClass="form-control input-sm" placeholder="Course" runat="server"></asp:DropDownList>
                           </div>
                               </div>
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
                                   <asp:DropDownList ID="drpQuota" CssClass="form-control input-sm" placeholder="Course" runat="server" >
                                    </asp:DropDownList>
                                    </div>
                                     </div>
                                   </div>
                               
                                
                                <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label14" style="line-height:30px" runat="server" Text="Label"><b>Hostel Acommodation</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-left:0px;">
                                 <div class="form-group" > 
                                   <asp:CheckBox ID="chkHostelAcommodation" CssClass="form-control input-sm" runat="server" />
                           
                               </div>
                                    </div>
                                <div class="col-lg-2" style="padding-right:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label15" style="line-height:30px" runat="server" Text="Label"><b>Date of Birth</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-3" style="padding-right:0px;">
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
                                
                                  <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label9" style="line-height:30px" runat="server" Text="Label"><b>Name of previous inst.</b></asp:Label> 
                                    </div>
                                     </div>
                                  <div class="col-lg-4" style="padding-left:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   <asp:TextBox ID="txtPreviousInst" runat="server" CssClass="form-control input-sm" placeholder="Previous Institute"></asp:TextBox>
                           </div>
                               </div>
                                    </div>
                                  <div class="col-lg-2" style="padding-right: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="lblAge" Style="line-height: 30px" runat="server" Text="Label"><b>Age</b></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-1" style="padding-right: 5px;">
                                        <div class="form-group">
                                            <div class="input-group">
                                              
                                                <asp:Label ID="txtYear" CssClass="form-control input-sm" runat="server" Width="50px"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-1" style="padding-right: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="lbYear" Style="line-height: 30px" runat="server" Text="label"><b>Year</b></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-1" style="padding-right: 5px;">
                                        <div class="form-group">
                                            <div class="input-group">
                                                
                                                <asp:Label ID="txtmonths" CssClass="form-control input-sm" runat="server" Width="50px" ></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-1" style="padding-right: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="lbmonths" Style="line-height: 30px" runat="server"><b>Months</b></asp:Label>
                                        </div>
                                    </div>
                                
                                  <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label18" style="line-height:30px" runat="server" Text="Label"><b>Application Cost</b></asp:Label> 
                                    </div>
                                     </div>
                               <div class="col-lg-4" style="padding-left:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   <asp:Label ID="lblApplicationCost" CssClass="form-control input-sm" runat="server" ></asp:Label>
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
                                   <asp:DropDownList ID="drpModeOfPayment" CssClass="form-control input-sm" placeholder="Course" AutoPostBack="true" >
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
                                    </div>
                                <div class="col-lg-2" style="padding-right:0px; font-size:12px; ">
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
                           </div>










    <%--<div class="col-lg-6">
          <div class="col-lg-6">
         
            <div class="col-lg-12" style="padding-left:0px;">
                            <div class="form-group">        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-earphone"></span></span>
                               <asp:TextBox ID="TextBox1" runat="server" MaxLength="12" CssClass="form-control input-sm" placeholder="Contact Number"></asp:TextBox>
                                  
                               </div> 
                                   
                                </div>  
                               </div>
            <div class="col-lg-12" style="padding-left:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>
                                   <asp:DropDownList ID="DropDownList1" CssClass="form-control input-sm" placeholder="Course" runat="server"></asp:DropDownList>
                           </div>
                               </div>
                                    </div>
                                
            <div class="col-lg-12" style="padding-left:0px;">
                            <div class="form-group">        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-earphone"></span></span>
                               <asp:TextBox ID="txtContactNo" runat="server" MaxLength="12" CssClass="form-control input-sm" placeholder="Contact Number"></asp:TextBox>
                                  
                               </div> 
                                   
                                </div>  
                               </div>
            <div class="col-lg-12" style="padding-left:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>
                                   <asp:DropDownList ID="drpCourse" CssClass="form-control input-sm" placeholder="Course" runat="server"></asp:DropDownList>
                           </div>
                               </div>
                                    </div>
            </div>
                <div class="col-lg-6">
                       <div class="col-lg-12" style="padding-left :0px;" >
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>
                                  <asp:Image ID="img" runat="server" Height="170px" Width="100px" />
                           </div>
                               </div>
                                    </div>
                    </div>
            <div class="col-lg-6" style="padding-left:0px;">
                            <div class="form-group">        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-earphone"></span></span>
                               <asp:TextBox ID="TextBox2" runat="server" MaxLength="12" CssClass="form-control input-sm" placeholder="Contact Number"></asp:TextBox>
                                  
                               </div> 
                                   
                                </div>  
                               </div>
            <div class="col-lg-6" style="padding-left:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>
                                   <asp:DropDownList ID="DropDownList2" CssClass="form-control input-sm" placeholder="Course" runat="server"></asp:DropDownList>
                           </div>
                               </div>
                                    </div>

          <div class="col-lg-6" style="padding-left:0px;">
                            <div class="form-group">        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-earphone"></span></span>
                               <asp:TextBox ID="TextBox3" runat="server" MaxLength="12" CssClass="form-control input-sm" placeholder="Contact Number"></asp:TextBox>
                                  
                               </div> 
                                   
                                </div>  
                               </div>
            <div class="col-lg-6" style="padding-left:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>
                                   <asp:DropDownList ID="DropDownList3" CssClass="form-control input-sm" placeholder="Course" runat="server"></asp:DropDownList>
                           </div>
                               </div>
                                    </div>

          <div class="col-lg-6" style="padding-left:0px;">
                            <div class="form-group">        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-earphone"></span></span>
                               <asp:TextBox ID="TextBox4" runat="server" MaxLength="12" CssClass="form-control input-sm" placeholder="Contact Number"></asp:TextBox>
                                  
                               </div> 
                                   
                                </div>  
                               </div>
            <div class="col-lg-6" style="padding-left:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>
                                   <asp:DropDownList ID="DropDownList4" CssClass="form-control input-sm" placeholder="Course" runat="server"></asp:DropDownList>
                           </div>
                               </div>
                                    </div>
        </div>--%>
</asp:Content>

