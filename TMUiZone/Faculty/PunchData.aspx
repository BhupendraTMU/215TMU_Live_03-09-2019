<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="PunchData.aspx.cs" Inherits="Faculty_PunchData" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Employee Punch Detail" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <table>
        <tr style="height: 20px">
            <td></td>
        </tr>
       
                      
                                <tr>
                                    <%--<td>Month</td>
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
                                    <td style="width: 10px"></td>--%>
                                   <%-- <td>Year </td>
                                    <td style="width: 10px"></td>--%>
                                    <td>
                                        <asp:DropDownList ID="ddlYear1" runat="server" Height="29px" Visible="false"></asp:DropDownList>
                                    </td>
                                   
                                   <%-- <td style="width: 10px"></td>
                                    <td>Designation </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:DropDownList ID="drpDesignation" runat="server" Width="100px" Height="29px"></asp:DropDownList>
                                    </td>--%>
                                    <td style="width: 10px"></td>
                                    
                                    <td style="width:10px"></td>
                                     <td>Department </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:DropDownList ID="drpDepartment" runat="server" Width="100px" Height="29px"></asp:DropDownList>
                                    </td>
                                    <td style="width: 10px"></td>
                                     <td>
                                        <asp:Button ID="btnGet" runat="server" Text="Show" OnClick="btnGet_Click" />
                                    </td>
                                    <td style="width:10px"></td>
                                   
                                
           
        </tr>
    </table>

    <br />
    <asp:GridView ID="grdPunchData" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
        BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
        GridLines="Horizontal" EmptyDataText="There are no data records to display."
        AllowSorting="true">
        <AlternatingRowStyle BackColor="#F7F7F7" />
        <Columns>
            <asp:TemplateField HeaderText="Sl. No.">

                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
                <ItemStyle Width="2%" />
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Employee Code">
                <ItemTemplate>
                    <asp:Label ID="lblUserID" runat="server" Text='<%# Eval("Userid") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="7%" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Employee Name">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("Uname") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="7%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="HOD ID">
                <ItemTemplate>
                    <asp:Label ID="lblHODID" runat="server" Text='<%# Eval("HODUserID") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="10%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="HOD Name">
                <ItemTemplate>
                    <asp:Label ID="lblHODName" runat="server" Text='<%# Eval("HODName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="10%" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Month">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("Month") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="7%" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Year">
                <ItemTemplate>
                    <asp:Label ID="lblYear" runat="server" Text='<%# Eval("Year") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="7%" />
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Total Punch">
                <ItemTemplate>
                    <asp:LinkButton ID="Label2" runat="server" Text='<%# Eval("Punch Count") %>' OnClick="BtnPresent_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="7%" />
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
        <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
        <SortedAscendingCellStyle BackColor="#F4F4FD" />
        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
        <SortedDescendingCellStyle BackColor="#D8D8F0" />
        <SortedDescendingHeaderStyle BackColor="#3E3277" />

    </asp:GridView>
    
                    <asp:Panel ID="pnlGridViewDetails" CssClass="modalPopup" Width="65%" runat="server" Style="display: none;" >
      
                         <div class="header">
       <b> <asp:Label ID="lblNotification" runat="server" Text="Attendance Detail"></asp:Label></b><div class="close"><asp:Button ID="btnclose" OnClick="btnclose_Click"  runat="server" Text="X" /></div>
    </div>
                        <div id="Div1" runat="server" style="max-height: 1000px; overflow: auto;">
                       
       <div  class="body" >
           <div style="width:100%">
               <center>
           
                   <br />
               <asp:GridView ID="grdAttandanceDetails" Width="866px" EmptyDataText="There are no data records to display." runat="server">
                   <Columns>
                        <asp:TemplateField HeaderText="Sl. No.">
                                                            <ItemTemplate >
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="7%" />
                                                        </asp:TemplateField>
                   </Columns>
                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
               <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
               <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
               <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont"   />
               <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
               <SortedAscendingCellStyle BackColor="#F4F4FD" />
               <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
               <SortedDescendingCellStyle BackColor="#D8D8F0" />
               <SortedDescendingHeaderStyle BackColor="#3E3277" />

               </asp:GridView></center>
           </div>

       </div>

                        </div>
                        </asp:Panel>
                <asp:Button ID="btnDummy" runat="server" Style="display: none;" />
    <asp:ModalPopupExtender ID="GridViewDetails" runat="server" TargetControlID="btnDummy"
        PopupControlID="pnlGridViewDetails" BackgroundCssClass="modalBackground" />
<br />
    <br />
</asp:Content>

