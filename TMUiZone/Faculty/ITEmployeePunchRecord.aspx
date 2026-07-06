<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="ITEmployeePunchRecord.aspx.cs" Inherits="Faculty_ITEmployeePunchRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 100%; max-width: 1100px; margin: auto; padding: 20px; border: 1px solid #dcdcdc; border-radius: 10px; background-color: #f9f9f9; font-family: Arial;">

        <h2 style="color: #2c3e50; margin-bottom: 20px;">Employee Punch Reason Update
        </h2>

        <table style="margin-bottom: 20px;">
            <tr>
                <td style="padding-right: 10px; font-weight: bold;">Select Date :
                </td>

                <td style="padding-right: 10px;">

                    <asp:TextBox ID="txtDate"
                        runat="server"
                        TextMode="Date"
                        CssClass="form-control"
                        Style="padding: 6px; width: 180px;">
                    </asp:TextBox>

                </td>

                <td>

                    <asp:Button ID="btnShow"
                        runat="server"
                        Text="Show Punch"
                        OnClick="btnShow_Click"
                        BackColor="#007bff"
                        ForeColor="White"
                        BorderStyle="None"
                        Style="padding: 8px 18px; border-radius: 5px; cursor: pointer;" />

                </td>
                <td style="width:30px">

                </td>
                <td>

                    <asp:Button ID="btnExport"
                        runat="server"
                        Text="Export"
                        OnClick="btnExport_Click"
                        BackColor="#007bff"
                        ForeColor="White"
                        BorderStyle="None"
                        Style="padding: 8px 18px; border-radius: 5px; cursor: pointer;" />

                </td>
            </tr>
        </table>

        <asp:GridView ID="gvPunch"
            runat="server"
            AutoGenerateColumns="false"
            Width="100%"
            BorderWidth="0"
            GridLines="None"
            CellPadding="8"
            HeaderStyle-BackColor="#007bff"
            HeaderStyle-ForeColor="White"
            HeaderStyle-Font-Bold="true"
            RowStyle-BackColor="White"
            AlternatingRowStyle-BackColor="#f2f2f2"
            Style="border: 1px solid #ddd; border-radius: 8px; overflow: hidden;">

            <Columns>

                <asp:BoundField DataField="SrNo"
                    HeaderText="Sr No">
                    <ItemStyle Width="20px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="No_"
                    HeaderText="Employee No">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="First Name"
                    HeaderText="Employee Name">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="PunchTime"
                    HeaderText="Punch Time">
                    <ItemStyle Width="100px" />
                </asp:BoundField>

                <asp:BoundField DataField="LocationName"
                    HeaderText="Location">
                    <ItemStyle Width="100px" />
                </asp:BoundField>

                <asp:TemplateField HeaderText="Reason">
                    <ItemTemplate>

                        <asp:TextBox ID="txtReason"
                            runat="server"
                            Width="100%"
                            TextMode="MultiLine"
                            Rows="2"
                            Text='<%# Eval("Reason") %>'
                            Enabled='<%# Eval("Reason").ToString() == "" %>'
                            Style="padding: 6px; border: 1px solid #ccc; border-radius: 5px;">
        </asp:TextBox>

                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Action">

                    <ItemTemplate>

                        <asp:HiddenField ID="hdnMachineID"
                            runat="server"
                            Value='<%# Eval("NodeID") %>' />

                        <asp:Button ID="btnSave"
                            runat="server"
                            Text="Save"
                            CommandArgument='<%# Eval("PunchTime") %>'
                            OnClick="btnSave_Click"
                            BackColor="#28a745"
                            ForeColor="White"
                            BorderStyle="None"
                            Visible='<%# Eval("Reason").ToString() == "" %>'
                            Style="padding: 7px 16px; border-radius: 5px; cursor: pointer;" />

                    </ItemTemplate>

                    <ItemStyle Width="120px" HorizontalAlign="Center" />

                </asp:TemplateField>

            </Columns>

        </asp:GridView>

    </div>

</asp:Content>

