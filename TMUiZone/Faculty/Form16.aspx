<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="Form16.aspx.cs" EnableEventValidation="false" Inherits="Faculty_Form16" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Form 16" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <table>
        <tr style="height: 20px">
            <td></td>
        </tr>
        <tr>
            <td style="width: 20px"></td>
            <td style="font-size: larger">Financial Year:</td>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="drpacsession" Height="30px" Width="100px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpacsession_SelectedIndexChanged">
                    <asp:ListItem Text="SELECT" Value="0"></asp:ListItem>
                    <asp:ListItem Text="2021-2022" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2022-2023" Value="2"></asp:ListItem>
                    <asp:ListItem Text="2023-2024" Value="3"></asp:ListItem>
                    <asp:ListItem Text="2024-2025" Value="4"></asp:ListItem>
                    <asp:ListItem Text="2025-2026" Value="5"></asp:ListItem>
                </asp:DropDownList>


            </td>


            <%-- <td>
                &nbsp;&nbsp;&nbsp;
               <asp:Button ID="btnGetFiles" Text="Get Files From Folder" runat="server" OnClick="btnGetFiles_Click" />
            </td>--%>
            <%-- <td>
                &nbsp;&nbsp;&nbsp;
               <asp:Button ID="btndowload" Text="Download" runat="server" OnClick="btndowload_Click"/>
            </td>--%>
        </tr>
    </table>
    <br />
    <asp:GridView ID="grdmemberapprovallist" runat="server" AlternatingRowStyle-CssClass="danger" PageSize="50"
        AllowPaging="true" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" Visible="true">
        <PagerSettings Mode="NumericFirstLast" />
        <PagerStyle CssClass="csspager" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <%# Container.DataItemIndex +1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Employee Code" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:Label ID="lblemployeecode" runat="server" Text='<%# Bind("No_") %>'></asp:Label>
                    <asp:HiddenField ID="Hfemployeecode" Value='<%# Eval("No_") %>' runat="server" />
                    <asp:HiddenField ID="Hfhodname" Value='<%# Eval("No_") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Employee Name" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                <ItemTemplate>
                    <asp:Label ID="lblemployeename" runat="server" Text='<%# Eval("First Name") %>' Style="text-transform: uppercase;"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Designation" ItemStyle-Width="3%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                <ItemTemplate>
                    <asp:Label ID="lbldesignation" runat="server" Text='<%# Eval("[Job Title_Grade Desc]") %>' Style="text-transform: uppercase;"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Department" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:Label ID="lbldepartment" runat="server" Text='<%#Eval("[Department Name]") %>' Style="text-transform: uppercase;"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PAN NO" ItemStyle-Width="2%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:Label ID="lblprincipalapproval" runat="server" Text='<%# Eval("[PAN No]") %>'></asp:Label>

                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Aadhar Card" ItemStyle-Width="2%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:Label ID="lblAadharcard" runat="server" Text='<%# Eval("[Aadhar Card]") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Form 16 (2021-2022)" ItemStyle-Width="2%" HeaderStyle-CssClass="text-center" Visible="false" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonDownloadPdf" runat="server" Text="Download PDF" Style="color: Navy; font-weight: bold;"
                        OnClick="LinkButtonDownloadPdf_Click" />
                </ItemTemplate>
            </asp:TemplateField>
            <%-- <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" HeaderText="Select" ItemStyle-CssClass="text-center">                   
                    <ItemTemplate>
                        <asp:CheckBox ID="Chkemployee" Enabled='<%# Eval("txtMarksEnableDesable").ToString().Equals("true") %>'  runat="server" AutoPostBack="true"  />
                    </ItemTemplate>
                </asp:TemplateField>--%>

            <asp:TemplateField HeaderText="Form 16 (2022-2023)" ItemStyle-Width="2%" HeaderStyle-CssClass="text-center" Visible="false" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonDownloadPdf23" runat="server" Text="Download PDF" OnClick="LinkButtonDownloadPdf23_Click" Style="color: Navy; font-weight: bold;" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Form 16 (2023-2024)" ItemStyle-Width="2%" HeaderStyle-CssClass="text-center" Visible="false" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonDownloadPdf24" runat="server" Text="Download PDF" OnClick="LinkButtonDownloadPdf24_Click" Style="color: Navy; font-weight: bold;" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Form 16 (2024-2025)" ItemStyle-Width="2%" HeaderStyle-CssClass="text-center" Visible="false" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonDownloadPdf25" runat="server" Text="Download PDF" OnClick="LinkButtonDownloadPdf25_Click" Style="color: Navy; font-weight: bold;" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Form 16 (2025-2026)" ItemStyle-Width="2%" HeaderStyle-CssClass="text-center" Visible="false" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonDownloadPdf26" runat="server" Text="Download PDF" OnClick="LinkButtonDownloadPdf26_Click" Style="color: Navy; font-weight: bold;" />
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

