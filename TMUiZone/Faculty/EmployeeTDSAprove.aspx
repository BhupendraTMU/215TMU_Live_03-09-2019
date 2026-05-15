<%@ Page Language="C#" MasterPageFile="~/Faculty/IndexMaster.master"  EnableEventValidation="false" AutoEventWireup="true" CodeFile="EmployeeTDSAprove.aspx.cs" Inherits="Faculty_EmployeeTDSAprove" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        /* ===== Custom Modal Styling ===== */
        #userDetailModal .modal-content {
            border-radius: 16px;
            border: none;
            box-shadow: 0 6px 25px rgba(0, 0, 0, 0.2);
            background: #ffffff;
            overflow: hidden;
        }

        #userDetailModal .modal-header {
            /*background: linear-gradient(135deg, #0d6efd, #4dabf7);*/
            background-color: #ed7600;
            color: #fff;
            padding: 15px 20px;
            border-bottom: none;
        }

        #userDetailModal .modal-title {
            font-size: 1.25rem;
            font-weight: 600;
        }

        #userDetailModal .btn-close {
            filter: brightness(0) invert(1); /* make close button white */
            opacity: 0.9;
        }

        /* ===== Table Styling ===== */
        #userDetailModal table {
            margin: 0;
            border-collapse: separate;
            border-spacing: 0 6px;
            width: 100%;
        }

        #userDetailModal th {
            width: 30%;
            background: #f8f9fa;
            color: #333;
            font-weight: 600;
            padding: 10px;
            border: none;
            border-radius: 6px 0 0 6px;
        }

        #userDetailModal td {
            background: #fff;
            padding: 10px;
            border: none;
            border-radius: 0 6px 6px 0;
            font-weight: 500;
        }

        /* Zebra effect with soft shadow */
        #userDetailModal tr {
            box-shadow: 0 2px 6px rgba(0,0,0,0.05);
            margin-bottom: 5px;
            display: table-row;
        }

        /* ===== Mobile Responsive ===== */
        @media (max-width: 576px) {
            #userDetailModal .modal-dialog {
                margin: 10px;
            }

            #userDetailModal th {
                width: 40%;
                font-size: 0.9rem;
            }

            #userDetailModal td {
                font-size: 0.9rem;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Employee TDS Amount Aprove" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <table>
        <tr style="height: 20px">
            <td></td>
        </tr>
        <tr>
            <td style="width: 20px"></td>
            <br />

            <asp:Button ID="btnExportExcel" runat="server"
                Text="Export To Excel"
                CssClass="btn btn-primary"
                OnClick="btnExportExcel_Click" />

            <br />
         
        </tr>
    </table>
    <br />
    <asp:GridView ID="grdmemberapprovallist" DataKeyNames="Year,Month,EmployeeNo" runat="server" AlternatingRowStyle-CssClass="danger" PageSize="50"
        AllowPaging="true" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" Visible="true" OnRowCommand="grdmemberapprovallist_RowCommand">
        <PagerSettings Mode="NumericFirstLast" />
        <PagerStyle CssClass="csspager" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <%# Container.DataItemIndex +1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Employee Code">
                <ItemTemplate>
                    <a href='#' onclick="ViewUserDetails('<%# Eval("EmployeeNo") %>');">
                        <%# Eval("EmployeeNo") %>
                    </a>
                    <asp:HiddenField ID="Hfemployeecode" Value='<%# Eval("EmployeeNo") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Employee Name">
                <ItemTemplate>
                    <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Eval("EmployeeName") %>' Style="text-transform: uppercase;"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="from Date">
                <ItemTemplate>
                    <asp:Label ID="lblfrom_date" runat="server" Text='<%# Eval("from_date", "{0:dd MMM yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="To Date">
                <ItemTemplate>
                    <asp:Label ID="lblTp_date" runat="server" Text='<%# Eval("to_date", "{0:dd MMM yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Actual Amount">
                <ItemTemplate>
                    <asp:Label ID="lblOldAmount" runat="server" Text='<%# Eval("OldAmount") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Updated Amount">
                <ItemTemplate>
                    <asp:Label ID="lblNewAmount" runat="server" Text='<%# Eval("NewAmount") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label ID="lblStatusText" runat="server" Text='<%# Eval("StatusText") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Remark">
                <ItemTemplate>
                    <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("remark") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:Button ID="btnAccept" runat="server" Text="Accept" CommandName="ActionCommand" CommandArgument='<%# Container.DataItemIndex %>' Style="background-color: green; color: white" />
                    <asp:Button ID="btnReject" runat="server" Text="Reject" CommandName="ActionCommand" CommandArgument='<%# Container.DataItemIndex %>' Style="background-color: red; color: white" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
    </asp:GridView>
    <asp:GridView ID="gvDetails" CellPadding="5" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Text" HeaderText="FileName" />
        </Columns>
        <HeaderStyle BackColor="#df5015" Font-Bold="true" ForeColor="White" />


    </asp:GridView>

</asp:Content>
