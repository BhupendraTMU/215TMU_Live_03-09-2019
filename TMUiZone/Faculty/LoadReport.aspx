<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="LoadReport.aspx.cs" Inherits="Faculty_LoadReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="updmain">
        <ContentTemplate >--%>
    <fieldset class="boxBody">
     <table>
         <tr>
             <td>
                 <asp:Label ID="Label1" runat="server" 
            Text="Load Report" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
             </td>
             <td>&nbsp&nbsp </td>
             <td>
                  Academic Year:&nbsp&nbsp
                    <asp:DropDownList ID="ddlAcademicYear" Width="100px" Height="20px" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged"></asp:DropDownList>
    
                  <asp:DropDownList ID="ddlOddEvenYear" Width="100px" Height="20px" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddlOddEvenYear_SelectedIndexChanged">
                                <asp:ListItem Value="ODD" Text="ODD"></asp:ListItem>
                                <asp:ListItem Value="EVEN" Text="EVEN"></asp:ListItem>
                                <asp:ListItem Value="YEAR" Text="YEAR"></asp:ListItem>                    
                  </asp:DropDownList>
    <asp:RequiredFieldValidator ID="rfvOddEvenYear" Font-Size="13px"  runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="ddlOddEvenYear" ValidationGroup="g1" ErrorMessage="Please select the Academic Year!"></asp:RequiredFieldValidator>

             </td>
         </tr>
     </table>
      
 </fieldset>

     <fieldset class="boxBodyHeader">   
        
    </fieldset>
    <fieldset class="boxBodyInner">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" SizeToReportContent = "true" AsyncRendering="true" CssClass="active"></rsweb:ReportViewer>
        </fieldset>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

