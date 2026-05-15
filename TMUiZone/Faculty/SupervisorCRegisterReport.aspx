<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="SupervisorCRegisterReport.aspx.cs" Inherits="Faculty_SupervisorCRegisterReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 202px;
        }
        .auto-style2 {
            width: 385px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="SUPERVISOR COMPLAIN REGISTER REPORT" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
</fieldset>
    <table class="boxBody" width="1200px">
         <tr>


                                         <td style="width:15PX"></td>
                                        <td>From Date
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:TextBox ID="txtFromDate" runat="server" BorderColor="Black" Width="200px" onkeydown="return false;" autocomplete="off" oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" AutoPostBack="True" OnTextChanged="txtFromDate_TextChanged"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate" Format="dd MMM yyyy"></asp:CalendarExtender>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromDate" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>


                                        </td>
                                        <td style="width: 10px"></td>
                                        <td>&nbsp;To Date</td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:TextBox ID="txtTodate" runat="server" AutoPostBack="True" BorderColor="Black" oncontextmenu="return false" autocomplete="off" oncopy="return false" oncut="return false" onkeydown="return false;" onpaste="return false" OnTextChanged="txtTodate_TextChanged" Width="200px"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd MMM yyyy" TargetControlID="txtTodate">
                                            </asp:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtTodate" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                        </td>

                  <td>
                <asp:Button ID="btnShow" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px" Width="90px" Text="Show Report" OnClick="btnShow_Click" />
                </td>
            <td>
                <asp:Button runat="server" CssClass="btn-sm btn-primary btn-block" Text="Export to Excel" Height="30px" Width="120px" ID="btnexporttoexel" OnClick="btnexporttoexel_Click"/>
            </td>
           </tr>
         </table>
    <br />
     <table class="boxBody" width="1200px">
                                        <td class="auto-style1"> &nbsp;&nbsp;Search By Supervisor.
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td class="auto-style2">
                                            <asp:TextBox ID="txtsupervisor" runat="server" BorderColor="Black" Width="200px" autocomplete="off"
                                                oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                                           
                                        </td>

        <td>
            <asp:Button ID="Search" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px" Width="90px" Text="Show Report" OnClick="Search_Click"/>
        </td>
           </tr>
         </table>
    <br />
    <div style="height: 250px; width: 1190px; overflow: scroll;">
          <asp:GridView ID="grdcomplainregister" runat="server" DataKeyNames="id" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
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

                  <asp:TemplateField HeaderText="Complaint By">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="130px" Text='<%# Eval("Employee_Name") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Employee Code">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="130px" Text='<%# Eval("Employee_Code") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />

                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Date Of Complaint">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="130px" Text='<%# Eval("Date_Of_Complaint") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Floor Name">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="150px" Text='<%# Eval("Floor_Name") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Ward Name">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="130px" Text='<%# Eval("Ward_Name") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Room No">
                      <ItemTemplate>

                          <asp:Label runat="server" Width="130px" Text='<%# Eval("Room_No") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Type Of Complaint">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="150px" Text='<%# Eval("Type_Of_Complaint") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>
                   <asp:TemplateField HeaderText="Actual Complaint">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="130px" Text='<%# Eval("Actual_Complaint") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Person Responsible To Solve Complain">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="130px" Text='<%# Eval("Person_Responsible_To_Solve_Complaint") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Complaint FWD. On Date">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="130px" Text='<%# Eval("Complaint_FWD_On_Date") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Complaint Resolved On Date">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="150px" Text='<%# Eval("Complaint_Resolved_On_Date") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Sig Of Manager After Confirmation">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="150px" Text='<%# Eval("Signature_Of_Manager_After_Confirmation") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Remark">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="150px" Text='<%# Eval("Remark") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>
                   <asp:TemplateField HeaderText="Remainder1">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="150px" Text='<%# Eval("One_OnDate") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>
                   <asp:TemplateField HeaderText="Remainder2">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="150px" Text='<%# Eval("Two_OnDate") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>
                   <asp:TemplateField HeaderText="Remainder3">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="150px" Text='<%# Eval("Three_OnDate") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>
                   <asp:TemplateField HeaderText="Remainder4">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="150px" Text='<%# Eval("More_Than_Three") %>'></asp:Label>
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

