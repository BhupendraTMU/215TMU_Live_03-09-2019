<%@ Page Title="" Language="C#" MasterPageFile="~/Student/StudentMaster.master" AutoEventWireup="true" CodeFile="StudentDetailsView.aspx.cs" Inherits="Student_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../bootstrap/js/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../bootstrap/js/jquery-1.11.2.min.js"></script>
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../bootstrap/js/bootstrap.min.js"></script>

    <style>
        .red-border {
            border: 1px solid red;
        }
    </style>
    <style type="text/css"> 
        .completionList {
        border:solid 1px Gray;
        margin:0px;
        padding:3px;
        height: 140px;
        overflow:scroll;
        background-color: #FFFFFF;     
        } 
        .listItem {
        color: #191919;
        } 
        .itemHighlighted {
        background-color: #ADD6FF;       
        }
    </style>
    <script>
        $(document).ready(function () {
            $("#head1").click(function () {
                $("#body1").toggle();
            });

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <br />
    <div class="container" style="padding-top:inherit; padding-left:10px;">
        <br />
        <div class="row">
            <div class="col-lg-10">
                <div class="panel panel-info" style="border-color:#337ab7">
                    
                    <asp:UpdatePanel ID="updatepanel1" runat="server">
                        <ContentTemplate>

                    <div class="panel-body" id="body1">
                             <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label3" style="line-height:30px" runat="server" ><b>Student No.</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-left:0px;">
                           <div class="form-group">       
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>   
                                   <asp:TextBox ID="txtStudentNo" runat="server" CssClass="form-control input-sm" autocomplete="off" placeholder="Student No" AutoPostBack="true" OnTextChanged="txtStudentNo_TextChanged" ></asp:TextBox>                   
                               <asp:AutoCompleteExtender ServiceMethod="SearchCustomers" MinimumPrefixLength="3" CompletionInterval="0" EnableCaching="false"
                                    CompletionSetCount="6" TargetControlID="txtStudentNo"  CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                     CompletionListHighlightedItemCssClass="itemHighlighted" ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "true"></asp:AutoCompleteExtender>
                           </div>
                          </div>
                        </div>
                         <div class="col-lg-2" style="padding-right:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label2" style="line-height:30px" runat="server" ><b>Name</b></asp:Label> 
                                    </div>
                                     </div>

                                <div class="col-lg-4" style="padding-right:0px;">
                           <div class="form-group">       
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>                      
                               <asp:Label ID="lblName" CssClass="form-control input-sm" runat="server" ></asp:Label>
                           </div>
                                    </div>
                                    </div>
                         <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label4" style="line-height:30px" runat="server" ><b>Course</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-left:0px;">
                           <div class="form-group">       
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>                      
                               <asp:Label ID="lblCourse" CssClass="form-control input-sm" runat="server" ></asp:Label>
                           </div>
                                    </div>
                                    </div>
                         <div class="col-lg-2" style="padding-right:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label6" style="line-height:30px" runat="server" ><b>Semester</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-right:0px;">
                           <div class="form-group">       
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>                      
                               <asp:Label ID="lblSemester" CssClass="form-control input-sm" runat="server" ></asp:Label>
                           </div>
                                    </div>
                                    </div>
                         <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label8" style="line-height:30px" runat="server"><b>Academic Year</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-left:0px;">
                           <div class="form-group">       
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>                      
                               <asp:Label ID="lblAcademicYear" CssClass="form-control input-sm" runat="server" ></asp:Label>
                           </div>
                                    </div>
                                    </div>
                         <div class="col-lg-2" style="padding-right:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label10" style="line-height:30px" runat="server" ><b>Section</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-right:0px;">
                           <div class="form-group">       
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>                      
                               <asp:Label ID="lblSection" CssClass="form-control input-sm" runat="server" ></asp:Label>
                           </div>
                                    </div>
                                    </div>
                         <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label1" style="line-height:30px" runat="server" ><b>Date of Birth</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-left:0px;">
                           <div class="form-group">       
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>                      
                               <asp:Label ID="lblDOB" CssClass="form-control input-sm" runat="server" ></asp:Label>
                           </div>
                                    </div>
                                    </div>
                        <div class="col-lg-2" style="padding-right:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label5" style="line-height:30px" runat="server" ><b>Mobile Number</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-right:0px;">
                           <div class="form-group">       
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>                      
                               <asp:Label ID="lblMobileNumber" CssClass="form-control input-sm" runat="server" ></asp:Label>
                           </div>
                                    </div>
                                    </div>
                         <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label7" style="line-height:30px" runat="server" ><b>Father's Name</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-left:0px;">
                           <div class="form-group">       
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>                      
                               <asp:Label ID="lblFathersName" CssClass="form-control input-sm" runat="server" ></asp:Label>
                           </div>
                                    </div>
                                    </div>
                         <div class="col-lg-2" style="padding-right:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label11" style="line-height:30px" runat="server" ><b>Mother's Name</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-right:0px;">
                           <div class="form-group">       
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>                      
                               <asp:Label ID="lblMothersName" CssClass="form-control input-sm" runat="server" ></asp:Label>
                           </div>
                                    </div>
                                    </div>
                         <div class="col-lg-2" style="padding-left:0px; font-size:12px; ">
                                    <div class="form-group">
                                <asp:Label ID="Label13" style="line-height:30px" runat="server" ><b>Category</b></asp:Label> 
                                    </div>
                                     </div>
                                <div class="col-lg-4" style="padding-left:0px;">
                           <div class="form-group">       
                               <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>                      
                               <asp:Label ID="lblCategory" CssClass="form-control input-sm" runat="server" ></asp:Label>
                           </div>
                                    </div>
                                    </div>
                    </div>
                                
                         </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtStudentNo" EventName="TextChanged"/>
                        </Triggers>
                    </asp:UpdatePanel>
                    
                </div>
              
            </div>
        </div>

    </div>
</asp:Content>



