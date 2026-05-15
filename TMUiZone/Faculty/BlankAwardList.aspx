<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="BlankAwardList.aspx.cs" Inherits="Faculty_BlankAwardList" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <fieldset class="boxBodyInner" >
        <div>
            <table width="100%">
                <tr align="left" style="width: 100%; text-align: left">
                    <td style="width: 200px; font: bold" colspan="10">EXAM TYPE : &nbsp&nbsp
                          <asp:RadioButton ID="rdInternal" Text="Internal" Width="90px" Font-Bold="true" AutoPostBack="true" Checked="true" runat="server" OnCheckedChanged="rdInternal_CheckedChanged" GroupName="examtype"></asp:RadioButton>
                        <asp:RadioButton ID="rdExternal" Text="External" Width="90px" Font-Bold="true" AutoPostBack="true" runat="server" OnCheckedChanged="rdExternal_CheckedChanged" GroupName="examtype"></asp:RadioButton></td>
                </tr>
            </table>
        </div>
        <br />
        <table cellpadding="0px" cellspacing="0px">
            <caption>



                <tr>

                    <td>Academic Year  </td>
                    <td style="width: 10px"></td>
                    <td>
                        <asp:DropDownList ID="drpAcademicYear" Width="100px" Height="20px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpAcademicYear_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td style="width: 20px"></td>
                    <td>Course
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpCourse" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 10px"></td>
                    <td>
                        <asp:DropDownList ID="drpCourse" runat="server" AutoPostBack="true" Height="20px" Width="150px" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 20px"></td>
                    <td>Semester/Year
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpSemester" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 10px"></td>
                    <td>
                        <asp:DropDownList ID="drpSemester" runat="server" AutoPostBack="true" Height="20px" OnSelectedIndexChanged="drpSemester_SelectedIndexChanged" Width="120px">
                        </asp:DropDownList>
                    </td>

                    <td style="width: 20px"></td>
                    <td>Subject
                    </td>
                    <td style="width: 10px"></td>
                    <td>
                        <asp:DropDownList ID="ddlSubject" runat="server" AutoPostBack="true" Height="20px" Width="120px" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                       <td style="width: 20px"></td>
                          <td>
                                <label>Section  </label>
                            </td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:DropDownList ID="drpSection" runat="server" Height="20px" Width="100px" OnSelectedIndexChanged="drpSection_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                    <td style="width: 20px"></td>
                    <asp:Panel ID="faculty" runat="server" Visible="false">
                        <td>Faculty
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:DropDownList ID="drpFaculty" runat="server" AutoPostBack="true" Height="20px" Width="120px" OnSelectedIndexChanged="drpFaculty_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </asp:Panel>
                    <td style="width: 10px"></td>
                    <td>
                        <asp:Button ID="btnShow" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" OnClick="btnShow_Click" Height="30px" Width="90px" Text="SHOW" />

                    </td>

                </tr>
              <tr id="tddate" runat="server" visible="false">
                       
                        <td>
                           
                            From Date:</td>
                       
                        <td colspan="5">
                            <asp:TextBox ID="txtDateFrom" BackColor="#e8e8e8" Width="120px" Height="25px" runat="server" onkeypress="return false" placeholder="From" onKeyDown="preventBackspace();"></asp:TextBox>
                                <asp:Image src="../Images/Calendar.png" runat="server" Height="25px" Width="25px" alt="" ID="fdate" />
                                <asp:CalendarExtender ID="CalendarExtender2" Format="dd MMM yyyy" runat="server"
                                    CssClass="cal_Theme1" PopupButtonID="fdate"  Enabled="true" TargetControlID="txtDateFrom">
                                </asp:CalendarExtender>
                        </td>
                       
                        <td >
                           
                            To Date:</td>
                       
                        <td colspan="5">
                            <asp:TextBox ID="txtDateTo" autocomplete="off" AutoCompleteType="Disabled"  Width="120px" BackColor="#e8e8e8" Height="25px" runat="server" placeholder="To" onkeypress="return false" onKeyDown="preventBackspace();"></asp:TextBox>
                                <asp:Image src="../Images/Calendar.png" runat="server" Height="25px" Width="25px" alt="" ID="fdate1" />
                                <asp:CalendarExtender ID="CalendarExtender1" Format="dd MMM yyyy" runat="server"
                                    CssClass="cal_Theme1" PopupButtonID="fdate1" Enabled="true" TargetControlID="txtDateTo">
                                </asp:CalendarExtender>
                        </td>
                        
                    </tr>

            </caption>

        </table>
        <table width="100%">
            <tr align="right">
                <td align="left" style="width: 25%; font: bold; visibility: hidden">Report Status :
                    <asp:Label ID="lblReportStatus" ForeColor="Red" runat="server" Text=""></asp:Label></td>

                <asp:Panel ID="tblAR" runat="server" Visible="false">
                    <td align="right" style="width: 62%">
                        <asp:Button ID="btnApprove" BackColor="Green" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px" Width="132px" OnClick="btnApprove_Click" Text="Approved" /></td>
                    <td align="left">
                        <asp:Button ID="btnReject" runat="server" BackColor="Red" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px" Width="132px" OnClick="btnReject_Click" Text="Reject" /></td>
                </asp:Panel>
            </tr>
        </table>
    </fieldset>

    <fieldset id="Fieldset1" class="boxBodyInner" runat="server" style="width: 1000px; text-align: center">


        <div align="center">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" SizeToReportContent="true" AsyncRendering="true" CssClass="active" Border="Solid"></rsweb:ReportViewer>
            <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
        </div>
    </fieldset>
</asp:Content>

