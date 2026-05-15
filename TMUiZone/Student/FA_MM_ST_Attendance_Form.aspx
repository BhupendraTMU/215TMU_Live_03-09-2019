<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" EnableEventValidation="true" AutoEventWireup="true" CodeFile="FA_MM_ST_Attendance_Form.aspx.cs" Inherits="Student_FA_MM_Attendance_Form" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <fieldset class="boxBody">
                <asp:Label ID="Label1" runat="server"
                    Text="Student Attendance Undertaking" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
            </fieldset>
            <br />
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
                            <asp:Label runat="server" type="text" ID="CollegeDepartment" name="CollegeDepartment" Style="flex: 1; padding: 4px 0; border: none; background: transparent; outline: none;"></asp:Label>
                            <%--  Teerthanker Mahaveer University, Moradabad.--%>
                        </div>
                        <div style="margin-top: 5px">
                            Teerthanker Mahaveer University, Moradabad.
                        </div>
                    </div>

                    <div class="right">
                        <label for="Date1" style="margin-right: 8px;">Date:</label><asp:TextBox runat="server" ID="Date1" name="Date1" placeholder="Date" Style="flex: 1; padding: 4px 0; border: none; background: transparent; outline: none; width: 120px;" />
                    </div>
                </header>

                <h2 class="subject" id="subjectNormal" runat="server" visible="false">Subject: - Undertaking for maintaining 75% attendance.</h2>
                <h2 class="subject" id="subjectNursing" runat="server" visible="false">Subject: - Undertaking for maintaining 80% attendance.</h2>
                <p class="lead">
                    Respected Sir/Madam,
                </p>
                <p style="font-size: 14px;" id="pContent" runat="server" visible="false">
                    This is to state that I, <span id="studentName1" runat="server"></span>
                    and my father/mother Sh./Smt. <span style="min-width: 200px; font-size: 1px bold" id="fatherName1" runat="server"></span>
                    have complete knowledge about the Teerthanker Mahaveer University Ordinance governing the attendance of students,
      according to which I have to attend at least 75% of the classes individually in each course during the entire semester/year
      of the programme; failing which I will not be allowed to appear in internal and/or external examinations of the University
      in the course(s) wherein my attendance is less than 75%.
                </p>
                <p style="font-size: 14px;" id="p1" runat="server" visible="false">
                    This is to state that I, <span id="Span1" runat="server"></span>
                    and my father/mother Sh./Smt. <span style="min-width: 200px; font-size: 1px bold" id="Span2" runat="server"></span>
                    have complete knowledge about the Indian Nursing Council(INC) Guidelines governing the attendance of students,
according to which I have to attend at least 80% of the classes individually in each course(irrespective of the kind of absence) during the entire semester/year
of the programme; failing which I will not be allowed to appear in internal and/or external examinations of the University
in the course(s) wherein my attendance is less than 80%.
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
                <p class="note" style="font-size: 14px;" id="Nornalres" runat="server" visible="false">
                    It would be my personal responsibility to ensure 75% attendance separately in each course during the programme otherwise I shall have no right/claim to appear in the internal and/or external examinations of the university in the course(s) less than 75% attendance.
                </p>
                <p class="note" style="font-size: 14px;" id="NornalNur" runat="server" visible="false">
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
                            <asp:CheckBox runat="server" ID="CheckBox1" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                            I have read and understood the undertaking furnished above by me, and that I fully understand its implications.
                        </div>
                    </div>

                </div>

            </div>
            <div class="sign-block2" style="width: 100%">
                <asp:Button ID="btnPrint" runat="server" Visible="false" CssClass="button btn-success" UseSubmitBehavior="false" OnClientClick="PrintDiv();" Text="Print" Style="float: right;" />
                <asp:Button ID="SubmitButton" runat="server" Visible="false" CssClass="button btn-success" UseSubmitBehavior="false" Text="Submit" OnClick="SubmitButton_Click" Style="float: right;" />
                <br />
                <br />
            </div>
        </ContentTemplate>
        <%-- <Triggers>
            <asp:PostBackTrigger ControlID="fatherEmail" />
            <asp:PostBackTrigger ControlID="SubmitButton" />
            
        </Triggers>--%>
    </asp:UpdatePanel>
</asp:Content>
