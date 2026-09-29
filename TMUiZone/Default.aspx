<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>TMU ERP - Login</title>

    <!-- Bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css"
        rel="stylesheet" />

    <!-- Font Awesome -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.2/css/all.min.css"
        rel="stylesheet" />

    <style type="text/css">

        /* =========================================
           GLOBAL
        ========================================= */

        * {
            box-sizing: border-box;
        }

        html,
        body {
            width: 100%;
            height: 100%;
            margin: 0;
            padding: 0;
            overflow: hidden;
            font-family: "Segoe UI", Arial, sans-serif;
        }


        /* =========================================
           BACKGROUND IMAGE
        ========================================= */

        #bgImage {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            object-fit: cover;
            z-index: -5;
            background: #111;
        }


        /* =========================================
           DARK OVERLAY
        ========================================= */

        .video-overlay {
            position: fixed;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;

            background: linear-gradient(
                90deg,
                rgba(0, 0, 0, 0.35) 0%,
                rgba(0, 0, 0, 0.12) 40%,
                rgba(0, 0, 0, 0.68) 100%
            );

            z-index: -4;
        }


        /* =========================================
           MAIN PAGE
        ========================================= */

        .login-page {
            width: 100%;
            min-height: 100vh;

            display: flex;
            align-items: center;
            justify-content: space-between;

            padding: 35px 5%;

            position: relative;
        }


        /* =========================================
           MODAL
        ========================================= */

        .modalBackground {
            background-color: rgba(0, 0, 0, 0.65);
        }

        .modalPopup {
            background: white;
            width: 450px;
            max-width: 90%;
            padding: 20px;
            border-radius: 12px;
            box-shadow: 0 10px 40px rgba(0, 0, 0, 0.4);
        }

        .modalPopup .body {
            width: 100%;
        }

        .modalPopup .button {
            float: right;
            border: none;
            background: #990033;
            color: white;
            border-radius: 4px;
            cursor: pointer;
        }


        /* =========================================
           LEFT BRANDING
        ========================================= */

        .branding {
            position: absolute;
            top: 35px;
            left: 5%;

            color: white;
            z-index: 2;
        }

        .brand-title {
            font-size: 38px;
            font-weight: 700;
            line-height: 1.05;
            letter-spacing: 1px;

            text-shadow: 0 3px 15px rgba(0, 0, 0, 0.7);
        }

        .brand-subtitle {
            font-size: 17px;
            margin-top: 8px;
            letter-spacing: 0.5px;
        }


        /* =========================================
           WELCOME TEXT
        ========================================= */

        .welcome {
            position: absolute;
            left: 5%;
            bottom: 16%;

            color: white;
            z-index: 2;
        }

        .welcome h1 {
            font-size: 43px;
            font-weight: 700;
            margin: 0 0 10px 0;

            text-shadow: 0 3px 15px rgba(0, 0, 0, 0.8);
        }

        .welcome p {
            font-size: 21px;
            margin: 0;

            text-shadow: 0 2px 10px rgba(0, 0, 0, 0.7);
        }

        .orange-line {
            width: 75px;
            height: 4px;

            background: #ff8a00;

            margin-top: 18px;

            border-radius: 10px;
        }


        /* =========================================
           LOGIN CARD
        ========================================= */

        .login-container {
            width: 450px;
            max-width: 94vw;

            padding: 38px 42px;

            border-radius: 26px;

            background: rgba(10, 10, 10, 0.52);

            border: 1px solid rgba(255, 255, 255, 0.38);

            backdrop-filter: blur(18px);
            -webkit-backdrop-filter: blur(18px);

            box-shadow:
                0 25px 70px rgba(0, 0, 0, 0.50),
                inset 0 1px 1px rgba(255, 255, 255, 0.15);

            color: white;

            margin-left: auto;

            z-index: 10;
        }


        /* =========================================
           LOGIN ICON / LOGO
        ========================================= */

        .login-icon {
            width: 70px;
            height: 70px;

            margin: 0 auto 17px auto;

            display: flex;
            align-items: center;
            justify-content: center;

            color: #ff8a00;

            font-size: 30px;
        }

        .login-icon img {
            width: 350px;
            height: 120px;

            object-fit: contain;

            border-radius: 50%;
        }


        /* =========================================
           LOGIN TITLE
        ========================================= */

        .login-title {
            text-align: center;

            font-size: 33px;
            font-weight: 600;

            margin: 0;
        }

        .login-subtitle {
            text-align: center;

            color: rgba(255, 255, 255, 0.82);

            font-size: 16px;

            margin-top: 5px;
        }

        .login-line {
            width: 60px;
            height: 3px;

            background: #ff8a00;

            margin: 18px auto 30px auto;

            border-radius: 10px;
        }


        /* =========================================
           LABEL
        ========================================= */

        .input-label {
            display: block;

            color: white;

            font-size: 15px;

            margin-bottom: 8px;
        }


        /* =========================================
           INPUT
        ========================================= */

        .input-wrapper {
            position: relative;

            margin-bottom: 22px;
        }

        .left-icon {
            position: absolute;

            left: 18px;
            top: 17px;

            color: rgba(255, 255, 255, 0.78);

            font-size: 18px;

            z-index: 3;
        }

        .login-input {
            width: 100%;
            height: 54px;

            padding: 0 50px;

            border-radius: 12px;

            border: 1px solid rgba(255, 255, 255, 0.40);

            background: rgba(255, 255, 255, 0.07);

            color: white;

            font-size: 15px;

            outline: none;

            transition: all 0.25s ease;
        }

        .login-input::placeholder {
            color: rgba(255, 255, 255, 0.62);
        }

        .login-input:focus {
            border-color: #ff8a00;

            background: rgba(255, 255, 255, 0.11);

            box-shadow: 0 0 0 3px rgba(255, 138, 0, 0.12);
        }


        /* =========================================
           PASSWORD EYE
        ========================================= */

        .password-eye {
            position: absolute;

            right: 18px;
            top: 17px;

            color: rgba(255, 255, 255, 0.78);

            cursor: pointer;

            z-index: 4;
        }

        .password-eye:hover {
            color: #ff9800;
        }


        /* =========================================
           OPTIONS
        ========================================= */

        .login-options {
            display: flex;

            justify-content: space-between;

            align-items: center;

            font-size: 14px;

            margin: 3px 2px 24px;
        }

        .remember {
            display: flex;

            align-items: center;

            gap: 8px;

            cursor: pointer;
        }

        .remember input {
            width: 17px;
            height: 17px;

            accent-color: #ff8a00;

            cursor: pointer;
        }

        .forgot {
            color: #ff9d26;

            text-decoration: none;
        }

        .forgot:hover {
            color: #ffb45b;

            text-decoration: underline;
        }


        /* =========================================
           LOGIN BUTTON
        ========================================= */

        .btn-login {
            width: 100%;
            height: 55px;

            border: none;

            border-radius: 12px;

            background: linear-gradient(
                135deg,
                #ff9800,
                #f27600
            );

            color: white;

            font-size: 17px;

            font-weight: 600;

            letter-spacing: 0.5px;

            cursor: pointer;

            transition: all 0.25s ease;

            box-shadow:
                0 8px 25px rgba(255, 130, 0, 0.25);
        }

        .btn-login:hover {
            transform: translateY(-2px);

            box-shadow:
                0 12px 30px rgba(255, 130, 0, 0.40);
        }


        /* =========================================
           OR SECTION
        ========================================= */

        .or-section {
            display: flex;

            align-items: center;

            gap: 14px;

            margin: 25px 0;

            color: rgba(255, 255, 255, 0.85);
        }

        .or-line {
            height: 1px;

            flex: 1;

            background: rgba(255, 255, 255, 0.28);
        }


        /* =========================================
           SSO BUTTON
        ========================================= */

        .btn-sso {
            width: 100%;
            height: 54px;

            border-radius: 12px;

            border: 1px solid rgba(255, 255, 255, 0.38);

            background: rgba(255, 255, 255, 0.04);

            color: white;

            font-size: 16px;

            cursor: pointer;

            transition: all 0.25s ease;
        }

        .btn-sso:hover {
            background: rgba(255, 255, 255, 0.12);

            border-color: rgba(255, 255, 255, 0.60);
        }

        .btn-sso i {
            margin-right: 10px;
        }


        /* =========================================
           FOOTER
        ========================================= */

        .footer {
            position: fixed;

            bottom: 16px;
            left: 4%;

            color: rgba(255, 255, 255, 0.88);

            font-size: 13px;

            z-index: 20;
        }

        .footer a {
            color: white;

            text-decoration: none;

            margin: 0 8px;
        }

        .footer a:hover {
            color: #ff9800;
        }


        /* =========================================
           COLLEGE LOGIN MODAL
        ========================================= */

        .college-modal-bg {
            background: rgba(0, 0, 0, 0.72);
        }

        .college-modal {
            width: 590px;
            max-width: 92%;

            background: rgba(255, 255, 255, 0.98);

            border-radius: 18px;

            overflow: hidden;

            box-shadow:
                0 20px 60px rgba(0, 0, 0, 0.45);

            font-family: Arial, sans-serif;
        }


        /* =========================================
           MODAL HEADER
        ========================================= */

        .college-modal-header {
            display: flex;

            align-items: center;

            padding: 20px 22px;

            background: linear-gradient(
                135deg,
                #ffffff,
                #f7f7f7
            );

            border-bottom: 1px solid #eeeeee;

            position: relative;
        }


        /* =========================================
           COLLEGE LOGO
        ========================================= */

        .college-logo {
            width: 55px;
            height: 55px;

            border-radius: 50%;

            display: flex;

            align-items: center;

            justify-content: center;

            background: white;

            box-shadow:
                0 3px 12px rgba(0, 0, 0, 0.15);

            margin-right: 14px;
        }

        .college-logo img {
            width: 45px;
            height: 45px;

            object-fit: contain;
        }


        /* =========================================
           MODAL TITLE
        ========================================= */

        .college-title-area h3 {
            margin: 0;

            font-size: 21px;

            font-weight: 700;

            color: #333;
        }

        .college-title-area span {
            display: block;

            margin-top: 4px;

            font-size: 12px;

            color: #777;
        }


        /* =========================================
           CLOSE BUTTON
        ========================================= */

        .college-close {
            position: absolute;

            right: 15px;
            top: 15px;

            width: 30px;
            height: 30px;

            border: none;

            border-radius: 50%;

            background: #eeeeee;

            color: #555;

            font-size: 22px;

            line-height: 25px;

            cursor: pointer;

            transition: 0.2s;
        }

        .college-close:hover {
            background: #990033;

            color: white;
        }


        /* =========================================
           MODAL BODY
        ========================================= */

        .college-modal-body {
            padding: 25px 25px 10px 25px;
        }

        .college-label {
            display: block;

            font-size: 13px;

            font-weight: 600;

            color: #444;

            margin-bottom: 8px;
        }


        /* =========================================
           COLLEGE DROPDOWN
        ========================================= */

        .college-select-wrapper {
            position: relative;
        }

        .college-select-wrapper i {
            position: absolute;

            left: 15px;

            top: 50%;

            transform: translateY(-50%);

            color: #990033;

            z-index: 2;
        }

        .college-select {
            width: 100%;

            max-width: 100%;

            height: 48px;

            padding: 0 40px 0 40px;

            border: 1px solid #dddddd;

            border-radius: 10px;

            background: #fafafa;

            color: #333;

            font-size: 14px;

            outline: none;

            cursor: pointer;

            box-sizing: border-box;

            overflow: hidden;

            text-overflow: ellipsis;

            white-space: nowrap;
        }

        .college-select:focus {
            border-color: #990033;

            background: white;

            box-shadow:
                0 0 0 3px rgba(153, 0, 51, 0.10);
        }


        /* =========================================
           VALIDATION
        ========================================= */

        .college-error {
            display: block;

            margin-top: 6px;

            font-size: 12px;
        }


        /* =========================================
           MODAL FOOTER
        ========================================= */

        .college-modal-footer {
            padding: 15px 25px 25px 25px;

            text-align: right;
        }


        /* =========================================
           CONTINUE BUTTON
        ========================================= */

        .college-continue-btn {
            width: 140px;

            height: 45px;

            border: none;

            border-radius: 9px;

            background: #990033;

            color: white;

            font-size: 13px;

            font-weight: 700;

            letter-spacing: 0.5px;

            cursor: pointer;

            transition: all 0.25s ease;
        }

        .college-continue-btn:hover {
            background: #750029;

            transform: translateY(-1px);

            box-shadow:
                0 6px 15px rgba(153, 0, 51, 0.25);
        }


        /* =========================================
           MOBILE
        ========================================= */

        @media (max-width: 900px) {

            html,
            body {
                overflow-y: auto;
                overflow-x: hidden;
            }

            .login-page {
                min-height: 100vh;

                justify-content: center;

                padding: 25px 15px;
            }

            .branding,
            .welcome {
                display: none;
            }

            .login-container {
                width: 450px;

                margin: 0 auto;
            }

            .footer {
                position: relative;

                left: auto;

                bottom: auto;

                text-align: center;

                padding: 12px;

                font-size: 12px;
            }
        }


        /* =========================================
           SMALL MOBILE
        ========================================= */

        @media (max-width: 500px) {

            .login-container {
                width: 94%;

                padding: 30px 22px;

                border-radius: 20px;
            }

            .login-title {
                font-size: 28px;
            }

            .login-icon {
                width: 60px;
                height: 60px;

                font-size: 25px;
            }

            .login-icon img {
                width: 250px;
                height: 90px;
            }


            /* College Modal */

            .college-modal {
                width: 94%;

                max-width: 94%;
            }

            .college-modal-header {
                padding: 17px;
            }

            .college-modal-body {
                padding: 20px 18px 8px;
            }

            .college-modal-footer {
                padding: 12px 18px 20px;
            }

            .college-title-area h3 {
                font-size: 18px;
            }

            .college-select-wrapper {
                width: 100%;
            }

            .college-select {
                width: 100%;

                font-size: 13px;
            }
        }

    </style>

</head>


<body>

    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server" />


        <!-- =====================================
             BACKGROUND IMAGE
        ====================================== -->

        <img id="bgImage"
             src="images/tmulogoDefault.jpg"
             alt="TMU Background" />


        <!-- =====================================
             DARK OVERLAY
        ====================================== -->

        <div class="video-overlay"></div>


        <!-- =====================================
             MAIN PAGE
        ====================================== -->

        <div class="login-page">


            <!-- =================================
                 LEFT BRANDING
            ================================== -->

            <%--
            <div class="branding">

                <div class="brand-title">

                    TEERTHANKER<br />

                    MAHAVEER<br />

                    UNIVERSITY

                </div>

                <div class="brand-subtitle">

                    Moradabad, Uttar Pradesh

                </div>

            </div>
            --%>


            <!-- =================================
                 WELCOME
            ================================== -->

            <div class="welcome">

                <h1>
                    WELCOME TO TMU
                </h1>

                <p>
                    Inspiring Excellence, Transforming Lives
                </p>

                <div class="orange-line"></div>

            </div>


            <!-- =================================
                 LOGIN CARD
            ================================== -->

            <div class="login-container">


                <!-- TMU LOGO -->

                <div class="login-icon">

                    <img src="images/logoLatest.png"
                         alt="TMU Logo" />

                </div>


                <!-- TITLE -->

                <h2 class="login-title">
                    Sign in to your TMU account
                </h2>


                <div class="login-line"></div>


                <!-- =================================
                     USERNAME
                ================================== -->

                <label class="input-label">
                    Username
                </label>

                <div class="input-wrapper">

                    <i class="fa-regular fa-user left-icon"></i>

                    <asp:TextBox
                        ID="txtUserid"
                        runat="server"
                        CssClass="login-input"
                        placeholder="Enter your username">
                    </asp:TextBox>

                </div>


                <!-- =================================
                     PASSWORD
                ================================== -->

                <label class="input-label">
                    Password
                </label>

                <div class="input-wrapper">

                    <i class="fa-solid fa-lock left-icon"></i>

                    <asp:TextBox
                        ID="txtpassword"
                        runat="server"
                        TextMode="Password"
                        CssClass="login-input"
                        placeholder="Enter your password">
                    </asp:TextBox>


                    <i class="fa-regular fa-eye password-eye"
                       id="eyeIcon"
                       onclick="togglePassword()">
                    </i>

                </div>


                <!-- =================================
                     OPTIONS
                ================================== -->

                <div class="login-options">

                    <label class="remember">

                        <asp:CheckBox
                            ID="chkrem"
                            runat="server" />

                        <span>

                            <asp:Label
                                ID="Label2"
                                runat="server"
                                Text="Remember Me"
                                Font-Size="12pt"
                                ForeColor="White"
                                Font-Names="Georgia,Times New Roman,Helvetica Neue">
                            </asp:Label>

                        </span>

                    </label>


                    <a href="#"
                       class="forgot">
                        Forgot Password?
                    </a>

                </div>


                <!-- =================================
                     LOGIN BUTTON
                ================================== -->

                <asp:Button
                    ID="btnLogin"
                    runat="server"
                    Text="LOGIN"
                    CssClass="btn-login"
                    OnClick="ImgBttn_Login_Click1" />


            </div>

        </div>


        <!-- =====================================
             FOOTER
        ====================================== -->

        <div class="footer">

            © 2026 Teerthanker Mahaveer University.
            All Rights Reserved.

            <a href="#">
                Privacy Policy
            </a>

            |

            <a href="#">
                Terms &amp; Conditions
            </a>

        </div>


        <!-- =====================================
             COLLEGE LOGIN MODAL
        ====================================== -->

        <div id="link" runat="server">


            <asp:Button
                ID="btnLog"
                runat="server"
                Text=""
                Style="display: none;" />


            <asp:ModalPopupExtender
                ID="mpeLog"
                runat="server"
                TargetControlID="btnLog"
                PopupControlID="pnlPopLog"
                CancelControlID="btnClosee"
                BackgroundCssClass="college-modal-bg"
                DropShadow="true">
            </asp:ModalPopupExtender>


            <asp:Panel
                ID="pnlPopLog"
                runat="server"
                CssClass="college-modal"
                Style="display: none;">


                <!-- =================================
                     MODAL HEADER
                ================================== -->

                <div class="college-modal-header">


                    <div class="college-logo">

                        <img src="images/tmulogo.png"
                             alt="TMU Logo" />

                    </div>


                    <div class="college-title-area">

                        <h3>
                            Login With College
                        </h3>

                        <span>
                            Select your college to continue
                        </span>

                    </div>


                    <asp:Button
                        ID="btnClosee"
                        runat="server"
                        Text="×"
                        CssClass="college-close"
                        OnClientClick="return fun();" />

                </div>


                <!-- =================================
                     MODAL BODY
                ================================== -->

                <div class="college-modal-body">


                    <label class="college-label">
                        Select College
                    </label>


                    <div class="college-select-wrapper">


                        <i class="fa-solid fa-building-columns"></i>


                        <asp:DropDownList
                            ID="ddlCollege"
                            runat="server"
                            CssClass="college-select">
                        </asp:DropDownList>


                    </div>


                    <asp:RequiredFieldValidator
                        ID="rfvddlCollege"
                        runat="server"
                        ControlToValidate="ddlCollege"
                        InitialValue="0"
                        ErrorMessage="Please select a college"
                        ForeColor="Red"
                        ValidationGroup="vgCollege"
                        CssClass="college-error">
                    </asp:RequiredFieldValidator>

                </div>


                <!-- =================================
                     MODAL FOOTER
                ================================== -->

                <div class="college-modal-footer">


                    <asp:Button
                        ID="btnCollege"
                        runat="server"
                        Text="CONTINUE"
                        CssClass="college-continue-btn"
                        ValidationGroup="vgCollege"
                        OnClick="btnCollege_Click" />


                </div>


            </asp:Panel>

        </div>

    </form>


    <!-- =========================================
         PASSWORD SHOW / HIDE
    ========================================= -->

    <script type="text/javascript">

        function togglePassword() {

            var password =
                document.getElementById(
                    '<%= txtpassword.ClientID %>'
                );

            var eye =
                document.getElementById("eyeIcon");


            if (password.type === "password") {

                password.type = "text";

                eye.classList.remove("fa-eye");

                eye.classList.add("fa-eye-slash");

            }
            else {

                password.type = "password";

                eye.classList.remove("fa-eye-slash");

                eye.classList.add("fa-eye");

            }

        }

    </script>

</body>

</html>