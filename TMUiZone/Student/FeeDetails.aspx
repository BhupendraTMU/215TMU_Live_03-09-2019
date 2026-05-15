<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="FeeDetails.aspx.cs" Inherits="Student_FeeDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .txtTotal {
            background-color: #ed7600;
            text-align: right;
            border-width: 0px;
            border-color: #ed7600;
        }
    </style>

    <%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <%-- <script type="text/javascript">
        $(function () {
            $("[id*=grdFeeDetails] input[type=checkbox]").click(function () {
              
                if ($(this).is(":checked")) {
                    $("[id*=grdFeeDetails] input[type=checkbox]").removeAttr("checked");
                    $(this).attr("checked", "checked");
                }
            });
        });
</script>--%>



    <script type="text/jscript">


        function OnChangeCheckbox(lnk) {



            //$("[id*=grdFeeDetails] input[type=checkbox]").removeAttr("checked");
            //$('[id$=' + lnk.id + ']').prop('checked', true);

            debugger

            var row = lnk.parentNode.parentNode.parentNode;
            var Description = row.cells[0].innerHTML;

            var Amount = row.cells[1].innerHTML;
            while (Amount.search(",") >= 0) {
                Amount = (Amount + "").replace(',', '');
            }

            if (lnk.checked) {
                //$('[id$=Amt]').val(0);
                //$(this).attr("checked", "checked");
                if ($('[id$=Amt]').val() == '') {

                    $('[id$=Amt]').val(parseFloat(Amount));
                    $('[id$=chkboxSelectAmount]').prop('checked', true);
                    //lnk.prop('checked', true);
                }
                else {
                    $('[id$=Amt]').val(parseFloat($('[id$=Amt]').val()) + parseFloat(Amount));
                    $('[id$=chkboxSelectAmount]').prop('checked', true);
                    //lnk.prop('checked', true);
                }








            }
            else {
                $('[id$=Amt]').val(parseFloat($('[id$=Amt]').val()) - parseFloat(Amount));
            }
            $('[id$=tb1]').text($('[id$=Amt]').val());



        }

        function CheckTotal() {

        }
        <%--$(document).ready(function () {
            $('[id$=btnPay]').click(function () {

                var Enrollment = '<%= Session["enroll"] %>';
                //if (Enrollment != 'TLL1801073') {
                //    alert("Payment process under testing.Please try after time.");
                //    return false;
                //}




                var str = '';
                var str1 = '';

                var TutionSelect = '';
                var TutionUnSelect = '';
                var FineIndivisual = '';
                var Grid_Table = document.getElementById('<%= grdFeeDetails.ClientID %>');

                for (var row = 1; row < Grid_Table.rows.length - 1; row++) {

                    var row1 = row - 1;

                    if (!Grid_Table.rows[row].cells[0].innerText.includes("Fine") && document.getElementById('ContentPlaceHolder1_grdFeeDetails_chkboxSelectAmount_' + row1).checked == true) {

                        FineIndivisual = "Ok";
                        //for (var row = 1; row < Grid_Table.rows.length - 1; row++) {

                        //    var row1 = row - 1;


                        //    if (!Grid_Table.rows[row].cells[0].innerText.includes("Fine") && document.getElementById('ContentPlaceHolder1_grdFeeDetails_chkboxSelectAmount_' + row1).checked == true && FineIndivisual !="Ok") {
                        //        FineIndivisual = "Ok";
                        //    }

                        //}
                    }


                    if (Grid_Table.rows[row].cells[0].innerText == 'Tuition Fee' && document.getElementById('ContentPlaceHolder1_grdFeeDetails_chkboxSelectAmount_' + row1).checked == false) {
                        str = 'Pending';
                    }

                    if (Grid_Table.rows[row].cells[0].innerText == 'Examination Fee' && document.getElementById('ContentPlaceHolder1_grdFeeDetails_chkboxSelectAmount_' + row1).checked == true) {
                        str1 = 'ExamSelect';
                    }
                    if (Grid_Table.rows[row].cells[0].innerText == 'Tuition Fee' && document.getElementById('ContentPlaceHolder1_grdFeeDetails_chkboxSelectAmount_' + row1).checked == true) {

                        for (var row = 1; row < Grid_Table.rows.length - 1; row++) {

                            var row1 = row - 1;



                            if (document.getElementById('ContentPlaceHolder1_grdFeeDetails_chkboxSelectAmount_' + row1).checked == true) {
                                TutionSelect = document.getElementById('ContentPlaceHolder1_grdFeeDetails_hdfOrder_' + row1).value;
                            }
                            else {
                                TutionUnSelect = document.getElementById('ContentPlaceHolder1_grdFeeDetails_hdfOrder_' + row1).value;
                            }
                            if (Grid_Table.rows[row].cells[0].innerText == 'Tuition Fee Fine' && document.getElementById('ContentPlaceHolder1_grdFeeDetails_chkboxSelectAmount_' + row1).checked == false) {

                                alert("Please Select Tution Fee Fine");
                                return false;
                            }

                        }
                    }
                    if (Grid_Table.rows[row].cells[0].innerText == 'Examination Fee' && document.getElementById('ContentPlaceHolder1_grdFeeDetails_chkboxSelectAmount_' + row1).checked == true) {

                        for (var row = 1; row < Grid_Table.rows.length - 1; row++) {

                            var row1 = row - 1;


                            if (Grid_Table.rows[row].cells[0].innerText == 'Exam Fee Fine' && document.getElementById('ContentPlaceHolder1_grdFeeDetails_chkboxSelectAmount_' + row1).checked == false) {
                                alert("Please Select Exam Fee Fine");
                                return false;
                            }

                        }
                    }
                    if (Grid_Table.rows[row].cells[0].innerText == 'Hostel Fee' && document.getElementById('ContentPlaceHolder1_grdFeeDetails_chkboxSelectAmount_' + row1).checked == true) {

                        for (var row = 1; row < Grid_Table.rows.length - 1; row++) {

                            var row1 = row - 1;


                            if (Grid_Table.rows[row].cells[0].innerText == 'Hostel Fee Fine' && document.getElementById('ContentPlaceHolder1_grdFeeDetails_chkboxSelectAmount_' + row1).checked == false) {
                                alert("Please Select Hostel Fee Fine");
                                return false;
                            }

                        }
                    }


                }
                if (str == 'Pending' && str1 == 'ExamSelect') {
                    alert("Please Clear Your Tution Fee First");
                    return false;
                }


                if (parseInt(TutionSelect) > parseInt(TutionUnSelect)) {
                    alert("Please Clear Your Previous Semester Tution Fee First");
                    return false;
                }
                if (parseInt(TutionSelect) > parseInt(TutionUnSelect)) {
                    alert("Please Clear Your Previous Semester Tution Fee First");
                    return false;
                }



                    if (FineIndivisual=="") {
                        alert("You can not Pay fine Indivisually");
                        return false;
                    }
                else {
                    $('[id$=Amt]').val($('[id$=tb1]').text());
                    $('[id$=btnHide]').click();
                    return false;
                }
            });
        });--%>

    </script>
    <script type="text/javascript" src="Script/jquery-1.5.1min.js"></script>

    <script type="text/javascript" language="javascript">

  

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <br />
    <%-- <asp:Label ID="lblfeemsg" Text="Please contact fee section" ForeColor="Red" runat="server" Font-Size="X-Large"></asp:Label>--%>
    <div id="hid" runat="server">
        <%--Hide fee 13-10-2018 by rajesh sir--%>
        <fieldset class="boxBody">
            <asp:Label ID="Label3" runat="server"
                Text="Fee Details" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
        </fieldset>
        <fieldset class="boxBodyHeader">
        </fieldset>

       <fieldset style="border-top: 1px solid #dde0e8; border-bottom: 1px solid #dde0e8; padding: 10px 20px; height: 100%">
    <asp:HiddenField runat="server" ID="Amt" />
    <center>
        <table align="center" width="100%">

            <tr>

                <td style="height: 30px" align="center" valign="middle">
                    <asp:Label runat="server" ID="lblPaidFee" Font-Size="15px" Text="Paid Fee" Visible="false"></asp:Label>
                </td>

                <td style="height: 30px" align="left" valign="middle">
                    <asp:Label runat="server" ID="lblUnpaidFee" Font-Bold="true" Font-Size="20px" Text="Pending Dues:-"></asp:Label>
                </td>

            </tr>
            <tr >

                <td colspan="2">
                    <asp:GridView ID="grdFeeDetails" runat="server"  AutoGenerateColumns="False" BackColor="White" BorderColor="LightGray" HeaderStyle-Font-Size="Large"
                        EmptyDataText="There are no data records to display." BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="1100px" ShowFooter="true"
                        GridLines="Horizontal">
                        <Columns>
                            <%--HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" ItemStyle-HorizontalAlign="Right"--%>
                            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Amount" HeaderText="Fee Amount" Visible="false" SortExpression="TotalAmount" DataFormatString="{0:N2}" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="RemainingAmount" HeaderText="Remaining Amount" SortExpression="ApplicantName"
                                DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg"></asp:BoundField>
                            <asp:TemplateField HeaderText="Sem/Year" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                <ItemTemplate>
                                    <asp:Label ID="lblSem" Text='<%# Bind("Semester") %>' runat="server" />
                                    <asp:HiddenField ID="hdfEntryNo" Value='<%# Bind("[Entry No_]") %>' runat="server" />
                                    <asp:HiddenField ID="hdfDesc" Value='<%# Bind("[Description]") %>' runat="server" />
                                    <asp:HiddenField ID="hdfOrder" Value='<%# Bind("[semvalue]") %>' runat="server" />
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Select" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkboxSelectAmount" HeaderText="Select" onclick="OnChangeCheckbox (this)" runat="server" />
                                </ItemTemplate>
                                <HeaderTemplate>Select</HeaderTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="Entry No_" HeaderText="Entry No" SortExpression="ApplicantName" ItemStyle-Font-Size="0" HeaderStyle-Font-Size="0" />
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <HeaderStyle BackColor="LightGray" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                        <RowStyle CssClass="cssGridheaderfont" Font-Size="Larger" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <SortedAscendingCellStyle BackColor="#F4F4FD" />
                        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                        <SortedDescendingCellStyle BackColor="#D8D8F0" />
                        <SortedDescendingHeaderStyle BackColor="#3E3277" />
                    </asp:GridView>



                </td>

            </tr>
            <tr runat="server" visible="false">
                <td colspan="2">
                    <asp:GridView ID="grdPaidFeesDetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" Width="1150px" GridLines="Horizontal" ShowFooter="true"
                        OnPageIndexChanging="grdPaidFeesDetails_PageIndexChanging" EmptyDataText="There are no data records to display." AllowPaging="True" HorizontalAlign="Center">
                        <Columns>
                            <%--HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" ItemStyle-HorizontalAlign="Right"--%>
                            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="ApplicantName" />
                            <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="ApplicantName" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                DataFormatString="{0:N1}">
                                <FooterStyle HorizontalAlign="Right"></FooterStyle>

                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#F7F7F7" />
                        <%--<FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />--%>
                        <FooterStyle BackColor="#ed7600" ForeColor="#F7F7F7" CssClass="cssGridheaderfont" />
                        <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                        <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <SortedAscendingCellStyle BackColor="#F4F4FD" />
                        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                        <SortedDescendingCellStyle BackColor="#D8D8F0" />
                        <SortedDescendingHeaderStyle BackColor="#3E3277" />
                    </asp:GridView>
                </td>

            </tr>
            <tr>

                <td colspan="2" style="width: 96%" align="right">
                    <div class="btn pull-right">
                        <asp:Button ID="btnPay" runat="server" CssClass="btn-sm btn-primary btn-block" Height="35px" Width="90px" Text="Pay" OnClick="btnPay_Click" />
                         
                    </div>
                </td>

            </tr>
            <tr>
                <td colspan="2" style="width: 96%">
                    <asp:Label runat="server" ID="lblMsg"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hash" runat="server" />
        <asp:HiddenField ID="txnid" runat="server" />
        <asp:HiddenField ID="key" runat="server" />
    </center>
    <asp:Label runat="server" ID="lblTotal"></asp:Label>
    <%--<asp:Label ID="lblNote" runat="server" Text=" &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp   Note: Payment Status will be updated with in 2 working days. For fee related any query please mail us on feequery@tmu.ac.in." ForeColor="Red" Font-Bold="true"></asp:Label>--%>
</fieldset>

        <div class="modal fade" id="ModalPaytm" role="dialog">
            <div class="modal-dialog" style="width: 420px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #88CCFF;">
                        <div>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title"><b style="font-family: Arial; font-size: 15px">Payment through</b></h4>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="pull-left">
                            <asp:ImageButton runat="server" ImageUrl="~/images/Paytm1.jpg" Height="45px" Width="120px" ID="btnPaytm"  />
                           <%-- OnClick="btnPaytm_Click"--%>

                        </div>

                        <center>
                            <div class="col-lg-3" style="padding-right: 0px; font-size: 12px;">
                                <%-- <label style="font-weight:bold; font-size:30px">or</label>--%>
                            </div>
                        </center>
                        <%--  <div class="pull-right">                                    
                                <asp:ImageButton runat="server" ImageUrl="~/images/PayUmoney1.jpg"  Height="45px" Width="160px" ID="btnPayUmoney"  OnClick="btnPay_Click" />                                   
                        </div> --%>
                    </div>
                    <div class="modal-footer" style="border-top-width: 0px">
                        <%--<asp:Button runat="server" ID="btnClose"  CssClass="btn-sm btn-primary"  class="close" data-dismiss="modal" Width="20%"  Text="Close"></asp:Button>--%>
                    </div>
                </div>
                <asp:Button runat="server" Width="0px" Height="0px" ID="btnHide" data-toggle="modal" OnClientClick="return false" data-target="#ModalPaytm" />

            </div>

        </div>

    </div>
</asp:Content>

