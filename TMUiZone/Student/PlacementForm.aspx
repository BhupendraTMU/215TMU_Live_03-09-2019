<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="PlacementForm.aspx.cs" Inherits="Student_PlacementForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">

        //function validation() {

        //    var mail = (document.getElementById("ContentPlaceHolder1_txtMail").value);

        //    var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
        //    if (mail.match(mailformat)) {

        //       // document.getElementById("ContentPlaceHolder1_txtMail").focus();
        //        return true;
        //    }
        //    else {
        //        alert("You have entered an invalid email address!");
        //        document.getElementById("ContentPlaceHolder1_txtMail").focus();
        //        return false;
        //    }


        //}
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        function preventInput(evnt) {

            if (evnt.which != 9) evnt.preventDefault();

        }
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="mnupd">
        <Triggers>

            <asp:PostBackTrigger ControlID="btnSubmit" />

        </Triggers>
        <ContentTemplate>
            <div style="text-align: right; width: 100%">
                <asp:Button ID="BtnPrint" OnClientClick="PrintDiv();" runat="server" Width="10%" Text="Print" Visible="false" Font-Bold="true" BorderColor="WhiteSmoke" />
            </div>

            <div id="printarea" style="border-style: none">
                <div style="text-align: center; width: 100%" id="logoDiv">
                    <table style="width: 80%; margin-left: 10%;">
                        <tr>
                            <td style="width: 200px;" align="left">
                                <img src="~/images/rightlogo.png" id="Image1" runat="server" width="100" height="102" visible="true" />
                            </td>
                            <td style="width: 80%; vertical-align: middle" align="left">
                                <asp:Label ID="LblTitle" runat="server" Text="Teerthanker Mahaveer University,Moradabad" Style="font-size: 25px;"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="panel-heading" style="background-color: #2b5b69">
                    <center>
                        <div class="panel-title" style="fit-position: center;">
                            <b>
                                <p style="color: white; font-size: 22px">
                                    Student Placement Form  &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                </p>
                            </b>
                        </div>
                    </center>
                </div>
                <fieldset class="boxBodyInner">

                    <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                        <div class="form-group">
                            <asp:Label ID="Label1" Style="line-height: 30px" runat="server" Text="Label"><b>Name</b></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4" style="padding-right: 0px;">
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:TextBox ID="txtName" CssClass="form-control input-sm" placeholder="Student Name" Width="300px" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtName" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                        <div class="form-group">
                            <asp:Label ID="Label2" Style="line-height: 30px" runat="server" Text="Label"><b>Permanent Address</b></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4" style="padding-right: 0px;">
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:TextBox ID="txtPAddress" CssClass="form-control input-sm" placeholder="Address" runat="server" Width="300px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPAddress" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                        <div class="form-group">
                            <asp:Label ID="Label3" Style="line-height: 30px" runat="server" Text="Label"><b>State</b></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4" style="padding-right: 0px;">
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:TextBox ID="txtState" CssClass="form-control input-sm" placeholder="State" runat="server" Width="300px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtState" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                        <div class="form-group">
                            <asp:Label ID="Label4" Style="line-height: 30px" runat="server" Text="Label"><b>Current Address</b></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4" style="padding-right: 0px;">
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:TextBox ID="CAddress" CssClass="form-control input-sm" placeholder="Address" runat="server" Width="300px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="CAddress" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                        <div class="form-group">
                            <asp:Label ID="Label5" Style="line-height: 30px" runat="server" Text="Label"><b>E-Mail</b></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4" style="padding-right: 0px;">
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:TextBox ID="txtMail" CssClass="form-control input-sm" placeholder="Mail" runat="server" Width="300px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMail" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>



                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                        <div class="form-group">
                            <asp:Label ID="Label6" Style="line-height: 30px" runat="server" Text="Label"><b>Mobile</b></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4" style="padding-right: 0px;">
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:TextBox ID="txtMobile" CssClass="form-control input-sm" placeholder="Mobile" onkeypress="return isNumber(event)" MaxLength="10" runat="server" Width="300px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMobile" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                        <div class="form-group">
                            <asp:Label ID="Label7" Style="line-height: 30px" runat="server" Text="Label"><b>Date Of Birth</b></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4" style="padding-right: 0px;">
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:TextBox ID="txtDOB" CssClass="form-control input-sm" MaxLength="12" onkeydown="javascript:preventInput(event);" placeholder="Date of birth" runat="server" Width="300px"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDOB" Format="dd MMM yyyy">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDOB" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                        <div class="form-group">
                            <asp:Label ID="Label8" Style="line-height: 30px" runat="server" Text="Label"><b>Gender</b></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4" style="padding-right: 0px;">
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:DropDownList ID="drpGender" CssClass="form-control input-sm" placeholder="Gender" runat="server" Width="300px">

                                    <asp:ListItem Text="Male" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Female" Value="2"></asp:ListItem>



                                </asp:DropDownList>

                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                        <div class="form-group">
                            <asp:Label ID="Label9" Style="line-height: 30px" runat="server" Text="Label"><b>Category</b></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4" style="padding-right: 0px;">
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:DropDownList ID="drpCategory" CssClass="form-control input-sm" runat="server" Width="300px">
                                    <asp:ListItem Text="GEN" Value="GEN"></asp:ListItem>
                                    <asp:ListItem Text="SC" Value="SC"></asp:ListItem>
                                    <asp:ListItem Text="ST" Value="ST"></asp:ListItem>
                                    <asp:ListItem Text="OBC" Value="OBC"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                        <div class="form-group">
                            <asp:Label ID="Label62" Style="line-height: 30px" runat="server" Text="Label"><b>Alternate Mobile</b></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4" style="padding-right: 0px;">
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:TextBox ID="txtAlternateMobile" CssClass="form-control input-sm" placeholder="Alternate Mobile" onkeypress="return isNumber(event)" MaxLength="10" runat="server" Width="300px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtAlternateMobile" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                        <div class="form-group">
                            <asp:Label ID="Label63" Style="line-height: 30px" runat="server" Text="Label"><b>Personal Email</b></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4" style="padding-right: 0px;">
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:TextBox ID="txtPersonalEmail" CssClass="form-control input-sm" placeholder="Personal Email" runat="server" Width="300px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator53" runat="server" ControlToValidate="txtPersonalEmail" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>

                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                        <div class="form-group">
                            <asp:Label ID="Label36" Style="line-height: 30px" runat="server" Text="Label"><b>Any Disability</b></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4" style="padding-right: 0px;">
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:DropDownList ID="drpDisability" CssClass="form-control input-sm" runat="server" Width="300px">
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>

                                </asp:DropDownList>

                            </div>
                        </div>
                    </div>




                </fieldset>
                <div id="divpg" runat="server">
                    <fieldset class="boxBodyHeader">
                        <asp:Label ID="lblCorres" runat="server"
                            Text="PG Detail" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                    </fieldset>
                    <fieldset class="boxBodyInner">
                        <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                            <div class="form-group">
                                <asp:Label ID="Label10" Style="line-height: 30px" runat="server" Text="Label"><b>PG Degree</b></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4" style="padding-right: 0px;">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                    <asp:TextBox ID="txtPGDegree" CssClass="form-control input-sm" runat="server" Width="300px">
                                    
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtPGDegree" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                            <div class="form-group">
                                <asp:Label ID="Label11" Style="line-height: 30px" runat="server" Text="Label"><b>PG Branch</b></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4" style="padding-right: 0px;">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                    <asp:TextBox ID="txtPGBranch" CssClass="form-control input-sm" runat="server" Width="300px">
                                    
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtPGBranch" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                            <div class="form-group">
                                <asp:Label ID="Label13" Style="line-height: 30px" runat="server" Text="Label"><b>Major</b></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4" style="padding-right: 0px;">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                    <asp:TextBox ID="txtMajor" CssClass="form-control input-sm" runat="server" Width="300px">
                                    
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtMajor" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                            <div class="form-group">
                                <asp:Label ID="Label14" Style="line-height: 30px" runat="server" Text="Label"><b>Minor</b></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4" style="padding-right: 0px;">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                    <asp:TextBox ID="txtMinor" CssClass="form-control input-sm" runat="server" Width="300px">
                                    
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtMinor" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                            <div class="form-group">
                                <asp:Label ID="Label15" Style="line-height: 30px" runat="server" Text="Label"><b>PG College</b></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4" style="padding-right: 0px;">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                    <asp:TextBox ID="PGCollege" CssClass="form-control input-sm" runat="server" Width="300px">
                                    
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="PGCollege" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                            <div class="form-group">
                                <asp:Label ID="Label16" Style="line-height: 30px" runat="server" Text="Label"><b>PG University</b></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4" style="padding-right: 0px;">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                    <asp:TextBox ID="PGUniv" CssClass="form-control input-sm" runat="server" Width="300px">
                                    
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="PGUniv" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                            <div class="form-group">
                                <asp:Label ID="Label17" Style="line-height: 30px" runat="server" Text="Label"><b>PG %</b></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4" style="padding-right: 0px;">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                    <asp:TextBox ID="txtPer" CssClass="form-control input-sm" onpaste="return false" onkeypress="return isNumber(event)" MaxLength="10" runat="server" Width="300px">
                                    
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtPer" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                            <div class="form-group">
                                <asp:Label ID="Label18" Style="line-height: 30px" runat="server" Text="Label"><b>Year of Passing</b></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4" style="padding-right: 0px;">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                    <asp:TextBox ID="txtPasingYearPG" CssClass="form-control input-sm" onkeydown="return false;" MaxLength="10" runat="server" Width="300px">
                                    
                                    </asp:TextBox>

                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPasingYearPG" Format="dd MMM yyyy">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtPasingYearPG" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
                <div id="divUG" runat="server">
                    <fieldset class="boxBodyHeader">
                        <asp:Label ID="Label12" runat="server"
                            Text="UG Detail" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                    </fieldset>
                    <fieldset class="boxBodyInner">
                        <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                            <div class="form-group">
                                <asp:Label ID="Label19" Style="line-height: 30px" runat="server" Text="Label"><b>UG Degree</b></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4" style="padding-right: 0px;">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                    <asp:TextBox ID="txtUGDegree" CssClass="form-control input-sm" runat="server" Width="300px">
                                    
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtUGDegree" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                            <div class="form-group">
                                <asp:Label ID="Label21" Style="line-height: 30px" runat="server" Text="Label"><b>UG Branch</b></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4" style="padding-right: 0px;">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                    <asp:TextBox ID="txtUGBranch" CssClass="form-control input-sm" runat="server" Width="300px">
                                    
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtUGBranch" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                            <div class="form-group">
                                <asp:Label ID="Label22" Style="line-height: 30px" runat="server" Text="Label"><b>UG College</b></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4" style="padding-right: 0px;">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                    <asp:TextBox ID="txtUGCollege" CssClass="form-control input-sm" runat="server" Width="300px">
                                    
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtUGCollege" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                            <div class="form-group">
                                <asp:Label ID="Label23" Style="line-height: 30px" runat="server" Text="Label"><b>UG University</b></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4" style="padding-right: 0px;">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                    <asp:TextBox ID="txtUGUniversity" CssClass="form-control input-sm" runat="server" Width="300px">
                                    
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtUGUniversity" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                            <div class="form-group">
                                <asp:Label ID="Label24" Style="line-height: 30px" runat="server" Text="Label"><b>UG %</b></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4" style="padding-right: 0px;">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                    <asp:TextBox ID="txtUGPer" CssClass="form-control input-sm" onpaste="return false" onkeypress="return isNumber(event)" MaxLength="10" runat="server" Width="300px">
                                    
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtUGPer" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                            <div class="form-group">
                                <asp:Label ID="Label25" Style="line-height: 30px" runat="server" Text="Label"><b>Year of Passing</b></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4" style="padding-right: 0px;">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                    <asp:TextBox ID="txtPassingYearUG" CssClass="form-control input-sm" onkeydown="return false;" onkeypress="return isNumber(event)" MaxLength="10" runat="server" Width="300px">
                                    
                                    </asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtPassingYearUG" Format="dd MMM yyyy">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtPassingYearUG" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>


                    </fieldset>
                </div>
                <div id="divDiploma" runat="server">
                    <fieldset class="boxBodyHeader">
                        <asp:Label ID="Label20" runat="server"
                            Text="Diploma Detail" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                    </fieldset>
                    <fieldset class="boxBodyInner">
                        <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                            <div class="form-group">
                                <asp:Label ID="Label26" Style="line-height: 30px" runat="server" Text="Label"><b>Diploma</b></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4" style="padding-right: 0px;">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                    <asp:TextBox ID="txtDiploma" CssClass="form-control input-sm" runat="server" Width="300px">
                                    
                                    </asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtDiploma" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                            <div class="form-group">
                                <asp:Label ID="Label28" Style="line-height: 30px" runat="server" Text="Label"><b>Diploma Branch</b></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4" style="padding-right: 0px;">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                    <asp:TextBox ID="txtDiplomaBranch" CssClass="form-control input-sm" runat="server" Width="300px">
                                    
                                    </asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtDiplomaBranch" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                            <div class="form-group">
                                <asp:Label ID="Label29" Style="line-height: 30px" runat="server" Text="Label"><b>Diploma College</b></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4" style="padding-right: 0px;">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                    <asp:TextBox ID="txtDiplomaCol" CssClass="form-control input-sm" runat="server" Width="300px"> 
                                    
                                    </asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtDiplomaCol" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                            <div class="form-group">
                                <asp:Label ID="Label30" Style="line-height: 30px" runat="server" Text="Label"><b>Diploma University</b></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4" style="padding-right: 0px;">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                    <asp:TextBox ID="txtDiplomaUniv" CssClass="form-control input-sm" runat="server" Width="300px">
                                    
                                    </asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtDiplomaUniv" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                            <div class="form-group">
                                <asp:Label ID="Label31" Style="line-height: 30px" runat="server" Text="Label"><b>Diploma %</b></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4" style="padding-right: 0px;">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                    <asp:TextBox ID="txtDiplomaPer" CssClass="form-control input-sm" onpaste="return false" onkeypress="return isNumber(event)" MaxLength="10" runat="server" Width="300px">
                                    
                                    </asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txtDiplomaPer" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                            <div class="form-group">
                                <asp:Label ID="Label32" Style="line-height: 30px" runat="server" Text="Label"><b>Year of Passing</b></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4" style="padding-right: 0px;">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                    <asp:TextBox ID="txtPassingYearDip" CssClass="form-control input-sm" onkeydown="return false;" onkeypress="return isNumber(event)" MaxLength="10" runat="server" Width="300px">
                                    
                                    </asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtPassingYearDip" Format="dd MMM yyyy">
                                    </cc1:CalendarExtender>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txtPassingYearDip" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                        </div>


                    </fieldset>
                </div>
                <fieldset class="boxBodyHeader">
                    <asp:Label ID="Label27" runat="server"
                        Text="12th Detail" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                </fieldset>
                <fieldset class="boxBodyInner">
                    <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                        <div class="form-group">
                            <asp:Label ID="Label33" Style="line-height: 30px" runat="server" Text="Label"><b>12th School Name</b></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4" style="padding-right: 0px;">
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:TextBox ID="txt12SchoolName" CssClass="form-control input-sm" runat="server" Width="300px">
                                    
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="txt12SchoolName" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                        <div class="form-group">
                            <asp:Label ID="Label34" Style="line-height: 30px" runat="server" Text="Label"><b>12th Board Name</b></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4" style="padding-right: 0px;">
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:TextBox ID="txtboardName12" CssClass="form-control input-sm" runat="server" Width="300px">
                                    
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="txtboardName12" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                        <div class="form-group">
                            <asp:Label ID="Label37" Style="line-height: 30px" runat="server" Text="Label"><b>12th Percentage</b></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4" style="padding-right: 0px;">
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:TextBox ID="txt12per" CssClass="form-control input-sm" onpaste="return false" onkeypress="return isNumber(event)" MaxLength="10" runat="server" Width="300px">
                                    
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="txt12per" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                        <div class="form-group">
                            <asp:Label ID="Label40" Style="line-height: 30px" runat="server" Text="Label"><b>Year of Passing</b></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4" style="padding-right: 0px;">
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:TextBox ID="passingYear12" CssClass="form-control input-sm" onkeydown="return false;" onkeypress="return isNumber(event)" MaxLength="10" runat="server" Width="300px">
                                    
                                </asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="passingYear12" Format="dd MMM yyyy">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="passingYear12" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>




                </fieldset>
                <fieldset class="boxBodyHeader">
                    <asp:Label ID="Label35" runat="server"
                        Text="10 Detail" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                </fieldset>
                <fieldset class="boxBodyInner">
                    <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                        <div class="form-group">
                            <asp:Label ID="Label41" Style="line-height: 30px" runat="server" Text="Label"><b>10th School Name</b></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4" style="padding-right: 0px;">
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:TextBox ID="txt10thSchoolName" CssClass="form-control input-sm" runat="server" Width="300px">
                                    
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="txt10thSchoolName" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                        <div class="form-group">
                            <asp:Label ID="Label42" Style="line-height: 30px" runat="server" Text="Label"><b>10th Board Name</b></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4" style="padding-right: 0px;">
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:TextBox ID="txt10thBoardName" CssClass="form-control input-sm" runat="server" Width="300px">
                                    
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="txt10thBoardName" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                        <div class="form-group">
                            <asp:Label ID="Label43" Style="line-height: 30px" runat="server" Text="Label"><b>10th Percentage</b></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4" style="padding-right: 0px;">
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:TextBox ID="txt10thPer" CssClass="form-control input-sm" onpaste="return false" onkeypress="return isNumber(event)" MaxLength="10" runat="server" Width="300px">
                                    
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="txt10thPer" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                        <div class="form-group">
                            <asp:Label ID="Label44" Style="line-height: 30px" runat="server" Text="Label"><b>Year of Passing</b></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4" style="padding-right: 0px;">
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:TextBox ID="txt10PassingYear" CssClass="form-control input-sm" onkeydown="return false;" onkeypress="return isNumber(event)" MaxLength="10" runat="server" Width="300px">
                                    
                                </asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txt10PassingYear" Format="dd MMM yyyy">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="txt10PassingYear" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>




                </fieldset>

                <fieldset class="boxBodyHeader">
                    <asp:Label ID="Label38" runat="server"
                        Text="Backlog Detail in Highest Degree" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                </fieldset>



                <fieldset class="boxBodyInner">
                    <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                        <div class="form-group">
                            <asp:Label ID="Label45" Style="line-height: 30px" runat="server" Text="Label"><b>Total Backlog (in number)</b></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4" style="padding-right: 0px;">
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:TextBox ID="txtTBacklogNumber" CssClass="form-control input-sm" onkeypress="return isNumber(event)" MaxLength="10" runat="server" Width="300px">
                                    
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="txtTBacklogNumber" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                        <div class="form-group">
                            <asp:Label ID="Label47" Style="line-height: 30px" runat="server" Text="Label"><b>Cleared backlog (in number)</b></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4" style="padding-right: 0px;">
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:TextBox ID="txtCbacklogNumber" CssClass="form-control input-sm" onkeypress="return isNumber(event)" MaxLength="10" runat="server" Width="300px">
                                    
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="txtCbacklogNumber" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                        <div class="form-group">
                            <asp:Label ID="Label46" Style="line-height: 30px" runat="server" Text="Label"><b>Pending backlog (in number)</b></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4" style="padding-right: 0px;">
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:TextBox ID="txtPBacklogNumber" CssClass="form-control input-sm" onkeypress="return isNumber(event)" MaxLength="10" runat="server" Width="300px">
                                    
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="txtPBacklogNumber" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>




                </fieldset>
                <div id="ProfessionalSkill" runat="server">
                    <fieldset class="boxBodyHeader">
                        <asp:Label ID="Label39" runat="server"
                            Text="Professional Skills" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                    </fieldset>
                    <fieldset class="boxBodyInner">
                        <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                            <div class="form-group">
                                <asp:Label ID="Label48" Style="line-height: 30px" runat="server" Text="Label"><b>Certification/VAC</b></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4" style="padding-right: 0px;">

                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:TextBox ID="txtVAC1" CssClass="form-control input-sm" runat="server" Width="300px">
                                    
                                </asp:TextBox>

                            </div>



                            <br />




                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:TextBox ID="txtVAC2" CssClass="form-control input-sm" runat="server" Width="300px">
                                    
                                </asp:TextBox>

                            </div>


                            <br />


                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:TextBox ID="txtVAC3" CssClass="form-control input-sm" runat="server" Width="300px">
                                    
                                </asp:TextBox>

                            </div>

                            <br />


                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:TextBox ID="txtVAC4" CssClass="form-control input-sm" runat="server" Width="300px">
                                    
                                </asp:TextBox>

                            </div>


                            <br />

                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                <asp:TextBox ID="txtVAC5" CssClass="form-control input-sm" runat="server" Width="300px">
                                    
                                </asp:TextBox>

                            </div>


                        </div>



                    </fieldset>
                </div>
                <fieldset class="boxBodyHeader">
                    <div class="form-group">
                        <asp:Label ID="Label64" Style="line-height: 30px" runat="server" Text="Need Placement Assistance" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                        <asp:DropDownList ID="drpPlacement" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpPlacement_SelectedIndexChanged1">
                            <asp:ListItem Text="Yes" Value="1">

                            </asp:ListItem>
                            <asp:ListItem Text="No" Value="0">

                            </asp:ListItem>




                        </asp:DropDownList>
                        &nbsp &nbsp &nbsp &nbsp &nbsp
                        <asp:Label ID="Label67" Style="line-height: 30px" runat="server" Text="Open to Relocation(Pan-India)" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                        <asp:DropDownList ID="drpRelocate" runat="server" AutoPostBack="true">
                            <asp:ListItem Text="Yes" Value="1">

                            </asp:ListItem>
                            <asp:ListItem Text="No" Value="0">

                            </asp:ListItem>



                        </asp:DropDownList>
                    </div>


                </fieldset>
                <div id="DivAdditional" runat="server" visible="false">
                    <fieldset class="boxBodyHeader">
                        <asp:Label ID="Label49" runat="server"
                            Text="Additional Information" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                    </fieldset>
                    <fieldset class="boxBodyInner">

                        <div style="height: 80px">
                            <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                                <div class="form-group">
                                    <asp:Label ID="Label50" Style="line-height: 30px" runat="server" Text="Label"><b>Higher Studies</b></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-4" style="padding-right: 0px;">

                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                    <asp:DropDownList ID="drpHigher" CssClass="form-control input-sm" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="drpHigher_SelectedIndexChanged">

                                       <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="1"></asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>
                            <div id="divHigherYes" runat="server" visible="false">
                                <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                                    <div class="form-group">

                                        <asp:Label ID="Label53" Style="line-height: 30px" runat="server" Text="Label"><b>Preferred University</b></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-4" style="padding-right: 0px;">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                            <asp:TextBox ID="txtPUniversity" CssClass="form-control input-sm" runat="server" Width="300px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                                    <div class="form-group">

                                        <asp:Label ID="Label51" Style="line-height: 30px" runat="server" Text="Label"><b>Preferred Course</b></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-4" style="padding-right: 0px;">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                            <asp:TextBox ID="txtPCourse" CssClass="form-control input-sm" runat="server" Width="300px">
                                    
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txt10thPer" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <br />
                        <div style="height: 80px">
                            <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                                <div class="form-group">
                                    <asp:Label ID="Label52" Style="line-height: 30px" runat="server" Text="Label"><b>Family Business</b></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-4" style="padding-right: 0px;">
                                <div class="form-group">
                                    <div class="input-group">
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                        <asp:DropDownList ID="drpFamily" CssClass="form-control input-sm" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="drpFamily_SelectedIndexChanged">

                                              <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Yes" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>



                            <div id="divFamily" runat="server" visible="false">

                                <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                                    <div class="form-group">

                                        <asp:Label ID="Label54" Style="line-height: 30px" runat="server" Text="Label"><b>Name of Enterprise</b></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-4" style="padding-right: 0px;">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                            <asp:TextBox ID="NameOfEnterPrise" CssClass="form-control input-sm" runat="server" Width="300px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                                    <div class="form-group">

                                        <asp:Label ID="Label55" Style="line-height: 30px" runat="server" Text="Label"><b>Location/Address</b></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-4" style="padding-right: 0px;">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                            <asp:TextBox ID="NLocation" CssClass="form-control input-sm" runat="server" Width="300px">
                                    
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txt10thPer" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                                    <div class="form-group">

                                        <asp:Label ID="Label57" Style="line-height: 30px" runat="server" Text="Label"><b>GSTIN No.</b></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-4" style="padding-right: 0px;">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                            <asp:TextBox ID="GSTINNo" CssClass="form-control input-sm" runat="server" Width="300px">
                                    
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" ControlToValidate="txt10thPer" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6" style="padding-right: 0px; font-size: 12px;">
                                </div>
                                <%--  <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                                 <asp:Label ID="blank" Width="400px" runat="server"></asp:Label>
                                 </div>
                            <div class="col-lg-4" style="padding-right: 0px;">
                                </div>--%>
                            </div>
                        </div>
                        <br />
                        <div style="height: 50px">
                            <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                                <div class="form-group">
                                    <asp:Label ID="Label58" Style="line-height: 30px" runat="server" Text="Label"><b>Startup</b></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-4" style="padding-right: 0px;">

                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                    <asp:DropDownList ID="drpStartUp" CssClass="form-control input-sm" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="drpStartUp_SelectedIndexChanged">

                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="1"></asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>

                            <div id="divStartUp" runat="server" visible="false">

                                <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                                    <div class="form-group">

                                        <asp:Label ID="Label56" Style="line-height: 30px" runat="server" Text="Label"><b>Type of StartUp/Industry</b></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-4" style="padding-right: 0px;">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                            <asp:TextBox ID="txtTypeofStartUp" CssClass="form-control input-sm" runat="server" Width="300px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>


                            </div>

                        </div>
                        <br />
                        <div style="height: 50px">
                            <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                                <div class="form-group">
                                    <asp:Label ID="Label591" Style="line-height: 30px" runat="server" Text="Label"><b>Others</b></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-4" style="padding-right: 0px;">

                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                                    <asp:TextBox ID="txtOthers" CssClass="form-control input-sm" runat="server" Width="300px">

                                        
                                    </asp:TextBox>

                                </div>
                            </div>
                    </fieldset>
                </div>


                <fieldset class="boxBodyHeader">
                    <asp:Label ID="Label61" runat="server"
                        Text="Attachments" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                </fieldset>
            </div>


            <fieldset class="boxBodyInner">
                <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                    <div class="form-group">
                        <asp:Label ID="Label59" Style="line-height: 30px" runat="server" Text="Label"><b>Resume</b></asp:Label>
                    </div>
                </div>
                <div class="col-lg-4" style="padding-right: 0px;">
                    <div class="form-group">
                        <div class="input-group">
                            <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                            <asp:FileUpload ID="txtResumeUploader" CssClass="form-control input-sm" placeholder="Resume" runat="server" Width="300px"></asp:FileUpload>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtResumeUploader" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                            <asp:Label ID="Label75" Style="line-height: 30px" runat="server" Text="RESUME" Visible="false"><b>Resume Uploaded</b></asp:Label>

                        </div>
                    </div>
                </div>
                <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
                    <div class="form-group">
                        <asp:Label ID="Label60" Style="line-height: 30px" runat="server" Text="Label"><b>Photo</b></asp:Label>
                    </div>
                </div>
                <div class="col-lg-4" style="padding-right: 0px;">
                    <div class="form-group">
                        <div class="input-group">
                            <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark"></span></span>
                            <asp:FileUpload ID="txtPhotoUploader" CssClass="form-control input-sm" placeholder="Photo" runat="server" Width="300px"></asp:FileUpload>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtPhotoUploader" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                            <asp:Label ID="Label76" Style="line-height: 30px" runat="server" Text="PHOTO" Visible="false"><b>Photo Uploaded</b></asp:Label>


                        </div>
                    </div>
                </div>
            </fieldset>

            </div>






            <fieldset class="boxBodyHeader" style="text-align: center">
                <asp:Button ID="btnSubmit" CssClass="btn" BackColor="#ed7600" ValidationGroup="odapps" OnClick="btnSubmit_Click" Text="Submit Details" runat="server" />
            </fieldset>
        </ContentTemplate>

    </asp:UpdatePanel>
    <br />
    <br />
</asp:Content>

