<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="StudentDetailsView1.aspx.cs" Inherits="Student_StudentDetailsView1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width = device-width, initial-scale = 0, minimum-scale = .3, maximum-scale = .3, user-scalable = no" />
<link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
    <!-- Font Awesome -->
    <link href="css/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
   
    <!-- Theme style -->
    <link rel="stylesheet" href="dist/css/AdminLTE.min.css">
    <link rel="stylesheet" href="dist/css/skins/_all-skins.min.css">
    <link href="css/CloudeStyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="dist/css/style.css" rel="stylesheet" />
    <script src="js/CloudeJScript.js" type="text/javascript"></script>
    <link href="css/gridstyle.css" rel="stylesheet" />
        <style type="text/css">
            #confirmModal.modal-dialog.modalPopup {
                width: 100%;
            }

            table thead tr th:first-child, .table > tbody > tr > th:first-child {
                border-left: 1px solid #60594f;
                padding: 5px 8px;
            }

            .modal-dialog.modalPopup {
                -webkit-transform: translate(0,0);
                -ms-transform: translate(0,0);
                -o-transform: translate(0,0);
                transform: translate(0,0);
                position: absolute;
                left: 50%;
                top: 25%;
                transform: translate(-50%,-50%) !important;
                margin: 0;
                border: none;
                width: 700px;
                height: 270px;
            }

            .modalPopup .modal-header {
                padding: 8px 15px;
                border-bottom: 1px solid #e5e5e5;
                background-color: #2FBDF1;
                -webkit-border-top-left-radius: 4px;
                -webkit-border-top-right-radius: 4px;
                -moz-border-radius-topleft: 4px;
                -moz-border-radius-topright: 4px;
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
            }

                .modalPopup .modal-header b {
                    color: #fff;
                }

            #ContentPlaceHolder1_updatepnl table td {
                padding: 5px 0px;
            }

            input[type=checkbox] {
                /* Double-sized Checkboxes */
                -ms-transform: scale(1.5); /* IE */
                -moz-transform: scale(1.5); /* FF */
                -webkit-transform: scale(1.5); /* Safari and Chrome */
                -o-transform: scale(1.5); /* Opera */
                transform: scale(1.5);
                padding: 10px;
            }

            /* Might want to wrap a span around your checkbox text */
            .checkboxtext {
                /* Checkbox text */
                font-size: 110%;
                display: inline;
            }

            .custom-header {
                background: linear-gradient(90deg, #ff9933, #ffcc80);
                color: #5a2d0c;
                background-color: #ffe5b4; /* light bhagwa */
               
                font-size: 20px;
                font-weight: 600;
            }

            .custom-body {
                max-height: 400px;
                overflow: auto;
                font-size: 16px;
            }

            .error-item {
                border-bottom: 1px solid #ddd;
                padding: 12px 0;
            }

            .error-text {
                color: #d9534f;
                font-size: 16px;
                line-height: 1.5;
            }

            .modal-title {
                font-size: 20px;
            }
        </style>





    <script type="text/javascript" src="https://www.google.com/jsapi">
    </script>
  <script src="https://www.google.com/jsapi" type="text/javascript">  
</script>  
<script type="text/javascript">
    google.load("elements", "1", {
        packages: "transliteration"
    });

    function onLoad() {
        var options = {
            sourceLanguage: google.elements.transliteration.LanguageCode.ENGLISH,
            destinationLanguage: [google.elements.transliteration.LanguageCode.HINDI],
            shortcutKey: 'ctrl+g',
            transliterationEnabled: true
        };

        var control = new google.elements.transliteration.TransliterationControl(options);
        control.makeTransliteratable(['TxtSH']);
        control.makeTransliteratable(['TxtFH']);
        control.makeTransliteratable(['TxtMH']);

    }
    google.setOnLoadCallback(onLoad);
</script>
<%--     <script>
         function alphanumeric(inputtxt) {
             var letterNumber = /^[0-9a-zA-Z-+()!@#$%^&*/|]+$/;
             if ((inputtxt.value.match(letterNumber))) {
                 alert("Enter only Hindi");
                 return false;
             }
             else {


                 return true;
             }
         }
    </script>--%>
    
    <script type="text/javascript">
        $(document).ready(function () {
            $('[id$=txtAddress]').keypress(function (e) {
                if ($(this).val().length >= 50) {
                    e.preventDefault();
                }
            });
        });
    </script>
    <script type="text/javascript">
        function callFeedbackMessage(inputType, inputText) {

            if (inputType == 'Error') {
                alertify.error(inputText);
                return false;
            }
            else if (inputType == 'Success') {
                //alertify.confirm().set('overflow', false);
                alertify.success("Update Successfully");
                return false;
            }
            else {
                alertify.log(inputText, "", 10000);
                return false;
            }
        }
    </script>

    <%--<script>
        function CloseModalConfirm() {
            $('#confirmModal').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        };
    </script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

               <asp:Panel ID="Panel2" runat="server" >
        <div id="confirmModal5" class="modal fade confirm-modal"  role="dialog">
            <div class="modal-dialog modalPopup" style="width:540px">
                <div class="modal-content" style="width:543px">
                   
                    <div class="clearfix" style="margin-bottom:10px;margin-top:10px;margin-left:20px">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                
                                 <table >
                                     
                                     <tr>
                                         <td style="height:45px">
                                             Student No_ 
                                         </td>
                                         <td style="width:5px">

                                         </td>
                                         <td>
                                             <asp:TextBox ID="txtstno" Enabled="false" Width="250px" runat="server"></asp:TextBox>
                                         </td>
                                         
                                     </tr>
                                     <tr>
                                                                                   <td>
    
 </td>
 <td style="width:5px">

 </td>
 <td>
    
 </td>
                                     </tr>
                                      <tr>
     <td style="height:45px">
         Student’s Name (As per 10th certificate)
     </td>
     <td style="width:3px">

     </td>
     <td style="margin-right:10px">
         <asp:TextBox ID="txtSTName10" Width="250px" Enabled="false" runat="server" ></asp:TextBox>
     </td>
 </tr>

                                     <tr>
    <td style="height:45px">
        Student’s Name (As per Aadhaar)
    </td>
    <td style="width:3px">

    </td>
    <td style="margin-right:10px">
        <asp:TextBox ID="txtSTNameA"  Width="250px" runat="server" ></asp:TextBox>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="**" ValidationGroup="g1" ForeColor="Red" ControlToValidate="txtSTNameA"></asp:RequiredFieldValidator>
    </td>
</tr>

                                     <tr>
    <td style="height:45px">
        Date of birth (As per 10th certificate)
    </td>
    <td style="width:3px">

    </td>
    <td style="margin-right:10px">
        <asp:TextBox ID="txtDOB10" Enabled="false" Width="250px" runat="server" ></asp:TextBox>
    </td>
</tr>

                                     <tr>
    <td style="height:45px">
        ⁠Date of birth (As per Aadhaar)
    </td>
    <td style="width:3px">

    </td>
    <td style="margin-right:10px">
        <asp:TextBox ID="txtDOBA" Width="250px" runat="server" onkeypress="return false" onKeyDown="preventBackspace();"
                                            oncopy="return false;" onpaste="return false;" autocomplete="off" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="**" ValidationGroup="g1" ForeColor="Red" ControlToValidate="txtDOBA"></asp:RequiredFieldValidator>
         <asp:CalendarExtender ID="CalendarExtender2" Format="dd MMM yyyy" runat="server"
     CssClass="cal_Theme1"  Enabled="true" TargetControlID="txtDOBA">
</asp:CalendarExtender>
    </td>
</tr>

                                     <tr>
    <td style="height:45px">
        Mobile number (linked with Aadhaar)
    </td>
    <td style="width:3px">

    </td>
    <td style="margin-right:10px">
        <asp:TextBox ID="txtMobileA" Width="250px" onkeypress="return isNumber(event)" MaxLength="10" runat="server" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="**" ValidationGroup="g1" ForeColor="Red" ControlToValidate="txtMobileA"></asp:RequiredFieldValidator>
    </td>
</tr>

                                     <tr>
    <td style="height:45px">
       Aadhar number
    </td>
    <td style="width:3px">

    </td>
    <td style="margin-right:10px">
        <asp:TextBox ID="txtAdhar" Width="250px" onkeypress="return isNumber(event)" MaxLength="12" runat="server" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="**" ValidationGroup="g1" ForeColor="Red" ControlToValidate="txtAdhar"></asp:RequiredFieldValidator>
     <asp:RegularExpressionValidator ID="rgxAadhaar" runat="server" ControlToValidate="txtAdhar"
 ValidationExpression="([0-9]{4}[0-9]{4}[0-9]{4}$)|([0-9]{4}\s[0-9]{4}\s[0-9]{4}$)|([0-9]{4}-[0-9]{4}-[0-9]{4}$)" ValidationGroup="g1"
 ErrorMessage="Invalid" ForeColor="Red"></asp:RegularExpressionValidator>

</td>
</tr>
                                    <tr>
                                        <td colspan="3" style="height:45px">
                                       <b style="color:red">The blank field must be filled in to proceed further.</b> 
                                        </td>
                                         <td style="width:3px">

</td>
                                    </tr>
                                     <tr>
    <td>
       
    </td>
    <td style="width:3px">

    </td>
    <td style="margin-right:10px;text-align:right">
        <asp:Button ID="btnSave" Width="150px" Text="SAVE"  ValidationGroup="g1" OnClick="btnSave_Click" runat="server" ></asp:Button>
    </td>
</tr>








                                 </table>
                            </ContentTemplate>

                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>



   <asp:Panel ID="Panel3" runat="server">
    
 <div id="confirmModal10" class="modal fade confirm-modal" role="dialog">
    <div class="modal-dialog modalPopup">
        <div>

            <!-- Header -->
            <div class="modal-header custom-header">
                <h5 class="modal-title">Exam Form Message Details</h5>                
            </div>

            <!-- Body -->
            <div class="modal-body custom-body">
                
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>

                        <!-- Error List -->
                        <asp:Repeater ID="rptErrors" runat="server">
                            <ItemTemplate>
                                <div class="error-item">
                                    <ul>
                                        <li class="error-text">
                                            <b>Error:</b> <%# Eval("ErrorMessage") %>
                                        </li>
                                    </ul>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                        <!-- Button -->
                        <div class="text-end mt-3">
                            <asp:Button 
                                ID="btnMarkAllRead" 
                                runat="server" 
                                Text="Mark as Read" 
                                CssClass="btn btn-success btn-lg"
                                OnClick="btnMarkAllRead_Click" />
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>

        </div>
    </div>
</div>
   </asp:Panel>






       <asp:Panel ID="Panel1" runat="server" >
        <div id="confirmModal1" class="modal fade confirm-modal" role="dialog">
            <div class="modal-dialog modalPopup">
                <div class="modal-content">
                    <div style="margin-bottom:-5px;margin-right:3px">
                        <button type="button"  class="close"  data-dismiss="modal" style="color:black;opacity:initial" >X</button>
                        
                    </div>
                    <div class="clearfix">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                 <div class="clearfix mb-5">
                                    <table style="width:100%">
                                          <tr>
                                         <td style="height:40px">

                                         </td>
                                     </tr>
                                        <tr style="font-weight:100;font-weight: bold;">
                                           
                                            <td style="width:100% ;text-align:left;" colspan="3" >

                                                <label style="font:bold;font-size:40px;font-family: Georgia, serif;">
  <span style="display: inline-block;margin: 8px 0;font:bold;padding-left:48px;opacity:2; ">Stay Connected!

                                                     
     </span>
</label>  
                                           </td>
                                        </tr>
                                           <tr>
                                         <td style="height:20px">

                                         </td>
                                     </tr>

                                          <tr style="font-weight:100;font-weight: bold;">
                                            <td style="width:100% ;text-align:center;opacity:.8;text-align:left" colspan="3" >

                                                <label style="font-size:larger;font: 25px/1 Verdana,Freesans,sans-serif;">
  <span style="display: inline-block;margin: 8px 0;margin-left:50px ">Follow TMU on all our social channels to get the latest updates,</span><br />
</label>

                                           </td>
                                        </tr>
                                         <tr style="font-weight:100;font-weight: bold;padding-top:40px">
                                            <td style="width:100% ;text-align:center;opacity:.8;text-align:left" colspan="3" >

                                                <label style="font-size:larger;font: 25px/1 Verdana,Freesans,sans-serif;">
  <span style="display: inline-block;margin: 8px 0;margin-left:50px;margin-top:-3px "> announcements, and exciting content!</span><br />
</label>

                                           </td>
                                        </tr>
                                        
                                     
                                        <tr>
                                         <td style="height:40px">

                                         </td>
                                     </tr>

                                        <tr style="width:100%;font-weight:100;font-weight: bold;">
                                            <td style="width:33% ;text-align:right ">  <a href="https://www.facebook.com/tmumbd/" target="_blank">
                                            <asp:Image ID="Image5" runat="server" ImageUrl="~/images/facebook.png" Width="200px" Height="250px" />
                                        </a>
                                            </td>
                                            <td style="width:33% ;text-align:center ">
                                                <a href="https://bit.ly/4ddvTyK" target="_blank">
                                            <asp:Image ID="Image7" runat="server" ImageUrl="~/images/insta_logo1.png" Width="205px" Height="260px" />


                                        </a>
                                            </td>
                                            <td style="width:33% ;text-align:left "><a href="https://bit.ly/4b5DdL9" target="_blank">
                                            <asp:Image ID="Image11" runat="server" ImageUrl="~/images/whatsapp.png" Width="200px" Height="245px" />


                                        </a>
                                            </td>
                                        </tr>
                                     <tr>
                                         <td style="height:40px">

                                         </td>
                                     </tr>
                                        <tr style="width:100%">
                                            <td style="width:33%;text-align:right ">
                                              <a href="https://bit.ly/4d3FOaa" target="_blank">
                                            <asp:Image ID="Image8" runat="server" ImageUrl="~/images/yt_logo 1.png" Width="200px" Height="250px"  />


                                        </a>
                                            </td>
                                            <td style="width:33% ;text-align:center ">
                                              <a href="https://bit.ly/3WdTNEl" target="_blank">
                                            <asp:Image ID="lnkYout" runat="server" ImageUrl="~/images/insta_logo2.png" Width="200px" Height="250px" />


                                        </a>
                                            </td>
                                            <td style="width:33% ;text-align:left;">
                                               <a href="https://bit.ly/3UfA8kM" target="_blank">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/Linkdn.png" Width="200px" Height="250px" />


                                        </a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height:30px">

                                            </td>
                                        </tr>
                                     

                                      
                                      

                                    </table>
                                </div>
                            </ContentTemplate>

                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>



    <asp:Panel ID="pnlPopup" runat="server">
        <div id="confirmModal" class="modal fade confirm-modal" role="dialog">
            <div class="modal-dialog modalPopup">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <b>
                            <center>Notification</center>
                        </b>
                    </div>
                    <div class="clearfix">
                        <asp:UpdatePanel ID="updatepnl" runat="server">
                            <ContentTemplate>
                                 <div class="clearfix mb-5">
                                    <table style="width:100%">
                                        <tr style="width:100%;font-weight:100;border-bottom: 1px solid #abb8bd;background-color: #9dc2c3;font-weight: bold;">
                                            <td style="width:33% ;text-align:center "><asp:Label ID="Label2" runat="server" Text="S.No" ></asp:Label>
                                            </td>
                                            <td style="width:33% ;text-align:center "><asp:Label ID="Label5" runat="server" Text="Inbox" ></asp:Label>
                                            </td>
                                            <td style="width:33% ;text-align:center "><asp:Label ID="Label6" runat="server" Text="Count" ></asp:Label>
                                            </td>
                                        </tr>
                                       
                                        <tr style="width:100%">
                                            <td style="width:33%;text-align:center ">
                                                <asp:Label ID="Sno" runat="server" Text="1"></asp:Label>
                                            </td>
                                            <td style="width:33% ;text-align:center ">
                                                <asp:Label ID="LblInbox" runat="server" Text="Placement"></asp:Label>
                                            </td>
                                            <td style="width:33% ;text-align:center;">
                                                <asp:LinkButton ID="Lblcount" OnClick="Lblcount_Click" runat="server"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>

                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Profile" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>

    <fieldset class="boxBodyHeader">
    </fieldset>
    <fieldset class="boxBodyInner">
        <table cellpadding="0px" cellspacing="0px">
            <tr>
                <td colspan="15" style="height: 10px">
                    <fieldset class="boxBodyHeader">
                        <asp:Label ID="Label3" runat="server"
                            Text="Personal Information" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                    </fieldset>
                    <br />
                    <fieldset class="boxBodyInner">
                        <table cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td>
                                    <table cellpadding="0px" cellspacing="0px">
                                          <tr>
                                            <td colspan="11" style="height: 10px;text-align:center">&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                                
                                                <a href=" https://digilocker.meripehchaan.gov.in/" id="A2" runat="server" target="_blank" visible="true" style="color:blue; font-size:large"><b>Click here to generate ABC ID</b></a> 
                                                </td>
                                        </tr>
                                        
                                        <tr>
                                            <td>
                                                <label>Enrolment No:</label></td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtRollNo" runat="server" Width="260px" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <label>ABC ID </label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtNADID" runat="server" MaxLength="12"  Width="260px"></asp:TextBox></td>
                                             <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtNADID" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                         
                                             <td  style="width: 10px;"> </td>
                                            
                                            <td>
                                                <label style="width: 100px; float: right;">Mentor:</label></td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtMentor" runat="server" Width="260px" Font-Underline="true" Enabled="false"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td colspan="11" style="height: 10px"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Student No. </label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtStudentNo" runat="server" Width="260px" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <label>Name </label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtName" runat="server" Enabled="False" Width="260px"></asp:TextBox></td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <label>Date of Birth </label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtDOB" runat="server" Enabled="False" Width="260px"></asp:TextBox></td>

                                        </tr>
                                        <tr>
                                            <td colspan="11" style="height: 10px"></td>
                                        </tr>

                                        <tr>
                                            <td>Course<label>  </label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtCourse" runat="server" Enabled="False" Width="260px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <label>Section</label></td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtSection" runat="server" Enabled="False" Width="260px"></asp:TextBox></td>
                                            <td style="width: 10px"></td>
                                            <td>Academic Year<label>  </label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtAcademicYear" runat="server" Width="260px" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="11" style="height: 10px"></td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <label>Category  </label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtCategory" runat="server" Enabled="False" Width="260px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <label>Father Name</label></td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtFatherName" runat="server" Enabled="False" Width="260px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Mother Name<label>  </label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtMotherName" runat="server" Enabled="False" Width="260px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="11" style="height: 10px"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Admitted Year  </label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtCurrentYear" runat="server" Enabled="False" Width="260px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <label>Semester/Year</label></td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtSemester" runat="server" Enabled="False" Width="260px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Batch<label>  </label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtBatch" runat="server" Enabled="False" Width="260px"></asp:TextBox>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td colspan="11" style="height: 10px"></td>
                                        </tr>
                                   <tr>
                                            <td>
                                                <label>छात्र/छात्रा का नाम</label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                      <div id="DSH" runat="server" visible="false">          
                                                
                                      
                                   <div class="Google-transliterate-Way2blogging">
                                    
                                    <input type="text" name="SHname" id="TxtSH" placeholder="Use Google Translate" "width:260px; maxlength="35"/>                               
                                       <asp:Label ID="hmsg" runat="server"></asp:Label>
                                 </div>
                                 </div>
                                 <asp:TextBox ID="txtStudentHindi" runat="server" Enabled="False" MaxLength="35"    Width="260px"></asp:TextBox>
                                        
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <label>पिता का नाम</label></td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <div id="DFH" runat="server" visible="false">          
                                      
                                        <div class="Google-transliterate-Way2blogging">
                                    <input type="text" name="FHname" id="TxtFH" placeholder="Use Google Translate" "width:260px; maxlength="35"/>
                                          </div>
                                                    </div>
                                       <asp:TextBox ID="txtFatherHindi" runat="server" Enabled="False"  MaxLength="35"  Width="260px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>माता का नाम<label>  </label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td> 
                                     <div id="DMH" runat="server" visible="false">          
                                      
                                        <div class="Google-transliterate-Way2blogging">
                                    
                                    <input type="text" name="MHname" id="TxtMH" placeholder="Use Google Translate" "width:260px; maxlength="35"/>
                                
<%--                                             &nbsp;&nbsp; &nbsp;<input type="submit" name="submit"  value="Submit" onclick="alphanumeric(document.form1.SHname)" />--%>
                                 </div> </div>
                                                <asp:TextBox ID="txtMotherHindi" runat="server"   Enabled="False" MaxLength="35"  Width="260px"></asp:TextBox>
                                            </td>
                                        </tr>                                       

                                        <tr>
                                            <td colspan="11" style="height: 10px">&nbsp;

                                              
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>

                    </fieldset>
                </td>
            </tr>

            <tr>
                <td colspan="15" style="height: 10px">
                    <fieldset class="boxBodyHeader">
                        <asp:Label ID="Label4" runat="server"
                            Text="Contact Information" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                    </fieldset>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <fieldset class="boxBodyInner">
                                <table cellpadding="0px" cellspacing="0px">

                                    <tr>
                                        <td colspan="15">

                                            <table cellpadding="0px" cellspacing="0px">

                                                <tr>
                                                    <td>
                                                        <label>E-Mail ID </label>
                                                    </td>
                                                    <td style="width: 30px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtEmailID" runat="server" Width="260px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label>Mobile No</label></td>
                                                    <td style="width: 30px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMobileNo" runat="server" Width="260px" MaxLength="13"></asp:TextBox></td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label>City </label>
                                                    </td>
                                                    <td style="width: 80px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtCity" runat="server" Width="260px" MaxLength="20"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                    <td>
                                                        <asp:RegularExpressionValidator ID="regEmail" Display="Dynamic" ControlToValidate="txtEmailID" Text="Please fill the valid Email address!" ForeColor="Red" ValidationGroup="g1"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server" />
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                    <td>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtMobileNo" FilterType="Numbers, Custom"></asp:FilteredTextBoxExtender>
                                                        <td style="width: 10px"></td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="11" style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>Address </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td colspan="11">
                                                        <asp:TextBox ID="txtAddress" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="11" style="height: 20px"></td>
                                                </tr>

                                                <%--<tr> <td colspan="11" align="right" >  
                      <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btnLogin" />
                      <asp:Label ID="lblHRUserId" runat="server" Visible="False"></asp:Label>
                      </td></tr>--%>
                                            </table>

                                        </td>
                                    </tr>
                                </table>

                                <div class="pull-right">
                                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn-sm btn-primary btn-block"  Height="30px" Width="90px" OnClick="btnUpdate_Click"   Text="UPDATE" />
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
                                <asp:Panel ID="PnlMain" runat="server">
                                      <asp:Label ID="Lblpopup" runat="server" Text="Dear student kindly check your detail. If correct then update."></asp:Label>
                                                                        <div class="modal-footer">
                                        <asp:Button ID="BtnYes" runat="server" OnClick="BtnYes_Click" Text="Update"
                                            UseSubmitBehavior="false" data-dismiss="modal" class="btn btn-success" />
                                        <button type="button" class="btn btn-danger" data-dismiss="modal">Cancel</button>
                                    </div>


                                </asp:Panel>
                            </div>
                        </div>
                        </div></div></div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>

