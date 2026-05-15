<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" EnableEventValidation="false"  CodeFile="PendingStudentForFeedbackEnty.aspx.cs" Inherits="Faculty_PendingStudentForFeedbackEnty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
 <asp:Label ID="Label3" runat="server" 
            Text="Pending Feedback Entry" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
        Academic Year:&nbsp&nbsp
                    <asp:DropDownList ID="ddlAcademicYear" Width="100px" Height="20px" runat="server" AutoPostBack="true"></asp:DropDownList>
 </fieldset>
 <fieldset class="boxBodyHeader"> 
  
 </fieldset>
    <fieldset  style="background:#fefefe; border-top:1px solid #dde0e8; border-bottom:1px solid #dde0e8; padding:10px 20px; height:100%">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        <center>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblcolloege" runat="server" Visible="false" Text="College Code"></asp:Label>
                        </td>
                    <td>
                        <asp:DropDownList ID="ddCollege" Visible="false"  Width="250px" runat="server" height="25px" ></asp:DropDownList>
                    </td>

                    <td>&nbsp;&nbsp;&nbsp; &nbsp;</td>
                        <td>Semester/Year</td>
                        <td>&nbsp;&nbsp;&nbsp; &nbsp;</td>
                        <td>
                            <asp:DropDownList ID="ddsem" runat="server" Height="25px">
                                <asp:ListItem Value="--- Select ----">--- Select ----</asp:ListItem>
                                <asp:ListItem Value="Even">Even Sem</asp:ListItem>
                                <asp:ListItem Value="Odd">Odd Sem</asp:ListItem>
                                <asp:ListItem Value="Year">Year</asp:ListItem>
                            </asp:DropDownList></td>
                            <td>&nbsp;&nbsp;&nbsp; &nbsp;</td> 
                    <td>
                        <asp:TextBox ID="txtSearch" placeholder="Enter Sem/Year" runat="server" Width="110px" Height="25px" ></asp:TextBox>
                    </td>                           
                        <td>
                            <asp:Button ID="btnshow" runat="server" Visible="true" Text="Show" OnClick="btnshow_Click"  Height="25px" Width="70px" /></td>

                        <td>&nbsp;&nbsp;&nbsp; &nbsp;</td>
                         

                            
                    <td>
                        
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                    <td>
                          <asp:ImageButton ID="btnExport" Visible="true" runat="server" ImageUrl="~/images/excel.jpg" Height="35px" OnClick="btnExport_Click" ></asp:ImageButton>
                    </td>
                </tr>
            </table>
            <table>
                                <tr>
                    <td>


                        <asp:GridView ID="grdPending" runat="server" Width="100%"
                              EmptyDataText="There are no data records to display." CssClass="table table-striped table-bordered table-hover" 
                              BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4"  >
                            <Columns>
                                 <asp:TemplateField HeaderText="Sl. No.">
                                                            <ItemTemplate >
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="7%" />
                                                        </asp:TemplateField>
                  
                                 <%--<asp:BoundField DataField="Enrollment No_" HeaderText="Enrollment No" />
                                 <asp:BoundField DataField="Student Name" HeaderText="Student Name" />--%>
                  
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
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>
        </asp:UpdatePanel>

        </fieldset>






</asp:Content>

