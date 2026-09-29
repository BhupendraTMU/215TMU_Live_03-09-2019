<%@ Page Title="Dental Faculty Punch Report"
    Language="C#"
    MasterPageFile="~/Faculty/IndexMaster.master"
    AutoEventWireup="true"
    CodeFile="DentalFacultyPunch.aspx.cs"
    Inherits="Faculty_DentalFacultyPunch" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <!-- Bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css"
        rel="stylesheet" />

    <!-- Font Awesome -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.2/css/all.min.css"
        rel="stylesheet" />

    <style>
        .page-header {
            background: linear-gradient(135deg, #f59e0b, #d97706);
            color: white;
            padding: 15px 20px;
            border-radius: 10px;
            margin-bottom: 20px;
        }

        /* Report Table Header */
        .attendance-table th {
            background-color: #f59e0b !important;
            color: #ffffff !important;
            text-align: center;
            vertical-align: middle;
            border-color: #d97706 !important;
        }

        /* Report Card Header */
        .report-card .card-header {
            background-color: #fff8e1 !important;
            border-bottom: 1px solid #f5d78e;
        }

       
        /* Search Button */
        .btn-search {
            background-color: #f59e0b !important;
            border-color: #f59e0b !important;
        }

            .btn-search:hover {
                background-color: #d97706 !important;
                border-color: #d97706 !important;
            }

        /* Filter Card slight yellow shade */
        .filter-card {
            border: 0;
            border-radius: 12px;
            box-shadow: 0 2px 10px rgba(0,0,0,.08);
            border-top: 3px solid #f59e0b;
        }

        /* Table */
        .attendance-table td {
            text-align: center;
            vertical-align: middle;
        }

        .attendance-table tbody tr:hover {
            background-color: #fff8e1 !important;
        }
    </style>

    <div class="container-fluid px-2 px-md-3">

        <!-- Page Header -->
        <div class="page-header">
            <div class="d-flex align-items-center">
                <i class="fa-solid fa-user-clock fs-4 me-3"></i>

                <div>
                    <h5 class="mb-0">Dental Faculty Punch Report
                    </h5>

                    <small>Faculty Wise Machine Punch Attendance
                    </small>
                </div>
            </div>
        </div>


        <!-- Filter -->
        <div class="card filter-card mb-3">

            <div class="card-body">

                <div class="row g-3 align-items-end">

                    <div class="col-md-4 col-lg-3">

                        <label class="form-label fw-semibold">
                            <i class="fa-regular fa-calendar me-1"></i>
                            Select Date
                        </label>

                        <asp:TextBox
                            ID="txtDate"
                            runat="server"
                            CssClass="form-control"
                            TextMode="Date">
                        </asp:TextBox>

                    </div>


                    <div class="col-md-3 col-lg-2">

                        <asp:Button
                            ID="btnSearch"
                            runat="server"
                            Text="Search"
                            CssClass="btn btn-primary btn-search"
                            OnClick="btnSearch_Click" />

                    </div>


                    <div class="col-md-5 col-lg-7 text-md-end">

                        <asp:Label
                            ID="lblMessage"
                            runat="server"
                            CssClass="text-danger fw-semibold">
                        </asp:Label>

                    </div>

                </div>

            </div>

        </div>


        <!-- Report -->
        <div class="card report-card">

            <div class="card-header bg-white">

                <div class="d-flex justify-content-between align-items-center">

                    <h6 class="mb-0 fw-bold">
                        <i class="fa-solid fa-table-list me-2 text-primary"></i>
                        Faculty Punch Details
                    </h6>

                    <asp:Label
                        ID="lblReportDate"
                        runat="server"
                        CssClass="badge bg-primary">
                    </asp:Label>

                </div>

            </div>


            <div class="card-body p-2">

                <div class="table-responsive">

                    <asp:GridView
                        ID="gvFacultyPunch"
                        runat="server"
                        AutoGenerateColumns="true"
                        CssClass="table table-bordered table-hover attendance-table mb-0"
                        EmptyDataText="No punch record found."
                        GridLines="None">

                        <HeaderStyle
                            CssClass="table-primary text-center" />

                    </asp:GridView>

                </div>

            </div>

        </div>

    </div>

</asp:Content>
