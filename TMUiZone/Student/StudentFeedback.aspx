<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentFeedback.aspx.cs" Inherits="Student_StudentFeedback" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript" src="../bootstrap/js/jquery-1.11.2.min.js"></script>
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../bootstrap/js/bootstrap.min.js"></script>
    <style>
        .red-border {
            border: 1px solid red;
        }
    </style>
    <script type="text/javascript">
        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select greater than current date!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
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
            debugger
            
            $('[id$=txtRemarks]').addClass("red-border");
            $('[id$=txtDateCommited]').addClass("red-border");
            $('[id$=txtFeedbackFor]').addClass("red-border");
            alert($('[id$=txtFeedbackFor]').val());
            var a = '';
            if ($('[id$=txtDateCommited]').val() == "") { a = "false" } else { $('[id$=txtDateCommited]').removeClass("red-border"); }
            if ($('[id$=txtFeedbackFor]').val() == "") { a = "false" } else { $('[id$=txtFeedbackFor]').removeClass("red-border"); }
            if ($('[id$=txtRemarks]').val() == "") { a = "false" } else { $('[id$=txtRemarks]').removeClass("red-border"); }
            if (a == "false")
                return false;
        }
   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"  Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
   <fieldset  class="boxBody">
    <table>
        
        <tr>
            <br />
            <td style="width:20px"> </td>
            <td>
                                <asp:Label ID="Label2" runat="server" ><b style="line-height:30px">Date Commited</b></asp:Label> 
                                    </td>
            <td style="width:20px"> </td>
            <td style="width:150px">
                                     <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span> 
                                   <asp:TextBox ID="txtDateCommited" CssClass="form-control input-sm" runat="server" onkeypress="return false" onKeyDown="preventBackspace();" ></asp:TextBox>
                                        </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="please enter the date!" ValidationGroup="g1" ForeColor="Red" ControlToValidate="txtDateCommited"></asp:RequiredFieldValidator>
                </td><td style="width:10px"> </td>
            <td>
                                    <asp:Image src="../Images/Calendar.png" runat="server" Height="30px" Width="25px" alt="" id="fdate" />
                                    <asp:CalendarExtender ID="CalendarExtender2" Format="dd MMM yyyy" runat="server"
                                        CssClass="cal_Theme1" OnClientDateSelectionChanged="checkDate" PopupButtonID="fdate" Enabled="true" TargetControlID="txtDateCommited">
                                   </asp:CalendarExtender>
                </td>
            <td style="width:200px"> </td>
            </tr>
        <tr> <td colspan="5" style="height:10px"> </td></tr>
        <tr>
            
           <td style="width:20px"> </td>
             <td>
                                <asp:Label ID="Label1"  runat="server" ><b style="line-height:30px">Feedback For</b></asp:Label> 
                        </td>  
            <td style="width:20px"> </td>
              <td style="width:300px" colspan="7">        
                                 <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>                     
                               <asp:TextBox ID="txtFeedbackFor" runat="server" CssClass="form-control input-sm" placeholder="Feedback For"></asp:TextBox>
                                    </div>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="please enter the input!" ValidationGroup="g1" ForeColor="Red" ControlToValidate="txtFeedbackFor"></asp:RequiredFieldValidator>
            </td>
                          </tr>
        <tr> <td colspan="5" style="height:10px"> </td></tr>
        <tr>
            <td style="width:20px"> </td>
            <td>
                                <asp:Label ID="Label4" runat="server" ><b style="line-height:30px">Remarks</b></asp:Label> 
                    </td>   
            <td style="width:20px"> </td>
             <td style="width:300px" colspan="7">           
                             <div class="input-group">
                               <span class="input-group-addon"><span class="glyphicon glyphicon-pencil"></span></span>  
                                   <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control input-sm" placeholder="Remarks" TextMode="multiline"></asp:TextBox>
                                </div>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="please enter the remarks!" ValidationGroup="g1" ForeColor="Red" ControlToValidate="txtRemarks"></asp:RequiredFieldValidator>
                        </td> 
            </tr>
        <tr>
            <td style="height:10px"> </td>
            <td colspan="9">
                <center>
                <br />
            <asp:Button ID="btnSave" runat="server"  CssClass="btn-lg btn-primary btn-block" Height="43px" Width="93px" ValidationGroup="g1"  Text="Save" OnClick="btnSave_Click" />
                </center>
            <br />
    <asp:GridView ID="grdFeedbackReport" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" OnPageIndexChanging="grdFeedbackReport_PageIndexChanging"  EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="10">
               <AlternatingRowStyle BackColor="#F7F7F7" />
                        <Columns>                            
                            <asp:BoundField DataField="SNo" HeaderText="Serial No" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Date1" HeaderText="Date" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="FeedbackFor" HeaderText="Feedback For" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                        </Columns>                       
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
               <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
               <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
               <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont"   />
               <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
               <SortedAscendingCellStyle BackColor="#F4F4FD" />
               <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
               <SortedDescendingCellStyle BackColor="#D8D8F0" />
               <SortedDescendingHeaderStyle BackColor="#3E3277" />
                    </asp:GridView>
            </td>
        </tr>
                </table>    
       </fieldset>
</asp:Content>

