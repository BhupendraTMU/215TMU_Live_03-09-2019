<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="EmployeePaySlip.aspx.cs" Inherits="Faculty_EmployeePaySlip" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../bootstrap/js/jquery-1.11.2.min.js"></script>
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../bootstrap/js/bootstrap.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.5.1/jspdf.umd.min.js"></script>

<!-- html2canvas -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/1.4.1/html2canvas.min.js"></script>

<script>

    async function downloadSalarySlipPDF() {

        const element = document.getElementById("salarySlipPrintArea");

        if (!element) {
            alert("Salary Slip area not found.");
            return;
        }

        // Temporarily remove shadow/border effect if required
        const oldWidth = element.style.width;

        element.style.width = "794px";

        // Wait for images/fonts
        await new Promise(resolve => setTimeout(resolve, 300));

        const canvas = await html2canvas(element, {
            scale: 2,
            useCORS: true,
            allowTaint: false,
            backgroundColor: "#ffffff",
            logging: false
        });

        const imgData = canvas.toDataURL("image/png");

        const {
            jsPDF
        } = window.jspdf;

        // A4 Portrait
        const pdf = new jsPDF({
            orientation: "portrait",
            unit: "mm",
            format: "a4"
        });

        const pageWidth = 210;
        const pageHeight = 297;

        // Margin
        const margin = 8;

        const availableWidth = pageWidth - (margin * 2);

        const imgWidth = availableWidth;

        const imgHeight =
            (canvas.height * imgWidth) / canvas.width;

        let heightLeft = imgHeight;

        let position = margin;

        // First page
        pdf.addImage(
            imgData,
            "PNG",
            margin,
            position,
            imgWidth,
            imgHeight,
            undefined,
            "FAST"
        );

        heightLeft -= (pageHeight - margin * 2);

        // Additional pages if content is longer
        while (heightLeft > 0) {

            position =
                heightLeft - imgHeight + margin;

            pdf.addPage();

            pdf.addImage(
                imgData,
                "PNG",
                margin,
                position,
                imgWidth,
                imgHeight,
                undefined,
                "FAST"
            );

            heightLeft -= (pageHeight - margin * 2);
        }

        pdf.save("Salary_Slip.pdf");

        // Restore width
        element.style.width = oldWidth;
    }




        function formatAmount(value) {

            if (
                value === null ||
                value === undefined ||
                value === ""
            ) {
                return "0";
            }

            return Number(value).toLocaleString(
                'en-IN',
                {
                    minimumFractionDigits: 0,
                    maximumFractionDigits: 2
                }
            );
        }
        function ViewSalarySlip(employeeNo, btn) {

            var row = btn.closest("tr");

            // Current row ka ddlMonth
            var ddlMonth = row.querySelector("select[id*='ddlMonth']");

            if (!ddlMonth) {
                alert("Month dropdown not found.");
                return false;
            }

            // SelectedValue
            var selectedMonth = ddlMonth.value;

            if (!selectedMonth) {
                alert("Please select month.");
                return false;
            }
            else {
                month = selectedMonth;
            }


            $("#earningBody").html("");
            $("#deductionBody").html("");

            $("#lblEmployeeName").text("Loading...");
            $("#lblEmployeeId").text(employeeNo);
            $("#lblSalaryMonth").text(month);

            // Open Modal
            $("#salarySlipModal").modal("show");

            // AJAX
            $.ajax({
                type: "POST",
                url: "EmployeePaySlip.aspx/GetSalarySlip",

                data: JSON.stringify({
                    employeeNo: employeeNo,
                    month: month
                }),

                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function (response) {

                    var data = response.d;

                    if (typeof data === "string") {
                        data = JSON.parse(data);
                    }

                    if (!data || data.length === 0) {

                        alert("Salary slip data not found.");

                        return;
                    }


                    var row = data[0];


                    // =====================================================
                    // EMPLOYEE DETAILS
                    // =====================================================

                    $("#lblSalaryMonth").text(
                        row.SalaryMonth || ""
                    );

                    $("#lblEmployeeName").text(
                        row.EmployeeName || ""
                    );

                    $("#lblEmployeeId").text(
                        row.EmployeeNo || ""
                    );

                    $("#lblFatherName").text(
                        row.FatherName || ""
                    );

                    $("#lblDOJ").text(
                        row.DOJ || ""
                    );

                    $("#lblPAN").text(
                        row.PAN || ""
                    );


                    $("#lblUnit").text(
                        row.Unit || ""
                    );

                    $("#lblDesignation").text(
                        row.DesignationName || ""
                    );

                    $("#lblDepartment").text(
                        row.DepartmentName || ""
                    );

                    $("#lblESI").text(
                        row.ESINo || ""
                    );

                    $("#lblUAN").text(
                        row.UAN || ""
                    );


                    // =====================================================
                    // EARNINGS
                    // =====================================================

                    var earningHtml = "";


                    if (row.Earnings && row.Earnings.length > 0) {

                        $.each(row.Earnings, function (i, item) {

                            earningHtml +=

                                "<tr>" +

                                "<td>" +
                                (item.Particular || "") +
                                "</td>" +

                                "<td>" +
                                formatAmount(item.PayRate) +
                                "</td>" +

                                "<td>" +
                                (item.PaidDays || "") +
                                "</td>" +

                                "<td>" +
                                formatAmount(item.PayEarned) +
                                "</td>" +

                                "</tr>";

                        });

                    }


                    $("#earningBody").html(
                        earningHtml
                    );


                    // =====================================================
                    // DEDUCTIONS
                    // =====================================================

                    var deductionHtml = "";


                    if (row.Deductions && row.Deductions.length > 0) {

                        $.each(row.Deductions, function (i, item) {

                            deductionHtml +=

                                "<tr>" +

                                "<td>" +
                                (item.Particular || "") +
                                "</td>" +

                                "<td>" +
                                formatAmount(item.Amount) +
                                "</td>" +

                                "</tr>";

                        });

                    }


                    $("#deductionBody").html(
                        deductionHtml
                    );


                    // =====================================================
                    // TOTALS
                    // =====================================================

                    $("#lblGrossEarning").text(
                        formatAmount(row.GrossEarning)
                    );


                    $("#lblGrossDeduction").text(
                        formatAmount(row.GrossDeduction)
                    );


                    $("#lblNetSalary").text(
                        formatAmount(row.NetSalary)
                    );


                    // =====================================================
                    // OTHER DETAILS
                    // =====================================================

                    $("#lblSalaryInWords").text(
                        row.SalaryInWords || ""
                    );


                    $("#lblAttendanceDetails").text(
                        row.AttendanceDetails || ""
                    );

                },

                error: function (xhr) {
                    console.log(xhr.responseText);
                    alert("Unable to load salary slip.");
                }
            });
        }


        function showPopup2() {
            $find("mpe2").show();
        }
        function closePopup2() {
            $find("mpe2").hide();   // modal hide
        }
        function showPopup3() {
            $find("mpe3").show();
        }
        function closePopup3() {
            $find("mpe3").hide();   // modal hide
        }

        function copyMonthsToHidden() {
            var fromVal = document.getElementById('ContentPlaceHolder1_fromMonth').value;
            var toVal = document.getElementById('ContentPlaceHolder1_toMonth').value;

            // Find hidden fields by ClientID from server controls:
            document.getElementById('<%= hfFromMonth.ClientID %>').value = fromVal;
            document.getElementById('<%= hfToMonth.ClientID %>').value = toVal;
        }
        function ViewUserDetails(No) {
            $.ajax({
                url: 'EmployeePaySlip.aspx/GetEmployeeDetailList',
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ employeeNo: No }),
                success: function (result) {
                    const data = result.d;

                    if (!data || data.length === 0) {
                        alert("No data found.");
                        return;
                    }

                    // Clear previous data
                    $('#gridHeader').empty();
                    $('#gridBody').empty();

                    // If it's a single object, wrap it into an array
                    const dataArray = Array.isArray(data) ? data : [data];

                    // Generate header from first object keys
                    const keys = Object.keys(dataArray[0]);
                    keys.forEach(key => {
                        $('#gridHeader').append(`<th>${key}</th>`);
                    });

                    // Generate body rows
                    dataArray.forEach(item => {
                        let row = '<tr>';
                        keys.forEach(key => {
                            row += `<td>${item[key] != null ? item[key] : ''}</td>`;
                        });
                        row += '</tr>';
                        $('#gridBody').append(row);
                    });

                    // Show modal
                    $("#userDetailModal").modal("show");
                },
                error: function () {
                    alert('Error loading employee details.');
                }
            });
        }

    </script>

    <style>
        /* =========================================================
       SALARY SLIP MODAL
       ========================================================= */

        .salary-slip-modal-dialog {
            max-width: 1200px !important;
            width: 96% !important;
        }

        .salary-slip-modal-content {
            border-radius: 5px;
            overflow: hidden;
        }

        #salarySlipModal .modal-body {
            padding: 15px;
            background: #f5f5f5;
        }


        /* =========================================================
       MAIN SALARY SLIP
       ========================================================= */

        .salary-slip {
            width: 100%;
            max-width: 1120px;
            margin: 0 auto;
            background: #ffffff;
            border: 1px solid #000;
            color: #000;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 13px;
            position: relative;
            overflow: hidden;
        }


            /* =========================================================
       WATERMARK
       ========================================================= */

            .salary-slip::before {
                content: "";
                position: absolute;
                top: 50%;
                left: 50%;
                width: 500px;
                height: 500px;
                transform: translate(-50%, -50%);
                background-image: url('../images/tmulogo.png');
                background-repeat: no-repeat;
                background-position: center;
                background-size: contain;
                opacity: 0.07;
                z-index: 0;
                pointer-events: none;
            }


            /* All salary content above watermark */

            .salary-slip > * {
                position: relative;
                z-index: 1;
            }


        /* =========================================================
       COMPANY HEADER
       ========================================================= */

        .company-header {
            position: relative;
            min-height: 90px;
            text-align: center;
            padding: 5px 10px;
            border-bottom: 1px solid #000;
            background: #fff;
        }


        .company-logo {
            position: absolute;
            left: 12px;
            top: 5px;
            width: 78px;
            height: 72px;
            object-fit: contain;
        }


        .company-name {
            font-size: 21px;
            font-weight: bold;
            line-height: 27px;
            padding-top: 2px;
        }


        .company-address {
            font-size: 14px;
            line-height: 20px;
        }


        .pay-slip-title {
            font-size: 17px;
            font-weight: bold;
            line-height: 22px;
        }


        /* =========================================================
       SECTION TITLE
       ========================================================= */

        .section-title {
            background: #dddcc9;
            border-bottom: 1px solid #000;
            padding: 5px 7px;
            font-size: 14px;
            font-weight: bold;
            text-align: left;
        }


        /* =========================================================
       EMPLOYEE DETAILS
       NO GAP BETWEEN LEFT AND RIGHT
       ========================================================= */

        .employee-details {
            display: flex;
            width: 100%;
            border-bottom: 1px solid #000;
        }


        .employee-left {
            width: 50%;
            border-right: 1px solid #000;
        }


        .employee-right {
            width: 50%;
        }


        .employee-table {
            width: 100%;
            border-collapse: collapse;
            table-layout: fixed;
        }


            .employee-table td {
                border-bottom: 1px solid #000;
                padding: 5px 7px;
                height: 29px;
                vertical-align: middle;
            }


            .employee-table tr:last-child td {
                border-bottom: none;
            }


        /* Employee labels */

        .employee-label {
            width: 36%;
            color: #000;
            font-weight: normal;
            text-align: left;
            white-space: nowrap;
        }


        /* Employee values */

        .employee-value {
            width: 64%;
            color: #000;
            font-weight: normal;
            text-align: left;
            white-space: normal;
            word-break: break-word;
            overflow-wrap: anywhere;
        }


        /* =========================================================
       SALARY ROW
       ========================================================= */

        .salary-row {
            display: flex;
            width: 100%;
            align-items: flex-start;
        }


        /* =========================================================
       EARNINGS
       ========================================================= */

        .earnings-column {
            width: calc(65% - 8px);
            flex-shrink: 0;
        }


        /* =========================================================
       GAP BETWEEN EARNING AND DEDUCTION
       ========================================================= */

        .salary-column-gap {
            width: 16px;
            flex-shrink: 0;
            background: #ffffff;
        }


        /* =========================================================
       DEDUCTIONS
       ========================================================= */

        .deduction-column {
            width: calc(35% - 8px);
            flex-shrink: 0;
        }


        /* =========================================================
       EARNING / DEDUCTION HEADING
       ========================================================= */

        .salary-heading {
            height: 33px;
            line-height: 33px;
            background: #dddcc9;
            border: 1px solid #000;
            text-align: center;
            font-size: 14px;
            font-weight: bold;
        }


        /* =========================================================
       SALARY TABLE
       ========================================================= */

        .salary-table {
            width: 100%;
            border-collapse: collapse;
            table-layout: fixed;
        }


            .salary-table th,
            .salary-table td {
                border: 1px solid #000;
                padding: 4px 6px;
                height: 29px;
                vertical-align: middle;
                color: #000;
            }


            /* Header */

            .salary-table thead th {
                background: #f5f4ea;
                font-weight: normal;
                text-align: center;
                line-height: 16px;
                height: 41px;
            }

            .salary-table tbody tr {
                height: 29px !important;
            }

            .salary-table tbody td {
                height: 29px !important;
                min-height: 29px !important;
                padding: 5px 6px !important;
                line-height: 18px !important;
                vertical-align: middle !important;
                box-sizing: border-box;
            }


        /* Deduction rows specifically */
        .deduction-table tbody tr {
            height: 29px !important;
        }

        .deduction-table tbody td {
            height: 29px !important;
            min-height: 29px !important;
            padding: 5px 6px !important;
            line-height: 18px !important;
        }


        /* Blank deduction row */
        .deduction-table .blank-salary-row td {
            height: 29px !important;
            min-height: 29px !important;
        }


        /* Earning rows */
        .earnings-table tbody tr {
            height: 29px !important;
        }

        .earnings-table tbody td {
            height: 29px !important;
            min-height: 29px !important;
        }

        /* =========================================================
       EARNING TABLE COLUMNS
       ========================================================= */

        .earnings-table th:nth-child(1),
        .earnings-table td:nth-child(1) {
            width: 40%;
            text-align: left;
        }


        .earnings-table th:nth-child(2),
        .earnings-table td:nth-child(2) {
            width: 18%;
            text-align: right;
        }


        .earnings-table th:nth-child(3),
        .earnings-table td:nth-child(3) {
            width: 14%;
            text-align: center;
        }


        .earnings-table th:nth-child(4),
        .earnings-table td:nth-child(4) {
            width: 28%;
            text-align: right;
        }


        /* =========================================================
       DEDUCTION TABLE COLUMNS
       ========================================================= */

        .deduction-table th:nth-child(1),
        .deduction-table td:nth-child(1) {
            width: 68%;
            text-align: left;
        }


        .deduction-table th:nth-child(2),
        .deduction-table td:nth-child(2) {
            width: 32%;
            text-align: right;
        }


        /* =========================================================
       BLANK ROW
       This is important.
       Deduction has 6 actual records.
       Earnings has 7 records.
       Therefore one blank deduction row is added.
       ========================================================= */

        .blank-salary-row td {
            height: 29px;
            padding: 4px 6px;
            background: #fff;
        }


        /* =========================================================
       TOTAL ROW
       ========================================================= */

        .total-row {
            background: #f5f4ea;
            font-weight: bold;
        }


            .total-row th,
            .total-row td {
                height: 32px;
                font-weight: bold;
            }


        .earnings-table .total-row th {
            text-align: right;
        }


        .earnings-table .total-row td {
            text-align: right;
        }


        .deduction-table .total-row th {
            text-align: right;
        }


        .deduction-table .total-row td {
            text-align: right;
        }


        /* =========================================================
       NET SALARY
       ========================================================= */

        .net-salary-box {
            border-top: 1px solid #000;
            border-bottom: 1px solid #000;
            padding: 6px 7px;
            font-size: 13px;
            line-height: 20px;
            color: #000;
        }


        .salary-amount {
            font-weight: bold;
        }


        .amount-words {
            font-weight: normal;
        }


        /* =========================================================
       ATTENDANCE
       ========================================================= */

        .attendance-box {
            border-bottom: 1px solid #000;
            padding: 5px 7px;
            font-size: 13px;
            line-height: 20px;
            color: #000;
        }


        /* =========================================================
       FOOTER
       ========================================================= */

        .salary-footer {
            padding: 5px 7px;
            font-size: 11px;
            color: #000;
        }


        /* =========================================================
       MODAL FOOTER
       ========================================================= */

        #salarySlipModal .modal-footer {
            padding: 10px 15px;
        }
        /* =========================================
   PAYSLIP REQUEST POPUP
   ========================================= */

        .modalPopup {
            background: #fff;
            border: 2px solid #ed7600;
            border-radius: 10px;
            box-shadow: 0 5px 20px rgba(0,0,0,0.25);
            overflow: hidden;
        }

            /* Header */
            .modalPopup .header {
                height: 42px;
                line-height: 42px;
                background: #ed7600;
                color: #fff;
                padding: 0 12px;
                font-size: 14px;
                position: relative;
            }

                .modalPopup .header .close {
                    position: absolute;
                    right: 8px;
                    top: 5px;
                }

                    .modalPopup .header .close input,
                    .modalPopup .header .close button {
                        width: 25px;
                        height: 28px;
                        border: none;
                        background: transparent;
                        color: #8a4a00;
                        font-size: 20px;
                        font-weight: bold;
                        cursor: pointer;
                    }

            /* Body */
            .modalPopup .body {
                padding: 18px 20px 15px 20px;
                background: #fff;
            }

            /* Two column layout */
            .modalPopup .section {
                display: grid;
                grid-template-columns: 1fr 1fr;
                column-gap: 25px;
                row-gap: 15px;
                width: 100%;
            }

                /* Individual field */
                .modalPopup .section .field {
                    width: 100%;
                    display: flex;
                    align-items: center;
                }

                    /* Label */
                    .modalPopup .section .field label {
                        width: 115px;
                        min-width: 115px;
                        margin: 0;
                        padding-right: 10px;
                        font-weight: bold;
                        font-size: 13px;
                        color: #333;
                        text-align: right;
                    }

                    /* Textbox */
                    .modalPopup .section .field input[type="text"],
                    .modalPopup .section .field input[type="month"] {
                        width: calc(100% - 115px);
                        height: 35px;
                        border: 1px solid #ccc;
                        border-radius: 2px;
                        padding: 5px 8px;
                        font-size: 13px;
                        background: #fff;
                        box-sizing: border-box;
                    }

                    /* Disabled textbox */
                    .modalPopup .section .field input[disabled] {
                        background: #f5f5f5;
                        color: #333;
                        opacity: 1;
                    }

            /* Submit button */
            .modalPopup .submit-btn {
                margin-top: 18px;
                margin-bottom: 0 !important;
                margin-left: 140px;
                padding: 7px 18px;
                height: 36px;
                border: 1px solid #ccc;
                background: #f5f5f5;
                color: #333;
                font-weight: bold;
                cursor: pointer;
            }

                .modalPopup .submit-btn:hover {
                    background: #e9e9e9;
                }
        /* =========================================
   PAYSLIP REQUEST GRIDVIEW - CELL BORDER
   ========================================= */

        .grid-cell-border {
            width: 100%;
            border-collapse: collapse !important;
            border-spacing: 0 !important;
            border: 1px solid #000 !important;
        }

            /* Header */
            .grid-cell-border th {
                border: 1px solid #000 !important;
                padding: 5px 6px !important;
                background-color: #ed7600 !important;
                color: #fff !important;
                font-weight: bold !important;
                text-align: center !important;
                vertical-align: middle !important;
                white-space: normal !important;
            }

            /* All cells */
            .grid-cell-border td {
                border: 1px solid #000 !important;
                padding: 5px 6px !important;
                vertical-align: middle !important;
                color: #222 !important;
                background-color: #fff;
            }

            /* Alternate row */
            .grid-cell-border tr:nth-child(even) td {
                background-color: #f7f7f7;
            }

            /* Hover */
            .grid-cell-border tr:hover td {
                background-color: #f1f1f1;
            }

            /* Remove Bootstrap table border interference */
            .grid-cell-border.table,
            .grid-cell-border.table-bordered {
                border: 1px solid #000 !important;
            }

                .grid-cell-border.table > tbody > tr > td,
                .grid-cell-border.table > tbody > tr > th,
                .grid-cell-border.table > thead > tr > td,
                .grid-cell-border.table > thead > tr > th {
                    border: 1px solid #000 !important;
                }

            /* Text wrapping */
            .grid-cell-border td,
            .grid-cell-border th {
                word-wrap: break-word;
                overflow-wrap: break-word;
            }

            /* Dropdown inside grid */
            .grid-cell-border select {
                height: 32px !important;
                padding: 3px 6px !important;
                border: 1px solid #bbb !important;
                border-radius: 3px !important;
                background: #fff !important;
            }

            /* View button */
            .grid-cell-border .btn {
                white-space: nowrap;
                margin: 0 !important;
            }
        /* Mobile */
        @media (max-width: 700px) {

            .modalPopup {
                width: 95% !important;
            }

                .modalPopup .section {
                    grid-template-columns: 1fr;
                    row-gap: 12px;
                }

                    .modalPopup .section .field label {
                        width: 110px;
                        min-width: 110px;
                    }

                    .modalPopup .section .field input[type="text"],
                    .modalPopup .section .field input[type="month"] {
                        width: calc(100% - 110px);
                    }

                .modalPopup .submit-btn {
                    margin-left: 110px;
                }
        }

        /* =========================================================
       PRINT
       ========================================================= */

        @media print {

            body * {
                visibility: hidden;
            }


            #salarySlipPrintArea,
            #salarySlipPrintArea * {
                visibility: visible;
            }


            #salarySlipPrintArea {
                position: absolute;
                left: 0;
                top: 0;
                width: 100% !important;
                max-width: none !important;
                margin: 0 !important;
                border: 1px solid #000 !important;
            }


            .modal-header,
            .modal-footer {
                display: none !important;
            }


            .modal,
            .modal-dialog,
            .modal-content,
            .modal-body {
                position: static !important;
                display: block !important;
                width: 100% !important;
                max-width: none !important;
                margin: 0 !important;
                padding: 0 !important;
                border: none !important;
                box-shadow: none !important;
            }


            @page {
                size: A4 landscape;
                margin: 8mm;
            }
        }
    </style>





</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <asp:UpdatePanel ID="upModal" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:LinkButton ID="lnkDummy2" runat="server" Style="display: none;"></asp:LinkButton>
            <!-- Modal Extender -->
            <asp:ModalPopupExtender ID="ModalPopupExtender2" BehaviorID="mpe2" runat="server" PopupControlID="pnlPopup2" TargetControlID="lnkDummy2" BackgroundCssClass="modalBackground" CancelControlID="btnHide2">
            </asp:ModalPopupExtender>
            <!-- Modal Panel -->
            <asp:Panel ID="pnlPopup2"
                runat="server"
                CssClass="modalPopup"
                Style="display: none; width: 700px;">

                <div class="header">
                    <b>
                        <asp:Label ID="lblNotification2"
                            runat="server"
                            Text="PaySlip Request">
                        </asp:Label>
                    </b>

                    <div class="close">
                        <asp:Button ID="btnHide2"
                            runat="server"
                            Text="X"
                            OnClientClick="closePopup2(); return false;" />
                    </div>
                </div>

                <div class="body">

                    <div class="section">

                        <!-- Employee No -->
                        <div class="field">
                            <label>Employee No</label>
                            <asp:TextBox ID="txtEmployeeNo"
                                runat="server"
                                Enabled="false">
                            </asp:TextBox>
                        </div>

                        <!-- Name -->
                        <div class="field">
                            <label>Name</label>
                            <asp:TextBox ID="txtEmployeeName"
                                runat="server"
                                Enabled="false">
                            </asp:TextBox>
                        </div>

                        <!-- Department -->
                        <div class="field">
                            <label>Department</label>
                            <asp:TextBox ID="txtDepartment"
                                runat="server"
                                Enabled="false">
                            </asp:TextBox>

                            <asp:HiddenField ID="txtDepartmentCode"
                                runat="server" />
                        </div>

                        <!-- Designation -->
                        <div class="field">
                            <label>Designation</label>
                            <asp:TextBox ID="txtDesignation"
                                runat="server"
                                Enabled="false">
                            </asp:TextBox>

                            <asp:HiddenField ID="txtDesignationCode"
                                runat="server" />

                            <asp:HiddenField ID="txtHODCode"
                                runat="server" />

                            <asp:HiddenField ID="txtHRCode"
                                runat="server" />
                        </div>

                        <!-- From Month -->
                        <div class="field">
                            <label>From Month</label>

                            <asp:TextBox ID="fromMonth"
                                runat="server"
                                TextMode="Month">
                            </asp:TextBox>

                            <asp:HiddenField ID="hfFromMonth"
                                runat="server" />
                        </div>

                        <!-- To Month -->
                        <div class="field">
                            <label>To Month</label>

                            <asp:TextBox ID="toMonth"
                                runat="server"
                                TextMode="Month">
                            </asp:TextBox>

                            <asp:HiddenField ID="hfToMonth"
                                runat="server" />
                        </div>

                    </div>

                    <asp:Button ID="btnSubmit11"
                        runat="server"
                        Text="Submit"
                        CssClass="submit-btn"
                        OnClick="btnSave_Click"
                        OnClientClick="copyMonthsToHidden();" />

                </div>

            </asp:Panel>


            <asp:LinkButton ID="lnkDummy3" runat="server" Style="display: none;"></asp:LinkButton>
            <!-- Modal Extender -->
            <asp:ModalPopupExtender ID="ModalPopupExtender3" BehaviorID="mpe3" runat="server" PopupControlID="pnlPopup3" TargetControlID="lnkDummy3" BackgroundCssClass="modalBackground" CancelControlID="btnHide3">
            </asp:ModalPopupExtender>
            <!-- Modal Panel -->
            <asp:Panel ID="pnlPopup3" runat="server" CssClass="modalPopup" Style="display: none; width: 1200px;">
                <div class="header">
                    <b>
                        <asp:Label ID="lblNotification3" runat="server" Text="PaySlip Request List"></asp:Label></b>
                    <div class="close">
                        <asp:Button ID="btnHide3" runat="server" Text="X" Style="padding: 0px;" OnClientClick="closePopup3(); return false;" />
                    </div>
                </div>

                <div class="body">
                </div>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Employee PaySlip Detail" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <table>
        <tr style="height: 20px">
            <td></td>
        </tr>
        <tr>
            <td style="width: 20px"></td>
            <td>&nbsp;&nbsp;&nbsp;
                <button type="button" onclick="showPopup2();" style="color: white; background-color: green; height: 30px; width: 120px;">PaySlip Request</button></td>
        </tr>
    </table>

    <br />
    <asp:GridView ID="getPaySlipRequestList" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="Black"
        BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="100%" Font-Size="12px" CssClass="grid-cell-border"
        GridLines="Both" EmptyDataText="There are no data records to display." OnRowDataBound="getPaySlipRequestList_RowDataBound"
        AllowSorting="true">
        <AlternatingRowStyle BackColor="#F7F7F7" />
        <Columns>
            <asp:TemplateField HeaderText="Sl. No.">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
                <ItemStyle Width="7%" />
            </asp:TemplateField>
        </Columns>
        <Columns>
            <asp:TemplateField HeaderText="Employee Code">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("EmployeeNo") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="7%" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Employee Name">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="7%" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Department Name">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("DepartmentName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="10%" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Designation Name">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("DesignationName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="7%" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="From Month">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("FromMonth", "{0:MMMM yyyy}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="7%" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="To Month">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("ToMonth", "{0:MMMM yyyy}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="7%" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="HOD Code">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("HODCode") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="7%" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="HOD Status">
                <ItemTemplate>
                    <asp:Label ID="LabelHOD" runat="server" Text='<%# Eval("HODStatusText") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="7%" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="HOD Remark">
                <ItemTemplate>
                    <asp:Label ID="LabelHODRemark" runat="server" Text='<%# Eval("HODRemark") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="7%" />
            </asp:TemplateField>


            <asp:TemplateField HeaderText="HR Code">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("HRCode") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="7%" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="HR Status">
                <ItemTemplate>
                    <asp:Label ID="HRStatus" runat="server" Text='<%# Eval("HrStatusText") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="7%" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="HR Remark">
                <ItemTemplate>
                    <asp:Label ID="HRRemark" runat="server" Text='<%# Eval("HRRemark") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="7%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Select Month">
                <ItemTemplate>
                    <asp:DropDownList
                        ID="ddlMonth"
                        runat="server"
                        CssClass="form-control"
                        Width="130px">
                    </asp:DropDownList>
                </ItemTemplate>
                <ItemStyle Width="12%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="View">
                <ItemTemplate>
                    <a href="javascript:void(0);"
                        class="btn btn-sm btn-primary"
                        style='<%# Eval("HrStatusText").ToString() == "Accept" ? "": "display:none;" %>'
                        onclick="ViewSalarySlip('<%# Eval("EmployeeNo") %>', this);">
                        <i class="fa fa-eye"></i>View
                    </a>
                </ItemTemplate>
                <ItemStyle Width="8%" />
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" />
        <AlternatingRowStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" />
        <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" />
        <RowStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" />
        <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
        <SortedAscendingCellStyle BackColor="#F4F4FD" />
        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
        <SortedDescendingCellStyle BackColor="#D8D8F0" />
        <SortedDescendingHeaderStyle BackColor="#3E3277" />

    </asp:GridView>

    <div class="modal fade"
        id="salarySlipModal"
        tabindex="-1"
        aria-hidden="true">

        <div class="modal-dialog modal-dialog-centered salary-slip-modal-dialog">

            <div class="modal-content salary-slip-modal-content">


                <!-- =====================================================
                 MODAL HEADER
                 ===================================================== -->

                <div class="modal-header bg-primary text-white">

                    <h5 class="modal-title">

                        <i class="fa fa-file-invoice-dollar"></i>
                        Salary Slip

                    </h5>

                    <button type="button"
                        class="btn-close btn-close-white"
                        data-bs-dismiss="modal">
                    </button>

                </div>


                <!-- =====================================================
                 MODAL BODY
                 ===================================================== -->

                <div class="modal-body">

                    <div id="salarySlipPrintArea"
                        class="salary-slip">


                        <!-- =================================================
                         COMPANY HEADER
                         ================================================= -->

                        <div class="company-header">

                            <img src="../images/tmulogo.png"
                                class="company-logo"
                                alt="TMU Logo" />


                            <div class="company-name">
                                TMIMIT (SOCIETY)
                            </div>


                            <div class="company-address">
                                TMU, Delhi Road Moradabad -244001 (UP)
                            </div>


                            <div class="pay-slip-title">
                                Pay Slip for the month of

                            <span id="lblSalaryMonth"></span>

                            </div>

                        </div>


                        <!-- =================================================
                         EMPLOYEE DETAILS HEADING
                         ================================================= -->

                        <div class="section-title">
                            Employee Details:

                        </div>


                        <!-- =================================================
                         EMPLOYEE DETAILS
                         ================================================= -->

                        <div class="employee-details">


                            <!-- ================= LEFT ================= -->

                            <div class="employee-left">

                                <table class="employee-table">

                                    <tr>

                                        <td class="employee-label">Employee Name
                                        </td>

                                        <td class="employee-value"
                                            id="lblEmployeeName"></td>

                                    </tr>


                                    <tr>

                                        <td class="employee-label">Employee ID
                                        </td>

                                        <td class="employee-value"
                                            id="lblEmployeeId"></td>

                                    </tr>


                                    <tr>

                                        <td class="employee-label">Father's Name
                                        </td>

                                        <td class="employee-value"
                                            id="lblFatherName"></td>

                                    </tr>


                                    <tr>

                                        <td class="employee-label">DOJ
                                        </td>

                                        <td class="employee-value"
                                            id="lblDOJ"></td>

                                    </tr>


                                    <tr>

                                        <td class="employee-label">PAN
                                        </td>

                                        <td class="employee-value"
                                            id="lblPAN"></td>

                                    </tr>

                                </table>

                            </div>


                            <!-- ================= RIGHT ================= -->

                            <div class="employee-right">

                                <table class="employee-table">

                                    <tr>

                                        <td class="employee-label">Unit
                                        </td>

                                        <td class="employee-value"
                                            id="lblUnit"></td>

                                    </tr>


                                    <tr>

                                        <td class="employee-label">Designation
                                        </td>

                                        <td class="employee-value"
                                            id="lblDesignation"></td>

                                    </tr>


                                    <tr>

                                        <td class="employee-label">Department
                                        </td>

                                        <td class="employee-value"
                                            id="lblDepartment"></td>

                                    </tr>


                                    <tr>

                                        <td class="employee-label">ESI No.
                                        </td>

                                        <td class="employee-value"
                                            id="lblESI"></td>

                                    </tr>


                                    <tr>

                                        <td class="employee-label">UAN
                                        </td>

                                        <td class="employee-value"
                                            id="lblUAN"></td>

                                    </tr>

                                </table>

                            </div>

                        </div>


                        <!-- =================================================
                         EARNINGS + GAP + DEDUCTIONS
                         ================================================= -->

                        <div class="salary-row">


                            <!-- =================================================
                             EARNINGS
                             ================================================= -->

                            <div class="earnings-column">


                                <div class="salary-heading">
                                    Earnings (₹)

                                </div>


                                <table class="salary-table earnings-table">


                                    <thead>

                                        <tr>

                                            <th>Particular
                                            </th>

                                            <th>Pay Rate
                                            </th>

                                            <th>Paid<br />
                                                Days
                                            </th>

                                            <th>Pay Earned
                                            </th>

                                        </tr>

                                    </thead>


                                    <tbody id="earningBody">
                                    </tbody>


                                    <!--
                                    EXTRA BLANK ROW

                                    Earnings records:
                                    1 Basic
                                    2 AGP
                                    3 DA
                                    4 HRA
                                    5 Special Allowance
                                    6 Arrears
                                    7 Other Income

                                    Total = 7 rows
                                -->


                                    <tfoot>

                                        <tr class="total-row">

                                            <th colspan="3">Gross Earning

                                            </th>

                                            <th id="lblGrossEarning">0.00

                                            </th>

                                        </tr>

                                    </tfoot>

                                </table>

                            </div>


                            <!-- =================================================
                             GAP
                             ================================================= -->

                            <div class="salary-column-gap">
                            </div>


                            <!-- =================================================
                             DEDUCTIONS
                             ================================================= -->

                            <div class="deduction-column">


                                <div class="salary-heading">
                                    Deductions (₹)

                                </div>


                                <table class="salary-table deduction-table">


                                    <thead>

                                        <tr>

                                            <th>Particular
                                            </th>

                                            <th>Amount
                                            </th>

                                        </tr>

                                    </thead>


                                    <tbody id="deductionBody">
                                    </tbody>


                                    <!-- =================================================
                                     EXTRA BLANK ROW

                                     Deduction records:
                                     1 TDS
                                     2 PF
                                     3 ESI
                                     4 Other Deduction
                                     5 Electricity
                                     6 Rent/Maint. Deduction

                                     Earnings = 7 rows
                                     Deduction = 6 rows

                                     So 1 blank row is required here
                                     ================================================= -->

                                    <tbody>

                                        <tr class="blank-salary-row">

                                            <td>&nbsp;
                                            </td>

                                            <td>&nbsp;
                                            </td>

                                        </tr>

                                    </tbody>


                                    <tfoot>

                                        <tr class="total-row">

                                            <th>Gross Deduction
                                            </th>

                                            <th id="lblGrossDeduction">0.00

                                            </th>

                                        </tr>

                                    </tfoot>

                                </table>

                            </div>

                        </div>


                        <!-- =================================================
                         NET SALARY
                         ================================================= -->

                        <div class="net-salary-box">

                            <strong>Net Salary:
                            </strong>

                            ₹

                        <span id="lblNetSalary"
                            class="salary-amount"></span>

                            &nbsp;-&nbsp;

                        <strong>(In Words:
                        </strong>

                            <span id="lblSalaryInWords"
                                class="amount-words"></span>

                            <strong>)
                            </strong>

                        </div>


                        <!-- =================================================
                         ATTENDANCE
                         ================================================= -->

                        <div class="attendance-box">

                            <strong>Attendance details :
                            </strong>

                            <span id="lblAttendanceDetails"></span>

                        </div>


                        <!-- =================================================
                         FOOTER
                         ================================================= -->

                        <div class="salary-footer">
                            **This is a computer generated document, No signature required.

                        </div>


                    </div>

                </div>


                <!-- =====================================================
                 MODAL FOOTER
                 ===================================================== -->

                <div class="modal-footer">

                    <button type="button"
                        class="btn btn-secondary"
              data-bs-dismiss="modal" >
                        Close
                    </button>


                    <button type="button"
                        class="btn btn-primary"
                        onclick="downloadSalarySlipPDF();">

                        <i class="fa fa-print"></i>

                       Download

                    </button>

                </div>

            </div>

        </div>

    </div>


</asp:Content>

