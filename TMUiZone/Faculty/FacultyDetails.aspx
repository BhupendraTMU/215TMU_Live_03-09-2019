<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="FacultyDetails.aspx.cs" Inherits="Faculty_FacultyDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        #confirmModal.modal-dialog.modalPopup {
            width: 100%;
        }

        table thead tr th:first-child, .table > tbody > tr > th:first-child {
            border-left: 1px solid #60594f;
            padding: 5px 8px;
        }

        .modal-dialog.modalPopup {
            -webkit-transform: translate(0,0);
            -ms-transform: translate(0,0);
            -o-transform: translate(0,0);
            transform: translate(0,0);
            position: absolute;
            left: 70%;
            top: 32%;
            transform: translate(-50%,-50%) !important;
            margin: 0;
            border: none;
            width: 610px;
            height: 700px;
        }

        .modalPopup .modal-header {
            padding: 8px 15px;
            border-bottom: 1px solid #e5e5e5;
            background-color: #2FBDF1;
            -webkit-border-top-left-radius: 4px;
            -webkit-border-top-right-radius: 4px;
            -moz-border-radius-topleft: 4px;
            -moz-border-radius-topright: 4px;
            border-top-left-radius: 6px;
            border-top-right-radius: 6px;
        }

            .modalPopup .modal-header b {
                color: #fff;
            }

        #ContentPlaceHolder1_updatepnl table td {
            padding: 5px 0px;
        }

        input[type=checkbox] {
            /* Double-sized Checkboxes */
            -ms-transform: scale(1.5); /* IE */
            -moz-transform: scale(1.5); /* FF */
            -webkit-transform: scale(1.5); /* Safari and Chrome */
            -o-transform: scale(1.5); /* Opera */
            transform: scale(1.5);
            padding: 10px;
        }

        /* Might want to wrap a span around your checkbox text */
        .checkboxtext {
            /* Checkbox text */
            font-size: 110%;
            display: inline;
        }

        .section {
            display: flex;
            flex-wrap: wrap;
            gap: 15px;
            margin-bottom: 15px;
        }

        .field {
            display: flex;
            align-items: center; /* Label aur input ek line me center align honge */
            width: 48%; /* 2 field per row */
        }

            .field label {
                width: 120px; /* Label ki fixed width */
                font-weight: bold;
                color: #333;
            }

            .field input {
                flex: 1; /* Input box remaining width le lega */
                padding: 6px 8px;
                border: 1px solid #ccc;
                border-radius: 5px;
            }

            .field select {
                flex: 1; /* Input box remaining width le lega */
                padding: 6px 8px;
                border: 1px solid #ccc;
                border-radius: 5px;
            }

        .submit-btn {
            background: #28a745;
            color: #fff;
            border: none;
            padding: 8px 14px;
            border-radius: 6px;
            cursor: pointer;
        }

            .submit-btn:hover {
                background: #218838;
            }
    </style>


    <script type="text/javascript">
        function fun() {

            document.getElementById('ContentPlaceHolder1_Social').style.visibility = 'hidden';
            document.getElementById('ContentPlaceHolder1_pnlPopup').style.visibility = 'hidden';

        }

        function closePopup() {
            document.getElementById('ContentPlaceHolder1_Social').style.visibility = 'hidden';
            document.getElementById('ContentPlaceHolder1_pnlPopup').style.visibility = 'hidden';
            document.getElementById('mpe_backgroundElement').style.visibility = 'hidden';
        }
        function showPopup2() {
            var hfprofilereq = document.getElementById('ContentPlaceHolder1_hfprofilereq').value;
            if (hfprofilereq == "100" || hfprofilereq == "2") {
                $find("mpe2").show();
            }
            else {
                alert("already Apllied !");
            }

            // modal show
        }
        function closePopup2() {
            $find("mpe2").hide();   // modal hide
        }
        function validateForm() {
            var firstDOB = document.getElementById('ContentPlaceHolder1_txtBirthName').value.trim();
            var secondDOB = document.getElementById('ContentPlaceHolder1_txtBirthName11').value.trim();

            var txtName11 = document.getElementById('ContentPlaceHolder1_txtName11').value;
            var ddlGender11 = document.getElementById('ContentPlaceHolder1_ddlGender11').value;
            var txtBirthName11 = document.getElementById('ContentPlaceHolder1_txtBirthName11').value;
          
            var ddlDesignationName11 = document.getElementById('ContentPlaceHolder1_ddlDesignationName11').value;
            var txtFatherName11 = document.getElementById('ContentPlaceHolder1_txtFatherName11').value;
            var txtAdharCard11 = document.getElementById('ContentPlaceHolder1_txtAdharCard11').value;
            var txtPANCardNo11 = document.getElementById('ContentPlaceHolder1_txtPANCardNo11').value;
            var txtMobileNo11 = document.getElementById('ContentPlaceHolder1_txtMobileNo11').value;
            var txtEmail11 = document.getElementById('ContentPlaceHolder1_txtEmail11').value;
          

           
            if (txtName11 == "")
            {
                alert("Please FillName !");
                return false;
            }
            if (ddlGender11 == "")
           {
                
                    alert("Please Fill Name !");
                    return false;
               
            }
       
        
            if (txtBirthName11 == "")
            {
                alert("Please Fill Bate of Birth !");
                return false;
            }
          
            if (txtFatherName11 == "")
            {
                alert("Please Fill Father Name !");
                return false;
            }
            if (txtAdharCard11=="")
            {
                alert("Please Fill Adhar Card Number !");
                return false;
            }
            if (txtPANCardNo11=="")
            {
                alert("Please Fill Pan Card Number !");
                return false;
            }
            if (txtMobileNo11 == "")
            {
                alert("Please Fill Mobile Number !");
                return false;
            }
            if (txtEmail11 == "")
            {
                alert("Please Fill E-Mail !");
                return false;
            }
           

            var date1 = new Date(firstDOB);
            var date2 = new Date(secondDOB);

            if (isNaN(date1.getTime()) || isNaN(date2.getTime())) {
                alert("Invalid date format. Please use a valid date.");
                return false;
            }
            date1.setHours(0, 0, 0, 0);
            date2.setHours(0, 0, 0, 0);
            if (date1.getTime() !== date2.getTime()) {
                var fileInput = document.getElementById('ContentPlaceHolder1_FileUpload1');
                if (!fileInput.value) {  // no file selected
                    alert("Please upload the DOB verification document.");
                    return false;
                }
                return true;
            }

            return true;
        }


        $(document).ready(function () {
            $('[id$=Principal1]').hide();
            $('[id$=Principal2]').hide();
            $('[id$=drpCourseCode1]').hide();
            $('[id$=drpFacultyCode]').hide();
            $('[id$=divContactBody]').hide();
            $('[id$=divAdministrationBody]').hide();
            $('[id$=divPersonalInformationBody]').hide();
            $('[id$=btnUpdate]').click(function Save() {
                var FacultyCode = $('[id$=txtFacultyNo]').val();
                var arr = {
                    MobileNo: $('[id$=txtMobileNo]').val(), PhoneNo: $('[id$=txtPhoneNo]').val(), Extension: $('[id$=txtExtension]').val(),
                    Email: $('[id$=txtEmail]').val(), EmergencyContactPer: $('[id$=txtEmergencyContactPer]').val(), EmergencyPhoneNo: $('[id$=txtEmergencyPhoneNo]').val(),
                    State: $('[id$=txtState]').val(), Address: $('[id$=txtAddress]').val(), GoogleSiteLink: $('[id$=txtGoogleSiteLink]').val(), FacultyCode: FacultyCode


                };
                $.ajax({
                    type: "POST",
                    url: "FacultyDetails.aspx/Save",
                    data: JSON.stringify(arr),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess,
                    error: function (xmlHttpRequest, textStatus, errorThrown) {
                        alert('Error');
                    }
                });
            });
        });
        function HideShow(me) {
            if (me.checked == true) {
                $('[id$=Principal1]').show();
                $('[id$=Principal2]').show();
                $('[id$=drpCourseCode1]').show();
                $('[id$=drpFacultyCode]').show();
            }
            else {
                $('[id$=Principal1]').hide();
                $('[id$=Principal2]').hide();
                $('[id$=drpCourseCode1]').hide();
                $('[id$=drpFacultyCode]').hide();
                var grid = document.getElementById("<%=drpFacultyCode.ClientID %>");
                grid.options.length = 0;
                var FacultyCode = $('[id$=chkboxAsPrincipal]').is(':checked') ? $('[id$=drpFacultyCode]').val() : '<%= Session["uid"] %>';
                var arr3 = { FacultyCode: FacultyCode };
                $.ajax({
                    type: "POST",
                    url: "FacultyDetails.aspx/bindData",
                    data: JSON.stringify(arr3),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess2,
                    error: function (xmlHttpRequest, textStatus, errorThrown) {
                        alert('Error');
                    }
                });
            }
        }

        function myFunction() {
            $('[id$=txtAddress]').keypress(function (e) {
                if ($(this).val().length >= 80) {
                    e.preventDefault();
                }
            });
        }
        function OnSuccess(data) {
            alertify.success("Update Successfully");
        }
        function bindfacultyCode() {
            var arr1 = { CourseCode: $('[id$=drpCourseCode1]').val() };
            $.ajax({
                type: "POST",
                url: "FacultyDetails.aspx/BindFacultyCode",
                data: JSON.stringify(arr1),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess1,
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    alert('Course');
                }
            });
        }
        function OnSuccess1(response) {
            debugger
            var grid = document.getElementById("<%=drpFacultyCode.ClientID %>");
            grid.options.length = 0;

            var options1 = "<option value = ''>-- Name --</option>";
            for (var i = 0; i < response.d.length; i++) {
                options1 += "<option value = '" + response.d[i].FacultyCode + " '>" + response.d[i].FacultyName + " </option>";
            }

            $('[id$=drpFacultyCode]').append(options1);

            return false;
        }
        function bindData() {
            var arr2 = { FacultyCode: $('[id$=drpFacultyCode]').val() };
            $.ajax({
                type: "POST",
                url: "FacultyDetails.aspx/bindData",
                data: JSON.stringify(arr2),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess2,
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    alert('Facylty');
                }
            });
        }
        function OnSuccess2(data) {
            for (var i = 0; i < data.d.length; i++) {
                $('[id$=txtAddress]').val(data.d[i].Address);
                $('[id$=txtAdharCard]').val(data.d[i].AdharCard);
                $('[id$=txtBirthName]').val(data.d[i].DOB);
                $('[id$=txtBloodGroup]').val(data.d[i].BloodGroup);
                $('[id$=txtBranchCode]').val(data.d[i].BranchCode);
                $('[id$=txtCardNo]').val(data.d[i].CardNo);
                $('[id$=txtCollegeCode]').val(data.d[i].CollegeCode);
                $('[id$=txtDepartmantCode]').val(data.d[i].DepartmantCode);
                $('[id$=txtDesignationCode]').val(data.d[i].DesignationCode);
                $('[id$=txtEmail]').val(data.d[i].Email);
                $('[id$=txtEmergencyContactPer]').val(data.d[i].EmergencyContactPer);
                $('[id$=txtEmergencyPhoneNo]').val(data.d[i].EmergencyPhoneNo);
                $('[id$=txtEmployeeType]').val(data.d[i].EmployeeType);
                $('[id$=txtEmployementDate]').val(data.d[i].EmployementDate);
                $('[id$=txtExtension]').val(data.d[i].Extension);
                $('[id$=txtFacultyNo]').val(data.d[i].FacultyNo);
                $('#txtFacultyNo11').val(data.d[i].FacultyNo);
                $('[id$=txtFatherName]').val(data.d[i].FatherName);
                $('[id$=txtGrade]').val(data.d[i].Grade);
                $('[id$=txtHOD]').val(data.d[i].HOD);
                $('[id$=txtHODName]').val(data.d[i].HODName);
                $('[id$=txtMobileNo]').val(data.d[i].MobileNo);
                $('[id$=txtMotherName]').val(data.d[i].MotherName);
                $('[id$=txtName]').val(data.d[i].Name);
                $('[id$=txtPFNo]').val(data.d[i].PFNo);
                $('[id$=txtPhoneNo]').val(data.d[i].PhoneNo);
                $('[id$=txtReligion]').val(data.d[i].Religion);
                $('[id$=txtReportingInchargeName]').val(data.d[i].ReportingInchargeName);
                $('[id$=txtSearchName]').val(data.d[i].SearchName);
                $('[id$=txtState]').val(data.d[i].State);
                $('[id$=txtTitle]').val(data.d[i].Title);
                $('[id$=txtVoterID]').val(data.d[i].VoterID);
                $('[id$=txtUANNo]').val(data.d[i].UANNo);
                $('[id$=txtGoogleSiteLink]').val(data.d[i].GoogleSiteLink);
                $('[id$=txtHOD1]').val(data.d[i].HOD1);
                $('[id$=txtHOD1Name]').val(data.d[i].HOD1Name);
                if (data.d[i].Gender == 0) {
                    $('[id$=txtGender]').val('');
                }
                else if (data.d[i].Gender == 1) {
                    $('[id$=txtGender]').val('Female');
                }
                else if (data.d[i].Gender == 2) {
                    $('[id$=txtGender]').val('Male');
                }
                data.d[i].EmployeeStatus == 0 ? $('[id$=txtEmployeeStatus]').val('Active') : $('[id$=txtEmployeeStatus]').val('Inactive');
                if (data.d[i].MaritalStatus == 0) {
                    $('[id$=txtMaritalStatus]').val('Single');
                }
                else if (data.d[i].MaritalStatus == 1) {
                    $('[id$=txtMaritalStatus]').val('Married');
                }
                else if (data.d[i].MaritalStatus == 2) {
                    $('[id$=txtMaritalStatus]').val('Divorced');
                }
                else if (data.d[i].MaritalStatus == 3) {
                    $('[id$=txtMaritalStatus]').val('Widowed');
                }
            }
        }
        function Contact() {
            $('[id$=divContactBody]').slideToggle();
        }
        function Administration() {
            $('[id$=divAdministrationBody]').slideToggle();
        }
        function PersonalInformation() {
            $('[id$=divPersonalInformationBody]').slideToggle();
        }
        function General() {
            $('[id$=divGeneralBody]').slideToggle();
        }
    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>

    <asp:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
        PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground" CancelControlID="btnHide">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none;">
        <div class="header">
            <b>
                <asp:Label ID="lblNotification" runat="server" Text="Notification"></asp:Label></b><div class="close">
                    <asp:Button ID="btnHide" runat="server" Text="X" />
                    <asp:HiddenField ID="hfprofilereq" runat="server" />
                </div>
        </div>
        <div id="main" runat="server" visible="false">

            <div class="body">
                <div style="width: 100%; overflow: scroll">
                    <asp:GridView ID="grdInbox" runat="server" AutoGenerateColumns="False" BackColor="White" BorderStyle="None" Width="100%" GridLines="Horizontal" EmptyDataText="Welcome" OnRowCommand="grdInbox_RowCommand">
                        <%--There are no data records to display.--%>
                        <AlternatingRowStyle BackColor="#F7F7F7" />
                        <Columns>
                            <asp:BoundField DataField="Dash" />
                            <asp:BoundField DataField="Dash" />
                            <asp:BoundField DataField="Dash" />
                            <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Right" ItemStyle-Width="100px">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>

                            <asp:BoundField HeaderText="Inbox" ItemStyle-HorizontalAlign="Left" DataField="InboxView" />
                            <asp:BoundField DataField="Dash" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btncount" runat="server" CommandName='<%# Eval("InboxView")%>' CommandArgument='<%# Eval("InboxCount")%>' Text='<%# Eval("InboxCount")%>'></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    Count
                                </HeaderTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>


                </div>


            </div>
        </div>
        <div id="Social" runat="server" visible="false">



            <div class="modal-dialog modalPopup">



                <div style="margin-bottom: -5px; margin-right: 3px">
                    <button type="button" class="close" data-dismiss="modal" onclick="closePopup()" style="color: black; opacity: initial">X</button>
                </div>
                <div style="width: 100%">



                    <table style="width: 100%">
                        <tr>
                            <td style="height: 5px"></td>
                        </tr>
                        <tr style="font-weight: 100; font-weight: bold;">

                            <td style="width: 100%; text-align: left;" colspan="3">

                                <label style="font: bold; font-size: 40px; font-family: Georgia, serif;">
                                    <span style="display: inline-block; margin: 8px 0; font: bold; padding-left: 48px; opacity: 2;">Stay Connected!

                                                     
                                    </span>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 5px"></td>
                        </tr>

                        <tr style="font-weight: 100; font-weight: bold;">
                            <td style="width: 100%; text-align: center; opacity: .8; text-align: left" colspan="3">

                                <label style="font-size: larger; font: 25px/1 Verdana,Freesans,sans-serif;">
                                    <span style="display: inline-block; margin: 8px 0; margin-left: 50px">Follow TMU on all our social channels to get the latest updates,</span><br />
                                </label>

                            </td>
                        </tr>
                        <tr style="font-weight: 100; font-weight: bold; padding-top: 40px">
                            <td style="width: 100%; text-align: center; opacity: .8; text-align: left" colspan="3">

                                <label style="font-size: larger; font: 25px/1 Verdana,Freesans,sans-serif;">
                                    <span style="display: inline-block; margin: 8px 0; margin-left: 50px; margin-top: -3px">announcements, and exciting content!</span><br />
                                </label>

                            </td>
                        </tr>


                        <tr>
                            <td style="height: 5px"></td>
                        </tr>

                        <tr style="width: 100%; font-weight: 100; font-weight: bold;">
                            <td style="width: 33%; text-align: right"><a href="https://www.facebook.com/tmumbd/" target="_blank">
                                <asp:Image ID="Image5" runat="server" ImageUrl="~/images/facebook.png" Width="200px" Height="250px" />
                            </a>
                            </td>
                            <td style="width: 33%; text-align: center">
                                <a href="https://bit.ly/4ddvTyK" target="_blank">
                                    <asp:Image ID="Image7" runat="server" ImageUrl="~/images/insta_logo1.png" Width="205px" Height="260px" />


                                </a>
                            </td>
                            <td style="width: 33%; text-align: left"><a href="https://bit.ly/4b5DdL9" target="_blank">
                                <asp:Image ID="Image11" runat="server" ImageUrl="~/images/whatsapp.png" Width="200px" Height="245px" />


                            </a>
                            </td>
                        </tr>
                        <tr style="width: 100%">
                            <td style="width: 33%; text-align: right">
                                <a href="https://bit.ly/4d3FOaa" target="_blank">
                                    <asp:Image ID="Image8" runat="server" ImageUrl="~/images/yt_logo 1.png" Width="200px" Height="250px" />
                                </a>
                            </td>
                            <td style="width: 33%; text-align: center">
                                <a href="https://bit.ly/3WdTNEl" target="_blank">
                                    <asp:Image ID="lnkYout" runat="server" ImageUrl="~/images/insta_logo2.png" Width="200px" Height="250px" />
                                </a>
                            </td>
                            <td style="width: 33%; text-align: left;">
                                <a href="https://bit.ly/3UfA8kM" target="_blank">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/Linkdn.png" Width="200px" Height="250px" />
                                </a>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 5px"></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </asp:Panel>


    <!-- Dummy Link (hidden) -->
    <asp:LinkButton ID="lnkDummy2" runat="server" Style="display: none;"></asp:LinkButton>

    <!-- Modal Extender -->
    <asp:ModalPopupExtender ID="ModalPopupExtender2" BehaviorID="mpe2" runat="server" PopupControlID="pnlPopup2" TargetControlID="lnkDummy2" BackgroundCssClass="modalBackground" CancelControlID="btnHide2">
    </asp:ModalPopupExtender>

    <!-- Modal Panel -->
    <asp:Panel ID="pnlPopup2" runat="server" CssClass="modalPopup" Style="display: none; width: 700px;">
        <div class="header">
            <b>
                <asp:Label ID="lblNotification2" runat="server" Text="Profile Detail"></asp:Label></b>
            <div class="close">
                <asp:Button ID="btnHide2" runat="server" Text="X" Style="padding: 0px;" OnClientClick="closePopup2(); return false;" />
            </div>
        </div>

        <div class="body">
            <div class="section">
                
                <div class="field">
                    <label>Name</label>
                    <asp:TextBox ID="txtName11" runat="server"></asp:TextBox>
                </div>
                <div class="field">
                    <label>Gender</label>
                    <asp:DropDownList ID="ddlGender11" runat="server">
                        <asp:ListItem Value="" Text="Select Gender" />
                        <asp:ListItem Value="2" Text="Male" />
                        <asp:ListItem Value="1" Text="Female" />
                        <asp:ListItem Value="3" Text="Other" />
                    </asp:DropDownList>
                </div>
               
            </div>

            <div class="section">
                <div class="field">
                    <label>Date of Birth</label>
                    <asp:TextBox ID="txtBirthName11" runat="server" type="date"></asp:TextBox>
                </div>

               
                <div class="field">
                    <label>Designation Name</label>
                    <asp:DropDownList ID="ddlDesignationName11" runat="server" Width="120px" />
                </div>


                <div class="field">
                    <label>Father Name</label>
                    <asp:TextBox ID="txtFatherName11" runat="server"></asp:TextBox>
                </div>
                <div class="field">
                    <label>Aadhar Card</label>
                    <asp:TextBox ID="txtAdharCard11" runat="server"></asp:TextBox>
                </div>
                <div class="field">
                    <label>PAN Card No</label>
                    <asp:TextBox ID="txtPANCardNo11" runat="server"></asp:TextBox>
                </div>
               
             
            </div>

            <div class="section">
                <div class="field">
                    <label>Mobile No</label>
                    <asp:TextBox ID="txtMobileNo11" runat="server"></asp:TextBox>
                </div>
                <div class="field">
                    <label>Email</label>
                    <asp:TextBox ID="txtEmail11" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="section">
               
                <div class="field">
                    <label>File Upload</label>
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="220px" accept=".pdf" />
                </div>
            </div>
            <asp:Button ID="btnSubmit11" runat="server" Text="Submit" CssClass="submit-btn" OnClick="btnSave_Click" OnClientClick="return validateForm();" Style="margin-bottom: 15px;" />
        </div>

    </asp:Panel>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <fieldset class="boxBody">
        <div class="col-md-6">
            <asp:Label ID="Label1" runat="server"
                Text="Profile" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

            <button type="button" onclick="showPopup2(); return false;"  class="btn btn-info" style="float:right;margin-left:20px">Profile Change Request</button> 
           <a href="TDSdetail.aspx" class="btn btn-info" style="float:right;color:white">TDS Change Request</a>
        </div>
    </fieldset>

    <fieldset class="boxBodyHeader">
    </fieldset>
    <fieldset class="boxBodyInner">

        <fieldset class="boxBodyInner">
            <table cellpadding="0px" cellspacing="0px">
                <tr>
                    <td>
                        <asp:CheckBox runat="server" ID="chkboxAsPrincipal" Onclick="return HideShow(this);" Text="As Principal" Visible="false" />
                    </td>
                    <td>
                        <div id="Principal" class="pull-right">
                            <b style="font-weight: bold" id="Principal1" runat="server">Course:</b>&nbsp;
                                <asp:DropDownList ID="drpCourseCode1" onchange="javascript:bindfacultyCode();" Width="200px" runat="server"></asp:DropDownList>
                            <b style="font-weight: bold" id="Principal2" runat="server">Faculty:</b>&nbsp;
                                 <asp:DropDownList ID="drpFacultyCode" Width="200px" runat="server" onchange="javascript:bindData();"></asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="15" style="height: 10px">
                        <div onclick="General()">
                            <fieldset class="boxBodyHeader">
                                <asp:Label ID="Label3" runat="server"
                                    Text="General" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                            </fieldset>
                        </div>
                        <br />
                        <div id="divGeneralBody">
                            <fieldset class="boxBodyInner">

                                <table cellpadding="0px" cellspacing="0px">
                                    <tr>
                                        <td colspan="15">
                                            <table cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <td>
                                                        <label style="line-height: 25px">Faculty No. </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtFacultyNo" runat="server" Width="220px" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label style="line-height: 25px">Card No </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtCardNo" runat="server" Enabled="False" Width="220px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label style="line-height: 25px">Title</label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtTitle" runat="server" Enabled="False" Width="220px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="11" style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label style="line-height: 25px">Name</label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtName" runat="server" Enabled="False" Width="220px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label style="line-height: 25px">Search Name </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtSearchName" runat="server" Enabled="False" Width="220px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label style="line-height: 25px">Gender </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtGender" runat="server" Enabled="False" Width="220px"></asp:TextBox>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td colspan="11" style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label style="line-height: 25px">Branch Code</label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtBranchCode" runat="server" Enabled="False" Width="220px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label style="line-height: 25px">College Code </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtCollegeCode" runat="server" Enabled="False" Width="220px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label style="line-height: 25px">Department Code </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtDepartmantCode" runat="server" Enabled="False" Width="220px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="11" style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label style="line-height: 25px">Emp. Type </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtEmployeeType" runat="server" Enabled="False" Width="220px"></asp:TextBox></td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label style="line-height: 25px">Emp. Status </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtEmployeeStatus" runat="server" Enabled="False" Width="220px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="11" style="height: 10px"></td>
                                                </tr>

                                            </table>
                                        </td>
                                    </tr>
                                </table>

                            </fieldset>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="15" style="height: 10px">
                        <div onclick="PersonalInformation()">
                            <fieldset class="boxBodyHeader">
                                <asp:Label ID="Label4" runat="server" Text="Personal Information" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                            </fieldset>
                        </div>
                        <div id="divPersonalInformationBody">
                            <fieldset class="boxBodyInner">
                                <table cellpadding="0px" cellspacing="0px">

                                    <tr>
                                        <td colspan="15">
                                            <table cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <td>
                                                        <label style="line-height: 25px">Date Of Birth </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtBirthName" runat="server" Enabled="false" Width="220px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label style="line-height: 25px">Father Name </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtFatherName" runat="server" Enabled="false" Width="220px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label style="line-height: 25px">Mother Name</label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMotherName" runat="server" Enabled="false" Width="220px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="11" style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label style="line-height: 25px">Marital Status&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMaritalStatus" Enabled="false" runat="server" Width="220px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label style="line-height: 25px">Religion </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtReligion" runat="server" Enabled="false" Width="220px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label style="line-height: 25px">Blood Group </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtBloodGroup" runat="server" Enabled="false" Width="220px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="11" style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label style="line-height: 25px">Voter ID</label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtVoterID" runat="server" Enabled="false" Width="220px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label style="line-height: 25px">Aadhar Card </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtAdharCard" runat="server" Enabled="false" Width="220px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label style="line-height: 25px">UAN No. </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtUANNo" runat="server" Enabled="false" Width="220px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="11" style="height: 10px"></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="15" style="height: 10px">
                        <div id="divContact" onclick="Contact()">
                            <fieldset class="boxBodyHeader">
                                <asp:Label ID="Label2" runat="server" Text="Contact Information" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                            </fieldset>
                        </div>
                        <div id="divContactBody">
                            <fieldset class="boxBodyInner">
                                <table cellpadding="0px" cellspacing="0px">

                                    <tr>
                                        <td colspan="15">
                                            <table cellpadding="0px" cellspacing="0px">
                                                <br />
                                                <tr>
                                                    <td>
                                                        <label style="line-height: 25px">Mobile No. </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMobileNo" runat="server" Width="220px" MaxLength="13"></asp:TextBox>
                                                    </td>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtMobileNo" FilterType="Numbers, Custom"></asp:FilteredTextBoxExtender>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label style="line-height: 25px">Phone No </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtPhoneNo" runat="server" MaxLength="13" Width="220px"></asp:TextBox>
                                                    </td>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtPhoneNo" FilterType="Numbers, Custom"></asp:FilteredTextBoxExtender>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label style="line-height: 25px">Extension</label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtExtension" runat="server" MaxLength="13" Width="220px"></asp:TextBox>
                                                    </td>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtExtension" FilterType="Numbers, Custom"></asp:FilteredTextBoxExtender>
                                                </tr>
                                                <tr>
                                                    <td colspan="11" style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label style="line-height: 25px">Emgy. Cont. Person </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtEmergencyContactPer" runat="server" MaxLength="20" Width="220px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label style="line-height: 25px">State </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtState" runat="server" Width="220px" MaxLength="20"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label style="line-height: 25px">E-Mail</label></td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtEmail" runat="server" Width="220px"></asp:TextBox>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                    <td>
                                                        <asp:RegularExpressionValidator ID="regEmail" Display="Dynamic" ValidationGroup="g1" ControlToValidate="txtEmail" Text="Please fill the valid Email address!" ForeColor="Red"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtEmergencyPhoneNo" FilterType="Numbers, Custom"></asp:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="11" style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label style="line-height: 25px">Emgy. Phone No. </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtEmergencyPhoneNo" runat="server" Width="220px" MaxLength="13"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label style="line-height: 25px">Google Site Link</label></td>
                                                    <td style="width: 10px"></td>
                                                    <td colspan="5">
                                                        <asp:TextBox runat="server" ID="txtGoogleSiteLink" MaxLength="70"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="11" style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label style="line-height: 25px">Address</label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td colspan="9">
                                                        <asp:TextBox ID="txtAddress" runat="server" MaxLength="80" Width="100%" onkeypress="myFunction()" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="11" style="height: 20px"></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>

                            </fieldset>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="15" style="height: 10px">
                        <div onclick="Administration()">
                            <fieldset class="boxBodyHeader">
                                <asp:Label ID="Label5" runat="server"
                                    Text="Administration" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                            </fieldset>
                        </div>
                        <div id="divAdministrationBody">
                            <fieldset class="boxBodyInner">
                                <table cellpadding="0px" cellspacing="0px">
                                    <tr>
                                        <td colspan="15">
                                            <table cellpadding="0px" cellspacing="0px">
                                                <br />
                                                <tr>
                                                    <td>
                                                        <label style="line-height: 25px">Desig. Code </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtDesignationCode" runat="server" Width="220px" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label style="line-height: 25px">Employment Date </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtEmployementDate" runat="server" Enabled="False" Width="220px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label style="line-height: 25px">Grade </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtGrade" runat="server" Enabled="False" Width="220px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="11" style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label style="line-height: 25px">HOD/Principal </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtHOD" runat="server" Enabled="False" Width="220px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label style="line-height: 25px">HOD/Principal Name </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtHODName" runat="server" Enabled="False" Width="220px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label style="line-height: 25px">Rept. Incharge Name</label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtReportingInchargeName" runat="server" Enabled="False" Width="220px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="11" style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label style="line-height: 25px">HOD1 </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtHod1" runat="server" Enabled="False" Width="220px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label style="line-height: 25px">HOD1 Name </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtHod1Name" runat="server" Enabled="False" Width="220px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label style="line-height: 25px">PF No.</label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td style="width: 10px">
                                                        <asp:TextBox ID="txtPFNo" runat="server" Enabled="False" Width="220px"></asp:TextBox>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td colspan="11" style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                    <td style="width: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="11" style="height: 20px"></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                    </td>
                </tr>
            </table>

        </fieldset>
        <div class="pull-right">
            <asp:Button ID="btnUpdate" runat="server" Visible="false" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px" Width="90px" OnClientClick="return false" Text="UPDATE" />
        </div>
    </fieldset>
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.js"></script>
</asp:Content>

