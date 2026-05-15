<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="TeacherDairy1.aspx.cs" Inherits="Faculty_TeacherDairy" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>--%>


    <script type="text/javascript">
        function numeric(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && ((charCode >= 48 && charCode <= 57) || charCode == 46))
                return true;
            else {
                alert('Please Enter Number Only .');
                return false;
            }
        }


    </script>

    <style type="text/css">
        /* Padding for links in a row */
        tr.myclass a {
            padding-right: 7px;
            padding-left: 7px;
        }

        /* GridView style */
        .gridview-style {
            border: 1px solid #C0C0C0;
            border-radius: 8px;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
        }

        /* Grid Header */
        .grid-header {
            background-color: #4CAF50; /* Green */
            color: white;
            font-weight: bold;
            text-align: center;
        }

        /* Edit Button */
        .btn-edit {
            background-color: #4CAF50;
            color: white;
            border: none;
            border-radius: 5px;
            padding: 5px 10px;
            cursor: pointer;
        }

            .btn-edit:hover {
                background-color: #45a049;
            }

        /* Hover effect for Grid View Row */
        .GridViewRow:hover {
            background-color: #f1f1f1;
        }

        /* Alternating Row Style */
        .AlternatingGridViewRow {
            background-color: #f7f7f7;
        }

        /* Label inside Grid */
        .gridview-style label {
            font-size: 14px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>

    <fieldset>
        <div id="divGeneralBody">
            <fieldset class="boxBodyInner">
                <div class="form-horizontal">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:UpdatePanel ID="pnlpic" runat="server">
                                        <ContentTemplate>
                                            <div class="col-md-2">
                                                <label style="width: 200px; font: bold; color: black; font-size: large">Academic Session:</label>
                                            </div>

                                            <div class="col-md-3">
                                                <asp:DropDownList ID="drpacademicsession" runat="server" BorderColor="Black" CssClass="form-control">
                                                    <%--<asp:ListItem Text="Select" Value="0"></asp:ListItem>--%>
                                                    <asp:ListItem Text="2022-2023" Value="22-23"></asp:ListItem>
                                                    <asp:ListItem Text="2023-2024" Value="23-24"></asp:ListItem>
                                                    <asp:ListItem Text="2024-2025" Value="24-25"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <label style="width: 200px; font: bold; color: black; font-size: large">Semester:</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:DropDownList ID="drpsemester" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpsemester_SelectedIndexChanged" BorderColor="Black" CssClass="form-control">
                                                    <asp:ListItem Text="Even" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Odd" Value="1"></asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="col-md-2">
                                        <asp:Button ID="btnTeacherdairy" runat="server" Text="View" Height="35px" BackColor="blue" ForeColor="White" OnClick="btnTeacherdairy_Click" Width="100px" Font-Bold="true" CssClass="form-control" class="btn btn-success btn-sm form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </fieldset>
        </div>

    </fieldset>

    <div id="divteacherdairy" runat="server" visible="false">
        <asp:Panel ID="pnlGridViewdata" CssClass="modalPopup" Width="99%" runat="server" ScrollBars="Auto" Height="100%">
            <fieldset class="boxBody" style="text-align: center; border-color: black; background-color: black;">
                <asp:Label ID="Label14" runat="server" Text="TEACHER DIARY" Font-Size="15pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

            </fieldset>



            <fieldset class="boxBody">


                <div style="width: 100%; margin-bottom: 10px; margin-left: 1%; margin-right: 1%; margin-top: 5px;">

                    <table style="width: 98%;">
                        <tr>
                            <td style="width: 1%"></td>
                            <td style="width: 12%; text-align: left">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/UPDATEDLOGO.jpg" Width="55%" />
                            </td>
                            <td style="width: 53%; text-align: center">
                                <strong>
                                    <asp:Label ID="lblCName" Font-Size="Large" Text="Teerthanker Mahaveer University, Moradabad" runat="server"></asp:Label></strong>
                                <br />
                                <strong>
                                    <asp:Label ID="lblAC" runat="server" Text="(Established under Govt. of U. P. Act No. 30, 2008)"></asp:Label></strong>

                                <br />
                                <strong>
                                    <asp:Label ID="LblType" runat="server" Text="Delhi Road, Moradabad (U.P)"></asp:Label>
                                </strong>
                                <br />

                            </td>
                            <td style="width: 12%; text-align: right">
                                <%--<asp:Image ID="Image2" runat="server" ImageUrl="~/images/UPDATEDLOGO.jpg" Width="55%" />--%>
                                <asp:Image ID="ImgPrv" Height="100px" Width="100px" runat="server" />
                            </td>

                        </tr>

                    </table>
                </div>
            </fieldset>
            <fieldset class="boxBody" style="text-align: center; border-color: black; background-color: black;">
                <asp:Label ID="Label1" runat="server" Text="FACULTY PROFILE" Font-Size="15pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
            </fieldset>
            <div id="divGeneralBodyenrollmentform">
                <fieldset class="boxBodyInner">
                    <div class="form-horizontal">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">Faculty Name:</label>
                                        </div>

                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtFName" runat="server" Enabled="false" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">Faculty Code:</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtFacultyCode" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">Designation:</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtdesignaton" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">Department:</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtDepartment" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">College/Faculty:</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtcollegeFaculty" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">Education Qualification:</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtPrograme" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">Area of Specialization:</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtAreaofSpecilization" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">Contact No:</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtcontactno" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">Email Id:</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtemailid" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">Date Of Birth:</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtdateofbirth" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">Date of Joining:</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtdateofjoining" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">Working Experience: (No. of Years):</label>
                                        </div>


                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">Teaching(within TMU):</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtteachingwithintmu" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">Teaching(Outside TMU):</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtteachingoutsidetmu" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">Research:</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtresearch" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">Industry:</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtindustry" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">Others:</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtothers" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-8">
                                            <label style="font: bold; color: black; font-size: large">Total Publications (from the start of teaching till date)</label>
                                        </div>
                                        <div class="col-md-4">
                                            <%--<asp:TextBox ID="TextBox1" runat="server" BorderColor="Black"  CssClass="form-control"></asp:TextBox>--%>
                                            <asp:TextBox ID="TextBox1" runat="server" Font-Size="12px" onkeydown="return false;" BorderColor="Black" autocomplete="off" oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" CssClass="form-control"></asp:TextBox>
                                            <asp:CalendarExtender runat="server" TargetControlID="TextBox1" Format="yyyy-MM-dd" ID="CalendarExtender8"></asp:CalendarExtender>
                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">h-index:</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txthindex" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">i10-index:</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txti10index" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">Citations:</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtcitations" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-8">
                                        </div>

                                        <div class="col-md-1">
                                            <asp:TextBox ID="txtprofileid" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control" Visible="false"></asp:TextBox>
                                        </div>

                                        <div class="col-md-2">
                                            <asp:Button ID="btnprofileupdate" runat="server" Text="Update" OnClick="btnprofileupdate_Click" Height="35px" Width="100px" Font-Bold="true" CssClass="form-control" class="btn btn-success btn-sm form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">Research Projects (Govt. & Non-Govt.)(Applied/Sanctioned)/Seed Money Projects:</label>
                                            <asp:Button ID="btnResearchproject" runat="server" OnClick="btnResearchproject_Click" Text="+" Font-Size="Large" />
                                        </div>


                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <%--<asp:GridView ID="grdresearchproject" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                                            GridLines="Horizontal" EmptyDataText="There are no data records to display."
                                            AllowSorting="true" ShowHeaderWhenEmpty="true">

                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" Visible="false" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblemployeecode" runat="server" Text='<%# Bind("[ID]") %>'></asp:Label>
                                                        <asp:HiddenField ID="Hfemployeecode" Value='<%# Eval("[ID]") %>' runat="server" />
                                                        <asp:HiddenField ID="Hfhodname" Value='<%# Eval("[ID]") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Project Title">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="130px" Text='<%# Eval("ProjectTitle") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Amount Sanctioned/Applied">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="130px" Text='<%# Eval("AmountSanctionedApplied") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="170px" Text='<%# Eval("Date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Funding Agency">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="170px" Text='<%# Eval("FundingAgency") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Funding Body (Govt & Non-Govt)">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="200px" Text='<%# Eval("FundingBody") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Status (Ongoing/Completed)">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="130px" Text='<%# Eval("Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnselect" runat="server" OnClick="btnselect_Click" Text="Edit" Width="80px" />
                                                    </ItemTemplate>
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

                                        </asp:GridView>--%>
                                            <asp:GridView ID="grdresearchproject" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
                                                BorderStyle="Solid" BorderWidth="1px" CellPadding="5" Width="100%" GridLines="Both" EmptyDataText="There are no data records to display."
                                                AllowSorting="true" ShowHeaderWhenEmpty="true">

                                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="7%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ID" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" Visible="false" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblemployeecode" runat="server" Text='<%# Bind("[ID]") %>'></asp:Label>
                                                            <asp:HiddenField ID="Hfemployeecode" Value='<%# Eval("[ID]") %>' runat="server" />
                                                            <asp:HiddenField ID="Hfhodname" Value='<%# Eval("[ID]") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Project Title">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Width="130px" Text='<%# Eval("ProjectTitle") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Amount Sanctioned/Applied">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Width="130px" Text='<%# Eval("AmountSanctionedApplied") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Width="170px" Text='<%# Eval("Date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Funding Agency">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Width="170px" Text='<%# Eval("FundingAgency") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Funding Body (Govt & Non-Govt)">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Width="200px" Text='<%# Eval("FundingBody") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Status (Ongoing/Completed)">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Width="130px" Text='<%# Eval("Status") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnselect" runat="server" OnClick="btnselect_Click" Text="Edit" Width="80px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>

                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Center" Height="50px" CssClass="cssGridheaderfont" />
                                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                                <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridrowfont" BorderWidth="1px" BorderColor="#E7E7FF" />
                                                <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
                                                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                                <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                            </asp:GridView>


                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">Ph.D Guidance</label>
                                            <asp:Button ID="btnGuidance" runat="server" OnClick="btnGuidance_Click" Text="+" Font-Size="Large" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <%--<asp:GridView ID="grdphdguidence" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                                            GridLines="Horizontal" ShowHeaderWhenEmpty="true" EmptyDataText="There are no data records to display."
                                            AllowSorting="true">

                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl. No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name of Scholars">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="130px" Text='<%# Eval("ScholarName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Date of Registration">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="130px" Text='<%# Eval("RegistrationDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ph.D. Topic">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="170px" Text='<%# Eval("PhDTopic") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Current Status">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="170px" Text='<%# Eval("CurrentStatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnselect" runat="server" OnClick="btnselect_Click1" Text="Edit" Width="80px" />
                                                    </ItemTemplate>
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

                                        </asp:GridView>--%>
                                            <asp:GridView ID="grdphdguidence" runat="server" DataKeyNames="ID" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="1px"
                                                CellPadding="5" Width="100%" GridLines="Both" ShowHeaderWhenEmpty="true"
                                                EmptyDataText="There are no data records to display." AllowSorting="true">

                                                <AlternatingRowStyle BackColor="#F7F7F7" />

                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="7%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Name of Scholars">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("ScholarName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Date of Registration">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("RegistrationDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Ph.D. Topic">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("PhDTopic") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Current Status">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("CurrentStatus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnselect" runat="server" OnClick="btnselect_Click1" Text="Edit" Width="80px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>

                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />

                                                <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7"
                                                    HorizontalAlign="Center" Height="50px" CssClass="cssGridheaderfont" />

                                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />

                                                <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridrowfont" BorderWidth="1px"
                                                    BorderColor="#E7E7FF" />

                                                <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />

                                                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />

                                                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                                <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                            </asp:GridView>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">Patents</label>
                                            <asp:Button ID="btnPatents" runat="server" OnClick="btnPatents_Click" Text="+" Font-Size="Large" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <%--<asp:GridView ID="grdpatents" DataKeyNames="ID" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                                            GridLines="Horizontal" EmptyDataText="There are no data records to display."
                                            AllowSorting="true" ShowHeaderWhenEmpty="true">

                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Title of Patent ">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="130px" Text='<%# Eval("PatentTitle ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Date of filed/published/Grant ">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="130px" Text='<%# Eval("FiledPublishedGrantDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="170px" Text='<%# Eval("Remark") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnPatent" OnClick="btnPatent_Click" runat="server" Text="Edit" Width="80px" />
                                                    </ItemTemplate>
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

                                        </asp:GridView>--%>

                                            <asp:GridView ID="grdpatents" DataKeyNames="ID" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="1px"
                                                CellPadding="5" Width="100%" GridLines="Both" EmptyDataText="There are no data records to display."
                                                AllowSorting="true" ShowHeaderWhenEmpty="true">

                                                <AlternatingRowStyle BackColor="#F7F7F7" />

                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="7%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Title of Patent">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("PatentTitle") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Date of filed/published/Grant">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("FiledPublishedGrantDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Remark">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="30%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnPatent" OnClick="btnPatent_Click" runat="server" Text="Edit" Width="80px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>


                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />


                                                <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Center"
                                                    Height="50px" CssClass="cssGridheaderfont" />


                                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />


                                                <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" BorderWidth="1px" BorderColor="#E7E7FF"
                                                    CssClass="cssGridrowfont" />


                                                <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />


                                                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                                <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                            </asp:GridView>

                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">Awards/Recognitions</label>
                                            <asp:Button ID="btnAwardsrecognition" runat="server" OnClick="btnAwardsrecognition_Click" Text="+" Font-Size="Large" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <%--<asp:GridView ID="grdaward" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                                            GridLines="Horizontal" EmptyDataText="There are no data records to display."
                                            AllowSorting="true" ShowHeaderWhenEmpty="true">
                                            <AlternatingRowStyle BackColor="#F7F7F7" />

                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl. No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="130px" Text='<%# Eval("AwardDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Internal Awards(Title)">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="130px" Text='<%# Eval("InternalAwardsTitle") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="External Awards (Title)">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="170px" Text='<%# Eval("ExternalAwardsTitle") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="170px" Text='<%# Eval("Remark") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnaward" runat="server" OnClick="btnaward_Click1" Text="Edit" Width="80px" />
                                                    </ItemTemplate>
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

                                        </asp:GridView>--%>
                                            <asp:GridView ID="grdaward" runat="server" DataKeyNames="ID" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="1px"
                                                CellPadding="5" Width="100%" GridLines="Both" EmptyDataText="There are no data records to display."
                                                AllowSorting="true" ShowHeaderWhenEmpty="true">


                                                <AlternatingRowStyle BackColor="#F7F7F7" />

                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="7%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("AwardDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Internal Awards (Title)">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("InternalAwardsTitle") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="External Awards (Title)">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("ExternalAwardsTitle") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnaward" runat="server" OnClick="btnaward_Click1" Text="Edit" Width="80px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>


                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Center"
                                                    Height="50px" CssClass="cssGridheaderfont" />
                                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                                <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" BorderWidth="1px" BorderColor="#E7E7FF"
                                                    CssClass="cssGridrowfont" />
                                                <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
                                                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                                <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                            </asp:GridView>

                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">Membership of Professional Bodies etc</label>
                                            <asp:Button ID="btnmembership" runat="server" OnClick="btnmembership_Click" Text="+" Font-Size="Large" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <%--<asp:GridView ID="grdmembership" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                                            GridLines="Horizontal" EmptyDataText="There are no data records to display."
                                            AllowSorting="true" ShowHeaderWhenEmpty="true">

                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Membership Duration">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="130px" Text='<%# Eval("MembershipDuration") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Name of Professional Body">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="130px" Text='<%# Eval("ProfessionalBodyName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Type of Member (Annual/Life/Fellow)">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="170px" Text='<%# Eval("MemberType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Membership Number">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="170px" Text='<%# Eval("MembershipNumber") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnmemberprofessional" OnClick="btnmemberprofessional_Click1" runat="server" Text="Edit" Width="80px" />
                                                    </ItemTemplate>
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

                                        </asp:GridView>--%>
                                            <asp:GridView ID="grdmembership" runat="server" DataKeyNames="ID" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="1px"
                                                CellPadding="5" Width="100%" GridLines="Both" EmptyDataText="There are no data records to display."
                                                AllowSorting="true" ShowHeaderWhenEmpty="true">


                                                <AlternatingRowStyle BackColor="#F7F7F7" />

                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="7%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Membership Duration">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("MembershipDuration") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Name of Professional Body">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("ProfessionalBodyName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Type of Member (Annual/Life/Fellow)">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("MemberType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Membership Number">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("MembershipNumber") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnmemberprofessional" runat="server" OnClick="btnmemberprofessional_Click1" Text="Edit" Width="80px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>


                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />


                                                <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Center" Height="50px" CssClass="cssGridheaderfont" />


                                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />


                                                <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" BorderWidth="1px" BorderColor="#E7E7FF"
                                                    CssClass="cssGridrowfont" />


                                                <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />


                                                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                                <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                            </asp:GridView>

                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">Research Paper/Book Chapter Published</label>
                                            <asp:Button ID="btnResearch" runat="server" OnClick="btnResearch_Click" Text="+" Font-Size="Large" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <%--<asp:GridView ID="grdresearch" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                                            GridLines="Horizontal" EmptyDataText="There are no data records to display."
                                            AllowSorting="true" ShowHeaderWhenEmpty="true">

                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Title of Paper/Book Chapter">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="130px" Text='<%# Eval("TitleOfPaperOrBookChapter") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Detail of Journal (Journal name, Vol., No, Page Nos., Publisher,Year)">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="130px" Text='<%# Eval("DetailsofJournal") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Detail of Indexing/ISSN/ISBN/DOI">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="170px" Text='<%# Eval("DetailsofIndexing") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnResearch" OnClick="btnResearch_Click1" runat="server" Text="Edit" Width="80px" />
                                                    </ItemTemplate>
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

                                        </asp:GridView>--%>

                                            <asp:GridView ID="grdresearch" runat="server" DataKeyNames="ID" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="1px"
                                                CellPadding="5" Width="100%" GridLines="Both" EmptyDataText="There are no data records to display."
                                                AllowSorting="true" ShowHeaderWhenEmpty="true">


                                                <AlternatingRowStyle BackColor="#F7F7F7" />

                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="7%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Title of Paper/Book Chapter">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("TitleOfPaperOrBookChapter") %>' Width="170px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Detail of Journal (Journal name, Vol., No, Page Nos., Publisher, Year)">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("DetailsofJournal") %>' Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="25%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Detail of Indexing/ISSN/ISBN/DOI">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("DetailsofIndexing") %>' Width="170px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="30%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnResearch" OnClick="btnResearch_Click1" runat="server" Text="Edit" Width="80px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>


                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />


                                                <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Center" Height="50px" CssClass="cssGridheaderfont" />


                                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />


                                                <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" BorderWidth="1px" BorderColor="#E7E7FF"
                                                    CssClass="cssGridrowfont" />


                                                <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />


                                                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                                <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                            </asp:GridView>


                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">Book Published with details</label>
                                            <asp:Button ID="btnbookpublished" runat="server" OnClick="btnbookpublished_Click" Text="+" Font-Size="Large" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <%--<asp:GridView ID="grdbookpublished" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                                            GridLines="Horizontal" EmptyDataText="There are no data records to display."
                                            AllowSorting="true" ShowHeaderWhenEmpty="true">

                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Title of Book">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="130px" Text='<%# Eval("TitleOfBook") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Authors">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="130px" Text='<%# Eval("Authors") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Publishing Month & Year">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="170px" Text='<%# Eval("PublishingMonthAndYear") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Detail of Publisher">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="170px" Text='<%# Eval("PublisherDetails") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ISBN">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="170px" Text='<%# Eval("ISBN") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnbookpublisheddetail" OnClick="btnbookpublisheddetail_Click1" runat="server" Text="Edit" Width="80px" />
                                                    </ItemTemplate>
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

                                        </asp:GridView>--%>
                                            <asp:GridView ID="grdbookpublished" runat="server" DataKeyNames="ID" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="1px"
                                                CellPadding="5" Width="100%" GridLines="Both" EmptyDataText="There are no data records to display."
                                                AllowSorting="true" ShowHeaderWhenEmpty="true">


                                                <AlternatingRowStyle BackColor="#F7F7F7" />

                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Title of Book">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("TitleOfBook") %>' Width="180px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Authors">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("Authors") %>' Width="180px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Publishing Month & Year">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("PublishingMonthAndYear") %>' Width="170px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Detail of Publisher">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("PublisherDetails") %>' Width="180px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="ISBN">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("ISBN") %>' Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnbookpublisheddetail" OnClick="btnbookpublisheddetail_Click1" runat="server" Text="Edit" Width="80px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>


                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />


                                                <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Center" Height="50px" CssClass="cssGridheaderfont" />


                                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />


                                                <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" BorderWidth="1px" BorderColor="#E7E7FF"
                                                    CssClass="cssGridrowfont" />


                                                <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />


                                                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                                <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                            </asp:GridView>



                                        </div>
                                    </div>


                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">Seminars/Conferences/QIPs/Workshops/FDPs/Guest Lectures attended as participant/Expert/Presented research paper</label>
                                            <asp:Button ID="btnseminars" runat="server" OnClick="btnseminars_Click" Text="+" Font-Size="Large" />
                                        </div>


                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <%--<asp:GridView ID="grdSeminar" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                                            GridLines="Horizontal" EmptyDataText="There are no data records to display."
                                            AllowSorting="true" ShowHeaderWhenEmpty="true">

                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="130px" Text='<%# Eval("Date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Name of Event">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="130px" Text='<%# Eval("EventName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Title">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="170px" Text='<%# Eval("Title") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Organizing Institute">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="170px" Text='<%# Eval("OrganizingInstitute") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Duration of Programme">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="170px" Text='<%# Eval("DurationOfProgramme") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Expert/Participated/Paper Presented">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="170px" Text='<%# Eval("ExpertOrParticipatedOrPaperPresented") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnseminarscon" OnClick="btnseminarscon_Click" runat="server" Text="Edit" Width="80px" />
                                                    </ItemTemplate>
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

                                        </asp:GridView>--%>
                                            <asp:GridView ID="grdSeminar" runat="server" DataKeyNames="ID" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="1px"
                                                CellPadding="5" Width="100%" GridLines="Both" EmptyDataText="There are no data records to display."
                                                AllowSorting="true" ShowHeaderWhenEmpty="true">


                                                <AlternatingRowStyle BackColor="#F7F7F7" />

                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("Date") %>' Width="130px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Name of Event">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("EventName") %>' Width="130px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Title">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("Title") %>' Width="170px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Organizing Institute">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("OrganizingInstitute") %>' Width="170px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Duration of Programme">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("DurationOfProgramme") %>' Width="170px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Expert/Participated/Paper Presented">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("ExpertOrParticipatedOrPaperPresented") %>' Width="170px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnseminarscon" OnClick="btnseminarscon_Click" runat="server" Text="Edit" Width="80px" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="8%" />
                                                    </asp:TemplateField>
                                                </Columns>


                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />


                                                <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Center" Height="50px" CssClass="cssGridheaderfont" />


                                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />


                                                <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" BorderWidth="1px" BorderColor="#E7E7FF" CssClass="cssGridrowfont" />


                                                <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />


                                                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                                <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                            </asp:GridView>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">Techniques Developed/In-Progress/any other</label>
                                            <asp:Button ID="btntechniques" OnClick="btntechniques_Click" runat="server" Text="+" Font-Size="Large" />
                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <%--<asp:GridView ID="grdtechniques" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                                            GridLines="Horizontal" EmptyDataText="There are no data records to display."
                                            AllowSorting="true" ShowHeaderWhenEmpty="true">

                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl. No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="130px" Text='<%# Eval("TechniquesDeveloped") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btntechniquesdevlopment" OnClick="btntechniquesdevlopment_Click" runat="server" Text="Edit" Width="80px" />
                                                    </ItemTemplate>
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

                                        </asp:GridView>--%>

                                            <asp:GridView ID="grdtechniques" runat="server" DataKeyNames="ID" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="1px"
                                                CellPadding="5" Width="100%" GridLines="Both" EmptyDataText="There are no data records to display."
                                                AllowSorting="true" ShowHeaderWhenEmpty="true">


                                                <AlternatingRowStyle BackColor="#F7F7F7" />

                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("TechniquesDeveloped") %>' Width="250px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="40%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btntechniquesdevlopment" OnClick="btntechniquesdevlopment_Click" runat="server" Text="Edit" Width="80px" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                </Columns>


                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />


                                                <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Center" Height="50px" CssClass="cssGridheaderfont" />


                                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />


                                                <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" BorderWidth="1px" BorderColor="#E7E7FF" CssClass="cssGridrowfont" />


                                                <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />


                                                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                                <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                            </asp:GridView>




                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">Any other Academic/Administrative Assignments</label>
                                            <asp:Button ID="btnanyother" runat="server" OnClick="btnanyother_Click" Text="+" Font-Size="Large" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <%--<asp:GridView ID="grdAcademicAdministrative" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                                            GridLines="Horizontal" EmptyDataText="There are no data records to display."
                                            AllowSorting="true" ShowHeaderWhenEmpty="true">

                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="130px" Text='<%# Eval("AcademicAdministrativeAssignments") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnAcademicAdministrative" OnClick="btnAcademicAdministrative_Click" runat="server" Text="Edit" Width="80px" />
                                                    </ItemTemplate>
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

                                        </asp:GridView>--%>

                                            <asp:GridView ID="grdAcademicAdministrative" runat="server" DataKeyNames="ID" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="1px"
                                                CellPadding="5" Width="100%" GridLines="Both" EmptyDataText="There are no data records to display."
                                                AllowSorting="true" ShowHeaderWhenEmpty="true">


                                                <AlternatingRowStyle BackColor="#F7F7F7" />

                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("AcademicAdministrativeAssignments") %>' Width="250px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="60%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnAcademicAdministrative" OnClick="btnAcademicAdministrative_Click" runat="server" Text="Edit" Width="80px" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>
                                                </Columns>


                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Center" Height="50px" CssClass="cssGridheaderfont" />
                                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                                <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" BorderWidth="1px" BorderColor="#E7E7FF" CssClass="cssGridrowfont" />
                                                <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
                                                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                                <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                            </asp:GridView>

                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">Records of Additional Activites(Contribution to Development of Workshop/ Laboratories/Infrastructure/Course Curriculum/ Participation in Co-Curricular & Extracurricular activities/Lectures delivered etc.)</label>
                                            <asp:Button ID="btntrecord" runat="server" OnClick="btntrecord_Click" Text="+" Font-Size="Large" />
                                        </div>


                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <%--<asp:GridView ID="grdrecordofadditional" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                                            GridLines="Horizontal" EmptyDataText="There are no data records to display."
                                            AllowSorting="true" ShowHeaderWhenEmpty="true">

                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date and Duration">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="130px" Text='<%# Eval("DateandDuration") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Details of Activity">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Width="130px" Text='<%# Eval("DetailsofActivity") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnAdditionalactivity" OnClick="btnAdditionalactivity_Click" runat="server" Text="Edit" Width="80px" />
                                                    </ItemTemplate>
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

                                        </asp:GridView>--%>

                                            <asp:GridView ID="grdrecordofadditional" runat="server" DataKeyNames="ID" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="1px"
                                                CellPadding="5" Width="100%" GridLines="Both" EmptyDataText="There are no data records to display."
                                                AllowSorting="true" ShowHeaderWhenEmpty="true">


                                                <AlternatingRowStyle BackColor="#F7F7F7" />

                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Date and Duration">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("DateandDuration") %>' Width="250px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="25%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Details of Activity">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("DetailsofActivity") %>' Width="250px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="40%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnAdditionalactivity" OnClick="btnAdditionalactivity_Click" runat="server" Text="Edit" Width="80px" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>
                                                </Columns>


                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />


                                                <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Center" Height="50px" CssClass="cssGridheaderfont" />


                                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />


                                                <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" BorderWidth="1px" BorderColor="#E7E7FF" CssClass="cssGridrowfont" />


                                                <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />


                                                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                                <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                            </asp:GridView>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">Student's Projects Ongoing at UG/PG Level </label>
                                            <asp:Button ID="btnstudentproject" runat="server" OnClick="btnstudentproject_Click" Text="+" Font-Size="Large" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <asp:GridView ID="grdstudentproject" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
                                                BorderStyle="Solid" BorderWidth="1px" CellPadding="5" Width="100%" GridLines="Both" EmptyDataText="There are no data records to display."
                                                AllowSorting="true" ShowHeaderWhenEmpty="true">

                                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Group No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="UG/PG Level">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("UGPGLevel") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Academic Session">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("AcademicYear") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Semester">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("Semester") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="8%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Programme">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("Programme") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Students Name">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("StudentsName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Project Topic">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("ProjectTopic") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Progress (Month-1)">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("Month1") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="8%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Progress (Month-2)">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("Month2") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="8%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Progress (Month-3)">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("Month3") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="8%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Progress (Month-4)">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("Month4") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="8%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Overall Evaluation">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("OverallEvaluation") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnStudentproject" runat="server" Text="Edit" Width="80px" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>
                                                </Columns>

                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Center" Height="50px" CssClass="cssGridheaderfont" />
                                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                                <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridrowfont" BorderWidth="1px" BorderColor="#E7E7FF" />
                                                <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
                                                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                                <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">Teaching Load at UG/PG/PhD Level (Per week) </label>
                                            <asp:Button ID="btnTeachingload" runat="server" OnClick="btnTeachingload_Click" Text="+" Font-Size="Large" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <asp:GridView ID="grdteacherload" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" OnRowDataBound="grdteacherload_RowDataBound" BackColor="White" BorderColor="#E7E7FF"
                                                BorderStyle="Solid" BorderWidth="1px" CellPadding="5" Width="100%" GridLines="Both" EmptyDataText="No data to display."
                                                AllowSorting="true" ShowHeaderWhenEmpty="true" ShowFooter="True">

                                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                                <Columns>

                                                    <asp:TemplateField HeaderText="S. No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Program">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("Program") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Semester">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("Semester1") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Student Strength">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("StudentStrength") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="12%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Course Code">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("CourseCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="12%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Course Name">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("CourseName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="L">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("L") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotalL" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="T">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("T") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotalT" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="P">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("P") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotalP" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="C">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("C") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotalC" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Course Category">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("CourseCategory") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnteachingloadug" OnClick="btnteachingloadug_Click" runat="server" Text="Edit" Width="80px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Center" Height="50px" CssClass="cssGridheaderfont" />
                                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                                <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridrowfont" BorderWidth="1px" BorderColor="#E7E7FF" />
                                                <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
                                                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                                <SortedDescendingHeaderStyle BackColor="#3E3277" />


                                            </asp:GridView>

                                        </div>
                                    </div>


                                    <fieldset class="boxBody" style="text-align: center; border-color: black; background-color: black;">
                                        <asp:Label ID="Label53" runat="server" Text="TIME TABLE" Font-Size="15pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                    </fieldset>




                                    <div class="form-group">
                                        <div class="col-md-12">

                                            <asp:Table ID="timeTable" runat="server" CssClass="table table-bordered"></asp:Table>


                                        </div>
                                    </div>




                                    <fieldset class="boxBody" style="text-align: center; border-color: black; background-color: black;">
                                        <asp:Label ID="Label54" runat="server" Text="Student Attendance" Font-Size="15pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                    </fieldset>

                                    <div class="form-group">
                                        <div class="col-md-12">

                                            <asp:GridView ID="grdStudentAttendance" runat="server" Font-Size="20px" AutoGenerateColumns="False" CssClass="MyClass" BackColor="White" BorderColor="#E7E7FF"
                                                DataKeyNames="Student No_"
                                                BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="1100px" GridLines="Horizontal"
                                                EmptyDataText="There are no data records to display.">
                                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="7%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="ApplicantName">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStudent" runat="server" CssClass='<%# Bind("css") %>' Text='<%# Eval("[Student No_]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="visible-lg" />
                                                        <ItemStyle CssClass="visible-lg" Font-Size="0pt" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Enrollment No_" HeaderText="Enrollment No" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                        <HeaderStyle CssClass="visible-lg" />
                                                        <ItemStyle CssClass="visible-lg" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Student Name" SortExpression="ApplicantName">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("[Student Name]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="visible-lg" />
                                                        <ItemStyle CssClass="visible-lg" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Total" HeaderText="Delivered Lecture" SortExpression="Delivered Lecture" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                        <HeaderStyle CssClass="visible-lg" />
                                                        <ItemStyle CssClass="visible-lg" />
                                                    </asp:BoundField>

                                                    <asp:TemplateField HeaderText="Present" SortExpression="ApplicantName">

                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="BtnPresent" runat="server" Text='<%# Eval("Present") %>' OnClick="BtnPresent_Click"></asp:LinkButton>


                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="visible-lg" />
                                                        <ItemStyle CssClass="visible-lg" />
                                                    </asp:TemplateField>





                                                    <asp:TemplateField HeaderText="Absent" SortExpression="ApplicantName">

                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="BtnAbsent" runat="server" Text='<%# Eval("Absent") %>' OnClick="BtnAbsent_Click"></asp:LinkButton>

                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="visible-lg" />
                                                        <ItemStyle CssClass="visible-lg" />
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="Percentage" HeaderText="Percentage(%)" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">

                                                        <HeaderStyle CssClass="visible-lg" />
                                                        <ItemStyle CssClass="visible-lg" />
                                                    </asp:BoundField>

                                                    <asp:TemplateField HeaderText="Hosteller" SortExpression="Hosteller">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHosteler" runat="server" Text='<%# Eval("[Hosteler]") %>'></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="visible-lg" />
                                                        <ItemStyle CssClass="visible-lg" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                <HeaderStyle BackColor="LightGray" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" Font-Size="Large" Height="40px" VerticalAlign="Bottom" />
                                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                                <RowStyle ForeColor="#4A3C8C" Font-Bold="true" Font-Size="Medium" BorderStyle="Solid" BorderColor="Black" />
                                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                                <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                            </asp:GridView>


                                        </div>
                                    </div>


                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">Theory Course 01 </label>
                                            <asp:Button ID="Button29" runat="server" OnClick="Button29_Click" Text="+" Font-Size="Large" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">Lab Course 01 </label>
                                            <asp:Button ID="Button34" runat="server" OnClick="Button34_Click" Text="+" Font-Size="Large" />
                                        </div>
                                    </div>



                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">Lession Plan Summary (UNIT WISE) Theory Course 01 </label>
                                            <asp:Button ID="Button37" runat="server" OnClick="Button37_Click" Text="+" Font-Size="Large" />
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnlLessonPlans" runat="server">
                                    </asp:Panel>

                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <asp:GridView ID="grdlessionplanunitwise" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
                                                BorderStyle="Solid" BorderWidth="1px" CellPadding="5" Width="100%" GridLines="Both" EmptyDataText="There are no data records to display."
                                                AllowSorting="true" ShowHeaderWhenEmpty="true">

                                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                                <Columns>

                                                    <asp:TemplateField HeaderText="S. No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Unit No.">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("UnitNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="No. of Topics to be covered in this unit">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("TopicsCount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Start Date">
                                                        <ItemTemplate>
                                                            <%--<asp:Label runat="server" Text='<%# Eval("PlannedStartDate") %>'></asp:Label>--%>
                                                            <asp:Label runat="server" Text='<%# Eval("PlannedStartDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="End Date">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("PlannedEndDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Total Lectures">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("PlannedLectures") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Start Date">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("ActualStartDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="End Date">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("ActualEndDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Total Lectures">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("ActualLectures") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>

                                                </Columns>

                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Center" Height="50px" CssClass="cssGridheaderfont" />
                                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                                <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" BorderWidth="1px" BorderColor="#E7E7FF" CssClass="cssGridrowfont" />
                                                <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
                                                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                                <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                            </asp:GridView>


                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">Lession Plan (LECTURE WISE) Theory Course 01 </label>
                                            <asp:Button ID="Button40" runat="server" OnClick="Button40_Click" Text="+" Font-Size="Large" />
                                        </div>
                                    </div>
                                    <asp:Panel ID="PnlLessionPlanLECTUREWISE" runat="server">
                                    </asp:Panel>

                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">Laboratory Work Plan (LAB COURSE 01) </label>
                                            <asp:Button ID="Button41" runat="server" OnClick="Button41_Click" Text="+" Font-Size="Large" />
                                        </div>
                                    </div>
                                    <asp:Panel ID="PnlLaboratoryWorkPlan" runat="server">
                                    </asp:Panel>

                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">
                                                Detail of Class Tests
                                       
                                        <br />
                                                Theory Course 01
                                            </label>
                                            <asp:Button ID="Button44" runat="server" OnClick="Button44_Click" Text="+" Font-Size="Large" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">
                                                Details of Assignments                                       
                                        <br />
                                                Theory Course 01
                                            </label>
                                            <asp:Button ID="Button47" runat="server" OnClick="Button47_Click" Text="+" Font-Size="Large" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">
                                                Attendance & Continuous Assessment (Theory Course 01)                                       
                                            </label>
                                            <asp:Button ID="Button52" runat="server" OnClick="Button52_Click" Text="+" Font-Size="Large" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">
                                                Attendance & Continuous Assessment (Lab Course 01)                                       
                                            </label>
                                            <asp:Button ID="Button55" runat="server" OnClick="Button55_Click" Text="+" Font-Size="Large" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">
                                                Attendance of Slow Learners (Theory Course 01)                                       
                                            </label>
                                            <asp:Button ID="Button58" runat="server" OnClick="Button58_Click" Text="+" Font-Size="Large" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">
                                                Attendance of Advanced Learners (Theory Course 01):                                       
                                            </label>
                                            <asp:Button ID="Button59" runat="server" OnClick="Button59_Click" Text="+" Font-Size="Large" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">
                                                Attendance of Extra Classes                                       
                                            </label>
                                            <asp:Button ID="Button62" runat="server" OnClick="Button62_Click" Text="+" Font-Size="Large" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label style="font: bold; color: black; font-size: large">
                                                Attendance of Competitive Classes                                       
                                            </label>
                                            <asp:Button ID="Button67" runat="server" OnClick="Button67_Click" Text="+" Font-Size="Large" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>


            <asp:Panel ID="pnlAttDetails" Width="60%" CssClass="modalPopup" runat="server" Style="display: none;">

                <div class="header">
                    <b>
                        <asp:Label ID="lblNotification" runat="server" Text="Attendance Detail"></asp:Label></b><div class="close">
                            <asp:Button ID="btnclose" OnClick="btnclose_Click" runat="server" Text="X" />
                        </div>
                </div>
                <div id="Div1" runat="server" style="max-height: 500px; overflow: auto;">

                    <div class="body">
                        <div style="width: 100%">
                            <center>
                                <asp:Label ID="lblStudent" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:Label ID="lblCourse" runat="server" Text="Label"></asp:Label>
                                <br />
                                <asp:GridView ID="grdAttandanceDetails" Width="1050px" EmptyDataText="There are no data records to display." runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl. No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle Width="7%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont1" />
                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                    <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont1" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                    <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                    <SortedDescendingHeaderStyle BackColor="#3E3277" />

                                </asp:GridView>
                            </center>
                        </div>

                    </div>

                </div>
            </asp:Panel>
            <asp:Button ID="Button25" runat="server" Style="display: none;" />
            <asp:ModalPopupExtender ID="ModalPopupExtender12" runat="server" TargetControlID="btnDummy"
                PopupControlID="pnlAttDetails" BackgroundCssClass="modalBackground" />

            <asp:Panel ID="pnlGridViewDetails" CssClass="modalPopup" Width="60%" runat="server" Style="display: none;" Height="50%" ScrollBars="Vertical">
                <fieldset>
                    <div class="box box-info">
                        <div class="box-header with-border" style="background-color: lightblue">
                            <h3 class="box-title" style="color: darkblue; text-align: center; height: 30px; font-size: large;">Research Projects (Govt. & Non-Govt.)(Applied/Sanctioned)/Seed Money Projects:</h3>
                            <div style="text-align: right; margin-top: -27px">
                                <asp:Button ID="Button1" Font-Bold="true" ForeColor="White" BackColor="Black" OnClick="Button1_Click" BorderStyle="Solid" runat="server" Text="X" />
                            </div>
                        </div>

                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-6" style="text-align: left;">
                                <label id="Label2" runat="server">Project Title</label>
                                <asp:TextBox ID="txtprojecttitle" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator" ValidationGroup="ResearchProject" runat="server"
                                    ControlToValidate="txtprojecttitle" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label3" runat="server">Amount Sanctioned/Applied</label>
                                <asp:TextBox ID="txtamountsantioned" runat="server" BorderColor="Black" onkeypress="return numeric(event)" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="ResearchProject" runat="server"
                                    ControlToValidate="txtamountsantioned" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label4" runat="server">Date</label>
                                <asp:TextBox ID="txtDate" runat="server" Font-Size="12px" onkeydown="return false;" BorderColor="Black" autocomplete="off" oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender runat="server" TargetControlID="txtDate" Format="yyyy-MM-dd" ID="CalendarExtender2"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="ResearchProject" runat="server"
                                    ControlToValidate="txtDate" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label5" runat="server">Funding Agency</label>
                                <asp:TextBox ID="txtfundingagency" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="ResearchProject" runat="server"
                                    ControlToValidate="txtfundingagency" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label6" runat="server">Funding Body(Govt & non-Govt)</label>
                                <asp:TextBox ID="txtfundibodynd" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="ResearchProject" runat="server"
                                    ControlToValidate="txtfundibodynd" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label7" runat="server">Status</label>
                                <asp:DropDownList ID="drpstatus" runat="server" BorderColor="Black" CssClass="form-control">
                                    <asp:ListItem Text="Ongoing" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Completed" Value="2"></asp:ListItem>
                                </asp:DropDownList>

                            </div>

                            <div class="col-md-3" style="text-align: left; margin-top: 20px">
                                <asp:Button ID="btn_Save" runat="server" Text="Save" OnClick="btn_Save_Click" Height="35px" ValidationGroup="ResearchProject" Width="100px" Font-Bold="true" CssClass="form-control" class="btn btn-success btn-sm form-control" />

                            </div>
                        </div>
                        <%-- </ContentTemplate>
                        </asp:UpdatePanel>--%>
                        <div class="form-group">
                            <div class="col-md-3" style="text-align: left;">

                                <asp:TextBox ID="txtid" runat="server" BorderColor="Black" CssClass="form-control" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </fieldset>

            </asp:Panel>
            <asp:Button ID="btnDummy" runat="server" Style="display: none;" />
            <asp:ModalPopupExtender ID="GridViewDetails" runat="server" TargetControlID="btnDummy"
                PopupControlID="pnlGridViewDetails" BackgroundCssClass="modalBackground" />






          

            <asp:Panel ID="Panel1" CssClass="modalPopup" Width="60%" runat="server" Style="display: none;" Height="50%" ScrollBars="Vertical">
                <fieldset>
                    <div class="box box-info">
                        <div class="box-header with-border" style="background-color: lightblue">
                            <h3 class="box-title" style="color: darkblue; text-align: center; font-size: large">Ph.D Guidance</h3>
                            <div style="text-align: right; margin-top: -20px">
                                <asp:Button ID="Button2" Font-Bold="true" ForeColor="White" BackColor="Black" OnClick="Button2_Click" BorderStyle="Solid" runat="server" Text="X" />
                            </div>
                        </div>

                    </div>

                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-6" style="text-align: left;">
                                <label id="Label8" runat="server">Name of Scholars</label>
                                <asp:TextBox ID="txtnameofscholars" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="phdguidance" runat="server"
                                    ControlToValidate="txtnameofscholars" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-6" style="text-align: left;">
                                <label id="Label10" runat="server">Date of Registration</label>
                                <asp:TextBox ID="txtdateofregistration" runat="server" Font-Size="12px" onkeydown="return false;" BorderColor="Black" autocomplete="off" oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender runat="server" TargetControlID="txtdateofregistration" Format="yyyy-MM-dd" ID="CalendarExtender1"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="phdguidance" runat="server"
                                    ControlToValidate="txtdateofregistration" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label11" runat="server">Ph.D. Topic</label>
                                <asp:TextBox ID="txtphdtopic" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="phdguidance" runat="server"
                                    ControlToValidate="txtphdtopic" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label13" runat="server">Current Status</label>
                                <asp:DropDownList ID="drpcurrentstatus" runat="server" BorderColor="Black" CssClass="form-control">
                                    <asp:ListItem Text="Ongoing" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Completed" Value="2"></asp:ListItem>
                                </asp:DropDownList>

                            </div>

                            <div class="col-md-3" style="text-align: left; margin-top: 20px">
                                <asp:Button ID="btnphdguidance" runat="server" Text="Save" OnClick="btnphdguidance_Click" Height="35px" ValidationGroup="phdguidance" Width="100px" Font-Bold="true" CssClass="form-control" class="btn btn-success btn-sm form-control" />

                            </div>

                            <div class="col-md-3" style="text-align: left;">

                                <asp:TextBox ID="txtphdid" runat="server" BorderColor="Black" CssClass="form-control" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </asp:Panel>


            <asp:Button ID="Button3" runat="server" Style="display: none;" />

            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button3" PopupControlID="Panel1" BackgroundCssClass="modalBackground" />


            <asp:Panel ID="Panel2" CssClass="modalPopup" Width="60%" runat="server" Style="display: none;" Height="50%" ScrollBars="Vertical">
                <fieldset>
                    <div class="box box-info">
                        <div class="box-header with-border" style="background-color: lightblue">
                            <h3 class="box-title" style="color: darkblue; text-align: center; font-size: large">Patents</h3>
                            <div style="text-align: right; margin-top: -20px">
                                <asp:Button ID="Button4" Font-Bold="true" ForeColor="White" BackColor="Black" OnClick="Button4_Click" BorderStyle="Solid" runat="server" Text="X" />
                            </div>
                        </div>

                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-6" style="text-align: left;">
                                <label id="Label9" runat="server">Title of Patent</label>
                                <asp:TextBox ID="txttitleofpatent" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="Patent" runat="server"
                                    ControlToValidate="txttitleofpatent" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-6" style="text-align: left;">
                                <label id="Label12" runat="server">Date of filed/published/Grant</label>
                                <asp:TextBox ID="txtdateoffiled" runat="server" Font-Size="12px" onkeydown="return false;" BorderColor="Black" autocomplete="off" oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender runat="server" TargetControlID="txtdateoffiled" Format="yyyy-MM-dd" ID="CalendarExtender3"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="Patent" runat="server"
                                    ControlToValidate="txtdateoffiled" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label15" runat="server">Remark</label>
                                <asp:TextBox ID="txtremarkpatent" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="Patent" runat="server"
                                    ControlToValidate="txtremarkpatent" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-3" style="text-align: left; margin-top: 20px">
                                <asp:Button ID="btnpatent" runat="server" Text="Save" OnClick="btnpatent_Click" Height="35px" ValidationGroup="Patent" Width="100px" Font-Bold="true" CssClass="form-control" class="btn btn-success btn-sm form-control" />

                            </div>

                            <div class="col-md-3" style="text-align: left;">

                                <asp:TextBox ID="txtidpatent" runat="server" BorderColor="Black" CssClass="form-control" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </asp:Panel>

            <asp:Button ID="Button5" runat="server" Style="display: none;" />

            <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Button5" PopupControlID="Panel2" BackgroundCssClass="modalBackground" />



            <asp:Panel ID="Panel3" CssClass="modalPopup" Width="60%" runat="server" Style="display: none;" Height="50%" ScrollBars="Vertical">
                <fieldset>
                    <div class="box box-info">
                        <div class="box-header with-border" style="background-color: lightblue">
                            <h3 class="box-title" style="color: darkblue; text-align: center; font-size: large">Awards/Recognitions</h3>
                            <div style="text-align: right; margin-top: -20px">
                                <asp:Button ID="Button6" Font-Bold="true" ForeColor="White" BackColor="Black" OnClick="Button6_Click" BorderStyle="Solid" runat="server" Text="X" />
                            </div>
                        </div>

                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label17" runat="server">Date</label>
                                <asp:TextBox ID="txtdateAward" runat="server" Font-Size="12px" onkeydown="return false;" BorderColor="Black" autocomplete="off" oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender runat="server" TargetControlID="txtdateAward" Format="yyyy-MM-dd" ID="CalendarExtender4"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="Award" runat="server"
                                    ControlToValidate="txtdateAward" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label16" runat="server">Internal Awards(Title)</label>
                                <asp:TextBox ID="txtinternalaward" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="Award" runat="server"
                                    ControlToValidate="txtinternalaward" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label18" runat="server">External Awards (Title)</label>
                                <asp:TextBox ID="txtexternalaward" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="Award" runat="server"
                                    ControlToValidate="txtexternalaward" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label19" runat="server">Remarks</label>
                                <asp:TextBox ID="txtremarksaward" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ValidationGroup="Award" runat="server"
                                    ControlToValidate="txtremarksaward" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>


                        <div class="form-group">
                            <div class="col-md-3" style="text-align: left; margin-top: 20px">
                                <asp:Button ID="btnaward" runat="server" Text="Save" OnClick="btnaward_Click" Height="35px" ValidationGroup="Award" Width="100px" Font-Bold="true" CssClass="form-control" class="btn btn-success btn-sm form-control" />

                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-3" style="text-align: left;">

                                <asp:TextBox ID="txtidawarded" runat="server" BorderColor="Black" CssClass="form-control" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </asp:Panel>

            <asp:Button ID="Button7" runat="server" Style="display: none;" />

            <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="Button7" PopupControlID="Panel3" BackgroundCssClass="modalBackground" />

            <asp:Panel ID="Panel4" CssClass="modalPopup" Width="60%" runat="server" Style="display: none;" Height="50%" ScrollBars="Vertical">
                <fieldset>
                    <div class="box box-info">
                        <div class="box-header with-border" style="background-color: lightblue">
                            <h3 class="box-title" style="color: darkblue; text-align: center; font-size: large">Membership of Professional Bodies etc</h3>
                            <div style="text-align: right; margin-top: -20px">
                                <asp:Button ID="Button8" Font-Bold="true" ForeColor="White" BackColor="Black" OnClick="Button8_Click" BorderStyle="Solid" runat="server" Text="X" />
                            </div>
                        </div>

                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label20" runat="server">Membership Duration</label>
                                <asp:TextBox ID="txtmembership" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ValidationGroup="Membership" runat="server"
                                    ControlToValidate="txtmembership" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label21" runat="server">Name of Professional Body</label>
                                <asp:TextBox ID="txtnameofprofessional" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ValidationGroup="Membership" runat="server"
                                    ControlToValidate="txtnameofprofessional" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label22" runat="server">Type of Member</label>
                                <asp:DropDownList ID="drptypemember" runat="server" BorderColor="Black" CssClass="form-control">
                                    <asp:ListItem Text="Annual" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Life" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Fellow" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label23" runat="server">Membership Number</label>
                                <asp:TextBox ID="txtmembershipnumber" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ValidationGroup="Membership" runat="server"
                                    ControlToValidate="txtmembershipnumber" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>


                        <div class="form-group">
                            <div class="col-md-3" style="text-align: left; margin-top: 20px">
                                <asp:Button ID="btnmemberprofessional" runat="server" Text="Save" OnClick="btnmemberprofessional_Click" Height="35px" ValidationGroup="Membership" Width="100px" Font-Bold="true" CssClass="form-control" class="btn btn-success btn-sm form-control" />

                            </div>

                            <div class="col-md-3" style="text-align: left;">

                                <asp:TextBox ID="txtidmembership" runat="server" BorderColor="Black" CssClass="form-control" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </asp:Panel>

            <asp:Button ID="Button9" runat="server" Style="display: none;" />

            <asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="Button9" PopupControlID="Panel4" BackgroundCssClass="modalBackground" />



            <asp:Panel ID="Panel5" CssClass="modalPopup" Width="60%" runat="server" Style="display: none;" Height="50%" ScrollBars="Vertical">
                <fieldset>
                    <div class="box box-info">
                        <div class="box-header with-border" style="background-color: lightblue">
                            <h3 class="box-title" style="color: darkblue; text-align: center; font-size: large">Research Paper/Book Chapter Published</h3>
                            <div style="text-align: right; margin-top: -20px">
                                <asp:Button ID="Button10" Font-Bold="true" ForeColor="White" BackColor="Black" OnClick="Button10_Click" BorderStyle="Solid" runat="server" Text="X" />
                            </div>
                        </div>

                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-6" style="text-align: left;">
                                <label id="Label24" runat="server">Title of Paper/Book Chapter</label>
                                <asp:TextBox ID="txttitleofpaperbook" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ValidationGroup="Researchpaper" runat="server"
                                    ControlToValidate="txttitleofpaperbook" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-6" style="text-align: left;">
                                <label id="Label25" runat="server">Detail of Journal (Journal name,Vol., No, Page Nos., Publisher,Year)</label>
                                <asp:TextBox ID="txtdetailofjournal" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ValidationGroup="Researchpaper" runat="server"
                                    ControlToValidate="txtdetailofjournal" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-md-6" style="text-align: left;">
                                <label id="Label27" runat="server">Detail of Indexing/ISSN/ISBN/DOI</label>
                                <asp:TextBox ID="txtdetailofindexing" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" ValidationGroup="Researchpaper" runat="server"
                                    ControlToValidate="txtdetailofindexing" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-6" style="text-align: left; margin-top: 20px">
                                <asp:Button ID="btnResearchPaper" runat="server" Text="Save" OnClick="btnResearchPaper_Click" Height="35px" ValidationGroup="Researchpaper" Width="100px" Font-Bold="true" CssClass="form-control" class="btn btn-success btn-sm form-control" />

                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-3" style="text-align: left;">

                                <asp:TextBox ID="txtidResearchPaper" runat="server" BorderColor="Black" CssClass="form-control" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </asp:Panel>

            <asp:Button ID="Button11" runat="server" Style="display: none;" />

            <asp:ModalPopupExtender ID="ModalPopupExtender5" runat="server" TargetControlID="Button11" PopupControlID="Panel5" BackgroundCssClass="modalBackground" />



            <asp:Panel ID="Panel6" CssClass="modalPopup" Width="60%" runat="server" Style="display: none;" Height="50%" ScrollBars="Vertical">
                <fieldset>
                    <div class="box box-info">
                        <div class="box-header with-border" style="background-color: lightblue">
                            <h3 class="box-title" style="color: darkblue; text-align: center; font-size: large">Books Published with details</h3>
                            <div style="text-align: right; margin-top: -20px">
                                <asp:Button ID="Button12" Font-Bold="true" ForeColor="White" BackColor="Black" OnClick="Button12_Click" BorderStyle="Solid" runat="server" Text="X" />
                            </div>
                        </div>

                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label26" runat="server">Title of Book</label>
                                <asp:TextBox ID="txttitleofbook" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ValidationGroup="bookpublished" runat="server"
                                    ControlToValidate="txttitleofbook" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label28" runat="server">Authors </label>
                                <asp:TextBox ID="txtauthors" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ValidationGroup="bookpublished" runat="server"
                                    ControlToValidate="txtauthors" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label29" runat="server">Publishing Month & Year</label>
                                <asp:TextBox ID="txtpublishingmonthyear" runat="server" Font-Size="12px" onkeydown="return false;" BorderColor="Black" autocomplete="off" oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender runat="server" TargetControlID="txtpublishingmonthyear" Format="yyyy-MM-dd" ID="CalendarExtender5"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" ValidationGroup="bookpublished" runat="server"
                                    ControlToValidate="txtpublishingmonthyear" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label30" runat="server">Detail of Publisher </label>
                                <asp:TextBox ID="txtdetailofpublisher" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" ValidationGroup="bookpublished" runat="server"
                                    ControlToValidate="txtdetailofpublisher" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label31" runat="server">ISBN</label>
                                <asp:TextBox ID="txtisbn" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" ValidationGroup="bookpublished" runat="server"
                                    ControlToValidate="txtisbn" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-3" style="text-align: left; margin-top: 20px">
                                <asp:Button ID="btnbookpublisheddetail" runat="server" Text="Save" OnClick="btnbookpublisheddetail_Click" Height="35px" ValidationGroup="bookpublished" Width="100px" Font-Bold="true" CssClass="form-control" class="btn btn-success btn-sm form-control" />

                            </div>

                            <div class="col-md-3" style="text-align: left;">

                                <asp:TextBox ID="txtidbookpublished" runat="server" BorderColor="Black" CssClass="form-control" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </asp:Panel>

            <asp:Button ID="Button13" runat="server" Style="display: none;" />

            <asp:ModalPopupExtender ID="ModalPopupExtender6" runat="server" TargetControlID="Button13" PopupControlID="Panel6" BackgroundCssClass="modalBackground" />


            <asp:Panel ID="Panel7" CssClass="modalPopup" Width="60%" runat="server" Style="display: none;" Height="50%" ScrollBars="Vertical">
                <fieldset>
                    <div class="box box-info">
                        <div class="box-header with-border" style="background-color: lightblue">
                            <h3 class="box-title" style="color: darkblue; text-align: center; font-size: large">Seminars/Conferences/QIPs/Workshops/FDPs/Guest Lectures attended as participant/Expert/Presented research paper</h3>
                            <div style="text-align: right; margin-top: -20px">
                                <asp:Button ID="Button14" Font-Bold="true" ForeColor="White" BackColor="Black" OnClick="Button14_Click" BorderStyle="Solid" runat="server" Text="X" />
                            </div>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label33" runat="server">Date </label>
                                <asp:TextBox ID="txtdateseminar" runat="server" Font-Size="12px" onkeydown="return false;" BorderColor="Black" autocomplete="off" oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender runat="server" TargetControlID="txtdateseminar" Format="yyyy-MM-dd" ID="CalendarExtender7"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" ValidationGroup="Seminars" runat="server"
                                    ControlToValidate="txtdateseminar" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label32" runat="server">Name of Event</label>
                                <asp:TextBox ID="txtnameofevent" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator26" ValidationGroup="Seminars" runat="server"
                                    ControlToValidate="txtnameofevent" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label34" runat="server">Title</label>
                                <asp:TextBox ID="txttitleseminar" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" ValidationGroup="Seminars" runat="server"
                                    ControlToValidate="txttitleseminar" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label35" runat="server">Organizing Institute</label>
                                <asp:TextBox ID="txtorganizinginstitute" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" ValidationGroup="Seminars" runat="server"
                                    ControlToValidate="txtorganizinginstitute" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label36" runat="server">Duration of Programme</label>
                                <asp:TextBox ID="txtdurationofprogram" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" ValidationGroup="Seminars" runat="server"
                                    ControlToValidate="txtdurationofprogram" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label37" runat="server">Expert/Participated/Paper Presented</label>
                                <asp:TextBox ID="txtexpertparticipated" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" ValidationGroup="Seminars" runat="server"
                                    ControlToValidate="txtexpertparticipated" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-md-1" style="text-align: left; margin-top: 20px">
                                <asp:Button ID="btnseminar" runat="server" Text="Save" Height="35px" OnClick="btnseminar_Click" ValidationGroup="Seminars" Width="100px" Font-Bold="true" CssClass="form-control" class="btn btn-success btn-sm form-control" />

                            </div>

                            <div class="col-md-3" style="text-align: left;">

                                <asp:TextBox ID="txtidseminar" runat="server" BorderColor="Black" CssClass="form-control" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </asp:Panel>

            <asp:Button ID="Button15" runat="server" Style="display: none;" />

            <asp:ModalPopupExtender ID="ModalPopupExtender7" runat="server" TargetControlID="Button15" PopupControlID="Panel7" BackgroundCssClass="modalBackground" />


            <asp:Panel ID="Panel10" CssClass="modalPopup" Width="60%" runat="server" Style="display: none;" Height="50%" ScrollBars="Vertical">
                <fieldset>
                    <div class="box box-info">
                        <div class="box-header with-border" style="background-color: lightblue">
                            <h3 class="box-title" style="color: darkblue; text-align: center; font-size: large">Techniques Developed/In-Progress/any other</h3>
                            <div style="text-align: right; margin-top: -20px">
                                <asp:Button ID="Button18" Font-Bold="true" ForeColor="White" BackColor="Black" OnClick="Button18_Click" BorderStyle="Solid" runat="server" Text="X" />
                            </div>
                        </div>

                    </div>
                    <div class="box-body">

                        <div class="form-group">
                            <div class="col-md-10" style="text-align: left;">
                                <label id="Label41" runat="server"></label>
                                <asp:TextBox ID="txtTechniquesDeveloped" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator35" ValidationGroup="TechniquesDevloped" runat="server"
                                    ControlToValidate="txtTechniquesDeveloped" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-2" style="text-align: left;">
                                <asp:Button ID="btntechdevlopment" runat="server" Text="Save" Height="35px" OnClick="btntechdevlopment_Click" ValidationGroup="TechniquesDevloped" Width="100px" Font-Bold="true" CssClass="form-control" class="btn btn-success btn-sm form-control" />

                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-2" style="text-align: left;">

                                <asp:TextBox ID="txtidtechnicaldevlopment" runat="server" BorderColor="Black" CssClass="form-control" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </fieldset>

            </asp:Panel>
            <asp:Button ID="Button22" runat="server" Style="display: none;" />

            <asp:ModalPopupExtender ID="ModalPopupExtender10" runat="server" TargetControlID="Button22" PopupControlID="Panel10" BackgroundCssClass="modalBackground" />


            <asp:Panel ID="Panel11" CssClass="modalPopup" Width="60%" runat="server" Style="display: none;" Height="50%" ScrollBars="Vertical">
                <fieldset>
                    <div class="box box-info">
                        <div class="box-header with-border" style="background-color: lightblue">
                            <h3 class="box-title" style="color: darkblue; text-align: center; font-size: large">Any other Academic/Administrative Assignments</h3>
                            <div style="text-align: right; margin-top: -20px">
                                <asp:Button ID="Button23" Font-Bold="true" ForeColor="White" BackColor="Black" OnClick="Button23_Click" BorderStyle="Solid" runat="server" Text="X" />
                            </div>
                        </div>

                    </div>
                    <div class="box-body">

                        <div class="form-group">
                            <div class="col-md-10" style="text-align: left;">
                                <label id="Label40" runat="server"></label>
                                <asp:TextBox ID="txtAcademicAdministrativeAssignments" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator34" ValidationGroup="AcademicAdministrativ" runat="server"
                                    ControlToValidate="txtAcademicAdministrativeAssignments" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-2" style="text-align: left;">
                                <asp:Button ID="btnAcademicAdmin" runat="server" Text="Save" Height="35px" OnClick="btnAcademicAdmin_Click" ValidationGroup="AcademicAdministrativ" Width="100px" Font-Bold="true" CssClass="form-control" class="btn btn-success btn-sm form-control" />

                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-2" style="text-align: left;">

                                <asp:TextBox ID="txtidAcademicAdmin" runat="server" BorderColor="Black" CssClass="form-control" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </asp:Panel>
            <asp:Button ID="Button24" runat="server" Style="display: none;" />

            <asp:ModalPopupExtender ID="ModalPopupExtender11" runat="server" TargetControlID="Button24" PopupControlID="Panel11" BackgroundCssClass="modalBackground" />






            <asp:Panel ID="Panel8" CssClass="modalPopup" Width="60%" runat="server" Style="display: none;" Height="50%" ScrollBars="Vertical">
                <fieldset>
                    <div class="box box-info">
                        <div class="box-header with-border" style="background-color: lightblue">
                            <h3 class="box-title" style="color: darkblue; text-align: center; font-size: large">Records of Additional Activites(Contribution to Development of Workshop/ Laboratories/Infrastructure/Course Curriculum/ Participation in Co-Curricular & Extracurricular activities/Lectures delivered etc.)</h3>
                            <div style="text-align: right; margin-top: -20px">
                                <asp:Button ID="Button16" Font-Bold="true" ForeColor="White" BackColor="Black" OnClick="Button16_Click" BorderStyle="Solid" runat="server" Text="X" />
                            </div>
                        </div>

                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-5" style="text-align: left;">
                                <label id="Label38" runat="server">Date and Duration </label>
                                <asp:TextBox ID="txtdateandduration" runat="server" Font-Size="12px" onkeydown="return false;" BorderColor="Black" autocomplete="off" oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender runat="server" TargetControlID="txtdateandduration" Format="yyyy-MM-dd" ID="CalendarExtender6"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" ValidationGroup="Additional" runat="server"
                                    ControlToValidate="txtdateandduration" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-5" style="text-align: left;">
                                <label id="Label39" runat="server">Details of Activity</label>
                                <asp:TextBox ID="txtdetailsofactivity" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator33" ValidationGroup="Additional" runat="server"
                                    ControlToValidate="txtdetailsofactivity" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-1" style="text-align: left; margin-top: 20px">
                                <asp:Button ID="btnadditional" runat="server" Text="Save" Height="35px" OnClick="btnadditional_Click" ValidationGroup="Additional" Width="100px" Font-Bold="true" CssClass="form-control" class="btn btn-success btn-sm form-control" />

                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-3" style="text-align: left;">

                                <asp:TextBox ID="txtidadditional" runat="server" BorderColor="Black" CssClass="form-control" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </asp:Panel>

            <asp:Button ID="Button17" runat="server" Style="display: none;" />

            <asp:ModalPopupExtender ID="ModalPopupExtender8" runat="server" TargetControlID="Button17" PopupControlID="Panel8" BackgroundCssClass="modalBackground" />



            <asp:Panel ID="Panel9" CssClass="modalPopup" Width="60%" runat="server" Style="display: none;" Height="50%" ScrollBars="Vertical">
                <fieldset>
                    <div class="box box-info">
                        <div class="box-header with-border" style="background-color: lightblue">
                            <h3 class="box-title" style="color: darkblue; text-align: center; font-size: large">Student's Projects Ongoing at UG/PG Level</h3>
                            <div style="text-align: right; margin-top: -20px">
                                <asp:Button ID="Button19" Font-Bold="true" ForeColor="White" BackColor="Black" OnClick="Button19_Click" BorderStyle="Solid" runat="server" Text="X" />
                            </div>
                        </div>

                    </div>
                </fieldset>
                <div class="box-body">
                    <div class="form-group">
                        <!-- UG/PG Level -->
                        <div class="col-md-4" style="text-align: left;">
                            <label id="Label43" runat="server">UG/PG Level</label>
                            <asp:DropDownList ID="ddlUGPG" runat="server" BorderColor="Black" CssClass="form-control">
                                <asp:ListItem Text="UG" Value="UG"></asp:ListItem>
                                <asp:ListItem Text="PG" Value="PG"></asp:ListItem>

                            </asp:DropDownList>

                        </div>
                        <!-- Academic Session -->
                        <div class="col-md-4" style="text-align: left;">
                            <label id="Label42" runat="server">Academic Session</label>
                            <asp:TextBox ID="txtacademicsession" Enabled="false" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator36" ValidationGroup="StudentProject" runat="server"
                                ControlToValidate="txtacademicsession" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <!-- Semester -->
                        <div class="col-md-4" style="text-align: left;">
                            <label id="Label44" runat="server">Semester</label>
                            <asp:DropDownList ID="ddlSemester" runat="server" BorderColor="Black" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator38" ValidationGroup="StudentProject" runat="server"
                                ControlToValidate="ddlSemester" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="form-group">
                        <!-- Programme -->
                        <div class="col-md-4" style="text-align: left;">
                            <label id="Label45" runat="server">Programme</label>
                            <asp:DropDownList ID="ddlProgram" runat="server" BorderColor="Black" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator39" ValidationGroup="StudentProject" runat="server"
                                ControlToValidate="ddlProgram" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <!-- Students Name -->
                        <div class="col-md-4" style="text-align: left;">
                            <label id="Label46" runat="server">Students Name</label>
                            <asp:DropDownList ID="ddlStudentList" runat="server" BorderColor="Black" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator40" ValidationGroup="StudentProject" runat="server"
                                ControlToValidate="ddlStudentList" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <!-- Project Topic -->
                        <div class="col-md-4" style="text-align: left;">
                            <label id="Label47" runat="server">Project Topic</label>
                            <asp:TextBox ID="txtProjectTopic" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator41" ValidationGroup="StudentProject" runat="server"
                                ControlToValidate="txtProjectTopic" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="form-group">
                        <!-- Month 1 -->
                        <div class="col-md-4" style="text-align: left;">
                            <label id="Label48" runat="server">Month 1</label>
                            <asp:TextBox ID="txtmonth1" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator42" ValidationGroup="StudentProject" runat="server"
                                ControlToValidate="txtmonth1" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <!-- Month 2 -->
                        <div class="col-md-4" style="text-align: left;">
                            <label id="Label49" runat="server">Month 2</label>
                            <asp:TextBox ID="txtmonth2" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator43" ValidationGroup="StudentProject" runat="server"
                                ControlToValidate="txtmonth2" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <!-- Month 3 -->
                        <div class="col-md-4" style="text-align: left;">
                            <label id="Label50" runat="server">Month 3</label>
                            <asp:TextBox ID="txtmonth3" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator44" ValidationGroup="StudentProject" runat="server"
                                ControlToValidate="txtmonth3" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="form-group">
                        <!-- Month 4 -->
                        <div class="col-md-4" style="text-align: left;">
                            <label id="Label51" runat="server">Month 4</label>
                            <asp:TextBox ID="txtmonths4" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator45" ValidationGroup="StudentProject" runat="server"
                                ControlToValidate="txtmonths4" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <!-- Overall Evaluation -->
                        <div class="col-md-4" style="text-align: left;">
                            <label id="Label52" runat="server">Overall Evaluation</label>
                            <asp:TextBox ID="txtOverallEvaluation" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator46" ValidationGroup="StudentProject" runat="server"
                                ControlToValidate="txtOverallEvaluation" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <!-- Save Button -->
                        <div class="col-md-4" style="text-align: left; margin-top: 20px;">
                            <asp:Button ID="Button21" runat="server" Text="Save" Height="35px" ValidationGroup="StudentProject" OnClick="Button21_Click" Width="100px" Font-Bold="true" CssClass="btn btn-success btn-sm form-control" />
                            <asp:TextBox ID="txtidstudentongoing" runat="server" Visible="false" BorderColor="Black" CssClass="form-control"></asp:TextBox>

                        </div>
                    </div>
                </div>


            </asp:Panel>

            <asp:Button ID="Button20" runat="server" Style="display: none;" />

            <asp:ModalPopupExtender ID="ModalPopupExtender9" runat="server" TargetControlID="Button20" PopupControlID="Panel9" BackgroundCssClass="modalBackground" />

            <asp:Panel ID="Panel12" CssClass="modalPopup" Width="60%" runat="server" Style="display: none;" Height="50%" ScrollBars="Vertical">
                <fieldset>
                    <div class="box box-info">
                        <div class="box-header with-border" style="background-color: lightblue">
                            <h3 class="box-title" style="color: darkblue; text-align: center; font-size: large">TEACHING LOAD at UG/PG/PhD Level (Per week)</h3>
                            <div style="text-align: right; margin-top: -20px">
                                <asp:Button ID="Button26" Font-Bold="true" ForeColor="White" BackColor="Black" OnClick="Button26_Click" BorderStyle="Solid" runat="server" Text="X" />
                            </div>
                        </div>

                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label55" runat="server">Program</label>
                                <asp:DropDownList ID="Drpprogram" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Drpprogram_SelectedIndexChanged" BorderColor="Black" CssClass="form-control">
                                </asp:DropDownList>

                            </div>


                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label66" runat="server">Semester</label>
                                <%-- <asp:TextBox ID="txtsemteacher" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator47" ValidationGroup="TEACHINGLOAD" runat="server"
                                                ControlToValidate="txtsemteacher" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>--%>

                                <asp:DropDownList ID="drpsemteacher" runat="server" BorderColor="Black" AutoPostBack="true" OnSelectedIndexChanged="drpsemteacher_SelectedIndexChanged" CssClass="form-control">
                                    <%-- <asp:ListItem Text="I" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="II" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="III" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="IV" Value="3"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>



                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label56" runat="server">Student Strength</label>
                                <asp:TextBox ID="txtstudentstrength" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator48" ValidationGroup="TEACHINGLOAD" runat="server"
                                    ControlToValidate="txtstudentstrength" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>


                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label58" runat="server">Course Code</label>
                                <asp:DropDownList ID="drpcoursecode" runat="server" BorderColor="Black" OnSelectedIndexChanged="drpcoursecode_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                    <%--<asp:ListItem Text="BCA-001" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="BBA-002" Value="1"></asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator37" ValidationGroup="TEACHINGLOAD" runat="server"
                                    ControlToValidate="drpcoursecode" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label57" runat="server">Course Name</label>
                                <asp:TextBox ID="drpcoursName" runat="server" BorderColor="Black" CssClass="form-control">
                                                <%--<asp:ListItem Text="Bachelor of Computer Applications" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Bachelor of Business Administration" Value="1"></asp:ListItem>--%>
                                </asp:TextBox>
                            </div>




                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label59" runat="server">L</label>
                                <asp:TextBox ID="txtl" runat="server" BorderColor="Black" onkeypress="return numeric(event)" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator51" ValidationGroup="TEACHINGLOAD" runat="server"
                                    ControlToValidate="txtl" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>


                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label60" runat="server">T</label>
                                <asp:TextBox ID="txtt" runat="server" BorderColor="Black" onkeypress="return numeric(event)" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator52" ValidationGroup="TEACHINGLOAD" runat="server"
                                    ControlToValidate="txtt" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>


                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label61" runat="server">P</label>
                                <asp:TextBox ID="txtp" runat="server" BorderColor="Black" onkeypress="return numeric(event)" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator53" ValidationGroup="TEACHINGLOAD" runat="server"
                                    ControlToValidate="txtp" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label62" runat="server">C</label>
                                <asp:TextBox ID="txtc" runat="server" BorderColor="Black" onkeypress="return numeric(event)" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator54" ValidationGroup="TEACHINGLOAD" runat="server"
                                    ControlToValidate="txtc" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label63" runat="server">Course category</label>
                                <asp:DropDownList ID="drpcorsecategory" runat="server" BorderColor="Black" CssClass="form-control">
                                    <asp:ListItem Text="Theory" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Lab" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Project" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Other" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-1" style="text-align: left; margin-top: 20px">
                                <asp:Button ID="Button27" runat="server" Text="Save" Height="35px" Width="100px" OnClick="Button27_Click" Font-Bold="true" ValidationGroup="TEACHINGLOAD" CssClass="form-control" class="btn btn-success btn-sm form-control" />

                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-3" style="text-align: left;">

                                <asp:TextBox ID="txtteachingloadid" runat="server" BorderColor="Black" CssClass="form-control" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </asp:Panel>

            <asp:Button ID="Button28" runat="server" Style="display: none;" />
            <asp:ModalPopupExtender ID="ModalPopupExtender13" runat="server" TargetControlID="Button28" PopupControlID="Panel12" BackgroundCssClass="modalBackground" />

            <asp:Panel ID="Panel13" CssClass="modalPopup" Width="60%" runat="server" Style="display: none;" Height="50%" ScrollBars="Vertical">
                <fieldset>
                    <div class="box box-info">
                        <div class="box-header with-border" style="background-color: lightblue">
                            <h3 class="box-title" style="color: darkblue; text-align: center; font-size: large">Theory Course 01</h3>
                            <div style="text-align: right; margin-top: -20px">
                                <asp:Button ID="Button30" Font-Bold="true" ForeColor="White" OnClick="Button30_Click" BackColor="Black" BorderStyle="Solid" runat="server" Text="X" />
                            </div>
                        </div>

                    </div>
                    <div class="box-body">
                    </div>
                    <div class="box-body">


                        <div class="form-group">

                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label68" runat="server">Course Code</label>
                                <asp:TextBox ID="txtcoursecodetheory" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label69" runat="server">Course Name</label>
                                <asp:TextBox ID="txtcoursenametheory" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label65" runat="server">Semester</label>
                                <asp:TextBox ID="txtsemestertheory" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>

                            </div>

                        </div>
                        <div class="form-group">
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label70" runat="server">No. of Credits</label>
                                <asp:TextBox ID="txtnoofcreditstheory" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>

                            </div>
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label64" runat="server">Program</label>
                                <asp:TextBox ID="txtprogramtheory" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>

                            </div>
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label67" runat="server">College</label>
                                <asp:TextBox ID="txtcollegetheory" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>

                            </div>
                        </div>

                        <div class="form-group">
                            <!-- Section for Programme Educational Objectives (PEOs) -->
                            <div class="col-md-12" style="text-align: left;">
                                <label id="LabelPEOs" runat="server"><b>Programme Educational Objectives (PEOs)</b></label>
                                <asp:Repeater ID="RepeaterPEOs" runat="server">
                                    <HeaderTemplate>
                                        <table class="table table-bordered">
                                            <thead>
                                                <tr>
                                                    <th>PEO</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("PEO") %></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                </table>
           
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>

                        <div class="form-group">
                            <!-- Section for Programme Outcomes (POs) -->
                            <div class="col-md-12" style="text-align: left;">
                                <label id="LabelPOs" runat="server"><b>Programme Outcomes (POs)</b></label>
                                <asp:Repeater ID="RepeaterPOs" runat="server">
                                    <HeaderTemplate>
                                        <table class="table table-bordered">
                                            <thead>
                                                <tr>
                                                    <th>PO</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("PO") %></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                </table>
           
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>


                        <div class="form-group">
                            <!-- Section for Programme Outcomes (POs) -->
                            <div class="col-md-12" style="text-align: left;">
                                <label id="Label127" runat="server"><b>Programme Specific Outcomes (PSOs)</b></label>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <HeaderTemplate>
                                        <table class="table table-bordered">
                                            <thead>
                                                <tr>
                                                    <th>PO</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("PSO") %></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                </table>
           
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>

                        <div class="form-group">
                            <!-- Section for Programme Outcomes (POs) -->
                            <div class="col-md-12" style="text-align: left;">
                                <label id="Label128" runat="server"><b>Course Outcomes (COs)</b></label>
                                <asp:Repeater ID="Repeater2" runat="server">
                                    <HeaderTemplate>
                                        <table class="table table-bordered">
                                            <thead>
                                                <tr>
                                                    <th>PO</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("CO") %></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                </table>
           
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>

                        <div class="form-group">
                            <!-- Section for Programme Outcomes (POs) -->
                            <div class="col-md-12" style="text-align: left;">
                                <label id="Label129" runat="server"><b>Mapping / Alignment of PEOs with POs</b></label>
                                <table class="table table-bordered">
                                    <thead>
                                        <tr>
                                            <th>Programme Outcomes</th>
                                            <th>PO-1</th>
                                            <th>PO-2</th>
                                            <th>PO-3</th>
                                            <th>PO-4</th>
                                            <th>PO-5</th>
                                            <th>PO-6</th>
                                            <th>PO-7</th>
                                            <th>PO-8</th>
                                            <th>PO-9</th>
                                            <th>PO-10</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="Repeater3" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("PEO") %></td>
                                                    <td><%# Eval("PO1") %></td>
                                                    <td><%# Eval("PO2") %></td>
                                                    <td><%# Eval("PO3") %></td>
                                                    <td><%# Eval("PO4") %></td>
                                                    <td><%# Eval("PO5") %></td>
                                                    <td><%# Eval("PO6") %></td>
                                                    <td><%# Eval("PO7") %></td>
                                                    <td><%# Eval("PO8") %></td>
                                                    <td><%# Eval("PO9") %></td>
                                                    <td><%# Eval("PO10") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>



                        <div class="form-group">
                            <!-- Section for Programme Outcomes (POs) -->
                            <div class="col-md-12" style="text-align: left;">
                                <label id="Label130" runat="server"><b>Mapping /Alignment of COs with POs (Programme Articulation Matrix)</b></label>
                                <table class="table table-bordered">
                                    <thead>
                                        <tr>
                                            <th>Programme Outcomes</th>
                                            <th>PO-1</th>
                                            <th>PO-2</th>
                                            <th>PO-3</th>
                                            <th>PO-4</th>
                                            <th>PO-5</th>
                                            <th>PO-6</th>
                                            <th>PO-7</th>
                                            <th>PO-8</th>
                                            <th>PO-9</th>
                                            <th>PO-10</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="Repeater4" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("CO-1") %></td>
                                                    <td><%# Eval("CO-2") %></td>
                                                    <td><%# Eval("CO-3") %></td>
                                                    <td><%# Eval("CO-4") %></td>
                                                    <td><%# Eval("CO-5") %></td>
                                                    <td><%# Eval("CO-6") %></td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>

                        <div class="form-group">
                            <!-- Section for Programme Outcomes (POs) -->
                            <div class="col-md-12" style="text-align: left;">
                                <label id="Label131" runat="server"><b>Mapping /Alignment of COs with PSOs</b></label>
                                <table class="table table-bordered">
                                    <thead>
                                        <tr>
                                            <th>Programme Outcomes</th>
                                            <th>PSO-1</th>
                                            <th>PSO-2</th>
                                            <th>PSO-3</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="Repeater5" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("CO-1") %></td>
                                                    <td><%# Eval("CO-2") %></td>
                                                    <td><%# Eval("CO-3") %></td>
                                                    <td><%# Eval("CO-4") %></td>
                                                    <td><%# Eval("CO-5") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>


                    </div>
                </fieldset>
            </asp:Panel>

            <asp:Button ID="Button32" runat="server" Style="display: none;" />

            <asp:ModalPopupExtender ID="ModalPopupExtender14" runat="server" TargetControlID="Button32" PopupControlID="Panel13" BackgroundCssClass="modalBackground" />

            <asp:Panel ID="Panel14" CssClass="modalPopup" Width="60%" runat="server" Style="display: none;" Height="50%" ScrollBars="Vertical">
                <fieldset>
                    <div class="box box-info">
                        <div class="box-header with-border" style="background-color: lightblue">
                            <h3 class="box-title" style="color: darkblue; text-align: center; font-size: large">Lab Course 01</h3>
                            <div style="text-align: right; margin-top: -20px">
                                <asp:Button ID="Button31" Font-Bold="true" ForeColor="White" BackColor="Black" OnClick="Button31_Click" BorderStyle="Solid" runat="server" Text="X" />
                            </div>
                        </div>

                    </div>

                    <div class="box-body">
                        <div class="form-group">

                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label71" runat="server">Course Code</label>
                                <asp:TextBox ID="txtlabcoursecode" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label72" runat="server">Course Name</label>
                                <asp:TextBox ID="txtlabcoursename" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label73" runat="server">Semester</label>
                                <asp:TextBox ID="txtlabsemester" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>

                            </div>

                        </div>
                        <div class="form-group">
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label74" runat="server">No. of Credits</label>
                                <asp:TextBox ID="txtlabnocredit" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>

                            </div>
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label75" runat="server">Program</label>
                                <asp:TextBox ID="txtlabprogram" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>

                            </div>
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label76" runat="server">College</label>
                                <asp:TextBox ID="txtlabcollege" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>

                            </div>
                        </div>

                        <!-- Section for Programme Educational Objectives (PEOs) -->
                        <div class="col-md-12" style="text-align: left;">
                            <label id="Label132" runat="server"><b>Programme Educational Objectives (PEOs)</b></label>
                            <asp:Repeater ID="Repeater6" runat="server">
                                <HeaderTemplate>
                                    <table class="table table-bordered">
                                        <thead>
                                            <tr>
                                                <th>PEO</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("PEO") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                </table>
           
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>

                    <div class="form-group">
                        <!-- Section for Programme Outcomes (POs) -->
                        <div class="col-md-12" style="text-align: left;">
                            <label id="Label133" runat="server"><b>Programme Outcomes (POs)</b></label>
                            <asp:Repeater ID="Repeater7" runat="server">
                                <HeaderTemplate>
                                    <table class="table table-bordered">
                                        <thead>
                                            <tr>
                                                <th>PO</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("PO") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                </table>
           
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>


                    <div class="form-group">
                        <!-- Section for Programme Outcomes (POs) -->
                        <div class="col-md-12" style="text-align: left;">
                            <label id="Label134" runat="server"><b>Programme Specific Outcomes (PSOs)</b></label>
                            <asp:Repeater ID="Repeater8" runat="server">
                                <HeaderTemplate>
                                    <table class="table table-bordered">
                                        <thead>
                                            <tr>
                                                <th>PO</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("PSO") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                </table>
           
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>

                    <div class="form-group">
                        <!-- Section for Programme Outcomes (POs) -->
                        <div class="col-md-12" style="text-align: left;">
                            <label id="Label135" runat="server"><b>Course Outcomes (COs)</b></label>
                            <asp:Repeater ID="Repeater9" runat="server">
                                <HeaderTemplate>
                                    <table class="table table-bordered">
                                        <thead>
                                            <tr>
                                                <th>PO</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("CO") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                </table>
           
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>

                    <div class="form-group">
                        <!-- Section for Programme Outcomes (POs) -->
                        <div class="col-md-12" style="text-align: left;">
                            <label id="Label136" runat="server"><b>Mapping / Alignment of PEOs with POs</b></label>
                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th>Programme Outcomes</th>
                                        <th>PO-1</th>
                                        <th>PO-2</th>
                                        <th>PO-3</th>
                                        <th>PO-4</th>
                                        <th>PO-5</th>
                                        <th>PO-6</th>
                                        <th>PO-7</th>
                                        <th>PO-8</th>
                                        <th>PO-9</th>
                                        <th>PO-10</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="Repeater10" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("PEO") %></td>
                                                <td><%# Eval("PO1") %></td>
                                                <td><%# Eval("PO2") %></td>
                                                <td><%# Eval("PO3") %></td>
                                                <td><%# Eval("PO4") %></td>
                                                <td><%# Eval("PO5") %></td>
                                                <td><%# Eval("PO6") %></td>
                                                <td><%# Eval("PO7") %></td>
                                                <td><%# Eval("PO8") %></td>
                                                <td><%# Eval("PO9") %></td>
                                                <td><%# Eval("PO10") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>



                    <div class="form-group">
                        <!-- Section for Programme Outcomes (POs) -->
                        <div class="col-md-12" style="text-align: left;">
                            <label id="Label137" runat="server"><b>Mapping /Alignment of COs with POs (Programme Articulation Matrix)</b></label>
                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th>Programme Outcomes</th>
                                        <th>PO-1</th>
                                        <th>PO-2</th>
                                        <th>PO-3</th>
                                        <th>PO-4</th>
                                        <th>PO-5</th>
                                        <th>PO-6</th>
                                        <th>PO-7</th>
                                        <th>PO-8</th>
                                        <th>PO-9</th>
                                        <th>PO-10</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="Repeater11" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("CO-1") %></td>
                                                <td><%# Eval("CO-2") %></td>
                                                <td><%# Eval("CO-3") %></td>
                                                <td><%# Eval("CO-4") %></td>
                                                <td><%# Eval("CO-5") %></td>
                                                <td><%# Eval("CO-6") %></td>
                                                <%-- <td><%# Eval("PO6") %></td>
                            <td><%# Eval("PO7") %></td>
                            <td><%# Eval("PO8") %></td>
                            <td><%# Eval("PO9") %></td>
                            <td><%# Eval("PO10") %></td>--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>

                    <div class="form-group">
                        <!-- Section for Programme Outcomes (POs) -->
                        <div class="col-md-12" style="text-align: left;">
                            <label id="Label138" runat="server"><b>Mapping /Alignment of COs with PSOs</b></label>
                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th>Programme Outcomes</th>
                                        <th>PSO-1</th>
                                        <th>PSO-2</th>
                                        <th>PSO-3</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="Repeater12" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("CO-1") %></td>
                                                <td><%# Eval("CO-2") %></td>
                                                <td><%# Eval("CO-3") %></td>
                                                <td><%# Eval("CO-4") %></td>
                                                <td><%# Eval("CO-5") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </fieldset>
            </asp:Panel>

            <asp:Button ID="Button33" runat="server" Style="display: none;" />

            <asp:ModalPopupExtender ID="ModalPopupExtender15" runat="server" TargetControlID="Button33" PopupControlID="Panel14" BackgroundCssClass="modalBackground" />


            <asp:Panel ID="Panel15" CssClass="modalPopup" Width="80%" runat="server" Style="display: none;" Height="60%" ScrollBars="Vertical">
                <fieldset>
                    <div class="box box-info">
                        <div class="box-header with-border" style="background-color: lightblue">
                            <h3 class="box-title" style="color: darkblue; text-align: center; font-size: large">LESSON PLAN SUMMARY (UNIT WISE) THEORY COURSE 01</h3>
                            <div style="text-align: right; margin-top: -20px">
                                <asp:Button ID="Button35" Font-Bold="true" ForeColor="White" OnClick="Button35_Click" BackColor="Black" BorderStyle="Solid" runat="server" Text="X" />
                            </div>
                        </div>

                    </div>

                    <div class="box-body">
                        <div class="form-group">


                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label81" runat="server">Program</label>
                                <asp:DropDownList ID="drpprogramlessionplantheorycourse1" runat="server" AutoPostBack="true" BorderColor="Black" OnSelectedIndexChanged="drpprogramlessionplantheorycourse1_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>

                            </div>
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label79" runat="server">Semester</label>
                                <asp:DropDownList ID="drpsemesterlessionplantheorycourse1" OnSelectedIndexChanged="drpsemesterlessionplantheorycourse1_SelectedIndexChanged" AutoPostBack="true" runat="server" BorderColor="Black" CssClass="form-control"></asp:DropDownList>
                            </div>

                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label77" runat="server">Course Code</label>
                                <asp:DropDownList ID="drpcoursecodelessionplantheorycourse1" OnSelectedIndexChanged="drpcoursecodelessionplantheorycourse1_SelectedIndexChanged" AutoPostBack="true" runat="server" BorderColor="Black" CssClass="form-control"></asp:DropDownList>
                            </div>


                        </div>
                        <div class="form-group">

                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label78" runat="server">Course Name</label>
                                <asp:TextBox ID="drpcoursenamelessionplantheorycourse1" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label80" runat="server">No. of Credits</label>
                                <asp:TextBox ID="txtnoofcreditlessionplantheorycourse1" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>

                            </div>

                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label82" runat="server">Total Teaching Hrs.</label>
                                <asp:TextBox ID="txttotalteachinghours" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">


                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label139" runat="server">Unit No</label>
                                <asp:DropDownList ID="drpunitno" runat="server" BorderColor="Black" CssClass="form-control">
                                    <asp:ListItem Text="I" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="II" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="III" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="IV" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="V" Value="4"></asp:ListItem>
                                </asp:DropDownList>

                            </div>


                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label140" runat="server">Topics to be covered in this unit</label>
                                <asp:TextBox ID="txttopiccovered" runat="server" BorderColor="Black" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label141" runat="server">Planned Start Date</label>
                                <asp:TextBox ID="txtplanstarteddate" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender runat="server" TargetControlID="txtplanstarteddate" Format="yyyy-MM-dd" ID="CalendarExtender9"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator47" ValidationGroup="Lessionplanunitwise" runat="server"
                                    ControlToValidate="txtplanstarteddate" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>


                            </div>

                        </div>

                        <div class="form-group">


                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label142" runat="server">Planned End Date</label>
                                <asp:TextBox ID="txtplannedenddate" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender runat="server" TargetControlID="txtplannedenddate" Format="yyyy-MM-dd" ID="CalendarExtender11"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator50" ValidationGroup="Lessionplanunitwise" runat="server"
                                    ControlToValidate="txtplannedenddate" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>

                            </div>


                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label143" runat="server">Total Lectures</label>
                                <asp:TextBox ID="txtplannedtotallecture" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label144" runat="server">Actual Start Date</label>
                                <asp:TextBox ID="txtactualstartdate" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender runat="server" TargetControlID="txtactualstartdate" Format="yyyy-MM-dd" ID="CalendarExtender10"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator49" ValidationGroup="Lessionplanunitwise" runat="server"
                                    ControlToValidate="txtactualstartdate" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>


                            </div>

                        </div>


                        <div class="form-group">


                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label145" runat="server">Actual End Date</label>
                                <asp:TextBox ID="txtactualenddate" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender runat="server" TargetControlID="txtactualenddate" Format="yyyy-MM-dd" ID="CalendarExtender12"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator55" ValidationGroup="Lessionplanunitwise" runat="server"
                                    ControlToValidate="txtactualenddate" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>

                            </div>


                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label146" runat="server">Total Lectures</label>
                                <asp:TextBox ID="txtactualtotallecture" runat="server" BorderColor="Black" ValidationGroup="Lessionplanunitwise" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-1" style="text-align: left; margin-top: 20px">
                                <asp:Button ID="Button68" runat="server" Text="Save" Height="35px" Width="100px" OnClick="Button68_Click" Font-Bold="true" ValidationGroup="Lessionplanunitwise" CssClass="form-control" class="btn btn-success btn-sm form-control" />
                            </div>
                        </div>
                        <div class="form-group">

                            <div class="col-md-4" style="text-align: left;">

                                <asp:TextBox ID="txtidlessionplanunitwise" runat="server" BorderColor="Black" CssClass="form-control" Visible="false"></asp:TextBox>
                            </div>




                        </div>

                    </div>

                </fieldset>
            </asp:Panel>

            <asp:Button ID="Button36" runat="server" Style="display: none;" />

            <asp:ModalPopupExtender ID="ModalPopupExtender16" runat="server" TargetControlID="Button36" PopupControlID="Panel15" BackgroundCssClass="modalBackground" />


          <%--  <asp:Panel ID="Panel16" CssClass="modalPopup" Width="60%" runat="server" Style="display: none;" Height="70%" ScrollBars="Vertical">
                <fieldset>
                    <div class="box box-info">
                        <div class="box-header with-border" style="background-color: lightblue">
                            <h3 class="box-title" style="color: darkblue; text-align: center; font-size: large">Lession Plan (LECTURE WISE) Theory Course 01</h3>
                            <div style="text-align: right; margin-top: -20px">
                                <asp:Button ID="Button38" Font-Bold="true" ForeColor="White" OnClick="Button38_Click" BackColor="Black" BorderStyle="Solid" runat="server" Text="X" />
                            </div>
                        </div>

                    </div>
                    <div class="box-body">
                    </div>
                    <div class="box-body">
                        <div class="form-group">


                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label83" runat="server">Program</label>
                                <asp:DropDownList ID="drpprogramlecturewisetheorycourse1" runat="server" BorderColor="Black" OnSelectedIndexChanged="drpprogramlecturewisetheorycourse1_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>

                            </div>
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label86" runat="server">Semester</label>
                                <asp:DropDownList ID="drpsemesterlecturewisetheorycourse1" OnSelectedIndexChanged="drpsemesterlecturewisetheorycourse1_SelectedIndexChanged" AutoPostBack="true" runat="server" BorderColor="Black" CssClass="form-control"></asp:DropDownList>

                            </div>

                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label84" runat="server">Course Code</label>
                                <asp:DropDownList ID="drpcoursecodelecturewisetheorycourse1" runat="server" OnSelectedIndexChanged="drpcoursecodelecturewisetheorycourse1_SelectedIndexChanged" AutoPostBack="true" BorderColor="Black" CssClass="form-control"></asp:DropDownList>
                            </div>


                        </div>
                        <div class="form-group">

                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label85" runat="server">Course Name</label>
                                <asp:TextBox ID="drpcoursenamelecturewisetheorycourse1" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label87" runat="server">No. of Credits</label>
                                <asp:TextBox ID="txtnoofcreditlecturewisetheorycourse1" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>

                            </div>

                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label88" runat="server">Total Teaching Hrs.</label>
                                <asp:TextBox ID="txttotalteachinghrslecturewisetheorycourse1" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>

                            </div>


                        </div>


                        <div class="form-group">
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label147" runat="server">Lecture No.</label>
                                <asp:TextBox ID="txtlecturenolecturewisetheorycourse1" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>

                            </div>

                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label148" runat="server">Unit No.</label>
                                <asp:TextBox ID="txtunitnolecturewisetheorycourse1" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>

                            </div>

                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label149" runat="server">Planned Date</label>
                                <asp:TextBox ID="txtplanneddatelecturewisetheorycourse1" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender runat="server" TargetControlID="txtplanneddatelecturewisetheorycourse1" Format="yyyy-MM-dd" ID="CalendarExtender13"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator56" runat="server"
                                    ControlToValidate="txtplanneddatelecturewisetheorycourse1" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>


                            </div>


                        </div>

                        <div class="form-group">

                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label152" runat="server">Date of actual conduction</label>
                                <asp:TextBox ID="txtdateofactualconductionlecturewisetheorycourse1" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender runat="server" TargetControlID="txtdateofactualconductionlecturewisetheorycourse1" Format="yyyy-MM-dd" ID="CalendarExtender14"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator57" runat="server"
                                    ControlToValidate="txtdateofactualconductionlecturewisetheorycourse1" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>


                            </div>

                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label150" runat="server">Planned</label>
                                <asp:TextBox ID="txtplannedlecturewisetheorycourse1" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>

                            </div>

                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label151" runat="server">Actually Covered</label>
                                <asp:TextBox ID="txtactuallycoveredlecturewisetheorycourse1" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>

                            </div>




                        </div>

                        <div class="form-group">

                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label153" runat="server">Pedagogy/Teaching Methods*</label>
                                <asp:TextBox ID="txtpedagogylecturewisetheorycourse1" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>


                            </div>

                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label154" runat="server">Text Book/Reference Book/Web references</label>
                                <asp:TextBox ID="txtbookreferencelecturewisetheorycourse1" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>

                            </div>

                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label155" runat="server">Lecture outcome(**Bloom’s Taxonomy)</label>
                                <asp:TextBox ID="txtlectureotcomelecturewisetheorycourse1" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label156" runat="server">Course Outcome (CO)</label>
                                <asp:TextBox ID="txtcourseotcomelecturewisetheorycourse1" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-1" style="text-align: left; margin-top: 20px">
                                <asp:Button ID="Button69" runat="server" Text="Save" Height="35px" Width="100px" OnClick="Button69_Click" Font-Bold="true" CssClass="form-control" class="btn btn-success btn-sm form-control" />

                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-4" style="text-align: left;">
                                <asp:TextBox ID="txtidlecturewisetheorycourse1" runat="server" BorderColor="Black" CssClass="form-control" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </asp:Panel>
            <asp:Button ID="Button39" runat="server" Style="display: none;" />
            <asp:ModalPopupExtender ID="ModalPopupExtender17" runat="server" TargetControlID="Button39" PopupControlID="Panel16" BackgroundCssClass="modalBackground" />--%>
           <%-- <asp:Panel ID="Panel17" CssClass="modalPopup" Width="60%" runat="server" Style="display: none;" Height="50%" ScrollBars="Vertical">
                <fieldset>
                    <div class="box box-info">
                        <div class="box-header with-border" style="background-color: lightblue">
                            <h3 class="box-title" style="color: darkblue; text-align: center; font-size: large">Laboratory Work Plan (LAB COURSE 01)</h3>
                            <div style="text-align: right; margin-top: -20px">
                                <asp:Button ID="Button42" Font-Bold="true" ForeColor="White" OnClick="Button42_Click" BackColor="Black" BorderStyle="Solid" runat="server" Text="X" />
                            </div>
                        </div>

                    </div>
                    <div class="box-body">
                    </div>
                    <div class="box-body">
                        <div class="form-group">


                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label89" runat="server">Program</label>
                                <asp:DropDownList ID="drpprogramLaboratoryWork" runat="server" OnSelectedIndexChanged="drpprogramLaboratoryWork_SelectedIndexChanged" AutoPostBack="true" BorderColor="Black" CssClass="form-control"></asp:DropDownList>

                            </div>
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label92" runat="server">Semester</label>
                                <asp:DropDownList ID="drpsemesterLaboratoryWork" runat="server" BorderColor="Black" OnSelectedIndexChanged="drpsemesterLaboratoryWork_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>

                            </div>
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label90" runat="server">Course Code</label>
                                <asp:DropDownList ID="drpcoursecodeLaboratoryWork" runat="server" BorderColor="Black" OnSelectedIndexChanged="drpcoursecodeLaboratoryWork_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                            </div>


                        </div>
                        <div class="form-group">
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label91" runat="server">Course Name</label>
                                <asp:TextBox ID="drpcoursenameLaboratoryWork" runat="server" Enabled="false" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                            </div>


                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label93" runat="server">No. of Credits</label>
                                <asp:TextBox ID="txtnoofcreditsLaboratoryWork" runat="server" BorderColor="Black" ValidationGroup="LaboratoryWork" CssClass="form-control"></asp:TextBox>

                            </div>

                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label94" runat="server">Total Teaching Hrs.</label>
                                <asp:TextBox ID="txttotalteachingLaboratoryWork" runat="server" BorderColor="Black" ValidationGroup="LaboratoryWork" CssClass="form-control"></asp:TextBox>

                            </div>


                        </div>

                        <div class="form-group">
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label157" runat="server">Name of Experiment</label>
                                <asp:TextBox ID="txtnameofexperimentLaboratoryWork" runat="server" ValidationGroup="LaboratoryWork" BorderColor="Black" CssClass="form-control"></asp:TextBox>

                            </div>

                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label158" runat="server">Planned Date</label>
                                <asp:TextBox ID="txtplanneddateLaboratoryWork" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender runat="server" TargetControlID="txtplanneddateLaboratoryWork" Format="yyyy-MM-dd" ID="CalendarExtender15"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator58" ValidationGroup="LaboratoryWork" runat="server"
                                    ControlToValidate="txtplanneddateLaboratoryWork" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>

                            </div>

                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label159" runat="server">Date of Completion </label>
                                <asp:TextBox ID="txtdateofcompletionLaboratoryWork" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender runat="server" TargetControlID="txtdateofcompletionLaboratoryWork" Format="yyyy-MM-dd" ID="CalendarExtender16"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator59" ValidationGroup="LaboratoryWork" runat="server"
                                    ControlToValidate="txtdateofcompletionLaboratoryWork" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>

                            </div>


                        </div>
                        <div class="form-group">
                            <div class="col-md-4" style="text-align: left;">
                                <label id="Label160" runat="server">Remarks</label>
                                <asp:TextBox ID="txtremarkLaboratoryWork" runat="server" BorderColor="Black" ValidationGroup="LaboratoryWork" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-1" style="text-align: left; margin-top: 20px">
                                <asp:Button ID="Button70" runat="server" Text="Save" Height="35px" OnClick="Button70_Click" Width="100px" Font-Bold="true" ValidationGroup="LaboratoryWork" CssClass="form-control" class="btn btn-success btn-sm form-control" />

                            </div>
                        </div>
                        <div class="form-group">

                            <div class="col-md-4" style="text-align: left;">

                                <asp:TextBox ID="txtidLaboratoryWork" runat="server" BorderColor="Black" CssClass="form-control" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </asp:Panel>
            <asp:Button ID="Button43" runat="server" Style="display: none;" />
            <asp:ModalPopupExtender ID="ModalPopupExtender18" runat="server" TargetControlID="Button43" PopupControlID="Panel17" BackgroundCssClass="modalBackground" />
       --%>     <asp:Panel ID="Panel18" CssClass="modalPopup" Width="60%" runat="server" Style="display: none;" Height="50%" ScrollBars="Vertical">
                <fieldset>
                    <div class="box box-info">
                        <div class="box-header with-border" style="background-color: lightblue">
                            <h3 class="box-title" style="color: darkblue; text-align: center; font-size: large">Details of Class Tests
                                <br />
                                <br />
                                Theory Course 01
                            </h3>
                            <div style="text-align: right; margin-top: -20px">
                                <asp:Button ID="Button45" Font-Bold="true" ForeColor="White" BackColor="Black" OnClick="Button45_Click" BorderStyle="Solid" runat="server" Text="X" />
                            </div>
                        </div>

                    </div>
                    <div class="box-body">
                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label95" runat="server">Program</label>
                                <asp:TextBox ID="txtprogramdetailsofclasstext" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>

                            </div>

                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label96" runat="server">Course Code</label>
                                <asp:TextBox ID="txtcoursecodedetailsofclasstext" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label97" runat="server">Course Name</label>
                                <asp:TextBox ID="txtcoursenamedetailsofclasstext" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>


                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label98" runat="server">Semester</label>
                                <asp:TextBox ID="txtsemesterdetailsofclasstext" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                </fieldset>
            </asp:Panel>

           <%-- <asp:Button ID="Button46" runat="server" Style="display: none;" />

            <asp:ModalPopupExtender ID="ModalPopupExtender19" runat="server" TargetControlID="Button46" PopupControlID="Panel18" BackgroundCssClass="modalBackground" />

            <asp:Panel ID="Panel19" CssClass="modalPopup" Width="60%" runat="server" Style="display: none;" Height="50%" ScrollBars="Vertical">
                <fieldset>
                    <div class="box box-info">
                        <div class="box-header with-border" style="background-color: lightblue">
                            <h3 class="box-title" style="color: darkblue; text-align: center; font-size: large">Details of Assignments
                                <br />
                                <br />
                                Theory Course 01
                            </h3>
                            <div style="text-align: right; margin-top: -20px">
                                <asp:Button ID="Button48" Font-Bold="true" ForeColor="White" OnClick="Button48_Click" BackColor="Black" BorderStyle="Solid" runat="server" Text="X" />
                            </div>
                        </div>

                    </div>
                    <div class="box-body">
                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label99" runat="server">Program</label>
                                <asp:TextBox ID="txtprogramdetailsofassignments" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>

                            </div>

                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label100" runat="server">Course Code</label>
                                <asp:TextBox ID="txtcoursecodedetailsofassignments" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label101" runat="server">Course Name</label>
                                <asp:TextBox ID="txtcoursenamedetailsofassignments" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>


                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label102" runat="server">Semester</label>
                                <asp:TextBox ID="txtsemesterdetailsofassignments" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                </fieldset>
            </asp:Panel>--%>

            <asp:Button ID="Button49" runat="server" Style="display: none;" />

            <asp:ModalPopupExtender ID="ModalPopupExtender20" runat="server" TargetControlID="Button49" PopupControlID="Panel19" BackgroundCssClass="modalBackground" />


            <asp:Panel ID="Panel20" CssClass="modalPopup" Width="60%" runat="server" Style="display: none;" Height="50%" ScrollBars="Vertical">
                <fieldset>
                    <div class="box box-info">
                        <div class="box-header with-border" style="background-color: lightblue">
                            <h3 class="box-title" style="color: darkblue; text-align: center; font-size: large">Attendance & Continuous Assessment (Theory Course 01)</h3>
                            <div style="text-align: right; margin-top: -20px">
                                <asp:Button ID="Button50" Font-Bold="true" ForeColor="White" OnClick="Button50_Click" BackColor="Black" BorderStyle="Solid" runat="server" Text="X" />
                            </div>
                        </div>

                    </div>
                    <div class="box-body">
                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label103" runat="server">Program</label>
                                <asp:TextBox ID="txtprogrammattendancecontinuous" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>

                            </div>

                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label104" runat="server">Course Code</label>
                                <asp:TextBox ID="txtcoursecodeattendancecontinuous" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label105" runat="server">Course Name</label>
                                <asp:TextBox ID="txtcoursenameattendancecontinuous" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>


                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label106" runat="server">Semester</label>
                                <asp:TextBox ID="txtsemesterattendancecontinuous" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                </fieldset>
            </asp:Panel>

            <asp:Button ID="Button51" runat="server" Style="display: none;" />

            <asp:ModalPopupExtender ID="ModalPopupExtender21" runat="server" TargetControlID="Button51" PopupControlID="Panel20" BackgroundCssClass="modalBackground" />




            <asp:Panel ID="Panel21" CssClass="modalPopup" Width="60%" runat="server" Style="display: none;" Height="50%" ScrollBars="Vertical">
                <fieldset>
                    <div class="box box-info">
                        <div class="box-header with-border" style="background-color: lightblue">
                            <h3 class="box-title" style="color: darkblue; text-align: center; font-size: large">Attendance & Continuous Assessment (Lab Course 01)</h3>
                            <div style="text-align: right; margin-top: -20px">
                                <asp:Button ID="Button53" Font-Bold="true" ForeColor="White" OnClick="Button53_Click" BackColor="Black" BorderStyle="Solid" runat="server" Text="X" />
                            </div>
                        </div>

                    </div>
                    <div class="box-body">
                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label107" runat="server">Program</label>
                                <asp:TextBox ID="txtprogramAttendancelab1" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>

                            </div>

                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label108" runat="server">Course Code</label>
                                <asp:TextBox ID="txtcoursecodeAttendancelab1" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label109" runat="server">Course Name</label>
                                <asp:TextBox ID="txtcoursenameAttendancelab1" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>


                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label110" runat="server">Semester</label>
                                <asp:TextBox ID="txtsemesterAttendancelab1" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                </fieldset>
            </asp:Panel>

            <asp:Button ID="Button54" runat="server" Style="display: none;" />

            <asp:ModalPopupExtender ID="ModalPopupExtender22" runat="server" TargetControlID="Button54" PopupControlID="Panel21" BackgroundCssClass="modalBackground" />


            <asp:Panel ID="Panel22" CssClass="modalPopup" Width="60%" runat="server" Style="display: none;" Height="50%" ScrollBars="Vertical">
                <fieldset>
                    <div class="box box-info">
                        <div class="box-header with-border" style="background-color: lightblue">
                            <h3 class="box-title" style="color: darkblue; text-align: center; font-size: large">Attendance of Slow Learners (Theory Course 01)</h3>
                            <div style="text-align: right; margin-top: -20px">
                                <asp:Button ID="Button56" Font-Bold="true" ForeColor="White" OnClick="Button56_Click" BackColor="Black" BorderStyle="Solid" runat="server" Text="X" />
                            </div>
                        </div>

                    </div>
                    <div class="box-body">
                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label111" runat="server">Program</label>
                                <asp:TextBox ID="txtprogramattendanceslowTheory1" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>

                            </div>

                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label112" runat="server">Course Code</label>
                                <asp:TextBox ID="txtcoursecodeattendanceslowTheory1" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label113" runat="server">Course Name</label>
                                <asp:TextBox ID="txtcoursenameattendanceslowTheory1" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>


                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label114" runat="server">Semester</label>
                                <asp:TextBox ID="txtsemesterattendanceslowTheory1" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                </fieldset>
            </asp:Panel>

            <asp:Button ID="Button57" runat="server" Style="display: none;" />

            <asp:ModalPopupExtender ID="ModalPopupExtender23" runat="server" TargetControlID="Button57" PopupControlID="Panel22" BackgroundCssClass="modalBackground" />


            <asp:Panel ID="Panel23" CssClass="modalPopup" Width="60%" runat="server" Style="display: none;" Height="50%" ScrollBars="Vertical">
                <fieldset>
                    <div class="box box-info">
                        <div class="box-header with-border" style="background-color: lightblue">
                            <h3 class="box-title" style="color: darkblue; text-align: center; font-size: large">Attendance of Advanced Learners (Theory Course 01)</h3>
                            <div style="text-align: right; margin-top: -20px">
                                <asp:Button ID="Button60" Font-Bold="true" ForeColor="White" OnClick="Button60_Click" BackColor="Black" BorderStyle="Solid" runat="server" Text="X" />
                            </div>
                        </div>

                    </div>
                    <div class="box-body">
                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label115" runat="server">Program</label>
                                <asp:TextBox ID="txtprogramAdvancedLearnerstheory1" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>

                            </div>

                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label116" runat="server">Course Code</label>
                                <asp:TextBox ID="txtcoursecodeAdvancedLearnerstheory1" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label117" runat="server">Course Name</label>
                                <asp:TextBox ID="txtcoursenameAdvancedLearnerstheory1" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>


                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label118" runat="server">Semester</label>
                                <asp:TextBox ID="txtsemesterAdvancedLearnerstheory1" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                </fieldset>
            </asp:Panel>

            <asp:Button ID="Button61" runat="server" Style="display: none;" />

            <asp:ModalPopupExtender ID="ModalPopupExtender24" runat="server" TargetControlID="Button61" PopupControlID="Panel23" BackgroundCssClass="modalBackground" />


            <asp:Panel ID="Panel24" CssClass="modalPopup" Width="60%" runat="server" Style="display: none;" Height="50%" ScrollBars="Vertical">
                <fieldset>
                    <div class="box box-info">
                        <div class="box-header with-border" style="background-color: lightblue">
                            <h3 class="box-title" style="color: darkblue; text-align: center; font-size: large">Attendance of Extra Classes</h3>
                            <div style="text-align: right; margin-top: -20px">
                                <asp:Button ID="Button63" Font-Bold="true" ForeColor="White" OnClick="Button63_Click" BackColor="Black" BorderStyle="Solid" runat="server" Text="X" />
                            </div>
                        </div>

                    </div>
                    <div class="box-body">
                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label119" runat="server">Program</label>
                                <asp:TextBox ID="txtprogramextraclass" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>

                            </div>

                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label120" runat="server">Course Code</label>
                                <asp:TextBox ID="txtcoursecodeextraclass" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label121" runat="server">Course Name</label>
                                <asp:TextBox ID="txtcoursenameextraclass" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>


                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label122" runat="server">Semester</label>
                                <asp:TextBox ID="txtsemesterextraclass" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                </fieldset>
            </asp:Panel>

            <asp:Button ID="Button64" runat="server" Style="display: none;" />

            <asp:ModalPopupExtender ID="ModalPopupExtender25" runat="server" TargetControlID="Button64" PopupControlID="Panel24" BackgroundCssClass="modalBackground" />


            <asp:Panel ID="Panel25" CssClass="modalPopup" Width="60%" runat="server" Style="display: none;" Height="50%" ScrollBars="Vertical">
                <fieldset>
                    <div class="box box-info">
                        <div class="box-header with-border" style="background-color: lightblue">
                            <h3 class="box-title" style="color: darkblue; text-align: center; font-size: large">Attendance of Competitive Classes</h3>
                            <div style="text-align: right; margin-top: -20px">
                                <asp:Button ID="Button65" Font-Bold="true" ForeColor="White" OnClick="Button65_Click" BackColor="Black" BorderStyle="Solid" runat="server" Text="X" />
                            </div>
                        </div>

                    </div>
                    <div class="box-body">
                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label123" runat="server">Program</label>
                                <asp:TextBox ID="txtprogramCompetitiveClasses" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>

                            </div>

                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label124" runat="server">Course Code</label>
                                <asp:TextBox ID="txtcoursecodeCompetitiveClasses" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label125" runat="server">Course Name</label>
                                <asp:TextBox ID="txtcoursenameCompetitiveClasses" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>


                            <div class="col-md-3" style="text-align: left;">
                                <label id="Label126" runat="server">Semester</label>
                                <asp:TextBox ID="txtsemesterCompetitiveClasses" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                </fieldset>
            </asp:Panel>

            <asp:Button ID="Button66" runat="server" Style="display: none;" />

            <asp:ModalPopupExtender ID="ModalPopupExtender26" runat="server" TargetControlID="Button66" PopupControlID="Panel25" BackgroundCssClass="modalBackground" />
        </asp:Panel>

    </div>


</asp:Content>

