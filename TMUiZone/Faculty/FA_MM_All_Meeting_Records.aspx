<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="FA_MM_All_Meeting_Records.aspx.cs" EnableEventValidation="false" Inherits="FA_MM_All_Meeting_Records" %>


<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Bootstrap Multiselect CSS & JS -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.15/css/bootstrap-multiselect.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.15/js/bootstrap-multiselect.js"></script>

    <style type="text/css">
        /* Dashboard Orange Header */
        .card-header-custom {
            background-color: #ed7600;
            color: white;
            padding: 12px 15px;
            font-weight: bold;
            border-radius: 4px;
            font-size: 16px;
            margin-bottom: 10px;
        }

        /* Filter Box Design */
        .filter-section {
            background: #fdfdfd;
            border: 1px solid #ddd;
            padding: 15px 20px;
            border-radius: 4px;
            margin-bottom: 20px;
        }

        .form-label {
            font-weight: bold;
            font-size: 13px;
            color: #333;
            margin-bottom: 5px;
            display: block;
        }

        /* Entry Table Container (No Scroll) */
        .table-container-custom {
            width: 100%;
            overflow: visible !important; /* Scrolling hatayi gayi */
            border: 1px solid #ddd;
            border-radius: 4px;
            background-color: #fff;
            padding-bottom: 100px; /* Dropdowns ke liye space */
        }

        .gridview-custom {
            width: 100% !important;
            border-collapse: collapse;
            table-layout: fixed; /* Columns ko fixed rakhta hai percentage ke hisaab se */
        }

            /* Table Header & Cells Alignment */
            .gridview-custom th {
                background-color: #ed7600 !important;
                color: white !important;
                text-align: center;
                vertical-align: middle !important;
                padding: 8px;
                font-size: 12px;
                border: 1px solid #ddd;
            }

            .gridview-custom td {
                padding: 8px;
                border: 1px solid #dee2e6;
                vertical-align: middle !important;
                text-align: center;
            }

        /* Textbox & Dropdown Fit Styling */
        .custom-textarea {
            height: 80px !important;
            width: 100% !important;
            resize: vertical;
            font-size: 11px;
            padding: 5px;
        }

        .large-dropdown {
            height: 34px !important;
            width: 100% !important;
            font-size: 11px;
        }

        /* Multiselect & Calendar Layers */
        .btn-group, .multiselect {
            width: 100% !important;
        }

        .multiselect-container {
            z-index: 99999 !important;
            max-height: 250px !important;
            overflow-y: auto !important;
        }

        .custom-calendar .ajax__calendar_container {
            z-index: 100005 !important;
        }
    </style>
    <style type="text/css">
        /* Cells ko upar (Top) align karne ke liye */
        .align-top-row td {
            vertical-align: top !important;
            padding-top: 10px !important; /* Thoda gap upar se takki border se na chipke */
            text-align: center !important;
        }

        /* Textareas ko align-left rakhna behtar hai input ke liye */
        .custom-textarea {
            height: 85px !important;
            width: 100% !important;
            resize: vertical;
            font-size: 11px;
            padding: 5px;
            text-align: left !important;
            vertical-align: top !important;
        }

        /* Dropdowns aur Date box fix */
        .large-dropdown, .date-box {
            margin-top: 0px !important;
        }
        /* 1. Bahar wale wrapper (div) ka border poora hata do takki extra line na dikhe */
.table-container-custom {
    border: none !important; 
    padding-bottom: 0px !important;
    background-color: transparent !important;
}

/* 2. Table ka nichla border khatam karo */
.gridview-custom {
    border-bottom: none !important;
    border-collapse: collapse !important; /* Lines ko merge karne ke liye */
}

/* 3. SIRF cells (td) ke niche line rakho (taki row ki line dikhe) */
.gridview-custom td {
    border-bottom: 1px solid #ddd !important; /* Yeh aapki row wali line hai */
    border-left: 1px solid #ddd !important;
    border-right: 1px solid #ddd !important;
    border-top: 1px solid #ddd !important;
}

/* Header ke borders fix karein */
.gridview-custom th {
    border: 1px solid #ddd !important;
}
    </style>

    <script type="text/javascript">
        function pageLoad() {
            $('[id*=lb_Student_list]').multiselect({
                includeSelectAllOption: true,
                maxHeight: 250,
                buttonWidth: '100%',
                nonSelectedText: 'Select Student',
                allSelectedText: 'All Selected',
                numberDisplayed: 1,
                enableFiltering: true,
                enableCaseInsensitiveFiltering: true,
                buttonClass: 'form-control',
                onDropdownShow: function (event) {
                    $(event.target).closest('.table-scroll-wrapper').css('overflow', 'visible');
                }
            });
        }
        $(document).ready(function () { pageLoad(); });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>

    <div class="container-fluid" style="padding: 15px;">

        <!-- Dashboard Style Title -->
        <div class="card-header-custom">
            Record of Mentor-Mentee Meetings
        </div>

        <!-- Horizontal Filter Section -->
        <div class="filter-section">
            <div class="row align-items-end">
                <div class="col-md-4">
                    <label class="form-label">Student No./Enrollment No./Name</label>
                    <asp:TextBox ID="txt_mentorFormentee_studentEnrollmentName" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label class="form-label">Program</label>
                    <asp:DropDownList ID="ddl_mentorFormentee_course" AutoPostBack="true" OnSelectedIndexChanged="ddl_mentorFormentee_course_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label class="form-label">Academic Year</label>
                    <asp:DropDownList ID="ddl_mentorFormentee_academicYear" AutoPostBack="true" OnSelectedIndexChanged="ddl_mentorFormentee_academicYear_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label class="form-label">&nbsp;</label>
                    <asp:Button ID="btn_mentorFormentee_get" OnClick="btn_mentorFormentee_get_Click" runat="server" CssClass="btn btn-danger btn-sm w-100" Text="GET RECORD" Font-Bold="true" />
                </div>
                <div class="col-md-1">
                    <label class="form-label">&nbsp;</label>
                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-secondary btn-sm w-100" Visible="false" />
                </div>
            </div>
        </div>

        <!-- Scrolling Entry Table -->
        <!-- Entry Table Section (Fit to Screen) -->
        <!-- Entry Table Section -->
        <div class="table-container-custom">
            <table class="gridview-custom table table-bordered">
                <thead>
                    <tr style="background-color: #ed7600; color: white;">
                        <th style="width: 8%;">Semester</th>
                        <th style="width: 11%;">Date</th>
                        <th style="width: 16%;">Student's Name</th>
                        <th style="width: 20%;">Issue Discussed</th>
                        <th style="width: 20%;">Advice by Mentor</th>
                        <th style="width: 20%;">Action/Remark</th>
                        <th style="width: 5%;">Action</th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="align-top-row">
                        <!-- Yahan class add ki gayi hai -->
                        <td>
                            <asp:DropDownList ID="ddl_mentorMentee_semester" runat="server" CssClass="form-control large-dropdown"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_mentorMentee_date" runat="server" CssClass="form-control date-box" Style="text-align: center; font-size: 11px;"></asp:TextBox>
                            <asp:CalendarExtender ID="calextender" TargetControlID="txt_mentorMentee_date" runat="server" Format="dd MMM yyyy" CssClass="custom-calendar" />
                        </td>
                        <td>
                            <div style="width: 100%;">
                                <asp:ListBox ID="lb_Student_list" runat="server" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_mentorMentee_issue_identified" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control custom-textarea" placeholder="Issues..."></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_mentorMentee_providedAdvice_bymentor" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control custom-textarea" placeholder="Advice..."></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_mentorMentee_summaryRemark" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control custom-textarea" placeholder="Remark..."></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnAddMenteeRecord" CssClass="btn btn-primary btn-sm font-weight-bold" runat="server" Text="Save" OnClick="btnAddMenteeRecord_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <br />

        <!-- Records GridView -->
        <asp:GridView ID="grdview_mentorMentee_tbl" runat="server" AutoGenerateColumns="False" CssClass="gridview-custom table table-hover"
            OnRowEditing="grdview_mentorMentee_tbl_RowEditing" OnRowUpdating="grdview_mentorMentee_tbl_RowUpdating"
            OnRowDeleting="grdview_mentorMentee_tbl_RowDeleting" OnRowCancelingEdit="grdview_mentorMentee_tbl_RowCancelingEdit">
            <Columns>
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CssClass="btn btn-link btn-sm" Text="Edit"></asp:LinkButton>
                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CssClass="btn btn-link btn-sm text-danger" Text="Delete" OnClientClick="return confirm('Delete this record?');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Semester" HeaderText="Sem" />
                <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:dd MMM yyyy}" />
                <asp:BoundField DataField="Name_Student" HeaderText="Student Name" />
                <asp:BoundField DataField="Issue_Discussed_Identified_ProblemS" HeaderText="Issues" />
                <asp:BoundField DataField="Provided_Advice_Solutions_ByMentor" HeaderText="Advice" />
                <asp:BoundField DataField="Summary_ActionTaken_Remark" HeaderText="Summary" />
            </Columns>
            <EmptyDataTemplate>
                <div class="alert alert-info mt-2">No records found.</div>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
