<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="exitinterviewformDetails.aspx.cs" Inherits="Faculty_exitinterviewformDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function SelectMachineCode(row) {
            var rowData = row.parentNode.parentNode;
            var rowIndex = rowData.rowIndex;
            // var strTest = rowData.cells[0].innerHTML;  //Report Open it                              
            //  PageMethods.CreateSessionViaJavascript(strTest);   //Report Open it                              
            // window.open('../Faculty/StudentDetailsForMentor.aspx?search=' + rowData.cells[7].innerHTML);
            window.open('../SredirectToReport.aspx?search=' + rowData.cells[0].innerHTML);
            // window.open('../StudentReport.aspx?search=' + rowData.cells[7].innerHTML, '_blank');  //Rport open it
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager EnablePageMethods="true" ID="MainSM" runat="server" ScriptMode="Release" LoadScriptsBeforeUI="true"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <fieldset class="boxBody">
                <asp:Label ID="Label1" runat="server" Text="Exit Interview Form Details" Font-Size="15pt" ForeColor="#093A62" 
                    Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
            </fieldset>
            <fieldset class="boxBodyHeader">
            </fieldset>
            <div class="clearfix"></div>
            <br />
            <center>
                <table width="98%" >
                    <tr>
                        <td style="padding-left: 2px" runat="server" id="tdCollege">
                            <asp:DropDownList ID="DrpCollege" CssClass="form-control" Width="350px" runat="server"></asp:DropDownList>
                        </td>
                        <td style="padding-left: 1px; width: 45%">
                            <asp:TextBox ID="txtemp" placeholder="Employee Name" CssClass="form-control" runat="server" placholder="Search Employee Name"></asp:TextBox>
                        </td>
                        <td style="padding-left: 5px; width: 25%">
                            <asp:Button ID="btnSearch" runat="server" Text="Show" OnClick="btnSearch_Click" Width="100px" Height="33px" />
                        </td>
                    </tr>
                </table>
                <table width="98%" >
                    <tr>
                        <td>
                            <asp:GridView  ID="exitformDetails" runat="server" ShowFooter="true" BackColor="White" DataKeyNames="Employee Code" BorderColor="#E7E7FF" BorderStyle="None" 
                                BorderWidth="1px" CellPadding="13" Width="98%" GridLines="Horizontal" EmptyDataText="There are no data records to display." AllowPaging="True" 
                                AutoGenerateColumns="False" OnRowCommand="exitformDetails_RowCommand" >
                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                <Columns>                                    
                                    <asp:BoundField DataField="Employee Name" HeaderText="Employee Name"  />
                                    <asp:BoundField DataField="Employee Code" HeaderText="Employee Code" />
                                    <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                    <asp:BoundField DataField="Institution" HeaderText="Institution" />
                                    <asp:BoundField DataField="HOD" HeaderText="HOD" />
                                    <asp:BoundField DataField="cdate" HeaderText="Exit Form Submit Date" />
                                    <asp:TemplateField HeaderText="View">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnviewAttendanced" runat="server" Text="View" CommandName="printfrm" CommandArgument='<%# Eval("Employee Code") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Center" CssClass="cssGridheaderfont" />
                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

