<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="DailyRoundReport.aspx.cs" Inherits="Faculty_DailyRoundReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 219px
        }
        .auto-style2 {
            width: 317px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="WARD ASSISTANT'S SUPERVISOR DAILY ROUND REPORT" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
</fieldset>
    <table class="boxBody" width="1200px">
         

                                      
                                        <td> &nbsp;&nbsp;From Date
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:TextBox ID="txtFromDate" runat="server" Width="200px" onkeydown="return false;" autocomplete="off"
                                                oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" BorderColor="Black" AutoPostBack="True" OnTextChanged="txtFromDate_TextChanged"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate" Format="dd MMM yyyy"></asp:CalendarExtender>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromDate" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>


                                        </td>
                                        <td style="width: 10px"></td>
                                        <td>&nbsp;To Date</td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:TextBox ID="txtTodate" runat="server" AutoPostBack="True" oncontextmenu="return false" autocomplete="off" oncopy="return false" oncut="return false" onkeydown="return false;" onpaste="return false" BorderColor="Black" OnTextChanged="txtTodate_TextChanged" Width="200px"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd MMM yyyy" TargetControlID="txtTodate">
                                            </asp:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtTodate" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                        </td>







               <td>
                <asp:Button ID="btnShow" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px" Width="90px" Text="Show Report" OnClick="btnShow_Click"/>
                </td>
            <td>
                <asp:Button runat="server" CssClass="btn-sm btn-primary btn-block" Text="Export to Excel" Height="30px" Width="120px" ID="btnexporttoexel" OnClick="btnexporttoexel_Click" />
            </td>
           </tr>
         </table>
    <br />
    <table class="boxBody" width="1200px">
         

                                      <tr>
                                        <td class="auto-style1"> &nbsp;&nbsp;Search By Ward Assistant Name.
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td class="auto-style2">
                                            <asp:TextBox ID="txtsupervisor" runat="server" Width="200px" BorderColor="Black" autocomplete="off"
                                                oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                                           

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>


                                        </td>

        <td>
            <asp:Button ID="Search" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px" Width="90px" Text="Show Report" OnClick="Search_Click" />
        </td>
           </tr>
         </table>
    
      <br />
    <div style="height: 500px; width: 1190px; overflow: scroll;">
          <asp:GridView ID="grdroundreport" runat="server" AutoGenerateColumns="False" DataKeyNames="id" BackColor="White" BorderColor="#E7E7FF"
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
                          <asp:Label runat="server" Width="130px" Text='<%# Eval("Supervisor_Name") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Supervisor Code">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="130px" Text='<%# Eval("Supervisor_Code") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />

                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Ward Assistant Name">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="130px" Text='<%# Eval("Ward_Assistant_Name") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>
                 <asp:TemplateField HeaderText="Temp. Ward Assistant Name">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="170px" Text='<%# Eval("Temp_Ward_Assistant_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Floor Name">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="200px" Text='<%# Eval("Floor_Name") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Ward Name">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="130px" Text='<%# Eval("Ward_Name") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Shift">
                      <ItemTemplate>

                          <asp:Label runat="server" Width="130px" Text='<%# Eval("Shift") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Shift Timing">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="200px" Text='<%# Eval("Shift_Timing") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Round Time-1">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="130px" Text='<%# Eval("Round_Time1") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Out Time Round1">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Out_Time1") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="In Time Round1">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("In_Time1") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Place Round1">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Place1") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Transaction Date Round1">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Trans_Date1") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Round Time-2">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="130px" Text='<%# Eval("Round_Time2") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Out Time Round2">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Out_Time2") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                 
                <asp:TemplateField HeaderText="In Time Round2">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("In_Time2") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Place Round2">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Place2") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Transaction Date Round2">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Trans_Date2") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Round Time-3">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="130px" Text='<%# Eval("Round_Time3") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Out Time Round3">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Out_Time3") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                   <asp:TemplateField HeaderText="In Time Round3">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("In_Time3") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Place Round 3">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Place3") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Transaction Date Round3">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Trans_Date3") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Round Time-4">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="130px" Text='<%# Eval("Round_Time4") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Out Time Round4">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Out_Time4") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="In Time Round4">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("In_Time4") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Place Round4">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Place4") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Transaction Date Round4">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Trans_Date4") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Round Time-5">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="130px" Text='<%# Eval("Round_Time5") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Out Time Round5">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Out_Time5") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="In Time Round5">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("In_Time5") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Place Round5">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Place5") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Transaction Date Round5">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Trans_Date5") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Round Time-6">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="130px" Text='<%# Eval("Round_Time6") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Out Time Round6">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Out_Time6") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="In Time Round6">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("In_Time6") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Place Round6">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Place6") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Transaction Date Round6">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Trans_Date6") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Status">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="130px" Text='<%# Eval("Status") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Complain">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="130px" Text='<%# Eval("Complain") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Other Complain">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="150px" Text='<%# Eval("Other_Complain") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>
                   <asp:TemplateField HeaderText="Option to Download Photo">
                    <ItemTemplate>
                     <%--   <asp:Label runat="server" Width="100px" Text='<%# Eval("Upload_Photo") %>'></asp:Label>--%>
                        <asp:LinkButton ID="lnkPhoto" runat="server" ForeColor="Red" Font-Underline="true" Enabled='<%# Eval("Upload_Photo").ToString() == "0" ? false : true %>' OnClick="lnkPhoto_Click"> View Photo</asp:LinkButton>
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