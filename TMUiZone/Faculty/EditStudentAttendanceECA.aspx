<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="EditStudentAttendanceECA.aspx.cs" Inherits="Faculty_EditStudentAttendanceECA" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .completionList {
            border: solid 1px Gray;
            margin: 0px;
            padding: 3px;
            height: 140px;
            overflow: scroll;
            background-color: #FFFFFF;
        }

        .listItem {
            color: #191919;
        }

        .itemHighlighted {
            background-color: #ADD6FF;
        }
    </style>

    <script type="text/javascript">
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

        function checkDateFrom(sender, args) {
            var today = new Date();
            if (sender._selectedDate > today) {
                alertify.error("You cannot select greater than current date !");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }

            else {
                var f = new Date($('[id$=txtDateTo]').val());
                if (new Date(sender._selectedDate).val() > f) {
                    alertify.error("You cannot select greater than To date !");
                    sender._textbox.set_Value('');
                }
            }


        }
        function checkDateTo(sender, args) {
            var today = new Date();
            if (sender._selectedDate > today) {
                alertify.error("You cannot select greater than current date !");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
            if ($('[id$=txtDateFrom]').val() == '') {
                alertify.error('First select the from date !');
                sender._textbox.set_Value('');
                return false;
            }
            else {
                var f = new Date($('[id$=txtDateFrom]').val());

                if (sender._selectedDate < f) {
                    alertify.error("You cannot select less than from date !");
                    sender._textbox.set_Value('');
                }
            }
        }
    </script>
    <script type="text/javascript">
        var arr1 = [];
        function myFunctionGetCustomers(me) {
            debugger
            var added = false;

            for (i = 0; i < arr1.length; i++) {
                if ($('[id$=txtStudentNo]').val() == arr1[i])
                    added = true;
            }
            if ($('[id$=hdnStudentNo]').val() != $('[id$=txtStudentNo]').val()) {
                if (added == true) {
                    alertify.error('You cant select this student again !');
                    $('[id$=txtStudentNo]').val('');
                    return false;
                }
                arr1.push($('[id$=txtStudentNo]').val());
                $('[id$=hdnStudentNo]').val($('[id$=txtStudentNo]').val());
                var arr = { StudentName: $('[id$=txtStudentNo]').val() };
                $.ajax({
                    type: "POST",
                    url: "EditStudentAttendanceECA.aspx/GetCustomers",
                    data: JSON.stringify(arr),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess,
                    error: function (xmlHttpRequest, textStatus, errorThrown) {
                        alert('Error');
                    }
                });
            }
            else
                $('[id$=txtStudentNo]').val('');
        }
        function OnSuccess(response) {
            $('[id$=pnlGrid]').show();
            if (response.d.length == 0) {
                $('[id$=btnSave]').attr("disabled", true);
                $('[id$=txtStudentNo]').focus();
                return false;
            }
            else {
                for (var i = 0; i < response.d.length; i++) {
                    $('[id$=grdAttendanceDetails]').append("<tr><td>" + response.d[i].EnrollmentNo + "</td><td>" + response.d[i].StudentNo + "</td><td>" + response.d[i].StudentName + "</td><td>" + response.d[i].FatherName + "</td><td>"
                        + response.d[i].CourseCode + "</td><td>" + response.d[i].Semester + "</td><td>" + response.d[i].Section + "</td><td>" + response.d[i].Admission_Date + "</td><td><img src='../logo/close.png'  height='20px' width='20px'  class='btnDelete'/></td></tr>");
                }
                $('[id$=btnSave]').attr("disabled", false);
            }
            $(".btnDelete").bind("click", Delete);
            $('[id$=txtStudentNo]').val('');
            return false;
        }

        function Delete() {
            var par = $(this).parent().parent();
            par.remove();
            var grid = document.getElementById("<%=grdAttendanceDetails.ClientID %>");
            var rowsCount = grid.rows.length;
            if (rowsCount == 1) { $('[id$=pnlGrid]').hide(); }
            return false;
        }

        function ShowHide() {
            $('[id$=btnSave]').attr("disabled", true);
            $('[id$=pnlGrid]').hide();
            $('[id$=txtDateFrom]').val('');
            $('[id$=txtDateTo]').val('');
            $('[id$=txtRemarks]').val('');

            if ($('[id$=hdnValue]').val() == '1') {
                $('[id$=pnlGrid]').show();
                $('[id$=btnSave]').attr("disabled", false);
            }
        }

        $(document).ready(function () {
            $('[id$=btnUpload]').click(function Save() {
                $('[id$=hdnValue]').val('1');
            });
            ShowHide();

            $('[id$=btnSave]').click(function () {

                if ($('[id$=txtDateFrom]').val() == '' || $('[id$=txtDateTo]').val() == '') {
                    alertify.error('Please select the "From Date" and "To Date"');
                    return false;
                }
                if ($('[id$=ddlEventType]').val() == '' || $('[id$=ddlEventType]').val() == '') {
                    alertify.error('Please select Reason"');
                    return false;
                }
                if ($('[id$=ddlEventType1]').val() == '' || $('[id$=ddlEventType1]').val() == '') {
                    alertify.error('Please select Reason Type');
                    return false;
                }
                if ($('[id$=txtRemarks]').val() == '') {
                    alertify.error('Please write Remarks');
                    return false;
                }
                //----------ashu------13-02-2017---list box--
                var values = ""; var arrLecture = new Array();
                var listBox = document.getElementById("<%= ddlLecture.ClientID%>");
                var j = 0;
                for (var i = 0; i < listBox.options.length; i++) {
                    if (listBox.options[i].selected) {
                        // values += listBox.options[i].value + ",";

                        arrLecture[j] = listBox.options[i].value;
                        j++;
                    }
                }
                alert(arrLecture);
                if (arrLecture.length > 0) { }
                else { alertify.error('Please select Lecture !'); return false; }
                //------------------------------------------

                var grid = document.getElementById("<%=grdAttendanceDetails.ClientID %>");
                var Count1 = grid.rows.length;

                if (Count1 < 2) {
                    alertify.error('Please Add Student');
                    return false;
                }

                var StudentNolist = new Array();
                for (var j = 1; j < Count1; j++) {
                    var MName = grid.rows[j].cells[1].outerText;
                    StudentNolist[j] = MName;
                };
                 //var chkManyDays = $('[id$=chkManyDays]').is(':checked') ? "yes" : "no";
                var chkPresent = $('[id$=chkPresent]').is(':checked') ? "yes" : $('[id$=chkAbsent]').is(':checked') ? "no" : "NC";
                var remarks = $('[id$=txtRemarks]').val();
                var Lecture = values.substring(0, values.length - 1);// $('[id$=ddlLecture]').val();    
                var arr1 = {
                    StudentNolist: StudentNolist, chkPresent: chkPresent, EventType: $('[id$=ddlEventType]').val(), EventType1: $('[id$=ddlEventType1]').val(), remarks: remarks,
                    DateFrom: $('[id$=txtDateFrom]').val(), DateTo: $('[id$=txtDateTo]').val(), Lecture: arrLecture,
                };
                $.ajax({
                    type: "POST",
                    url: "EditStudentAttendanceECA.aspx/Save",
                    data: JSON.stringify(arr1),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    // success: OnSuccess1,


                    success: function (result) {
                        var result1 = JSON.stringify(result);
                        obj = JSON.parse(result1);

                        if (obj.d == "true") {
                            alert("ECA Attendance Submitted Successfully..!!")

                            location.reload(); // then reload the page.


                        }
                        else if (obj.d == "false") {
                            alert("ECA Attendance can't Submitted..!!");

                            location.reload(); // then reload the page.     
                        }
                        else {
                            alert(obj.d);


                        }
                    },

                    error: function (xmlHttpRequest, textStatus, errorThrown) {

                        alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                        //  alert('Error');                       
                    },
                    //success: OnSuccess1,
                    // success: function (result) {  alert("success");   }, 
                    failure: function (result) { alert("failure"); },
                });
            });
        });
        function OnSuccess1() {
            $('[id$=hdnValue]').val('0');
            $('[id$=grdAttendanceDetails]').find('tr:has(td)').each(function () {
                $(this).remove();
            });

            // alertify.success('Update Successfully');
            ShowHide();
            return false;
        }
        function ShowP(me) {
            if (me.checked == true) {

                $('[id$=chkAbsent]').prop("checked", false);
                $('[id$=ChkNC]').prop("checked", false);
                $('[id$=chkPresent]').prop("checked", true);

            }
            else {
                $('[id$=chkPresent]').prop("checked", false);
                $('[id$=ChkNC]').prop("checked", false);
                $('[id$=chkAbsent]').prop("checked", true);


            }
        }
        function ShowA(me) {
            if (me.checked == true) {

                $('[id$=chkPresent]').prop("checked", false);
                $('[id$=ChkNC]').prop("checked", false);
                $('[id$=chkAbsent]').prop("checked", true);
            }
            else {

                $('[id$=chkPresent]').prop("checked", false);
                $('[id$=chkAbsent]').prop("checked", false);
                $('[id$=ChkNC]').prop("checked", true);

            }
        }
        function ShowNC(me) {
            if (me.checked == true) {
                $('[id$=ChkNC]').prop("checked", true);
                $('[id$=chkPresent]').prop("checked", false);
                $('[id$=chkAbsent]').prop("checked", false);
            }
            else {
                $('[id$=chkPresent]').prop("checked", true);
                $('[id$=chkAbsent]').prop("checked", false);
                $('[id$=ChkNC]').prop("checked", false);


            }
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset style="background: #fefefe; border-top: 1px solid #dde0e8; border-bottom: 1px solid #dde0e8; padding: 10px 20px; height: 100%">
        <fieldset class="boxBodyHeader">
            <asp:Label ID="Label1" runat="server"
                Text="Edit Attendance" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

        </fieldset>
        <asp:HiddenField ID="hdnValue" runat="server" />
        <br />
        <center>
            <asp:Panel ID="Panel1" runat="server" BorderColor="#E8E8E8" BorderWidth="1px" Width="100%">
                <fieldset class="boxBodyInner">
                    <div class="pull-right">
                        <a href="../Files/Edit Attendance.xlsx" class="btn btn-info btn-sm">
                            <span class="glyphicon glyphicon-download"></span>Download Format
                        </a>
                    </div>
                    <center>
                        <div>
                            <div class="form-group" style="margin-left: 35%;">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>
                                    <asp:TextBox ID="txtStudentNo" runat="server" Width="250px" autocomplete="off" placeholder="Student Name" onchange="return myFunctionGetCustomers(this);"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </center>
                    <asp:AutoCompleteExtender ServiceMethod="SearchCustomers" MinimumPrefixLength="1" CompletionInterval="0" EnableCaching="false" CompletionSetCount="6"
                        TargetControlID="txtStudentNo" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                        CompletionListHighlightedItemCssClass="itemHighlighted" ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
                    </asp:AutoCompleteExtender>

                    <div style="width: 350px">
                        <div class="pull-left">
                            <asp:FileUpload runat="server" ID="fileUploadStudent" />
                        </div>
                        <div class="pull-right">
                            <asp:LinkButton ID="btnUpload" ValidationGroup="g1" class="btn btn-info btn-sm" runat="server" OnClick="btnUpload_Click">
                         <span class="glyphicon glyphicon-upload"></span> Upload 
                            </asp:LinkButton>
                        </div>
                    </div>
                    <br />
                    <div class="panel-body" id="pnlGrid" runat="server">
                        <asp:GridView ID="grdAttendanceDetails" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Horizontal" OnRowCommand="grdAttendanceDetails_RowCommand">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>
                                <asp:BoundField DataField="Enrollment No_" HeaderText="Roll No" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="No_" HeaderText="Student No" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Student Name" HeaderText="Student Name" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Fathers Name" HeaderText="Father's Name" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Course Code" HeaderText="Course Code" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Semester" HeaderText="Semester/Yr" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Section" HeaderText="Section" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Admission Date" HeaderText="Admission Date" ItemStyle-CssClass="visible-lg" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="gdlbtnRemove" runat="server" Text="Delete" CommandName="Select" OnClientClick="return RemoveRow(this)"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderTemplate>Remove</HeaderTemplate>
                                </asp:TemplateField>

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
                    </div>

                </fieldset>
            </asp:Panel>
            <br />
            <div class="panel-body" style="border-color: #337ab7; width: 100%; border-top: groove; border-bottom: groove" id="body2">

                <div class="col-sm-12">
                    <div class="col-sm-4">
                        <div class="pull-left">
                            <asp:Label ID="lblDateFrom" runat="server" Text="From:"></asp:Label>
                        </div>
                        <div class="pull-left" style="padding-left: 3%">
                            <asp:TextBox ID="txtDateFrom" runat="server" Width="100px" Height="22px" placeholder="From Date" onkeypress="return false" onKeyDown="preventBackspace();"
                                oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDateFrom"
                                CssClass="cal_Theme1" Enabled="true" TargetControlID="txtDateFrom">
                            </asp:CalendarExtender>
                        </div>

                        <div class="pull-left" style="padding-left: 3%">

                            <asp:Label ID="lblDateTo" runat="server" Text="To:"></asp:Label>
                        </div>

                        <div class="pull-left" style="padding-left: 3%">
                            <asp:TextBox ID="txtDateTo" runat="server" Width="100px" Height="22px" placeholder="To Date" onkeypress="return false" onKeyDown="preventBackspace();"
                                oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender3" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDateTo"
                                CssClass="cal_Theme1" Enabled="true" TargetControlID="txtDateTo">
                            </asp:CalendarExtender>


                            <%--                    <asp:CheckBox runat="server" ID="chkManyDays" Onclick="ShowHide(this);" Text="Click for more then one days" Font-Bold="true" />
                    &nbsp&nbsp&nbsp&nbsp  &nbsp&nbsp&nbsp&nbsp  &nbsp&nbsp&nbsp&nbsp  &nbsp&nbsp&nbsp&nbsp --%>
                        </div>

                        <div class="clearfix"></div>
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                        <div class="pull-left">
                            <asp:Label ID="eventtyp" runat="server">Reason</asp:Label>
                        </div>
                        <div class="pull-left" style="padding-left: 2%">

                            <asp:DropDownList ID="ddlEventType" runat="server"  Width="240px" CssClass="form-control input-sm" AutoPostBack="True"  OnSelectedIndexChanged="DropDownList_Changed">
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlEventType" ErrorMessage="**" SetFocusOnError="True" ValidationGroup="g1" InitialValue="--Select--"></asp:RequiredFieldValidator>
                        </div>
                         <div class="pull-left">
                             <asp:Label ID="Label2" runat="server">Reason Type</asp:Label>
                         </div>
                          <div class="pull-left" style="padding-left: 2%">
                              <asp:DropDownList ID="ddlEventType1" runat="server" Width="240px" CssClass="form-control input-sm">
                              </asp:DropDownList>
                          </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                    </div>

                    <div class="col-sm-2">
                        <div class="pull-left" style="padding-left: 0%">
                            <asp:Label runat="server" ID="lblLecture" Text="Lecture" Font-Bold="true" Visible="true" />

                        </div>
                        <div class="pull-left" style="padding-left: 5%">
                            <asp:ListBox runat="server" ID="ddlLecture" SelectionMode="Multiple">
                                <asp:ListItem Value="1">I</asp:ListItem>
                                <asp:ListItem Value="2">II</asp:ListItem>
                                <asp:ListItem Value="3">III</asp:ListItem>
                                <asp:ListItem Value="4">IV</asp:ListItem>
                                <asp:ListItem Value="5">V</asp:ListItem>
                                <asp:ListItem Value="6">VI</asp:ListItem>
                                <asp:ListItem Value="7">VII</asp:ListItem>
                                <asp:ListItem Value="8">VIII</asp:ListItem>
                                <asp:ListItem Value="9">IX</asp:ListItem>
                                <asp:ListItem Value="10">X</asp:ListItem>
                            </asp:ListBox>
                        </div>


                    </div>

                    <div class="col-sm-6">

                        <div class="pull-left" style="padding-left: 0%">
                            <asp:CheckBox runat="server" ID="chkPresent" Onclick="ShowP(this);" Text="Present" Font-Bold="true" Checked="true" />

                        </div>
                        <div class="pull-left" style="padding-left: 3%">
                            <asp:CheckBox runat="server" ID="chkAbsent" Onclick="ShowA(this);" Text="Absent" Font-Bold="true" />

                        </div>
                         <div class="pull-left" style="padding-left: 3%">
                            <asp:CheckBox runat="server" ID="ChkNC" Onclick="ShowNC(this);" Text="NC" Font-Bold="true" />

                        </div>
                        <div class="clearfix"></div>
                        <br />
                        <div class="pull-left" style="padding-left: 0%">
                            <asp:Label ID="txtr" runat="server">Remarks</asp:Label>
                        </div>
                        <div class="pull-left" style="padding-left: 3%">
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" Width="240px" MaxLength="250" placeholder="Enter Maximum 250 Character  "></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRemarks" ErrorMessage="**" SetFocusOnError="True" ValidationGroup="g1" InitialValue="--Select--"></asp:RequiredFieldValidator>


                        </div>
                        <div class="pull-left" style="padding-left: 5%">
                            <asp:Button ID="btnSave" runat="server" OnClientClick="return false;" Height="30px" ValidationGroup="g1" class="btn btn-success" Text="Update" />


                        </div>
                    </div>

                </div>



            </div>

        </center>
        <%-- <asp:Button ID="BtnHideQuotation" runat="server" Style="display: none" />
            <asp:ModalPopupExtender runat="server" ID="ModalPopupMsg" TargetControlID="BtnHideQuotation"
                PopupControlID="PanelQuotation" BackgroundCssClass="modalbackground" RepositionMode="RepositionOnWindowScroll"
                PopupDragHandleControlID="PanelQuotation">
            </asp:ModalPopupExtender>
            <asp:Panel runat="server" ID="PanelQuotation" CssClass="modalPopupWhite">
                <br />
                <div id="divmsg" runat="server" style="text-align: center; color: Red; margin-bottom: 20px;">
                </div>
                <div style="clear: both">
                </div>
                <div style="text-align: center">
                    <asp:Button ID="btncancelpopup" Text="Ok" OnClick="btncancelpopup_Click" runat="server" />
                </div>
            </asp:Panel>--%>

        <asp:HiddenField runat="server" ID="hdnStudentNo" Value=" " />
        <asp:HiddenField runat="server" ID="HfUpdateCount" />
    </fieldset>
</asp:Content>

