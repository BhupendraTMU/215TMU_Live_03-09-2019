<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="EmployeePaySlip.aspx.cs" Inherits="Faculty_EmployeePaySlip" %>
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
         function ViewUserDetails(No) {
             $.ajax({
                 url: 'EmployeePaySlip.aspx/GetEmployeeDetailList',
                 type: 'POST',
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 data: JSON.stringify({ employeeNo: No }),
                 success: function (result) {
                     const data = result.d;

                     if (!data || data.length === 0) {
                         alert("No data found.");
                         return;
                     }

                     // Clear previous data
                     $('#gridHeader').empty();
                     $('#gridBody').empty();

                     // If it's a single object, wrap it into an array
                     const dataArray = Array.isArray(data) ? data : [data];

                     // Generate header from first object keys
                     const keys = Object.keys(dataArray[0]);
                     keys.forEach(key => {
                         $('#gridHeader').append(`<th>${key}</th>`);
                     });

                     // Generate body rows
                     dataArray.forEach(item => {
                         let row = '<tr>';
                         keys.forEach(key => {
                             row += `<td>${item[key] != null ? item[key] : ''}</td>`;
                         });
                         row += '</tr>';
                         $('#gridBody').append(row);
                     });

                     // Show modal
                     $("#userDetailModal").modal("show");
                 },
                 error: function () {
                     alert('Error loading employee details.');
                 }
             });
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
        <b><asp:Label ID="lblNotification2" runat="server" Text="PaySlip Request"></asp:Label></b>
        <div class="close">
            <asp:Button ID="btnHide2" runat="server" Text="X" style="padding:0px;" OnClientClick="closePopup2(); return false;" />
        </div>
    </div>

   <div class="body">
    <div class="section">
            <div class="field">
                <label>Employee No</label>
                 <asp:TextBox ID="txtEmployeeNo" runat="server" Enabled="false"></asp:TextBox>
            </div>
            <div class="field">
                <label>Name</label>
                <asp:TextBox ID="txtEmployeeName" runat="server" Enabled="false"></asp:TextBox>
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

    <asp:Button ID="btnSubmit11" runat="server" Text="Submit" CssClass="submit-btn" OnClick="btnSave_Click"  style="margin-bottom:15px;"  OnClientClick="copyMonthsToHidden();"/>
</div>

</asp:Panel>


   <asp:LinkButton ID="lnkDummy3" runat="server" Style="display:none;"></asp:LinkButton> 
<!-- Modal Extender -->
<asp:ModalPopupExtender ID="ModalPopupExtender3" BehaviorID="mpe3" runat="server" PopupControlID="pnlPopup3" TargetControlID="lnkDummy3" BackgroundCssClass="modalBackground" CancelControlID="btnHide3">
</asp:ModalPopupExtender>
<!-- Modal Panel -->
<asp:Panel ID="pnlPopup3" runat="server" CssClass="modalPopup" Style="display:none;width:1200px;">
    <div class="header">
        <b><asp:Label ID="lblNotification3" runat="server" Text="PaySlip Request List"></asp:Label></b>
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
            Text="Employee PaySlip Detail" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <table>
        <tr style="height: 20px">
            <td></td>
        </tr>
        <tr>
            <td style="width: 20px"></td>
                <td>&nbsp;&nbsp;&nbsp;
                <button type="button" onclick="showPopup2();" style="color: white; background-color: green; height: 30px; width: 120px;">PaySlip Request</button></td>
            </tr>
    </table>

    <br />
         <asp:GridView ID="getPaySlipRequestList" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="Black"
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
        <asp:TemplateField HeaderText="Employee Code">
            <ItemTemplate>
                <asp:Label runat="server" Text='<%# Eval("EmployeeNo") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="7%" />
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Employee Name">
            <ItemTemplate>
                <asp:Label runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="7%" />
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Department Name">
            <ItemTemplate>
                <asp:Label runat="server" Text='<%# Eval("DepartmentName") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="10%" />
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Designation Name">
            <ItemTemplate>
                <asp:Label runat="server" Text='<%# Eval("DesignationName") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="7%" />
        </asp:TemplateField>

        <asp:TemplateField HeaderText="From Month">
            <ItemTemplate>
               <asp:Label runat="server" Text='<%# Eval("FromMonth", "{0:MMMM yyyy}") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="7%" />
        </asp:TemplateField>

                 <asp:TemplateField HeaderText="To Month">
            <ItemTemplate>
                <asp:Label runat="server" Text='<%# Eval("ToMonth", "{0:MMMM yyyy}") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="7%" />
        </asp:TemplateField>

             <asp:TemplateField HeaderText="HOD Code">
        <ItemTemplate>
            <asp:Label runat="server" Text='<%# Eval("HODCode") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="7%" />
    </asp:TemplateField>

          <asp:TemplateField HeaderText="HOD Status">
      <ItemTemplate>
          <asp:Label ID="Label2" runat="server" Text='<%# Eval("HODStatusText") %>'></asp:Label>
      </ItemTemplate>
      <ItemStyle Width="7%" />
  </asp:TemplateField>

                <asp:TemplateField HeaderText="HOD Remark">
    <ItemTemplate>
        <asp:Label ID="Label2" runat="server" Text='<%# Eval("HODRemark") %>'></asp:Label>
    </ItemTemplate>
    <ItemStyle Width="7%" />
</asp:TemplateField>


                     <asp:TemplateField HeaderText="HR Code">
        <ItemTemplate>
            <asp:Label runat="server" Text='<%# Eval("HRCode") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="7%" />
    </asp:TemplateField>

          <asp:TemplateField HeaderText="HR Status">
      <ItemTemplate>
          <asp:Label ID="Label2" runat="server" Text='<%# Eval("HrStatusText") %>'></asp:Label>
      </ItemTemplate>
      <ItemStyle Width="7%" />
  </asp:TemplateField>

  <asp:TemplateField HeaderText="HR Remark">
    <ItemTemplate>
        <asp:Label ID="Label2" runat="server" Text='<%# Eval("HRRemark") %>'></asp:Label>
    </ItemTemplate>
    <ItemStyle Width="7%" />
</asp:TemplateField>
    <asp:TemplateField HeaderText="Download">
    <ItemTemplate>
        <a href='#' onclick="ViewUserDetails('<%# Eval("EmployeeNo") %>');">View</a>
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
  <h5 class="modal-title" style="color: #093A62; font-family: 'Georgia', 'Times New Roman', 'Helvetica Neue'; font-size: 15pt;">
    Employee PaySlip List
</h5>     </div>
      <div class="modal-body">
          <div style="max-height: 350px; overflow-y: auto;">
      <table class="table table-bordered" id="employeeDetailsGrid">
          <thead style="background-color: #ED7600;color:white">
            <tr id="gridHeader"></tr>
          </thead>
          <tbody id="gridBody"></tbody>
        </table>

          </div>
      </div>      
    </div>
  </div>
</div>
   
</asp:Content>

