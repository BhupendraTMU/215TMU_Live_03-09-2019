<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="FA_Mentor_For_Mentee.aspx.cs" Inherits="FA_Mentor_For_Mentee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function PrintDiv() {
            var divToPrint = document.getElementById('printarea');
            var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px');
            popupWin.document.open();

            // Grab all <link> and <style> tags from the main document and add them to the new window
            var styles = "";
            Array.from(document.querySelectorAll("link[rel='stylesheet'], style")).forEach(function (styleNode) {
                styles += styleNode.outerHTML;
            });

            popupWin.document.write(`
        <html>
            <head>
                <title>Print Preview</title>
                ${styles}
            </head>
            <body onload="window.print(); window.close();">
                ${divToPrint.innerHTML}
            </body>
        </html>
    `);
            popupWin.document.close();
        }
    </script>
    <link href="pms.css" rel="stylesheet" />
    <style>
        :root {
            /*--max-width:900px;
    --muted:#555;
    --accent:#111;
    --gap:14px;
    --signature-height:70px;*/
        }

        body {
            font-family: "Times New Roman", Georgia, serif;
            color: var(--accent);
            background: #f7f7f7;
            padding: 24px;
            -webkit-print-color-adjust: exact;
        }

        .page {
            max-width: var(--max-width);
            margin: 16px auto;
            background: white;
            padding: 28px 36px;
            box-shadow: 0 6px 18px rgba(0,0,0,0.06);
            line-height: 1.45;
        }

        header {
            display: flex;
            justify-content: space-between;
            align-items: flex-start;
            margin-bottom: 10px;
        }

        .left {
            width: 65%;
        }

        .right {
            display: flex;
            align-items: center; /* vertically center text & input */
            justify-content: flex-start; /* text on left, input on right */
            gap: 8px; /* small space between label & input */
        }

        .college {
            margin-top: 6px;
        }

        h2.subject {
            text-align: center;
            font-size: 1rem;
            margin: 18px 0 8px;
            text-decoration: underline;
            text-decoration-thickness: 1px;
        }

        p.lead {
            text-indent: 40px;
            margin: 8px -40px 12px;
        }

        .blank-line {
            display: inline-block;
            border-bottom: 1px solid #000;
            min-width: 260px;
            margin-left: 6px;
            vertical-align: middle;
            padding-bottom: 2px;
        }

        table.attendance {
            width: 100%;
            border-collapse: collapse;
            margin-top: 10px;
            margin-bottom: 14px;
            font-size: 0.95rem;
        }

            table.attendance th,
            table.attendance td {
                border: 1px solid #777;
                padding: 8px 10px;
                text-align: left;
            }

            table.attendance th {
                background: #efefef;
                font-weight: 700;
            }

        .note {
            margin-top: 8px;
            margin-bottom: 18px;
        }

        .verifications {
            display: flex;
            gap: 18px;
            flex-wrap: wrap;
            margin-top: 8px;
        }

        .sign-block2 {
            padding: 10px;
        }

        .sign-block {
            min-width: 260px;
            padding: 10px;
            width: 100%;
        }

        .sign-block1 {
            flex: 1 1 450px;
            min-width: 260px;
        }

        .sign-label {
            display: block;
            margin-top: 18px;
            font-weight: 700;
        }

        .signature {
            height: var(--signature-height);
            border-bottom: 1px solid #000;
            margin-top: 12px;
        }

        .meta {
            margin-top: 18px;
            display: grid;
            grid-template-columns: 1fr;
            gap: 8px;
            max-width: 600px;
        }

            .meta .item {
                display: flex;
                align-items: center;
                gap: 8px;
            }

                .meta .item label {
                    font-weight: bold;
                    min-width: 120px; /* adjust to your label width */
                }

                .meta .item input {
                    flex: 1;
                    padding: 4px 0;
                    border: none;
                    border-bottom: 1px solid #000;
                    background: transparent;
                    outline: none;
                }
                    /* optional: underline color change on focus */
                    .meta .item input:focus {
                        border-bottom-color: #007BFF;
                    }

        footer {
            margin-top: 18px;
            text-align: right;
            color: var(--muted);
            font-size: 0.9rem;
        }

        /* Print friendly */
        @media print {
            body {
                background: #fff;
            }

            .sheet {
                width: auto;
                min-height: auto;
                margin: 0;
                box-shadow: none;
                padding: 20mm;
            }

            .gridmargin {
                margin-left: -10px;
            }

            .scale {
                width: 100%;
            }

            .page-number:after {
                counter-increment: page;
                position: fixed;
                bottom: 0;
                left: 0;
                text-align: center;
            }
        }
    </style>
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 630px;
            border: 3px solid #0DA9D0;
            border-radius: 12px;
            padding: 0;
        }

            .modalPopup .header {
                background-color: #2FBDF1;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
            }

            .modalPopup .body {
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .footer {
                padding: 6px;
            }

            .modalPopup .yes, .modalPopup .no {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                border-radius: 4px;
            }

            .modalPopup .yes {
                background-color: #2FBDF1;
                border: 1px solid #0DA9D0;
            }

            .modalPopup .no {
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }

        /* General table style */
        .gridview {
            width: 100%;
            border-collapse: collapse;
            font-family: Arial, sans-serif;
        }

            /* Header styling */
            .gridview th {
                background-color: #ed7600; /* Oceanic Blue */
                color: white;
                font-size: 13px;
                padding: 12px;
                text-align: left;
                border: 1px solid #dddddd;
            }

            /* Row styling */
            .gridview td {
                padding: 10px;
                border: 1px solid #dddddd;
                text-align: left;
                font-size: 12px;
                color: #333;
            }

            /* Alternating row colors */
            .gridview tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            /* Hover effect */
            .gridview tr:hover {
                background-color: #d9edf7;
            }

        /* Responsive design */
        @media screen and (max-width: 768px) {
            .gridview, .gridview thead, .gridview tbody, .gridview th, .gridview td, .gridview tr {
                display: block;
                width: 100%;
            }

                .gridview th, .gridview td {
                    box-sizing: border-box;
                    text-align: right;
                    padding: 12px 8px;
                }

                .gridview td {
                    border: none;
                    border-bottom: 1px solid #dddddd;
                    text-align: right;
                }

                .gridview tr {
                    margin-bottom: 12px;
                    display: block;
                }

                .gridview thead {
                    display: none;
                }

                .gridview td:before {
                    content: attr(data-label);
                    float: left;
                    font-weight: bold;
                    color: #007bff;
                }
        }

        /* General textbox styling */
        .textbox {
            width: 100%;
            padding: 5px 7px;
            border: 1px solid #ccc;
            border-radius: 4px;
            font-size: 13px;
            color: #333;
            box-sizing: border-box;
            transition: border-color 0.3s ease-in-out, box-shadow 0.3s ease-in-out;
            height: 30px;
        }

            /* Focus effect */
            .textbox:focus {
                border-color: #007bff; /* Focused border color */
                box-shadow: 0 0 5px rgba(0, 123, 255, 0.5);
                outline: none;
            }

            /* Disabled state */
            .textbox:disabled {
                background-color: #f2f2f2;
                color: #999;
            }

            /* Textbox with an error state */
            .textbox.error {
                border-color: #e74c3c; /* Red border for errors */
                box-shadow: 0 0 5px rgba(231, 76, 60, 0.5);
            }

            /* Placeholder styling */
            .textbox::placeholder {
                color: #999;
                font-style: italic;
            }

        /* Responsive design */
        @media (max-width: 768px) {
            .textbox {
                font-size: 12px;
                padding: 8px 12px;
            }
        }
        /* Base button styling */
        .button {
            padding: 10px 20px;
            font-size: 13px;
            color: white;
            background-color: #007bff; /* Primary blue color */
            border: none;
            border-radius: 4px;
            cursor: pointer;
            transition: background-color 0.3s ease, box-shadow 0.3s ease;
            box-shadow: 0 4px 6px rgba(0, 123, 255, 0.2);
        }

            /* Hover effect */
            .button:hover {
                background-color: #0056b3; /* Darker blue on hover */
                box-shadow: 0 6px 8px rgba(0, 123, 255, 0.3);
            }

            /* Active state */
            .button:active {
                background-color: #004085; /* Even darker blue on click */
                box-shadow: 0 3px 5px rgba(0, 123, 255, 0.2);
                transform: translateY(1px); /* Slight movement on click */
            }

            /* Disabled state */
            .button:disabled {
                background-color: #cccccc; /* Gray color for disabled button */
                cursor: not-allowed;
                box-shadow: none;
            }

            /* Secondary button */
            .button.secondary {
                background-color: #6c757d; /* Secondary gray color */
            }

                .button.secondary:hover {
                    background-color: #5a6268; /* Darker gray on hover */
                }

            /* Success button */
            .button.success {
                background-color: #28a745; /* Success green color */
            }

                .button.success:hover {
                    background-color: #218838; /* Darker green on hover */
                }

            /* Danger button */
            .button.danger {
                background-color: #dc3545; /* Danger red color */
            }

                .button.danger:hover {
                    background-color: #c82333; /* Darker red on hover */
                }

        /* Responsive design */
        @media (max-width: 768px) {
            .button {
                padding: 8px 15px;
                font-size: 12px;
            }
        }
    </style>
    <script type="text/javascript">


        function HidePopup1() {

            $('#confirmModal1').modal('hide');
        }
        function HidePopup2() {

            $('#confirmModal2').modal('hide');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container">
        <!-- Main div of web-form -->
        <h3 class="text-center"><b>Mentee Management</b></h3>

        <table class="table-borderless" style="width: 100%;">
            <!-- Table for alignment -->
            <tr>
                <td>
                    <asp:Label ID="lbl_mentorFormentee_studentEnrollmentName" runat="server" Text="Filter by Student No./Enrollment No./Name: " Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_mentorFormentee_studentEnrollmentName" runat="server" Width="200px"></asp:TextBox>
                </td>

                <td>
                    <asp:Label ID="lbl_mentorFormentee_course" runat="server" Text="Program: " Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddl_mentorFormentee_course" Font-Bold="true" runat="server" Width="200px"></asp:DropDownList>
                </td>

                <td>
                    <asp:Label ID="lbl_mentorFormentee_academicYear" runat="server" Text="Admitted Year: " Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddl_mentorFormentee_academicYear" runat="server" Font-Bold="true"></asp:DropDownList>
                </td>

                <td>
                    <asp:Button ID="btn_mentorFormentee_get" runat="server" CssClass="btn btn-danger btn-sm text-uppercase" Text="Get Record" OnClick="btn_mentorFormentee_get_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="7" align="right">

                    <table class="table-borderless" style="text-align: center" width="100%">
                        <tr align="center" style="display: inline">
                            <td>
                                <asp:Button ID="btn_All_MM_Records" runat="server" CssClass=" btn btn-success btn-sm text-uppercase" Text="Get All Mentee Meeting Records" OnClick="btn_All_MM_Records_Click1" Visible="false" />
                            </td>
                            <td>
                                <asp:Button ID="btn_All_ActivityRecords" runat="server" CssClass="btn btn-success btn-sm text-uppercase" OnClick="btn_All_ActivityRecords_Click" Text="Get All Mentee Activity Records." Visible="false" />

                            </td>
                        </tr>

                    </table>
                </td>
            </tr>

            <tr>
                <td colspan="7">
                    <div class="container" style="width: 100%; overflow-x: auto;">
                        <asp:GridView ID="grdView_mentorForMentee" AutoGenerateColumns="false" runat="server" Width="100%" CssClass="gridview">
                            <Columns>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_AutoNo" runat="server" Text='<%# Eval("[AutoNo]")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Student No">
                                    <ItemTemplate>

                                        <asp:Label ID="lblGrd_menterForMentee_studentno" runat="server" Text='<%# Eval("[No]")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Student Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrd_menterForMentee_studentName" runat="server" Text='<%#Eval("[StudentName]")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Father Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrd_menterForMentee_fatherName" runat="server" Text='<%#Eval("[FatherName]")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Mother Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrd_menterForMentee_motherName" runat="server" Text='<%#Eval("[MotherName]")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Mobile No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrd_menterForMentee_mobileNo" runat="server" Text='<%#Eval("[MobileNo]")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Date of Birth">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrd_menterForMentee_dateOfBirth" runat="server" Text='<%#Eval("[DOB]","{0:dd MMM yyyy}")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Mentee Records">
                                    <ItemTemplate>

                                        <asp:Button ID="btn_mentorForMentee_attendanceUndertaking" runat="server"
                                            Text="Attendance Undertaking" OnClick="btn_mentorForMentee_attendanceUndertaking_Click" CssClass="btn btn-primary btn-sm" />
                                        <br />
                                        <asp:Button ID="btn_mentorForMentee_MMrecord" runat="server"
                                            Text="Mentorship Meeting Record" CssClass="btn btn-primary btn-sm"
                                            OnClick="btn_mentorForMentee_MMrecord_Click" />

                                        <br />
                                        <asp:Button ID="btn_mentorForMentee_recordCoActivities" runat="server"
                                            Text="Co-Curricular/Extra-Curricular Activities Record" CssClass="btn btn-primary btn-sm"
                                            OnClick="btn_mentorForMentee_recordCoActivities_Click" />
                                        <br />
                                        <asp:Button ID="btn_mentorForMentee_studentBymentee" runat="server"
                                            Text="Special Needs/Advice/Help Required By Mentee" CssClass="btn btn-primary btn-sm"
                                            OnClick="btn_mentorForMentee_studentBymentee_Click" />
                                        <br />
                                        <asp:Button ID="btn_SpecialAchievements" runat="server"
                                            Text="Special Achievements of Student" CssClass="btn btn-primary btn-sm"
                                            OnClick="btn_SpecialAchievements_Click" />
                                        <br />
                                        <asp:Button ID="btn_Students_Assessment" runat="server"
                                            Text="Student Assessment Report" CssClass="btn btn-primary btn-sm"
                                            OnClick="btn_Students_Assessment_Click" />
                                        <br />
                                        <asp:Button ID="btn_mentorForMentee_result" runat="server"
                                            Text="Academic Results" OnClick="btn_mentorForMentee_result_Click" CssClass="btn btn-primary btn-sm" />
                                        <br />
                                        <asp:Button ID="btn_View_Attendance" runat="server"
                                            Text="View Attendance" CssClass="btn btn-primary btn-sm"
                                            OnClick="btn_View_Attendance_Click" />


                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Reports">
                                    <ItemTemplate>
                                        <div>
                                            <asp:Button ID="btn_mentorForMentee_viewReports" runat="server" Text="View Report"
                                                CssClass="btn btn-success btn-sm" OnClick="btn_mentorForMentee_viewReports_Click" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="lbl_emplydatatemplate" runat="server" Font-Bold="true" Text="There are no records found."></asp:Label>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>

                </td>
            </tr>

        </table>


    </div>

    <div id="confirmModal1" class="modal fade confirm-modal" role="dialog">

        <div class="modal-dialog modalPopup" style="width: 1000px; height: 800px">
            <div style="text-align: right; padding-bottom: -40px">
                <asp:Button ID="Button1" runat="server" Text="X" OnClientClick="HidePopup1();" Font-Size="Larger" />
            </div>
            <div class="clearfix" style="margin-bottom: 10px; margin-top: 10px; overflow: scroll; height: 700px; margin-left: 20px">

                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="col-sm-12 p-0">
                            <div class="form-group clearfix">

                                <div class="col-sm-12" style="text-align: center">
                                    <asp:Label ID="lblSem" runat="server" Text="Sem/Year :"></asp:Label>
                                    &nbsp&nbsp&nbsp&nbsp
                                    <asp:DropDownList ID="drpSemester" runat="server" Font-Bold="true" Height="32px" Font-Size="small" Width="220px">
                                    </asp:DropDownList>
                                    <br />
                                    <br />
                                </div>

                                <div id="printarea" class="sheet">
                                    <div style="text-align: center; font-size: x-large; font-weight: bold; text-decoration: underline;">
                                        Attendance Undertaking
                                    </div>
                                    <br />
                                    <br />
                                    <header>
        
      <div class="left">
        <div>
          <strong>The Director/Principal/HOD</strong><br />
         
        </div>
        <div class="college">
          <asp:Label runat="server" type="text" id="CollegeDepartment" name="CollegeDepartment"  style="flex: 1;padding: 4px 0;border: none;background: transparent;outline: none;" ></asp:Label>
        <%--  Teerthanker Mahaveer University, Moradabad.--%>
        </div>
          <div style="margin-top:5px">
              Teerthanker Mahaveer University, Moradabad.
          </div>
      </div>

      <div class="right">
      <label for="Date1" style="margin-right:8px;width:10px">Date:</label><asp:TextBox runat="server"  id="Date1" name="Date1" placeholder="Date" style="flex: 1;padding: 4px 0;border: none;background: transparent;outline: none;width:65px;" /> 
      </div>
    </header>

                                    <h2 class="subject">Subject: - Undertaking for maintaining 75% attendance.</h2>

                                    <p class="lead">
                                        Respected Sir/Madam,
                                    </p>
                                    <p style="font-size: 14px;">
                                        This is to state that I, <span id="studentName1" runat="server"></span>
                                        and my father/mother Sh./Smt. <span style="min-width: 200px; font-size: 1px bold" id="fatherName1" runat="server"></span>
                                        have complete knowledge about the Teerthanker Mahaveer University Ordinance governing the attendance of students,
      according to which I have to attend at least 75% of the classes individually in each course during the entire semester/year
      of the programme; failing which I will not be allowed to appear in internal and/or external examinations of the University
      in the course(s) wherein my attendance is less than 75%.
                                    </p>

                                    <p style="margin-top: 8px;">
                                        As on date, my attendance in various courses is as mentioned in the table below:
                                    </p>
                                    <asp:GridView ID="grdAttendanceReport" CssClass="attendance" aria-label="Attendance table" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
                                        EmptyDataText="There are no data records to display." BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                        GridLines="Horizontal">
                                        <AlternatingRowStyle BackColor="#F7F7F7" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex +1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Course Name" ItemStyle-Width="5%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCourse" runat="server" Text='<%# Bind("[Course Name]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Course Code" ItemStyle-Width="3%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCourseCode" runat="server" Text='<%# Bind("[Course Code]") %>'></asp:Label>



                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Percent" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPer" runat="server" Text='<%# "" + Eval("Per") + " % "%>'></asp:Label>
                                                    <%-- <asp:Label ID="lblPer" runat="server" Text='<%# Eval("Per") %>'></asp:Label>--%>
                                                </ItemTemplate>
                                                <%-- <FooterTemplate>
                 <div style="text-align: right; width: 150px">
                     <asp:Label ID="lblTotalqty" runat="server" Text="sdhjdh" Font-Bold="true" />
                 </div>
             </FooterTemplate>--%>
                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle ForeColor="Green" Font-Bold="true" Font-Size="Medium" BorderStyle="Solid" BorderColor="Black" BackColor="LightGray" />
                                        <HeaderStyle BackColor="LightGray" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" Font-Size="Large" Height="40px" VerticalAlign="Bottom" />
                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                        <RowStyle ForeColor="#4A3C8C" Font-Bold="true" Font-Size="Small" BorderStyle="Solid" BorderColor="Black" />
                                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                        <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                        <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                    </asp:GridView>
                                    <p class="note" style="font-size: 14px;">
                                        It would be my personal responsibility to ensure 75% attendance separately in each course during the programme otherwise I shall have no right/claim to appear in the internal and/or external examinations of the university in the course(s) less than 75% attendance.
                                    </p>

                                    <p style="font-size: 14px;">
                                        I further, undertake that it shall be my responsibility to inform my parents regarding my short attendance as mentioned above.
                                    </p>

                                    <p style="font-size: 14px;">
                                        I am signing this undertaking after reading the University Ordinance on attendance and other matters.
                                    </p>

                                    <div class="verifications" aria-label="Verification blocks">
                                        <div class="sign-block1">
                                            <div class="meta">
                                                <div class="item">

                                                    <label for="studentName">Student’s Name :</label>
                                                    <asp:TextBox runat="server" type="text" ID="studentName" placeholder="student Name" ReadOnly="true"></asp:TextBox>
                                                </div>
                                                <div class="item">
                                                    <label for="program">Program :</label>
                                                    <asp:TextBox runat="server" type="text" ID="program" placeholder="program" ReadOnly="true"></asp:TextBox>
                                                </div>
                                                <div class="item">
                                                    <label for="branch">Branch (if any):</label>
                                                    <asp:TextBox runat="server" type="text" ID="branch" placeholder="branch" ReadOnly="true"></asp:TextBox>
                                                </div>
                                                <div class="item">
                                                    <label for="semester">Semester/Year :</label>
                                                    <asp:TextBox runat="server" type="text" ID="semester" placeholder="Semester/Year" ReadOnly="true"></asp:TextBox>
                                                </div>
                                                <div class="item">
                                                    <label for="studentMobile">Student’s Mobile No :</label>
                                                    <asp:TextBox runat="server" type="tel" ID="studentMobile" placeholder="Student Mobile" ReadOnly="true"></asp:TextBox>
                                                </div>
                                                <div class="item">
                                                    <label for="studentEmail">Student’s E-Mail ID :</label>
                                                    <asp:TextBox runat="server" type="email" ID="studentEmail" placeholder="Student Email" ReadOnly="true"></asp:TextBox>
                                                </div>
                                                <div class="item">
                                                    <label for="fatherMobile">Father’s Mobile No :</label>
                                                    <asp:TextBox runat="server" type="tel" ID="fatherMobile" placeholder="Father Mobile" ReadOnly="true"></asp:TextBox>
                                                </div>
                                                <div class="item">
                                                    <label for="fatherEmail">Father’s E-Mail ID :</label>
                                                    <asp:TextBox runat="server" ID="fatherEmail" placeholder="Father Email"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="sign-block">
                                            <div class="sign-label">Declaration</div>
                                            <div>
                                                <asp:CheckBox runat="server" ID="CheckBox1" Checked="true" Enabled="false" />
                                                I have read and understood the undertaking furnished above by me, and that I fully understand its implications.
                                            </div>
                                        </div>

                                    </div>

                                </div>
                                <div class="sign-block2" style="width: 100%">
                                    <asp:Button ID="btnPrint" runat="server" Visible="false" CssClass="button btn-success" UseSubmitBehavior="false" OnClientClick="PrintDiv();" Text="Print" Style="float: right;" />
                                    <asp:Button ID="SubmitButton" runat="server" Visible="false" CssClass="button btn-success" UseSubmitBehavior="false" Text="Submit" Style="float: right;" />
                                    <br />
                                    <br />
                                </div>
                            </div>
                        </div>


                    </ContentTemplate>

                </asp:UpdatePanel>
            </div>
        </div>
    </div>



     <div id="confirmModal2" class="modal fade confirm-modal" role="dialog">

     <div class="modal-dialog modalPopup" style="width: 1000px; height: 800px">
         <div style="text-align: right; padding-bottom: -40px">
             <asp:Button ID="Button2" runat="server" Text="X" OnClientClick="HidePopup2();" Font-Size="Larger" />
         </div>
         <div class="clearfix" style="margin-bottom: 10px; margin-top: 10px; overflow: scroll; height: 700px; margin-left: 20px">

             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                 <Triggers>
                 </Triggers>
                 <ContentTemplate>
                     <div class="col-sm-12 p-0">
                         <div class="form-group clearfix">

                             <div class="col-sm-12" style="text-align: center">
                                 <asp:Label ID="Label1" runat="server" Text="Sem/Year :"></asp:Label>
                                 &nbsp&nbsp&nbsp&nbsp
                                 <asp:DropDownList ID="DropDownList1" runat="server" Font-Bold="true" Height="32px" Font-Size="small" Width="220px">
                                 </asp:DropDownList>
                                 <br />
                                 <br />
                             </div>

                             <div id="printarea" class="sheet">
                                 <div style="text-align: center; font-size: x-large; font-weight: bold; text-decoration: underline;">
                                     Attendance Undertaking
                                 </div>
                                 <br />
                                 <br />
                                 <header>
     
   <div class="left">
     <div>
       <strong>The Director/Principal/HOD</strong><br />
      
     </div>
     <div class="college">
       <asp:Label runat="server" type="text" id="Label2" name="CollegeDepartment"  style="flex: 1;padding: 4px 0;border: none;background: transparent;outline: none;" ></asp:Label>
     <%--  Teerthanker Mahaveer University, Moradabad.--%>
     </div>
       <div style="margin-top:5px">
           Teerthanker Mahaveer University, Moradabad.
       </div>
   </div>

   <div class="right">
   <label for="Date1" style="margin-right:8px;width:10px">Date:</label><asp:TextBox runat="server"  id="TextBox1" name="Date1" placeholder="Date" style="flex: 1;padding: 4px 0;border: none;background: transparent;outline: none;width:65px;" /> 
   </div>
 </header>

                                 <h2 class="subject">Subject: - Undertaking for maintaining 80% attendance.</h2>

                                 <p class="lead">
                                     Respected Sir/Madam,
                                 </p>
                                 <p style="font-size: 14px;">
                                     This is to state that I, <span id="Span1" runat="server"></span>
                                     and my father/mother Sh./Smt. <span style="min-width: 200px; font-size: 1px bold" id="Span2" runat="server"></span>
                                     have complete knowledge about the Teerthanker Mahaveer University Ordinance governing the attendance of students,
   according to which I have to attend at least 80% of the classes individually in each course during the entire semester/year
   of the programme; failing which I will not be allowed to appear in internal and/or external examinations of the University
   in the course(s) wherein my attendance is less than 80%.
                                 </p>

                                 <p style="margin-top: 8px;">
                                     As on date, my attendance in various courses is as mentioned in the table below:
                                 </p>
                                 <asp:GridView ID="GridView1" CssClass="attendance" aria-label="Attendance table" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
                                     EmptyDataText="There are no data records to display." BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                     GridLines="Horizontal">
                                     <AlternatingRowStyle BackColor="#F7F7F7" />
                                     <Columns>
                                         <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-center">
                                             <ItemTemplate>
                                                 <%# Container.DataItemIndex +1 %>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Course Name" ItemStyle-Width="5%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                             <ItemTemplate>
                                                 <asp:Label ID="lblCourse" runat="server" Text='<%# Bind("[Course Name]") %>'></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Course Code" ItemStyle-Width="3%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                             <ItemTemplate>
                                                 <asp:Label ID="lblCourseCode" runat="server" Text='<%# Bind("[Course Code]") %>'></asp:Label>



                                             </ItemTemplate>
                                         </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Percent" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                             <ItemTemplate>
                                                 <asp:Label ID="lblPer" runat="server" Text='<%# "" + Eval("Per") + " % "%>'></asp:Label>
                                                 <%-- <asp:Label ID="lblPer" runat="server" Text='<%# Eval("Per") %>'></asp:Label>--%>
                                             </ItemTemplate>
                                             <%-- <FooterTemplate>
              <div style="text-align: right; width: 150px">
                  <asp:Label ID="lblTotalqty" runat="server" Text="sdhjdh" Font-Bold="true" />
              </div>
          </FooterTemplate>--%>
                                         </asp:TemplateField>

                                     </Columns>
                                     <FooterStyle ForeColor="Green" Font-Bold="true" Font-Size="Medium" BorderStyle="Solid" BorderColor="Black" BackColor="LightGray" />
                                     <HeaderStyle BackColor="LightGray" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" Font-Size="Large" Height="40px" VerticalAlign="Bottom" />
                                     <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                     <RowStyle ForeColor="#4A3C8C" Font-Bold="true" Font-Size="Small" BorderStyle="Solid" BorderColor="Black" />
                                     <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                     <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                     <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                     <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                     <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                 </asp:GridView>
                                 <p class="note" style="font-size: 14px;">
                                     It would be my personal responsibility to ensure 80% attendance separately in each course during the programme otherwise I shall have no right/claim to appear in the internal and/or external examinations of the university in the course(s) less than 80% attendance.
                                 </p>

                                 <p style="font-size: 14px;">
                                     I further, undertake that it shall be my responsibility to inform my parents regarding my short attendance as mentioned above.
                                 </p>

                                 <p style="font-size: 14px;">
                                     I am signing this undertaking after reading the University Ordinance on attendance and other matters.
                                 </p>

                                 <div class="verifications" aria-label="Verification blocks">
                                     <div class="sign-block1">
                                         <div class="meta">
                                             <div class="item">

                                                 <label for="studentName">Student’s Name :</label>
                                                 <asp:TextBox runat="server" type="text" ID="TextBox2" placeholder="student Name" ReadOnly="true"></asp:TextBox>
                                             </div>
                                             <div class="item">
                                                 <label for="program">Program :</label>
                                                 <asp:TextBox runat="server" type="text" ID="TextBox3" placeholder="program" ReadOnly="true"></asp:TextBox>
                                             </div>
                                             <div class="item">
                                                 <label for="branch">Branch (if any):</label>
                                                 <asp:TextBox runat="server" type="text" ID="TextBox4" placeholder="branch" ReadOnly="true"></asp:TextBox>
                                             </div>
                                             <div class="item">
                                                 <label for="semester">Semester/Year :</label>
                                                 <asp:TextBox runat="server" type="text" ID="TextBox5" placeholder="Semester/Year" ReadOnly="true"></asp:TextBox>
                                             </div>
                                             <div class="item">
                                                 <label for="studentMobile">Student’s Mobile No :</label>
                                                 <asp:TextBox runat="server" type="tel" ID="TextBox6" placeholder="Student Mobile" ReadOnly="true"></asp:TextBox>
                                             </div>
                                             <div class="item">
                                                 <label for="studentEmail">Student’s E-Mail ID :</label>
                                                 <asp:TextBox runat="server" type="email" ID="TextBox7" placeholder="Student Email" ReadOnly="true"></asp:TextBox>
                                             </div>
                                             <div class="item">
                                                 <label for="fatherMobile">Father’s Mobile No :</label>
                                                 <asp:TextBox runat="server" type="tel" ID="TextBox8" placeholder="Father Mobile" ReadOnly="true"></asp:TextBox>
                                             </div>
                                             <div class="item">
                                                 <label for="fatherEmail">Father’s E-Mail ID :</label>
                                                 <asp:TextBox runat="server" ID="TextBox9" placeholder="Father Email"></asp:TextBox>
                                             </div>
                                         </div>
                                     </div>

                                     <div class="sign-block">
                                         <div class="sign-label">Declaration</div>
                                         <div>
                                             <asp:CheckBox runat="server" ID="CheckBox2" Checked="true" Enabled="false" />
                                             I have read and understood the undertaking furnished above by me, and that I fully understand its implications.
                                         </div>
                                     </div>

                                 </div>

                             </div>
                             <div class="sign-block2" style="width: 100%">
                                 <asp:Button ID="Button3" runat="server" Visible="false" CssClass="button btn-success" UseSubmitBehavior="false" OnClientClick="PrintDiv();" Text="Print" Style="float: right;" />
                                 <asp:Button ID="Button4" runat="server" Visible="false" CssClass="button btn-success" UseSubmitBehavior="false" Text="Submit" Style="float: right;" />
                                 <br />
                                 <br />
                             </div>
                         </div>
                     </div>


                 </ContentTemplate>

             </asp:UpdatePanel>
         </div>
     </div>
 </div>


</asp:Content>

