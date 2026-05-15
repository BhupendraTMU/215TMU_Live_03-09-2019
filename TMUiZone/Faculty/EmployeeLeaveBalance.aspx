<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="EmployeeLeaveBalance.aspx.cs" EnableEventValidation="false" Inherits="Faculty_EmployeeLeaveBalance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:Panel ID="pnlReport" runat="server" Visible="false" CssClass="leftBackground">
   <table>
       <tr>
           <td class="auto-style1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Employee Code: &nbsp;&nbsp;&nbsp;
           </td>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <td>
               
               <asp:TextBox ID="txtemployeecode" runat="server" Width="200px" BorderColor="Black" style ="text-transform:uppercase" autocomplete="off"
                   oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>


               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtemployeecode" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>


           </td>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        <td>
            <asp:Button ID="Search" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px" Width="90px" Text="Show" OnClick="Search_Click" />
        </td>
           <td>
               <asp:Button ID="btnexporttoexel" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px" Width="90px" Text=" Export To Exel" OnClick="btnexporttoexel_Click" />
           </td>
       </tr>
         </table>
    
        
<fieldset class="boxBodyInner">
        <br />
        <asp:GridView ID="grdleavebalancelist" runat="server"  AlternatingRowStyle-CssClass="danger" PageSize="50" AllowPaging="true" AutoGenerateColumns="false" OnPageIndexChanging="grdleavebalancelist_PageIndexChanging" CssClass="table table-striped table-bordered table-hover" Visible="true" >
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
                        <asp:Label ID="lblemployeecode" runat="server" Text='<%# Bind("[Employee Code]") %>'></asp:Label>
                    
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Employee Name" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lblemployeename" runat="server" Text='<%# Eval("First Name") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Department Name" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lbldepartmentname" runat="server" Text='<%#Eval("Department Name") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Designation" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblDesignation" runat="server" Text='<%#Eval("Designation") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Leave code" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblleave" runat="server" Text='<%#Eval("Leave code") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Leave Balance" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblBalance" runat="server" Text='<%#Eval("[Balance]") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Pending" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblPending" runat="server" Text='<%#Eval("Pending") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Employee Posting Group" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblemployeepostinggroup" runat="server" Text='<%#Eval("Employee Posting Group") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="HOD Name" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblhodname" runat="server" Text='<%#Eval("HOD Name") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                
            </Columns>
            <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
        </asp:GridView>
          </fieldset>
         </asp:Panel>
    <asp:Panel ID="pnlmsg" runat="server" Visible="false" CssClass="leftBackground">

        <fieldset class="boxBody">
            <asp:Label ID="Label11" runat="server"
                Text="You are not Authorized for this page." Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

        </fieldset>
    </asp:Panel>

</asp:Content>

