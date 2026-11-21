<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="StudentDetailsView1.aspx.cs" Inherits="Student_StudentDetailsView1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
    <!-- Font Awesome -->
    <link href="css/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />

    <!-- Theme style -->
    <link rel="stylesheet" href="dist/css/AdminLTE.min.css">
    <link rel="stylesheet" href="dist/css/skins/_all-skins.min.css">
    <link href="css/CloudeStyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="dist/css/style.css" rel="stylesheet" />
    <script src="js/CloudeJScript.js" type="text/javascript"></script>
    <link href="css/gridstyle.css" rel="stylesheet" />

    <style type="text/css">
        /* ===== Profile page (Faculty portal style) ===== */
        .pf-title {
            display: flex;
            align-items: center;
            justify-content: space-between;
            padding: 6px 4px 12px;
            margin-bottom: 14px;
            border-bottom: 1px solid #ece4d6;
        }
        .pf-title h2 {
            margin: 0;
            font-family: Georgia, "Times New Roman", serif;
            font-size: 22px;
            color: #093A62;
        }
        .pf-abc {
            display: inline-block;
            padding: 8px 18px;
            font-size: 13px;
            font-weight: bold;
            color: #fff !important;
            background: #3fb5df;
            border-radius: 4px;
            text-decoration: none;
        }
        .pf-abc:hover { background: #2a9dc8; text-decoration: none; }

        .pf-card {
            margin-bottom: 18px;
            border: 1px solid #e6e0d3;
            border-radius: 6px;
            background: #fff;
            overflow: hidden;
        }
        .pf-card-head {
            padding: 9px 16px;
            background: linear-gradient(90deg, #f1f1ec, #e6e9ea);
            border-bottom: 1px solid #ddd;
            border-left: 4px solid #e57a00;
            font-family: Georgia, "Times New Roman", serif;
            font-size: 16px;
            color: #093A62;
        }
        .pf-card-body { padding: 16px 18px 6px; }

        .pf-grid {
            display: grid;
            grid-template-columns: repeat(3, 1fr);
            gap: 14px 22px;
        }
        .pf-field label {
            display: block;
            margin: 0 0 4px;
            font-family: Georgia, "Times New Roman", serif;
            font-size: 12.5px;
            font-weight: normal;
            color: #666;
        }
        .pf-field input[type=text],
        .pf-field textarea,
        .pf-field input[type=password] {
            width: 100% !important;
            box-sizing: border-box;
            height: 36px;
            padding: 6px 10px;
            font-size: 13.5px;
            color: #333;
            background: #fff;
            border: 1px solid #cfd6dc;
            border-radius: 5px;
            outline: none;
            transition: border-color .15s, box-shadow .15s;
        }
        .pf-field textarea { height: 80px; resize: vertical; }
        .pf-field input[type=text]:focus,
        .pf-field textarea:focus {
            border-color: #f08a00;
            box-shadow: 0 0 0 3px rgba(240,138,0,.18);
        }
        .pf-field input[disabled],
        .pf-field input[readonly] {
            background: #f5f6f4;
            color: #555;
        }
        .pf-full { grid-column: 1 / -1; }
        .pf-note { font-size: 12px; color: #c0392b; }

        .pf-actions { text-align: right; padding: 4px 18px 16px; }
        .pf-btn {
            min-width: 110px;
            height: 36px;
            padding: 0 20px;
            font-size: 13px;
            font-weight: bold;
            letter-spacing: .5px;
            color: #fff;
            background: linear-gradient(180deg, #f79a1a, #e57a00);
            border: 0;
            border-radius: 5px;
            box-shadow: 0 2px 4px rgba(0,0,0,.2);
            cursor: pointer;
        }
        .pf-btn:hover { background: linear-gradient(180deg, #e57a00, #c96800); }

        /* ===== Hero banner ===== */
        @keyframes pfFadeUp { from { opacity: 0; transform: translateY(14px); } to { opacity: 1; transform: none; } }
        .pf-hero, .pf-tiles, .pf-card { animation: pfFadeUp .5s ease both; }
        .pf-tiles { animation-delay: .1s; }
        .pf-hero {
            position: relative;
            display: flex;
            align-items: center;
            gap: 22px;
            margin-bottom: 18px;
            padding: 22px 26px;
            color: #fff;
            border-radius: 12px;
            overflow: hidden;
            background: linear-gradient(120deg, #7a1212 0%, #c2410c 48%, #f79a1a 100%);
            box-shadow: 0 8px 22px rgba(122,18,18,.28);
        }
        .pf-hero::before, .pf-hero::after {
            content: "";
            position: absolute;
            border-radius: 50%;
            background: rgba(255,255,255,.09);
        }
        .pf-hero::before { width: 260px; height: 260px; right: -70px; top: -110px; }
        .pf-hero::after { width: 170px; height: 170px; right: 150px; bottom: -100px; }
        .pf-hero-photo {
            flex: 0 0 auto;
            width: 104px;
            height: 104px;
            border-radius: 50%;
            padding: 4px;
            background: linear-gradient(135deg, #fff, #ffd9a0);
            box-shadow: 0 6px 18px rgba(0,0,0,.35);
            position: relative;
            z-index: 1;
        }
        .pf-hero-photo img { width: 100%; height: 100%; border-radius: 50%; object-fit: cover; background: #fff; display: block; }
        .pf-hero-info { position: relative; z-index: 1; min-width: 0; }
        .pf-hero-hello { font-size: 13px; letter-spacing: 1px; text-transform: uppercase; opacity: .85; font-family: "Trebuchet MS", Verdana, sans-serif; }
        .pf-hero-info h1 { margin: 2px 0 10px; font-family: Georgia, "Times New Roman", serif; font-size: 30px; font-weight: bold; color: #fff; text-transform: uppercase; }
        .pf-chips { display: flex; flex-wrap: wrap; gap: 8px; }
        .pf-chip {
            display: inline-flex;
            align-items: center;
            gap: 7px;
            padding: 5px 13px;
            font-size: 12.5px;
            font-weight: bold;
            font-family: "Trebuchet MS", Verdana, sans-serif;
            background: rgba(255,255,255,.18);
            border: 1px solid rgba(255,255,255,.35);
            border-radius: 20px;
            backdrop-filter: blur(2px);
        }
        .pf-chip:empty, .pf-chip.pf-hide { display: none; }

        /* ===== Quick tiles ===== */
        .pf-tiles { display: grid; grid-template-columns: repeat(8, 1fr); gap: 12px; margin-bottom: 20px; }
        .pf-tile {
            display: flex;
            flex-direction: column;
            align-items: center;
            gap: 8px;
            padding: 14px 6px 12px;
            text-align: center;
            text-decoration: none !important;
            color: #444 !important;
            font-family: "Trebuchet MS", Verdana, sans-serif;
            font-size: 12.5px;
            font-weight: bold;
            background: #fff;
            border: 1px solid #eee3d0;
            border-radius: 12px;
            box-shadow: 0 2px 6px rgba(0,0,0,.06);
            transition: transform .18s, box-shadow .18s, border-color .18s;
        }
        .pf-tile i {
            display: flex;
            align-items: center;
            justify-content: center;
            width: 44px;
            height: 44px;
            font-size: 20px;
            color: #fff;
            border-radius: 12px;
            box-shadow: 0 4px 10px rgba(0,0,0,.18);
        }
        .pf-tile:hover { transform: translateY(-4px); border-color: #f79a1a; box-shadow: 0 10px 20px rgba(229,122,0,.25); }
        .pf-t1 i { background: linear-gradient(135deg, #34d399, #059669); }
        .pf-t2 i { background: linear-gradient(135deg, #fbbf24, #d97706); }
        .pf-t3 i { background: linear-gradient(135deg, #60a5fa, #2563eb); }
        .pf-t4 i { background: linear-gradient(135deg, #f472b6, #db2777); }
        .pf-t5 i { background: linear-gradient(135deg, #a78bfa, #7c3aed); }
        .pf-t6 i { background: linear-gradient(135deg, #fb923c, #ea580c); }
        .pf-t7 i { background: linear-gradient(135deg, #2dd4bf, #0d9488); }
        .pf-t8 i { background: linear-gradient(135deg, #f87171, #dc2626); }

        /* ===== nicer cards / fields ===== */
        .pf-card { border-radius: 12px; box-shadow: 0 3px 10px rgba(0,0,0,.06); }
        .pf-card-head { display: flex; align-items: center; gap: 10px; padding: 11px 18px; font-weight: bold; background: linear-gradient(90deg, #fff4e2, #fffaf2); }
        .pf-card-head i { display: inline-flex; align-items: center; justify-content: center; width: 28px; height: 28px; color: #fff; font-size: 14px; background: linear-gradient(135deg, #f79a1a, #e57a00); border-radius: 8px; }
        .pf-field { position: relative; }
        .pf-field label { font-family: "Trebuchet MS", Verdana, sans-serif; font-size: 11.5px; font-weight: bold; letter-spacing: .4px; text-transform: uppercase; color: #8a6d3b; }
        .pf-field input[disabled], .pf-field input[readonly] { background: #faf6ee; border-color: #eadfc9; color: #3b3b3b; font-weight: bold; }
        .pf-field input[type=text]:not([disabled]):not([readonly]) { background: #fff; border-color: #f0c987; }
        .pf-btn { border-radius: 24px; padding: 0 28px; }
        .pf-abc { border-radius: 20px; background: linear-gradient(135deg, #38bdf8, #0284c7); box-shadow: 0 4px 10px rgba(2,132,199,.35); }

        @media (max-width: 1100px) { .pf-tiles { grid-template-columns: repeat(4, 1fr); } }
        @media (max-width: 600px) {
            .pf-hero { flex-direction: column; text-align: center; }
            .pf-chips { justify-content: center; }
            .pf-hero-info h1 { font-size: 22px; }
            .pf-tiles { grid-template-columns: repeat(2, 1fr); }
        }

        /* ===== Modals ===== */
        .pf-modal .modal-dialog { margin: 60px auto; }
        .pf-modal .modal-content { border: 0; border-radius: 10px; overflow: hidden; box-shadow: 0 12px 40px rgba(0,0,0,.35); }
        .pf-modal .modal-header {
            padding: 12px 18px;
            background: linear-gradient(90deg, #f79a1a, #e57a00);
            border-bottom: 0;
            color: #fff;
        }
        .pf-modal .modal-header .close { color: #fff; opacity: .9; text-shadow: none; }
        .pf-modal .modal-title { font-size: 17px; font-weight: bold; color: #fff; }
        .pf-modal .modal-body { padding: 18px 22px; }

        .pf-aadhaar .modal-dialog { width: 640px; max-width: 94%; }
        .pf-aadhaar .pf-grid { grid-template-columns: 1fr 1fr; }
        .pf-aadhaar .pf-warn {
            margin: 14px 0 4px;
            padding: 8px 12px;
            font-size: 13px;
            font-weight: bold;
            color: #b30000;
            background: #fdecea;
            border-left: 4px solid #d9534f;
            border-radius: 4px;
        }

        .pf-error-modal .modal-dialog { width: 700px; max-width: 94%; }
        .pf-error-modal .modal-header { background: linear-gradient(90deg, #ff9933, #ffcc80); }
        .pf-error-modal .modal-title { color: #5a2d0c; }
        .pf-error-modal .modal-body { max-height: 400px; overflow: auto; }
        .error-item { border-bottom: 1px solid #eee; padding: 8px 0; }
        .error-item ul { margin: 0; padding-left: 18px; }
        .error-text { color: #d9534f; font-size: 15px; line-height: 1.5; }

        /* ===== Stay Connected popup (new design) ===== */
        .pf-social .modal-dialog { width: 640px; max-width: 94%; }
        .pf-social .modal-content { position: relative; border-radius: 18px; box-shadow: 0 20px 50px rgba(0,0,0,.45); }
        .pf-social .sc-close {
            position: absolute;
            top: 12px;
            right: 14px;
            z-index: 3;
            width: 32px;
            height: 32px;
            padding: 0;
            font-size: 22px;
            line-height: 30px;
            text-align: center;
            color: #fff;
            background: rgba(255,255,255,.22);
            border: 1px solid rgba(255,255,255,.55);
            border-radius: 50%;
            cursor: pointer;
            opacity: 1;
            text-shadow: none;
            transition: background .15s, transform .15s;
        }
        .pf-social .sc-close:hover { background: rgba(255,255,255,.4); transform: rotate(90deg); }
        .pf-social .sc-head {
            position: relative;
            padding: 24px 62px 22px 28px;
            color: #fff;
            text-align: left;
            overflow: hidden;
            background: linear-gradient(120deg, #7a1212 0%, #c2410c 48%, #f79a1a 100%);
        }
        .pf-social .sc-head::before { content: ""; position: absolute; width: 200px; height: 200px; right: -60px; top: -90px; border-radius: 50%; background: rgba(255,255,255,.1); }
        .pf-social .sc-head::after { content: ""; position: absolute; width: 120px; height: 120px; left: 180px; bottom: -80px; border-radius: 50%; background: rgba(255,255,255,.08); }
        .pf-social .sc-title { position: relative; z-index: 1; display: flex; align-items: center; gap: 14px; margin-bottom: 14px; }
        .pf-social .sc-ico {
            flex: 0 0 auto;
            display: inline-flex; align-items: center; justify-content: center;
            width: 46px; height: 46px; font-size: 20px; color: #c2410c;
            background: linear-gradient(135deg, #fff, #ffd9a0);
            border-radius: 50%;
            box-shadow: 0 4px 12px rgba(0,0,0,.3);
        }
        .pf-social .sc-head h2 {
            margin: 0;
            font-family: Georgia, "Times New Roman", serif;
            font-size: 30px;
            font-weight: bold;
            line-height: 1.1;
            color: #fff;
            text-shadow: 0 2px 6px rgba(0,0,0,.25);
        }
        .pf-social .sc-head h2::after {
            content: "";
            display: block;
            width: 56px;
            height: 3px;
            margin-top: 7px;
            border-radius: 3px;
            background: linear-gradient(90deg, #ffd9a0, rgba(255,217,160,0));
        }
        .pf-social .sc-head p {
            position: relative;
            z-index: 1;
            margin: 0;
            padding: 11px 16px;
            font-family: "Trebuchet MS", Verdana, sans-serif;
            font-size: 14px;
            line-height: 1.65;
            color: #fff;
            background: rgba(255,255,255,.15);
            border-left: 3px solid #ffd9a0;
            border-radius: 0 10px 10px 0;
        }
        .pf-social .sc-head p b { color: #ffe2a8; font-weight: bold; }
        .pf-social .modal-body.sc-body { padding: 0; background: #fffdf8; }
        .pf-social .pf-social-grid {
            display: grid;
            grid-template-columns: repeat(3, 1fr);
            gap: 16px;
            padding: 22px 24px 26px;
            text-align: center;
        }
        .pf-social .pf-social-grid a {
            display: flex;
            align-items: center;
            justify-content: center;
            height: 190px;
            padding: 8px;
            background: #fff;
            border: 1px solid #f3dcb0;
            border-radius: 14px;
            box-shadow: 0 3px 8px rgba(229,122,0,.10);
            transition: transform .18s, box-shadow .18s, border-color .18s;
        }
        .pf-social .pf-social-grid a:hover { transform: translateY(-4px); border-color: #f0a830; box-shadow: 0 10px 20px rgba(229,122,0,.25); }
        .pf-social .pf-social-img { width: 100% !important; height: 100% !important; object-fit: contain; display: block; }

        .pf-notify table { width: 100%; }
        .pf-notify th, .pf-notify td { padding: 10px; text-align: center; border-bottom: 1px solid #eee; }
        .pf-notify tr.pf-head td { background: #fde9cf; font-weight: bold; color: #5a2d0c; }

        @media (max-width: 900px) {
            .pf-grid { grid-template-columns: 1fr 1fr; }
        }
        @media (max-width: 600px) {
            .pf-grid, .pf-aadhaar .pf-grid { grid-template-columns: 1fr; }
            .pf-title { flex-direction: column; align-items: flex-start; gap: 8px; }
            .pf-social .pf-social-grid { grid-template-columns: 1fr 1fr; }
            .pf-social .sc-head h2 { font-size: 24px; }
        }
    </style>

    <script type="text/javascript" src="https://www.google.com/jsapi">
    </script>
    <script src="https://www.google.com/jsapi" type="text/javascript">  
    </script>
    <script type="text/javascript">
        google.load("elements", "1", {
            packages: "transliteration"
        });

        function onLoad() {
            var options = {
                sourceLanguage: google.elements.transliteration.LanguageCode.ENGLISH,
                destinationLanguage: [google.elements.transliteration.LanguageCode.HINDI],
                shortcutKey: 'ctrl+g',
                transliterationEnabled: true
            };

            var control = new google.elements.transliteration.TransliterationControl(options);
            control.makeTransliteratable(['TxtSH']);
            control.makeTransliteratable(['TxtFH']);
            control.makeTransliteratable(['TxtMH']);

        }
        google.setOnLoadCallback(onLoad);
    </script>

    <script type="text/javascript">
        // Hero banner: photo aur details master/fields se
        function pfFillHero() {
            try {
                var v = function (id) { return $.trim($('[id$=' + id + ']').val() || ''); };
                var name = v('txtName');
                if (name) $('#pfHeroName').text(name);
                var src = $('[id$=imgProfile]').attr('src');
                if (src) $('#pfHeroImg').attr('src', src);
                var set = function (chip, text, prefix) {
                    var $c = $('#' + chip);
                    if (text) { $c.find('span').text((prefix || '') + text); $c.removeClass('pf-hide'); }
                    else { $c.addClass('pf-hide'); }
                };
                set('pfChipEnr', v('txtRollNo'), 'Enrol: ');
                set('pfChipCourse', v('txtCourse'));
                set('pfChipSem', v('txtSemester'), 'Sem: ');
                set('pfChipYear', v('txtAcademicYear'), 'Session: ');
            } catch (e) { }
        }
        $(document).ready(function () {
            pfFillHero();
            if (window.Sys && Sys.WebForms && Sys.WebForms.PageRequestManager) {
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(pfFillHero);
            }
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('[id$=txtAddress]').keypress(function (e) {
                if ($(this).val().length >= 50) {
                    e.preventDefault();
                }
            });
        });
    </script>
    <script type="text/javascript">
        function callFeedbackMessage(inputType, inputText) {

            if (inputType == 'Error') {
                alertify.error(inputText);
                return false;
            }
            else if (inputType == 'Success') {
                alertify.success("Update Successfully");
                return false;
            }
            else {
                alertify.log(inputText, "", 10000);
                return false;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <!-- ========== Aadhaar details popup ========== -->
    <asp:Panel ID="Panel2" runat="server">
        <div id="confirmModal5" class="modal fade confirm-modal pf-modal pf-aadhaar" role="dialog">
            <div class="modal-dialog modalPopup">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title"><i class="fa fa-id-card-o"></i>&nbsp; Verify Aadhaar Details</h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="pf-grid">
                                    <div class="pf-field pf-full">
                                        <label>Student No_</label>
                                        <asp:TextBox ID="txtstno" Enabled="false" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="pf-field">
                                        <label>Student’s Name (As per 10th certificate)</label>
                                        <asp:TextBox ID="txtSTName10" Enabled="false" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="pf-field">
                                        <label>Student’s Name (As per Aadhaar)</label>
                                        <asp:TextBox ID="txtSTNameA" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="**" ValidationGroup="g1" ForeColor="Red" ControlToValidate="txtSTNameA"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="pf-field">
                                        <label>Date of birth (As per 10th certificate)</label>
                                        <asp:TextBox ID="txtDOB10" Enabled="false" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="pf-field">
                                        <label>Date of birth (As per Aadhaar)</label>
                                        <asp:TextBox ID="txtDOBA" runat="server" onkeypress="return false" onKeyDown="preventBackspace();"
                                            oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="**" ValidationGroup="g1" ForeColor="Red" ControlToValidate="txtDOBA"></asp:RequiredFieldValidator>
                                        <asp:CalendarExtender ID="CalendarExtender2" Format="dd MMM yyyy" runat="server"
                                            CssClass="cal_Theme1" Enabled="true" TargetControlID="txtDOBA">
                                        </asp:CalendarExtender>
                                    </div>
                                    <div class="pf-field">
                                        <label>Mobile number (linked with Aadhaar)</label>
                                        <asp:TextBox ID="txtMobileA" onkeypress="return isNumber(event)" MaxLength="10" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="**" ValidationGroup="g1" ForeColor="Red" ControlToValidate="txtMobileA"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="pf-field">
                                        <label>Aadhar number</label>
                                        <asp:TextBox ID="txtAdhar" onkeypress="return isNumber(event)" MaxLength="12" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="**" ValidationGroup="g1" ForeColor="Red" ControlToValidate="txtAdhar"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rgxAadhaar" runat="server" ControlToValidate="txtAdhar"
                                            ValidationExpression="([0-9]{4}[0-9]{4}[0-9]{4}$)|([0-9]{4}\s[0-9]{4}\s[0-9]{4}$)|([0-9]{4}-[0-9]{4}-[0-9]{4}$)" ValidationGroup="g1"
                                            ErrorMessage="Invalid" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="pf-warn"><i class="fa fa-exclamation-circle"></i>&nbsp; The blank field must be filled in to proceed further.</div>
                                <div class="pf-actions" style="padding-right: 0; padding-bottom: 0">
                                    <asp:Button ID="btnSave" CssClass="pf-btn" Text="SAVE" ValidationGroup="g1" OnClick="btnSave_Click" runat="server"></asp:Button>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <!-- ========== Exam form messages popup ========== -->
    <asp:Panel ID="Panel3" runat="server">
        <div id="confirmModal10" class="modal fade confirm-modal pf-modal pf-error-modal" role="dialog">
            <div class="modal-dialog modalPopup">
                <div class="modal-content">
                    <div class="modal-header custom-header">
                        <h5 class="modal-title"><i class="fa fa-bell"></i>&nbsp; Exam Form Message Details</h5>
                    </div>
                    <div class="modal-body custom-body">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:Repeater ID="rptErrors" runat="server">
                                    <ItemTemplate>
                                        <div class="error-item">
                                            <ul>
                                                <li class="error-text">
                                                    <b>Error:</b> <%# Eval("ErrorMessage") %>
                                                </li>
                                            </ul>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>

                                <div class="text-right" style="margin-top: 14px">
                                    <asp:Button
                                        ID="btnMarkAllRead"
                                        runat="server"
                                        Text="Mark as Read"
                                        CssClass="btn btn-success btn-lg"
                                        OnClick="btnMarkAllRead_Click" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <!-- ========== Social media popup (Stay Connected) ========== -->
    <asp:Panel ID="Panel1" runat="server">
        <div id="confirmModal1" class="modal fade confirm-modal pf-modal pf-social" role="dialog">
            <div class="modal-dialog modalPopup">
                <div class="modal-content">
                    <button type="button" class="close sc-close" data-dismiss="modal" title="Close">&times;</button>

                    <div class="sc-head">
                        <div class="sc-title">
                            <span class="sc-ico"><i class="fa fa-share-alt"></i></span>
                            <h2>Stay Connected!</h2>
                        </div>
                        <p>Follow TMU on all our social channels to get the <b>latest updates</b>, <b>announcements</b>, and <b>exciting content</b>!</p>
                    </div>

                    <div class="modal-body sc-body">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="pf-social-grid">
                                    <a href="https://www.facebook.com/tmumbd/" target="_blank">
                                        <asp:Image ID="Image5" runat="server" ImageUrl="~/images/facebook.png" CssClass="pf-social-img" />
                                    </a>
                                    <a href="https://bit.ly/4ddvTyK" target="_blank">
                                        <asp:Image ID="Image7" runat="server" ImageUrl="~/images/insta_logo1.png" CssClass="pf-social-img" />
                                    </a>
                                    <a href="https://bit.ly/4b5DdL9" target="_blank">
                                        <asp:Image ID="Image11" runat="server" ImageUrl="~/images/whatsapp.png" CssClass="pf-social-img" />
                                    </a>
                                    <a href="https://bit.ly/4d3FOaa" target="_blank">
                                        <asp:Image ID="Image8" runat="server" ImageUrl="~/images/yt_logo 1.png" CssClass="pf-social-img" />
                                    </a>
                                    <a href="https://bit.ly/3WdTNEl" target="_blank">
                                        <asp:Image ID="lnkYout" runat="server" ImageUrl="~/images/insta_logo2.png" CssClass="pf-social-img" />
                                    </a>
                                    <a href="https://bit.ly/3UfA8kM" target="_blank">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/Linkdn.png" CssClass="pf-social-img" />
                                    </a>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <!-- ========== Notification popup ========== -->
    <asp:Panel ID="pnlPopup" runat="server">
        <div id="confirmModal" class="modal fade confirm-modal pf-modal pf-notify" role="dialog">
            <div class="modal-dialog modalPopup">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title"><i class="fa fa-bell"></i>&nbsp; Notification</h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="updatepnl" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr class="pf-head">
                                        <td style="width: 33%">
                                            <asp:Label ID="Label2" runat="server" Text="S.No"></asp:Label>
                                        </td>
                                        <td style="width: 33%">
                                            <asp:Label ID="Label5" runat="server" Text="Inbox"></asp:Label>
                                        </td>
                                        <td style="width: 33%">
                                            <asp:Label ID="Label6" runat="server" Text="Count"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Sno" runat="server" Text="1"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LblInbox" runat="server" Text="Placement"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="Lblcount" OnClick="Lblcount_Click" runat="server"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <!-- ========== Page content ========== -->
    <div class="pf-title">
        <h2>
            <asp:Label ID="Label1" runat="server" Text="Profile"></asp:Label></h2>
        <a href="https://digilocker.meripehchaan.gov.in/" id="A2" runat="server" target="_blank" visible="true" class="pf-abc"><i class="fa fa-id-badge"></i>&nbsp; Click here to generate ABC ID</a>
    </div>

    <!-- Personal Information -->
    <div class="pf-card">
        <div class="pf-card-head">
            <i class="fa fa-user"></i>
            <asp:Label ID="Label3" runat="server" Text="Personal Information"></asp:Label>
        </div>
        <div class="pf-card-body">
            <div class="pf-grid">
                <div class="pf-field">
                    <label>Enrolment No:</label>
                    <asp:TextBox ID="txtRollNo" runat="server" Enabled="false"></asp:TextBox>
                </div>
                <div class="pf-field">
                    <label>ABC ID</label>
                    <asp:TextBox ID="txtNADID" runat="server" MaxLength="12"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtNADID" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                </div>
                <div class="pf-field">
                    <label>Mentor:</label>
                    <asp:TextBox ID="txtMentor" runat="server" Font-Underline="true" Enabled="false"></asp:TextBox>
                </div>

                <div class="pf-field">
                    <label>Student No.</label>
                    <asp:TextBox ID="txtStudentNo" runat="server" Enabled="false"></asp:TextBox>
                </div>
                <div class="pf-field">
                    <label>Name</label>
                    <asp:TextBox ID="txtName" runat="server" Enabled="False"></asp:TextBox>
                </div>
                <div class="pf-field">
                    <label>Date of Birth</label>
                    <asp:TextBox ID="txtDOB" runat="server" Enabled="False"></asp:TextBox>
                </div>

                <div class="pf-field">
                    <label>Course</label>
                    <asp:TextBox ID="txtCourse" runat="server" Enabled="False"></asp:TextBox>
                </div>
                <div class="pf-field">
                    <label>Section</label>
                    <asp:TextBox ID="txtSection" runat="server" Enabled="False"></asp:TextBox>
                </div>
                <div class="pf-field">
                    <label>Academic Year</label>
                    <asp:TextBox ID="txtAcademicYear" runat="server" Enabled="false"></asp:TextBox>
                </div>

                <div class="pf-field">
                    <label>Category</label>
                    <asp:TextBox ID="txtCategory" runat="server" Enabled="False"></asp:TextBox>
                </div>
                <div class="pf-field">
                    <label>Father Name</label>
                    <asp:TextBox ID="txtFatherName" runat="server" Enabled="False"></asp:TextBox>
                </div>
                <div class="pf-field">
                    <label>Mother Name</label>
                    <asp:TextBox ID="txtMotherName" runat="server" Enabled="False"></asp:TextBox>
                </div>

                <div class="pf-field">
                    <label>Admitted Year</label>
                    <asp:TextBox ID="txtCurrentYear" runat="server" Enabled="False"></asp:TextBox>
                </div>
                <div class="pf-field">
                    <label>Semester/Year</label>
                    <asp:TextBox ID="txtSemester" runat="server" Enabled="False"></asp:TextBox>
                </div>
                <div class="pf-field">
                    <label>Batch</label>
                    <asp:TextBox ID="txtBatch" runat="server" Enabled="False"></asp:TextBox>
                </div>

                <div class="pf-field">
                    <label>छात्र/छात्रा का नाम</label>
                    <div id="DSH" runat="server" visible="false">
                        <div class="Google-transliterate-Way2blogging">
                            <input type="text" name="SHname" id="TxtSH" placeholder="Use Google Translate" maxlength="35" />
                            <asp:Label ID="hmsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <asp:TextBox ID="txtStudentHindi" runat="server" Enabled="False" MaxLength="35"></asp:TextBox>
                </div>
                <div class="pf-field">
                    <label>पिता का नाम</label>
                    <div id="DFH" runat="server" visible="false">
                        <div class="Google-transliterate-Way2blogging">
                            <input type="text" name="FHname" id="TxtFH" placeholder="Use Google Translate" maxlength="35" />
                        </div>
                    </div>
                    <asp:TextBox ID="txtFatherHindi" runat="server" Enabled="False" MaxLength="35"></asp:TextBox>
                </div>
                <div class="pf-field">
                    <label>माता का नाम</label>
                    <div id="DMH" runat="server" visible="false">
                        <div class="Google-transliterate-Way2blogging">
                            <input type="text" name="MHname" id="TxtMH" placeholder="Use Google Translate" maxlength="35" />
                        </div>
                    </div>
                    <asp:TextBox ID="txtMotherHindi" runat="server" Enabled="False" MaxLength="35"></asp:TextBox>
                </div>
            </div>
            <div style="height: 12px"></div>
        </div>
    </div>

    <!-- Contact Information -->
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="pf-card">
                <div class="pf-card-head">
                    <i class="fa fa-phone"></i>
                    <asp:Label ID="Label4" runat="server" Text="Contact Information"></asp:Label>
                </div>
                <div class="pf-card-body">
                    <div class="pf-grid">
                        <div class="pf-field">
                            <label>E-Mail ID</label>
                            <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="regEmail" Display="Dynamic" ControlToValidate="txtEmailID" Text="Please fill the valid Email address!" ForeColor="Red" ValidationGroup="g1"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server" />
                        </div>
                        <div class="pf-field">
                            <label>Mobile No</label>
                            <asp:TextBox ID="txtMobileNo" runat="server" MaxLength="13"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtMobileNo" FilterType="Numbers, Custom"></asp:FilteredTextBoxExtender>
                        </div>
                        <div class="pf-field">
                            <label>City</label>
                            <asp:TextBox ID="txtCity" runat="server" MaxLength="20"></asp:TextBox>
                        </div>
                        <div class="pf-field pf-full">
                            <label>Address</label>
                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div style="height: 12px"></div>
                </div>
                <div class="pf-actions">
                    <asp:Button ID="btnUpdate" runat="server" CssClass="pf-btn" OnClick="btnUpdate_Click" Text="UPDATE" />
                </div>
            </div>

            <!-- Confirm update popup -->
            <div id="confirmModalB" class="modal fade confirm-modal pf-modal" role="dialog">
                <div class="modal-dialog modalPopup border-box" style="max-width: 94%">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" name="btn_close" id="Button1" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title"><i class="fa fa-question-circle"></i>&nbsp; Confirm</h4>
                        </div>
                        <div class="modal-body">
                            <asp:Panel ID="PnlMain" runat="server">
                                <asp:Label ID="Lblpopup" runat="server" Text="Dear student kindly check your detail. If correct then update."></asp:Label>
                                <div class="modal-footer" style="padding: 14px 0 0; border-top: 0">
                                    <asp:Button ID="BtnYes" runat="server" OnClick="BtnYes_Click" Text="Update"
                                        UseSubmitBehavior="false" data-dismiss="modal" class="btn btn-success" />
                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Cancel</button>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
