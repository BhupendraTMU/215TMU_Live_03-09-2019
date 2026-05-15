<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="FA_Mentor_Allocation.aspx.cs" Inherits="FA_Mentor_Allocation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script src="js/jquery.min.js"></script>

            <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "PMS%20Img/minus.png");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "PMS%20Img/plus.png");
            $(this).closest("tr").next().remove();
        });
        function PopupShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 99999999;
        }
        function confirmAction() {
            return confirm("Are you sure you want to proceed?");
        }

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
.cursor-pointer {
    cursor: pointer;
}

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <div class="container">

                    <asp:Panel ID="pnl_Mentor" Visible="true" runat="server">
                  <div class="container"> <!-- Main div for Mentor Allocation -->
    <h3 class="text-left font-weight-bold"><b>Mentor To Mentee Allocation</b></h3>
                      

    <table class="table-borderless" style="width:100%; padding-bottom: 50px; font-weight: bold;"> <!-- Table for alignment -->
                        <tr> <td colspan="7">
                            <table cellpadding="0px" cellspacing="0px"> 
                                <tr> <td>  Employee Code / Name</td> <td style="width:10px"> </td> <td> Program: </td>  <td style="width:10px"> </td> <td>  Admitted Year </td> <td style="width:10px"> </td> <td>  Semester </td> <td style="width:10px"> </td> <td> Section </td><td style="width:10px"> </td> <td>  </td> </tr>
                                <tr> <td>   <asp:TextBox ID="txt_filterby_name" runat="server" Width="200px"></asp:TextBox></td> <td style="width:10px"> </td> <td>     <asp:DropDownList ID="ddl_course" AutoPostBack="true" OnSelectedIndexChanged="ddl_course_SelectedIndexChanged" runat="server" Width="200px"></asp:DropDownList> </td>
                                      <td style="width:10px"> </td> <td>  <asp:DropDownList ID="dd_AcademicYear" AutoPostBack="true" OnSelectedIndexChanged="dd_AcademicYear_SelectedIndexChanged" runat="server"></asp:DropDownList> </td> <td style="width:10px"> </td> <td>  <asp:DropDownList ID="dd_Semester" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dd_Semester_SelectedIndexChanged"></asp:DropDownList> </td> <td style="width:10px"> </td> <td> <asp:DropDownList ID="dd_Section" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dd_Section_SelectedIndexChanged"></asp:DropDownList> </td><td style="width:10px"> </td> <td> <asp:Button ID="btn_filter" runat="server" CssClass="btn btn-danger text-uppercase" Text="Get Record" OnClick="btn_filter_Click1" /> </td> </tr>


                            </table>

                             </td></tr>
     
        <tr> <td colspan="7">  
            <br />
                        <h5 style="font-weight:bold" align="right">
                         Total No. Of Mentee:<asp:LinkButton ID="lnk_TotalMentee" runat="server" OnClick="lnk_TotalMentee_Click">
                             <asp:Label ID="lbl_TotalMentee" runat="server" Text="Total"></asp:Label></asp:LinkButton>
                         Unassigned Mentee:<asp:LinkButton ID="lnl_UnassignedMentee" OnClick="lnl_UnassignedMentee_Click" runat="server">
                             <asp:Label ID="lbl_UnassignedMentee" runat="server" Text="Pending"></asp:Label></asp:LinkButton>

                      </h5>

             </td></tr>

                <tr> <td colspan="7">
                      <asp:GridView ID="grd_Menter_details_popup" OnPageIndexChanging="grd_Menter_details_popup_PageIndexChanging" CssClass="gridview" runat="server" BorderStyle="None" AutoGenerateColumns="False" CellPadding="3" Width="100%" OnRowDataBound="grd_Menter_details_popup_RowDataBound">
    <Columns>
        <asp:TemplateField HeaderText="">
            <ItemTemplate>
                <asp:Image ID="img_Mentee_Details" ImageAlign="Right" runat="server" Style="cursor: pointer" src="PMS%20Img/plus.png" />
                <asp:Panel ID="pnl_Mentee" runat="server" Style="display: none">
                        <div style="padding-bottom: 20px; font-weight:bold">
                            <span>Filter by Student No/Enrollment No/Name</span>
                            <asp:TextBox ID="txt_Studentfilterby_name" runat="server"></asp:TextBox>
                       
                            <asp:Button ID="btn_Studentfilter" runat="server" CssClass="btn-danger" Text="Get Record" OnClick="btn_Studentfilter_Click" />
                        </div>

                    <div style="height:500px;overflow:scroll">

                    <asp:GridView ID="gv_Mentee"  CssClass="gridview"  AutoGenerateColumns="false" runat="server" OnRowDataBound="gv_Mentee_RowDataBound">
                        <Columns>
                             <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnk_remove" OnClientClick="return confirmAction();" runat="server" Visible="false" CssClass="btn" CommandArgument='<%#Bind("[AutoNo]") %>' OnCommand="lnk_remove_Command">Remove</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Code">
                                <ItemTemplate>
                                     <%--<asp:Label ID="lbl_AutoNo" runat="server" Text='<%# Bind("[AutoNo]") %>' Visible="false"></asp:Label>--%>

                                     <asp:Label ID="lblAllocated" runat="server" Text='<%# Bind("[Allocated]") %>' Visible="false"></asp:Label>
                                     <asp:Label ID="lbl_Academic_Year" runat="server" Text='<%# Bind("[Academic_Year]") %>' Visible="false"></asp:Label>
                                     <asp:Label ID="lbl_Course_Code" runat="server" Text='<%# Bind("[Course_Code]") %>' Visible="false"></asp:Label>
                                    
                                     <asp:Label ID="lblMenterID" runat="server" Text='<%# Bind("[Mentor_ID]") %>' Visible="false"></asp:Label>
                                   
                                    <asp:CheckBox ID="chk_Mentee" runat="server" />
                                    <asp:Label ID="lbl_St_Code_grid" runat="server" Text='<%# Bind("No_") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_St_Name_grid" runat="server" Text='<%# Bind("[Student Name]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date Of Birth">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_St_DOB_grid" runat="server" Text='<%# Bind("[Date of Birth]","{0:dd MMM yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Father's Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_St_Father_grd" runat="server" Text='<%# Bind("[Fathers Name]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mother's Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_St_Mother_grd" runat="server" Text='<%# Bind("[Mothers Name]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile No">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_St_Mobile_grd" runat="server" Text='<%# Bind("[Mobile Number]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                          
                        </Columns>
                        
                    </asp:GridView>
                        </div>
       <div style="padding-top:20px">
           <asp:Button ID="btn_assign_Mentee" OnClick="btn_assign_Mentee_Click"  CssClass="btn btn-primary" runat="server" Text="Assign Mentor" />
       </div> 

                </asp:Panel>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Employee Code">
            <ItemTemplate>
                <asp:Label ID="lbl_No_grid" runat="server" Text='<%# Bind("No_") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Name">
            <ItemTemplate>
                <asp:Label ID="lbl_FullName_grid" runat="server" Text='<%# Bind("[Full Name]") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Department">
            <ItemTemplate>
                <asp:Label ID="lbl_DepttName_grid" runat="server" Text='<%# Bind("[Department Name]") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Date Of Joining">
            <ItemTemplate>
                <asp:Label ID="lbl_DOJ_grid" runat="server" Text='<%# Bind("[Employment Date]","{0:dd MMM yyyy}") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <EmptyDataTemplate>
        There are no records found!
    </EmptyDataTemplate>
</asp:GridView>


                     </td> </tr>

    </table>
</div>
    
                        
                    </asp:Panel>

         <asp:Button ID="Button1" runat="server" Style="display: none" Text="Button" />
         <asp:ModalPopupExtender ID="md_Employee_Count_Details" runat="server" PopupControlID="pnl_md_Mentee" TargetControlID="Button1" BackgroundCssClass="modalBackground"   OkControlID="btn_Close_Employee">           

</asp:ModalPopupExtender>
         <asp:Panel ID="pnl_md_Mentee" runat="server" CssClass="modalPopup" Style="display: none" Height="500px" ScrollBars="Vertical">
             <div class="row">
                 <div class="col-md-4">                  <asp:Label ID="lbl_md_txt" Font-Bold="true"  runat="server" Text=""></asp:Label> </div>
                 <div class="col-md-6">  <asp:LinkButton ID="btn_Excel_Export" runat="server" CssClass="btn button success" OnClick="btn_Excel_Export_Click" >Export in Excel</asp:LinkButton></div>
                 <div class="col-md-2"><asp:LinkButton ID="btn_Close_Employee" runat="server" CssClass="btn button danger" >Close</asp:LinkButton></div>
             </div>

             <div class="row">
                 <div class="col-md-12">  
       
                 <asp:GridView ID="md_grd_Mentee" runat="server" OnPageIndexChanging="md_grd_Mentee_PageIndexChanging" CssClass="gridview" AllowPaging="true" AutoGenerateColumns="false">
                        <Columns>
                            
                            <asp:TemplateField HeaderText="Student Code">
                                <ItemTemplate>                                  
                                    <asp:Label ID="lbl_St_Code_grid" runat="server" Text='<%# Bind("No_") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_St_Name_grid" runat="server" Text='<%# Bind("[Student Name]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date Of Birth">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_St_DOB_grid" runat="server" Text='<%# Bind("[Date of Birth]","{0:dd MMM yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Father's Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_St_Father_grd" runat="server" Text='<%# Bind("[Fathers Name]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mother's Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_St_Mother_grd" runat="server" Text='<%# Bind("[Mothers Name]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile No">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_St_Mobile_grd" runat="server" Text='<%# Bind("[Mobile Number]") %>'></asp:Label>
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
                    </asp:GridView>

                     </div>
                 </div>
         </asp:Panel>




    </div>
</asp:Content>

