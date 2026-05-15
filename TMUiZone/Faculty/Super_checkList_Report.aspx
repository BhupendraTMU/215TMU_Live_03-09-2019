<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="Super_checkList_Report.aspx.cs" Inherits="Faculty_Super_checkList_Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style2 {
            width: 319px;
        }
        .auto-style3 {
            width: 162px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text=" SUPERVISOR CHECK LIST REPORT" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
</fieldset>
    <table class="boxBody" width="1200px">
         <tr>


            <td style="width:20PX"></td>
                                        <td>From Date
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:TextBox ID="txtFromDate" runat="server" OnTextChanged="txtFromDate_TextChanged" Width="200px" BorderColor="Black" onkeydown="return false;" autocomplete="off"
                                                oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" AutoPostBack="True"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate" Format="dd MMM yyyy"></asp:CalendarExtender>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromDate" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>


                                        </td>
                                        <td style="width: 10px"></td>
                                        <td>&nbsp;To Date</td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:TextBox ID="txtTodate" runat="server" OnTextChanged="txtTodate_TextChanged" AutoPostBack="True" oncontextmenu="return false" autocomplete="off" oncopy="return false" oncut="return false" BorderColor="Black" onkeydown="return false;" onpaste="return false" Width="200px"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd MMM yyyy" TargetControlID="txtTodate">
                                            </asp:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtTodate" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                        </td>

               <td>
                <asp:Button ID="btnShow" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px" Width="90px" Text="Show Report" OnClick="btnShow_Click"/>
                </td>
            <td>
                <asp:Button runat="server" CssClass="btn-sm btn-primary btn-block" Text="Export to Excel" Height="30px" Width="120px" ID="btnexporttoexel" OnClick="btnexporttoexel_Click"/>
            </td>
           </tr>
         </table>
    <br />
    <table class="boxBody" width="1200px">
                                        <td class="auto-style3" style="text-size-adjust:auto"> &nbsp;&nbsp;Search By Supervisor.
                                        </td>
                                        <td class="auto-style2">
                                            <asp:TextBox ID="txtsupervisor" runat="server" BorderColor="Black" Width="200px" autocomplete="off"
                                                oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" AutoPostBack="true"></asp:TextBox>
                                           
                                        </td>

        <td>
            <asp:Button ID="Search" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px" Width="90px" Text="Show Report" OnClick="Search_Click"/>
        </td>
           </tr>
         </table>
    <br />
       <div style="height: 500px; width: 1190px; overflow: scroll;">
          <asp:GridView ID="grdsuperchecklist" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                                        GridLines="Horizontal" EmptyDataText="There are no data records to display."
                                        AllowSorting="true">
         
                                        <AlternatingRowStyle BackColor="#F7F7F7" />
              <Columns>
                  <asp:TemplateField HeaderText="Sl. No.">
                      <ItemTemplate>
                          <%# Container.DataItemIndex + 1 %>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Supervisor Name">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="130px" Text='<%# Eval("Supervisor Name") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Supervisor_Code">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="130px" Text='<%# Eval("Supervisor_Code") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />

                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Patient Detail">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="130px" Text='<%# Eval("Patient Detail") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Report Date">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="150px" Text='<%# Eval("Report Date") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Helper Detail">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="130px" Text='<%# Eval("Helper Detail") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Ward Name">
                      <ItemTemplate>

                          <asp:Label runat="server" Width="130px" Text='<%# Eval("Ward Name") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Question">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="440px" Text='<%# Eval("Question") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>
                   <asp:TemplateField HeaderText="Status">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="130px" Text='<%# Eval("Status") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Remark">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="130px" Text='<%# Eval("Remark") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Ward Room">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="130px" Text='<%# Eval("WardRoom") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Trans Date">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="130px" Text='<%# Eval("Trans_date") %>'></asp:Label>
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
        </div>

</asp:Content>

