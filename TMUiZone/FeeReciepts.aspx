<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="FeeReciepts.aspx.cs" Inherits="FeeReciepts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width:830px; vertical-align:top; ">
        
<p class="Navigator">
        TMU &gt;&gt; Fee Bill & Receipts</p>
        <div id="shadetabs0">
        <ul id="countrytabs" class="shadetabsPage">
          <li><a href="FeeStructure.aspx" rel="countrycontainer"  ><span>Fee Structure</span></a></li>
           <li><a href="FeeBill.aspx" rel="countrycontainer"  ><span>Fee Bill</span></a></li>
		   <li><a href="FeeReciepts.aspx" rel="countrycontainer" class="selected"><span>Fee receipt</span></a></li>
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
							<td align="center"><font color="#cccccc">Fee Receipt&#160; </font>
							&#160;</td>
						</tr>
					<tr>
							<td class="bottomDashed">
										<table width="100%">
											
									
                                    
										<tr>
											<td align="left" bgcolor="#cccccc" colspan="3">
												Semester :
												<span id="ctl00_ContentPlaceHolder1_rptFeeReceipt_ctl01_lblSemster">4</span>
											</td>
										</tr>
										<tr>
											<td align="right" colspan="3">
												<table cellspacing="0" cellpadding="5" rules="all" border="0" id="ctl00_ContentPlaceHolder1_rptFeeReceipt_ctl01_dgReceipt" style="border-width:0px;width:90%;border-collapse:collapse;">
		<tr class="DataGrid_ItemStyle">
			<td valign="top" style="font-weight:bold;width:15%;">Academic</td><td valign="top" style="font-weight:bold;width:10%;">373078</td><td>DD AXIS BANK LTD [141310]<Br></td><td align="right" valign="bottom" style="width:10%;">235500.00</td><td valign="bottom" style="width:5%;">
																<a href="">Print</a>
															</td>
		</tr><tr class="DataGrid_ItemStyle" style="background-color:#DDDDDD;">
			<td>&nbsp;</td><td>&nbsp;</td><td><b>Total</b></td><td align="right">235500.00</td><td>&nbsp;</td>
		</tr>
	</table>
											</td>
										</tr>
										<tr>
											<td align="right" colspan="3">
												<table cellspacing="0" cellpadding="5" rules="all" border="0" id="ctl00_ContentPlaceHolder1_rptFeeReceipt_ctl01_gdRefund" style="border-width:0px;width:90%;border-collapse:collapse;">
		<tr class="DataGrid_ItemStyle">
			<td>&nbsp;</td><td>&nbsp;</td>
		</tr>
	</table>
											</td>
										</tr>
									
										<tr>
											<td align="left" bgcolor="#cccccc" colspan="3">
												Semester :
												<span id="ctl00_ContentPlaceHolder1_rptFeeReceipt_ctl02_lblSemster">3</span>
											</td>
										</tr>
										<tr>
											<td align="right" colspan="3">
												<table cellspacing="0" cellpadding="5" rules="all" border="0" id="ctl00_ContentPlaceHolder1_rptFeeReceipt_ctl02_dgReceipt" style="border-width:0px;width:90%;border-collapse:collapse;">
		<tr class="DataGrid_ItemStyle">
			<td valign="top" style="font-weight:bold;width:15%;">Academic</td><td valign="top" style="font-weight:bold;width:10%;">346395</td><td>Cash</td><td align="right" valign="bottom" style="width:10%;">235500.00</td><td valign="bottom" style="width:5%;">
																<a href="">Print</a>
															</td>
		</tr><tr class="DataGrid_ItemStyle" style="background-color:#DDDDDD;">
			<td>&nbsp;</td><td>&nbsp;</td><td><b>Total</b></td><td align="right">235500.00</td><td>&nbsp;</td>
		</tr>
	</table>
											</td>
										</tr>
										<tr>
											<td align="right" colspan="3">
												<table cellspacing="0" cellpadding="5" rules="all" border="0" id="ctl00_ContentPlaceHolder1_rptFeeReceipt_ctl02_gdRefund" style="border-width:0px;width:90%;border-collapse:collapse;">
		<tr class="DataGrid_ItemStyle">
			<td>&nbsp;</td><td>&nbsp;</td>
		</tr>
	</table>
											</td>
										</tr>
									
										<tr>
											<td align="left" bgcolor="#cccccc" colspan="3">
												Semester :
												<span id="ctl00_ContentPlaceHolder1_rptFeeReceipt_ctl03_lblSemster">2</span>
											</td>
										</tr>
										<tr>
											<td align="right" colspan="3">
												<table cellspacing="0" cellpadding="5" rules="all" border="0" id="ctl00_ContentPlaceHolder1_rptFeeReceipt_ctl03_dgReceipt" style="border-width:0px;width:90%;border-collapse:collapse;">
		<tr class="DataGrid_ItemStyle">
			<td valign="top" style="font-weight:bold;width:15%;">Academic</td><td valign="top" style="font-weight:bold;width:10%;">307290</td><td>Cash</td><td align="right" valign="bottom" style="width:10%;">224000.00</td><td valign="bottom" style="width:5%;">
																<a href="">Print</a>
															</td>
		</tr><tr class="DataGrid_ItemStyle" style="background-color:#DDDDDD;">
			<td>&nbsp;</td><td>&nbsp;</td><td><b>Total</b></td><td align="right">224000.00</td><td>&nbsp;</td>
		</tr>
	</table>
											</td>
										</tr>
										<tr>
											<td align="right" colspan="3">
												<table cellspacing="0" cellpadding="5" rules="all" border="0" id="ctl00_ContentPlaceHolder1_rptFeeReceipt_ctl03_gdRefund" style="border-width:0px;width:90%;border-collapse:collapse;">
		<tr class="DataGrid_ItemStyle">
			<td>&nbsp;</td><td>&nbsp;</td>
		</tr>
	</table>
											</td>
										</tr>
									
									     <tr>
					                                <td align="left" bgcolor="#cccccc" colspan="3">
						                               Semester :
						                             <span id="ctl00_ContentPlaceHolder1_rptFeeReceipt_ctl04_lblSemster">1</span>
					                             </td>
				                         </tr>
				                         								<tr>
											<td align="right" colspan="3">
												<table cellspacing="0" cellpadding="5" rules="all" border="0" id="ctl00_ContentPlaceHolder1_rptFeeReceipt_ctl04_dgReceiptAdmFee" style="border-width:0px;width:90%;border-collapse:collapse;">
		<tr class="DataGrid_ItemStyle">
			<td valign="top" style="font-weight:bold;width:15%;">Academic</td><td valign="top" style="font-weight:bold;width:10%;">99007</td><td>DD OBC [472663]<Br></td><td align="right" valign="bottom" style="width:10%;">244000.00</td><td valign="bottom" style="width:5%;">
																<a href="">Print</a>
															</td>
		</tr><tr class="DataGrid_AlternateItemStyle">
			<td valign="top" style="font-weight:bold;width:15%;">Hostel</td><td valign="top" style="font-weight:bold;width:10%;">102739</td><td>Cash</td><td align="right" valign="bottom" style="width:10%;">140000.00</td><td valign="bottom" style="width:5%;">
																<a href="">Print</a>
															</td>
		</tr><tr class="DataGrid_ItemStyle" style="background-color:#DDDDDD;">
			<td>&nbsp;</td><td>&nbsp;</td><td><b>Total</b></td><td align="right">384000.00</td><td>&nbsp;</td>
		</tr>
	</table>
											</td>
										</tr>
										<tr>
											<td colspan="3">&#160;</td>
										</tr>
				</table>
				                    
                 </td></tr>

                                     
                <tr bgColor="#5e6669">
							<td align="center"><font color="#cccccc">Fee Receipt Hostel [Senior Students] </font>
							&#160;</td>
			    </tr>
			    <tr>
			        <td align="right">
			            <table cellspacing="0" cellpadding="5" rules="all" border="0" id="ctl00_ContentPlaceHolder1_dgReceiptHostel" style="border-width:0px;width:90%;border-collapse:collapse;">
		<tr class="DataGrid_ItemStyle">
			<td valign="top" style="font-weight:bold;width:15%;">2014-2015</td><td valign="top" style="font-weight:bold;width:10%;">11957</td><td>Cash</td><td align="right" valign="bottom" style="width:10%;">140000</td><td valign="bottom" style="width:5%;">
																<a href="">Print</a>
															</td>
		</tr><tr class="DataGrid_ItemStyle">
			<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>
		</tr>
	</table>
			        </td>
			    </tr>
				<tr>
					<td align="left">
						<P>* For indicative purpose only, check with accounts for exact figure</P>
					</td>
				</tr>
				</TBODY>
				</table>
				
				
</div>

</asp:Content>

