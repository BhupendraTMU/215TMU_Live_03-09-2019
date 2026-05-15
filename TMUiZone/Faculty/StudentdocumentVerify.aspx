<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentdocumentVerify.aspx.cs" EnableEventValidation="false" Inherits="Faculty_StudentdocumentVerify" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        tr.myclass a {
            padding-right: 7px;
            padding-left: 7px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset>
        <div class="text-right" style="padding-left: 250px">

            <asp:Button ID="BtnSubmit" runat="server" Text="Approved" OnClick="BtnSubmit_Click" ForeColor="White" CssClass="btn" BackColor="#ff9900" />
            <asp:Button ID="BtnRejected" runat="server" Text="Rejected" ForeColor="White" OnClick="BtnRejected_Click" CssClass="btn" BackColor="#ff9900" />
            <asp:Button ID="BtnExporttoExel" runat="server" Text="Export To Excel" ForeColor="White" OnClick="BtnExporttoExel_Click" CssClass="btn" BackColor="#ff9900" />

        </div>
    </fieldset>
    <fieldset>
        <div class="row" style="margin-top: 25px; align-items: center;">

            <!-- Approval Status -->
            <div class="col-md-2">
                <asp:Label ID="Label2" runat="server" Text="Approval Status:"
                    Font-Bold="true"></asp:Label>
                <asp:DropDownList ID="drpstatus" runat="server"
                    CssClass="form-control"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="drpstatus_SelectedIndexChanged">
                    <asp:ListItem Text="All" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Approved" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Rejected" Value="4"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <!-- Student No -->


            <!-- College Code -->
            <div class="col-md-2">
                <asp:Label ID="Label1" runat="server" Text="College Code"
                    Font-Bold="true"></asp:Label>
                <asp:DropDownList ID="drpCollageCode" runat="server"
                    CssClass="form-control"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="drpCollage_SelectedIndexChanged">
                </asp:DropDownList>
            </div>

            <!-- Course -->
            <div class="col-md-2">
                <asp:Label ID="Label3" runat="server" Text="Course"
                    Font-Bold="true"></asp:Label>
                <asp:DropDownList ID="drpCourse" runat="server"
                    CssClass="form-control"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="drpCourse_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <asp:Label ID="Label4" runat="server" Text="Student No.:"
                    Font-Bold="true"></asp:Label>
                <asp:TextBox ID="txtstudentNo" runat="server"
                    CssClass="form-control"
                    AutoPostBack="true"
                    OnTextChanged="txtstudentNo_TextChanged">
                </asp:TextBox>
            </div>
            <!-- Button -->
            <div class="col-md-2" style="margin-top: 22px;">
                <asp:Button ID="btnexporttoexcel" runat="server"
                    Text="Document Download"
                    CssClass="btn btn-warning btn-block"
                    OnClick="btnDocumentDownload_Click" />
            </div>

        </div>
    </fieldset>

    <fieldset class="boxBodyInner">
        <br />
        <div style="overflow: scroll">
            <asp:GridView ID="grddocumentverificatiolist" runat="server" DataKeyNames="ID" OnPageIndexChanging="grddocumentverificatiolist_PageIndexChanging" AlternatingRowStyle-CssClass="danger" PageSize="10"
                AllowPaging="true" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" Visible="true">
                <PagerSettings Mode="NumericFirstLast" PageButtonCount="6" FirstPageText="First" LastPageText="Last" />
                <PagerSettings Mode="NumericFirstLast" />
                <PagerStyle CssClass="myclass" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <%# Container.DataItemIndex +1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student Number" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lblemployeecode" runat="server" Text='<%# Bind("Student_Number") %>'></asp:Label>
                            <asp:HiddenField ID="Hfemployeecode" Value='<%# Eval("Student_Number") %>' runat="server" />
                            <asp:HiddenField ID="Hfhodname" Value='<%# Eval("Student_Number") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--  <asp:TemplateField HeaderText="View" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbutton" OnClick="lnkbutton_Click" runat="server">View</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                        <ItemTemplate>
                            <asp:Label ID="lblstudentName" runat="server" Text='<%# Eval("Student_Name") %>' Style="text-transform: uppercase;" Width="150px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Programme" ItemStyle-Width="3%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                        <ItemTemplate>
                            <asp:Label ID="lblprogram" runat="server" Text='<%# Eval("Coursename") %>' Style="text-transform: uppercase;" Width="150px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="College Name" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lblStudentCategory" runat="server" Text='<%#Eval("Collegecode") %>' Style="text-transform: uppercase;" Width="150px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Academic Year" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lblAcademicYear" runat="server" Text='<%#Eval("AcademicYear") %>' Style="text-transform: uppercase;" Width="150px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="HighSchool" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbuttonhigh" OnClick="lnkbuttonhigh_Click" runat="server" Font-Bold="true" Width="150px">High School</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Inter" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbuttonInter" OnClick="lnkbuttonInter_Click" runat="server" Font-Bold="true" Width="150px">Inter</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Diploma Final Year" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbuttondiplomafinal" OnClick="lnkbuttondiplomafinal_Click" Font-Bold="true" runat="server" Width="150px">Diploma Final Year</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UG Final Year" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbuttonugfinal" OnClick="lnkbuttonugfinal_Click" runat="server" Font-Bold="true" Width="150px">UG Final Year</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Transfer Certificate" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbuttontc" OnClick="lnkbuttontc_Click" runat="server" Font-Bold="true" Width="150px">Transfer Certificate</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AntiRagging" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbuttoncc" OnClick="lnkbuttoncc_Click" runat="server" Font-Bold="true" Width="150px">Anti Ragging</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Migration" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbuttonmigration" OnClick="lnkbuttonmigration_Click" Font-Bold="true" runat="server" Width="150px">Migration</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="Gap Certificate" ItemStyle-Width="200px" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                       <asp:LinkButton ID="lnkbuttongapcertificate" OnClick="lnkbuttongapcertificate_Click" Font-Bold="true"  runat="server" Width="150px">Gap Certificate</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Domocile" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbuttondomicile" OnClick="lnkbuttondomicile_Click" runat="server" Font-Bold="true" Width="150px">Domocile</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student Aadhar" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbuttonstudentaadhar" OnClick="lnkbuttonstudentaadhar_Click" Font-Bold="true" runat="server" Width="150px">Student Aadhar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Guardian_Aadhar" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbuttonguardianaadhar" OnClick="lnkbuttonguardianaadhar_Click" Font-Bold="true" runat="server" Width="150px">Guardian Aadhar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ABC ID" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkABCID" OnClick="lnkABCID_Click" Font-Bold="true" runat="server" Width="150px">ABC ID</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remark" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRemark" runat="server" Enabled='<%# Eval("txtMarksEnableDesable").ToString().Equals("true") %>' Text='<%#Eval("[Document Reject Remark]") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--  <asp:TemplateField HeaderText="Admission Approval" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblDirectorAdmission" runat="server" Text='<%#Eval("Director_Approval") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                    <asp:TemplateField ItemStyle-Width="500px" HeaderStyle-CssClass="text-center" HeaderText="Select" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:CheckBox ID="Chkemployee" Enabled='<%# Eval("txtMarksEnableDesable").ToString().Equals("true") %>' Font-Bold="true" Width="200px" Text="I have checked all The documents." runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
            </asp:GridView>
        </div>
    </fieldset>
    <div id="documentverify" runat="server" visible="false">
        <asp:GridView ID="grddocumentverify1" runat="server" DataKeyNames="ID" AlternatingRowStyle-CssClass="danger"
            AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" Visible="true">

            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle CssClass="myclass" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <%# Container.DataItemIndex +1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student Number" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblemployeecode" runat="server" Text='<%# Bind("Student_Number") %>'></asp:Label>
                        <asp:HiddenField ID="Hfemployeecode" Value='<%# Eval("Student_Number") %>' runat="server" />
                        <asp:HiddenField ID="Hfhodname" Value='<%# Eval("Student_Number") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lblstudentName" runat="server" Text='<%# Eval("Student_Name") %>' Style="text-transform: uppercase;" Width="150px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Programme" ItemStyle-Width="3%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lblprogram" runat="server" Text='<%# Eval("Coursename") %>' Style="text-transform: uppercase;" Width="150px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="College Name" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblStudentCategory" runat="server" Text='<%#Eval("Collegecode") %>' Style="text-transform: uppercase;" Width="150px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Academic Year" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblAcademicYear" runat="server" Text='<%#Eval("AcademicYear") %>' Style="text-transform: uppercase;" Width="150px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Document Upload Status" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblAcademicYear" runat="server" Text='<%#Eval("Doc_Upload_Status") %>' Style="text-transform: uppercase;" Width="150px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Document Verify Status" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblAcademicYear" runat="server" Text='<%#Eval("Doc_Verify_Status") %>' Style="text-transform: uppercase;" Width="150px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>

