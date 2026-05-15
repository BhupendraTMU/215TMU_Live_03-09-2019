<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="ViewAttendance.aspx.cs" Inherits="Student_ViewAttendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../bootstrap/js/jquery-1.11.2.min.js"></script>
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../bootstrap/js/bootstrap.min.js"></script>
    <style>
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
        <asp:Label ID="Label3" runat="server"
            Text="Course Wise Attendance" Font-Size="18pt" Font-Bold="true" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <fieldset class="boxBodyHeader">
    </fieldset>
    <fieldset style="background: #fefefe; border-top: 1px solid #dde0e8; border-bottom: 1px solid #dde0e8; padding: 10px 20px; height: 100%">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="col-sm-3 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font: bold; font-size: medium">&nbsp&nbsp&nbsp Academic Year</label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlAcademicYear" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="col-sm-3 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font: bold; font-size: medium">&nbsp&nbsp&nbsp Sem/Year&nbsp&nbsp&nbsp&nbsp     </label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlSem" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>


                <div class="col-sm-3 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font: bold; font-size: medium">&nbsp&nbsp&nbsp Course &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp  &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp </label>
                        <div class="col-sm-10">
                            <asp:DropDownList ID="drpSubject" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpSubject_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3 p-0">
                    <div class="form-group clearfix">

                        <div class="col-sm-5">
                            <asp:Button ID="btnSearch" runat="server" CssClass="form-control" BackColor="Blue" ForeColor="White" Text="Search" AutoPostBack="true" OnClick="btnSearch_Click"></asp:Button>
                        </div>
                    </div>
                </div>

                <br />

                <table width="100%">
                    <tr>
                        <td style="width: 10px"></td>
                        <td>
                            <div class="panel panel-info" id="pnlAttendance" runat="server" style="width: 100%;">
                                <%-- <div class="panel-heading" style="height: 45px;vertical-align:middle">
                                    <asp:Label ID="Inbox" runat="server" Text="Attendance Sheet" Style="line-height: 8px" Font-Size="30px"></asp:Label>
                                </div>--%>
                                <div class="panel-body" id="grdInboxBody">
                                    <asp:GridView ID="grdAttendanceReport" DataKeyNames="Course Code" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
                                        EmptyDataText="There are no data records to display." BorderStyle="None" BorderWidth="2px" CellPadding="3" Width="1130px"
                                        GridLines="Horizontal" ShowFooter="true">
                                        <AlternatingRowStyle BackColor="#F7F7F7" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex +1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Course Name" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCourse" runat="server" Text='<%# Bind("[Course Name]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Course Code" ItemStyle-Width="3%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCourseCode" runat="server" Text='<%# Bind("[Course Code]") %>'></asp:Label>



                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Attended/Delivered" ItemStyle-Width="2%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbllecture" runat="server" Text='<%# Bind("[Attend]") %>' ></asp:Label>
                                                    / 
                                                     <asp:LinkButton ID="lblDel" runat="server" Text='<%# Bind("[Delivered]") %>' OnClick="lblDel_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Percent" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPer" runat="server" Text='<%# "" + Eval("Per") + " % "%>'></asp:Label>
                                                    <%-- <asp:Label ID="lblPer" runat="server" Text='<%# Eval("Per") %>'></asp:Label>--%>
                                                </ItemTemplate>
                                                <%-- <FooterTemplate>
                                                    <div style="text-align: right; width: 150px">
                                                        <asp:Label ID="lblTotalqty" runat="server" Text="sdhjdh" Font-Bold="true" />
                                                    </div>
                                                </FooterTemplate>--%>
                                            </asp:TemplateField>


                                            


                                             <asp:TemplateField HeaderText="Handout" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lnkDownload" runat="server"
                                                        Text="Download"
                                                        NavigateUrl='<%# Eval("FilePath") %>'
                                                        Visible='<%# Eval("FilePath").ToString() == "0" ? false : true %>'
                                                        Target="_blank" />
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                        </Columns>
                                        <FooterStyle ForeColor="Green" Font-Bold="true" Font-Size="Medium" BorderStyle="Solid" BorderColor="Black" BackColor="LightGray" />
                                        <HeaderStyle BackColor="LightGray" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" Font-Size="Large" Height="40px" VerticalAlign="Bottom" />
                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                        <RowStyle ForeColor="#4A3C8C" Font-Bold="true" Font-Size="Medium" BorderStyle="Solid" BorderColor="Black" />
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
                                    <asp:GridView ID="grdAttandanceDetails" AutoGenerateColumns="false" Width="700px" EmptyDataText="There are no data records to display." runat="server" AlternatingRowStyle-BackColor="#F7F7F7">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl. No.">
                                                <ItemTemplate>
                                               &nbsp &nbsp&nbsp&nbsp    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Date1" HeaderText="Lecture Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="Hour" HeaderText="Hour" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="AttendanceStatus" HeaderText="Attendance Status" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="AttandanceMarkby" HeaderText="Attandance_Mark_by" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                          <asp:BoundField DataField="Updated Date" HeaderText="Attendance Mark Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
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
    </fieldset>
</asp:Content>
