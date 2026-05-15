<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="ITAsset.aspx.cs" Inherits="Faculty_ITAsset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset class="boxBody" style="margin-bottom: 10px;">
        <asp:Label ID="LabelTitle" runat="server"
            Text="IT Asset Details"
            Font-Size="16pt"
            ForeColor="#093A62"
            Font-Bold="true"
            Font-Names="Georgia, Times New Roman, Helvetica Neue">
        </asp:Label>
    </fieldset>

    <asp:GridView ID="grdAssestDetails" runat="server" OnRowCommand="grdAssestDetails_RowCommand"
        AutoGenerateColumns="False"
        Width="100%"
        BackColor="White"
        BorderColor="#ddd"
        BorderStyle="Solid"
        BorderWidth="1px"
        CellPadding="6"
        GridLines="None"
        EmptyDataText="No records found"
        AllowSorting="true"
        CssClass="table">

        <AlternatingRowStyle BackColor="#f9f9f9" />

        <Columns>


            <asp:TemplateField HeaderText="Sl. No.">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
                <ItemStyle Width="4%" />
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Indent No">
                <ItemTemplate>
                    <asp:Label ID="lblIndent" runat="server" Text='<%# Eval("Indent No_") %>' />
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Asset No">
                <ItemTemplate>
                    <asp:Label ID="lblAsset" runat="server" Text='<%# Eval("Asset No_") %>' />
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Serial No">
                <ItemTemplate>
                    <asp:Label ID="lblSerial" runat="server" Text='<%# Eval("Item Serial No_") %>' />
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Item Name">
                <ItemTemplate>
                    <asp:Label ID="lblItem" runat="server" Text='<%# Eval("Description") %>' />
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Issue Date">
                <ItemTemplate>
                    <asp:Label ID="lblDate" runat="server"
                        Text='<%# Eval("Alloted Date", "{0:dd-MMM-yyyy}") %>' />
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Approver ID">
                <ItemTemplate>
                    <asp:Label ID="lblApproverId" runat="server" Text='<%# Eval("Approver ID") %>' />
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Approver Name">
                <ItemTemplate>
                    <asp:Label ID="lblApproverName" runat="server" Text='<%# Eval("Approver ID Name") %>' />
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Room No">
                <ItemTemplate>
                    <asp:TextBox ID="lblRoom" runat="server" Text='<%# Eval("Location_Room no_") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Button ID="btnReceive" runat="server"
                        Text='<%# Eval("Status").ToString() == "Received" ? "Received" : "Not Yet Received" %>'
                        CommandName="Receive"
                        CommandArgument='<%# Container.DataItemIndex %>'
                        CssClass="btnAction"
                        Enabled='<%# Eval("Status").ToString() != "Received" %>'
                        OnClientClick="return confirm('Are you sure you want to mark as Received?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>


        <HeaderStyle BackColor="#ed7600" ForeColor="White" Font-Bold="true" />
        <RowStyle BackColor="#ffffff" ForeColor="#333" />
        <SelectedRowStyle BackColor="#88dde3" Font-Bold="true" />

    </asp:GridView>



</asp:Content>

