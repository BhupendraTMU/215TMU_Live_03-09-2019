<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="EditStudentAttendance.aspx.cs" Inherits="Faculty_EditStudentAttendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Content/ReviewAttendance_OrangeTheme.css" rel="stylesheet" type="text/css" />



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
            if (sender._selectedDate > new Date()) {
                alert("You cannot select greater than current date!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
    </script>
    <script type="text/javascript">

        function onUpdating() {
            // get the divImage

            var panelProg = $get('divImage');
            // set it to visible
            panelProg.style.display = '';

            // hide label if visible     
            var lbl = $get('<%= this.lblText.ClientID %>');
            lbl.innerHTML = '';

        }
    </script>
    <style type="text/css">
        <style type="text/css" >
        /* ==========================================================
TMU UNIVERSITY ERP - REVIEW ATTENDANCE
Orange + pale lime theme matched to the supplied screenshot.
Put this file in: ~/Content/ReviewAttendance_OrangeTheme.css
========================================================== */
        .boxBody, .boxBodyHeader, .boxBodyInner,
        .attendance-page-heading, .attendance-page-content {
            box-sizing: border-box;
            font-family: "Segoe UI", Arial, sans-serif;
        }

        .attendance-page-heading {
            display: flex;
            flex-wrap: wrap;
            align-items: center;
            gap: 12px 14px;
            margin: 0;
            padding: 18px 22px;
            border: 0;
            border-radius: 10px 10px 0 0;
            background: #ffffff;
            color: #25344b;
            box-shadow: 0 2px 9px rgba(43, 54, 29, .08);
        }

            .attendance-page-heading #Label3 {
                display: inline-block;
                margin: 0 20px 0 0;
                color: #173b70 !important;
                font-family: "Segoe UI", Arial, sans-serif !important;
                font-size: 23px !important;
                font-weight: 700;
                letter-spacing: .2px;
            }

            .attendance-page-heading > span,
            .attendance-page-heading label {
                color: #35445a;
                font-size: 13px;
            }

            .attendance-page-heading select {
                min-width: 120px;
                min-height: 35px;
                padding: 6px 10px;
                border: 1px solid #d6deea;
                border-radius: 6px;
                background: #fff !important;
                color: #26364d;
                font: 13px "Segoe UI", Arial, sans-serif;
            }

        .boxBodyHeader {
            height: 5px;
            min-height: 5px;
            margin: 0;
            padding: 0;
            border: 0;
            background: #f58200;
        }

        .attendance-page-content {
            margin: 0;
            padding: 20px;
            border: 1px solid #e5e9d1;
            border-top: 0;
            border-radius: 0 0 10px 10px;
            background: #f0f6b0;
            color: #26364d;
        }

            .attendance-page-content > center {
                display: block;
                width: 100%;
            }

            /* Detained / elective options */
            .attendance-page-content .pull-left {
                display: inline-block;
                margin: 0 18px 14px 0;
                padding: 9px 13px;
                border: 1px solid #e4e9cb;
                border-radius: 7px;
                background: #fffef8;
                color: #34425a;
                font-size: 13px;
            }

            .attendance-page-content input[type="checkbox"] {
                width: 15px;
                height: 15px;
                margin-right: 6px;
                accent-color: #f58200;
                vertical-align: middle;
            }

        /* Course, semester, section, date and batch filter panel */
        .attendance-filter-table {
            width: 100% !important;
            margin: 8px auto 18px;
            border: 1px solid #e1e6d0 !important;
            border-radius: 9px;
            border-collapse: separate !important;
            border-spacing: 0 !important;
            background: #fff !important;
            overflow: hidden;
        }

            .attendance-filter-table tr,
            .attendance-filter-table td {
                background: #fff !important;
            }

            .attendance-filter-table td {
                padding: 9px 7px;
                color: #34435a;
                font-size: 13px;
                vertical-align: middle;
            }

            .attendance-filter-table select,
            .attendance-filter-table input[type="text"] {
                box-sizing: border-box;
                min-height: 35px;
                padding: 6px 9px;
                border: 1px solid #d5deeb;
                border-radius: 6px;
                background: #fff !important;
                color: #26364d;
                font: 13px "Segoe UI", Arial, sans-serif;
            }

                .attendance-filter-table select:focus,
                .attendance-filter-table input[type="text"]:focus {
                    border-color: #f58200;
                    outline: 0;
                    box-shadow: 0 0 0 3px rgba(245, 130, 0, .13);
                }

            .attendance-filter-table img {
                vertical-align: middle;
                cursor: pointer;
            }

        /* Orange action buttons, matching the university navigation theme */
        #btnShow, #btnShow1, #btnExport {
            display: inline-block;
            padding: 9px 18px;
            border: 1px solid #e87500;
            border-radius: 6px;
            background: linear-gradient(180deg, #ff980b 0%, #f07800 100%);
            color: #fff !important;
            font: 600 13px "Segoe UI", Arial, sans-serif;
            text-decoration: none;
            cursor: pointer;
            box-shadow: 0 3px 7px rgba(190, 93, 0, .18);
            transition: background .15s ease, transform .15s ease;
        }

            #btnShow:hover, #btnShow1:hover, #btnExport:hover {
                background: #df6900;
                transform: translateY(-1px);
            }

        #btnExport {
            margin-top: 10px;
        }

        /* Attendance results table */
        .attendance-grid-scroll {
            width: 100%;
            max-height: 480px;
            overflow: auto;
            border: 1px solid #e0e5d4;
            border-radius: 8px;
            background: #fff;
            box-shadow: 0 3px 10px rgba(39, 53, 24, .06);
        }

        .MyClass {
            width: 100% !important;
            min-width: 900px;
            margin: 0;
            border: 0 !important;
            border-collapse: separate !important;
            border-spacing: 0 !important;
            background: #fff;
            font-family: "Segoe UI", Arial, sans-serif;
            font-size: 13px !important;
        }

            .MyClass th {
                position: sticky;
                top: 0;
                z-index: 2;
                padding: 12px 10px !important;
                border: 0 !important;
                border-right: 1px solid rgba(255,255,255,.35) !important;
                background: #f58200 !important;
                color: #fff !important;
                font-size: 12px !important;
                font-weight: 700 !important;
                text-align: left;
                white-space: nowrap;
            }

            .MyClass td {
                padding: 10px !important;
                border: 0 !important;
                border-bottom: 1px solid #ecefe5 !important;
                color: #34435a !important;
                background: #fff !important;
                font-size: 13px !important;
                font-weight: 400 !important;
                vertical-align: middle;
            }

            .MyClass tr:nth-child(even) td {
                background: #fffaf1 !important;
            }

            .MyClass tr:hover td {
                background: #fff0d9 !important;
            }

            .MyClass a {
                color: #e87500;
                font-weight: 600;
                text-decoration: none;
            }

                .MyClass a:hover {
                    color: #a84e00;
                    text-decoration: underline;
                }

        /* Details popup */
        .modalPopup {
            max-width: 96%;
            padding: 0 !important;
            border: 1px solid #e4e8d8;
            border-radius: 10px;
            background: #fff;
            box-shadow: 0 16px 48px rgba(31, 41, 20, .24);
        }

            .modalPopup .header {
                min-height: 40px;
                padding: 8px 15px;
                border-radius: 9px 9px 0 0;
                background: #f58200;
                color: #fff;
                line-height: 24px;
                text-align: left;
                font-weight: 700;
            }

            .modalPopup .close {
                float: right;
            }

            .modalPopup .body {
                min-height: 50px;
                padding: 15px;
                background: #fff;
                line-height: 1.6;
                text-align: center;
            }

            .modalPopup .footer {
                padding: 8px;
            }

            .modalPopup .header input {
                padding: 4px 9px;
                border: 0;
                border-radius: 5px;
                background: #fff;
                color: #9a4a00;
                cursor: pointer;
            }

            .modalPopup td {
                text-align: left;
            }

        .modalBackground {
            background: rgba(30, 38, 20, .48);
        }

        .CSS1 {
            background-color: #fff0a8 !important;
        }

        .CSS2 {
            background-color: #fffaf1 !important;
        }

        .redBorder {
            border: 1px solid #d9534f !important;
        }

        .cssGridheaderfont1 {
            font-size: 12px;
            font-family: "Segoe UI", Arial, sans-serif;
        }

        /* Validation and loading feedback */
        .attendance-page-heading span[style*="color:Red"],
        .attendance-page-content span[style*="color:Red"] {
            color: #c62828 !important;
            font-size: 12px !important;
        }

        #divImage {
            color: #f58200;
            margin-left: 8px;
        }

        @media (max-width: 1100px) {
            .attendance-page-heading {
                gap: 10px;
            }

                .attendance-page-heading #Label3 {
                    width: 100%;
                    margin-bottom: 4px;
                }

            .attendance-page-content {
                padding: 12px;
            }

            .attendance-filter-table {
                min-width: 900px;
            }
        }


        /* ===== Compact spacing overrides (reduce excess vertical gaps) ===== */
        .attendance-page-heading {
            padding: 10px 18px !important;
            gap: 7px 12px !important;
            min-height: 0 !important;
        }

            .attendance-page-heading #Label3 {
                margin: 0 12px 0 0 !important;
                line-height: 1.2 !important;
            }

        .boxBodyHeader {
            height: 4px !important;
            min-height: 4px !important;
        }

        .attendance-page-content {
            padding: 10px 14px !important;
        }

            .attendance-page-content > center > br {
                display: none !important;
            }

            .attendance-page-content .pull-left {
                margin: 0 12px 8px 0 !important;
                padding: 5px 9px !important;
            }

        .attendance-filter-table {
            margin: 4px auto 10px !important;
        }

            .attendance-filter-table td {
                padding: 5px 5px !important;
            }

            .attendance-filter-table select,
            .attendance-filter-table input[type="text"] {
                min-height: 31px !important;
                padding: 4px 7px !important;
            }

        .attendance-grid-scroll {
            max-height: 400px !important;
        }

            .attendance-grid-scroll .MyClass th {
                padding: 9px 8px !important;
            }

            .attendance-grid-scroll .MyClass td {
                padding: 7px 8px !important;
            }

        .attendance-page-content .clearfix {
            margin-bottom: 0 !important;
            padding-bottom: 0 !important;
        }

        @media (max-width: 1100px) {
            .attendance-page-content {
                padding: 8px !important;
            }

            .attendance-page-heading {
                padding: 10px 12px !important;
            }
        }


        /* ===== Extra-tight spacing: heading, Course/Group filters, GridView ===== */

        /* Reduce gap between Review Attendance title and academic filters */
        .attendance-page-heading {
            padding: 7px 14px !important;
            gap: 4px 8px !important;
            min-height: 0 !important;
        }

            .attendance-page-heading #Label3 {
                margin-right: 5px !important;
                font-size: 21px !important;
            }

            .attendance-page-heading select {
                min-height: 29px !important;
                padding: 3px 7px !important;
            }

        /* Reduce large blank area between Course row and Group row */
        .attendance-page-content {
            padding: 6px 10px !important;
        }

            .attendance-page-content > center {
                margin: 0 !important;
                padding: 0 !important;
            }

            .attendance-page-content .pull-left {
                margin: 0 8px 4px 0 !important;
                padding: 3px 7px !important;
            }

        /* Compact filter table cells and both filter rows */
        .attendance-filter-table {
            margin: 0 auto 6px !important;
            border-spacing: 0 !important;
        }

            .attendance-filter-table tr,
            .attendance-filter-table td {
                line-height: 1.2 !important;
            }

            .attendance-filter-table td {
                padding: 3px 4px !important;
            }

            .attendance-filter-table select,
            .attendance-filter-table input[type="text"] {
                min-height: 28px !important;
                height: 29px !important;
                padding: 3px 6px !important;
                font-size: 12px !important;
            }

        /* Reduce gap above and below GridView */
        .attendance-grid-scroll {
            margin: 0 !important;
            max-height: 370px !important;
            min-height: 0 !important;
            padding: 0 !important;
        }

            .attendance-grid-scroll .MyClass {
                margin: 0 !important;
            }

                .attendance-grid-scroll .MyClass th {
                    padding: 7px 7px !important;
                    line-height: 1.15 !important;
                }

                .attendance-grid-scroll .MyClass td {
                    padding: 5px 7px !important;
                    line-height: 1.15 !important;
                }

        /* Remove empty spacer rows within the filter table */
        .attendance-filter-table tr:has(td[colspan="17"]:empty) {
            height: 0 !important;
        }


        /* ===== Final layout adjustments requested in screenshot ===== */

        /* Keep the pale green background continuing down the page content area */
        body {
            background-color: #eef5ad;
        }

        .attendance-page-content {
            background: #eef5ad !important;
            padding: 5px 8px !important;
            min-height: 520px !important;
            overflow: visible !important;
        }

        /* Reduce horizontal spacing between Course and Group filter fields */
        .attendance-filter-table {
            margin-top: 0 !important;
            margin-bottom: 4px !important;
        }

            .attendance-filter-table td {
                padding: 2px 3px !important;
            }

            .attendance-filter-table select,
            .attendance-filter-table input[type="text"] {
                min-height: 27px !important;
                height: 28px !important;
            }

            /* Keep the filter card compact and let the grid start immediately after it */
            .attendance-filter-table + br,
            .attendance-page-content center > br {
                display: none !important;
            }

        .attendance-grid-scroll {
            margin-top: 2px !important;
            margin-bottom: 0 !important;
            max-height: 370px !important;
        }

            .attendance-grid-scroll .MyClass th {
                padding: 6px 6px !important;
            }

            .attendance-grid-scroll .MyClass td {
                padding: 4px 6px !important;
            }


        /* ===== Latest requested adjustments ===== */

        /* Move Course label/control slightly lower to align with the filter row */
        .attendance-filter-table tr:nth-child(2) td {
            padding-top: 5px !important;
            padding-bottom: 3px !important;
        }

        /* White background behind filter labels/words */
        .attendance-filter-table,
        .attendance-filter-table tr,
        .attendance-filter-table td {
            background-color: #ffffff !important;
        }

        /* Keep the filter card white while retaining the light-green outer page */
        .attendance-page-content {
            background-color: #eef5ad !important;
        }


        /* ===== Fine spacing alignment requested ===== */

        /* Move the complete first filter row slightly down and reduce row-to-row gap */
        .attendance-filter-table {
            margin-top: 1px !important;
            margin-bottom: 2px !important;
        }

            .attendance-filter-table tr:first-child td {
                padding-top: 5px !important;
                padding-bottom: 1px !important;
            }

            .attendance-filter-table tr:nth-child(2) td {
                padding-top: 2px !important;
                padding-bottom: 1px !important;
            }

            /* Bring Course and Group labels/controls a little closer horizontally */
            .attendance-filter-table td {
                padding-left: 2px !important;
                padding-right: 2px !important;
            }

            /* Nudge Course and Group controls slightly lower without changing the whole row */
            .attendance-filter-table tr:first-child select,
            .attendance-filter-table tr:first-child input[type="text"],
            .attendance-filter-table tr:nth-child(2) select,
            .attendance-filter-table tr:nth-child(2) input[type="text"] {
                position: relative;
                top: 2px;
            }

        /* Reduce the gap before GridView and compact its rows a little more */
        .attendance-grid-scroll {
            margin-top: 0 !important;
            padding-top: 0 !important;
            max-height: 360px !important;
        }

            .attendance-grid-scroll .MyClass th {
                padding-top: 5px !important;
                padding-bottom: 5px !important;
            }

            .attendance-grid-scroll .MyClass td {
                padding-top: 3px !important;
                padding-bottom: 3px !important;
            }


        /* ===== Final requested layout: white panel to the bottom ===== */

        /* Main page canvas: white instead of lime green */
        html,
        body,
        form {
            background: #ffffff !important;
        }

        /* Outer content wrapper should remain white down to the footer */
        .attendance-page-content,
        .attendance-page-content .attendance-grid-scroll,
        .attendance-page-content .attendance-filter-table {
            background-color: #ffffff !important;
        }

        /* White main panel extends down the page, matching the second screenshot */
        .attendance-page-content {
            min-height: 440px !important;
            height: auto !important;
            padding-bottom: 8px !important;
            border-color: #e6e6e6 !important;
            border-radius: 0 0 8px 8px !important;
        }

            /* The centered page shell should not paint green behind the white card */
            .attendance-page-content > center,
            .attendance-page-content fieldset,
            .attendance-page-content table,
            .attendance-page-content tr,
            .attendance-page-content td {
                background-color: #ffffff !important;
            }

        /* Preserve the orange university theme and alternating GridView rows */
        .attendance-grid-scroll .MyClass th {
            background: #f58200 !important;
            color: #ffffff !important;
        }

        .attendance-grid-scroll .MyClass td {
            background-color: #ffffff !important;
        }

        .attendance-grid-scroll .MyClass tr:nth-child(even) td {
            background-color: #fffaf1 !important;
        }

        .attendance-grid-scroll .MyClass tr:hover td {
            background-color: #fff0d9 !important;
        }

        /* Keep white background continuous; avoid lime color showing around the card */
        body {
            background-image: none !important;
        }


        /* ===== Premium Attendance Details Popup ===== */
        .modalPopup {
            width: min(920px, 94vw) !important;
            max-width: 94vw !important;
            padding: 0 !important;
            border: 1px solid #d8e1ee !important;
            border-radius: 12px !important;
            overflow: hidden !important;
            background: #ffffff !important;
            box-shadow: 0 20px 60px rgba(18, 35, 61, .30) !important;
            font-family: "Segoe UI", Arial, sans-serif !important;
        }

            .modalPopup .header {
                display: flex !important;
                align-items: center !important;
                justify-content: space-between !important;
                min-height: 46px !important;
                padding: 10px 16px !important;
                border-radius: 0 !important;
                background: linear-gradient(110deg, #f58200, #ff9b21) !important;
                color: #ffffff !important;
                font-size: 15px !important;
                font-weight: 700 !important;
                line-height: 1.3 !important;
                text-align: left !important;
            }

                .modalPopup .header b {
                    color: #ffffff !important;
                    font-weight: 700 !important;
                }

            .modalPopup .close {
                float: none !important;
                margin-left: auto !important;
            }

                .modalPopup .close input,
                .modalPopup .header input[type="submit"],
                .modalPopup .header input[type="button"] {
                    display: inline-flex !important;
                    align-items: center !important;
                    justify-content: center !important;
                    width: 30px !important;
                    height: 30px !important;
                    padding: 0 !important;
                    border: 1px solid rgba(255,255,255,.65) !important;
                    border-radius: 7px !important;
                    background: rgba(255,255,255,.18) !important;
                    color: #ffffff !important;
                    font-size: 16px !important;
                    font-weight: 700 !important;
                    cursor: pointer !important;
                }

                    .modalPopup .close input:hover,
                    .modalPopup .header input[type="submit"]:hover,
                    .modalPopup .header input[type="button"]:hover {
                        background: #ffffff !important;
                        color: #e87500 !important;
                    }

            .modalPopup #Div1 {
                max-height: 68vh !important;
                overflow: auto !important;
                background: #ffffff !important;
            }

            .modalPopup .body {
                min-height: 0 !important;
                padding: 16px !important;
                background: #ffffff !important;
                color: #26364d !important;
                text-align: left !important;
                line-height: 1.45 !important;
            }

                .modalPopup .body > div {
                    width: 100% !important;
                }

                .modalPopup .body center {
                    text-align: left !important;
                }

            .modalPopup #lblStudent,
            .modalPopup #lblCourse {
                display: inline-block !important;
                margin: 0 18px 12px 0 !important;
                padding: 7px 11px !important;
                border: 1px solid #e4eaf2 !important;
                border-radius: 7px !important;
                background: #f5f8fd !important;
                color: #233b60 !important;
                font-size: 13px !important;
                font-weight: 600 !important;
            }

            .modalPopup #grdAttandanceDetails {
                width: 100% !important;
                min-width: 650px !important;
                margin: 0 !important;
                border: 1px solid #e0e7f0 !important;
                border-collapse: separate !important;
                border-spacing: 0 !important;
                border-radius: 8px !important;
                overflow: hidden !important;
                background: #ffffff !important;
                font-family: "Segoe UI", Arial, sans-serif !important;
                font-size: 12px !important;
            }

                .modalPopup #grdAttandanceDetails th {
                    padding: 10px 9px !important;
                    border: 0 !important;
                    border-right: 1px solid rgba(255,255,255,.35) !important;
                    background: #f58200 !important;
                    color: #ffffff !important;
                    font-size: 12px !important;
                    font-weight: 700 !important;
                    text-align: left !important;
                    white-space: nowrap !important;
                }

                .modalPopup #grdAttandanceDetails td {
                    padding: 8px 9px !important;
                    border: 0 !important;
                    border-bottom: 1px solid #e9eef5 !important;
                    color: #33445d !important;
                    background: #ffffff !important;
                    font-size: 12px !important;
                    font-weight: 400 !important;
                    text-align: left !important;
                    vertical-align: middle !important;
                }

                .modalPopup #grdAttandanceDetails tr:nth-child(even) td {
                    background: #f7f9fd !important;
                }

                .modalPopup #grdAttandanceDetails tr:hover td {
                    background: #fff2df !important;
                }

            .modalPopup .body br {
                display: none !important;
            }

        .modalBackground {
            background: rgba(20, 31, 47, .48) !important;
        }

        @media (max-width: 700px) {
            .modalPopup {
                width: 96vw !important;
                max-width: 96vw !important;
            }

                .modalPopup .body {
                    padding: 10px !important;
                }

                .modalPopup #lblStudent,
                .modalPopup #lblCourse {
                    margin: 0 6px 8px 0 !important;
                }
        }



        .modalPopup {
            width: 92vw !important;
            max-width: 1280px !important;
            box-sizing: border-box !important;
            padding: 0 !important;
            border: 1px solid #f3c58f !important;
            border-radius: 13px !important;
            overflow: hidden !important;
            background: #fff !important;
            box-shadow: 0 18px 55px rgba(27, 42, 63, .30) !important;
            font-family: "Segoe UI", Arial, sans-serif !important;
        }

            /* Orange title bar */
            .modalPopup .header {
                display: flex !important;
                align-items: center !important;
                justify-content: space-between !important;
                min-height: 48px !important;
                padding: 9px 16px !important;
                box-sizing: border-box !important;
                border: 0 !important;
                border-radius: 0 !important;
                background: linear-gradient(110deg, #f58200 0%, #ff9d22 100%) !important;
                color: #fff !important;
                text-align: left !important;
                line-height: 1.35 !important;
            }

                .modalPopup .header b,
                .modalPopup #lblNotification {
                    color: #fff !important;
                    font-family: "Segoe UI", Arial, sans-serif !important;
                    font-size: 16px !important;
                    font-weight: 700 !important;
                }

            .modalPopup .close {
                float: none !important;
                margin-left: auto !important;
            }

                .modalPopup .close input,
                .modalPopup .close button,
                .modalPopup .header input[type="submit"],
                .modalPopup .header input[type="button"] {
                    display: inline-flex !important;
                    align-items: center !important;
                    justify-content: center !important;
                    width: 30px !important;
                    height: 30px !important;
                    min-width: 30px !important;
                    padding: 0 !important;
                    border: 1px solid rgba(255,255,255,.7) !important;
                    border-radius: 7px !important;
                    background: rgba(255,255,255,.17) !important;
                    color: #fff !important;
                    font-size: 17px !important;
                    font-weight: 700 !important;
                    cursor: pointer !important;
                }

                    .modalPopup .close input:hover,
                    .modalPopup .close button:hover {
                        background: #fff !important;
                        color: #e87500 !important;
                    }

            /* Scrollable popup content */
            .modalPopup #Div1 {
                max-height: 70vh !important;
                overflow: auto !important;
                background: #fff !important;
            }

            .modalPopup .body {
                min-height: 0 !important;
                padding: 14px 16px 16px !important;
                background: #fff !important;
                color: #273a55 !important;
                text-align: left !important;
                line-height: 1.4 !important;
            }

                .modalPopup .body > div {
                    width: 100% !important;
                }

                .modalPopup .body center {
                    display: block !important;
                    width: 100% !important;
                    text-align: left !important;
                }

            /* Student and course detail chips */
            .modalPopup #lblStudent,
            .modalPopup #lblCourse {
                display: inline-block !important;
                max-width: 100% !important;
                margin: 0 12px 12px 0 !important;
                padding: 8px 12px !important;
                border: 1px solid #f3dfc4 !important;
                border-radius: 7px !important;
                background: #fffaf3 !important;
                color: #253c5b !important;
                font-size: 13px !important;
                font-weight: 600 !important;
                line-height: 1.4 !important;
                vertical-align: middle !important;
            }

            /* Attendance details GridView */
            .modalPopup #grdAttandanceDetails {
                width: 100% !important;
                min-width: 900px !important;
                margin: 0 !important;
                border: 1px solid #dce5ef !important;
                border-collapse: collapse !important;
                border-spacing: 0 !important;
                background: #fff !important;
                font-family: "Segoe UI", Arial, sans-serif !important;
                font-size: 12px !important;
            }

                .modalPopup #grdAttandanceDetails th {
                    padding: 10px 9px !important;
                    border: 1px solid #e8a24c !important;
                    background: #f58200 !important;
                    color: #fff !important;
                    font-size: 12px !important;
                    font-weight: 700 !important;
                    text-align: left !important;
                    white-space: nowrap !important;
                }

                .modalPopup #grdAttandanceDetails td {
                    padding: 8px 9px !important;
                    border: 1px solid #dfe7ef !important;
                    background: #fff !important;
                    color: #33445d !important;
                    font-size: 12px !important;
                    font-weight: 400 !important;
                    text-align: left !important;
                    vertical-align: middle !important;
                }

                .modalPopup #grdAttandanceDetails tr:nth-child(even) td {
                    background: #fff8ed !important;
                }

                .modalPopup #grdAttandanceDetails tr:hover td {
                    background: #ffefd9 !important;
                }

                /* Remove inherited legacy colors that make the header/body hard to read */
                .modalPopup #grdAttandanceDetails .cssGridheaderfont1 {
                    font-family: "Segoe UI", Arial, sans-serif !important;
                    font-size: 12px !important;
                    font-weight: 400;
                }

                .modalPopup #grdAttandanceDetails th.cssGridheaderfont1 {
                    font-weight: 700 !important;
                }

                .modalPopup #grdAttandanceDetails td,
                .modalPopup #grdAttandanceDetails th {
                    opacity: 1 !important;
                    visibility: visible !important;
                }

            .modalPopup .body br {
                display: none !important;
            }

        .modalBackground {
            background: rgba(24, 35, 50, .52) !important;
        }

        @media (max-width: 760px) {
            .modalPopup {
                width: 96vw !important;
                max-width: 96vw !important;
            }

                .modalPopup .body {
                    padding: 10px !important;
                }

                .modalPopup #grdAttandanceDetails {
                    min-width: 760px !important;
                }

                .modalPopup #lblStudent,
                .modalPopup #lblCourse {
                    margin-right: 5px !important;
                    margin-bottom: 8px !important;
                }
        }


        .modalPopup table[id$="grdAttandanceDetails"],
        .modalPopup table[id*="grdAttandanceDetails"] {
            width: 100% !important;
            min-width: 900px !important;
            border: 1px solid #cfd8e3 !important;
            border-collapse: collapse !important;
            border-spacing: 0 !important;
            table-layout: auto !important;
            background: #ffffff !important;
            font-family: "Segoe UI", Arial, sans-serif !important;
            font-size: 12px !important;
        }

            /* Force the header row to be visible orange with white text */
            .modalPopup table[id$="grdAttandanceDetails"] thead,
            .modalPopup table[id$="grdAttandanceDetails"] tr:first-child,
            .modalPopup table[id*="grdAttandanceDetails"] tr:first-child {
                display: table-row !important;
                visibility: visible !important;
                opacity: 1 !important;
            }

                .modalPopup table[id$="grdAttandanceDetails"] th,
                .modalPopup table[id*="grdAttandanceDetails"] th,
                .modalPopup table[id$="grdAttandanceDetails"] tr:first-child td,
                .modalPopup table[id*="grdAttandanceDetails"] tr:first-child td {
                    padding: 10px 9px !important;
                    border: 1px solid #e2a04a !important;
                    background: #f58200 !important;
                    color: #ffffff !important;
                    -webkit-text-fill-color: #ffffff !important;
                    font-family: "Segoe UI", Arial, sans-serif !important;
                    font-size: 12px !important;
                    font-weight: 700 !important;
                    line-height: 1.35 !important;
                    text-align: left !important;
                    white-space: nowrap !important;
                    visibility: visible !important;
                    opacity: 1 !important;
                }

            /* Clear legacy GridView row styles and add light grey borders */
            .modalPopup table[id$="grdAttandanceDetails"] td,
            .modalPopup table[id*="grdAttandanceDetails"] td {
                padding: 8px 9px !important;
                border: 1px solid #d8e0e9 !important;
                background: #ffffff !important;
                color: #34445b !important;
                -webkit-text-fill-color: #34445b !important;
                font-family: "Segoe UI", Arial, sans-serif !important;
                font-size: 12px !important;
                font-weight: 500 !important;
                line-height: 1.35 !important;
                vertical-align: middle !important;
                visibility: visible !important;
                opacity: 1 !important;
            }

            .modalPopup table[id$="grdAttandanceDetails"] tr:nth-child(even) td,
            .modalPopup table[id*="grdAttandanceDetails"] tr:nth-child(even) td {
                background: #f7f9fc !important;
            }

            .modalPopup table[id$="grdAttandanceDetails"] tr:hover td,
            .modalPopup table[id*="grdAttandanceDetails"] tr:hover td {
                background: #fff0db !important;
            }

        /* Keep popup body readable and prevent content from hiding the header */
        .modalPopup #Div1,
        .modalPopup div[id$="Div1"] {
            max-height: 70vh !important;
            overflow: auto !important;
            background: #ffffff !important;
        }

        .modalPopup .body {
            padding: 15px !important;
            background: #ffffff !important;
            overflow-x: auto !important;
        }

        .modalPopup .header,
        .modalPopup .header b,
        .modalPopup .header #lblNotification {
            background: linear-gradient(110deg, #f58200, #ff9e24) !important;
            color: #ffffff !important;
            -webkit-text-fill-color: #ffffff !important;
            visibility: visible !important;
            opacity: 1 !important;
        }

        .modalPopup .close input,
        .modalPopup .close input[type="submit"],
        .modalPopup .close input[type="button"] {
            color: #ffffff !important;
            -webkit-text-fill-color: #ffffff !important;
            border: 1px solid rgba(255,255,255,.75) !important;
            background: rgba(255,255,255,.18) !important;
        }


            /* ==========================================================
FINAL POPUP FIX: white Close X + scroll after 10 rows
========================================================== */

            /* White close button background, orange X */
            .modalPopup .close input,
            .modalPopup .close input[type="submit"],
            .modalPopup .close input[type="button"],
            .modalPopup #btnclose {
                background: #ffffff !important;
                background-color: #ffffff !important;
                color: #f58200 !important;
                -webkit-text-fill-color: #f58200 !important;
                border: 1px solid #ffffff !important;
                border-radius: 6px !important;
                box-shadow: none !important;
                width: 30px !important;
                height: 30px !important;
                min-width: 30px !important;
                padding: 0 !important;
                font-weight: 700 !important;
                line-height: 28px !important;
                text-align: center !important;
                cursor: pointer !important;
            }

                .modalPopup .close input:hover,
                .modalPopup #btnclose:hover {
                    background: #fff4e5 !important;
                    background-color: #fff4e5 !important;
                    color: #d96500 !important;
                }

        /* The panel already has Div1 around the GridView: constrain it and scroll */
        .modalPopup #Div1,
        .modalPopup div[id$="Div1"] {
            height: auto !important;
            max-height: 70vh !important;
            overflow-y: auto !important;
            overflow-x: auto !important;
        }

        /* Keep the GridView header visible and show about 10 records before scrolling */
        .modalPopup #grdAttandanceDetails,
        .modalPopup table[id$="grdAttandanceDetails"],
        .modalPopup table[id*="grdAttandanceDetails"] {
            width: 100% !important;
            min-width: 900px !important;
            border-collapse: collapse !important;
            border-spacing: 0 !important;
        }

            /* Compact detail rows: roughly 10 rows fit within a 350-380px content area */
            .modalPopup #grdAttandanceDetails th,
            .modalPopup table[id*="grdAttandanceDetails"] th {
                position: sticky !important;
                top: 0 !important;
                z-index: 3 !important;
                padding: 8px 9px !important;
                background: #f58200 !important;
                color: #ffffff !important;
                border: 1px solid #e2a04a !important;
                white-space: nowrap !important;
            }

            .modalPopup #grdAttandanceDetails td,
            .modalPopup table[id*="grdAttandanceDetails"] td {
                padding: 7px 9px !important;
                border: 1px solid #d8e0e9 !important;
                line-height: 1.25 !important;
            }

        .modalPopup .body {
            padding: 12px 14px !important;
        }

        /* Allow popup content to scroll vertically after the first 10-ish rows */
        .modalPopup #Div1,
        .modalPopup div[id$="Div1"] {
            max-height: 390px !important;
            overflow-y: auto !important;
            overflow-x: auto !important;
        }

        @media (max-height: 800px) {
            .modalPopup #Div1,
            .modalPopup div[id$="Div1"] {
                max-height: 330px !important;
            }
        }


        /* Close X button: white background with orange X */
        .modalPopup #btnclose,
        .modalPopup .close #btnclose,
        .modalPopup .close input[id$="btnclose"],
        .modalPopup .close input[type="submit"],
        .modalPopup .close input[type="button"] {
            background: #ffffff !important;
            background-color: #ffffff !important;
            color: #f58200 !important;
            -webkit-text-fill-color: #f58200 !important;
            border: 1px solid #ffffff !important;
            border-radius: 6px !important;
            box-shadow: none !important;
            width: 30px !important;
            min-width: 30px !important;
            height: 30px !important;
            padding: 0 !important;
            font-size: 16px !important;
            font-weight: 700 !important;
            line-height: 28px !important;
            text-align: center !important;
            cursor: pointer !important;
        }

            .modalPopup #btnclose:hover,
            .modalPopup .close input[id$="btnclose"]:hover {
                background: #fff4e5 !important;
                background-color: #fff4e5 !important;
                color: #d96500 !important;
                -webkit-text-fill-color: #d96500 !important;
            }


        /* ===== Center GridView header text vertically and horizontally ===== */
        .attendance-grid-scroll .MyClass th,
        .modalPopup #grdAttandanceDetails th,
        .modalPopup table[id*="grdAttandanceDetails"] th {
            vertical-align: middle !important;
            line-height: 1.35 !important;
            padding-top: 9px !important;
            padding-bottom: 9px !important;
            text-align: center !important;
        }

        /* Keep detail-popup headers centered; preserve orange header and white text */
        .modalPopup #grdAttandanceDetails th,
        .modalPopup table[id*="grdAttandanceDetails"] th {
            background: #f58200 !important;
            color: #ffffff !important;
            -webkit-text-fill-color: #ffffff !important;
        }

        /* Main attendance table headers centered, no text touching the top edge */
        .attendance-grid-scroll .MyClass th {
            display: table-cell !important;
            vertical-align: middle !important;
        }

        .review-header-row {
            display: flex;
            align-items: center;
            justify-content: space-between;
        }

        .export-button-container {
            margin-left: auto;
        }

        .btn-export-excel {
            min-height: 35px;
            padding: 10px 16px;
            background: #ed7600;
            color: white !important;
            border: none;
            border-radius: 5px;
            font-weight: 600;
            cursor: pointer;
        }

            .btn-export-excel:hover {
                background: #cc6200;
            }




        /* ===== FINAL FIX: remove white strip below attendance content ===== */
        html,
        body,
        form {
            background-color: #eef5ad !important;
        }

        .attendance-page-content,
        .boxBodyInner {
            min-height: 0 !important;
            height: auto !important;
            margin-bottom: 0 !important;
            padding-bottom: 0 !important;
        }

        .attendance-grid-scroll {
            margin-bottom: 0 !important;
            padding-bottom: 0 !important;
        }

        /* Keep the attendance panel attached to the footer area */
        .attendance-page-content {
            border-bottom: 0 !important;
            border-radius: 0 !important;
        }

        /* Prevent default spacing above the footer */
        footer {
            margin-top: 0 !important;
            border-top: 0 !important;
        }



        /* ===== Extend the white attendance panel down toward the footer ===== */
        .attendance-page-content,
        .boxBodyInner {
            min-height: calc(100vh - 380px) !important;
            height: auto !important;
            margin-bottom: 0 !important;
            padding-bottom: 0 !important;
            box-sizing: border-box !important;
            background: #ffffff !important;
        }

        /* Let the attendance GridView use more vertical space */
        .attendance-grid-scroll {
            max-height: 500px !important;
            margin-bottom: 0 !important;
            padding-bottom: 0 !important;
        }

        /* Remove any border/spacing that looks like a white strip */
        .attendance-page-content {
            border-bottom: 0 !important;
            border-radius: 0 !important;
        }

        footer {
            margin-top: 0 !important;
            border-top: 0 !important;
        }


        /* GridView ko thoda neeche tak badhayein */
        .attendance-grid-scroll {
            max-height: calc(100vh - 510px) !important;
            margin-bottom: 0 !important;
        }

        /* Footer ko grid ke kareeb rakhein */
        .attendance-page-content {
            padding-bottom: 0 !important;
            margin-bottom: 0 !important;
        }

        .calendar-icon {
            width: 25px !important;
            height: 25px !important;
            margin-left: 4px;
            vertical-align: middle;
            cursor: pointer;
            object-fit: contain;
            transition: transform .2s ease;
        }

            .calendar-icon:hover {
                transform: scale(1.12);
            }

        /* Calendar Popup */
        .cal_Theme1 .ajax__calendar_container {
            width: 220px !important;
            padding: 8px !important;
            background: #fff !important;
            border: 1px solid #f58200 !important;
            border-radius: 10px !important;
            box-shadow: 0 5px 18px rgba(0,0,0,.18) !important;
            font-family: "Segoe UI", Arial, sans-serif !important;
            box-sizing: border-box !important;
        }

        /* Orange Month Header */
        .cal_Theme1 .ajax__calendar_header {
            height: 30px !important;
            padding: 5px !important;
            background: #f58200 !important;
            border-radius: 6px !important;
            box-sizing: border-box !important;
        }

        .cal_Theme1 .ajax__calendar_title,
        .cal_Theme1 .ajax__calendar_prev,
        .cal_Theme1 .ajax__calendar_next {
            color: #fff !important;
            font-weight: 600 !important;
        }

        /* Calendar Dates */
        .cal_Theme1 .ajax__calendar_body {
            width: 100% !important;
            height: 150px !important;
            overflow: visible !important;
        }

        .cal_Theme1 .ajax__calendar_days,
        .cal_Theme1 .ajax__calendar_months,
        .cal_Theme1 .ajax__calendar_years {
            width: 100% !important;
        }

        .cal_Theme1 .ajax__calendar_dayname,
        .cal_Theme1 .ajax__calendar_day,
        .cal_Theme1 .ajax__calendar_month,
        .cal_Theme1 .ajax__calendar_year {
            text-align: center !important;
            border: 0 !important;
            box-sizing: border-box !important;
        }

        .cal_Theme1 .ajax__calendar_dayname {
            color: #f58200 !important;
            font-weight: 700 !important;
        }

        .cal_Theme1 .ajax__calendar_day {
            color: #333 !important;
            border-radius: 5px !important;
        }

        .cal_Theme1 .ajax__calendar_other .ajax__calendar_day {
            color: #aaa !important;
        }

        /* Hover and Selected Date */
        .cal_Theme1 .ajax__calendar_hover,
        .cal_Theme1 .ajax__calendar_active {
            background: #f58200 !important;
            color: #fff !important;
            border-radius: 5px !important;
        }

        .cal_Theme1 .ajax__calendar_today {
            color: #f58200 !important;
            font-weight: 700 !important;
        }

        /* Footer */
        .cal_Theme1 .ajax__calendar_footer {
            border-top: 1px solid #eee !important;
            padding-top: 6px !important;
            color: #f58200 !important;
        }


        .attendance-filter-table {
            position: relative !important;
        }

        .attendance-btn-right {
            position: absolute !important;
            right: 10px !important;
            top: 47px !important;
            width: auto !important;
            display: flex !important;
            justify-content: flex-end !important;
            align-items: center !important;
            gap: 8px !important;
            white-space: nowrap !important;
            z-index: 5 !important;
        }

        .attendance-btn {
            margin: 0 !important;
            color: #000 !important;
            font-weight: bold !important;
        }


        a.attendance-btn,
        a.attendance-btn:link,
        a.attendance-btn:visited,
        a.attendance-btn:hover,
        a.attendance-btn:focus,
        a.attendance-btn:active {
            color: #000000 !important;
            font-weight: 700 !important;
            text-decoration: none !important;
        }

            a.attendance-btn .glyphicon {
                color: #000000 !important;
                font-weight: bold !important;
            }


        /* Calendar popup ko poora visible rakho */
        .attendance-filter-table {
            overflow: visible !important;
        }

            .attendance-filter-table td {
                overflow: visible !important;
            }

        .cal_Theme1,
        .cal_Theme1 .ajax__calendar_container {
            position: absolute !important;
            z-index: 99999 !important;
            overflow: visible !important;
        }

        /* Buttons ki position same rakho */
        .attendance-btn-right {
            z-index: 5 !important;
        }

        .attendance-filter-table {
            padding-bottom: 8px !important;
        }

            .attendance-filter-table tr:last-child td {
                padding-bottom: 8px !important;
            }

            .attendance-filter-table tr + tr td {
                padding-top: 4px !important;
                padding-bottom: 2px !important;
            }
        /* Hide dates belonging to previous and next months */
        .cal_Theme1 .ajax__calendar_other .ajax__calendar_day {
            display: none !important;
        }
    </style>
    <style type="text/css">
        .modalBackground {
            background-color: rgba(0, 0, 0, 0.30);
        }

        .mentee-modal {
            width: 850px;
            max-width: 92vw;
            background: #fff;
            border: 1px solid #ddd;
            border-radius: 2px;
            box-shadow: 0 3px 12px rgba(0,0,0,.18);
            font-family: Arial, sans-serif;
            color: #34445b;
        }

        .mentee-modal-header {
            background: #fff;
            color: #24456b;
            padding: 12px 15px;
            border-bottom: 3px solid #f58200;
        }

        .mentee-modal-title {
            float: left;
            font-size: 18px;
            font-weight: normal;
        }

        .mentee-modal-close {
            float: right;
            color: #f58200;
            font-size: 25px;
            line-height: 20px;
            text-decoration: none;
        }

        .mentee-modal-toolbar {
            display: flex;
            align-items: center;
            gap: 8px;
            padding: 10px 12px;
            background: #fff;
            border-bottom: 1px solid #eee;
        }

        .mentee-total {
            margin-right: auto;
            color: #34445b;
            font-size: 13px;
        }

        .mentee-export {
            display: inline-block;
            padding: 8px 12px;
            background: #f58200 !important;
            color: #fff !important;
            border: 1px solid #e87500;
            border-radius: 2px;
            font-size: 12px;
            font-weight: normal;
            text-decoration: none !important;
            white-space: nowrap;
        }

            .mentee-export:hover {
                background: #e87500 !important;
                color: #fff !important;
            }

        .mentee-grid-wrapper {
            padding: 10px;
            max-height: 400px;
            overflow: auto;
            background: #fff;
        }

        .mentee-grid {
            width: 100%;
            border-collapse: collapse;
            font-size: 12px;
        }

            .mentee-grid th {
                background: #f58200;
                color: #fff;
                padding: 9px 8px;
                border: 1px solid #e87500;
                text-align: center;
                white-space: nowrap;
            }

            .mentee-grid td {
                padding: 8px;
                color: #45546a;
                border: 1px solid #eee5d8;
                background: #fff;
            }

            .mentee-grid tr:nth-child(even) td {
                background: #fff8eb;
            }

            .mentee-grid tr:hover td {
                background: #fff0d6;
            }

        .ec-form-row {
            display: flex;
            gap: 15px;
            padding: 16px;
            background: #fff;
            align-items: flex-start;
        }

        .ec-half {
            width: 50%;
            min-width: 0;
        }

        .ec-label {
            display: block;
            margin-bottom: 8px;
            color: #34445b;
            font-size: 13px;
            font-weight: 600;
        }

        .ec-remarks {
            width: 100%;
            height: 110px;
            box-sizing: border-box;
            padding: 10px;
            border: 1px solid #ddd;
            border-left: 3px solid #f58200;
            border-radius: 4px;
            font: 13px Arial;
            resize: vertical;
        }

            .ec-remarks:focus {
                outline: none;
                border-color: #f58200;
            }

        .ec-upload-box {
            min-height: 110px;
            box-sizing: border-box;
            padding: 14px 10px;
            border: 1px dashed #f58200;
            border-radius: 4px;
            background: #fffaf2;
        }

        .ec-file {
            width: 100%;
            font-size: 12px;
        }

        .ec-upload-hint {
            display: block;
            margin-top: 12px;
            color: #788397;
            font-size: 11px;
        }

        @media (max-width: 600px) {
            .ec-form-row {
                flex-direction: column;
            }

            .ec-half {
                width: 100%;
            }
        }
    </style>
    <style type="text/css">
        .student-checkbox,
        .student-code {
            display: inline-block;
            vertical-align: middle;
        }

        .student-checkbox {
            position: relative;
            top: -3px;
            margin: 0 8px 0 0;
            vertical-align: middle;
        }

        .student-code {
            line-height: 20px;
        }

        .ec-status {
            display: inline-block;
            min-width: 75px;
            padding: 5px 10px;
            border-radius: 5px;
            font-size: 12px;
            font-weight: 600;
            text-align: center;
            letter-spacing: 0.2px;
            border: 1px solid transparent;
        }

        .ec-pending {
            color: #0875b7;
            background: #e8f5ff;
            border-color: #b9e0fa;
        }

        .ec-approved {
            color: #168044;
            background: #e8f8ee;
            border-color: #b6e7c8;
        }
    </style>
    <style type="text/css">
        .attachment-link {
            color: #ff6600;
            text-decoration: underline;
            cursor: pointer;
        }

        .attachment-modal {
            display: none;
            position: fixed;
            z-index: 99999;
            inset: 0;
            background: rgba(0,0,0,.75);
            align-items: center;
            justify-content: center;
        }

        .attachment-box {
            position: relative;
            background: #fff;
            width: 80%;
            height: 85%;
            padding: 15px;
            border-radius: 6px;
        }

        .attachment-close {
            position: absolute;
            right: 12px;
            top: 2px;
            font-size: 32px;
            cursor: pointer;
            z-index: 2;
        }

        .attachment-content {
            height: 100%;
            padding-top: 25px;
            box-sizing: border-box;
            overflow: auto;
            text-align: center;
        }

        #attachmentImage {
            max-width: 100%;
            max-height: 95%;
            object-fit: contain;
            cursor: zoom-in;
        }

            #attachmentImage.enlarged {
                max-width: none;
                max-height: none;
                width: auto;
                height: auto;
                cursor: zoom-out;
            }

        #attachmentPdf {
            width: 100%;
            height: 100%;
            border: none;
        }
    </style>
    <style type="text/css">
        
.remarks-label {
    display: -webkit-box;
    width: 190px;
    max-height: 36px;
    overflow: hidden;
    -webkit-line-clamp: 2;
    -webkit-box-orient: vertical;
    overflow-wrap: anywhere;
    cursor: pointer;
    line-height: 18px;
    color: #333;
}
.remarks-label:hover {
    color: #f58200;
    text-decoration: underline;
}
.remarks-popup {
    display: none;
    position: fixed;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    width: 380px;
    max-width: 90%;
    padding: 16px;
    background: #fff;
    border-top: 3px solid #f58200;
    border-radius: 7px;
    box-shadow: 0 5px 20px #999;
    z-index: 99999;
    overflow-wrap: anywhere;
    line-height: 1.6;
}
.remarks-popup .close {
    float: right;
    color: #f58200;
    cursor: pointer;
    font-size: 22px;
}
.remarks-label {
    display: block;
    width: 190px;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    cursor: pointer;
    color: #333;
}

.remarks-label:hover {
    color: #f58200;
}

.ec-approve-btn {
    background: #ff8000 !important;
    color: #fff !important;
    border: 1px solid #e87500 !important;
    padding: 8px 20px !important;
    border-radius: 4px !important;
    font-size: 13px !important;
    font-weight: 600;
    min-width: 85px;
    cursor: pointer;
}

.ec-approve-btn:disabled {
    background: #d1d5db !important;
    color: #6b7280 !important;
    border: 1px solid #b8bec7 !important;
    cursor: not-allowed;
    opacity: 1;
}


.faculty-info {
    display: flex;
    align-items: center;
    gap: 8px;
    width: fit-content;
    max-width: 100%;
    margin: 5px 10px 2px;
    padding: 5px 8px;
    background: #fff7ed;
    border: 1px solid #ffd6a5;
    border-left: 3px solid #ff8000;
    border-radius: 5px;
}

.faculty-item {
    display: flex;
    align-items: center;
    gap: 8px;
}

.faculty-title {
    font-size: 13px;
    font-weight: 600;
    color: #555;
    white-space: nowrap;
}

.faculty-value {
    font-size: 13px;
    font-weight: 600;
    color: #b45309;
}

.faculty-item {
    gap: 5px;
}

.faculty-divider {
    height: 18px;
}
    </style>
    <style type="text/css">
        .chk-row {
    display: flex;
    flex-wrap: wrap;
    align-items: center;
    gap: 10px;
    padding: 6px 0;
}

.chk-pill {
    display: inline-flex;
    align-items: center;
    gap: 6px;
    padding: 2px 10px;
    background: transparent;
    border: 1px solid #d9c9b8;
    border-radius: 50px;
    cursor: pointer;
    transition: border-color 0.2s ease;
}

.chk-pill:hover {
    border-color: #f28c1e;
}

.chk-pill input[type="checkbox"] {
    width: 14px;
    height: 14px;
    margin: 0;
    cursor: pointer;
    accent-color: #f28c1e;
}

.chk-pill label {
    margin: 0;
    font-size: 10pt;
    font-weight: 500;
    color: #222;
    cursor: pointer;
    white-space: nowrap;
}
    </style>

    <script type="text/javascript">
        function openAttachment(url) {
            var modal = document.getElementById('attachmentModal');
            var img = document.getElementById('attachmentImage');
            var pdf = document.getElementById('attachmentPdf');

            img.style.display = 'none';
            pdf.style.display = 'none';
            img.classList.remove('enlarged');
            img.onclick = function () {
                enlargeImage(this);
            };

            var extension = url.split('?')[0].split('.').pop().toLowerCase();

            modal.style.display = 'flex';

            if (extension === 'pdf') {
                pdf.src = url;
                pdf.style.display = 'block';
            } else {
                img.onload = function () {
                    img.style.display = 'inline-block';
                };

                img.onerror = function () {
                    alert('Image open nahi ho rahi. Attachment path check karein.');
                };

                img.src = url;
            }
        }

        function enlargeImage(img) {
            img.classList.toggle('enlarged');
        }

        function hideAttachment() {
            document.getElementById('attachmentModal').style.display = 'none';
            document.getElementById('attachmentImage').src = '';
            document.getElementById('attachmentPdf').src = '';
        }

        function closeAttachment(event) {
            if (event.target.id === 'attachmentModal') {
                hideAttachment();
            }
        }

        document.addEventListener('keydown', function (event) {
            if (event.key === 'Escape') {
                hideAttachment();
            }
        });
    </script>
    
<script type="text/javascript">
    function showRemarks(el) {
        document.getElementById('remarksPopupText').textContent = el.innerText;
        document.getElementById('remarksPopup').style.display = 'block';
    }

    function hideRemarks() {
        document.getElementById('remarksPopup').style.display = 'none';
    }
</script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1"
        TargetControlID="UpdatePanel1" runat="server">
        <Animations>
            <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script="onUpdating();" />
                    <EnableAction AnimationTarget="ddlReaapear" Enabled="false" /> 
                </Parallel>
            </OnUpdating>
        </Animations>
    </asp:UpdatePanelAnimationExtender>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <fieldset class="boxBody attendance-page-heading">

                <asp:Label ID="Label3" runat="server"
                    Text="Approved ECA Attendance" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    Academic Year:&nbsp&nbsp
                   
                <asp:DropDownList ID="ddlAcademicYear" Width="100px" Height="20px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Font-Size="13px" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="ddlAcademicYear" ValidationGroup="g1" ErrorMessage="Please select the Academic Year!"></asp:RequiredFieldValidator>
                <asp:Label ID="lblText" runat="server" Text="" Height="10px"></asp:Label>
                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                   Type:&nbsp&nbsp
                   
                <asp:DropDownList ID="ddlTypeClass" Width="100px" Height="20px" runat="server" OnSelectedIndexChanged="ddlTypeClass_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Value="0,4">Regular</asp:ListItem>
                    <asp:ListItem Value="1">Remedial</asp:ListItem>
                    <asp:ListItem Value="2">GD/PD Class</asp:ListItem>
                    <asp:ListItem Value="4">Extra Class</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Font-Size="13px" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="ddlAcademicYear" ValidationGroup="g1" ErrorMessage="Please select the Academic Year!"></asp:RequiredFieldValidator>

                <div class="export-button-container">
                    <asp:Button
                        ID="btnExport"
                        runat="server"
                        Text="Export To Excel"
                        CssClass="btn-export-excel"
                        Visible="false"
                        OnClick="ExportToExcel" />
                </div>



                <id id="divImage" style="display: none;" class="fa fa-spinner fa-pulse fa-2x fa-fw"></id>
                <span class="sr-only">Loading...</span>



            </fieldset>
            <fieldset class="boxBodyHeader">
            </fieldset>
            <fieldset class="boxBodyInner attendance-page-content">
                <center>
                    <br />
  <div class="chk-row">
    <asp:CheckBox ID="Chkdetained" runat="server"
        CssClass="chk-pill"
        AutoPostBack="true"
        OnCheckedChanged="Chkdetained_CheckedChanged"
        Text="Detained/Supplementary" />

    <asp:CheckBox ID="chkOpen" runat="server"
        CssClass="chk-pill"
        AutoPostBack="true"
        OnCheckedChanged="chkOpen_CheckedChanged"
        Text="Open Elective" />
</div>

                    <br />
                    <br />
                    <div class="pull-left" style="display: none">
                        <asp:CheckBox runat="server" ID="chkboxPrinciple" AutoPostBack="true" Font-Bold="true" Visible="false" Text="As Principal" OnCheckedChanged="chkboxPrinciple_CheckedChanged" />

                    </div>
                    <div class="clearfix"></div>
                    <table class="attendance-filter-table" width="95%">
                        <tr style="height: 10px; background-color: rgba(0, 0, 0, 0.06);">
                            <td colspan="17"></td>
                        </tr>
                        <tr style="height: 10px; background-color: rgba(0, 0, 0, 0.06);">
                            <td style="width: 10px"></td>
                            <td>Course:</td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:DropDownList ID="drpCourse" runat="server" AutoPostBack="true" Width="150px" Height="20px" BackColor="#e8e8e8" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged"></asp:DropDownList></td>
                            <td style="width: 10px"></td>
                            <td>Semester/Year:</td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:DropDownList ID="drpSemester" runat="server" AutoPostBack="true" Width="150px" Height="20px" BackColor="#e8e8e8" OnSelectedIndexChanged="drpSemester_SelectedIndexChanged"></asp:DropDownList></td>
                            <td style="width: 10px"></td>
                            <td>Section:</td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:DropDownList ID="drpSection" runat="server" AutoPostBack="true" Width="150px" Height="20px" BackColor="#e8e8e8" OnSelectedIndexChanged="drpSection_SelectedIndexChanged"></asp:DropDownList></td>
                            <td style="width: 10px"></td>
                            <td>Subject:</td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:DropDownList ID="drpSubject" runat="server" Width="150px" Height="20px" BackColor="#e8e8e8"></asp:DropDownList></td>
                            <td style="width: 70px"></td>
                        </tr>

                        <tr style="height: 10px; background-color: rgba(0, 0, 0, 0.06);">
                            <td colspan="17"></td>
                        </tr>
                        <tr style="background-color: rgba(0, 0, 0, 0.06);">
                            <td></td>
                            <td>Group:</td>
                            <td></td>
                            <td>
                                <asp:DropDownList ID="ddlGroup" Width="150px" Height="20px" BackColor="#e8e8e8" runat="server"></asp:DropDownList>

                            </td>
                            <td></td>
                            <td>Batch:</td>
                            <td></td>
                            <td>
                                <asp:DropDownList ID="ddlBatch" Width="150px" Height="20px" BackColor="#e8e8e8" runat="server"></asp:DropDownList>

                            </td>
                            <td></td>
                            <td>Date: </td>
                            <td></td>
                            <td>
                                <asp:TextBox ID="txtDateFrom" BackColor="#e8e8e8" Width="120px" Height="25px" runat="server" onkeypress="return false" placeholder="From" onKeyDown="preventBackspace();"></asp:TextBox>
                                <asp:Image src="../Images/Calendar.jpg" runat="server" Height="25px" Width="25px" alt="Calendar" ID="fdate" CssClass="calendar-icon" />
                                <asp:CalendarExtender ID="CalendarExtender2"
                                    runat="server"
                                    Format="dd MMM yyyy"
                                    CssClass="cal_Theme1"
                                    PopupButtonID="fdate"
                                    TargetControlID="txtDateFrom"
                                    Enabled="true"
                                    OnClientDateSelectionChanged="checkDate">
                                </asp:CalendarExtender>
                            </td>
                            <td style="display: none"></td>
                            <td style="display: none">To</td>
                            <td style="display: none"></td>
                            <td style="display: none">
                                <asp:TextBox ID="txtDateTo" Width="120px" BackColor="#e8e8e8" Height="25px" runat="server" placeholder="To" onkeypress="return false" onKeyDown="preventBackspace();"></asp:TextBox>
                                <asp:Image src="../Images/Calendar.png" runat="server" Height="25px" Width="25px" alt="" ID="fdate1" />
                                <asp:CalendarExtender ID="CalendarExtender1" Format="dd MMM yyyy" runat="server"
                                    CssClass="cal_Theme1" PopupButtonID="fdate1" OnClientDateSelectionChanged="checkDate" Enabled="true" TargetControlID="txtDateTo">
                                </asp:CalendarExtender>
                            </td>

                            <td>
                                <div style="display: flex; justify-content: flex-end; align-items: center; gap: 8px; width: 100%;">

                                    <div class="attendance-btn-right">
                                        <asp:LinkButton ID="btnShow"
                                            runat="server"
                                            CssClass="btn btn-info btn-sm attendance-btn"
                                            OnClick="btnShow_Click">
                                        <span class="glyphicon glyphicon-upload"></span> Show
                                        </asp:LinkButton>

                                        <asp:LinkButton ID="btnShow1"
                                            CssClass="btn btn-info btn-sm attendance-btn"
                                            runat="server"
                                            Visible="false"
                                            OnClick="btnShow1_Click">
                                       <span class="glyphicon glyphicon-upload"></span> Show
                                        </asp:LinkButton>

                                        <asp:LinkButton ID="btn_EC"
                                            runat="server"
                                            Visible="false"
                                            CssClass="btn btn-warning btn-sm attendance-btn"
                                            OnClick="btn_EC_Click">
                                        <span class="glyphicon glyphicon-upload"></span> EC ATT
                                        </asp:LinkButton>


                                    </div>
                                </div>
                            </td>

                        </tr>

                        <tr style="height: 10px">
                            <td colspan="17"></td>
                        </tr>
                        <tr>
                            <td colspan="17">
                                <asp:HiddenField ID="hfR_FromDate" runat="server" />
                                <asp:HiddenField ID="hfR_ToDate" runat="server" />
                                <asp:HiddenField ID="hfR_Session" runat="server" />
                                <asp:HiddenField ID="hfR_Course" runat="server" />
                                <asp:HiddenField ID="hfR_SemYear" runat="server" />
                                <asp:HiddenField ID="hfR_Section" runat="server" />
                                <asp:HiddenField ID="hfR_Subject" runat="server" />
                                <asp:HiddenField ID="hfR_Group" runat="server" />
                                <asp:HiddenField ID="hfR_Batch" runat="server" />
                            </td>
                        </tr>
                        <tr style="height: 10px">
                            <td colspan="17"></td>
                        </tr>
                        <tr>

                            <td colspan="17" align="center">
                                <div class="attendance-grid-scroll">
                                    <asp:GridView ID="grdStudentAttendanceEdit" runat="server" Font-Size="20px" AutoGenerateColumns="False" CssClass="MyClass" BackColor="White" BorderColor="#E7E7FF"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Horizontal"
                                        EmptyDataText="There are no data records to display." OnRowDataBound="grdStudentAttendanceEdit_RowDataBound">
                                        <AlternatingRowStyle BackColor="#F7F7F7" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl. No." ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Academic Year" SortExpression="ApplicantName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAcademicYear" runat="server" Text='<%# Eval("[AcademicYear]") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="visible-lg" />
                                                <ItemStyle CssClass="visible-lg" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Course" SortExpression="ApplicantName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("[Course]") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="visible-lg" />
                                                <ItemStyle CssClass="visible-lg" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Subject1" SortExpression="ApplicantName" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSubjectNo" runat="server" Text='<%# Eval("[SubjectNo]") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="visible-lg" />
                                                <ItemStyle CssClass="visible-lg" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Subject" SortExpression="ApplicantName">
                                               <ItemTemplate>
                                                   <asp:Label ID="lblSubjectName" runat="server" Text='<%# Eval("[SubjectName]") %>'></asp:Label>
                                               </ItemTemplate>
                                               <HeaderStyle CssClass="visible-lg" />
                                               <ItemStyle CssClass="visible-lg" />
                                           </asp:TemplateField>

                                            <asp:TemplateField HeaderText="College" SortExpression="ApplicantName" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCollege" runat="server" Text='<%# Eval("[College]") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="visible-lg" />
                                                <ItemStyle CssClass="visible-lg" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Date" SortExpression="ApplicantName" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAttendance_Date" runat="server" Text='<%# Eval("[Attendance_Date]") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="visible-lg" />
                                                <ItemStyle CssClass="visible-lg" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Semester" SortExpression="ApplicantName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("[Semester]") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="visible-lg" />
                                                <ItemStyle CssClass="visible-lg" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Faculty Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemarks" runat="server"
                                                        Text='<%# Eval("Remarks") %>'
                                                        CssClass="remarks-label"
                                                        onclick="showRemarks(this);">
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="visible-lg" />
                                                <ItemStyle CssClass="visible-lg" Width="200px" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="EC Status" SortExpression="ApplicantName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblECA1" runat="server" Text='<%# Eval("[Attendance]") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="visible-lg" />
                                                <ItemStyle CssClass="visible-lg" HorizontalAlign="Center" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Student Details" SortExpression="ApplicantName">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnClick"
                                                        runat="server"
                                                        Text="View Details"
                                                        CausesValidation="false"
                                                        OnClick="btnClick_Click">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="visible-lg" />
                                                <ItemStyle CssClass="visible-lg" HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Attachment">
                                                <ItemTemplate>
                                                    <asp:LinkButton
                                                        ID="lnkAttachment"
                                                        runat="server"
                                                        Text="View"
                                                        CssClass="attachment-link"
                                                        CausesValidation="false"
                                                        OnClientClick="openAttachment(this.getAttribute('data-url')); return false;"
                                                        data-url='<%# ResolveUrl(Eval("Attachment").ToString()) %>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                 <HeaderStyle CssClass="visible-lg" />
                                                <ItemStyle CssClass="visible-lg" HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <%-- <asp:TemplateField HeaderText="Attachment" SortExpression="ApplicantName">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnAttachment" runat="server" Text='<%# Eval("Attachment") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="visible-lg" />
                                                <ItemStyle CssClass="visible-lg" />
                                            </asp:TemplateField>--%>


                                            <asp:TemplateField HeaderText="EC ATT" SortExpression="EC ATT" Visible="false">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkEC_ATT" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="visible-lg" />
                                                <ItemStyle CssClass="visible-lg" HorizontalAlign="Center" />
                                            </asp:TemplateField>



                                        </Columns>
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                        <HeaderStyle BackColor="LightGray" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" Font-Size="Large" Height="40px" VerticalAlign="Bottom" />
                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                        <RowStyle ForeColor="#4A3C8C" Font-Bold="true" Font-Size="Medium" BorderStyle="Solid" BorderColor="Black" />
                                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                        <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                        <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                    </asp:GridView>
                                    
                                 <div id="remarksPopup" class="remarks-popup">
                                     <span class="close" onclick="hideRemarks()">&times;</span>
                                     <strong>Faculty Remarks</strong>
                                     <hr />
                                     <div id="remarksPopupText"></div>
                                 </div>

                                </div>


                                <asp:Panel ID="pnlGridViewDetails" CssClass="modalPopup" runat="server" Style="display: none;">
                                    <%--Add other controls here--%>
                                    <div class="header">
                                        <b>
                                            <asp:Label ID="lblNotification" runat="server" Text="Attendance Detail"></asp:Label></b><div class="close">
                                                <asp:Button ID="btnclose" OnClick="btnclose_Click" runat="server" Text="X" />
                                            </div>
                                    </div>
                                    <div id="Div1" runat="server" style="max-height: 500px; overflow: auto;">

                                        <div class="body">
                                            <div style="width: 100%">
                                                <center>
                                                    <asp:Label ID="lblStudent" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    
                                                    <asp:Label ID="lblCourse" runat="server" Text="Label"></asp:Label>
                                                    <br />
                                                    <asp:GridView ID="grdAttandanceDetails" Width="1050px" EmptyDataText="There are no data records to display." runat="server">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="7%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                        <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont1" />
                                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                                        <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont1" />
                                                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                        <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                                        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                                        <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                                        <SortedDescendingHeaderStyle BackColor="#3E3277" />

                                                    </asp:GridView>
                                                </center>
                                            </div>

                                        </div>

                                    </div>
                                </asp:Panel>
                                <asp:Button ID="btnDummy" runat="server" Style="display: none;" />
                                <asp:ModalPopupExtender ID="GridViewDetails" runat="server" TargetControlID="btnDummy"
                                    PopupControlID="pnlGridViewDetails" BackgroundCssClass="modalBackground" />

                            </td>

                        </tr>
                        <tr style="height: 10px">
                            <td colspan="17"></td>
                        </tr>
                        <%--  <tr>
                            <td colspan="15"></td>
                            <td>
                                <asp:Button ID="btnExport" runat="server" Text="Export To Excel" Visible="false" OnClick="ExportToExcel" />
                            </td>
                        </tr>--%>

                        <tr style="height: 10px">
                            <td colspan="17"></td>
                        </tr>
                    </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>

    <%--    <%2nd %>--%>


    <asp:UpdatePanel
        ID="up_StudentListEC"
        runat="server"
        UpdateMode="Conditional">

        <ContentTemplate>

            <asp:Button
                ID="Button1"
                runat="server"
                Style="display: none;"
                Text="Open Student Popup"
                CausesValidation="false" />

            <asp:ModalPopupExtender
                ID="md_Student_EC_Details"
                runat="server"
                PopupControlID="pnl_md_Student_EC"
                TargetControlID="Button1"
                BackgroundCssClass="modalBackground"
                CancelControlID="btn_Close_Student"
                DropShadow="true">
            </asp:ModalPopupExtender>

            <asp:Panel
                ID="pnl_md_Student_EC"
                runat="server"
                CssClass="mentee-modal"
                Style="display: none;">

                <div class="mentee-modal-header">

                    <span class="mentee-modal-title">Student Details
                    </span>

                    <asp:LinkButton
                        ID="btn_Close_Student"
                        runat="server"
                        CssClass="mentee-modal-close"
                        ToolTip="Close"
                        CausesValidation="false">
                    ×
                    </asp:LinkButton>

                    <div style="clear: both;"></div>

                </div>

               <div class="faculty-info">
    <div class="faculty-item">
        <span class="faculty-title">Faculty ID</span>
        <asp:Label ID="lblFacultyID1" runat="server"
            CssClass="faculty-value"></asp:Label>
    </div>

    <div class="faculty-divider"></div>

    <div class="faculty-item">
        <span class="faculty-title">Faculty Name</span>
        <asp:Label ID="lblFacultyName1" runat="server"
            CssClass="faculty-value"></asp:Label>
    </div>
</div>

                <div class="mentee-grid-wrapper">

                    <asp:GridView
                        ID="md_grd_CE"
                        runat="server"
                        CssClass="mentee-grid"
                        AutoGenerateColumns="false">

                        <Columns>

                            <asp:TemplateField HeaderText="Sl. No." ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle Width="7%" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Student Code"
                                ItemStyle-Width="14%"
                                HeaderStyle-Width="14%">
                                <ItemTemplate>
                                    <asp:Label ID="lblStudent" runat="server"
                                        CssClass="student-code"
                                        Text='<%# Eval("StudentCode") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Student Name"
                                ItemStyle-Width="19%"
                                HeaderStyle-Width="19%">
                                <ItemTemplate>
                                    <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("StudentName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Attendance"
                                ItemStyle-Width="10%"
                                HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblAbsent" runat="server" Text='<%# Eval("Attendance1") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                        </Columns>

                        <EmptyDataTemplate>
                            <div style="color: red;">
                                No Records Found !!!
                            </div>
                        </EmptyDataTemplate>

                    </asp:GridView>

                </div>



               <div class="ec-form-row">

                 <div class="ec-half" style="margin-left:auto;">
                     <label class="ec-label">✎ Remarks</label>
                     <asp:TextBox ID="txtRemarks" runat="server"
                         TextMode="MultiLine" Rows="4"
                         CssClass="ec-remarks"
                         placeholder="Enter your remarks...">
                     </asp:TextBox>
                 </div>
             
                 <div class="ec-half" style="display:none">
                     <label class="ec-label">📎 Attach Document</label>
                     <div class="ec-upload-box">
                         <asp:FileUpload ID="fuAttachment" runat="server"
                             CssClass="ec-file" />
                         <span class="ec-upload-hint">Select supporting document</span>
                     </div>
                 </div>
             
             </div>



                <div class="mentee-modal-toolbar">

                    <asp:Label
                        ID="lbl_md_txt"
                        runat="server"
                        CssClass="mentee-total"
                        Font-Bold="true">
                    </asp:Label>

                    <asp:LinkButton
                        ID="btn_Reassign"
                        runat="server"
                        CssClass="ec-approve-btn"
                        OnClick="btn_Reassign_Click">
                        Approved
                    </asp:LinkButton>

                    <%-- <asp:LinkButton
                        ID="btn_Excel_Export"
                        runat="server"
                        CssClass="mentee-export">
                    Export in Excel
                    </asp:LinkButton>--%>

                    <div style="clear: both;"></div>

                </div>


            </asp:Panel>

        </ContentTemplate>

        <Triggers>

            <asp:PostBackTrigger ControlID="btn_Reassign" />




        </Triggers>

    </asp:UpdatePanel>

    <div id="attachmentModal" class="attachment-modal"
        onclick="closeAttachment(event)">

        <div class="attachment-box">
            <span class="attachment-close"
                onclick="hideAttachment()">&times;</span>

            <div class="attachment-content">
                <img id="attachmentImage"
                    src=""
                    alt="Attachment"
                    onclick="enlargeImage(this)"
                    style="display: none;" />

                <iframe id="attachmentPdf"
                    style="display: none;"
                    title="PDF Attachment"></iframe>
            </div>
        </div>
    </div>



    </center>

        </fieldset>
</asp:Content>


