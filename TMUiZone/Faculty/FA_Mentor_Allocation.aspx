<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="FA_Mentor_Allocation.aspx.cs" EnableEventValidation="false" Inherits="FA_Mentor_Allocation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="js/jquery.min.js"></script>

    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "PMS%20Img/minus.png");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "PMS%20Img/plus.png");
            $(this).closest("tr").next().remove();
        });
        function PopupShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 99999999;
        }
        function confirmAction() {
            return confirm("Are you sure you want to proceed?");
        }

    </script>
    <script type="text/javascript">
        function SingleCheck(chk) {
            // alert("Function Called"); // Test

            var gv = document.getElementById('<%= grd_Menter_details_popup.ClientID %>');
            var checkBoxes = gv.getElementsByTagName("input");

            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "checkbox" && checkBoxes[i] != chk) {
                    checkBoxes[i].checked = false;
                }
            }
        }
    </script>


    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 630px;
            border: 3px solid #0DA9D0;
            border-radius: 12px;
            padding: 0
        }

            .modalPopup .header {
                background-color: #2FBDF1;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
            }

            .modalPopup .body {
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .footer {
                padding: 6px;
            }

            .modalPopup .yes, .modalPopup .no {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                border-radius: 4px;
            }

            .modalPopup .yes {
                background-color: #2FBDF1;
                border: 1px solid #0DA9D0;
            }

            .modalPopup .no {
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }

        /* General table style */
        .gridview {
            width: 100%;
            border-collapse: collapse;
            font-family: Arial, sans-serif;
        }

            /* Header styling */
            .gridview th {
                background-color: #ed7600; /* Oceanic Blue */
                color: white;
                font-size: 13px;
                padding: 12px;
                text-align: left;
                border: 1px solid #dddddd;
            }

            /* Row styling */
            .gridview td {
                padding: 10px;
                border: 1px solid #dddddd;
                text-align: left;
                font-size: 12px;
                color: #333;
            }

            /* Alternating row colors */
            .gridview tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            /* Hover effect */
            .gridview tr:hover {
                background-color: #d9edf7;
            }

        /* Responsive design */
        @media screen and (max-width: 768px) {
            .gridview, .gridview thead, .gridview tbody, .gridview th, .gridview td, .gridview tr {
                display: block;
                width: 100%;
            }

                .gridview th, .gridview td {
                    box-sizing: border-box;
                    text-align: right;
                    padding: 12px 8px;
                }

                .gridview td {
                    border: none;
                    border-bottom: 1px solid #dddddd;
                    text-align: right;
                }

                .gridview tr {
                    margin-bottom: 12px;
                    display: block;
                }

                .gridview thead {
                    display: none;
                }

                .gridview td:before {
                    content: attr(data-label);
                    float: left;
                    font-weight: bold;
                    color: #007bff;
                }
        }

        /* General textbox styling */
        .textbox {
            width: 100%;
            padding: 5px 7px;
            border: 1px solid #ccc;
            border-radius: 4px;
            font-size: 13px;
            color: #333;
            box-sizing: border-box;
            transition: border-color 0.3s ease-in-out, box-shadow 0.3s ease-in-out;
            height: 30px;
        }

            /* Focus effect */
            .textbox:focus {
                border-color: #007bff; /* Focused border color */
                box-shadow: 0 0 5px rgba(0, 123, 255, 0.5);
                outline: none;
            }

            /* Disabled state */
            .textbox:disabled {
                background-color: #f2f2f2;
                color: #999;
            }

            /* Textbox with an error state */
            .textbox.error {
                border-color: #e74c3c; /* Red border for errors */
                box-shadow: 0 0 5px rgba(231, 76, 60, 0.5);
            }

            /* Placeholder styling */
            .textbox::placeholder {
                color: #999;
                font-style: italic;
            }

        /* Responsive design */
        @media (max-width: 768px) {
            .textbox {
                font-size: 12px;
                padding: 8px 12px;
            }
        }
        /* Base button styling */
        .button {
            padding: 10px 20px;
            font-size: 13px;
            color: white;
            background-color: #007bff; /* Primary blue color */
            border: none;
            border-radius: 4px;
            cursor: pointer;
            transition: background-color 0.3s ease, box-shadow 0.3s ease;
            box-shadow: 0 4px 6px rgba(0, 123, 255, 0.2);
        }

            /* Hover effect */
            .button:hover {
                background-color: #0056b3; /* Darker blue on hover */
                box-shadow: 0 6px 8px rgba(0, 123, 255, 0.3);
            }

            /* Active state */
            .button:active {
                background-color: #004085; /* Even darker blue on click */
                box-shadow: 0 3px 5px rgba(0, 123, 255, 0.2);
                transform: translateY(1px); /* Slight movement on click */
            }

            /* Disabled state */
            .button:disabled {
                background-color: #cccccc; /* Gray color for disabled button */
                cursor: not-allowed;
                box-shadow: none;
            }

            /* Secondary button */
            .button.secondary {
                background-color: #6c757d; /* Secondary gray color */
            }

                .button.secondary:hover {
                    background-color: #5a6268; /* Darker gray on hover */
                }

            /* Success button */
            .button.success {
                background-color: #28a745; /* Success green color */
            }

                .button.success:hover {
                    background-color: #218838; /* Darker green on hover */
                }

            /* Danger button */
            .button.danger {
                background-color: #dc3545; /* Danger red color */
            }

                .button.danger:hover {
                    background-color: #c82333; /* Darker red on hover */
                }

        /* Responsive design */
        @media (max-width: 768px) {
            .button {
                padding: 8px 15px;
                font-size: 12px;
            }
        }

        .cursor-pointer {
            cursor: pointer;
        }
    </style>
    <style type="text/css">
        /* Background overlay */
        .modalBackground {
            background-color: #000000;
            opacity: 0.55;
        }

        /* Main Popup */
        .mentorModalPopup {
            background-color: #ffffff;
            border-radius: 4px;
            width: 700px;
            max-width: 90%;
            box-shadow: 0 4px 15px rgba(0,0,0,0.4);
            overflow: hidden;
        }

        /* Popup Header */
        .mentorModalHeader {
            background-color: #ed861f;
            color: white;
            height: 45px;
            line-height: 45px;
            padding-left: 15px;
            font-size: 14px;
            font-weight: bold;
        }

        /* Close X */
        .mentorModalClose {
            float: right;
            color: #ffffff !important;
            font-size: 24px;
            font-weight: bold;
            text-decoration: none !important;
            margin-right: 12px;
            line-height: 42px;
            cursor: pointer;
        }

            .mentorModalClose:hover {
                color: #eeeeee !important;
            }

        /* Popup Body */
        .mentorModalBody {
            padding: 15px;
            background-color: #ffffff;
        }

        /* Button Area */
        .mentorModalButtonArea {
            text-align: right;
            padding: 10px 15px 15px 15px;
        }

        /* GridView */
        .mentorGrid {
            width: 100%;
            border-collapse: collapse;
            font-size: 13px;
        }

            .mentorGrid th {
                background-color: #ed861f !important;
                color: #ffffff !important;
                font-weight: bold;
                padding: 8px;
                border: 1px solid #ddd;
                text-align: left;
            }

            .mentorGrid td {
                padding: 7px;
                border: 1px solid #ddd;
                color: #333333;
                background-color: #ffffff;
            }

            .mentorGrid tr:hover td {
                background-color: #f5f5f5;
            }

            /* Paging */
            .mentorGrid .pager {
                text-align: center;
                padding: 8px;
            }
    </style>
    <style type="text/css">
        .mentor-main-panel {
            width: 100%;
            background: #ffffff;
            border: 1px solid #ddd;
            border-radius: 4px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.10);
            overflow: hidden;
        }

        /* Orange Heading */
        .mentor-title {
            background-color: #ed861f;
            color: #ffffff;
            padding: 12px 15px;
            font-size: 18px;
            font-weight: bold;
        }

        .mentor-body {
            padding: 20px;
        }

        /* Filter area */
        .filter-box {
            background: #f7f7f7;
            border: 1px solid #ddd;
            padding: 15px;
            border-radius: 4px;
            margin-bottom: 15px;
        }

        .filter-row {
            display: flex;
            align-items: center;
            gap: 10px;
            flex-wrap: wrap;
        }

        .filter-label {
            font-weight: bold;
            color: #333;
            margin-right: 3px;
        }

        .filter-control {
            width: 200px;
            height: 34px;
            border: 1px solid #ccc;
            border-radius: 3px;
            padding: 5px 8px;
            box-sizing: border-box;
        }

        /* Buttons */
        .btn-get-record {
            margin-right: 8px;
        }

        /* Total counters */
        .count-area {
            text-align: right;
            padding: 8px 0 15px 0;
            font-weight: bold;
        }

        .count-link {
            margin-left: 5px;
            margin-right: 20px;
            font-size: 15px;
            text-decoration: none;
        }

        /* Student filter */
        .student-filter {
            background: #f7f7f7;
            border: 1px solid #ddd;
            padding: 12px 15px;
            margin-bottom: 10px;
            border-radius: 4px;
        }

            .student-filter label {
                font-weight: bold;
                margin-right: 8px;
            }

            .student-filter input {
                margin-right: 8px;
            }

        /* Grid */
        .mentee-grid-container {
            height: 500px;
            overflow: auto;
            border: 1px solid #ddd;
        }

        .mentor-grid {
            width: 100%;
            border-collapse: collapse;
            font-size: 13px;
        }

            .mentor-grid th {
                background-color: #ed861f !important;
                color: #ffffff !important;
                font-weight: bold;
                padding: 9px;
                border: 1px solid #d87817;
                white-space: nowrap;
            }

            .mentor-grid td {
                padding: 8px;
                border: 1px solid #ddd;
                background-color: #ffffff;
            }

            .mentor-grid tr:hover td {
                background-color: #fff4e8;
            }

        /* Assign button */
        .assign-button-area {
            padding-top: 15px;
            text-align: right;
        }
    </style>
    <style type="text/css">
        .filter-container {
            background: #f7f7f7;
            border: 1px solid #ddd;
            padding: 15px;
            border-radius: 4px;
        }

        .filter-row {
            display: flex;
            align-items: center;
            margin-bottom: 12px;
        }

        .filter-item {
            display: flex;
            align-items: center;
            width: 320px; /* reduce space between columns */
        }

        .filter-label {
            font-weight: bold;
            width: 125px; /* reduce label-to-box gap */
            white-space: nowrap;
        }

        .filter-control {
            width: 175px !important;
            height: 34px;
            border: 1px solid #ccc;
            border-radius: 3px;
            padding: 5px 8px;
            box-sizing: border-box;
        }
        /* Employee Code / Name */
        .filter-item:first-child .filter-label {
            width: 150px;
        }
    </style>
    <style type="text/css">
        /* Modal */
        .mentee-modal {
            width: 660px !important;
            height: 500px !important;
            background: #fff !important;
            padding: 0 !important;
            border: none !important;
            border-radius: 5px !important;
            overflow: hidden !important;
            box-shadow: 0 5px 20px rgba(0,0,0,0.4);
        }

        /* Orange Header */
        .mentee-modal-header {
            height: 45px;
            line-height: 45px;
            background: #f58a00;
            color: #fff;
            padding: 0 12px;
            font-size: 15px;
            font-weight: bold;
        }

        .mentee-modal-title {
            float: left;
        }

        /* Only X button */
        .mentee-modal-close {
            float: right;
            color: #fff !important;
            text-decoration: none !important;
            font-size: 24px;
            font-weight: bold;
            cursor: pointer;
            line-height: 43px;
        }

        /* Toolbar */
        .mentee-modal-toolbar {
            height: 48px;
            padding: 8px 10px;
            background: #fff;
            border-bottom: 1px solid #ddd;
            box-sizing: border-box;
        }

        .mentee-total {
            float: left;
            line-height: 30px;
            font-size: 12px;
            color: #333;
        }

        /* Export button */
        .mentee-export {
            float: right;
            background: #28a745 !important;
            color: #fff !important;
            padding: 7px 14px;
            border-radius: 3px;
            text-decoration: none !important;
            font-size: 11px;
        }

        /* Grid outer margin */
        .mentee-grid-wrapper {
            margin: 10px;
            height: 380px;
            overflow-y: auto;
            overflow-x: hidden;
        }

        /* Grid */
        .mentee-grid {
            width: 100% !important;
            table-layout: fixed !important;
            border-collapse: collapse !important;
            margin: 0 !important;
            font-size: 11px;
        }

            /* Grid Header */
            .mentee-grid th {
                background: #f58a00 !important;
                color: #fff !important;
                font-weight: bold !important;
                padding: 8px 6px !important;
                border: 1px solid #ddd !important;
                text-align: left !important;
            }

            /* Grid Cells */
            .mentee-grid td {
                background: #fff !important;
                color: #444 !important;
                padding: 7px 6px !important;
                border: 1px solid #ddd !important;
            }

            /* Alternate row */
            .mentee-grid tr:nth-child(even) td {
                background: #f8f8f8 !important;
            }

            /* Hover */
            .mentee-grid tr:hover td {
                background: #fff3df !important;
            }

        /* Modal background */
        .modalBackground {
            background-color: #000 !important;
            opacity: 0.55 !important;
        }

        .mentee-modal-toolbar {
            display: flex;
            align-items: center;
            gap: 15px;
        }

        .mentee-total {
            margin-right: auto;
        }

        /* ReAssign */
        .mentee-reassign {
            background-color: #ffc107 !important;
            color: #000 !important;
            padding: 8px 18px;
            border-radius: 5px;
            text-decoration: none !important;
        }


        /* Excel */
        .mentee-excel {
            background-color: #198754 !important;
            color: #000 !important;
            padding: 8px 18px;
            border-radius: 5px;
            text-decoration: none !important;
        }

        .mentee-reassign:hover {
            background-color: #e0a800 !important;
            color: #000 !important;
        }

        .mentee-excel:hover {
            background-color: #157347 !important;
        }
    </style>
        <script type="text/javascript">
            function confirmApproval() {
                if (confirm("Are you sure you want to change the mentor for this mentee?")) {
                    document.getElementById('<%= hdnApprovalConfirm.ClientID %>').value = "Yes";
                return true;
            }

            document.getElementById('<%= hdnApprovalConfirm.ClientID %>').value = "No";
                return false;
            }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container">

        <asp:Panel ID="pnl_Mentor" Visible="true" runat="server">
            <div class="mentor-main-panel">
                <div class="mentor-title">
                    Mentor To Mentee Allocation
                </div>

                <div class="mentor-body">
                    <div class="filter-container">
                        <div class="filter-row">
                            <div class="filter-item" style="display: none">
                                <span class="filter-label">Employee Code / Name</span>

                                <asp:TextBox
                                    ID="txt_filterby_name"
                                    runat="server"
                                    CssClass="filter-control"
                                    placeholder="Employee Code / Name">
                                </asp:TextBox>
                            </div>


                            <div class="filter-item">
                                <span class="filter-label">Program</span>

                                <asp:DropDownList
                                    ID="ddl_course"
                                    runat="server"
                                    CssClass="filter-control"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="ddl_course_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>


                            <div class="filter-item">
                                <span class="filter-label">Admitted Year</span>

                                <asp:DropDownList
                                    ID="dd_AcademicYear"
                                    runat="server"
                                    CssClass="filter-control"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="dd_AcademicYear_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>

                            <div class="filter-item">
                                <span class="filter-label">Semester</span>

                                <asp:DropDownList
                                    ID="dd_Semester"
                                    runat="server"
                                    CssClass="filter-control"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="dd_Semester_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>

                            <div class="filter-item">
                                <span class="filter-label">Section</span>

                                <asp:DropDownList
                                    ID="dd_Section"
                                    runat="server"
                                    CssClass="filter-control"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="dd_Section_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <div style="text-align: right; margin-top: 5px;">
                                <asp:Button
                                    ID="btn_filter"
                                    runat="server"
                                    CssClass="btn btn-danger text-uppercase"
                                    Style="margin-right: 10px;"
                                    Text="Get Record"
                                    OnClick="btn_filter_Click1" />

                                <asp:Button
                                    ID="btn_AssignMEntorList"
                                    runat="server"
                                    CssClass="btn btn-success text-uppercase"
                                    Text="Mentor List"
                                    OnClick="btn_AssignMEntorList_Click" />

                            </div>
                        </div>
                    </div>



                    <asp:UpdatePanel
                        ID="up_MenteeCount"
                        runat="server"
                        UpdateMode="Conditional">

                        <ContentTemplate>

                            <div class="count-area">
                                Total No. Of Mentee:

                <asp:LinkButton
                    ID="lnk_TotalMentee"
                    runat="server"
                    CssClass="count-link"
                    OnClick="lnk_TotalMentee_Click">

                    <asp:Label
                        ID="lbl_TotalMentee"
                        runat="server"
                        Text="Total">
                    </asp:Label>

                </asp:LinkButton>
                                Assigned Mentee:
                <asp:LinkButton
                    ID="lnl_UnassignedMentee"
                    runat="server"
                    CssClass="count-link"
                    OnClick="lnl_UnassignedMentee_Click">

                    <asp:Label
                        ID="lbl_UnassignedMentee"
                        runat="server"
                        Text="Pending">
                    </asp:Label>
                </asp:LinkButton>
                            </div>
                        </ContentTemplate>

                    </asp:UpdatePanel>




                    <asp:Panel ID="pnl_Mentee" runat="server">
                        <div class="student-filter">
                            <span class="filter-label">Filter by Student No / Enrollment No / Name
                            </span>
                            <asp:TextBox ID="txt_Studentfilterby_name"
                                runat="server"
                                CssClass="filter-control"
                                placeholder="Student No / Name">
                            </asp:TextBox>


                            <asp:Button
                                ID="btn_Studentfilter"
                                runat="server"
                                CssClass="btn btn-danger"
                                Text="Get Record"
                                OnClick="btn_Studentfilter_Click" />
                        </div>

                        <asp:UpdatePanel
                            ID="up_Mentee"
                            runat="server"
                            UpdateMode="Conditional">

                            <ContentTemplate>

                                <div class="mentee-grid-container">

                                    <asp:GridView
                                        ID="gv_Mentee"
                                        runat="server"
                                        CssClass="mentor-grid"
                                        AutoGenerateColumns="false"
                                        OnRowDataBound="gv_Mentee_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton
                                                        ID="lnk_remove"
                                                        runat="server"
                                                        Visible="false"
                                                        CssClass="btn btn-danger btn-sm"
                                                        CommandArgument='<%# Bind("[AutoNo]") %>'
                                                        OnCommand="lnk_remove_Command"
                                                        OnClientClick="return confirmAction();">
                                        Remove
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Student Code">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        ID="lblAllocated"
                                                        runat="server"
                                                        Text='<%# Bind("[Allocated]") %>'
                                                        Visible="false">
                                                    </asp:Label>
                                                    <asp:Label
                                                        ID="lbl_Academic_Year"
                                                        runat="server"
                                                        Text='<%# Bind("[Academic_Year]") %>'
                                                        Visible="false">
                                                    </asp:Label>
                                                    <asp:Label
                                                        ID="lbl_Course_Code"
                                                        runat="server"
                                                        Text='<%# Bind("[Course_Code]") %>'
                                                        Visible="false">
                                                    </asp:Label>
                                                    <asp:Label ID="lblMenterID" runat="server" Text='<%# Bind("[Mentor_ID]") %>' Visible="false"></asp:Label>

                                                    <asp:CheckBox ID="chk_Mentee" runat="server" />
                                                    <asp:Label ID="lbl_St_Code_grid" runat="server" Text='<%# Bind("No_") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Student Name">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        ID="lbl_St_Name_grid"
                                                        runat="server"
                                                        Text='<%# Bind("[Student Name]") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Date Of Birth">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        ID="lbl_St_DOB_grid"
                                                        runat="server"
                                                        Text='<%# Bind("[Date of Birth]", "{0:dd MMM yyyy}") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Father's Name">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        ID="lbl_St_Father_grd"
                                                        runat="server"
                                                        Text='<%# Bind("[Fathers Name]") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Mother's Name">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        ID="lbl_St_Mother_grd"
                                                        runat="server"
                                                        Text='<%# Bind("[Mothers Name]") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Mobile No">

                                                <ItemTemplate>

                                                    <asp:Label
                                                        ID="lbl_St_Mobile_grd"
                                                        runat="server"
                                                        Text='<%# Bind("[Mobile Number]") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <div style="color: red;">No Records Found !!!</div>
                                        </EmptyDataTemplate>
                                        <HeaderStyle
                                            BackColor="#ed861f"
                                            ForeColor="White"
                                            Font-Bold="True" />
                                        <RowStyle
                                            BackColor="White"
                                            ForeColor="#333333" />
                                        <AlternatingRowStyle
                                            BackColor="#fafafa" />
                                        <PagerStyle
                                            HorizontalAlign="Center"
                                            BackColor="#eeeeee"
                                            ForeColor="#333333" />
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>

                        </asp:UpdatePanel>
                        <!-- ================= ASSIGN BUTTON ================= -->
                        <%-- <div class="assign-button-area">
                            <asp:Button
                                ID="btn_assign_Mentee"
                                runat="server"
                                OnClick="btn_assign_Mentee_Click"
                                CssClass="btn btn-primary"
                                Text="Assign Mentor" />
                        </div>--%>
                    </asp:Panel>

                </div>

            </div>

        </asp:Panel>

        <asp:UpdatePanel
            ID="up_Employee_Count_Details"
            runat="server"
            UpdateMode="Conditional">

            <ContentTemplate>

                <asp:Button
                    ID="Button1"
                    runat="server"
                    Style="display: none;"
                    Text="Button" />

                <asp:ModalPopupExtender
                    ID="md_Employee_Count_Details"
                    runat="server"
                    PopupControlID="pnl_md_Mentee"
                    TargetControlID="Button1"
                    BackgroundCssClass="modalBackground"
                    OkControlID="btn_Close_Employee">
                </asp:ModalPopupExtender>



                <asp:Panel
                    ID="pnl_md_Mentee"
                    runat="server"
                    CssClass="mentee-modal"
                    Style="display: none;">


                    <div class="mentee-modal-header">

                        <span class="mentee-modal-title">Mentee Details
                        </span>


                        <asp:LinkButton
                            ID="btn_Close_Employee"
                            runat="server"
                            CssClass="mentee-modal-close"
                            ToolTip="Close">
            ×
                        </asp:LinkButton>
                        <div style="clear: both;"></div>
                    </div>

                    <div class="mentee-modal-toolbar">

                        <asp:Label
                            ID="lbl_md_txt"
                            runat="server"
                            CssClass="mentee-total"
                            Font-Bold="true"
                            Text="">
                        </asp:Label>

                        <asp:LinkButton
                            ID="btn_Reassign"
                            runat="server"
                            CssClass="mentee-export"
                            OnClick="btn_Reassign_Click"
                            OnClientClick="return confirmApproval();">
                         ReAssign
                        </asp:LinkButton>
                          <asp:HiddenField ID="hdnApprovalConfirm" runat="server" Value="No" />



                        <asp:LinkButton
                            ID="btn_Excel_Export"
                            runat="server"
                            CssClass="mentee-export"
                            OnClick="btn_Excel_Export_Click">
                           Export in Excel
                        </asp:LinkButton>
                        <div style="clear: both;"></div>
                    </div>

                    <div class="mentee-grid-wrapper">
                        <asp:GridView
                            ID="md_grd_Mentee"
                            runat="server"
                            CssClass="mentee-grid"
                            AutoGenerateColumns="false">

                            <Columns>
                                <asp:TemplateField
                                    HeaderText="Student Code"
                                    ItemStyle-Width="14%"
                                    HeaderStyle-Width="14%">

                                    <ItemTemplate>
                                        <asp:Label
                                            ID="lbl_St_Code_grid"
                                            runat="server"
                                            Text='<%# Bind("No_") %>'>
                                        </asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>


                                <asp:TemplateField
                                    HeaderText="Student Name"
                                    ItemStyle-Width="19%"
                                    HeaderStyle-Width="19%">

                                    <ItemTemplate>
                                        <asp:Label
                                            ID="lbl_St_Name_grid"
                                            runat="server"
                                            Text='<%# Bind("[Student Name]") %>'>
                                        </asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>


                                <asp:TemplateField
                                    HeaderText="Date Of Birth"
                                    ItemStyle-Width="13%"
                                    HeaderStyle-Width="13%">

                                    <ItemTemplate>
                                        <asp:Label
                                            ID="lbl_St_DOB_grid"
                                            runat="server"
                                            Text='<%# Bind("[Date of Birth]","{0:dd MMM yyyy}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>


                                <asp:TemplateField
                                    HeaderText="Father's Name"
                                    ItemStyle-Width="18%"
                                    HeaderStyle-Width="18%">

                                    <ItemTemplate>
                                        <asp:Label
                                            ID="lbl_St_Father_grd"
                                            runat="server"
                                            Text='<%# Bind("[Fathers Name]") %>'>
                                        </asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>


                                <asp:TemplateField
                                    HeaderText="Mother's Name"
                                    ItemStyle-Width="18%"
                                    HeaderStyle-Width="18%">

                                    <ItemTemplate>
                                        <asp:Label
                                            ID="lbl_St_Mother_grd"
                                            runat="server"
                                            Text='<%# Bind("[Mothers Name]") %>'>
                                        </asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>


                                <asp:TemplateField
                                    HeaderText="Mobile No"
                                    ItemStyle-Width="18%"
                                    HeaderStyle-Width="18%">

                                    <ItemTemplate>
                                        <asp:Label
                                            ID="lbl_St_Mobile_grd"
                                            runat="server"
                                            Text='<%# Bind("[Mobile Number]") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div style="color: red;">No Records Found !!!</div>
                            </EmptyDataTemplate>
                        </asp:GridView>

                    </div>

                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnk_TotalMentee" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="lnl_UnassignedMentee" EventName="Click" />
                <asp:PostBackTrigger ControlID="btn_Excel_Export" />
            </Triggers>
        </asp:UpdatePanel>

        <asp:Button ID="Button2" runat="server" Style="display: none;" Text="Open" />

        <asp:ModalPopupExtender ID="Md_md_Menter_details" runat="server" PopupControlID="pnl_md_Menter_details"
            TargetControlID="Button2"
            BackgroundCssClass="modalBackground"
            OkControlID="LinkButton2">
        </asp:ModalPopupExtender>

        <asp:Panel ID="pnl_md_Menter_details" runat="server" CssClass="mentorModalPopup" Style="display: none;">
            <div class="mentorModalHeader">
                Mentor Details

        <asp:LinkButton
            ID="LinkButton2"
            runat="server"
            CssClass="mentorModalClose"
            CausesValidation="false">
            &times;
        </asp:LinkButton>
            </div>

            <asp:UpdatePanel
                ID="up_MentorDetails"
                runat="server"
                UpdateMode="Conditional">

                <ContentTemplate>
                    <div class="mentorModalBody">
                        <asp:Label
                            ID="Label1"
                            runat="server"
                            Font-Bold="true">
                        </asp:Label>

                        <div style="text-align: right; margin-bottom: 12px;">

                            <asp:TextBox
                                ID="txt_SearchMentee"
                                runat="server"
                                CssClass="form-control"
                                Width="200px"
                                placeholder="Search Employee Code">
                            </asp:TextBox>

                            <asp:Button
                                ID="Button3"
                                runat="server"
                                Text="Search"
                                CssClass="btn btn-success"
                                OnClick="btn_SearchMentee_Click"
                                Style="margin-left: 5px;" />

                            <asp:Button
                                ID="Button4"
                                runat="server"
                                Text="Assign Mentor"
                                CssClass="btn btn-primary"
                                OnClick="btn_assign_Mentee_Click"
                                Style="margin-left: 5px;" />
                        </div>

                        <div style="width: 100%; max-height: 350px; overflow-y: auto;">
                            <asp:GridView
                                ID="grd_Menter_details_popup"
                                runat="server"
                                CssClass="mentorGrid"
                                AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="Employee Code">
                                        <ItemTemplate>

                                            <asp:CheckBox
                                                ID="chk_Mentee_Detail"
                                                runat="server"
                                                onclick="SingleCheck(this);" />

                                            <asp:Label
                                                ID="lbl_No_grid"
                                                runat="server"
                                                Text='<%# Bind("No_") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label
                                                ID="lbl_FullName_grid"
                                                runat="server"
                                                Text='<%# Bind("[Full Name]") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label
                                                ID="lbl_DepttName_grid"
                                                runat="server"
                                                Text='<%# Bind("[Department Name]") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Date Of Joining">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_DOJ_grid" runat="server" Text='<%# Bind("[Employment Date]", "{0:dd MMM yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                                <HeaderStyle
                                    BackColor="#ed861f"
                                    ForeColor="White"
                                    Font-Bold="True" />

                                <RowStyle
                                    BackColor="White"
                                    ForeColor="#333333" />

                                <AlternatingRowStyle
                                    BackColor="#f8f8f8" />

                                <PagerStyle
                                    HorizontalAlign="Center"
                                    BackColor="#eeeeee"
                                    ForeColor="#333333" />
                            </asp:GridView>

                        </div>

                    </div>

                </ContentTemplate>


                <Triggers>

                    <asp:AsyncPostBackTrigger
                        ControlID="Button3"
                        EventName="Click" />

                    <asp:AsyncPostBackTrigger
                        ControlID="Button4"
                        EventName="Click" />

                </Triggers>

            </asp:UpdatePanel>

        </asp:Panel>



    </div>
</asp:Content>

