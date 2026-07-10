<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CRACMeetingPHD.aspx.cs" Inherits="Alumni_CRACMeeting" MasterPageFile="~/Alumni/IndexMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .phd-form .input-box {
            height: 34px;
            font-size: 13px;
            border-radius: 4px;
        }

        .phd-form .field-label {
            font-weight: 600;
            display: block;
            margin-bottom: 6px;
        }

        .section-header {
            font-size: 18px;
            font-weight: 600;
            margin-bottom: 12px;
        }

        .phd-otp-btn {
            margin-left: 8px;
        }

        .phd-container {
            background: #fff;
            padding: 15px;
            border-radius: 4px;
        }

        .input-group-btn .btn {
            margin-left: 6px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <!-- PhD panel moved outside divnodues so it can be shown independently -->
    <div id="divPhd" runat="server" visible="false" class="phd-container">
        <div class="section-header">PHD Research Scholar - No Dues Form</div>

        <div class="row phd-form">
            <div class="col-md-4">
                <label class="field-label">Name of Research Scholar :</label>
                <asp:TextBox ID="txtPhdName" runat="server" CssClass="form-control input-box" Enabled="false"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label class="field-label">Employee Code (If Internal Faculty Member) :</label>
                <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control input-box" Enabled="false"></asp:TextBox>
            </div>             
             <div class="col-md-4">
                 <label class="field-label">CRAC Meeting :</label>
                 <asp:DropDownList ID="ddlCRACMeeting" runat="server" CssClass="form-control input-box" AutoPostBack="true" OnSelectedIndexChanged="ddlCRACMeeting_SelectedIndexChanged">
                     <asp:ListItem Text="-- Select --" Value="" />
                     <asp:ListItem Text="I-CRAC" Value="1" />
                     <asp:ListItem Text="II-CRAC" Value="2" />
                     <asp:ListItem Text="III-CRAC" Value="3" />
                     <asp:ListItem Text="IV-CRAC" Value="4" />
                     <asp:ListItem Text="V-CRAC" Value="5" />
                     <asp:ListItem Text="VI-CRAC" Value="6" />
                 </asp:DropDownList>
            </div>
        </div>

        <div class="row phd-form" style="margin-top: 10px;">
            <div class="col-md-4">
                <label class="field-label">Father's Name:</label>
                <asp:TextBox ID="txtPhdFather" runat="server" CssClass="form-control input-box" Enabled="false"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label class="field-label">Student No.:</label>
                <asp:TextBox ID="txtPhdStudentNo" runat="server" CssClass="form-control input-box" Enabled="false"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label class="field-label">Enrollment No.:</label>
                <asp:TextBox ID="txtPhdEnroll" runat="server" CssClass="form-control input-box" Enabled="false"></asp:TextBox>
            </div>  
        </div>

       
     

        <div class="row phd-form" style="margin-top: 10px;">   
             <div class="col-md-4">
                 <label class="field-label">College/Dept:</label>
                 <asp:TextBox ID="txtPhdCollegeDept" runat="server" CssClass="form-control input-box" Enabled="false"></asp:TextBox>
             </div>  
            <div class="col-md-4">
                <label class="field-label">Course Name:</label>
                <asp:TextBox ID="txtPhdCourseName" runat="server" CssClass="form-control input-box" Enabled="false"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label class="field-label">Gender:</label>
                <asp:TextBox ID="txtPhdGender" runat="server" CssClass="form-control input-box" Enabled="false"></asp:TextBox>
            </div>            
        </div>   

    <!-- hidden fields used by code-behind when present -->
    <asp:TextBox ID="txtPhdAcademicYear1" runat="server" CssClass="form-control input-box" Visible="false"></asp:TextBox>


    <div style="margin-top: 18px">
        <asp:GridView ID="gvPhdFees" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed" OnRowDataBound="gvPhdFees_RowDataBound">
            <Columns>
                <asp:BoundField HeaderText="Fee Description" DataField="FeeDescription" />
                 <asp:BoundField HeaderText="Fee Amount (Rs.)" DataField="FeeAmount" DataFormatString="{0:N2}" />
                <asp:BoundField HeaderText="Paid Amount (Rs.)" DataField="PaidAmount" DataFormatString="{0:N2}" />
                 <asp:BoundField HeaderText="Pending Amount (Rs.)" DataField="PendingAmount" DataFormatString="{0:N2}" />
                 <asp:BoundField HeaderText="Status" DataField="Status" />
                <asp:BoundField HeaderText="Receipt No." DataField="ReceiptNo" />
                <asp:BoundField HeaderText="Due Date" DataField="DueDate" />
                <asp:BoundField HeaderText="Payment Date" DataField="PaymentDate" />               
                <asp:BoundField HeaderText="Academic Year" DataField="AcademicYear" />
                 <asp:BoundField HeaderText="Admitted Year" DataField="Admitted Year" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkPayNow" runat="server" Text="Pay Now" CssClass="btn btn-xs btn-primary" OnClick="lnkPayNow_Click"
                            CommandArgument='<%# Eval("FeeDescription") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <div style="margin-top: 12px; text-align: right">
        <asp:Button ID="btnPhdSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnPhdSave_Click" Visible="true" />
    </div>
    </div>
    <!-- end PhD panel moved outside divnodues so it can be shown independently -->
</asp:Content>
