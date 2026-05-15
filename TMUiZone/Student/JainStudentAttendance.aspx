<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="JainStudentAttendance.aspx.cs" Inherits="Student_JainStudentAttendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../bootstrap/js/jquery-1.11.2.min.js"></script>
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../bootstrap/js/bootstrap.min.js"></script>
    <style>
        .red-border {
            border: 1px solid red;
        }
.JainStudentList {
  font-family: Arial, Helvetica, sans-serif;
  border-collapse: collapse;
  width: 100%;
}

.JainStudentList td, .JainStudentList th {
  border: 1px solid #ddd;
  padding: 8px;
}

.JainStudentList tr:nth-child(even){background-color: #f2f2f2;}

.JainStudentList tr:hover {background-color: #ddd;}

.JainStudentList th {
  padding-top: 12px;
  padding-bottom: 12px;
  text-align: left;
  background-color: #04AA6D;
  color: white;
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
        <asp:Label ID="Label3" runat="server"
            Text="" Font-Size="18pt" Font-Bold="true" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <fieldset class="boxBodyHeader">
    </fieldset>
    <fieldset id="JainStudent" runat="server" style="background: #fefefe; border-top: 1px solid #dde0e8; border-bottom: 1px solid #dde0e8; padding: 10px 20px; height: 100%">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="col-sm-3 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font: bold; font-size: medium">&nbsp&nbsp&nbsp Academic Year</label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlAcademicYear" runat="server" CssClass="form-control" AutoPostBack="false"></asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="col-sm-3 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font: bold; font-size: medium">&nbsp&nbsp&nbsp Month &nbsp&nbsp&nbsp&nbsp     </label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlMonth" runat="server" Height="29px">
                            <asp:ListItem Text="January" Value="1"></asp:ListItem>
                            <asp:ListItem Text="February" Value="2"></asp:ListItem>
                            <asp:ListItem Text="March" Value="3"></asp:ListItem>
                            <asp:ListItem Text="April" Value="4"></asp:ListItem>
                            <asp:ListItem Text="May" Value="5"></asp:ListItem>
                            <asp:ListItem Text="June" Value="6"></asp:ListItem>
                            <asp:ListItem Text="July" Value="7"></asp:ListItem>
                            <asp:ListItem Text="August" Value="8"></asp:ListItem>
                            <asp:ListItem Text="September" Value="9"></asp:ListItem>
                            <asp:ListItem Text="October" Value="10"></asp:ListItem>
                            <asp:ListItem Text="November" Value="11"></asp:ListItem>
                            <asp:ListItem Text="December" Value="12"></asp:ListItem>
                           </asp:DropDownList>
                        </div>
                    </div>
                </div>


                <div class="col-sm-3 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font: bold; font-size: medium">&nbsp&nbsp&nbsp Year &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp  &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp </label>
                        <div class="col-sm-10">
                               <asp:DropDownList ID="ddlYear" runat="server" Height="29px">
                               </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3 p-0" style="margin-top: 25px;">
                    <div class="form-group clearfix">

                        <div class="col-sm-5">
                            <asp:Button ID="btnSearch" runat="server" CssClass="form-control" BackColor="Blue" ForeColor="White" Text="Search" AutoPostBack="false" OnClick="btnSearch_Click"></asp:Button>
                        </div>
                    </div>
                </div>

                <br />

                <table width="100%">
                    <tr>
                        <td>
                            <div class="panel panel-info" id="pnlAttendance" runat="server" style="width: 100%;">
                                <%-- <div class="panel-heading" style="height: 45px;vertical-align:middle">
                                    <asp:Label ID="Inbox" runat="server" Text="Attendance Sheet" Style="line-height: 8px" Font-Size="30px"></asp:Label>
                                </div>--%>
                                <div class="panel-body" id="grdInboxBody">
                                  <asp:UpdatePanel ID="Orderlist" runat="server">
                                     <ContentTemplate>
                                         <div class="box box-default" style="background-color: rgba(255, 255, 255, 0.80);overflow: auto;height: 720px;">
    <asp:GridView ID="JainStudentList" runat="server" AutoGenerateColumns="False" CssClass="JainStudentList" BackColor="White" BorderColor="#E7E7FF"
     EmptyDataText="There are no data records to display." BorderStyle="Solid" BorderWidth="2px" CellPadding="3" Width="1130px"
     GridLines="Horizontal" ShowFooter="true">
     <AlternatingRowStyle BackColor="#F7F7F7" />
     <Columns>
         <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-center">
             <ItemTemplate>
                 <%# Container.DataItemIndex +1 %>
             </ItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Finger No" ItemStyle-Width="2%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
             <ItemTemplate>
                 <asp:Label ID="lblFingerNo" runat="server" Text='<%# Bind("[Finger No_]") %>'></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>

         <asp:TemplateField HeaderText="ST No." ItemStyle-Width="3%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
             <ItemTemplate>
                 <asp:Label ID="lblNo_" runat="server" Text='<%# Bind("[No_]") %>'></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
          <asp:TemplateField HeaderText="Enrollment No." ItemStyle-Width="2%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
             <ItemTemplate>
                 <asp:Label ID="lblEnrollmentNo" runat="server" Text='<%# Bind("[Enrollment No_]") %>'></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
                  <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="3%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
             <ItemTemplate>
                 <asp:Label ID="lblStudentName" runat="server" Text='<%# Bind("[Student Name]") %>'></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
                  <asp:TemplateField HeaderText="Course Name" ItemStyle-Width="2%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
             <ItemTemplate>
                 <asp:Label ID="lblCourseName" runat="server" Text='<%# Bind("[Course Name]") %>'></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
                  <asp:TemplateField HeaderText="Percentage" ItemStyle-Width="2%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
    <ItemTemplate>
        <asp:Label ID="lblTotalPercentage" runat="server" Text='<%# Bind("[TotalPercentage]") %>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

         <asp:TemplateField HeaderText="Present / Total_Attendance" ItemStyle-Width="3%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
             <ItemTemplate>
                 <asp:LinkButton ID="lblTotalDays" runat="server" Text='<%# Bind("[Total_Present]") %>' OnClick="lblDel_Click"></asp:LinkButton>
           /
                 <asp:Label ID="lblTotalPresent" runat="server" Text='<%# Bind("[Total_Days]") %>' ></asp:Label>
              
                    </ItemTemplate>
         </asp:TemplateField>

       
     </Columns>
 </asp:GridView>
             <div class="box-footer">
             </div>
         </div>
     </ContentTemplate>
     </asp:UpdatePanel>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
                <%--ModalPOPUP--%>
                <asp:Panel ID="pnlGridViewDetails" CssClass="modalPopup" runat="server" Style="display: none; width: auto; height: auto">
                    <%--Add other controls here--%>
                    <div class="header">
                        <b>
                            <asp:Label ID="lblNotification" runat="server" Text="Attendance Detail"></asp:Label></b><div class="close">
                                <asp:Button ID="Button1" runat="server" Text="X" />
                            </div>
                    </div>
                    <div id="Div1" runat="server" style="max-height: 500px; overflow: auto;">

                        <div class="body">
                            <div style="width: 100%">
                                <center>

                                    <br />
                                    <asp:GridView ID="grdAttandanceDetails" AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound" Width="700px" EmptyDataText="There are no data records to display." runat="server" AlternatingRowStyle-BackColor="#F7F7F7">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl. No.">
                                                <ItemTemplate>
                                               &nbsp &nbsp&nbsp&nbsp    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PunchDate" HeaderText="Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg"  DataFormatString="{0:dd-MM-yyyy}" />
                                            <asp:BoundField DataField="AttendanceStatus" HeaderText="Attendance Status" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
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
                <asp:ModalPopupExtender ID="GridViewDetails" runat="server" TargetControlID="btnDummy"
                    PopupControlID="pnlGridViewDetails" BackgroundCssClass="modalBackground" />




                <%--ModalPOPUP--%>
            </ContentTemplate>
        </asp:UpdatePanel>
       <%-- <asp:Button ID="btnDownload" runat="server" Text="Download"  OnClick="btnDownload_Click" />--%>
</fieldset>
</asp:Content>
