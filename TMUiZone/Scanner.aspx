<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Scanner.aspx.cs" Inherits="Scanner" %>

<!DOCTYPE html>
<script runat="server">


</script>

<html>
<head>
    <meta charset="UTF-8" />
    <title>Barcode & QR Scanner</title>
    <script src="https://unpkg.com/html5-qrcode" type="text/javascript"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />

    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style>
        #reader {
            width: 300px;
            margin: auto;
        }
    </style>

</head>
<body>
    <form runat="server">
        <div id="mainQRCode" runat="server" style="text-align: center">


            <h2>Scan Barcode or QR Code</h2>


            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" />
            <asp:HiddenField ID="lblstudentId" runat="server" />

            <div id="reader" style="text-align: center">
            </div>
            <br />
            
            <div class="mb-3">
                <label for="txthostler" class="form-label">Student Enrollment No_</label>
                <asp:TextBox ID="txtEnrollmentNo_" runat="server"  CssClass="form-control" />
                <br />
                  <asp:Button ID="btnSearch" runat="server" Text="Search"  BackColor="#ff6600" CssClass="form-control" OnClick="btnSearch_Click" />
            </div>
            
        </div>

        <div id="studentDetails" runat="server" visible="false">
            <div class="container mt-5">
                <div class="card shadow">
                    <div class="card-header bg-primary text-white">
                        <h4 class="mb-0">Student Details</h4>
                    </div>
                    <div class="card-body">
                        <div class="mb-3" style="text-align:center">
                            
                           <asp:Image ID="stImg" runat="server" alt="Avatar" Style="width:250px; height:200px; border-radius:50%;" />
                        </div>
                        <div class="mb-3">
                            <label for="txtStudentID" class="form-label">Enrollment No</label>
                            <asp:TextBox ID="txtStudentID" runat="server" Enabled="false" CssClass="form-control" />
                        </div>
                        <div class="mb-3">
                            <label for="txthostler" class="form-label">Hosteller</label>
                            <asp:TextBox ID="txthostler" runat="server" Enabled="false" CssClass="form-control" />
                        </div>
                        <div class="mb-3">
                            <label for="txthostler" class="form-label">Transport</label>
                            <asp:TextBox ID="txttransport" runat="server" Enabled="false" CssClass="form-control" />
                        </div>
                        <div class="mb-3">
                            <label for="txtReligion" class="form-label">Religion</label>
                            <asp:TextBox ID="txtReligion" runat="server" Enabled="false" CssClass="form-control" />
                        </div>
                        <div class="mb-3">
                            <label for="txtName" class="form-label">Full Name</label>
                            <asp:TextBox ID="txtName" runat="server" Enabled="false" CssClass="form-control" />
                        </div>

                        <div class="mb-3">
                            <label for="txtEmail" class="form-label">Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" Enabled="false" TextMode="Email" CssClass="form-control" />
                        </div>

                        <div class="mb-3">
                            <label for="txtDOB" class="form-label">Date of Birth</label>
                            <asp:TextBox ID="txtDOB" runat="server" Enabled="false" CssClass="form-control" />
                        </div>

                        <div class="mb-3">
                            <label class="form-label">Gender</label>
                            <asp:TextBox ID="txtGender" runat="server" Enabled="false" CssClass="form-control" />
                        </div>

                        <div class="mb-3">
                            <label for="ddlCourse" class="form-label">Course</label>
                            <asp:TextBox ID="txtCourse" runat="server" Enabled="false" CssClass="form-control" />
                        </div>

                        <div class="mb-3">
                            <label for="txtAddress" class="form-label">Address</label>
                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Enabled="false" Rows="3" CssClass="form-control" />
                        </div>

                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="form-control" BackColor="Green" OnClick="btnCancel_Click" />
                        <br />
                        <br />

                    </div>
                </div>
            </div>
        </div>
    </form>


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
</body>
</html>

