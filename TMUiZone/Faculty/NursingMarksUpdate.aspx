<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="NursingMarksUpdate.aspx.cs" Inherits="Faculty_NursingMarksUpdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="panel panel-primary">
        <div class="panel-heading" style="background-color: #ed7600;">
            <b>Update Marks</b>
        </div>

        <div class="panel-body">

            <!-- Row 1 -->
            <div class="row">

                <div class="col-md-2">
                    <label>Academic Year</label>
                    <asp:DropDownList ID="drpAcademicYear" runat="server"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="drpAcademicYear_SelectedIndexChanged" />
                </div>

                <div class="col-md-3">
                    <label>Program</label>
                    <asp:DropDownList ID="drpCourse" runat="server"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="drpCourse_SelectedIndexChanged" />
                </div>

                <div class="col-md-3">
                    <label>Semester / Year</label>
                    <asp:DropDownList ID="drpSemester" runat="server"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="drpSemester_SelectedIndexChanged" />
                </div>

                <div class="col-md-4">
                    <label>Course</label>
                    <asp:DropDownList ID="ddlSubject" runat="server"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnTextChanged="ddlSubject_TextChanged" />
                </div>

            </div>

            <br />

            <!-- Row 2 -->
            <div class="row">

                <div class="col-md-2">
                    <label>Section</label>
                    <asp:DropDownList ID="drpSection" runat="server"
                        CssClass="form-control"
                        AutoPostBack="true" />
                </div>

                <div class="col-md-2">
                    <label>Group</label>
                    <asp:DropDownList ID="ddlGroup" runat="server"
                        CssClass="form-control"
                        AutoPostBack="true" />
                </div>

                <div class="col-md-2">
                    <label>Batch</label>
                    <asp:DropDownList ID="ddlBatch" runat="server"
                        CssClass="form-control" />
                </div>

                <div class="col-md-3">
                    <label>Exam Type</label>
                    <asp:DropDownList ID="ddlexamtype" runat="server" AutoPostBack="true" OnTextChanged="ddlexamtype_SelectedIndexChanged"
                        CssClass="form-control">

                        <asp:ListItem Text="Select" Value="Select" Selected="True" />
                        <asp:ListItem Text="Internal" Value="0" />
                        <asp:ListItem Text="External" Value="1" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-3">
                    <label>Exam Method</label>
                    <asp:DropDownList ID="ddlexamMethod" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>
                </div>

            </div>

            <br />

            <!-- Buttons -->
            <div class="row">
                <div class="col-md-12 text-right">

                    <asp:Button ID="btnShow" runat="server"
                        Text="SHOW / BACK"
                        CssClass="btn btn-primary"
                        OnClick="btnShow_Click" />

                    &nbsp;

                <asp:Button ID="btnSave" runat="server"
                    Text="Update"
                    CssClass="btn btn-success"  OnClick="btnSave_Click" />

                </div>
            </div>
            <br />

            <div class="row">
                <div class="col-md-12">

                    <asp:GridView ID="gvStudentMarks" runat="server"
                        CssClass="table table-bordered table-striped table-hover"
                        AutoGenerateColumns="False"
                        EmptyDataText="No Record Found"  DataKeyNames="Document No_,Enrollement No,Academic Year,Course,Subject Code,Exam Method"
                        Width="100%">
                        <Columns>

                            <asp:BoundField DataField="Document No_" HeaderText="Document No" />
                            <asp:BoundField DataField="Enrollement No" HeaderText="Enrollment No" />
                            <asp:BoundField DataField="Student Name" HeaderText="Student Name" />
                            <asp:BoundField DataField="Course" HeaderText="Course" />
                            <asp:BoundField DataField="Subject Code" HeaderText="Subject Code" />
                            <asp:BoundField DataField="Exam Method" HeaderText="Exam Method" />
                            <asp:BoundField DataField="Semester" HeaderText="Semester" />
                            <asp:TemplateField HeaderText="Previous Marks">
                                <ItemTemplate>
                                    <asp:Label ID="lblPreviousMarks" runat="server"
                                        CssClass="form-control"
                                        Width="80px"
                                        Text='<%# Eval("IAMarks", "{0:0.00}") %>'>
</asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IA Marks">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtIAMarks" runat="server"
                                        CssClass="form-control"
                                        Width="80px"
                                        Text='<%# Eval("IAMarks", "{0:0.00}") %>'>
                        </asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>

                </div>
            </div>
        </div>
    </div>


</asp:Content>

