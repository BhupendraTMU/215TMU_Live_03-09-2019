<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="Itindentapprovalform.aspx.cs" Inherits="Faculty_Itindentapprovalform" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Approve ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
        function ItemReceive() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Please confirm that you have receive this item")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

    </script>
    <script type="text/javascript">
        function closeModalPopup() {
            var mpe = $find("mpeItemLineData");
            if (mpe) {
                mpe.hide();
            }
        }
        function closeModalPopupE() {
            var mpe = $find("mpeItemLineDataE");
            if (mpe) {
                mpe.hide();
            }
        }
    </script>
    <script type="text/javascript">
        function Delete() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Reject ?")) {
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
            Text="IT Indent Approval" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>

    <table cellpadding="0px" cellspacing="0px" style="width: 100%">
        <tr>
            <td style="width: 10px"></td>
            <td>
                <table cellpadding="0px" cellspacing="0px" style="width: 100%">
                    <tr>
                        <td style="height: 20px" colspan="2"></td>
                    </tr>
                    <tr>
                        <td align="left">
                            <table cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td style="width: 20%"></td>
                                    <td>
                                        <asp:Panel ID="pnlDatewisefilter" runat="server" Visible="true">
                                            <table cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <td>From Date  </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtFromDate" runat="server" onkeydown="return false;"
                                                            oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" Width="120px"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate" Format="dd MMM yyyy"></asp:CalendarExtender>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>Till Date </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtTillDate" runat="server" onkeydown="return false;"
                                                            oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" Width="120px"></asp:TextBox>

                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTillDate" Format="dd MMM yyyy"></asp:CalendarExtender>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>Status </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddStatus" runat="server" Height="30px">
                                                            <asp:ListItem Value="7">ALL</asp:ListItem>
                                                            <asp:ListItem Value="1">Processed for Approval</asp:ListItem>
                                                            <asp:ListItem Value="2">Approved(HOD)</asp:ListItem>
                                                            <asp:ListItem Value="4">Rejected</asp:ListItem>


                                                            <asp:ListItem Value="8">Approved(Management)</asp:ListItem>
                                                            <asp:ListItem Value="9">Rejected(Management)</asp:ListItem>
                                                            <asp:ListItem Value="5">Issued</asp:ListItem>
                                                            <asp:ListItem Value="10">Item Received</asp:ListItem>


                                                        </asp:DropDownList></td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:Button ID="btnSearch" runat="server" Text="Get" OnClick="btnSearch_Click" />
                                                        <asp:Button ID="Button4" runat="server" Text="Export to Excel" OnClick="Button4_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>

                        </td>
                    </tr>

                    <tr>
                        <td style="height: 20px"></td>
                    </tr>
                    <tr>
                        <td style="border-color: #ff9900">
                            <input type="text" id="txtSearch" name="off" autocomplete="off" placeholder="Search Data" style="margin-right: 10px; margin-bottom: 12px;" />
                            <input id="btnClickMe" type="button" value="Search" style="height: 31px;" onclick="return SearchGrid('txtSearch', '<%=grdApproval.ClientID%>')" />
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

                            <div style="overflow:scroll;height:400px">

                            <asp:GridView ID="grdApproval" runat="server" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None" DataKeyNames="DocumentNo"
                                CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px"
                                CellPadding="4" OnRowDataBound="grdApproval_RowDataBound" OnPageIndexChanging="grdApproval_PageIndexChanging" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Indent No">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblDoumnentNo" runat="server" Text='<%#Bind("DocumentNo") %>' OnCommand="lblDoumnentNo_Command1" CommandArgument='<%#Bind("DocumentNo") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--  <asp:BoundField DataField=" DocumentNo" HeaderText="Indent No" />--%>
                                    <asp:BoundField DataField="Issue Date" HeaderText="Issue Date" DataFormatString="{0:dd MMM yyyy}" />
                                    <asp:BoundField DataField="Issue For" HeaderText="Issue For" />
                                    <%--<asp:BoundField DataField="Status" HeaderText="Status" />--%>

                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("Status") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Issue Id" HeaderText="Issue Id" />
                                    <asp:BoundField DataField="Issue Name" HeaderText="Issue Name" />

                                    <asp:TemplateField HeaderText="HOD Remark">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRRemark" Enabled="false" TextMode="MultiLine" Text='<%#Bind("HodRemark") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Management Remark">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtMRemark" Enabled="false" TextMode="MultiLine" Text='<%#Bind("Management_remark") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnApprove" runat="server" Text="Approve" OnCommand="btnApprove_Command" OnClientClick='<%# Eval("Status").ToString() == "Pending on Management" ? "" : "return Confirm();" %>' CommandArgument='<%# Eval("DocumentNo") %>' />
                                            <asp:Button ID="btnClose" runat="server" Visible="false" Text="Item Received" OnCommand="btnClose_Command" OnClientClick="return ItemReceive()" CommandArgument='<%# Eval("DocumentNo") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnReject" runat="server" Text="Reject" CommandArgument='<%# Eval("DocumentNo") %>' OnClientClick="Delete()" OnCommand="btnReject_Command" />
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


                                </div>


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


    <asp:Panel ID="PnlIndentLineData" runat="server" BackColor="White" Style="display: none">
        <asp:GridView ID="GridView2" runat="server" ForeColor="#333333" GridLines="None"
            CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px"
            CellPadding="4">

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

        <table cellpadding="0px" cellspacing="0px">
            <tr>
                <td style="height: 20px"></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="Button2" runat="server" Text="QTY Update" OnClick="btnClose_Click" />
                    <asp:Button ID="btnClose" runat="server" BackColor="Red" Text="Close" />

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
                    <asp:GridView ID="grdViewIndentLine" runat="server" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None"
                        CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#3366CC" BorderStyle="None"
                        BorderWidth="1px" CellPadding="4" AllowPaging="True" OnPageIndexChanging="grdViewIndentLine_PageIndexChanging" DataKeyNames="Line No_">
                        <Columns>
                            <asp:BoundField DataField="Document No" HeaderText="Indent No" />
                            <asp:TemplateField HeaderText="No_">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkNo_" runat="server" OnClick="lnkNo__Click" Text='<%#Bind("[No_]") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:TemplateField HeaderText="Item No.">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblItemNo_Grid" runat="server" OnClick="lblItemNo_Grid_Click" Text='<%#Bind("[Item No]") %>'></asp:LinkButton>
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

                            <asp:TemplateField HeaderText="Issue Qty">

                                <ItemTemplate>
                                    <asp:Label ID="lblishuquty_Grid" runat="server" Text='<%#Bind("[Issued Qty]","{0:n}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Rem. Quantity">

                                <ItemTemplate>
                                    <asp:Label ID="lblqiai_Grid" runat="server" Text='<%#Bind("Rem_Qty","{0:n}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Requsition Qty">

                                <ItemTemplate>
                                    <asp:Label ID="lblRequsition" runat="server" Text='<%#Bind("Quantity","{0:n}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:TemplateField HeaderText="Variance Code"></asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="HOD Qty">
                                <ItemTemplate>

                                    <asp:TextBox ID="lblQuantity_Grid" runat="server" Width="70px" Text='<%#Bind("[HOD Appr_ QTY]","{0:0}" ) %>'></asp:TextBox>
                                    <asp:HiddenField ID="hfQuantity" runat="server" Value='<%#Bind("[HOD Appr_ QTY]","{0:0}" ) %>'></asp:HiddenField>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Final QTY">
                                <ItemTemplate>
                                    <asp:TextBox ID="lblFinalQTY" Enabled="false" Width="70px" runat="server" Text='<%#Bind("[Management Appr_ QTY]","{0:0}") %>'></asp:TextBox>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserRemark" runat="server" Text='<%#Bind("[User Remark]") %>'></asp:Label>

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





    <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="PnlItemLineData" BackgroundCssClass="modalBackgroundforco" BehaviorID="mpeItemLineData"></asp:ModalPopupExtender>


    <asp:Panel ID="PnlItemLineData" runat="server" BackColor="White" Style="display: none">


        <table cellpadding="0px" cellspacing="0px">
            <tr>
                <td style="height: 20px"></td>
            </tr>
            <tr>
                <td align="right">

                    <asp:Button ID="Button5" runat="server" BackColor="Red" Text="Close" OnClientClick="closeModalPopup(); return false;" />

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
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Item Details" Font-Bold="True" Font-Size="10pt" ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-family: Arial; font-size: 14px; color: black; font-weight: bold;">Item Code :</td>
                            <td style="width: 5px"></td>
                            <td style="height: 10px">
                                <asp:Label ID="lblItemCode" Style="font-family: Arial; font-size: 14px; color: navy; font-weight: bold;" runat="server" Text="ds"></asp:Label></td>
                            <td style="width: 5px"></td>
                            <td style="font-family: Arial; font-size: 14px; color: black; font-weight: bold;">Last QTY Purchase :</td>
                            <td style="width: 5px"></td>
                            <td style="height: 10px">
                                <asp:Label ID="Label4" runat="server" Style="font-family: Arial; font-size: 14px; color: navy; font-weight: bold;" Text="ds"></asp:Label></td>
                            <td style="width: 5px"></td>
                            <td style="font-family: Arial; font-size: 14px; color: black; font-weight: bold;">Last QTY Purchase Date :</td>
                            <td style="width: 5px"></td>
                            <td style="height: 10px">
                                <asp:Label ID="Label5" runat="server" Style="font-family: Arial; font-size: 14px; color: navy; font-weight: bold;" Text="ds"></asp:Label></td>
                            <td style="width: 5px"></td>
                            <td style="font-family: Arial; font-size: 14px; color: black; font-weight: bold;">Available QTY :</td>
                            <td style="width: 5px"></td>
                            <td style="height: 10px">
                                <asp:Label ID="Label6" runat="server" Style="font-family: Arial; font-size: 14px; color: navy; font-weight: bold;" Text="ds"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None"
                        CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#3366CC" BorderStyle="None"
                        BorderWidth="1px" CellPadding="4">

                        <Columns>
                            <asp:BoundField DataField="Asset No_" HeaderText="Asset No" />
                            <asp:BoundField DataField="Document No" HeaderText="Document No" />
                            <asp:BoundField DataField="Description" HeaderText="Description" />
                            <%-- <asp:BoundField DataField="Available QTY" HeaderText="Available Quantity" />--%>
                            <%--  <asp:BoundField DataField="Last QTY" HeaderText="Last Quantity" />--%>
                            <%--  <asp:BoundField DataField="Last Purchase Date" HeaderText="Last Purchase Date" DataFormatString="{0:dd/MM/yyyy}" />--%>
                            <%--<asp:BoundField DataField="Last Purchase Price" HeaderText="Last Purchase Price" DataFormatString="{0:C}" />--%>
                            <asp:BoundField DataField="No_" HeaderText="Issue ID" />
                            <asp:BoundField DataField="Name" HeaderText="Issue Name" />
                            <asp:BoundField DataField="Posting Date" HeaderText="Issue Date" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="Location_Room no_" HeaderText="Location" />
                        </Columns>
                        <FooterStyle ForeColor="Green" Font-Bold="true" Font-Size="Medium" BorderStyle="Solid" BorderColor="Black" BackColor="LightGray" />
                        <HeaderStyle BackColor="LightGray" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" Font-Size="Large" Height="40px" VerticalAlign="Bottom" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                        <RowStyle ForeColor="#4A3C8C" Font-Bold="true" Font-Size="Medium" BorderStyle="Solid" BorderColor="Black" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <SortedAscendingCellStyle BackColor="#F4F4FD" />
                        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                        <SortedDescendingCellStyle BackColor="#D8D8F0" />
                        <SortedDescendingHeaderStyle BackColor="#3E3277" />
                    </asp:GridView>

                </td>
            </tr>


            <tr>
                <td style="height: 120px"></td>
            </tr>

        </table>




    </asp:Panel>

     <asp:Button ID="Button6" runat="server" Text="Button" Style="display: none" />
 <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Button6" PopupControlID="Panel1" BackgroundCssClass="modalBackgroundforco" BehaviorID="mpeItemLineDataE"></asp:ModalPopupExtender>


 <asp:Panel ID="Panel1" runat="server" BackColor="White" Style="display: none">


     <table cellpadding="0px" cellspacing="0px">
         <tr>
             <td style="height: 20px"></td>
         </tr>
         <tr>
             <td align="right">

                 <asp:Button ID="Button7" runat="server" BackColor="Red" Text="Close" OnClientClick="closeModalPopupE(); return false;" />

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
                         <td>
                             <asp:Label ID="Label7" runat="server" Text="Item Details" Font-Bold="True" Font-Size="10pt" ForeColor="Black"></asp:Label>
                         </td>
                     </tr>
                     <tr>
                         <td style="font-family: Arial; font-size: 14px; color: black; font-weight: bold;">Item Code :</td>
                         <td style="width: 5px"></td>
                         <td style="height: 10px">
                             <asp:Label ID="Label8" Style="font-family: Arial; font-size: 14px; color: navy; font-weight: bold;" runat="server" Text="ds"></asp:Label></td>
                         <td style="width: 5px"></td>
                         <td style="font-family: Arial; font-size: 14px; color: black; font-weight: bold;">Last QTY Purchase :</td>
                         <td style="width: 5px"></td>
                         <td style="height: 10px">
                             <asp:Label ID="Label9" runat="server" Style="font-family: Arial; font-size: 14px; color: navy; font-weight: bold;" Text="ds"></asp:Label></td>
                         <td style="width: 5px"></td>
                         <td style="font-family: Arial; font-size: 14px; color: black; font-weight: bold;">Last QTY Purchase Date :</td>
                         <td style="width: 5px"></td>
                         <td style="height: 10px">
                             <asp:Label ID="Label10" runat="server" Style="font-family: Arial; font-size: 14px; color: navy; font-weight: bold;" Text="ds"></asp:Label></td>
                         <td style="width: 5px"></td>
                         <td style="font-family: Arial; font-size: 14px; color: black; font-weight: bold;">Available QTY :</td>
                         <td style="width: 5px"></td>
                         <td style="height: 10px">
                             <asp:Label ID="Label11" runat="server" Style="font-family: Arial; font-size: 14px; color: navy; font-weight: bold;" Text="ds"></asp:Label></td>
                     </tr>
                 </table>
             </td>
         </tr>
         <tr>
             <td>
                 <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None"
                     CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#3366CC" BorderStyle="None"
                     BorderWidth="1px" CellPadding="4">

                     <Columns>
                         <asp:BoundField DataField="Asset No_" HeaderText="Asset No" />
                         <asp:BoundField DataField="Document No" HeaderText="Document No" />
                         <asp:BoundField DataField="Description" HeaderText="Description" />
                         <%-- <asp:BoundField DataField="Available QTY" HeaderText="Available Quantity" />--%>
                         <%--  <asp:BoundField DataField="Last QTY" HeaderText="Last Quantity" />--%>
                         <%--  <asp:BoundField DataField="Last Purchase Date" HeaderText="Last Purchase Date" DataFormatString="{0:dd/MM/yyyy}" />--%>
                         <%--<asp:BoundField DataField="Last Purchase Price" HeaderText="Last Purchase Price" DataFormatString="{0:C}" />--%>
                         <asp:BoundField DataField="No_" HeaderText="Issue ID" />
                         <asp:BoundField DataField="Name" HeaderText="Issue Name" />
                         <asp:BoundField DataField="Posting Date" HeaderText="Issue Date" DataFormatString="{0:dd/MM/yyyy}" />
                         <asp:BoundField DataField="Location_Room no_" HeaderText="Location" />
                     </Columns>
                     <FooterStyle ForeColor="Green" Font-Bold="true" Font-Size="Medium" BorderStyle="Solid" BorderColor="Black" BackColor="LightGray" />
                     <HeaderStyle BackColor="LightGray" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" Font-Size="Large" Height="40px" VerticalAlign="Bottom" />
                     <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                     <RowStyle ForeColor="#4A3C8C" Font-Bold="true" Font-Size="Medium" BorderStyle="Solid" BorderColor="Black" />
                     <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                     <SortedAscendingCellStyle BackColor="#F4F4FD" />
                     <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                     <SortedDescendingCellStyle BackColor="#D8D8F0" />
                     <SortedDescendingHeaderStyle BackColor="#3E3277" />
                 </asp:GridView>

             </td>
         </tr>


         <tr>
             <td style="height: 120px"></td>
         </tr>

     </table>




 </asp:Panel>


</asp:Content>

