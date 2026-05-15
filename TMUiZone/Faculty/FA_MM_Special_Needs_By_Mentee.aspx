<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FA_MM_Special_Needs_By_Mentee.aspx.cs" Inherits="FA_MM_Special_Needs_By_Mentee" %>
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


    <div class="container"> <%--this is the main div of special needs or advice or help required by the mentee.--%>
            <h3 style="text-align:center"><b>Special needs or advice or help required by the mentee</b></h3>
        
    <div align="right">
       <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="button success" /> 

        </div>
        <br />
        <div style="width: 100%; overflow-x:auto;"> <%--this is the div of heading and table.--%>


            <table class="table table-bordered" style="text-align:center; margin:auto; width:100%">
                <tr style="background-color:#ed7600">
                    <th style="width:15%"><asp:Label ID="lbl_specialNeedsBymentee_semester" runat="server" Text="Semester" ForeColor="White"></asp:Label></th>
                    <th style="width:20%"><asp:Label ID="lbl_specialNeedsBymentee_studentName" runat="server" Text="Student Name" ForeColor="White"></asp:Label></th>
                    <th style="width:15%"><asp:Label ID="lbl_specialNeedsBymentee_date" runat="server" Text="Date" ForeColor="White"></asp:Label></th>
                    <th style="width:25%"><asp:Label ID="lbl_specialNeedsBymentee_provide_thementee" runat="server" Text="Provide any special facility/advice/help to the mentee" ForeColor="White"></asp:Label></th>
                    <th style="width:15%"><asp:Label ID="lbl_specialNeedsBymentee_remark" runat="server" Text="Remarks" ForeColor="White"></asp:Label></th>
                    <th style="width:10%"></th>
                </tr>

                <tr>
                    <td class="Text-Center">
                        <asp:DropDownList ID="ddl_Semester" runat="server"></asp:DropDownList>
                    </td>
                    <td class="Text-Center">
                        <asp:Label ID="lbl_specialBymentee_studentName" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_specialBymentee_date" runat="server" style="width:130px" class="Text-Center"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="calextender" CssClass="" TargetControlID="txt_specialBymentee_date" runat="server" Format="dd MMM yyyy"></ajaxToolkit:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txt_specialBymentee_date" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>

                    </td>
                    <td>
                        <asp:TextBox ID="txt_specialBymentee_provide_mentee" runat="server" class="Text-Center" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_specialBymentee_remark" runat="server" class="Text-Center" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btn_specialBymentee_addData" CssClass="button info" runat="server" Text="Save" Font-Bold="true" style="width:70px" OnClick="btn_specialBymentee_addData_Click"/>
                    </td>
                </tr>
                <tr> <td colspan="6">

                        <div style="width: 100%; overflow-x:auto;"̥>
        <asp:GridView ID="grdview_speicialBymentee_tbl" OnRowUpdating="grdview_speicialBymentee_tbl_RowUpdating"
    OnRowEditing="grdview_speicialBymentee_tbl_RowEditing" OnRowDeleting="grdview_speicialBymentee_tbl_RowDeleting"
    OnRowCancelingEdit="grdview_speicialBymentee_tbl_RowCancelingEdit" OnRowDataBound="grdview_speicialBymentee_tbl_RowDataBound"
    runat="server" Width="100%" CssClass="gridview" AutoGenerateColumns="false">
    <Columns>
        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton><br />
                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" Text="Delete"
                    OnClientClick="return confirm('Are you sure you want to delete this record?');"></asp:LinkButton>
            </ItemTemplate>
            
            <EditItemTemplate>
                <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Update" Text="Update"></asp:LinkButton><br />
                <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Auto No" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lbl_AutoNogrd" runat="server" Text='<%# Eval("AutoNo") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Label ID="lbl_AutoNogrd_Edit" runat="server" Text='<%# Eval("AutoNo") %>'></asp:Label>
            </EditItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Sr No" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lbl_SrNo" runat="server" Text='<%# Eval("Sr_No") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Label ID="lbl_SrNo_" runat="server" Text='<%# Eval("Sr_No") %>'></asp:Label>
            </EditItemTemplate>
        </asp:TemplateField>
               
        <asp:TemplateField HeaderText="Semester">
            <ItemTemplate>
                <asp:Label ID="lblGrd_specialBymentee_semester" runat="server" Text='<%# Eval("Semester") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Label ID="txtGrd_specialBymentee_semester" runat="server" Text='<%# Eval("Semester") %>'></asp:Label>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Student Name">
            <ItemTemplate>
                <asp:Label ID="lblGrd_specialBymentee_studentName" runat="server" Text='<%# Eval("Student_Name") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Label ID="txtGrd_specialBymentee_studentName" runat="server" Text='<%# Eval("Student_Name") %>'></asp:Label>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Date">
            <ItemTemplate>
                <asp:Label ID="lblGrd_specialBymentee_date" runat="server" Text='<%# Eval("Date","{0:dd MMM yyyy}") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Label ID="txtGrd_specialBymentee_date" runat="server" Text='<%# Eval("Date","{0:dd MMM yyyy}") %>'></asp:Label>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Provide any Special facility/advice/help to the mentee">
            <ItemTemplate>
                <asp:Label ID="lblGrd_specialBymentee_providetheMentee" runat="server" Text='<%# Eval("ProvideFacility_theMentee") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtGrd_specialBymentee_providetheMentee" runat="server" Text='<%# Eval("ProvideFacility_theMentee") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Remarks">
            <ItemTemplate>
                <asp:Label ID="lblGrd_specialBymentee_remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtGrd_specialBymentee_remark" runat="server" Text='<%# Eval("Remark") %>'></asp:TextBox>
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

        <br />

        
    </div>

    </form>
</body>
</html>
