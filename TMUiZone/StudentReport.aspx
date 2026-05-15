<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentReport.aspx.cs" Inherits="StudentReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
    </title>
   

</head>
<body>
     <script type = "text/javascript">
         function PrintPanel() {
             var panel = document.getElementById("<%=ReportViewer1.ClientID %>");
            var printWindow = window.open('', '', 'height=1700,width=1200');
             //printWindow.document.write('<html><head><title>DIV Contents</title>');            
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {

                printWindow.print();
            }, 1500);
            return false;
        }
    </script>


    <style type="text/css">
        .auto-style1 {
            height: 18px;
        }
    </style>

    <form id="form1" runat="server">
        <div  >
         <centre>  <h2> Student Details for Mentorship 
                
            </h2> </centre>
        </div>
        <div>
            <table runat="server" id="tblsearch" visible="false">
                <tr>
                    <td ></td>
                    <td>Enrollment No. :</td>
                    <td> <asp:TextBox ID="txtEnrollmentNo" runat="server"></asp:TextBox>
    </td>
                    <td> <asp:Button  id="btnShow" ValidationGroup="vgShow"  runat="server"  OnClick="btnShow_Click" Text="Show" />                         
                          </td>
                    <td >&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        <asp:Button  id="btnPrint" ValidationGroup="Print"  runat="server"   Text="Print" OnClick="btnPrint_Click" OnClientClick = "return PrintPanel();"  Visible="false" /></td>
                </tr>

            </table>
            
           
            
        </div>
        <div>
        
        <asp:ScriptManager ID="ScriptManager1" runat="server"  ></asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%"  Height="700px" ></rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
