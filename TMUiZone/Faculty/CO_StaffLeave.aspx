<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="CO_StaffLeave.aspx.cs" Inherits="Faculty_CO_StaffLeave" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="dropdowneditable/jquery.min.js"></script>
    <script type="text/javascript" src="dropdowneditable/jquery.searchabledropdown-1.0.8.min.js"></script>


    <script type="text/javascript">
        $(document).ready(function () {
            $("select").searchable();
        });


        // demo functions
        $(document).ready(function () {
            $("#value").html($("#ddPointReport :selected").text() + " (VALUE: " + $("#ddPointReport").val() + ")");
            $("select").change(function () {
                $("#value").html(this.options[this.selectedIndex].text + " (VALUE: " + this.value + ")");
            });
        });

        function modifySelect() {
            $("select").get(0).selectedIndex = 5;
        }

        function appendSelectOption(str) {
            $("select").append("<option value=\"" + str + "\">" + str + "</option>");
        }

        function applyOptions() {
            $("select").searchable({
                maxListSize: $("#maxListSize").val(),
                maxMultiMatch: $("#maxMultiMatch").val(),
                latency: $("#latency").val(),
                exactMatch: $("#exactMatch").get(0).checked,
                wildcards: $("#wildcards").get(0).checked,
                ignoreCase: $("#ignoreCase").get(0).checked
            });

            alert(
				"OPTIONS\n---------------------------\n" +
				"maxListSize: " + $("#maxListSize").val() + "\n" +
				"maxMultiMatch: " + $("#maxMultiMatch").val() + "\n" +
				"exactMatch: " + $("#exactMatch").get(0).checked + "\n" +
				"wildcards: " + $("#wildcards").get(0).checked + "\n" +
				"ignoreCase: " + $("#ignoreCase").get(0).checked + "\n" +
				"latency: " + $("#latency").val()
			);
        }
    </script>



    <script type="text/javascript">
        $(document).ready(function () {
            $("select").searchable();
        });


        // demo functions
        $(document).ready(function () {
            $("#value").html($("#ddSubPointReport :selected").text() + " (VALUE: " + $("#ddSubPointReport").val() + ")");
            $("select").change(function () {
                $("#value").html(this.options[this.selectedIndex].text + " (VALUE: " + this.value + ")");
            });
        });

        function modifySelect() {
            $("select").get(0).selectedIndex = 5;
        }

        function appendSelectOption(str) {
            $("select").append("<option value=\"" + str + "\">" + str + "</option>");
        }

        function applyOptions() {
            $("select").searchable({
                maxListSize: $("#maxListSize").val(),
                maxMultiMatch: $("#maxMultiMatch").val(),
                latency: $("#latency").val(),
                exactMatch: $("#exactMatch").get(0).checked,
                wildcards: $("#wildcards").get(0).checked,
                ignoreCase: $("#ignoreCase").get(0).checked
            });

            alert(
				"OPTIONS\n---------------------------\n" +
				"maxListSize: " + $("#maxListSize").val() + "\n" +
				"maxMultiMatch: " + $("#maxMultiMatch").val() + "\n" +
				"exactMatch: " + $("#exactMatch").get(0).checked + "\n" +
				"wildcards: " + $("#wildcards").get(0).checked + "\n" +
				"ignoreCase: " + $("#ignoreCase").get(0).checked + "\n" +
				"latency: " + $("#latency").val()
			);
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("select").searchable();
        });


        // demo functions
        $(document).ready(function () {
            $("#value").html($("#ddStaffNameDepartment :selected").text() + " (VALUE: " + $("#ddStaffNameDepartment").val() + ")");
            $("select").change(function () {
                $("#value").html(this.options[this.selectedIndex].text + " (VALUE: " + this.value + ")");
            });
        });

        function modifySelect() {
            $("select").get(0).selectedIndex = 5;
        }

        function appendSelectOption(str) {
            $("select").append("<option value=\"" + str + "\">" + str + "</option>");
        }

        function applyOptions() {
            $("select").searchable({
                maxListSize: $("#maxListSize").val(),
                maxMultiMatch: $("#maxMultiMatch").val(),
                latency: $("#latency").val(),
                exactMatch: $("#exactMatch").get(0).checked,
                wildcards: $("#wildcards").get(0).checked,
                ignoreCase: $("#ignoreCase").get(0).checked
            });

            alert(
                "OPTIONS\n---------------------------\n" +
                "maxListSize: " + $("#maxListSize").val() + "\n" +
                "maxMultiMatch: " + $("#maxMultiMatch").val() + "\n" +
                "exactMatch: " + $("#exactMatch").get(0).checked + "\n" +
                "wildcards: " + $("#wildcards").get(0).checked + "\n" +
                "ignoreCase: " + $("#ignoreCase").get(0).checked + "\n" +
                "latency: " + $("#latency").val()
            );
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("select").searchable();
        });


        // demo functions
        $(document).ready(function () {
            $("#value").html($("#ddAirlinefileter :selected").text() + " (VALUE: " + $("#ddAirlinefileter").val() + ")");
            $("select").change(function () {
                $("#value").html(this.options[this.selectedIndex].text + " (VALUE: " + this.value + ")");
            });
        });

        function modifySelect() {
            $("select").get(0).selectedIndex = 5;
        }

        function appendSelectOption(str) {
            $("select").append("<option value=\"" + str + "\">" + str + "</option>");
        }

        function applyOptions() {
            $("select").searchable({
                maxListSize: $("#maxListSize").val(),
                maxMultiMatch: $("#maxMultiMatch").val(),
                latency: $("#latency").val(),
                exactMatch: $("#exactMatch").get(0).checked,
                wildcards: $("#wildcards").get(0).checked,
                ignoreCase: $("#ignoreCase").get(0).checked
            });

            alert(
                "OPTIONS\n---------------------------\n" +
                "maxListSize: " + $("#maxListSize").val() + "\n" +
                "maxMultiMatch: " + $("#maxMultiMatch").val() + "\n" +
                "exactMatch: " + $("#exactMatch").get(0).checked + "\n" +
                "wildcards: " + $("#wildcards").get(0).checked + "\n" +
                "ignoreCase: " + $("#ignoreCase").get(0).checked + "\n" +
                "latency: " + $("#latency").val()
            );
        }
    </script>


    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 500px;
            height: 200px;
        }
    </style>
    <style type="text/css">
        .GridPager a, .GridPager span {
            display: block;
            height: 15px;
            width: 15px;
            font-weight: bold;
            text-align: center;
            text-decoration: none;
        }

        .GridPager a {
            background-color: #f5f5f5;
            border: 1px solid #969696;
        }

        .GridPager span {
            background-color: #A1DCF2;
            border: 1px solid #3AC0F2;
        }
    </style>

    <script type="text/javascript">
        function validateHhMm(inputField) {
            var isValid = /^(([01][0-9])|(2[0-3])):([0-5][0-9])$/.test(inputField.value);

            if (isValid) {
                inputField.style.backgroundColor = '#bfa';
            } else {
                inputField.style.backgroundColor = '#fba';

                alert("Accept only Time format .. (HH:mm)!  ");
                inputField.value = "";
            }

            return isValid;


        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">




    <fieldset class="boxBody">
        <asp:Label ID="Label4" runat="server"
            Text="Leave" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
        &nbsp &nbsp &nbsp &nbsp Employee List :&nbsp<asp:DropDownList ID="drpEmployee" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drpEmployee_SelectedIndexChanged"></asp:DropDownList>
    </fieldset>

    <table cellpadding="0px" cellspacing="0px">
        <tr>
            <td style="width: 30px"></td>
            <td valign="top" style="width: 100px">



                <table cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td class="leftmMenu">&nbsp;<asp:LinkButton ID="lnkODApplication" runat="server" OnClick="lnkODApplication_Click">Application</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td class="leftmMenu">&nbsp;<asp:LinkButton ID="lnkODView" runat="server" OnClick="lnkODView_Click">Report</asp:LinkButton></td>
                    </tr>

                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td class="leftmMenu">
                            <asp:LinkButton ID="lnkApproval" runat="server" OnClick="lnkApproval_Click">Approval</asp:LinkButton>
                            <asp:Label ID="lblCountODAppoval" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>




                </table>

            </td>
            <td style="width: 30px"></td>
            <td style="width: 1px; background-color: #f1f1f1"></td>
            <td style="width: 30px"></td>
            <td valign="top">

                <table cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblHeader" runat="server"
                                Text="Application" Font-Size="15pt" ForeColor="#093A62"
                                Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td style="height: 1px; background-color: #f1f1f1"></td>
                    </tr>






                    <tr>
                        <td>


                            <asp:Panel ID="pnlCOApplication" runat="server" Visible="true">
                                <table cellpadding="0px" cellspacing="0px">

                                    <tr>
                                        <td colspan="16" align="right">

                                            <table cellpadding="0px" cellspacing="0px">

                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label6" runat="server" Text=" Note : Approval Authority : 1 ) " ForeColor="Green" Font-Size="12pt"></asp:Label>
                                                        <asp:Label ID="lblfirstApproval" runat="server" ForeColor="#FF3300" Font-Size="10pt"></asp:Label>
                                                        .&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 2) </td>
                                                    <td>
                                                        <asp:Label ID="lblSecondApproval" runat="server" ForeColor="#FF3300" Font-Size="10pt"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblApprovalAuthority1" runat="server" Text="Approval authority not tag . Please contact HR admin ,otherwise CO can't apply." Visible="False" Font-Bold="True" ForeColor="Red" Font-Size="11pt"></asp:Label></td>
                                                </tr>

                                            </table>

                                        </td>
                                    </tr>



                                    <tr>
                                        <td colspan="16" style="height: 10px"></td>
                                    </tr>




                                    <tr>
                                        <td>Date
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:TextBox ID="txtFromDate" runat="server" onkeydown="return false;" autocomplete="off"
                                                oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" AutoPostBack="True" OnTextChanged="txtFromDate_TextChanged"></asp:TextBox>
                                            <asp:CalendarExtender ID="clndAppliedate" runat="server" TargetControlID="txtFromDate" Format="dd MMM yyyy"></asp:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromDate" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>

                                        </td>
                                        <td style="width: 0px"></td>
                                        <td>From Time</td>
                                        <td style="width: 0px"></td>
                                        <td>
                                            <asp:TextBox ID="txtFromTime" runat="server" AutoPostBack="True" Height="23px" Width="100px" onchange="validateHhMm(this);" OnTextChanged="txtFromTime_TextChanged"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtFromTime" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 0px"></td>
                                        <td>Till Time</td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:TextBox ID="txtToTime" runat="server" AutoPostBack="True" Height="23px" Width="100px" onchange="validateHhMm(this);" OnTextChanged="txtToTime_TextChanged"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtToTime" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td></td>
                                        <td style="width: 10px"></td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                    </tr>


                                    <tr>
                                        <td colspan="16" style="height: 10px"></td>
                                    </tr>

                                    <tr>
                                        <td>Destination
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:TextBox ID="txtDestination" runat="server" MaxLength="100"></asp:TextBox>
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td>Purpose </td>
                                        <td style="width: 10px"></td>
                                        <td colspan="9">
                                            <asp:TextBox ID="txtPurpose" runat="server" Width="500px" MaxLength="100"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtPurpose" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>


                                    <tr>
                                        <td colspan="16" style="height: 10px"></td>
                                    </tr>

                                    <tr>
                                        <td>Remarks /Work Details</td>
                                        <td style="width: 10px"></td>
                                        <td colspan="13">
                                            <asp:TextBox ID="txtRemarks" runat="server" Width="800px"></asp:TextBox>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="16" style="height: 10px"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="16" align="right">

                                            <table cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblCOSuccess" runat="server" Text="Application has been send successfully for Approval" Font-Bold="True" Font-Size="12pt" ForeColor="Red" Visible="false"></asp:Label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:Button ID="btnSendForApproval" runat="server" Text="Send For Approval" OnClick="btnSendForApproval_Click" ValidationGroup="odapps" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="16" style="height: 10px"></td>
                                    </tr>

                                </table>
                            </asp:Panel>




                            <asp:Panel ID="pnlViewStatus" runat="server" Visible="false">
                                <table cellpadding="0px" cellspacing="0px" style="width: 100%">








                                    <tr>
                                        <td style="height: 10px" colspan="17"></td>
                                    </tr>

                                </table>




                            </asp:Panel>





                        </td>
                    </tr>


                </table>


                <asp:ScriptManager ID="ScriptManager2" runat="server">
                </asp:ScriptManager>



            </td>
        </tr>
    </table>













</asp:Content>

