<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentAttendanceMark1.aspx.cs" Inherits="Faculty_StudentAttendanceMark1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .redBorder {
            border: 1px solid red;
        }

        .loader {
            position: fixed;
            left: 45%;
            top: 45%;
            width: 100px;
            height: 100px;
            z-index: 9999;
            background: url('../images/loader.gif') 50% 50% no-repeat rgb(249,249,249);
        }
    </style>
    <script type="text/javascript">
        function preventBackspaceD(e) {
            var evt = e || window.event;
            if (evt) {
                var keyCode = evt.charCode || evt.keyCode;
                if (keyCode === 8) {
                    if (evt.preventDefault) {
                        evt.preventDefault();
                    } else {
                        evt.returnValue = false;
                    }
                }
            }
        }

        function checkDateD(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select greater than current date!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
        function callFeedbackMessageD(inputType, inputText) {

            if (inputType == 'Error') {
                alertify.error(inputText);
                return false;
            }
            else if (inputType == 'Success') {
                alertify.success("Save Successfully");
                return false;
            }
            else {
                alertify.log(inputText, "", 10000);
                return false;
            }
        }

        function CheckOneD(me) {
            debugger
            $('[id$=chkPresentAll]').attr('checked', false);
            $('[id$=chkAbsentAll]').attr('checked', false);
            me.checked = true;
            var GridId = "<%=grdStudentAttendanceD.ClientID %>";
            var grid = document.getElementById(GridId);
            rowscount = grid.rows.length - 1;

            for (i = 0; i < parseInt(rowscount) ; i++) {
                if (me.id == 'ContentPlaceHolder1_chkAbsentAll') {
                    $('[id$=ContentPlaceHolder1_grdStudentAttendanceD_chkboxAttendanceD_' + i + ']').prop("checked", false);
                }
                else {
                    $('[id$=ContentPlaceHolder1_grdStudentAttendanceD_chkboxAttendanceD_' + i + ']').prop("checked", true);
                }
            }
        }

        function DisableCheckboxD(me) {
            var GridId = "<%=grdStudentAttendanceD.ClientID %>";
            var grid = document.getElementById(GridId);
            rowscount = grid.rows.length - 1;

            for (i = 0; i < parseInt(rowscount) ; i++) {
                if (me.checked == true) {
                    $('[id$=chkbox1stAttendance_' + i + ']').attr("disabled", false);
                    $('[id$=chkbox2ndAttendance_' + i + ']').attr("disabled", false);
                    $('[id$=chkbox3rdAttendance_' + i + ']').attr("disabled", false);
                }
                else if (me.checked == false) {
                    $('[id$=chkbox1stAttendance_' + i + ']').attr("disabled", true);
                    $('[id$=chkbox2ndAttendance_' + i + ']').attr("disabled", true);
                    $('[id$=chkbox3rdAttendance_' + i + ']').attr("disabled", true);
                }
            }
        }

        function SaveD() {
            var elem = document.getElementById("Loader1");
            elem.style.display = "block";
            $(".loader").fadeIn("slow");
            $('[id$=btnSave2D]').click();
        }

        function CountD() {
            $('[id$=ddlUnitD]').addClass("redBorder");

            $('[id$=ddlUnitD]').removeClass("redBorder");
            document.getElementById('<%=btnSaveD.ClientID %>').style.visibility = "visible";
                var j = 0;
                var k = 0;
                var GridId = "<%=grdStudentAttendanceD.ClientID %>";
                var grid = document.getElementById(GridId);
                rowscount = grid.rows.length - 1;
                for (i = 0; i < parseInt(rowscount) ; i++) {
                    if ($('[id$=ContentPlaceHolder1_grdStudentAttendanceD_chkboxAttendanceD_' + i + ']').prop("checked") == true) {
                        j++;
                    }
                    k++;
                }
                $('[id$=lblNoOfStudentD]').text(j);
                $('[id$=Label3D]').text('out of');
                $('[id$=lblTotalNoOfStudentD]').text(k);
            }


            function CheckAllD(me) {
                var GridId = "<%=grdStudentAttendanceD.ClientID %>";
            var grid = document.getElementById(GridId);
            rowscount = grid.rows.length - 1;

            for (i = 0; i < parseInt(rowscount) ; i++) {
                if (me.checked == true) {
                    $('[id$=ContentPlaceHolder1_grdStudentAttendanceD_chkboxAttendanceD_' + i + ']').prop("checked", true);
                }
                else {
                    $('[id$=ContentPlaceHolder1_grdStudentAttendanceD_chkboxAttendanceD_' + i + ']').prop("checked", false);
                }
            }
        }
        ////////////// main

        function preventBackspace(e) {
            var evt = e || window.event;
            if (evt) {
                var keyCode = evt.charCode || evt.keyCode;
                if (keyCode === 8) {
                    if (evt.preventDefault) {
                        evt.preventDefault();
                    } else {
                        evt.returnValue = false;
                    }
                }
            }
        }
        function Disable() {
            var GridId = "<%=grdAttendanceDetails.ClientID %>";
            var grid = document.getElementById(GridId);
            rowscount = grid.rows.length - 1;

            for (i = 0; i < parseInt(rowscount) ; i++) {
                $('[id$=ContentPlaceHolder1_grdAttendanceDetails_chkbox1stAttendance_' + i + ']').attr("disabled", true);
                $('[id$=ContentPlaceHolder1_grdAttendanceDetails_chkbox2ndAttendance_' + i + ']').attr("disabled", true);
                $('[id$=ContentPlaceHolder1_grdAttendanceDetails_chkbox3rdAttendance_' + i + ']').attr("disabled", true);
            }
        }
        function DisableOpen() {
            var GridId = "<%=GridView1.ClientID %>";
             var grid = document.getElementById(GridId);
             rowscount = grid.rows.length - 1;

             for (i = 0; i < parseInt(rowscount) ; i++) {
                 $('[id$=ContentPlaceHolder1_GridView1_chkbox1stAttendance_' + i + ']').attr("disabled", true);
                 $('[id$=ContentPlaceHolder1_GridView1_chkbox2ndAttendance_' + i + ']').attr("disabled", true);
                 $('[id$=ContentPlaceHolder1_GridView1_chkbox3rdAttendance_' + i + ']').attr("disabled", true);
             }
         }
        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select greater than current date!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
        function callFeedbackMessage(inputType, inputText) {

            if (inputType == 'Error') {
                alertify.error(inputText);
                return false;
            }
            else if (inputType == 'Success') {
                alertify.success("Save Successfully");
                return false;
            }
            else {
                alertify.log(inputText, "", 10000);
                return false;
            }
        }

        function CheckOne(me) {
            $('[id$=chkPresentAll]').attr('checked', false);
            $('[id$=chkAbsentAll]').attr('checked', false);
            me.checked = true;
            var GridId = "<%=grdAttendanceDetails.ClientID %>";
            var grid = document.getElementById(GridId);
            rowscount = grid.rows.length - 1;

            for (i = 0; i < parseInt(rowscount) ; i++) {
                if (me.id == 'ContentPlaceHolder1_chkAbsentAll') {
                    $('[id$=ContentPlaceHolder1_grdAttendanceDetails_chkboxAttendance_' + i + ']').prop("checked", false);
                }
                else {
                    $('[id$=ContentPlaceHolder1_grdAttendanceDetails_chkboxAttendance_' + i + ']').prop("checked", true);
                }
            }
        }
        function CheckOneOpen(me) {
            $('[id$=CheckBox2]').attr('checked', false);
            $('[id$=CheckBox3]').attr('checked', false);
            me.checked = true;
            var GridId = "<%=GridView1.ClientID %>";
            var grid = document.getElementById(GridId);
            rowscount = grid.rows.length - 1;

            for (i = 0; i < parseInt(rowscount) ; i++) {
                if (me.id == 'ContentPlaceHolder1_CheckBox3') {
                    $('[id$=ContentPlaceHolder1_GridView1_chkboxAttendance_' + i + ']').prop("checked", false);
                }
                else {
                    $('[id$=ContentPlaceHolder1_GridView1_chkboxAttendance_' + i + ']').prop("checked", true);
                }
            }
        }


        function DisableCheckbox(me) {
            var GridId = "<%=grdAttendanceDetails.ClientID %>";
            var grid = document.getElementById(GridId);
            rowscount = grid.rows.length - 1;

            for (i = 0; i < parseInt(rowscount) ; i++) {
                if (me.checked == true) {
                    $('[id$=ContentPlaceHolder1_grdAttendanceDetails_chkbox1stAttendance_' + i + ']').attr("disabled", false);
                    $('[id$=ContentPlaceHolder1_grdAttendanceDetails_chkbox2ndAttendance_' + i + ']').attr("disabled", false);
                    $('[id$=ContentPlaceHolder1_grdAttendanceDetails_chkbox3rdAttendance_' + i + ']').attr("disabled", false);
                }
                else if (me.checked == false) {
                    $('[id$=ContentPlaceHolder1_grdAttendanceDetails_chkbox1stAttendance_' + i + ']').attr("disabled", true);
                    $('[id$=ContentPlaceHolder1_grdAttendanceDetails_chkbox2ndAttendance_' + i + ']').attr("disabled", true);
                    $('[id$=ContentPlaceHolder1_grdAttendanceDetails_chkbox3rdAttendance_' + i + ']').attr("disabled", true);
                }
            }
        }



        function DisableCheckboxOpen(me) {
            var GridId = "<%=GridView1.ClientID %>";
            var grid = document.getElementById(GridId);
            rowscount = grid.rows.length - 1;

            for (i = 0; i < parseInt(rowscount) ; i++) {
                if (me.checked == true) {
                    $('[id$=ContentPlaceHolder1_GridView1_chkbox1stAttendance_' + i + ']').attr("disabled", false);
                    $('[id$=ContentPlaceHolder1_GridView1_chkbox2ndAttendance_' + i + ']').attr("disabled", false);
                    $('[id$=ContentPlaceHolder1_GridView1_chkbox3rdAttendance_' + i + ']').attr("disabled", false);
                }
                else if (me.checked == false) {
                    $('[id$=ContentPlaceHolder1_GridView1_chkbox1stAttendance_' + i + ']').attr("disabled", true);
                    $('[id$=ContentPlaceHolder1_GridView1_chkbox2ndAttendance_' + i + ']').attr("disabled", true);
                    $('[id$=ContentPlaceHolder1_GridView1_chkbox3rdAttendance_' + i + ']').attr("disabled", true);
                }
            }
        }

        function Save() {
            //var elem = document.getElementById("Loader1");
            //elem.style.display = "block";
            //$(".loader").fadeIn("slow");
            $('[id$=btnSave2]').click();
        }
        function SaveOpen() {
         
            //var elem = document.getElementById("Loader1");
            //elem.style.display = "block";
            //$(".loader").fadeIn("slow");
            //alert('dfdfd');
            $('[id$=btnSave5]').click();
           
        }
        function Count() {
            var j = 0;
            var k = 0;
            var GridId = "<%=grdAttendanceDetails.ClientID %>";
            var grid = document.getElementById(GridId);
            rowscount = grid.rows.length - 1;
            for (i = 0; i < parseInt(rowscount) ; i++) {
                if ($('[id$=ContentPlaceHolder1_grdAttendanceDetails_chkboxAttendance_' + i + ']').prop("checked") == true) {
                    j++;
                }
                k++;
            }
            $('[id$=lblNoOfStudent]').text(j);
            $('[id$=lblTotalNoOfStudent]').text(k);
        }
        function CountOpen() {
            var j = 0;
            var k = 0;
            var GridId = "<%=GridView1.ClientID %>";
            var grid = document.getElementById(GridId);
            rowscount = grid.rows.length - 1;
            for (i = 0; i < parseInt(rowscount) ; i++) {
                if ($('[id$=ContentPlaceHolder1_GridView1_chkboxAttendance_' + i + ']').prop("checked") == true) {
                    j++;
                }
                k++;
            }
            $('[id$=lblNoOfStudentOpen]').text(j);
            $('[id$=lblTotalNoOfStudentOpen]').text(k);
        }
        function CheckAll(me) {
            var GridId = "<%=grdAttendanceDetails.ClientID %>";
            var grid = document.getElementById(GridId);
            rowscount = grid.rows.length - 1;

            for (i = 0; i < parseInt(rowscount) ; i++) {
                if (me.checked == true) {
                    $('[id$=ContentPlaceHolder1_grdAttendanceDetails_chkboxAttendance_' + i + ']').prop("checked", true);
                }
                else {
                    $('[id$=ContentPlaceHolder1_grdAttendanceDetails_chkboxAttendance_' + i + ']').prop("checked", false);
                }
            }
        }
        function CheckAllOpen(me) {
            var GridId = "<%=GridView1.ClientID %>";
              var grid = document.getElementById(GridId);
              rowscount = grid.rows.length - 1;

              for (i = 0; i < parseInt(rowscount) ; i++) {
                  if (me.checked == true) {
                      $('[id$=ContentPlaceHolder1_GridView1_chkboxAttendance_' + i + ']').prop("checked", true);
                  }
                  else {
                      $('[id$=ContentPlaceHolder1_GridView1_chkboxAttendance_' + i + ']').prop("checked", false);
                  }
              }
          }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Mark Attendance" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>

    <fieldset class="boxBodyHeader">
        <div class="pull-left">
            <asp:CheckBox ID="Chkdetained" runat="server" OnCheckedChanged="Chkdetained_CheckedChanged" AutoPostBack="true" Text="Detained/Supplementary" Font-Size="12pt"></asp:CheckBox>&nbsp&nbsp&nbsp&nbsp</div>
        <div class="pull-left">
            <asp:CheckBox ID="chkOpen" runat="server" Text="Open Elective" AutoPostBack="true" OnCheckedChanged="chkOpen_CheckedChanged" />
        </div>
    </fieldset>
    <fieldset class="boxBodyInner">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <asp:Panel ID="PnlDeatinedAttendence" runat="server">
                    <fieldset class="boxBodyInner">
                        <div class="loader" id="Loader1" style="display: none"></div>
                    </fieldset>
                    <fieldset class="boxBodyInner">

                        <table cellpadding="0px" cellspacing="0px">
                            <caption>
                                <br />
                                <tr>
                                    <td>
                                        <label>
                                            No.
                                        </label>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:TextBox ID="lblNoD" runat="server" Height="20px" Enabled="false" Width="220px"></asp:TextBox>
                                    </td>
                                    <td style="width: 20px"></td>
                                    <td>
                                        <label>
                                            Faculty Code
                                        </label>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:TextBox ID="lblFacultyCodeD" runat="server" Height="20px" Enabled="False" Width="220px"></asp:TextBox>
                                    </td>
                                    <td style="width: 20px"></td>
                                    <td>
                                        <label>
                                            Date
                                        </label>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:TextBox ID="txtDateD" Enabled="true" runat="server" Width="150px" Height="22px" onkeypress="return false"
                                            onKeyDown="preventBackspaceD();" OnTextChanged="txtDateD_TextChanged"
                                            AutoPostBack="true" oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDateD"
                                            CssClass="cal_Theme1" Enabled="true" TargetControlID="txtDateD">
                                        </asp:CalendarExtender>
                                        <%--<asp:CheckBox ID="CheckBox1" runat="server" Text="Arragment"/>--%>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="12" style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td>Academic Year  </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:DropDownList ID="drpAcademicYearD" Width="150px" Height="20px" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="drpAcademicYearD_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 20px"></td>
                                    <td>Course 
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:DropDownList ID="drpCourseD" runat="server" AutoPostBack="true" Height="20px"
                                            OnSelectedIndexChanged="drpCourseD_SelectedIndexChanged" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 20px"></td>
                                    <td>Semester/Year 
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:DropDownList ID="drpSemesterD" runat="server" AutoPostBack="true" Height="20px"
                                            OnSelectedIndexChanged="drpSemesterD_SelectedIndexChanged" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 10px"></td>

                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="g1" Display="Dynamic"
                                            ControlToValidate="drpAcademicYearD" InitialValue="" ErrorMessage="please select Academic Year!" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                    <td colspan="3"></td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="drpCourseD"
                                            Display="Dynamic" ErrorMessage="please select Course!" ForeColor="Red" InitialValue="" ValidationGroup="g1"></asp:RequiredFieldValidator>
                                    </td>
                                    <td colspan="3"></td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="drpSemesterD" Display="Dynamic" ErrorMessage="please select Semester!" ForeColor="Red" InitialValue="-- Semester --" ValidationGroup="g1"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="drpSemesterD" Display="Dynamic" ErrorMessage="please select Semester!" ForeColor="Red" InitialValue="" ValidationGroup="g1"></asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="12" style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td>Section </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:DropDownList ID="drpSectionD" runat="server" Height="20px" Width="150px" AutoPostBack="True"
                                            OnSelectedIndexChanged="drpSectionD_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>Group</td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:DropDownList ID="ddlGroupD" Width="150px" Height="20px" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlGroupD_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>Batch</td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:DropDownList ID="ddlBatchD" Width="150px" Height="20px" AutoPostBack="true" runat="server"
                                            OnSelectedIndexChanged="ddlBatchD_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 10px"></td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                    <td></td>
                                    <td colspan="3"></td>
                                    <td></td>
                                    <td colspan="3"></td>
                                    <td>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtTopic" InitialValue="" ErrorMessage="please input the Topic!" ForeColor="Red" ></asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="12" style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td>Subject </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:DropDownList ID="drpSubjectD" Width="150px" Height="20px" AutoPostBack="true" runat="server"
                                            OnSelectedIndexChanged="drpSubjectD_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>Lecture
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:DropDownList ID="drpLectureD" Width="150px" Height="20px" AutoPostBack="true"
                                            runat="server" OnSelectedIndexChanged="drpLectureD_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>Unit</td>
                                    <td></td>
                                    <td>
                                        <asp:DropDownList ID="drpUnitD" Width="150px" Height="20px" runat="server"></asp:DropDownList>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>

                                    <td colspan="2">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="g1" Display="Dynamic"
                                            ControlToValidate="drpSubjectD" InitialValue="" ErrorMessage="please select Subject!" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="drpSubjectD"
                                            Display="Dynamic" ErrorMessage="please select Subject!" ForeColor="Red" InitialValue="-- Subject --" ValidationGroup="g1"></asp:RequiredFieldValidator>
                                    </td>
                                    <td colspan="2"></td>
                                    <td colspan="2">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ValidationGroup="g1" Display="Dynamic"
                                            ControlToValidate="drpLectureD" InitialValue="" ErrorMessage="please select Lecture!" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server"
                                            ControlToValidate="drpLectureD" Display="Dynamic" ErrorMessage="please select Lecture!" ForeColor="Red" InitialValue="-- Lecture --" ValidationGroup="g1"></asp:RequiredFieldValidator>
                                    </td>
                                    <td colspan="2"></td>
                                    <td colspan="2">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ValidationGroup="g1" Display="Dynamic"
                                            ControlToValidate="drpUnitD" InitialValue="" ErrorMessage="please select Unit!" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server"
                                            ControlToValidate="drpUnitD" Display="Dynamic" ErrorMessage="please select Unit!" ForeColor="Red" InitialValue="-- Unit --" ValidationGroup="g1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="12" style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td>Subject Type</td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:TextBox ID="lblSubjectTypeD" Width="220px" Height="20px" Enabled="false" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>Topic </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:TextBox ID="txtTopicD" Width="220px" MaxLength="200" Height="20px" runat="server" Text=""></asp:TextBox>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <label></label>
                                        <%--Remedial Class--%>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:CheckBox ID="chkBoxExtraClassD" runat="server" Visible="false" Enabled="False" />
                                    </td>
                                    <td style="width: 10px"></td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                    <td></td>
                                    <td colspan="3"></td>
                                    <td></td>
                                    <td colspan="4"></td>
                                    <td>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtTopic" InitialValue="" ErrorMessage="please input the Topic!" ForeColor="Red" ></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                        </table>
                        <br />
                        <asp:Button ID="btnShowD" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" OnClick="btnShowD_Click"
                            Height="30px" Width="90px" Text="SHOW" />
                    </fieldset>
                    <asp:Button runat="server" ID="btnSave2D" BackColor="White" Height="0px" Width="0px" OnClick="btnSave2D_Click" BorderColor="White" />

                    <table style="width: 100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblMessageD" runat="server" Visible="false" ForeColor="Red" Width="100%"
                                    Text="<u>You have already mark the Attendance of this lecture..</u>"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <br />
                                <asp:Panel ID="pnlStudentAttendanceD" runat="server" Width="100%" Visible="true">

                                    <asp:GridView ID="grdStudentAttendanceD" DataKeyNames="Enrollment No" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                                        GridLines="Horizontal" OnSelectedIndexChanged="grdStudentAttendanceD_SelectedIndexChanged" EmptyDataText="There are no data records to display."
                                        AllowSorting="true">
                                        <AlternatingRowStyle BackColor="#F7F7F7" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl. No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Enrollment No" HeaderText="Roll No" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="Student Name" HeaderText="Student Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="Course" HeaderText="Course" SortExpression="Course" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="Subject Code" HeaderText="Subject Code" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="Description" HeaderText="Subject Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="SemesterYear" HeaderText="Semester/Year" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="Section" HeaderText="Section" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="Group" HeaderText="Group" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="Batch" HeaderText="Batch" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:TemplateField HeaderText="Today">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkboxAttendanceD" HeaderText="Today" Checked='<%#Convert.ToBoolean(Convert.ToInt32(Eval("Today"))) %>'
                                                        TabIndex="1" runat="server" />
                                                </ItemTemplate>

                                                <HeaderTemplate>Today</HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfSubjectTypeD" runat="server" Value='<%# Eval("SubjectType") %>' />
                                                </ItemTemplate>
                                                <HeaderTemplate></HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfStudentNoD" runat="server" Value='<%# Eval("No_") %>' />
                                                </ItemTemplate>
                                                <HeaderTemplate></HeaderTemplate>
                                            </asp:TemplateField>

                                            <%--<asp:ButtonField DataTextField="Percentage" ButtonType="Link"  ControlStyle-ForeColor="Orange"  CommandName="Select" HeaderText="Percentage" />  --%>
                                        </Columns>
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                        <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                        <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
                                        <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                        <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                        <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                    </asp:GridView>

                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                    </table>
                    <div class="btn pull-right">
                        <asp:LinkButton ID="btnSubmitD" runat="server" CssClass="btn-sm btn-primary btn-block" OnClientClick="return CountD();" data-toggle="modal" data-target="#myModal2D" Visible="false" Height="30px" Width="90px" Text="SUBMIT" />
                    </div>
                    <div class="modal fade" id="myModal2D">
                        <div class="modal-dialog" style="width: 400px; height: 100px">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #88CCFF;">
                                    <div>
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title"><b style="font-family: Arial; font-size: 15px; font-weight: bold">Number of Student - Present</b></h4>
                                    </div>
                                </div>
                                <div class="modal-body">
                                    <br />
                                    <center>
                                        <asp:Label runat="server" ID="lblNoOfStudentD" Font-Bold="true" Font-Size="15px"></asp:Label>
                                        <asp:Label runat="server" ID="Label3D" Font-Bold="true" Font-Size="15px" Text=" out of "></asp:Label></u>
                                    <asp:Label runat="server" ID="lblTotalNoOfStudentD" Font-Bold="true" Font-Size="15px"></asp:Label></u>
                                    </center>
                                </div>
                                <div class="modal-footer" style="border-top-width: 0px">
                                    <asp:Button runat="server" ID="btnSaveD" CssClass="btn-sm btn-primary" OnClientClick="SaveD();" class="close" data-dismiss="modal" Width="20%" Text="Ok"></asp:Button>
                                </div>
                            </div>

                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="PanelmainAttendence" runat="server">
                    <fieldset class="boxBodyInner">
                        <div class="loader" id="Loader2" style="display: none"></div>
                    </fieldset>
                    <fieldset class="boxBodyInner">
                        <table cellpadding="0px" cellspacing="0px">
                            <caption>
                                <br />
                                <tr>
                                    <td>
                                        <label>
                                            No.
                                        </label>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:TextBox ID="lblNo" runat="server" Height="20px" Enabled="false" Width="150px"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px"></td>
                                    <td>
                                        <label>
                                            Faculty Code
                                        </label>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:TextBox ID="lblFacultyCode" runat="server" Height="20px" Enabled="False" Width="150px"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px"></td>
                                    <td>
                                        <label>
                                            Date
                                        </label>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:TextBox ID="txtDate" Enabled="true" runat="server" Width="150px" Height="22px" onkeypress="return false" onKeyDown="preventBackspace();" OnTextChanged="txtDate_TextChanged"
                                            AutoPostBack="true" oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDate"
                                            CssClass="cal_Theme1" Enabled="true" TargetControlID="txtDate">
                                        </asp:CalendarExtender>
                                        <%--<asp:CheckBox ID="CheckBox1" runat="server" Text="Arragment"/>--%>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="12" style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td>Academic Year  </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:DropDownList ID="drpAcademicYear" Width="150px" Height="20px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpAcademicYear_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                    <td style="width: 20px"></td>
                                    <td>Course 
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:DropDownList ID="drpCourse" runat="server" AutoPostBack="true" Height="20px" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 20px"></td>
                                    <td>Semester/Year 
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:DropDownList ID="drpSemester" runat="server" AutoPostBack="true" Height="20px" OnSelectedIndexChanged="drpSemester_SelectedIndexChanged" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 10px"></td>

                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpAcademicYear" InitialValue="" ErrorMessage="please select Academic Year!" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                    <td colspan="3"></td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpCourse" Display="Dynamic" ErrorMessage="please select Course!" ForeColor="Red" InitialValue="" ValidationGroup="g1"></asp:RequiredFieldValidator>
                                    </td>
                                    <td colspan="3"></td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpSemester" Display="Dynamic" ErrorMessage="please select Semester!" ForeColor="Red" InitialValue="-- Semester --" ValidationGroup="g1"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="drpSemester" Display="Dynamic" ErrorMessage="please select Semester!" ForeColor="Red" InitialValue="" ValidationGroup="g1"></asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="12" style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td>Section </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:DropDownList ID="drpSection" runat="server" Height="20px" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="drpSection_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>Group</td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:DropDownList ID="ddlGroup" Width="150px" Height="20px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>Batch</td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:DropDownList ID="ddlBatch" Width="150px" Height="20px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                    <td style="width: 10px"></td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                    <td></td>
                                    <td colspan="3"></td>
                                    <td></td>
                                    <td colspan="3"></td>
                                    <td>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtTopic" InitialValue="" ErrorMessage="please input the Topic!" ForeColor="Red" ></asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="12" style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td>Subject </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:DropDownList ID="drpSubject" Width="150px" Height="20px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drpSubject_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>Lecture
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:DropDownList ID="drpLecture" Width="150px" Height="20px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drpLecture_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>Unit</td>
                                    <td></td>
                                    <td>
                                        <asp:DropDownList ID="drpUnit" Width="150px" Height="20px" runat="server"></asp:DropDownList>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>

                                    <td colspan="2">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpSubject" InitialValue="" ErrorMessage="please select Subject!" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="drpSubject" Display="Dynamic" ErrorMessage="please select Subject!" ForeColor="Red" InitialValue="-- Subject --" ValidationGroup="g1"></asp:RequiredFieldValidator>
                                    </td>
                                    <td colspan="2"></td>
                                    <td colspan="2">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpLecture" InitialValue="" ErrorMessage="please select Lecture!" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="drpLecture" Display="Dynamic" ErrorMessage="please select Lecture!" ForeColor="Red" InitialValue="-- Lecture --" ValidationGroup="g1"></asp:RequiredFieldValidator>
                                    </td>
                                    <td colspan="2"></td>
                                    <td colspan="2">
                                        <asp:RequiredFieldValidator ID="rfvUnit" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpUnit" InitialValue="" ErrorMessage="please select Unit!" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvddlUnit" runat="server" ControlToValidate="drpUnit" Display="Dynamic" ErrorMessage="please select Unit!" ForeColor="Red" InitialValue="-- Unit --" ValidationGroup="g1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="12" style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td>Subject Type</td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:TextBox ID="lblSubjectType" Width="150px" Height="20px" Enabled="false" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>Topic </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:TextBox ID="txtTopic" Width="150px" MaxLength="200" Height="20px" runat="server" Text=""></asp:TextBox>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <label>Remedial Class </label>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:CheckBox ID="chkBoxExtraClass" runat="server"  />
                                    </td>
                                    <td style="width: 10px"></td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                    <td></td>
                                    <td colspan="3"></td>
                                    <td></td>
                                    <td colspan="4"></td>
                                    <td>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtTopic" InitialValue="" ErrorMessage="please input the Topic!" ForeColor="Red" ></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                 <tr>
                                    <td colspan="12" style="height: 10px"></td>
                                </tr>
                                <tr ID="divAdmissionPeriod" runat="server"  visible="false">
                                    <td>Admission Period</td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:DropDownList  ID="drpAdmissionPeriod"   Width="150px" Height="20px" runat="server">
                                            <asp:ListItem Text="WINTER" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="SUMMER" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td> </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <label> </label>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                       
                                    </td>
                                    <td style="width: 10px"></td>
                                </tr>
                        </table>
                        <br />
                        <asp:Button ID="btnShow" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px" Width="90px" OnClick="btnShow_Click" Text="SHOW" />
                    </fieldset>

                    <asp:Button runat="server" ID="btnSave2" BackColor="White" Height="0px" Width="0px" OnClick="btnSave1_Click" BorderColor="White" />
                    <asp:Panel runat="server" ID="pnlCheckBox" Visible="false">
                        <table>
                            <tr style="height: 10px">
                                <td colspan="5"></td>
                            </tr>
                            <tr>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:CheckBox ID="chkPresentAll" runat="server" Text=" Present All" Checked="true" onClick="CheckOne(this)" />

                                </td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:CheckBox ID="chkAbsentAll" Text="Absent All" runat="server" onClick="CheckOne(this)" />
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr style="height: 10px">
                                <td colspan="5"></td>
                            </tr>
                            <tr>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:CheckBox runat="server" ID="chkboxEditAttendance" Visible="false" onClick="DisableCheckbox(this)" Text="Edit Attendance" />
                                </td>
                                <td style="width: 10px"></td>
                                <td colspan="2">
                                    <asp:CheckBox runat="server" ID="chkMultiplaeAttendance" AutoPostBack="true" OnCheckedChanged="chkMultiplaeAttendance_CheckedChanged" Text="Multiple Attendance" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>

                    <center>
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblMessage" runat="server" Visible="false" ForeColor="Red" Width="100%" Text="<u>You have already mark the Attendance of this lecture..</u>"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <br />
                                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both" Height="400" Width="100%" Visible="false">

                                        <asp:GridView ID="grdAttendanceDetails" DataKeyNames="No_" runat="server" AutoGenerateColumns="False" BackColor="White"
                                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Horizontal"
                                            OnSelectedIndexChanged="OnSelectedIndexChanged" EmptyDataText="There are no data records to display.">
                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="3%" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Enrollment No_" HeaderText="Roll No" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="No_" HeaderText="Student No" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" DataFormatString="{0:N2}" ItemStyle-CssClass="visible-lg" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkbox1stAttendance" HeaderText="1st" TabIndex="2" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>1st</HeaderTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkbox2ndAttendance" HeaderText="2nd" TabIndex="3" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>2nd</HeaderTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkbox3rdAttendance" HeaderText="3rd" TabIndex="4" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>3rd</HeaderTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkboxAttendance" HeaderText="Today" Checked="true" TabIndex="1" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>Today</HeaderTemplate>
                                                </asp:TemplateField>
                                                <%--       <asp:TemplateField>
                                    <ItemTemplate >
                                        <asp:LinkButton ID="lnkPercentage" OnClientClick="return BindGrid(this);"   HeaderText="Today"  CommandArgument='<%# Eval("Percentage") %>' runat="server" />                                                               
                                    </ItemTemplate>
                                        <HeaderTemplate>Percentage</HeaderTemplate>
                                    </asp:TemplateField>--%>
                                                <asp:BoundField DataField="Student Name" HeaderText="Student Name" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" DataFormatString="{0:N2}" ItemStyle-CssClass="visible-lg" />
                                                <asp:ButtonField DataTextField="Percentage" ButtonType="Link" ControlStyle-ForeColor="Orange" CommandName="Select" HeaderText="Percentage" />

                                            </Columns>
                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                            <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                            <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
                                            <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
                                            <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                            <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                            <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                            <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                        </asp:GridView>

                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="btn pull-right">
                                        <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn-sm btn-primary btn-block" OnClientClick="return Count();" data-toggle="modal" data-target="#myModal2" Visible="false" Height="30px" Width="90px" Text="SUBMIT" OnClick="btnSubmit_Click" />
                                    </div>
                                </td>
                            </tr>
                        </table>

                        <div class="modal fade" id="myModal3">
                            <div class="modal-dialog" style="width: 800px;">
                                <div class="modal-content">
                                    <div class="modal-header" style="background-color: #88CCFF;">
                                        <div>
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                            <h4 class="modal-title"><b style="font-family: Arial; font-size: 15px; font-weight: bold">Attendance Details</b></h4>
                                        </div>
                                    </div>
                                    <div class="modal-body">
                                        <br />
                                        <center>
                                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="200" Width="80%">
                                                <asp:GridView ID="grdAttendanceReport" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                                                    <AlternatingRowStyle BackColor="#F7F7F7" />
                                                    <Columns>
                                                        <asp:BoundField DataField="Student Name" HeaderText="Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                        <asp:BoundField DataField="Date1" HeaderText="Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                        <asp:BoundField DataField="Hour" HeaderText="Lecture No" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                        <asp:BoundField DataField="Attendance" HeaderText="Attendance" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                    </Columns>
                                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                    <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                                    <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
                                                    <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
                                                    <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                                    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                                    <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                                    <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </center>
                                    </div>
                                    <div class="modal-footer" style="border-top-width: 0px">
                                        <asp:Button runat="server" Text="Export to Excel" ID="btnExportToExcel" BackColor="#ACE9FB" OnClick="ExportToExcel" />
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="modal fade" id="myModal2">
                            <div class="modal-dialog" style="width: 400px; height: 100px">
                                <div class="modal-content">
                                    <div class="modal-header" style="background-color: #88CCFF;">
                                        <div>
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                            <h4 class="modal-title"><b style="font-family: Arial; font-size: 15px; font-weight: bold">Number of Student - Present</b></h4>
                                        </div>
                                    </div>
                                    <div class="modal-body">
                                        <br />
                                        <center>
                                            <asp:Label runat="server" ID="lblNoOfStudent" Font-Bold="true" Font-Size="15px"></asp:Label>
                                            <asp:Label runat="server" ID="Label3" Font-Bold="true" Font-Size="15px" Text=" out of "></asp:Label></u>
                                    <asp:Label runat="server" ID="lblTotalNoOfStudent" Font-Bold="true" Font-Size="15px"></asp:Label></u>
                                        </center>
                                    </div>
                                    <div class="modal-footer" style="border-top-width: 0px">
                                        <asp:Button runat="server" ID="btnSave1" CssClass="btn-sm btn-primary" OnClientClick="Save();" class="close" data-dismiss="modal" Width="20%" Text="Ok"></asp:Button>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </center>
                </asp:Panel>
                <asp:Panel ID="pnlOpen" runat="server" >
                    <fieldset class="boxBodyInner">
                        <div class="loader" id="Div1" style="display: none"></div>
                    </fieldset>
                    <fieldset class="boxBodyInner">
                        <table cellpadding="0px" cellspacing="0px">
                            <caption>
                                <br />
                                <tr>
                                    <td>
                                        <label>
                                            No.
                                        </label>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:TextBox ID="txtNo1" runat="server" Height="20px" Enabled="false" Width="220px"></asp:TextBox>
                                    </td>
                                    <td style="width: 20px"></td>
                                    <td>
                                        <label>
                                            Faculty Code
                                        </label>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:TextBox ID="txtStaff1" runat="server" Height="20px" Enabled="False" Width="220px"></asp:TextBox>
                                    </td>
                                    <td style="width: 20px"></td>
                                    <td>
                                        <label>
                                            Date
                                        </label>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:TextBox ID="txtDate1" Enabled="true" runat="server" Width="150px" Height="22px" onkeypress="return false" onKeyDown="preventBackspace();" OnTextChanged="txtDate1_TextChanged"
                                            AutoPostBack="true" oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender3" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDate"
                                            CssClass="cal_Theme1" Enabled="true" TargetControlID="txtDate1">
                                        </asp:CalendarExtender>
                                        <%--<asp:CheckBox ID="CheckBox1" runat="server" Text="Arragment"/>--%>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="12" style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td>Academic Year  </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:DropDownList ID="drpAcademic1" Width="150px" Height="20px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpAcademicYear_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                    <td style="width: 20px"></td>
                                    <td>Subject
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                       <asp:DropDownList ID="drpSubject1" Width="150px" Height="20px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drpSubject1_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                    <td style="width: 20px"></td>
                                    <td>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                      
                                    </td>
                                    <td style="width: 10px"></td>

                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ValidationGroup="g5" Display="Dynamic" ControlToValidate="drpAcademic1" InitialValue="" ErrorMessage="please select Academic Year!" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                    <td colspan="3"></td>
                                    
                                    <td colspan="3"></td>
                                    
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="12" style="height: 10px"></td>
                                </tr>
                                
                                <tr>
                                    <td colspan="2"></td>
                                    <td></td>
                                    <td colspan="3"></td>
                                    <td></td>
                                    <td colspan="3"></td>
                                    <td>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtTopic" InitialValue="" ErrorMessage="please input the Topic!" ForeColor="Red" ></asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="12" style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td>Section </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                       <asp:DropDownList ID="drpSection1" Width="150px" Height="20px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drpSection1_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>Lecture
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:DropDownList ID="drpLecture1" Width="150px" Height="20px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drpLecture1_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>Unit</td>
                                    <td></td>
                                    <td>
                                        <asp:DropDownList ID="drpUnit1" Width="150px" Height="20px" runat="server"></asp:DropDownList>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>

                                    <td colspan="2">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ValidationGroup="g5" Display="Dynamic" ControlToValidate="drpSubject1" InitialValue="" ErrorMessage="please select Subject!" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="drpSubject1" Display="Dynamic" ErrorMessage="please select Subject!" ForeColor="Red" InitialValue="-- Subject --" ValidationGroup="g5"></asp:RequiredFieldValidator>
                                    </td>
                                    <td colspan="2"></td>
                                    <td colspan="2">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ValidationGroup="g5" Display="Dynamic" ControlToValidate="drpLecture1" InitialValue="" ErrorMessage="please select Lecture!" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="drpLecture1" Display="Dynamic" ErrorMessage="please select Lecture!" ForeColor="Red" InitialValue="-- Lecture --" ValidationGroup="g5"></asp:RequiredFieldValidator>
                                    </td>
                                    <td colspan="2"></td>
                                    <td colspan="2">
                                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ValidationGroup="g5" Display="Dynamic" ControlToValidate="drpUnit1" InitialValue="" ErrorMessage="please select Unit!" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="drpUnit1" Display="Dynamic" ErrorMessage="please select Unit!" ForeColor="Red" InitialValue="-- Unit --" ValidationGroup="g5"></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="12" style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td>Subject Type</td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:TextBox ID="txtSubjectType1" Width="220px" Height="20px" Enabled="false" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>Topic </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:TextBox ID="txttopic1" Width="220px" MaxLength="200" Height="20px" runat="server" Text=""></asp:TextBox>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <label>Remedial Class </label>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:CheckBox ID="chkRemedial1" runat="server" Enabled="False" />
                                    </td>
                                    <td style="width: 10px"></td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                    <td></td>
                                    <td colspan="3"></td>
                                    <td></td>
                                    <td colspan="4"></td>
                                    <td>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtTopic" InitialValue="" ErrorMessage="please input the Topic!" ForeColor="Red" ></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                        </table>
                        <br />
                        <asp:Button ID="btnShow1" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g5" Height="30px" Width="90px" OnClick="btnShow1_Click" Text="SHOW" />
                    </fieldset>

                    <asp:Button runat="server" ID="btnSave5" BackColor="White" Height="0px" Width="0px" OnClick="btnSave3_Click" BorderColor="White" />
                   
                    
                    
                     <asp:Panel runat="server" ID="Panel2" Visible="false">
                        <table>
                            <tr style="height: 10px">
                                <td colspan="5"></td>
                            </tr>
                            <tr>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:CheckBox ID="CheckBox2" runat="server" Text="Present All" Checked="true" onClick="CheckOneOpen(this)" />

                                </td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:CheckBox ID="CheckBox3" Text="Absent All" runat="server" onClick="CheckOneOpen(this)" />
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr style="height: 10px">
                                <td colspan="5"></td>
                            </tr>
                            <tr>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:CheckBox runat="server" ID="CheckBox4" onClick="DisableCheckboxOpen(this)" Text="Edit Attendance" />
                                </td>
                                <td style="width: 10px"></td>
                                <td colspan="2">
                                    <asp:CheckBox runat="server" ID="CheckBox5" AutoPostBack="true" OnCheckedChanged="CheckBox5_CheckedChanged" Text="Multiple Attendance" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>

                    <center>
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Visible="false" ForeColor="Red" Width="100%" Text="<u>You have already mark the Attendance of this lecture..</u>"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <br />
                                    <asp:Panel ID="Panel3" runat="server" ScrollBars="Both" Height="400" Width="100%" Visible="false">

                                        <asp:GridView ID="GridView1" DataKeyNames="No_" runat="server" AutoGenerateColumns="False" BackColor="White"
                                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Horizontal"
                                            OnSelectedIndexChanged="OnSelectedIndexChanged" EmptyDataText="There are no data records to display.">
                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="3%" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Enrollment No_" HeaderText="Roll No" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="No_" HeaderText="Student No" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" DataFormatString="{0:N2}" ItemStyle-CssClass="visible-lg" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkbox1stAttendance" HeaderText="1st" TabIndex="2" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>1st</HeaderTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkbox2ndAttendance" HeaderText="2nd" TabIndex="3" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>2nd</HeaderTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkbox3rdAttendance" HeaderText="3rd" TabIndex="4" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>3rd</HeaderTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkboxAttendance" HeaderText="Today" Checked="true" TabIndex="1" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>Today</HeaderTemplate>
                                                </asp:TemplateField>
                                               
                                                <asp:BoundField DataField="Student Name" HeaderText="Student Name" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" DataFormatString="{0:N2}" ItemStyle-CssClass="visible-lg" />
                                                <asp:ButtonField DataTextField="Percentage" ButtonType="Link" ControlStyle-ForeColor="Orange" CommandName="Select" HeaderText="Percentage" />

                                            </Columns>
                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                            <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                            <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
                                            <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
                                            <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                            <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                            <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                            <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                        </asp:GridView>

                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="btn pull-right">
                                        <asp:LinkButton ID="lnkSubmitOpen" runat="server" CssClass="btn-sm btn-primary btn-block" OnClientClick="return CountOpen();" data-toggle="modal" data-target="#Div3" Visible="false" Height="30px" Width="90px" Text="SUBMIT" OnClick="lnkSubmitOpen_Click" />
                                    </div>
                                </td>
                            </tr>
                        </table>

                        <div class="modal fade" id="Div2">
                            <div class="modal-dialog" style="width: 800px;">
                                <div class="modal-content">
                                    <div class="modal-header" style="background-color: #88CCFF;">
                                        <div>
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                            <h4 class="modal-title"><b style="font-family: Arial; font-size: 15px; font-weight: bold">Attendance Details</b></h4>
                                        </div>
                                    </div>
                                    <div class="modal-body">
                                        <br />
                                        <center>
                                            <asp:Panel ID="Panel4" runat="server" ScrollBars="Vertical" Height="200" Width="80%">
                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                                                    <AlternatingRowStyle BackColor="#F7F7F7" />
                                                    <Columns>
                                                        <asp:BoundField DataField="Student Name" HeaderText="Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                        <asp:BoundField DataField="Date1" HeaderText="Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                        <asp:BoundField DataField="Hour" HeaderText="Lecture No" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                        <asp:BoundField DataField="Attendance" HeaderText="Attendance" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                    </Columns>
                                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                    <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                                    <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
                                                    <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
                                                    <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                                    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                                    <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                                    <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </center>
                                    </div>
                                    <div class="modal-footer" style="border-top-width: 0px">
                                        <asp:Button runat="server" Text="Export to Excel" ID="Button3" BackColor="#ACE9FB" OnClick="ExportToExcel" />
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="modal fade" id="Div3">
                            <div class="modal-dialog" style="width: 400px; height: 100px">
                                <div class="modal-content">
                                    <div class="modal-header" style="background-color: #88CCFF;">
                                        <div>
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                            <h4 class="modal-title"><b style="font-family: Arial; font-size: 15px; font-weight: bold">Number of Student - Present</b></h4>
                                        </div>
                                    </div>
                                    <div class="modal-body">
                                        <br />
                                        <center>
                                            <asp:Label runat="server" ID="lblNoOfStudentOpen" Font-Bold="true" Font-Size="15px"></asp:Label>
                                            <asp:Label runat="server" ID="Label5" Font-Bold="true" Font-Size="15px" Text=" out of "></asp:Label></u>
                                    <asp:Label runat="server" ID="lblTotalNoOfStudentOpen" Font-Bold="true" Font-Size="15px"></asp:Label></u>
                                        </center>
                                    </div>
                                    <div class="modal-footer" style="border-top-width: 0px">
                                        <asp:Button runat="server" ID="Button4" CssClass="btn-sm btn-primary" OnClientClick="SaveOpen();" class="close" data-dismiss="modal" Width="20%" Text="Ok"></asp:Button>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </center>
                </asp:Panel>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="drpCourse" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="btnShow" EventName="Click" />
                <asp:PostBackTrigger ControlID="btnExportToExcel" />
            </Triggers>
        </asp:UpdatePanel>
    </fieldset>
</asp:Content>

