<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="summeryOfJoinAndLeave.aspx.cs" Inherits="Faculty_summeryOfJoinAndLeave" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function preventBackspace(e) {
            var evt = e || window.event;
            if (evt) {
                var keyCode = evt.charCode || evt.keyCode;
                if (keyCode === 8 || keyCode === 13) {
                    if (evt.preventDefault) {
                        evt.preventDefault();
                    } else {
                        evt.returnValue = false;
                    }
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <fieldset class="boxBody">
 <asp:Label ID="Label1" runat="server" 
            Text="Summery" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 </fieldset>
             <fieldset class="boxBodyHeader"> 
  
 </fieldset>

            <fieldset class="boxBodyInner">
                <fieldset class="boxBodyInner">
                    <center>
                    <table>
                        <tr>
                            <td style="padding-left:10px;"></td>
                            <td style="padding-left:10px;">
                                <table>
                                    <tr>
                                        <td><asp:TextBox ID="frmdate" runat="server" onkeypress="return false;" onKeyDown="preventBackspace();" Enabled="false" CssClass="form-control"  placeholder="From Date"></asp:TextBox></td>
                                        <td>&nbsp;</td>
                                        <td>
                                             <asp:Image src="../Images/Calendar.png" runat="server" Height="30px" Width="30px" alt="" ID="fdate" />
            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="frmdate" PopupButtonID="fdate" Format="MMM-yyyy"></asp:CalendarExtender>
                                                                <asp:HiddenField ID="firstdate" runat="server" />

                                        </td>
                                    </tr>
                                </table>

                                 

                            </td>
                            <td style="padding-left:10px;"></td>

                            <td style="padding-left:10px;">

                                <table>
                                    <tr>
                                        <td> <asp:TextBox ID="todate" runat="server" onkeypress="return false;" onKeyDown="preventBackspace();"
                                            autocomplete="false" placeholder="To Date" CssClass="form-control" Enabled="false" ></asp:TextBox></td>
                                        <td>&nbsp;</td>
                                        <td>
                                             <asp:Image src="../Images/Calendar.png" runat="server" Height="30px" Width="30px" alt="" ID="tdate" />
            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="todate" PopupButtonID="tdate" Format="MMM-yyyy"></asp:CalendarExtender>
                                
                                <asp:HiddenField ID="lastdate" runat="server" />

                                        </td>
                                    </tr>
                                </table>

                  
                               
                            </td>

                            <td style="padding-left:10px;">
                                Employee(TEACH/NON-TEACH)
                            </td>
                            <td style="padding-left:10px;">
                                <asp:DropDownList ID="drptechnontech" runat="server" CssClass="form-control">
                                    <asp:ListItem>--  Select  --</asp:ListItem>
                                    <asp:ListItem Value="TEACH">TEACHING</asp:ListItem>
                                    <asp:ListItem Value="NON-TEACH">NON-TEACHING</asp:ListItem>
                                </asp:DropDownList></td>
                            <td style="padding-left:10px;">
                                <asp:Button ID="btnshow" runat="server" Text="Show"  CssClass="btn-sm btn-primary btn-block" OnClick="Button1_Click" />

                            </td>
                        </tr>
                        <tr>
                            <td colspan="8" align="center" >
                                <br />

                                <asp:GridView ID="sumeerygrid" runat="server" BackColor="White"  BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal"   EmptyDataText="There are no data records to display." AutoGenerateColumns="False" >
               <AlternatingRowStyle BackColor="#F7F7F7" />
                                     
                                              <Columns>
                                                  <asp:BoundField DataField="College" HeaderText="College" />
                                                  <asp:BoundField DataField="DOJ_Count" HeaderText="Joining Count" />
                                                  <asp:BoundField DataField="Releaving_Count" HeaderText="Relieving count" />
                                                 
                                    </Columns>   
                                              <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
               <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Center" CssClass="cssGridheaderfont" />
               <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
               <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont"   />
               <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
               <SortedAscendingCellStyle BackColor="#F4F4FD" />
               <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
               <SortedDescendingCellStyle BackColor="#D8D8F0" />
               <SortedDescendingHeaderStyle BackColor="#3E3277" />
                    </asp:GridView>


                            </td>
                        </tr>
                    </table>

                    </center>
             
                </fieldset> 
                </fieldset>  
                  </ContentTemplate>
    </asp:UpdatePanel>
         

</asp:Content>

