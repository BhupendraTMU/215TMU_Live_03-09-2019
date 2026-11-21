<%@ Page Title="View Student CRAC Meeting" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master"   AutoEventWireup="true" CodeFile="ViewStudentCRACFormDetails.aspx.cs"   Inherits="Faculty_ViewStudentCRACFormDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .approval-grid {
            width: 100%;
            border-collapse: collapse;
        }

        .approval-grid th {
            background-color: #ED7600; /* project yellow header */
            color: white;
            padding: 12px 10px;
            text-align: left;
            font-weight: bold;
        }

        .approval-grid td {
            padding: 8px;
            border-bottom: 1px solid #ddd;
            vertical-align: middle;
        }

        /* checkbox alignment and spacing tweaks (design only) */
        .approval-grid td input[type="checkbox"] {
            margin: 0;
            vertical-align: middle;
            transform: translateY(0);
        }

        .approval-grid th input[type="checkbox"] {
            margin: 0;
            vertical-align: middle;
        }

        /*.approval-grid tr:hover {
            background-color: #f5f5f5;
        }*/

        .btn-action {
            margin: 2px;
            padding: 5px 10px;
            font-size: 12px;
        }

        .status-badge {
            display: inline-block;
            padding: 5px 10px;
            border-radius: 3px;
            font-size: 12px;
            font-weight: bold;
            margin: 2px 0;
        }

        .status-pending {
            background-color: #ED7600;
            color: black;
        }

        .status-approved {
            background-color: #28a745;
            color: white;
        }

        .status-rejected {
            background-color: #dc3545;
            color: white;
        }

        .modal-title {
            color: White;
            font-weight: bold;
        }

        .form-section {
            background-color: #f9f9f9;
            padding: 15px;
            margin: 10px 0;
            /*border-left: 4px solid #2b5b69;*/
        }

        .form-section label {
            font-weight: bold;
            color: #333;
        }

        .form-section .form-value {
            padding: 8px;
            background-color: #fff;
            border: 1px solid #ddd;
            border-radius: 3px;
            margin: 5px 0;
        }

        /* Search pill styles to match screenshot */
        .search-pill {
            background: #fff6ea; /* very light warm background */
            border-radius: 30px;
            padding: 10px 16px;
            display: inline-flex;
            align-items: center;
            box-shadow: 0 1px 2px rgba(0,0,0,0.05);
        }

        .search-pill .search-label {
            margin-right: 12px;
            font-weight: 600;
            color: #333;
            white-space: nowrap;
        }

        .search-input {
            height: 34px;
            border-radius: 4px;
            border: 1px solid #cfe2ff;
            background-color: #ffffff !important; /* changed to white */
            padding: 6px 10px;
            width: 260px;
            box-sizing: border-box;
        }

        .btn-search {
            background-color: #ff9800; /* orange */
            border-color: #ff9800;
            color: #fff;
            padding: 6px 12px;
            margin-left: 10px;
            border-radius: 4px;
            font-weight: 600;
        }

        .btn-clear-small {
            background-color: transparent;
            border: none;
            color: #666;
            margin-left: 8px;
            text-decoration: underline;
            padding: 0;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="panel-heading" style="background-color: #ED7600">
                <center>
                    <div class="panel-title">
                        <b>
                            <p style="color: white; font-size: 20px">
                                View Student CRAC Meeting
                            </p>
                        </b>
                    </div>
                </center>
            </div>
            <br />

            <fieldset class="boxBodyInner">
                <!-- Approval level is determined from user's Session (Departmentcode and uid). Dropdown removed. -->

               

                <!-- Enrollment Search -->
                <div class="row ml-5 mr-5 mt-2">
                    <div class="col-sm-8">
                        <!-- New pill-styled search matching screenshot -->
                        <div class="search-pill">
                            <div class="search-label">Enrollment No:</div>
                            <asp:TextBox ID="txtSearchEnrollmentCRAC" runat="server" CssClass="search-input" autocomplete="off" placeholder="" ></asp:TextBox>
                            <asp:Button ID="btnSearchCRAC" runat="server" Text="Search" CssClass="btn-search" OnClick="btnSearchCRAC_Click" />
                            <asp:Button ID="btnClearCRAC" runat="server" Text="Clear" CssClass="btn-clear-small" OnClick="btnClearCRAC_Click" />
                        </div>
                    </div>
                </div>

                <script type="text/javascript">
                    // Design-only: clear any browser autofill after load and keep modals unaffected
                    window.addEventListener('load', function () {
                        try {
                            var el = document.getElementById('<%= txtSearchEnrollmentCRAC.ClientID %>');
                            if (el) {
                                // Clear immediately and multiple times to defeat browser autofill
                                try { el.value = ''; el.removeAttribute('value'); } catch (e) { }
                                try { el.setAttribute('autocomplete','new-password'); } catch (e) { }
                                try { el.setAttribute('name',''); } catch (e) { }
                                // Clear again after short intervals
                                setTimeout(function () { try { el.value = ''; el.removeAttribute('value'); } catch (e) { } }, 300);
                                setTimeout(function () { try { el.value = ''; el.removeAttribute('value'); } catch (e) { } }, 800);
                                setTimeout(function () { try { el.value = ''; el.removeAttribute('value'); } catch (e) { } }, 1500);
                            }
                        } catch (e) { }
                    });
                </script>

                 <div class="row ml-5 mr-5">
                     <div class="col-sm-12">
                         <asp:Label ID="lblMsgCRAC" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
                         <asp:Label ID="lblErrCRAC" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                     </div>
                 </div>
                <div class="row ml-5 mr-5 mt-3">
                    <div class="col-sm-12">
                        <div style="overflow-x: auto;">
                            <!-- Enrollment search input and buttons are placed above the GridView and approval-grid CSS class is applied -->
                            <asp:GridView ID="gvCRACMeeting" runat="server" CssClass="table table-striped approval-grid"
                                AutoGenerateColumns="False" OnRowCommand="gvCRACMeeting_RowCommand"    Width="100%"
                                DataKeyNames="Id,StudentNo,CollegeCode,CourseCode,FathersName,CRACMeetingName,CRACMeetingNo"
                                AllowPaging="True" PageSize="10" OnPageIndexChanging="gvCRACMeeting_PageIndexChanging">
                                <HeaderStyle BackColor="#ED7600" ForeColor="White" Font-Bold="true" />
                                <RowStyle CssClass="approval-grid-row" />
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="ID" Visible="false" />
                                    <asp:BoundField DataField="StudentName" HeaderText="Student Name" />
                                    <asp:BoundField DataField="EnrollmentNo" HeaderText="Enrollment No" />
                                    <asp:BoundField DataField="CollegeName" HeaderText="College" />
                                    <asp:BoundField DataField="CourseName" HeaderText="Course" />
                                    <asp:BoundField DataField="AcademicYear" HeaderText="Academic Year" />
                                    <asp:BoundField DataField="MobileNo" HeaderText="Mobile" />
                                    <asp:BoundField DataField="EmailId" HeaderText="Email" />
                                    <asp:BoundField DataField="CRACMeetingName" HeaderText="CRAC Meeting" />                                   
                                    <asp:TemplateField HeaderText="Actions" ItemStyle-CssClass="actions-column" HeaderStyle-CssClass="actions-column-header">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkView" runat="server" Text="Details" CssClass="btn btn-sm btn-info btn-action"
                                                CommandName="ViewDetails" CommandArgument='<%# Eval("Id") %>'  />
                                        </ItemTemplate>
                                    </asp:TemplateField>                                 
                                </Columns>
                                <EmptyDataTemplate>
                                    <div style="padding: 20px; text-align: center; color: #666;">
                                        <b>No records found for approval.</b>
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <!-- Actions removed: page is view-only printable list -->
                        </div>
                    </div>
                </div>
            </fieldset>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="gvCRACMeeting" />
        </Triggers>
    </asp:UpdatePanel>

    <!-- Modal for Viewing Details -->
    <div class="modal fade" id="detailsModal" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #ED7600; color: white;">
                    <h5 class="modal-title">Student Details & Fee Information</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updatePanelDetails" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-section">
                                        <label>Student Name:</label>
                                        <div class="form-value">
                                            <asp:Label ID="lblDetailStudentNameCRAC" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-section">
                                        <label>Enrollment No:</label>
                                        <div class="form-value">
                                            <asp:Label ID="lblDetailEnrollmentNoCRAC" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-section">
                                        <label>Father's Name:</label>
                                        <div class="form-value">
                                            <asp:Label ID="lblDetailFathersNameCRAC" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-section">
                                         <label>Academic Year:</label>
                                         <div class="form-value">
                                             <asp:Label ID="lblDetailAcademicYearCRAC" runat="server"></asp:Label>
                                         </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-section">
                                        <label>College:</label>
                                        <div class="form-value">
                                            <asp:Label ID="lblDetailCollegeCRAC" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-section">
                                        <label>Course:</label>
                                        <div class="form-value">
                                            <asp:Label ID="lblDetailCourseCRAC" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-section">
                                        <label>CRAC Meeting:</label>
                                        <div class="form-value">
                                            <asp:Label ID="lblDetailCRACMeeting" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6"></div>
                            </div>

                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-section">
                                        <label>Email:</label>
                                        <div class="form-value">
                                            <asp:Label ID="lblDetailEmailCRAC" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-section">
                                        <label>Mobile:</label>
                                        <div class="form-value">
                                            <asp:Label ID="lblDetailMobileCRAC" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>                         

                            <hr />
                            <h6 style="color: #2b5b69; font-weight: bold; margin-top: 20px;">Fee Details:</h6>

                            <div style="overflow-x: auto; margin-top: 15px;">
                                <asp:GridView ID="gvFeeDetailsCRAC" runat="server" CssClass="table table-striped table-sm"
                                    AutoGenerateColumns="False" Width="100%">
                                    <HeaderStyle BackColor="#ed7600" ForeColor="White" Font-Bold="true" Font-Size="Small" />
                                    <Columns>
                                        <asp:BoundField DataField="Fee Description" HeaderText="Fee Description" />
                                        <asp:BoundField DataField="FeeAmount" HeaderText="Fee Amount" DataFormatString="{0:C}" />
                                        <asp:BoundField DataField="PaidAmount" HeaderText="Paid Amount" DataFormatString="{0:C}" />
                                        <asp:BoundField DataField="PendingAmount" HeaderText="Pending Amount" DataFormatString="{0:C}" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div style="padding: 10px; text-align: center;">No fee details found</div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal for Approval/Rejection -->
    <div class="modal fade" id="approvalModal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #ED7600; color: white;">
                    <h5 class="modal-title">Approval Action</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updatePanelApproval" runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                                <label>Action:</label>
                                <asp:Label ID="lblApprovalAction" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label>Student Name:</label>
                                <asp:Label ID="lblApprovalStudentName" runat="server"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label>Enrollment No:</label>
                                <asp:Label ID="lblApprovalEnrollmentNo" runat="server"></asp:Label>
                            </div>
                            <asp:HiddenField ID="hiddenRecordId" runat="server" />
                            <asp:HiddenField ID="hiddenApprovalAction" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function showDetailsModal() {
            $('#detailsModal').modal('show');
        }

        function showApprovalModal(action) {
            document.getElementById('<%= hiddenApprovalAction.ClientID %>').value = action;
            $('#approvalModal').modal('show');
        }

        function toggleSelectAll(chkAll) {
            var grid = document.getElementById('<%= gvCRACMeeting.ClientID %>');
            if (!grid) return;
            var inputs = grid.getElementsByTagName('input');
            for (var i = 0; i < inputs.length; i++) {
                var inp = inputs[i];
                if (inp.type === 'checkbox' && inp.id.indexOf('chkSelect') !== -1) {
                    inp.checked = chkAll.checked;
                }
            }
        }


        //function toggleSelectAll(chk) {
        //    try {
        //        var items = document.querySelectorAll('.item-checkbox');
        //        for (var i = 0; i < items.length; i++) {
        //            items[i].checked = chk.checked;
        //        }
        //    } catch (e) { }
        //}

        // Update header checkbox when any item checkbox changes
        function onItemCheckboxChange() {
            try {
                var items = document.querySelectorAll('.item-checkbox');
                var allChecked = true;
                var anyChecked = false;
                for (var i = 0; i < items.length; i++) {
                    if (!items[i].checked) allChecked = false;
                    if (items[i].checked) anyChecked = true;
                }
                var hdr = document.getElementById('chkAll');
                if (hdr) hdr.checked = allChecked;
            } catch (e) { }
        }

    </script>
        <style>
            /* Adjust header color to project yellow and provide spacing for status badge */
            .approval-grid th {
                background-color: #ED7600 !important;
                color: WHITE !important;
                padding: 10px !important;
            }

            .approval-grid .status-badge {
                display: inline-block;
                margin-top: 4px;
                background-color: #ED7600;
                color: #000;
                padding: 5px 8px;
                border-radius: 4px;
            }

        /* Reduce gap after Actions header and align checkbox column */
        .actions-column {
            white-space: nowrap;
            padding-right: 6px;
        }

        .actions-column-header {
            padding-right: 6px !important;
        }

        /* Ensure header checkbox aligns vertically with item checkboxes */
        .approval-grid th input[type="checkbox"] {
            margin-top: 0 !important;
        }

        /* Checkbox column specific alignment */
        .checkbox-column { text-align:center; }
        .checkbox-column-header { text-align:center; vertical-align:middle; }
        .approval-grid .item-checkbox { margin:0; transform:translateY(0); }

        /* Keep modals opening when server requests them; buttons trigger postback only */
        .btn-action, .btn-search, .btn-clear-small { cursor:pointer; }
        </style>
    <script type="text/javascript">
        // Toggle all item checkboxes when header checkbox clicked
       

        // Clear hidden fields when modals are closed to avoid stale state on reload
        jQuery(document).ready(function () {
            try {
                jQuery('#detailsModal').on('hidden.bs.modal', function () {
                    try { document.getElementById('<%= hiddenRecordId.ClientID %>').value = ''; } catch (e) { }
                    try { document.getElementById('<%= hiddenApprovalAction.ClientID %>').value = ''; } catch (e) { }
                });

                jQuery('#approvalModal').on('hidden.bs.modal', function () {
                    try { document.getElementById('<%= hiddenRecordId.ClientID %>').value = ''; } catch (e) { }
                    try { document.getElementById('<%= hiddenApprovalAction.ClientID %>').value = ''; } catch (e) { }
                });
            } catch (e) { }
        });
    </script>
    </asp:Content>