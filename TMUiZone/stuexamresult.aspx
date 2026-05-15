<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="stuexamresult.aspx.cs" Inherits="stuexamresult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


                    <div style="width:830px">
                    <p class="Navigator">TMU &gt;&gt; Examination </p>
      <div id="shadetabs0">
        <ul id="countrytabs" class="shadetabsPage">
          
          <li><a href="StuExamResult.aspx" id="ctl00_ContentPlaceHolder1_linkExamResult" rel="countrycontainer" class="selected"><span>Exam Result</span></a></li>
          
          
          <li><a href="BackPaper.aspx" rel="countrycontainer" ><span>Back Papers Payment</span></a></li>           
          
          <li><a href="ExamSchedule.aspx" rel="countrycontainer" ><span>Exam Schedule</span></a></li>           
          
                    
          
        </ul>
      </div>
		<table id="Table1" cellSpacing="0" cellPadding="4" width="90%"  border="0" align="left">
	
								<TR>
					<TD width="100%" align="center" colspan="2">
						<STRONG><FONT color="#003366">Semester/Year(s) Exam Result</FONT></STRONG>

					</TD>
				</TR>
				<tr>
				    <td align="center"  colspan="2">
                        <asp:DropDownList ID="ddsemester" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddsemester_SelectedIndexChanged"></asp:DropDownList>
                       
				    </td>
				</tr>
				<tr>
				<td  colspan="2">
				    <input type="hidden" name="ctl00$ContentPlaceHolder1$hfNewResultFormat" id="ctl00_ContentPlaceHolder1_hfNewResultFormat" value="1" />
                    <div>
	<asp:GridView ID="grdCourse" runat="server" BackColor="White" BorderColor="#3366CC" Width="823px" BorderStyle="None" BorderWidth="1px" CellPadding="4" AutoGenerateColumns="False">
                    <Columns>

                        <asp:BoundField DataField="Subject Code" HeaderText="Course Code" />
                        <asp:BoundField DataField="Description" HeaderText="Course Title" />
                        <asp:BoundField DataField="Total Maximum" HeaderText="Max Total" DataFormatString="{0:0.00}" />
                        <asp:BoundField DataField="GO" HeaderText="GO" DataFormatString="{0:0.00}" />
                        <asp:BoundField DataField="GP" HeaderText="GP" DataFormatString="{0:0.00}"/>
                        <asp:BoundField DataField="cp" HeaderText="CP" DataFormatString="{0:0.00}"/>
                    </Columns>
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <HeaderStyle BackColor="#CAD3D8" Font-Bold="True" ForeColor="Gray" Height="35px" HorizontalAlign="Left"/>
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" ForeColor="Gray" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                    <SortedDescendingHeaderStyle BackColor="#002876" />
                </asp:GridView>
</div>
				

				</td>
				</tr>
				 
				<tr>
				<td colspan="2">
				                         <div><asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#3366CC" Width="823px" BorderStyle="None" BorderWidth="1px" CellPadding="4" AutoGenerateColumns="False">
                    <Columns>

                        <asp:BoundField DataField="Semester" HeaderText="Semester" />
                        <asp:BoundField DataField="SGPA" HeaderText="SGPA" DataFormatString="{0:0.00}"/>
                        <asp:BoundField DataField="CGPA" HeaderText="CGPA" DataFormatString="{0:0.00}" />
                        <asp:BoundField DataField="Backpaper" HeaderText="Back Papers" DataFormatString="{0:0.00}" />
                    </Columns>
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <HeaderStyle BackColor="#CAD3D8" Font-Bold="True" ForeColor="Gray" Height="35px" HorizontalAlign="Left"/>
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" ForeColor="Gray" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                    <SortedDescendingHeaderStyle BackColor="#002876" />
                </asp:GridView>
                                             
</div>
				</td>
				</tr>
				<tr>
				<td colspan="2"><span id="ctl00_ContentPlaceHolder1_lblFinalStatus" style="font-weight:bold;">Final Result : First Division</span></td>
				</tr>
				<tr>
				<td valign="top" width="40%"> 
				<table id="ctl00_ContentPlaceHolder1_tb1" style="border-collapse:collapse;" border="1" cellpadding="2" bordercolor="#D2D2D2" width="100%">
	<tr>
		<td colspan="2">
					* Mandatory Course. Passing is Mandatory. Credit is not counted for calculation 
                    of SGPA.<br />
                    - For indicative purpose only.<br>No one is responsible for any inadvertent error that may have crept in the results being published on NET. The results published on net are for immediate information to the examinees. These cannot be treated as original mark sheets. Original mark sheets are issued by the University.  </td>
	</tr>
	<tr>
		<td colspan="2"><B>Abbreviation :</B> </td>
	</tr>
	<tr>
		<td colspan="2">&nbsp;</td>
	</tr>
	<tr>
		<td>AB</td>
		<td>Absent</td>
	</tr>
	<tr>
		<td>DE/DC</td>
		<td>Debarred</td>
	</tr>
	<tr>
		<td>UFM</td>
		<td nowrap="nowrap">Unfair Means</td>
	</tr>
	<tr>
		<td>RL</td>
		<td nowrap="nowrap">Result Later</td>
	</tr>
	<tr>
		<td>I</td>
		<td>Incomplete</td>
	</tr>
	<tr>
		<td>EC</td>
		<td>Exam Cancelled</td>
	</tr>
</table>

				</td>
				<td valign="top"  width="60%">
				
				    <table id="ctl00_ContentPlaceHolder1_tb2" style="border-collapse:collapse;" border="1" cellpadding="2" bordercolor="#D2D2D2" width="100%">
	<tr>
		<td>
                                <b>Column</b></td>
		<td>
                                <b>Description</b></td>
	</tr>
	<tr>
		<td>
                                Sem</td>
		<td>
                                Semester</td>
	</tr>
	<tr>
		<td>
                                CE</td>
		<td>
                                Continuous Evaluation Marks Obtained</td>
	</tr>
	<tr>
		<td>
                                MaxCE</td>
		<td>
                                Continuous Evaluation Maximum Marks</td>
	</tr>
	<tr>
		<td>
                                EE</td>
		<td>
                                Endterm Examinatioin Marks Obtained</td>
	</tr>
	<tr>
		<td>
                                MaxEE</td>
		<td>
                                Endterm Examination Maximum Marks</td>
	</tr>
	<tr>
		<td>
                                Total</td>
		<td>
                                Total Marks Obtained</td>
	</tr>
	<tr>
		<td>
                                MaxTotal</td>
		<td>
                                Total Maximum Marks</td>
	</tr>
	<tr>
		<td>
                                GO</td>
		<td>
                                Grade Obtained</td>
	</tr>
	<tr>
		<td>
                                GP</td>
		<td>
                                Grade Points</td>
	</tr>
	<tr>
		<td>
                                CP</td>
		<td>
                                Credit Points</td>
	</tr>
	</table>

				
				</td>
				</tr>
			</table>
		

    </div>
    </td>
  </tr>

  
  <tr>
    <td colspan="3" bgcolor="#999999">&nbsp;</td>
  </tr>
</table>

</asp:Content>

