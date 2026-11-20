<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master"
    AutoEventWireup="true" CodeFile="UploadCo.aspx.cs"
    Inherits="Faculty_UploadCo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <title>Co-Leave Application</title>

    <meta name="viewport"
        content="width=device-width, initial-scale=1.0" />

    <script type="text/javascript">

        function SelectAll(source) {

            var grid = document.getElementById(
                '<%= gvExcel.ClientID %>'
            );

            if (!grid) return;

            var checkboxes = grid.querySelectorAll(
                'input[type="checkbox"]'
            );

            for (var i = 0; i < checkboxes.length; i++) {

                if (checkboxes[i] != source) {
                    checkboxes[i].checked = source.checked;
                }
            }
        }


        function SelectAllChecker(source) {

            var grid = document.getElementById(
                '<%= gvChecker.ClientID %>'
            );

            if (!grid) return;

            var checkboxes = grid.querySelectorAll(
                'input[type="checkbox"]'
            );

            for (var i = 0; i < checkboxes.length; i++) {

                if (checkboxes[i] != source) {
                    checkboxes[i].checked = source.checked;
                }
            }
        }


        function confirmReject() {

            var remark =
                document.getElementById(
                    '<%= txtRejectRemark.ClientID %>'
                ).value.trim();

            if (remark == "") {

                alert("Please enter rejection remark.");

                return false;
            }

            return confirm(
                "Are you sure you want to reject selected records?"
            );
        }


        function confirmApprove() {

            return confirm(
                "Are you sure you want to approve selected records?"
            );
        }

    </script>


    <style>

        body {
            font-family: Arial, sans-serif;
            background: #f5f6fa;
            margin: 0;
            padding: 30px;
        }


        .container {
            max-width: 1200px;
            margin: auto;
            background: white;
            padding: 25px;
            border-radius: 10px;
            box-shadow: 0 2px 10px rgba(0,0,0,.12);
        }


        .title {
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 20px;
        }


        .panel-title {
            font-size: 20px;
            font-weight: bold;
            padding: 12px 15px;
            margin-bottom: 20px;
            border-radius: 6px;
            background: #343a40;
            color: white;
        }


        .upload-box {
            border: 1px solid #ddd;
            padding: 20px;
            border-radius: 8px;
            background: #fafafa;
        }


        .btn {
            padding: 9px 18px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            margin-left: 5px;
            color: white;
        }


        .btn-upload {
            background: #007bff;
        }


        .btn-save {
            background: #28a745;
        }


        .btn-approve {
            background: #28a745;
        }


        .btn-reject {
            background: #dc3545;
        }


        .message {
            display: block;
            margin-top: 15px;
            font-weight: bold;
        }


        .grid {
            width: 100%;
            margin-top: 25px;
            border-collapse: collapse;
        }


        .grid th {
            background: #343a40;
            color: white;
            padding: 10px;
            text-align: center;
        }


        .grid td {
            padding: 8px;
            border: 1px solid #ddd;
        }


        .grid tr:nth-child(even) {
            background: #f8f9fa;
        }


        .success {
            color: green;
        }


        .error {
            color: red;
        }


        .checker-actions {
            margin-top: 20px;
            padding: 20px;
            background: #f8f9fa;
            border: 1px solid #ddd;
            border-radius: 8px;
        }


        .remark-label {
            font-weight: bold;
            display: block;
            margin-bottom: 8px;
        }


        .remark-box {
            width: 100%;
            max-width: 600px;
            height: 70px;
            padding: 8px;
            border: 1px solid #ccc;
            border-radius: 5px;
            resize: vertical;
        }


        .status-pending {
            color: #d39e00;
            font-weight: bold;
        }


        .status-approved {
            color: green;
            font-weight: bold;
        }


        .status-rejected {
            color: red;
            font-weight: bold;
        }


        .section-space {
            margin-top: 30px;
        }


        @media(max-width:768px) {

            body {
                padding: 10px;
            }

            .container {
                padding: 15px;
            }

            .grid {
                font-size: 12px;
            }

            .grid th,
            .grid td {
                padding: 6px;
                white-space: nowrap;
            }

        }

    </style>

</asp:Content>


<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="Server">


    <div class="container">


        <div class="title">
            Co-Leave Application
        </div>


        <!-- =====================================================
             MAKER PANEL
             TMU05573 / TMU05574
        ====================================================== -->

        <asp:Panel ID="pnlMaker"
            runat="server"
            Visible="false">


            <div class="panel-title">
                Maker - Co-Leave Excel Upload
            </div>


            <div class="upload-box">


                <asp:FileUpload
                    ID="fuExcel"
                    runat="server" />


                <asp:Button
                    ID="btnPreview"
                    runat="server"
                    Text="Preview Excel"
                    CssClass="btn btn-upload"
                    OnClick="btnPreview_Click" />


                <asp:Button
                    ID="btnSave"
                    runat="server"
                    Text="Submit for Approval"
                    CssClass="btn btn-save"
                    Visible="false"
                    OnClick="btnSave_Click"
                    OnClientClick="return confirm('Are you sure you want to submit selected records for approval?');" />


                <asp:Label
                    ID="lblMessage"
                    runat="server"
                    CssClass="message">
                </asp:Label>


            </div>


            <asp:GridView
                ID="gvExcel"
                runat="server"
                CssClass="grid"
                AutoGenerateColumns="false"
                EmptyDataText="No records found.">


                <Columns>


                    <asp:BoundField
                        DataField="Employee Code"
                        HeaderText="Employee Code" />


                    <asp:BoundField
                        DataField="Employee Name"
                        HeaderText="Employee Name" />


                    <asp:BoundField
                        DataField="Atte_Date"
                        HeaderText="Attendance Date"
                        DataFormatString="{0:dd-MM-yyyy}" />


                    <asp:BoundField
                        DataField="Remarks"
                        HeaderText="Remarks" />


                    <asp:BoundField
                        DataField="Purpose"
                        HeaderText="Purpose" />


                    <asp:TemplateField
                        HeaderText="Select">


                        <HeaderTemplate>

                            <asp:CheckBox
                                ID="chkAll"
                                runat="server"
                                onclick="SelectAll(this);"
                                Checked="true" />

                        </HeaderTemplate>


                        <ItemTemplate>

                            <asp:CheckBox
                                ID="chkSelect"
                                runat="server"
                                Checked="true" />

                        </ItemTemplate>


                    </asp:TemplateField>


                </Columns>


            </asp:GridView>


        </asp:Panel>


        <!-- =====================================================
             CHECKER PANEL
             TMU00049
        ====================================================== -->

        <asp:Panel ID="pnlChecker"
            runat="server"
            Visible="false"
            CssClass="section-space">


            <div class="panel-title">
                Checker - Co-Leave Approval
            </div>


            <asp:Label
                ID="lblCheckerMessage"
                runat="server"
                CssClass="message">
            </asp:Label>


            <asp:GridView
                ID="gvChecker"
                runat="server"
                CssClass="grid"
                AutoGenerateColumns="false"
                EmptyDataText="No Pending Applications Found.">


                <Columns>


                    <asp:BoundField
                        DataField="ID"
                        HeaderText="ID" />


                    <asp:BoundField
                        DataField="Userid"
                        HeaderText="Employee Code" />


                    <asp:BoundField
                        DataField="Uname"
                        HeaderText="Employee Name" />


                    <asp:BoundField
                        DataField="Atte_Date"
                        HeaderText="Attendance Date"
                        DataFormatString="{0:dd-MM-yyyy}" />


                    <asp:BoundField
                        DataField="Remarks"
                        HeaderText="Remarks" />


                    <asp:BoundField
                        DataField="Purpose"
                        HeaderText="Purpose" />


                    <asp:BoundField
                        DataField="ApplyStaff"
                        HeaderText="Maker" />


                    <asp:BoundField
                        DataField="CreatedDate"
                        HeaderText="Submitted On"
                        DataFormatString="{0:dd-MM-yyyy HH:mm}" />


                    <asp:TemplateField
                        HeaderText="Select">


                        <HeaderTemplate>

                            <asp:CheckBox
                                ID="chkAllChecker"
                                runat="server"
                                onclick="SelectAllChecker(this);" />

                        </HeaderTemplate>


                        <ItemTemplate>

                            <asp:CheckBox
                                ID="chkChecker"
                                runat="server" />

                        </ItemTemplate>


                    </asp:TemplateField>


                </Columns>


            </asp:GridView>


            <div class="checker-actions">


                <span class="remark-label">
                    Rejection Remark
                </span>


                <asp:TextBox
                    ID="txtRejectRemark"
                    runat="server"
                    CssClass="remark-box"
                    TextMode="MultiLine"
                    placeholder="Enter rejection reason...">
                </asp:TextBox>


                <br />
                <br />


                <asp:Button
                    ID="btnApprove"
                    runat="server"
                    Text="Approve Selected"
                    CssClass="btn btn-approve"
                    OnClick="btnApprove_Click"
                    OnClientClick="return confirmApprove();" />


                <asp:Button
                    ID="btnReject"
                    runat="server"
                    Text="Reject Selected"
                    CssClass="btn btn-reject"
                    OnClick="btnReject_Click"
                    OnClientClick="return confirmReject();" />


            </div>


        </asp:Panel>


        <!-- ACCESS DENIED -->

        <asp:Panel ID="pnlAccessDenied"
            runat="server"
            Visible="false">


            <div class="panel-title">
                Access Denied
            </div>


            <asp:Label
                ID="lblAccessDenied"
                runat="server"
                CssClass="message error">
            </asp:Label>


        </asp:Panel>


    </div>


</asp:Content>