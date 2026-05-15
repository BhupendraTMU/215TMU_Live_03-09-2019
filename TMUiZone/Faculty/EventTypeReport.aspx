<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="EventTypeReport.aspx.cs" Inherits="Faculty_EventTypeReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .example-print {
            display: none;
        }

        @media print {
            .example-screen {
                display: none;
            }

            .example-print {
                display: block;
            }
        }
    </style>

    <script type="text/javascript">

        function PrintDiv() {

            var divToPrint = document.getElementById('printarea');

            var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px, margin:0mm');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
        }
    </script>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ty" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="fe" runat="server" UpdateMode="Conditional">
        <ContentTemplate>


            <div class="panel-heading" style="background-color: #2b5b69">
                <center>
                    <div class="panel-title" style="fit-position: center;">
                        <b>
                            <p style="color: white; font-size: 20px">
                                Event Type Report
                            
                            </p>
                        </b>
                    </div>
                       
                </center>
               
            </div>
            <br />
            <fieldset class="boxBodyInner">
                <div class="row tmu-form ml-5 mr-5">
                    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label">Academic Year</label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlAcademicYear" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label">College Code</label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="DdlCollege" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlCollege_SelectedIndexChanged" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label">Course</label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label">Event Type</label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlEventType" runat="server" onchange='CheckColors(this.value);' Width="240px" CssClass="form-control input-sm">
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlEventType" ErrorMessage="**" SetFocusOnError="True" ValidationGroup="g1" InitialValue="--Select--"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 p-0 mr-10">
                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label">From :</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" placeholder="From Date" onkeypress="return false" onKeyDown="preventBackspace();"
                                        oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender2" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDateFrom"
                                        CssClass="cal_Theme1" Enabled="true" TargetControlID="txtDateFrom">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label">To :</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" placeholder="To Date" onkeypress="return false" onKeyDown="preventBackspace();"
                                        oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender3" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDateTo"
                                        CssClass="cal_Theme1" Enabled="true" TargetControlID="txtDateTo">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">
                            <asp:Button ID="BtnShow" runat="server" Text="Show" CssClass="btn btn-warning" OnClick="BtnShow_Click" />
                         <%--   <asp:Button ID="BtnPrint" OnClientClick="PrintDiv();" Visible="false" runat="server" Text="Print" CssClass="btn-warning" />--%>

                        </div>

                        
                    </div>
                </div>
                <div id="printarea">
                    <asp:Panel ID="Heading" Class="example-print" runat="server">
                        <div class="panel-heading" style="background-color: #2b5b69">
                            <center>
                                <div class="panel-title" style="fit-position: center;">
                                    <b>
                                        <p style="color: white; font-size: 20px">
                                            Event Type Report
                                        </p>
                                    </b>
                                </div>
                            </center>

                        </div>
                    </asp:Panel>
                    <fieldset class="boxBodyInner">
                         <div class="col-sm-12 p-0 text-right mr-20">
                                 <asp:ImageButton ID="BtnPrint"  runat="server" ImageUrl="~/images/pdf.jpg" OnClientClick="PrintDiv();" Width="40px" Height="30px" Visible="false"></asp:ImageButton>
                             </div>
                        <asp:GridView ID="GrdEventType" runat="server" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Data to display">
                            <Columns>
                                <asp:TemplateField HeaderText="S.no">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex +1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>

                    </fieldset>
            </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

