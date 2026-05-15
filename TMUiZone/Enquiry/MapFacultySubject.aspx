<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="MapFacultySubject.aspx.cs" Inherits="Enquiry_MapFacultySubject" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function EnterEvent(e) {
            if (e.keyCode == 13) {
                $('[id$=btnSearch]').focus();
                $('[id$=btnSearch]').click();
                return false;
            }
        }
    </script>
     <script type="text/javascript">
         function callFeedbackMessage(inputType, inputText) {

             if (inputType == 'Error') {
                 alertify.error(inputText);
                 return false;
             }
             else if (inputType == 'Success') {
                 //alertify.confirm().set('overflow', false);
                 alertify.success("Save Successfully");
                 return false;
             }
             else {
                 alertify.log(inputText, "", 10000);
                 return false;
             }
         }
    </script>
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        .modalPopup {
            background-color: #ffffdd;
            border-width: 3px;
            border-style: solid;
            border-color: Gray;
            padding: 3px;
            width: 30%;
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

            .modalPopup td {
                text-align: left;
            }

        .redBorder {
            border: 1px solid red;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Subject Details" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <fieldset class="boxBodyHeader">
    </fieldset>
    <asp:UpdatePanel ID="main" runat="server">
        <ContentTemplate>
            <fieldset class="boxBodyInner">
                <asp:UpdatePanel ID="updCourseSubj" runat="server">

                    <ContentTemplate>
                       <table runat="server" width="100%" id="Subject" >
                           <tr>
                               
                               <td> 
                                   <asp:DropDownList ID="ddlCourse" AutoPostBack="true" Width="300px" Height="30px"  runat="server" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"></asp:DropDownList>

                               </td>
                               <td>
                                   <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse" ErrorMessage="*" ForeColor="Red" ValidationGroup="vgAdd"
                                        Font-Bold="true" InitialValue=""></asp:RequiredFieldValidator>
                               </td>
                               <td>
                                     <asp:DropDownList ID="ddlSemester" Width="300px" Height="30px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlSemester" runat="server" ControlToValidate="ddlSemester" ErrorMessage="*" ForeColor="Red" ValidationGroup="vgAdd"
                                        Font-Bold="true" InitialValue=""></asp:RequiredFieldValidator>
                               </td>
                               <td>
                                     <asp:DropDownList ID="ddlSubject" Width="300px" Height="30px"  runat="server"></asp:DropDownList>
                               </td>
                               <td>
                                   <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ControlToValidate="ddlSubject" ErrorMessage="*" ForeColor="Red" ValidationGroup="vgAdd"
                                        Font-Bold="true" InitialValue=""></asp:RequiredFieldValidator>
                               </td>
                               <td>
                                   <asp:LinkButton ID="btnAdd" type="button" class="btn btn-info btn " runat="server" ValidationGroup="vgAdd" OnClick="btnAdd_Click" OnClientClick="return SAVE();" Width="80px" Height="30px">             
                             <span class="glyphicon glyphicons-search"></span>Add </asp:LinkButton>
                              </td>
                           </tr>

                           <tr>
                               <td>
                                   <br />
                                     <asp:DropDownList ID="ddlSearch" placeholder="Search" runat="server" Width="300px" Height="30px"></asp:DropDownList>
                               </td>
                               <td>
                                    <br />
                                   <%-- <asp:RequiredFieldValidator ID="rfvddlSearch" runat="server" ControlToValidate="ddlSearch" InitialValue="0"
                                        Font-Bold="true" ValidationGroup="Search" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true" BorderColor="Red"></asp:RequiredFieldValidator>--%>
                               </td>
                               <td>
                                    <br />
                                    <asp:TextBox ID="txtSearch" runat="server" placeholder="Enter Your Text"  Width="300px" Height="30px" onkeypress="return EnterEvent(event)"></asp:TextBox>
                               </td>
                               <td>
                                    <br />
                                   <%--<asp:RequiredFieldValidator ID="rfvtxtSearch" runat="server" ControlToValidate="txtSearch" ValidationGroup="Search"
                                        Font-Bold="true" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                               </td>
                               <td colspan="2">
                                    <br />
                                     <button id="btnSearch" type="button" class="btn btn-info btn" runat="server" onserverclick="btnSearch_Click" validationgroup="Search" width="80px" height="30px">
                                        <span ></span>Search</button>
                               </td>
                               <td >
                                    <br />
                                   <button id="btnRefresh" type="button" class="btn btn-info btn" runat="server" onserverclick="btnRefresh_Click" width="80px" height="30px">
                                        <span ></span>Refresh</button>
                               </td>
                               
                           </tr>
                           <tr>
                               <td colspan="6">
                                   <div class="table-responsive">
                                   <asp:GridView ID="grvGrid" runat="server"></asp:GridView>
                                       </div>
                               </td>
                           </tr>
                       </table>
                      
                           
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlCourse" EventName="SelectedIndexChanged" />
                        <asp:PostBackTrigger ControlID="btnAdd" />
                    </Triggers>
                </asp:UpdatePanel>
                
            </fieldset>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grdFacultySubject" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" PageSize="20"
                                            DataKeyNames="ID" EmptyDataText="There are no data records to display." AllowPaging="True" OnPageIndexChanging="grdFacultySubject_PageIndexChanging" OnRowDeleting="grdFacultySubject_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl. No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="Course" HeaderText="Course" SortExpression="CourseCode" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg">
                                                    <HeaderStyle CssClass="visible-lg" />
                                                    <ItemStyle CssClass="visible-lg" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SemYear" HeaderText="Semester/Year" SortExpression="SemYear" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg">
                                                    <HeaderStyle CssClass="visible-lg" />
                                                    <ItemStyle CssClass="visible-lg" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SubjectCode" HeaderText="Subject Code" SortExpression="SubjectCode" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs">
                                                    <HeaderStyle CssClass="hidden-xs" />
                                                    <ItemStyle CssClass="hidden-xs" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs">
                                                    <HeaderStyle CssClass="hidden-xs" />
                                                    <ItemStyle CssClass="hidden-xs" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FacultyCode" HeaderText="FacultyCode" SortExpression="FacultyCode" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                    <HeaderStyle CssClass="visible-lg" />
                                                    <ItemStyle CssClass="visible-lg" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Name" HeaderText="Faculty Name" SortExpression="Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                    <HeaderStyle CssClass="visible-lg" />
                                                    <ItemStyle CssClass="visible-lg" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Assign" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="cbRows" runat="server" AutoPostBack="True" OnCheckedChanged="cbRows_CheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowDeleteButton="True" />
                                            </Columns>
                                            <AlternatingRowStyle CssClass="danger" />
                                        </asp:GridView>

                                    </div>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>




            <asp:LinkButton Text="" ID="Add" runat="server" />
            <asp:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="Add"
                CancelControlID="btnClose" BackgroundCssClass="modalBackground" Drag="true" PopupDragHandleControlID="pnlPopup">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">

                <div class="panel-heading">
                    <center>
                        <div class="panel-title" style="fit-position: center;">
                            <b>
                                <p style="color: white; font-size: 25px;"></p>
                            </b>
                        </div>
                    </center>

                </div>
                <div>
                    <table width="100%">
                        <tr>
                            <td style="width: 90%">
                                <div style="text-align: center">
                                    <asp:Label runat="server" ID="lblMsg" ForeColor="Red" Font-Bold="true"></asp:Label>
                                </div>
                            </td>
                            <td style="width: 10%">
                                <div style="text-align: right">
                                    <asp:Button ID="btnClose" runat="server" Text="X" Width="30px" CssClass="button" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
        </Triggers>
    </asp:UpdatePanel>
    
</asp:Content>

