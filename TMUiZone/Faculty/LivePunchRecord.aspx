<%@ Page Title="Live Biometric Punch"
    Language="C#"
    MasterPageFile="~/Faculty/IndexMaster.master"
    AutoEventWireup="true"
    CodeFile="LivePunchRecord.aspx.cs"
    Inherits="Faculty_LivePunchRecord" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="Server">

    <meta name="viewport"
        content="width=device-width, initial-scale=1" />

    <style type="text/css">
        .live-punch-container {
            width: 100%;
            max-width: 100%;
            min-width: 0;
            box-sizing: border-box;
            overflow: hidden;
        }

        #punchContainer.live-punch-grid {
            display: flex !important;
            flex-direction: row !important;
            flex-wrap: nowrap !important;
            justify-content: flex-start !important;
            align-items: stretch;
            width: 100% !important;
            max-width: 100% !important;
            min-width: 0 !important;
            overflow-x: auto !important;
            overflow-y: hidden !important;
            gap: 16px;
            padding: 5px 5px 16px;
            box-sizing: border-box;
            scroll-behavior: smooth;
            -webkit-overflow-scrolling: touch;
            overscroll-behavior-x: contain;
        }

        /* Keep cards at a fixed width */
        #punchContainer > .punch-card {
            flex: 0 0 1300px !important;
            width: 1300px !important;
            min-width: 1300px !important;
            max-width: 1300px !important;
            box-sizing: border-box;
        }

        /* Visible horizontal scrollbar */
        #punchContainer::-webkit-scrollbar {
            height: 12px;
        }

        #punchContainer::-webkit-scrollbar-track {
            background: #edf2f7;
            border-radius: 10px;
        }

        #punchContainer::-webkit-scrollbar-thumb {
            background: #8096b2;
            border-radius: 10px;
        }

        #punchContainer {
            scrollbar-width: auto;
            scrollbar-color: #8096b2 #edf2f7;
        }

        /* Header */
        .live-punch-header {
            display: flex;
            align-items: center;
            justify-content: space-between;
            flex-wrap: wrap;
            gap: 12px;
            padding: 12px 16px;
            margin-bottom: 12px;
            background: #ffffff;
            border: 1px solid #e1e7f0;
            border-radius: 12px;
        }

            .live-punch-header h2 {
                margin: 0;
                color: #062b61;
                font-size: 21px;
                font-weight: 700;
            }

        /* Machine buttons */
        .machine-selector {
            display: flex;
            align-items: center;
            gap: 8px;
        }

        .machine-btn {
            padding: 9px 18px;
            border: 1px solid #cbd5e1;
            border-radius: 8px;
            background: #ffffff;
            color: #17365d;
            font-size: 14px;
            font-weight: 700;
            cursor: pointer;
            transition: background .2s ease, color .2s ease, border-color .2s ease;
        }

            .machine-btn:hover {
                background: #edf4ff;
                border-color: #174ea6;
            }

            .machine-btn.active {
                background: #073b78;
                color: #ffffff;
                border-color: #073b78;
                box-shadow: 0 3px 9px rgba(7,59,120,.25);
            }

            .machine-btn:focus {
                outline: 2px solid #80b5ff;
                outline-offset: 2px;
            }

        /* Connection status */
        .live-status {
            display: inline-block;
            padding: 6px 12px;
            border-radius: 20px;
            background: #dcfce7;
            color: #166534;
            font-size: 12px;
            font-weight: 700;
            white-space: nowrap;
        }

            .live-status.error {
                background: #fee2e2;
                color: #991b1b;
            }

        /* Horizontal cards */
        .live-punch-grid {
            display: flex;
            flex-direction: row;
            flex-wrap: nowrap;
            align-items: stretch;
            gap: 16px;
            width: 100%;
            padding: 2px 2px 12px;
            box-sizing: border-box;
            overflow-x: auto;
            overflow-y: hidden;
            -webkit-overflow-scrolling: touch;
            scrollbar-width: thin;
            scrollbar-color: #a8b9d0 #edf2f7;
        }

            .live-punch-grid::-webkit-scrollbar {
                height: 8px;
            }

            .live-punch-grid::-webkit-scrollbar-track {
                background: #edf2f7;
                border-radius: 10px;
            }

            .live-punch-grid::-webkit-scrollbar-thumb {
                background: #a8b9d0;
                border-radius: 10px;
            }

        /* Student card */
        .punch-card {
            display: grid;
            grid-template-columns: 400px minmax(0, 1fr);
            grid-template-rows: auto 1fr;
            column-gap: 16px;
            row-gap: 12px;
            flex: 0 0 1000px;
            width: 1300px;
            min-width: 1300px;
            max-width: 1300px;
            min-height: 340px;
            padding: 16px;
            box-sizing: border-box;
            background: #ffffff;
            border: 1px solid #e1e7f0;
            border-radius: 18px;
            box-shadow: 0 5px 20px rgba(20,40,75,.08);
            overflow: hidden;
            transition: box-shadow .2s ease, border-color .2s ease;
        }

            .punch-card:hover {
                box-shadow: 0 8px 24px rgba(20,40,75,.13);
                border-color: #b8cce5;
            }

        .punch-card-top {
            display: contents;
        }

        /* Photo spans two rows */
        .student-photo,
        .student-photo-placeholder {
            grid-column: 1;
            grid-row: 1 / span 2;
            align-self: stretch;
            width: 100%;
            height: 100%;
            min-height: 0;
            box-sizing: border-box;
            border-radius: 14px;
        }

        .student-photo {
            display: block;
            object-fit: cover;
            object-position: center top;
        }

        .student-photo-placeholder {
            display: flex;
            align-items: center;
            justify-content: center;
            background: linear-gradient(145deg,#e8eef8,#d9e6f7);
            color: #062b61;
            font-size: 54px;
            font-weight: 800;
            border: 1px solid #d5dfef;
        }

        /* Student identity */
        .punch-card-top > div {
            grid-column: 2;
            grid-row: 1;
            align-self: start;
            min-width: 0;
            padding: 14px 16px;
            background: #f2f5fa;
            border-radius: 14px;
            box-sizing: border-box;
        }

        .student-name {
            margin: 0 0 7px;
            color: #172033;
            font-size: 25px;
            font-weight: 750;
            line-height: 1.25;
            overflow-wrap: anywhere;
        }

        .student-number {
            display: inline-block;
            padding: 5px 9px;
            color: #174ea6;
            background: #ffffff;
            border: 1px solid #e1e7f0;
            border-radius: 7px;
            font-size: 12px;
            font-weight: 700;
            overflow-wrap: anywhere;
        }

        /* Student fields */
        .punch-fields {
            grid-column: 2;
            grid-row: 2;
            display: grid;
            grid-template-columns: repeat(2,minmax(0,1fr));
            grid-auto-rows: minmax(68px,auto);
            align-content: start;
            gap: 9px;
            width: 100%;
            min-width: 0;
            box-sizing: border-box;
        }

        .punch-field {
            display: flex;
            flex-direction: column;
            justify-content: center;
            gap: 5px;
            min-width: 0;
            min-height: 68px;
            padding: 10px 11px;
            background: #f8fafc;
            border: 1px solid #e5eaf2;
            border-radius: 9px;
            box-sizing: border-box;
            transition: background .2s ease, border-color .2s ease;
        }

            .punch-field:hover {
                background: #f0f5fc;
                border-color: #cbd9ec;
            }

        .punch-field-label {
            color: #64748b;
            font-size: 10px;
            font-weight: 600;
            line-height: 1.3;
            overflow-wrap: anywhere;
        }

        .punch-field-value {
            color: #1e293b;
            font-size: 13px;
            font-weight: 700;
            line-height: 1.4;
            overflow-wrap: anywhere;
            word-break: break-word;
        }

        /* Loading / empty state */
        .punch-message {
            flex: 0 0 100%;
            width: 100%;
            padding: 24px 12px;
            box-sizing: border-box;
            text-align: center;
            background: #ffffff;
            border: 1px solid #e2e8f0;
            border-radius: 10px;
            color: #64748b;
        }

        .punch-footer {
            margin-top: 8px;
            color: #64748b;
            font-size: 11px;
            text-align: right;
        }

        /* Tablet */
        @media (max-width: 900px) {
            .punch-card {
                flex-basis: 820px;
                width: 820px;
                min-width: 820px;
                max-width: 820px;
                grid-template-columns: 190px minmax(0,1fr);
                column-gap: 12px;
                padding: 12px;
                min-height: 280px;
            }

            .student-photo,
            .student-photo-placeholder {
                min-height: 270px;
            }

            .punch-card-top > div {
                padding: 11px;
            }

            .student-name {
                font-size: 21px;
            }

            .punch-fields {
                gap: 7px;
                grid-auto-rows: minmax(60px,auto);
            }

            .punch-field {
                min-height: 60px;
                padding: 8px;
            }
        }

        /* Mobile */
        @media (max-width: 600px) {
            .live-punch-container {
                padding: 5px;
            }

            .live-punch-header {
                align-items: flex-start;
                padding: 11px;
            }

                .live-punch-header h2 {
                    width: 100%;
                    font-size: 17px;
                }

            .machine-selector {
                flex-wrap: wrap;
                gap: 8px;
            }

            .machine-btn {
                min-width: 110px;
                padding: 9px 12px;
            }

            .live-punch-grid {
                gap: 10px;
            }

            .punch-card {
                flex-basis: 660px;
                width: 660px;
                min-width: 660px;
                max-width: 660px;
                grid-template-columns: 155px minmax(0,1fr);
                column-gap: 10px;
                row-gap: 9px;
                padding: 10px;
                border-radius: 12px;
                min-height: 260px;
            }

            .student-photo,
            .student-photo-placeholder {
                min-height: 245px;
                border-radius: 9px;
            }

            .student-photo-placeholder {
                font-size: 36px;
            }

            .punch-card-top > div {
                padding: 9px;
                border-radius: 9px;
            }

            .student-name {
                font-size: 18px;
                margin-bottom: 5px;
            }

            .student-number {
                font-size: 11px;
                padding: 4px 6px;
            }

            .punch-fields {
                gap: 5px;
                grid-auto-rows: minmax(54px,auto);
            }

            .punch-field {
                min-height: 54px;
                padding: 7px;
                border-radius: 7px;
            }

            .punch-field-label {
                font-size: 10px;
            }

            .punch-field-value {
                font-size: 12px;
            }

            /* Main container */
            .live-punch-container {
                width: 100%;
                max-width: 100%;
                min-width: 0;
                box-sizing: border-box;
                overflow: visible !important;
            }

            /* Cards container: horizontal scroll */
            #punchContainer.live-punch-grid {
                display: flex !important;
                flex-direction: row !important;
                flex-wrap: nowrap !important;
                width: 100% !important;
                max-width: 100% !important;
                min-width: 0 !important;
                overflow-x: auto !important;
                overflow-y: hidden !important;
                gap: 16px;
                padding: 5px 5px 14px;
                box-sizing: border-box;
                scroll-behavior: smooth;
                -webkit-overflow-scrolling: touch;
                touch-action: pan-x pan-y;
            }

            /* Each card must keep its fixed width */
            #punchContainer .punch-card {
                flex: 0 0 1000px !important;
                width: 1000px !important;
                min-width: 1000px !important;
                max-width: 1000px !important;
                box-sizing: border-box;
            }

            /* Visible scrollbar */
            #punchContainer::-webkit-scrollbar {
                height: 10px;
            }

            #punchContainer::-webkit-scrollbar-track {
                background: #e8edf4;
                border-radius: 10px;
            }

            #punchContainer::-webkit-scrollbar-thumb {
                background: #8096b2;
                border-radius: 10px;
            }

            #punchContainer {
                scrollbar-width: auto;
                scrollbar-color: #8096b2 #e8edf4;
            }
        }
    </style>

</asp:Content>

<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="Server">

    <div class="live-punch-container">

        <div class="live-punch-header">

            <h2>Live Biometric Punch Records</h2>

            <div class="machine-selector">

                <button type="button"
                    class="machine-btn active"
                    aria-pressed="true"
                    onclick="selectMachine(21, this)">
                    Hospital Emergency Gate
                </button>

                <button type="button"
                    class="machine-btn"
                    aria-pressed="false"
                    onclick="selectMachine(22, this)">
                    SecurityGate No 2
                </button>

            </div>

            <span id="connectionStatus"
                class="live-status">Connecting...
            </span>

        </div>

        <div id="punchContainer"
            class="live-punch-grid"
            aria-live="polite">

            <div class="punch-message">
                Loading latest biometric punches...
            </div>

        </div>

        <div class="punch-footer">
            Last refreshed:
            <span id="lastUpdated">--</span>
        </div>

    </div>

    <script type="text/javascript">
        (function () {
            "use strict";

            var container =
                document.getElementById("punchContainer");

            var status =
                document.getElementById("connectionStatus");

            var lastUpdated =
                document.getElementById("lastUpdated");

            var selectedDeviceId = 21;
            var requestNumber = 0;
            var refreshTimer = null;

            function safeText(value) {
                if (value === null ||
                    value === undefined ||
                    value === "") {
                    return "-";
                }

                return String(value);
            }

            function createElement(tag, className, text) {
                var element = document.createElement(tag);

                if (className) {
                    element.className = className;
                }

                if (text !== undefined && text !== null) {
                    element.textContent = text;
                }

                return element;
            }

            function createPunchCard(student) {
                var card = createElement("div", "punch-card");
                var top = createElement("div", "punch-card-top");

                var imageUrl = student.studentImage || "";
                var photo;

                if (imageUrl !== "") {
                    photo = createElement("img", "student-photo");
                    photo.alt = "Student photo";
                    photo.src = imageUrl;

                    photo.onerror = function () {
                        var placeholder = createElement(
                            "div",
                            "student-photo-placeholder",
                            "S"
                        );

                        if (photo.parentNode) {
                            photo.parentNode.replaceChild(
                                placeholder,
                                photo
                            );
                        }
                    };
                } else {
                    photo = createElement(
                        "div",
                        "student-photo-placeholder",
                        "S"
                    );
                }

                var identity =
                    createElement("div", "student-identity");

                identity.appendChild(
                    createElement(
                        "div",
                        "student-name",
                        safeText(student.studentName)
                    )
                );

                identity.appendChild(
                    createElement(
                        "div",
                        "student-number",
                        safeText(student.Graduation)
                    )
                );

                top.appendChild(photo);
                top.appendChild(identity);

                var fieldsContainer =
                    createElement("div", "punch-fields");

                var fields = student.fields || [];

                for (var i = 0; i < fields.length; i++) {
                    var field =
                        createElement("div", "punch-field");

                    field.appendChild(
                        createElement(
                            "div",
                            "punch-field-label",
                            safeText(fields[i].label)
                        )
                    );

                    field.appendChild(
                        createElement(
                            "div",
                            "punch-field-value",
                            safeText(fields[i].value)
                        )
                    );

                    fieldsContainer.appendChild(field);
                }

                card.appendChild(top);
                card.appendChild(fieldsContainer);

                return card;
            }

            function displayPunches(data) {
                container.textContent = "";

                if (!Array.isArray(data) || data.length === 0) {
                    container.appendChild(
                        createElement(
                            "div",
                            "punch-message",
                            "No biometric punch records found for " +
                            "the selected machine."
                        )
                    );
                    return;
                }

                for (var i = 0; i < data.length; i++) {
                    container.appendChild(
                        createPunchCard(data[i])
                    );
                }

                container.scrollLeft = 0;
            }

            function loadPunches() {
                // Capture the machine and request identity.
                var deviceId = selectedDeviceId;
                var thisRequest = ++requestNumber;

                var url = "LivePunchRecord.aspx?json=1&deviceId=" +
                    encodeURIComponent(deviceId) + "&_=" + new Date().getTime();

                fetch(url, {
                    method: "GET",
                    cache: "no-store",
                    credentials: "same-origin",
                    headers: {
                        "Accept": "application/json"
                    }
                })
                    .then(function (response) {
                        if (!response.ok) {
                            throw new Error(
                                "HTTP " + response.status
                            );
                        }

                        return response.json();
                    })
                    .then(function (data) {
                        // Ignore responses from previous requests.
                        if (thisRequest !== requestNumber ||
                            deviceId !== selectedDeviceId) {
                            return;
                        }

                        if (data && data.error) {
                            throw new Error(data.error);
                        }

                        displayPunches(data);
                        container.scrollLeft = container.scrollWidth;
                        status.textContent = "Live - " +
                            (deviceId === 21
                                ? "Hospital Emergency Gate"
                                : "SecurityGate No 2");

                        status.className = "live-status";

                        lastUpdated.textContent =
                            new Date().toLocaleTimeString();
                    })
                    .catch(function (error) {
                        if (thisRequest !== requestNumber ||
                            deviceId !== selectedDeviceId) {
                            return;
                        }

                        status.textContent = "Connection Error";
                        status.className = "live-status error";

                        if (window.console &&
                            console.error) {
                            console.error(
                                "Live punch error:", error
                            );
                        }
                    });
            }

            // Must be on window for inline HTML onclick.
            window.selectMachine = function (deviceId, button) {
                if (deviceId !== 21 && deviceId !== 22) {
                    return;
                }

                selectedDeviceId = deviceId;

                var buttons =
                    document.querySelectorAll(".machine-btn");

                for (var i = 0; i < buttons.length; i++) {
                    buttons[i].classList.remove("active");
                    buttons[i].setAttribute(
                        "aria-pressed", "false"
                    );
                }

                button.classList.add("active");
                button.setAttribute("aria-pressed", "true");

                status.textContent =
                    "Loading Machine " +
                    (deviceId === 21 ? "1" : "2");

                status.className = "live-status";

                container.textContent = "";

                container.appendChild(
                    createElement(
                        "div",
                        "punch-message",
                        "Loading Machine " +
                        (deviceId === 21 ? "1" : "2") +
                        " biometric punches..."
                    )
                );

                // Invalidate earlier requests and load immediately.
                loadPunches();
            };

            // Initial load.
            loadPunches();

            // Refresh the currently selected machine every second.
            refreshTimer = window.setInterval(function () {
                loadPunches();
            }, 1000);

        })();
    </script>

</asp:Content>
