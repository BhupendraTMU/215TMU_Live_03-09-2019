<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master"
    AutoEventWireup="true" CodeFile="Announcement.aspx.cs"
    Inherits="Faculty_Announcement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background: #f4f7fc;
            font-family: 'Segoe UI', sans-serif;
        }

        /* Main Container */
        .announcement-card {
            background: #fff;
            margin: 30px auto;
            padding: 35px;
            border-radius: 20px;
            box-shadow: 0 8px 30px rgba(0,0,0,0.08);
            border-top: 5px solid #0d6efd;
        }

        /* Page Heading */
        .page-title {
            font-size: 28px;
            font-weight: 700;
            color: #093A62;
            margin-bottom: 30px;
            text-align: center;
            position: relative;
        }

            .page-title:after {
                content: '';
                width: 80px;
                height: 4px;
                background: #0d6efd;
                display: block;
                margin: 10px auto 0;
                border-radius: 10px;
            }

        /* Labels */
        .form-label {
            display: block;
            margin-bottom: 8px;
            font-size: 14px;
            font-weight: 600;
            color: #444;
        }

        /* Inputs */
        .form-input,
        .form-textarea,
        select {
            width: 100%;
            border: 1px solid #dce3ec;
            border-radius: 10px;
            padding: 10px 14px;
            font-size: 14px;
            transition: all .3s ease;
            background: #fff;
        }

        .form-input {
            height: 42px;
        }

            .form-input:focus,
            .form-textarea:focus {
                border-color: #0d6efd;
                box-shadow: 0 0 0 4px rgba(13,110,253,.15);
                outline: none;
            }

        .form-textarea {
            min-height: 140px;
            resize: vertical;
        }

        /* Row */
        .row-flex {
            display: flex;
            gap: 20px;
            margin-bottom: 22px;
            flex-wrap: wrap;
        }

            .row-flex > div {
                flex: 1;
                min-width: 250px;
            }

        /* Multiselect */
        .multiSelectBox {
            width: 100%;
            height: 42px;
            line-height: 42px;
            border: 1px solid #dce3ec;
            border-radius: 10px;
            padding: 0 14px;
            background: white;
            cursor: pointer;
            transition: .3s;
        }

            .multiSelectBox:hover {
                border-color: #0d6efd;
            }

        .multiSelectPanel {
            display: none;
            width: 100%;
            max-height: 250px;
            overflow-y: auto;
            background: white;
            border-radius: 12px;
            border: 1px solid #dce3ec;
            box-shadow: 0 10px 25px rgba(0,0,0,.12);
            margin-top: 5px;
            padding: 10px;
        }

            .multiSelectPanel input[type=checkbox] {
                margin-right: 8px;
            }

        .selectAll {
            background: #f8f9fa;
            padding: 10px;
            border-radius: 8px;
            font-weight: 600;
            margin-bottom: 10px;
        }

        /* Validation */
        .text-danger {
            color: #dc3545;
            font-size: 12px;
            display: block;
            margin-top: 4px;
        }

        /* Buttons */
        .save-btn {
            border: none;
            padding: 12px 30px;
            border-radius: 50px;
            font-size: 14px;
            font-weight: 600;
            margin-right: 10px;
            transition: all .3s;
        }

        .btn-primary {
            background: linear-gradient(135deg,#0d6efd,#0b5ed7);
        }

            .btn-primary:hover {
                transform: translateY(-2px);
            }

        .btn-warning {
            background: linear-gradient(135deg,#ffb703,#fb8500);
            color: white;
        }

            .btn-warning:hover {
                transform: translateY(-2px);
            }

        /* GridView */
        .table {
            border-radius: 15px;
            overflow: hidden;
            box-shadow: 0 8px 25px rgba(0,0,0,.08);
            background: white;
        }

            .table th {
                background: #093A62;
                color: white;
                text-align: center;
                font-weight: 600;
                padding: 12px;
            }

            .table td {
                padding: 10px;
                vertical-align: middle;
            }

        .table-striped tbody tr:nth-child(even) {
            background: #f8faff;
        }

        .table tbody tr:hover {
            background: #eef5ff;
            transition: .3s;
        }

        /* Action Links */
        .gv-action-btn {
            color: #0d6efd;
            font-weight: 600;
            text-decoration: none;
        }

            .gv-action-btn:hover {
                text-decoration: underline;
            }

        .gv-action-btn-danger {
            color: #dc3545;
        }

        /* File Upload */
        input[type=file] {
            border: 1px dashed #0d6efd;
            border-radius: 10px;
            padding: 8px;
            width: 100%;
            background: #f8fbff;
        }

        .multiSelectPanel table {
            width: 100%;
        }

        .multiSelectPanel td {
            padding: 4px 0;
            vertical-align: top;
        }

        .multiSelectPanel input[type="checkbox"] {
            margin-right: 8px;
            vertical-align: top;
        }

        .multiSelectPanel label {
            display: inline-block;
            width: calc(100% - 25px);
            word-wrap: break-word;
            line-height: 20px;
        }

        .multiSelectPanel {
            max-height: 300px;
            overflow-y: auto;
            overflow-x: hidden;
        }

            .multiSelectPanel input[type=checkbox] + label {
                display: inline-block;
                padding: 4px 0;
                cursor: pointer;
            }

            .multiSelectPanel label:hover {
                color: #0d6efd;
            }

            .multiSelectPanel tr:hover {
                background: #f5f9ff;
            }
    </style>
    <script type="text/javascript">
        function updateCourseText() {
            var selected = [];
            var checkboxes = document.querySelectorAll(
        '#<%= drpCourse.ClientID %> input[type=checkbox]');

            checkboxes.forEach(cb => {
                if (cb.checked)
                    selected.push(cb.nextSibling.textContent.trim().split('-')[0]);
            });

            courseBox.innerHTML = selected.length === 0
                ? "-- Select Course --"
                : selected.length <= 3
                    ? selected.join(', ')
                    : selected.length + " Courses Selected";
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="announcement-card">

        <div class="page-title">
            📢 Student Announcement Management
        </div>

        <!-- Removed ValidationSummary to avoid the "Please fix the following errors:" header and global list.
         Field-level validators will display next to each control instead. -->

        <!-- Row 1 -->
        <div class="row-flex">
            <div>
                <label class="form-label">Collage</label>
                <asp:DropDownList ID="drpCollage" runat="server"
                    CssClass="form-input"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="drpCollage_SelectedIndexChanged"
                    AppendDataBoundItems="true">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvCollege" runat="server"
                    ControlToValidate="drpCollage"
                    InitialValue=""
                    ErrorMessage="Please select college."
                    Display="Dynamic"
                    CssClass="text-danger" ValidationGroup="xyz1" />
            </div>

            <div>
                <label class="form-label">Course</label>
                <div id="courseBox" class="multiSelectBox">-- Select Course --</div>

                <asp:Panel ID="pnlCourse" runat="server" CssClass="multiSelectPanel">
                    <div class="selectAll">
                        <input type="checkbox" id="chkSelectAll" onclick="toggleSelectAll(this)" />
                        Select All
               
                    </div>
                    <asp:CheckBoxList ID="drpCourse" runat="server"
                        onclick="updateCourseText()" />
                </asp:Panel>

                <asp:CustomValidator ID="cvCourse" runat="server"
                    ErrorMessage="Please select at least one course."
                    Display="Dynamic"
                    CssClass="text-danger"
                    ClientValidationFunction="validateCourse"
                    OnServerValidate="cvCourse_ServerValidate" ValidationGroup="xyz1" />
            </div>

            <div>
                <label class="form-label">Semester</label>
                <asp:DropDownList ID="drpSemester" runat="server" CssClass="form-input">
                    <asp:ListItem Text="-- Semester --" Value="" />
                    <asp:ListItem Text="Sem I" Value="I" />
                    <asp:ListItem Text="Sem II" Value="II" />
                    <asp:ListItem Text="Sem III" Value="III" />
                    <asp:ListItem Text="Sem IV" Value="IV" />
                    <asp:ListItem Text="Sem V" Value="V" />
                    <asp:ListItem Text="Sem VI" Value="VI" />
                    <asp:ListItem Text="Sem VII" Value="VII" />
                    <asp:ListItem Text="Sem VIII" Value="VIII" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvSemester" runat="server"
                    ControlToValidate="drpSemester" ValidationGroup="xyz1"
                    InitialValue=""
                    ErrorMessage="Please select semester."
                    Display="Dynamic"
                    CssClass="text-danger" />
            </div>
        </div>

        <!-- Title -->
        <div class="row-flex">
            <div style="flex: 1">
                <label class="form-label">Title</label>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-input" Width="100%" />
                <asp:RequiredFieldValidator ID="rfvTitle" runat="server"
                    ControlToValidate="txtTitle" ValidationGroup="xyz1"
                    ErrorMessage="Please enter title."
                    Display="Dynamic"
                    CssClass="text-danger" />
            </div>
        </div>

        <!-- Description -->
        <div class="row-flex">
            <div style="flex: 1">
                <label class="form-label">Description</label>
                <asp:TextBox ID="txtDescription" runat="server"
                    TextMode="MultiLine" CssClass="form-textarea" />
                <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ValidationGroup="xyz1"
                    ControlToValidate="txtDescription"
                    ErrorMessage="Please enter description."
                    Display="Dynamic"
                    CssClass="text-danger" />
            </div>
        </div>

        <!-- Date & File -->
        <div class="row-flex">
            <div>
                <label class="form-label">Announcement Date</label>
                <asp:TextBox ID="txtDate" runat="server"
                    TextMode="Date" CssClass="form-input" />
                <asp:RequiredFieldValidator ID="rfvDate" runat="server" ValidationGroup="xyz1"
                    ControlToValidate="txtDate"
                    ErrorMessage="Please select announcement date."
                    Display="Dynamic"
                    CssClass="text-danger" />
            </div>

            <div>
                <label class="form-label">Upload File</label>
                <asp:FileUpload ID="fuAttachment" runat="server" />
                <asp:CustomValidator ID="cvFile" runat="server" ValidationGroup="xyz1"
                    ErrorMessage="Please upload a file."
                    Display="Dynamic"
                    CssClass="text-danger"
                    ClientValidationFunction="validateFile"
                    OnServerValidate="cvFile_ServerValidate" />
            </div>
        </div>

        <!-- Hidden for edit -->
        <asp:HiddenField ID="hfAnnouncementID" runat="server" />

        <!-- Buttons -->
        <div style="margin-top: 30px;">
            <asp:Button ID="btnSave" runat="server" ValidationGroup="xyz1"
                Text="Save Announcement"
                CssClass="btn btn-primary save-btn"
                OnClick="btnSave_Click" />

            <asp:Button ID="btnUpdate" runat="server" ValidationGroup="xyz1"
                Text="Update Announcement"
                CssClass="btn btn-warning save-btn"
                OnClick="btnUpdate_Click"
                Visible="false" />
        </div>

    </div>

    <div class="announcement-card" style="margin-top: 20px;">
        <div class="page-title">📋 Announcement List</div>
        <div style="max-width: 950px; margin: 20px auto;">
            <asp:GridView ID="gvAnnouncement" runat="server"
                CssClass="table table-striped table-bordered"
                AutoGenerateColumns="False"
                EmptyDataText="No announcements"
                DataKeyNames="AnnouncementID"
                OnRowCommand="gvAnnouncement_RowCommand">
                <Columns>
                    <asp:BoundField DataField="AnnouncementDate" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Title" HeaderText="Title" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="CourseIds" HeaderText="Course" />
                    <asp:TemplateField HeaderText="File">
                        <ItemTemplate>
                            <asp:HyperLink ID="hl" runat="server"
                                Text="View"
                                NavigateUrl='<%# Eval("FilePath") %>'
                                Target="_blank" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit"
                                CssClass="gv-action-btn"
                                CommandName="EditAnn"
                                CausesValidation="false"
                                CommandArgument='<%# Eval("AnnouncementID") %>' />
                            &nbsp;|&nbsp;
                   
                        <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete"
                            CssClass="gv-action-btn gv-action-btn-danger"
                            CommandName="DeleteAnn"
                            CausesValidation="false"
                            CommandArgument='<%# Eval("AnnouncementID") %>'
                            OnClientClick="return confirm('Are you sure you want to delete this announcement?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <script type="text/javascript">

            var courseBox = document.getElementById('courseBox');
            var coursePanel = document.getElementById('<%= pnlCourse.ClientID %>');

            courseBox.onclick = function (e) {
                e.stopPropagation();
                coursePanel.style.display =
                    (coursePanel.style.display === "block") ? "none" : "block";
            };

            coursePanel.onclick = function (e) {
                e.stopPropagation();
            };

            document.onclick = function () {
                coursePanel.style.display = "none";
            };

            function toggleSelectAll(source) {
                var checkboxes = document.querySelectorAll(
            '#<%= drpCourse.ClientID %> input[type=checkbox]');
                checkboxes.forEach(cb => cb.checked = source.checked);
                updateCourseText();
            }

            function updateCourseText() {
                var selected = [];
                var checkboxes = document.querySelectorAll(
            '#<%= drpCourse.ClientID %> input[type=checkbox]');

                checkboxes.forEach(cb => {
                    if (cb.checked)
                        selected.push(cb.nextSibling.textContent.trim());
                });

                document.getElementById('chkSelectAll').checked =
                    (selected.length === checkboxes.length);

                courseBox.innerHTML = selected.length === 0
                    ? "-- Select Course --"
                    : selected.length <= 2
                        ? selected.join(', ')
                        : selected.length + " Courses Selected";
            }

            // Client validator for course: returns true if at least one checkbox is checked
            function validateCourse(sender, args) {
                var checkboxes = document.querySelectorAll(
            '#<%= drpCourse.ClientID %> input[type=checkbox]');
                var any = false;
                checkboxes.forEach(function (cb) { if (cb.checked) any = true; });
                args.IsValid = any;
            }

            // Client validator for file: if editing (hfAnnouncementID has value) it's allowed to be empty.
            function validateFile(sender, args) {
                var hf = document.getElementById('<%= hfAnnouncementID.ClientID %>');
                var fileInput = document.getElementById('<%= fuAttachment.ClientID %>');
                if (hf && hf.value && hf.value.trim().length > 0) {
                    // editing - file optional
                    args.IsValid = true;
                    return;
                }
                args.IsValid = fileInput && fileInput.value && fileInput.value.trim().length > 0;
            }

</script>
</asp:Content>
