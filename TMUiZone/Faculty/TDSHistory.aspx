<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="TDSHistory.aspx.cs" Inherits="Faculty_TDSHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <table cellpadding="0px" cellspacing="0px">
         
             <tr>
                 <td style="height:30px">

                 </td>
             </tr>
                <tr>
                    <td style="width: 35%" colspan="6">
                      &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp <asp:Label ID="Label1" runat="server"
                            Text="TDS History" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                    </td>

                </tr>
             
           <tr>
               <td style="height:22px">

               </td>
              
           </tr>
         
                <tr>
                   <td style="width:20px"></td>
                    <td>Financial Year  &nbsp&nbsp&nbsp&nbsp
                        <asp:DropDownList ID="drpAcademicYear" Width="100px" Height="20px" runat="server" AutoPostBack="true">

                            <asp:ListItem Text="16-17" Value="16-17"></asp:ListItem>
                             <asp:ListItem Text="17-18" Value="17-18"></asp:ListItem>
                             <asp:ListItem Text="18-19" Value="18-19"></asp:ListItem>
                             <asp:ListItem Text="19-20" Value="19-20"></asp:ListItem>
                             <asp:ListItem Text="20-21" Value="20-21"></asp:ListItem>
                             <asp:ListItem Text="21-22" Value="21-22"></asp:ListItem>
                             <asp:ListItem Text="22-23" Value="22-23"></asp:ListItem>
                            
                     </asp:DropDownList>
                    </td>
                    </tr>
         <tr>
              <td style="width:20px"></td>
             <td>
                 <asp:GridView ID="grdResult" runat="server" DataKeyNames="No_" BackColor="White" AutoGenerateColumns="false" ShowFooter="true" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                        GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                        <AlternatingRowStyle BackColor="#F7F7F7" />

                        <Columns>
                            <asp:TemplateField HeaderText="Sr. No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle Width="3%" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Employee Code" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="EmpCode" runat="server" Text='<%#Eval("No_")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Employee Name" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="EmpName" runat="server" Text='<%#Eval("EmpName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Month" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblMonth" runat="server" Text='<%#Eval("Month")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Year" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Year")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            </Columns>
                     </asp:GridView>
             </td>
         </tr>
         </table>


</asp:Content>

