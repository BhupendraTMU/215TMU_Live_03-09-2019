<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FA_MM_Special_Achievements.aspx.cs" Inherits="FA_MM_Special_Achievements" %>

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

        <div class="container">
            <%--this is the main div of special achievements--%>
            
            <div style="text-align: center">
                <h2>Special Achievements</h2>
            </div>
            <div style="text-align: right">
                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="button success" OnClick="btnBack_Click" />

            </div>
            <br />
            <div style="overflow-x:auto;">
            <table class="table table-bordered" style="width: 100%; text-align: center; margin: auto;">
                <tr style="background-color:#ed7600;text-align:center">
                    <th style="width: 8%">
                        <asp:Label ID="lbl_splAchievements_semester" ForeColor="White" runat="server" Text="Semester"></asp:Label>
                    </th>
                    <th style="width: 12%">
                        <asp:Label ID="lbl_splAchievements_achievement_" ForeColor="White" runat="server" Text="Achievement"></asp:Label>
                    </th>
                    <th style="width: 10%">
                        <asp:Label ID="lbl_splAchievements_stname_" runat="server" ForeColor="White" Text="Student Name"></asp:Label>
                    </th>
                    <th style="width: 8%">
                        <asp:Label ID="lbl_splAchievements_date_" runat="server" ForeColor="White" Text="Date"></asp:Label>
                    </th>
                    <th style="width: 10%">
                        <asp:Label ID="lbl_splAchievements_activityName_" ForeColor="White" runat="server" Text="Activity Name"></asp:Label>
                    </th>
                    <th style="width: 20%">
                        <asp:Label ID="lbl_splAchievements_retailEventOrganization_" ForeColor="White" runat="server" Text="Details of Event/Organization"></asp:Label>
                    </th>
                    <th style="width: 14%">
                        <asp:Label ID="lbl_splAchievements_interIntra_" runat="server" ForeColor="White" Text="Inter /Intra University"></asp:Label>
                    </th>
                    <th style="width: 9%">
                        <asp:Label ID="lbl_splAchievements_position_" runat="server" ForeColor="White" Text="Position"></asp:Label>
                    </th>
                    <th style="width: 9%">
                        <asp:Label ID="lbl_splAchievements_remants_" ForeColor="White" runat="server" Text="Remarks"></asp:Label>
                    </th>
                    <th>

                    </th>
                </tr>


                <tr>
                    <td>
                        <asp:DropDownList ID="ddl_semester" runat="server" Font-Bold="true">
                            
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_achievement" runat="server" Font-Bold="true">
                            <asp:ListItem runat="server" Text="Academic"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Co-Curricular"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Extra-Curricular"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Sports"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Cultural"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Placement"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Training"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Project"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Others"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lbl_splAchievements_stName" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_splAchievements_date" runat="server" Width="100px"></asp:TextBox>
                          <ajaxToolkit:CalendarExtender ID="calextender" CssClass="" TargetControlID="txt_splAchievements_date" runat="server" Format="dd MMM yyyy">
                               </ajaxToolkit:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txt_splAchievements_date" SetFocusOnError="true" runat="server" ErrorMessage="Date is Required"></asp:RequiredFieldValidator>
                  

                    </td>
                    <td>
                        <asp:TextBox ID="txt_splAchievements_activityName" TextMode="MultiLine" Width="150px"  runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_splAchievements_retailEventOrganization" TextMode="MultiLine" runat="server" Width="150px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_splAchievements_interIntra" runat="server" TextMode="MultiLine" Width="150px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_splAchievements_position" TextMode="MultiLine" runat="server" Width="150px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_splAchievements_remants" TextMode="MultiLine" runat="server" Width="150px"></asp:TextBox>
                    </td>
                     <td>
                        <asp:Button ID="btn_SaveData" runat="server" Text="Save" style="width:70px" Font-Bold="true" CssClass="button info" OnClick="btn_SaveData_Click"/>
                    </td>
                </tr>
            </table>
                </div>
            <br />
            <div style="overflow-x:auto;">

                <asp:GridView ID="gv_special_achievements" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="gridview"
    OnRowEditing="gv_special_achievements_RowEditing"
    OnRowUpdating="gv_special_achievements_RowUpdating"
    OnRowCancelingEdit="gv_special_achievements_RowCancelingEdit"
                    OnRowDeleting="gv_special_achievements_RowDeleting">
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

      
        <asp:TemplateField HeaderText="Semester">
            <ItemTemplate>
                <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("Semester") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Label ID="lblSemester_Edit" runat="server" Text='<%# Eval("Semester") %>'></asp:Label>
               
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Achievements">
            <ItemTemplate>
                <asp:Label ID="lblAchievements" runat="server" Text='<%# Eval("Achievements") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>               
                <asp:DropDownList ID="ddl_Achievements_edit" runat="server" Font-Bold="true">
                            <asp:ListItem runat="server" Text="Academic"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Co-Curricular"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Extra-Curricular"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Sports"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Cultural"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Placement"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Training"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Project"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Others"></asp:ListItem>
                        </asp:DropDownList>                
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Student Id" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lbl_Student_Id" runat="server" Text='<%# Eval("Student_Id") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Label ID="lbl_Student_Id_edit" runat="server" Text='<%# Bind("Student_Id") %>'></asp:Label>
            </EditItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Student Name">
            <ItemTemplate>
                <asp:Label ID="lbl_StudentName" runat="server" Text='<%# Eval("Name_Student") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Label ID="lbl_StudentName_Edit" runat="server" Text='<%# Bind("Name_Student") %>'></asp:Label>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Date">
            <ItemTemplate>
                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date", "{0:dd MMM yyyy}") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Label ID="txtDate" runat="server" Text='<%# Bind("Date", "{0:yyyy-MM-dd}") %>'></asp:Label>
                <%--<ajaxToolkit:CalendarExtender ID="calDate" runat="server" TargetControlID="txtDate" Format="yyyy-MM-dd" />--%>
            </EditItemTemplate>
        </asp:TemplateField>

         <asp:TemplateField HeaderText="Activity Name">
            <ItemTemplate>
                <asp:Label ID="lblActivityName" runat="server" Text='<%# Eval("ActivityName") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtActivityName" runat="server" Text='<%# Bind("ActivityName") %>' CssClass="form-control"></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>

          <asp:TemplateField HeaderText="Details Of Event/Organization">
            <ItemTemplate>
                <asp:Label ID="lblDetailsOfEvent" runat="server" Text='<%# Eval("DetailsOfEvent") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtDetailsOfEvent" runat="server" Text='<%# Bind("DetailsOfEvent") %>' CssClass="form-control"></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>

         <asp:TemplateField HeaderText="Inter/Intra University">
            <ItemTemplate>
                <asp:Label ID="lblInter_Intra_Uni" runat="server" Text='<%# Eval("Inter_Intra_Uni") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtInter_Intra_Uni" runat="server" Text='<%# Bind("Inter_Intra_Uni") %>' CssClass="form-control"></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Position">
            <ItemTemplate>
                <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtPosition" runat="server" Text='<%# Bind("Position") %>' CssClass="form-control"></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Remarks">
            <ItemTemplate>
                <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Bind("Remarks") %>' CssClass="form-control"></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>

          <asp:TemplateField HeaderText="AutoNo" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lblAutoNo" runat="server" Text='<%# Eval("AutoNo") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Label ID="txtAutoNo" runat="server" Text='<%# Bind("AutoNo") %>'></asp:Label>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="SrNo" Visible="false"> 
            <ItemTemplate>
                <asp:Label ID="lblSr_No" runat="server" Text='<%# Eval("Sr_No") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Label ID="txtSr_No" runat="server" Text='<%# Bind("Sr_No") %>'></asp:Label>
            </EditItemTemplate>
        </asp:TemplateField>

    </Columns>
    <EmptyDataTemplate>
        <asp:Label ID="lblNoRecords" runat="server" Text="No records found." CssClass="text-danger"></asp:Label>
    </EmptyDataTemplate>
</asp:GridView>


            </div>



        </div>
    </form>
</body>
</html>
