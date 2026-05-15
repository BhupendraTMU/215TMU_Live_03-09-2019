<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="FeeBill.aspx.cs" Inherits="FeeBill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div style="width:830px; vertical-align:top; ">
        
<p class="Navigator">
        TMU &gt;&gt; Fee Bill & Receipts</p>
        <div id="shadetabs0">
        <ul id="countrytabs" class="shadetabsPage">
          <li><a href="FeeStructure.aspx" rel="countrycontainer"  ><span>Fee Structure</span></a></li>
           <li><a href="FeeBill.aspx" rel="countrycontainer" class="selected" ><span>Fee Bill</span></a></li>
		  <li><a href="FeeReciepts.aspx" rel="countrycontainer"><span>Fee receipt</span></a></li>
        </ul>
        </div>
        <style>.bottomDashed { BORDER-RIGHT: #8f8f8f 1px dashed; BORDER-TOP: #8f8f8f 1px; BORDER-LEFT: #8f8f8f 1px dashed; BORDER-BOTTOM: #8f8f8f 1px dashed }
	.Grid TD { BORDER-RIGHT: #8f8f8f 1px dotted; BORDER-TOP: #8f8f8f 1px dotted; BORDER-LEFT: #8f8f8f 1px dotted; BORDER-BOTTOM: #8f8f8f 1px dotted }
		</style>
        <span id="ctl00_ContentPlaceHolder1_lblNoStr"></span>
		<div id="ctl00_ContentPlaceHolder1_pnlDetails">
	
				<table cellspacing="2" cellpadding="5" rules="none" align="left" border="0" width="100%" >
					<TBODY>
						<tr>
							<td><span id="ctl00_ContentPlaceHolder1_lblDetails"><BR><b>Samiksha Lamba  (MBA (IB) 2013-2015)</b> : 2nd semester onwards<br><br><br></span></td>
						</tr>
						<tr bgColor="#5e6669">
							<td align="center"><font color="#cccccc">Fee Bill</font><FONT color="#ffffff"> (Academic)</FONT>
							</td>
						</tr>
					<tr>
							<td class="bottomDashed">
										<table width="100%">
									
										<tr>
											<td colspan="3" align="left" bgcolor="#cccccc">
												Semester :
												<span id="ctl00_ContentPlaceHolder1_rptFeeReceipt_ctl01_lblSemster">4</span>
											</td>
										</tr>
										<tr>
											<td colspan="3" align="right">
												<table cellspacing="0" cellpadding="5" rules="all" border="0" id="ctl00_ContentPlaceHolder1_rptFeeReceipt_ctl01_dgReceipt" style="border-width:0px;width:90%;border-collapse:collapse;">
		<tr class="DataGrid_ItemStyle">
			<td valign="top" style="font-weight:bold;width:15%;">Academic</td><td valign="top" style="font-weight:bold;width:10%;">343399</td><td>
																Last Fee Submit Date : <br>Nov 25 2014
															</td><td align="right" valign="bottom" style="width:10%;">235500.00</td><td valign="bottom" style="width:150px;">
																<br />
																<a href="">Print<br>Campus Pay in Slip</a>	<br />
																
															</td>
		</tr><tr class="DataGrid_ItemStyle" style="background-color:#DDDDDD;">
			<td>&nbsp;</td><td>&nbsp;</td><td><b>Total</b></td><td align="right">235500.00</td><td>&nbsp;</td>
		</tr>
	</table>
											</td>
										</tr>
									
										<tr>
											<td colspan="3" align="left" bgcolor="#cccccc">
												Semester :
												<span id="ctl00_ContentPlaceHolder1_rptFeeReceipt_ctl02_lblSemster">3</span>
											</td>
										</tr>
										<tr>
											<td colspan="3" align="right">
												<table cellspacing="0" cellpadding="5" rules="all" border="0" id="ctl00_ContentPlaceHolder1_rptFeeReceipt_ctl02_dgReceipt" style="border-width:0px;width:90%;border-collapse:collapse;">
		<tr class="DataGrid_ItemStyle">
			<td valign="top" style="font-weight:bold;width:15%;">Academic</td><td valign="top" style="font-weight:bold;width:10%;">321664</td><td>
																Last Fee Submit Date : <br>Jun 11 2014
															</td><td align="right" valign="bottom" style="width:10%;">235500.00</td><td valign="bottom" style="width:150px;">
																<br />
																	<br />
																
															</td>
		</tr><tr class="DataGrid_ItemStyle" style="background-color:#DDDDDD;">
			<td>&nbsp;</td><td>&nbsp;</td><td><b>Total</b></td><td align="right">235500.00</td><td>&nbsp;</td>
		</tr>
	</table>
											</td>
										</tr>
									
										<tr>
											<td colspan="3" align="left" bgcolor="#cccccc">
												Semester :
												<span id="ctl00_ContentPlaceHolder1_rptFeeReceipt_ctl03_lblSemster">2</span>
											</td>
										</tr>
										<tr>
											<td colspan="3" align="right">
												<table cellspacing="0" cellpadding="5" rules="all" border="0" id="ctl00_ContentPlaceHolder1_rptFeeReceipt_ctl03_dgReceipt" style="border-width:0px;width:90%;border-collapse:collapse;">
		<tr class="DataGrid_ItemStyle">
			<td valign="top" style="font-weight:bold;width:15%;">Academic</td><td valign="top" style="font-weight:bold;width:10%;">281262</td><td>
																Last Fee Submit Date : <br>Nov 12 2013
															</td><td align="right" valign="bottom" style="width:10%;">224000.00</td><td valign="bottom" style="width:150px;">
																<br />
																	<br />
																
															</td>
		</tr><tr class="DataGrid_ItemStyle" style="background-color:#DDDDDD;">
			<td>&nbsp;</td><td>&nbsp;</td><td><b>Total</b></td><td align="right">224000.00</td><td>&nbsp;</td>
		</tr>
	</table>
											</td>
										</tr>
									
										<tr>
											<td colspan="3">&nbsp;</td>
										</tr>
				</table>
				</TD></TR>
				<tr>
					<td align="left">
						<P>* For indicative purpose only, check with accounts for exact figure</P>
					</td>
				</tr>
				<tr>
				<td align="left">
				<table cellspacing="0" cellpadding="0">
                    <colgroup>
                       
                        <tr height="17">
                            <td height="17" width="33">
                            </td>
                            <td colspan="6">
                                <strong>Notes on Fee Bill -&nbsp;</strong></td>
                        </tr>
                        <tr height="46">
                            <td height="46">
                                1</td>
                            <td colspan="6">
                                Fee may be paid as per the options given below :-</td>
                        </tr>
                        <tr height="25">
                            <td height="25">
                            </td>
                            <td colspan="2" valign="top" class="style4">
                                <b>Option-1:-</b></td>
                            <td colspan="5" valign="top">
                                THROUGH NET BANKING - Fee can be deposited through net banking facility by 
                                following the instructions available on gateway of amizone (www.amizone.net) of 
                                the students.</td>
                        </tr>
                        <tr height="92">
                            <td height="92">
                            </td>
                            <td colspan="2" height="92" valign="top" class="style4">
                                <b>Option-2:-</b></td>
                            <td colspan="5" valign="top">
                                THROUGH AXIS BANK - Fee can be deposited at any Axis Bank before last date of 
                                fee payment by Pay Order / Demand Draft / Cheque /Cash (PAN Number required). 
                                Pay Order / Demand Draft / Cheque should be in favour of &quot;AMITY UNIVERSITY UTTAR 
                                PRADESH A/C Enrolment No.................&quot; payable at the respective city of the 
                                branch where student is depositing the fee. For e.g., in case student is 
                                depositing the fee at Ludhiana branch of Axis Bank, the Pay Order / Demand Draft 
                                / Cheque should be payable at Ludhiana. For this option, Pay-In Slip can be 
                                downloaded from Amizone (www.amizone.net) to deposit the fee. This option will 
                                be available till the date of fee payment without late fee. Student should 
                                mentioned Contact No. on Pay-In-Slip.</td>
                        </tr>
                        <tr height="53">
                            <td height="53">
                            </td>
                            <td colspan="2" height="92" valign="top" class="style4">
                                <b>Option-3:-</b></td>
                            <td colspan="5" valign="top">
                                IN CAMPUS - Pay Order / Demand Draft in favour of &quot;AMITY UNIVERSITY UTTAR 
                                PRADESH&quot; payable at Service Branch, New Delhi can be dropped at the Fee Boxes 
                                placed at Amity University campus at Sector-125 , Noida, between 9.30 a.m. to 
                                4.00p.m., or may be sent by courier to the Accounts Department. The students&#39; 
                                Name, Enrolment No. Contact No., Programme &amp; Batch should be mentioned&nbsp; (IN 
                                CAPITAL LETTER) on the back side of the Pay Order / Demand Draft. Fees through 
                                Cash or Cheque is not acceptable.</td>
                        </tr>
                        <tr height="17">
                            <td height="17">
                                2</td>
                            <td colspan="6">
                                After Last date of payment, late fine will be charged as per Fee Schedule.</td>
                        </tr>
                        <tr height="17">
                            <td height="17">
                                3</td>
                            <td colspan="6">
                                Registration for the semester will not be done without fee payment receipt.</td>
                        </tr>
                        <tr height="17">
                            <td height="17">
                                4</td>
                            <td colspan="6">
                                Students are advised to check the fee status on Amizone after three days of fee 
                                payment and should contact to concerned Axis Bank Branch, for discripencies, if 
                                any.</td>
                        </tr>
                        <tr height="18">
                            <td height="18">
                                5</td>
                            <td colspan="6">
                                Please refer Fee schedule available on Amizone for detailed instructions.</td>
                        </tr>
                    </colgroup>
</table>
</div>

</asp:Content>

