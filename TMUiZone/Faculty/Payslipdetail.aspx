<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="Payslipdetail.aspx.cs" Inherits="Payslipdetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type = "text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlSalarySlip.ClientID %>");
            var printWindow = window.open('', '', 'height=500,width=945');
            printWindow.document.write('<html><head><title>DIV Contents</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                
                printWindow.print();
            }, 500);
            return false;
        }
    </script>


    <style type="text/css">
        .auto-style1 {
            height: 18px;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <fieldset class="boxBody">
  <asp:Label ID="Label1" runat="server" Text="View Pay Slip" 
            Font-Size="12pt" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" 
            ForeColor="#093A62"></asp:Label>

 </fieldset>


    <fieldset class="boxBody"> 
        <table cellpadding="0px" cellspacing="0px" style="width:100%"> 
            
             <tr> <td>   Month :  </td>  <td style="width:10px"> </td>  <td>  <asp:DropDownList ID="ddMonth" runat="server" Height="29px">
            <asp:ListItem Value="1">January</asp:ListItem>
            <asp:ListItem Value="2">February</asp:ListItem>
            <asp:ListItem Value="3">March</asp:ListItem>
            <asp:ListItem Value="4">April</asp:ListItem>
            <asp:ListItem Value="5">May</asp:ListItem>
            <asp:ListItem Value="6">June</asp:ListItem>
            <asp:ListItem Value="7">July</asp:ListItem>
            <asp:ListItem Value="8">August</asp:ListItem>
            <asp:ListItem Value="9">September</asp:ListItem>
            <asp:ListItem Value="10">October</asp:ListItem>
            <asp:ListItem Value="11">November</asp:ListItem>
            <asp:ListItem Value="12">December</asp:ListItem>
            </asp:DropDownList> </td><td style="width:10px"> </td><td>  Year :  </td>  <td style="width:10px"> </td>  <td>  <asp:DropDownList ID="ddYear" runat="server" Height="29px"></asp:DropDownList> </td> <td style="width:10px"> </td> <td>  <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" CssClass="btnLogin" />&nbsp;
                <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick = "return PrintPanel();" Visible="False" OnClick="btnPrint_Click" CssClass="btnLogin"/>&nbsp;<asp:Button ID="btnsendEmail" runat="server" Text="Send Email"   OnClick="btnsendEmail_Click" Visible="False" CssClass="btnLogin"/>     
               <%--  <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />--%>
                 </td>   </tr>




            <tr> <td colspan="9" style="height:10px"> </td></tr>

             <tr> <td colspan="9">

                 <div id="pnldata" runat="server"> 

                 <asp:Panel ID="pnlSalarySlip" runat="server" Visible="false">
                   <table cellpadding="0px" cellspacing="0px" style="width:100%;vertical-align:central; border: 1px solid black;"   >  

            <tr> <td style="height:100px;vertical-align:central" align="center" ><br />  <asp:Image ID="imgPhoto" runat="server"  style="margin-bottom: 0px" ImageUrl="~/logo/Logo.jpg" />  <br /><br /></td>  <td style="width:1px;background-color:Black;"> </td> <td align="center" style="vertical-align:central"> <br /> <br /><asp:Label ID="lblCompanyAddress" runat="server" Font-Bold="True"></asp:Label> </td> <td style="width:1px;background-color:Black;"> </td>  <td align="center" style="vertical-align:central">  <table cellpadding="0px" cellspacing="0px" > <tr> <td align="center"><br /> <asp:Label ID="Label2" runat="server" Text="Pay Slip for the month of " Font-Size="13pt" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;<br /> &nbsp;<br />
                <asp:Label ID="lblCalenderDate" runat="server" Font-Bold="True" Font-Size="13pt"></asp:Label>
                &nbsp;&nbsp; </td> </tr></table> </td></tr>


            <tr> <td colspan="5" style="height:1px;background-color:Black;">  </td></tr>



             <tr> <td >  <table cellpadding="0px" cellspacing="0px">  <tr> <td>&nbsp;&nbsp;  Employee ID :&nbsp;&nbsp; </td>  <td> <asp:Label ID="lblEmployeeid" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label></td></tr></table>   </td>  <td style="width:1px;background-color:Black;"> </td> <td >   <table cellpadding="0px" cellspacing="0px">  <tr> <td> &nbsp;&nbsp; Employee Name :&nbsp;&nbsp; </td>  <td>  <asp:Label ID="lblEmployeeName" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label></td></tr></table></td> <td style="width:1px;background-color:Black;"> </td>  <td>   <table cellpadding="0px" cellspacing="0px">  <tr> <td> &nbsp;&nbsp;Days Paid :&nbsp;&nbsp; </td>  <td>  <asp:Label ID="lblDaysPaid" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label></td> <td style="width:30px"> </td> <td> LWP :&nbsp;&nbsp; </td>  <td>  <asp:Label ID="lbllwp" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label></td></tr></table> </td></tr>

            <tr> <td colspan="5" style="height:1px;background-color:Black;">  </td></tr>

             <tr> <td >  <table cellpadding="0px" cellspacing="0px">  <tr> <td>&nbsp;&nbsp;  Pay Mode :&nbsp;&nbsp; </td>  <td> <asp:Label ID="lblPayMode" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label></td></tr></table>   </td>  <td style="width:1px;background-color:Black;"> </td> <td >   <table cellpadding="0px" cellspacing="0px">  <tr> <td> &nbsp;&nbsp; Designation :&nbsp;&nbsp; </td>  <td>  <asp:Label ID="lblDesignation" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label></td></tr></table></td> <td style="width:1px;background-color:Black;"> </td>  <td>   <table cellpadding="0px" cellspacing="0px">  <tr> <td class="auto-style1"> &nbsp;&nbsp;Department :&nbsp;&nbsp; </td>  <td class="auto-style1">  <asp:Label ID="lblDepartment" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label></td> </tr></table> </td></tr>
            <tr> <td colspan="5" style="height:1px;background-color:Black;">  </td></tr>
             <tr> <td >  <table cellpadding="0px" cellspacing="0px">  <tr> <td>&nbsp;&nbsp;  DOJ :&nbsp;&nbsp; </td>  <td> <asp:Label ID="lblDOJ" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label></td></tr></table>   </td>  <td style="width:1px;background-color:Black;"> </td> <td >   <table cellpadding="0px" cellspacing="0px">  <tr> <td> &nbsp;&nbsp; Account No :&nbsp;&nbsp; </td>  <td>  <asp:Label ID="lblAcoountNo" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label></td></tr></table></td> <td style="width:1px;background-color:Black;"> </td>  <td>   <table cellpadding="0px" cellspacing="0px">  <tr> <td> &nbsp;&nbsp;Location :&nbsp;&nbsp; </td>  <td>  <asp:Label ID="lblLocation" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label></td> </tr></table> </td></tr>
            <tr> <td colspan="5" style="height:1px;background-color:Black;">  </td></tr>

             <tr> <td >  <table cellpadding="0px" cellspacing="0px">  <tr> <td>&nbsp;&nbsp;  PAN No :&nbsp;&nbsp; </td>  <td> <asp:Label ID="lblPanNo" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label></td></tr></table>   </td>  <td style="width:1px;background-color:Black;"> </td> <td >   <table cellpadding="0px" cellspacing="0px">  <tr> <td> &nbsp;&nbsp; PF No :&nbsp;&nbsp; </td>  <td>  <asp:Label ID="lblPFNo" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label></td></tr></table></td> <td style="width:1px;background-color:Black;"> </td>  <td>   <table cellpadding="0px" cellspacing="0px">  <tr> <td> &nbsp;&nbsp;ESI No :&nbsp;&nbsp; </td>  <td>  <asp:Label ID="lblESINo" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label></td> </tr></table> </td></tr>
            <tr> <td colspan="5" style="height:1px;background-color:Black;">  </td></tr>
             <tr> <td colspan="5" style="height:20px"> 



                  </td></tr>

             <%-- <tr> <td colspan="5" class="linecs">  </td></tr>--%>


            
              <tr> <td colspan="5">
                  <table cellpadding="0px" cellspacing="0px" style="width:100%"> <tr> <td>   <asp:GridView ID="grdSalary" runat="server" AutoGenerateColumns="False" ShowFooter="True" Width="100%" CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnRowDataBound="grdSalary_RowDataBound">
                      <Columns>
                          <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Center">
                              <ItemTemplate>
                                  <asp:Label ID="Label3" runat="server" Text='<%#Bind("[Pay Element Code]") %>'></asp:Label>
                              </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                          </asp:TemplateField>
                       <%--   <asp:TemplateField HeaderText="Rate (Rs/-)" HeaderStyle-HorizontalAlign="Center">
                              <ItemTemplate >
                                  <asp:Label ID="lblActualAmountsalary" runat="server" Text='<%#Bind("[Actual Amount]","{0:n}") %>'></asp:Label>
                              </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                          </asp:TemplateField>--%>
                
                          <asp:BoundField DataField="Actual Amount" HeaderText="Rate (Rs/-)" DataFormatString="{0:f2}" />
                
                          <asp:BoundField DataField="Payable Amount" HeaderText="Earning (Rs/-)" DataFormatString="{0:f2}" />
                          <asp:BoundField DataField="Payable Amount" DataFormatString="{0:f2}" HeaderText="Arrear Amt (Rs/-)" />
                          <asp:BoundField DataField="Payable Amount" DataFormatString="{0:f2}" HeaderText="Total Amt (Rs/-)" />
                      </Columns>
                      <FooterStyle BackColor="White" ForeColor="Black" BorderColor="Black" BorderStyle="Inset" HorizontalAlign="Center" BorderWidth="1px" Font-Bold="True" />
                      <HeaderStyle  Font-Bold="True" ForeColor="Black" BackColor="White" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" />
                      <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                      <RowStyle  HorizontalAlign="Center" ForeColor="Black"  />
                      <SelectedRowStyle Font-Bold="True" ForeColor="White" />
                      <SortedAscendingCellStyle BackColor="#F1F1F1" />
                      <SortedAscendingHeaderStyle BackColor="#007DBB" />
                      <SortedDescendingCellStyle BackColor="#CAC9C9" />
                      <SortedDescendingHeaderStyle BackColor="#00547E" />
                  </asp:GridView>
</td> </tr> </table>
                
                   </td></tr>
              <tr> <td colspan="5" style="height:20px"> 



                  </td></tr>

              <tr> <td colspan="5" style="height:1px;background-color:Black;">  </td></tr>

            <tr> <td colspan="5">

                <table cellpadding="0px" cellspacing="0px" style="width:100%">  <tr> <td align="right"> Total Earnings&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td> <td align="right">
                    <asp:Label ID="lblTotalEarning" runat="server" Font-Bold="True"></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td></tr> </table>

                 </td></tr>
              <tr> <td colspan="5">

                <table cellpadding="0px" cellspacing="0px" style="width:100%">  <tr> <td align="right"> Net Payablee&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td> <td align="right">
                    <asp:Label ID="lblNetPayable" runat="server" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td></tr> </table>

                 </td></tr>
               <tr> <td colspan="5" style="height:1px;background-color:Black;">  </td></tr>


               <tr> <td colspan="5" style="height:20px" align="center"> <asp:Label ID="lblTextRupees" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label> </td></tr>
              <tr> <td colspan="5" style="height:1px;background-color:Black;">  </td></tr>


             <tr> <td colspan="5" style="height:20px" align="center"> <asp:Label ID="Label8" runat="server" Text="This is computer generated sheet hence no signatures required."></asp:Label> </td></tr>
 <tr> <td colspan="5" style="height:1px;background-color:Black;">  </td></tr>

                        <tr> <td colspan="5">
                           <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <cc1:Editor ID="Editor1" runat="server" />--%>


                             </td></tr>

        </table>

                 </asp:Panel>

                      </div>

                  </td></tr>



        </table>






        </fieldset>

</asp:Content>

