<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="Undertaking_NEP_Option.aspx.cs" Inherits="Student_Undertaking_NEP_Option" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Student NEP Option Form</title>

    <style>
        body {
            font-family: "Times New Roman", serif;
        }

        .container {
            width: 750px;
            margin: auto;
            border: 1px solid black;
            padding: 25px;
        }

        h3 {
            text-align: center;
            margin: 2px;
        }

        p {
            font-size: 14px;
            text-align: justify;
            line-height: 1.6;
        }

        .section {
            margin-top: 15px;
        }

        .row {
            margin: 8px 0;
        }

        .line {
            border-bottom: 1px solid black;
            display: inline-block;
            min-width: 200px;
            padding-left: 5px;
        }

        .full {
            min-width: 400px;
        }

        .signature-table {
            width: 100%;
            margin-top: 30px;
        }

            .signature-table td {
                padding-top: 15px;
            }

        .btn-print {
            margin: 15px auto;
            display: block;
            padding: 10px 20px;
            background: #007bff;
            color: white;
            border: none;
            cursor: pointer;
        }

        @media print {
            .btn-print {
                display: none;
            }
        }
    </style>

    <script>
        function PrintForm() {
            window.print();
        }
        function singleCheck(chk) {
            var chkA = document.getElementById('<%= chkA.ClientID %>');
            var chkB = document.getElementById('<%= chkB.ClientID %>');

            if (chk.id === chkA.id && chkA.checked) {
                chkB.checked = false;
            }
            else if (chk.id === chkB.id && chkB.checked) {
                chkA.checked = false;
            }
        }
    </script>

    <script>
        function PrintForm() {
            var printContents = document.querySelector('.container').innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;
            window.print();
            document.body.innerHTML = originalContents;

            location.reload(); // optional (to restore events properly)
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- <asp:Button ID="btnPrint" runat="server" Text="Print / Save PDF"
        CssClass="btn-print" OnClientClick="PrintForm(); return false;" />--%>

    <div class="container">

        <h3>TEERTHANKER MAHAVEER UNIVERSITY</h3>
        <h3>Delhi Road, Moradabad – 244001 (U.P.)</h3>
        <h3>STUDENT UNDERTAKING / OPTION CHOOSING FORM</h3>

        <p style="text-align: center;">
            (For Exit after Three Years or Continuation for Fourth Year under NEP2020 Programs)
       
        </p>

        <!-- Intro -->
        <div class="section">
            <p>
                As per the provisions of the National Education Policy (NEP2020) and applicable University 
                regulations, students admitted in the Academic Session 2023–24 are required to choose 
                their option after completion of three academic years (six semesters). Students must 
                carefully read the options below and select only one.
           
            </p>
        </div>

        <!-- Student Details -->
        <div class="section">
            <b>Student Details</b>

            <div class="row">
                Enrollment No: <span>
                    <asp:Label ID="lblEnroll" runat="server" Font-Underline="true" /></span>
            </div>
            <div class="row">
                Name: <span>
                    <asp:Label ID="lblName" runat="server" Font-Underline="true" /></span>
            </div>
            <div class="row">
                Father’s Name: <span>
                    <asp:Label ID="lblFather" Font-Underline="true" runat="server" /></span>
            </div>
            <div class="row">
                Program Code: <span>
                    <asp:Label ID="lblProgramCode" Font-Underline="true" runat="server" /></span>
            </div>
            <div class="row">
                Program Name: <span>
                    <asp:Label ID="lblProgramName" Font-Underline="true" runat="server" /></span>
            </div>
            <div class="row">
                College: <span>
                    <asp:Label ID="lblCollege" runat="server" Font-Underline="true" /></span>
            </div>
            <div class="row">
                Session: <span>
                    <asp:Label ID="lblSession" runat="server" Font-Underline="true" /></span>
            </div>
        </div>

        <!-- Options -->
        <div class="section">
            <b>Option to be Chosen by the Student (✓)</b>
            <div id="OPTA" runat="server">
                <p>
                    <asp:CheckBox ID="chkA" runat="server" Enabled="true" onclick="singleCheck(this)" />
                    <b>Option A – Exit after Three Years under NEP</b>
                </p>

                <p>
                    I hereby opt to exit the programme after completion of three academic years (six semesters) 
                under NEP2020. I understand that the qualification/degree awarded will be in accordance 
                with NEP provisions and University rules will be applicable for exit after three years. 
                I further declare that I shall not claim continuation of the program in the fourth year 
                after choosing this option.
           
                </p>
            </div>
            <div id="OPTB" runat="server">
                <p>
                    <asp:CheckBox ID="chkB" runat="server" Enabled="true" onclick="singleCheck(this)" />
                    <b>Option B – Continue the Programme for the Fourth Year</b>
                </p>

                <p>
                    I hereby opt to continue the programme for the Fourth Year in accordance with NEP2020 
                and University regulations and will fulfill all the academic requirements prescribed 
                for the fourth year.
           
                </p>
            </div>
        </div>

        <!-- Declaration -->
        <div class="section">
            <b>Declaration by the Student</b>

            <p>I hereby confirm that the above option has been chosen voluntarily by me after understanding the academic implications.</p>
            <p>I understand that the option once submitted by me be treated as final as per University rules & regulations.</p>
            <p>I declare that the information provided by me is true and correct.</p>
        </div>

        <!-- Signature -->
        <table class="signature-table">
            <tr>
                <td>Date: <span class="line">
                    <asp:Label ID="lblDate" runat="server" /></span></td>
                <td>Place: <span class="line">
                    <asp:Label ID="lblPlace" runat="server" /></span></td>
            </tr>



            <tr>
                <td>Mobile: <span class="line">
                    <asp:Label ID="lblMobile" runat="server" /></span></td>
                <td>Email: <span class="line">
                    <asp:Label ID="lblEmail" runat="server" /></span></td>
            </tr>
            <tr>

                <td>Name: <span class="line">
                    <asp:Label ID="lblName2" runat="server" /></span></td>
                <td>Signature: <span class="line">
                    <asp:Label ID="lblSign" runat="server" /></span></td>
            </tr>
        </table>

        <!-- Verification -->
        <div class="section">
            <b>Verified & approved by the concern College authority</b>
            <p>Verified that the above details have been checked from the University records.</p>

            <div class="row">
                Signature: <span class="line">
                    <asp:Label ID="lblAuth" runat="server" /></span>
            </div>
            <div class="row">
                Head Name: <span class="line">
                    <asp:Label ID="lblHead" runat="server" /></span>
            </div>
            <div class="row">
                Designation: <span class="line">
                    <asp:Label ID="lblDesg" runat="server" /></span>
            </div>
            <div class="row">
                Date: <span class="line">
                    <asp:Label ID="lblVerifyDate" runat="server" /></span>
            </div>
        </div>
        <div class="action-buttons">

            <asp:Button ID="btnSave" runat="server" Text="Submit"
                CssClass="btn btn-print" OnClick="btnSave_Click" />

            <asp:Button ID="btnPrint" runat="server" Text="Print"
                CssClass="btn btn-print"
                OnClientClick="PrintForm(); return false;" />

        </div>
    </div>






</asp:Content>

