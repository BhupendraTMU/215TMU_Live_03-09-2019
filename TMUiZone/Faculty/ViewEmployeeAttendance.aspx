<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="ViewEmployeeAttendance.aspx.cs" Inherits="ViewEmployeeAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="dropdowneditable/jquery.min.js"></script>
	<script type="text/javascript" src="dropdowneditable/jquery.searchabledropdown-1.0.8.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("select").searchable();
        });        
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <table cellpadding="0px" cellspacing="0px" style="width: 100%">
        <tr>
            <td style="height: 13px"></td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp; &nbsp; 
                <asp:Label ID="Label3" runat="server"
                    Text="Team Attendance" Font-Size="15pt" ForeColor="#093A62"
                    Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
            </td>
        </tr>

        <tr>
            <td style="height: 13px"></td>
        </tr>


        <tr>
            <td class="leftm"></td>
        </tr>

        <tr>
            <td style="height: 13px"></td>
        </tr>

        <tr>
            <td align="center">
                <table cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td>Month</td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:DropDownList ID="ddlMonth" runat="server" Height="29px">
                                <asp:ListItem Value="01">January</asp:ListItem>
                                <asp:ListItem Value="02">February</asp:ListItem>
                                <asp:ListItem Value="03">March</asp:ListItem>
                                <asp:ListItem Value="04">April</asp:ListItem>
                                <asp:ListItem Value="05">May</asp:ListItem>
                                <asp:ListItem Value="06">June</asp:ListItem>
                                <asp:ListItem Value="07">July</asp:ListItem>
                                <asp:ListItem Value="08">August</asp:ListItem>
                                <asp:ListItem Value="09">September</asp:ListItem>
                                <asp:ListItem Value="10">October</asp:ListItem>
                                <asp:ListItem Value="11">November</asp:ListItem>
                                <asp:ListItem Value="12">December</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="width: 10px"></td>
                        <td>Year </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:DropDownList ID="ddlYear" runat="server" Height="29px"></asp:DropDownList>
                        </td>
                        <td style="width: 10px"></td>
                        <td>Employee </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:DropDownList ID="txtUserid" runat="server" Height="28px"></asp:DropDownList>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:Button ID="btnGet" runat="server" Text="Get" OnClick="btnGet_Click" />
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:Button ID="btnexporttoexcel" runat="server" Text="Export To Excel" OnClick="btnexporttoexcel_Click" /></td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td style="height: 13px"></td>
        </tr>

        <tr>
            <td>

                <table cellpadding="0px" cellspacing="0px" style="width: 100%">
                    <tr>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:GridView ID="grd_ViewAttendance" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnRowDataBound="grd_ViewAttendance_RowDataBound" CssClass="table table-striped table-bordered table-hover">
                                <Columns>
                                    <asp:BoundField DataField="Attendance Date" HeaderText="Date"></asp:BoundField>
                                    <asp:BoundField DataField="Week Day" HeaderText="Week Day" />
                                    <asp:BoundField DataField="ShiftTime" HeaderText="Shift Time" />
                                    <asp:BoundField DataField="Time From" HeaderText="In Time" />
                                    <asp:BoundField DataField="Time To" HeaderText="Out Time" />
                                    <asp:BoundField DataField="WorkingHour" HeaderText="Working Hour" />
                                    <asp:BoundField DataField="LateBy" HeaderText="Late BY" />

                                    <%--  <asp:TemplateField HeaderText="Late BY">
                                <ItemTemplate>
                                    <asp:Label ID="lblLateBY_GRID" runat="server" Text='<%#Bind("LateBy") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Early BY">
                                <ItemTemplate>
                                    <asp:Label ID="lblLateEarlyBy" runat="server" Text='<%#Bind("EarlyBy") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                                    <asp:BoundField DataField="EarlyBy" HeaderText="Early BY" />
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="shiftTimeIn" HeaderText="ShiftInTime" Visible="false" />
                                    <asp:BoundField DataField="ShiftTimeOut" HeaderText="ShiftOutTime" Visible="false" />
                                </Columns>
                                <EmptyDataTemplate>
                                    There is no record found
                                </EmptyDataTemplate>
                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                <HeaderStyle HorizontalAlign="Center" Height="20px" BackColor="#ff9900" ForeColor="White" />
                                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                <RowStyle BackColor="White" ForeColor="#330099" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                <SortedDescendingHeaderStyle BackColor="#7E0000" />

                            </asp:GridView>
                        </td>
                        <td style="width: 10px"></td>
                    </tr>
                </table>


            </td>
        </tr>

        <tr>
            <td style="height: 90px"></td>
        </tr>
    </table>

</asp:Content>

