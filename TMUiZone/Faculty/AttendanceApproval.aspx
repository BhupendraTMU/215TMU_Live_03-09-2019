<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="AttendanceApproval.aspx.cs" Inherits="Faculty_AttendanceApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        #confirmModalB .modal-dialog.modalPopup {
            width: 95%;
        }

        table thead tr th:first-child, .table > tbody > tr > th:first-child {
            border-left: 1px solid #60594f;
            padding: 5px 8px;
        }
    </style>










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
                                MARK ATTENDANCE APPROVAL LIST:
                            </p>
                        </b>
                    </div>
                </center>
            </div>
            <br />
            <fieldset class="boxBodyInner">
                <div class="row tmu-form ml-5 mr-5">
                    <%--    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label">Exam Type</label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlReaapear" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlReaapear_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="Main"> </asp:ListItem>
                                    <asp:ListItem Value="1" Text="Re-Appear"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label">Academic Year</label>
                            <div class="col-sm-7">
                                <asp:DropDownList ID="ddlAcademicYear" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged1" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label">Course</label>
                            <div class="col-sm-7">
                                <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4 p-0">
                        <div class="form-group clearfix">
                            <div id="divSp" runat="server" visible="false">
                                <label for="inputEmail3" style="margin-right: 5px;" class="col-form-label">Special</label>
                                <asp:CheckBox ID="Chekap" runat="server" OnCheckedChanged="Chekap_CheckedChanged" AutoPostBack="true" />

                            </div>
                            <label for="inputEmail3" class="col-form-label">Sem/Year</label>
                            <div class="col-sm-7">
                                <asp:DropDownList ID="ddlSem" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>--%>




                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtSearch" placeholder="Search By" runat="server" />
                            </td>
                            <td style="width: 20px"></td>
                            <td style="width: 80px">From Date : </td>
                            <td style="width: 20px">
                                <td>

                                    <asp:TextBox ID="txtFromDate" runat="server" />
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfromDate" Format="dd MMM yyyy">
                                    </cc1:CalendarExtender>
                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" WatermarkText="dd MMM yyyy" TargetControlID="txtfromDate">
                                    </cc1:TextBoxWatermarkExtender>
                                </td>
                                <td style="width: 20px">
                                    <td style="width: 80px">To Date : </td>
                                    <td style="width: 20px">
                                        <td>
                                            <asp:TextBox ID="txtTodate" runat="server" />
                                              <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTodate" Format="dd MMM yyyy">
                                    </cc1:CalendarExtender>
                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" WatermarkText="dd MMM yyyy" TargetControlID="txtTodate">
                                    </cc1:TextBoxWatermarkExtender>
                                        </td>
                                        <td style="width: 20px">
                                            <td>
                                                <asp:Button ID="BtnShow" runat="server" Text="Search" CssClass="btn" OnClick="BtnShow_Click" />
                                            </td>
                                            <td style="width: 20px">
                                                <td>
                                                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" OnClick="BtnSubmit_Click" CssClass="btn" />
                                                </td>
                        </tr>
                    </table>







                </div>
            </fieldset>
            <fieldset class="boxBodyInner">
                <div class="text-center" style="overflow: auto; height: 288px;">
                    <asp:GridView ID="GrdExamList" runat="server" DataKeyNames="ID" AlternatingRowStyle-CssClass="danger" PageSize="20"
                        PagerStyle-Font-Bold="true" PagerStyle-HorizontalAlign="Center" AutoGenerateColumns="false"
                        CssClass="table table-striped table-bordered table-hover" Visible="true">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex +1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Course Code" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblEnrollNo" runat="server" Text='<%# Bind("[Course]") %>'></asp:Label>



                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Subject" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubject" runat="server" Text='<%# Bind("[Subject]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lecture No" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lbllecture" runat="server" Text='<%# Bind("[Lecture]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Date" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Semester" ItemStyle-Width="3%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSem" runat="server" Text='<%# Eval("Semester") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Faculty Code" ItemStyle-Width="5%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                <ItemTemplate>
                                    <asp:Label ID="lblFaculty" runat="server" Text='<%#Eval("FacultyCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="College Code" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("CollegeCode") %>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="2%">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" Checked="false" OnCheckedChanged="chkAll_CheckedChanged" />
                                    Select All
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkStudent" runat="server" AutoPostBack="true" Enabled='<%# Eval("enab").ToString()=="false" ? false : true %>' Checked="false" OnCheckedChanged="chkStudent_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>









</asp:Content>

