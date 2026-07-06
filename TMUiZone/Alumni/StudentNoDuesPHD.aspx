<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentNoDuesPHD.aspx.cs" Inherits="StudentNoDuesPHD" MasterPageFile="~/Alumni/IndexMaster.master" %>

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
                 <label class="field-label">No Dues Id:</label>
                 <asp:TextBox ID="txtPhdNoDuesId" runat="server" CssClass="form-control input-box" Enabled="false"></asp:TextBox>
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
                 <label class="field-label">Registration No. (optional):</label>
                 <asp:TextBox ID="txtPhdRegNo" runat="server" CssClass="form-control input-box" Enabled="false"></asp:TextBox>
             </div>
            <div class="col-md-4">
                <label class="field-label">Date of Registration (optional):</label>
                <asp:TextBox ID="txtPhdDateReg" runat="server" CssClass="form-control input-box" Enabled="false"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label class="field-label">TMRF No.(If TMU Research Fellow) :</label>
                <asp:TextBox ID="txtTMRFNo" runat="server" CssClass="form-control input-box" Enabled="false"></asp:TextBox>
            </div>
           
        </div>
        <div class="row phd-form" style="margin-top: 10px;">
             <div class="col-md-4">
                 <label class="field-label">Date of Joining as Fellow  :</label>
                 <asp:TextBox ID="txtDateofJoiningFellow" runat="server" CssClass="form-control input-box" Enabled="false"></asp:TextBox>
             </div>
            <div class="col-md-4">
                <label class="field-label">Date of Leaving as Fellow   :</label>
                <asp:TextBox ID="txtDateofLeavingFellow" runat="server" CssClass="form-control input-box" Enabled="false"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label class="field-label">Faculty & Discipline :</label>
                <asp:TextBox ID="txtPhdFacultyDiscipline" runat="server" CssClass="form-control input-box" Enabled="false"></asp:TextBox>
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
    <%--<asp:TextBox ID="txtPhdCOEApprovalDate" runat="server" CssClass="form-control input-box" Visible="false"></asp:TextBox>--%>

    <div class="row" style="margin-top: 12px;">
        <div class="col-md-4">
            <label class="field-label">Mobile Number:</label>
            <asp:Panel ID="pnlPhdMobileSend" runat="server">
                <div class="input-group">
                    <asp:TextBox ID="txtPhdMobile" runat="server" CssClass="form-control input-box"></asp:TextBox>
                    <span class="input-group-btn">
                        <asp:Panel ID="pnlPhdMobileSendBtn" runat="server">
                            <asp:Button ID="btnPhdSendOtp" runat="server" CssClass="btn btn-default phd-otp-btn" Text="Send Mobile OTP" OnClick="btnPhdSendOtp_Click" />
                        </asp:Panel>
                    </span>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlPhdMobileVerify" runat="server" Visible="false" Style="margin-top: 6px;">
                <asp:TextBox ID="txtPhdVerifyMobileOTP" runat="server" CssClass="form-control input-box" Placeholder="Enter Mobile OTP"></asp:TextBox>
                <asp:Panel ID="pnlPhdMobileVerifyBtn" runat="server" Visible="false" Style="display: inline-block; margin-left: 8px;">
                    <asp:Button ID="btnPhdVerifyMobile" runat="server" Text="Verify" CssClass="btn btn-default" OnClick="btnPhdVerifyMobile_Click" />
                </asp:Panel>
                <asp:Label ID="lblPhdMSGOTP" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </asp:Panel>
        </div>
        <div class="col-md-4">
            <label class="field-label">E-Mail Address:</label>
            <asp:Panel ID="pnlPhdEmailSend" runat="server">
                <div class="input-group">
                    <asp:TextBox ID="txtPhdEmail" runat="server" CssClass="form-control input-box"></asp:TextBox>
                    <span class="input-group-btn">
                        <asp:Panel ID="pnlPhdEmailSendBtn" runat="server">
                            <asp:Button ID="btnPhdSendEmailOtp" runat="server" CssClass="btn btn-default phd-otp-btn" Text="Send Email OTP" OnClick="btnPhdSendEmailOtp_Click" />
                        </asp:Panel>
                    </span>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlPhdEmailVerify" runat="server" Visible="false" Style="margin-top: 6px;">
                <asp:TextBox ID="txtPhdVerifyEmailOTP" runat="server" CssClass="form-control input-box" Placeholder="Enter Email OTP"></asp:TextBox>
                <asp:Panel ID="pnlPhdEmailVerifyBtn" runat="server" Visible="false" Style="display: inline-block; margin-left: 8px;">
                    <asp:Button ID="btnPhdVerifyEmail" runat="server" Text="Verify" CssClass="btn btn-default" OnClick="btnPhdVerifyEmail_Click" />
                </asp:Panel>
                <asp:Label ID="lblPhdMSGOTPEmail" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </asp:Panel>
        </div>
        <div class="col-md-4">
            <!-- reserved for future inline controls -->
        </div>
    </div>

    <div style="margin-top: 18px">
        <asp:GridView ID="gvPhdFees" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed">
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
                        <asp:LinkButton ID="lnkPayNow" runat="server" Text="Pay Now"   CssClass="btn btn-xs btn-primary"   OnClick="lnkPayNow_Click" 
                            Visible='<%# Eval("Status").ToString().ToLower().Contains("pending") %>'
                            CommandArgument='<%# Eval("FeeDescription") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <div style="margin-top: 12px; text-align: right">
        <asp:Button ID="btnPhdSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnPhdSave_Click" Visible="false" />
    </div>
    </div>
    <!-- end PhD panel moved outside divnodues so it can be shown independently -->
</asp:Content>
