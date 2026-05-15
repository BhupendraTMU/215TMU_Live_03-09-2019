<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="SecondaryLoadReport.aspx.cs" Inherits="Faculty_SecondaryLoadReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .parent {
            text-align: center;
            display: block;
            border: 1px solid outset;
        }

        .child {
            display: inline-block;
            width: 200px;
        }
    </style>
    <script type="text/javascript">

        function preventBackspace(e) {
            var evt = e || window.event;
            if (evt) {
                var keyCode = evt.charCode || evt.keyCode;
                if (keyCode === 8) {
                    if (evt.preventDefault) {
                        evt.preventDefault();
                    } else {
                        evt.returnValue = false;
                    }
                }
            }
        }
        function checkDate(sender, args) {

            var f = new Date($('[id$=txtDateTo]').val());
            if (sender._selectedDate > f) {
                alertify.error("You cannot select Greater than To date!");
                sender._textbox.set_Value('');
            }
        }



        function checkDate1(sender, args) {

            if ($('[id$=txtDateFrom]').val() == '') {
                alertify.error('First select the from date!');
                sender._textbox.set_Value('');
                return false;
            }
            else {
                var f = new Date($('[id$=txtDateFrom]').val());

                if (sender._selectedDate < f) {
                    alertify.error("You cannot select less than from date!");
                    sender._textbox.set_Value('');
                }
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="elist" runat="server">
        <ContentTemplate>
            <fieldset class="boxBody">
                <table>
                    <tr>

                        <td>
                            <asp:Label ID="Label1" runat="server" Visible="true" Text="Secondary Load" Font-Size="15pt" ForeColor="#093A62"
                                Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label></td>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                        <td>Academic Year  </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:DropDownList ID="drpAcademicYear" Width="150px" Height="20px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpAcademicYear_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td style="width: 20px"></td>
                        <td>
                            
                        </td>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                        </td>
                        <td>

                           
                        </td>

                    </tr>
                </table>



            </fieldset>
            <fieldset class="boxBodyHeader">
            </fieldset>


            <asp:Panel ID="pnlList" runat="server" BorderWidth="2px" BorderColor="#ACE9FB" ScrollBars="Vertical">

                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="Entry No" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" BackColor="White" 
                        EmptyDataText="There are no data records to display." BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                        Width="100%" GridLines="Horizontal" AllowPaging="true"
                        PageSize="55">
                        <AlternatingRowStyle BackColor="#F7F7F7" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sr. No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle Width="6%" />
                            </asp:TemplateField>

                            <asp:BoundField ItemStyle-Width="150px" DataField="Employee Name" HeaderText="Faculty">
                                <ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>                           
                            <asp:BoundField ItemStyle-Width="150px" DataField="Course Name" HeaderText="Course Name">
                                <ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Semester/Year">
                                <ItemTemplate>
                                    <asp:Label ID="lblSemYear" runat="server" Text='<%#Eval("Semest") %>' />                                                                                 
                                </ItemTemplate>
                            </asp:TemplateField>
                           <%-- <asp:BoundField ItemStyle-Width="150px" DataField="Semest" HeaderText="Semester/Year">
                                <ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>  --%>  
                            <asp:TemplateField HeaderText="College">
                                <ItemTemplate>
                                    <asp:Label ID="lblCollegeCode" runat="server" Text='<%#Eval("College Code") %>' />                                                                                 
                                </ItemTemplate>
                            </asp:TemplateField>                        
                            <%--<asp:BoundField ItemStyle-Width="150px" DataField="College Code" HeaderText="College Code">
                                <ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>   --%>                         
                            <asp:BoundField ItemStyle-Width="150px" DataField="Secondary Load Description" HeaderText="Description">
                                <ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks") %>' />                                                                                 
                                </ItemTemplate>
                            </asp:TemplateField>  
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                        <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <SortedAscendingCellStyle BackColor="#F4F4FD" />
                        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                        <SortedDescendingCellStyle BackColor="#D8D8F0" />
                        <SortedDescendingHeaderStyle BackColor="#3E3277" />
                    </asp:GridView>
                    <br />
                </div>
            </asp:Panel>



        </ContentTemplate>
        <Triggers>
            <%-- <asp:PostBackTrigger ControlID="btnExportToexcel" />
            <asp:PostBackTrigger ControlID="BtnExportpdf" />--%>
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

