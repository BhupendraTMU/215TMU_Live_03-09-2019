<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="IndentForIt.aspx.cs" Inherits="Faculty_IndentForIt" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Send For Approval ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

    </script>

    <script type="text/javascript">
        function Delete() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Delete ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

    </script>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text=" IT Indent" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>

    <table cellpadding="0px" cellspacing="0px" style="width: 100%">
        <tr>
            <td style="width: 10px"></td>
            <td>
                <table cellpadding="0px" cellspacing="0px" style="width: 100%">

                    <tr>
                        <td style="height: 20px"></td>
                    </tr>
                    <tr>
                        <td align="right">

                            <table cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td align="left">
                                       
                                        <asp:Panel ID="pnlDatewisefilter" runat="server" Visible="true">
                                            <table cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <td>From Date  </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtFromDate" runat="server" onkeydown="return false;"
                                                            oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" Width="120px"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate" Format="dd MMM yyyy"></asp:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="rfvtxtFromDate" runat="server" ControlToValidate="txtFromDate" ErrorMessage="*" BackColor="Red" SetFocusOnError="true" ValidationGroup="get"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>Till Date </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtTillDate" runat="server" onkeydown="return false;"
                                                            oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" Width="120px"></asp:TextBox>

                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTillDate" Format="dd MMM yyyy"></asp:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="rfvtxtTillDate" runat="server" ControlToValidate="txtTillDate" ErrorMessage="*" BackColor="Red" SetFocusOnError="true" ValidationGroup="get"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>Status </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddStatus" runat="server" Height="30px">
                                                            <asp:ListItem Value="7">ALL</asp:ListItem>
                                                            <asp:ListItem Value="0">Open</asp:ListItem>
                                                            <asp:ListItem Value="1">Pending on HOD</asp:ListItem>
                                                            <asp:ListItem Value="2">Approved(HOD)</asp:ListItem>
                                                            <asp:ListItem Value="4">Rejected(HOD)</asp:ListItem>
                                                            <asp:ListItem Value="8">Approved(Management)</asp:ListItem>
                                                            <asp:ListItem Value="9">Rejected(Management)</asp:ListItem>
                                                            <asp:ListItem Value="5">Issued</asp:ListItem>
                                                        </asp:DropDownList></td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:Button ID="btnSearch" runat="server" Text="Get" OnClick="btnSearch_Click" ValidationGroup="get" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>

                                    </td>
                                    <td style="width: 150px"></td>
                                    <td align="right">
                                        <asp:Button ID="btnCreate" runat="server" Text="Create New Indent" OnClick="btnCreate_Click" />
                                        &nbsp&nbsp&nbsp
                                        <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" OnClick="btnExportToExcel_Click" />
                                    </td>
                                </tr>
                            </table>

                        </td>
                    </tr>

                    <tr>
                        <td style="height: 20px"></td>
                    </tr>
                    <tr>
                        <td style="border-color: #ff9900">
                            <%--  <input type="text"  id="txtSearch"  class="form-control" name="off" autocomplete="off" placeholder="Search Data"   onkeyup=" SearchGrid('txtSearch', '<%=grdApproval.ClientID%>')"/>--%>
           
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <script type="text/javascript">

                                function SearchGrid(txtSearch, grdApproval) {
                                    if ($("[id *=" + txtSearch + " ]").val() != "") {
                                        $("[id *=" + grdApproval + " ]").children
                                            ('tbody').children('tr').each(function () {
                                                $(this).show();
                                            });
                                        $("[id *=" + grdApproval + " ]").children
                                            ('tbody').children('tr').each(function () {
                                                var match = false;
                                                $(this).children('td').each(function () {
                                                    if ($(this).text().toUpperCase().indexOf($("[id *=" +
                                                        txtSearch + " ]").val().toUpperCase()) > -1) {
                                                        match = true;
                                                        return false;
                                                    }
                                                });
                                                if (match) {
                                                    $(this).show();
                                                    $(this).children('th').show();
                                                }
                                                else {
                                                    $(this).hide();
                                                    $(this).children('th').show();
                                                }
                                            });


                                        $("[id *=" + grdApproval + " ]").children('tbody').
                                            children('tr').each(function (index) {
                                                if (index == 0)
                                                    $(this).show();
                                            });
                                    }
                                    else {
                                        $("[id *=" + grdApproval + " ]").children('tbody').
                                            children('tr').each(function () {
                                                $(this).show();
                                            });
                                    }
                                }

                            </script>
                            <asp:GridView ID="grdApproval" runat="server" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None"
                                CssClass="table table-striped table-bordered table-hover" OnPageIndexChanging="grdApproval_PageIndexChanging" OnDataBound="grdApproval_DataBound" OnRowDataBound="grdApproval_RowDataBound" BackColor="White" BorderColor="#3366CC" BorderStyle="None"
                                BorderWidth="1px" CellPadding="4" AllowPaging="True" DataKeyNames="DocumentNo">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr No" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Indent No">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblDoumnentNo" runat="server" Text='<%#Bind("DocumentNo") %>' CommandArgument='<%#Bind("DocumentNo") %>' OnCommand="lblDoumnentNo_Command"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--  <asp:BoundField DataField=" DocumentNo" HeaderText="Indent No" />--%>
                                    <asp:BoundField DataField="Issue Date" HeaderText="Issue Date" DataFormatString="{0:dd MMM yyyy}" />
                                    <asp:BoundField DataField="Issue For" HeaderText="Issue For" />
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Issue Id" HeaderText="Issue Id" />
                                    <asp:BoundField DataField="Issue Name" HeaderText="Issue Name" />
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                    <asp:BoundField DataField="HodRemark" HeaderText="Hod Remark" />
                                    <asp:BoundField DataField="Management_remark" HeaderText="Management Remark" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnApprove" runat="server" Text="Send for Approval" CommandArgument='<%# Eval("DocumentNo") %>' OnClientClick="Confirm()" OnCommand="btnApprove_Command" />
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnReject" runat="server" Text="Delete" CommandArgument='<%# Eval("DocumentNo") %>' OnClientClick="Delete()" OnCommand="btnReject_Command" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    There are no record found.....
                                </EmptyDataTemplate>
                                <FooterStyle BackColor="#ff9900" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#ff9900" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#ff9900" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                <SortedDescendingHeaderStyle BackColor="#002876" />
                            </asp:GridView>

                            <%--<script type="text/javascript" src="../jquery.min.js"></script>
                            <script type="text/javascript" src="../quicksearch.js"></script>
                            <script type="text/javascript">
                                $(function () {
                                    $('.search_textbox').each(function (i) {
                                        $(this).quicksearch("[id*=grdApproval] tr:not(:has(th))", {
                                            'testQuery': function (query, txt, row) {
                                                return $(row).children(":eq(" + i + ")").text().toLowerCase().indexOf(query[0].toLowerCase()) != -1;
                                            }
                                        });
                                    });
                                });
                            </script>--%>

                        </td>
                    </tr>

                    <tr>
                        <td style="height: 90px"></td>
                    </tr>

                </table>
            </td>
            <td style="width: 10px"></td>
        </tr>
    </table>



    <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none" />
    <asp:ModalPopupExtender ID="mdIndentLine" runat="server" TargetControlID="Button1" PopupControlID="PnlIndentLineData" BackgroundCssClass="modalBackgroundforco"></asp:ModalPopupExtender>


    <asp:Panel ID="PnlIndentLineData" runat="server" BackColor="White">


        <table cellpadding="0px" cellspacing="0px">
            <tr>
                <td style="height: 20px"></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnClose" runat="server" Text="Close" />
                    &nbsp;&nbsp;</td>
            </tr>

            <tr>
                <td style="height: 20px"></td>
            </tr>
            <tr>
                <td style="background-color: #e5e3e3">
                    <table cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td>&nbsp; &nbsp;
                                <asp:Label ID="Label2" runat="server" Text="Indent Sub Form" Font-Bold="True" Font-Size="10pt" ForeColor="Black"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                    </table>

                </td>
            </tr>

            <tr>
                <td>
                    <asp:GridView ID="grdViewIndentLine" runat="server" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None" CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" AllowPaging="True" OnPageIndexChanging="grdViewIndentLine_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="Document No" HeaderText="Indent No" />
                            <asp:BoundField HeaderText="No_" DataField="No_" />
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:TemplateField HeaderText="Item No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemNo_Grid" runat="server" Text='<%#Bind("[Item No]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemNoDescription_Grid" runat="server" Text='<%#Bind("[Description]") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit of Measure">

                                <ItemTemplate>
                                    <asp:Label ID="lblUnitofMeasure_Grid" runat="server" Text='<%#Bind("[Unit of Measure]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Variance Code"></asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Qty. for Requsition">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuantity_Grid" runat="server" Text='<%#Bind("Quantity","{0:n}" ) %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HOD QTY">
                                <ItemTemplate>
                                    <asp:Label ID="lblHODQTY" runat="server" Text='<%#Bind("[HOD Appr_ QTY]","{0:n}" ) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Management QTY">
                                <ItemTemplate>
                                    <asp:Label ID="lblMQTY" runat="server" Text='<%#Bind("[Management Appr_ QTY]","{0:n}" ) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%#Bind("[User Remark]" ) %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>






                        </Columns>
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#ff9900" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                        <SortedDescendingHeaderStyle BackColor="#002876" />
                    </asp:GridView>



                </td>
            </tr>


            <tr>
                <td style="height: 120px"></td>
            </tr>

        </table>




    </asp:Panel>
</asp:Content>

