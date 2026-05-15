<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="Employee_Punch_Data.aspx.cs" Inherits="Faculty_Employee_Punch_Data" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <table cellpadding="0px" cellspacing="0px" style="width:100%">
            <tr> <td style="height:13px"> </td></tr>
         <tr> <td>  


      &nbsp;&nbsp;&nbsp; &nbsp;  <asp:Label ID="Label3" runat="server" 
            Text="Punch Data" Font-Size="15pt" ForeColor="#093A62" 
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
                    </asp:DropDownList></td>  <td style="width:10px"> </td> <td>  Year </td> <td style="width:10px"> </td> <td> <asp:DropDownList ID="ddlYear" runat="server" Height="29px"></asp:DropDownList> </td> <td style="width:10px"> </td><td> <asp:Button ID="btnGet" runat="server" Text="Get" OnClick="btnGet_Click" /> </td> <td style="width:10px">  </td>  <td>  <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" OnClick="btnExportToExcel_Click" /></td></tr> </table> </td></tr>

                <tr> <td style="height:13px"> </td></tr>

                <tr> <td>

                    <table cellpadding="0px" cellspacing="0px" style="width:100%">  <tr> <td style="width:10px">  </td> <td>  <asp:GridView ID="grdPunchdata" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnRowDataBound="grdPunchdata_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="Attendance Date" HeaderText="Date" DataFormatString="{0:D}" />
                            <asp:BoundField DataField="In Time" HeaderText="In Time" />
                            <asp:BoundField DataField="Out Time" HeaderText="Out Time" />
                            <asp:BoundField DataField="P1" HeaderText="P1" />
                            <asp:BoundField DataField="P2" HeaderText="P2" />
                            <asp:BoundField DataField="P3" HeaderText="P3" />
                            <asp:BoundField DataField="P4" HeaderText="P4" />
                            <asp:BoundField DataField="P5" HeaderText="P5" />
                            <asp:BoundField DataField="P6" HeaderText="P6" />
                            <asp:BoundField DataField="P7" HeaderText="P7" />
                            <asp:BoundField DataField="P8" HeaderText="P8" />
                            <asp:BoundField DataField="P9" HeaderText="P9" />
                        </Columns>
                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                        <HeaderStyle BackColor="#ff9900"  ForeColor="White"  Font-Bold="True" />
                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" ForeColor="#330099" />
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

