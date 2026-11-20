<%@ Page Title="Barcode & QR Scanner"
    Language="C#"
    MasterPageFile="~/Faculty/IndexMaster.master"
    AutoEventWireup="true"
    CodeFile="Scanner.aspx.cs"
    Inherits="Scanner" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <!-- Bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css"
        rel="stylesheet" />

    <!-- Font Awesome -->
    <link rel="stylesheet"
        href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.2/css/all.min.css" />

    <!-- QR Scanner -->
    <script src="https://unpkg.com/html5-qrcode"
        type="text/javascript"></script>


    <style>
.no-zoom {
    font-size: 16px !important;
}

        /* =========================================
           PAGE
        ========================================= */

        .scanner-page {
            min-height: calc(100vh - 80px);
            background: #f5f7fb;
            padding: 30px 20px;
        }

        .scanner-container {
            max-width: 1100px;
            margin: auto;
        }


        /* =========================================
           SCANNER CARD
        ========================================= */

        .scanner-card {
            background: #ffffff;
            border-radius: 18px;
            border: none;
            overflow: hidden;
            box-shadow: 0 8px 30px rgba(0,0,0,.08);
        }


        .scanner-header {
            background: linear-gradient( 135deg, #0d6efd, #084298 );
            color: white;
            padding: 22px 25px;
        }

            .scanner-header h3 {
                font-size: 22px;
                font-weight: 700;
                margin: 0;
            }

            .scanner-header p {
                margin: 5px 0 0;
                opacity: .85;
                font-size: 14px;
            }


        /* =========================================
           CAMERA AREA
        ========================================= */

        .camera-section {
            padding: 30px;
        }

        .camera-box {
            position: relative;
            max-width: 520px;
            margin: auto;
            background: #111;
            border-radius: 18px;
            padding: 10px;
            box-shadow: 0 10px 30px rgba(0,0,0,.18);
            overflow: hidden;
        }


        /* Scanner library container */

        #reader {
            width: 100% !important;
            border: none !important;
            background: #111;
            border-radius: 12px;
            overflow: hidden;
        }

            #reader video {
                width: 100% !important;
                height: auto !important;
                display: block;
                border-radius: 10px;
            }


        /* Remove default library border */

        #reader__scan_region {
            border: none !important;
        }


        /* =========================================
           SCAN FRAME
        ========================================= */

        .scan-overlay {
            position: absolute;
            left: 50%;
            top: 50%;
            transform: translate(-50%, -50%);
            width: 70%;
            height: 180px;
            pointer-events: none;
            z-index: 5;
        }

        .scan-corner {
            position: absolute;
            width: 35px;
            height: 35px;
            border-color: #00ff88;
            border-style: solid;
        }

        .corner-tl {
            top: 0;
            left: 0;
            border-width: 4px 0 0 4px;
        }

        .corner-tr {
            top: 0;
            right: 0;
            border-width: 4px 4px 0 0;
        }

        .corner-bl {
            bottom: 0;
            left: 0;
            border-width: 0 0 4px 4px;
        }

        .corner-br {
            bottom: 0;
            right: 0;
            border-width: 0 4px 4px 0;
        }


        /* Scanning animation */

        .scan-line {
            position: absolute;
            left: 5px;
            right: 5px;
            height: 2px;
            background: #00ff88;
            box-shadow: 0 0 10px #00ff88;
            animation: scanAnimation 2s infinite;
        }

        @keyframes scanAnimation {

            0% {
                top: 5px;
            }

            50% {
                top: calc(100% - 5px);
            }

            100% {
                top: 5px;
            }
        }
.no-zoom {
    font-size: 16px !important;
}

        /* =========================================
           CAMERA INSTRUCTIONS
        ========================================= */

        .camera-instruction {
            text-align: center;
            margin-top: 18px;
        }

            .camera-instruction .camera-icon {
                font-size: 28px;
                color: #0d6efd;
                margin-bottom: 8px;
            }

            .camera-instruction h5 {
                font-weight: 600;
                margin-bottom: 4px;
            }

            .camera-instruction p {
                color: #6c757d;
                font-size: 14px;
                margin: 0;
            }


        /* =========================================
           MANUAL SEARCH
        ========================================= */

        .manual-search {
            max-width: 520px;
            margin: 25px auto 0;
            padding-top: 25px;
            border-top: 1px solid #e5e7eb;
        }

        .search-title {
            text-align: center;
            font-size: 15px;
            font-weight: 600;
            color: #495057;
            margin-bottom: 15px;
        }

        .search-input {
            height: 48px;
            border-radius: 10px;
            border: 1px solid #ced4da;
        }

            .search-input:focus {
                border-color: #0d6efd;
                box-shadow: 0 0 0 .2rem rgba(13,110,253,.12);
            }

        .search-button {
            height: 48px;
            border-radius: 10px;
            font-weight: 600;
            background: #ff6600 !important;
            border: none;
            color: white !important;
        }

        /* =========================================
           STUDENT DETAILS
        ========================================= */

        .student-card {
            background: #fff;
            border-radius: 18px;
            border: none;
            overflow: hidden;
            box-shadow: 0 8px 30px rgba(0,0,0,.08);
        }

        .student-header {
            background: linear-gradient( 135deg, #0d6efd, #084298 );
            color: white;
            padding: 20px 25px;
        }

            .student-header h4 {
                margin: 0;
                font-weight: 700;
            }


        /* =========================================
           STUDENT IMAGE
        ========================================= */

        .student-image-wrapper {
            text-align: center;
            padding: 25px 0 15px;
        }

        .student-image {
            width: 190px;
            height: 190px;
            object-fit: cover;
            border-radius: 50%;
            border: 5px solid #fff;
            box-shadow: 0 5px 20px rgba(0,0,0,.18);
        }


        /* =========================================
           STUDENT FORM
        ========================================= */

        .student-body {
            padding: 25px 35px 35px;
        }

        .student-details-grid {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 20px 25px;
        }

        .student-field label {
            font-size: 13px;
            font-weight: 700;
            color: #495057;
            margin-bottom: 6px;
        }

        .student-field .form-control {
            min-height: 44px;
            border-radius: 8px;
            background: #f8f9fa;
        }

            .student-field .form-control:disabled {
                background: #f8f9fa;
                color: #212529;
                opacity: 1;
            }

        .full-width {
            grid-column: 1 / 3;
        }


        /* =========================================
           STATUS
        ========================================= */

        .status-input {
            font-weight: 700 !important;
            text-align: center;
        }


        /* =========================================
           CANCEL
        ========================================= */

        .cancel-button {
            margin-top: 25px;
            height: 48px;
            border-radius: 10px;
            font-weight: 600;
            border: none;
        }


        /* =========================================
           MOBILE
        ========================================= */
        @media (max-width: 767px) {

            input,
            textarea,
            select,
            .form-control {
                font-size: 16px !important;
            }

            .search-input {
                font-size: 16px !important;
            }
        }

        @media (max-width: 767px) {
input.no-zoom {
        font-size: 16px !important;
        line-height: 1.5 !important;
        transform: none !important;
        -webkit-text-size-adjust: 100% !important;
    }

            .scanner-page {
                padding: 10px;
            }

            .scanner-header {
                padding: 17px;
            }

                .scanner-header h3 {
                    font-size: 19px;
                }

                .scanner-header p {
                    font-size: 12px;
                }

            .camera-section {
                padding: 15px;
            }

            .camera-box {
                padding: 5px;
                border-radius: 12px;
            }

            #reader {
                border-radius: 8px;
            }

                #reader video {
                    border-radius: 8px;
                }

            .scan-overlay {
                width: 75%;
                height: 140px;
            }

            .scan-corner {
                width: 28px;
                height: 28px;
            }

            .camera-instruction {
                margin-top: 15px;
            }

                .camera-instruction h5 {
                    font-size: 16px;
                }

                .camera-instruction p {
                    font-size: 12px;
                }

            .manual-search {
                margin-top: 20px;
                padding-top: 20px;
            }

            .student-header {
                padding: 16px;
            }

                .student-header h4 {
                    font-size: 18px;
                }

            .student-body {
                padding: 15px;
            }

            .student-details-grid {
                display: block;
            }

            .student-field {
                margin-bottom: 15px;
            }

            .full-width {
                grid-column: auto;
            }

            .student-image {
                width: 150px;
                height: 150px;
            }

            .cancel-button {
                margin-top: 10px;
            }
        }


        /* =========================================
           SMALL MOBILE
        ========================================= */

        @media (max-width: 400px) {

            .scanner-page {
                padding: 5px;
            }

            .camera-section {
                padding: 10px;
            }

            .scan-overlay {
                width: 80%;
                height: 120px;
            }

            .student-image {
                width: 135px;
                height: 135px;
            }
        }
    </style>


    <div class="scanner-page">

        <div class="scanner-container">


            <!-- =====================================================
                 SCANNER SECTION
            ====================================================== -->

            <div id="mainQRCode"
                runat="server"
                class="scanner-card">

                <!-- Header -->

                <div class="scanner-header">

                    <h3>
                        <i class="fa-solid fa-qrcode me-2"></i>
                        Student Scanner
                    </h3>

                    <p>
                        Scan student barcode or QR code
                    </p>

                </div>


                <div class="camera-section">


                    <!-- Hidden ASP.NET Controls -->

                    <asp:HiddenField
                        ID="lblstudentId"
                        runat="server" />

                    <asp:Button
                        ID="btnSubmit"
                        runat="server"
                        Text="Submit"
                        Style="display: none;"
                        OnClick="btnSubmit_Click" />


                    <!-- CAMERA -->

                    <div class="camera-box">

                        <div id="reader"></div>


                        <!-- Scanner Overlay -->

                        <div class="scan-overlay">

                            <div class="scan-corner corner-tl"></div>

                            <div class="scan-corner corner-tr"></div>

                            <div class="scan-corner corner-bl"></div>

                            <div class="scan-corner corner-br"></div>

                            <div class="scan-line"></div>

                        </div>

                    </div>


                    <!-- Camera Instruction -->

                    <div class="camera-instruction">

                        <div class="camera-icon">

                            <i class="fa-solid fa-camera"></i>

                        </div>

                        <h5>Scan Student ID
                        </h5>

                        <p>
                            Point your camera at the barcode or QR code
                        </p>

                    </div>


                    <!-- Manual Search -->

                    <div class="manual-search">

                        <div class="search-title">

                            <i class="fa-solid fa-keyboard me-1"></i>

                            Or enter enrollment number manually

                        </div>


                        <div class="row g-2">

                            <div class="col-12 col-md-8">

                                <asp:TextBox
                                    ID="txtEnrollmentNo_"
                                    runat="server" 
                                    CssClass="form-control search-input no-zoom"
                                    placeholder="Enter Enrollment No" />

                            </div>

                            <div class="col-12 col-md-4">

                                <asp:Button
                                    ID="btnSearch"
                                    runat="server"
                                    Text="Search"
                                    CssClass="btn search-button w-100"
                                    OnClick="btnSearch_Click" />

                            </div>

                        </div>

                    </div>

                </div>

            </div>


            <!-- =====================================================
                 STUDENT DETAILS
            ====================================================== -->

            <div id="studentDetails"
                runat="server"
                visible="false"
                class="student-card">


                <!-- Header -->

                <div class="student-header">

                    <h4>

                        <i class="fa-solid fa-user-graduate me-2"></i>

                        Student Details

                    </h4>

                </div>


                <div class="student-body">


                    <!-- Student Image -->

                    <div class="student-image-wrapper">

                        <asp:Image
                            ID="stImg"
                            runat="server"
                            AlternateText="Student Image"
                            CssClass="student-image" />

                    </div>


                    <!-- Details -->

                    <div class="student-details-grid">


                        <!-- Enrollment -->

                        <div class="student-field">

                            <label>
                                Enrollment No
                            </label>

                            <asp:TextBox
                                ID="txtStudentID"
                                runat="server"
                                Enabled="false"
                                CssClass="form-control" />

                        </div>


                        <!-- Name -->

                        <div class="student-field">

                            <label>
                                Full Name
                            </label>

                            <asp:TextBox
                                ID="txtName"
                                runat="server"
                                Enabled="false"
                                CssClass="form-control" />

                        </div>


                        <!-- Course -->

                        <div class="student-field">

                            <label>
                                Course
                            </label>

                            <asp:TextBox
                                ID="txtCourse"
                                runat="server"
                                Enabled="false"
                                CssClass="form-control" />

                        </div>


                        <!-- Gender -->

                        <div class="student-field">

                            <label>
                                Gender
                            </label>

                            <asp:TextBox
                                ID="txtGender"
                                runat="server"
                                Enabled="false"
                                CssClass="form-control" />

                        </div>


                        <!-- DOB -->

                        <div class="student-field">

                            <label>
                                Date of Birth
                            </label>

                            <asp:TextBox
                                ID="txtDOB"
                                runat="server"
                                Enabled="false"
                                CssClass="form-control" />

                        </div>


                        <!-- Religion -->

                        <div class="student-field">

                            <label>
                                Religion
                            </label>

                            <asp:TextBox
                                ID="txtReligion"
                                runat="server"
                                Enabled="false"
                                CssClass="form-control" />

                        </div>


                        <!-- Hosteller -->

                        <div class="student-field">

                            <label>
                                Hosteller
                            </label>

                            <asp:TextBox
                                ID="txthostler"
                                runat="server"
                                Enabled="false"
                                CssClass="form-control status-input" />

                        </div>


                        <!-- Transport -->

                        <div class="student-field">

                            <label>
                                Transport
                            </label>

                            <asp:TextBox
                                ID="txttransport"
                                runat="server"
                                Enabled="false"
                                CssClass="form-control status-input" />

                        </div>


                        <!-- Email -->

                        <div class="student-field">

                            <label>
                                Email
                            </label>

                            <asp:TextBox
                                ID="txtEmail"
                                runat="server"
                                Enabled="false"
                                TextMode="Email"
                                CssClass="form-control" />

                        </div>


                        <!-- Address -->

                        <div class="student-field full-width">

                            <label>
                                Address
                            </label>

                            <asp:TextBox
                                ID="txtAddress"
                                runat="server"
                                TextMode="MultiLine"
                                Enabled="false"
                                Rows="3"
                                CssClass="form-control" />

                        </div>


                    </div>


                    <!-- Cancel -->

                    <asp:Button
                        ID="btnCancel"
                        runat="server"
                        Text="Scan Another Student"
                        CssClass="btn btn-success w-100 cancel-button"
                        OnClick="btnCancel_Click" />


                </div>

            </div>


        </div>

    </div>


    <!-- =====================================================
         SCANNER JAVASCRIPT
    ====================================================== -->

   <script>

       let html5QrCode = null;
       let isScanning = false;
       let isScanProcessed = false;

       function onScanSuccess(decodedText, decodedResult) {

           if (isScanProcessed)
               return;

           isScanProcessed = true;

           console.log("Scanned code:", decodedText);

           // Set scanned value
           var studentId = document.getElementById(
            '<%= lblstudentId.ClientID %>'
        );

        if (studentId) {
            studentId.value = decodedText;
        }

        // Stop camera before postback
        stopScanner();

        // Submit ASP.NET button automatically
        setTimeout(function () {

            var btn = document.getElementById(
                '<%= btnSubmit.ClientID %>'
            );

            if (btn) {
                btn.click();
            }

        }, 200);
       }


       function onScanFailure(error) {
           // Do nothing.
           // Scanner continuously calls this
           // when QR/Barcode is not detected.
       }


       function startScanner() {

           if (isScanning)
               return;

           try {

               html5QrCode = new Html5Qrcode("reader");

               const config = {

                   fps: 10,

                   qrbox: {
                       width: 250,
                       height: 150
                   },

                   formatsToSupport: [

                       Html5QrcodeSupportedFormats.EAN_13,

                       Html5QrcodeSupportedFormats.CODE_128,

                       Html5QrcodeSupportedFormats.UPC_A,

                       Html5QrcodeSupportedFormats.UPC_E,

                       Html5QrcodeSupportedFormats.CODE_39,

                       Html5QrcodeSupportedFormats.QR_CODE
                   ],

                   aspectRatio: 1.777778

               };


               html5QrCode.start(

                   {
                       facingMode: "environment"
                   },

                   config,

                   onScanSuccess,

                   onScanFailure

               ).then(function () {

                   isScanning = true;

                   console.log("Camera started automatically.");

               }).catch(function (err) {

                   console.error("Camera start error:", err);

                   isScanning = false;

                   // Optional message
                   console.log(
                       "Camera permission is required to scan."
                   );

               });

           }
           catch (error) {

               console.error(
                   "Scanner initialization error:",
                   error
               );

           }
       }


       function stopScanner() {

           if (
               html5QrCode &&
               isScanning
           ) {

               html5QrCode.stop()

                   .then(function () {

                       console.log(
                           "Scanner stopped."
                       );

                       isScanning = false;

                   })

                   .catch(function (err) {

                       console.log(
                           "Scanner stop error:",
                           err
                       );

                       isScanning = false;

                   });
           }
       }


       // Automatically start camera when page loads

       document.addEventListener(
           "DOMContentLoaded",
           function () {

               setTimeout(function () {

                   startScanner();

               }, 300);

           }
       );

   </script>

</asp:Content>
