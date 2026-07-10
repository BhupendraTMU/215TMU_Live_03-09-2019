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
            <div class="row mb-3">

                <div class="col-md-3">
                    <label>Academic Year</label>
                    <asp:DropDownList ID="drpAcademicYear" runat="server"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="drpAcademicYear_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

                <div class="col-md-3">
                    <label>Exam Type</label>
                    <asp:DropDownList ID="ddlexamtype" runat="server"
                        CssClass="form-control"
                        AutoPostBack="true">
                        <asp:ListItem Text="Select" Value="" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Internal" Value="0"></asp:ListItem>
                        <asp:ListItem Text="External" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-md-3">
                    <label>Program</label>
                    <asp:DropDownList ID="drpCourse" runat="server"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="drpCourse_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

                <div class="col-md-3">
                    <label>Semester / Year</label>
                    <asp:DropDownList ID="drpSemester" runat="server"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="drpSemester_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

            </div>

            <!-- Row 2 -->
            <div class="row mb-3">



                <div class="col-md-2">
                    <label>Section</label>
                    <asp:DropDownList ID="drpSection" runat="server"
                        CssClass="form-control"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </div>

                <div class="col-md-2">
                    <label>Group</label>
                    <asp:DropDownList ID="ddlGroup" runat="server"
                        CssClass="form-control"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </div>


                <div class="col-md-4">
                    <label>Course / Subject</label>
                    <asp:DropDownList ID="ddlSubject" runat="server"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlSubject_TextChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label>Exam Method</label>
                    <asp:DropDownList ID="ddlexamMethod" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>
                </div>

            </div>

            <!-- Buttons -->
            <div class="row mb-3">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btnShow" runat="server"
                        Text="SHOW / BACK"
                        CssClass="btn btn-primary"
                        OnClick="btnShow_Click" />

                    <asp:Button ID="btnSave" runat="server"
                        Text="Update"
                        CssClass="btn btn-success"
                        OnClick="btnSave_Click"
                        Style="margin-left: 8px;" />
                </div>
            </div>

            <!-- Grid -->
            <div class="table-responsive">
                <asp:GridView ID="gvStudentMarks" runat="server"
                    CssClass="table table-bordered table-striped table-hover"
                    AutoGenerateColumns="False"
                    Width="100%"
                    EmptyDataText="No Record Found"
                    DataKeyNames="Document No_,Enrollement No,Academic Year,Course,Subject Code,Exam Method">

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
                                    CssClass="form-control text-center" MaxLength="4" 
                                    Text='<%# Eval("IAMarks").ToString() == "-1" ? "AB" : Eval("IAMarks").ToString() %>'>
        </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Marks">
                            <ItemTemplate>
                                <asp:TextBox ID="txtIAMarks" runat="server"
                                    CssClass="form-control text-center" MaxLength="4"
                                    Text='<%# Eval("IAMarks").ToString() == "-1" ? "AB" : Eval("IAMarks").ToString() %>'>
        </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>
            </div>

        </div>
    </div>


</asp:Content>

