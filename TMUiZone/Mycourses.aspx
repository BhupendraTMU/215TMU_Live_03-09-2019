<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Mycourses.aspx.cs" Inherits="Mycourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div>
        
    <p class="Navigator">
        TMU &gt;&gt; My Courses</p>
    <table width="99%" border="0">
        <tr>
            <td align="right">
                Semester : 
                
                <asp:DropDownList ID="ddsesmester" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddsesmester_SelectedIndexChanged"></asp:DropDownList>
                <%--<select name="ctl00$ContentPlaceHolder1$ddlSem" onchange="javascript:setTimeout('__doPostBack(\'ctl00$ContentPlaceHolder1$ddlSem\',\'\')', 0)" id="ctl00_ContentPlaceHolder1_ddlSem">
	<option value="0">Select Semester</option>
	<option selected="selected" value="4">4</option>
	<option value="3">3</option>
	<option value="2">2</option>
	<option value="1">1</option>

</select>--%>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <div class="Header"><span id="ctl00_ContentPlaceHolder1_lblSem">Semester :<asp:Label ID="semester" runat="server" Text=""></asp:Label> </span></div>
                <%--<div id="ctl00_ContentPlaceHolder1_divGSSC" align="right"><b>[<a href='../gssc/GSSCRegistration.aspx'>GSSC Registration</a>]</b></div>
                	
                <div id="ctl00_ContentPlaceHolder1_divElectives" align="right"><b>[<a href="../Electives/StudentElectivesNew.aspx">Choose Electives</a>]</b></div>
                --%>
            </td>
        </tr>
        <tr>
            <td>
                <%--<table cellspacing="2" cellpadding="2" rules="all" border="0" id="ctl00_ContentPlaceHolder1_dgCoursesGrid" style="border-width:0px;width:100%;">
	<tr style="background-color:#CAD3D8;">
		<td>SNo.</td><td>Course Code</td><td>Course Name</td><td>Type</td><td>Course Syllabus</td><td>Session Plans</td><td>Attendance</td><td>Internal Asses.</td><td>Exam Date</td>
	</tr><tr>
		<td>1
                        
                    </td><td>FIBA701 </td><td>Behavioural Finance</td><td>Compulsory</td><td><a target=_blank href="../../../AdminAmizone/WebForms/Academics/NewSyllabus/1715201472541503.pdf" ><img src="images/ProgramStr.gif" border=0 /></a></td><td><a href="SessionPlansNew.aspx?crs=199930" ><img src="images/SessionPlanExists.gif" border=0 /></a></td><td><a target=_blank href="AttendanceDetails.aspx?cid=199930">31/44 (70.45)</a></td><td>18.00[18.00+0]/30.00</td><td>07-05-2015 10:00</td>
	</tr><tr>
		<td>2
                        
                    </td><td>BC703 </td><td>Business Etiquette and Protocol</td><td>Compulsory</td><td><a target=_blank href="../../../AdminAmizone/WebForms/Academics/NewSyllabus/1015201572378341.pdf" ><img src="images/ProgramStr.gif" border=0 /></a></td><td><a href="SessionPlansNew.aspx?crs=204677" ><img src="images/SessionPlanExists.gif" border=0 /></a></td><td><a target=_blank href="AttendanceDetails.aspx?cid=204677">10/15 (66.67)</a></td><td>27.00[27.00+0]/40.00</td><td>02-05-2015 10:00</td>
	</tr><tr>
		<td>3
                        
                    </td><td>MSDS600</td><td>Dissertation</td><td>Compulsory</td><td><a href="#" ><img src="images/ProgramStr.gif" border=0 /></a></td><td><a href="SessionPlansNew.aspx?crs=203222" ><img src="images/SessionPlanExists.gif" border=0 /></a></td><td><a target=_blank href="AttendanceDetails.aspx?cid=203222">0/0 (0)</a></td><td>0.00[0.00+0]/40</td><td>&nbsp;</td>
	</tr><tr>
		<td>4
                        
                    </td><td>IB734        </td><td>Global Outsourcing</td><td>Compulsory</td><td><a target=_blank href="../../../AdminAmizone/WebForms/Academics/NewSyllabus/1615201472574099.pdf" ><img src="images/ProgramStr.gif" border=0 /></a></td><td><a href="SessionPlansNew.aspx?crs=202612" ><img src="images/SessionPlanExists.gif" border=0 /></a></td><td><a target=_blank href="AttendanceDetails.aspx?cid=202612">31/42 (73.81)</a></td><td>16.00[16.00+0]/30.00</td><td>16-05-2015 10:00</td>
	</tr><tr>
		<td>5
                        
                    </td><td>IB723        </td><td>International Business Negotiation</td><td>Compulsory</td><td><a target=_blank href="../../../AdminAmizone/WebForms/Academics/NewSyllabus/1614201472515140.pdf" ><img src="images/ProgramStr.gif" border=0 /></a></td><td><a href="SessionPlansNew.aspx?crs=202609" ><img src="images/SessionPlanExists.gif" border=0 /></a></td><td><a target=_blank href="AttendanceDetails.aspx?cid=202609">13/45 (28.89)</a></td><td>0.00[0.00+0]/30.00</td><td>05-05-2015 10:00</td>
	</tr><tr>
		<td>6
                        
                    </td><td>BS702</td><td>Leadership and Managing Excellence</td><td>Compulsory</td><td><a target=_blank href="../../../AdminAmizone/WebForms/Academics/NewSyllabus/1012014103004444.pdf" ><img src="images/ProgramStr.gif" border=0 /></a></td><td><a href="SessionPlansNew.aspx?crs=204684" ><img src="images/SessionPlanExists.gif" border=0 /></a></td><td><a target=_blank href="AttendanceDetails.aspx?cid=204684">14/15 (93.33)</a></td><td>31.00[27.00+4]/40.00</td><td>29-04-2015 10:00</td>
	</tr><tr>
		<td>7
                        
                    </td><td>MGMT705 </td><td>Management in Action - Social Economic and Ethical Issues</td><td>Compulsory</td><td><a target=_blank href="../../../AdminAmizone/WebForms/Academics/NewSyllabus/118201472541899.pdf" ><img src="images/ProgramStr.gif" border=0 /></a></td><td><a href="SessionPlansNew.aspx?crs=199911" ><img src="images/SessionPlanExists.gif" border=0 /></a></td><td><a target=_blank href="AttendanceDetails.aspx?cid=199911">45/60 (75.00)</a></td><td>15.00[15.00+0]/30.00</td><td>28-04-2015 14:00</td>
	</tr><tr>
		<td>8
                        
                    </td><td>FIBA732        </td><td>Security Analysis and Portfolio Management</td><td>Compulsory</td><td><a target=_blank href="../../../AdminAmizone/WebForms/Academics/NewSyllabus/127201472827176.pdf" ><img src="images/ProgramStr.gif" border=0 /></a></td><td><a href="SessionPlansNew.aspx?crs=199926" ><img src="images/SessionPlanExists.gif" border=0 /></a></td><td><a target=_blank href="AttendanceDetails.aspx?cid=199926">24/45 (53.33)</a></td><td>17.00[17.00+0]/30.00</td><td>13-05-2015 10:00</td>
	</tr><tr>
		<td>9
                        
                    </td><td>FIBA734        </td><td>Wealth Management</td><td>Compulsory</td><td><a target=_blank href="../../../AdminAmizone/WebForms/Academics/NewSyllabus/1244201541735431.pdf" ><img src="images/ProgramStr.gif" border=0 /></a></td><td><a href="SessionPlansNew.aspx?crs=199933" ><img src="images/SessionPlanExists.gif" border=0 /></a></td><td><a target=_blank href="AttendanceDetails.aspx?cid=199933">15/45 (33.33)</a></td><td>0.00[0.00+0]/30.00</td><td>15-05-2015 10:00</td>
	</tr>
</table>--%>
            
            
                <asp:GridView ID="grdCourse" runat="server" BackColor="White" BorderColor="#3366CC" Width="823px" BorderStyle="None" BorderWidth="1px" CellPadding="4" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="SNO.">
                    <ItemTemplate>
                      <%# Container.DisplayIndex+1 %>
                    </ItemTemplate>
 </asp:TemplateField>

                        <asp:BoundField DataField="Subject Code" HeaderText="Course Code" />
                        <asp:BoundField DataField="Description" HeaderText="Course Name" />
                        <asp:BoundField DataField="Subject Type" HeaderText="Type" />
                        <asp:TemplateField HeaderText="Attendance">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkAttendance" runat="server" ForeColor="#FF9900" Text='<%# Eval("Attendancedetails") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Internal Mark" HeaderText="Internal Assessment" DataFormatString="{0:0.00}" />
                        <asp:BoundField DataField="External Mark" HeaderText="External Assessment" DataFormatString="{0:0.00}" />
                        <asp:BoundField DataField="Total" HeaderText="Result" DataFormatString="{0:0.00}"/>
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
            
            </td>
              </tr>
              
                   
                   
            
            
            
         
             <tr>
            <td>
           
              
            </td>
            </tr>
      
    </table>

    </div>
</asp:Content>


