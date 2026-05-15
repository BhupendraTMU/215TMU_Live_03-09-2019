<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="IndentList.aspx.cs" Inherits="IndentList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .modalPopup {
            background-color: #ffffdd;
            border-width: 3px;
            border-style: solid;
            border-color: Gray;
            padding: 3px;
            width: 90%;
        }

            .modalPopup .header {
                background-color: #2FBDF1;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .body {
                min-height: 50px;
                line-height: 30px;
                text-align: center;
                padding: 5px;
            }

            .modalPopup .footer {
                padding: 3px;
            }

            .modalPopup .button {
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                background-color: red;
                border: 1px solid #5C5C5C;
            }
        /*.modalPopup td
    {
        text-align:left;
    }*/
        .redBorder {
            border: 4px solid white;
        }
    </style>
       <script language="javascript" type="text/javascript">
           function callFeedbackMessage(inputType, inputText) {

               if (inputType == 'Error') {
                   alertify.error(inputText);
                   return false;
               }
               else if (inputType == 'Success') {                   
                   alertify.success(inputText);
                   return false;
               }
               else {
                   alertify.log(inputText, "", 10000);
                   return false;
               }
           }
           
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Course Plan List" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
    </fieldset>
    <fieldset class="boxBodyHeader">
    </fieldset>
    <fieldset class="boxBodyInner">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <fieldset class="boxBodyInner">
                    <asp:Panel ID="pnlHeader" runat="server" BorderWidth="1px">
                        <table cellpadding="0px" cellspacing="10px" width="100%">
                            <caption>
                                <br />
                                <tr>

                                    <td>&nbsp Academic Year</td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:DropDownList ID="ddlAcademicYear" Width="200px" Height="20px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                    <td style="width: 20px"></td>
                                    <td>
                                        <label>Faculty </label>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:DropDownList ID="ddlFaculty" runat="server" AutoPostBack="true" Height="20px" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 20px"></td>
                                    <td>Status </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="1">Pending</asp:ListItem>
                                            <asp:ListItem Value="2">Approved</asp:ListItem>
                                            <asp:ListItem Value="3">Reject</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td style="width: 10px"></td>

                                </tr>

                                <tr>
                                    <td colspan="11" style="height: 10px">
                                        <asp:HiddenField runat="server" ID="hfDocumentNo" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp Course
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" Height="20px" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" Width="200px"></asp:DropDownList>
                                    </td>
                                    <td style="width: 20px"></td>
                                    <td>Subject</td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:DropDownList ID="ddlSubject" Width="200px" Height="20px" runat="server"></asp:DropDownList>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td></td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:Button ID="btnShow" runat="server" Text="Show Data" class="btn-info" Width="100px" OnClick="btnShow_Click" ValidationGroup="h1" />
                                        &nbsp&nbsp
                                    </td>
                                    <td style="width: 10px"></td>
                                </tr>

                            </caption>
                        </table>
                        <br />
                    </asp:Panel>
                    <br />
                    <asp:Panel runat="server" ID="pnlAppliedCoursePlan">
                        <asp:GridView ID="grdFacultyCoursePlanHeader" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Horizontal" EmptyDataText="There are no data records to display." DataKeyNames="No_" OnSelectedIndexChanged="OnSelectedIndexChanged">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>
                                <asp:BoundField DataField="No_" HeaderText="Document No" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Academic Year" HeaderText="Academic Year" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Course Code" HeaderText="Course Code" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Subject Code" HeaderText="Subject" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <%--<asp:BoundField DataField="Subject Type" HeaderText="Subject Type" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />--%>
                                <asp:BoundField DataField="SemesterYear" HeaderText="Semester/Year" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Section" HeaderText="Section" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Group" HeaderText="Group" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Batch" HeaderText="Batch" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="PStatus" HeaderText="Plan Status" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:ButtonField Text="Details" CommandName="Select" />
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
                    <asp:LinkButton Text="" ID="lnkFollowUP" runat="server" />
                    <asp:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="lnkFollowUP"
                        CancelControlID="btnClose" BackgroundCssClass="modalBackground" Drag="true" PopupDragHandleControlID="pnlPopup">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                        <asp:Panel runat="server" ID="pnllineHeader" CssClass="Header">
                            <center><b>Course Plan Details </b></center>
                        </asp:Panel>
                        <asp:Panel ID="pnlSearchLine" runat="server" BorderWidth="2px">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <div class="col-lg-12">
                                            <div class="col-lg-3" style="padding-right: 0px; padding-top: 12px;">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="glyphicon glyphicon-copyright-mark"></span></span>
                                                        <asp:DropDownList ID="ddlSearch" CssClass="form-control input-sm" placeholder="Search" runat="server" Height="35px"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-5" style="padding-left: 0px; padding-top: 12px;">
                                                <div class="form-group has-feedback">
                                                    <input type="text" class="form-control" placeholder="Search Text" runat="server" id="txtSearch" />
                                                    <i class="glyphicon glyphicon-user form-control-feedback"></i>
                                                </div>
                                            </div>
                                            <div class="col-lg-4" style="padding-left: 0px; font-size: 20px; padding-top: 10px;">
                                                <div class="col-lg-4">
                                                    <button id="btnSearch" type="button" class="btn btn-info btn" runat="server" onserverclick="btnSearch_Click">
                                                        <span class="glyphicon glyphicon-search"></span>Search 
                                                    </button>
                                                </div>
                                                <div class="col-lg-4" style="padding-left: 50px;">
                                                    <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/images/excel.jpg" OnClick="btnExport_Click" Width="40px" Height="30px"></asp:ImageButton>
                                                </div>

                                            </div>
                                        </div>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btnClose" runat="server" Text="X" Width="30px" CssClass="button" Height="30px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlgrdl" runat="server" BorderWidth="2px" style="overflow-x:scroll; max-height:520px;">
                            <table width="100%" runat="server" class="redBorder">
                                <tr style="height: 45px" valign="middle">
                                    <td width="70%" align="right">
                                        <asp:Label runat="server" ID="lblError"></asp:Label>
                                        <asp:TextBox runat="server" placeholder="Remarks" id="txtRemarks" MaxLength="250" Width="80%"></asp:TextBox> 
                                        <asp:RequiredFieldValidator ID="rfvTxtRemarks" runat="server" ValidationGroup="vgReject" ControlToValidate="txtRemarks" ErrorMessage="Enter Remarks" ></asp:RequiredFieldValidator>

                                    </td>
                                    <td align="right" width="20%">
                                        <asp:Button ID="btnApproved" runat="server" Text="Approved" Width="100px" BackColor="LawnGreen" Height="35px" OnClick="btnApproved_Click" />
                                    </td>
                                    <td align="right" width="10%">
                                        <asp:Button ID="btnReject" runat="server" Text="Rejected" Width="100px" BackColor="Red" Height="35px" OnClick="btnReject_Click" ValidationGroup="vgReject"/>
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="3">
                                        <div>
                                            <asp:GridView ID="grdFacultyCoursePlanLine" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                                                GridLines="Both"
                                                EmptyDataText="There are no data records to display.">
                                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                                <Columns>
                                                    <asp:BoundField DataField="Unit Name" HeaderText="Unit Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                    <asp:BoundField DataField="Chapter Name" HeaderText="Chapter Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                    <asp:TemplateField HeaderText="Topic" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <div style="width: 150px; overflow: auto; white-space: nowrap; text-overflow: clip">
                                                                <%# Eval("Topics") %>
                                                            </div>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Period" HeaderText="Period" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                    <asp:BoundField DataField="No Of Minuites" HeaderText="No. Of Minuites" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                    <asp:BoundField DataField="Scheduled Date" HeaderText="Scheduled Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                    <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
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
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>



                    </asp:Panel>
                </fieldset>
            </ContentTemplate>
             <Triggers >
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        </asp:UpdatePanel>
    </fieldset>
</asp:Content>

