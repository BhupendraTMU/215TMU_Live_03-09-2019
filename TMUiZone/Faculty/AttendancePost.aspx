<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="AttendancePost.aspx.cs" Inherits="Faculty_AttendancePost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        body {
            background: #f4f7fb;
        }

        .card {
            border-radius: 12px;
        }

        .card-header {
            font-size: 18px;
            font-weight: 600;
        }

        .form-control,
        .form-select {
            height: 45px;
            border-radius: 8px;
        }

        .btn {
            height: 45px;
            border-radius: 8px;
            font-weight: 600;
        }

        .summary-card {
            border: none;
            transition: .3s;
        }

            .summary-card:hover {
                transform: translateY(-4px);
            }

        .table th {
            background: #fba90a;
            color: white;
            text-align: center;
            vertical-align: middle;
        }

        .table td {
            text-align: center;
            vertical-align: middle;
        }

        .table tbody tr:hover {
            background: #eef5ff;
        }

        .badge {
            padding: 8px 15px;
            font-size: 13px;
        }

        .attendance-header {
            background: linear-gradient(90deg,#fba90a,#0b5ed7);
            color: white;
            padding: 18px 25px;
        }

            .attendance-header h3 {
                margin: 0;
                font-size: 28px;
                font-weight: 600;
            }

            .attendance-header p {
                margin: 0;
                opacity: .9;
            }

        .summary-box {
            border-radius: 10px;
            padding: 20px;
            color: #fff;
            box-shadow: 0 2px 8px rgba(0,0,0,.1);
        }

            .summary-box h6 {
                margin-bottom: 10px;
                font-weight: 600;
            }

            .summary-box h2 {
                margin: 0;
                font-weight: bold;
            }

        .total {
            background: #0d6efd;
        }

        .present {
            background: #198754;
        }

        .absent {
            background: #dc3545;
        }

        .percent {
            background: #fd7e14;
        }

        .form-control,
        .form-select {
            height: 45px;
        }

        .btn {
            height: 45px;
            font-weight: 600;
        }

        .search-panel .form-label {
            display: block;
            margin-bottom: 6px;
            font-size: 14px;
            font-weight: 600;
            color: #495057;
        }

        .search-panel .form-control,
        .search-panel .form-select {
            height: 42px;
            border-radius: 6px;
            width: 100%;
        }

        .search-panel .btn {
            height: 42px;
            border-radius: 6px;
            font-weight: 600;
        }

        .status-present {
            background: #198754;
            color: #fff;
            padding: 6px 15px;
            border-radius: 20px;
            font-weight: 600;
            display: inline-block;
            min-width: 80px;
            text-align: center;
        }

        .status-absent {
            background: #dc3545;
            color: #fff;
            padding: 6px 15px;
            border-radius: 20px;
            font-weight: 600;
            display: inline-block;
            min-width: 80px;
            text-align: center;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container-fluid mt-4">

        <div class="card shadow-lg border-0 mb-4">

            <!-- Header -->
            <div class="row search-panel align-items-end">

                <div class="col-md-1">
                    <label class="form-label">A/C Year</label>
                    <asp:DropDownList ID="ddlAcademicYear"
                        runat="server"
                        CssClass="form-select">
                    </asp:DropDownList>
                </div>

                <div class="col-md-2">
                    <label class="form-label">Program</label>
                    <asp:DropDownList ID="ddlCourse"
                        runat="server"
                        CssClass="form-select">
                    </asp:DropDownList>
                </div>

                <div class="col-md-2">
                    <label class="form-label">Year</label>
                    <asp:DropDownList ID="ddlYear"
                        runat="server"
                        CssClass="form-select">
                        <asp:ListItem Text="YEAR 1" Value="YEAR 1"></asp:ListItem>
                         <asp:ListItem Text="YEAR 2" Value="YEAR 2"></asp:ListItem>
                         <asp:ListItem Text="YEAR 3" Value="YEAR 3"></asp:ListItem>
                         <asp:ListItem Text="YEAR 4" Value="YEAR 4"></asp:ListItem>
                         <asp:ListItem Text="I" Value="I"></asp:ListItem>
                         <asp:ListItem Text="II" Value="II"></asp:ListItem>
                         <asp:ListItem Text="III" Value="III"></asp:ListItem>
                         <asp:ListItem Text="IV" Value="IV"></asp:ListItem>
                         <asp:ListItem Text="V" Value="V"></asp:ListItem>
                         <asp:ListItem Text="VI" Value="VI"></asp:ListItem>
                         <asp:ListItem Text="VII" Value="VII"></asp:ListItem>
                         <asp:ListItem Text="VIII" Value="VIII"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label class="form-label">Date</label>
                    <asp:TextBox ID="txtDate"
                        runat="server"
                        TextMode="Date" OnTextChanged="txtDate_TextChanged" AutoPostBack="True"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>
                <div class="col-md-2">
                    <label class="form-label">Course</label>
                    <asp:DropDownList ID="drpCourse"
                        runat="server"
                        CssClass="form-select">
                        <asp:ListItem Text="--Select--"></asp:ListItem>
                    </asp:DropDownList>

                </div>
                <div class="col-md-1">
                    <label class="form-label">Lecture</label>
                    <asp:DropDownList ID="DropDownList1"
                        runat="server"
                        CssClass="form-select">
                        <asp:ListItem Text="Lect 1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Lect 2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Lect 3" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Lect 4" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Lect 5" Value="5"></asp:ListItem>
                        <asp:ListItem Text="Lect 6" Value="6"></asp:ListItem>
                        <asp:ListItem Text="Lect 7" Value="7"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-md-2">
                    <label class="form-label">&nbsp;</label>
                    <asp:Button ID="btnSearch"
                        runat="server"
                        Text="🔍 Search"
                        CssClass="btn btn-primary w-100"
                        OnClick="btnSearch_Click" />
                </div>

            </div>

            <hr />

            <!-- Summary -->

            <div class="row text-center">

                <div class="col-md-3 mb-3">

                    <div class="summary-box total">

                        <h6>Total Students</h6>

                        <h2>
                            <asp:Label ID="lblTotal" runat="server"></asp:Label></h2>

                    </div>

                </div>

                <div class="col-md-3 mb-3">

                    <div class="summary-box present">

                        <h6>Present</h6>

                        <h2>
                            <asp:Label ID="lblPresent" runat="server"></asp:Label></h2>

                    </div>

                </div>

                <div class="col-md-3 mb-3">

                    <div class="summary-box absent">

                        <h6>Absent</h6>

                        <h2>
                            <asp:Label ID="lblAbsent" runat="server"></asp:Label></h2>

                    </div>

                </div>

                <div class="col-md-3 mb-3">



                    <h6>
                        <asp:Button ID="btnPost" runat="server" CssClass="btn btn-primary w-100" Text="Attenmdance Post" OnClick="btnPost_Click" /></h6>





                </div>
            </div>

        </div>

    </div>



    <!-- Grid -->

    <div class="card shadow border-0">



        <div class="card-body">

            <div class="table-responsive">

                <asp:GridView ID="grdAttendance"
                    runat="server"
                    AutoGenerateColumns="False"
                    CssClass="table table-striped table-hover table-bordered align-middle">

                    <Columns>

                        <asp:BoundField DataField="Enrollment No"
                            HeaderText="Enrollment No" />

                        <asp:BoundField DataField="Student Name"
                            HeaderText="Student Name" />

                        <asp:BoundField DataField="Subject Code"
                            HeaderText="Subject Code" />

                        <asp:BoundField DataField="Description"
                            HeaderText="Subject Name" />

                        <asp:BoundField DataField="LectureNo"
                            HeaderText="Lecture" />
                        <asp:BoundField DataField="student_group"
                            HeaderText="Group" />
                        <asp:BoundField DataField="StartTime"
                            HeaderText="Start Time" />

                        <asp:BoundField DataField="EndTime"
                            HeaderText="End Time" />

                        <asp:TemplateField HeaderText="Attendance">
                            <ItemTemplate>

                                <asp:Label ID="lblAttendance"
                                    runat="server"
                                    Text='<%# Eval("AttendanceStatus") %>'
                                    CssClass='<%# Eval("AttendanceStatus").ToString().Equals("Present", StringComparison.OrdinalIgnoreCase) ? "status-present" : "status-absent" %>'>
                                </asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>

            </div>

        </div>

    </div>








</asp:Content>

