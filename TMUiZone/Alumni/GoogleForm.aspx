<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/IndexMaster.master" AutoEventWireup="true" CodeFile="GoogleForm.aspx.cs" Inherits="Alumni_GoogleForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">



    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="assets/vendor/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/vendor/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="assets/vendor/css/ie10-viewport-bug-workaround.css" rel="stylesheet" />
    <link href="assets/vendor/css/common.css" rel="stylesheet" />
    <link href="assets/vendor/css/style.css" rel="stylesheet" />
    <link href="assets/vendor/css/header-style.css" rel="stylesheet" />
    <link href="assets/vendor/css/responsive.css" rel="stylesheet" />
    <link href="assets/vendor/css/font-awesome.min.css" rel="stylesheet" />
    <script src="assets/vendor/js/ie-emulation-modes-warning.js"></script>
    <link href="assets/vendor/css/ch-pie-line.css" rel="stylesheet" />
    <link href="assets/vendor/css/my.css" rel="stylesheet" />
    <link href="assets/vendor/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="assets/vendor/js/jsapi.js"></script>
    <link rel="stylesheet" href="assets/css/Ajaxcal.css" />
    <link rel="stylesheet" href="assets/toastr/toastr.min.css" />
    <!-- jquery: REQUIRED JQUERY FILE -->
    <script src="assets/vendor/js/ie-emulation-modes-warning.js"></script>
    <link rel="stylesheet" href="assets/css/Ajaxcal.css" />
    <link rel="stylesheet" href="assets/toastr/toastr.min.css" />
    <script src="assets/plugins/jQuery-lib/2.0.3/jquery.min.js"></script>
    <script src="assets/plugins/jquery-ui/jquery-ui-1.10.2.custom.min.js"></script>
    <script src="assets/plugins/bootstrap/js/bootstrap.min.js"></script>
    <script src="assets/toastr/toastr.min.js"></script>
    <!-- jquery: REQUIRED JQUERY FILE -->
    <script src="assets/vendor/js/jquery-1.10.0.min.js"></script>
    <script src="assets/plugins/bootstrap/js/bootstrap.min.js"></script>


    <style type='text/css'>
        .myCssClass {
            border-top-color: rgb(127, 126, 126);
            border-top-style: solid;
            border-top-width: 1px;
            border-right-color: rgb(127, 126, 126);
            border-right-style: solid;
            border-right-width: 1px;
            border-bottom-color: rgb(127, 126, 126);
            border-bottom-style: solid;
            border-bottom-width: 1px;
            border-left-color: rgb(127, 126, 126);
            border-left-style: solid;
            border-left-width: 1px;
            border-image-source: initial;
            border-image-slice: initial;
            border-image-width: initial;
            border-image-outset: initial;
            border-image-repeat: initial;
            width: 100%;
            padding: 6px 12px;
            border-top-left-radius: 4px;
            border-top-right-radius: 4px;
            border-bottom-right-radius: 4px;
            border-bottom-left-radius: 4px;
            height: 26px;
            font-size: 12px;
            background-color: #f4ecce;
        }
    </style>
    <script>
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

        function GetValue(val) {

            var id = val;
            if (id == 1) {
                document.getElementById("txtPhyDDYes").disabled = false;
            }
            else {
                document.getElementById("txtPhyDDYes").value = "";

                document.getElementById("txtPhyDDYes").disabled = true;
            }
        }
        function GetSuspended(val) {

            var id = val;
            if (id == 1) {
                document.getElementById("txtsuspend").disabled = false;
            }
            else {
                document.getElementById("txtsuspend").value = "";

                document.getElementById("txtsuspend").disabled = true;
            }
        }
        function Validate() {
           
            var Email = document.getElementById('<%= txtEmail.ClientID %>').value;

            var Mobile = document.getElementById('<%= txtMobile.ClientID %>').value;

            var Address = document.getElementById('<%= txtPresent.ClientID %>').value;


            var linkedin_url = document.getElementById('<%= txtLinkUrl.ClientID %>').value;
            var Facebook_url = document.getElementById('<%= txtFacebook.ClientID %>').value;
            var Twitter_url = document.getElementById('<%= txtTwitter.ClientID %>').value;
         
            var reEmail = /^(?:[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+\.)*[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\-](?!\.)){0,61}[a-zA-Z0-9]?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\[(?:(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\.){3}(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\]))$/;

            if (!Email.match(reEmail)) {
                alert("Invalid email address");
                document.getElementById('<%= txtEmail.ClientID %>').style.borderColor = "red";
                return false;
            }
            if (Mobile == "") {
                document.getElementById('<%= txtMobile.ClientID %>').style.borderColor = "red";
                return false;
            }
            if (Address == "") {
                document.getElementById('<%= txtPresent.ClientID %>').style.borderColor = "red";
                 return false;
             }
         
            if (document.getElementById('<%= txtLinkUrl.ClientID %>').value != "") {
                if (/(ftp|http|https):\/\/?(?:www\.)?linkedin.com(\w+:{0,1}\w*@)?(\S+)(:([0-9])+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/.test(linkedin_url)) {

                } else {
                    alert("Please Enter Valid Url.");
                    document.getElementById('<%= txtLinkUrl.ClientID %>').style.borderColor = "red";
                    return false;
                }
            }

            if (document.getElementById('<%= txtFacebook.ClientID %>').value != "") {
                if (/^(https?:\/\/)?((w{3}\.)?)facebook.com\/.*/i.test(Facebook_url)) {

                } else {
                    alert("Please Enter Valid Url.");
                    document.getElementById('<%= txtFacebook.ClientID %>').style.borderColor = "red";
                    return false;
                }
            }



            if (document.getElementById('<%= txtTwitter.ClientID %>').value != "") {
                if (/^(https?:\/\/)?((w{3}\.)?)twitter\.com\/(#!\/)?[a-z0-9_]+$/i.test(Twitter_url)) {

                } else {
                    alert("Please Enter Valid Url.");
                    document.getElementById('<%= txtTwitter.ClientID %>').style.borderColor = "red";
                    return false;
                }
            }

            return true;
        }

        function Validate1() {

            var drpCurrent = document.getElementById('<%= drpCurrent.ClientID %>').value;

            if (drpCurrent == "0") {
                alert("Please Select Current Professional Status.");
                return false;
            }
            if (drpCurrent == "1") {
               
                var Employer = document.getElementById('<%= txtEmployer.ClientID %>').value; 

                var Designation = document.getElementById('<%= txtDesignation.ClientID %>').value;

                var txtEAddress = document.getElementById('<%= txtEAddress.ClientID %>').value;

                var txtIndustry = document.getElementById('<%= txtIndustry.ClientID %>').value;

                var txtJDesc = document.getElementById('<%= txtJDesc.ClientID %>').value;

                var txtSDate = document.getElementById('<%= txtSDate.ClientID %>').value;
                if (Employer == "") {
                    document.getElementById('<%= txtEmployer.ClientID %>').style.borderColor = "red";
                    return false;
                }
                if (Designation == "") {
                    document.getElementById('<%= txtDesignation.ClientID %>').style.borderColor = "red";
                      return false;
                }
               
                if (txtEAddress == "") {
                    document.getElementById('<%= txtEAddress.ClientID %>').style.borderColor = "red";
                      return false;
                }
                if (txtIndustry == "") {
                    document.getElementById('<%= txtIndustry.ClientID %>').style.borderColor = "red";
                      return false;
                }
                if (txtJDesc == "") {
                    document.getElementById('<%= txtJDesc.ClientID %>').style.borderColor = "red";
                      return false;
                }
                if (txtSDate == "") {
                    document.getElementById('<%= txtSDate.ClientID %>').style.borderColor = "red";
                     return false;
                 }

                return true;
            }
          
            if (drpCurrent == "2") {
                
                var txtEnterName = document.getElementById('<%= txtEnterName.ClientID %>').value;

                var txtSelfIndustry = document.getElementById('<%= txtSelfIndustry.ClientID %>').value;

                var txtSelfAddress = document.getElementById('<%= txtSelfAddress.ClientID %>').value;

                var txtRole = document.getElementById('<%= txtRole.ClientID %>').value;

                if (txtEnterName == "") {
                    document.getElementById('<%= txtEnterName.ClientID %>').style.borderColor = "red";
                    return false;
                }
                if (txtSelfIndustry == "") {
                    document.getElementById('<%= txtSelfIndustry.ClientID %>').style.borderColor = "red";
                      return false;
                  }
                if (txtSelfAddress == "") {
                    document.getElementById('<%= txtSelfAddress.ClientID %>').style.borderColor = "red";
                       return false;
                }
                if (txtRole == "") {
                    document.getElementById('<%= txtRole.ClientID %>').style.borderColor = "red";
                     return false;
                 }

                return true;
            }
            if (drpCurrent == "3") {
                var txtCollegeName = document.getElementById('<%= txtCollegeName.ClientID %>').value;

                var txtEduAddress = document.getElementById('<%= txtEduAddress.ClientID %>').value;

                var txtEWebUrl = document.getElementById('<%= txtEWebUrl.ClientID %>').value;

                var txtProgramName = document.getElementById('<%= txtProgramName.ClientID %>').value;

                var txtEduAdmissionYear = document.getElementById('<%= txtEduAdmissionYear.ClientID %>').value;

                var txtExGradYear = document.getElementById('<%= txtExGradYear.ClientID %>').value;

                var txtFurtherPlan = document.getElementById('<%= txtFurtherPlan.ClientID %>').value;

                if (txtCollegeName == "") {
                    document.getElementById('<%= txtCollegeName.ClientID %>').style.borderColor = "red";
                    return false;
                }
                if (txtEduAddress == "") {
                    document.getElementById('<%= txtEduAddress.ClientID %>').style.borderColor = "red";
                    return false;
                }
                if (txtEWebUrl == "") {
                    document.getElementById('<%= txtEWebUrl.ClientID %>').style.borderColor = "red";
                    return false;
                }
                if (txtProgramName == "") {
                    document.getElementById('<%= txtProgramName.ClientID %>').style.borderColor = "red";
                    return false;
                }
                if (txtEduAdmissionYear == "") {
                    document.getElementById('<%= txtEduAdmissionYear.ClientID %>').style.borderColor = "red";
                     return false;
                }
                if (txtExGradYear == "") {
                    document.getElementById('<%= txtExGradYear.ClientID %>').style.borderColor = "red";
                    return false;
                }
                if (txtFurtherPlan == "") {
                    document.getElementById('<%= txtFurtherPlan.ClientID %>').style.borderColor = "red";
                     return false;
                 }

                return true;
            }
            if (drpCurrent == "4") {

                return true;

            }
            return false;
        }

        function GetNationality(val) {

            var id = val;

            var div = document.getElementById('divnat');

            // hide
            //div.style.visibility = 'hidden';
            // OR
            //div.style.display = 'none';

            // show
            //div.style.visibility = 'visible';
            // OR
            //div.style.display = 'block';
            if (id == "FOREIGN") {
                //
                document.getElementById("divnat").removeAttribute("hidden", "");
                //alert(id);

            }
            else {
                document.getElementById("txtNationalSpec").value = "";
                document.getElementById("divnat").setAttribute("hidden", "");

            }
        }

        function GetHostel(val) {

            var id = val;
            if (id == 1) {
                document.getElementById("drpHostReqYes").disabled = false;
            }
            else {
                document.getElementById("drpHostReqYes").value = "0";
                document.getElementById("drpHostReqYes").disabled = true;
            }
        }
        function GetChronic(val) {

            var id = val;
            if (id == 1) {
                document.getElementById("txtchronic").disabled = false;
            }
            else {
                document.getElementById("txtchronic").value = "";
                document.getElementById("txtchronic").disabled = true;
            }
        }
        function GetFamily(val) {

            var id = val;
            if (id == "Yes") {
                document.getElementById("txtSRelation").disabled = false;
                document.getElementById("txtStudiedName").disabled = false;
                document.getElementById("txtSNprogram").disabled = false;
                document.getElementById("txtSNyear").disabled = false;
                document.getElementById("txtworkname").disabled = false;
                document.getElementById("txtWNDepartment").disabled = false;
                document.getElementById("txtWNYEAR").disabled = false;
            }
            else {
                document.getElementById("txtSRelation").value = "";
                document.getElementById("txtStudiedName").value = "";
                document.getElementById("txtSNprogram").value = "";
                document.getElementById("txtSNyear").value = "";
                document.getElementById("txtworkname").value = "";
                document.getElementById("txtWNDepartment").value = "";
                document.getElementById("txtWNYEAR").value = "";

                document.getElementById("txtSRelation").disabled = true;
                document.getElementById("txtStudiedName").disabled = true;
                document.getElementById("txtSNprogram").disabled = true;
                document.getElementById("txtSNyear").disabled = true;
                document.getElementById("txtworkname").disabled = true;
                document.getElementById("txtWNDepartment").disabled = true;
                document.getElementById("txtWNYEAR").disabled = true;
            }
        }



        function customAlert(msgType, txtMSG) {
            msgType = msgType.toUpperCase()

            if (msgType == "S") {
                showSuccess(txtMSG);
                return false;
            }

            if (msgType == "W") {
                showWarning(txtMSG);
                return false;
            }

            if (msgType == "E") {
                showError(txtMSG);
                return false;
            }

            if (msgType == "I") {
                showInfo(txtMSG);
                return false;
            }
        }

        //Tostr Notification implementation Start !!!!
        //Success!!!
        function showSuccess(msg) {
            toastr.options = {
                "closeButton": true,
                "progressBar": true,
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut",
                "positionClass": "toast-top-right"
            }
            toastr.success(msg, "Success");
            return false;
        }

        //Information!!!
        function showInfo(msg) {
            toastr.options = {
                "closeButton": true,
                "progressBar": true,
                "positionClass": "toast-top-right",
                "closeDuration": "60000"
            }
            toastr.info(msg, "Information");
            return false;
        }

        //Warning!!!
        function showWarning(msg) {
            toastr.options = {
                "closeButton": true,
                "progressBar": true,
                "positionClass": "toast-top-right"
            }
            toastr.warning(msg, "Warning");
            return false;
        }

        //Error!!!
        function showError(msg) {
            toastr.options = {
                "closeButton": true,
                "progressBar": true,
                "positionClass": "toast-top-right"
            }
            toastr.error(msg, "Error");
            return false;
        }
    </script>
    <script>
        $(window).load(function () {
            $(".loader").fadeOut("slow");;
        });
    </script>
    <script>
        $(document).ready(function () {
            document.getElementById("txtPhyDDYes").disabled = true;
            document.getElementById("drpHostReqYes").disabled = true;
            document.getElementById("txtSRelation").disabled = true;
            document.getElementById("txtStudiedName").disabled = true;
            document.getElementById("txtSNprogram").disabled = true;
            document.getElementById("txtSNyear").disabled = true;
            document.getElementById("txtworkname").disabled = true;
            document.getElementById("txtWNDepartment").disabled = true;
            document.getElementById("txtWNYEAR").disabled = true;
            document.getElementById("txtchronic").disabled = true;
            document.getElementById("txtsuspend").disabled = true;
            $("#stuMaritalStatus").on("change", function (event) {
                var marital = $("#stuMaritalStatus").val();
                showDiv(marital);
            })
        });



    </script>
    <script>
        var vCategory = document.getElementById("ContentPlaceHolder1_lblSessionCategory").innerHTML;
        var vChkCategoryCast = vCategory.toUpperCase();
        if (vChkCategoryCast == "GENERAL") {
            $("#lbltxtPermanentState").show();
            document.getElementById("txtCountry").value = "IN";
            bindState('IN');
            // $('#txtCountry option:selected').val() = "IN";
            document.getElementById("txtCountry").disabled = true;
            $('#chkPermanent').change(function () {
                //$('#txtCountry').toggleClass("dis");
                // document.getElementById("txtCountry").disabled = true;
            });
        }
        else {

            document.getElementById("txtCountry").disabled = false;
            $("#lbltxtPermanentState").hides();
            //  $("#lbltxtPresentState").hide();              
        }

        function NumericTextBox(evt) {

            var charCode = (evt.charCode) ? evt.charCode : ((evt.which) ? evt.which : evt.keyCode);
            if (charCode == 8 || charCode == 9 || //backspace
                charCode == 46 || //delete
                charCode == 13)   //enter key
            {
                return true;
            }
            else if (charCode >= 37 && charCode <= 40) //arrow keys
            {
                return true;
            }
            else if (charCode >= 48 && charCode <= 57) //0-9 on key pad
            {
                return true;
            }
            else if (charCode >= 96 && charCode <= 105) //0-9 on num pad
            {
                return true;
            }
            else
                return false;



            window.onload = function () {
                var d = new Date().getTime();
                document.getElementById("tid").value = d;
            };
        }


    </script>
    <!-- start: JAVASCRIPTS REQUIRED FOR THIS PAGE ONLY -->
    <script src="assets/plugins/jquery-validation/dist/jquery.validate.min.js"></script>
    <script src="assets/plugins/jQuery-Smart-Wizard/js/jquery.smartWizard.js"></script>
    <script src="assets/js/form-wizard.js"></script>





    <script src="assets/vendor/js/jquery-1.10.0.min.js"></script>
    <%-- <script src="assets/plugins/jquery-ui/jquery-ui-1.10.2.custom.min.js"></script>--%>
    <script src="assets/plugins/bootstrap/js/bootstrap.min.js"></script>
    <script src="assets/toastr/toastr.min.js"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">




    <div class="row" style="margin-bottom: 30px;">
        <div class="col-md-12 text-center top_heading_border">
            <h2 class="top_heading">ALUMNI REGISTRATION FORM</h2>
            <span></span>
        </div>
    </div>

    <div action="#" runat="server" role="form" class="smart-wizard form-horizontal" id="form">
        <div class="col-md-1">
        </div>
        <div class="col-md-10">
            <div class="form-wizard-wrapper">
                <div class="form-wizard-wrapper-inner">
                    <asp:ScriptManager ID="test" runat="server"></asp:ScriptManager>

                    <div class="right_col_bg">
                        <div class="right_col_content border-box label-responsive">
                            <div class="row">
                                <div class="col-md-12">
                                    <%--<h2 style="text-align:center">Application Form</h2>--%>

                                    <div id="wizard" class="swMain" runat="server">


                                        <div class="progress progress-striped active progress-sm">
                                            <div id="divTrackDistributor" runat="server" aria-valuemax="100" aria-valuemin="0" role="progressbar" class="progress-bar progress-bar-success step-bar">
                                                <span class="sr-only">0% Complete (success)</span>
                                                <asp:Label ID="lblprogress" runat="server" Font-Size="Large" ForeColor="Green"></asp:Label>
                                            </div>
                                        </div>

                                        <div id="STEPA" runat="server" visible="true">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <h5 style="text-align: right;">Step 1 of 3</h5>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-6 form-group">
                                                    <div class="col-md-6" style="vertical-align: bottom">
                                                        <label style="font: bold; color: black">
                                                            Email address :<span class="symbol required"></span>
                                                        </label>
                                                    </div>
                                                    <div class="col-md-6" style="vertical-align: top">
                                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email Address" autocomplete="off" MaxLength="100" onchange="blockSpecialChar(this)" onkeyup="blockSpecialChar(this)"></asp:TextBox>

                                                    </div>
                                                </div>
                                                <div class="col-sm-6 form-group">
                                                    <div class="col-md-6">
                                                        <label style="font: bold; color: black">
                                                            Student Name : <span class="symbol required"></span>
                                                        </label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtFname" CssClass="form-control" Enabled="false" placeholder="First Name" runat="server">
                                                            
                                                        </asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>

                                            <%-- <div class="row">
                                                <div class="col-sm-6 form-group">
                                                    <div class="col-md-6" style="vertical-align:bottom">
                                                        <label style="font:bold;color:black">
                                                           Middle Name :<span class="symbol required"></span>
                                                        </label>
                                                    </div>
                                                    <div class="col-md-6" style="vertical-align:top">
                                                        <asp:TextBox ID="txtMname" runat="server" CssClass="form-control" placeholder="Middle Name" autocomplete="off" MaxLength="100" onchange="blockSpecialChar(this)" onkeyup="blockSpecialChar(this)"></asp:TextBox>
                                                       
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 form-group">
                                                    <div class="col-md-6">
                                                        <label style="font:bold;color:black">
                                                            Last Name : <span class="symbol required"></span>
                                                        </label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtLName" CssClass="form-control" placeholder="Last Name" runat="server">
                                                            
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                                
                                            </div> --%>

                                            <div class="row">
                                                <div class="col-sm-6 form-group">
                                                    <div class="col-md-6" style="vertical-align: bottom">
                                                        <label style="font: bold; color: black">
                                                            D.O.B :<span class="symbol required"></span>
                                                        </label>
                                                    </div>
                                                    <div class="col-md-6" style="vertical-align: top">
                                                        <asp:TextBox ID="txtDOB" runat="server" Enabled="false" CssClass="form-control" placeholder="Date Of Birth" autocomplete="off" MaxLength="100" onchange="blockSpecialChar(this)" onkeyup="blockSpecialChar(this)"></asp:TextBox>

                                                    </div>
                                                </div>
                                                <div class="col-sm-6 form-group">
                                                    <div class="col-md-6">
                                                        <label style="font: bold; color: black">
                                                            Gender : <span class="symbol required"></span>
                                                        </label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtGender" Enabled="false" CssClass="form-control" placeholder="Gender" runat="server">
                                                            
                                                        </asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>


                                            <div class="row">
                                                <div class="col-sm-6 form-group">
                                                    <div class="col-md-6" style="vertical-align: bottom">
                                                        <label style="font: bold; color: black">
                                                            Mobile Number :<span class="symbol required"></span>
                                                        </label>
                                                    </div>
                                                    <div class="col-md-6" style="vertical-align: top">
                                                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" placeholder="Mobile Number" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(event)" ></asp:TextBox>

                                                    </div>
                                                </div>
                                                <div class="col-sm-6 form-group">
                                                    <div class="col-md-6">
                                                        <label style="font: bold; color: black">
                                                            WhatsApp Number : <span class="symbol required"></span>
                                                        </label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtWhatsUp" CssClass="form-control" placeholder="WhatsApp Number" runat="server" MaxLength="10" onkeypress="return isNumberKey(event)">
                                                            
                                                        </asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="row">
                                                <div class="col-sm-6 form-group">
                                                    <div class="col-md-6" style="vertical-align: bottom">
                                                        <label style="font: bold; color: black">
                                                            Present Address :<span class="symbol required"></span>
                                                        </label>
                                                    </div>
                                                    <div class="col-md-6" style="vertical-align: top">
                                                        <asp:TextBox ID="txtPresent" runat="server" CssClass="form-control" placeholder="Prsent Address" autocomplete="off" MaxLength="100" onchange="blockSpecialChar(this)" onkeyup="blockSpecialChar(this)"></asp:TextBox>

                                                    </div>
                                                </div>
                                                <div class="col-sm-6 form-group">
                                                    <div class="col-md-6">
                                                        <label style="font: bold; color: black">
                                                            Permanent Address : <span class="symbol required"></span>
                                                        </label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtPermanent" CssClass="form-control" Enabled="false" placeholder="Permanent Address" runat="server">
                                                            
                                                        </asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="row">
                                                <div class="col-sm-6 form-group">
                                                    <div class="col-md-6" style="vertical-align: bottom">
                                                        <label style="font: bold; color: black">
                                                            LinkedIn URL :<span class="symbol required"></span>
                                                        </label>
                                                    </div>
                                                    <div class="col-md-6" style="vertical-align: top">
                                                        <asp:TextBox ID="txtLinkUrl" runat="server" CssClass="form-control" placeholder="LinkedIn Url" autocomplete="off" MaxLength="100" onchange="blockSpecialChar(this)" onkeyup="blockSpecialChar(this)"></asp:TextBox>

                                                    </div>
                                                </div>
                                                <div class="col-sm-6 form-group">
                                                    <div class="col-md-6">
                                                        <label style="font: bold; color: black">
                                                            Facebook URL : <span class="symbol required"></span>
                                                        </label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtFacebook" CssClass="form-control" placeholder="Facebook Url" runat="server">
                                                            
                                                        </asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6 form-group">
                                                    <div class="col-md-6" style="vertical-align: bottom">
                                                        <label style="font: bold; color: black">
                                                            Twitter Handle :<span class="symbol required"></span>
                                                        </label>
                                                    </div>
                                                    <div class="col-md-6" style="vertical-align: top">
                                                        <asp:TextBox ID="txtTwitter" runat="server" CssClass="form-control" placeholder="Twitter" autocomplete="off" MaxLength="100" onchange="blockSpecialChar(this)" onkeyup="blockSpecialChar(this)"></asp:TextBox>

                                                    </div>
                                                </div>


                                            </div>













                                            <div class="row">

                                                <div class="col-sm-6"></div>
                                                <div class="col-sm-6 form-group text-right">
                                                    <div class="col-md-12">


                                                        <asp:Button ID="btnsavestep1" CssClass="btn btn-success btn_next" runat="server" OnClientClick="return Validate();"    OnClick="btnsavestep1_Click"  Text="Save & Next" />


                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div id="Divpayment" runat="server" visible="false">

                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <h5 style="text-align: right;">Step 2 of 3</h5>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-bottom: 30px">
                                                <div class="col-sm-12">
                                                    <div class="col-lg-12 block-headerBottom">
                                                        <h1>Educational Detail</h1>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-6 form-group">
                                                <div class="col-md-6">
                                                    <label style="font: bold; color: black">
                                                        College Name : <span class="symbol required"></span>
                                                    </label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtCollege" Enabled="false" CssClass="form-control" placeholder="College Name" runat="server">
                                                    </asp:TextBox>
                                                    <asp:HiddenField ID="hfCollegeCode"  runat="server" />
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-sm-6 form-group">
                                                    <div class="col-md-6" style="vertical-align: bottom">
                                                        <label style="font: bold; color: black">
                                                            Program Name :<span class="symbol required"></span>
                                                        </label>
                                                    </div>
                                                    <div class="col-md-6" style="vertical-align: top">
                                                        <asp:TextBox ID="txtProgram" runat="server" Enabled="false" CssClass="form-control" placeholder="Program Name" autocomplete="off" MaxLength="100" onchange="blockSpecialChar(this)" onkeyup="blockSpecialChar(this)"></asp:TextBox>

                                                    </div>
                                                </div>


                                            </div>
                                            <div class="col-sm-6 form-group">
                                                <div class="col-md-6">
                                                    <label style="font: bold; color: black">
                                                        Enrollment No : <span class="symbol required"></span>
                                                    </label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtEnroll" CssClass="form-control" Enabled="false" placeholder="Enrollment No" runat="server">
                                                            
                                                    </asp:TextBox>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-sm-6 form-group">
                                                    <div class="col-md-6" style="vertical-align: bottom">
                                                        <label style="font: bold; color: black">
                                                            Admission Year :<span class="symbol required"></span>
                                                        </label>
                                                    </div>
                                                    <div class="col-md-6" style="vertical-align: top">
                                                        <asp:TextBox ID="txtAdmissionYear" runat="server" Enabled="false" CssClass="form-control" placeholder="Admission Year" autocomplete="off" MaxLength="100" onchange="blockSpecialChar(this)" onkeyup="blockSpecialChar(this)"></asp:TextBox>

                                                    </div>
                                                </div>


                                            </div>



                                            <div class="col-sm-6 form-group">
                                                <div class="col-md-6">
                                                    <label style="font: bold; color: black">
                                                        Passout Year : <span class="symbol required"></span>
                                                    </label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtGraduation" CssClass="form-control" Enabled="false" placeholder="Graduation Year" runat="server">
                                                            
                                                    </asp:TextBox>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-xs-4 col-sm-6 form-group">
                                                    <div class="col-md-12">
                                                    </div>
                                                </div>
                                                <div class="col-xs-8 col-sm-6 form-group">
                                                    <div class="col-md-12 text-right">
                                                        <asp:Button ID="btnbackPayment" runat="server" class="btn btn-warning back-step" OnClick="btnbackPayment_Click" Text="Back" />

                                                        <asp:Button ID="btnpaymentNext" runat="server" CssClass="btn btn-success" OnClick="btnpaymentNext_Click" Text="SAVE&NEXT" />


                                                    </div>
                                                </div>
                                            </div>
                                        </div>




                                        <div id="STEPB" runat="server" visible="false">





                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <h5 style="text-align: right;">Step 3 of 3</h5>
                                                </div>

                                            </div>


                                            <div class="row" style="margin-bottom: 30px">
                                                <div class="col-sm-12">
                                                    <div>
                                                        <h1>Professional Status</h1>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-sm-6 form-group">
                                                    <div class="col-md-6">
                                                        <label style="font: bold; color: black">
                                                            Current Status : <span class="symbol required"></span>
                                                        </label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="drpCurrent" CssClass="form-control" OnSelectedIndexChanged="drpCurrent_SelectedIndexChanged" placeholder="Graduation Year" AutoPostBack="true" runat="server">
                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Employed" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Self Employed/Start Up" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Further Education" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="Unemployed" Value="4"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div>
                                                </div>
                                            </div>
                                            <div id="divEmployee" runat="server" visible="false">
                                                <div class="row">
                                                    <div class="col-sm-6 form-group">
                                                        <div class="col-md-6" style="vertical-align: bottom">
                                                            <label style="font: bold; color: black">
                                                                Employer Name :<span class="symbol required"></span>
                                                            </label>
                                                        </div>
                                                        <div class="col-md-6" style="vertical-align: top">
                                                            <asp:TextBox ID="txtEmployer" runat="server" CssClass="form-control" placeholder="Employer Name" autocomplete="off" MaxLength="100" onchange="blockSpecialChar(this)" onkeyup="blockSpecialChar(this)"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6 form-group">
                                                        <div class="col-md-6">
                                                            <label style="font: bold; color: black">
                                                                Designation : <span class="symbol required"></span>
                                                            </label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:TextBox ID="txtDesignation" CssClass="form-control" placeholder="Designation" runat="server">
                                                            
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6 form-group">
                                                        <div class="col-md-6" style="vertical-align: bottom">
                                                            <label style="font: bold; color: black">
                                                                Address :<span class="symbol required"></span>
                                                            </label>
                                                        </div>
                                                        <div class="col-md-6" style="vertical-align: top">
                                                            <asp:TextBox ID="txtEAddress" runat="server" CssClass="form-control" placeholder="Company Address" autocomplete="off" MaxLength="100" onchange="blockSpecialChar(this)" onkeyup="blockSpecialChar(this)"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6 form-group">
                                                        <div class="col-md-6">
                                                            <label style="font: bold; color: black">
                                                                Industry : <span class="symbol required"></span>
                                                            </label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:TextBox ID="txtIndustry" CssClass="form-control" placeholder="Industry" runat="server">
                                                            
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6 form-group">
                                                        <div class="col-md-6" style="vertical-align: bottom">
                                                            <label style="font: bold; color: black">
                                                                Job Description :<span class="symbol required"></span>
                                                            </label>
                                                        </div>
                                                        <div class="col-md-6" style="vertical-align: top">
                                                            <asp:TextBox ID="txtJDesc" runat="server" CssClass="form-control" placeholder="Job Description" autocomplete="off" MaxLength="100" onchange="blockSpecialChar(this)" onkeyup="blockSpecialChar(this)"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6 form-group">
                                                        <div class="col-md-6">
                                                            <label style="font: bold; color: black">
                                                                Job Status : <span class="symbol required"></span>
                                                            </label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:DropDownList ID="ddlJDesc" CssClass="form-control" runat="server">
                                                                <asp:ListItem Text="Full Time" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Part Time" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Intership" Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6 form-group">
                                                        <div class="col-md-6" style="vertical-align: bottom">
                                                            <label style="font: bold; color: black">
                                                                Start Date :<span class="symbol required"></span>
                                                            </label>
                                                        </div>
                                                        <div class="col-md-6" style="vertical-align: top">
                                                            <asp:TextBox ID="txtSDate" runat="server" CssClass="form-control" placeholder="Start Date" autocomplete="off" MaxLength="100" onchange="blockSpecialChar(this)" onkeyup="blockSpecialChar(this)"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSDate" Format="dd MMM yyyy">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6 form-group">
                                                        <div class="col-md-6">
                                                            <label style="font: bold; color: black">
                                                                Company Website URL : <span class="symbol required"></span>
                                                            </label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:TextBox ID="txtCompanyURL" placeholder="Company Website"  CssClass="form-control" runat="server">
                                                            
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-6 form-group">
                                                        <div class="col-md-6" style="vertical-align: bottom">
                                                            <label style="font: bold; color: black">
                                                                Company Email :<span class="symbol required"></span>
                                                            </label>
                                                        </div>
                                                        <div class="col-md-6" style="vertical-align: top">
                                                            <asp:TextBox ID="txtCMail" runat="server" CssClass="form-control" placeholder="Company Email" autocomplete="off" MaxLength="100" onchange="blockSpecialChar(this)" onkeyup="blockSpecialChar(this)"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6 form-group">
                                                        <div class="col-md-6">
                                                            <label style="font: bold; color: black">
                                                                Company Telephone :<span class="symbol required"></span>
                                                            </label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:TextBox ID="txtCompanyTelephone"  placeholder="Company Telephone" onkeypress="return isNumberKey(event)" CssClass="form-control" runat="server">
                                                            
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                            <div id="divSelfEmployee" runat="server" visible="false">
                                                <div class="row">
                                                    <div class="col-sm-6 form-group">
                                                        <div class="col-md-6" style="vertical-align: bottom">
                                                            <label style="font: bold; color: black">
                                                                Enterprise Name :<span class="symbol required"></span>
                                                            </label>
                                                        </div>
                                                        <div class="col-md-6" style="vertical-align: top">
                                                            <asp:TextBox ID="txtEnterName" runat="server" CssClass="form-control" placeholder="Enterprise Name" autocomplete="off" MaxLength="100" onchange="blockSpecialChar(this)" onkeyup="blockSpecialChar(this)"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6 form-group">
                                                        <div class="col-md-6">
                                                            <label style="font: bold; color: black">
                                                                Industry : <span class="symbol required"></span>
                                                            </label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:TextBox ID="txtSelfIndustry" CssClass="form-control" placeholder="Industry" runat="server">
                                                            
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-6 form-group">
                                                        <div class="col-md-6" style="vertical-align: bottom">
                                                            <label style="font: bold; color: black">
                                                                Address :<span class="symbol required"></span>
                                                            </label>
                                                        </div>
                                                        <div class="col-md-6" style="vertical-align: top">
                                                            <asp:TextBox ID="txtSelfAddress" runat="server" CssClass="form-control" placeholder="Company Address" autocomplete="off" MaxLength="100" onchange="blockSpecialChar(this)" onkeyup="blockSpecialChar(this)"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6 form-group">
                                                        <div class="col-md-6">
                                                            <label style="font: bold; color: black">
                                                                Your Role : <span class="symbol required"></span>
                                                            </label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:TextBox ID="txtRole" CssClass="form-control" placeholder="Role" runat="server">
                                                            
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-6 form-group">
                                                        <div class="col-md-6" style="vertical-align: bottom">
                                                            <label style="font: bold; color: black">
                                                                Company WebSite URL :<span class="symbol required"></span>
                                                            </label>
                                                        </div>
                                                        <div class="col-md-6" style="vertical-align: top">
                                                            <asp:TextBox ID="txtSelfCompanyURL" runat="server" CssClass="form-control" placeholder="Company URL" autocomplete="off" MaxLength="100" onchange="blockSpecialChar(this)" onkeyup="blockSpecialChar(this)"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                            <div id="divFurther" runat="server" visible="false">
                                                <div class="row">
                                                    <div class="col-sm-6 form-group">
                                                        <div class="col-md-6" style="vertical-align: bottom">
                                                            <label style="font: bold; color: black">
                                                                University/College Name :<span class="symbol required"></span>
                                                            </label>
                                                        </div>
                                                        <div class="col-md-6" style="vertical-align: top">
                                                            <asp:TextBox ID="txtCollegeName" runat="server" CssClass="form-control" placeholder="College Name" autocomplete="off" MaxLength="100" onchange="blockSpecialChar(this)" onkeyup="blockSpecialChar(this)"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6 form-group">
                                                        <div class="col-md-6">
                                                            <label style="font: bold; color: black">
                                                                Address : <span class="symbol required"></span>
                                                            </label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:TextBox ID="txtEduAddress" CssClass="form-control" placeholder="College Address" runat="server">
                                                            
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-6 form-group">
                                                        <div class="col-md-6" style="vertical-align: bottom">
                                                            <label style="font: bold; color: black">
                                                                University Website URL :<span class="symbol required"></span>
                                                            </label>
                                                        </div>
                                                        <div class="col-md-6" style="vertical-align: top">
                                                            <asp:TextBox ID="txtEWebUrl" runat="server" CssClass="form-control" placeholder="University Website URL" autocomplete="off" MaxLength="100" onchange="blockSpecialChar(this)" onkeyup="blockSpecialChar(this)"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6 form-group">
                                                        <div class="col-md-6">
                                                            <label style="font: bold; color: black">
                                                                Programme Name : <span class="symbol required"></span>
                                                            </label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:TextBox ID="txtProgramName" CssClass="form-control" placeholder="Programme Name" runat="server">
                                                            
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-6 form-group">
                                                        <div class="col-md-6" style="vertical-align: bottom">
                                                            <label style="font: bold; color: black">
                                                                Admission Year :<span class="symbol required"></span>
                                                            </label>
                                                        </div>
                                                        <div class="col-md-6" style="vertical-align: top">
                                                            <asp:TextBox ID="txtEduAdmissionYear" runat="server" CssClass="form-control"  placeholder="Admission Year" onkeypress="return isNumberKey(event)" autocomplete="off" MaxLength="5" onchange="blockSpecialChar(this)" onkeyup="blockSpecialChar(this)"></asp:TextBox>

                                                        </div>
                                                    </div>

                                                    <div class="col-sm-6 form-group">
                                                        <div class="col-md-6" style="vertical-align: bottom">
                                                            <label style="font: bold; color: black">
                                                                Expected Graduation Year :<span class="symbol required"></span>
                                                            </label>
                                                        </div>
                                                        <div class="col-md-6" style="vertical-align: top">
                                                            <asp:TextBox ID="txtExGradYear" runat="server" CssClass="form-control" placeholder="Expected Graduation Year" onkeypress="return isNumberKey(event)" autocomplete="off" MaxLength="5" onchange="blockSpecialChar(this)" onkeyup="blockSpecialChar(this)"></asp:TextBox>

                                                        </div>
                                                    </div>


                                                </div>


                                                <div class="row">
                                                    <div class="col-sm-6 form-group">
                                                        <div class="col-md-6" style="vertical-align: bottom">
                                                            <label style="font: bold; color: black">
                                                                Further Plan :<span class="symbol required"></span>
                                                            </label>
                                                        </div>
                                                        <div class="col-md-6" style="vertical-align: top" >
                                                            <asp:TextBox ID="txtFurtherPlan" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Further Plan" autocomplete="off" MaxLength="100" onchange="blockSpecialChar(this)" onkeyup="blockSpecialChar(this)"></asp:TextBox>

                                                        </div>
                                                    </div>




                                                </div>




                                            </div>

                                            <div id="divUnEmployee" runat="server" visible="false">
                                                <label style="font: bold; color: black">
                                                    Please Click Save/Next for Complete the Process </span>
                                                </label>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-4 col-sm-6 form-group">
                                                    <div class="col-md-12">
                                                    </div>
                                                </div>
                                                <div class="col-xs-8 col-sm-6 form-group">
                                                    <div class="col-md-12 text-right">
                                                        <asp:Button ID="btnbackPayment1" runat="server" class="btn btn-warning back-step" OnClick="btnbackPayment1_Click" Text="Back" />

                                                        <asp:Button ID="btnpaymentNext1" runat="server" CssClass="btn btn-success"   OnClientClick="return Validate1();" OnClick="btnpaymentNext1_Click" Text="SAVE&SUBMIT" />
                                                    </div>
                                                </div>
                                            </div>




                                        </div>





                                    </div>











                                </div>

                            </div>

                        </div>

                    </div>


                </div>
            </div>
        </div>
    </div>



    </div>

</asp:Content>

