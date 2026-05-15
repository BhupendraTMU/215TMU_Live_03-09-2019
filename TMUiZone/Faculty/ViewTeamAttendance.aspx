<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="ViewTeamAttendance.aspx.cs" Inherits="Faculty_ViewAttendance" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/css/select2.min.css" rel="stylesheet" />
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/js/select2.min.js"></script>

   <script type="text/javascript">
    $(document).ready(function () {
        $('.searchable').select2({
            placeholder: "Select User",
            allowClear: true,
            width: '250px'
        });
    });

    // Fix for UpdatePanel postback
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
        $('.searchable').select2({
            placeholder: "Select User",
            allowClear: true,
            width: '250px'
        });
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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

        <tr> <td align="center">  <table cellpadding="0px" cellspacing="0px"> <tr> <td>  Month</td> <td style="width:10px"> </td> <td> <asp:DropDownList ID="ddlMonth" runat="server" Height="29px">
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
                    </asp:DropDownList></td>  <td style="width:10px"> </td> <td>  Year </td> <td style="width:10px"> </td> <td>
                         <asp:DropDownList ID="ddlYear" runat="server" Height="29px"></asp:DropDownList> </td> <td style="width:10px"> </td>
              <td> Employee </td> <td style="width:10px">  </td>
            <td> <asp:DropDownList ID="txtUserid" runat="server" Height="28px" CssClass="searchable">

                                                                     </asp:DropDownList> </td> <td style="width:10px"> </td> <td> 
                                                                         <asp:Button ID="btnGet" runat="server" Text="Get" OnClick="btnGet_Click" /> </td><td style="width:10px"> </td>
            <td> <asp:Button ID="btnexporttoexcel" runat="server" Text="Export To Excel" OnClick="btnexporttoexcel_Click" /></td> 
            <td> <asp:Button ID="btnConsolidate" runat="server" Text="Consolidate Attendance" OnClick="btnConsolidate_Click" /></td> 

                                                                              </tr> </table> </td></tr>

                <tr> <td style="height:13px"> 
                    <asp:Label ID="lblMonth" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lbleyear" runat="server" Visible="False"></asp:Label>
                    </td></tr>

                <tr> <td>

                    <table cellpadding="0px" cellspacing="0px" style="width:100%">  <tr> <td style="width:10px">  </td> <td> 
                        <asp:GridView ID="grd_ViewAttendance" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnRowDataBound="grd_ViewAttendance_RowDataBound" CssClass="table table-striped table-bordered table-hover">
                        <Columns>
                            <asp:BoundField DataField="Attendance Date" HeaderText="Date" >                           
                            </asp:BoundField>
                            <asp:BoundField DataField="Week Day" HeaderText="Week Day" />
                           <asp:BoundField DataField="ShiftTime" HeaderText="Shift Time" />
                            <asp:BoundField DataField="Time From" HeaderText="In Time" />
                            <asp:BoundField DataField="Time To" HeaderText="Out Time" />
                             <asp:BoundField DataField="WorkingHour" HeaderText="Working Hour" />
                            <asp:BoundField DataField="LateBy" HeaderText="Late BY" />                         
                            <asp:BoundField DataField="EarlyBy" HeaderText="Early BY" />
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("Status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField DataField="shiftTimeIn" HeaderText="ShiftInTime" Visible="false"/>
                            <asp:BoundField DataField="ShiftTimeOut" HeaderText="ShiftOutTime"  Visible="false"/>
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
                        <asp:GridView ID="GridView1" runat="server" Visible="false"  AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnRowDataBound="GridView1_RowDataBound" CssClass="table table-striped table-bordered table-hover">
                        <Columns>
                             <asp:BoundField DataField="Employee No" HeaderText="Employee No_" >                           
                            </asp:BoundField>
                             <asp:BoundField DataField="Employee Name" HeaderText="Employee Name" >                           
                            </asp:BoundField>
                            <asp:BoundField DataField="Attendance Date" HeaderText="Date" >                           
                            </asp:BoundField>
                            <asp:BoundField DataField="Week Day" HeaderText="Week Day" />
                           <asp:BoundField DataField="ShiftTime" HeaderText="Shift Time" />
                            <asp:BoundField DataField="Time From" HeaderText="In Time" />
                            <asp:BoundField DataField="Time To" HeaderText="Out Time" />
                             <asp:BoundField DataField="WorkingHour" HeaderText="Working Hour" />
                            <asp:BoundField DataField="LateBy" HeaderText="Late BY" />                         
                            <asp:BoundField DataField="EarlyBy" HeaderText="Early BY" />
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("Status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField DataField="shiftTimeIn" HeaderText="ShiftInTime" Visible="false"/>
                            <asp:BoundField DataField="ShiftTimeOut" HeaderText="ShiftOutTime"  Visible="false"/>
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

