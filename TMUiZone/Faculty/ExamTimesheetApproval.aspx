<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="ExamTimesheetApproval.aspx.cs" Inherits="Faculty_ExamTimesheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <style>
        .rbl input[type="radio"] {
            margin-left: 5px;
            margin-right: 1px;
        }
         </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ty" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="fe" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <fieldset class="boxBodyInner">
                <div class="row tmu-form ml-12 mr-6">
                    <div class="col-sm-2 p-0">
                        <p style="color: black; font-size: 20px">EXAM SHEET</p>
                    </div>
                   
                    <div class="col-sm-10 p-0">
                        <asp:RadioButtonList ID="Rblist" runat="server"  AutoPostBack="true" RepeatDirection="Horizontal"  OnSelectedIndexChanged="Rblist_SelectedIndexChanged" CssClass="rbl">
                           
                            <asp:ListItem Value="1" Text="Released" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Approved by HOD"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Approved by Principal"></asp:ListItem>
                            <asp:ListItem Value="5" Text="Rejected by Principal"></asp:ListItem>
                            <asp:ListItem Value="6" Text="Approved By COE"></asp:ListItem>
                            <asp:ListItem Value="7" Text="Rejected by COE"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>


                </div>
            </fieldset>


            <br />
            <fieldset class="boxBodyInner">
                 <div class="row tmu-form ml-5 mr-5">
                    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label">Open Elective</label>
                            <div class="col-sm-6">
                                <asp:CheckBox ID="ChkOpen" runat="server"  OnCheckedChanged="ChkOpen_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                            </div>
                            </div>
                        </div>
                     </div>
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
                            <label for="inputEmail3" class="col-form-label">Course</label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label">Sem/Year</label>
                            <div class="col-sm-7">
                                <asp:DropDownList ID="ddlSem" CssClass="form-control" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                   
                                 
                                </asp:DropDownList>
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

                    <div class="col-sm-13 p-0 text-right">
                         <asp:CheckBox ID="CHKSpecial" Text="Special Date Sheet" Font-Bold="true" Width="145px" runat="server"  />
                       

                        <asp:Button ID="BtnShow" runat="server" Text="Show" CssClass="btn" OnClick="BtnShow_Click" />
                        <asp:Button ID="BtnSubmit" runat="server" Text="Approve"  Visible="false" OnClick="BtnSubmit_Click" CssClass="btn" />
                        <asp:Button ID="BtnReject" runat="server"  Text="Reject" Visible="false"  OnClick="BtnReject_Click" CssClass="btn" />
                    </Div>

                </div>


                    <div class="text-center">
                        <br />
                        <asp:GridView ID="GrdExamTimeSheet" runat="server" CssClass="table table-striped table-bordered table-hover" OnRowDataBound="GrdExamTimeSheet_RowDataBound" Style="width: 95%; margin-left: 2%; margin-right: 2%" EmptyDataText="No Data to display">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.no">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex +1 %>
                                        <asp:HiddenField ID="hfstatus" Value='<%# Eval("[Status]") %>' runat="server" />
                                           <asp:HiddenField ID="hfsemester" Value='<%# Eval("[SemYear]") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

