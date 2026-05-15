<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="~/Enquiry/Enquiry_Phone.aspx.cs" Inherits="Enquiry_Phone" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="js/jquery-1.11.0.min.js" type="text/javascript"></script>
        <script src="js/bootstrap.min.js" type="text/javascript"></script
        <script src="js/wow.min.js" type="text/javascript"></script>
    <link href="bootstrap/css/bootstrap.css" rel="stylesheet" />

    <%--<link href="bootstrap/css/bootstrap.css" rel="stylesheet" />
    <script src="bootstrap/js/bootstrap.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <script src="Scripts/alertify/alertify.js"></script>
    <script src="Scripts/alertify/alertify.min.js"></script>
    <link href="packages/alertify.0.3.11/content/Content/alertify/alertify.core.css" rel="stylesheet" />
    <link href="packages/alertify.0.3.11/content/Content/alertify/alertify.default.css" rel="stylesheet" />--%>
     
     <style type="text/css">
        .red-border{
            border: 1px solid red;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            var MaxLength = 80;
            $('[id$=txtRemarks]').keypress(function (e) {
                if ($(this).val().length >= MaxLength) {
                    e.preventDefault();
                }
            });
            $('[id$=txtAddress]').keypress(function (e) {
                if ($(this).val().length >= 50) {
                    e.preventDefault();
                }
            });
        });
       </script>
        <script type="text/javascript">
        $(document).ready(function () {
            $('[id$=Button1]').click(function () {
                $('[id$=txtName]').css('border-color','red');               
                $('[id$=txtContactNo]').css('border-color', 'red');
                $('[id$=ddlCity]').css('border-color', 'red');
                $('[id$=drpCourse]').addClass("red-border");             

                var a = '';
                if ($('[id$=txtName]').val() == '') { a = 'false'; } else $('[id$=txtName]').css('border-color', '');
                if ($('[id$=txtContactNo]').val().length< 10) { a = "false" } else $('[id$=txtContactNo]').css('border-color', '');
                if ($('[id$=ddlCity]').val() == '') { a = "false" } else $('[id$=ddlCity]').css('border-color', '');
                if ($('[id$=drpCourse]').val() == '') { a = "false" } else $('[id$=drpCourse]').removeClass("red-border");                
                if (a == "false") {
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
       <div class="panel panel-info" style="border-color:#ACE9FB">
                    <div class="panel-heading" style="background-color:#ACE9FB">
                        <div class="panel-title" style="fit-position:center;"><center><b><p style="color:white;font-size:25px"> Phone Enquiry</p></b></center></div> 
                    </div>                                             
                    <div class="panel-body">
                                 <div > 
                               <asp:Label ID="lblMandatory" runat="server" Text=" * Highlighted fields  are mandatory " BackColor="#ffff97"> </asp:Label>   
                                 </div>
                                <div class="clearfix">.<asp:Label ID="lblMandatoryMsg" runat="server" text="" ForeColor="Red" Font-Bold="true"> </asp:Label>   </div>
                                <div class="col-lg-6" style="padding-left: 0px;">
                                    <div class="form-group">
                                        <div class="input-group" style="border-color: red">
                                            <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control input-sm" placeholder="Name" BackColor="#ffff97"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-6" style="padding-right: 0px;">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-addon"><span class="glyphicon glyphicon-menu-hamburger"></span></span>
                                            <asp:DropDownList ID="drpCourse" CssClass="form-control input-sm" placeholder="Course" runat="server" BackColor="#ffff97"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
   <div class="col-lg-6" style="padding-left:0px;">
       
       <div class="col-lg-4" style="padding-left:0px;">
                            <div class="form-group">        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-earphone"></span></span>
                                <asp:Label ID="txtcontactPrefix" runat="server" Text="+91" CssClass="form-control input-sm"></asp:Label>                                   
                               </div> 
                                   
                                </div>  
                               </div>
        <div class="col-lg-8" style="padding-left:0px;">
                            <div class="form-group"> 
                              <asp:TextBox ID="txtContactNo" runat="server" MaxLength="10" CssClass="form-control input-sm" placeholder="10 Digit Contact Number" BackColor="#ffff97"></asp:TextBox>
                                   <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtContactNo" FilterType="Numbers, Custom"></asp:FilteredTextBoxExtender>
                                </div>  
                               </div>
       </div>
                              <%--  <div class="col-lg-6" style="padding-left:0px;">
                            <div class="form-group">        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-earphone"></span></span>
                               <asp:TextBox ID="txtContactNo" runat="server" MaxLength="12" CssClass="form-control input-sm" placeholder="Contact Number"></asp:TextBox>
                                   <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtContactNo" FilterType="Numbers, Custom"></asp:FilteredTextBoxExtender>
                               </div> 
                                   
                                </div>  
                               </div>--%>
                                <div class="col-lg-6" style="padding-right:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>
                                  <asp:DropDownList ID="ddlCity" runat="server"  CssClass="form-control input-sm" placeholder="Place" BackColor="#ffff97"></asp:DropDownList>
                           </div>
                               </div>
                                    </div>
                               
                                <div class="col-lg-6" style="padding-left:0px;">
                                 <div class="form-group" >    
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-globe"></span></span>
                                   <asp:DropDownList ID="drpSource" CssClass="form-control input-sm" runat="server">
                                       
                                   </asp:DropDownList>
                           </div>
                               </div>
                                    </div>
                                <div class="col-lg-6" style="padding-right:0px;">
                                 <div class="form-group" >       
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-copyright-mark"></span></span>
                                  <asp:DropDownList ID="drpReligion" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpReligion_SelectedIndexChanged">  
                                       
                                   </asp:DropDownList>
                           </div>
                               </div>
                                    </div>

                                <div class="col-lg-6" style="padding-left:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                    <asp:DropDownList ID="drpCategory" CssClass="form-control input-sm" placeholder="Course" runat="server">
                                       
                                   </asp:DropDownList>
                           </div>
                                     <div class="col-lg-6" id="errorShow" style="padding-left:0px;">
                                         </div>
                               </div>
                                    </div>
                                <div class="col-lg-6" style="padding-right:0px;">
                                 <div class="form-group" >        
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                   <asp:DropDownList ID="drpSubReligion" CssClass="form-control input-sm" placeholder="Course" runat="server">
                                       
                                   </asp:DropDownList>
                                   <asp:Label ID="lblSubReligionNotRequired" Style="line-height: 30px" runat="server" CssClass="form-control input-sm" Visible="false" ForeColor="Red" Text="Not Required">
                                                    </asp:Label>
                           </div>
                               </div>
                                    </div>
                                <div class="col-lg-6" style="padding-left:0px;">
                                <div class="form-group">                                                              
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-pencil"></span></span>
                               <asp:TextBox ID="txtRemarks"  TextMode="MultiLine" runat="server"  Class="form-control input-sm" placeholder="Remarks"></asp:TextBox>
                                   </div>
                           </div>
                                    </div>
                                  <div class="col-lg-6" style="padding-right:0px;">
                                <div class="form-group">                                                               
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-menu-hamburger"></span></span>
                               <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="form-control input-sm" placeholder="Address"></asp:TextBox>
                                   </div>
                           </div>
                                    </div>

                            <div >
                                <center>
                                <asp:Button ID="Button1" runat="server"  ValidationGroup="g1" Text="Submit" CssClass="btn-lg btn-primary btn-block"  Height="40px" Width="93px" OnClick="Button1_Click" />
                                    </center>
                            </div>
                        </div>
                </div>
</asp:Content>



