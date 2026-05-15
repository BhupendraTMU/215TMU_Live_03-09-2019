<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FA_MM_All_Activity_Records.aspx.cs" Inherits="Faculty_FA_MM_All_Activity_Records" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="pms.css" rel="stylesheet" />
    
    
 <!-- Bootstrap v3 CSS -->
<link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />

<!-- jQuery Library (required for bootstrap-multiselect) -->
<script src="https://code.jquery.com/jquery-2.2.4.min.js"></script>

<!-- Bootstrap v3 JS -->
<script src="../bootstrap/js/bootstrap.min.js"></script>

<!-- Bootstrap Multiselect CSS & JS (minified versions) -->
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.15/css/bootstrap-multiselect.css" />
<script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.15/js/bootstrap-multiselect.js"></script>

   
     <script type="text/javascript">
    $(function () {
        $('#lb_Student_list').multiselect({
            includeSelectAllOption: true,
            maxHeight: 200, // Set a fixed height for the dropdown list
        buttonWidth: '150px', // Ensure button width matches the list box width
        dropDown: false,
        });
    });
</script>

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
   
</head>
<body>
    <form id="form1" runat="server">
    <div >
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <h3 style="text-align:center">
            <b>Record of Co-Curricular Activities & Extra-Curricular Activities</b>
        </h3>
        <div align="right">
       <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="button success" Visible="false" /> 

        </div>
    <br />
         <table class="table-borderless" style="width:100%;"> <!-- Table for alignment -->
        <tr>
            <td>
               
                <asp:Label ID="lbl_mentorFormentee_studentEnrollmentName" runat="server" Text="Filter by Student No./Enrollment No./Name: " Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_mentorFormentee_studentEnrollmentName" runat="server" Width="200px"></asp:TextBox>
            </td>

            <td>
                <asp:Label ID="lbl_mentorFormentee_course" runat="server" Text="Program: " Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddl_mentorFormentee_course" AutoPostBack="true" Font-Bold="true" OnSelectedIndexChanged="ddl_mentorFormentee_course_SelectedIndexChanged" runat="server" Width="200px"></asp:DropDownList>
            </td>

            <td>
                <asp:Label ID="lbl_mentorFormentee_academicYear" runat="server" Text="Academic Year: " Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddl_mentorFormentee_academicYear" AutoPostBack="true" OnSelectedIndexChanged="ddl_mentorFormentee_academicYear_SelectedIndexChanged" runat="server" Font-Bold="true"></asp:DropDownList>
            </td>

            <td>
                <asp:Button ID="btn_mentorFormentee_get" OnClick="btn_mentorFormentee_get_Click" runat="server" CssClass="btn btn-danger btn-sm text-uppercase" Text="Get Record"  />
            </td>
        </tr>
        
    </table>
        <br />
                
        <div style="width: 100%;"> <%--this is div of heading and table.--%>

            

            <table class="table table-bordered" style="width: 100%;">
                <tr style="background-color:#ed7600">
                     <th style="width:9%"><asp:Label ID="lbl_recordCo_ExtraActivities_type" runat="server" Text="Type" ForeColor="White"></asp:Label></th>
                    <th style="width:9%"><asp:Label ID="lbl_recordCo_ExtraActivities_semester" runat="server" Text="Semester" ForeColor="White"></asp:Label></th>
                    <th style="width:9%"><asp:Label ID="lbl_recordCo_ExtraActivities_studentName" runat="server" Text="Student Name" ForeColor="White"></asp:Label></th>
                    <th style="width:9%"><asp:Label ID="lbl_recordCo_ExtraActivities_activityName" runat="server" Text="Activity Name" ForeColor="White"></asp:Label></th>
                    <th style="width:8%"><asp:Label ID="lbl_recordCo_ExtraActivities_date" runat="server" Text="Date" ForeColor="White"></asp:Label></th>
                    <th style="width:10%"><asp:Label ID="lbl_recordCo_ExtraActivities_eventDetails" runat="server" Text="Event Details" ForeColor="White"></asp:Label></th>
                    <th style="width:10%"><asp:Label ID="lbl_recordCo_ExtraActivities_detailEventOrganizer" runat="server" Text="Detail of Event Organizer" ForeColor="White"></asp:Label></th>
                    <th style="width:10%"><asp:Label ID="lbl_recordCo_ExtraActivities_level_CUSNI" runat="server" Text="Level (College/University/ State/National/International)" ForeColor="White"></asp:Label></th>
                    <th style="width:10%"><asp:Label ID="lbl_recordCo_ExtraActivities_certificationPosition" runat="server" Text="Certification & Position" ForeColor="White"></asp:Label></th>
                   
                    <th style="width:14%"></th>
                </tr>

                <tr>
                    <td>
                        <asp:DropDownList ID="ddl_recordCo_CurricularActivities" runat="server" Font-Bold="true">
                            <asp:ListItem ID="ListItem_Record_Co_CurricularlActivities" runat="server" Text="Record of Co-Curricular Activities"></asp:ListItem>
                            <asp:ListItem ID="ListItem_Record_Extra_CurricularlActivities" runat="server" Text="Record of Extra-Curricular Activities"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="lbl_recordCo_Curricular_semester" runat="server"></asp:DropDownList>
                    </td>
                    <td>
                        <%--<asp:Label ID="lbl_recordCo_Curricular_studentName" runat="server" class="Text-Center"></asp:Label>--%>
                        <span>
                            <asp:ListBox ID="lb_Student_list" SelectionMode="Multiple" runat="server"></asp:ListBox>

                        </span>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_recordCo_Curricular_activityName" TextMode="MultiLine" runat="server" class="Text-Center"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_recordCo_Curricular_date" Width="100px" runat="server" class="Text-Center"></asp:TextBox>
                         <ajaxToolkit:CalendarExtender ID="calextender" CssClass="" TargetControlID="txt_recordCo_Curricular_date" runat="server" Format="dd MMM yyyy">
                               </ajaxToolkit:CalendarExtender>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  ControlToValidate="txt_recordCo_Curricular_date" SetFocusOnError="true" runat="server" ErrorMessage="Please Choose A Date"></asp:RequiredFieldValidator>

                    </td>
                    <td>
                        <asp:TextBox ID="txt_recordCo_Curricular_eventDetails" TextMode="MultiLine" runat="server"   class="Text-Center"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_recordCo_Curricular_detailEvent_organizer" TextMode="MultiLine" runat="server" class="Text-Center"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_recordCo_Curricular_level_CUSNI" TextMode="MultiLine" runat="server" class="Text-Center"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_recordCo_Curricular_certificaltionPosition" TextMode="MultiLine" runat="server"   class="Text-Center"></asp:TextBox>
                    </td>
                    
                    <td>
                        <asp:Button ID="btn_recordCo_Curricular_addData" CssClass="button info" runat="server" Text="Save" Width="70px" Font-Bold="true" OnClick="btn_recordCo_Curricular_addData_Click"/>
                    </td>                   
                </tr>
            </table>
        </div>

        <br />
<%--           <div class="container" style="width: 100%; overflow-x:auto;">--%>
    <asp:GridView ID="grdview_recordCo_curricular_tbl" 
              OnRowDataBound="grdview_recordCo_curricular_tbl_RowDataBound"
              OnRowUpdating="grdview_recordCo_curricular_tbl_RowUpdating" 
              OnRowCancelingEdit="grdview_recordCo_curricular_tbl_RowCancelingEdit" 
              OnRowDeleting="grdview_recordCo_curricular_tbl_RowDeleting" 
              OnRowEditing="grdview_recordCo_curricular_tbl_RowEditing"  
              runat="server" AutoGenerateColumns="false" CssClass="gridview" Width="100%">

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
                <asp:Label ID="lbl_SrNo_grd_" runat="server" Text='<%# Eval("Sr_No") %>'></asp:Label>
            </ItemTemplate>        
             <EditItemTemplate>
                <asp:Label ID="lbl_SrNo_grd_" runat="server" Text='<%# Eval("Sr_No") %>'></asp:Label>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="AutoNo" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lbl_AutoNo_grd" runat="server" Text='<%# Eval("AutoNo") %>'></asp:Label>
            </ItemTemplate>  
             <EditItemTemplate>
                <asp:Label ID="lbl_AutoNo_grd" runat="server" Text='<%# Eval("AutoNo") %>'></asp:Label>
            </EditItemTemplate>            
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Semester">
            <ItemTemplate>
                <asp:Label ID="lbl_Semester_grd" runat="server" Text='<%# Eval("Semester") %>'></asp:Label>
            </ItemTemplate>              
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Student Name">
            <ItemTemplate>
                <asp:Label ID="lbl_StudentName_grd" runat="server" Text='<%# Eval("Student_Name") %>'></asp:Label>
            </ItemTemplate>               
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Activity Name">
            <ItemTemplate>
                <asp:Label ID="lbl_ActivityName_grd" runat="server" Text='<%# Eval("Activity_Name") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txt_ActivityName_grd" runat="server" Text='<%# Eval("Activity_Name") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Date">
            <ItemTemplate>
                <asp:Label ID="lbl_Date_grd" runat="server" Text='<%# Eval("Date", "{0:dd MMM yyyy}") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Label ID="txt_Date_grd" runat="server" Text='<%# Eval("Date", "{0:dd MMM yyyy}") %>'></asp:Label>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Event Details">
            <ItemTemplate>
                <asp:Label ID="lbl_EventDetails_grd" runat="server" Text='<%# Eval("Event_Details") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txt_EventDetails_grd" runat="server" Text='<%# Eval("Event_Details") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Detail of Event Organizer">
            <ItemTemplate>
                <asp:Label ID="lbl_EventOrganizer_grd" runat="server" Text='<%# Eval("Detail_EventOrganizer") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txt_EventOrganizer_grd" runat="server" Text='<%# Eval("Detail_EventOrganizer") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Level (College/University/State/National/International)">
            <ItemTemplate>
                <asp:Label ID="lbl_CUSNI_grd" runat="server" Text='<%# Eval("Level_C_U_S_N_I") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txt_CUSNI_grd" runat="server" Text='<%# Eval("Level_C_U_S_N_I") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Certification & Position">
            <ItemTemplate>
                <asp:Label ID="lbl_Certification_grd" runat="server" Text='<%# Eval("Certification_Position") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txt_Certification_grd" runat="server" Text='<%# Eval("Certification_Position") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Type">
            <ItemTemplate>
                <asp:Label ID="lbl_ActivityType_grd" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:DropDownList ID="ddl_recordCo_CurricularActivities" runat="server" SelectedValue='<%# Eval("Type") %>'>
                    <asp:ListItem Text="Record of Co-Curricular Activities" Value="Record of Co-Curricular Activities"></asp:ListItem>
                    <asp:ListItem Text="Record of Extra-Curricular Activities" Value="Record of Extra-Curricular Activities"></asp:ListItem>
                </asp:DropDownList>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Status" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lbl_Activites_Status" runat="server" Text="Pending"></asp:Label>
            </ItemTemplate>
             <EditItemTemplate>
                <asp:Label ID="lbl_Activites_Status" runat="server" Text="Pending"></asp:Label>
                
            </EditItemTemplate>
                   </asp:TemplateField>
    </Columns>

    <EmptyDataTemplate>
        <asp:Label ID="lbl_gv_empty" runat="server" Text="There were no records found." Font-Bold="true"></asp:Label>
    </EmptyDataTemplate>
</asp:GridView>

<%--</div>--%>
    </div>
    </form>
</body>
</html>
