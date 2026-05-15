<%@ Page Title="" Language="C#" MasterPageFile="~/Application/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentGatePass.aspx.cs" Inherits="Application_StudentGatePass" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <script src="https://cdn.jsdelivr.net/npm/jsbarcode@3.11.6/dist/JsBarcode.all.min.js"></script>


     <script type="text/javascript">
         //document.addEventListener("contextmenu", function (e) {
         //    e.preventDefault();
         //});
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
          
         function ViewUserDetails(No) {
             $.ajax({
                 url: 'StudentGatePass.aspx/GetStudentGatePassList',
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
                     $("#studentName").text(data.StudentName + " (" + data.No + ")");
                     $("#gatePassNo").text(data.GatePassNo);
                     $("#dateTime").text(data.CreatedAt); 
                     $("#class").text(data.Class + " , " + data.AcademicYear);
                     $("#college").text(data.College);
                     $("#hostel").text(data.Hostel);
                     $("#roomNo").text(data.RoomNo);
                     $("#studentMobileNo").text(data.StudentMobileNo);

                     $("#fromDate").text(data.FormDate);
                     $("#toDate").text(data.ToDate);

                     $("#fatherMobileNo").text(data.FatherMobileNo);
                     $("#studentAddress").text(data.StudentAddress);                   
                     $("#noOfHours").text(data.NoOfHours);
                     $("#reason").text(data.Reason);
                     $("#qrcode").empty(); // purana QR clear karne ke liye
                     new   JsBarcode("#qrcode", data.GatePassNo, {
                         format: "CODE128",     // barcode type
                         lineColor: "#000000",  // color
                         width: 1,              // har line ki width
                         height: 50,            // barcode height (QR ke equal)
                         displayValue: true     // neeche value show kare
                     });
                     // Show modal
                     if (data.WardenStatus == 1 && data.WardenStatus == 1 && data.InTime == 0)
                     $("#userDetailModal").modal("show");
                 },
                 error: function () {
                     alert('Error loading Out Pass details.');
                 }
             });
         }
         function printDiv(divId) {
             var divContents = document.getElementById(divId).innerHTML;

             // ✅ Add all required CSS here (inline)
             var styles = `
             <style>
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
.page .meta .field {
  border-radius: 8px;
  padding: 8px 10px;
}
.page .meta .field label {
  display:block;
  font-size: 11px;
  color: #64748b;
  margin-bottom: 3px;
}
.page .meta .field .value {
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
    `;

             // ✅ Open print window with injected CSS + HTML
             var win = window.open('', '', 'height=700,width=900');
             win.document.write('<html><head><title>Out Pass</title>');
             win.document.write(styles); // inject CSS here
             win.document.write('</head><body>');
             win.document.write(divContents);
             win.document.write('</body></html>');
             win.document.close();
             win.print();
         }
         function updateHiddenFields() {
             var fromDateStr = document.getElementById("txtfromDate").value;
             var toDateStr = document.getElementById("txtToDate").value;

             // Hidden fields update
             document.getElementById('ContentPlaceHolder1_hiddenfromDate').value = fromDateStr;
             document.getElementById('ContentPlaceHolder1_hiddenToDate').value = toDateStr;

              var maxTimeNew = document.getElementById('ContentPlaceHolder1_hidMaxTime').value;

              if (fromDateStr) {
                 var now = new Date();
                 var fromParts = fromDateStr.split("T");
                 var from = new Date(fromParts[0] + "T" + fromParts[1]); 
                 var todayStr = now.toISOString().split("T")[0];

                 var maxTimeDateNew = new Date(todayStr + "T" + maxTimeNew);
                 var maxTimeDate = new Date(todayStr + "T" + fromParts[1]);

                 var nowDate = now; 
                  if (from <= now) {
                      alert("From Date/Time cannot be in the past!");
                      document.getElementById("txtfromDate").value = "";
                      document.getElementById('ContentPlaceHolder1_txtNoOfHours').value = "";
                      return;
                  }

                  if (fromParts[0] != todayStr) {
                      alert("Out Pass can be issued only for the same date.");
                      document.getElementById("txtfromDate").value = "";
                      document.getElementById('ContentPlaceHolder1_txtNoOfHours').value = "";
                      return;
                  }               

                 if (maxTimeDateNew < maxTimeDate) {
                     alert("now time is greater than max In/Out time");
                     document.getElementById("txtfromDate").value = "";
                     document.getElementById('ContentPlaceHolder1_txtNoOfHours').value = "";
                     return;
                 }

                              
             }
             if (toDateStr) {
                 var now = new Date();
                 var toParts = toDateStr.split("T");
                 var to = new Date(toParts[0] + "T" + toParts[1]);
                 var todayStr = now.toISOString().split("T")[0];

                 var maxTimeDateNew = new Date(todayStr + "T" + maxTimeNew);
                 var maxTimeDate = new Date(todayStr + "T" + toParts[1]);

                 var nowDate = now; 


                 if (to <= now) {
                     alert("To Date/Time cannot be in the past!");
                     document.getElementById("txtToDate").value = "";
                     document.getElementById('ContentPlaceHolder1_txtNoOfHours').value = "";
                     return;
                 }

                 if (toParts[0] != todayStr) {
                     alert("Out Pass can be issued only for the same date.");
                     document.getElementById("txtToDate").value = "";
                     document.getElementById('ContentPlaceHolder1_txtNoOfHours').value = "";
                     return;
                 }

                 if (maxTimeDateNew < maxTimeDate) {
                     alert("now time is greater than max In/Out time");
                     document.getElementById("txtToDate").value = "";
                     document.getElementById('ContentPlaceHolder1_txtNoOfHours').value = "";
                     return;
                 }

                 
                
             }
    if (fromDateStr && toDateStr) {
        var fromParts = fromDateStr.split("T");
        var toParts = toDateStr.split("T");

        var from = new Date(fromParts[0] + " " + fromParts[1] + ":00");
        var to = new Date(toParts[0] + " " + toParts[1] + ":00");

        if (from > to) {
            alert("From Date/Time should not be after To Date/Time!");
            document.getElementById("txtToDate").value = "";
            document.getElementById('ContentPlaceHolder1_hiddenNoOfHours').value = "";
            document.getElementById('ContentPlaceHolder1_txtNoOfHours').value = "";
            return;
        }

        var diffMinutes = (to - from) / (1000 * 60); // total minutes
        if(diffMinutes < 0){
            alert("From Date/Time should not be later than To Date/Time.");
            document.getElementById('ContentPlaceHolder1_txtNoOfHours').value = "0h 0m";
            return;
        }

        var days = Math.floor(diffMinutes / (60 * 24));
        var remainingMinutes = diffMinutes - (days * 60 * 24);
        var hours = Math.floor(remainingMinutes / 60);
        var minutes = Math.floor(remainingMinutes % 60);

        var parts = [];
        if (days > 0) parts.push(days + "d");
        if (hours > 0) parts.push(hours + "h");
        if (minutes > 0 || parts.length === 0) parts.push(minutes + "m"); 
        
        var diffText = parts.join(" ");

        document.getElementById('ContentPlaceHolder1_hiddenNoOfHours').value = diffText;
        document.getElementById('ContentPlaceHolder1_txtNoOfHours').value = diffText;
             }
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
}

.field label {
    margin-right: 5px;
    font-weight: bold;
    color: #333;
    width: 100px;
}

.field input {
    flex: 1;               /* Input box remaining width le lega */
    padding: 6px 8px;
    border: 1px solid #ccc;
    border-radius: 5px;
    width:190px;
}

.field select {
    flex: 1;               /* Input box remaining width le lega */
    padding: 6px 8px;
    border: 1px solid #ccc;
    border-radius: 5px;
    width:190px;
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
    }
    </style>
    <style>
        /* ===== Out Pass Card Styles ===== */
.page {
  background: #ffffff;
  border: 1px solid #cbd5e1;  /* light border */
  border-radius: 12px;
  padding: 20px;
  margin: 10px 0;
  position: relative;
  box-shadow: 0 5px 15px rgba(0,0,0,0.05);
}

/* Header (logo - title - QR) */
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

/* Badge */
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

/* Meta info fields */
.page .meta {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 10px;
}
.page .meta .field {
  border-radius: 8px;
  padding: 8px 10px;
}
.page .meta .field label {
  display:block;
  font-size: 11px;
  color: #64748b;
  margin-bottom: 3px;
}
.page .meta .field .value {
  font-weight: 600;
  font-size: 11px;
  color: #0f172a;
}

/* Details table */
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

/* Signature row */
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

/* Responsive for smaller screens */
@media (max-width: 768px) {
  .page .meta { grid-template-columns: repeat(2, 1fr); }
  .page header { grid-template-columns: 60px 1fr 80px; }
  .page .qr { width: 80px; height: 80px; }
  .page .sign-row { grid-template-columns: 1fr; gap: 10px; }
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
        <b><asp:Label ID="lblNotification2" runat="server" Text="Student Out Pass Request"></asp:Label></b>
        <div class="close">
            <asp:Button ID="btnHide2" runat="server" Text="X" style="padding:0px;" OnClientClick="closePopup2(); return false;" />
        </div>
    </div>

   <div class="body">
<div class="section">
            <div class="field">
                <label style="text-align: left;margin-left: 5px;">Student No</label>
                 <asp:TextBox ID="txtStudentNo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                <asp:HiddenField ID="hfMobileNumber" runat="server" />
                <asp:HiddenField ID="hfFatherNumber" runat="server" />
                <asp:HiddenField ID="hfFullAddress" runat="server" />
                <asp:HiddenField ID="hfWarden" runat="server" />

                <asp:HiddenField ID="hidFromDate" runat="server" />
                <asp:HiddenField ID="hidToDate" runat="server" />
                <asp:HiddenField ID="hidMaxTime" runat="server" />


            </div>
            <div class="field">
                <label style="text-align: left;margin-left: 5px;">Student Name</label>
                <asp:TextBox ID="txtStudentName" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>
         </div>
<div class="section">
                   <div class="field">
                <label for="txtAcadmicYear" style="text-align: left;margin-left: 5px;">Acadmic Year</label>
                <asp:TextBox ID="txtAcadmicYear" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>

             <div class="field">
                  <label for="txtClass" style="text-align: left;margin-left: 5px;">Program</label>
                  <asp:TextBox ID="txtClass" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
              </div>
 </div>
<div class="section">
               <div class="field">
                  <label for="txtCollege" style="text-align: left;margin-left: 5px;">College</label>
                  <asp:TextBox ID="txtCollege" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
              </div>

              <div class="field">
                  <label for="txtHostel" style="text-align: left;margin-left: 5px;">Hostel</label>
                  <asp:TextBox ID="txtHostel" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
              </div>
     </div>
<div class="section">
              <div class="field">
                  <label for="txtRoomNo" style="text-align: left;margin-left: 5px;">Room No</label>
                  <asp:TextBox ID="txtRoomNo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
              </div>   
    <div class="field">
   <label for="txtfromDate" style="text-align: left;margin-left: 5px;">From Time</label>
   <input id="txtfromDate"  type="datetime-local"  class="form-control" required="" onchange="updateHiddenFields()"/>
   <asp:HiddenField ID="hiddenfromDate" runat="server" />

 </div>
     </div>
<div class="section">
     
            <div class="field">
                <label for="txtToDate" style="text-align: left;margin-left: 5px;">To Time</label>
                <input id="txtToDate" type="datetime-local" required=""  class="form-control" onchange="updateHiddenFields()" />
                <asp:HiddenField ID="hiddenToDate" runat="server" />
            </div>

            <div class="field">
                <label for="txtNoOfHours" style="text-align: left;margin-left: 5px;">No. of Hours</label>
                <asp:TextBox ID="txtNoOfHours" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                 <asp:HiddenField ID="hiddenNoOfHours" runat="server" />
              </div>
          <div class="field">
        <label for="ddlLunchStatus" style="text-align: left;margin-left: 5px;">Lunch Status</label>
        <asp:DropDownList ID="ddlLunchStatus" runat="server" CssClass="form-control" >
            <asp:ListItem Text="Yes" Value="1" />
            <asp:ListItem Text="No" Value="0" />
        </asp:DropDownList>
    </div>
            <div class="field">
                  <label for="txtReason" style="text-align: left;margin-left: 5px;">Reason</label>
                  <asp:TextBox ID="txtReason" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" style="width: 195px; height: 70px;"></asp:TextBox>
                </div>
        

  </div>
    <asp:Button 
    ID="btnSubmit11" 
    runat="server" 
    Text="Submit" 
    CssClass="submit-btn" 
    OnClick="btnSave_Click"  
    OnClientClick="return confirm('Are you sure you want to submit this Out Pass request?');" 
    style="margin-bottom:15px;"  />
</div>

</asp:Panel>


   <asp:LinkButton ID="lnkDummy3" runat="server" Style="display:none;"></asp:LinkButton> 
<!-- Modal Extender -->
<asp:ModalPopupExtender ID="ModalPopupExtender3" BehaviorID="mpe3" runat="server" PopupControlID="pnlPopup3" TargetControlID="lnkDummy3" BackgroundCssClass="modalBackground" CancelControlID="btnHide3">
</asp:ModalPopupExtender>
<!-- Modal Panel -->
<asp:Panel ID="pnlPopup3" runat="server" CssClass="modalPopup" Style="display:none;width:1200px;">
    <div class="header">
        <b><asp:Label ID="lblNotification3" runat="server" Text="Student Out Pass Request List"></asp:Label></b>
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
            Text="Student Out Pass List" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <table>
        <tr style="height: 20px">
            <td></td>
        </tr>
        <tr>
            <td style="width: 20px"></td>
                <td>&nbsp;&nbsp;&nbsp;
                <button type="button" onclick="showPopup2();" style="color: white; background-color: green; height: 30px; width: 180px;">Out Pass Request</button></td>
            </tr>
    </table>

    <br />
         <asp:GridView ID="getGatePassRequestList" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="Black"
    BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="100%" Font-Size="12px" CssClass="grid-cell-border"
    GridLines="Both" EmptyDataText="There are no data records to display."
    AllowSorting="true">
    <AlternatingRowStyle BackColor="#F7F7F7" />
    <Columns>
        <asp:TemplateField HeaderText="Sl. No.">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
            <ItemStyle Width="7%" />
        </asp:TemplateField>
    </Columns>
    <Columns>
        <asp:TemplateField HeaderText="Student Code">
            <ItemTemplate>
                <asp:Label runat="server" Text='<%# Eval("No_") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="7%" />
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Student Name">
            <ItemTemplate>
                <asp:Label runat="server" Text='<%# Eval("StudentName") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="7%" />
        </asp:TemplateField>
      
        <asp:TemplateField HeaderText="Hostel">
            <ItemTemplate>
               <asp:Label runat="server" Text='<%# Eval("Hostel") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="7%" />
        </asp:TemplateField>

                 <asp:TemplateField HeaderText="Room No">
            <ItemTemplate>
                <asp:Label runat="server" Text='<%# Eval("RoomNo") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="7%" />
        </asp:TemplateField>
                  <asp:TemplateField HeaderText="Form Date">
        <ItemTemplate>
            <asp:Label ID="Label12" runat="server" Text='<%# DateTime.Parse(Eval("FormDate").ToString()).ToString("dd-MM-yyyy hh:mm tt") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="7%" />
    </asp:TemplateField>

          <asp:TemplateField HeaderText="To Date">
      <ItemTemplate>
             <asp:Label ID="Label112" runat="server" Text='<%# DateTime.Parse(Eval("ToDate").ToString()).ToString("dd-MM-yyyy hh:mm tt") %>'></asp:Label>      
      </ItemTemplate>
      <ItemStyle Width="7%" />
  </asp:TemplateField>

                <asp:TemplateField HeaderText="No Of Hours">
    <ItemTemplate>
        <asp:Label ID="Label2" runat="server" Text='<%# Eval("NoOfHours") %>'></asp:Label>
    </ItemTemplate>
    <ItemStyle Width="7%" />
</asp:TemplateField>

 <asp:TemplateField HeaderText="Lunch">
        <ItemTemplate>
            <asp:Label runat="server" Text='<%# Eval("LunchStatusText") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="7%" />
    </asp:TemplateField>
                <asp:TemplateField HeaderText="Reason">
    <ItemTemplate>
        <asp:Label ID="Label2" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
    </ItemTemplate>
    <ItemStyle Width="7%" />
</asp:TemplateField>
          <asp:TemplateField HeaderText="Warden Status">
      <ItemTemplate>
          <asp:Label ID="Label2" runat="server" Text='<%# Eval("WardenStatusText") %>'></asp:Label>
      </ItemTemplate>
      <ItemStyle Width="7%" />
  </asp:TemplateField>

  <asp:TemplateField HeaderText="Warden Remark">
    <ItemTemplate>
        <asp:Label ID="Label2" runat="server" Text='<%# Eval("WardenRemark") %>'></asp:Label>
    </ItemTemplate>
    <ItemStyle Width="7%" />
</asp:TemplateField>

<%-- <asp:TemplateField HeaderText="Chief Warden Status">
      <ItemTemplate>
          <asp:Label ID="Label2" runat="server" Text='<%# Eval("CWardenStatusText") %>'></asp:Label>
      </ItemTemplate>
      <ItemStyle Width="7%" />
  </asp:TemplateField>

  <asp:TemplateField HeaderText="Chief Warden Remark">
    <ItemTemplate>
        <asp:Label ID="Label2" runat="server" Text='<%# Eval("CWardenRemark") %>'></asp:Label>
    </ItemTemplate>
    <ItemStyle Width="7%" />
</asp:TemplateField>--%>
    <asp:TemplateField HeaderText="Download">
    <ItemTemplate>
        <a href='#' 
           onclick='ViewUserDetails("<%# Eval("ID") %>");' 
           <%# Eval("WardenStatus").ToString() == "1" ? "" : "style=\"display:none;\"" %>>
           <u>View</u>
        </a>
    </ItemTemplate>
    <ItemStyle Width="7%" />
</asp:TemplateField>
    </Columns>
    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" />
    <AlternatingRowStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" />
    <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"/>
    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" />
    <RowStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" />
    <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
    <SortedAscendingCellStyle BackColor="#F4F4FD" />
    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
    <SortedDescendingCellStyle BackColor="#D8D8F0" />
    <SortedDescendingHeaderStyle BackColor="#3E3277" />

</asp:GridView>      
  <div class="modal fade" id="userDetailModal" tabindex="-1" role="dialog">
  <div class="modal-dialog modal-lg" role="document">
    <div class="modal-content">
      
      <div class="modal-header">
  <h5 class="modal-title">Student Exit Out Pass</h5>
  <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="margin-top: -21px;">
    <span aria-hidden="true">&times;</span>
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
              <div class="field">
                <label>Out Pass No</label>
                <div class="value" id="gatePassNo"></div>
              </div>
              <div class="field">
                <label>Student Name</label>
                <div class="value" id="studentName"></div>
              </div>
               <div class="field">
                <label>Program</label>
                <div class="value" id="class"></div>
              </div>
            </div>

            <table class="details">
                   <tr>
      <th>College</th>
        <td id="college"></td>
</tr>
                <tr>
      <th>Hostel</th>
        <td id="hostel"></td>
</tr>   
                   <tr>
                <th>From Date</th>
                <td  id="fromDate"></td>
              </tr>
              <tr>
                <th>To Date</th>
                <td  id="toDate"></td>
              </tr>
              <tr>
                <th>No. of Hours</th>
                <td  id="noOfHours"></td>
              </tr>
              <tr>
                <th>Reason</th>
                <td  id="reason"></td>
              </tr>
                 <tr>
                   <th>Student Mobile No.</th>
                   <td  id="studentMobileNo"></td>
                 </tr>
                <tr>
                  <th>Father Mobile No.</th>
                  <td  id="fatherMobileNo"></td>
                </tr>
                 <tr>
                   <th>Address</th>
                   <td  id="studentAddress"></td>
                 </tr>
            </table>

            <div class="sign-row">
              <div class="sig">Warden Signature<span>Warden</span></div>
                 <div></div>
              <div> <svg style="width: 230px;" id="qrcode"></svg></div>
             </div>

          </div>
            </div>
   <div class="modal-footer">
        <asp:HiddenField runat="server" ID="hidEmpID" />
         <button type="button" class="btn btn-primary" onclick="printDiv('printArea')">Print</button>
         <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
      </div>
    </div>
  </div>  </div>





</asp:Content>

