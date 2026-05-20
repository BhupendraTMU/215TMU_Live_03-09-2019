<%@ Page Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="NEPExistsReport.aspx.cs" EnableEventValidation="false" Inherits="Faculty_NEPExistsReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        /* Grid wrapper to allow horizontal scroll for wide tables */
        .gv-wrapper { overflow-x: auto; width: 100%; padding: 0 5%; }
        /* Ensure long text wraps inside cells */
        .gv-table td, .gv-table th { white-space: normal; word-wrap: break-word; }
        /* Minimum width for the generated table so scrolling appears when needed */
        .gv-table { min-width: 1400px; }
        /* Smaller padding for cells to fit more columns */
        .gv-table th, .gv-table td { padding: 6px 8px; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="panel panel-default" style="background:transparent; border:none;">
        <div class="panel-heading" style="background-color:transparent; padding:0 5%;">
            <h3 style="margin:0;">NEP Exists Report</h3>
        </div>
        <div class="panel-body" style="padding:15px 5%;">
            <fieldset class="boxBodyInner" style="padding:10px 15px">
                <div class="row tmu-form">
                    <div class="col-sm-4">
                        <div class="form-group clearfix">
                            <label class="col-form-label">Enrollment No</label>
                            <div>
                                <asp:TextBox ID="txtEnrollment" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <asp:Button ID="btnGet" runat="server" Text="Get" CssClass="btn btn-default" OnClick="btnGet_Click" />
                    </div>
                    <div class="col-sm-2">
                        <asp:Button ID="btnExport" runat="server" Text="Export to Excel" CssClass="btn btn-default" OnClick="btnExport_Click" />
                    </div>
                </div>
            </fieldset>

            <br />
            <div class="gv-wrapper">
                <asp:GridView ID="gvNEP" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvNEP_PageIndexChanging" CssClass="gv-table table table-striped table-bordered table-hover" ShowHeaderWhenEmpty="false" Style="margin:0 auto;">
        <Columns>
            <asp:TemplateField HeaderText="Enrollment No">
                <ItemTemplate>
                    <asp:Label ID="lblEnrollmentNo" runat="server" Text='<%# Eval("EnrollmentNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="NEP Student Name">
                <ItemTemplate>
                    <asp:Label ID="lblNEPName" runat="server" Text='<%# Eval("StudentName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Admission Session">
                <ItemTemplate>
                    <asp:Label ID="lblAdmissionSession" runat="server" Text='<%# Eval("AdmissionSession") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Option Selected">
                <ItemTemplate>
                    <asp:Label ID="lblOptionSelected" runat="server" Text='<%# Eval("OptionSelected") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Course Name">
                <ItemTemplate>
                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("CourseName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Department">
                <ItemTemplate>
                    <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("Department") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="College">
                <ItemTemplate>
                    <asp:Label ID="lblCollege" runat="server" Text='<%# Eval("College") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Semester">
                <ItemTemplate>
                    <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("Semester") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Section">
                <ItemTemplate>
                    <asp:Label ID="lblSection" runat="server" Text='<%# Eval("Section") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Full Name">
                <ItemTemplate>
                    <asp:Label ID="lblFullName" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Father Name">
                <ItemTemplate>
                    <asp:Label ID="lblFatherName" runat="server" Text='<%# Eval("FatherName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Mother Name">
                <ItemTemplate>
                    <asp:Label ID="lblMotherName" runat="server" Text='<%# Eval("MotherName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Mobile No">
                <ItemTemplate>
                    <asp:Label ID="lblMobile" runat="server" Text='<%# Eval("MobileNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Email Id">
                <ItemTemplate>
                    <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("EmailId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Gender">
                <ItemTemplate>
                    <asp:Label ID="lblGender" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="DOB">
                <ItemTemplate>
                    <asp:Label ID="lblDOB" runat="server" Text='<%# Eval("DOB") == DBNull.Value ? "" : String.Format("{0:dd-MMM-yyyy}", Eval("DOB")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="City">
                <ItemTemplate>
                    <asp:Label ID="lblCity" runat="server" Text='<%# Eval("City") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="State">
                <ItemTemplate>
                    <asp:Label ID="lblState" runat="server" Text='<%# Eval("State") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Student Status">
                <ItemTemplate>
                    <asp:Label ID="lblStuStatus" runat="server" Text='<%# Eval("Student Status") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Session">
                <ItemTemplate>
                    <asp:Label ID="lblSession" runat="server" Text='<%# Eval("Session") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
                    <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
