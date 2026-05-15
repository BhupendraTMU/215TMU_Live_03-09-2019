<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reporttest.aspx.cs" Inherits="Reporttest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript">

        function PrintDiv() {

            var divToPrint = document.getElementById('printarea');

            var popupWin = window.open('', '_blank', 'width=500,height=400,location=no,left=200px');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
        }
    </script>

    <style type="text/css">
        .auto-style1 {
            width: 201px;
        }

        .auto-style2 {
            width: 447px;
        }

        .auto-style3 {
            width: 294px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server" style="padding-left: 5%; padding-right: 5%; border: thin; border-width: 1px">
        <div style="text-align: right; width: 100%" id="divprint" runat="server">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblC" Text="Lab No." runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCrNo" Text="" runat="server" MaxLength="9"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnShow" Text="Preview" runat="server" OnClick="btnShow_Click"></asp:Button>
                    </td>
                    <td style="width: 20px"></td>
                    <td>
                        <asp:CheckBox ID="chkHeader" runat="server" Text="With Header" />
                    </td>
                </tr>
            </table>


            <asp:Button ID="BtnPrint" OnClientClick="PrintDiv();" runat="server" Width="10%" Text="Print" Font-Bold="true" BorderColor="WhiteSmoke" />
        </div>
        <div id="MSG" style="border-style: none" runat="server">
            <h1>The Report is under process,Please Wait...</h1>
        </div>
        <div id="printarea" style="border-style: none" runat="server">
            <table style="width: 98%; border-collapse: separate; border-spacing: 0 .5em; margin-left: 25px">
                <tr style="height: 300px;">
                    <td>
                        <asp:Image ID="imglogo" ImageUrl="~/images/LETTERHEAD.png" runat="server" Width="98%" Visible="false" />
                        <asp:Image ID="imglogo1" ImageUrl="~/images/Biochemistry_Header.png" runat="server" Width="98%" Visible="false" />
                        <asp:Image ID="imglogo2" ImageUrl="~/images/Pathology_H.png" runat="server" Width="98%" Visible="false" />
                        <asp:Image ID="imglogo3" ImageUrl="~/images/Microbiology_Header.png" runat="server" Width="98%" Visible="false" />


                    </td>
                </tr>

            </table>
            <table style="border: thin solid #000000; width: 90%; font-family: Arial; border-collapse: separate; border-spacing: 0 .5em; margin-left: 25px">
                <tr>
                    <td style="width: 100px">
                        <asp:Label ID="lbl" runat="server" Text="Patient Name" Font-Bold="true"></asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="lblPatient" runat="server" Text=""></asp:Label>
                    </td>

                    <td style="width: 150px">
                        <asp:Label ID="Label5" runat="server" Text="Sample Date" Font-Bold="true"></asp:Label></td>
                    <td>
                        <asp:Label ID="lblSampleDate" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 150px">
                        <asp:Label ID="Label3" runat="server" Text="Age/Gender" Font-Bold="true"></asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="lblAge" runat="server" Text=""></asp:Label>
                    </td>

                    <td style="width: 100px">
                        <asp:Label ID="Label7" runat="server" Text="Ack. Date" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 200px">
                        <asp:Label ID="lblAck" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px">
                        <asp:Label ID="Label1" runat="server" Text="IP No." Font-Bold="true"></asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="lblIPNo" runat="server" Text=""></asp:Label>
                    </td>

                    <td style="width: 100px">
                        <asp:Label ID="Label15" runat="server" Text="Report Date" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 200px">
                        <asp:Label ID="lblReportDate" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px">
                        <asp:Label ID="Label9" runat="server" Text="CR No." Font-Bold="true"></asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="lblCR" runat="server" Text=""></asp:Label>
                    </td>

                    <td style="width: 100px">
                        <asp:Label ID="Label19" runat="server" Text="Lab No" Font-Bold="true"></asp:Label></td>
                    <td style="width: 200px">
                        <asp:Label ID="lblLab" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 150px">
                        <asp:Label ID="Label13" runat="server" Text="Bed No/Ward" Font-Bold="true"></asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="lblBed" runat="server" Text=""></asp:Label>
                    </td>

                    <td style="width: 100px;" colspan="2" rowspan="4">
                        <table>
                            <tr>

                                <td style="text-align: right; width: 250px">
                                    <asp:Image ID="imgQrCode" runat="server" />
                                </td>
                            </tr>
                        </table>

                    </td>

                </tr>
                <tr>
                    <td style="width: 150px">
                        <asp:Label ID="Label17" runat="server" Text="Referred By" Font-Bold="true"></asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="lblReferred" runat="server" Text=""></asp:Label>
                    </td>



                </tr>
                <tr>
                    <td style="width: 150px">
                        <asp:Label ID="Label21" runat="server" Text="Report Status" Font-Bold="true"></asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                    </td>


                </tr>
                <tr>
                    <td style="width: 150px">
                        <asp:Label ID="Label25" runat="server" Text="Gurdian/Add." Font-Bold="true"></asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="Label26" runat="server" Text='<%#Eval("Local Address") %>'></asp:Label>
                    </td>


                </tr>
            </table>

            </br>
                     
                                            <table style="border: thin solid #000000; width: 90%; font-family: Arial; border-collapse: separate; border-spacing: 0 .5em; margin-left: 25px; text-align: left">

                                                <tr>
                                                    <td style="padding-left: 10px; width: 80px">Test</td>
                                                    <td style="padding-left: 10px; width: 20px"></td>
                                                    <td style="width: 50px; text-align: left">&nbsp&nbsp&nbsp&nbsp Result</td>
                                                    <td style="width: 50px; text-align: center">Unit1&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</td>
                                                    <td style="width: 50px; text-align: center">Reference Range&nbsp&nbsp&nbsp</td>
                                                    <td style="width: 50px; text-align: center">Method</td>
                                                </tr>
                                            </table>



            <table style="width: 92%; margin-top: -10px; padding-left: 20px; margin-left: 2px; text-align: left">
                <tr>
                    <td>
                        <asp:GridView ID="gvService" runat="server" AutoGenerateColumns="false" GridLines="None" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="1px" ShowHeader="false" HeaderStyle-HorizontalAlign="Left" Width="100%" HeaderStyle-Height="25px" HeaderStyle-ForeColor="Black" CellPadding="10"
                            DataKeyNames="Service Name" OnRowDataBound="gvService_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Test" HeaderStyle-Width="320px" HeaderStyle-Height="20px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblService" Height="20px" Font-Underline="true" Font-Bold="true" runat="server" Text='<%# Bind("[Service Name]") %>'></asp:Label>

                                        <asp:Panel ID="pnlOrders" runat="server" BorderStyle="None">
                                            <asp:GridView ID="grdtest" runat="server" BackColor="White" AutoGenerateColumns="false" GridLines="None">
                                                <Columns>

                                                    <asp:TemplateField HeaderStyle-Width="320px" HeaderStyle-Height="20px" ItemStyle-VerticalAlign="Top">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltest" runat="server" Text='<%# Bind("[Field Name]") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOut" Font-Bold="true" ForeColor="Red" runat="server" Width="10px" Text='<%# Bind("[OP]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="180px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblResult" runat="server" Width="160px" Text='<%# Bind("[result]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="180px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnit" runat="server" Text='<%# Bind("[Unit Name]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="180px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblreference" runat="server" Text='<%# Bind("[RR]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="Range">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblrange" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                </Columns>

                                            </asp:GridView>
                                             <asp:GridView ID="GridView1" runat="server" Font-Bold="false" BackColor="White" AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound" GridLines="None" Visible="false">
                                                                                <Columns>

                                                                                    <asp:TemplateField HeaderStyle-Width="320px" HeaderStyle-Height="20px" ItemStyle-VerticalAlign="Top">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbltest" runat="server" Text='<%# Bind("[Field Name]") %>'></asp:Label>

                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="700px">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblResult" runat="server" Width="700px" Text='<%# Bind("[result1]") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                </Columns>

                                                                            </asp:GridView>
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                        
                    </td>
                </tr>

            </table>










            <table style="width: 95%; margin-bottom: 7px; padding-left: 20px; font-family: Arial" id="pnlRemark" runat="server">
                <tr>
                    <td>
                        <asp:Label ID="lblremark" Font-Bold="true" runat="server" Text="Remarks :"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 5px"></td>
                </tr>
                <tr>
                    <td>
                                                            <asp:Label ID="Label2" Font-Bold="true" runat="server" Width="900px" Text="1. ICMR Registration number for Covid-19 is TMMCRCMUP"></asp:Label><br />

                                                            <asp:Label ID="Label4" Font-Bold="true" runat="server" Width="900px" Text="2. Negative result does not rule out the possibility of Covid-19 infection .Presence of inhibitors , mutations &insufficient RNA specific to SARS-CoV-2 can Influence the test result.Kindly correlate the results with clinical findings.A negative in a single upper respiratory tract sample does not rule out SARS-Cov-2 infection. Hence in such case repeat sample should be sent.Lower respiratory tract sample like Sputum, BAL,ET aspirate are appropriate samples especially in severe and progressive lung disease."></asp:Label><br />

                                                            <asp:Label ID="Label29" Font-Bold="true" runat="server" Width="900px" Text="3. Covid-19 Test conducted as per kits approved by ICMR/ CE-1VD /USFDA"></asp:Label><br />

                                                             <asp:Label ID="Label8" Font-Bold="true" runat="server" Width="900px" Text="4. The kit has 100% specificity & sensitivity & detects 1000 copies /ml of SARS Cov-2 RNA in clinical samples as per the literature."></asp:Label><br />

                                                            <asp:Label ID="Label10" Font-Bold="true" runat="server" Width="900px" Text="5. Kindly Consult Referring Physician/Authorized hospital for appropriate followup."></asp:Label><br />
                                                        </td>
                </tr>
                <tr></tr>
                <tr>
                    <td>
                        <asp:Label ID="Label11" Font-Bold="true" runat="server" Width="900px" Text="This report is for the perusal of doctor only.Not for medico legal cases.Clinical correlation is essential.Please contact us in case of unexpected result."></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 15px"></td>
                </tr>

                <tr style="text-align: center">

                    <td>
                        <asp:Label ID="Label12" Font-Bold="true" Width="900px" runat="server" Text="****End  of  Report****"></asp:Label>
                    </td>
                </tr>

            </table>
            <br />
            <br />
            <br />
            <br />
            <br />
            __________________________________________________________________________________________________________________________________________________
           
          
             <table style="width: 95%; margin-bottom: 7px; padding-left: 20px;">

                 <tr>
                     <td class="auto-style3">
                         <asp:Label ID="Label6" Text="TECHNOLOGIST" Font-Bold="true" runat="server"></asp:Label>
                     </td>
                     <td style="text-align: right" class="auto-style2">&nbsp&nbsp&nbsp&nbsp&nbsp
                         <asp:Label ID="Label14" Text="RESIDENT" Font-Bold="true" runat="server"></asp:Label></td>
                     <td style="text-align: right">
                         <asp:Label ID="Label16" Text="MICROBIOLOGIST" Font-Bold="true" runat="server"></asp:Label></td>
                 </tr>
                 <tr>
                     <td class="auto-style3"></td>
                     <td style="text-align: right" class="auto-style2"></td>
                     <td style="text-align: right; text-align: center">
                         <asp:Image ID="Image1" Width="100px" Height="50px" runat="server" />

                     </td>
                 </tr>
                 <tr>
                     <td class="auto-style3"></td>
                     <td style="text-align: right" class="auto-style2"></td>
                     <td style="text-align: right; text-align: center">
                         <asp:Label ID="lbldoc" Font-Size="Small" Width="200px" runat="server" />

                     </td>
                 </tr>
             </table>
            <br />
        </div>
    </form>
</body>
</html>
