<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="Enquiry.aspx.cs" Inherits="Enquiry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">   
   
    <script src="js/jquery-1.11.0.min.js" type="text/javascript"></script>
        <script src="js/bootstrap.min.js" type="text/javascript"></script
        <script src="js/wow.min.js" type="text/javascript"></script>
    <link href="bootstrap/css/bootstrap.css" rel="stylesheet" />

    <script type="text/javascript">
        function CalculateAge(DOB) {
            debugger
            if (DOB.value != '') {
                now = new Date()
                dob = DOB.value.split('-');
                if (dob.length === 3) {
                    born = new Date(dob[2], dob[1] * 1 - 1, dob[0]);
                    age = Math.floor((now.getTime() - born.getTime()) / (365.25 * 24 * 60 * 60 * 1000));
                    day = Math.floor(now.getDate());
                    day1 = Math.floor(born.getDate());
                    if (day1 < day) {
                        month = Math.floor((now.getMonth() - born.getMonth()));
                    }
                    else {
                        month = Math.floor((now.getMonth() - born.getMonth())) - 1;
                    }
                    if (month < 0) {
                        month = 12 + month;
                    }
                    if (age < 0) {
                    }
                    else {
                        $('[id$=txtYear]').val(age);
                        $('[id$=txtMonth]').val(month);
                        return false;
                    }
                }
            }
        }
        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select greater than current date!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
       $(document).ready(function () {
           //var MaxLength = 80;
           $('[id$=txtAddress]').keypress(function (e) {
               if ($(this).val().length >= 50) {
                   e.preventDefault();
               }
           });
           $('[id$=txtRemarks]').keypress(function (e) {
               if ($(this).val().length >= 80) {
                   e.preventDefault();
               }
           });
          
       });

    </script>
    <script type="text/javascript">
        function preventBackspace(e) {
            var evt = e || window.event;
            if (evt) {
                var keyCode = evt.charCode || evt.keyCode;
                if (keyCode === 8) {
                    if (evt.preventDefault) {
                        evt.preventDefault();
                    } else {
                        evt.returnValue = false;
                    }
                }
            }
        }
    </script>
    <style>
     .redBorder {
            border:1px solid red;
        }
        </style>
    <script type="text/javascript">
        $(document).ready(function () {
           
            $('[id$=btnSave]').click(function () {               
                $('[id$=ddlEnquiryNo]').addClass("redBorder");
                $('[id$=txtEnquiryDate]').css('border-color', 'red');
                $('[id$=txtApplicantName]').css('border-color', 'red');
                $('[id$=ddlCourse]').addClass("redBorder");
                $('[id$=txtContactNo]').css('border-color', 'red');
                $('[id$=ddlCity]').addClass("redBorder");

                var a = ''
                if ($('[id$=ddlEnquiryNo]').val() == '') { a = 'false'; } else $('[id$=ddlEnquiryNo]').removeClass("redBorder");
                if ($('[id$=txtEnquiryDate]').val() == '') { a = 'false'; } else $('[id$=txtEnquiryDate]').css('border-color', '');                
                if ($('[id$=txtApplicantName]').val() == '') { a = 'false'; } else $('[id$=txtApplicantName]').css('border-color', '');
                if ($('[id$=ddlCourse]').val() == '') { a = 'false'; } else $('[id$=ddlCourse]').removeClass("redBorder");
                if ($('[id$=txtContactNo]').val().length < 10) { a = 'false'; } else $('[id$=txtContactNo]').css('border-color', '');
                if ($('[id$=ddlCity]').val().length == '') { a = 'false'; } else $('[id$=ddlCity]').removeClass("redBorder");
                
                if(a=='false')
                {
                   return false;

                }
            });

        });
    </script>   
    <script type="text/javascript">
    function ShowProgress() {
        setTimeout(function () {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 200);
    }
    $('form').live("submit", function () {
        ShowProgress();
    });
</script>
 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table>
        <tr>
            <td>
                <%-- <div class="loading" align="center">
    Loading. Please wait.<br />
    <br />
    <img src="images/loader.gif" alt="" />
</div>--%>
    <div style="width: 100%">
        <div class="container" style="padding-top: inherit; padding-left: 50px;">
           
            <div class="row">
                <div class="col-lg-12" style="background-color: #ACE9FB" >
                   <%-- <div class="col-lg-10" >--%>
                    <div class="panel panel-info" style="border-color: #ACE9FB">
                        <div class="panel-heading" style="background-color: #ACE9FB">
                            <center>
                                <div class="panel-title" style="fit-position: center;">
                                    <b>
                                        <p style="color: white; font-size: 25px">Enquiry Form</p>
                                    </b>
                                </div>
                            </center>
                        </div>
                        <div >
                            <div class="col-lg-12" > <br />                               
                                <asp:Label ID="lblMsgmandatory" runat="server" Text=" * Highlighted fields  are mandatory " BackColor="#ffff97"> </asp:Label>     
                                <asp:HiddenField ID="hfCourseAge" runat="server" />
                                                         </div>
                            <asp:UpdatePanel runat="server" ID="updmain">
                                <ContentTemplate>
                            <%--New/Old Enquiry--%>
                            <div class="col-lg-3" style="padding-left: 5px;">
                                <div class="form-group">
                                   <asp:RadioButtonList ID="rblOldNew" runat="server" OnSelectedIndexChanged="rblOldNew_SelectedIndexChanged" RepeatDirection="Horizontal" AutoPostBack="true">
                                       <asp:ListItem Selected="True">Old Enquiry</asp:ListItem>
                                       <asp:ListItem>New Enquiry</asp:ListItem>
                                    </asp:RadioButtonList>
                                   
                                </div>
                            </div>
                            
                                    <div class="col-lg-3" style="padding-left: 5px;">
                                        <div class="form-group">
                                              <div class="input-group" id="dvEnquirylbl">
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-flash"></span></span>
                                                <asp:Label ID="lblEnquiryNo" CssClass="form-control input-sm" placeholder="Enquiry No" runat="server" Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlEnquiryNo" CssClass="form-control input-sm" placeholder="Enquiry No" runat="server" OnSelectedIndexChanged="ddlEnquiryNo_SelectedIndexChanged" AutoPostBack="true" BackColor="#ffff97"></asp:DropDownList>
                                                <asp:HiddenField ID="hfOldNew" runat="server"></asp:HiddenField>
                                            </div>
                                           
                                        </div>
                                    </div>
                                    <div class="col-lg-2" style="padding-right: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="lblEnquiryDate" Style="line-height: 30px" runat="server" Text="Label"><b>Enquiry Date</b></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-3" style="padding-right: 5px;">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>
                                                <asp:TextBox ID="txtEnquiryDate" CssClass="form-control input-sm" runat="server" onkeypress="return false;" onKeyDown="preventBackspace();" BackColor="#ffff97" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-1" style="padding-right: 5px;">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <%-- <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>--%>
                                                <asp:Image runat="server" ID="imgEnquiry" Width="25px" Height="30px" ImageUrl="~/images/calendar.png" /><%--Width="25px" Height="30px"  ImageUrl="~/images/Cal.png"  />--%>
                                                <asp:CalendarExtender ID="cleEnquiryDate" Format="dd MMM yyyy" runat="server"
                                                    CssClass="cal_Theme1" PopupButtonID="imgEnquiry" Enabled="true" TargetControlID="txtEnquiryDate">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                     <%-- Applicant Name and CourseName--%>
                                     <div class="col-lg-2" style="padding-left: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="Label8" Style="line-height: 30px" runat="server" Text="Label"><b>Applicant Name</b></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-4" style="padding-left: 5px;">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                                <asp:TextBox ID="txtApplicantName" runat="server" CssClass="form-control input-sm" placeholder="Applicant Name" MaxLength="50" BackColor="#ffff97"></asp:TextBox>
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
                                                <asp:DropDownList ID="ddlCourse" CssClass="form-control input-sm" placeholder="Course" runat="server" BackColor="#ffff97"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                     <%-- MobileNo  and Place--%>
                                     <div class="col-lg-2" style="padding-left: 5px; font-size: 12px;">
                                        <div class="form-group">
                                             <asp:Label ID="lblContactNo" Style="line-height: 30px" runat="server" Text="Label"><b>Contact No</b></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-4" style="padding-left: 5px;">
                                         <div class="col-lg-4" style="padding-left:0px;">
                            <div class="form-group">        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-earphone"></span></span>
                                <asp:Label ID="txtcontactPrefix" runat="server" Text="+91" CssClass="form-control input-sm"></asp:Label>                                   
                               </div> 
                                   
                                </div>  
                               </div>
                                         <div class="col-lg-8" style="padding-right:0px;">
                                            <div class="form-group"> 
                                              <asp:TextBox ID="txtContactNo" CssClass="form-control input-sm" runat="server" MaxLength="10" onkeypress="return NumberOnly()" BackColor="#ffff97"></asp:TextBox>
                                             
                                             </div>  
                                          </div>
                                       <%-- <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                                 <asp:TextBox ID="txtContactNo" CssClass="form-control input-sm" runat="server" MaxLength="12" onkeypress="return NumberOnly()" ></asp:TextBox>
                                                
                                            </div>
                                        </div>--%>
                                            
                                    </div>
                                    <div class="col-lg-2" style="padding-right: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="lblPlace" Style="line-height: 30px" runat="server" Text="Label"><b>Place</b></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-4" style="padding-right: 5px;">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                              <asp:DropDownList ID="ddlCity" runat="server"  CssClass="form-control input-sm" placeholder="Place" BackColor="#ffff97"></asp:DropDownList>
                                           <%--   <asp:TextBox ID="txtPlace" runat="server" CssClass="form-control input-sm" placeholder="Place Name" MaxLength="10" BackColor="#ffff97"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                    </div>

                                    <%-- Session  and DOB--%>
                                    <div class="col-lg-2" style="padding-left: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="lblSession" Style="line-height: 30px" runat="server" Text="Label"><b>Session</b></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-4" style="padding-left: 5px;">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-copyright-mark"></span></span>
                                                <asp:DropDownList ID="ddlSession" CssClass="form-control input-sm" placeholder="Session" runat="server" ></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>                                    
                                    <div class="col-lg-2" style="padding-right: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="Label15" Style="line-height: 30px" runat="server" Text="Label"><b>Date of Birth</b></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-3" style="padding-right: 5px;">
                                        <div class="form-group">
                                            <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   <asp:TextBox ID="txtDOB" CssClass="form-control input-sm" runat="server"  onchange='return CalculateAge(this)'   onKeyDown="preventBackspace();"
                                       AutoPostBack="false"   oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>

                                   <%-- <asp:Image runat="server" ID="imgCal" Width="25px" Height="30px" ImageUrl="~/images/calendar.png" />--%>
                                                <asp:CalendarExtender ID="cleDOB" Format="dd-MM-yyyy" runat="server" OnClientDateSelectionChanged="checkDate"
                                                    CssClass="cal_Theme1" Enabled="true" TargetControlID="txtDOB">
                                                </asp:CalendarExtender>
                           </div>
                                        </div>
                                    </div>


                                    <%--Religion/Age--%>
                                    <div class="col-lg-2" style="padding-left: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="lblReligion" Style="line-height: 30px" runat="server" Text="Label"><b>Religion</b></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-4" style="padding-left: 5px;">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-globe"></span></span>
                                                 <asp:DropDownList ID="ddlReligion" CssClass="form-control input-sm" runat="server" OnSelectedIndexChanged="ddlReligion_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
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
                                                <%--<span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>--%>
                                                <asp:TextBox ID="txtYear" onkeypress="return false" onKeyDown="preventBackspace();" CssClass="form-control input-sm"  runat="server" Width="50px"></asp:TextBox>
                                                
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
                                                <%--<span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>--%>
                                                <asp:TextBox ID="txtMonth" onkeypress="return false" onKeyDown="preventBackspace();" CssClass="form-control input-sm" runat="server"  Width="50px" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-1" style="padding-right: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="lbmonths" Style="line-height: 30px" runat="server"><b>Months</b></asp:Label>
                                        </div>
                                    </div>

                                                                       
                                    <%--Category/Subreligion--%>
                                     <div class="col-lg-2" style="padding-left: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="lblCategory" Style="line-height: 30px" runat="server" Text="Label"><b>Category</b></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-4" style="padding-left: 5px;">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>
                                                <asp:DropDownList ID="ddlCategory" CssClass="form-control input-sm" placeholder="Course" runat="server" ></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-2" style="padding-right: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="lblSubReligion" Style="line-height: 30px" runat="server" Text="Label"><b>Sub Religion</b></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-4" style="padding-right: 5px;">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-globe"></span></span>
                                                 <asp:DropDownList ID="ddlSubReligion" CssClass="form-control input-sm" runat="server" ></asp:DropDownList>
                                                <asp:Label ID="lblSubReligionNotRequired" Style="line-height: 30px" runat="server" CssClass="form-control input-sm" Visible="false" ForeColor="Red" Text="Not Required">
                                                    </asp:Label>
                                            </div>
                                        </div>
                                    </div>


                                    <%--Gender/Nationality--%>
                                    <div class="col-lg-2" style="padding-left: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="Label13" Style="line-height: 30px" runat="server" Text="Label"><b>Gender</b></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-4" style="padding-left: 5px;">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                                <asp:DropDownList ID="ddlGender" CssClass="form-control input-sm" placeholder="Course" runat="server" ></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-2" style="padding-right: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="Label5" Style="line-height: 30px" runat="server" Text="Label"><b>Nationality</b></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-4" style="padding-right: 5px;">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-globe"></span></span>
                                                <asp:DropDownList ID="ddlNationality" CssClass="form-control input-sm" placeholder="Course" runat="server" >
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <%--FeeType/Enquirer Name--%>

                                  
                                  <div class="col-lg-2" style="padding-left: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="lblFeeType" Style="line-height: 30px" runat="server" Text="Label"><b>Fee Type</b></asp:Label>
                                        </div>
                                    </div>
                                  <div class="col-lg-4" style="padding-left: 5px;">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-copyright-mark"></span></span>
                                                <asp:DropDownList ID="ddlFeeType" CssClass="form-control input-sm" placeholder="Fee Type" runat="server" ></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                  <div class="col-lg-2" style="padding-right: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="Label18" Style="line-height: 30px" runat="server" Text="Label"><b>Enquirer Name</b></asp:Label>
                                        </div>
                                    </div>
                                  <div class="col-lg-4" style="padding-right: 5px;">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                                <asp:TextBox ID="txtEnquirerName" CssClass="form-control input-sm" runat="server" Text="Enquirer Name" MaxLength="50" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <%--Father's Name/Applicant RelationShip--%>

                                    <div class="col-lg-2" style="padding-left: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="Label21" Style="line-height: 30px" runat="server" Text="Label"><b>Father's Name</b></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-4" style="padding-left: 5px;">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                                <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control input-sm" placeholder="Father's Name" MaxLength="50" > </asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-2" style="padding-right: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="Label14" Style="line-height: 30px" runat="server" Text="Label"><b>Applicant RelationShip</b></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-4" style="padding-right: 5px;">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                                <asp:TextBox ID="txtApplicantRelationShip" CssClass="form-control input-sm" runat="server" Text="Applicant RelationShip" MaxLength="50" ></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    
                                    <%--Mother's Name/Prequalification--%>                                   
                                    <div class="col-lg-2" style="padding-left: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="Label23" Style="line-height: 30px" runat="server" Text="Label"><b>Mother's Name</b></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-4" style="padding-left: 5px;">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                                <asp:TextBox ID="txtMotherName" runat="server" CssClass="form-control input-sm" placeholder="Mother's Name" MaxLength="50" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-lg-2" style="padding-right: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="Label6" Style="line-height: 30px" runat="server" Text="Label"><b>Prequalification</b></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-4" style="padding-right: 5px;">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-copyright-mark"></span></span>
                                                <asp:DropDownList ID="ddlPrequalification" CssClass="form-control input-sm" placeholder="Course" runat="server" ></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <%--Enquiry Type/Enquiry Source--%>
                                   
                                    <div class="col-lg-2" style="padding-left: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="Label16" Style="line-height: 30px" runat="server" Text="Label"><b>Enquiry Type</b></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-4" style="padding-left: 5px;">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                                <asp:DropDownList ID="ddlEnquiryType" CssClass="form-control input-sm" placeholder="Enquiry Type" runat="server" ></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-lg-2" style="padding-right: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" Style="line-height: 30px" runat="server" Text="Label"><b>Enquiry Source</b></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-4" style="padding-right: 5px;">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-copyright-mark"></span></span>
                                                <asp:DropDownList ID="ddlEnquirySource" CssClass="form-control input-sm" placeholder="Enquiry Source" runat="server" ></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <%--Name of the Media/Address--%>                                   
                                    <div class="col-lg-2" style="padding-left: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="Label2" Style="line-height: 30px" runat="server" Text="Label"><b>Name Of Media</b></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-4" style="padding-left: 5px;">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                                <asp:DropDownList ID="ddlNameOfMedia" CssClass="form-control input-sm" placeholder="Name of Media " runat="server" ></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                      <div class="col-lg-2" style="padding-right: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="lblAddress" Style="line-height: 30px" runat="server" Text="Label"><b>Address </b></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-4" style="padding-right: 5px;">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control input-sm" placeholder="Address" TextMode="MultiLine" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- Remarks --%>
                                    <div class="col-lg-2" style="padding-left: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="lblREmarks" Style="line-height: 30px" runat="server" Text="Label"><b>Remarks</b></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-4" style="padding-left: 5px;">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control input-sm" placeholder="Remarks" TextMode="MultiLine" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>                                  
                                    
                                    <div class="col-lg-6" style="padding-right: 5px; font-size: 12px;">
                                        <div class="form-group">
                                            <asp:Label ID="Label7" Style="line-height: 30px" runat="server" Text="Label"><b></b></asp:Label>
                                        </div>
                                    </div>

                                     
                                     </ContentTemplate>

                            </asp:UpdatePanel>                                   
                             <table width="100%">
                                       <tr>
                                           <td>
                                               <div class="panel-footer">
                                        <center>

                                            <asp:Button ID="btnSave" runat="server" ValidationGroup="g1" Text="Submit" CssClass="btn-lg btn-primary btn-block" Height="43px" Width="93px" OnClick="btnSave_Click" />

                                        </center>
                                    </div>

                                           </td>
                                       </tr>
                                   </table>
                               
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

