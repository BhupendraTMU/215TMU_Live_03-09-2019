<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LivePunch.aspx.cs" Inherits="LivePunch" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Live Biometric Punch</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <style>
        body {
            font-family: Arial;
            background: #f3f6fb;
            padding: 20px;
        }

        .card {
            max-width: 850px;
            margin: auto;
            background: white;
            padding: 25px;
            border-radius: 10px;
            box-shadow: 0 3px 15px #ddd;
        }

        h2 {
            color: #062b61;
        }

        .grid {
            display: grid;
            grid-template-columns: repeat(2, minmax(0, 1fr));
            gap: 15px;
        }

        .item {
            background: #f7f9fc;
            padding: 15px;
            border-radius: 6px;
            overflow-wrap: anywhere;
        }

        .label {
            color: #666;
            font-size: 13px;
        }

        .value {
            font-size: 17px;
            font-weight: bold;
            margin-top: 8px;
        }

        #message {
            margin-bottom: 20px;
            color: #067647;
        }

        @media(max-width:600px) {
            .grid {
                grid-template-columns: 1fr;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="card">
            <h2>Live Biometric Punch</h2>

            <div id="message">Connecting...</div>

            <div class="grid">
                <div class="item">
                    <div class="label">Finger No.</div>
                    <div class="value" id="fingerNo">--</div>
                </div>

                <div class="item">
                    <div class="label">Punch Date & Time</div>
                    <div class="value" id="punchTime">--</div>
                </div>

                <div class="item">
                    <div class="label">Student Name</div>
                    <div class="value" id="studentName">--</div>
                </div>

                <div class="item">
                    <div class="label">Student No.</div>
                    <div class="value" id="studentNo">--</div>
                </div>

                <div class="item">
                    <div class="label">College Code</div>
                    <div class="value" id="collegeCode">--</div>
                </div>

                <div class="item">
                    <div class="label">Device Serial</div>
                    <div class="value" id="deviceSerial">--</div>
                </div>
            </div>
        </div>

        <script type="text/javascript">

            // YE LINE ASPX FILE MEIN HI LAGANI HAI
            var dataUrl = '<%= ResolveUrl("~/LivePunch.aspx?json=1") %>';

            function setValue(id, value) {
                document.getElementById(id).textContent =
                    (value === null || value === undefined || value === "")
                        ? "--" : value;
            }

            function loadPunch() {
                fetch(dataUrl, { cache: "no-store" })
                    .then(function (response) {
                        if (!response.ok) {
                            throw new Error("HTTP " + response.status);
                        }
                        return response.json();
                    })
                    .then(function (data) {
                        if (data.error) {
                            throw new Error(data.error);
                        }

                        var p = data.data || data;
                        var emp = p.employee || p.student || {};

                        if (p.status === "waiting") {
                            document.getElementById("message").textContent =
                                "Waiting for punch...";
                            return;
                        }

                        setValue("fingerNo",
                            p.fingerNo || p.punchNumber || p.PIN);

                        setValue("punchTime",
                            p.punchTime || p.punchDateTime || p.time);

                        setValue("studentName",
                            p.studentName || emp.studentName ||
                            emp.employeeName || emp.name);

                        setValue("studentNo",
                            p.studentNo || emp.studentNo ||
                            emp.employeeCode || emp.enrollmentNo);

                        setValue("collegeCode",
                            p.collegeCode || emp.collegeCode ||
                            emp.department);

                        setValue("deviceSerial",
                            p.deviceSerial || p.SN || p.serialNumber);

                        document.getElementById("message").textContent =
                            "Connected - punch data received";
                    })
                    .catch(function (error) {
                        document.getElementById("message").textContent =
                            "Error: " + error.message;
                    });
            }

            loadPunch();
            setInterval(loadPunch, 2000);

        </script>
    </form>
</body>
</html>
