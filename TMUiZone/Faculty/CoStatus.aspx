<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="CoStatus.aspx.cs" Inherits="Faculty_CoStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Co Status Employee Wise" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>

    <fieldset class="boxBody">
        <table>
            <tr>
                <td>Employee Code
                </td>
                <td style="width: 20px"></td>
                <td>
                    <asp:TextBox ID="txtEmployee" runat="server" Style="text-transform: uppercase" Height="25px"></asp:TextBox>
                </td>
                <td style="width: 20px"></td>
                <td>
                    <asp:Button ID="btnSeasrch" runat="server" Text="Search" OnClick="btnSeasrch_Click" />
                </td>
            </tr>

        </table>



    </fieldset>
    <table >
        <tr >
            <td style="width:100px"></td>
            <td>
                <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="SNo" ItemStyle-Width="50px">
                            <ItemTemplate>
                               
                                    <%#Container.DataItemIndex + 1%>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User ID" ControlStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpId" runat="server" Text='<%#Bind("[UserID]") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Emp Name" ControlStyle-Width="200px">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpName" runat="server" Text='<%#Bind("[Name]") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Co Date" ControlStyle-Width="150px">
                            <ItemTemplate>
                                <asp:Label ID="lblPeriod" runat="server" Text='<%#Bind("[Co_Date]") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Expire On" ControlStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Label ID="lblLeaveDay" runat="server" Text='<%#Bind("[Expire on]") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Co Status" ControlStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Label ID="lblLeaveType" runat="server" Text='<%#Bind("[Status]") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        

                    </Columns>


                    <EditRowStyle BackColor="#7C6F57" />
                    <EmptyDataTemplate>
                        There is no leave detail
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="#ed7600" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="White"
                        HorizontalAlign="Left" CssClass="cssGridheaderfont" Font-Names="Open Sans" />
                    <PagerStyle BackColor="#ed7600" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" CssClass="cssGridheaderfont" Font-Names="Open Sans" Font-Size="10px" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="height:20px"></td>
        </tr>
      
    </table>



</asp:Content>

