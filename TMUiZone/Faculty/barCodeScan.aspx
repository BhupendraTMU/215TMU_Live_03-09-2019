<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="barCodeScan.aspx.cs" Inherits="Faculty_barCodeScan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        /* ===============================
           Scanner Container
        =============================== */
        .scanner-wrapper {
            max-width: 600px;
            margin: 30px auto;
            padding: 25px;
            background: #ffffff;
            border-radius: 15px;
            box-shadow: 0 5px 25px rgba(0,0,0,0.12);
        }

        .scanner-title {
            text-align: center;
            color: #093A62;
            font-weight: 600;
            margin-bottom: 20px;
        }

        #reader {
            width: 100%;
            max-width: 500px;
            margin: auto;
        }

        .search-section {
            margin-top: 25px;
        }

        .search-label {
            font-weight: 600;
            color: #333;
            margin-bottom: 8px;
            display: block;
        }

        .search-button {
            margin-top: 10px;
        }


        /* ===============================
           Student Popup
        =============================== */
        .student-modal {
            display: none;
            position: fixed;
            z-index: 99999;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            overflow: auto;
            background: rgba(0,0,0,0.65);
        }

        .student-modal-content {
            background: #ffffff;
            width: 90%;
            max-width: 700px;
            margin: 5% auto;
            border-radius: 15px;
            overflow: hidden;
            box-shadow: 0 10px 40px rgba(0,0,0,0.30);
            animation: popupAnimation 0.3s ease;
        }

        @keyframes popupAnimation {
            from {
                transform: scale(0.8);
                opacity: 0;
            }

            to {
                transform: scale(1);
                opacity: 1;
            }
        }

        .student-modal-header {
            background: #093A62;
            color: white;
            padding: 15px 20px;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .student-modal-header h4 {
            margin: 0;
        }

        .close-popup {
            font-size: 28px;
            font-weight: bold;
            cursor: pointer;
            color: white;
        }

        .student-modal-body {
            padding: 25px;
        }

        .student-photo {
            width: 180px;
            height: 180px;
            object-fit: cover;
            border-radius: 50%;
            border: 5px solid #eee;
            margin-bottom: 20px;
        }

        .student-field {
            margin-bottom: 15px;
        }

        .student-field label {
            font-weight: 600;
            color: #093A62;
            display: block;
            margin-bottom: 5px;
        }

        .student-field input,
        .student-field textarea {
            width: 100%;
            background: #f7f7f7 !important;
            border: 1px solid #ddd !important;
            border-radius: 6px;
        }

        @media(max-width:600px) {

            .scanner-wrapper {
                margin: 15px;
                padding: 15px;
            }

            .student-modal-content {
                width: 95%;
                margin: 10% auto;
            }

            .student-modal-body {
                padding: 15px;
            }
        }

    </style>
     <script>
     function onScanSuccess(decodedText, decodedResult) {

         console.log("Scanned code:", decodedText, decodedResult);
         //alert("Detected code: " + decodedText);

         document.getElementById('lblstudentId').value = decodedText;
         //alert('ok');
         const button = document.getElementById("btnSubmit");

         button.click();
        <%-- __doPostBack('<%= btnSubmit.UniqueID %>', '');--%>



         html5QrcodeScanner.clear().catch(error =>
             console.error("Clearing failed:", error));
     }

     function onScanFailure(error) {
         console.warn("Scan failure:", error);
     }

     const formatsToSupport = [
         Html5QrcodeSupportedFormats.EAN_13,
         Html5QrcodeSupportedFormats.CODE_128,
         Html5QrcodeSupportedFormats.UPC_A,
         Html5QrcodeSupportedFormats.UPC_E,
         Html5QrcodeSupportedFormats.CODE_39,
         // add more as needed, e.g., EAN_8, ITF, PDF_417...
         Html5QrcodeSupportedFormats.QR_CODE  // optional
     ];

     const config = {
         fps: 10,
         qrbox: { width: 250, height: 150 },
         formatsToSupport: formatsToSupport,
         verbose: true
     };

     const html5QrcodeScanner = new Html5QrcodeScanner(
         "reader",
         config,
         false
     );

     html5QrcodeScanner.render(onScanSuccess, onScanFailure);
     </script>

</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    Runat="Server">


    <!-- ===============================
         Barcode Scanner Section
    =============================== -->

    <div id="mainQRCode"
        runat="server"
        class="scanner-wrapper">

        <h2 class="scanner-title">
            Scan Barcode or QR Code
        </h2>


        <!-- Hidden ASP.NET Button -->
        <asp:Button ID="btnSubmit"
            runat="server"
            Text="Submit"
            Style="display:none;"
            OnClick="btnSubmit_Click" />


        <!-- Hidden Student ID -->
        <asp:HiddenField ID="lblstudentId"
            runat="server" />


        <!-- Scanner -->
        <div id="reader"></div>


        <div class="search-section">

            <label class="search-label">
                Student Enrollment No.
            </label>

            <asp:TextBox ID="txtEnrollmentNo_"
                runat="server"
                CssClass="form-control"
                placeholder="Enter Enrollment No." />

            <asp:Button ID="btnSearch"
                runat="server"
                Text="Search"
                BackColor="#ff6600"
                ForeColor="White"
                CssClass="form-control search-button"
                OnClick="btnSearch_Click" />

        </div>

    </div>


    <!-- ===============================
         Student Details Popup
    =============================== -->

    <div id="studentDetails"
        runat="server"
        class="student-modal">

        <div class="student-modal-content">


            <!-- Header -->

            <div class="student-modal-header">

                <h4>
                    Student Details
                </h4>

                <span class="close-popup"
                    onclick="closeStudentPopup();">
                    &times;
                </span>

            </div>


            <!-- Body -->

            <div class="student-modal-body">


                <!-- Student Image -->

                <div style="text-align:center;">

                    <asp:Image ID="stImg"
                        runat="server"
                        CssClass="student-photo"
                        AlternateText="Student Photo" />

                </div>


                <!-- Enrollment -->

                <div class="student-field">

                    <label>
                        Enrollment No
                    </label>

                    <asp:TextBox ID="txtStudentID"
                        runat="server"
                        Enabled="false"
                        CssClass="form-control" />

                </div>


                <!-- Hosteller -->

                <div class="student-field">

                    <label>
                        Hosteller
                    </label>

                    <asp:TextBox ID="txthostler"
                        runat="server"
                        Enabled="false"
                        CssClass="form-control" />

                </div>


                <!-- Transport -->

                <div class="student-field">

                    <label>
                        Transport
                    </label>

                    <asp:TextBox ID="txttransport"
                        runat="server"
                        Enabled="false"
                        CssClass="form-control" />

                </div>


                <!-- Religion -->

                <div class="student-field">

                    <label>
                        Religion
                    </label>

                    <asp:TextBox ID="txtReligion"
                        runat="server"
                        Enabled="false"
                        CssClass="form-control" />

                </div>


                <!-- Full Name -->

                <div class="student-field">

                    <label>
                        Full Name
                    </label>

                    <asp:TextBox ID="txtName"
                        runat="server"
                        Enabled="false"
                        CssClass="form-control" />

                </div>


                <!-- Email -->

                <div class="student-field">

                    <label>
                        Email
                    </label>

                    <asp:TextBox ID="txtEmail"
                        runat="server"
                        Enabled="false"
                        TextMode="Email"
                        CssClass="form-control" />

                </div>


                <!-- DOB -->

                <div class="student-field">

                    <label>
                        Date of Birth
                    </label>

                    <asp:TextBox ID="txtDOB"
                        runat="server"
                        Enabled="false"
                        CssClass="form-control" />

                </div>


                <!-- Gender -->

                <div class="student-field">

                    <label>
                        Gender
                    </label>

                    <asp:TextBox ID="txtGender"
                        runat="server"
                        Enabled="false"
                        CssClass="form-control" />

                </div>


                <!-- Course -->

                <div class="student-field">

                    <label>
                        Course
                    </label>

                    <asp:TextBox ID="txtCourse"
                        runat="server"
                        Enabled="false"
                        CssClass="form-control" />

                </div>


                <!-- Address -->

                <div class="student-field">

                    <label>
                        Address
                    </label>

                    <asp:TextBox ID="txtAddress"
                        runat="server"
                        TextMode="MultiLine"
                        Enabled="false"
                        Rows="3"
                        CssClass="form-control" />

                </div>


                <!-- Cancel -->

                <asp:Button ID="btnCancel"
                    runat="server"
                    Text="Close"
                    CssClass="form-control"
                    BackColor="Green"
                    ForeColor="White"
                    OnClick="btnCancel_Click" />

            </div>

        </div>

    </div>


    <!-- ===============================
         Scanner JavaScript
    =============================== -->

    <script type="text/javascript">

        let html5QrcodeScanner = null;
        let scanAlreadyDone = false;


        document.addEventListener("DOMContentLoaded", function () {

            const formatsToSupport = [

                Html5QrcodeSupportedFormats.EAN_13,
                Html5QrcodeSupportedFormats.CODE_128,
                Html5QrcodeSupportedFormats.UPC_A,
                Html5QrcodeSupportedFormats.UPC_E,
                Html5QrcodeSupportedFormats.CODE_39,
                Html5QrcodeSupportedFormats.EAN_8,
                Html5QrcodeSupportedFormats.ITF,
                Html5QrcodeSupportedFormats.QR_CODE

            ];


            const config = {

                fps: 10,

                qrbox: {
                    width: 300,
                    height: 180
                },

                formatsToSupport: formatsToSupport,

                rememberLastUsedCamera: true,

                showTorchButtonIfSupported: true,

                showZoomSliderIfSupported: true

            };


            html5QrcodeScanner =
                new Html5QrcodeScanner(
                    "reader",
                    config,
                    false
                );


            html5QrcodeScanner.render(
                onScanSuccess,
                onScanFailure
            );

        });


        /* ===============================
           Scan Success
        =============================== */

        function onScanSuccess(decodedText, decodedResult) {

            if (scanAlreadyDone) {
                return;
            }

            scanAlreadyDone = true;


            console.log(
                "Scanned Code:",
                decodedText
            );


            // ASP.NET rendered Client IDs
            const hiddenField =
                document.getElementById(
                    '<%= lblstudentId.ClientID %>'
                );


            const submitButton =
                document.getElementById(
                    '<%= btnSubmit.ClientID %>'
                );


            if (hiddenField) {

                hiddenField.value = decodedText;

            }


            console.log(
                "Student ID:",
                decodedText
            );


            // Stop scanner
            if (html5QrcodeScanner) {

                html5QrcodeScanner
                    .clear()
                    .then(function () {

                        console.log(
                            "Scanner stopped."
                        );

                    })
                    .catch(function (error) {

                        console.log(
                            "Scanner clear error:",
                            error
                        );

                    });

            }


            // ASP.NET server-side click
            if (submitButton) {

                setTimeout(function () {

                    submitButton.click();

                }, 200);

            }

        }


        /* ===============================
           Scan Failure
        =============================== */

        function onScanFailure(error) {

            // Don't continuously print errors
            // console.log(error);

        }


        /* ===============================
           Close Popup
        =============================== */

        function closeStudentPopup() {

            const popup =
                document.getElementById(
                    '<%= studentDetails.ClientID %>'
                );

            if (popup) {

                popup.style.display = "none";

            }

        }


        /* ===============================
           Open Popup
        =============================== */

        function openStudentPopup() {

            const popup =
                document.getElementById(
                    '<%= studentDetails.ClientID %>'
                );

            if (popup) {

                popup.style.display = "block";

            }

        }

    </script>

</asp:Content>

