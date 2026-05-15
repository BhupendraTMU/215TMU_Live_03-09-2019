<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentAttendanceUndertaking.aspx.cs" Inherits="Student_StudentAttendanceUndertaking" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
  :root{
    /*--max-width:900px;
    --muted:#555;
    --accent:#111;
    --gap:14px;
    --signature-height:70px;*/
  }
  body{
    font-family: "Times New Roman", Georgia, serif;
    color:var(--accent);
    background: #f7f7f7;
    padding: 24px;
    -webkit-print-color-adjust: exact;
  }
  .page{
    max-width:var(--max-width);
    margin: 16px auto;
    background: white;
    padding: 28px 36px;
    box-shadow: 0 6px 18px rgba(0,0,0,0.06);
    line-height:1.45;
  }

  header{
    display:flex;
    justify-content:space-between;
    align-items:flex-start;
    margin-bottom: 10px;
  }
  .left{
    width:65%;
  }
  .right {
  display: flex;
  align-items: center;  /* vertically center text & input */
  justify-content: flex-start; /* text on left, input on right */
  gap: 8px; /* small space between label & input */
}

  .college{
    margin-top:6px;
    font-weight:700;
  }
  h2.subject{
    text-align:center;
    font-size:1rem;
    margin: 18px 0 8px;
    text-decoration: underline;
    text-decoration-thickness: 1px;
  }

  p.lead{
    text-indent: 40px;
    margin: 8px -40px 12px;
  }

  .blank-line{
    display:inline-block;
    border-bottom: 1px solid #000;
    min-width:260px;
    margin-left:6px;
    vertical-align:middle;
    padding-bottom:2px;
  }

  table.attendance{
    width:100%;
    border-collapse:collapse;
    margin-top:10px;
    margin-bottom:14px;
    font-size:0.95rem;
  }
  table.attendance th,
  table.attendance td{
    border: 1px solid #777;
    padding: 8px 10px;
    text-align:left;
  }
  table.attendance th{
    background:#efefef;
    font-weight:700;
  }

  .note{
    margin-top:8px;
    margin-bottom:18px;
  }

  .verifications{
    display:flex;
    gap:18px;
    flex-wrap:wrap;
    margin-top: 8px;
  }
  .sign-block2{
  padding: 10px;
}
  .sign-block{
    min-width:260px;
    padding: 10px;
    width:100%
  }
  .sign-block1{
  flex:1 1 450px;
  min-width:260px;
  }
  .sign-label{
    display:block;
    margin-top:18px;
    font-weight:700;
  }
  .signature{
    height: var(--signature-height);
    border-bottom:1px solid #000;
    margin-top:12px;
  }
   .meta {
    margin-top: 18px;
    display: grid;
    grid-template-columns: 1fr;
    gap: 8px;
    max-width: 600px;
  }
  .meta .item {
    display: flex;
    align-items: center;
    gap: 8px;
  }
  .meta .item label {
    font-weight: bold;
    min-width: 120px; /* adjust to your label width */
  }
  .meta .item input {
    flex: 1;
    padding: 4px 0;
    border: none;               
    border-bottom: 1px solid #000;
    background: transparent;      
    outline: none;                
  }
  /* optional: underline color change on focus */
  .meta .item input:focus {
    border-bottom-color: #007BFF;
  }

  footer{
    margin-top:18px;
    text-align:right;
    color:var(--muted);
    font-size:0.9rem;
  }

  /* Print friendly */
  @media print{
    body{background: #fff; padding: 0;}
    .page{box-shadow:none; margin:0; max-width:100%; padding:18mm;}
    a[href]:after{content: none;}
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
        <b><asp:Label ID="lblNotification2" runat="server" Text="Student Gate Pass Request"></asp:Label></b>
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

    <asp:Button ID="btnSubmit11" runat="server" Text="Submit" CssClass="submit-btn" OnClick="btnSave_Click"  style="margin-bottom:15px;"  />
</div>

</asp:Panel>


   <asp:LinkButton ID="lnkDummy3" runat="server" Style="display:none;"></asp:LinkButton> 
<!-- Modal Extender -->
<asp:ModalPopupExtender ID="ModalPopupExtender3" BehaviorID="mpe3" runat="server" PopupControlID="pnlPopup3" TargetControlID="lnkDummy3" BackgroundCssClass="modalBackground" CancelControlID="btnHide3">
</asp:ModalPopupExtender>
<!-- Modal Panel -->
<asp:Panel ID="pnlPopup3" runat="server" CssClass="modalPopup" Style="display:none;width:1200px;">
    <div class="header">
        <b><asp:Label ID="lblNotification3" runat="server" Text="Student Gate Pass Request List"></asp:Label></b>
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
            Text="Student Attendance Undertaking" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
    </fieldset> 
    <br />
     <div class="page" role="document" aria-label="Attendance undertaking form">
     <header>
      <div class="left">
        <div>
          <strong>To,</strong><br />
          The Director/Principal/HOD
        </div>
        <div class="college">
          <asp:TextBox runat="server" type="text" id="CollegeDepartment" name="CollegeDepartment" placeholder="College/Department" style="flex: 1;padding: 4px 0;border: none;border-bottom: 1px solid #000;background: transparent;outline: none;width: 430px;" ></asp:TextBox> (College/Department)<br />
          Teerthanker Mahaveer University, Moradabad.
        </div>
      </div>

      <div class="right">
      <label for="Date1" style="margin-right:8px;">Date:</label><asp:TextBox runat="server" type="date" id="Date1" name="Date1" placeholder="Date" style="flex: 1;padding: 4px 0;border: none;border-bottom: 1px solid #000;background: transparent;outline: none;width: 120px;" /> 
      </div>
    </header>

    <h2 class="subject">Subject: - Undertaking for maintaining 75% attendance.</h2>

    <p class="lead">
      Respected Sir/Madam,
    </p>
           <p style="font-size: 14px;">
      This is to state that I, <span class="blank-line" id="studentName1" runat="server"></span>
      and my father/mother Sh./Smt. <span class="blank-line" style="min-width:200px;" id="fatherName1" runat="server"></span>
      have complete knowledge about the Teerthanker Mahaveer University Ordinance governing the attendance of students,
      according to which I have to attend at least 75% of the classes individually in each course during the entire semester/year
      of the programme; failing which I will not be allowed to appear in internal and/or external examinations of the University
      in the course(s) wherein my attendance is less than 75%.
    </p>

    <p style="margin-top:8px;">
      As on date, my attendance in various courses is as mentioned in the table below:
    </p>
   <asp:GridView ID="grdAttendanceReport" CssClass="attendance" aria-label="Attendance table" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
     EmptyDataText="There are no data records to display." BorderStyle="None" BorderWidth="2px" CellPadding="3" 
     GridLines="Horizontal" ShowFooter="true">
     <AlternatingRowStyle BackColor="#F7F7F7" />
     <Columns>
         <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-center">
             <ItemTemplate>
                 <%# Container.DataItemIndex +1 %>
             </ItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Course Name" ItemStyle-Width="5%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
             <ItemTemplate>
                 <asp:Label ID="lblCourse" runat="server" Text='<%# Bind("[Course Name]") %>'></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>

         <asp:TemplateField HeaderText="Course Code" ItemStyle-Width="3%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
             <ItemTemplate>
                 <asp:Label ID="lblCourseCode" runat="server" Text='<%# Bind("[Course Code]") %>'></asp:Label>



             </ItemTemplate>
         </asp:TemplateField>
   
         <asp:TemplateField HeaderText="Percent" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
             <ItemTemplate>
                 <asp:Label ID="lblPer" runat="server" Text='<%# "" + Eval("Per") + " % "%>'></asp:Label>
                 <%-- <asp:Label ID="lblPer" runat="server" Text='<%# Eval("Per") %>'></asp:Label>--%>
             </ItemTemplate>
             <%-- <FooterTemplate>
                 <div style="text-align: right; width: 150px">
                     <asp:Label ID="lblTotalqty" runat="server" Text="sdhjdh" Font-Bold="true" />
                 </div>
             </FooterTemplate>--%>
         </asp:TemplateField>

     </Columns>
     <FooterStyle ForeColor="Green" Font-Bold="true" Font-Size="Medium" BorderStyle="Solid" BorderColor="Black" BackColor="LightGray" />
     <HeaderStyle BackColor="LightGray" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" Font-Size="Large" Height="40px" VerticalAlign="Bottom" />
     <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
     <RowStyle ForeColor="#4A3C8C" Font-Bold="true" Font-Size="Medium" BorderStyle="Solid" BorderColor="Black" />
     <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
     <SortedAscendingCellStyle BackColor="#F4F4FD" />
     <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
     <SortedDescendingCellStyle BackColor="#D8D8F0" />
     <SortedDescendingHeaderStyle BackColor="#3E3277" />
 </asp:GridView>
         <p class="note" style="font-size: 14px;">
      It would be my personal responsibility to ensure 75% attendance separately in each course during the programme otherwise I shall have no right/claim to appear in the internal and/or external examinations of the university in the course(s) less than 75% attendance.
    </p>

    <p style="font-size: 14px;">
      I further, undertake that it shall be my responsibility to inform my parents regarding my short attendance as mentioned above.
    </p>

    <p style="font-size: 14px;">
      I am signing this undertaking after reading the University Ordinance on attendance and other matters.
    </p>

    <div class="verifications" aria-label="Verification blocks">
      <div class="sign-block1">
         <div class="meta">
  <div class="item">
    <label for="studentName">Student’s Name :</label>
    <asp:TextBox runat="server" type="text" ID="studentName" placeholder="student Name" ReadOnly="true"></asp:TextBox>
  </div>
  <div class="item">
    <label for="program">Program :</label>
    <asp:TextBox runat="server"  type="text" ID="program" placeholder="program" ReadOnly="true"></asp:TextBox>
  </div>
  <div class="item">
    <label for="branch">Branch (if any):</label>
    <asp:TextBox runat="server"  type="text" ID="branch" placeholder="branch" ReadOnly="true"></asp:TextBox>
  </div>
  <div class="item">
    <label for="semester">Semester/Year :</label>
    <asp:TextBox runat="server"  type="text" ID="semester" placeholder="Semester/Year" ReadOnly="true"></asp:TextBox>
  </div>
  <div class="item">
    <label for="studentMobile">Student’s Mobile No :</label>
    <asp:TextBox runat="server"  type="tel" ID="studentMobile" placeholder="Student Mobile" ReadOnly="true"></asp:TextBox>
  </div>
  <div class="item">
    <label for="studentEmail">Student’s E-Mail ID :</label>
    <asp:TextBox runat="server" type="email" ID="studentEmail" placeholder="Student Email" ReadOnly="true"></asp:TextBox>
  </div>
  <div class="item">
    <label for="fatherMobile">Father’s Mobile No :</label>
    <asp:TextBox runat="server"  type="tel" ID="fatherMobile" placeholder="Father Mobile" ReadOnly="true"></asp:TextBox>
  </div>
  <div class="item">
    <label for="fatherEmail">Father’s E-Mail ID :</label>
    <asp:TextBox runat="server" type="email" ID="fatherEmail" placeholder="Father Email" ReadOnly="true"></asp:TextBox>
  </div>
</div>
       </div>

      <div class="sign-block" >
        <div class="sign-label">Declaration</div>
        <div><asp:CheckBox runat="server" ID="CheckBox1"  /> I have read and understood the undertaking furnished above by me, and that I fully understand its implications.</div>
      </div>
        <div class="sign-block2" style="width:100%">
            <asp:Button ID="SubmitButton" runat="server" CssClass="button btn-success" Text="Submit" style="float: right;" />
        </div>
    </div>

 </div>

</asp:Content>

