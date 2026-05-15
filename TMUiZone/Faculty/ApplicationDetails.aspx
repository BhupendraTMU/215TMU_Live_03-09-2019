<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="ApplicationDetails.aspx.cs" Inherits="Faculty_ApplicationDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Admission Details" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <table>
        <tr>
           
            <td style="text-align: right">Program Code :
                <asp:DropDownList ID="drpProgram" runat="server">
                     <asp:ListItem Text="" Value=""></asp:ListItem>
                     <asp:ListItem Text="All" Value="All"></asp:ListItem>
                    <asp:ListItem Text="BBA-001" Value="BBA-001"></asp:ListItem>
                    <asp:ListItem Text="BSC-006" Value="BSC-006"></asp:ListItem>
                    <asp:ListItem Text="MBA-001" Value="MBA-001"></asp:ListItem>
                    <asp:ListItem Text="NUR-005" Value="NUR-005"></asp:ListItem>
                    <asp:ListItem Text="PT-001" Value="PT-001"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btnLogin" OnClick="btnShow_Click" />

            
                <asp:Button ID="btnExport" runat="server" CssClass="btnLogin" Text="Export to Excel" OnClick="btnExport_Click" />

            </td>
        </tr>
        <tr>
            
            <td style="padding-left: 20px">
                <asp:GridView ID="GridView1" runat="server" CellPadding="3"
                    CellSpacing="3" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" Width="1000px">

                    <Columns>

                        <asp:TemplateField HeaderText="SrNo">
                            <ItemTemplate>
                                <span>
                                    <%#Container.DataItemIndex + 1%>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Program Code">
                            <ItemTemplate>
                                <asp:Label ID="ProgramCode" runat="server" Text='<%#Bind("[Program Code]") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Program Description" ControlStyle-Width="500px">
                            <ItemTemplate>
                                <asp:Label ID="ProgramDescription" runat="server" Text='<%#Bind("[Program Description]") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Admitted Year" ControlStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Label ID="AdmittedYear" runat="server" Text='<%#Bind("[Admitted Year]") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Application Received" ControlStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Label ID="ApplicationReceived" runat="server" Text='<%#Bind("[Application Received]") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Intake" ControlStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Label ID="Intake" runat="server" Text='<%#Bind("[Intake]") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Admission Made" ControlStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Label ID="AdmissionMade" runat="server" Text='<%#Bind("[Admission Made]") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <EmptyDataTemplate>
                        There is no Record Found.
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="#ed7600" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="White"
                        HorizontalAlign="Left" CssClass="cssGridheaderfont" Font-Names="Open Sans" />
                    <PagerStyle BackColor="#ed7600" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle CssClass="cssGridheaderfont" Font-Names="Open Sans" Font-Size="10px" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                </asp:GridView>
            </td>
        </tr>
    </table>


</asp:Content>

