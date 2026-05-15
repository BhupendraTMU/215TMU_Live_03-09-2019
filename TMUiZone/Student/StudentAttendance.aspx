<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentAttendance.aspx.cs" Inherits="StudentAttendance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="../bootstrap/js/jquery-1.11.2.min.js"></script>
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../bootstrap/js/bootstrap.min.js"></script>
     <style type="text/css">
        .red-border {
            border: 1px solid red;
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
          function checkDate(sender, args) {
              if (sender._selectedDate > new Date()) {
                  alert("You cannot select greater than current date!");
                  sender._selectedDate = new Date();
                  sender._textbox.set_Value(sender._selectedDate.format(sender._format))
              }
          }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <fieldset class="boxBody">
 <asp:Label ID="Label3" runat="server" 
            Text="Attendance" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 </fieldset>
 <fieldset class="boxBodyHeader"> 
  
 </fieldset>
<fieldset  style="background:#fefefe; border-top:1px solid #dde0e8; border-bottom:1px solid #dde0e8; padding:10px 20px; height:100%">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <table>
        <tr>
            <td style="width:10px"></td> 
            <td>
            <asp:RadioButton ID="rdbDateWise" Text="Date Wise" OnCheckedChanged="rdbDateWise_CheckedChanged" AutoPostBack="true" runat="server" Checked="true" GroupName="g" />
                </td>
            <td style="width:10px"></td> 
            <td>
            <asp:RadioButton ID="rdbSubjectWise" Text="Subject Wise" OnCheckedChanged="rdbSubjectWise_CheckedChanged" AutoPostBack="true" runat="server" GroupName="g"/>
            </td>
        </tr>
    </table>    

    <table>
        <tr>
        <br />
       <td style="width:10px"> <asp:HiddenField ID="hfCollegeCode" runat="server" /></td> 
        <td>
         <asp:Label ID="Label2"  runat="server" ><b style="line-height:20px">From</b></asp:Label>
        </td> 
        <td style="width:10px"></td>  
        <td>
                                   <asp:TextBox ID="txtDateFrom" Width="120px" Height="25px"  runat="server" onkeypress="return false" placeholder="From" onKeyDown="preventBackspace();" ></asp:TextBox>                                                                            
        </td>
        <td style="width:10px"></td>
        <td>
                                    <asp:Image src="../Images/Calendar.png" runat="server" Height="25px" Width="25px" alt="" id="fdate" />
                                    <asp:CalendarExtender ID="CalendarExtender2" Format="dd MMM yyyy" runat="server"
                                        CssClass="cal_Theme1" PopupButtonID="fdate" OnClientDateSelectionChanged="checkDate" Enabled="true" TargetControlID="txtDateFrom">
                                   </asp:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="please enter the date!" ValidationGroup="g1" ForeColor="Red" ControlToValidate="txtDateFrom"></asp:RequiredFieldValidator>
        </td>
        <td style="width:10px"></td>
        <td>
                                <asp:Label ID="Label1" runat="server" ><b style="line-height:20px">To</b></asp:Label>                                    
        </td>
        <td style="width:10px"></td>
        <td>                          
                                   <asp:TextBox ID="txtDateTo" Width="120px" Height="25px" runat="server" placeholder="To" onkeypress="return false" onKeyDown="preventBackspace();" ></asp:TextBox>
                                                                          
        </td>
        <td style="width:10px"></td>
        <td>
                                    <asp:Image src="../Images/Calendar.png" runat="server" Height="25px" Width="25px" alt="" id="fdate1" />
                                    <asp:CalendarExtender ID="CalendarExtender1" Format="dd MMM yyyy" runat="server"
                                        CssClass="cal_Theme1" PopupButtonID="fdate1" OnClientDateSelectionChanged="checkDate" Enabled="true" TargetControlID="txtDateTo">
                                   </asp:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="please enter the date!" ValidationGroup="g1" ForeColor="Red" ControlToValidate="txtDateTo"></asp:RequiredFieldValidator>
        </td>
         <td style="width:10px"></td>
        <td>
                                <asp:Label ID="lblSubject" runat="server" ><b style="line-height:25px">Subject</b></asp:Label>                                    
        </td>
        <td style="width:10px"></td>
        <td>                          <asp:DropDownList ID="drpSubject" Width="200px" Height="25px" runat="server" ></asp:DropDownList>
        </td>
        <td style="width:50px"></td>
        <td>
            &nbsp;&nbsp;&nbsp;<asp:LinkButton id="btnSearch" type="button" class="btn btn-info"  Height="30px" autopostback="true" runat="server" ValidationGroup="g1"
                 OnClick="btnSearch_Click"  >
                                     <span class="glyphicon glyphicon-search"></span> Search 
                                    </asp:LinkButton><br /><br />
        </td>
        </tr>
        </table>

    <br />
                       
    <table width="100%">
        <tr>
            <td style="width:10px"></td>
            <td>
                <div class="panel panel-info" id="pnlAttendance" visible="false" runat="server" style="width:80%;">
                    <div class="panel-heading" style="height:25px">
                            <asp:Label ID="Inbox" runat="server" Text="Attendance Sheet" style="line-height:5px"  Font-Size="15px"></asp:Label>
                        </div>
                     <div class="panel-body" id="grdInboxBody">
                         <asp:GridView ID="grdAttendanceReport" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" 
                             EmptyDataText="There are no data records to display." BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" 
                             GridLines="Horizontal" OnPageIndexChanging="grdAttendanceReport_PageIndexChanging" >
                   <AlternatingRowStyle BackColor="#F7F7F7" />
                 <Columns>
                                <asp:BoundField DataField="Date" HeaderText="Date"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                     <asp:BoundField DataField="LecturNo" HeaderText="Lectur No"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Subject Type" HeaderText="Subject Type"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Subject Description" HeaderText="Subject Description"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
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

                         <asp:GridView ID="grdAttendanceReport1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" 
                             EmptyDataText="There are no data records to display." BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" 
                             GridLines="Horizontal" OnPageIndexChanging="grdAttendanceReport_PageIndexChanging" OnRowCommand="grdAttendanceReport1_RowCommand">
                   <AlternatingRowStyle BackColor="#F7F7F7" />
                     <Columns>
                                <asp:BoundField DataField="Subject Description" HeaderText="Subject Description" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                      <asp:BoundField DataField="Code" HeaderText="Subject Code"   HeaderStyle-CssClass="hiddenfield" ItemStyle-CssClass="hiddenfield" />
                     <asp:TemplateField HeaderText="Present">
                         
                                
                            <ItemTemplate>
                                   <asp:LinkButton ID="BtnPresent" runat="server" Text='<%# Eval("Present") %>' CommandName="PresentItem"></asp:LinkButton>
                                        </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="Present" HeaderText="Present" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />--%>
                               <%-- <asp:BoundField DataField="Absent" HeaderText="Absent" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />--%>
                       <asp:TemplateField HeaderText="Absent">
                     <ItemTemplate>
                                   <asp:LinkButton ID="Absent" runat="server" Text='<%# Eval("Absent") %>' CommandName="AbsentItem"></asp:LinkButton>
                                        </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Percentage" HeaderText="Percentage(%)" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                 
                               <%-- <asp:BoundField DataField="Subject Description" HeaderText="Subject Description" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Present" HeaderText="Present" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Absent" HeaderText="Absent" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Percentage" HeaderText="Percentage(%)" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />--%>
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
                     </div>
                 </div>
            </td>
        </tr>
        </table>
           <%--ModalPOPUP--%> 
          <asp:Panel ID="pnlGridViewDetails" CssClass="modalPopup" runat="server" Style="display: none; width:auto;height:auto">
                                <%--Add other controls here--%>
                                <div class="header">
                                    <b>
                                        <asp:Label ID="lblNotification" runat="server" Text="Attendance Detail"></asp:Label></b><div class="close">
                                            <asp:Button ID="Button1" runat="server" Text="X" /></div>
                                </div>
                                <div id="Div1" runat="server" style="max-height: 500px; overflow: auto;">

                                    <div class="body">
                                        <div style="width: 100%">
                                            <center>
                                               
                                                <br />
                                                <asp:GridView ID="grdAttandanceDetails" AutoGenerateColumns="false" Width="700px" EmptyDataText="There are no data records to display." runat="server" AlternatingRowStyle-BackColor="#F7F7F7">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl. No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="7%" />
                                                        </asp:TemplateField>
                                                         <asp:BoundField DataField="Date" HeaderText="Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg"  />
                                                         <asp:BoundField DataField="Staff Code" HeaderText="Staff Code" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                        <asp:BoundField DataField="Remark" HeaderText="Remark" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                      <%--  <asp:BoundField DataField="Date" HeaderText="Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                         <asp:BoundField DataField="Staff Code" HeaderText="Staff Code" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />--%>
                                                      <%--  <asp:BoundField DataField="Subject Code" HeaderText="Subject Code" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />--%>
                                                       
                                                    </Columns>
                                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                    <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                                    <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
                                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                    <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                                    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                                    <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                                    <SortedDescendingHeaderStyle BackColor="#3E3277" />

                                                </asp:GridView>
                                            </center>
                                        </div>

                                    </div>

                                </div>
                            </asp:Panel>
                            <asp:Button ID="btnDummy" runat="server" Style="display: none;" />
                            <asp:ModalPopupExtender ID="MpaDetails" runat="server" TargetControlID="btnDummy"  PopupControlID="pnlGridViewDetails" BackgroundCssClass="modalBackground" />


    

   <%--ModalPOPUP--%> 
        </ContentTemplate>
    </asp:UpdatePanel>
</fieldset>
</asp:Content>

