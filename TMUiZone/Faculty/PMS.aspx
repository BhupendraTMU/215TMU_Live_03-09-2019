<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="PMS.aspx.cs" Inherits="PMS" EnableEventValidation="false" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="pms.css" rel="stylesheet" />
        <script type="text/javascript">
            function validatePositiveNumber(textBox) {
                // Remove any non-numeric characters
                var value = textBox.value;
                if (!/^\d+$/.test(value)) {
                    textBox.value = '';  // Clear the textbox if not a valid integer
                    //alert('Please enter a non-negative integer.');
                } else if (parseInt(value) < 0) {
                    textBox.value = '';  // Clear the textbox if negative
                    alert('Please enter a non-negative integer.');
                }

            }


            function confirmAction() {
                return confirm("Are you sure you want to proceed?");
            }
            function confirmAction_For_designation() {
                return confirm("Are you sure you want to proceed? Once selected, this cannot be modified.");
            }
        </script>
  
<style type="text/css">
    .modalBackground {
        background-color: Black;
        filter: alpha(opacity=60);
        opacity: 0.6;
    }

    .modalPopup {
        background-color: #FFFFFF;
        width: 900px;
        border: 3px solid #0DA9D0;
        border-radius: 12px;
        padding: 0;
    }

        .modalPopup .header {
            background-color: #2FBDF1;
            height: 30px;
            color: White;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
            border-top-left-radius: 6px;
            border-top-right-radius: 6px;
        }

        .modalPopup .body {
            text-align: center;
            font-weight: bold;
        }

        .modalPopup .footer {
            padding: 6px;
        }

        .modalPopup .yes, .modalPopup .no {
            height: 40px;
            color: White;
            line-height: 23px;
            text-align: center;
            font-weight: bold;
            cursor: pointer;
            border-radius: 4px;
        }

        .modalPopup .yes {
            background-color: #2FBDF1;
            border: 1px solid #0DA9D0;
        }

        .modalPopup .no {
            background-color: #9F9F9F;
            border: 1px solid #5C5C5C;
        }

    /* General table style */
    .gridview {
        width: 100%;
        border-collapse: collapse;
        font-family: Arial, sans-serif;
    }

        /* Header styling */
        .gridview th {
            background-color: #ed7600; /* Oceanic Blue */
            color: white;
            font-size: 13px;
            padding: 12px;
            text-align: left;
            border: 1px solid #dddddd;
        }

        /* Row styling */
        .gridview td {
            padding: 10px;
            border: 1px solid #dddddd;
            text-align: left;
            font-size: 12px;
            color: #333;
        }

        /* Alternating row colors */
        .gridview tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        /* Hover effect */
        .gridview tr:hover {
            background-color: #d9edf7;
        }

    /* Responsive design */
    @media screen and (max-width: 768px) {
        .gridview, .gridview thead, .gridview tbody, .gridview th, .gridview td, .gridview tr {
            display: block;
            width: 100%;
        }

            .gridview th, .gridview td {
                box-sizing: border-box;
                text-align: right;
                padding: 12px 8px;
            }

            .gridview td {
                border: none;
                border-bottom: 1px solid #dddddd;
                text-align: right;
            }

            .gridview tr {
                margin-bottom: 12px;
                display: block;
            }

            .gridview thead {
                display: none;
            }

            .gridview td:before {
                content: attr(data-label);
                float: left;
                font-weight: bold;
                color: #007bff;
            }
    }

    /* General textbox styling */
    .textbox {
        width: 100%;
        padding: 5px 7px;
        border: 1px solid #ccc;
        border-radius: 4px;
        font-size: 13px;
        color: #333;
        box-sizing: border-box;
        transition: border-color 0.3s ease-in-out, box-shadow 0.3s ease-in-out;
        height: 30px;
    }

        /* Focus effect */
        .textbox:focus {
            border-color: #007bff; /* Focused border color */
            box-shadow: 0 0 5px rgba(0, 123, 255, 0.5);
            outline: none;
        }

        /* Disabled state */
        .textbox:disabled {
            background-color: #f2f2f2;
            color: #999;
        }

        /* Textbox with an error state */
        .textbox.error {
            border-color: #e74c3c; /* Red border for errors */
            box-shadow: 0 0 5px rgba(231, 76, 60, 0.5);
        }

        /* Placeholder styling */
        .textbox::placeholder {
            color: #999;
            font-style: italic;
        }

    /* Responsive design */
    @media (max-width: 768px) {
        .textbox {
            font-size: 12px;
            padding: 8px 12px;
        }
    }
    /* Base button styling */
    .button {
        padding: 10px 20px;
        font-size: 13px;
        color: white;
        background-color: #007bff; /* Primary blue color */
        border: none;
        border-radius: 4px;
        cursor: pointer;
        transition: background-color 0.3s ease, box-shadow 0.3s ease;
        box-shadow: 0 4px 6px rgba(0, 123, 255, 0.2);
    }

        /* Hover effect */
        .button:hover {
            background-color: #0056b3; /* Darker blue on hover */
            box-shadow: 0 6px 8px rgba(0, 123, 255, 0.3);
        }

        /* Active state */
        .button:active {
            background-color: #004085; /* Even darker blue on click */
            box-shadow: 0 3px 5px rgba(0, 123, 255, 0.2);
            transform: translateY(1px); /* Slight movement on click */
        }

        /* Disabled state */
        .button:disabled {
            background-color: #cccccc; /* Gray color for disabled button */
            cursor: not-allowed;
            box-shadow: none;
        }

        /* Secondary button */
        .button.secondary {
            background-color: #6c757d; /* Secondary gray color */
        }

            .button.secondary:hover {
                background-color: #5a6268; /* Darker gray on hover */
            }

        /* Success button */
        .button.success {
            background-color: #28a745; /* Success green color */
        }

            .button.success:hover {
                background-color: #218838; /* Darker green on hover */
            }

        /* Danger button */
        .button.danger {
            background-color: #dc3545; /* Danger red color */
        }

            .button.danger:hover {
                background-color: #c82333; /* Darker red on hover */
            }

    /* Responsive design */
    @media (max-width: 768px) {
        .button {
            padding: 8px 15px;
            font-size: 12px;
        }
    }
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
		<ContentTemplate>
            <table style="width:100% ; background-color:white">
                <tr> <td><p> <br/>  <h2> &nbsp; Appraisal Form </h2> <br/>
                    <p>
                    </p>
                    <p>
                    </p>
                    </p></td></tr>
	<tr> 
		<td>
            <%--<div id="wrap" >
			--%>
            <asp:Panel ID="pnl_Dashboard" runat="server">

                         <table style="width:100%;">

                            <tr> <td> &nbsp;Employee Code/Name </td> <td> Session </td><td> Month </td> <td>  Status</td> <td> Department </td> <td></td> <td> </td></tr>
                          
                               <tr> <td>   
								   <asp:TextBox ID="txtFilterBy" runat="server" AutoPostBack="true" CssClass="textbox" OnTextChanged="txtFilterBy_TextChanged"></asp:TextBox>

                                    </td>
								   <td> 
										<asp:DropDownList ID="dd_AcademicYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dd_AcademicYear_SelectedIndexChanged" 
											CssClass="textbox"></asp:DropDownList> 


                                                                                                                                                                                            </td> 
								   
								   <td>
									   <asp:DropDownList ID="drpMonth" runat="server" AutoPostBack="true"  CssClass="textbox">

    <asp:ListItem Value="1">January</asp:ListItem>
    <asp:ListItem Value="2">February</asp:ListItem>
    <asp:ListItem Value="3">March</asp:ListItem>
    <asp:ListItem Value="4">April</asp:ListItem>
    <asp:ListItem Value="5">May</asp:ListItem>
    <asp:ListItem Value="6">June</asp:ListItem>
    <asp:ListItem Value="7">July</asp:ListItem>
    <asp:ListItem Value="8">August</asp:ListItem>
    <asp:ListItem Value="9">September</asp:ListItem>
    <asp:ListItem Value="10">October</asp:ListItem>
    <asp:ListItem Value="11">November</asp:ListItem>
    <asp:ListItem Value="12">December</asp:ListItem>

</asp:DropDownList>
								   </td>
								   <td>  <asp:DropDownList ID="dd_Staus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dd_Staus_SelectedIndexChanged" CssClass="textbox">
                            <asp:ListItem Selected="True">All</asp:ListItem>
                            <asp:ListItem Value="Pending_For_Assessment">Pending For Assessment Approval</asp:ListItem>
                            <asp:ListItem Value="Pending_For_HR">Pending For HR Approval</asp:ListItem>
                            <asp:ListItem Value="Pending_For_VC">Pending For VC Approval</asp:ListItem>
                            <asp:ListItem>Approved</asp:ListItem>
                            </asp:DropDownList></td> <td> <asp:DropDownList ID="dd_department" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dd_department_SelectedIndexChanged" CssClass="textbox"></asp:DropDownList>   </td>  <td>  <asp:Button ID="btnCreateNew" CssClass="button danger" Visible="false" runat="server" Text="Create New"  OnClick="btnCreateNew_Click" /></td> <td> </td></tr>
                          

                            
                              <tr> <td colspan="6"> 
                                <asp:Panel ID="pnl_Count_HR_Details" runat="server" Visible="false">
                                    <table style="color:red; font-size:large; font:italic"> <tr> <td>  Total filled PMS Form is  </td> <td>  <asp:LinkButton ID="lnk_Total_PMS_Filled" runat="server" OnClick="lnk_Total_PMS_Filled_Click"></asp:LinkButton></td> <td> and pending is </td>  <td> <asp:LinkButton ID="lnk_Total_PMS_Pending" runat="server" OnClick="lnk_Total_PMS_Pending_Click"></asp:LinkButton> </td> <td>
                                        <asp:LinkButton ID="lnk_PMS_Report" runat="server" OnClick="lnk_PMS_Report_Click" >PMS Report</asp:LinkButton>   </td></tr> </table>

                                </asp:Panel> </td></tr>
                            
                            <tr> <td colspan="6">

                                <asp:GridView ID="grd_Data" Width="100%" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"   BorderWidth="1px" CellPadding="3" AllowPaging="True" OnPageIndexChanging="grd_Data_PageIndexChanging" CssClass="gridview">
                                    <Columns>
                                           <asp:TemplateField HeaderText="Created On">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Created_On_Grid" runat="server" Text='<%#Bind("Created_On","{0:dd MMM yyyy HH:mm}") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_UserID_Grid" runat="server" Text='<%#Bind("UserID") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Name_Grid" runat="server" Text='<%#Bind("Name") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Is RM Approval" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_IsAssessment_Approval_Grid" runat="server" Text='<%#Bind("IsAssessment_Approval") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <asp:TemplateField HeaderText="RM Approval By" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Assessment_Approval_Name_Grid" runat="server" Text='<%#Bind("Assessment_Approval_Name") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                         <asp:TemplateField HeaderText="RM Approval On" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Assesment_Approval_On_Grid" runat="server" Text='<%#Bind("Assesment_Approval_On","{0:dd MMM yyyy HH:mm}") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      

                                        <asp:TemplateField HeaderText="Is HR Approval" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_IsHR_Approval_Grid" runat="server" Text='<%#Bind("IsHR_Approval") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                        <asp:TemplateField HeaderText="HR Approval By" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_HR_Approval_Name_Grid" runat="server" Text='<%#Bind("HR_Approval_Name") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="HR Approval On" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_HR_Approval_On_Grid" runat="server" Text='<%#Bind("HR_Approval_On","{0:dd MMM yyyy HH:mm}") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                        <asp:TemplateField HeaderText="Is VC Approval" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_IsVC_Approval_Grid" runat="server" Text='<%#Bind("IsVC_Approval") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                          <asp:TemplateField HeaderText="VC Approval On" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_VC_Approval_On_Grid" runat="server" Text='<%#Bind("VC_Approval_On","{0:dd MMM yyyy HH:mm}") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      

                                         <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Status_Grid" runat="server" Text='<%#Bind("Status") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="btnViewDetails" CssClass="button success" runat="server" Text="View Detail / Edit"  CommandArgument='<%#Bind("ID") %>' OnCommand="btnViewDetails_Command"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="btnPDFView" CssClass="button danger" runat="server" Text="Report"  CommandArgument='<%#Bind("ID") %>' OnCommand="btnPDFView_Command"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    <PagerStyle BackColor="#00ccff" ForeColor="#000066" HorizontalAlign="Left" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                </asp:GridView>
                                 </td></tr>

                        </table>
 
                  
            </asp:Panel>
            <asp:Panel ID="pnlcreate" runat="server" Visible="false">
                 
                    <table style="width:100%">
                <tr> <td align="right"> <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="button success" /> 

                     </td></tr>

						<tr>							
							<td>
						<h3 align="center"><b>Academic Session: <asp:Label ID="ddl_academic_session" runat="server"></asp:Label>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</b> Month: <asp:Label ID="lblMonth" runat="server"></asp:Label>
							<asp:HiddenField ID="hfmonth" runat="server" />
						</h3>
						<h4><strong>Faculty Name: </strong> <asp:Label ID="lbl_faculty_name" runat="server" Text="Label"></asp:Label></h4>
								<h4><strong>Designation: </strong> <asp:Label ID="lbl_designation" runat="server" ></asp:Label> 
                                    <asp:DropDownList ID="dd_Designation" runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="dd_Designation_SelectedIndexChanged" CssClass="dropdown_uppervalue" >
									<asp:ListItem>PROFESSOR</asp:ListItem>
									<asp:ListItem>ASSISTANT PROFESSOR</asp:ListItem>
									<asp:ListItem>ASSOCIATE PROFESSOR</asp:ListItem>
									<asp:ListItem>LECTURER</asp:ListItem>
									<asp:ListItem>DEMONSTRATOR</asp:ListItem>
									<asp:ListItem>TUTOR UG</asp:ListItem>
                                    <asp:ListItem>TUTOR PG</asp:ListItem>
									<asp:ListItem>SENIOR RESIDENT</asp:ListItem>
									<asp:ListItem>CLINICAL INSTRUCTOR</asp:ListItem>
									<asp:ListItem>TRAINER</asp:ListItem>
									</asp:DropDownList>

								</h4>
								<h4><strong>Emplyoee Code: </strong><asp:Label ID="lbl_emp_code" runat="server" Text="Test1"></asp:Label></h4>
								<h4><strong>EMP Type: </strong><asp:Label ID="lbl_college_department" runat="server" Text="Label"></asp:Label></h4>
								<h4><strong>College/Department: </strong><asp:Label ID="lbl_New_College" runat="server" Text=""></asp:Label> /<asp:Label ID="lbl_New_Department" runat="server" Text=""></asp:Label> </h4>
								
								
								<h3 style="text-align: center"><b><u>Performance Measurement System (PMS) for Faculty</u></h3></b>
							</td>
						</tr>
						<tr>
							<td>
								<p style="margin-left:10px; margin-right:10px">
									<strong>Purpose: </strong>PMS aims to facilitate individuals to perform to one’s potential through direction,
                        guidance, support & review. The performance evaluation system is developed to be flexible,
                        transparent & objective to support the performance.
								</p>

								<p style="margin-left:10px; margin-right:10px">
									The teaching faculty will be evaluated on various criteria with the relevant Academic Performance
                        Indicators (APIs):
								</p>
							</td>
						</tr>
                 
						 <tr>
                <td align="center">
                    <table border="1">
                        <tr>
                            <th class="highlighted-row">Criteria</th>
                            <th class="highlighted-row">Academic Performance Indicators (APIs)</th>
                            <th class="highlighted-row">Maximum API Score</th>
                        </tr>
                        <tr>
                            <th colspan="3" style="text-align: center">API Score through Self-Assessment</th>
                        </tr>
                        <tr>
                            <td class="center-text">
                                <asp:Label ID="lbl_academicperformanceindicators_criteria" runat="server" Text="A" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbl_academicperformance" runat="server" Text="Academic Performance"></asp:Label>
                            </td>
                            <td class="center-text">
                                <asp:Label ID="lbl_academicperformance_maxapiscore" runat="server" Text="250"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="center-text">
                                <asp:Label ID="lbl_ResearchandDevelopment_criteria" runat="server" Text="B" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbl_ResearchandDevelopment" runat="server" Text="Research & Development"></asp:Label>
                            </td>
                            <td class="center-text">
                                <asp:Label ID="lbl_ResearchandDevelopment_maxapi" runat="server" Text="200"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="center-text">
                                <asp:Label ID="lbl_ProfessionalandPersonalCompetency_criteria" runat="server" Text="C" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbl_ProfessionalandPersonalCompetency" runat="server" Text="Professional and Personal Competency"></asp:Label>
                            </td>
                            <td class="center-text">
                                <asp:Label ID="lbl_ProfessionalandPersonalCompetency_maxapi" runat="server" Text="100"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="center-text">
                                <asp:Label ID="lbl_Administration_criteria" runat="server" Text="D" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbl_Administration" runat="server" Text="Administration"></asp:Label>
                            </td>
                            <td class="center-text">
                                <asp:Label ID="lbl_Administration_maxapi" runat="server" Text="100"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th colspan="3" style="text-align: center">API Score through Reporting Authority</th>
                        </tr>

                        <tr>
                            <td class="center-text">
                                <asp:Label ID="lbl_FacultyAssessmentbyReportingAuthority_criteria" runat="server" Text="E" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbl_FacultyAssessmentbyReportingAuthority" runat="server" Text="Faculty Assessment by Reporting Authority"></asp:Label>
                            </td>
                            <td class="center-text">
                                <asp:Label ID="lbl_FacultyAssessmentbyReportingAuthority_maxapi" runat="server" Text="50"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td class="center-text">
                                <asp:Label ID="lbl_StudentFeedback_criteria" runat="server" Text="F" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbl_StudentFeedback" runat="server" Text="Student’s Feedback"></asp:Label>
                            </td>
                            <td class="center-text">
                                <asp:Label ID="lbl_StudentFeedback_maxapi" runat="server" Text="50"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center" colspan="2">
                                <asp:Label ID="lbl_AcademicPerformanceIndicators_total" runat="server" Text="Total " Font-Bold="true"></asp:Label>
                            </td>
                            <td class="center-text">
                                <asp:Label ID="lbl_AcademicPerformanceIndicators_totalno" runat="server" Text="750" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                  </table>					
					
                </td>
            </tr>

			<tr>
				<td align="center">

					<span style="display: block; text-align:left; padding-top: 20px;"><strong>Required Minimum API Score:
					</strong>
					</span>
					<table style="text-align: center" border="1">
						<tr>
							<th colspan="3" class="highlighted-row"></th>
							<th colspan="4" style="text-align: center" class="highlighted-row">Required Minimum API Score
							</th>
						</tr>
						<tr>
							<th style="text-align:center" class="highlighted-row">Criteria</th>
							<th  style="text-align:center" class="highlighted-row">Academic Performance Indicators (APIs)</th>
							<th style="text-align:center" class="highlighted-row">Max. API Score</th>
							<th style="text-align:center" class="highlighted-row">Demonstrator/Lecturer/Tutor(UG/PG)/ Senior Resident/Clinical Instructor/ Trainer 
                               <%-- <br>--%> 
                              <%--  <br></br>
                                &lt;/&gt; </br>--%>

							</th>
							<th style="text-align:center" class="highlighted-row">Assistant Professor</th>
							<th style="text-align:center" class="highlighted-row">Associate Professor</th>
							<th style="text-align:center" class="highlighted-row">Professor</th>
                            </tr>
							<tr>

								<td>
									<asp:Label ID="lbl_RequiredMinimumAPIScore_criteria_a" runat="server" Text="A" Font-Bold="true"></asp:Label></td>
								<td class="left-text">
									<asp:Label ID="lbl_RequiredMinimumAPIScore_APIs_AcademicPerformance" runat="server" Text="Academic Performance"></asp:Label></td>
								<td>
									<asp:Label ID="lbl_RequiredMinimumAPIScore_MaxAPIScore_250" runat="server" Text="250"></asp:Label></td>
								<td>
									<asp:Label ID="lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_100" runat="server" Text="100"></asp:Label></td>
								<td>
									<asp:Label ID="lbl_RequiredMinimumAPIScore_AssistantProfessor_120" runat="server" Text="120"></asp:Label></td>
								<td>
									<asp:Label ID="lbl_RequiredMinimumAPIScore_AssociateProfessor_130" runat="server" Text="130"></asp:Label></td>
								<td>
									<asp:Label ID="lbl_RequiredMinimumAPIScore_Professor_140" runat="server" Text="140"></asp:Label></td>


							</tr>

						<tr>

							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_criteria_b" runat="server" Text="B" Font-Bold="true"></asp:Label></td>
							<td class="left-text">
								<asp:Label ID="lbl_RequiredMinimumAPIScore_APIs_ResearchandDevelopment" runat="server" Text="Research & Development"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_MaxAPIScore_200" runat="server" Text="200"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_80" runat="server" Text="80"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_AssistantProfessor_110" runat="server" Text="110"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_AssociateProfessor_140" runat="server" Text="140"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_Professor_150" runat="server" Text="150"></asp:Label></td>


						</tr>

						<tr>

							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_criteria_c" runat="server" Text="C" Font-Bold="true"></asp:Label></td>
							<td class="left-text">
								<asp:Label ID="lbl_RequiredMinimumAPIScore_APIs_ProfessionalandPersonalCompetency" runat="server" Text="Professional & Personal Competency "></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_MaxAPIScore_100" runat="server" Text="100"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_40" runat="server" Text="40"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_AssistantProfessor_45" runat="server" Text="45"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_AssociateProfessor_50" runat="server" Text="50"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_Professor_60" runat="server" Text="60"></asp:Label></td>


						</tr>

						<tr>

							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_criteria_d" runat="server" Text="D" Font-Bold="true"></asp:Label></td>
							<td class="left-text">
								<asp:Label ID="lbl_RequiredMinimumAPIScore_APIs_Administration" runat="server" Text="Administration"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_APIs_Administration_MaxApiScore_100" runat="server" Text="100"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_25" runat="server" Text="25"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_AssistantProfessor_40" runat="server" Text="40"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_AssociateProfessor_60" runat="server" Text="60"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_Professor_70" runat="server" Text="70"></asp:Label></td>


						</tr>

						<tr>

							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_criteria_e" runat="server" Text="E" Font-Bold="true"></asp:Label></td>
							<td class="left-text">
								<asp:Label ID="lbl_RequiredMinimumAPIScore_APIs_FacultyAssessmentbyReportingAuthority" runat="server" Text="Faculty Assessment by Reporting Authority"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_APIs_FacultyAssessmentbyReportingAuthority_50" runat="server" Text="50"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_FacultyAssessmentbyReportingAuthority_Demonstrator_Lecturer_25" runat="server" Text="25"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_AssistantProfessor_30" runat="server" Text="30"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_AssociateProfessor_30" runat="server" Text="30"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_Professor_40" runat="server" Text="40"></asp:Label></td>


						</tr>

						<tr>

							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_criteria_f" runat="server" Text="F" Font-Bold="true"></asp:Label></td>
							<td class="left-text">
								<asp:Label ID="lbl_RequiredMinimumAPIScore_APIs_StudentFeedback" runat="server" Text="Student’s Feedback"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_APIs_StudentFeedback_50" runat="server" Text="50"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_FacultyAssessmentbyReportingAuthority_Demonstrator_Lecturer_30" runat="server" Text="30"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_AssistantProfessor_35" runat="server" Text="35"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_AssociateProfessor_40" runat="server" Text="40"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_StudentFeedback_Professor_40" runat="server" Text="40"></asp:Label></td>


						</tr>

						<tr>
							<td colspan="2">
								<asp:Label ID="lbl_RequiredMinimumAPIScore_Total" runat="server" Text="Total" Font-Bold="true"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_MaxApiScore_Total_750" runat="server" Text="750" Font-Bold="true"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_Total_300" runat="server" Text="300" Font-Bold="true"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_AssistantProfessor_Total_380" runat="server" Text="380" Font-Bold="true"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_AssociateProfessor_Total_450" runat="server" Text="450" Font-Bold="true"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_RequiredMinimumAPIScore_Professor_Total_500" runat="server" Text="500" Font-Bold="true"></asp:Label></td>

						</tr>

					</table>
				</td>
			</tr>
			<tr>
				
				<td align="center">
					<span style="display: block; text-align:left; padding-top: 20px;"><strong>Faculty categorization based on API Score:</strong></span>

					<table border="1" style="text-align: center; width:60%;">
						<tr>
							<td class="highlighted-row" style="width:5%;">
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_S_No" runat="server" Text="S.No" Font-Bold="true"></asp:Label></td>
							<td class="highlighted-row" style="width:25%;">
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_FacultyCadre" runat="server" Text="Faculty Cadre" Font-Bold="true"></asp:Label></td>
							<td class="highlighted-row" style="width:15%;">
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_APIScoreRange" runat="server" Text="API Score Range" Font-Bold="true"></asp:Label></td>
							<td class="highlighted-row" style="width:15%;">
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_Category" runat="server" Text="Category" Font-Bold="true"></asp:Label></td>

						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_S_No_1" runat="server" Text="1"></asp:Label></td>
							<td rowspan="3"><b>Demonstrator/ Lecturer/Tutor (UG/PG)/ <br/> Senior Resident/Clinical Instructor/ Trainer</b></td>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_APIScoreRange_300" runat="server" Text="<300"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_Category_Average" runat="server" Text="Average"></asp:Label></td>

						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_S_No_2" runat="server" Text="2"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_APIScoreRange_300_380" runat="server" Text="300-380"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_Category_Good" runat="server" Text="Good"></asp:Label></td>

						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_S_No_3" runat="server" Text="3"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_APIScoreRange_380" runat="server" Text=">380"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_Category_Excellent" runat="server" Text="Excellent"></asp:Label></td>

						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_S_No_4" runat="server" Text="4"></asp:Label></td>
							<td rowspan="3">
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_FacultyCadre_AssistantProfessor" runat="server" Text="Assistant Professor" Font-Bold="true"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_APIScoreRange_AssistantProfessor_380" runat="server" Text="<380"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_Category_AssistantProfessor_Average" runat="server" Text="Average"></asp:Label></td>

						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_S_No_5" runat="server" Text="5"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_APIScoreRange_AssistantProfessor_380_450" runat="server" Text="380-450"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_AssistantProfessor_Category_Good" runat="server" Text="Good"></asp:Label></td>

						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_S_No_6" runat="server" Text="6"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_APIScoreRange_AssistantProfessor_450" runat="server" Text=">450"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_AssistantProfessor_Category_Excellent" runat="server" Text="Excellent"></asp:Label></td>

						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_S_No_7" runat="server" Text="7"></asp:Label></td>
							<td rowspan="3">
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_FacultyCadre_AssociateProfessor" runat="server" Text="Associate Professor" Font-Bold="true"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_APIScoreRange_AssociateProfessor_450" runat="server" Text="<450"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_AssociateProfessor_Category_Average" runat="server" Text="Average"></asp:Label></td>

						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_S_No_8" runat="server" Text="8"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_APIScoreRange_AssociateProfessor_400_500" runat="server" Text="450-500"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_AssociateProfessor_Category_Good" runat="server" Text="Good"></asp:Label></td>

						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_S_No_9" runat="server" Text="9"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_APIScoreRange_AssociateProfessor_500" runat="server" Text=">500"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_AssociateProfessor_Category_Excellent" runat="server" Text="Excellent"></asp:Label></td>

						</tr>


						<tr>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_S_No_10" runat="server" Text="10"></asp:Label></td>
							<td rowspan="3">
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_FacultyCadre_Professor" runat="server" Text="Professor" Font-Bold="true"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_APIScoreRange_Professor_500" runat="server" Text="<500"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_Professor_Category_Average" runat="server" Text="Average"></asp:Label></td>

						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_S_No_11" runat="server" Text="11"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_APIScoreRange_Professor_500_550" runat="server" Text="500-550"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_Professor_Category_Good" runat="server" Text="Good"></asp:Label></td>

						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_S_No_12" runat="server" Text="12"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_APIScoreRange_Professor_550" runat="server" Text=">550"></asp:Label></td>
							<td>
								<asp:Label ID="lbl_FacultycategorizationbasedonAPIScore_Professor_Category_Excellent" runat="server" Text="Excellent"></asp:Label></td>

						</tr>
					</table>
				</td>
			</tr>

				<tr style="display: block; padding-top: 20px;">
					<td style="text-align: center">
						<h3 style="text-decoration: underline; display: block; margin: 0 auto; text-align: center;"><b>Criteria-A: Academic Performance</h3></b>
						<span style="display: block; text-align: right"><strong>(Max. API Score: 250)</strong></span>
						<h4 style="text-align:justify;">Based on the teacher’s self-assessment, API scores are proposed for <strong>(a)</strong> teaching-related activities; <strong>(b)</strong> developing new Courses/Programmes; <strong>(c) </strong>Contribution as a member of BOS, BOF, Senate/Academic Council, Curriculum development team <strong>(d)</strong> Supervision of Project Work <strong>(e)</strong> Innovative Pedagogy etc.</h4>
                        <br />
						<%--Start of academic performance table--%>
						<table border="1">
						<tr>
							<th colspan="2"; class="whitebg-row; "></th>
							<th style="text-align:center" class="red-highlighted-row1">Max. API Score(P)</th>
							<th style="text-align:center" class="red-highlighted-row1">Max Scores Per Activity(A)</th>
							<th style="text-align:center" class="red-highlighted-row1">Total No of Activities Performed in Acutal(B)</th>
							<th style="text-align:center" class="red-highlighted-row1">API Score<br />through Self-Assessment <br /> (Q=A*B)</th>
							<th style="text-align:center" class="red-highlighted-row1">Finally Obtained API Score (Minimum of P & Q)</th>
							<th style="text-align:center" class="red-highlighted-row1">Assessment by Reporting Authority through API Score</th>
							<th style="text-align:center" class="red-highlighted-row1">Upload Relevant Documents <br />(pdf, jpg with <200kb size)</th>
						</tr>

							<tr>
							<td class="highlighted-row"><asp:Label ID="lbl_CriteriaA_AcademicPerformance_A" runat="server" Text="A)" Font-Bold="true"></asp:Label></td>
							<td colspan="6" class="highlighted-row left-text"><strong>Classroom Teaching</strong></td>						
							
								<td class="highlighted-row"></td>
							<td class="highlighted-row"></td>
						</tr>

							<tr>
								<td><asp:Label ID="lbl_CriteriaA_AcademicPerformance_A_a1" runat="server" Text="a.1" Font-Bold="true"></asp:Label></td>
								<td class="left-text">Score for credit of Courses taught <br /> by faculty during current Session <br /> (Odd & Even Sem) including practical (lab) & tutorial<br /> <p style="font-style:italic">(3 scores for each credit)</p></td>
								<td><asp:Label ID="lbl_a1_Maxapiscore" runat="server" Text="100"></asp:Label></td>
								<td><asp:Label ID="lbl_a1_maxscoresperacitivity" runat="server" Text="3" Font-Bold="true"></asp:Label></td>
								<td><asp:TextBox ID="txt_a1_actualtotalactivites" runat="server" Width="50px"   AutoPostBack="True"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_a1_actualtotalactivites_TextChanged" Class="center-text"></asp:TextBox></td>
								<td><asp:Label ID="lbl_a1_apiscore_through_self_assessment" runat="server"></asp:Label></td>
								<td><asp:Label ID="lbl_a1_final_obtained_value" runat="server" Font-Bold="true"></asp:Label></td>
								<td><asp:TextBox ID="txt_a1_reporting_authority_assessment" AutoPostBack="True" OnTextChanged="txt_a1_reporting_authority_assessment_TextChanged" runat="server"     oninput="validatePositiveNumber(this)"  Width="50px"  ></asp:TextBox></td>
								<td align="center">
                                    <asp:Button ID="fu_a1" runat="server" Text="Upload File" OnClick="btn_Fu_A1_Click" Enabled="false"  />
                                     

								</td>					
							</tr>

								<tr>
							<td style="height:30px" class="whitebg-row"></td>
							<td class="whitebg-row"></td>
							<td class="whitebg-row"></td>
							<td class="whitebg-row"></td>
							<td class="whitebg-row"></td>
							<td class="whitebg-row"></td>
							<td class="whitebg-row"></td>
							<td class="whitebg-row"></td>
							<td class="whitebg-row"></td>
							</tr>
							<tr>
							<td class="highlighted-row"><asp:Label ID="lbl_CriteriaA_AcademicPerformance_B" runat="server" Text="B)" Font-Bold="true"></asp:Label></td>
							<td colspan="6" class="highlighted-row left-text"><strong>New Courses/Programmes Developed</strong></td>						
							
								<td class="highlighted-row"></td>
							<td class="highlighted-row"></td>
						</tr>

							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaA_AcademicPerformance_A_b1" runat="server" Text="b.1" Font-Bold="true"></asp:Label></td>
								<td  class="left-text">
									Revised/ developed a new course(s)<br /> for a Programme <br /> <p style="font-style:italic"> (Max. 3 courses will be considered)</p>
									</td>
								<td>
									<asp:Label ID="lbl_b1_MaxAPIScore" runat="server" Text="9" Font-Bold="true"></asp:Label></td>
								<td>
									<asp:Label ID="lbl_b1_MaxScoresPerActivity" runat="server" Text="3" Font-Bold="true"></asp:Label></td>
								<td>
									<asp:TextBox ID="txt_b1_ActualTotalActivities" AutoPostBack="true"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_b1_ActualTotalActivities_TextChanged" runat="server" Width="50px"   Class="center-text"></asp:TextBox></td>
								<td>
									<asp:Label ID="lbl_b1_APIScoreThroughSelfAssessment" runat="server"></asp:Label></td>
								<td>
									<asp:Label ID="lbl_b1_FinalObtainedValue" runat="server" Font-Bold="true"></asp:Label></td>
								<td>
									<asp:TextBox ID="txt_b1_ReportingAuthorityAssessment" AutoPostBack="true"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_b1_ReportingAuthorityAssessment_TextChanged" runat="server" Width="50px"  ></asp:TextBox></td>
								<td>
									  <asp:Button ID="fu_b1" runat="server" Text="Upload File" OnClick="fu_b1_Click" Enabled="false"/>
								
								</td>
							</tr>

							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaA_AcademicPerformance_A_b2" runat="server" Text="b.2" Font-Bold="true"></asp:Label></td>
								<td  class="left-text">
									<asp:Label ID="lbl_CriteriaA_AcademicPerformance_A_b2_" runat="server" Text="Contribution to developed/ modified a new programme"></asp:Label></td>
								<td>
									<asp:Label ID="lbl_b2_MaxAPIScore" runat="server" Text="5" Font-Bold="true"></asp:Label></td>
								<td>
									<asp:Label ID="lbl_b2_MaxScoresPerActivity" runat="server" Text="5" Font-Bold="true"></asp:Label></td>
								<td>
									<asp:TextBox ID="txt_b2_ActualTotalActivities" AutoPostBack="true"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_b2_ActualTotalActivities_TextChanged" runat="server" Width="50px"   Class="center-text"></asp:TextBox></td>
								<td>
									<asp:Label ID="lbl_b2_APIScoreThroughSelfAssessment" runat="server"></asp:Label></td>
								
								<td>
									<asp:Label ID="lbl_b2_FinalObtainedValue" runat="server" Font-Bold="true"></asp:Label></td>
								<td>
									<asp:TextBox ID="txt_b2_ReportingAuthorityAssessment" AutoPostBack="true"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_b2_ReportingAuthorityAssessment_TextChanged" runat="server" Width="50px"  ></asp:TextBox></td>	
								<td>
									<asp:Button ID="fu_b2" Text="Upload File" OnClick="fu_b2_Click" runat="server" Enabled="false"/></td>
							</tr>

							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaA_AcademicPerformance_A_b3" runat="server" Text="b.3" Font-Bold="true"></asp:Label></td>
								<td  class="left-text">
									<asp:Label ID="lbl_CriteriaA_AcademicPerformance_A_b3_" runat="server" Text="Revised/developed an experiment for a Programme<br />"></asp:Label><p style="font-style:italic">(Max. 3 experiments will be considered)</p></td>
								<td>
									<asp:Label ID="lbl_b3_MaxAPIScore" runat="server" Text="6" Font-Bold="true"></asp:Label></td>
								<td>
									<asp:Label ID="lbl_b3_MaxScoresPerActivity" runat="server" Text="2" Font-Bold="true"></asp:Label></td>
								<td>
									<asp:TextBox ID="txt_b3_ActualTotalActivities" AutoPostBack="true"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_b3_ActualTotalActivities_TextChanged" runat="server" Width="50px"   Class="center-text"></asp:TextBox></td>
								<td>
									<asp:Label ID="lbl_b3_APIScoreThroughSelfAssessment" runat="server"></asp:Label></td>
								
								<td>
									<asp:Label ID="lbl_b3_FinalObtainedValue" runat="server" Font-Bold="true"></asp:Label></td>
								<td>
									<asp:TextBox ID="txt_b3_ReportingAuthorityAssessment" AutoPostBack="true"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_b3_ReportingAuthorityAssessment_TextChanged" runat="server" Width="50px"  ></asp:TextBox></td>
								<td>
									<asp:Button ID="fu_b3" Text="Upload File" runat="server" Enabled="false" OnClick="fu_b3_Click"/></td>
							</tr>

							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaA_AcademicPerformance_A_b4" runat="server" Text="b.4" Font-Bold="true"></asp:Label></td>
								<td class="left-text">
									<asp:Label ID="lbl_CriteriaA_AcademicPerformance_A_b4_" runat="server" Text="Developed lab manual <br /> "></asp:Label> <p style="font-style:italic">(Max. 3 experiments will be considered)</p>  </td>
								<td>
									<asp:Label ID="lbl_b4_MaxAPIScore" runat="server" Text="9" Font-Bold="true"></asp:Label></td>
								<td>
									<asp:Label ID="lbl_b4_MaxScoresPerActivity" runat="server" Text="3" Font-Bold="true"></asp:Label></td>
								<td>
									<asp:TextBox ID="txt_b4_ActualTotalActivities" AutoPostBack="true"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_b4_ActualTotalActivities_TextChanged" runat="server" Width="50px"   Class="center-text"></asp:TextBox></td>
								<td>
									<asp:Label ID="lbl_b4_APIScoreThroughSelfAssessment" runat="server"></asp:Label></td>
								<td>
									<asp:Label ID="lbl_b4_FinalObtainedValue" runat="server" ></asp:Label></td>
								<td>
									<asp:Textbox ID="txt_b4_ReportingAuthorityAssessment" AutoPostBack="true"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_b4_ReportingAuthorityAssessment_TextChanged" runat="server" Width="50px"  ></asp:Textbox></td>
								<td>
									<asp:Button ID="fu_b4" Text="Upload File" OnClick="fu_b4_Click" runat="server" Enabled="false" /></td>
							</tr>

							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaA_AcademicPerformance_A_b5" runat="server" Text="b.5" Font-Bold="true"></asp:Label></td>
								<td class="left-text">
									<asp:Label ID="lbl_CriteriaA_AcademicPerformance_A_b5_" runat="server" Text="Developed the content for Short-term Courses/FDPs"></asp:Label></td>
								<td>
									<asp:Label ID="lbl_b5_MaxAPIScore" runat="server" Text="6" Font-Bold="true"></asp:Label></td>
								<td>
									<asp:Label ID="lbl_b5_MaxScoresPerActivity" runat="server" Text="6" Font-Bold="true"></asp:Label></td>
								<td>
									<asp:TextBox ID="txt_b5_ActualTotalActivities" AutoPostBack="true"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_b5_ActualTotalActivities_TextChanged" runat="server" Width="50px"   Class="center-text"></asp:TextBox></td>
								<td>
									<asp:Label ID="lbl_b5_APIScoreThroughSelfAssessment" runat="server"></asp:Label></td>
								<td>
									<asp:Label ID="lbl_b5_FinalObtainedValue" runat="server"></asp:Label></td>
								<td>
									<asp:Textbox ID="txt_b5_ReportingAuthorityAssessment" AutoPostBack="true"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_b5_ReportingAuthorityAssessment_TextChanged" runat="server"  Width="50px"  ></asp:Textbox></td>
								<td>
									<asp:Button ID="fu_b5" OnClick="fu_b5_Click" Text="Upload File" runat="server"  Enabled="false"/></td>
				</tr>

						<tr>
								<td>
									<asp:Label ID="lbl_CriteriaA_AcademicPerformance_A_b6" runat="server" Text="b.6" Font-Bold="true"></asp:Label></td>
								<td class="left-text">
									<asp:Label ID="lbl_CriteriaA_AcademicPerformance_A_b6_" runat="server" Text="E-contents developed for a Programme <br /> "></asp:Label> <p style="font-style:italic">(Max. 3 E-contents will be considered)</p> </td>
								<td>
									<asp:Label ID="lbl_b6_MaxAPIScore" runat="server" Text="15" Font-Bold="true"></asp:Label></td>
								<td>
									<asp:Label ID="lbl_b6_MaxScoresPerActivity" runat="server" Text="5" Font-Bold="true"></asp:Label></td>
								<td>
									<asp:TextBox ID="txt_b6_ActualTotalActivities" AutoPostBack="true"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_b6_ActualTotalActivities_TextChanged" runat="server" Width="50px"   Class="center-text"></asp:TextBox></td>
								<td>
									<asp:Label ID="lbl_b6_APIScoreThroughSelfAssessment" runat="server"></asp:Label></td>
							<td>
									<asp:Label ID="lbl_b6_FinalObtainedValue" runat="server"></asp:Label></td>	
							<td>
									<asp:TextBox ID="txt_b6_ReportingAuthorityAssessment" AutoPostBack="true"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_b6_ReportingAuthorityAssessment_TextChanged" runat="server" Width="50px"  ></asp:TextBox></td>
								<td>

									<asp:Button ID="fu_b6" Text="Upload File" OnClick="fu_b6_Click" runat="server"  Enabled="false"/></td>
							</tr>

								<tr>
							<td class="highlighted-row"><asp:Label ID="lbl_CriteriaA_AcademicPerformance_C" runat="server" Text="C)" Font-Bold="true"></asp:Label></td>
							<td colspan="6" class="highlighted-row left-text"><strong>Contribution as member of BOS, BOF, Senate/Academic Council, Curriculum development team etc.</strong></td>
							
									<td class="highlighted-row"></td>
							<td class="highlighted-row"></td>
						</tr>
							<tr>
								<td>
									<asp:Label ID="lbl_c1" runat="server" Text="c.1" Font-Bold="true"></asp:Label>
								</td>
								<td class="left-text">
									<asp:Label ID="lbl_c1_Description" runat="server" Text="A member within TMU of any of the above bodies "></asp:Label> <p style="font-style:italic"> (Maximum 6 Marks will be allotted if faculty is a member of single or all committees)</p>
								</td>
								<td>
									<asp:Label ID="lbl_c1_MaxAPIScore" runat="server" Text="6" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_c1_ScoresPerActivity" runat="server" Text="6" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_c1_RequiredActivities" AutoPostBack="true"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_c1_RequiredActivities_TextChanged" runat="server" Width="50px"   Class="center-text"></asp:TextBox>
								</td>
								<td>
									<asp:Label ID="lbl_c1_TotalAPIScore" runat="server"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_c1_FinalScore" runat="server" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_c1_AssessmentByAuthority" AutoPostBack="true"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_c1_AssessmentByAuthority_TextChanged" runat="server" Width="50px"  ></asp:TextBox>
								</td>
								
								<td>
									<asp:Button ID="fu_c1" Text="Upload File" OnClick="fu_c1_Click" runat="server" Enabled="false"/>
								</td>
							</tr>


							<tr>
								<td>
									<asp:Label ID="lbl_c2" runat="server" Text="c.2" Font-Bold="true"></asp:Label>
								</td>
								<td class="left-text">
									<asp:Label ID="lbl_c2_Description" runat="server" Text="A member of an external Institution/University of any of the above bodies<br />"></asp:Label> <p style="font-style:italic">(Maximum 8 Marks will be allotted if faculty is a member of single or all committees)</p>
								</td>
								<td>
									<asp:Label ID="lbl_c2_MaxAPIScore" runat="server" Text="8" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_c2_ScoresPerActivity" runat="server" Text="8" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_c2_RequiredActivities" AutoPostBack="true" OnTextChanged="txt_c2_RequiredActivities_TextChanged" runat="server" Width="50px"       oninput="validatePositiveNumber(this)"  Class="center-text"></asp:TextBox>
								</td>
								<td>
									<asp:Label ID="lbl_c2_TotalAPIScore" runat="server"></asp:Label>
								</td>
								
								<td>
									<asp:Label ID="lbl_c2_FinalScore" runat="server" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_c2_AssessmentByAuthority" AutoPostBack="true" OnTextChanged="txt_c2_AssessmentByAuthority_TextChanged" runat="server" Width="50px"       oninput="validatePositiveNumber(this)" ></asp:TextBox>
								</td>
								<td>
									<asp:Button ID="fu_c2" Text="Upload File" OnClick="fu_c2_Click" runat="server" Enabled="false" />
								</td>
							</tr>
				
							<tr>
							<td class="highlighted-row"><asp:Label ID="lbl_CriteriaA_AcademicPerformance_D" runat="server" Text="D)" Font-Bold="true"></asp:Label></td>
							<td colspan="6" class="highlighted-row left-text"><strong>Project Work Supervision</strong></td>						
							
								<td class="highlighted-row"></td>
							<td class="highlighted-row"></td>
						</tr>
							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaA_AcademicPerformance_A_d1" runat="server" Text="d.1" Font-Bold="true"></asp:Label>
								</td>
								<td class="left-text">
									<asp:Label ID="lbl_CriteriaA_AcademicPerformance_A_d1_" runat="server" Text="UG project supervised <br /> "></asp:Label> <p style="font-style:italic"> (Max. 2 UG projects will be considered)</p>
								</td>
								<td>
									<asp:Label ID="lbl_d1_MaxAPIScore" runat="server" Text="8" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_d1_MaxScoresPerActivity" runat="server" Text="4" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_d1_ActualTotalActivities" AutoPostBack="true" OnTextChanged="txt_d1_ActualTotalActivities_TextChanged" runat="server" Width="50px"       oninput="validatePositiveNumber(this)"  Class="center-text"></asp:TextBox>
								</td>
								<td>
									<asp:Label ID="lbl_d1_APIScoreThroughSelfAssessment" runat="server"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_d1_FinalObtainedValue" runat="server" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_d1_ReportingAuthorityAssessment" OnTextChanged="txt_d1_ReportingAuthorityAssessment_TextChanged1" AutoPostBack="true" runat="server" Width="50px" oninput="validatePositiveNumber(this)" ></asp:TextBox>
								</td>
								<td>
									<asp:Button ID="fu_d1" Text="Upload File" OnClick="fu_d1_Click" runat="server" Enabled="false" />
								</td>
							</tr>

							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaA_AcademicPerformance_A_d2" runat="server" Text="d.2" Font-Bold="true"></asp:Label>
								</td>
								<td class="left-text">
									<asp:Label ID="lbl_CriteriaA_AcademicPerformance_A_d2_" runat="server" Text="PG project supervised <br />"></asp:Label><p style="font-style:italic"> (Max. 2 PG projects will be considered)</p>
								</td>
								<td>
									<asp:Label ID="lbl_d2_MaxAPIScore" runat="server" Text="12" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_d2_MaxScoresPerActivity" runat="server" Text="6" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_d2_ActualTotalActivities" AutoPostBack="true" OnTextChanged="txt_d2_ActualTotalActivities_TextChanged" runat="server" Width="50px"       oninput="validatePositiveNumber(this)"  Class="center-text"></asp:TextBox>
								</td>
								<td>
									<asp:Label ID="lbl_d2_APIScoreThroughSelfAssessment" runat="server"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_d2_FinalObtainedValue" runat="server" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_d2_ReportingAuthorityAssessment" AutoPostBack="true" OnTextChanged="txt_d2_ReportingAuthorityAssessment_TextChanged" runat="server" Width="50px"       oninput="validatePositiveNumber(this)" ></asp:TextBox>
								</td>
								<td>
									<asp:Button ID="fu_d2" Text="Upload File" OnClick="fu_d2_Click" runat="server"  Enabled="false"/>
								</td>
							</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaA_AcademicPerformance_A_d3" runat="server" Text="d.3" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaA_AcademicPerformance_A_d3_" runat="server" Text="UG/PG field project supervised"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_d3_MaxAPIScore" runat="server" Text="8" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_d3_MaxScoresPerActivity" runat="server" Text="8" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_d3_ActualTotalActivities" AutoPostBack="true" OnTextChanged="txt_d3_ActualTotalActivities_TextChanged" runat="server" Width="50px"       oninput="validatePositiveNumber(this)"  Class="center-text"></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_d3_APIScoreThroughSelfAssessment" runat="server"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_d3_FinalObtainedValue" runat="server" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_d3_ReportingAuthorityAssessment" OnTextChanged="txt_d3_ReportingAuthorityAssessment_TextChanged" AutoPostBack="true" runat="server" Width="50px"       oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_d3" Text="Upload File" OnClick="fu_d3_Click" runat="server"  Enabled="false"/>
							</td>
						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaA_AcademicPerformance_A_d4" runat="server" Text="d.4" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaA_AcademicPerformance_A_d4_" runat="server" Text="UG/PG Industry project supervised (real-time problem)"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_d4_MaxAPIScore" runat="server" Text="10" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_d4_MaxScoresPerActivity" runat="server" Text="10" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_d4_ActualTotalActivities" AutoPostBack="true" OnTextChanged="txt_d4_ActualTotalActivities_TextChanged" runat="server" Width="50px"       oninput="validatePositiveNumber(this)"  Class="center-text" ></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_d4_APIScoreThroughSelfAssessment" runat="server"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_d4_FinalObtainedValue" runat="server" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_d4_ReportingAuthorityAssessment" AutoPostBack="true" OnTextChanged="txt_d4_ReportingAuthorityAssessment_TextChanged" runat="server" Width="50px"       oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_d4" Text="Upload File" OnClick="fu_d4_Click" runat="server" Enabled="false" />
							</td>
						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaA_AcademicPerformance_A_d5" runat="server" Text="d.5" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaA_AcademicPerformance_A_d5_" runat="server" Text="UG/PG project of the student that has been shortlisted for the competition"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_d5_MaxAPIScore" runat="server" Text="12" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_d5_MaxScoresPerActivity" runat="server" Text="12" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_d5_ActualTotalActivities" AutoPostBack="true" OnTextChanged="txt_d5_ActualTotalActivities_TextChanged" runat="server" Width="50px"       oninput="validatePositiveNumber(this)"  Class="center-text"></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_d5_APIScoreThroughSelfAssessment" runat="server"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_d5_FinalObtainedValue" runat="server" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_d5_ReportingAuthorityAssessment" AutoPostBack="true" OnTextChanged="txt_d5_ReportingAuthorityAssessment_TextChanged" runat="server" Width="50px"       oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_d5" Text="Upload File" OnClick="fu_d5_Click" runat="server" Enabled="false" />
							</td>
						</tr>
						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaA_AcademicPerformance_A_d6" runat="server" Text="d.6" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaA_AcademicPerformance_A_d6_" runat="server" Text="UG/PG project won any first three positions in a competition (irrespective of no. of students supervised)"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_d6_MaxAPIScore" runat="server" Text="16" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_d6_MaxSctxt_d6_ActualTotalActivitiesoresPerActivity" runat="server" Text="16" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_d6_ActualTotalActivities" AutoPostBack="true" OnTextChanged="txt_d6_ActualTotalActivities_TextChanged" runat="server" Width="50px"       oninput="validatePositiveNumber(this)"  Class="center-text"></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_d6_APIScoreThroughSelfAssessment" runat="server"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_d6_FinalObtainedValue" runat="server" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_d6_ReportingAuthorityAssessment" AutoPostBack="true" OnTextChanged="txt_d6_ReportingAuthorityAssessment_TextChanged" runat="server" Width="50px"       oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_d6" Text="Upload File" OnClick="fu_d6_Click" runat="server"  Enabled="false"/>
							</td>
						</tr>
						<tr>
							<td class="highlighted-row"><asp:Label ID="lbl_CriteriaA_AcademicPerformance_E" runat="server" Text="E)" Font-Bold="true"></asp:Label></td>
							<td colspan="6" class="highlighted-row left-text"><strong>Exam Related Works</strong></td>						
							
							<td class="highlighted-row"></td>
							<td class="highlighted-row"></td>
						</tr>
						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaA_AcademicPerformance_A_e1" runat="server" Text="e.1" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaA_AcademicPerformance_A_e1_" runat="server" Text="Scores for submission of examination /assessment work within stipulated time & quality in terms of objectivity & fairness (Question papers setting, timely uploaded of CT Marks/Assignment/Attendance/ Evaluation/Examiner(inhouse)"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_e1_MaxAPIScore" runat="server" Text="20" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_e1_MaxScoresPerActivity" runat="server" Text="5" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_e1_ScoresPerActivity" AutoPostBack="true" OnTextChanged="txt_e1_ScoresPerActivity_TextChanged" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  Class="center-text"></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_e1_APIScoreThroughSelfAssessment" runat="server"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_e1_FinalObtainedValue" runat="server" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_e1_ReportingAuthorityAssessment" runat="server" Width="50px"   AutoPostBack="true" OnTextChanged="txt_e1_ReportingAuthorityAssessment_TextChanged"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_e1" Text="Upload File" OnClick="fu_e1_Click" runat="server"  Enabled="false"/>
							</td>
						</tr>

						<tr>
							<td colspan="2"></td>
							<td class="grey-highlighted-row">
								<asp:Label ID="lbl_CriteriaA_AcademicPerformance_MaxAPIScore_Total" runat="server" Text="250" Font-Bold="true"></asp:Label></td>
							<td colspan="2" class="whitebg-row"></td>
							<td class="grey-highlighted-row">
								<asp:Label ID="lbl_e1_APIScoreThroughSelfAssessment_Total" runat="server" Text="" Font-Bold="true"></asp:Label></td>
							<td class="grey-highlighted-row">
								<asp:Label ID="lbl_e1_FinalObtainedValue_Total" runat="server" Text="" Font-Bold="true"></asp:Label></td>
							<td class="grey-highlighted-row">
								<asp:Label ID="lbl_CriteriaA_AcademicPerformance_Total_Total" Font-Bold="true" runat="server" Text=""></asp:Label></td>

						</tr>
					</table>
						</td></tr>
						<%--End of academic performance--%>
				
			<tr class="vertical-padding" style="text-align: center;">
				<td>
					<h3 style="text-decoration-line: underline"><b>Criteria-B: Research & Development</h3></b>
					<span style="text-align: right; display: block"><strong>(Max API Score: 200)</strong></span>
					<h4 style="padding-top: 20px; padding-bottom:20px; text-align:left; margin: 0 auto;">Based on the teacher’s self-assessment, API scores are proposed for <strong>(a)</strong> Research Papers &amp; Book publication; <strong>(b)</strong> Research Supervision; <strong>(c)</strong> Research Projects; <strong>(d)</strong> Participation in Research Activities; <strong>(e)</strong> Consultancy etc.</h4>

					<table border="1">
						<tr>
							<th colspan="2"; class="whitebg-row; "></th>
							<th style="text-align:center" class="red-highlighted-row1">Max. API Score(P)</th>
							<th style="text-align:center" class="red-highlighted-row1">Max Scores Per Activity(A)</th>
							<th style="text-align:center" class="red-highlighted-row1">Any Activity of this month done or not</th>
							<th style="text-align:center" class="red-highlighted-row1">Total No of Activities Performed in Acutal(B)</th>
							<th style="text-align:center" class="red-highlighted-row1">API Score through Self-Assessment(Q=A*B)</th>
							<th style="text-align:center" class="red-highlighted-row1">Finally Obtained API Score (Minimum of P & Q)</th>
							<th style="text-align:center" class="red-highlighted-row1">Assessment by Reporting Authority through API Score</th>
							<th style="text-align:center" class="red-highlighted-row1">Upload Relevant Documents(pdf, jpg with <200kb size)</th>
						</tr>
							<tr>
							<td class="highlighted-row"><asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_F" runat="server" Text="F)" Font-Bold="true"></asp:Label></td>
							<td colspan="6" class="highlighted-row left-text"><strong>Research Papers & Book Publication</strong></td>						
							
								<td class="highlighted-row"></td>
							<td class="highlighted-row"></td>
						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_f1" runat="server" Text="f.1**" ForeColor="Red" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_f1_Description" runat="server" Text="Research Paper published in a Journal indexed in <br /> <b>a)</b>Scopus/WoS/PubMed/SCI/ABDC<br /><b>b)</b>UGC-Care & Indian Citation Index"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f1" runat="server" Text="32" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f1" runat="server" Text="8" Font-Bold="true"></asp:Label><br />
							<br />	<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f1_2" runat="server" Text="4" Font-Bold="true"></asp:Label><br />
									
							</td>
							<td>
								<asp:DropDownList ID="drpactivityYESNO" runat="server" AutoPostBack="true" OnTextChanged="drpactivityYESNO_TextChanged">
									<asp:ListItem Text="YES" Value="1"></asp:ListItem>
									<asp:ListItem Text="NO" Value="2"></asp:ListItem>
								</asp:DropDownList>
							</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1_TextChanged"></asp:TextBox><br />
							<br />	<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1_2" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1_2_TextChanged"></asp:TextBox><br />

									
								</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f1" Visible="true" runat="server"   Width="50px"></asp:Label><br />
								<br />
								
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f1_2" runat="server" Visible="false"  Width="50px"></asp:Label><br />
							</td>
							<td><br />
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f1" Visible="true" runat="server" Text="" Font-Bold="true"></asp:Label><br />							
								<br />	<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f1_2" Visible="false" runat="server" Text="" Font-Bold="true"></asp:Label><br />

							</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1_TextChanged"  runat="server"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox><br />
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1_2" Visible="false" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1_TextChanged"  runat="server"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox><br />

							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_f1" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_f1_Click" runat="server" Enabled="false" />
								<asp:Button ID="Button3" Text="Update" OnClick="Button3_Click" runat="server" Visible="false" />
							</td>
						</tr>
						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_f2" runat="server" Text="f.2**" ForeColor="Red" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_f2_Description" runat="server" Text="Article Published in a National magazine or newspaper or reviewing the article <br/> "></asp:Label><p style="font-style:italic">(Max. 3 articles will be considered) </p>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f2" runat="server" Text="6" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f2" runat="server" Text="2" Font-Bold="true"></asp:Label>
							</td>
								<td>
					<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnTextChanged="DropDownList1_TextChanged">
						<asp:ListItem Text="YES" Value="1"></asp:ListItem>
						<asp:ListItem Text="NO" Value="2"></asp:ListItem>
					</asp:DropDownList>
				</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f2" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f2_TextChanged"></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f2" runat="server"   Width="50px"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f2" runat="server" Text="" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2_TextChanged"></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_f2" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_f2_Click" runat="server" Enabled="false"/>
							    <asp:Button ID="Button4" Text="Update" OnClick="Button4_Click" runat="server" Visible="false" />
							</td>
						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_f3" runat="server" Text="f.3**" ForeColor="Red" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_f3_Description" runat="server" Text="Book chapter Publication with <br /><b> a)</b> National Publisher <br /> <b>b)</b> International Publisher"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f3" runat="server" Text="5" Font-Bold="true"></asp:Label>
							</td>
														
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f3" runat="server" Text="2" Font-Bold="true"></asp:Label><br />
							<br />	<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f3_2" runat="server" Text="3" Font-Bold="true"></asp:Label><br />

							</td>
																					<td>
				<asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true" OnTextChanged="DropDownList2_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3"  AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3_TextChanged" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox><br />
							<br />	<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3_2" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3_2_TextChanged" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox><br />

							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f3" Visible="true" runat="server"   Width="50px"></asp:Label><br />
			
							
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f3_2" Visible="false" runat="server"   Width="50px"></asp:Label><br />

							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f3" Visible="true" runat="server" Text="" Font-Bold="true"></asp:Label><br />
							
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f3_2" Visible="false" runat="server" Text="" Font-Bold="true"></asp:Label><br />

							</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3_TextChanged" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox><br />
							<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3_2" Visible="false" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3_2_TextChanged" runat="server"  Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox><br />

							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_f3" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_f3_Click" runat="server" Enabled="false"/>
							    <asp:Button ID="Button5" Text="Update" OnClick="Button5_Click" runat="server" Visible="false" />
							</td>
						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_f4" runat="server" Text="f.4**" ForeColor="Red" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_f4_Description" runat="server" Text="Authoring/co-authoring an edited book or Conference Proceeding by an International Publisher"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f4" runat="server" Text="6" Font-Bold="true"></asp:Label>
							</td>
														
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f4" runat="server" Text="6" Font-Bold="true"></asp:Label>
							</td>
																					<td>
				<asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="true" OnTextChanged="DropDownList3_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
							<td>
								<asp:TextBox AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f4_TextChanged" ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f4" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f4" runat="server"   Width="50px"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f4" runat="server" Text="" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f4" runat="server"   AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f4_TextChanged" Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_f4" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_f4_Click" runat="server" Enabled="false" />
							    <asp:Button ID="Button6" Text="Update" OnClick="Button6_Click" runat="server" Visible="false" />
							</td>
						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_f5" runat="server" Text="f.5**" ForeColor="Red" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_f5_Description" runat="server" Text="Authoring/co-authoring an edited book or Conference Proceeding by a National Publisher"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f5" runat="server" Text="4" Font-Bold="true"></asp:Label>
							</td>
														
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f5" runat="server" Text="4" Font-Bold="true"></asp:Label>
							</td>
																					<td>
				<asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="true" OnTextChanged="DropDownList4_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
							<td>
								<asp:TextBox AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f5_TextChanged" ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f5" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f5" runat="server"   Width="50px"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f5" runat="server" Text="" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f5_TextChanged" ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f5" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_f5" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_f5_Click" Text="Upload File" runat="server" Enabled="false" />
							    <asp:Button ID="Button7" Text="Update" OnClick="Button7_Click" runat="server" Visible="false" />
							</td>
						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_f6" runat="server" Text="f.6**" ForeColor="Red" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_f6_Description" runat="server" Text="Book published through National Publishers"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f6" runat="server" Text="8" Font-Bold="true"></asp:Label>
							</td>
														
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f6" runat="server" Text="8" Font-Bold="true"></asp:Label>
							</td>
																					<td>
				<asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="true" OnTextChanged="DropDownList5_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
							<td>
								<asp:TextBox AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f6_TextChanged" ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f6" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f6" runat="server"   Width="50px"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f6" runat="server" Text="" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f6_TextChanged" ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f6" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_f6" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_f6_Click" runat="server" Enabled="false"/>
							    <asp:Button ID="Button8" Text="Update" OnClick="Button8_Click" runat="server" Visible="false" />
							</td>
						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_f7" runat="server" Text="f.7**" ForeColor="Red" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_f7_Description" runat="server" Text="Book published through International Publishers"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f7" runat="server" Text="10" Font-Bold="true"></asp:Label>
							</td>
														
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f7" runat="server" Text="10" Font-Bold="true"></asp:Label>
							</td>
																					<td>
				<asp:DropDownList ID="DropDownList6" runat="server" AutoPostBack="true" OnTextChanged="DropDownList6_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
							<td>
								<asp:TextBox AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f7_TextChanged" ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f7" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f7" runat="server"   Width="50px"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f7" runat="server" Text="" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f7" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f7_TextChanged" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_f7" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_f7_Click" runat="server" Enabled="false"/>
							    <asp:Button ID="Button9" Text="Update" OnClick="Button9_Click" runat="server" Visible="false" />
							</td>
						</tr>

						<tr>
							<td class="highlighted-row">
								<asp:Label ID="lbl_CriteriaB_ResearchSupervision_G" runat="server" Text="G)" Font-Bold="true"></asp:Label></td>
							<td colspan="6" class="highlighted-row left-text"><strong>Research Supervision</strong></td>
							
							<td class="highlighted-row"></td>
							<td class="highlighted-row"></td>
						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_g1" runat="server" Text="g.1" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_g1_Description" runat="server" Text="Scores for each PhD awarded during the current session"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_g1" runat="server" Text="5" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_g1" runat="server" Text="5" Font-Bold="true"></asp:Label>
							</td>
																					<td>
				<asp:DropDownList ID="DropDownList7" runat="server">
					<%--<asp:ListItem Text="YES" Value="1"></asp:ListItem>--%>
					<asp:ListItem Text="NO" Value="2" Selected="True"></asp:ListItem>
				</asp:DropDownList>
</td>
							<td>
								<asp:TextBox AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g1_TextChanged" ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g1" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g1" runat="server"   Width="50px"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g1" runat="server" Text="" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g1_TextChanged" ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g1" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_g1" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_g1_Click" runat="server" Enabled="false"/>
							</td>
						</tr>


						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_g2" runat="server" Text="g.2" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_g2_Description" runat="server" Text="Pre-thesis submission, and presentation completed & approved by CRC"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_g2" runat="server" Text="4" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_g2" runat="server" Text="4" Font-Bold="true"></asp:Label>
							</td>
																					<td>
				<asp:DropDownList ID="DropDownList8" runat="server">
					<%--<asp:ListItem Text="YES" Value="1"></asp:ListItem>--%>
					<asp:ListItem Text="NO" Value="2" Selected="True"></asp:ListItem>
				</asp:DropDownList>
</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g2" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g2_TextChanged" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g2" runat="server"   Width="50px"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g2" runat="server" Text="" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g2" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g2_TextChanged" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_g2" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_g2_Click" runat="server" Enabled="false"/>
							</td>
						</tr>
						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_g3" runat="server" Text="g.3" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_g3_Description" runat="server" Text="Active PhD registered candidate (have been continually submitting progress reports without any break)"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_g3" runat="server" Text="10" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_g3" runat="server" Text="2" Font-Bold="true"></asp:Label>
							</td>
																					<td>
				<asp:DropDownList ID="DropDownList9" runat="server">
					<%--<asp:ListItem Text="YES" Value="1"></asp:ListItem>--%>
					<asp:ListItem Text="NO" Value="2" Selected="True"></asp:ListItem>
				</asp:DropDownList>
</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g3" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g3_TextChanged"></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g3" runat="server"   Width="50px"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g3" runat="server" Text="" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g3" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g3_TextChanged" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_g3" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_g3_Click" runat="server" Enabled="false"/>
							</td>
						</tr>
						<tr>
							<td class="highlighted-row">
								<asp:Label ID="lbl_CriteriaB_ResearchSupervision_H" runat="server" Text="H)" Font-Bold="true"></asp:Label></td>
							<td colspan="6" class="highlighted-row left-text"><strong>Research Project</strong></td>
							
							<td class="highlighted-row"></td>
							<td class="highlighted-row"></td>
						</tr>


						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_h1" runat="server" Text="h.1**" ForeColor="Red" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_h1_Description" runat="server" Text="Patent awarded/published/filed at the international level."></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h1" runat="server" Text="8" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h1" runat="server" Text="8" Font-Bold="true"></asp:Label>
							</td>
																					<td>
				<asp:DropDownList ID="DropDownList10" runat="server" AutoPostBack="true" OnTextChanged="DropDownList10_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h1" runat="server"   Width="50px" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h1_TextChanged"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h1" runat="server"   Width="50px"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h1" runat="server" Text="" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h1" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h1_TextChanged" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_h1" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_h1_Click" runat="server" Enabled="false" />
							<asp:Button ID="Button10" Text="Update" OnClick="Button10_Click" runat="server" Visible="false" />
							</td>
						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_h2" runat="server" Text="h.2**" ForeColor="Red" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_h2_Description" runat="server" Text="Patent awarded/published/filed at National level."></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h2" runat="server" Text="6" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h2" runat="server" Text="6" Font-Bold="true"></asp:Label>
							</td>
																												<td>
				<asp:DropDownList ID="DropDownList11" runat="server" AutoPostBack="true" OnTextChanged="DropDownList11_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h2" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h2_TextChanged" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h2" runat="server"   Width="50px"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h2" runat="server" Text="" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h2" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h2_TextChanged"></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_h2" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_h2_Click" runat="server" Enabled="false"/>
						<asp:Button ID="Button11" Text="Update" OnClick="Button11_Click" runat="server" Visible="false" />
								</td>
						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_h3" runat="server" Text="h.3**" ForeColor="Red" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_h3_Description" runat="server" Text="Completed research project of more than 10 lakhs funded by government bodies"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h3" runat="server" Text="10" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h3" runat="server" Text="10" Font-Bold="true"></asp:Label>
							</td>
																												<td>
				<asp:DropDownList ID="DropDownList12" runat="server" AutoPostBack="true" OnTextChanged="DropDownList12_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h3" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h3_TextChanged" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h3" runat="server"   Width="50px"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h3" runat="server" Text="" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h3" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h3_TextChanged"></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_h3" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_h3_Click" runat="server" Enabled="false"/>
							<asp:Button ID="Button12" Text="Update" OnClick="Button12_Click" runat="server" Visible="false" />
							</td>
						</tr>
						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_h4" runat="server" Text="h.4**" ForeColor="Red" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_h4_Description" runat="server" Text="Completed research project of less than 10 lakhs funded by government bodies"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h4" runat="server" Text="7" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h4" runat="server" Text="7" Font-Bold="true"></asp:Label>
							</td>
																												<td>
				<asp:DropDownList ID="DropDownList13" runat="server" AutoPostBack="true" OnTextChanged="DropDownList13_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h4" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h4_TextChanged"></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h4" runat="server"   Width="50px"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h4" runat="server" Text="" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h4" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h4_TextChanged"></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_h4" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_h4_Click" runat="server" Enabled="false"/>
							<asp:Button ID="Button13" Text="Update" OnClick="Button13_Click" runat="server" Visible="false" />
							</td>
						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_h5" runat="server" Text="h.5**" ForeColor="Red" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_h5_Description" runat="server" Text="Ongoing research project of more than 10 lakhs funded by government bodies"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h5" runat="server" Text="6" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h5" runat="server" Text="6" Font-Bold="true"></asp:Label>
							</td>
																												<td>
				<asp:DropDownList ID="DropDownList14" runat="server" AutoPostBack="true" OnTextChanged="DropDownList14_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h5" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h5_TextChanged"></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h5" runat="server"   Width="50px"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h5" runat="server" Text="" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h5" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h5_TextChanged"></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_h5" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_h5_Click" runat="server" Enabled="false"/>
							<asp:Button ID="Button14" Text="Update" OnClick="Button14_Click" runat="server" Visible="false" />
							</td>
						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_h6" runat="server" Text="h.6**" ForeColor="Red" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_h6_Description" runat="server" Text="Ongoing research project of less than 10 lakhs funded by government bodies"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h6" runat="server" Text="5" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h6" runat="server" Text="5" Font-Bold="true"></asp:Label>
							</td>
																												<td>
				<asp:DropDownList ID="DropDownList15" runat="server" AutoPostBack="true" OnTextChanged="DropDownList15_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h6" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h6_TextChanged"></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h6" runat="server"   Width="50px"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h6" runat="server" Text="" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h6" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h6_TextChanged"></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_h6" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_h6_Click" runat="server" Enabled="false"/>
							    <asp:Button ID="Button15" Text="Update" OnClick="Button15_Click" runat="server" Visible="false" />
							</td>
						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_h7" runat="server" Text="h.7**" ForeColor="Red" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_h7_Description" runat="server" Text="Research Project funded by non-government bodies"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h7" runat="server" Text="4" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h7" runat="server" Text="4" Font-Bold="true"></asp:Label>
							</td>
																												<td>
				<asp:DropDownList ID="DropDownList16" runat="server" AutoPostBack="true" OnTextChanged="DropDownList16_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h7" runat="server"   Width="50px" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h7_TextChanged"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h7" runat="server"   Width="50px"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h7" runat="server" Text="" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h7" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h7_TextChanged"></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_h7" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_h7_Click" runat="server" Enabled="false"/>
							<asp:Button ID="Button16" Text="Update" OnClick="Button16_Click" runat="server" Visible="false" />
							
							</td>
						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_h8" runat="server" Text="h.8**" ForeColor="Red" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_h8_Description" runat="server" Text="Research project (Seed money Project) funded by university"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h8" runat="server" Text="4" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h8" runat="server" Text="4" Font-Bold="true"></asp:Label>
							</td>
																												<td>
				<asp:DropDownList ID="DropDownList17" runat="server" AutoPostBack="true" OnTextChanged="DropDownList17_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h8" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h8_TextChanged"></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h8" runat="server"   Width="50px"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h8" runat="server" Text="" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8_TextChanged"></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_h8" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_h8_Click" runat="server" Enabled="false"/>
							   <asp:Button ID="Button17" Text="Update" OnClick="Button17_Click" runat="server" Visible="false" />
							</td>
						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_h9" runat="server" Text="h.9**" ForeColor="Red" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_h9_Description" runat="server" Text="Scores for published technical reports"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h9" runat="server" Text="3" Font-Bold="true"></asp:Label>
							</td>
																												
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h9" runat="server" Text="3" Font-Bold="true"></asp:Label>
							</td>
																																			<td>
				<asp:DropDownList ID="DropDownList18" runat="server" AutoPostBack="true" OnTextChanged="DropDownList18_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h9" runat="server"   Width="50px" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h9_TextChanged"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h9" runat="server"   Width="50px"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h9" runat="server" Text="" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9" runat="server"   Width="50px" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9_TextChanged"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_h9" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_h9_Click" runat="server" Enabled="false"/>
							<asp:Button ID="Button34" Text="Update" OnClick="Button34_Click" runat="server" Visible="false" />
							</td>
						</tr>

						
							<tr>
							<td class="highlighted-row">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_i" runat="server" Text="I)" Font-Bold="true"></asp:Label></td>
							<td colspan="6" class="highlighted-row left-text"><strong>Participation in Research Activites</strong></td>
							
								<td class="highlighted-row"></td>
							<td class="highlighted-row"></td>
						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_i1" runat="server" Text="i.1**" ForeColor="Red" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_i1_description" runat="server" Text="Research Paper presented at the National Conference"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i1" runat="server" Text="2" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_i1" runat="server" Text="2" Font-Bold="true"></asp:Label>
							</td>
																												<td>
				<asp:DropDownList ID="DropDownList19" runat="server" AutoPostBack="true" OnTextChanged="DropDownList19_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i1" runat="server"   AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i1_TextChanged" Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i1" runat="server"   Width="50px"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i1" runat="server" Text="" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i1" runat="server"   Width="50px" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i1_TextChanged"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_i1" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_i1_Click" runat="server" Enabled="false"/>
							<asp:Button ID="Button20" Text="Update" OnClick="Button20_Click" runat="server" Visible="false" />
							</td>
						</tr>
						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_i2" runat="server" Text="i.2**" ForeColor="Red" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_i2_Description" runat="server" Text="Research Paper presented at the International Conference"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i2" runat="server" Text="3" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_i2" runat="server" Text="3" Font-Bold="true"></asp:Label>
							</td>
																												<td>
				<asp:DropDownList ID="DropDownList20" runat="server" AutoPostBack="true" OnTextChanged="DropDownList20_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i2" runat="server" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i2_TextChanged"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i2" runat="server"   Width="50px"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i2" runat="server" Text="" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i2" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i2_TextChanged"></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_i2" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_i2_Click" runat="server" Enabled="false"/>
							<asp:Button ID="Button21" Text="Update" OnClick="Button21_Click" runat="server" Visible="false" />
							</td>
						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_i3" runat="server" Text="i.3**" ForeColor="Red" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_i3_Description" runat="server" Text="Research Paper publication in a Conference Proceeding"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i3" runat="server" Text="3" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_i3" runat="server" Text="3" Font-Bold="true"></asp:Label>
							</td>
																												<td>
				<asp:DropDownList ID="DropDownList21" runat="server" AutoPostBack="true" OnTextChanged="DropDownList21_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i3" runat="server"   Width="50px" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i3_TextChanged"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i3" runat="server"   Width="50px"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i3" runat="server" Text="" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i3" runat="server"   AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i3_TextChanged" Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_i3" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_i3_Click" runat="server" Enabled="false"/>
							<asp:Button ID="Button22" Text="Update" OnClick="Button22_Click" runat="server" Visible="false" />
							</td>
						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_i4" runat="server" Text="i.4**" ForeColor="Red" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_i4_Description" runat="server" Text="Organized Workshops, Seminar, Conference, IPR, Tech-Fest, Industry-Academia Innovative Practice, Short-Term Courses, Value added Courses at the National level as Convenor, Secretary/coordinator of an activity <br/> "></asp:Label><p style="font-style:italic">(Max. 3 activities will be considered)</p>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i4" runat="server" Text="12" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_i4" runat="server" Text="4" Font-Bold="true"></asp:Label>
							</td>
																												<td>
				<asp:DropDownList ID="DropDownList22" runat="server" AutoPostBack="true" OnTextChanged="DropDownList22_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i4" runat="server"   AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i4_TextChanged" Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i4" runat="server"   Width="50px"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i4" runat="server" Text="" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i4" runat="server" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i4_TextChanged"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_i4" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_i4_Click" runat="server" Enabled="false"/>
							<asp:Button ID="Button23" Text="Update" OnClick="Button23_Click" runat="server" Visible="false" />
							</td>
						</tr>


						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_i5" runat="server" ForeColor="Red" Text="i.5**" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_i5_Description" runat="server" Text="Organized Workshops, Seminar, Conference, IPR, Tech-Fest, Industry-Academia Innovative Practice, Short-Term Courses, Value added Courses at the international level as Convenor, Secretary/coordinator of an activity<br/>"></asp:Label><p style="font-style:italic">(Max. 2 activities will be considered)</p>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i5" runat="server" Text="12" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_i5" runat="server" Text="6" Font-Bold="true"></asp:Label>
							</td>
																												<td>
				<asp:DropDownList ID="DropDownList23" runat="server" AutoPostBack="true" OnTextChanged="DropDownList23_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i5" runat="server" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i5_TextChanged"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i5" runat="server"   Width="50px"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i5" runat="server" Text="" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i5" runat="server"   Width="50px" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i5_TextChanged"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_i5" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_i5_Click" runat="server" Enabled="false"/>
							<asp:Button ID="Button24" Text="Update" OnClick="Button24_Click" runat="server" Visible="false" />
							</td>
						</tr>

						<tr>
							<td class="highlighted-row"><asp:Label ID="lbl_CriteriaB_ConsultancyProjectsFDPsMDPsandStartups_J" runat="server" Text="J)" Font-Bold="true"></asp:Label></td>
							<td colspan="6" class="highlighted-row left-text"><strong>Consultancy, Projects, FDPs/MDPs & Start-ups </strong></td>
							
							<td class="highlighted-row"></td>
							<td class="highlighted-row"></td>
						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_j1" runat="server" Text="j.1**" ForeColor="Red" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_j1_Description" runat="server" Text="• 4 scores for revenue up to Rs. 50,000/-<br>• 6 scores for revenue between Rs. 50,000 to Rs. 1,00,000<br>• 8 scores for revenue between Rs. 1,00,000 to Rs. 2,00,000<br>• 10 scores for revenue between Rs. 2,00,000 to Rs. 3,00,000<br>• 12 scores for revenue more than Rs. 3,00,000"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_j1" runat="server" Text="12" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_j1" runat="server" Text="12" Font-Bold="true"></asp:Label>
							</td>
																												<td>
				<asp:DropDownList ID="DropDownList24" runat="server" AutoPostBack="true" OnTextChanged="DropDownList24_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j1" runat="server"   Width="50px" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j1_TextChanged"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_j1" runat="server"   Width="50px"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_j1" runat="server" Text="" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j1" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j1_TextChanged" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_j1" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_j1_Click" runat="server" Enabled="false"/>
							<asp:Button ID="Button18" Text="Update" OnClick="Button18_Click" runat="server" Visible="false" />
							</td>
						</tr>

						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_j2" runat="server" Text="j.2**" ForeColor="Red" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_j2_Description" runat="server" Text="Scores for incubating one start-up"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_j2" runat="server" Text="13" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_j2" runat="server" Text="13" Font-Bold="true"></asp:Label>
							</td>
																												<td>
				<asp:DropDownList ID="DropDownList25" runat="server" AutoPostBack="true" OnTextChanged="DropDownList25_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j2" runat="server" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j2_TextChanged"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_j2" runat="server"   Width="50px"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_j2" runat="server" Text="" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j2" runat="server" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j2_TextChanged"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_j2" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_j2_Click" runat="server" Enabled="false"/>
							<asp:Button ID="Button19" Text="Update" OnClick="Button19_Click" runat="server" Visible="false" />
							</td>
						</tr>
						<tr>
							
							<td colspan="2"></td>
							<td class="grey-highlighted-row">
								<asp:Label ID="lbl_maxscore_total" runat="server" Text="200" Font-Bold="true"></asp:Label></td>
							<td colspan="2" class="whitebg-row"></td>
							<td class="grey-highlighted-row">
								<asp:Label ID="lbl_api_score_through_self_assessment_total" runat="server" Text="" Font-Bold="true"></asp:Label></td>
							<td class="grey-highlighted-row">
								<asp:Label ID="lbl_finally_obtained_score_total" runat="server" Text="" Font-Bold="true"></asp:Label></td>
							<td class="grey-highlighted-row">
								<asp:Label ID="lbl_assessmentby_reportingauthority_total" Font-Bold="true" runat="server" Text=""></asp:Label></td>
														
						</tr>


					</table>

					</td>
				</tr>


						<tr style="display: block; padding-top: 20px;">
					<td style="text-align: center">
						<h3 style="text-decoration: underline; display: block; margin: 0 auto; text-align: center;"><b>Criteria-C: Professional & Personal Competency</h3></b>
						<span style="display: block; text-align: right"><strong>(Max. API Score: 100)</strong></span>
						<h4 style="text-align:left" class="vertical-padding">Based on the teacher’s self-assessment, API scores are proposed for <strong>(a)</strong> Self-Development; <strong>(b)</strong> Peer/Institute/Industry connect; <strong>(c)</strong> Student Support &amp; Counselling etc.</h4>

						<table border="1">
						<tr>
							<th colspan="2"; class="whitebg-row; "></th>
							<th style="text-align:center" class="red-highlighted-row1">Max. API Score(P)</th>
							<th style="text-align:center" class="red-highlighted-row1">Max Scores Per Activity(A)</th>
							<th style="text-align:center" class="red-highlighted-row1">Any Activity of this month done or not</th>
							<th style="text-align:center" class="red-highlighted-row1">Total No of Activities Performed in Acutal(B)</th>
							<th style="text-align:center" class="red-highlighted-row1">API Score through Self-Assessment(Q=A*B)</th>
							<th style="text-align:center" class="red-highlighted-row1">Finally Obtained API Score (Minimum of P & Q)</th>
							<th style="text-align:center" class="red-highlighted-row1">Assessment by Reporting Authority through API Score</th>
							<th style="text-align:center" class="red-highlighted-row1">Upload Relevant Documents(pdf, jpg with <200kb size)</th>
						</tr>
							<tr>
							<td class="highlighted-row"><asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_K" runat="server" Text="K)" Font-Bold="true"></asp:Label></td>
							<td colspan="8" class="highlighted-row left-text"><strong>Self Development</strong></td></tr>				
						
						
							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_k1" runat="server" Text="k.1" Font-Bold="true"></asp:Label>
								</td>
								<td class="left-text">
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_k1_Description" runat="server" Text="PhD completed (During current session)"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k1" runat="server" Text="5" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k1" runat="server" Text="5" Font-Bold="true"></asp:Label>
								</td>
																																				<td>
				<asp:DropDownList ID="DropDownList26" runat="server">
					<%--<asp:ListItem Text="YES" Value="1"></asp:ListItem>--%>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k1" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k1_TextChanged"></asp:TextBox>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k1" runat="server"   Width="50px"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k1" runat="server" Text="" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k1" AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k1_TextChanged" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
								</td>
								<td>
									<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_k1" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_k1_Click" runat="server" Enabled="false" />
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_k2" runat="server" Text="k.2" Font-Bold="true"></asp:Label>
								</td>
								<td class="left-text">
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_k2_Description" runat="server" Text="PhD registered (During current session)"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k2" runat="server" Text="3" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k2" runat="server" Text="3" Font-Bold="true"></asp:Label>
								</td>
																																				<td>
				<asp:DropDownList ID="DropDownList27" runat="server">
					<%--<asp:ListItem Text="YES" Value="1"></asp:ListItem>--%>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k2" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k2_TextChanged" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true">
</asp:TextBox>

								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k2" runat="server"   Width="50px"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k2" runat="server" Text="" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k2" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k2_TextChanged" AutoPostBack="true"></asp:TextBox>
								</td>
								<td>
									<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_k2" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_k2_Click" runat="server" Enabled="false"/>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_k3" runat="server" Text="k.3**" ForeColor="Red" Font-Bold="true"></asp:Label>
								</td>
								<td class="left-text">
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_k3_Description" runat="server" Text="Attended FDPs/Conferences/Workshops/Skill Enhancement Programmes/Seminars at the National Level <br/> "></asp:Label><p style="font-style:italic">(Max. 3 activities will be considered)</p>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k3" runat="server" Text="6" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k3" runat="server" Text="2" Font-Bold="true"></asp:Label>
								</td>
																																				<td>
				<asp:DropDownList ID="DropDownList28" runat="server" AutoPostBack="true" OnTextChanged="DropDownList28_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k3" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k3_TextChanged" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true"></asp:TextBox>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k3" runat="server"   Width="50px"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k3" runat="server" Text="" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k3" AutoPostBack="true" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k3_TextChanged"></asp:TextBox>
								</td>
								<td>
									<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_k3" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_k3_Click" runat="server" Enabled="false"/>
								<asp:Button ID="Button25" Text="Update" OnClick="Button25_Click" runat="server" Visible="false" />
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_k4" runat="server" Text="k.4**" ForeColor="Red" Font-Bold="true"></asp:Label>
								</td>
								<td class="left-text">
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_k4_Description" runat="server" Text="Attended FDPs/Conferences/Workshops/Skill Enhancement Programmes/Seminars at the International Level <br/> "></asp:Label> <p style="font-style:italic"> (Max. 3 activities will be considered)</p>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k4" runat="server" Text="9" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k4" runat="server" Text="3" Font-Bold="true"></asp:Label>
								</td>
																																				<td>
				<asp:DropDownList ID="DropDownList29" runat="server" AutoPostBack="true" OnTextChanged="DropDownList29_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k4" runat="server"   Width="50px" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k4_TextChanged"     oninput="validatePositiveNumber(this)"  AutoPostBack="true"></asp:TextBox>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k4" runat="server"   Width="50px"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k4" runat="server" Text="" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k4" AutoPostBack="true" runat="server"   Width="50px" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k4_TextChanged"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
								</td>
								<td>
									<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_k4" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_k4_Click" runat="server" Enabled="false"/>
								<asp:Button ID="Button26" Text="Update" OnClick="Button26_Click" runat="server" Visible="false" />
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_k5" runat="server" Text="k.5**" ForeColor="Red" Font-Bold="true"></asp:Label>
								</td>
								<td class="left-text">
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_k5_Description" runat="server" Text="Attending UGC/AICTE sponsored Refresh Course/Induction Programme/FDPs conducted by any of the top University (NAAC 'A or higher graded or 'NIRF' ranking)"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k5" runat="server" Text="5" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k5" runat="server" Text="5" Font-Bold="true"></asp:Label>
								</td>
																																				<td>
				<asp:DropDownList ID="DropDownList30" runat="server" AutoPostBack="true" OnTextChanged="DropDownList30_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k5" runat="server"   Width="50px" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k5_TextChanged"     oninput="validatePositiveNumber(this)"  AutoPostBack="true"></asp:TextBox>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k5" runat="server"   Width="50px"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k5" runat="server" Text="" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k5" AutoPostBack="true" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k5_TextChanged"></asp:TextBox>
								</td>
								<td>
									<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_k5" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_k5_Click" runat="server" Enabled="false"/>
								<asp:Button ID="Button27" Text="Update" OnClick="Button27_Click" runat="server" Visible="false" />
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_k6" runat="server" Text="k.6**" ForeColor="Red" Font-Bold="true"></asp:Label>
								</td>
								<td class="left-text">
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_k6_Description" runat="server" Text="Invited for extension lectures/PG level project evaluation/External Examination by College/University"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k6" runat="server" Text="6" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k6" runat="server" Text="3" Font-Bold="true"></asp:Label>
								</td>
																																				<td>
				<asp:DropDownList ID="DropDownList31" runat="server" AutoPostBack="true" OnTextChanged="DropDownList31_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k6" runat="server"   Width="50px" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k6_TextChanged"     oninput="validatePositiveNumber(this)"  AutoPostBack="true"></asp:TextBox>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k6" runat="server"   Width="50px"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k6" runat="server" Text="" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k6" AutoPostBack="true" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k6_TextChanged"></asp:TextBox>
								</td>
								<td>
									<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_k6" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_k6_Click" runat="server" Enabled="false"/>
								<asp:Button ID="Button28" Text="Update" OnClick="Button28_Click" runat="server" Visible="false" />
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_k7" runat="server" Text="k.7**" ForeColor="Red" Font-Bold="true"></asp:Label>
								</td>
								<td class="left-text">
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_k7_Description" runat="server" Text="Thesis Examination"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k7" runat="server" Text="3" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k7" runat="server" Text="3" Font-Bold="true"></asp:Label>
								</td>
																																				<td>
				<asp:DropDownList ID="DropDownList32" runat="server" AutoPostBack="true" OnTextChanged="DropDownList32_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k7" runat="server"   OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k7_TextChanged" Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true"></asp:TextBox>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k7" runat="server"   Width="50px"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k7" runat="server" Text="" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k7" AutoPostBack="true" runat="server"   Width="50px" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k7_TextChanged"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
								</td>
								<td>
									<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_k7" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_k7_Click" runat="server" Enabled="false"/>
								<asp:Button ID="Button29" Text="Update" OnClick="Button29_Click" runat="server" Visible="false" />
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_k8" runat="server" Text="k.8**" ForeColor="Red" Font-Bold="true"></asp:Label>
								</td>
								<td class="left-text">
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_k8_Description" runat="server" Text="Membership of Institutional/ Professional bodies"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k8" runat="server" Text="2" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k8" runat="server" Text="2" Font-Bold="true"></asp:Label>
								</td>
																																				<td>
				<asp:DropDownList ID="DropDownList33" runat="server" AutoPostBack="true" OnTextChanged="DropDownList33_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k8" runat="server"   Width="50px" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k8_TextChanged"     oninput="validatePositiveNumber(this)"  AutoPostBack="true"></asp:TextBox>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k8" runat="server"   Width="50px"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k8" runat="server" Text="" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k8" AutoPostBack="true" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k8_TextChanged"></asp:TextBox>
								</td>
								<td>
									<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_k8" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_k8_Click" Text="Upload File" runat="server" Enabled="false"/>
								   <asp:Button ID="Button30" Text="Update" OnClick="Button30_Click" runat="server" Visible="false" />
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_k9" runat="server" Text="k.9**" ForeColor="Red" Font-Bold="true"></asp:Label>
								</td>
								<td class="left-text">
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_k9_Description" runat="server" Text="Received Award/fellowship at the International Level"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k9" runat="server" Text="6" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k9" runat="server" Text="6" Font-Bold="true"></asp:Label>
								</td>
																																				<td>
				<asp:DropDownList ID="DropDownList34" runat="server" AutoPostBack="true" OnTextChanged="DropDownList34_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k9" runat="server"   Width="50px" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k9_TextChanged"     oninput="validatePositiveNumber(this)"  AutoPostBack="true"></asp:TextBox>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k9" runat="server"   Width="50px"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k9" runat="server" Text="" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k9" AutoPostBack="true" runat="server"   OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k9_TextChanged" Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
								</td>
								<td>
									<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_k9" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_k9_Click" runat="server" Enabled="false"/>
								    <asp:Button ID="Button31" Text="Update" OnClick="Button31_Click" runat="server" Visible="false" />
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_k10" runat="server" Text="k.10**" ForeColor="Red" Font-Bold="true"></asp:Label>
								</td>
								<td class="left-text">
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_k10_Description" runat="server" Text="Received Award/fellowship at the National Level"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k10" runat="server" Text="5" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k10" runat="server" Text="5" Font-Bold="true"></asp:Label>
								</td>
																																				<td>
				<asp:DropDownList ID="DropDownList35" runat="server" AutoPostBack="true" OnTextChanged="DropDownList35_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k10" runat="server" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k10_TextChanged"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true"></asp:TextBox>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k10" runat="server"   Width="50px"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k10" runat="server" Text="" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k10" AutoPostBack="true" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k10_TextChanged"></asp:TextBox>
								</td>
								<td>
									<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_k10" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_k10_Click" runat="server" Enabled="false"/>
								 <asp:Button ID="Button32" Text="Update" OnClick="Button32_Click" runat="server" Visible="false" />
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_k11" runat="server" Text="k.11**" ForeColor="Red" Font-Bold="true"></asp:Label>
								</td>
								<td class="left-text">
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_k11_Description" runat="server" Text="MOOC course completion"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k11" runat="server" Text="4" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k11" runat="server" Text="4" Font-Bold="true"></asp:Label>
								</td>
																																				<td>
				<asp:DropDownList ID="DropDownList36" runat="server" AutoPostBack="true" OnTextChanged="DropDownList36_TextChanged">
					<asp:ListItem Text="YES" Value="1"></asp:ListItem>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k11" runat="server"   OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k11_TextChanged" Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true"></asp:TextBox>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k11" runat="server"   Width="50px"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k11" runat="server" Text="" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k11" AutoPostBack="true" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k11_TextChanged"></asp:TextBox>
								</td>
								<td>
								 <asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_k11" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_k11_Click" runat="server" Enabled="false"/>
								 <asp:Button ID="Button33" Text="Update" OnClick="Button33_Click" runat="server" Visible="false" />
								</td>
							</tr>
							
							<tr>
							<td class="highlighted-row"><asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_L" runat="server" Text="L)" Font-Bold="true"></asp:Label></td>
							<td colspan="6" class="highlighted-row left-text"><strong>Peer/Institute/Industry Connect </strong></td>						
							
								<td class="highlighted-row"></td>
							<td class="highlighted-row"></td>
						</tr>
						<tr>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_l1" runat="server" Text="l.1" Font-Bold="true"></asp:Label>
							</td>
							<td class="left-text">
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_l1_Description" runat="server" Text="MOU for collaborative work & resource sharing etc. at the international level"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l1" runat="server" Text="5" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_l1" runat="server" Text="5" Font-Bold="true"></asp:Label>
							</td>
							
																																				<td>
				<asp:DropDownList ID="DropDownList37" runat="server">
					<%--<asp:ListItem Text="YES" Value="1"></asp:ListItem>--%>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l1" runat="server"   OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l1_TextChanged" Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true"></asp:TextBox>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l1" runat="server"   Width="50px"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l1" runat="server" Text="" Font-Bold="true"></asp:Label>
							</td>
							<td>
								<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l1" AutoPostBack="true" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l1_TextChanged"></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_l1" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_l1_Click" runat="server" Enabled="false" />
							</td>
						</tr>
							
							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_l2" runat="server" Text="l.2" Font-Bold="true"></asp:Label>
								</td>
								<td class="left-text">
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_l2_Description" runat="server" Text="MOU for collaborative work & resource sharing etc. at National level"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l2" runat="server" Text="3" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_l2" runat="server" Text="3" Font-Bold="true"></asp:Label>
								</td>
								
																																				<td>
				<asp:DropDownList ID="DropDownList38" runat="server">
					<%--<asp:ListItem Text="YES" Value="1"></asp:ListItem>--%>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l2" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l2_TextChanged"></asp:TextBox>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l2" runat="server"   Width="50px"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l2" runat="server" Text="" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l2" AutoPostBack="true" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l2_TextChanged"></asp:TextBox>
								</td>
								<td>
									<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_l2" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_l2_Click" runat="server" Enabled="false"/>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_l3" runat="server" Text="l.3" Font-Bold="true"></asp:Label>
								</td>
								<td class="left-text">
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_l3_Description" runat="server" Text="Social/Rural/ NSS/ NCC/ Red Cross/ NGO project or activities <br/> "></asp:Label> <p style="font-style:italic">(Max. 3 activities will be considered)</p>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l3" runat="server" Text="6" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_l3" runat="server" Text="2" Font-Bold="true"></asp:Label>
								</td>
								
																																				<td>
				<asp:DropDownList ID="DropDownList39" runat="server">
					<%--<asp:ListItem Text="YES" Value="1"></asp:ListItem>--%>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l3" runat="server"   OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l3_TextChanged" Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true"></asp:TextBox>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l3" runat="server"   Width="50px"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l3" runat="server" Text="" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l3" AutoPostBack="true" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l3_TextChanged"></asp:TextBox>
								</td>
								<td>
									<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_l3" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_l3_Click" Text="Upload File" runat="server" Enabled="false"/>
								</td>
							</tr>
							<tr>
							<td class="highlighted-row"><asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_M" runat="server" Text="M)" Font-Bold="true"></asp:Label></td>
							<td colspan="6" class="highlighted-row left-text"><strong>Student Support & Counselling  </strong></td>						
							
								<td class="highlighted-row"></td>
							<td class="highlighted-row"></td>
						</tr>

							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_m1" runat="server" Text="m.1" Font-Bold="true"></asp:Label>
								</td>
								<td class="left-text">
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_m1_Description" runat="server" Text="Mentorship as a Mentor of batch/class"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m1" runat="server" Text="4" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_m1" runat="server" Text="4" Font-Bold="true"></asp:Label>
								</td>
								
																																				<td>
				<asp:DropDownList ID="DropDownList40" runat="server">
					<%--<asp:ListItem Text="YES" Value="1"></asp:ListItem>--%>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m1" runat="server" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m1_TextChanged"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true"></asp:TextBox>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m1" runat="server"   Width="50px"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m1" runat="server" Text="" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m1" AutoPostBack="true" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m1_TextChanged"></asp:TextBox>
								</td>
								<td>
									<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_m1" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_m1_Click" runat="server" Enabled="false"/>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_m2" runat="server" Text="m.2" Font-Bold="true"></asp:Label>
								</td>
								<td class="left-text">
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_m2_Description" runat="server" Text="Competitive classes for Govt. Services/NET/GATE/CAT/JRF etc."></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m2" runat="server" Text="3" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_m2" runat="server" Text="3" Font-Bold="true"></asp:Label>
								</td>
								
																																				<td>
				<asp:DropDownList ID="DropDownList41" runat="server">
					<%--<asp:ListItem Text="YES" Value="1"></asp:ListItem>--%>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m2" runat="server" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m2_TextChanged"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true"></asp:TextBox>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m2" runat="server"   Width="50px"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m2" runat="server" Text="" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m2" AutoPostBack="true" runat="server"   Width="50px" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m2_TextChanged"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
								</td>
								<td>
									<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_m2" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_m2_Click" runat="server" Enabled="false"/>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_m3" runat="server" Text="m.3" Font-Bold="true"></asp:Label>
								</td>
								<td class="left-text">
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_m3_Description" runat="server" Text="Organized & accompanied students for industry visits/Education trips/excursions as coordinator or member at the National Level"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m3" runat="server" Text="2" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_m3" runat="server" Text="2" Font-Bold="true"></asp:Label>
								</td>
								
																																				<td>
				<asp:DropDownList ID="DropDownList42" runat="server">
					<%--<asp:ListItem Text="YES" Value="1"></asp:ListItem>--%>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m3" runat="server"   OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m3_TextChanged" Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true"></asp:TextBox>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m3" runat="server"   Width="50px"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m3" runat="server" Text="" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m3" AutoPostBack="true" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m3_TextChanged"></asp:TextBox>
								</td>
								<td>
									<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_m3" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_m3_Click" runat="server" Enabled="false"/>
								</td>
							</tr>

							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_m4" runat="server" Text="m.4" Font-Bold="true"></asp:Label>
								</td>
								<td class="left-text">
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_m4_Description" runat="server" Text="Organized & accompanied students for industry visits/Education trips/excursions as coordinator or member at the International Level"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m4" runat="server" Text="3" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_m4" runat="server" Text="3" Font-Bold="true"></asp:Label>
								</td>
								
																																				<td>
				<asp:DropDownList ID="DropDownList43" runat="server">
					<%--<asp:ListItem Text="YES" Value="1"></asp:ListItem>--%>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m4" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m4_TextChanged" AutoPostBack="true"></asp:TextBox>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m4" runat="server"   Width="50px"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m4" runat="server" Text="" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m4" AutoPostBack="true" runat="server"   OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m4_TextChanged" Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
								</td>
								<td>
									<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_m4" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_m4_Click" runat="server" Enabled="false"/>
								</td>
							</tr>
							<tr>
							<td class="highlighted-row"><asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_N" runat="server" Text="N)" Font-Bold="true"></asp:Label></td>
							<td colspan="6" class="highlighted-row left-text"><strong>Innovative Pedagogy</strong></td>						
							
								<td class="highlighted-row"></td>
							<td class="highlighted-row"></td>
						</tr>
							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_n1" runat="server" Text="n.1" Font-Bold="true"></asp:Label>
								</td>
								<td class="left-text">
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_n1_Description" runat="server" Text="Expert talk as an expert invited from the Institution/University except NIRF/NAAC"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n1" runat="server" Text="6" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_n1" runat="server" Text="3" Font-Bold="true"></asp:Label>
								</td>
								
																																				<td>
				<asp:DropDownList ID="DropDownList44" runat="server">
					<%--<asp:ListItem Text="YES" Value="1"></asp:ListItem>--%>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n1" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n1_TextChanged" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true"></asp:TextBox>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n1" runat="server"   Width="50px"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n1" runat="server" Text="" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n1" AutoPostBack="true" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n1_TextChanged"></asp:TextBox>
								</td>
								<td>
									<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_n1" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_n1_Click" runat="server" Enabled="false"/>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_n2" runat="server" Text="n.2" Font-Bold="true"></asp:Label>
								</td>
								<td class="left-text">
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_n2_Description" runat="server" Text="Expert talk as an expert invited from NIRF/A or higher NAAC accredited"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n2" runat="server" Text="10" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_n2" runat="server" Text="5" Font-Bold="true"></asp:Label>
								</td>
								
																																				<td>
				<asp:DropDownList ID="DropDownList45" runat="server">
					<%--<asp:ListItem Text="YES" Value="1"></asp:ListItem>--%>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n2" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n2_TextChanged"></asp:TextBox>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n2" runat="server"   Width="50px"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n2" runat="server" Text="" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n2" AutoPostBack="true" runat="server"   Width="50px" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n2_TextChanged"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
								</td>
								<td>
									<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_n2" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_n2_Click" runat="server" Enabled="false"/>
								</td>
							</tr>

							<tr>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_n3" runat="server" Text="n.3" Font-Bold="true"></asp:Label>
								</td>
								<td class="left-text">
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_n3_Description" runat="server" Text="Lecture(s) delivered by using recognized & proven innovative teaching methods"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n3" runat="server" Text="4" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_n3" runat="server" Text="4" Font-Bold="true"></asp:Label>
								</td>
								
																																				<td>
				<asp:DropDownList ID="DropDownList46" runat="server">
					<%--<asp:ListItem Text="YES" Value="1"></asp:ListItem>--%>
					<asp:ListItem Text="NO" Value="2"></asp:ListItem>
				</asp:DropDownList>
</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n3" runat="server"   Width="50px" OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n3_TextChanged"     oninput="validatePositiveNumber(this)"  AutoPostBack="true"></asp:TextBox>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n3" runat="server"   Width="50px"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n3" runat="server" Text="" Font-Bold="true"></asp:Label>
								</td>
								<td>
									<asp:TextBox ID="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n3" AutoPostBack="true" runat="server"   OnTextChanged="txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n3_TextChanged" Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
								</td>
								<td>
									<asp:Button ID="fu_CriteriaB_ResearchAndDevelopment_FileUpload_n3" Text="Upload File" OnClick="fu_CriteriaB_ResearchAndDevelopment_FileUpload_n3_Click" runat="server" Enabled="false"/>
								</td>
							</tr>

							<tr>
							
							<td colspan="2"></td>
							<td class="grey-highlighted-row">
								<asp:Label ID="lbl_maxscore_total_criteriac" runat="server" Text="100" Font-Bold="true"></asp:Label></td>
							<td colspan="2" class="whitebg-row"></td>
							<td class="grey-highlighted-row">
								<asp:Label ID="lbl_api_score_through_self_assessment_total_criteriac" runat="server" Text="" Font-Bold="true"></asp:Label></td>
							<td class="grey-highlighted-row">
								<asp:Label ID="lbl_finally_obtained_score_total_criteriac" runat="server" Text="" Font-Bold="true"></asp:Label></td>
							<td class="grey-highlighted-row">
								<asp:Label ID="lbl_assessmentby_reportingauthority_total_Criteriac" Font-Bold="true" runat="server" Text=""></asp:Label></td>
														
						</tr>

						</table>



					</td>
						</tr>

					<tr class="vertical-padding" style="text-align: center;">
						<td>
							<h3 style="text-decoration-line: underline"><b>Criteria-D: Administration</h3></b>
							<span style="text-align: right; display: block"><strong>(Max API Score: 100)</strong></span>
							<h4 style="padding-bottom: 20px; margin: 0 auto;">API scores are proposed for Institution Building based on the teacher's self-assessment.</h4>
						
							<table border="1">
						<tr>
							<th colspan="2"; class="whitebg-row; "></th>
							<th style="text-align:center" class="red-highlighted-row1">Max. API Score(P)</th>
							<th style="text-align:center" class="red-highlighted-row1">Max Scores Per Activity(A)</th>
							<th style="text-align:center" class="red-highlighted-row1">Total No of Activities Performed in Acutal(B)</th>
							<th style="text-align:center" class="red-highlighted-row1">API Score through Self-Assessment(Q=A*B)</th>
							<th style="text-align:center" class="red-highlighted-row1">Finally Obtained API Score (Minimum of P & Q)</th>
							<th style="text-align:center" class="red-highlighted-row1">Assessment by Reporting Authority through API Score</th>
							<th style="text-align:center" class="red-highlighted-row1">Upload Relevant Documents(pdf, jpg with <200kb size)</th>
						</tr>
							<tr>
							<td class="highlighted-row"><asp:Label ID="lbl_CriteriaD_Administration_D" runat="server" Text="O)" Font-Bold="true"></asp:Label></td>
							<td colspan="6" class="highlighted-row left-text"><strong>Institution Building.</strong></td>						
							
								<td class="highlighted-row"></td>
							<td class="highlighted-row"></td>
						</tr>
								<tr>
									<td>
										<asp:Label ID="lbl_CriteriaD_Administration_o1" runat="server" Text="o.1" Font-Bold="true"></asp:Label>
									</td>
									<td class="left-text">
										<asp:Label ID="lbl_CriteriaD_Administration_o1_Description" runat="server" Text="Administrative responsibility at College & University level"></asp:Label>
									</td>
									<td>
										<asp:Label ID="lbl_CriteriaD_Administration_MaxAPIScore_o1" runat="server" Text="30" Font-Bold="true"></asp:Label>
									</td>
									<td>
										<asp:Label ID="lbl_CriteriaD_Administration_ScoresPerActivity_o1" runat="server" Text="30" Font-Bold="true"></asp:Label>
									</td>
									<td>
										<asp:TextBox ID="txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o1" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true" OnTextChanged="txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o1_TextChanged"></asp:TextBox>
									</td>
									<td>
										<asp:Label ID="lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o1" runat="server"   Width="50px"></asp:Label>
									</td>
									<td>
										<asp:Label ID="lbl_CriteriaD_Administration_TotalorFaculty_o1" runat="server" Text="" Font-Bold="true"></asp:Label>
									</td>
									<td>
										<asp:TextBox ID="txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o1" AutoPostBack="true" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o1_TextChanged"></asp:TextBox>
									</td>
									<td>
										<asp:Button ID="fu_CriteriaD_Administration_FileUpload_o1" Text="Uplaod File" OnClick="fu_CriteriaD_Administration_FileUpload_o1_Click" runat="server" Enabled="false" />
									</td>
								</tr>
								<tr>
									<td>
										<asp:Label ID="lbl_CriteriaD_Administration_o2" runat="server" Text="o.2" Font-Bold="true"></asp:Label>
									</td>
									<td class="left-text">
										<asp:Label ID="lbl_CriteriaD_Administration_o2_Description" runat="server" Text="Department & programme level responsibility at college"></asp:Label>
									</td>
									<td>
										<asp:Label ID="lbl_CriteriaD_Administration_MaxAPIScore_o2" runat="server" Text="20" Font-Bold="true"></asp:Label>
									</td>
									<td>
										<asp:Label ID="lbl_CriteriaD_Administration_ScoresPerActivity_o2" runat="server" Text="20" Font-Bold="true"></asp:Label>
									</td>
									<td>
										<asp:TextBox ID="txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o2" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o2_TextChanged" AutoPostBack="true"></asp:TextBox>
									</td>
									<td>
										<asp:Label ID="lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o2" runat="server"   Width="50px"></asp:Label>
									</td>
									<td>
										<asp:Label ID="lbl_CriteriaD_Administration_TotalorFaculty_o2" runat="server" Text="" Font-Bold="true"></asp:Label>
									</td>
									<td>
										<asp:TextBox ID="txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o2" AutoPostBack="true" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o2_TextChanged"></asp:TextBox>
									</td>
									<td>
										<asp:Button ID="fu_CriteriaD_Administration_FileUpload_o2" Text="Uplaod File" OnClick="fu_CriteriaD_Administration_FileUpload_o2_Click" runat="server" Enabled="false"/>
									</td>
								</tr>
								<tr>
									<td>
										<asp:Label ID="lbl_CriteriaD_Administration_o3" runat="server" Text="o.3" Font-Bold="true"></asp:Label>
									</td>
									<td class="left-text">
										<asp:Label ID="lbl_CriteriaD_Administration_o3_Description" runat="server" Text="Organized non-academic events as event coordinator"></asp:Label>
									</td>
									<td>
										<asp:Label ID="lbl_CriteriaD_Administration_MaxAPIScore_o3" runat="server" Text="20" Font-Bold="true"></asp:Label>
									</td>
									<td>
										<asp:Label ID="lbl_CriteriaD_Administration_ScoresPerActivity_o3" runat="server" Text="10" Font-Bold="true"></asp:Label>
									</td>
									<td>
										<asp:TextBox ID="txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o3" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o3_TextChanged" AutoPostBack="true"></asp:TextBox>
									</td>
									<td>
										<asp:Label ID="lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o3" runat="server"   Width="50px"></asp:Label>
									</td>
									<td>
										<asp:Label ID="lbl_CriteriaD_Administration_TotalorFaculty_o3" runat="server" Text="" Font-Bold="true"></asp:Label>
									</td>
									<td>
										<asp:TextBox ID="txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o3" AutoPostBack="true" runat="server"   Width="50px" OnTextChanged="txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o3_TextChanged"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
									</td>
									<td>
										<asp:Button ID="fu_CriteriaD_Administration_FileUpload_o3" Text="Upload File" OnClick="fu_CriteriaD_Administration_FileUpload_o3_Click" runat="server" Enabled="false"/>
									</td>
								</tr>
								<tr>
									<td>
										<asp:Label ID="lbl_CriteriaD_Administration_o4" runat="server" Text="o.4" Font-Bold="true"></asp:Label>
									</td>
									<td class="left-text">
										<asp:Label ID="lbl_CriteriaD_Administration_o4_Description" runat="server" Text="Contribution for the work related to admissions/placements"></asp:Label>
									</td>
									<td>
										<asp:Label ID="lbl_CriteriaD_Administration_MaxAPIScore_o4" runat="server" Text="10" Font-Bold="true"></asp:Label>
									</td>
									<td>
										<asp:Label ID="lbl_CriteriaD_Administration_ScoresPerActivity_o4" runat="server" Text="10" Font-Bold="true"></asp:Label>
									</td>
									<td>
										<asp:TextBox ID="txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o4" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o4_TextChanged" AutoPostBack="true"></asp:TextBox>
									</td>
									<td>
										<asp:Label ID="lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o4" runat="server"   Width="50px"></asp:Label>
									</td>
									<td>
										<asp:Label ID="lbl_CriteriaD_Administration_TotalorFaculty_o4" runat="server" Text="" Font-Bold="true"></asp:Label>
									</td>
									<td>
										<asp:TextBox ID="txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o4" OnTextChanged="txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o4_TextChanged" AutoPostBack="true" runat="server"   Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
									</td>
									<td>
										<asp:Button ID="fu_CriteriaD_Administration_FileUpload_o4" Text="Upload File" OnClick="fu_CriteriaD_Administration_FileUpload_o4_Click" runat="server" Enabled="false"/>
									</td>
								</tr>
								<tr>
									<td>
										<asp:Label ID="lbl_CriteriaD_Administration_o5" runat="server" Text="o.5" Font-Bold="true"></asp:Label>
									</td>
									<td class="left-text">
										<asp:Label ID="lbl_CriteriaD_Administration_o5_Description" runat="server" Text="Contribution in Non academic committee(s) as a member of organizing committee"></asp:Label>
									</td>
									<td>
										<asp:Label ID="lbl_CriteriaD_Administration_MaxAPIScore_o5" runat="server" Text="20" Font-Bold="true"></asp:Label>
									</td>
									<td>
										<asp:Label ID="lbl_CriteriaD_Administration_ScoresPerActivity_o5" runat="server" Text="10" Font-Bold="true"></asp:Label>
									</td>
									<td>
										<asp:TextBox ID="txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o5" runat="server" OnTextChanged="txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o5_TextChanged"   Width="50px"     oninput="validatePositiveNumber(this)"  AutoPostBack="true"></asp:TextBox>
									</td>
									<td>
										<asp:Label ID="lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o5" runat="server"   Width="50px"></asp:Label>
									</td>
									<td>
										<asp:Label ID="lbl_CriteriaD_Administration_TotalorFaculty_o5" runat="server" Text="" Font-Bold="true"></asp:Label>
									</td>
									<td>
										<asp:TextBox ID="txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o5" AutoPostBack="true" runat="server"   OnTextChanged="txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o5_TextChanged" Width="50px"     oninput="validatePositiveNumber(this)" ></asp:TextBox>
									</td>
									<td>
										<asp:Button ID="fu_CriteriaD_Administration_FileUpload_o5" OnClick="fu_CriteriaD_Administration_FileUpload_o5_Click" Text="Upload File" runat="server" Enabled="false"/>
									</td>
								</tr>

								<tr>
							
							<td colspan="2"></td>
							<td class="grey-highlighted-row">
								<asp:Label ID="lbl_maxscore_total_criteriad" runat="server" Text="100" Font-Bold="true"></asp:Label></td>
							<td colspan="2" class="whitebg-row"></td>
							<td class="grey-highlighted-row">
								<asp:Label ID="lbl_api_score_through_self_assessment_total_criteriad" runat="server" Text="" Font-Bold="true"></asp:Label></td>
							<td class="grey-highlighted-row">
								<asp:Label ID="lbl_finally_obtained_score_total_criteriad" runat="server" Text="" Font-Bold="true"></asp:Label></td>
							<td class="grey-highlighted-row">
								<asp:Label ID="lbl_assessmentby_reportingauthority_total_Criteriad" Font-Bold="true" runat="server" Text=""></asp:Label></td>
														
						</tr>




							</table>						
						</td>
					</tr>
				
				
							<tr class="vertical-padding" style="text-align: center;">
						<td align="center">
							<h3 style="text-decoration-line: underline"><b>Criteria-E: Faculty Assessment by Reporting Authority.</h3></b>
							<span style="text-align: right; display: block"><strong>(Max API Score: 50)</strong></span>
						<h4 style="padding-bottom: 20px; margin: 0 auto;">Qualitative Assessment of Individuals by Reporting Authority:</h4>
							
							<table style="text-align:center" border="1">
								<tr>
									<td></td>
									<td></td>
									<td style="text-align:center" class="red-highlighted-row1"><p><strong>Max. API Score</strong></p></td>
									<td style="text-align:center" class="red-highlighted-row1"><p><strong>Score given by the Reporting Authority</strong></p></td>
								</tr>

								<tr>
							<td class="highlighted-row"><asp:Label ID="lbl_CriteriaE_ReportingAuthority_P" runat="server" Text="P)" Font-Bold="true"></asp:Label></td>
							<td class="highlighted-row left-text"><strong>Faculty Assessment Points</strong></td>													
							<td class="highlighted-row"></td>
							<td class="highlighted-row"></td>
						</tr>
								<tr align="center">
									<td>
										<asp:Label ID="lbl_P_p1" runat="server" Text="p.1"></asp:Label></td>
									
									<td class="left-text">
										<asp:Label ID="lbl_p1_description" runat="server" Text="Commitment towards work for the Department /University"></asp:Label></td>
									<td>
										<asp:Label ID="lbl_p1_maxapiscore" runat="server" Text="5"></asp:Label></td>
									<td style="width:50px">
										<asp:TextBox ID="txt_p1_ReportingAuthority"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_p_ReportingAuthority_TextChanged"   Width="50px" AutoPostBack="true" runat="server"></asp:TextBox></td>
								</tr>
								<tr>
									<td>
										<asp:Label ID="lbl_P_p2" runat="server" Text="p.2"></asp:Label>
									</td>
									<td class="left-text">
										<asp:Label ID="lbl_p2_description" runat="server" Text="Relationships with Colleagues & Superiors"></asp:Label>
									</td>
									<td>
										<asp:Label ID="lbl_p2_maxapiscore" runat="server" Text="5"></asp:Label>
									</td>
									<td>
										<asp:TextBox ID="txt_p2_ReportingAuthority"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_p2_ReportingAuthority_TextChanged"   Width="50px" AutoPostBack="true" runat="server"></asp:TextBox>
									</td>
								</tr>
								<tr>
									<td>
										<asp:Label ID="lbl_P_p3" runat="server" Text="p.3"></asp:Label>
									</td>
									<td class="left-text">
										<asp:Label ID="lbl_p3_description" runat="server" Text="Punctuality & Regularity (classroom delivery)"></asp:Label>
									</td>
									<td>
										<asp:Label ID="lbl_p3_maxapiscore" runat="server" Text="5"></asp:Label>
									</td>
									<td>
										<asp:TextBox ID="txt_p3_ReportingAuthority"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_p3_ReportingAuthority_TextChanged"   Width="50px" AutoPostBack="true" runat="server"></asp:TextBox>
									</td>
								</tr>
								<tr>
									<td>
										<asp:Label ID="lbl_P_p4" runat="server" Text="p.4"></asp:Label>
									</td>
									<td class="left-text">
										<asp:Label ID="lbl_p4_description" runat="server" Text="Maturity & Temperament"></asp:Label>
									</td>
									<td>
										<asp:Label ID="lbl_p4_maxapiscore" runat="server" Text="5"></asp:Label>
									</td>
									<td>
										<asp:TextBox ID="txt_p4_ReportingAuthority"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_p4_ReportingAuthority_TextChanged"   Width="50px" AutoPostBack="true" runat="server"></asp:TextBox>
									</td>
								</tr>
								
								<tr>
									<td>
										<asp:Label ID="lbl_P_p5" runat="server" Text="p.5"></asp:Label>
									</td>
									<td class="left-text">
										<asp:Label ID="lbl_p5_description" runat="server" Text="Work Knowledge"></asp:Label>
									</td>
									<td>
										<asp:Label ID="lbl_p5_maxapiscore" runat="server" Text="5"></asp:Label>
									</td>
									<td>
										<asp:TextBox ID="txt_p5_ReportingAuthority"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_p5_ReportingAuthority_TextChanged"   Width="50px" AutoPostBack="true" runat="server"></asp:TextBox>
									</td>
								</tr>
								<tr>
									<td>
										<asp:Label ID="lbl_P_p6" runat="server" Text="p.6"></asp:Label>
									</td>
									<td class="left-text">
										<asp:Label ID="lbl_p6_description" runat="server" Text="Attitude"></asp:Label>
									</td>
									<td>
										<asp:Label ID="lbl_p6_maxapiscore" runat="server" Text="5"></asp:Label>
									</td>
									<td>
										<asp:TextBox ID="txt_p6_ReportingAuthority"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_p6_ReportingAuthority_TextChanged" runat="server"   Width="50px" AutoPostBack="true"></asp:TextBox>
									</td>
								</tr>

								<tr>
									<td>
										<asp:Label ID="lbl_P_p7" runat="server" Text="p.7"></asp:Label>
									</td>
									<td class="left-text">
										<asp:Label ID="lbl_p7_description" runat="server" Text="Ability to deal in difficult situations"></asp:Label>
									</td>
									<td>
										<asp:Label ID="lbl_p7_maxapiscore" runat="server" Text="5"></asp:Label>
									</td>
									<td>
										<asp:TextBox ID="txt_p7_ReportingAuthority"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_p7_ReportingAuthority_TextChanged" runat="server"   Width="50px" AutoPostBack="true"></asp:TextBox>
									</td>
								</tr>

								<tr>
									<td>
										<asp:Label ID="lbl_P_p8" runat="server" Text="p.8"></asp:Label>
									</td>
									<td class="left-text">
										<asp:Label ID="lbl_p8_description" runat="server" Text="Communication Skills"></asp:Label>
									</td>
									<td>
										<asp:Label ID="lbl_p8_maxapiscore" runat="server" Text="5"></asp:Label>
									</td>
									<td>
										<asp:TextBox ID="txt_p8_ReportingAuthority" runat="server"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_p8_ReportingAuthority_TextChanged"   Width="50px" AutoPostBack="true"></asp:TextBox>
									</td>
								</tr>
								<tr>
									<td>
										<asp:Label ID="lbl_P_p9" runat="server" Text="p.9"></asp:Label>
									</td>
									<td class="left-text">
										<asp:Label ID="lbl_p9_description" runat="server" Text="Capability to work in Teams"></asp:Label>
									</td>
									<td>
										<asp:Label ID="lbl_p9_maxapiscore" runat="server" Text="5"></asp:Label>
									</td>
									<td>
										<asp:TextBox ID="txt_p9_ReportingAuthority"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_p9_ReportingAuthority_TextChanged"   Width="50px" AutoPostBack="true" runat="server"></asp:TextBox>
									</td>
								</tr>

								<tr>
									<td>
										<asp:Label ID="lbl_P_p10" runat="server" Text="p.10"></asp:Label>
									</td>
									<td class="left-text">
										<asp:Label ID="lbl_p10_description" runat="server" Text="Leadership Skills"></asp:Label>
									</td>
									<td>
										<asp:Label ID="lbl_p10_maxapiscore" runat="server" Text="5"></asp:Label>
									</td>
									<td>
										<asp:TextBox ID="txt_p10_ReportingAuthority"     oninput="validatePositiveNumber(this)"  OnTextChanged="txt_p10_ReportingAuthority_TextChanged"   Width="50px" AutoPostBack="true" runat="server"></asp:TextBox>
									</td>
								</tr>
								<tr style="text-align:center">
									<td class="whitebg-row"></td>
									<td class="whitebg-row"></td>
									<td class="grey-highlighted-row">
										<asp:Label ID="lbl_p_maxapiscore_total" Font-Bold="true" runat="server" Text="50"></asp:Label></td>
									<td style="padding-right: 43px;" class="grey-highlighted-row">
										<asp:Label ID="lbl_p_ReportingAuthority_total" runat="server" Text=""></asp:Label>
									</td>


								</tr>



								</table>
							</td>
								</tr>

				
					<tr align="center">
						<td>	
							<table>
							<tr class="vertical-padding" style="text-align: center;">
						<td align="center">
							<h3 style="text-decoration-line: underline"><b>Criteria-F: Student’s Feedback.</h3></b>
							<span style="text-align: right; display: block"><strong>(Max API Score: 50)</strong></span>
						<h4 style="padding-bottom: 20px; margin: 0 auto;">Marks criteria of Faculty feedback which was given by Students through ERP as:</h4>
							
						 <table border="1" style="text-align:center; margin:auto;">
            <tr class="grey-highlighted-row">
                <th>Sr. No.</th>
                <th>Faculty Feedback in %</th>
                <th>Marks</th>
            </tr>
            <tr class="k">
                <td><asp:Label ID="lbl_criteriaF_srno1" runat="server" Text="1" Font-Bold="true"></asp:Label></td>
                <td><asp:Label ID="lbl_criteriaF_faculty_srno1" runat="server" Text=">=90%"></asp:Label></td>
                <td><asp:Label ID="lbl_criteriaF_marks_srno1" runat="server" Text="50"></asp:Label></td>
            </tr>

            <tr class="k">
                <td><asp:Label ID="lbl_criteriaF_srno2" runat="server" Text="2" Font-Bold="true"></asp:Label></td>
                <td><asp:Label ID="lbl_criteriaF_faculty_srno2" runat="server" Text="80%-89.9%"></asp:Label></td>
                <td><asp:Label ID="lbl_criteriaF_marks_srno2" runat="server" Text="45"></asp:Label></td>
            </tr>

            <tr class="k">
                <td><asp:Label ID="lbl_criteriaF_srno3" runat="server" Text="3" Font-Bold="true"></asp:Label></td>
                <td><asp:Label ID="lbl_criteriaF_faculty_srno3" runat="server" Text="70%-79.9%"></asp:Label></td>
                <td><asp:Label ID="lbl_criteriaF_marks_srno3" runat="server" Text="40"></asp:Label></td>
            </tr>

            <tr class="k">
                <td><asp:Label ID="lbl_criteriaF_srno4" runat="server" Text="4" Font-Bold="true"></asp:Label></td>
                <td><asp:Label ID="lbl_criteriaF_faculty_srno4" runat="server" Text="60%-69.9%"></asp:Label></td>
                <td><asp:Label ID="lbl_criteriaF_marks_srno4" runat="server" Text="35"></asp:Label></td>
            </tr>

            <tr class="k">
                <td><asp:Label ID="lbl_criteriaF_srno5" runat="server" Text="5" Font-Bold="true"></asp:Label></td>
                <td><asp:Label ID="lbl_criteriaF_faculty_srno5" runat="server" Text="50%-59.9%"></asp:Label></td>
                <td><asp:Label ID="lbl_criteriaF_marks_srno5" runat="server" Text="30"></asp:Label></td>
            </tr>

            <tr class="k">
                <td><asp:Label ID="lbl_criteriaF_srno6" runat="server" Text="6" Font-Bold="true"></asp:Label></td>
                <td><asp:Label ID="lbl_criteriaF_faculty_srno6" runat="server" Text="40%-49.9%"></asp:Label></td>
                <td><asp:Label ID="lbl_criteriaF_marks_srno6" runat="server" Text="25"></asp:Label></td>
            </tr>

            <tr class="k">
                <td><asp:Label ID="lbl_criteriaF_srno7" runat="server" Text="7" Font-Bold="true"></asp:Label></td>
                <td><asp:Label ID="lbl_criteriaF_faculty_srno7" runat="server" Text="30%-39.9%"></asp:Label></td>
                <td><asp:Label ID="lbl_criteriaF_marks_srno7" runat="server" Text="20"></asp:Label></td>
            </tr>

             <tr class="k">
                <td><asp:Label ID="lbl_criteriaF_srno8" runat="server" Text="8" Font-Bold="true"></asp:Label></td>
                <td><asp:Label ID="lbl_criteriaF_faculty_srno8" runat="server" Text="20%-29.9%"></asp:Label></td>
                <td><asp:Label ID="lbl_criteriaF_marks_srno8" runat="server" Text="15"></asp:Label></td>
            </tr>

             <tr class="k">
                <td><asp:Label ID="lbl_criteriaF_srno9" runat="server" Text="9" Font-Bold="true"></asp:Label></td>
                <td><asp:Label ID="lbl_criteriaF_faculty_srno9" runat="server" Text="10%-19.9%"></asp:Label></td>
                <td><asp:Label ID="lbl_criteriaF_marks_srno9" runat="server" Text="10"></asp:Label></td>
            </tr>

             <tr class="k">
                <td><asp:Label ID="lbl_criteriaF_srno10" runat="server" Text="10" Font-Bold="true"></asp:Label></td>
                <td><asp:Label ID="lbl_criteriaF_faculty_srno10" runat="server" Text="0-9.9%"></asp:Label></td>
                <td><asp:Label ID="lbl_criteriaF_marks_srno10" runat="server" Text="5"></asp:Label></td>
            </tr>
        </table>		
							
							</td>
							</tr>	
								<tr>
									<td style="padding-top:10px">
										<b>Faculty Feedback of Odd Sem in %=<asp:Label ID="lbl_ff_odd_sem" runat="server" Text="....."></asp:Label></b>
									</td>
								</tr>
								
								<tr>
									<td style="padding-top:20px">
										<b>Faculty Feedback of Even Sem in %=<asp:Label ID="lbl_ff_even_sem" runat="server" Text="......"></asp:Label></b>
									</td>
								</tr>
								<tr>
									<td class="vertical-padding"><b><span>Average Faculty Feedback in % = </span></b> <asp:Label ID="lbl_facultyfeedback_inpercentage" runat="server" Text=""></asp:Label></td>
                                    <caption>
                                        <br />
                                    </caption>
									</tr>
									<tr class="vertical-padding">	
									<td><b><span>Faculty Obtained Marks Out Of 50 = </span></b><asp:Label ID="lbl_facultyobtained_total" runat="server" Text=""></asp:Label></td>
							
									
								</tr>
			</table>
				</td>
				</tr>



				<tr align="center">
					<td>
						<table>
					<tr class="vertical-padding" style="text-align: center;">
						<td align="center">
							<h3 style="text-decoration-line: underline"><b>API Score Calculation</h3></b>
							<span style="text-align: center; display: block; padding-bottom:20px"><strong>(Maximum API Score: 750)</strong></span>
						</td></tr>
							
	<tr> <td>  

							<table>
								<tr>
									<th class="vertical-padding" style="text-decoration:underline">API Score Through Self-Assessment By Faculty</th>
								</tr>
								<tr>
									<td><b>Criteria-A (Academic Performance):</b></td>
									<td>
										<b><asp:Label ID="lbl_apiscorecalculation_criteriaA" runat="server" Text=""></asp:Label></b></td>
								</tr>
								<tr>
									<td><b>Criteria-B (Research & Development):</b></td>
									<td>
									 <b>  <asp:Label ID="lbl_apiscorecalculation_criteriaB" runat="server" Text=""></asp:Label></b>
									</td>
								</tr>
								<tr>
									<td><b>Criteria-C (Professional & Personal Competency):</b></td>
									<td>
									  <b>  <asp:Label ID="lbl_apiscorecalculation_criteriaC" runat="server" Text=""></asp:Label></b>
									</td>
								</tr>

								<tr>
									<td><b>Criteria-D (Administration):</b></td>
									<td>
									<b>	<asp:Label ID="lbl_apiscorecalculation_criteriaD" runat="server" Text=""></asp:Label></b>
									</td>
								</tr>

								<tr><td style="text-decoration:underline" class="vertical-padding"><b>API Score through Reporting Authority</b></td></tr>

								<tr>
									<td><b>Criteria-E (Faculty Assessment):</b></td>
									<td>
										<b><asp:Label ID="lbl_apiscorecalculation_criteriaE" runat="server" Text=""></asp:Label></b>
									</td>
								</tr>

								<tr>
									<td><b>Criteria-F (Student’s Feedback):</b></td>
									<td>
									<b>	<asp:Label ID="lbl_apiscorecalculation_criteriaF" runat="server" Text=""></asp:Label> </b>
									</td>
								</tr>
                                
                                <tr> <td style="height:10px" colspan="2"> </td></tr>
								<tr>
									<td style="border-top: 1px solid black; border-bottom: 1px solid black;" colspan="2"></td></tr>
                                
                                <tr> <td style="height:10px" colspan="2"> </td></tr>
								<tr>
									<td>
										<b>Total obtained API Score:</b></td>
										<td> <b>  <asp:Label ID="lbl_totalAPIScore" runat="server" Text=""></asp:Label></b></td>
									
								</tr>

                                <tr> <td style="height:10px" colspan="2"> </td></tr>
                                <tr>
									<td style="border-top: 1px solid black; border-bottom: 1px solid black;" colspan="2"></td></tr>

                                
                                <tr> <td style="height:10px" colspan="2"> </td></tr>
								<tr>
									<td><b>Faculty category based on API Score:<br /> (Average, Good, Excellent)</b></td>
									<td>
										<asp:Label ID="lbl_facultyCategory" runat="server" Text=""></asp:Label>
									</td>
								</tr>

								
                                <tr> <td style="height:10px" colspan="2"> </td></tr>

							</table>
        </td></tr>
							<tr align="center">
									<td>
										<h3 class="vertical-padding">Comments & suggestions after review of individual progress by Reporting Authority</h3>
										<table>
											<tr>
												<td class="vertical-padding"><span><b>a) </b></span>
												</td>
                                                <td> <asp:TextBox ID="txt_commentsAndsuggestion_a" MaxLength="250" Width="800px" runat="server"></asp:TextBox> </td>
											</tr>
											
											<tr>
												<td class="vertical-padding"><span><b>b) </b></span>
												</td>
                                                <td> <asp:TextBox ID="txt_commentsAndsuggestion_b" MaxLength="250" runat="server" Width="800px"></asp:TextBox></td>
											</tr>
											
											<tr>
												<td class="vertical-padding">
												<span><b>c) </b></span>
												</td>
                                                <td> <asp:TextBox ID="txt_commentsAndsuggestion_c" MaxLength="250" runat="server" Width="800px"></asp:TextBox></td>
											</tr>
											
											<tr>
												<td class="vertical-padding">
												<span><b>d) </b></span>
												</td>
                                                <td> <asp:TextBox ID="txt_commentsAndsuggestion_d" MaxLength="250" runat="server" Width="800px"></asp:TextBox> </td>
											</tr>
											
											<tr>
												<td class="vertical-padding">
												<span><b>e) </b></span>
												</td>
                                                <td> <asp:TextBox ID="txt_commentsAndsuggestion_e" MaxLength="250" runat="server" Width="800px"></asp:TextBox></td>
											</tr>
										</table>
									</td>
								</tr>
				
									<tr class="vertical-padding">
						<td align="center">
							<h3 style="text-decoration-line: underline">HR Department</h3>
						</td></tr>
									<table style="margin:0 auto">
										<tr>
											<td class="vertical-padding"><b>Total Obtained API Score:</b></td>
											<td class="vertical-padding">
												<asp:Label ID="lbl_hrdepartment_totalapiscore" Font-Bold="true" runat="server" Text=""></asp:Label> <b>/</b> <asp:Label ID="lbl_hrdepartment_totalapi_mAX_score" Font-Bold="true" runat="server" Text="">750</asp:Label>  </td>
										
										</tr>
										<tr>
											<td class="vertical-padding">
												<b>Faculty category based on API Score:<br /> (Average, Good, Excellent)</b>
											</td>
											<td class="vertical-padding">
												<asp:Label ID="lbl_hrdepartment_faculty_categorybasedscore" runat="server" Text=""></asp:Label></td>
										
										</tr>

                                        <tr> <td>  <b>Required Improvement in the categories<br />(Academic Performance/ Research Development/<br />Professional & Personal Competency/ Administration):</b></td>
                                            
                                             <td> <table> 
                                                 <tr> <td>   1. </td><td><asp:Label ID="txt_hrdepartment_required_improvement_1" MaxLength="150" runat="server"></asp:Label> </td></tr>
                                                 <tr> <td>   2. </td><td><asp:Label ID="txt_hrdepartment_required_improvement_2" MaxLength="150" runat="server"></asp:Label> </td></tr>
                                                 <tr> <td>   3. </td><td><asp:Label ID="txt_hrdepartment_required_improvement_3" MaxLength="150" runat="server"></asp:Label> </td></tr>
                                                 <tr> <td>   4. </td><td><asp:Label ID="txt_hrdepartment_required_improvement_4" MaxLength="150" runat="server"></asp:Label> </td></tr>

                                                  </table> </td> </tr>

										
									</table>
									<tr align="center">
									<td>
										<h3 class="vertical-padding">HR Recommendations</h3>
										<table>
											<tr>
												<td class="vertical-padding"><span><b>a) </b></span></td>
											 <td>	<asp:TextBox ID="txt_hr_recommendations_a" MaxLength="250" Width="800px" runat="server"></asp:TextBox></td>
											</tr>
											
											<tr>
												<td class="vertical-padding"><span><b>b) </b></span></td>
											 <td>
												<asp:TextBox ID="txt_hr_recommendations_b" MaxLength="250" runat="server" Width="800px"></asp:TextBox></td>
											</tr>
											
											<tr>
												<td class="vertical-padding">
												<span><b>c) </b></span></td>
											 <td>
												<asp:TextBox ID="txt_hr_recommendations_c" MaxLength="250" runat="server" Width="800px"></asp:TextBox></td>
											</tr>
											
											<tr>
												<td class="vertical-padding">
												<span><b>d) </b></span></td>
											 <td>
												<asp:TextBox ID="txt_hr_recommendations_d" MaxLength="250" runat="server" Width="800px"></asp:TextBox></td>
											</tr>
											
											<tr>
												<td class="vertical-padding">

												<span><b>e) </b></span>
                                                    </td>
											 <td>
												<asp:TextBox ID="txt_hr_recommendations_e" MaxLength="250" runat="server" Width="800px"></asp:TextBox></td>
											</tr>

                                            	

										</table>
									</td>
								</tr>
								<tr >
								<th><p style="text-align:center"> Approval & Comments By Vice Chancellor:</p> </th>
								
							</tr>
													<tr align="center">
                <td class="vertical-padding" colspan="2">
                    …………………………………………………………………………………………………………………………….  
                    <br />
                    …………………………………………………………………………………………………………………………….
                </td>
            </tr>
<tr align="center" style="padding-top: 50px; text-align: center;">
			   <th style="text-align: center;">
				   <b>(Vice Chancellor)<br />
					   Signature</b>
			   </th>
		   </tr>
				</table>
					

			</td>
            </tr>

               <tr>
				   <td>
					   <table style="width:100%">
						   <tr>
							   <td class="vertical-margin vertical-padding" style="text-align:center">
								   <asp:Button ID="Btn_Save" OnClientClick="return confirmAction();" OnClick="Btn_Save_Click1" runat="server" Text="Save" CssClass="button success" />							   
								   <asp:Button ID="Btn_Approval" OnClientClick="return confirmAction();" runat="server" Text="Send For Approval" Visible="false" CssClass="button danger" OnClick="Btn_Approval_Click" />
                                    <asp:Button ID="btn_Print" runat="server" Text="Print" CssClass="button secondary" OnClick="btn_Print_Click" />
							   </td>
						   </tr>
					   </table>
				   </td>
               </tr>

          
            
			</table> 
			
    

					
                </asp:Panel>
			<%--main table end--%>
            <%--               </div>--%>

            </td>
				</tr>


			</table>		

       

              
			</ContentTemplate>
        <Triggers>
            
            <asp:PostBackTrigger  ControlID="btnCreateNew"/>
                        <asp:PostBackTrigger  ControlID="grd_Data"/>


   <asp:PostBackTrigger  ControlID="btn_Print"/>
   <asp:PostBackTrigger  ControlID="lnk_Total_PMS_Filled"/>
           <asp:PostBackTrigger  ControlID="lnk_Total_PMS_Pending"/>
           <asp:PostBackTrigger  ControlID="btn_Print"/>
           

                     
        </Triggers>
		</asp:UpdatePanel>
    
            
      <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none" />
   
    <asp:ModalPopupExtender ID="md_FileUpload" runat="server" PopupControlID="pnlPopup" TargetControlID="Button2" BackgroundCssClass="modalBackground"   OkControlID="btOK">
</asp:ModalPopupExtender>

<asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
    
            <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
		<ContentTemplate>--%>
     <div class="header">
        File Upload
    </div>
<div class="body" align="center" style="height:800px; overflow:scroll">
      
                  <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                    <ContentTemplate>
                       
                        Applicable For :  <asp:Label ID="lblApplicablefor" runat="server" Text="Applicable For"></asp:Label>				
                        <br />
                       


                        
   <asp:HiddenField ID="hfApplication" runat="server" ></asp:HiddenField>
                    <div id="F_1" runat="server" visible="false">


                                <div class="col-sm-3 p-0" style="margin-top:5px">

                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp Type Of Research Incentive</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txttitlePaper" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="drpResearchIncentive" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="drpResearchIncentive_SelectedIndexChanged" Width="200px"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 p-0">
                                    <div class="form-group clearfix" style="text-align:left">
                                       <asp:Label ID="lblTitlepaper" runat="server" class="col-form-label" Text="Title of the Paper" Width="200px" Style="font-size: small; margin-left: 20px"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txttitlePaper" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txttitlePaper" runat="server" CssClass="form-control" Width="200px"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 p-0">
                                    <div class="form-group clearfix">
                                        <asp:Label ID="lNameOfJournal" runat="server" class="col-form-label" Text="Name of the Journal" Style="font-size: small; margin-left: 20px"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtNameofJournal" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtNameofJournal" runat="server" CssClass="form-control" Width="200px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-1 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Volume</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtVolume" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtVolume" runat="server" CssClass="form-control" Width="70px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-2 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Issue</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtNoIssue" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtNoIssue" runat="server" CssClass="form-control" Width="100px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 100px"> Page No. </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtPageNop" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtPageNop" runat="server" CssClass="form-control" Width="200px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 p-0">
                                    <div class="form-group clearfix">
                                        <asp:Label ID="lblISBN" runat="server" class="col-form-label" Text="ISSN no." Width="200px" Style="font-size: small; margin-left: -50px">  </asp:Label>

                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtISSNno" runat="server" autocomplete="off" CssClass="form-control" Width="200px"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp  Date of Publication </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtDOP" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtDOP" runat="server" CssClass="form-control" Width="200px"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDOP" Format="dd-MM-yyyy"></asp:CalendarExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp Total No. of Author(s)  </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtNumberOfAuthor" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtNumberOfAuthor" runat="server" CssClass="form-control" Width="200px" onkeypress="return isNumberKey(event)">
                                            </asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small;text-align:left;margin-left:15px;  width: 250px"> Link of Article   </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtLOA" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtLOA" runat="server" CssClass="form-control" Width="400px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small;visibility:hidden">&nbsp&nbsp&nbsp Total No. of Author(s)  </label>
                                       
                                        <div class="col-sm-8">
                                           <asp:FileUpload ID="btn_Fu_A1" runat="server" />

                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-2 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small;visibility:hidden">&nbsp&nbsp&nbsp Total No. of Author(s)  </label>
                                       
                                        <div class="col-sm-8">
                                       <asp:Button ID="btnAttachmentSave_Fu_A1" runat="server" Text="Save Data" OnClick="btnAttachmentSave_Fu_A1_Click" CssClass="button success"/>

                                        </div>
                                    </div>
                                </div>
                             <table style="width:100%"> <tr> 

		 <td align="center">  
                            
			 <div style="overflow:scroll;height:450px;width:950px">
                              <asp:GridView ID="gv_a1" runat="server"   AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                                       <Columns>
         <asp:TemplateField HeaderText="Type" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" >
            <ItemTemplate>
                <asp:Label ID="lblType" runat="server" Text='<%#Bind("Type_Of_research") %>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Title" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" >
            <ItemTemplate>
                <asp:Label ID="lblTitle" runat="server" Text='<%#Bind("Title") %>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Journal" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" >
            <ItemTemplate>
                <asp:Label ID="lblAppNo" runat="server" Text='<%#Bind("ApplicationNo") %>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Volume" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" >
            <ItemTemplate>
                <asp:Label ID="lblvolume" runat="server" Text='<%#Bind("volume") %>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Issue" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" >
            <ItemTemplate>
                <asp:Label ID="lblIssue" runat="server" Text='<%#Bind("Issue") %>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Page No." ItemStyle-CssClass="column" HeaderStyle-CssClass="column" >
            <ItemTemplate>
                <asp:Label ID="lblPage" runat="server" Text='<%#Bind("PageNo") %>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="ISSN No." ItemStyle-CssClass="column" HeaderStyle-CssClass="column" >
            <ItemTemplate>
                <asp:Label ID="lblISSNNo" runat="server" Text='<%#Bind("ISSNNo") %>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Publication Date" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" >
            <ItemTemplate>
                <asp:Label ID="lblDOP" runat="server" Text='<%#Bind("DOP") %>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Authors" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" >
            <ItemTemplate>
                <asp:Label ID="lblAuthors" runat="server" Text='<%#Bind("No_of_auth") %>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Article" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" >
            <ItemTemplate>
                <asp:Label ID="lblArticle" runat="server" Text='<%#Bind("ArticleLink") %>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="lnk_A1_Downalod" runat="server" CommandArgument='<%#Bind("AutoNo") %>' OnCommand="lnk_A1_Downalod_Command">View / Download</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField Visible="false">
            <ItemTemplate >
                <asp:LinkButton ID="lnk_A1_Delete" runat="server" CommandArgument='<%#Bind("AutoNo") %>' OnCommand="lnk_A1_Delete_Command" OnClientClick="return confirmDelete();">Delete</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>
                                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                        <SortedDescendingHeaderStyle BackColor="#242121" />
</asp:GridView> 
				 </div>
				 </td></tr> </table>
    <br />
	</div>

		           <div id="F_2" runat="server" visible="false" class="mb-3 p-3"
     style="max-width:900px; margin:auto; border:1px solid #ccc; border-radius:5px; background-color:#f9f9f9;">
			    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>

    <div class="form-row mb-3">
        <div class="col-md-6 mb-2">
            <label for="txtTitle" class="form-label">Title</label>
            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" placeholder="Enter Title" />
        </div>
        <div class="col-md-6 mb-2">
            <label for="txtMagazine" class="form-label">Magazine Name</label>
            <asp:TextBox ID="txtMagazine" runat="server" CssClass="form-control" placeholder="Enter Magazine Name" />
        </div>
    </div>

    <!-- Row 2: URL & Publish Date -->
    <div class="form-row mb-3">
        <div class="col-md-6 mb-2">
            <label for="txtUrl" class="form-label">URL</label>
            <asp:TextBox ID="txtUrl" runat="server" CssClass="form-control" placeholder="Enter URL" />
        </div>
        <div class="col-md-6 mb-2">
            <label for="txtDate" class="form-label">Publish Date</label>
            <asp:TextBox ID="txtDate" runat="server" TextMode="Date" CssClass="form-control" />
        </div>
    </div>


    <!-- Row 4: Activity Type & File Upload -->

        <!-- Your full form -->
        <asp:FileUpload ID="fileUpload" runat="server" />

       

    
    

   
    <div class="form-group mb-3">
        <label for="txtRemark" class="form-label"></label>
        <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Enter Remark" />
    </div>

    
    <div class="text-center mb-4">
		<asp:Label ID="lblerror" runat="server" ForeColor="Red" ></asp:Label>
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success px-4"
            OnClick="btnSave_Click" />
    </div>
		    </ContentTemplate>

    <Triggers>
       
        <asp:PostBackTrigger ControlID="btnSave" />
    </Triggers>
</asp:UpdatePanel>
    <hr />

    <!-- Records Grid -->
    <h5 class="mb-2">Records</h5>
    <div style="overflow:auto; max-height:350px;">
        <asp:GridView ID="gvData" runat="server"
            AutoGenerateColumns="False"
            OnRowCommand="gvData_RowCommand"
            CssClass="table table-bordered table-striped table-hover"
            Width="100%"
            GridLines="None">

            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" ItemStyle-Width="50px" />
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:BoundField DataField="MagazineName" HeaderText="Magazine" />

                <asp:TemplateField HeaderText="Download" ItemStyle-Width="120px">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDownload" runat="server"
                            CommandName="Download"
                            CommandArgument='<%# Eval("Id") %>'
                            CssClass="btn btn-primary btn-sm w-100">Download</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

              <%--  <asp:TemplateField HeaderText="Delete" ItemStyle-Width="120px">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server"
                            CommandName="DeleteRow"
                            CommandArgument='<%# Eval("Id") %>'
                            OnClientClick="return confirm('Are you sure you want to delete?');"
                            CssClass="btn btn-danger btn-sm w-100">Delete</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>

            <HeaderStyle BackColor="#343a40" Font-Bold="true" ForeColor="White" />
            <RowStyle BackColor="#f8f9fa" />
            <AlternatingRowStyle BackColor="#e9ecef" />
        </asp:GridView>
    </div>
</div>

                                
                   <div id="F_3" runat="server" visible="true"
     class="mb-3 p-4"
     style="max-width:850px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelF3" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">F-3 Book Chapter Upload</h5>

            <!-- Row 1 -->
            <div class="row mb-3">
                <div class="col-md-6">
                    <label>Level</label>
                    <asp:DropDownList ID="ddlLevel" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select Level" Value="" />
                        <asp:ListItem Text="National" Value="National" />
                        <asp:ListItem Text="International" Value="International" />
                        <asp:ListItem Text="State" Value="State" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Category</label>
                    <asp:DropDownList ID="ddlCategoryF3" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Book Chapter" Value="Book Chapter" />
                    </asp:DropDownList>
                </div>
            </div>

            <!-- File Upload -->
            <div class="mb-3">
                <label>Upload File</label>
                <asp:FileUpload ID="fileUploadF3" runat="server" CssClass="form-control" />
                <small class="text-muted">PDF/JPG/PNG (Max 200KB)</small>
            </div>

            <!-- Remark -->
            <div class="mb-3">
                <label>Remark</label>
                <asp:TextBox ID="txtRemarkF3" runat="server"
                    CssClass="form-control" TextMode="MultiLine" Rows="3" />
            </div>

            <!-- Error -->
            <asp:Label ID="lblErrorF3" runat="server" ForeColor="Red"></asp:Label>

            <!-- Button -->
            <div class="text-center mt-3">
                <asp:Button ID="btnSaveF3" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveF3_Click" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveF3" />
			 <asp:PostBackTrigger ControlID="gvF3" />
			
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <!-- Grid -->
    <asp:GridView ID="gvF3" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvF3_RowCommand">

        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" />
            <asp:BoundField DataField="Level" HeaderText="Level" />
            <asp:BoundField DataField="Category" HeaderText="Category" />
            <asp:BoundField DataField="Remark" HeaderText="Remark" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownload" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("Id") %>'
                        CssClass="btn btn-primary btn-sm">
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

           <%-- <asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDelete" runat="server"
                        CommandName="DeleteRow"
                        CommandArgument='<%# Eval("Id") %>'
                        OnClientClick="return confirm('Are you sure?');"
                        CssClass="btn btn-danger btn-sm">
                        Delete
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>--%>
        </Columns>

    </asp:GridView>

</div>
					
					<div id="F_4" runat="server" visible="true"
     class="mb-3 p-4"
     style="max-width:850px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelF4" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">F-4 Book / Edited Book Upload</h5>

            <!-- Row 1 -->
            <div class="row mb-3">

                <!-- Book Type -->
                <div class="col-md-6">
                    <label>Book Type</label>
                    <asp:DropDownList ID="ddlBookType" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select Book Type" Value="" />
                        <asp:ListItem Text="Book" Value="Book" />
                        <asp:ListItem Text="Edited Book" Value="Edited Book" />
                    </asp:DropDownList>
                </div>

                <!-- Level -->
                <div class="col-md-6">
                    <label>Level</label>
                    <asp:DropDownList ID="ddlLevelF4" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select Level" Value="" />
                        <asp:ListItem Text="National" Value="National" />
                        <asp:ListItem Text="International" Value="International" />
                        <asp:ListItem Text="State" Value="State" />
                    </asp:DropDownList>
                </div>

            </div>

            <!-- File Upload -->
            <div class="mb-3">
                <label>Upload File</label>
                <asp:FileUpload ID="fileUploadF4" runat="server" CssClass="form-control" />
                <small class="text-muted">PDF/JPG/PNG (Max 200KB)</small>
            </div>

            <!-- Remark -->
            <div class="mb-3">
                <label>Remark</label>
                <asp:TextBox ID="txtRemarkF4" runat="server"
                    CssClass="form-control"
                    TextMode="MultiLine" Rows="3" />
            </div>

            <!-- Error -->
            <asp:Label ID="lblErrorF4" runat="server" ForeColor="Red"></asp:Label>

            <!-- Button -->
            <div class="text-center mt-3">
                <asp:Button ID="btnSaveF4" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveF4_Click" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveF4" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <!-- Grid -->
    <asp:GridView ID="gvF4" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvF4_RowCommand">

        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" />
            <asp:BoundField DataField="BookType" HeaderText="Book Type" />
            <asp:BoundField DataField="Category" HeaderText="Level" />
            <asp:BoundField DataField="Remark" HeaderText="Remark" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownload" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("Id") %>'
                        CssClass="btn btn-primary btn-sm">
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

           <%-- <asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDelete" runat="server"
                        CommandName="DeleteRow"
                        CommandArgument='<%# Eval("Id") %>'
                        OnClientClick="return confirm('Are you sure?');"
                        CssClass="btn btn-danger btn-sm">
                        Delete
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>--%>
        </Columns>

    </asp:GridView>

</div>

                     


			<div id="F_5" runat="server" visible="true"
     class="mb-3 p-4"
     style="max-width:850px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelF5" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">F-5 Book / Edited Book</h5>

            <!-- 🔹 Row -->
            <div class="row mb-6">

                <!-- Book Type -->
                <div class="col-md-6">
                    <label class="form-label">Book Type</label>
                    <asp:DropDownList ID="ddlBookTypeF5" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Book" Value="Book" />
                        <asp:ListItem Text="Edited Book" Value="Edited Book" />
                    </asp:DropDownList>
                </div>

                <!-- Category -->
                <div class="col-md-6">
                    <label class="form-label">Category</label>
                    <asp:DropDownList ID="ddlLevelF5" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="National" Value="National" />
                        <asp:ListItem Text="International" Value="International" />
                        <asp:ListItem Text="State" Value="State" />
                    </asp:DropDownList>
                </div>

                <!-- File Upload -->
                <div class="col-md-6">
                    <label class="form-label">Upload File</label>
                    <asp:FileUpload ID="fileUploadF5" runat="server" CssClass="form-control" />
                    <small class="text-muted">PDF/JPG/PNG (Max 200KB)</small>
                </div>

                <!-- Remark -->
                <div class="col-md-12">
                    <label class="form-label">Remark</label>
                    <asp:TextBox ID="txtRemarkF5" runat="server" TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <!-- 🔹 Button + Error -->
            <div class="row mb-3">
                <div class="col-md-12 text-center">

                    <asp:Button ID="btnSaveF5" runat="server"
                        Text="Save"
                        CssClass="btn btn-success px-4"
                        OnClick="btnSaveF5_Click" />

                    <br />

                    <asp:Label ID="lblErrorF5" runat="server" ForeColor="Red" />

                </div>
            </div>

        </ContentTemplate>

        <Triggers>
          
            <asp:PostBackTrigger ControlID="btnSaveF5" />
			 <asp:PostBackTrigger ControlID="gvF5" />
        </Triggers>

    </asp:UpdatePanel>

    <hr />

    <!-- 🔹 Grid -->
    <h5 class="mb-3 text-secondary">Records</h5>

    <div style="overflow:auto; max-height:350px;">

        <asp:GridView ID="gvF5" runat="server"
            AutoGenerateColumns="False"
            CssClass="table table-bordered table-striped table-hover"
            Width="100%"
            OnRowCommand="gvF5_RowCommand">

            <Columns>

                <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-Width="50px" />

                <asp:BoundField DataField="BookType" HeaderText="Book Type" />

                <asp:BoundField DataField="Category" HeaderText="Category" />

                <asp:BoundField DataField="Remark" HeaderText="Remark" />

                <asp:BoundField DataField="CreatedOn" HeaderText="Date" 
                    DataFormatString="{0:dd-MM-yyyy}" />


                <asp:TemplateField HeaderText="Download" ItemStyle-Width="120px">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDownload" runat="server"
                            CommandName="Download"
                            CommandArgument='<%# Eval("ID") %>'
                            CssClass="btn btn-primary btn-sm w-100">
                            Download
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

              
               

            </Columns>

           
            <HeaderStyle BackColor="#343a40" ForeColor="White" Font-Bold="true" />
            <RowStyle BackColor="#ffffff" />
            <AlternatingRowStyle BackColor="#f2f2f2" />

        </asp:GridView>

    </div>

</div>


					<div id="F_6" runat="server" visible="true"
     class="mb-3 p-4"
     style="max-width:850px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelF6" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">F-6 Book / Edited Book</h5>

            <div class="row mb-6">

                <div class="col-md-6">
                    <label>Book Type</label>
                    <asp:DropDownList ID="ddlBookTypeF6" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Book" Value="Book" />
                        <asp:ListItem Text="Edited Book" Value="Edited Book" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Category</label>
                    <asp:DropDownList ID="ddlLevelF6" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="National" Value="National" />
                        <asp:ListItem Text="International" Value="International" />
                        <asp:ListItem Text="State" Value="State" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUploadF6" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemarkF6" runat="server" TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSaveF6" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveF6_Click" />

                <br />

                <asp:Label ID="lblErrorF6" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveF6" />
			 <asp:PostBackTrigger ControlID="gvF6" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <asp:GridView ID="gvF6" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvF6_RowCommand">

        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="BookType" HeaderText="Book Type" />
            <asp:BoundField DataField="Category" HeaderText="Category" />
            <asp:BoundField DataField="Remark" HeaderText="Remark" />
            <asp:BoundField DataField="CreatedOn" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownload" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

           <%-- <asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDelete" runat="server"
                        CommandName="DeleteRow"
                        CommandArgument='<%# Eval("ID") %>'
                        OnClientClick="return confirm('Are you sure?');">
                        Delete
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>--%>

        </Columns>
    </asp:GridView>

</div>




					<div id="F_7" runat="server" visible="true"
     class="mb-3 p-4"
     style="max-width:850px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelF7" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">F-7 Book / Edited Book</h5>

            <div class="row mb-6">

                <div class="col-md-6">
                    <label>Book Type</label>
                    <asp:DropDownList ID="ddlBookType_F7" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Book" Value="Book" />
                        <asp:ListItem Text="Edited Book" Value="Edited Book" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Category</label>
                    <asp:DropDownList ID="ddlCategory_F7" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="National" Value="National" />
                        <asp:ListItem Text="International" Value="International" />
                        <asp:ListItem Text="State" Value="State" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUpload_F7" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemark_F7" runat="server" TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSave_F7" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSave_F7_Click" />

                <br />

                <asp:Label ID="lblErrorF7" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave_F7" />
            <asp:PostBackTrigger ControlID="gvF7" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <asp:GridView ID="gvF7" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvF7_RowCommand">

        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="BookType" HeaderText="Book Type" />
            <asp:BoundField DataField="Category" HeaderText="Category" />
            <asp:BoundField DataField="Remark" HeaderText="Remark" />
            <asp:BoundField DataField="CreatedOn" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownload" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>

</div>
				
                   <div id="h_1" runat="server" visible="false"
    class="mb-3 p-4"
    style="max-width:850px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelH1" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">H-1 Patent Details</h5>

            <div class="row mb-3">

                <div class="col-md-6">
                    <label>National / International / State</label>
                    <asp:DropDownList ID="ddlCategoryH1" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="National" Value="National" />
                        <asp:ListItem Text="International" Value="International" />
                        <asp:ListItem Text="State" Value="State" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Patent Number / Application No</label>
                    <asp:TextBox ID="txtPatentNoH1" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Title</label>
                    <asp:TextBox ID="txtTitleH1" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Patent Category</label>
                    <asp:DropDownList ID="ddlPatentCategoryH1" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="NA" Value="NA" />
                        <asp:ListItem Text="Design" Value="Design" />
                        <asp:ListItem Text="Utility" Value="Utility" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Date of Award</label>
                    <asp:TextBox ID="txtDateH1" runat="server" CssClass="form-control" TextMode="Date" />
                </div>

                <div class="col-md-6">
                    <label>Patent Awarding Agency</label>
                    <asp:TextBox ID="txtAgencyH1" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>URL</label>
                    <asp:TextBox ID="txtURLH1" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Name of all inventors</label>
                    <asp:TextBox ID="txtInventorsH1" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUploadH1" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemarkH1" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSaveH1" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveH1_Click" />

                <br />

                <asp:Label ID="lblErrorH1" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveH1" />
            <asp:PostBackTrigger ControlID="gvH1" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <!-- GRIDVIEW -->
    <asp:GridView ID="gvH1" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvH1_RowCommand">

        <Columns>

            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="Category" HeaderText="Category" />
            <asp:BoundField DataField="PatentNumber_ApplicationNo" HeaderText="Patent No" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="PatentCategory" HeaderText="Patent Category" />
            <asp:BoundField DataField="DateOfAward" HeaderText="Date"
                DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField DataField="PatentAwardingAgency" HeaderText="Agency" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownloadH1" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

</div>
					<div id="h_2" runat="server" visible="false"
    class="mb-3 p-4"
    style="max-width:850px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelH2" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">H-2 Patent Details</h5>

            <div class="row mb-3">

                <div class="col-md-6">
                    <label>National / International / State</label>
                    <asp:DropDownList ID="ddlCategoryH2" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="National" Value="National" />
                        <asp:ListItem Text="International" Value="International" />
                        <asp:ListItem Text="State" Value="State" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Patent Number / Application No</label>
                    <asp:TextBox ID="txtPatentNoH2" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Title</label>
                    <asp:TextBox ID="txtTitleH2" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Patent Category</label>
                    <asp:DropDownList ID="ddlPatentCategoryH2" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="NA" Value="NA" />
                        <asp:ListItem Text="Design" Value="Design" />
                        <asp:ListItem Text="Utility" Value="Utility" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Date of Award</label>
                    <asp:TextBox ID="txtDateH2" runat="server" CssClass="form-control" TextMode="Date" />
                </div>

                <div class="col-md-6">
                    <label>Patent Awarding Agency</label>
                    <asp:TextBox ID="txtAgencyH2" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>URL</label>
                    <asp:TextBox ID="txtURLH2" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Name of all inventors</label>
                    <asp:TextBox ID="txtInventorsH2" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUploadH2" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemarkH2" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSaveH2" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveH2_Click" />

                <br />

                <asp:Label ID="lblErrorH2" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveH2" />
            <asp:PostBackTrigger ControlID="gvH2" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <!-- GRIDVIEW -->
    <asp:GridView ID="gvH2" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvH2_RowCommand">

        <Columns>

            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="Category" HeaderText="Category" />
            <asp:BoundField DataField="PatentNumber_ApplicationNo" HeaderText="Patent No" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="PatentCategory" HeaderText="Patent Category" />
            <asp:BoundField DataField="DateOfAward" HeaderText="Date"
                DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField DataField="PatentAwardingAgency" HeaderText="Agency" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownloadH2" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

</div>
					<div id="h_3" runat="server" visible="false"
    class="mb-3 p-4"
    style="max-width:850px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelH3" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">H-3 Project / Funding Details</h5>

            <div class="row mb-3">

                <div class="col-md-6">
                    <label>Government / Non-Government</label>
                    <asp:DropDownList ID="ddlFundingTypeH3" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Government" Value="Government" />
                        <asp:ListItem Text="Non-Government" Value="Non-Government" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Name of PI / Co-PI</label>
                    <asp:TextBox ID="txtPIH3" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Title</label>
                    <asp:TextBox ID="txtTitleH3" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Name of Agency</label>
                    <asp:TextBox ID="txtAgencyH3" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Duration</label>
                    <asp:TextBox ID="txtDurationH3" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Year of Award / Sanction</label>
                    <asp:TextBox ID="txtYearH3" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Amount</label>
                    <asp:TextBox ID="txtAmountH3" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUploadH3" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemarkH3" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSaveH3" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveH3_Click" />

                <br />

                <asp:Label ID="lblErrorH3" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveH3" />
            <asp:PostBackTrigger ControlID="gvH3" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <asp:GridView ID="gvH3" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvH3_RowCommand">

        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="FundingType" HeaderText="Funding Type" />
            <asp:BoundField DataField="PI_CoPI_Name" HeaderText="PI/Co-PI" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="AgencyName" HeaderText="Agency" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" />
            <asp:BoundField DataField="CreatedOn" HeaderText="Date"
                DataFormatString="{0:dd-MM-yyyy}" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownloadH3" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>

</div>
					<div id="h_4" runat="server" visible="false"
    class="mb-3 p-4"
    style="max-width:850px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelH4" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">H-4 Project / Funding Details</h5>

            <div class="row mb-3">

                <div class="col-md-6">
                    <label>Government / Non-Government</label>
                    <asp:DropDownList ID="ddlFundingTypeH4" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Government" Value="Government" />
                        <asp:ListItem Text="Non-Government" Value="Non-Government" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Name of PI / Co-PI</label>
                    <asp:TextBox ID="txtPIH4" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Title</label>
                    <asp:TextBox ID="txtTitleH4" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Name of Agency</label>
                    <asp:TextBox ID="txtAgencyH4" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Duration</label>
                    <asp:TextBox ID="txtDurationH4" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Year of Award / Sanction</label>
                    <asp:TextBox ID="txtYearH4" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Amount</label>
                    <asp:TextBox ID="txtAmountH4" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUploadH4" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemarkH4" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSaveH4" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveH4_Click" />

                <br />

                <asp:Label ID="lblErrorH4" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveH4" />
            <asp:PostBackTrigger ControlID="gvH4" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <asp:GridView ID="gvH4" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvH4_RowCommand">

        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="FundingType" HeaderText="Funding Type" />
            <asp:BoundField DataField="PI_CoPI_Name" HeaderText="PI/Co-PI" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="AgencyName" HeaderText="Agency" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownloadH4" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>

</div>
					<div id="h_5" runat="server" visible="false"
    class="mb-3 p-4"
    style="max-width:850px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelH5" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">H-5 Project / Funding Details</h5>

            <div class="row mb-3">

                <div class="col-md-6">
                    <label>Government / Non-Government</label>
                    <asp:DropDownList ID="ddlFundingTypeH5" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Government" Value="Government" />
                        <asp:ListItem Text="Non-Government" Value="Non-Government" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Name of PI / Co-PI</label>
                    <asp:TextBox ID="txtPIH5" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Title</label>
                    <asp:TextBox ID="txtTitleH5" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Name of Agency</label>
                    <asp:TextBox ID="txtAgencyH5" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Duration</label>
                    <asp:TextBox ID="txtDurationH5" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Year of Award / Sanction</label>
                    <asp:TextBox ID="txtYearH5" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Amount</label>
                    <asp:TextBox ID="txtAmountH5" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUploadH5" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemarkH5" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSaveH5" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveH5_Click" />

                <br />

                <asp:Label ID="lblErrorH5" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveH5" />
            <asp:PostBackTrigger ControlID="gvH5" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <asp:GridView ID="gvH5" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvH5_RowCommand">

        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="FundingType" HeaderText="Funding Type" />
            <asp:BoundField DataField="PI_CoPI_Name" HeaderText="PI/Co-PI" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="AgencyName" HeaderText="Agency" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownloadH5" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>

</div>
					<div id="h_6" runat="server" visible="false"
    class="mb-3 p-4"
    style="max-width:850px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelH6" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">H-6 Project / Funding Details</h5>

            <div class="row mb-3">

                <div class="col-md-6">
                    <label>Government / Non-Government</label>
                    <asp:DropDownList ID="ddlFundingTypeH6" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Government" Value="Government" />
                        <asp:ListItem Text="Non-Government" Value="Non-Government" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Name of PI / Co-PI</label>
                    <asp:TextBox ID="txtPIH6" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Title</label>
                    <asp:TextBox ID="txtTitleH6" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Name of Agency</label>
                    <asp:TextBox ID="txtAgencyH6" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Duration</label>
                    <asp:TextBox ID="txtDurationH6" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Year of Award / Sanction</label>
                    <asp:TextBox ID="txtYearH6" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Amount</label>
                    <asp:TextBox ID="txtAmountH6" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUploadH6" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemarkH6" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSaveH6" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveH6_Click" />

                <br />

                <asp:Label ID="lblErrorH6" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveH6" />
            <asp:PostBackTrigger ControlID="gvH6" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <asp:GridView ID="gvH6" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvH6_RowCommand">

        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="FundingType" HeaderText="Funding Type" />
            <asp:BoundField DataField="PI_CoPI_Name" HeaderText="PI/Co-PI" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="AgencyName" HeaderText="Agency" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownloadH6" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>

</div>
					<div id="h_7" runat="server" visible="false"
    class="mb-3 p-4"
    style="max-width:850px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelH7" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">H-7 Project / Funding Details</h5>

            <div class="row mb-3">

                <div class="col-md-6">
                    <label>Government / Non-Government</label>
                    <asp:DropDownList ID="ddlFundingTypeH7" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Government" Value="Government" />
                        <asp:ListItem Text="Non-Government" Value="Non-Government" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Name of PI / Co-PI</label>
                    <asp:TextBox ID="txtPIH7" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Title</label>
                    <asp:TextBox ID="txtTitleH7" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Name of Agency</label>
                    <asp:TextBox ID="txtAgencyH7" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Duration</label>
                    <asp:TextBox ID="txtDurationH7" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Year of Award / Sanction</label>
                    <asp:TextBox ID="txtYearH7" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Amount</label>
                    <asp:TextBox ID="txtAmountH7" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUploadH7" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemarkH7" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSaveH7" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveH7_Click" />

                <br />

                <asp:Label ID="lblErrorH7" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveH7" />
            <asp:PostBackTrigger ControlID="gvH7" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <asp:GridView ID="gvH7" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvH7_RowCommand">

        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="FundingType" HeaderText="Funding Type" />
            <asp:BoundField DataField="PI_CoPI_Name" HeaderText="PI/Co-PI" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="AgencyName" HeaderText="Agency" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownloadH7" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>

</div>
					<div id="h_8" runat="server" visible="false"
    class="mb-3 p-4"
    style="max-width:850px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelH8" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">H-8 Project Grant Details</h5>

            <div class="row mb-3">

                <div class="col-md-6">
                    <label>Project Title</label>
                    <asp:TextBox ID="txtProjectTitleH8" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Name of PI / Co-PI</label>
                    <asp:TextBox ID="txtPIH8" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Date of Granted</label>
                    <asp:TextBox ID="txtDateH8" runat="server" CssClass="form-control" TextMode="Date" />
                </div>

                <div class="col-md-6">
                    <label>Amount</label>
                    <asp:TextBox ID="txtAmountH8" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUploadH8" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemarkH8" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSaveH8" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveH8_Click" />

                <br />

                <asp:Label ID="lblErrorH8" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveH8" />
            <asp:PostBackTrigger ControlID="gvH8" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <asp:GridView ID="gvH8" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvH8_RowCommand">

        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="ProjectTitle" HeaderText="Project Title" />
            <asp:BoundField DataField="PI_CoPI_Name" HeaderText="PI/Co-PI" />
            <asp:BoundField DataField="DateOfGranted" HeaderText="Date"
                DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownloadH8" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>

</div>
				    <div id="h_9" runat="server" visible="false"
    class="mb-3 p-4"
    style="max-width:850px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelH9" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">H-9 Publication Details</h5>

            <div class="row mb-3">

                <div class="col-md-6">
                    <label>Title</label>
                    <asp:TextBox ID="txtTitleH9" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Publisher</label>
                    <asp:TextBox ID="txtPublisherH9" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>URL</label>
                    <asp:TextBox ID="txtURLH9" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Date</label>
                    <asp:TextBox ID="txtDateH9" runat="server" CssClass="form-control" TextMode="Date" />
                </div>

                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUploadH9" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemarkH9" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSaveH9" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveH9_Click" />

                <br />

                <asp:Label ID="lblErrorH9" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveH9" />
            <asp:PostBackTrigger ControlID="gvH9" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <asp:GridView ID="gvH9" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvH9_RowCommand">

        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="Publisher" HeaderText="Publisher" />
            <asp:BoundField DataField="URL" HeaderText="URL" />
            <asp:BoundField DataField="DateOfPublication" HeaderText="Date"
                DataFormatString="{0:dd-MM-yyyy}" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownloadH9" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>

</div>
                   <div id="i_1" runat="server" visible="false"
    class="mb-3 p-4"
    style="max-width:850px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelI1" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">I-1 Conference Details</h5>

            <div class="row mb-3">

                <div class="col-md-6">
                    <label>Title</label>
                    <asp:TextBox ID="txtTitleI1" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Category</label>
                    <asp:DropDownList ID="ddlCategoryI1" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="National" Value="National" />
                        <asp:ListItem Text="International" Value="International" />
                        <asp:ListItem Text="State" Value="State" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Conference Name</label>
                    <asp:TextBox ID="txtConferenceI1" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Hosting Institute / University / Venue</label>
                    <asp:TextBox ID="txtInstituteI1" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Date</label>
                    <asp:TextBox ID="txtDateI1" runat="server" CssClass="form-control" TextMode="Date" />
                </div>

                <div class="col-md-6">
                    <label>Mode</label>
                    <asp:DropDownList ID="ddlModeI1" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Online" Value="Online" />
                        <asp:ListItem Text="Offline" Value="Offline" />
                        <asp:ListItem Text="Hybrid" Value="Hybrid" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUploadI1" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemarkI1" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSaveI1" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveI1_Click" />

                <br />

                <asp:Label ID="lblErrorI1" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveI1" />
            <asp:PostBackTrigger ControlID="gvI1" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <asp:GridView ID="gvI1" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvI1_RowCommand">

        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="Category" HeaderText="Category" />
            <asp:BoundField DataField="ConferenceName" HeaderText="Conference" />
            <asp:BoundField DataField="HostingInstitute" HeaderText="Institute" />
            <asp:BoundField DataField="DateOfConference" HeaderText="Date"
                DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField DataField="Mode" HeaderText="Mode" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownloadI1" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>

</div>
                    <div id="i_2" runat="server" visible="false"
    class="mb-3 p-4"
    style="max-width:850px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelI2" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">I-2 Conference Details</h5>

            <div class="row mb-3">

                <div class="col-md-6">
                    <label>Title</label>
                    <asp:TextBox ID="txtTitleI2" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Category</label>
                    <asp:DropDownList ID="ddlCategoryI2" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="National" Value="National" />
                        <asp:ListItem Text="International" Value="International" />
                        <asp:ListItem Text="State" Value="State" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Conference Name</label>
                    <asp:TextBox ID="txtConferenceI2" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Hosting Institute / University / Venue</label>
                    <asp:TextBox ID="txtInstituteI2" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Date</label>
                    <asp:TextBox ID="txtDateI2" runat="server" CssClass="form-control" TextMode="Date" />
                </div>

                <div class="col-md-6">
                    <label>Mode</label>
                    <asp:DropDownList ID="ddlModeI2" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Online" Value="Online" />
                        <asp:ListItem Text="Offline" Value="Offline" />
                        <asp:ListItem Text="Hybrid" Value="Hybrid" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUploadI2" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemarkI2" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSaveI2" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveI2_Click" />

                <br />

                <asp:Label ID="lblErrorI2" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveI2" />
            <asp:PostBackTrigger ControlID="gvI2" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <asp:GridView ID="gvI2" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvI2_RowCommand">

        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="Category" HeaderText="Category" />
            <asp:BoundField DataField="ConferenceName" HeaderText="Conference" />
            <asp:BoundField DataField="HostingInstitute" HeaderText="Institute" />
            <asp:BoundField DataField="DateOfConference" HeaderText="Date"
                DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField DataField="Mode" HeaderText="Mode" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownloadI2" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>

</div>
                   <div id="i_3" runat="server" visible="false"
    class="mb-3 p-4"
    style="max-width:850px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelI3" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">I-3 Conference Details</h5>

            <div class="row mb-3">

                <div class="col-md-6">
                    <label>Title</label>
                    <asp:TextBox ID="txtTitleI3" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Category</label>
                    <asp:DropDownList ID="ddlCategoryI3" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="National" Value="National" />
                        <asp:ListItem Text="International" Value="International" />
                        <asp:ListItem Text="State" Value="State" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Conference Name</label>
                    <asp:TextBox ID="txtConferenceI3" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Hosting Institute / University / Venue</label>
                    <asp:TextBox ID="txtInstituteI3" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Date</label>
                    <asp:TextBox ID="txtDateI3" runat="server" CssClass="form-control" TextMode="Date" />
                </div>

                <div class="col-md-6">
                    <label>Mode</label>
                    <asp:DropDownList ID="ddlModeI3" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Online" Value="Online" />
                        <asp:ListItem Text="Offline" Value="Offline" />
                        <asp:ListItem Text="Hybrid" Value="Hybrid" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUploadI3" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemarkI3" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSaveI3" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveI3_Click" />

                <br />

                <asp:Label ID="lblErrorI3" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveI3" />
            <asp:PostBackTrigger ControlID="gvI3" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <asp:GridView ID="gvI3" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvI3_RowCommand">

        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="Category" HeaderText="Category" />
            <asp:BoundField DataField="ConferenceName" HeaderText="Conference" />
            <asp:BoundField DataField="HostingInstitute" HeaderText="Institute" />
            <asp:BoundField DataField="DateOfConference" HeaderText="Date"
                DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField DataField="Mode" HeaderText="Mode" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownloadI3" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>

</div>
                    <div id="i_4" runat="server" visible="false"
    class="mb-3 p-4"
    style="max-width:900px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelI4" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">I-4 Event Details</h5>

            <div class="row mb-3">

                <div class="col-md-6">
                    <label>Category of Event</label>
                    <asp:DropDownList ID="ddlEventCategoryI4" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="FDP" />
                        <asp:ListItem Text="Conference" />
                        <asp:ListItem Text="Invited Talk" />
                        <asp:ListItem Text="Workshop" />
                        <asp:ListItem Text="Short Term Course" />
                        <asp:ListItem Text="Seminar" />
                        <asp:ListItem Text="IPR" />
                        <asp:ListItem Text="Tech-Fest" />
                        <asp:ListItem Text="Innovative Activities" />
                        <asp:ListItem Text="Value added Course" />
                        <asp:ListItem Text="Refreshers Course" />
                        <asp:ListItem Text="Induction Program" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Role</label>
                    <asp:DropDownList ID="ddlRoleI4" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Convenor" />
                        <asp:ListItem Text="Co-convenor" />
                        <asp:ListItem Text="Mentor" />
                        <asp:ListItem Text="Coordinator" />
                        <asp:ListItem Text="Member" />
                        <asp:ListItem Text="Secretary" />
                        <asp:ListItem Text="Other" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Title</label>
                    <asp:TextBox ID="txtTitleI4" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Date</label>
                    <asp:TextBox ID="txtDateI4" runat="server" CssClass="form-control" TextMode="Date" />
                </div>

                <div class="col-md-6">
                    <label>Category</label>
                    <asp:DropDownList ID="ddlCategoryI4" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="National" />
                        <asp:ListItem Text="International" />
                        <asp:ListItem Text="State" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Hosting Institute / University / Venue</label>
                    <asp:TextBox ID="txtInstituteI4" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Mode</label>
                    <asp:DropDownList ID="ddlModeI4" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Online" />
                        <asp:ListItem Text="Offline" />
                        <asp:ListItem Text="Hybrid" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Duration</label>
                    <asp:TextBox ID="txtDurationI4" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Attendee’s Details</label>
                    <asp:TextBox ID="txtAttendeesI4" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUploadI4" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemarkI4" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSaveI4" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveI4_Click" />

                <br />

                <asp:Label ID="lblErrorI4" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveI4" />
            <asp:PostBackTrigger ControlID="gvI4" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <asp:GridView ID="gvI4" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvI4_RowCommand">

        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="EventCategory" HeaderText="Event" />
            <asp:BoundField DataField="Role" HeaderText="Role" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="EventDate" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField DataField="Category" HeaderText="Category" />
            <asp:BoundField DataField="Mode" HeaderText="Mode" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownloadI4" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>

</div>
                   <div id="i_5" runat="server" visible="false"
    class="mb-3 p-4"
    style="max-width:900px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelI5" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">I-5 Event Details</h5>

            <div class="row mb-3">

                <div class="col-md-6">
                    <label>Category of Event</label>
                    <asp:DropDownList ID="ddlEventCategoryI5" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="FDP" />
                        <asp:ListItem Text="Conference" />
                        <asp:ListItem Text="Invited Talk" />
                        <asp:ListItem Text="Workshop" />
                        <asp:ListItem Text="Short Term Course" />
                        <asp:ListItem Text="Seminar" />
                        <asp:ListItem Text="IPR" />
                        <asp:ListItem Text="Tech-Fest" />
                        <asp:ListItem Text="Innovative Activities" />
                        <asp:ListItem Text="Value added Course" />
                        <asp:ListItem Text="Refreshers Course" />
                        <asp:ListItem Text="Induction Program" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Role</label>
                    <asp:DropDownList ID="ddlRoleI5" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Convenor" />
                        <asp:ListItem Text="Co-convenor" />
                        <asp:ListItem Text="Mentor" />
                        <asp:ListItem Text="Coordinator" />
                        <asp:ListItem Text="Member" />
                        <asp:ListItem Text="Secretary" />
                        <asp:ListItem Text="Other" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Title</label>
                    <asp:TextBox ID="txtTitleI5" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Date</label>
                    <asp:TextBox ID="txtDateI5" runat="server" CssClass="form-control" TextMode="Date" />
                </div>

                <div class="col-md-6">
                    <label>Category</label>
                    <asp:DropDownList ID="ddlCategoryI5" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="National" />
                        <asp:ListItem Text="International" />
                        <asp:ListItem Text="State" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Hosting Institute / University / Venue</label>
                    <asp:TextBox ID="txtInstituteI5" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Mode</label>
                    <asp:DropDownList ID="ddlModeI5" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Online" />
                        <asp:ListItem Text="Offline" />
                        <asp:ListItem Text="Hybrid" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Duration</label>
                    <asp:TextBox ID="txtDurationI5" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Attendee’s Details</label>
                    <asp:TextBox ID="txtAttendeesI5" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUploadI5" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemarkI5" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSaveI5" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveI5_Click" />

                <br />

                <asp:Label ID="lblErrorI5" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveI5" />
            <asp:PostBackTrigger ControlID="gvI5" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <asp:GridView ID="gvI5" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvI5_RowCommand">

        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="EventCategory" HeaderText="Event" />
            <asp:BoundField DataField="Role" HeaderText="Role" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="EventDate" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField DataField="Category" HeaderText="Category" />
            <asp:BoundField DataField="Mode" HeaderText="Mode" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownloadI5" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>

</div>
                    <div id="j_1" runat="server" visible="false"
    class="mb-3 p-4"
    style="max-width:850px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelJ1" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">J-1 Project / Activity Details</h5>

            <div class="row mb-3">

                <div class="col-md-6">
                    <label>Title</label>
                    <asp:TextBox ID="txtTitleJ1" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Project Title</label>
                    <asp:TextBox ID="txtProjectTitleJ1" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Duration</label>
                    <asp:TextBox ID="txtDurationJ1" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Date</label>
                    <asp:TextBox ID="txtDateJ1" runat="server" CssClass="form-control" TextMode="Date" />
                </div>

                <div class="col-md-6">
                    <label>Mode</label>
                    <asp:DropDownList ID="ddlModeJ1" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Online" />
                        <asp:ListItem Text="Offline" />
                        <asp:ListItem Text="Hybrid" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Name of Agency</label>
                    <asp:TextBox ID="txtAgencyJ1" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Amount</label>
                    <asp:TextBox ID="txtAmountJ1" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUploadJ1" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemarkJ1" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSaveJ1" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveJ1_Click" />

                <br />

                <asp:Label ID="lblErrorJ1" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveJ1" />
            <asp:PostBackTrigger ControlID="gvJ1" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <asp:GridView ID="gvJ1" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvJ1_RowCommand">

        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="ProjectTitle" HeaderText="Project" />
            <asp:BoundField DataField="Duration" HeaderText="Duration" />
            <asp:BoundField DataField="ActivityDate" HeaderText="Date"
                DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField DataField="Mode" HeaderText="Mode" />
            <asp:BoundField DataField="AgencyName" HeaderText="Agency" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownloadJ1" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>

</div>
                    <div id="j_2" runat="server" visible="false"
    class="mb-3 p-4"
    style="max-width:850px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelJ2" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">J-2 Project / Activity Details</h5>

            <div class="row mb-3">

                <div class="col-md-6">
                    <label>Title</label>
                    <asp:TextBox ID="txtTitleJ2" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Project Title</label>
                    <asp:TextBox ID="txtProjectTitleJ2" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Duration</label>
                    <asp:TextBox ID="txtDurationJ2" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Date</label>
                    <asp:TextBox ID="txtDateJ2" runat="server" CssClass="form-control" TextMode="Date" />
                </div>

                <div class="col-md-6">
                    <label>Mode</label>
                    <asp:DropDownList ID="ddlModeJ2" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Online" />
                        <asp:ListItem Text="Offline" />
                        <asp:ListItem Text="Hybrid" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Name of Agency</label>
                    <asp:TextBox ID="txtAgencyJ2" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Amount</label>
                    <asp:TextBox ID="txtAmountJ2" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUploadJ2" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemarkJ2" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSaveJ2" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveJ2_Click" />

                <br />

                <asp:Label ID="lblErrorJ2" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveJ2" />
            <asp:PostBackTrigger ControlID="gvJ2" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <asp:GridView ID="gvJ2" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvJ2_RowCommand">

        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="ProjectTitle" HeaderText="Project" />
            <asp:BoundField DataField="Duration" HeaderText="Duration" />
            <asp:BoundField DataField="ActivityDate" HeaderText="Date"
                DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField DataField="Mode" HeaderText="Mode" />
            <asp:BoundField DataField="AgencyName" HeaderText="Agency" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownloadJ2" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>

</div>

                   <div id="k_3" runat="server" visible="false"
    class="mb-3 p-4"
    style="max-width:900px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelK3" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">K-3 Event Details</h5>

            <div class="row mb-3">

                <div class="col-md-6">
                    <label>Mode</label>
                    <asp:DropDownList ID="ddlModeK3" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Online" />
                        <asp:ListItem Text="Offline" />
                        <asp:ListItem Text="Hybrid" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Level</label>
                    <asp:DropDownList ID="ddlLevelK3" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="State" />
                        <asp:ListItem Text="National" />
                        <asp:ListItem Text="International" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Category of Event</label>
                    <asp:DropDownList ID="ddlEventCategoryK3" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="FDP" />
                        <asp:ListItem Text="Conference" />
                        <asp:ListItem Text="Invited Talk" />
                        <asp:ListItem Text="Workshop" />
                        <asp:ListItem Text="Short Term Course" />
                        <asp:ListItem Text="Seminar" />
                        <asp:ListItem Text="IPR" />
                        <asp:ListItem Text="Tech-Fest" />
                        <asp:ListItem Text="Innovative Activities" />
                        <asp:ListItem Text="Value added Course" />
                        <asp:ListItem Text="Refreshers Course" />
                        <asp:ListItem Text="Induction Program" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Role</label>
                    <asp:DropDownList ID="ddlRoleK3" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Convenor" />
                        <asp:ListItem Text="Co-convenor" />
                        <asp:ListItem Text="Mentor" />
                        <asp:ListItem Text="Coordinator" />
                        <asp:ListItem Text="Member" />
                        <asp:ListItem Text="Secretary" />
                        <asp:ListItem Text="Other" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Title</label>
                    <asp:TextBox ID="txtTitleK3" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Date</label>
                    <asp:TextBox ID="txtDateK3" runat="server" CssClass="form-control" TextMode="Date" />
                </div>

                <div class="col-md-6">
                    <label>Hosting Institute / University / Venue</label>
                    <asp:TextBox ID="txtInstituteK3" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Duration</label>
                    <asp:TextBox ID="txtDurationK3" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Any Other Information</label>
                    <asp:TextBox ID="txtOtherInfoK3" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUploadK3" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemarkK3" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSaveK3" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveK3_Click" />

                <br />

                <asp:Label ID="lblErrorK3" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveK3" />
            <asp:PostBackTrigger ControlID="gvK3" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <asp:GridView ID="gvK3" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvK3_RowCommand">

        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="Mode" HeaderText="Mode" />
            <asp:BoundField DataField="Level" HeaderText="Level" />
            <asp:BoundField DataField="EventCategory" HeaderText="Event" />
            <asp:BoundField DataField="Role" HeaderText="Role" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="EventDate" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField DataField="HostingInstitute" HeaderText="Institute" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownloadK3" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>

</div>
                   <div id="k_4" runat="server" visible="false"
    class="mb-3 p-4"
    style="max-width:900px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelK4" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">K-4 Event Details</h5>

            <div class="row mb-3">

                <div class="col-md-6">
                    <label>Mode</label>
                    <asp:DropDownList ID="ddlModeK4" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Online" />
                        <asp:ListItem Text="Offline" />
                        <asp:ListItem Text="Hybrid" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Level</label>
                    <asp:DropDownList ID="ddlLevelK4" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="State" />
                        <asp:ListItem Text="National" />
                        <asp:ListItem Text="International" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Category of Event</label>
                    <asp:DropDownList ID="ddlEventCategoryK4" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="FDP" />
                        <asp:ListItem Text="Conference" />
                        <asp:ListItem Text="Invited Talk" />
                        <asp:ListItem Text="Workshop" />
                        <asp:ListItem Text="Short Term Course" />
                        <asp:ListItem Text="Seminar" />
                        <asp:ListItem Text="IPR" />
                        <asp:ListItem Text="Tech-Fest" />
                        <asp:ListItem Text="Innovative Activities" />
                        <asp:ListItem Text="Value added Course" />
                        <asp:ListItem Text="Refreshers Course" />
                        <asp:ListItem Text="Induction Program" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Role</label>
                    <asp:DropDownList ID="ddlRoleK4" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Convenor" />
                        <asp:ListItem Text="Co-convenor" />
                        <asp:ListItem Text="Mentor" />
                        <asp:ListItem Text="Coordinator" />
                        <asp:ListItem Text="Member" />
                        <asp:ListItem Text="Secretary" />
                        <asp:ListItem Text="Other" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Title</label>
                    <asp:TextBox ID="txtTitleK4" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Date</label>
                    <asp:TextBox ID="txtDateK4" runat="server" CssClass="form-control" TextMode="Date" />
                </div>

                <div class="col-md-6">
                    <label>Hosting Institute / University / Venue</label>
                    <asp:TextBox ID="txtInstituteK4" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Duration</label>
                    <asp:TextBox ID="txtDurationK4" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Any Other Information</label>
                    <asp:TextBox ID="txtOtherInfoK4" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUploadK4" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemarkK4" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSaveK4" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveK4_Click" />

                <br />

                <asp:Label ID="lblErrorK4" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveK4" />
            <asp:PostBackTrigger ControlID="gvK4" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <asp:GridView ID="gvK4" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvK4_RowCommand">

        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="Mode" HeaderText="Mode" />
            <asp:BoundField DataField="Level" HeaderText="Level" />
            <asp:BoundField DataField="EventCategory" HeaderText="Event" />
            <asp:BoundField DataField="Role" HeaderText="Role" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="EventDate" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField DataField="HostingInstitute" HeaderText="Institute" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownloadK4" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>

</div>
                   <div id="k_5" runat="server" visible="false"
    class="mb-3 p-4"
    style="max-width:900px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelK5" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">K-5 Event Details</h5>

            <div class="row mb-3">

                <div class="col-md-6">
                    <label>Mode</label>
                    <asp:DropDownList ID="ddlModeK5" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Online" />
                        <asp:ListItem Text="Offline" />
                        <asp:ListItem Text="Hybrid" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Level</label>
                    <asp:DropDownList ID="ddlLevelK5" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="State" />
                        <asp:ListItem Text="National" />
                        <asp:ListItem Text="International" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Category of Event</label>
                    <asp:DropDownList ID="ddlEventCategoryK5" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="FDP" />
                        <asp:ListItem Text="Conference" />
                        <asp:ListItem Text="Invited Talk" />
                        <asp:ListItem Text="Workshop" />
                        <asp:ListItem Text="Short Term Course" />
                        <asp:ListItem Text="Seminar" />
                        <asp:ListItem Text="IPR" />
                        <asp:ListItem Text="Tech-Fest" />
                        <asp:ListItem Text="Innovative Activities" />
                        <asp:ListItem Text="Value added Course" />
                        <asp:ListItem Text="Refreshers Course" />
                        <asp:ListItem Text="Induction Program" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Role</label>
                    <asp:DropDownList ID="ddlRoleK5" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Convenor" />
                        <asp:ListItem Text="Co-convenor" />
                        <asp:ListItem Text="Mentor" />
                        <asp:ListItem Text="Coordinator" />
                        <asp:ListItem Text="Member" />
                        <asp:ListItem Text="Secretary" />
                        <asp:ListItem Text="Other" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Title</label>
                    <asp:TextBox ID="txtTitleK5" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Date</label>
                    <asp:TextBox ID="txtDateK5" runat="server" CssClass="form-control" TextMode="Date" />
                </div>

                <div class="col-md-6">
                    <label>Hosting Institute / University / Venue</label>
                    <asp:TextBox ID="txtInstituteK5" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Duration</label>
                    <asp:TextBox ID="txtDurationK5" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Any Other Information</label>
                    <asp:TextBox ID="txtOtherInfoK5" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUploadK5" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemarkK5" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSaveK5" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveK5_Click" />

                <br />

                <asp:Label ID="lblErrorK5" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveK5" />
            <asp:PostBackTrigger ControlID="gvK5" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <asp:GridView ID="gvK5" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvK5_RowCommand">

        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="Mode" HeaderText="Mode" />
            <asp:BoundField DataField="Level" HeaderText="Level" />
            <asp:BoundField DataField="EventCategory" HeaderText="Event" />
            <asp:BoundField DataField="Role" HeaderText="Role" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="EventDate" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField DataField="HostingInstitute" HeaderText="Institute" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownloadK5" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>

</div>
                   <div id="k_6" runat="server" visible="false"
    class="mb-3 p-4"
    style="max-width:850px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelK6" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">K-6 Activity Details</h5>

            <div class="row mb-3">

                <div class="col-md-6">
                    <label>Type of Activity</label>
                    <asp:DropDownList ID="ddlActivityTypeK6" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Invited Extension Lecture" />
                        <asp:ListItem Text="PG Project Evaluation" />
                        <asp:ListItem Text="External Examination" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Level</label>
                    <asp:DropDownList ID="ddlLevelK6" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="UG" />
                        <asp:ListItem Text="PG" />
                        <asp:ListItem Text="PhD" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Mode</label>
                    <asp:DropDownList ID="ddlModeK6" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Online" />
                        <asp:ListItem Text="Offline" />
                        <asp:ListItem Text="Hybrid" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Hosting Institute / University / Venue</label>
                    <asp:TextBox ID="txtInstituteK6" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Date</label>
                    <asp:TextBox ID="txtDateK6" runat="server" CssClass="form-control" TextMode="Date" />
                </div>

                <div class="col-md-12">
                    <label>Purpose of Invitation</label>
                    <asp:TextBox ID="txtPurposeK6" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUploadK6" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemarkK6" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSaveK6" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveK6_Click" />

                <br />

                <asp:Label ID="lblErrorK6" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveK6" />
            <asp:PostBackTrigger ControlID="gvK6" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <asp:GridView ID="gvK6" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvK6_RowCommand">

        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="ActivitySubType" HeaderText="Activity" />
            <asp:BoundField DataField="Level" HeaderText="Level" />
            <asp:BoundField DataField="Mode" HeaderText="Mode" />
            <asp:BoundField DataField="HostingInstitute" HeaderText="Institute" />
            <asp:BoundField DataField="ActivityDate" HeaderText="Date"
                DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField DataField="PurposeOfInvitation" HeaderText="Purpose" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownloadK6" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>

</div>
                  <div id="k_7" runat="server" visible="false"
    class="mb-3 p-4"
    style="max-width:850px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelK7" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">K-7 Activity Details</h5>

            <div class="row mb-3">

                <div class="col-md-6">
                    <label>Title</label>
                    <asp:TextBox ID="txtTitleK7" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Hosting Institute / University / Venue</label>
                    <asp:TextBox ID="txtInstituteK7" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Date</label>
                    <asp:TextBox ID="txtDateK7" runat="server"
                        TextMode="Date" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUploadK7" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemarkK7" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSaveK7" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveK7_Click" />

                <br />

                <asp:Label ID="lblErrorK7" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveK7" />
            <asp:PostBackTrigger ControlID="gvK7" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <asp:GridView ID="gvK7" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvK7_RowCommand">

        <Columns>

            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="HostingInstitute" HeaderText="Institute" />
            <asp:BoundField DataField="ActivityDate" HeaderText="Date"
                DataFormatString="{0:dd-MM-yyyy}" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownloadK7" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

</div>
                   <div id="k_8" runat="server" visible="false"
    class="mb-3 p-4"
    style="max-width:900px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelK8" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">K-8 Membership Details</h5>

            <div class="row mb-3">

                <div class="col-md-6">
                    <label>Name of Professional/Institutional Body</label>
                    <asp:TextBox ID="txtBodyNameK8" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Level</label>
                    <asp:DropDownList ID="ddlLevelK8" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="National" />
                        <asp:ListItem Text="International" />
                        <asp:ListItem Text="State" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Discipline / Subject Area</label>
                    <asp:TextBox ID="txtDisciplineK8" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Nature of Body</label>
                    <asp:DropDownList ID="ddlNatureK8" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Academic" />
                        <asp:ListItem Text="Professional" />
                        <asp:ListItem Text="Research" />
                        <asp:ListItem Text="Regulatory" />
                        <asp:ListItem Text="Industry" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Membership Type</label>
                    <asp:DropDownList ID="ddlMembershipTypeK8" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Life" />
                        <asp:ListItem Text="Annual" />
                        <asp:ListItem Text="Institutional" />
                        <asp:ListItem Text="Individual" />
                        <asp:ListItem Text="Student" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Membership Number / ID</label>
                    <asp:TextBox ID="txtMembershipIDK8" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Date of Registration</label>
                    <asp:TextBox ID="txtDateK8" runat="server"
                        TextMode="Date" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Duration</label>
                    <asp:TextBox ID="txtDurationK8" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Membership Level</label>
                    <asp:DropDownList ID="ddlMembershipLevelK8" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Ordinary Member" />
                        <asp:ListItem Text="Fellow" />
                        <asp:ListItem Text="Senior Member" />
                        <asp:ListItem Text="Associate" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Registration Fee</label>
                    <asp:TextBox ID="txtFeeK8" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUploadK8" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemarkK8" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSaveK8" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveK8_Click" />

                <br />

                <asp:Label ID="lblErrorK8" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveK8" />
            <asp:PostBackTrigger ControlID="gvK8" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <asp:GridView ID="gvK8" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvK8_RowCommand">

        <Columns>

            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="BodyName" HeaderText="Body" />
            <asp:BoundField DataField="Level" HeaderText="Level" />
            <asp:BoundField DataField="MembershipType" HeaderText="Type" />
            <asp:BoundField DataField="MembershipID" HeaderText="ID No" />
            <asp:BoundField DataField="DateOfRegistration" HeaderText="Date"
                DataFormatString="{0:dd-MM-yyyy}" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownloadK8" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

</div>


<div id="k_9" runat="server" visible="false"
    class="mb-3 p-4"
    style="max-width:900px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelK9" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">K-9 Award / Fellowship Details</h5>

            <div class="row mb-3">

                <div class="col-md-6">
                    <label>Level</label>
                    <asp:DropDownList ID="ddlLevelK9" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="National" />
                        <asp:ListItem Text="International" />
                        <asp:ListItem Text="State" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Name of Award / Fellowship</label>
                    <asp:TextBox ID="txtAwardNameK9" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Category</label>
                    <asp:DropDownList ID="ddlCategoryK9" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Award" />
                        <asp:ListItem Text="Fellowship" />
                        <asp:ListItem Text="Scholarship" />
                        <asp:ListItem Text="Grant" />
                        <asp:ListItem Text="Honor" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Nature</label>
                    <asp:DropDownList ID="ddlNatureK9" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Academic" />
                        <asp:ListItem Text="Research" />
                        <asp:ListItem Text="Teaching" />
                        <asp:ListItem Text="Innovation" />
                        <asp:ListItem Text="Social Science" />
                        <asp:ListItem Text="Professional" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Date of Award</label>
                    <asp:TextBox ID="txtAwardDateK9" runat="server" TextMode="Date" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Date of Registration</label>
                    <asp:TextBox ID="txtRegDateK9" runat="server" TextMode="Date" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Duration</label>
                    <asp:TextBox ID="txtDurationK9" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Awarding Agency</label>
                    <asp:TextBox ID="txtAgencyK9" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Type of Organization</label>
                    <asp:DropDownList ID="ddlOrgTypeK9" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Government" />
                        <asp:ListItem Text="PSU" />
                        <asp:ListItem Text="University" />
                        <asp:ListItem Text="Industry" />
                        <asp:ListItem Text="NGO" />
                        <asp:ListItem Text="International Agency" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Title of Award</label>
                    <asp:TextBox ID="txtTitleK9" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Amount</label>
                    <asp:TextBox ID="txtAmountK9" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUploadK9" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemarkK9" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSaveK9" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveK9_Click" />

                <br />

                <asp:Label ID="lblErrorK9" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveK9" />
            <asp:PostBackTrigger ControlID="gvK9" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <asp:GridView ID="gvK9" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvK9_RowCommand">

        <Columns>

            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="Level" HeaderText="Level" />
            <asp:BoundField DataField="AwardName" HeaderText="Award" />
            <asp:BoundField DataField="Category" HeaderText="Category" />
            <asp:BoundField DataField="Nature" HeaderText="Nature" />
            <asp:BoundField DataField="DateOfAward" HeaderText="Date"
                DataFormatString="{0:dd-MM-yyyy}" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownloadK9" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

</div>
<div id="k_10" runat="server" visible="false"
    class="mb-3 p-4"
    style="max-width:900px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelK10" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">K-10 Award / Fellowship Details</h5>

            <div class="row mb-3">

                <div class="col-md-6">
                    <label>Level</label>
                    <asp:DropDownList ID="ddlLevelK10" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="National" />
                        <asp:ListItem Text="International" />
                        <asp:ListItem Text="State" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Name of Award / Fellowship</label>
                    <asp:TextBox ID="txtAwardNameK10" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Category</label>
                    <asp:DropDownList ID="ddlCategoryK10" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Award" />
                        <asp:ListItem Text="Fellowship" />
                        <asp:ListItem Text="Scholarship" />
                        <asp:ListItem Text="Grant" />
                        <asp:ListItem Text="Honor" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Nature</label>
                    <asp:DropDownList ID="ddlNatureK10" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Academic" />
                        <asp:ListItem Text="Research" />
                        <asp:ListItem Text="Teaching" />
                        <asp:ListItem Text="Innovation" />
                        <asp:ListItem Text="Social Science" />
                        <asp:ListItem Text="Professional" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Date of Award</label>
                    <asp:TextBox ID="txtAwardDateK10" runat="server" TextMode="Date" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Date of Registration</label>
                    <asp:TextBox ID="txtRegDateK10" runat="server" TextMode="Date" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Duration</label>
                    <asp:TextBox ID="txtDurationK10" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Awarding Agency</label>
                    <asp:TextBox ID="txtAgencyK10" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Type of Organization</label>
                    <asp:DropDownList ID="ddlOrgTypeK10" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Government" />
                        <asp:ListItem Text="PSU" />
                        <asp:ListItem Text="University" />
                        <asp:ListItem Text="Industry" />
                        <asp:ListItem Text="NGO" />
                        <asp:ListItem Text="International Agency" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Title of Award</label>
                    <asp:TextBox ID="txtTitleK10" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Amount</label>
                    <asp:TextBox ID="txtAmountK10" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUploadK10" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemarkK10" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSaveK10" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveK10_Click" />

                <br />

                <asp:Label ID="lblErrorK10" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveK10" />
            <asp:PostBackTrigger ControlID="gvK10" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <asp:GridView ID="gvK10" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvK10_RowCommand">

        <Columns>

            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="Level" HeaderText="Level" />
            <asp:BoundField DataField="AwardName" HeaderText="Award" />
            <asp:BoundField DataField="Category" HeaderText="Category" />
            <asp:BoundField DataField="Nature" HeaderText="Nature" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownloadK10" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

</div>
<div id="k_11" runat="server" visible="false"
    class="mb-3 p-4"
    style="max-width:950px; margin:auto; border:1px solid #ddd; border-radius:8px; background:#f8f9fa;">

    <asp:UpdatePanel ID="UpdatePanelK11" runat="server">
        <ContentTemplate>

            <h5 class="mb-3 text-primary">K-11 Course / Project Details</h5>

            <div class="row mb-3">

                <!-- COURSE SECTION -->
                <div class="col-md-6">
                    <label>Course File</label>
                    <asp:TextBox ID="txtCourseFileK11" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Platform</label>
                    <asp:TextBox ID="txtPlatformK11" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Hosting Institute</label>
                    <asp:TextBox ID="txtInstituteK11" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Discipline</label>
                    <asp:TextBox ID="txtDisciplineK11" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Duration (Weeks)</label>
                    <asp:TextBox ID="txtDurationWeeksK11" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Course Start Date</label>
                    <asp:TextBox ID="txtStartDateK11" runat="server" TextMode="Date" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Course End Date</label>
                    <asp:TextBox ID="txtEndDateK11" runat="server" TextMode="Date" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Course Status</label>
                    <asp:DropDownList ID="ddlStatusK11" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Pass" />
                        <asp:ListItem Text="Fail" />
                        <asp:ListItem Text="Ongoing" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Result (Grade/Marks)</label>
                    <asp:TextBox ID="txtResultK11" runat="server" CssClass="form-control" />
                </div>

                <!-- PROJECT SECTION -->
                <div class="col-md-6">
                    <label>Funding Type</label>
                    <asp:DropDownList ID="ddlFundingK11" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Government" />
                        <asp:ListItem Text="Non-Government" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Name of PI/Co-PI</label>
                    <asp:TextBox ID="txtPIK11" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Project Title</label>
                    <asp:TextBox ID="txtProjectTitleK11" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Agency Name</label>
                    <asp:TextBox ID="txtAgencyK11" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Project Duration</label>
                    <asp:TextBox ID="txtProjDurationK11" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Year of Award</label>
                    <asp:TextBox ID="txtYearK11" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label>Amount</label>
                    <asp:TextBox ID="txtAmountK11" runat="server" CssClass="form-control" />
                </div>

                <!-- COMMON -->
                <div class="col-md-6">
                    <label>Upload File</label>
                    <asp:FileUpload ID="fileUploadK11" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label>Remark</label>
                    <asp:TextBox ID="txtRemarkK11" runat="server"
                        TextMode="MultiLine" CssClass="form-control" />
                </div>

            </div>

            <div class="text-center mb-3">
                <asp:Button ID="btnSaveK11" runat="server"
                    Text="Save"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveK11_Click" />

                <br />

                <asp:Label ID="lblErrorK11" runat="server" ForeColor="Red" />
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveK11" />
            <asp:PostBackTrigger ControlID="gvK11" />
        </Triggers>
    </asp:UpdatePanel>

    <hr />

    <asp:GridView ID="gvK11" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        OnRowCommand="gvK11_RowCommand">

        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="CourseFile" HeaderText="Course" />
            <asp:BoundField DataField="Platform" HeaderText="Platform" />
            <asp:BoundField DataField="HostingInstitute" HeaderText="Institute" />
            <asp:BoundField DataField="CourseStatus" HeaderText="Status" />

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownloadK11" runat="server"
                        CommandName="Download"
                        CommandArgument='<%# Eval("ID") %>'>
                        Download
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</div>

            <div class="footer text-end mt-3">
    <asp:Button ID="btOK" runat="server"
        Text="Close"
        OnClick="CloseModal"
        CssClass="btn btn-danger" />
</div>
                            
<br />
</div>
    <asp:Label ID = "lblMessage" Text="File uploaded successfully." runat="server" ForeColor="Green" Visible="false" />

                                    
                                    
						
                     
   
     

            
      
            </div>
                         </ContentTemplate>
                   <Triggers>          
                          <asp:PostBackTrigger  ControlID="btnAttachmentSave_Fu_A1"/> 
                        <asp:PostBackTrigger  ControlID="gv_a1"/> 
					    <asp:PostBackTrigger ControlID="gvData" />
  <asp:PostBackTrigger  ControlID="gvF3"/> 
 <asp:PostBackTrigger ControlID="gvF4" />
                        </Triggers>
                      
                </asp:UpdatePanel>

	             
	              </div>

     



             
       
                        <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
            </asp:Panel>
    
    
      <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none" />
   
    <asp:ModalPopupExtender ID="md_Employee_Count_Details" runat="server" PopupControlID="pnl_Employee_Count_Details" TargetControlID="Button1" BackgroundCssClass="modalBackground"   OkControlID="btn_Close_Employee">
</asp:ModalPopupExtender>

<asp:Panel ID="pnl_Employee_Count_Details" runat="server" CssClass="modalPopup" Style="display: none">


    
    <table style="width:100%" >
         <tr> <td align="right"> <asp:Button ID="btnexportinexcel" runat="server" Text="Export in Excel" OnClick="btnexportinexcel_Click" CssClass="button success"/> </td></tr>
        <tr> <td>Status :    <asp:Label ID="lbl_Employee_Status_Details" runat="server" Text=""></asp:Label></td></tr>
         <tr> <td  align="center">
                <asp:GridView ID="grd_Employee_Details" runat="server" CssClass="gridview" AllowPaging="True" OnPageIndexChanging="grd_Employee_Details_PageIndexChanging" BorderStyle="None"  AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderWidth="1px" CellPadding="4" PageSize="9">
    <Columns>
        
              <asp:TemplateField HeaderText="EMP ID">
            <ItemTemplate>
                <asp:Label ID="lbl_Employee_EMP_ID_GRID" runat="server" Text='<%#Bind("EMP_ID") %>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
                      <asp:TemplateField  HeaderText="EMP Name">
            <ItemTemplate>
                <asp:Label ID="lbl_Employee_EMP_NAME_GRID" runat="server" Text='<%#Bind("EMP_NAME") %>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
                      <asp:TemplateField  HeaderText="Created On">
            <ItemTemplate>
                <asp:Label ID="lbl_Employee_Created_On_GRID" runat="server" Text='<%#Bind("Created_On","{0:dd MMM yyyy HH:mm}") %>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField  HeaderText="Employement Date">
            <ItemTemplate>
                <asp:Label ID="lbl_Employee_EmployementDate_On_GRID" runat="server" Text='<%#Bind("[Employment Date]","{0:dd MMM yyyy}") %>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
                                      <asp:TemplateField  HeaderText="College">
            <ItemTemplate>
                <asp:Label ID="lbl_Employee_Global_Dimension_1_Code_On_GRID" runat="server" Text='<%#Bind("[Global Dimension 1 Code]") %>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
                              <asp:TemplateField  HeaderText="Department">
            <ItemTemplate>
                <asp:Label ID="lbl_Employee_Department_On_GRID" runat="server" Text='<%#Bind("[Department Name]") %>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>


    </Columns>
                                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                        <RowStyle BackColor="White" ForeColor="#330099" />
                                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                        <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                        <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                        <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                        <SortedDescendingHeaderStyle BackColor="#7E0000" />
</asp:GridView></td></tr>


        <tr> <td>
            <br />
            <asp:LinkButton ID="btn_Close_Employee" runat="server" OnClick="CloseModal_Employee_Details" CssClass="button danger" >Close</asp:LinkButton>  </td></tr>
    </table>

    </asp:Panel> 
    
      
	


</asp:Content>

