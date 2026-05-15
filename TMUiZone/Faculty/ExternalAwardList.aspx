<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="ExternalAwardList.aspx.cs" Inherits="Faculty_ExternalAwardList" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <fieldset class="boxBodyInner">
                <table cellpadding="0px" cellspacing="0px">
                    <caption>
                        <br />
                         <tr>
                            <td>Academic Year  </td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:DropDownList ID="drpAcademicYear" Width="100px" Height="20px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpAcademicYear_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td style="width: 20px"></td>
                            <td>Program
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpCourse" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:DropDownList ID="drpCourse" runat="server" AutoPostBack="true" Height="20px" Width="150px" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 20px"></td>
                            <td>Semester/Year
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpSemester" InitialValue="" ErrorMessage="**"  ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:DropDownList ID="drpSemester" runat="server" AutoPostBack="true" Height="20px" OnSelectedIndexChanged="drpSemester_SelectedIndexChanged" Width="120px">
                                </asp:DropDownList>
                            </td>
                             
                            <td style="width: 20px"></td>
                              <td>Course
                            </td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:DropDownList ID="ddlSubject" runat="server" AutoPostBack="true" Height="20px" Width="120px" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                              
                              <td style="width: 10px"></td>
                              <td>
                                <asp:Button ID="btnShow" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" OnClick="btnShow_Click" Height="30px" Width="90px" Text="SHOW" />

                            </td>
                            <td style="width: 10px"></td>
                        </tr>
                        </caption>
                </table>
            </fieldset>

    <fieldset id="Fieldset1" class="boxBodyInner" runat="server" style="width:1000px; text-align:center " >
    

      <div align="center" >
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" SizeToReportContent = "true" AsyncRendering="true" CssClass="active" Border="Solid" ></rsweb:ReportViewer>
        <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
          </div>  
         </fieldset>

</asp:Content>

