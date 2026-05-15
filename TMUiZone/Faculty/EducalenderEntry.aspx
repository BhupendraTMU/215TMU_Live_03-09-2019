<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="EducalenderEntry.aspx.cs" Inherits="Faculty_EducalenderEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ToolkitScriptManager ID="ScriptManager1" runat="server"></asp:ToolkitScriptManager>
     <asp:UpdatePanel runat="server" ID="updmain">
        <ContentTemplate>
          
             <fieldset class="boxBody">
                  <asp:Label ID="lblHeader" runat="server"
                    Text="Academic Calendar" Font-Size="15pt" ForeColor="#093A62"
                    Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                  </fieldset>
            <fieldset class="boxBody">

                  </fieldset>

   <%-- <table cellpadding="0px" cellspacing="0px" style="width: 100%">
        <tr>
            <td style="height: 10px">
                
            </td>
        </tr>
            </table>--%> <fieldset class="boxBodyInner">
                <fieldset class="boxBodyInner">

    <center>
        <table id="maintbl">
            <tr>
                <td>
                    <table id="codetbl">
                        <tr>
                            <td> College Code &nbsp;&nbsp;&nbsp;</td>
                             <td> <asp:DropDownList ID="CodeDropDownList" runat="server" OnSelectedIndexChanged="CodeDropDownList_SelectedIndexChanged" Height="28px"></asp:DropDownList></td>
                            
                            <td>Academic Year&nbsp;&nbsp;&nbsp; </td>
                             <td><asp:DropDownList ID="drpAcademicYear" runat="server" Height="28px" AutoPostBack="True" OnSelectedIndexChanged="drpAcademicYear_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><br /></td>
                            <td><br /></td>
                            <td></td>
                            <td></td>
                        </tr>
                         <tr>
                            <td>From Date&nbsp;&nbsp;&nbsp; </td>
                             <td>
                                  <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate" Format="dd MMM yyyy"></asp:CalendarExtender>
                                 &nbsp;&nbsp;&nbsp;&nbsp;
                             </td>
                             <td>To Date &nbsp;&nbsp;&nbsp;</td>
                             <td>
                                  <asp:TextBox ID="txtToDate" runat="server" OnTextChanged="txtToDate_TextChanged" ></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate" Format="dd MMM yyyy" ></asp:CalendarExtender>
                             </td>
                        </tr>
                        <tr>
                            <td><br /></td>
                            <td><br /></td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
             <tr>
                <td align="center"><asp:Button ID="btnshow" CssClass="btn-sm btn-primary btn-block" runat="server" Text="Show" OnClick="btnshow_Click" />
                    <br />
                    <br />

                </td>
            </tr>
             </table>
        <table id ="gridtbl">
            <tr>
<td>
     <asp:GridView ID="EduGridView" runat="server"  EmptyDataText="There are no data records to display." AutoGenerateColumns="False" Width="1000px" CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                    <Columns>
                        <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}" />
                        <asp:BoundField DataField="Day" HeaderText="Day" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
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

 </fieldset> 
                </fieldset>  
       </ContentTemplate>
         </asp:UpdatePanel>

</asp:Content>

