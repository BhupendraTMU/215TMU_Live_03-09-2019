<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="FeeDetails.aspx.cs" Inherits="Student_FeeDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <style> /* Main Card */ .fee-card { width: 100%; background: #ffffff; border-radius: 14px; box-shadow: 0 4px 20px rgba(0,0,0,0.08); border: 1px solid #eeeeee; overflow: hidden; margin: 15px auto; } /* Header */ .fee-card-header { display: flex; justify-content: space-between; align-items: center; padding: 20px 25px; background: linear-gradient(135deg, #f8f9ff, #ffffff); border-bottom: 1px solid #eeeeee; } .fee-title { font-size: 22px; font-weight: 700; color: #222222; } .fee-title i { margin-right: 8px; } .fee-subtitle { margin-top: 5px; font-size: 13px; color: #777777; } /* Pending Badge */ .pending-badge { background: #fff3cd; color: #856404; padding: 7px 14px; border-radius: 20px; font-size: 13px; font-weight: 600; } /* Section */ .section-title { display: flex; align-items: center; gap: 10px; padding: 18px 25px 12px; font-size: 18px; font-weight: 700; color: #333333; } .section-icon { width: 34px; height: 34px; display: flex; align-items: center; justify-content: center; border-radius: 50%; background: #f1f5ff; } /* Grid Wrapper */ .fee-grid-wrapper { width: 100%; overflow-x: auto; padding: 0 20px 10px; } /* Grid */ .modern-fee-grid { width: 100% !important; border-collapse: separate !important; border-spacing: 0 7px !important; font-size: 14px; } /* Header */ .modern-grid-header th { background: #f6f7fb !important; color: #555555 !important; font-weight: 600 !important; padding: 13px 15px !important; border: none !important; text-align: left; white-space: nowrap; } .modern-grid-header th:first-child { border-radius: 8px 0 0 8px; } .modern-grid-header th:last-child { border-radius: 0 8px 8px 0; text-align: center; } /* Rows */ .modern-grid-row td, .modern-grid-alt-row td { background: #ffffff !important; padding: 15px !important; border-top: 1px solid #eeeeee !important; border-bottom: 1px solid #eeeeee !important; color: #333333; vertical-align: middle; } .modern-grid-row td:first-child, .modern-grid-alt-row td:first-child { border-left: 1px solid #eeeeee !important; border-radius: 8px 0 0 8px; font-weight: 500; } .modern-grid-row td:last-child, .modern-grid-alt-row td:last-child { border-right: 1px solid #eeeeee !important; border-radius: 0 8px 8px 0; text-align: center; } /* Hover */ .modern-grid-row:hover td, .modern-grid-alt-row:hover td { background: #f9faff !important; } /* Semester */ .semester-badge { display: inline-block; padding: 5px 10px; background: #f1f5ff; border-radius: 6px; font-size: 13px; font-weight: 600; white-space: nowrap; } /* Checkbox */ .modern-checkbox { display: flex; justify-content: center; align-items: center; } .modern-checkbox input[type="checkbox"] { width: 19px; height: 19px; cursor: pointer; accent-color: #0d6efd; } /* Select Header */ .select-header { text-align: center; } /* Hide Entry No */ .hidden-column { display: none !important; } /* Footer */ .modern-grid-footer td { padding: 8px; background: transparent !important; border: none !important; } /* Payment Footer */ .payment-footer { display: flex; align-items: center; justify-content: space-between; padding: 18px 25px; margin-top: 5px; background: #fafafa; border-top: 1px solid #eeeeee; } .selected-info { display: flex; align-items: center; gap: 8px; color: #777777; font-size: 14px; } .selected-icon { font-size: 18px; } /* Pay Button */ .modern-pay-btn { border: none !important; border-radius: 8px !important; background: linear-gradient(135deg, #0d6efd, #0056d6) !important; color: white !important; font-size: 14px !important; font-weight: 600 !important; cursor: pointer; box-shadow: 0 4px 10px rgba(13,110,253,0.25); transition: all 0.2s ease; } .modern-pay-btn:hover { transform: translateY(-1px); box-shadow: 0 6px 14px rgba(13,110,253,0.35); } /* Message */ .message-area { padding: 0 25px 20px; text-align: center; } /* Mobile */ @media (max-width: 768px) { .fee-card { margin: 8px 0; border-radius: 10px; } .fee-card-header { padding: 15px; } .fee-title { font-size: 18px; } .fee-subtitle { font-size: 12px; } .pending-badge { display: none; } .section-title { padding: 15px; font-size: 16px; } .fee-grid-wrapper { padding: 0 10px 10px; } .modern-fee-grid { min-width: 650px; } .payment-footer { padding: 15px; } .selected-info { font-size: 12px; } } </style>


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
        <div class="fee-card"> 
            <div class="fee-card-header"> <div> <div class="fee-title"> 
                <i class="fa fa-credit-card"></i> Fee Details </div> 
                <div class="fee-subtitle"> Please select the pending dues you want to pay </div> </div> 
                <div class="pending-badge"> Pending Dues </div> </div> 
            <asp:Label runat="server" ID="lblPaidFee" Font-Size="15px" Text="Paid Fee" Visible="false"> 

            </asp:Label>  <div class="section-title"> 
                <span class="section-icon"> <i class="fa fa-clock-o"></i>

                </span> <span> Pending Dues </span> </div> 
            <div class="fee-grid-wrapper"> 
                <asp:GridView ID="grdFeeDetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="Transparent" BorderStyle="None" BorderWidth="0px" CellPadding="0" Width="100%" ShowFooter="true" GridLines="None" EmptyDataText="There are no pending dues to display." CssClass="modern-fee-grid"> <Columns> 
                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="ApplicantName"> </asp:BoundField>
                    <asp:BoundField DataField="Amount" HeaderText="Fee Amount" Visible="false" SortExpression="TotalAmount" DataFormatString="{0:N2}"> 

                    </asp:BoundField>
                    <asp:BoundField DataField="RemainingAmount" HeaderText="Remaining Amount" SortExpression="ApplicantName" DataFormatString="{0:N2}"> </asp:BoundField> 
                    <asp:TemplateField HeaderText="Semester / Year"> <ItemTemplate> <span class="semester-badge"> <%# Eval("Semester") %> </span> <asp:HiddenField ID="hdfEntryNo" Value='<%# Bind("[Entry No_]") %>' runat="server" /> <asp:HiddenField ID="hdfDesc" Value='<%# Bind("[Description]") %>' runat="server" /> <asp:HiddenField ID="hdfOrder" Value='<%# Bind("[semvalue]") %>' runat="server" /> </ItemTemplate> </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Select"> <HeaderTemplate> <div class="select-header"> Select </div> </HeaderTemplate> <ItemTemplate> <div class="modern-checkbox"> <asp:CheckBox ID="chkboxSelectAmount" runat="server" onclick="OnChangeCheckbox(this)" /> </div> </ItemTemplate> </asp:TemplateField> 
                    <asp:BoundField DataField="Entry No_" HeaderText="Entry No" ItemStyle-CssClass="hidden-column" HeaderStyle-CssClass="hidden-column"> </asp:BoundField> </Columns> <HeaderStyle CssClass="modern-grid-header" /> <RowStyle CssClass="modern-grid-row" /> <AlternatingRowStyle CssClass="modern-grid-alt-row" /> <FooterStyle CssClass="modern-grid-footer" /> </asp:GridView> </div> 
            <div style="display:none;"> <asp:GridView ID="grdPaidFeesDetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Horizontal" ShowFooter="true" OnPageIndexChanging="grdPaidFeesDetails_PageIndexChanging" EmptyDataText="There are no data records to display." AllowPaging="True" HorizontalAlign="Center"> <Columns> <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="ApplicantName" /> <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="ApplicantName" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N1}" /> </Columns> </asp:GridView> </div> 
            <div class="payment-footer"> <div class="selected-info"> <span class="selected-icon"> <i class="fa fa-check-circle"></i> </span> <span id="selectedFeeText"> Select fee(s) to continue </span> </div> <asp:Button ID="btnPay" runat="server" CssClass="modern-pay-btn" Height="42px" Width="110px" Text="Pay Now" OnClick="btnPay_Click" /> </div> 
            <div class="message-area"> <asp:Label runat="server" ID="lblMsg"> </asp:Label> </div> </div>
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

