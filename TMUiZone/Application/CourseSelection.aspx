<%@ Page Title="" Language="C#" MasterPageFile="~/Application/IndexMaster.master" AutoEventWireup="true" CodeFile="CourseSelection.aspx.cs" Inherits="Application_CourseSelection" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        body, td, th {
            font-family: Verdana, Geneva, sans-serif;
            font-size: 15px;
        }

        .checkboxlist input {
            margin-bottom: 5px;
            margin-top: 5px;
            margin-right: 10px !important;
        }

        .checkboxlist label {
            font-family: 'Times New Roman';
            margin-bottom: 5px;
            margin-top: 5px;
            color: black !important;
            margin-right: 8px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <fieldset class="boxBodyInner">
        <asp:UpdatePanel ID="pnlupdate" runat="server">
            <ContentTemplate>

                <table cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td style="height: 10px">
                            <fieldset class="boxBodyInner">
                                <asp:Label ID="Label3" runat="server"
                                    Text="SPECILIZATIONS LIST" Font-Size="12pt" ForeColor="#093A62" Width="100%" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                <asp:Label ID="bnnm" runat="server" Visible="false" Width="100%"></asp:Label>
                            </fieldset>

                            <fieldset class="boxBodyInner">
                                <asp:Label ID="Label1" runat="server"
                                    Text="MAJOR SPECILIZATIONS" Font-Size="12pt" ForeColor="#093A62" Width="100%" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>


                                <div class="col-sm-32" style="width: 100%;">
                                    <asp:RadioButtonList ID="drpStreamMajor" CssClass="checkboxlist" OnSelectedIndexChanged="drpStreamMajo_SelectedIndexChanged" class="listItem" RepeatLayout="Table" onClick="return checkboxes();" RepeatColumns="2" CellSpacing="4" RepeatDirection="Vertical" runat="server" Width="100%" AutoPostBack="true">
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-sm-32" style="width: 100%">
                                    <asp:GridView ID="grdMajor" runat="server" Style="width: 99%" Visible="true" AutoGenerateColumns="false"
                                        CssClass="table table-striped table-bordered table-hover" AlternatingRowStyle-CssClass="danger" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex +1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Program Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblProgramCode" runat="server" Text='<%# Bind("[Course]") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Course Code ">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblPracticalCode" runat="server" Text='<%# Bind("[SubjectCode]") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Course Name ">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblPracticalName" runat="server" Text='<%# Bind("[SubName]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Semester" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblSemester" runat="server" Text='<%# Bind("[Semester]") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Credit" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblCredit" runat="server" Text='<%# Bind("[Credit]") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Course Type" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblDE" runat="server" Text='<%# Bind("[Classification]") %>'></asp:Label>
                                                    <asp:HiddenField ID="Hftypeofcourse" Value='<%# Eval("[Type Of Course]") %>' runat="server" />
                                                    <asp:HiddenField ID="HfACYear" Value='<%# Eval("[Academic Year]") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <%-- <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Select 
                                                </HeaderTemplate>
                                                <ItemTemplate>

                                                    <asp:CheckBox ID="chkStudent" runat="server" AutoPostBack="true" Checked='<%# Eval("Checked").ToString().Equals("True") %>' Enabled='<%# Eval("Checked").ToString().Equals("False") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </fieldset>

                            <fieldset class="boxBodyInner">
                                <asp:Label ID="Label2" runat="server"
                                    Text="MINOR SPECILIZATIONS" Font-Size="12pt" ForeColor="#093A62" Width="99%" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                                <div class="col-sm-32" style="width: 100%;">
                                    <asp:RadioButtonList ID="drpchkMinor" CssClass="checkboxlist" OnSelectedIndexChanged="drpchkMinor_SelectedIndexChanged" class="listItem" RepeatLayout="Table" onClick="return checkboxes();" RepeatColumns="2" CellSpacing="4" RepeatDirection="Vertical" runat="server" Width="100%" AutoPostBack="true">
                                    </asp:RadioButtonList>
                                </div>

                                <div class="col-sm-32" style="width: 100%">
                                    <asp:GridView ID="GrdMinor" runat="server" Style="width: 99%" Visible="true" AutoGenerateColumns="false"
                                        CssClass="table table-striped table-bordered table-hover" AlternatingRowStyle-CssClass="danger" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex +1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Program Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblProgramCode" runat="server" Text='<%# Bind("[Course]") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Course Code ">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblPracticalCode" runat="server" Text='<%# Bind("[SubjectCode]") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Course Name ">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblPracticalName" runat="server" Text='<%# Bind("[SubName]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Semester" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblSemester" runat="server" Text='<%# Bind("[Semester]") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Credit" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblCredit" runat="server" Text='<%# Bind("[Credit]") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Course Type" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblDE" runat="server" Text='<%# Bind("[Classification]") %>'></asp:Label>
                                                    <asp:HiddenField ID="Hftypeofcourse" Value='<%# Eval("[Type Of Course]") %>' runat="server" />
                                                    <asp:HiddenField ID="HfACYear" Value='<%# Eval("[Academic Year]") %>' runat="server" />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           

                                        </Columns>
                                        <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </fieldset>
                            <fieldset class="boxBodyInner">
                                <asp:Label ID="Label5" runat="server"
                                    Text="MULTIDISCIPLINARY MINOR SPECILIZATIONS" Font-Size="12pt" ForeColor="#093A62" Width="100%" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>


                                <div class="col-sm-32" style="width: 100%">
                                    <asp:RadioButtonList ID="chkmulti" CssClass="checkboxlist" OnSelectedIndexChanged="chkmulti_SelectedIndexChanged" class="listItem" RepeatLayout="Table" onClick="return checkboxes();" RepeatColumns="2" CellSpacing="4" RepeatDirection="Vertical" runat="server" Width="100%" AutoPostBack="true">
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-sm-32" style="width: 100%">
                                    <asp:GridView ID="grdMultiDesc" runat="server" Style="width: 99%" Visible="true" AutoGenerateColumns="false"
                                        CssClass="table table-striped table-bordered table-hover" AlternatingRowStyle-CssClass="danger" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex +1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Program Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblProgramCode" runat="server" Text='<%# Bind("[Course]") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Course Code ">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblPracticalCode" runat="server" Text='<%# Bind("[SubjectCode]") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Course Name ">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblPracticalName" runat="server" Text='<%# Bind("[SubName]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Semester" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblSemester" runat="server" Text='<%# Bind("[Semester]") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Credit" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblCredit" runat="server" Text='<%# Bind("[Credit]") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Course Type" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblDE" runat="server" Text='<%# Bind("[Classification]") %>'></asp:Label>
                                                    <asp:HiddenField ID="Hftypeofcourse" Value='<%# Eval("[Type Of Course]") %>' runat="server" />
                                                    <asp:HiddenField ID="HfACYear" Value='<%# Eval("[Academic Year]") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            

                                        </Columns>
                                        <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </fieldset>

                            <fieldset class="boxBodyInner">
                                <asp:Label ID="Label4" runat="server"
                                    Text="CORE COURSE/VAC/AEC/SEC/PBL/INTERSHIP/RESEARCH PROJECT" Font-Size="12pt" ForeColor="#093A62" Width="100%" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>



                                <div class="col-sm-32" style="width: 100%">
                                    <asp:GridView ID="grdCoreCourse" runat="server" Style="width: 99%" Visible="true" AutoGenerateColumns="false"
                                        CssClass="table table-striped table-bordered table-hover" AlternatingRowStyle-CssClass="danger" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex +1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Program Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblProgramCode" runat="server" Text='<%# Bind("[Course]") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Course Code ">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblPracticalCode" runat="server" Text='<%# Bind("[SubjectCode]") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Course Name ">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblPracticalName" runat="server" Text='<%# Bind("[SubName]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Semester" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblSemester" runat="server" Text='<%# Bind("[Semester]") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Credit" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblCredit" runat="server" Text='<%# Bind("[Credit]") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Course Type" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblDE" runat="server" Text='<%# Bind("[Classification]") %>'></asp:Label>
                                                    <asp:HiddenField ID="Hftypeofcourse" Value='<%# Eval("[Type Of Course]") %>' runat="server" />
                                                    <asp:HiddenField ID="HfACYear" Value='<%# Eval("[Academic Year]") %>' runat="server" />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category of Course" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblCourseCategory" runat="server" Text='<%# Bind("[Course Category]") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </fieldset>









                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-info" OnClientClick="return confirm('Do want to submit data ?');" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                </table>

            </ContentTemplate>


        </asp:UpdatePanel>
    </fieldset>



</asp:Content>

