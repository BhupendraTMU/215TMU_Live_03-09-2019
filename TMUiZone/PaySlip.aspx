<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaySlip.aspx.cs" Inherits="PaySlip" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pay Slip</title>

    <link href="css/mainpage.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="wrap"> 
        <%--    <table cellpadding="0px" cellspacing="0px" style="width:100%"> <tr> <td> <asp:Image ID="imgPhoto" runat="server" /> </td>  <td>  </td> <td> <asp:Label ID="lblCompanyAddress" runat="server" Text=""></asp:Label> </td> <td> <table cellpadding="0px" cellspacing="0px"> <tr> <td>  <asp:Label ID="Label2" runat="server" Text="Pay Slip for the month of " Font-Size="15pt"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td> <td> <asp:Label ID="lblCalenderDate" runat="server" Font-Size="15pt"></asp:Label>  </td></tr></table>  </td></tr> </table>--%>
        <table cellpadding="0px" cellspacing="0px" style="width:100%" class="table1">  

            <tr> <td style="height:100px" align="center">  <asp:Image ID="imgPhoto" runat="server" Width="100px" Height="80px" style="margin-bottom: 0px" />  </td>  <td class="tblleft"> </td> <td align="center"> <asp:Label ID="lblCompanyAddress" runat="server" Text="company Addresss ddddddddddddddddddd"></asp:Label> </td> <td class="tblleft"> </td>  <td align="center">  <table cellpadding="0px" cellspacing="0px"> <tr> <td>  <asp:Label ID="Label2" runat="server" Text="Pay Slip for the month of " Font-Size="15pt"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td> <td> <asp:Label ID="lblCalenderDate" runat="server" Font-Size="15pt"></asp:Label>  </td></tr></table> </td></tr>


            <tr> <td colspan="5" class="linecs">  </td></tr>



             <tr> <td >  <table cellpadding="0px" cellspacing="0px">  <tr> <td>&nbsp;&nbsp;  Employee ID :&nbsp;&nbsp; </td>  <td> <asp:Label ID="lblEmployeeid" runat="server" Text=""></asp:Label></td></tr></table>   </td>  <td class="tblleft"> </td> <td >   <table cellpadding="0px" cellspacing="0px">  <tr> <td> &nbsp;&nbsp; Employee Name :&nbsp;&nbsp; </td>  <td>  <asp:Label ID="lblEmployeeName" runat="server" Text=""></asp:Label></td></tr></table></td> <td class="tblleft"> </td>  <td>   <table cellpadding="0px" cellspacing="0px">  <tr> <td> &nbsp;&nbsp;Days Paid :&nbsp;&nbsp; </td>  <td>  <asp:Label ID="lblDaysPaid" runat="server" Text=""></asp:Label></td> <td style="width:30px"> </td> <td> LWP :&nbsp;&nbsp; </td>  <td>  <asp:Label ID="lbllwp" runat="server" Text=""></asp:Label></td></tr></table> </td></tr>

            <tr> <td colspan="5" class="linecs">  </td></tr>

             <tr> <td >  <table cellpadding="0px" cellspacing="0px">  <tr> <td>&nbsp;&nbsp;  Pay Mode :&nbsp;&nbsp; </td>  <td> <asp:Label ID="lblPayMode" runat="server" Text=""></asp:Label></td></tr></table>   </td>  <td class="tblleft"> </td> <td >   <table cellpadding="0px" cellspacing="0px">  <tr> <td> &nbsp;&nbsp; Designation :&nbsp;&nbsp; </td>  <td>  <asp:Label ID="lblDesignation" runat="server" Text=""></asp:Label></td></tr></table></td> <td class="tblleft"> </td>  <td>   <table cellpadding="0px" cellspacing="0px">  <tr> <td> &nbsp;&nbsp;Department :&nbsp;&nbsp; </td>  <td>  <asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label></td> </tr></table> </td></tr>
            <tr> <td colspan="5" class="linecs">  </td></tr>
             <tr> <td >  <table cellpadding="0px" cellspacing="0px">  <tr> <td>&nbsp;&nbsp;  DOJ :&nbsp;&nbsp; </td>  <td> <asp:Label ID="lblDOJ" runat="server" Text=""></asp:Label></td></tr></table>   </td>  <td class="tblleft"> </td> <td >   <table cellpadding="0px" cellspacing="0px">  <tr> <td> &nbsp;&nbsp; Account No :&nbsp;&nbsp; </td>  <td>  <asp:Label ID="lblAcoountNo" runat="server" Text=""></asp:Label></td></tr></table></td> <td class="tblleft"> </td>  <td>   <table cellpadding="0px" cellspacing="0px">  <tr> <td> &nbsp;&nbsp;Location :&nbsp;&nbsp; </td>  <td>  <asp:Label ID="lblLocation" runat="server" Text=""></asp:Label></td> </tr></table> </td></tr>
            <tr> <td colspan="5" class="linecs">  </td></tr>

             <tr> <td >  <table cellpadding="0px" cellspacing="0px">  <tr> <td>&nbsp;&nbsp;  PAN No :&nbsp;&nbsp; </td>  <td> <asp:Label ID="lblPanNo" runat="server" Text=""></asp:Label></td></tr></table>   </td>  <td class="tblleft"> </td> <td >   <table cellpadding="0px" cellspacing="0px">  <tr> <td> &nbsp;&nbsp; PF No :&nbsp;&nbsp; </td>  <td>  <asp:Label ID="lblPFNo" runat="server" Text=""></asp:Label></td></tr></table></td> <td class="tblleft"> </td>  <td>   <table cellpadding="0px" cellspacing="0px">  <tr> <td> &nbsp;&nbsp;ESI No :&nbsp;&nbsp; </td>  <td>  <asp:Label ID="lblESINo" runat="server" Text=""></asp:Label></td> </tr></table> </td></tr>
            <tr> <td colspan="5" class="linecs">  </td></tr>
             <tr> <td colspan="5" style="height:20px"> 



                  </td></tr>

              <tr> <td colspan="5" class="linecs">  </td></tr>


            
              <tr> <td colspan="5">

                  <asp:GridView ID="grdSalary" runat="server" AutoGenerateColumns="False" ShowFooter="True" Width="100%" CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                      <Columns>
                          <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Center">
                              <ItemTemplate>
                                  <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                              </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Rate (Rs.)" HeaderStyle-HorizontalAlign="Center">
                              <ItemTemplate >
                                  <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                              </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Earning (Rs.) " HeaderStyle-HorizontalAlign="Center">
                              <ItemTemplate>
                                  <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                              </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Arrear Amt (Rs.) " HeaderStyle-HorizontalAlign="Center">
                              <ItemTemplate>
                                  <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
                              </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Total Amt (Rs.) " HeaderStyle-HorizontalAlign="Center">
                              <ItemTemplate>
                                  <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                              </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                          </asp:TemplateField>
                      </Columns>
                      <FooterStyle BackColor="White" ForeColor="#000066" />
                      <HeaderStyle  Font-Bold="True" ForeColor="Black" />
                      <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                      <RowStyle  HorizontalAlign="Center" ForeColor="#000066" />
                      <SelectedRowStyle Font-Bold="True" ForeColor="White" />
                      <SortedAscendingCellStyle BackColor="#F1F1F1" />
                      <SortedAscendingHeaderStyle BackColor="#007DBB" />
                      <SortedDescendingCellStyle BackColor="#CAC9C9" />
                      <SortedDescendingHeaderStyle BackColor="#00547E" />
                  </asp:GridView>

                   </td></tr>
              <tr> <td colspan="5" style="height:20px"> 



                  </td></tr>

              <tr> <td colspan="5" class="linecs">  </td></tr>

            <tr> <td colspan="5">

                <table cellpadding="0px" cellspacing="0px" style="width:100%">  <tr> <td align="right"> Total Earnings&nbsp;&nbsp;&nbsp;&nbsp; </td> <td align="right">
                    <asp:Label ID="lblTotalEarning" runat="server" Text=""></asp:Label>  </td></tr> </table>

                 </td></tr>
              <tr> <td colspan="5">

                <table cellpadding="0px" cellspacing="0px" style="width:100%">  <tr> <td align="right"> Net Payable&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td> <td align="right">
                    <asp:Label ID="lblNetPayable" runat="server" Text=""></asp:Label>  </td></tr> </table>

                 </td></tr>
               <tr> <td colspan="5" class="linecs">  </td></tr>


               <tr> <td colspan="5" style="height:20px" align="center"> <asp:Label ID="lblTextRupees" runat="server" Text=""></asp:Label> </td></tr>
              <tr> <td colspan="5" class="linecs">  </td></tr>


             <tr> <td colspan="5" style="height:20px" align="center"> <asp:Label ID="Label1" runat="server" Text="This is computer generated sheet hence no signatures required."></asp:Label> </td></tr>

        </table>




    </div>



    </div>
    </form>
</body>
</html>
