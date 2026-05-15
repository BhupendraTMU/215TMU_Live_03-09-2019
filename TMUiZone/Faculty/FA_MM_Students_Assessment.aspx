<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FA_MM_Students_Assessment.aspx.cs" Inherits="FA_MM_Students_Assessment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

<link href="pms.css" rel="stylesheet" />
    
   <style type="text/css">
    .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=60);
        opacity: 0.6;
    }
    .modalPopup
    {
        background-color: #FFFFFF;
        width: 630px;
        border: 3px solid #0DA9D0;
        border-radius: 12px;
        padding:0
      
    }
    .modalPopup .header
    {
        background-color: #2FBDF1;
        height: 30px;
        color: White;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
        border-top-left-radius: 6px;
        border-top-right-radius: 6px;
    }
    .modalPopup .body
    {

        text-align: center;
        font-weight: bold;
    }
    .modalPopup .footer
    {
        padding: 6px;
    }
    .modalPopup .yes, .modalPopup .no
    {
        height: 23px;
        color: White;
        line-height: 23px;
        text-align: center;
        font-weight: bold;
        cursor: pointer;
        border-radius: 4px;
    }
    .modalPopup .yes
    {
        background-color: #2FBDF1;
        border: 1px solid #0DA9D0;
    }
    .modalPopup .no
    {
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
    height:30px;
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
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>


</head>
<body>
    <form id="form1" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <div class="container"> <%--this is the main div of students' assessment--%>
            <h3 style="text-align:center"><b>Students' Assessment</b></h3>
        
 <div align="right">
       <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="button success" /> 

        </div>
         <br />
        <div style="width: 100%; overflow-x:auto;"> <%--this is the div of heading of table--%>

            
            <table class="table table-bordered" style="text-align:center; margin:auto;">
                <tr style="background-color:#ed7600">
                    <th rowspan="2"><asp:Label ID="lbl_studentsAssessment_semester" runat="server" Text="Semester" ForeColor="White"></asp:Label></th>
                    <th rowspan="2"><asp:Label ID="lbl_studentsAssessment_studentName" runat="server" Text="Student Name" ForeColor="White"></asp:Label></th>
                    <th rowspan="2"><asp:Label ID="lbl_studentsAssessment_date" runat="server" Text="Date" ForeColor="White"></asp:Label></th>
                    <th colspan="6" style="text-align:center"><asp:Label ID="lbl_studentsAssessment_attributeLevel" runat="server" Text="Attributes/Level" ForeColor="White"></asp:Label></th>
                    <th rowspan="2"></th>
                </tr>
                <tr style="background-color:#ed7600">
                    <th><asp:Label ID="lbl_studentsAssessment_regularityClassrooms" runat="server" Text="Regularity in classrooms" ForeColor="White"></asp:Label></th>
                    <th><asp:Label ID="lbl_studentsAssessment_performanceStudy" runat="server" Text="Performance in study" ForeColor="White"></asp:Label></th>
                    <th><asp:Label ID="lbl_studentsAssessment_participationCurricularActivities" runat="server" Text="Participation in extra-curricular activities" ForeColor="White"></asp:Label></th>
                    <th><asp:Label ID="lbl_studentsAssessment_physicalStatus" runat="server" Text="Physical health status" ForeColor="White"></asp:Label></th>
                    <th><asp:Label ID="lbl_studentsAssessment_teacherStudent" runat="server" Text="Behaviour with teachers and students" ForeColor="White"></asp:Label></th>
                    <th><asp:Label ID="lbl_studentsAssessment_mentalStatus" runat="server" Text="Mental health status" ForeColor="White"></asp:Label></th>
                </tr>

                <tr>
                    <td class="Text-Center">
                        <asp:DropDownList ID="ddl_Semester" runat="server"></asp:DropDownList>
                    </td>
                    <td class="Text-Center">
                        <asp:Label ID="txt_students_assessment_studentName" runat="server" Font-Bold="true" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_students_assessment_date" runat="server" Class="Text-Center"></asp:TextBox>
                           <ajaxToolkit:CalendarExtender ID="calextender" CssClass="" TargetControlID="txt_students_assessment_date" runat="server" Format="dd MMM yyyy">
                               </ajaxToolkit:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txt_students_assessment_date" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_students_assessment_regularity_classrooms" runat="server" Font-Bold="true">
                            <asp:ListItem ID="ListItem_students_assessment_1belowAverage" runat="server" Value="1">Below Average</asp:ListItem>
                            <asp:ListItem ID="ListItem_students_assessment_2average" runat="server" Value="2">Average</asp:ListItem>
                            <asp:ListItem ID="ListItem_students_assessment_3aboveAverage" runat="server" Value="3">Above Average</asp:ListItem>     
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_students_assessment_performance_study" runat="server" Font-Bold="true">
                            <asp:ListItem ID="ListItem_performanceStudy_1belowAverage" runat="server" Value="1">Below Average</asp:ListItem>
                            <asp:ListItem ID="ListItem_performanceStudy_2average" runat="server" Value="2">Average</asp:ListItem>
                            <asp:ListItem ID="ListItem_performanceStudy_3aboveAverage" runat="server" Value="3">Above Average</asp:ListItem>     
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_students_assessment_participation_activities" runat="server" Font-Bold="true">
                            <asp:ListItem ID="ListItem_participationActivities_1belowAverage" runat="server" Value="1">Below Average</asp:ListItem>
                            <asp:ListItem ID="ListItem_participationActivities_2average" runat="server" Value="2">Average</asp:ListItem>
                            <asp:ListItem ID="ListItem_participationActivities_3aboveAverage" runat="server" Value="3">Above Average</asp:ListItem>     
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_students_assessment_physicalHealth_status" runat="server" Font-Bold="true">
                            <asp:ListItem ID="ListItem_physicalStatus_1belowAverage" runat="server" Value="1">Below Average</asp:ListItem>
                            <asp:ListItem ID="ListItem_physicalStatus_2average" runat="server" Value="2">Average</asp:ListItem>
                            <asp:ListItem ID="ListItem_physicalStatus_3aboveAverage" runat="server" Value="3">Above Average</asp:ListItem>     
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_students_assessment_behaviour_teachers_students" runat="server" Font-Bold="true">
                            <asp:ListItem ID="ListItem_teachers_studedents_1belowAverage" runat="server" Value="1">Below Average</asp:ListItem>
                            <asp:ListItem ID="ListItem_teachers_studedents_2average" runat="server" Value="2">Average</asp:ListItem>
                            <asp:ListItem ID="ListItListItem_teachers_studedents_3aboveAverageem3" runat="server" Value="3">Above Average</asp:ListItem>     
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_students_assessment_mentalHealth_status" runat="server" Font-Bold="true">
                            <asp:ListItem ID="ListItem_mentalStatus_1belowAverage" runat="server" Value="1">Below Average</asp:ListItem>
                            <asp:ListItem ID="ListItem_mentalStatus_2average" runat="server" Value="2">Average</asp:ListItem>
                            <asp:ListItem ID="ListItem_mentalStatus_3aboveAverage" runat="server" Value="3">Above Average</asp:ListItem>     
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btn_students_assessment_addData" runat="server" Text="Save" style="width:70px" Font-Bold="true" CssClass="button info" OnClick="btn_students_assessment_addData_Click"/>
                    </td>
                </tr>

<tr> <td colspan="10">
            <div style="width: 100%; overflow-x:auto;">
    <asp:GridView ID="grdview_students_assessment_tbl" OnRowDataBound="grdview_students_assessment_tbl_RowDataBound" OnRowEditing="grdview_students_assessment_tbl_RowEditing"
             OnRowDeleting="grdview_students_assessment_tbl_RowDeleting"
             OnRowUpdating="grdview_students_assessment_tbl_RowUpdating"  
             OnRowCancelingEdit="grdview_students_assessment_tbl_RowCancelingEdit"
             runat="server" Width="100%" AutoGenerateColumns="false" CssClass="gridview">
    <Columns>
        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" Text="Delete"
                                OnClientClick="return confirm('Are you sure you want to delete this record?');"></asp:LinkButton>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Update" Text="Update"></asp:LinkButton>
                <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Sr No" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lblGrd_studentsAssessment_srno" runat="server" Text='<%# Eval("[Sr_No]")%>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Label ID="lblGrd_studentsAssessment_srnoEdit" runat="server" Text='<%# Eval("[Sr_No]")%>'></asp:Label>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="AutoNo" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lblGrd_studentsAssessment_autoNo" runat="server" Text='<%# Eval("[AutoNO]")%>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Label ID="lblGrd_studentsAssessment_autoNoEdit" runat="server" Text='<%# Eval("[AutoNO]")%>'></asp:Label>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Semester">
            <ItemTemplate>
                <asp:Label ID="lblGrd_studentsAssessment_semester" runat="server" Text='<%# Eval("[Semester]")%>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Label ID="txtGrd_studentsAssessment_semester" runat="server" Text='<%# Bind("[Semester]") %>'></asp:Label>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Student Name">
            <ItemTemplate>
                <asp:Label ID="lblGrd_studentsAssessment_studentName" runat="server" Text='<%# Eval("[Student_Name]")%>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Label ID="txtGrd_studentsAssessment_studentName" runat="server" Text='<%# Bind("[Student_Name]") %>'></asp:Label>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Date">
            <ItemTemplate>
                <asp:Label ID="lblGrd_studentsAssessment_date" runat="server" Text='<%# Eval("[Date]","{0:dd MMM yyyy}")%>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Label ID="txtGrd_studentsAssessment_date" runat="server" Text='<%# Bind("[Date]","{0:dd MMM yyyy}") %>'></asp:Label>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Regularity Classrooms">
            <ItemTemplate>
                <asp:Label ID="lblGrd_studentsAssessment_regularityClassrooms" runat="server" Text='<%# Eval("[Regularity_Classrooms_Text]")%>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
               <asp:DropDownList ID="ddl_students_assessment_regularity_classrooms_" runat="server" Font-Bold="true">
                            <asp:ListItem ID="ListItem_students_assessment_1belowAverage" runat="server" Value="1">Below Average</asp:ListItem>
                            <asp:ListItem ID="ListItem_students_assessment_2average" runat="server" Value="2">Average</asp:ListItem>
                            <asp:ListItem ID="ListItem_students_assessment_3aboveAverage" runat="server" Value="3">Above Average</asp:ListItem>     
                        </asp:DropDownList>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Performance Study">
            <ItemTemplate>
                <asp:Label ID="lblGrd_studentsAssessment_performanceStudy" runat="server" Text='<%# Eval("[Performance_Study_Text]")%>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                 <asp:DropDownList ID="ddl_students_assessment_performance_study_" runat="server" Font-Bold="true">
                            <asp:ListItem ID="ListItem_performanceStudy_1belowAverage" runat="server" Value="1">Below Average</asp:ListItem>
                            <asp:ListItem ID="ListItem_performanceStudy_2average" runat="server" Value="2">Average</asp:ListItem>
                            <asp:ListItem ID="ListItem_performanceStudy_3aboveAverage" runat="server" Value="3">Above Average</asp:ListItem>     
                        </asp:DropDownList>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Participation Curricular Activities">
            <ItemTemplate>
                <asp:Label ID="lblGrd_studentsAssessment_participationActivities" runat="server" Text='<%# Eval("[Participation_CurricularActivities_Text]")%>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                  <asp:DropDownList ID="ddl_students_assessment_participation_activities_" runat="server" Font-Bold="true">
                            <asp:ListItem ID="ListItem_participationActivities_1belowAverage" runat="server" Value="1">Below Average</asp:ListItem>
                            <asp:ListItem ID="ListItem_participationActivities_2average" runat="server" Value="2">Average</asp:ListItem>
                            <asp:ListItem ID="ListItem_participationActivities_3aboveAverage" runat="server" Value="3">Above Average</asp:ListItem>     
                        </asp:DropDownList>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Physical Status">
            <ItemTemplate>
                <asp:Label ID="lblGrd_studentsAssessment_physicalStatus" runat="server" Text='<%# Eval("[Physical_Status_Text]")%>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                 <asp:DropDownList ID="ddl_students_assessment_physicalHealth_status_" runat="server" Font-Bold="true">
                            <asp:ListItem ID="ListItem_physicalStatus_1belowAverage" runat="server" Value="1">Below Average</asp:ListItem>
                            <asp:ListItem ID="ListItem_physicalStatus_2average" runat="server" Value="2">Average</asp:ListItem>
                            <asp:ListItem ID="ListItem_physicalStatus_3aboveAverage" runat="server" Value="3">Above Average</asp:ListItem>     
                        </asp:DropDownList>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Behaviour Teachers Students">
            <ItemTemplate>
                <asp:Label ID="lblGrd_studentsAssessment_teacherStudent" runat="server" Text='<%# Eval("[Behaviour_Teachers_Students_Text]")%>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                  <asp:DropDownList ID="ddl_students_assessment_behaviour_teachers_students_" runat="server" Font-Bold="true">
                            <asp:ListItem ID="ListItem_teachers_studedents_1belowAverage" runat="server" Value="1">Below Average</asp:ListItem>
                            <asp:ListItem ID="ListItem_teachers_studedents_2average" runat="server" Value="2">Average</asp:ListItem>
                            <asp:ListItem ID="ListItListItem_teachers_studedents_3aboveAverageem3" runat="server" Value="3">Above Average</asp:ListItem>     
                        </asp:DropDownList>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Mental Status">
            <ItemTemplate>
                <asp:Label ID="lblGrd_studentsAssessment_mentalStatus" runat="server" Text='<%# Eval("[Mental_Status_Text]")%>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                 <asp:DropDownList ID="ddl_students_assessment_mentalHealth_status_" runat="server" Font-Bold="true">
                            <asp:ListItem ID="ListItem_mentalStatus_1belowAverage" runat="server" Value="1">Below Average</asp:ListItem>
                            <asp:ListItem ID="ListItem_mentalStatus_2average" runat="server" Value="2">Average</asp:ListItem>
                            <asp:ListItem ID="ListItem_mentalStatus_3aboveAverage" runat="server" Value="3">Above Average</asp:ListItem>     
                        </asp:DropDownList>
            </EditItemTemplate>
        </asp:TemplateField>       

    </Columns>
         <EmptyDataTemplate>
        <asp:Label ID="lblGrd_specialBymentee" runat="server" Text="There were no records found." Font-Bold="true"></asp:Label>
    </EmptyDataTemplate>
</asp:GridView>

        </div>


     </td></tr>

            </table>

        </div>


    </div>
    </form>
</body>
</html>
