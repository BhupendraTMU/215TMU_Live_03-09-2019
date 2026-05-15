<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="FA_AppraisalForm.aspx.cs" Inherits="Faculty_FA_AppraisalForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
                <table style="width:100% ; background-color:white">
                    <tr> <td> 
<%--<div id="wrap">--%>
    <table style="width:100%">
        <tr> <td>  <h3> PERFORMANCE APPRAISAL - "ELIGIBLE TEACHING EMPLOYEES"</h3> </td></tr>
        <tr> <td>  <table>  <tr><td>Academic Session : </td> <td>   <asp:DropDownList ID="dd_AcademicYear" OnSelectedIndexChanged="dd_AcademicYear_SelectedIndexChanged" runat="server" AutoPostBack="true"  CssClass="textbox">                
                    </asp:DropDownList>                  </td> <td> College </td> <td> <asp:DropDownList ID="ddl_College_Deptt" OnSelectedIndexChanged="ddl_College_Deptt_SelectedIndexChanged" runat="server" AutoPostBack="true"  CssClass="textbox">                       
                   </asp:DropDownList></td><td> <asp:Button ID="btn_Export_Excel" runat="server" Text="Export in Excel" OnClick="btnExportInExcel_Click" CssClass="button success btn" />
 </td> <td> <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-danger" OnClick="btnBack_Click" /> </td></tr></table></td></tr>
        <tr> <td> 
            <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Width="980px">   
                  <asp:GridView ID="GridView1" CssClass="gridview" Width="100%" AutoGenerateColumns="false" BackColor="White" BorderColor="#CCCCCC" BorderWidth="1px" CellPadding="3" AllowPaging="True" runat="server" OnPageIndexChanging="GridView1_PageIndexChanging">

            <Columns>
                <asp:TemplateField HeaderText="S.No">
                    <HeaderStyle Width="20px" />
                    <ItemStyle Width="10px" />
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Employee Code">
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("Employee_Code") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Employee Name">
                    <ItemTemplate>
                        <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Eval("Created_By") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Employment Date">
                    <ItemTemplate>
                        <asp:Label ID="lblTeachingJoiningDate" runat="server" Text='<%# Eval("[Employment Date]","{0:dd MMM yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Designation">
                    <ItemTemplate>
                        <asp:Label ID="lblDesignation" runat="server" Text='<%# Eval("Designation") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Current Salary">
                    <ItemStyle Width="50px" />
                    <ItemTemplate>
                        <asp:Label ID="lblCurrentCTC" runat="server" Text='<%# Eval("[Current CTC]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Criteria A-Academic Performance(250)">
                    <ItemTemplate>
                        <asp:Label ID="lblCriteriaA" runat="server" Text='<%# Eval("APIScore_CriteriaA_Faculty_Total") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Criteria B-Research & Development(200)">
                    <ItemTemplate>
                        <asp:Label ID="lblCriteriaB" runat="server" Text='<%# Eval("APIScore_CriteriaB_Faculty_Total") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Criteria C-Professional & Personal Competency(100)">
                    <ItemTemplate>
                        <asp:Label ID="lblCriteriaC" runat="server" Text='<%# Eval("APIScore_CriteriaC_Faculty_Total") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Criteria D-Administration(100)">
                    <ItemTemplate>
                        <asp:Label ID="lblCriteriaD" runat="server" Text='<%# Eval("APIScore_CriteriaD_Faculty_Total") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Criteria E-Faculty Assessment By Reprting Authority(50)">
                    <ItemTemplate>
                        <asp:Label ID="lblCriteriaE" runat="server" Text='<%# Eval("APIScore_CriteriaE_RM_Total") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Criteria F-Student's Feedback(50)">
                    <ItemTemplate>
                        <asp:Label ID="lblCriteriaF" runat="server" Text='<%# Eval("APIScore_CriteriaF_RM_Total") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Obtained API Score(Out of 750)">
                    <ItemTemplate>
                        <asp:Label ID="lblTotalObtained" runat="server" Text='<%# Eval("HR_TotalObtainedScore_Total") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Category(Excellent, Good, Average)">
                    <ItemTemplate>
                        <asp:Label ID="lblFeedbackCategory" runat="server" Text='<%# Eval("HR_FacultyCateogory") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Required Improvement In The Criteria from A,B,C,D">
                    <ItemTemplate>
                        <asp:Label ID="lblRequiredImprovement" runat="server" Text='<%# Eval("HR_RequiredImprovement_Combined") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>
        </asp:GridView>

                <asp:GridView ID="GridView2" Visible="false"  Width="100%" AutoGenerateColumns="false" BackColor="White" BorderColor="#CCCCCC" BorderWidth="1px" CellPadding="3" runat="server" >

            <Columns>
                <asp:TemplateField HeaderText="S.No">
                    <HeaderStyle Width="20px" />
                    <ItemStyle Width="10px" />
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Employee Code">
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("Employee_Code") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Employee Name">
                    <ItemTemplate>
                        <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Eval("Created_By") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Employment Date">
                    <ItemTemplate>
                        <asp:Label ID="lblTeachingJoiningDate" runat="server" Text='<%# Eval("[Employment Date]","{0:dd MMM yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Designation">
                    <ItemTemplate>
                        <asp:Label ID="lblDesignation" runat="server" Text='<%# Eval("Designation") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Current Salary">
                    <ItemStyle Width="50px" />
                    <ItemTemplate>
                        <asp:Label ID="lblCurrentCTC" runat="server" Text='<%# Eval("[Current CTC]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Criteria A-Academic Performance(250)">
                    <ItemTemplate>
                        <asp:Label ID="lblCriteriaA" runat="server" Text='<%# Eval("APIScore_CriteriaA_Faculty_Total") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Criteria B-Research & Development(200)">
                    <ItemTemplate>
                        <asp:Label ID="lblCriteriaB" runat="server" Text='<%# Eval("APIScore_CriteriaB_Faculty_Total") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Criteria C-Professional & Personal Competency(100)">
                    <ItemTemplate>
                        <asp:Label ID="lblCriteriaC" runat="server" Text='<%# Eval("APIScore_CriteriaC_Faculty_Total") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Criteria D-Administration(100)">
                    <ItemTemplate>
                        <asp:Label ID="lblCriteriaD" runat="server" Text='<%# Eval("APIScore_CriteriaD_Faculty_Total") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Criteria E-Faculty Assessment By Reprting Authority(50)">
                    <ItemTemplate>
                        <asp:Label ID="lblCriteriaE" runat="server" Text='<%# Eval("APIScore_CriteriaE_RM_Total") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Criteria F-Student's Feedback(50)">
                    <ItemTemplate>
                        <asp:Label ID="lblCriteriaF" runat="server" Text='<%# Eval("APIScore_CriteriaF_RM_Total") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Obtained API Score(Out of 750)">
                    <ItemTemplate>
                        <asp:Label ID="lblTotalObtained" runat="server" Text='<%# Eval("HR_TotalObtainedScore_Total") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Category(Excellent, Good, Average)">
                    <ItemTemplate>
                        <asp:Label ID="lblFeedbackCategory" runat="server" Text='<%# Eval("HR_FacultyCateogory") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Required Improvement In The Criteria from A,B,C,D">
                    <ItemTemplate>
                        <asp:Label ID="lblRequiredImprovement" runat="server" Text='<%# Eval("HR_RequiredImprovement_Combined") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>
        </asp:GridView>


            </asp:Panel>
</td></tr>

    </table>
     <%--</div>--%>
    </td></tr>
                    </table>
</asp:Content>

