<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FA_MM_SOP_Mentor_Mentee_Report.aspx.cs" Inherits="FA_MM_SOP_Mentor_Mentee_Report" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <div>
			<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
			<rsweb:ReportViewer ID="ReportViewer1" runat="server"></rsweb:ReportViewer>
			<h1 style="text-align:center">
				Full Report
              
			</h1>
        </div>
    </form>
</body>
</html>
