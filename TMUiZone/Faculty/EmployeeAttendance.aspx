<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="EmployeeAttendance.aspx.cs" Inherits="Faculty_EmployeeAttendance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
      <table cellpadding="0px" cellspacing="0px" style="width:100%">
            <tr> <td style="height:13px"> </td></tr>
         <tr> <td>  


      &nbsp;&nbsp;&nbsp; &nbsp;  <asp:Label ID="Label3" runat="server" 
            Text="Team Attendance" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
                                                     </td></tr> 

            <tr> <td style="height:13px"> </td></tr>


        <tr> <td class="leftm">  </td></tr>

        <tr> <td style="height:13px"> </td></tr>

        <tr> <td align="center">  <table cellpadding="0px" cellspacing="0px"> <tr> <td> From Date</td> <td style="width:10px"> </td> <td> 
           <asp:TextBox ID="txtfromDate" runat="server"></asp:TextBox>
             <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfromDate" Format="dd MMM yyyy"></asp:CalendarExtender>
                                                             
                  </td>  <td style="width:10px"> </td> <td>  To Date </td> <td style="width:10px"> </td> 
            <td> <asp:TextBox ID="txtTodate" runat="server"></asp:TextBox>
                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd MMM yyyy" TargetControlID="txtTodate">
                                            </asp:CalendarExtender>

            </td>
            
             <td style="width:10px"> </td> 
            
            <td> <asp:Button ID="btnGet" runat="server" Text="Get" OnClick="btnGet_Click" /> </td><td style="width:10px"> </td>
            <td>  <asp:Button ID="btnexporttoexcel" runat="server" Text="Export To Excel" OnClick="btnexporttoexcel_Click" />

            </td> </tr> </table> </td></tr>

            

                <tr> <td>

                    <table cellpadding="0px" cellspacing="0px" style="width:100%">  <tr> <td style="width:10px">  </td>
                        
                         <td>  <asp:GridView ID="grd_ViewAttendance" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4"  CssClass="table table-striped table-bordered table-hover">
                        <Columns>
                            <asp:BoundField DataField="Employee No" HeaderText="Employee Code" >
                           
                            </asp:BoundField>
                            <asp:BoundField DataField="Employee Name" HeaderText="Employee Name" />
                             <asp:BoundField DataField="Designation" HeaderText="Designation" />
                           <asp:BoundField DataField="HOD" HeaderText="HOD Name" />
                            <asp:BoundField DataField="Attendance Date" HeaderText="Attendance Date" />
                            <asp:BoundField DataField="Status" HeaderText="Attendance Status" />
                             

                           
                        </Columns>
                        <EmptyDataTemplate>
                            There is no record found
                        </EmptyDataTemplate>
                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                        <HeaderStyle HorizontalAlign="Center" Height="20px" BackColor="#ff9900"  ForeColor="White" />
                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" ForeColor="#330099" HorizontalAlign="Left"/>
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                        <SortedAscendingCellStyle BackColor="#FEFCEB" />
                        <SortedAscendingHeaderStyle BackColor="#AF0101" />
                        <SortedDescendingCellStyle BackColor="#F6F0C0" />
                        <SortedDescendingHeaderStyle BackColor="#7E0000" />
                      
                    </asp:GridView>
 </td> <td style="width:10px"> </td></tr> </table>
                   

                     </td></tr>

                <tr> <td style="height:90px"> </td></tr>
    </table>



</asp:Content>

