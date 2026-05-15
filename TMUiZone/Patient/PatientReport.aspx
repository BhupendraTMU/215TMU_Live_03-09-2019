<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PatientReport.aspx.cs" Inherits="PatientReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="margin-top: 10px; margin-left: 50px">
                <tr>
                    <td>

                        <asp:Image ID="imglogo" ImageUrl="~/images/rightlogo.png" Height="80px" Width="80px" runat="server" />
                    </td>


                </tr>
                <tr>
                    <td>
                        <asp:Label ID="msg" runat="server" Font-Underline="true" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 30px"></td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="PatientMain" DataKeyNames="LabNo,ServiceName" runat="server" BackColor="White" AutoGenerateColumns="false" BorderColor="#E7E7FF" HeaderStyle-BackColor="#ff9900" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>


                                <asp:TemplateField HeaderText="Source">
                                    <ItemTemplate>
                                        <asp:Label ID="LblSource" runat="server" Text='<%# Bind("[Source]") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lab No">
                                    <ItemTemplate>
                                        <asp:Label ID="LblLabNo" runat="server" Text='<%# Bind("[LabNo]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Registration No">
                                    <ItemTemplate>
                                        <asp:Label ID="LblRegistrationNo" runat="server" Text='<%# Bind("[RegistrationNo]") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Report">
                                    <ItemTemplate>
                                        <asp:Label ID="LblServiceName" runat="server" Text='<%# Bind("[ServiceName]") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Referred By">
                                    <ItemTemplate>
                                        <asp:Label ID="LblReferredBy" runat="server" Text='<%# Bind("[Referred By]") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Report Status">
                                    <ItemTemplate>
                                        <asp:Label ID="LblReportStatus" runat="server" Text='<%# Bind("[ReportStatus]") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sample Date">
                                    <ItemTemplate>
                                        <asp:Label ID="LblSampleDate" runat="server" Text='<%# Bind("[SampleCollectedDate]") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Print">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkPrint" runat="server" Text="PRINT" OnClick="lnkPrint_Click" ></asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>


                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
