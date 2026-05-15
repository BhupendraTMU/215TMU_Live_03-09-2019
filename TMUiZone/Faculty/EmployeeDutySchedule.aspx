<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="EmployeeDutySchedule.aspx.cs" Inherits="Faculty_EmployeeDutySchedule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ty" runat="server"></asp:ScriptManager>


    <div class="panel-heading" style="background-color: #2b5b69">
        <center>
            <div class="panel-title" style="fit-position: center;">
                <b>
                    <p style="color: white; font-size: 20px">
                        EXAM DUTY SCHEDULE
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
                    <label for="inputEmail3" class="col-form-label">Exam Type</label>
                    <div class="col-sm-8">
                        <asp:DropDownList ID="DdlExamType" CssClass="form-control" OnSelectedIndexChanged="DdlExamType_SelectedIndexChanged" AutoPostBack="true" runat="server">
                            <asp:ListItem Value="1" Text="Internal"></asp:ListItem>
                            <asp:ListItem Value="2" Text="External"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
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
                    <label for="inputEmail3" class="col-form-label">Exam Method</label>

                    <div class="col-sm-6">
                        <asp:DropDownList ID="DdlExamMethod" runat="server" CssClass="form-control" OnSelectedIndexChanged="DdlExamMethod_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-sm-4 p-0">
                <div class="form-group clearfix">
                    <label for="inputEmail3" class="col-form-label">Date</label>
                    <div class="col-sm-7">
                        <asp:TextBox ID="txtDateD" Enabled="true" runat="server" CssClass="form-control" onkeypress="return false"
                            onKeyDown="preventBackspaceD();" OnTextChanged="txtDateD_TextChanged"
                            AutoPostBack="true" oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" Format="dd MMM yyyy" runat="server" 
                            CssClass="cal_Theme1" Enabled="true" TargetControlID="txtDateD">
                        </asp:CalendarExtender>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 p-0 text-right">
                <asp:Button ID="BtnShow" runat="server" Text="Show" CssClass="btn" OnClick="BtnShow_Click" />
            </div>
        </div>
        <fieldset class="boxBodyInner">


            <div class="text-center">
                <asp:GridView ID="GrdExamSchedule" runat="server" CssClass="table table-striped table-bordered"
                    AutoGenerateColumns="false" Style="width: 95%; margin-left: 2%; margin-right: 2%" EmptyDataText="No Data to display">
                    <Columns>
                        <asp:TemplateField HeaderText="S.no">
                            <ItemTemplate>
                                <%# Container.DataItemIndex +1 %>
                                <asp:HiddenField ID="hdShift" runat="server" Value='<%#Eval("Shift")%>'></asp:HiddenField>

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Room No">
                            <ItemTemplate>
                                <asp:Label ID="lblRoomNo" runat="server" Text='<%#Eval("[Room No_]")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Exam Center Code">
                            <ItemTemplate>
                                <asp:Label ID="lblCenterCode" runat="server" Text='<%#Eval("[Exam Center Code]")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Exam Method">
                            <ItemTemplate>
                                <asp:Label ID="lblMethod" runat="server" Text='<%#Eval("[Exam Method]")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shift">
                            <ItemTemplate>
                                <asp:Label ID="lblShift" runat="server" Text='<%#Eval("[Shift]")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Exam Date">
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server" Text='<%#Eval("[Exam Date1]")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("[Duty]")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>

                            <ItemTemplate>
                                <asp:LinkButton ID="lnkAp" Text="Approve" CssClass="btn" Visible="false" OnClick="lnkAp_Click" runat="server"></asp:LinkButton>

                                <asp:LinkButton ID="lnkRj" Text="Reject" CssClass="btn" Visible='<%#Eval("ChkV")%>' runat="server" OnClick="lnkRj_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </fieldset>
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
                            <div class="col-md-12 p-0">
                                <div class="col-sm-4 col-md-3">
                                    <label>Remarks</label>
                                </div>
                                <div class="col-sm-8 col-md-9 form-group">
                                    <asp:TextBox ID="txtRemarks" CssClass="form-control" MaxLength="150" runat="server"></asp:TextBox>
                                </div>
                            </div>


                            <div class="modal-footer">
                                <asp:Button ID="BtnYes" runat="server" OnClick="BtnYes_Click" Text="Yes"
                                    UseSubmitBehavior="false" data-dismiss="modal" class="btn btn-success" />
                                <button type="button" class="btn btn-danger" data-dismiss="modal">No</button>
                            </div>


                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

