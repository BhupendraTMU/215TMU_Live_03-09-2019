<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="ResultCoutForFeedback.aspx.cs" Inherits="Faculty_ResultCoutForFeedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
 <asp:Label ID="Label3" runat="server" 
            Text="Real Time Feedback Count" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
       &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    Academic Year:&nbsp&nbsp
                    <asp:DropDownList ID="ddlAcademicYear" Width="100px" Height="20px" runat="server" ></asp:DropDownList>
        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    Semester/Year:&nbsp&nbsp
                    <asp:DropDownList ID="ddsem" Width="100px" Height="20px" runat="server" >
                        <asp:ListItem Value="--- Select ----">--- Select ----</asp:ListItem>
                                <asp:ListItem Value="Even">Even Sem</asp:ListItem>
                                <asp:ListItem Value="Odd">Odd Sem</asp:ListItem>
                                <asp:ListItem Value="Year">Year</asp:ListItem>
                    </asp:DropDownList>
             &nbsp;&nbsp;&nbsp; &nbsp;
                       
                            <asp:Button ID="btnshow" runat="server" Visible="true" Text="Show" OnClick="btnshow_Click" />

                        
 </fieldset>

 <fieldset class="boxBodyHeader"> 
  
 </fieldset>
    <fieldset  style="background:#fefefe; border-top:1px solid #dde0e8; border-bottom:1px solid #dde0e8; padding:10px 20px; height:100%">
      <asp:UpdatePanel ID="UpdatePanel1" runat="server" updatemode="Conditional"  ViewStateMode="Enabled">
            <ContentTemplate>
         <%-- <asp:UpdatePanel runat="server" ID="updTime" >
            <ContentTemplate >
                <asp:Timer ID="timetimer" runat="server" Interval="120000" OnTick="timetimer_Tick"  >
                </asp:Timer>
                <table>
                <tr>
                    <td align="right">
                      
                        <asp:Label ID="Label1" runat="server" Font-Bold="true"  ></asp:Label>
                    </td>
                </tr>
                 </table>
            </ContentTemplate>
        </asp:UpdatePanel>--%>
        
        <center>
            <asp:Timer ID="TimerRef" runat="server" Interval="60000" OnTick="TimerRef_Tick"></asp:Timer>
            <table>
                <tr>
                    <td align="right">
                        <%--<asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" />--%>
                        <asp:Label ID="lblTime" runat="server" Font-Bold="true" Font-Size="Small" ></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <asp:GridView ID="grdResult" runat="server" Width="100%"
                              EmptyDataText="There are no data records to display." CssClass="table table-striped table-bordered table-hover" 
                              BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4"  >
                            <Columns>
                              <asp:TemplateField HeaderText="Sl. No.">
                                                            <ItemTemplate >
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="7%" />
                                                        </asp:TemplateField>


                                </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#ff9900" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" ForeColor="#003399" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />

                        </asp:GridView>
                    </td>
                </tr>
            </table>

        </center>
            </ContentTemplate>           
        </asp:UpdatePanel>
        
        </fieldset>

</asp:Content>

