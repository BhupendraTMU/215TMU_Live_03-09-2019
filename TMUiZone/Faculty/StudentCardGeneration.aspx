<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentCardGeneration.aspx.cs" Inherits="Faculty_StudentCardGeneration" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type = "text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlSalarySlip.ClientID %>");
            var printWindow = window.open('', '', 'height=500,width=945');
            printWindow.document.write('<html><head><title>DIV Contents</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {

                printWindow.print();
            }, 500);
            return false;
        }
    </script>


    <style type="text/css">
        .auto-style1 {
            height: 18px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <fieldset class="boxBody">
  <asp:Label ID="Label1" runat="server" Text="Student I-Card" 
            Font-Size="12pt" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" 
            ForeColor="#093A62"></asp:Label>
 </fieldset>
    <fieldset class="boxBody"> 
        <table cellpadding="0px" cellspacing="0px" style="width:100%"> 
           <tr>
               <td>&nbsp</td>
               <td width="10%" align="right" ><b> Enrollment No : </b>&nbsp&nbsp</td>
               <td width="15%" align="left" > <asp:TextBox ID="txtEnrollmentNo" runat="server" Width="150px" Height="30px"></asp:TextBox>&nbsp;&nbsp;&nbsp;</td>
               <td width="23%" colspan="2" ><asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" CssClass="btnLogin"/>&nbsp;
               &nbsp&nbsp&nbsp<asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick = "return PrintPanel();" Visible="False" OnClick="btnPrint_Click" CssClass="btnLogin"/>&nbsp</td>
               <td width="12%" ><asp:Button ID="btnsendEmail" runat="server" Text="Send Email"   OnClick="btnsendEmail_Click" Visible="False" CssClass="btnLogin"/></td>
               <td width="50%" colspan="4">
                   <asp:Label runat="server" ID="lblMSG" ForeColor="Red" ></asp:Label>
               </td>              
           </tr>
            <tr>
               <td colspan="10" style="height:10px"> </td>
            </tr>
            <tr>
               <td colspan="10" style="height:10px">
                   <div id="pnldata" runat="server"> 
                     <asp:Panel ID="pnlSalarySlip" runat="server" Visible="false" Font-Size="8">
                         <table>
                             <tr>
                             <td> <table width="100%">
                             <tr>
                             <td width="50%">
                         <table>
                       <tr>
                       <td>&nbsp</td>
                           <td colspan="4">
                               <table >
                                   <tr>                                      
                                       <td rowspan="3">
                                           <asp:Image ID="imgPhoto" runat="server" Style="margin-bottom: 0px" ImageUrl="~/logo/Logo.jpg" Width="50px" Height="50px" /></td>
                                       <td  style="vertical-align:central" align="center"><b>TEERTHANKER MAHAVIR UNIVERSITY</b></td>
                                    </tr>
                                   <tr>
                                        <td style="vertical-align:central" align="center"><b><asp:Label runat="server" ID="lblCollege" ForeColor="#0066ff"></asp:Label></b></td>
                                   </tr>
                                   <tr>
                                        <td style="vertical-align:central" align="center"><b><asp:Label runat="server" ID="lblIdentityCard" Text="IDENTITY CARD" BackColor="Black" ForeColor="White"></asp:Label></b></td>
                                   </tr>
                               </table>
                           </td>                                              
                       </tr>
                       <tr>
                       <td>&nbsp</td>
                       <td>Enrollment No. :</td>
                       <td>:&nbsp<asp:Label runat="server" ID="lblEnrollmentNo"></asp:Label>  </td>
                       <td rowspan="5"> <asp:Image ID="imgStudent" runat="server" Height="100px" Width="100px" ImageAlign="Right" BorderStyle="Groove" Visible="true"   /></td>
                       </tr>
                       <tr>
                       <td>&nbsp</td>
                       <td>Name </td>
                       <td>:&nbsp<asp:Label runat="server" ID="lblName"></asp:Label>  </td>
                       </tr>
                       <tr>
                       <td>&nbsp</td>
                       <td>Father's Name</td>
                       <td>:&nbsp<asp:Label runat="server" ID="lblFatherName"></asp:Label>  </td>
                       </tr>
                       <tr>
                        <td>&nbsp</td>
                       <td>D.O.B</td>
                       <td>:&nbsp<asp:Label runat="server" ID="lblDOB"></asp:Label>  </td>
                       </tr>
                       <tr>
                        <td>&nbsp</td>
                       <td>Programme</td>
                       <td>:&nbsp<asp:Label runat="server" ID="lblProgramme"></asp:Label>  </td>
                       </tr>
                             <tr>
                        <td colspan="3" align="center">
                            Barcode                            
                        </td>                       
                       <td align="right" height="20px">  </td>
                       </tr>
                        <tr>
                        <td colspan="3" align="center">   
                            <asp:Label ID="lblBarcodeNo" runat="server" ></asp:Label>
                        </td>                       
                       <td align="center" ><asp:label runat="server" ID="lblsign" Height="30px" Text="Issuing Authority"></asp:label>  </td>
                       </tr>
                   </table>
                             </td>
                             <td width="50%">
                               
                             </td>
                                 </tr>
                         </table></td>
                             <td> <table width="100%">
                             <tr>
                             <td width="50%">
                    <table>
                                     <tr>
                                         <td>&nbsp</td>
                                         <td colspan="3">In case of emergency Please Contact-</td> 
                                     </tr>
                                     <tr>
                                         <td width="2%">&nbsp</td>
                                         <td width="35%">Guardian</td>                                         
                                         <td width="43%"><asp:Label id="lblGuardian" runat="server"></asp:Label></td>
                                         <td width="20%">Blood Group</td>
                                     </tr>
                                     <tr>
                                         <td>&nbsp</td>
                                         <td colspan="">College</td>                                         
                                         <td><asp:Label id="lblCollegePhoneNo" runat="server"></asp:Label></td>
                                         <td><asp:Label id="lblBloodGroup" runat="server"></asp:Label></td>
                                     </tr>
                                     <tr>
                                         <td colspan="4"><u><b>RESIDENCE</b></u></td>
                                     </tr>
                                     <tr>
                                         <td>&nbsp</td>
                                         <td colspan="2"><asp:Label id="lblAddress1" runat="server"/></td>
                                         <td></td>
                                     </tr>
                                     <tr>
                                         <td>&nbsp</td>
                                         <td>Admission Session: </td>
                                         <td><asp:Label id="lblAdmissionSession" runat="server"></asp:Label></td>
                                         <td height="20"></td>
                                     </tr>
                                     <tr>
                                         <td>&nbsp</td>
                                         <td>Valid Upto : </td>
                                         <td><asp:Label id="lblValidUpto" runat="server"></asp:Label></td>
                                         <td align="right">Student Sign.</td>
                                     </tr>
                                     <tr>
                                         <td colspan="4"  >
                                             
                                         </td>
                                     </tr>
                                     <tr>
                                         <td colspan="4">
                                             IF FOUND MISPLACED, PLEASE RETURN TO THE UNIVERSITY AT
                                             <b>Delhi Road, Moradabad(U.P) PH:0591-2360222</b>
                                             Note:-Duplicate Identity card will be issued on payment of Rs. 500/-
                                             along with the copy of F.I.R.
                                         </td>
                                     </tr>
                                 </table>
                                 </td>
                                 <td width="50%">                               
                                  </td>                                 
                                 </tr>
                             </table></td>
                             </tr>
                         </table>
                        
                        
                         </asp:Panel>
                       </div>
               </td>
            </tr>
            </table>
        </fieldset>
</asp:Content>

