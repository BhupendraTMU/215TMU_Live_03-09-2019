<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="FA_MM_All_Activity_Records.aspx.cs" EnableEventValidation="false" Inherits="FA_MM_All_Activity_Records" %>


<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Bootstrap Multiselect CSS & JS -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.15/css/bootstrap-multiselect.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.15/js/bootstrap-multiselect.js"></script>

    <style type="text/css">
        /* Dashboard Theme Colors */
        .card-header-custom {
            background-color: #ed7600;
            color: white;
            padding: 10px 15px;
            font-weight: bold;
            border-radius: 5px 5px 0 0;
            font-size: 16px;
        }

        .filter-section {
            background: #fdfdfd;
            border: 1px solid #ddd;
            padding: 20px;
            border-radius: 5px;
            margin-bottom: 20px;
        }

        .gridview-custom {
            width: 100%;
            border-collapse: collapse;
        }

            .gridview-custom th {
                background-color: #ed7600 !important;
                color: white !important;
                text-align: center;
                vertical-align: middle;
                padding: 10px;
                font-size: 13px;
            }

            .gridview-custom td {
                padding: 8px;
                border: 1px solid #dee2e6;
                font-size: 12px;
            }

        /* Multiselect Styling Fix */
        .btn-group, .multiselect {
            width: 100% !important;
            text-align: left !important;
        }

        .multiselect-container {
            width: 100% !important;
            z-index: 9999 !important;
        }

        .form-label {
            font-weight: bold;
            font-size: 13px;
            margin-bottom: 5px;
            display: block;
        }

        .btn-save {
            background-color: #007bff;
            color: white;
            font-weight: bold;
            border: none;
            padding: 5px 20px;
            border-radius: 4px;
        }
    </style>
    <style type="text/css">
        /* Dropdown ko sabse aage (Front) lane ke liye */
        .btn-group, .multiselect-container {
            z-index: 99999 !important; /* Bahut high z-index takki sabse upar rahe */
            position: relative;
        }

        /* List ka background white karne ke liye (takki piche ka text na dikhe) */
        .multiselect-container {
            background-color: white !important;
            border: 1px solid #ccc;
            box-shadow: 0 6px 12px rgba(0,0,0,.175);
            min-width: 200px !important;
        }

            /* Scrolling lagane ke liye */
            .multiselect-container.dropdown-menu {
                max-height: 250px !important; /* Scrollbar tab aayega jab 250px se bada hoga */
                overflow-y: auto !important;
                overflow-x: hidden !important;
            }

        /* IMPORTANT: Table ke overflow ko theek karne ke liye takki list cut na ho */
        .table-responsive {
            overflow: visible !important;
        }

        /* List items ki styling */
        .multiselect-container > li > a {
            padding: 5px 15px !important;
            color: #333 !important;
            display: block;
            background-color: white !important;
            text-decoration: none;
        }

            .multiselect-container > li > a:hover {
                background-color: #f5f5f5 !important;
            }
    </style>
    <style type="text/css">
        /* Boxes ko bada karne ke liye custom styling */
        .custom-textarea {
            height: 80px !important; /* Box ki height badhane ke liye */
            resize: vertical; /* User chota-bada kar sake */
            font-size: 13px !important;
            padding: 8px !important;
        }

        /* Student List Dropdown ko front mein lane ke liye */
        .btn-group, .multiselect-container {
            z-index: 99999 !important;
        }

        /* Checklist mein scrolling lagane ke liye */
        .multiselect-container {
            max-height: 250px !important; /* 250px ke baad scroll aayega */
            overflow-y: auto !important;
            overflow-x: hidden !important;
            background-color: white !important;
            border: 1px solid #ccc !important;
            box-shadow: 0 5px 15px rgba(0,0,0,0.2) !important;
        }

        /* Table layout fix takki boxes ek line mein rahein aur dropdown na chhite */
        .gridview-custom td {
            vertical-align: top !important; /* Text top se start ho */
        }

        .table-responsive {
            overflow: visible !important; /* Dropdown ko table ke bahar dikhane ke liye */
        }

        /* Calendar ko sabse aage dikhane ke liye */
        .custom-calendar .ajax__calendar_container {
            z-index: 100001 !important;
            background-color: #ffffff;
            border: 1px solid #ed7600;
            color: #000;
        }

        /* Calendar ke dino ki styling (Optional) */
        .custom-calendar .ajax__calendar_day {
            color: #333;
        }

        .custom-calendar .ajax__calendar_active .ajax__calendar_day {
            background-color: #ed7600;
            color: #fff;
        }
    </style>
    <style type="text/css">
        /* Header aur Cells ko center karne ke liye */
        .text-center-align {
            text-align: center !important;
            vertical-align: middle !important;
        }

        /* Entry boxes ki width aur height badhane ke liye */
        .custom-textarea {
            height: 85px !important;
            width: 100% !important;
            resize: vertical;
            font-size: 12px !important;
            padding: 5px !important;
        }

        /* Type aur Sem Dropdowns ko bada aur center karne ke liye */
        .large-dropdown {
            height: 35px !important;
            width: 100% !important;
            text-align-last: center; /* Dropdown text center */
        }

        /* Student List Checkbox fix aur scrolling */
        .multiselect-container {
            z-index: 99999 !important;
            max-height: 250px !important;
            overflow-y: auto !important;
            overflow-x: hidden !important;
            background-color: white !important;
            min-width: 220px !important;
            text-align: left !important; /* List items left hi rahenge readability ke liye */
        }

        /* Multiselect Button ko center align karna table cell mein */
        .btn-group {
            display: block !important;
            margin: 0 auto !important;
        }

        /* Table container overflow fix */
        .table-responsive {
            overflow: visible !important;
            padding-bottom: 50px; /* Space for dropdown */
        }

        /* Calendar Extender Z-Index */
        .custom-calendar .ajax__calendar_container {
            z-index: 100001 !important;
        }
    </style>

    <script type="text/javascript">
        function pageLoad() {
            $('[id*=lb_Student_list]').multiselect({
                includeSelectAllOption: true,
                maxHeight: 250, // Yahan se scrolling active hogi
                buttonWidth: '100%',
                nonSelectedText: 'Select Student',
                allSelectedText: 'All Selected',
                numberDisplayed: 1,
                enableFiltering: true,
                enableCaseInsensitiveFiltering: true,
                buttonClass: 'form-control',
                // Takki dropdown click karne par piche na jaye
                onDropdownShow: function (event) {
                    $(event.target).closest('.table-responsive').css('overflow', 'visible');
                }
            });
        }

        $(document).ready(function () {
            pageLoad();
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>

    <div class="container-fluid" style="padding: 20px;">

        <!-- Header Title -->
        <div class="card-header-custom">
            Record of Co-Curricular Activities & Extra-Curricular Activities
        </div>

        <!-- Filter Bar -->
        <!-- Filter Bar -->
        <div class="filter-section">
            <div class="row">
                <!-- Student No Column -->
                <div class="col-md-4">
                    <label class="form-label">Student No./Enrollment No./Name</label>
                    <asp:TextBox ID="txt_mentorFormentee_studentEnrollmentName" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                </div>

                <!-- Program Column -->
                <div class="col-md-3">
                    <label class="form-label">Program</label>
                    <asp:DropDownList ID="ddl_mentorFormentee_course" AutoPostBack="true" OnSelectedIndexChanged="ddl_mentorFormentee_course_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                </div>

                <!-- Academic Year Column -->
                <div class="col-md-2">
                    <label class="form-label">Academic Year</label>
                    <asp:DropDownList ID="ddl_mentorFormentee_academicYear" AutoPostBack="true" OnSelectedIndexChanged="ddl_mentorFormentee_academicYear_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                </div>

                <!-- Button Column (Alignment Fix) -->
                <div class="col-md-2">
                    <label class="form-label">&nbsp;</label>
                    <!-- Khali label alignment ke liye -->
                    <asp:Button ID="btn_mentorFormentee_get" OnClick="btn_mentorFormentee_get_Click" runat="server"
                        CssClass="btn btn-danger btn-sm w-100" Text="GET RECORD" Style="font-weight: bold;" />
                </div>

                <!-- Back Button Column -->
                <div class="col-md-1">
                    <label class="form-label">&nbsp;</label>
                    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click"
                        CssClass="btn btn-secondary btn-sm" Visible="false" />
                </div>
            </div>
        </div>

        <!-- Entry Table Section -->
        <!-- Entry Table Section -->
        <div class="table-responsive">
            <table class="gridview-custom table table-bordered">
                <thead>
                    <tr style="background-color: #ed7600; color: white;">
                        <th style="width: 13%;" class="text-center-align">Type</th>
                        <th style="width: 10%;" class="text-center-align">Sem</th>
                        <th style="width: 14%;" class="text-center-align">Student Name</th>
                        <th style="width: 10%;" class="text-center-align">Activity Name</th>
                        <th style="width: 10%;" class="text-center-align">Date</th>
                        <th style="width: 10%;" class="text-center-align">Event Details</th>
                        <th style="width: 10%;" class="text-center-align">Organizer</th>
                        <th style="width: 10%;" class="text-center-align">Level</th>
                        <th style="width: 10%;" class="text-center-align">Position</th>
                        <th style="width: 5%;" class="text-center-align">Action</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td class="text-center-align">
                            <asp:DropDownList ID="ddl_recordCo_CurricularActivities" runat="server" CssClass="form-control large-dropdown">
                                <asp:ListItem Text="Co-Curricular" Value="Co-Curricular"></asp:ListItem>
                                <asp:ListItem Text="Extra-Curricular" Value="Extra-Curricular"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="text-center-align">
                            <asp:DropDownList ID="lbl_recordCo_Curricular_semester" runat="server" CssClass="form-control large-dropdown"></asp:DropDownList>
                        </td>
                        <td class="text-center-align">
                            <div style="width: 100%;">
                                <asp:ListBox ID="lb_Student_list" runat="server" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_recordCo_Curricular_activityName" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control custom-textarea" placeholder="Activity..."></asp:TextBox>
                        </td>
                        <td class="text-center-align">
                            <asp:TextBox ID="txt_recordCo_Curricular_date" runat="server" CssClass="form-control" Style="text-align: center;"></asp:TextBox>
                            <asp:CalendarExtender ID="calextender" TargetControlID="txt_recordCo_Curricular_date" runat="server" Format="dd MMM yyyy" CssClass="custom-calendar" />
                        </td>
                        <td>
                            <asp:TextBox ID="txt_recordCo_Curricular_eventDetails" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control custom-textarea" placeholder="Event..."></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txt_recordCo_Curricular_detailEvent_organizer" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control custom-textarea" placeholder="Organizer..."></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txt_recordCo_Curricular_level_CUSNI" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control custom-textarea" placeholder="Level..."></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txt_recordCo_Curricular_certificaltionPosition" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control custom-textarea" placeholder="Position..."></asp:TextBox></td>
                        <td class="text-center-align">
                            <asp:Button ID="btn_recordCo_Curricular_addData" CssClass="btn btn-primary btn-sm font-weight-bold" runat="server" Text="Save" OnClick="btn_recordCo_Curricular_addData_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <br />

        <!-- Records Grid -->
        <asp:GridView ID="grdview_recordCo_curricular_tbl"
            OnRowDataBound="grdview_recordCo_curricular_tbl_RowDataBound"
            OnRowUpdating="grdview_recordCo_curricular_tbl_RowUpdating"
            OnRowCancelingEdit="grdview_recordCo_curricular_tbl_RowCancelingEdit"
            OnRowDeleting="grdview_recordCo_curricular_tbl_RowDeleting"
            OnRowEditing="grdview_recordCo_curricular_tbl_RowEditing"
            runat="server" AutoGenerateColumns="false" CssClass="gridview-custom table table-hover">
            <Columns>
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CssClass="btn btn-link btn-sm" Text="Edit"></asp:LinkButton>
                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CssClass="btn btn-link btn-sm text-danger" Text="Delete" OnClientClick="return confirm('Are you sure?');"></asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Update" Text="Update"></asp:LinkButton>
                        <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="Semester" HeaderText="Sem" />
                <asp:BoundField DataField="Student_Name" HeaderText="Student Name" />
                <asp:BoundField DataField="Activity_Name" HeaderText="Activity Name" />
                <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:dd MMM yyyy}" />
                <asp:BoundField DataField="Event_Details" HeaderText="Event Details" />
                <asp:BoundField DataField="Detail_EventOrganizer" HeaderText="Organizer" />
                <asp:BoundField DataField="Level_C_U_S_N_I" HeaderText="Level" />
                <asp:BoundField DataField="Certification_Position" HeaderText="Position" />
                <asp:BoundField DataField="Type" HeaderText="Type" />
            </Columns>
            <EmptyDataTemplate>
                <div class="alert alert-info mt-2">No records found.</div>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
