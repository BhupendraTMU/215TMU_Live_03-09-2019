<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master"
    AutoEventWireup="true"
    CodeFile="OnlineResultshow.aspx.cs"
    Inherits="Student_OnlineResultshow" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms"
    TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="asp" %>


<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="Server">

    <style type="text/css">
        /* =====================================================
           MAIN REPORT CONTAINER
           ===================================================== */

        .report-container {
            width: 100% !important;
            max-width: 1400px;
            margin: 15px auto !important;
            padding: 0 !important;
            border: none !important;
            box-sizing: border-box;
        }


        /* =====================================================
           CARD
           ===================================================== */

        .report-card {
            width: 100%;
            background: #ffffff;
            border: 1px solid #dee2e6;
            border-radius: 8px;
            padding: 15px;
            box-sizing: border-box;
        }


        /* =====================================================
           FILTER SECTION
           ===================================================== */

        .filter-section {
            width: 100%;
            background: #f8f9fa;
            border: 1px solid #dee2e6;
            border-radius: 8px;
            padding: 15px 20px;
            margin-bottom: 18px;
            box-sizing: border-box;
        }


            /* =====================================================
           LABEL
           ===================================================== */

            .filter-section .form-label {
                display: block;
                color: #343a40;
                margin-bottom: 6px;
                font-size: 14px;
                font-weight: 600;
            }


            /* =====================================================
           DROPDOWN
           ===================================================== */

            .filter-section .form-select {
                width: 120px !important;
                min-width: 120px !important;
                height: 40px !important;
                padding: 6px 30px 6px 10px !important;
                font-size: 14px !important;
                color: #343a40;
                background-color: #ffffff !important;
                border: 1px solid #ced4da !important;
                border-radius: 6px !important;
                box-shadow: none !important;
                box-sizing: border-box;
            }


                .filter-section .form-select:focus {
                    border-color: #80bdff !important;
                    box-shadow: 0 0 0 0.15rem rgba(0,123,255,.12) !important;
                }


        /* Academic Year */

        .academic-dropdown {
            width: 180px !important;
            min-width: 180px !important;
        }


        /* =====================================================
           VIEW BUTTON
           ===================================================== */

        .btn-view {
            width: 120px !important;
            height: 40px !important;
            padding: 0 15px !important;
            border: none !important;
            border-radius: 6px !important;
            background: #2878c8 !important;
            color: #ffffff !important;
            font-size: 14px !important;
            font-weight: 600 !important;
            white-space: nowrap;
            box-shadow: 0 1px 2px rgba(0,0,0,.10);
            transition: all .2s ease;
        }


            .btn-view:hover {
                background: #1769aa !important;
                color: #ffffff !important;
            }


        /* =====================================================
           REPORT WRAPPER
           ===================================================== */

        .report-viewer-wrapper {
            width: 100%;
            margin-top: 5px;
            background: #ffffff;
            border: 1px solid #dee2e6;
            border-radius: 8px;
            overflow: hidden;
            box-sizing: border-box;
        }


        /* =====================================================
           REPORT HEADER
           ===================================================== */

        .report-header {
            width: 100%;
            min-height: 43px;
            display: flex;
            align-items: center;
            padding: 0 20px;
            background: #f1f3f5;
            border-bottom: 1px solid #dee2e6;
            color: #343a40;
            font-size: 15px;
            font-weight: 600;
            box-sizing: border-box;
        }


            .report-header i {
                margin-right: 8px;
            }


        /* =====================================================
           REPORT VIEWER AREA
           ===================================================== */

        .report-viewer {
            width: 100%;
            min-height: 800px;
            padding: 8px;
            background: #ffffff;
            overflow-x: auto;
            overflow-y: auto;
            box-sizing: border-box;
        }


        /* =====================================================
           REPORT VIEWER CONTROL
           ===================================================== */

        .my-report-viewer {
            width: 100% !important;
            height: 800px !important;
            display: block;
            border: none !important;
            box-sizing: border-box;
        }


            /*
            Important:
            ReportViewer generated content should not be
            constrained by parent max-width.
        */

            .my-report-viewer table {
                max-width: none !important;
            }


            .my-report-viewer iframe {
                width: 100% !important;
            }


        /* =====================================================
           MESSAGE
           ===================================================== */

        .report-message {
            display: block;
            margin: 10px;
            padding: 10px 15px;
            border-radius: 5px;
            background: #fff3cd;
            color: #856404;
            border: 1px solid #ffeeba;
        }


        /* =====================================================
           REMOVE EXTRA SPACING
           ===================================================== */

        .report-card .row {
            margin-bottom: 0 !important;
        }


        /* =====================================================
           MOBILE
           ===================================================== */

        @media (max-width: 767px) {

            .report-container {
                width: 100% !important;
                margin: 10px auto !important;
                padding: 0 5px !important;
            }


            .report-card {
                padding: 8px;
                border-radius: 6px;
            }


            .filter-section {
                padding: 12px;
                margin-bottom: 12px;
            }


                .filter-section .form-select {
                    width: 100% !important;
                    min-width: 100% !important;
                }


            .academic-dropdown {
                width: 100% !important;
                min-width: 100% !important;
            }


            .btn-view {
                width: 100% !important;
            }


            .report-header {
                padding: 0 12px;
                font-size: 14px;
            }


            .report-viewer {
                min-height: 650px;
                padding: 3px;
                overflow-x: auto;
            }


            .my-report-viewer {
                height: 650px !important;
                min-width: 850px !important;
            }
        }


        /* =====================================================
           VERY SMALL MOBILE
           ===================================================== */

        @media (max-width: 480px) {

            .report-container {
                padding: 0 3px !important;
            }


            .report-card {
                padding: 5px;
            }


            .filter-section {
                padding: 10px;
            }


            .my-report-viewer {
                min-width: 800px !important;
            }
        }
    </style>

</asp:Content>


<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="Server">


    <!-- =====================================================
         SCRIPT MANAGER
         ===================================================== -->

    <asp:ScriptManager
        ID="ScriptManager1"
        runat="server">
    </asp:ScriptManager>


    <!-- =====================================================
         MESSAGE PANEL
         ===================================================== -->

    <asp:Panel
        ID="pnlmessag"
        runat="server"
        Visible="false">

        <asp:TextBox
            ID="txtmsg"
            runat="server"
            TextMode="MultiLine">
        </asp:TextBox>


        <asp:Button
            ID="btnSendmsg"
            runat="server"
            Text="Send"
            OnClick="btnSendmsg_Click" />

    </asp:Panel>


    <!-- =====================================================
         MAIN PANEL
         ===================================================== -->

    <asp:Panel
        ID="Panel1"
        runat="server"
        Visible="false">


        <!-- =================================================
             UNAUTHORIZED MESSAGE
             ================================================= -->

        <asp:Label
            ID="msg"
            runat="server"
            Text="You are not authorized to access this page"
            Font-Bold="true"
            Visible="false">
        </asp:Label>


        <!-- =================================================
             MAIN REPORT CONTAINER
             ================================================= -->

        <fieldset
            id="Fieldset1"
            class="boxBodyInner report-container"
            runat="server">


            <div class="report-card">
                <div class="filter-section">

                    <div class="row g-3 align-items-end">

                        <!-- RESULT TYPE -->
                        <div class="col-lg-2 col-md-6 col-sm-6">

                            <label class="form-label">
                                Result Type
                            </label>

                            <asp:DropDownList
                                ID="ddlResultType"
                                runat="server"
                                CssClass="form-select"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="ddlResultType_SelectedIndexChanged">


                                <asp:ListItem Text="Semester" Value="SEM" />
                                <asp:ListItem Text="Year" Value="YEAR" />

                            </asp:DropDownList>

                        </div>


                        <!-- ================= SEMESTER SECTION ================= -->

                        <div id="divsem"
                            runat="server"
                            class="col-lg-2 col-md-6 col-sm-6">

                            <label id="lblsem"
                                runat="server"
                                class="form-label">
                                Sem / Year
                            </label>

                            <asp:DropDownList
                                ID="ddlSem"
                                runat="server"
                                CssClass="form-select">
                            </asp:DropDownList>

                        </div>


                        <!-- ================= YEAR SECTION ================= -->

                        <div id="divYear"
                            runat="server"
                            visible="false"
                            class="col-lg-2 col-md-6 col-sm-6">

                            <label id="lblyear"
                                runat="server"
                                class="form-label">
                                Year
                            </label>

                            <asp:DropDownList
                                ID="drpYear"
                                runat="server"
                                CssClass="form-select">

                                <asp:ListItem Value="YEAR 1" Text="YEAR 1" />
                                <asp:ListItem Value="YEAR 2" Text="YEAR 2" />
                                <asp:ListItem Value="YEAR 3" Text="YEAR 3" />
                                <asp:ListItem Value="YEAR 4" Text="YEAR 4" />
                                <asp:ListItem Value="YEAR 5" Text="YEAR 5" />

                            </asp:DropDownList>

                        </div>


                        <!-- ================= EXAM TYPE ================= -->

                        <div id="divExam"
                            runat="server"
                            class="col-lg-2 col-md-6 col-sm-6">

                            <label id="lblExam"
                                runat="server"
                                class="form-label">
                                Exam Type
                            </label>

                            <asp:DropDownList
                                ID="drpExam"
                                runat="server"
                                CssClass="form-select"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="drpExam_SelectedIndexChanged">

                                <asp:ListItem Text="Main" Value="0" />
                                <asp:ListItem Text="Re-Appear" Value="1" />

                            </asp:DropDownList>

                        </div>


                        <!-- ================= ACADEMIC YEAR ================= -->

                        <div id="Academic"
                            runat="server"
                            class="col-lg-2 col-md-6 col-sm-6" visible="false">

                            <label id="lblAcademic"
                                runat="server"
                                class="form-label">
                                Academic Year

                            </label>

                            <asp:DropDownList
                                ID="drpAcademic"
                                runat="server"
                                CssClass="form-select academic-dropdown">
                            </asp:DropDownList>

                        </div>


                        <!-- ================= VIEW REPORT ================= -->

                        <div class="col-lg-2 col-md-6 col-sm-6">

                            <asp:Button
                                ID="btnView"
                                runat="server"
                                Text="View Report"
                                CssClass="btn btn-primary btn-view"
                                OnClick="btnView_Click" />

                        </div>

                    </div>

                </div>




                <div class="report-viewer-wrapper">


                    <!-- REPORT HEADER -->

                    <div class="report-header">

                        <span>

                            <i class="fa fa-file-alt"></i>

                            Examination Report

                        </span>

                    </div>


                    <!-- REPORT CONTENT -->

                    <div class="report-viewer">


                        <rsweb:ReportViewer
                            ID="ReportViewer1"
                            runat="server"
                            Width="100%"
                            Height="800px"
                            AsyncRendering="false"
                            SizeToReportContent="false"
                            ShowParameterPrompts="false"
                            ShowPrintButton="true"
                            ShowExportControls="true"
                            ShowZoomControl="true"
                            ZoomMode="PageWidth"
                            CssClass="my-report-viewer"
                            BorderStyle="None">
                        </rsweb:ReportViewer>
                        <iframe
                            id="pdfViewer" visible="false"
                            runat="server"
                            style="width: 100%; height: 800px; border: 1px solid #ddd; border-radius: 6px;"></iframe>


                        <!-- MESSAGE -->

                        <asp:Label
                            ID="lblmsg"
                            runat="server"
                            Visible="false"
                            CssClass="report-message">

                        </asp:Label>


                    </div>


                </div>


            </div>


        </fieldset>


    </asp:Panel>


</asp:Content>
