<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true"  EnableEventValidation="false"  CodeFile="StudentContinueAbsentReport.aspx.cs" Inherits="StudentContinueAbsentReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
   
    .parent
    {
        text-align:center;
        display: block;
        border: 1px solid outset;
    }
    .child
    {
        display: inline-block;       
        width: 500px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updmain">
        <ContentTemplate>
    <asp:Panel ID="pnlList" runat="server" BorderWidth="2px" BorderColor="#ACE9FB" ScrollBars="Vertical">
    <fieldset class="boxBody">
     <table>
         <tr>
             <td>
                 <asp:Label ID="Label1" runat="server" 
            Text="Student Absent List(More than 2 Days) " Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
             </td>
             <td>&nbsp&nbsp </td>
             <td>
                  Academic Year:&nbsp&nbsp
                    <asp:DropDownList ID="ddlAcademicYear" Width="100px" Height="20px" runat="server" AutoPostBack="true" ></asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Font-Size="13px"  runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="ddlAcademicYear" ValidationGroup="g1" ErrorMessage="Please select the Academic Year!"></asp:RequiredFieldValidator>

             </td>
         </tr>
     </table>
      
 </fieldset>

  <fieldset class="boxBodyHeader">   
        
    </fieldset>
         <fieldset class="boxBodyInner"> 
             <div class="row">
                 <div class="col-lg-3  ">
                     <asp:DropDownList ID="ddlCourse" CssClass="form-control dropdown-toggle" width="250px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"></asp:DropDownList>
                     
                 </div>
                 <div class="col-lg-3">
                     <asp:DropDownList ID="ddlSemYear" CssClass="form-control dropdown-toggle" width="250px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSemYear_SelectedIndexChanged"></asp:DropDownList>
                 </div>
                  <div class="col-lg-3">
                     <asp:DropDownList ID="ddlSection" CssClass="form-control dropdown-toggle" width="250px" runat="server"></asp:DropDownList>
                 </div>
                 <div class="col-lg-3">
                     <asp:Button ID="BtnShow" runat="server" class="btn btn-info btn-sm" width="150px" Text="Show" OnClick="BtnShow_Click" />
                     <br/>
                    
                 </div>
             </div>
             <div class="row">
                 <div class="col-lg-12">
                    <br />
                      <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" ShowPrintButton="False" ></rsweb:ReportViewer>
                 </div>
             </div>
             

             </fieldset>


   
       
      </asp:Panel>
  </ContentTemplate>        
    </asp:UpdatePanel>
            </asp:Content>

