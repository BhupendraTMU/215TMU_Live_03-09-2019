<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" EnableEventValidation="false"   CodeFile="DailyAttendance.aspx.cs" Inherits="DailyAttendance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
   
    .parent
    {
        text-align:center;
        display: block;
        border: 1px solid outset;
    }
    .child
    {
        display: inline-block;       
        width: 200px;
    }
</style>
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
           //function checkDate(sender, args) {
           //    if (sender._selectedDate > new Date()) {
           //        alert("You cannot select greater than current date!");
           //        sender._selectedDate = new Date();
           //        sender._textbox.set_Value(sender._selectedDate.format(sender._format))
           //    }
           //}
           function checkDate(sender, args) {
               __doPostBack('Button1', '');
           }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ToolkitScriptManager ID="ScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="updmain">
        <ContentTemplate>--%>
    <asp:Panel ID="pnlList" runat="server" BorderWidth="2px" BorderColor="#ACE9FB" ScrollBars="Vertical">
    
    <div class="parent" style="background-color: #ACE9FB" >
                            <center>
                                <div class="child" style="fit-position: left;" >
                                    <b>
                                        <p style="color: white; font-size: 25px">Daily Attendance</p>
                                    </b>
                                </div>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
                                <div class="child" style="fit-position: right;">
                                    <b>
               <asp:TextBox runat="server" ID="txtDateOfAttendance" CssClass="form-control input-sm" runat="server" Width="150px" onkeypress="return false" onKeyDown="preventBackspace();" OnTextChanged="txtDateOfAttendance_TextChanged" AutoPostBack="true"></asp:TextBox>        
                         <asp:Image src="../Images/Calendar.png" runat="server" Height="30px" Width="25px" alt="" id="fdate" />
                        <asp:CalendarExtender ID="cleDate" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDate" CssClass="cal_Theme1" 
                            PopupButtonID="fdate" Enabled="true" TargetControlID="txtDateOfAttendance" />
                                       <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" style="display:none" />
                                    </b>
                                </div>
            </ContentTemplate>
        
    </asp:UpdatePanel>
           
                            </center>
                        </div>

         <div class="table-responsive">
                    <asp:GridView ID="grdAttendance" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="true" 
                        EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="5" OnPageIndexChanging="grdAttendance_PageIndexChanging" AllowSorting="true" OnRowCommand="grdAttendance_RowCommand" >
                        <%--<Columns>                            
                            <asp:BoundField DataField="Name" HeaderText="Student Name" SortExpression="StudentName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />                            
                            <asp:BoundField DataField="EnrollmentNo" HeaderText="Enrollment No" SortExpression="EnrollmentNo" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" />
                             <asp:BoundField DataField="GroundsForAppeal" HeaderText="Grounds For Appeal" SortExpression="GroundsForAppeal" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Appeal1" HeaderText="Relevant section" SortExpression="Appeal1" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />                           
                            <asp:BoundField DataField="Appeal2" HeaderText="Specific Details" SortExpression="StudentAppeal2" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />                                                                                 
                            <asp:BoundField DataField="AttendanceDate" HeaderText="Attendance Date" SortExpression="AttendanceDate" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />                            
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="btnDownload" Text='<%# Eval("AttachmentD") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Download"  ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>  --%>                     
                        <AlternatingRowStyle CssClass="danger" />
                        <PagerStyle HorizontalAlign = "Right" CssClass = "GridPager" />
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" PreviousPageText="Previous" NextPageText="Next" FirstPageText="First" LastPageText="Last" Position="TopAndBottom"  />
                    </asp:GridView>
             <br />
              
                </div>
      </asp:Panel>
  <%--</ContentTemplate>
        
    </asp:UpdatePanel>--%>
</asp:Content>

