<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentTagingReport.aspx.cs" Inherits="Faculty_StudentTagingReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <table cellpadding="0px" cellspacing="0px" style="width: 100%">
        <tr>
            <td style="height: 13px"></td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp; &nbsp; 
                <asp:Label ID="Label3" runat="server"
                    Text="Student Tagging Report" Font-Size="15pt" ForeColor="#093A62"
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
                        <td>Academic Year</td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:DropDownList ID="ddlAcademicYear" runat="server" Height="29px">
                            </asp:DropDownList></td>
                        <td style="width: 10px"></td>
                        <td>College Code </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:DropDownList ID="ddlCollege" runat="server" Height="29px" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td style="width: 10px"></td>
                        <td>Course Code </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:DropDownList ID="drpCourse" runat="server" Height="29px"></asp:DropDownList>
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
                            <asp:GridView ID="grd_ViewAttendance" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                                CssClass="table table-striped table-bordered table-hover">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo">
                                        <ItemTemplate>
                                            <span>
                                                <%#Container.DataItemIndex + 1%>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:BoundField DataField="Global Dimension 1 Code" HeaderText="College Code" />
                                    <asp:BoundField DataField="Academic Year" HeaderText="Academic Year" />
                                    <asp:BoundField DataField="Course Name" HeaderText="Course Name" />
                                    <asp:BoundField DataField="No_" HeaderText="Student No_"></asp:BoundField>
                                    <asp:BoundField DataField="Course Code" HeaderText="Course Code" />
                                    <asp:BoundField DataField="Subject Code" HeaderText="Subject Code" />
                                    <asp:BoundField DataField="Tag Date" HeaderText="Tag Date" />







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

