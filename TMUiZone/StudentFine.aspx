<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="StudentFine.aspx.cs" Inherits="StudentFine" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="bootstrap/js/jquery-1.11.2.min.js"></script>
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="bootstrap/js/bootstrap.min.js"></script>

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
        function Validation() {
            $('[id$=txtStudentNo]').addClass("red-border");
            $('[id$=lblName]').addClass("red-border");
            $('[id$=txtDateCommited]').addClass("red-border");
            $('[id$=drpStaffCode]').addClass("red-border");
            $('[id$=drpActionTaken]').addClass("red-border");
            $('[id$=txtAmount]').addClass("red-border");
            var a = '';
            if ($('[id$=txtStudentNo]').val() == "") { a = "false" } else { $('[id$=txtStudentNo]').removeClass("red-border"); }
            if ($('[id$=lblName]').text() == "") { a = "false" } else { $('[id$=lblName]').removeClass("red-border"); }
            if ($('[id$=txtDateCommited]').val() == "")         { a = "false" } else { $('[id$=txtDateCommited]').removeClass("red-border"); }
            if ($('[id$=drpStaffCode]').val() == "--Select--")  { a = "false" } else { $('[id$=drpStaffCode]').removeClass("red-border"); }
            if ($('[id$=drpActionTaken]').val() == "-- Select --") { a = "false" } else { $('[id$=drpActionTaken]').removeClass("red-border"); }
            if ($('[id$=txtAmount]').val() == "") { a = "false" } else { $('[id$=txtAmount]').removeClass("red-border"); }
            if (a == "false")
                return false;
        }
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
                    <div class="panel-heading" style="background-color:#337ab7; height:35px; cursor:pointer" id="head1">
                       <center> <div class="panel-title"><b><p  style="color:white;font-size:20px">Student Fine Details</p></b></div></center>
                    </div>
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
                    </div>
                                
                                                   
                    <div class="panel-body" style="border-color:#337ab7; border-top:groove" id="body2">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th style="font-size:15px">Date Committed</th>
                                    <th style="font-size:15px">Staff Code</th>
                                    <th style="font-size:15px">Action Taken</th>
                                    <th style="font-size:15px">Fine Amount</th>
                                    <th style="font-size:15px">Save</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                    <div class="col-lg-6" style="padding-left:0px;">
                               
                                   <asp:TextBox ID="txtDateCommited" CssClass="form-control input-sm" runat="server" onkeypress="return false" onKeyDown="preventBackspace();" ></asp:TextBox>
                                        
                                    </div>
                                <div class="col-lg-2" style="padding-left:0px;">
                                    <asp:Image src="Images/Calendar.png" runat="server" Height="30px" Width="25px" alt="" id="fdate" />
                                    <asp:CalendarExtender ID="CalendarExtender2" Format="dd MMM yyyy" runat="server"
                                        CssClass="cal_Theme1" PopupButtonID="fdate" Enabled="true" TargetControlID="txtDateCommited">
                                   </asp:CalendarExtender>
                                  </div>
                                        </td>
                                        <td>
                                        <div class="col-lg-12" style="padding-left:0px;">
                                            <asp:DropDownList ID="drpStaffCode" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                                            </div>
                                            </td>
                                    <td>
                                        <div class="col-lg-12" style="padding-left:0px;">
                                            <asp:DropDownList ID="drpActionTaken" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                                            </div>
                                    </td>
                                    <td>
                                         <div class="col-lg-8" style="padding-left:0px;">
                                             <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control input-sm" placeholder="Amount"></asp:TextBox>
                                             <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtAmount" FilterType="Numbers, Custom"></asp:FilteredTextBoxExtender>
                                             
                                            </div>
                                    </td>
                                     <td>
                                         <div class="col-lg-10" style="padding-left:0px;">
                                             <asp:Button ID="btnSave" runat="server" OnClientClick="return Validation()"   Height="30px" class="btn btn-success" Text="Save" ValidationGroup="g1" OnClick="btnSave_Click"  />                                             
                                             </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        
                     <div class="table-responsive">
                    <asp:GridView ID="grdFineInfo" runat="server" OnPageIndexChanging="grdFineInfo_PageIndexChanging" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false"   EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="5" OnSelectedIndexChanged="grdFineInfo_SelectedIndexChanged" >
                        <Columns>                            
                            <asp:BoundField DataField="Student No_" HeaderText="Student No" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Date Commited" HeaderText="Date Commited" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Staff Code" HeaderText="Staff Code" SortExpression="CourseCode" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Action Taken" HeaderText="Action Taken" SortExpression="FathersName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Fine Amount" HeaderText="Fine Amount" SortExpression="EnquiryType" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            
                        </Columns>                       
                        <AlternatingRowStyle CssClass="danger" />
                    </asp:GridView>
                                         </div>
                    </div>
                         </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtStudentNo" EventName="TextChanged"/>
                            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click"/>
                        </Triggers>
                    </asp:UpdatePanel>
                    
                </div>
              
            </div>
        </div>

    </div>
</asp:Content>

