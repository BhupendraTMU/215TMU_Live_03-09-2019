<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="EmployeeLeaveDetail.aspx.cs" Inherits="Faculty_EmployeeLeaveDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .txtstyle {
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
            border-bottom-left-radius: 5px;
            border-bottom-right-radius: 5px;
            background: #FFFFFF no-repeat 2px 2px;
            padding: 1px 1px 1px 5px;
            border: 2px solid #9900FF;
        }

            .txtstyle:focus {
                transition: all 0.30s ease-in-out;
                border: 1px solid #000000;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset class="boxBody">
        <asp:Label ID="Label3" runat="server"
            Text="Employee Leave details " Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
    </fieldset>
    <fieldset class="boxBody">
        <table>
            <tr>
                <td>
                    <asp:TextBox ID="txtEmployee" Style="text-transform: uppercase" runat="server" PlaceHolder="Employee Code" CssClass="txtstyle"></asp:TextBox>
                </td>
                <td style="width: 20px"></td>
                <td>
                    <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" />
                </td>
                <td>
                      <asp:Button ID="btnExport" runat="server" Text="Export to Excel" CssClass="btnLogin" OnClick="btnExport_Click" />
                </td>
            </tr>
        </table>

    </fieldset>
    <table>
        <tr>
            <td style="width: 50px"></td>

            <td>
                <div style="overflow:auto;height:450px">
                <asp:GridView ID="grdleave" BorderStyle="Solid" BorderWidth="1px" runat="server" AutoGenerateColumns="False" Width="866px" OnRowCreated="grdleave_RowCreated">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr No" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Employee Code" HeaderText="Employee ID" />
                        <asp:BoundField DataField="Employee_name" HeaderText="Employee Name" />
                        <asp:BoundField DataField="Leave code" HeaderText="Leave Code" DataFormatString="{0:F2}" />

                        <asp:BoundField DataField="Leave Balance" HeaderText="Leave Balance" DataFormatString="{0:F2}" />
                        <asp:BoundField DataField="Unapproved Leave" HeaderText="Pending Approval" DataFormatString="{0:F2}" />
                    </Columns>
                    <EmptyDataTemplate>
                        There is no leave detail
                    </EmptyDataTemplate>
                    <HeaderStyle HorizontalAlign="Left" BackColor="#ed7600" ForeColor="White" CssClass="cssGridheaderfont" />
                    <RowStyle CssClass="cssGridheaderfont" />
                </asp:GridView>
                    </div>
            </td>
        </tr>

    </table>


</asp:Content>

