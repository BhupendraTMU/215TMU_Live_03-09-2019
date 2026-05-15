<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="VerifyEmployeeList.aspx.cs" Inherits="Faculty_VerifyEmployeeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlReport" runat="server" Visible="false" CssClass="leftBackground">
        <table cellpadding="0px" cellspacing="0px" width="99%">

            <tr>
                <td style="width: 25%">
                    <fieldset class="boxBody">
                        <asp:Label ID="Label1" runat="server"
                            Text="Verified Employee List" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                    </fieldset>
                </td>

            </tr>
            <tr>
                <td align="center">
                    <table cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td>Month</td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:DropDownList ID="DropDownList1" runat="server" Height="29px">
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
                                <asp:DropDownList ID="ddlYear1" runat="server" Height="29px"></asp:DropDownList>
                            </td>

                            <td style="width: 10px"></td>
                            <td>Employee Code </td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:TextBox ID="txtEmployeeCode" runat="server" Width="100px" Height="29px"></asp:TextBox>
                            </td>
                            <td style="width: 10px"></td>

                            <td style="width: 10px"></td>
                            <td>Department </td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:DropDownList ID="drpDepartment" runat="server" Width="100px" Height="29px"></asp:DropDownList>
                            </td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:Button ID="btnGet" runat="server" Text="Show" OnClick="btnGet_Click" />
                            </td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:Button ID="btnReport" runat="server" Text="Export to Excel" OnClick="btnReport_Click" />
                            </td>
                            <td style="width: 10px"></td>
                        </tr>
                        <tr>
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
                                    <asp:TemplateField HeaderText="Emp Name">
                                        <ItemTemplate>
                                            <asp:Label ID="EmpName" runat="server" Text='<%#Bind("[Employee Name]") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Emp Code" ControlStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="EmpCode" runat="server" Text='<%#Bind("[EmployeeCode]") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation" ControlStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="EmpDesig" runat="server" Text='<%#Bind("[Designation]") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="College Code" ControlStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="CollegeCode" runat="server" Text='<%#Bind("[College]") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department" ControlStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="Department" runat="server" Text='<%#Bind("[Department]") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approved By" ControlStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="Approvedby" runat="server" Text='<%#Bind("[ApprovedBY]") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HOD Code" ControlStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="HODCode" runat="server" Text='<%#Bind("[HOD]") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HOD Mail" ControlStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:Label ID="HODMail" runat="server" Text='<%#Bind("HODMail") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HOD Mobile" ControlStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="HODMobile" runat="server" Text='<%#Bind("HODMobile") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Year" ControlStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:Label ID="Reporting" runat="server" Text='<%#Bind("[Year]") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Month" ControlStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:Label ID="Deputed" runat="server" Text='<%#Bind("[Month]") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Hold">
                                        <ItemTemplate>
                                            <asp:Label ID="Hold" runat="server" Text='<%#Bind("[Hold]") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Verify">
                                        <ItemTemplate>
                                            <asp:Label ID="Verify" runat="server" Text='<%#Bind("[Verify]") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="txtRemark" runat="server" Width="200px" Text='<%#Bind("[Remark]") %>' />
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
                                <RowStyle BackColor="#E3EAEB" CssClass="cssGridheaderfont" Font-Names="Open Sans" Font-Size="10px" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                <SortedDescendingHeaderStyle BackColor="#15524A" />
                            </asp:GridView>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>


    </asp:Panel>

    <asp:Panel ID="pnlmsg" runat="server" Visible="false" CssClass="leftBackground">

        <fieldset class="boxBody">
            <asp:Label ID="Label11" runat="server"
                Text="You are not Authorized for this page." Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

        </fieldset>
    </asp:Panel>

</asp:Content>

