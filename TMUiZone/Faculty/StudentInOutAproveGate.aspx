<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentInOutAproveGate.aspx.cs" Inherits="Faculty_StudentInOutAproveGate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script type="text/javascript">
         function showPopup2() {
             $find("mpe2").show();
         }
         function closePopup2() {
             $find("mpe2").hide();   // modal hide
         }
         function showPopup3() {
             $find("mpe3").show();
         }
         function closePopup3() {
             $find("mpe3").hide();   // modal hide
         }

         function copyMonthsToHidden() {
             var fromVal = document.getElementById('ContentPlaceHolder1_fromMonth').value;
             var toVal = document.getElementById('ContentPlaceHolder1_toMonth').value;

             // Find hidden fields by ClientID from server controls:
             document.getElementById('<%= hfFromMonth.ClientID %>').value = fromVal;
            document.getElementById('<%= hfToMonth.ClientID %>').value = toVal;
         }
     </script>
    <style>
                .section {
    display: flex;
    flex-wrap: wrap;
    gap: 15px;
    margin-bottom: 15px;
}

.field {
    display: flex;
    align-items: center;   /* Label aur input ek line me center align honge */
    width: 48%;            /* 2 field per row */
}

.field label {
    width: 120px;          /* Label ki fixed width */
    font-weight: bold;
    color: #333;
}

.field input {
    flex: 1;               /* Input box remaining width le lega */
    padding: 6px 8px;
    border: 1px solid #ccc;
    border-radius: 5px;
}

.field select {
    flex: 1;               /* Input box remaining width le lega */
    padding: 6px 8px;
    border: 1px solid #ccc;
    border-radius: 5px;
}
.submit-btn {
    background: #28a745;
    color: #fff;
    border: none;
    padding: 8px 14px;
    border-radius: 6px;
    cursor: pointer;
}

.submit-btn:hover {
    background: #218838;
}
   #ContentPlaceHolder1_getTDSRequestList td, #ContentPlaceHolder1_getTDSRequestList th {
    text-align: center !important;
    font-size:10px;
} .grid-cell-border {
        border-collapse: collapse;
        width: 100%;
    }

    .grid-cell-border th,
    .grid-cell-border td {
        border: 1px solid black;
        padding: 6px;
        text-align: left;
    }  .grid-action-button {
        display: inline-block;
        margin-right: 5px;
        font-size: 12px;
        padding: 2px 6px;
    }
   /* base style for your table */
.mytable {
  border-collapse: collapse;      /* merge borders */
  width: 100%;                    /* optional full width */
}

/* style for headers */
.mytable th {
  border: 1px solid #000;
  background-color: #f2f2f2;      /* light grey header background */
  padding: 8px 15px;
  text-align: left;
}

/* style for data cells */
.mytable td {
  border: 1px solid #000;
  padding: 6px 15px;
}
                .page {
  background: #ffffff;
  border: 1px solid #cbd5e1;  /* light border */
  border-radius: 12px;
  padding: 20px;
  margin: 10px 0;
  position: relative;
  box-shadow: 0 5px 15px rgba(0,0,0,0.05);
}
              .page header {
  display: grid;
  grid-template-columns: 80px 1fr 100px;
  align-items: center;
  gap: 16px;
  margin-bottom: 16px;
}

.page .logo {
  width: 80px;
  height: 80px;
  border-radius: 8px;
  display:flex;
  align-items:center;
  justify-content:center;
  font-size: 11px;
  font-weight: 600;
  color: #64748b;  /* slate */
}
.page .title {
  text-align: center;
}
.page .title h1 {
  margin: 0;
  font-size: 20px;
  font-weight: 700;
  color: #0f172a; /* dark */
}
.page .title p {
  margin: 0;
  font-size: 12px;
  color: #64748b;
}
.page .qr {
  width: 100px;
  height: 100px;
  border: 2px dashed #cbd5e1;
  border-radius: 8px;
  display:flex;
  align-items:center;
  justify-content:center;
  font-size: 10px;
  color: #94a3b8;
  justify-self:end;
}
 .page .badge {
  display:inline-block;
  background: #1d4ed8; /* blue */
  color: #fff;
  font-weight: 600;
  font-size: 12px;
  padding: 6px 12px;
  border-radius: 50px;
  margin-bottom: 6px;
}

.page .notice {
    text-align:center;
  font-size: 12px;
  color: #64748b;
}
   .page .meta {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 10px;
}
.page .meta .field1 {
  border-radius: 8px;
  padding: 8px 10px;
}
.page .meta .field1 label {
  display:block;
  font-size: 11px;
  color: #64748b;
  margin-bottom: 3px;
}
.page .meta .field1 .value {
  font-weight: 600;
  font-size: 11px;
  color: #0f172a;
}
.page table.details {
  width: 100%;
  border-collapse: collapse;
  margin-bottom: 16px;
}
.page table.details th,
.page table.details td {
  border: 1px solid #cbd5e1;
  padding: 8px 10px;
  font-size: 13px;
}
.page table.details th {
  background: #f8fafc;
  font-weight: 600;
  width: 30%;
  color: #0f172a;
}
.page .sign-row {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 20px;
  margin-top: 20px;
}
.page .sig {
  text-align: center;
  padding-top: 15px;
  border-top: 1px solid #cbd5e1;
  margin-top: 20px;
}
.page .sig span {
  display:block;
  margin-top: 4px;
  font-size: 11px;
  color: #64748b;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" />

<asp:UpdatePanel ID="upModal" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
   <asp:LinkButton ID="lnkDummy2" runat="server" Style="display:none;"></asp:LinkButton> 
<!-- Modal Extender -->
<asp:ModalPopupExtender ID="ModalPopupExtender2" BehaviorID="mpe2" runat="server" PopupControlID="pnlPopup2" TargetControlID="lnkDummy2" BackgroundCssClass="modalBackground" CancelControlID="btnHide2">
</asp:ModalPopupExtender>
<!-- Modal Panel -->
<asp:Panel ID="pnlPopup2" runat="server" CssClass="modalPopup" Style="display:none;width:700px;">
    <div class="header">
        <b><asp:Label ID="lblNotification2" runat="server" Text="Out Pass Request"></asp:Label></b>
        <div class="close">
            <asp:Button ID="btnHide2" runat="server" Text="X" style="padding:0px;" OnClientClick="closePopup2(); return false;" />
        </div>
    </div>

   <div class="body">
    <div class="section">
            <div class="field">
                <label>Student No</label>
                 <asp:TextBox ID="txtStudentNo" runat="server" Enabled="false"></asp:TextBox>
            </div>
            <div class="field">
                <label>Name</label>
                <asp:TextBox ID="txtStudentName" runat="server" Enabled="false"></asp:TextBox>
            </div>
                 <div class="field">
                     <label>Department</label>
                     <asp:TextBox ID="txtDepartment" runat="server" Enabled="false"></asp:TextBox>
                      <asp:HiddenField ID="txtDepartmentCode" runat="server" ></asp:HiddenField>
                 </div>
            <div class="field">
                <label>Designation </label>
                <asp:TextBox ID="txtDesignation" runat="server" Enabled="false"></asp:TextBox>
                <asp:HiddenField ID="txtDesignationCode" runat="server" ></asp:HiddenField>
                 <asp:HiddenField ID="txtHODCode" runat="server" ></asp:HiddenField>
                 <asp:HiddenField ID="txtHRCode" runat="server" ></asp:HiddenField>
            </div>
           <div class="field">
                <label>From Month</label>
               <asp:TextBox ID="fromMonth" runat="server" TextMode="Month"></asp:TextBox>
               <asp:HiddenField ID="hfFromMonth" runat="server" />

            </div>
            <div class="field">
                <label>To Month</label>
                <asp:TextBox ID="toMonth" runat="server" TextMode="Month"></asp:TextBox>
                <asp:HiddenField ID="hfToMonth" runat="server" />
             </div>
  </div>

    <asp:Button ID="btnSubmit11" runat="server" Text="Submit" CssClass="submit-btn"  style="margin-bottom:15px;"  OnClientClick="copyMonthsToHidden();"/>
</div>

</asp:Panel>


   <asp:LinkButton ID="lnkDummy3" runat="server" Style="display:none;"></asp:LinkButton> 
<!-- Modal Extender -->
<asp:ModalPopupExtender ID="ModalPopupExtender3" BehaviorID="mpe3" runat="server" PopupControlID="pnlPopup3" TargetControlID="lnkDummy3" BackgroundCssClass="modalBackground" CancelControlID="btnHide3">
</asp:ModalPopupExtender>
<!-- Modal Panel -->
<asp:Panel ID="pnlPopup3" runat="server" CssClass="modalPopup" Style="display:none;width:1200px;">
    <div class="header">
        <b><asp:Label ID="lblNotification3" runat="server" Text="Out Pass Request List"></asp:Label></b>
        <div class="close">
            <asp:Button ID="btnHide3" runat="server" Text="X" style="padding:0px;" OnClientClick="closePopup3(); return false;" />
        </div>
    </div>

   <div class="body">  
       
  </div>
</asp:Panel>

   </ContentTemplate>
</asp:UpdatePanel>
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Student Out Pass Aprove" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
  <fieldset class="boxBodyInner">
    <asp:Panel ID="pnlMain" runat="server">
         <table>
     <tr>
         <td>
             <asp:TextBox ID="txtGatePassNo" runat="server"  Width="200px" />
         </td>
           <td>
             <asp:Button ID="btnShow" runat="server" Text="Search" CssClass="btn btn-primary" style="margin-left: 15px;" OnClick="btnGatePassNo_Click"
                 ValidationGroup="g1" />
         </td>
         <td style="width: 300px;">
        </td>
         <td>
            Form <asp:TextBox ID="FromDate" runat="server"  Width="110px" Height="30px" TextMode="Date" ></asp:TextBox>
             To <asp:TextBox ID="ToDate" runat="server"  Width="110px" Height="30px" TextMode="Date" ></asp:TextBox> 
              
         </td>
         
           <td><asp:DropDownList ID="ddlStatus" runat="server" Width="100px" Height="30px" style="margin-left: 5px;">
              <asp:ListItem Selected="True" Text="All" Value="All"></asp:ListItem>
               <asp:ListItem Text="In" Value="In"></asp:ListItem>
               <asp:ListItem Text="Out" Value="Out"></asp:ListItem>
              </asp:DropDownList> </td>
         <td> <asp:Button ID="Button2" runat="server" 
    Text='Search' 
    CssClass="btn btn-primary" 
    OnClick="btnSearch_Click"
    style="margin-left: 15px;" />

                </td>
          <td><asp:Button ID="Button1" runat="server" Text="Export to Excel" CssClass="btn btn-primary" style="margin-left: 15px;" OnClick="btnExporttoExcel_Click"/></td>
     </tr>
 </table>
    </asp:Panel>
</fieldset>
    <br />
    <div style="height:500px;overflow:scroll">
         <asp:GridView ID="getGatePassRequestList" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="Black"
    BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="100%" Font-Size="12px" CssClass="grid-cell-border" OnRowDataBound="getGatePassRequestList_RowDataBound"
    GridLines="Both" EmptyDataText="There are no data records to display." 
    AllowSorting="true">
 <%--   <AlternatingRowStyle BackColor="#F7F7F7" /> --%>
    <Columns>
     <asp:TemplateField HeaderText="Out Pass No">
    <ItemTemplate>
        <asp:Label ID="Label2" runat="server" Text='<%# Eval("GatePassNo") %>'></asp:Label>
    </ItemTemplate>
    <ItemStyle Width="10%" />
</asp:TemplateField>
        <asp:TemplateField HeaderText="Student Code">
            <ItemTemplate>
                 <asp:HiddenField ID="StudentID" runat="server" Value='<%# Eval("ID") %>'></asp:HiddenField>
               <asp:Label ID="StudentNo" runat="server" Text='<%# Eval("No_") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="7%" />
        </asp:TemplateField>

             
                      <asp:TemplateField HeaderText="Student Name">
    <ItemTemplate>
        <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("StudentName") %>'></asp:Label>
    </ItemTemplate>
    <ItemStyle Width="7%" />
</asp:TemplateField>  
                              <asp:TemplateField HeaderText="Hostel">
    <ItemTemplate>
        <asp:Label ID="lblHostel" runat="server" Text='<%# Eval("Hostel") %>'></asp:Label>
    </ItemTemplate>
    <ItemStyle Width="7%" />
</asp:TemplateField>  
          <asp:TemplateField HeaderText="Form Date/Time">
      <ItemTemplate>
          <asp:Label ID="FormDate" runat="server" Text='<%# DateTime.Parse(Eval("FormDate").ToString()).ToString("dd-MM-yyyy hh:mm tt") %>'></asp:Label>
      </ItemTemplate>
      <ItemStyle Width="7%" />
  </asp:TemplateField>

    <asp:TemplateField HeaderText="To Date/Time">
    <ItemTemplate>
          <asp:Label ID="ToDate" runat="server" Text='<%# DateTime.Parse(Eval("ToDate").ToString()).ToString("dd-MM-yyyy hh:mm tt") %>'></asp:Label>
    </ItemTemplate>
    <ItemStyle Width="7%" />
</asp:TemplateField>
     <asp:TemplateField HeaderText="No Of Hours">
    <ItemTemplate>
          <asp:Label ID="NoOfHours" runat="server" Text='<%# Eval("NoOfHours") %>'></asp:Label>
    </ItemTemplate>
    <ItemStyle Width="7%" />
</asp:TemplateField>
             <asp:TemplateField HeaderText="Out Status">
    <ItemTemplate>
          <asp:Label ID="OutStatus" runat="server" Text='<%# Eval("OutTimeText") %>'></asp:Label>
    </ItemTemplate>
    <ItemStyle Width="7%" />
</asp:TemplateField>
            <asp:TemplateField HeaderText="Out Time">
    <ItemTemplate>
           <asp:Label ID="OutTime1" runat="server" 
            Text='<%# 
                (Eval("OutTime1") != DBNull.Value && Eval("OutTime1") != null) 
                ? Convert.ToDateTime(Eval("OutTime1")).ToString("dd-MM-yyyy hh:mm tt") 
                : "" %>'>
        </asp:Label>
    </ItemTemplate>
    <ItemStyle Width="7%" />
</asp:TemplateField>
      <asp:TemplateField HeaderText="In Status">
    <ItemTemplate>
          <asp:Label ID="InStatus" runat="server" Text='<%# Eval("InTimeText") %>'></asp:Label>
    </ItemTemplate>
    <ItemStyle Width="7%" />
</asp:TemplateField>
                    <asp:TemplateField HeaderText="In Time">
    <ItemTemplate>
            <asp:Label ID="InTime1" runat="server" 
            Text='<%# 
                (Eval("InTime1") != DBNull.Value && Eval("InTime1") != null) 
                ? Convert.ToDateTime(Eval("InTime1")).ToString("dd-MM-yyyy hh:mm tt") 
                : "" %>'>
        </asp:Label>
    </ItemTemplate>
    <ItemStyle Width="7%" />
</asp:TemplateField>
  <asp:TemplateField HeaderText="">
    <ItemTemplate>
        <div style="white-space: nowrap;">
            <a href='#' onclick="ViewUserDetails('<%# Eval("ID") %>');"><u>View</u></a>
        </div>
    </ItemTemplate>
    <ItemStyle Width="7%" />
</asp:TemplateField>
    </Columns>
    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" />
    <AlternatingRowStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" />
    <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"/>
    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" />
    
 <%--   <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />--%>
    <SortedAscendingCellStyle BackColor="#F4F4FD" />
    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
    <SortedDescendingCellStyle BackColor="#D8D8F0" />
    <SortedDescendingHeaderStyle BackColor="#3E3277" />

</asp:GridView>
        </div>
  <div class="modal fade" id="userDetailModal" tabindex="-1" role="dialog">
  <div class="modal-dialog modal-lg" role="document">
    <div class="modal-content">
     <div class="modal-header">
    <h5 class="modal-title" style="margin:0;line-height:1.5;width: 106px;">Student Details</h5>
    <button type="button" class="close" style="margin-top:-20px;" data-dismiss="modal" aria-label="Close">
        <span aria-hidden="true">×</span>
    </button>
</div>
      <div class="modal-body">
          <div style="max-height: 380px; overflow-y: auto;">
              <div style="display:flex; align-items:flex-start; gap:20px;">
   <table class="mytable" style="width:600px; border:1px solid black; border-collapse: collapse;">
  <tr>
    <th style="border:1px solid black; padding:5px;">Out Pass No</th>
    <td id="gatePassNo" style="border:1px solid black; padding:5px;"></td>
  </tr>

  <tr>
    <td colspan="2" style="padding:0; border:none;">
      <table style="width:100%; border-collapse: collapse;">
        <tr>
          <th style="border:1px solid black; padding:5px;">ST No</th>
          <th style="border:1px solid black; padding:5px;">Name</th>
          <th style="border:1px solid black; padding:5px;">Academic Year</th>  
        </tr>
        <tr>
          <td id="studentNo" style="border:1px solid black; padding:5px;"></td>
          <td id="studentName" style="border:1px solid black; padding:5px;"></td>
          <td id="acadmicYear" style="border:1px solid black; padding:5px;"></td>
        </tr>
      </table>
    </td>
  </tr>

  <tr>
    <th style="border:1px solid black; padding:5px;">Program</th>
    <td id="className" style="border:1px solid black; padding:5px;"></td>
  </tr>
  <tr>
    <th style="border:1px solid black; padding:5px;">College</th>
    <td id="college" style="border:1px solid black; padding:5px;"></td>
  </tr>
  <tr>
    <th style="border:1px solid black; padding:5px;">Hostel</th>
    <td id="hostel" style="border:1px solid black; padding:5px;"></td>
  </tr>
  <tr>
    <th style="border:1px solid black; padding:5px;">Room No</th>
    <td id="roomNo" style="border:1px solid black; padding:5px;"></td>
  </tr>     
  <tr>
    <th style="border:1px solid black; padding:5px;">From Date</th>
    <td id="fromDate" style="border:1px solid black; padding:5px;"></td>
  </tr>
  <tr>
    <th style="border:1px solid black; padding:5px;">To Date</th>
    <td id="toDate" style="border:1px solid black; padding:5px;"></td>
  </tr>
  <tr>
    <th style="border:1px solid black; padding:5px;">No. of Hours</th>
    <td id="noOfHours" style="border:1px solid black; padding:5px;"></td>
  </tr>
  <tr>
    <th style="border:1px solid black; padding:5px;">Reason</th>
    <td id="reason" style="border:1px solid black; padding:5px;"></td>
  </tr>
  <tr>
    <th style="border:1px solid black; padding:5px;">Warden Remark</th>
    <td id="wardenRemark" style="border:1px solid black; padding:5px;"></td>
  </tr>
</table>


    
 <div>
    <img id="studentImage" src="student.jpg" alt="Student Image" style="width:250px; height:350px; border:1px solid black;"/>
  </div> </div>
          </div>
      </div>
       <div class="modal-footer">
           <asp:HiddenField ID="hfstudentNo" runat="server" />
           <asp:HiddenField ID="hfgatePassNo" runat="server" />
           <asp:HiddenField ID="hfOutTime" runat="server" />
           
    <div style="width: 100%; display: flex; justify-content: left; gap: 10px;">
        <asp:Button ID="btnOut" runat="server" Text="Out Entry" 
    style="padding: 10px; font-size: 20px; width: 270px;Height:50px" 
    CssClass="btn btn-danger" OnClick="btnOut_Click" />

<asp:Button ID="btnIn" runat="server" Text="In Entry" 
    style="padding: 10px; font-size: 20px; width: 270px;Height:50px" 
    CssClass="btn btn-primary" OnClick="btnIn_Click" />
 </div>
</div>

    </div>
  </div>
  </div>

      <div class="modal fade" id="userDetailModal1" tabindex="-1" role="dialog">
  <div class="modal-dialog modal-lg" role="document">
    <div class="modal-content">
     <div class="modal-header">
    <h5 class="modal-title" style="margin:0;line-height:1.5;width: 106px;">Student Details</h5>
    <button type="button" class="close" style="margin-top:-20px;" data-dismiss="modal" aria-label="Close">
        <span aria-hidden="true">×</span>
    </button>
</div>
     <div class="modal-body">
        <div>  
             <div id="printArea">
      <div class="page">
            <header>
              <div class="logo"><image src="../logo/logo123.png" style="width: 210px;margin-left: 90px;" ></image></div>
              <div class="title" style="margin-left: 95px;">
                <h1>TEERTHANKER MAHAVEER UNIVERSITY</h1>
                <p>Student Exit Out Pass</p>
              </div>
             <%-- <div class="qr" id="qrcode">QR Code</div>--%>
              
            </header>

           <p class="notice">This document authorizes the student to exit the campus premises as per details mentioned below.</p>

            <div class="meta">
              <div class="field1">
                <label>Out Pass No</label>
                <div class="value" id="gatePassNo1"></div>
              </div>
              <div class="field1">
                <label>Student Name</label>
                <div class="value" id="studentName1"></div>
              </div>
               <div class="field1">
                <label>Program</label>
                <div class="value" id="class1"></div>
              </div>
            </div>

            <table class="details">
                   <tr>
                          <th>College</th>
                            <td id="college1"></td>
                    </tr>
                                    <tr>
                          <th>Hostel</th>
                            <td id="hostel1"></td>
                    </tr>   
                   <tr>
                <th>From Date</th>
                <td  id="fromDate1"></td>
              </tr>
              <tr>
                <th>To Date</th>
                <td  id="toDate1"></td>
              </tr>
              <tr>
                <th>No. of Hours</th>
                <td  id="noOfHours1"></td>
              </tr>
              <tr>
                <th>Reason</th>
                <td  id="reason1"></td>
              </tr>
                 <tr>
                   <th>Student Mobile No.</th>
                   <td  id="studentMobileNo1"></td>
                 </tr>
                <tr>
                  <th>Father Mobile No.</th>
                  <td  id="fatherMobileNo1"></td>
                </tr>
                 <tr>
                   <th>Address</th>
                   <td  id="studentAddress1"></td>
                 </tr>
            </table>

            <%--<div class="sign-row">
              <div class="sig">Warden Signature<span>Warden</span></div>
                 <div></div>
              <div> <svg style="width: 230px;" id="qrcode"></svg></div>
             </div>--%>

          </div>
            </div>
   <div class="modal-footer">
        <asp:HiddenField runat="server" ID="hidEmpID" />
       <%--  <button type="button" class="btn btn-primary" onclick="printDiv('printArea')">Print</button>--%>
         <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
      </div>
    </div>
  </div> 
  </div>
</div>
</div>
 <script>
     document.addEventListener("DOMContentLoaded", function () {
         const txtGatePassNo = document.getElementById("ContentPlaceHolder1_txtGatePassNo");

         // Focus on textbox
         txtGatePassNo.focus();

         // Trigger on Enter key
         txtGatePassNo.addEventListener("keydown", function (e) {
             if (e.key === "Enter") {
                 callMyMethod(txtGatePassNo.value);
             }
         });
     });
     function parseDateTime(dateTimeString) {
         // Example: "17-11-2025 17:35:00"
         var [datePart, timePart] = dateTimeString.split(" ");

         var [day, month, year] = datePart.split("-");
         var [hour, minute, second] = timePart.split(":");

         return new Date(year, month - 1, day, hour, minute, second);
     }
     function callMyMethod(gatePassNo) {
         $.ajax({
             url: 'StudentInOutAproveGate.aspx/GetStudentDetailByGatePassNo',
             type: 'POST',
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             data: JSON.stringify({ gatePassNo: gatePassNo }),
             success: function (result) {
                 const data = result.d;

                 if (!data) {
                     document.getElementById('ContentPlaceHolder1_txtGatePassNo').value = "";
                     alert("No data found.");
                     return;
                 }
                 var fromDate = parseDateTime(data.FormDate);
                 var toDate = parseDateTime(data.ToDate);
                 var now = new Date();   // current date + time
                 if ((now < fromDate || now > toDate) && data.OutTime === '0' && data.InTime === '0') {
                     alert("Gatepass time expired or not valid at this time!");
                     return;
                 }

                else if (data.OutTime === '1' && data.InTime === '1') {
                     document.getElementById('ContentPlaceHolder1_txtGatePassNo').value = "";
                     alert("This GatePass is already processed.");
                     return;
                 } else {
                     // Set text fields
                     $("#studentNo").text(data.No_);
                     $("#ContentPlaceHolder1_hfstudentNo").val(data.No_);
                     $("#studentName").text(data.StudentName);
                     $("#acadmicYear").text(data.AcadmicYear);
                     $("#gatePassNo").text(data.GatePassNo);
                     $("#ContentPlaceHolder1_hfgatePassNo").val(data.GatePassNo);
                     $("#className").text(data.Class);
                     $("#college").text(data.College);
                     $("#hostel").text(data.Hostel);
                     $("#roomNo").text(data.RoomNo);
                     $("#fromDate").text(data.FormDate);
                     $("#toDate").text(data.ToDate);
                     $("#noOfHours").text(data.NoOfHours);
                     $("#reason").text(data.Reason);
                     $("#wardenStatus").text(data.WardenStatus);
                     $("#wardenRemark").text(data.WardenRemark);
                     $("#createdBy").text(data.CreatedBy);
                     $("#createdAt").text(data.CreatedAt.split(' ')[0]);
                     $("#updatedBy").text(data.UpdatedBy);
                     $("#updatedAt").text(data.UpdatedAt);
                     $("#ContentPlaceHolder1_hfOutTime").val(data.OutTime);



                     if (data.OutTime === '1') {
                         document.getElementById('ContentPlaceHolder1_btnOut').disabled = true;
                     }
                     if (data.InTime === '1') {
                         document.getElementById('ContentPlaceHolder1_btnIn').disabled = true;
                     }

                     // Set image safely
                     let img = document.getElementById("studentImage");
                     if (data.StudentImage) {
                         img.src = "data:image/png;base64," + data.StudentImage;
                     } else {
                         img.src = "/path/to/default-image.png"; // optional default
                     }

                     // Show the modal once all data is set
                     $("#userDetailModal").modal("show");
                 }
             },
             error: function () {
                 alert('Error loading student details.');
             }
         });
     }
     function ViewUserDetails(No) {
         $.ajax({
             url: 'StudentInOutAproveGate.aspx/GetStudentGatePassList',
             type: 'POST',
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             data: JSON.stringify({ StudentNo: No }),
             success: function (result) {
                 const data = result.d;

                 if (!data) {
                     alert("No Out Pass found.");
                     return;
                 }

                 // Fill modal fields
                 $("#studentName1").text(data.StudentName + " (" + data.No + ")");
                 $("#gatePassNo1").text(data.GatePassNo);
                 $("#dateTime1").text(data.CreatedAt);
                 $("#class1").text(data.Class + " , " + data.AcademicYear);
                 $("#college1").text(data.College);
                 $("#hostel1").text(data.Hostel);
                 $("#roomNo1").text(data.RoomNo);
                 $("#studentMobileNo1").text(data.StudentMobileNo);

                 $("#fromDate1").text(data.FormDate + "                                                            " + data.OutTime1 + "");
                     $("#toDate1").text(data.ToDate + "                                                            " + data.InTime1 + "");

                
                 $("#fatherMobileNo1").text(data.FatherMobileNo);
                 $("#studentAddress1").text(data.StudentAddress);
                 $("#noOfHours1").text(data.NoOfHours);
                 $("#reason1").text(data.Reason);                
                 $("#userDetailModal1").modal("show");
             },
             error: function () {
                 alert('Error loading Out Pass details.');
             }
         });
     }
 </script>


</asp:Content>


