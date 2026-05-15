<%@ Page Title="Attendance Undertaking Form" Language="C#" AutoEventWireup="true" CodeFile="FA_MM_Attendance_Form.aspx.cs" Inherits="Attendance_Form" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <title>Attendance Undertaking Form</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .card {
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
        }
        .btn-custom {
            width: 200px;
            margin-top: 10px;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div class="container mt-5 p-4" style="background-color: #f9f9f9; border-radius: 10px; padding-top:20px">
            <h2 class="text-center mb-4" style="color: #007bff;">Attendance Undertaking</h2>
            <br />
            <br />
            <!-- Student Information Section -->
            <div class="student-info mb-4">

               
                <div class="row">
                    <div class="col-md-3 text-center">
                        <label class="font-weight-bold">Academic Year:</label>
                        <%--<asp:DropDownList ID="lbl_AcademicYear" OnSelectedIndexChanged="lbl_AcademicYear_SelectedIndexChanged" runat="server"></asp:DropDownList>--%>
                       
                         <asp:Label ID="lbl_AcademicYear" CssClass="form-label" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="col-md-3 text-center">
                        <label class="font-weight-bold">Semester:</label>
                        <asp:DropDownList ID="ddl_Semster" AutoPostBack="true" OnSelectedIndexChanged="ddl_Semster_SelectedIndexChanged" runat="server"></asp:DropDownList>
                       
                         <%--<asp:Label ID="" CssClass="form-label" runat="server" Text=""></asp:Label>--%>
                    </div>
                    <div class="col-md-3 text-center">
                        <label class="font-weight-bold">Program:</label>
                        <asp:Label ID="lbl_Program" CssClass="form-label" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lbl_Course" CssClass="form-label" Visible="false" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="col-md-3 text-center">
                        <label class="font-weight-bold">Student Id:</label>
                        <asp:Label ID="lbl_AutoNo" Visible="false" CssClass="form-label" runat="server" Text=""></asp:Label>
                        
                        <asp:Label ID="lbl_MentorId" CssClass="form-label" Visible="false" runat="server" Text=""></asp:Label>
                        
                        <asp:Label ID="lbl_StudentName" CssClass="form-label" runat="server" Text=""></asp:Label>

                    </div>
                     <div style="padding-top:40px;text-align:center">
                   <h3>
                     <asp:Label ID="lbl_NoRecords" runat="server" Visible="false" Text="There were no records found."></asp:Label>

                   </h3>
                </div>
                </div>
            </div>
           <%--  <div class="row">
                     <h4 class="text-center mb-4"><u>Instructions</u></h4>
                <p class="text-center mb-4">Please download your attendance undertaking form, fill it out, and re-upload it below.</p>

               
                <!-- Download Button -->
                <div class="text-center mb-3" style="padding-bottom:20px;font-weight:bold">
                    <asp:LinkButton ID="Button1" OnCommand="btnPrint_Command" CommandArgument='<%#Bind("ID") %>'  runat="server" Text="Click Here To Download Attendance Undertaking Form" OnClick="btn_Print_Click"
                      />
                </div>
                </div>--%>
            <asp:Panel ID="pnl_Attendence_Upload" Visible="false" runat="server">
            <!-- Card for Attendance Undertaking Section -->
            <div class="card p-4" style="background-color: white;">
                <h4 class="text-center mb-4"><u>Instructions</u></h4>
                <p class="text-center mb-4">Please download your attendance undertaking form, fill it out, and re-upload it below.</p>

               
                <!-- Download Button -->
                <div class="text-center mb-3" style="padding-bottom:20px">
                    <asp:Button ID="btn_Print" OnCommand="btnPrint_Command" CommandArgument='<%#Bind("ID") %>'  runat="server" Text="Download Form" CssClass="btn btn-primary btn-custom" OnClick="btn_Print_Click"
                      />
                </div>

                <!-- File Upload Section -->
                <div class="text-center mb-3">
                    <asp:FileUpload ID="fu_AttendancePdf" runat="server" CssClass="form-control" AllowMultiple="false" style="max-width: 400px; margin: auto;" />
                </div>

                <!-- Upload Button -->
                <div class="text-center" style="padding-top:15px">
                    <asp:Button ID="btn_Upload" OnClick="UploadFile_Click" runat="server" Text="Upload Filled Form" CssClass="btn btn-success btn-custom" />
                </div>
            </div>
                </asp:Panel>
            <asp:Panel ID="pnl_Attendence_View" Visible="false" runat="server">
                 <div class="text-center mb-3" style="padding-bottom: 20px">
                                    <asp:Button ID="btn_View_" OnClick="btn_View__Click" runat="server" Text="View / Download Form" CssClass="btn btn-primary btn-custom" />
               
                                <div class="text-center" style="padding-top: 15px">
                                    <asp:Button ID="btn_Delete" OnClick="btn_Delete_Click" runat="server" Text="Delete Form" CssClass="btn btn-success btn-custom" />
                                </div>
                         </div>

            </asp:Panel>

        </div>
        
        <!-- Add jQuery and Bootstrap JS if not included globally -->
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    </form>
</body>
</html>