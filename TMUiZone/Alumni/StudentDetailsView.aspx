<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentDetailsView.aspx.cs" Inherits="Alumni_StudentDetailsView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <style>
        #confirmModal.modal-dialog.modalPopup {
            width: 80%;
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
            left: 50%;
            top: 50%;
            transform: translate(-50%,-50%) !important;
            margin: 0;
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
    <%--     <script>
         function alphanumeric(inputtxt) {
             var letterNumber = /^[0-9a-zA-Z-+()!@#$%^&*/|]+$/;
             if ((inputtxt.value.match(letterNumber))) {
                 alert("Enter only Hindi");
                 return false;
             }
             else {


                 return true;
             }
         }
    </script>--%>

    <%--<script type="text/javascript">
        $(document).ready(function () {
            $('[id$=txtAddress]').keypress(function (e) {
                if ($(this).val().length >= 50) {
                    e.preventDefault();
                }
            });
        });
    </script>--%>
    <script type="text/javascript">
        function callFeedbackMessage(inputType, inputText) {

            if (inputType == 'Error') {
                alertify.error(inputText);
                return false;
            }
            else if (inputType == 'Success') {
                //alertify.confirm().set('overflow', false);
                alertify.success("Update Successfully");
                return false;
            }
            else {
                alertify.log(inputText, "", 10000);
                return false;
            }
        }



        function Validate() {

            var drpCurrent = document.getElementById('<%= ddlEng.ClientID %>').value;

            var city = document.getElementById('<%= txtCity.ClientID %>').value;

            var country = document.getElementById('<%= txtCountry.ClientID %>').value;


            var Email = document.getElementById('<%= txtEmailID.ClientID %>').value;

             var Mobile = document.getElementById('<%= txtMobileNo.ClientID %>').value;

            var Address = document.getElementById('<%= txtAddress.ClientID %>').value;
            var PAddress = document.getElementById('<%= txtPAddress.ClientID %>').value;


             var linkedin_url = document.getElementById('<%= txtLinkin.ClientID %>').value;
             var Facebook_url = document.getElementById('<%= txtFacebook.ClientID %>').value;
             var Twitter_url = document.getElementById('<%= txtTwitter.ClientID %>').value;

             var reEmail = /^(?:[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+\.)*[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\-](?!\.)){0,61}[a-zA-Z0-9]?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\[(?:(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\.){3}(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\]))$/;

             if (!Email.match(reEmail)) {
                 alert("Invalid email address");
                 document.getElementById('<%= txtEmailID.ClientID %>').style.borderColor = "red";
                return false;
            }
            if (Mobile == "") {
                document.getElementById('<%= txtMobileNo.ClientID %>').style.borderColor = "red";
                return false;
            } 
            if (city == "") {
                document.getElementById('<%= txtCity.ClientID %>').style.borderColor = "red";
                 return false;
            }
            if (country == "") {
                document.getElementById('<%= txtCountry.ClientID %>').style.borderColor = "red";
                return false;
            }
            if (Address == "") {
                document.getElementById('<%= txtAddress.ClientID %>').style.borderColor = "red";
                return false;
            }
            if (PAddress == "") {
                document.getElementById('<%= txtPAddress.ClientID %>').style.borderColor = "red";
                 return false;
             }
            if (document.getElementById('<%= txtLinkin.ClientID %>').value != "") {
                 if (/(ftp|http|https):\/\/?(?:www\.)?linkedin.com(\w+:{0,1}\w*@)?(\S+)(:([0-9])+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/.test(linkedin_url)) {

                 } else {
                     alert("Please Enter Valid Url.");
                     document.getElementById('<%= txtLinkin.ClientID %>').style.borderColor = "red";
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

            if (drpCurrent == "1") {

                var Employer = document.getElementById('<%= txtEmployer.ClientID %>').value;

                 var Designation = document.getElementById('<%= txtDesig.ClientID %>').value;

                 var txtEAddress = document.getElementById('<%= txtEAddress.ClientID %>').value;

                 var txtIndustry = document.getElementById('<%= txtIndustry.ClientID %>').value;

                 var txtJDesc = document.getElementById('<%= txtJDesc.ClientID %>').value;

                 var txtSDate = document.getElementById('<%= txtSDate.ClientID %>').value;
                 if (Employer == "") {
                     document.getElementById('<%= txtEmployer.ClientID %>').style.borderColor = "red";
                    return false;
                }
                if (Designation == "") {
                    document.getElementById('<%= txtDesig.ClientID %>').style.borderColor = "red";
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
                var txtCollegeName = document.getElementById('<%= txtHSinstitute.ClientID %>').value;

                var txtEduAddress = document.getElementById('<%= txtHAdress.ClientID %>').value;

                var txtEWebUrl = document.getElementById('<%= txtEWebUrl.ClientID %>').value;

                var txtProgramName = document.getElementById('<%= txtHSProgram.ClientID %>').value;

                var txtEduAdmissionYear = document.getElementById('<%= txtEduAdmissionYear.ClientID %>').value;

                var txtExGradYear = document.getElementById('<%= txtExGradYear.ClientID %>').value;

                var txtFurtherPlan = document.getElementById('<%= txtFurtherPlan.ClientID %>').value;

                if (txtCollegeName == "") {
                    document.getElementById('<%= txtHSinstitute.ClientID %>').style.borderColor = "red";
                    return false;
                }
                if (txtEduAddress == "") {
                    document.getElementById('<%= txtHAdress.ClientID %>').style.borderColor = "red";
                    return false;
                }
                if (txtEWebUrl == "") {
                    document.getElementById('<%= txtEWebUrl.ClientID %>').style.borderColor = "red";
                    return false;
                }
                if (txtProgramName == "") {
                    document.getElementById('<%= txtHSProgram.ClientID %>').style.borderColor = "red";
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






            return true;
        }

    </script>

    <%--<script>
        function CloseModalConfirm() {
            $('#confirmModal').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        };
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>

    <asp:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
        PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground" CancelControlID="btnHide">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
        <div class="header">
            <b>
                <asp:Label ID="lblNotification" runat="server" Text="Notification"></asp:Label></b><div class="close">
                    <asp:Button ID="btnHide" runat="server" Text="X" />
                </div>
        </div>
        <div class="body">
            <div style="width: 100%">


                <asp:GridView ID="grdAluminiInbox" runat="server" AutoGenerateColumns="False" BackColor="White" BorderStyle="None" BorderWidth="1px" CellPadding="20" Width="100%" GridLines="Horizontal" EmptyDataText="Welcome" OnRowCommand="grdAluminiInbox_RowCommand">
                    <%--There are no data records to display.--%>
                    <AlternatingRowStyle BackColor="#F7F7F7" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl. No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle />
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="Inbox" ItemStyle-HorizontalAlign="Left" DataField="InboxView" />
                        <asp:BoundField DataField="Dash" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btncount" runat="server" CommandName='<%# Eval("InboxView")%>'
                                    CommandArgument='<%# Eval("InboxCount")%>' Text='<%# Eval("InboxCount")%>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderTemplate>
                                Count
                            </HeaderTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>


            </div>


        </div>
    </asp:Panel>


    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>




    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Profile" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>

    <fieldset class="boxBodyHeader">
    </fieldset>
    <fieldset class="boxBodyInner">
        <table cellpadding="0px" cellspacing="0px">
            <tr>
                <td colspan="15" style="height: 10px">
                    <fieldset class="boxBodyHeader">
                        <asp:Label ID="Label3" runat="server"
                            Text="Personal Information" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                    </fieldset>
                    <br />
                    <fieldset class="boxBodyInner">
                        <table cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td>
                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <label>Student Name</label></td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtFName" runat="server" Enabled="False" Width="260px"></asp:TextBox>

                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <label>Enrollment No </label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>

                                                <asp:TextBox ID="txtRollNo" runat="server" Width="260px" Enabled="false"></asp:TextBox>

                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <label>Date of Birth </label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtDOB" runat="server" Enabled="False" Width="260px"></asp:TextBox></td>

                                        </tr>
                                        <tr>
                                            <td colspan="11" style="height: 10px"></td>
                                        </tr>













                                        <tr>
                                            <td>
                                                <label>Programme </label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtCourse" runat="server" Enabled="False" Width="260px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <label>College </label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="TextCollege" runat="server" Enabled="False" Width="260px"></asp:TextBox></td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <label style="width: 100px; float: right;">Year of Passout</label></td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtMentor" runat="server" Width="260px" Enabled="false"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td colspan="11" style="height: 10px"></td>
                                        </tr>

                                        <tr>

                                            <td>
                                                <label>Gender </label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:DropDownList ID="drpGender" runat="server" Height="30px" Enabled="False" Width="260px">
                                                    <asp:ListItem Text="Male" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Female" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Other" Value=""></asp:ListItem>

                                                </asp:DropDownList></td>




                                        </tr>

                                        <tr>
                                            <td colspan="11" style="height: 10px"></td>
                                        </tr>

                                        <tr>
                                            <td colspan="11" style="height: 10px"></td>
                                        </tr>







                                    </table>
                                </td>
                            </tr>
                        </table>

                    </fieldset>
                </td>
            </tr>

            <tr>
                <td colspan="15" style="height: 10px">
                    <fieldset class="boxBodyHeader">
                        <asp:Label ID="Label4" runat="server"
                            Text="Contact Information" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                    </fieldset>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>

                            <fieldset class="boxBodyInner">
                                <table cellpadding="0px" cellspacing="0px">

                                    <tr>
                                        <td>

                                            <table cellpadding="0px" cellspacing="0px">

                                                <tr>
                                                    <td>
                                                        <label>E-Mail ID </label>
                                                    </td>

                                                    <td style="padding-left: 20px">
                                                        <asp:TextBox ID="txtEmailID" runat="server" Width="200px"></asp:TextBox>
                                                    </td>

                                                    <td style="padding-left: 20px">
                                                        <label>Mobile No</label></td>

                                                    <td style="padding-left: 20px">
                                                        <asp:TextBox ID="txtMobileNo" runat="server" Width="200px" MaxLength="10"></asp:TextBox></td>


                                                    <td style="padding-left: 20px">
                                                        <label>WhatsApp No</label></td>

                                                    <td style="padding-left: 20px">
                                                        <asp:TextBox ID="txtWhatsNo" runat="server" Width="200px" MaxLength="10"></asp:TextBox></td>


                                                </tr>
                                                <tr>
                                                    <td colspan="9" style="height: 10px;"></td>
                                                </tr>
                                                <tr>
                                                    
                                                    <td>
                                                        <label>City </label>
                                                    </td>

                                                    <td style="padding-left: 20px">
                                                        <asp:TextBox ID="txtCity" runat="server" Width="200px" MaxLength="20">
                                                            
                                                        </asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars"
                                                            ValidChars="zxcvbnmlkjhgfdsaqwertyuiopQWERTYUIOPLKJHGFDSAZXCVBNM"
                                                            TargetControlID="txtCity">
                                                        </asp:FilteredTextBoxExtender>
                                                    </td>


                                                    <td style="padding-left: 20px">
                                                        <label>Country</label>
                                                    </td>

                                                    <td style="padding-left: 20px">
                                                        <asp:TextBox ID="txtCountry" runat="server" Width="200px"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterMode="ValidChars"
                                                            ValidChars="zxcvbnmlkjhgfdsaqwertyuiopQWERTYUIOPLKJHGFDSAZXCVBNM"
                                                            TargetControlID="txtCountry">
                                                        </asp:FilteredTextBoxExtender>
                                                        <asp:RegularExpressionValidator ID="regEmail" Display="Dynamic" ControlToValidate="txtEmailID" Text="Please fill the valid Email address!" ForeColor="Red" ValidationGroup="g1"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server" />
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtMobileNo" FilterType="Numbers, Custom"></asp:FilteredTextBoxExtender>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtWhatsNo" FilterType="Numbers, Custom"></asp:FilteredTextBoxExtender>
                                                         <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtEduAdmissionYear" FilterType="Numbers, Custom"></asp:FilteredTextBoxExtender>
                                                         <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtExGradYear" FilterType="Numbers, Custom"></asp:FilteredTextBoxExtender>
                                                    
                                                     <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtCompanyTelephone" FilterType="Numbers, Custom"></asp:FilteredTextBoxExtender>
                                                    
                                                    
                                                    
                                                    </td>
                                                    <td style="padding-left: 20px">
                                                        <label>Present Address</label></td>

                                                    <td style="padding-left: 20px">
                                                        <asp:TextBox ID="txtAddress" runat="server" Width="200px" ></asp:TextBox></td>
                                                </tr>

                                                <tr>
                                                    <td colspan="9" style="height: 10px;"></td>
                                                </tr>


                                                <%--<tr> <td colspan="11" align="right" >  
                      <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btnLogin" />
                      <asp:Label ID="lblHRUserId" runat="server" Visible="False"></asp:Label>
                      </td></tr>--%>
                                        </td>
                                    </tr>


                                    <tr>




                                        <td>
                                            <label>Peramnent Address</label></td>

                                        <td style="padding-left: 20px">
                                            <asp:TextBox ID="txtPAddress" runat="server" Width="200px" MaxLength="200"></asp:TextBox></td>
                                        <td style="padding-left: 20px">
                                            <label>LinkedIN Url</label></td>

                                        <td style="padding-left: 20px">
                                            <asp:TextBox ID="txtLinkin" runat="server" Width="200px" MaxLength="50"></asp:TextBox></td>
                                        <td style="padding-left: 20px">
                                            <label>Facebook Url</label></td>

                                        <td style="padding-left: 20px">
                                            <asp:TextBox ID="txtFacebook" runat="server" Width="200px" MaxLength="50"></asp:TextBox></td>

                                    </tr>




                                    <tr>
                                        <td colspan="11" style="height: 10px"></td>
                                    </tr>
                                    <tr>




                                        <td>
                                            <label>Twitter Handler</label></td>

                                        <td style="padding-left: 20px">
                                            <asp:TextBox ID="txtTwitter" runat="server" Width="200px" ></asp:TextBox></td>

                                        <td style="padding-left: 20px;">
                                            <label>Engagement</label>
                                        </td>

                                        <td style="padding-left: 20px;">
                                            <asp:DropDownList ID="ddlEng" runat="server" Width="200px" Height="30px" OnSelectedIndexChanged="ddlEng_SelectedIndexChanged" AutoPostBack="true">

                                                <asp:ListItem Text="Employed" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Self Employed/Start Up" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Further Education" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Unemployed" Value="4"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>






                                    </tr>
                                    <tr>
                                        <td colspan="11" style="height: 10px"></td>
                                    </tr>
                                </table>
                                <table cellpadding="0px" cellspacing="0px">

                                    <asp:Panel ID="pnl1" runat="server">
                                        <tr>
                                            <td>

                                                <asp:Label ID="lblDesi" runat="server" >Designation</asp:Label>
                                            </td>

                                            <td style="padding-left: 14px;">
                                                <asp:TextBox ID="txtDesig" runat="server"  Width="200px"></asp:TextBox></td>

                                            <td style="padding-left: 20px">
                                                <asp:Label ID="lblEmployer"  runat="server">Employer</asp:Label>

                                            </td>

                                            <td style="padding-left: 4px;">
                                                <asp:TextBox ID="txtEmployer"  runat="server" Width="200px"></asp:TextBox>
                                            </td>
                                            <td style="padding-left: 20px">
                                                <asp:Label ID="lblAdress" runat="server">Address</asp:Label>

                                            </td>

                                            <td style="padding-left: 24px;">
                                                <asp:TextBox ID="txtEAddress" runat="server" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="11" style="height: 10px"></td>
                                        </tr>
                                        <tr>




                                            <td>

                                                <asp:Label ID="lblInd" runat="server">Industry</asp:Label>
                                            </td>

                                            <td style="padding-left: 12px;">
                                                <asp:TextBox ID="txtIndustry" runat="server" Width="200px"></asp:TextBox></td>

                                            <td style="padding-left: 20px;">
                                                <asp:Label ID="lblJD" runat="server">Job Description</asp:Label>

                                            </td>

                                            <td style="padding-left: 4px;">
                                                <asp:TextBox ID="txtJDesc" runat="server" Width="200px"></asp:TextBox>
                                            </td>
                                            <td style="padding-left: 20px">
                                                <asp:Label ID="lblHJS" runat="server">Job Status</asp:Label>

                                            </td>

                                            <td style="padding-left: 24px;">
                                                <asp:DropDownList ID="ddlJDesc" runat="server" Height="30px" Width="200px">
                                                    <asp:ListItem Text="Full Time" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Part Time" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Intership" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>


                                        <tr>
                                            <td colspan="11" style="height: 10px"></td>
                                        </tr>
                                        <tr>




                                            <td>

                                                <asp:Label ID="lblD" runat="server"> Start Date</asp:Label>
                                            </td>

                                            <td style="padding-left: 12px;">
                                                <asp:TextBox ID="txtSDate" runat="server" Width="200px" onkeydown="return false;"></asp:TextBox></td>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSDate" Format="dd MMM yyyy">
                                            </asp:CalendarExtender>
                                            <td style="padding-left: 20px;">
                                                <asp:Label ID="lblCW" runat="server"> Company URL</asp:Label>

                                            </td>

                                            <td style="padding-left: 4px;">
                                                <asp:TextBox ID="txtCompanyURL" runat="server" Width="200px"></asp:TextBox>
                                            </td>
                                            <td style="padding-left: 20px">
                                                <asp:Label ID="lblCE" runat="server">Company Email</asp:Label>

                                            </td>

                                            <td style="padding-left: 24px;">
                                                <asp:TextBox ID="txtCMail" runat="server" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="11" style="height: 10px"></td>
                                        </tr>
                                        <tr>




                                            <td>

                                                <asp:Label ID="lblTE" runat="server"> Company Telephone</asp:Label>
                                            </td>

                                            <td style="padding-left: 12px;">
                                                <asp:TextBox ID="txtCompanyTelephone" runat="server" Width="200px"></asp:TextBox></td>




                                        </tr>


                                    </asp:Panel>


                                    <asp:Panel ID="pnl2" runat="server">


                                          <tr>
                                            <td>

                                                <asp:Label ID="lblEntr" runat="server" >Enterprise Name</asp:Label>
                                            </td>

                                            <td style="padding-left: 37px;">
                                                <asp:TextBox ID="txtEnterName" runat="server"  Width="200px"></asp:TextBox></td>

                                            <td style="padding-left: 20px">
                                                <asp:Label ID="lblIndus"  runat="server">Industry</asp:Label>

                                            </td>

                                            <td style="padding-left: 18px;">
                                                <asp:TextBox ID="txtSelfIndustry"  runat="server" Width="200px"></asp:TextBox>
                                            </td>
                                            <td style="padding-left: 20px">
                                                <asp:Label ID="lblAdress1" runat="server">Address</asp:Label>

                                            </td>

                                            <td style="padding-left: 70px;">
                                                <asp:TextBox ID="txtSelfAddress" runat="server" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="11" style="height: 10px"></td>
                                        </tr>

                                        <tr>
                                        <td>
                                            <asp:Label ID="lblBusinessType" runat="server" >Your Role</asp:Label>

                                        </td>

                                        <td style="padding-left:37px">
                                            <asp:TextBox ID="txtRole"  runat="server" Width="200px"></asp:TextBox>
                                        </td>

                                        <td style="padding-left: 20px">
                                            <asp:Label ID="lblCompanyName" runat="server" >Company Url</asp:Label>

                                        </td>

                                        <td style="padding-left:18px">
                                            <asp:TextBox ID="txtSelfCompanyURL" runat="server"  Width="200px"></asp:TextBox>
                                        </td>
                                            </tr>
                                    </asp:Panel>
                                   

                                                <tr>
                                                    <td colspan="9" style="height: 10px;"></td>
                                                </tr>

                                   
                                        <asp:Panel ID="pnl3" runat="server">


                                            <tr>
                                            <td>

                                                <asp:Label ID="lblHDesi" runat="server" >University Name</asp:Label>
                                            </td>

                                            <td style="padding-left: 26px;">
                                                <asp:TextBox ID="txtHSinstitute" runat="server"  Width="200px"></asp:TextBox></td>

                                            <td style="padding-left: 20px">
                                                <asp:Label ID="lblHaddress"  runat="server">Address</asp:Label>

                                            </td>

                                            <td style="padding-left: 4px;">
                                                <asp:TextBox ID="txtHAdress"  runat="server" Width="200px"></asp:TextBox>
                                            </td>
                                            <td style="padding-left: 20px">
                                                <asp:Label ID="lblHURL" runat="server">University URL</asp:Label>

                                            </td>

                                            <td style="padding-left: 25px;">
                                                <asp:TextBox ID="txtEWebUrl" runat="server" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="11" style="height: 10px"></td>
                                        </tr>
                                        <tr>




                                            <td>

                                                <asp:Label ID="lblHProgram" runat="server">Programme Name</asp:Label>
                                            </td>

                                            <td style="padding-left: 26px;">
                                                <asp:TextBox ID="txtHSProgram" runat="server" Width="200px"></asp:TextBox></td>

                                            <td style="padding-left: 20px;">
                                                <asp:Label ID="lblHadmission" runat="server">Admission Year</asp:Label>

                                            </td>

                                            <td style="padding-left: 4px;">
                                                <asp:TextBox ID="txtEduAdmissionYear" MaxLength="5"  runat="server" Width="200px"></asp:TextBox>
                                            </td>
                                            <td style="padding-left: 20px">
                                                <asp:Label ID="lblJS" runat="server">Exp.Pass.Yr</asp:Label>

                                            </td>

                                            <td style="padding-left: 24px;">
                                                <asp:TextBox ID="txtExGradYear" runat="server" MaxLength="5" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>


                                        <tr>
                                            <td colspan="11" style="height: 10px"></td>
                                        </tr>
                                        <tr>




                                            <td>

                                                <asp:Label ID="lblHF" runat="server"> Further Plan</asp:Label>
                                            </td>

                                            <td style="padding-left: 25px;">
                                                <asp:TextBox ID="txtFurtherPlan" runat="server" Width="200px"></asp:TextBox></td>
                                            
                                           
                                        </tr>
                                        <tr>
                                            <td colspan="11" style="height: 10px"></td>
                                        </tr>
                                         
                                        </asp:Panel>
                                      <asp:Panel ID="pnl4" runat="server">


                                            <tr>
                                            <td>

                                               
                                            </td>

                                            <td style="padding-left: 26px;">
                                              

                                            <td style="padding-left: 20px">
                                               

                                            </td>

                                            <td style="padding-left: 4px;">
                                               
                                            </td>
                                            <td style="padding-left: 20px">
                                               

                                            </td>

                                            <td style="padding-left: 25px;">
                                               
                                            </td>
                                        </tr>
                                          </asp:Panel>
                                   

                                    <tr>
                                        <td colspan="9" style="height: 10px;"></td>
                                    </tr>





                                </table>


                                </tr>
                                </table>
                                 

                                <div class="pull-right">
                                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px" Width="90px" OnClientClick="return Validate();" OnClick="btnUpdate_Click" Text="UPDATE" />
                                </div>

                            </fieldset>

                            <div id="confirmModalB" class="modal fade confirm-modal" role="dialog">

                                <div class="modal-dialog modalPopup border-box">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" name="btn_close" id="Button1" class="close" data-dismiss="modal">&times;</button>

                                        </div>
                                        <div class="clearfix">
                                            <div class="col-sm-12">
                                                <asp:Panel ID="PnlMain" runat="server">
                                                    <asp:Label ID="Lblpopup" runat="server" Text="Dear student kindly check your detail. If correct then update."></asp:Label>
                                                    <div class="modal-footer">
                                                        <asp:Button ID="BtnYes" runat="server" OnClick="BtnYes_Click" Text="Update"
                                                            UseSubmitBehavior="false" data-dismiss="modal" class="btn btn-success" />
                                                        <button type="button" class="btn btn-danger" data-dismiss="modal">Cancel</button>
                                                    </div>


                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>

