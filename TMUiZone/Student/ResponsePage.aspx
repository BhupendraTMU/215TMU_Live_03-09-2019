<%@ Page Title="Fee Payment Receipt"
    Language="C#"
    MasterPageFile="~/Student/IndexMaster.master"
    AutoEventWireup="true"
    CodeFile="ResponsePage.aspx.cs"
    Inherits="Student_ResponsePage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="Server">

    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <style type="text/css">
        /* ==========================================================
           MODERN TMU FEE RECEIPT
           All classes use "tmu-" prefix to avoid MasterPage/
           Bootstrap CSS conflicts.
           ========================================================== */

        .tmu-page,
        .tmu-page * {
            box-sizing: border-box;
        }

        .tmu-page {
            width: 100%;
            min-height: 100vh;
            padding: 28px 18px 50px;
            background: #f3f6fa;
            font-family: "Segoe UI", Arial, Helvetica, sans-serif;
            color: #172b4d;
        }

        .tmu-receipt {
            position: relative;
            width: 100%;
            max-width: 1080px;
            margin: 0 auto;
            background: #ffffff;
            border: 1px solid #dce5ef;
            border-radius: 14px;
            box-shadow: 0 8px 30px rgba(28, 55, 90, .10);
            overflow: hidden;
        }

        /* ================= HEADER ================= */

        .tmu-header {
            position: relative;
            display: grid;
            grid-template-columns: 170px 1fr 250px;
            align-items: center;
            min-height: 145px;
            padding: 20px 30px;
            background: #ffffff;
        }

        .tmu-logo-area {
            text-align: center;
            padding-right: 22px;
            border-right: 1px solid #dce4ec;
        }

        .tmu-logo {
            display: inline-block !important;
            width: auto !important;
            height: auto !important;
            max-width: 145px !important;
            max-height: 105px !important;
            margin: 0 auto !important;
            object-fit: contain;
        }

        .tmu-university-area {
            padding: 0 30px;
        }

        .tmu-university-name {
            margin: 0;
            color: #24558f !important;
            font-size: 25px !important;
            line-height: 30px !important;
            font-weight: 800 !important;
            letter-spacing: .6px;
            text-transform: uppercase;
        }

        .tmu-location {
            margin-top: 7px;
            color: #64748b;
            font-size: 14px;
            line-height: 20px;
            text-align: center;
        }

        .tmu-receipt-head {
            padding-left: 22px;
            border-left: 1px solid #dce4ec;
            text-align: right;
        }

        .tmu-receipt-icon {
            display: inline-block;
            margin-right: 7px;
            color: #24558f;
            font-size: 25px;
            vertical-align: middle;
        }

        .tmu-receipt-title {
            display: inline-block;
            color: #24558f !important;
            font-size: 27px !important;
            line-height: 32px !important;
            font-weight: 800 !important;
            letter-spacing: .8px;
            vertical-align: middle;
        }

        .tmu-receipt-number {
            margin-top: 14px;
            color: #334155;
            font-size: 13px;
            font-weight: 600;
        }

        .tmu-receipt-number-value {
            display: inline-block;
            margin-left: 5px;
            padding: 6px 12px;
            border-radius: 18px;
            background: #e8f3ff;
            color: #24558f;
            font-size: 14px;
            font-weight: 800;
        }

        .tmu-color-line {
            height: 7px;
            background: linear-gradient( to right, #1769aa 0%, #1769aa 58%, #13a66a 58%, #13a66a 100% );
        }

        /* ================= STATUS ================= */

        .tmu-status {
            display: flex;
            align-items: center;
            gap: 18px;
            margin: 25px 32px 22px;
            padding: 17px 24px;
            border: 1px solid #a7e4c2;
            border-radius: 12px;
            background: #effcf5;
        }

        .tmu-status-icon {
            flex: 0 0 54px;
            width: 54px;
            height: 54px;
            border-radius: 50%;
            background: #12965f;
            color: #ffffff;
            text-align: center;
            font-size: 31px;
            line-height: 54px;
            font-weight: 800;
        }

        .tmu-status-content {
            flex: 1;
        }

        .tmu-status-title {
            display: block;
            margin: 0;
            color: #10804f !important;
            font-size: 20px !important;
            line-height: 25px !important;
            font-weight: 800 !important;
        }

        .tmu-status-text {
            margin-top: 3px;
            color: #49677e;
            font-size: 13px;
        }

        .tmu-failure {
            border-color: #f0b8b8;
            background: #fff5f5;
        }

            .tmu-failure .tmu-status-icon {
                background: #d83a3a;
            }

            .tmu-failure .tmu-status-title {
                color: #c62828 !important;
            }

        /* ================= CARDS ================= */

        .tmu-section {
            margin: 0 32px 22px;
            border: 1px solid #dbe5ef;
            border-radius: 12px;
            overflow: hidden;
            background: #ffffff;
            box-shadow: 0 3px 12px rgba(35, 67, 100, .06);
        }

        .tmu-section-title {
            display: flex;
            align-items: center;
            gap: 12px;
            min-height: 55px;
            padding: 12px 20px;
            background: linear-gradient(135deg, #245b95, #2f6eac);
            color: #ffffff !important;
            font-size: 17px !important;
            line-height: 22px !important;
            font-weight: 800 !important;
            text-transform: uppercase;
            letter-spacing: .5px;
        }

        .tmu-section-icon {
            display: inline-flex;
            width: 31px;
            height: 31px;
            align-items: center;
            justify-content: center;
            border-radius: 8px;
            background: rgba(255,255,255,.17);
            color: #ffffff;
            font-size: 17px;
            font-weight: 700;
        }

        /* ================= TABLE ================= */

        .tmu-table {
            width: 100% !important;
            margin: 0 !important;
            border-collapse: collapse !important;
            border-spacing: 0 !important;
            table-layout: fixed !important;
            background: #ffffff;
        }

            .tmu-table tr {
                display: table-row !important;
            }

            .tmu-table td {
                display: table-cell !important;
                position: static !important;
                float: none !important;
                vertical-align: middle !important;
                padding: 14px 16px !important;
                border: 1px solid #e0e7ef !important;
                color: #24364b !important;
                font-size: 13px !important;
                line-height: 19px !important;
            }

            .tmu-table .tmu-label {
                width: 19% !important;
                background: #f5f8fb !important;
                color: #31577d !important;
                font-weight: 700 !important;
            }

            .tmu-table .tmu-value {
                width: 31% !important;
                background: #ffffff !important;
                color: #1e293b !important;
                word-break: break-word !important;
                overflow-wrap: anywhere !important;
            }

                .tmu-table .tmu-value span,
                .tmu-table .tmu-value label {
                    display: inline !important;
                    position: static !important;
                    float: none !important;
                    margin: 0 !important;
                    padding: 0 !important;
                    border: 0 !important;
                    background: transparent !important;
                }

        .tmu-amount {
            color: #078653 !important;
            font-size: 20px !important;
            font-weight: 800 !important;
            white-space: nowrap;
        }

        .tmu-rupee {
            margin-right: 3px;
        }

        .tmu-order {
            color: #1769aa !important;
            font-weight: 700 !important;
            word-break: break-all !important;
        }

        .tmu-success-badge {
            display: inline-block !important;
            padding: 6px 12px !important;
            border-radius: 7px !important;
            background: #d9f6e7 !important;
            color: #078653 !important;
            font-weight: 800 !important;
        }

        /* ================= MESSAGE ================= */

        .tmu-message {
            display: flex !important;
            align-items: center;
            gap: 14px;
            margin: 28px 32px 22px !important;
            padding: 15px 20px !important;
            border: 1px solid #b9ddff !important;
            border-radius: 10px !important;
            background: #eff8ff !important;
            color: #1769aa !important;
            font-size: 14px !important;
            line-height: 20px !important;
            font-weight: 700 !important;
        }

        .tmu-message-icon {
            display: inline-flex;
            flex: 0 0 30px;
            width: 30px;
            height: 30px;
            align-items: center;
            justify-content: center;
            border-radius: 50%;
            background: #1769aa;
            color: #ffffff;
            font-size: 17px;
            font-weight: 800;
        }

        /* ================= FOOTER ================= */

        .tmu-footer {
            margin: 0 32px;
            padding: 20px 15px 10px;
            border-top: 1px solid #dce5ee;
            text-align: center;
            color: #60758a;
            font-size: 11px;
            line-height: 19px;
        }

        /* ================= BUTTONS ================= */

        .tmu-actions {
            display: flex;
            justify-content: center;
            align-items: center;
            gap: 16px;
            padding: 14px 30px 32px;
        }

        .tmu-button {
            display: inline-flex !important;
            align-items: center;
            justify-content: center;
            min-width: 160px !important;
            height: 48px !important;
            padding: 0 25px !important;
            border: 0 !important;
            border-radius: 8px !important;
            font-family: "Segoe UI", Arial, sans-serif !important;
            font-size: 14px !important;
            font-weight: 700 !important;
            line-height: 48px !important;
            cursor: pointer !important;
            text-decoration: none !important;
            transition: .2s ease;
        }

            .tmu-button:hover {
                transform: translateY(-1px);
                box-shadow: 0 5px 12px rgba(0,0,0,.12);
            }

        .tmu-ok {
            background: #ffffff !important;
            border: 2px solid #1769aa !important;
            color: #1769aa !important;
        }

        .tmu-print {
            background: #078653 !important;
            color: #ffffff !important;
        }

        /* ================= BOTTOM WAVE ================= */

        .tmu-bottom {
            position: relative;
            height: 58px;
            margin-top: 2px;
            overflow: hidden;
            background: #1f548e;
        }

            .tmu-bottom:before {
                content: "";
                position: absolute;
                left: -5%;
                top: -25px;
                width: 110%;
                height: 65px;
                background: #1769aa;
                border-radius: 50% 50% 0 0;
                transform: rotate(-2deg);
            }

            .tmu-bottom:after {
                content: "";
                position: absolute;
                left: 45%;
                top: -16px;
                width: 65%;
                height: 55px;
                background: #12a56a;
                border-radius: 50% 50% 0 0;
                transform: rotate(-2deg);
                opacity: .95;
            }

        /* ================= MOBILE ================= */

        @media screen and (max-width: 800px) {

            .tmu-page {
                padding: 10px 5px 30px;
            }

            .tmu-header {
                grid-template-columns: 1fr;
                gap: 15px;
                padding: 18px 12px;
                text-align: center;
            }

            .tmu-logo-area {
                padding: 0 0 10px;
                border-right: 0;
                border-bottom: 1px solid #dce4ec;
            }

            .tmu-university-area {
                padding: 0;
            }

            .tmu-university-name {
                font-size: 20px !important;
                line-height: 25px !important;
            }

            .tmu-receipt-head {
                padding: 0;
                border-left: 0;
                text-align: center;
            }

            .tmu-receipt-title {
                font-size: 23px !important;
            }

            .tmu-receipt-number {
                text-align: center;
            }

            .tmu-status {
                margin: 15px 12px;
                padding: 13px;
            }

            .tmu-section {
                margin: 15px 12px;
            }

            .tmu-table {
                table-layout: auto !important;
            }

                .tmu-table td {
                    padding: 10px 9px !important;
                    font-size: 12px !important;
                }

                .tmu-table .tmu-label {
                    width: 24% !important;
                }

                .tmu-table .tmu-value {
                    width: 26% !important;
                }

            .tmu-message {
                margin: 18px 12px !important;
            }

            .tmu-footer {
                margin: 0 12px;
            }

            .tmu-actions {
                gap: 8px;
                padding: 12px 10px 25px;
            }

            .tmu-button {
                min-width: 130px !important;
                padding: 0 12px !important;
            }
        }

        /* ================= PRINT ================= */

        /* ==========================================================
   PRINT - SAME AS VIEW
   ========================================================== */

        @media print {

            @page {
                size: A4 portrait;
                margin: 8mm;
            }

            html,
            body {
                margin: 0 !important;
                padding: 0 !important;
                background: #ffffff !important;
            }

                /* Hide everything outside receipt */
                body * {
                    visibility: hidden !important;
                }

            /* Show only receipt */
            #Print,
            #Print * {
                visibility: visible !important;
            }

            /* Receipt remains exactly like screen */
            #Print {
                position: absolute !important;
                left: 0 !important;
                top: 0 !important;
                width: 100% !important;
                max-width: 1080px !important;
                margin: 0 auto !important;
                background: #ffffff !important;
                border: 1px solid #dce5ef !important;
                border-radius: 14px !important;
                box-shadow: none !important;
                overflow: hidden !important;
            }

            .tmu-page {
                width: 100% !important;
                min-height: auto !important;
                margin: 0 !important;
                padding: 0 !important;
                background: #ffffff !important;
            }

            .tmu-receipt {
                width: 100% !important;
                max-width: 1080px !important;
                margin: 0 auto !important;
                background: #ffffff !important;
                border: 1px solid #dce5ef !important;
                border-radius: 14px !important;
                box-shadow: none !important;
            }


            /* ======================================================
       HEADER - SAME AS VIEW
       ====================================================== */

            .tmu-header {
                display: grid !important;
                grid-template-columns: 170px 1fr 250px !important;
                align-items: center !important;
                min-height: 145px !important;
                padding: 20px 30px !important;
                background: #ffffff !important;
            }

            .tmu-logo-area {
                padding-right: 22px !important;
                border-right: 1px solid #dce4ec !important;
                text-align: center !important;
            }

            .tmu-logo {
                display: inline-block !important;
                width: auto !important;
                height: auto !important;
                max-width: 145px !important;
                max-height: 105px !important;
                margin: 0 auto !important;
            }

            .tmu-university-area {
                padding: 0 30px !important;
            }

            .tmu-university-name {
                font-size: 25px !important;
                line-height: 30px !important;
                font-weight: 800 !important;
            }

            .tmu-location {
                margin-top: 7px !important;
                font-size: 14px !important;
                line-height: 20px !important;
            }

            .tmu-receipt-head {
                padding-left: 22px !important;
                border-left: 1px solid #dce4ec !important;
                text-align: right !important;
            }

            .tmu-receipt-title {
                font-size: 27px !important;
                line-height: 32px !important;
            }

            .tmu-receipt-icon {
                font-size: 25px !important;
            }

            .tmu-receipt-number {
                margin-top: 14px !important;
                font-size: 13px !important;
            }

            .tmu-receipt-number-value {
                padding: 6px 12px !important;
                font-size: 14px !important;
            }

            .tmu-color-line {
                height: 7px !important;
            }


            /* ======================================================
       SUCCESS / FAILURE - SAME AS VIEW
       ====================================================== */

            .tmu-status {
                display: flex !important;
                align-items: center !important;
                gap: 18px !important;
                margin: 25px 32px 22px !important;
                padding: 17px 24px !important;
            }

            .tmu-status-icon {
                flex: 0 0 54px !important;
                width: 54px !important;
                height: 54px !important;
                line-height: 54px !important;
                font-size: 31px !important;
            }

            .tmu-status-title {
                font-size: 20px !important;
                line-height: 25px !important;
            }

            .tmu-status-text {
                font-size: 13px !important;
            }


            /* ======================================================
       SECTION - SAME AS VIEW
       ====================================================== */

            .tmu-section {
                margin: 0 32px 22px !important;
                border: 1px solid #dbe5ef !important;
                border-radius: 12px !important;
                overflow: hidden !important;
                box-shadow: none !important;
            }

            .tmu-section-title {
                display: flex !important;
                align-items: center !important;
                gap: 12px !important;
                min-height: 55px !important;
                padding: 12px 20px !important;
                font-size: 17px !important;
                line-height: 22px !important;
                font-weight: 800 !important;
            }

            .tmu-section-icon {
                width: 31px !important;
                height: 31px !important;
                font-size: 17px !important;
            }


            /* ======================================================
       TABLE - SAME AS VIEW
       ====================================================== */

            .tmu-table {
                width: 100% !important;
                table-layout: fixed !important;
                border-collapse: collapse !important;
            }

                .tmu-table tr {
                    display: table-row !important;
                }

                .tmu-table td {
                    display: table-cell !important;
                    position: static !important;
                    float: none !important;
                    padding: 14px 16px !important;
                    font-size: 13px !important;
                    line-height: 19px !important;
                    border: 1px solid #e0e7ef !important;
                }

                .tmu-table .tmu-label {
                    width: 19% !important;
                }

                .tmu-table .tmu-value {
                    width: 31% !important;
                }

            .tmu-amount {
                font-size: 20px !important;
            }

            .tmu-success-badge {
                padding: 6px 12px !important;
            }


            /* ======================================================
       MESSAGE - SAME AS VIEW
       ====================================================== */

            .tmu-message {
                display: flex !important;
                align-items: center !important;
                gap: 14px !important;
                margin: 28px 32px 22px !important;
                padding: 15px 20px !important;
                font-size: 14px !important;
                line-height: 20px !important;
            }

            .tmu-message-icon {
                flex: 0 0 30px !important;
                width: 30px !important;
                height: 30px !important;
                font-size: 17px !important;
            }


            /* ======================================================
       FOOTER - SAME AS VIEW
       ====================================================== */

            .tmu-footer {
                margin: 0 32px !important;
                padding: 20px 15px 10px !important;
                font-size: 11px !important;
                line-height: 19px !important;
            }


            /* ======================================================
       IMPORTANT:
       BUTTONS ONLY HIDE
       ====================================================== */

            .tmu-actions,
            .no-print {
                display: none !important;
            }


            /* ======================================================
       BOTTOM DESIGN - SAME AS VIEW
       ====================================================== */

            .tmu-bottom {
                height: 58px !important;
            }


            /* ======================================================
       KEEP COLORS IN PRINT
       ====================================================== */

            .tmu-receipt,
            .tmu-section-title,
            .tmu-status,
            .tmu-success-badge,
            .tmu-message,
            .tmu-bottom,
            .tmu-color-line {
                -webkit-print-color-adjust: exact !important;
                print-color-adjust: exact !important;
            }
        }
    </style>

    <script type="text/javascript">

        function printDiv() {
            window.print();
            return false;
        }

    </script>

</asp:Content>


<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="Server">

    <div class="tmu-page">

        <div class="tmu-receipt" id="Print">

            <!-- ================= HEADER ================= -->

            <div class="tmu-header">

                <div class="tmu-logo-area">

                    <img src="../images/tmulogo.png"
                        class="tmu-logo"
                        alt="TMU Logo"
                        onerror="this.style.display='none';" />

                </div>

                <div class="tmu-university-area">

                    <div class="tmu-university-name">
                        Teerthanker Mahaveer University
                   
                    </div>

                    <div class="tmu-location">
                        Moradabad | Uttar Pradesh | India
                   
                    </div>

                </div>

                <div class="tmu-receipt-head">

                    <%--<div>
                        <span class="tmu-receipt-icon">▣</span>
                        <span class="tmu-receipt-title">FEE RECEIPT</span>
                    </div>

                    <div class="tmu-receipt-number">

                        Receipt No.:

                        <span class="tmu-receipt-number-value">
                            <asp:Label ID="srno"
                                runat="server"
                                Text="">
                            </asp:Label>
                        </span>

                    </div>--%>
                </div>

            </div>

            <div class="tmu-color-line"></div>


            <!-- ================= PAYMENT SUCCESS ================= -->

            <asp:Panel ID="pnlSuccess"
                runat="server"
                Visible="False"
                CssClass="tmu-status">

                <div class="tmu-status-icon">
                    ✓
               
                </div>

                <div class="tmu-status-content">

                    <asp:Label ID="lblStatustext"
                        runat="server"
                        CssClass="tmu-status-title"
                        Text="Payment Successful">
                    </asp:Label>

                    <div class="tmu-status-text">
                        Your payment has been successfully received.
                   
                    </div>

                </div>

            </asp:Panel>


            <!-- ================= PAYMENT FAILURE ================= -->

            <asp:Panel ID="pnlFalure"
                runat="server"
                Visible="False"
                CssClass="tmu-status tmu-failure">

                <div class="tmu-status-icon">
                    ×
               
                </div>

                <div class="tmu-status-content">

                    <asp:Label ID="lblstatusfauluere"
                        runat="server"
                        CssClass="tmu-status-title"
                        Text="Payment Failed">
                    </asp:Label>

                </div>

            </asp:Panel>


            <!-- ================= STUDENT DETAILS ================= -->

            <div class="tmu-section">

                <div class="tmu-section-title">

                    <span class="tmu-section-icon">●</span>

                    Student Details

               
                </div>

                <table class="tmu-table">

                    <tr>

                        <td class="tmu-label">College
                        </td>

                        <td class="tmu-value">
                            <asp:Label ID="lblCollegeCode"
                                runat="server"
                                Text="">
                            </asp:Label>
                        </td>

                        <td class="tmu-label">Student No.
                        </td>

                        <td class="tmu-value">
                            <asp:Label ID="lblStudentNo"
                                runat="server"
                                Text="">
                            </asp:Label>
                        </td>

                    </tr>

                    <tr>

                        <td class="tmu-label">Student Name
                        </td>

                        <td class="tmu-value">
                            <asp:Label ID="lblStudentName"
                                runat="server"
                                Text="">
                            </asp:Label>
                        </td>

                        <td class="tmu-label">Enrollment No.
                        </td>

                        <td class="tmu-value">
                            <asp:Label ID="lblAgentId"
                                runat="server"
                                Text="">
                            </asp:Label>
                        </td>

                    </tr>

                    <tr>

                        <td class="tmu-label">Father's Name
                        </td>

                        <td class="tmu-value">
                            <asp:Label ID="lblFatherName"
                                runat="server"
                                Text="">
                            </asp:Label>
                        </td>

                        <td class="tmu-label">Date of Birth
                        </td>

                        <td class="tmu-value">
                            <asp:Label ID="lblDob"
                                runat="server"
                                Text="">
                            </asp:Label>
                        </td>

                    </tr>
                    <tr>

                        <td class="tmu-label">Admitted Year
     </td>

                        <td class="tmu-value">
                            <asp:Label ID="lblAdmitted"
                                runat="server"
                                Text="">
         </asp:Label>
                        </td>

                        <td class="tmu-label">Contact No.
     </td>

                        <td class="tmu-value">
                            <asp:Label ID="lblContact"
                                runat="server"
                                Text="">
         </asp:Label>
                        </td>

                    </tr>
                </table>

            </div>


            <!-- ================= PAYMENT DETAILS ================= -->

            <div class="tmu-section">

                <div class="tmu-section-title">

                    <span class="tmu-section-icon">▣</span>

                    Payment Details

               
                </div>

                <table class="tmu-table">

                    <tr>

                        <td class="tmu-label">Payment Status
                        </td>

                        <td class="tmu-value">

                            <span class="tmu-success-badge">

                                <asp:Label ID="lblStatus"
                                    runat="server"
                                    Text="">
                                </asp:Label>

                            </span>

                        </td>

                        <td class="tmu-label">Amount
                        </td>

                        <td class="tmu-value tmu-amount">

                            <span class="tmu-rupee">₹</span>

                            <asp:Label ID="lblAmount"
                                runat="server"
                                Text="">
                            </asp:Label>

                        </td>

                    </tr>

                    <tr>

                        <td class="tmu-label">Order ID
                        </td>

                        <td class="tmu-value tmu-order">

                            <asp:Label ID="lblOrderid"
                                runat="server"
                                Text="">
                            </asp:Label>

                        </td>

                        <td class="tmu-label">Payment Mode
                        </td>

                        <td class="tmu-value">

                            <asp:Label ID="lblPaymentMode"
                                runat="server"
                                Text="">
                            </asp:Label>

                        </td>

                    </tr>

                    <tr>

                        <td class="tmu-label">Payment Date
                        </td>

                        <td class="tmu-value">

                            <asp:Label ID="lblPaymentDate"
                                runat="server"
                                Text="">
                            </asp:Label>

                        </td>

                        <td class="tmu-label">UTR No.
                        </td>

                        <td class="tmu-value">

                            <asp:Label ID="lblUTRNo"
                                runat="server"
                                Text="">
                            </asp:Label>

                        </td>

                    </tr>

                </table>

            </div>


            <!-- ================= INFORMATION MESSAGE ================= -->

            <div class="tmu-message">

                <span class="tmu-message-icon">i
                </span>

                <asp:Label ID="lblValidate"
                    runat="server"
                    Text="Your payment has been successfully received.">
                </asp:Label>

            </div>


            <!-- ================= FOOTER ================= -->

            <div class="tmu-footer">
                This is a computer generated fee Confirmation.
                No signature is required.

               

                <br />

                Please retain this receipt for future reference.

           
            </div>


            <!-- ================= ACTIONS ================= -->

            <div class="tmu-actions no-print">

                <asp:Button ID="btnOk"
                    runat="server"
                    Text="✓  OK"
                    CssClass="tmu-button tmu-ok"
                    OnClick="btnOk_Click" />

                <button type="button"
                    class="tmu-button tmu-print"
                    onclick="return printDiv();">
                    ▣ &nbsp; Print
               
                </button>

            </div>


            <!-- ================= BOTTOM DESIGN ================= -->

            <div class="tmu-bottom"></div>

        </div>

    </div>


    <!-- Existing hidden control -->

    <asp:Label ID="lblStatusCode"
        runat="server"
        Visible="False">
    </asp:Label>

</asp:Content>
