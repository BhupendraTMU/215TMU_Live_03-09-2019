<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentInOutAprove1.aspx.cs" Inherits="Faculty_StudentInOutAprove1" %>
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
    <table>
        <tr style="height: 20px">
            <td></td>
        </tr>
    </table>

    <br />
         <asp:GridView ID="getGatePassRequestList" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="Black"
    BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="100%" Font-Size="12px" CssClass="grid-cell-border"
    GridLines="Both" EmptyDataText="There are no data records to display." OnRowDataBound="getGatePassRequestList_RowDataBound" OnRowCommand="getGatePassRequestList_RowCommand"
    AllowSorting="true">
    <AlternatingRowStyle BackColor="#F7F7F7" /> 
    <Columns>
        <asp:TemplateField HeaderText="Student Code">
            <ItemTemplate>
                 <asp:HiddenField ID="StudentID" runat="server" Value='<%# Eval("ID") %>'></asp:HiddenField>
               <asp:Label ID="StudentNo" runat="server" Text='<%# Eval("No_") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="7%" />
        </asp:TemplateField>

              <asp:TemplateField HeaderText="Student Name">
    <ItemTemplate>
        <asp:Label ID="Label2" runat="server" Text='<%# Eval("StudentName") %>'></asp:Label>
    </ItemTemplate>
    <ItemStyle Width="7%" />
</asp:TemplateField>

            <asp:TemplateField HeaderText="Acadmic Year">
    <ItemTemplate>
        <asp:Label ID="Label3" runat="server" Text='<%# Eval("AcadmicYear") %>'></asp:Label>
    </ItemTemplate>
    <ItemStyle Width="7%" />
</asp:TemplateField>
           
                    <asp:TemplateField HeaderText="Hostel">
    <ItemTemplate>
        <asp:Label ID="Label4" runat="server" Text='<%# Eval("Hostel") %>'></asp:Label>
    </ItemTemplate>
    <ItemStyle Width="7%" />
</asp:TemplateField>
                            <asp:TemplateField HeaderText="Room No">
    <ItemTemplate>
        <asp:Label ID="Label5" runat="server" Text='<%# Eval("RoomNo") %>'></asp:Label>
    </ItemTemplate>
    <ItemStyle Width="7%" />
</asp:TemplateField>
            <asp:TemplateField HeaderText="Form Date">
    <ItemTemplate>
        <asp:Label ID="Label15" runat="server" Text='<%# DateTime.Parse(Eval("FormDate").ToString()).ToString("dd-MM-yyyy hh:mm tt") %>'></asp:Label>
    </ItemTemplate>
    <ItemStyle Width="7%" />
</asp:TemplateField>
                    <asp:TemplateField HeaderText="To Date">
    <ItemTemplate>
        <asp:Label ID="Label115" runat="server" Text='<%# DateTime.Parse(Eval("ToDate").ToString()).ToString("dd-MM-yyyy hh:mm tt") %>'></asp:Label>
    </ItemTemplate>
    <ItemStyle Width="7%" />
</asp:TemplateField>
     <asp:TemplateField HeaderText="Totel Time">
    <ItemTemplate>
        <asp:Label ID="Label119" runat="server" Text='<%# Eval("NoOfHours").ToString() %>'></asp:Label>
    </ItemTemplate>
    <ItemStyle Width="7%" />
</asp:TemplateField>
                <asp:TemplateField HeaderText="Student Reason">
    <ItemTemplate>
        <asp:Label ID="StudentReason" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
    </ItemTemplate>
    <ItemStyle Width="7%" />
</asp:TemplateField>
          <asp:TemplateField HeaderText="Warden Status">
      <ItemTemplate>
          <asp:Label ID="WardenStatusText" runat="server" Text='<%# Eval("WardenStatusText") %>'></asp:Label>
      </ItemTemplate>
      <ItemStyle Width="7%" />
  </asp:TemplateField>

    <asp:TemplateField HeaderText="Warden Remark">
    <ItemTemplate>
       <asp:TextBox ID="txtWardenRemark" runat="server" Text='<%# Bind("WardenRemark") %>' TextMode="MultiLine" Rows="2"></asp:TextBox>
    </ItemTemplate>
    <ItemStyle Width="7%" />
</asp:TemplateField>


               
          <asp:TemplateField HeaderText="Chief Warden Status" Visible="False">
      <ItemTemplate>
          <asp:Label ID="CWardenStatusText" runat="server" Text='<%# Eval("CWardenStatusText") %>'></asp:Label>
      </ItemTemplate>
      <ItemStyle Width="7%" />
  </asp:TemplateField>

  <asp:TemplateField HeaderText="Chief Warden Remark" Visible="False">
    <ItemTemplate>
       <asp:TextBox ID="txtCWardenRemark" runat="server" Text='<%# Bind("CWardenRemark") %>' TextMode="MultiLine" Rows="2"></asp:TextBox>
    </ItemTemplate>
    <ItemStyle Width="7%" />
</asp:TemplateField>
   <asp:TemplateField HeaderText="">
    <ItemTemplate>
        <div style="white-space: nowrap;">
           <asp:Button ID="AcceptButton" runat="server" Text="Accept" CssClass="grid-action-button" BackColor="Green" ForeColor="White" CommandName="Accept" CommandArgument='<%# Container.DataItemIndex %>' />
<asp:Button ID="RejectButton" runat="server" Text="Reject" CssClass="grid-action-button" BackColor="Red" ForeColor="White" CommandName="Reject" CommandArgument='<%# Container.DataItemIndex %>'/>

        </div>
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

</asp:Content>


