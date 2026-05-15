<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="MembershipAprrovalForm.aspx.cs" EnableEventValidation="false" Inherits="Faculty_MembershipAprrovalForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        #confirmModalB .modal-dialog.modalPopup {
            width: 95%;
        }

        table thead tr th:first-child, .table > tbody > tr > th:first-child {
            border-left: 1px solid #60594f;
            padding: 5px 8px;
        }
        .auto-style1 {
            width: 258px;
        }
    </style>
     <%-- <script type="text/javascript">
        function PrintDiv() {
            var divToPrint = document.getElementById('printarea');
            var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
        }

    </script>--%>
      <script language="javascript" type="text/javascript">
          function CallPrint(strid) {
              var prtContent = document.getElementById(strid);
              var WinPrint = window.open('', '', 'letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
              WinPrint.document.write(prtContent.innerHTML);
              WinPrint.document.close();
              WinPrint.focus();
              WinPrint.print();
              WinPrint.close();

              prtContent.innerHTML = strOldOne;
          }
    </script>
   <%-- <script type = "text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlGridViewDetails.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>DIV Contents</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
      <fieldset>
        <div class="text-right"  style="padding-left:150px">

            <asp:Button ID="BtnSubmit" runat="server" Text="Approved" ForeColor="White" CssClass="btn" OnClick="BtnSubmit_Click" BackColor="#ff9900" />
            <asp:Button ID="BtnRejected" runat="server" Text="Rejected" ForeColor="White" CssClass="btn" OnClick="BtnRejected_Click"  BackColor="#ff9900"/>
            <asp:Button ID="Btnexporttoexel" runat="server" Text="Export To Excel" ForeColor="White" CssClass="btn" OnClick="Btnexporttoexel_Click" BackColor="#ff9900"/>
        </div>
           <br />
           <div>
              <table class="boxBody" width="1200px">
         

                                      <tr>
                                          <td style="width: 20px"></td>
                                        <td style="width:150px; font-size:large; font:bold"> &nbsp;&nbsp;Employee Code
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td class="auto-style1">
                                            <asp:TextBox ID="txtemployeecodese" runat="server" Width="200px" BorderColor="Black" autocomplete="off"
                                                oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                                           

                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>--%>


                                        </td>

        <td>
            <asp:Button ID="Search" runat="server" CssClass="btn-sm btn-primary btn-block" OnClick="Search_Click" ValidationGroup="g1" Height="30px" Width="90px" Text="Show Report" />
        </td>
           </tr>
         </table>
          </div>
    </fieldset>
    <fieldset class="boxBodyInner">
        <br />
        <asp:GridView ID="grdmemberapprovallist" runat="server" DataKeyNames="Employee_code" OnPageIndexChanging="grdmemberapprovallist_PageIndexChanging"  OnRowDataBound="grdmemberapprovallist_RowDataBound" PageSize="50"
            AllowPaging="true" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" Visible="true">
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle CssClass="csspager" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <%# Container.DataItemIndex +1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Employee Code" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblemployeecode" runat="server" Text='<%# Bind("Employee_code") %>'></asp:Label>
                        <asp:HiddenField ID="Hfemployeecode" Value='<%# Eval("Employee_code") %>' runat="server" />
                        <asp:HiddenField ID="Hfhodname" Value='<%# Eval("Employee_code") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="View" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbutton" runat="server" OnClick="lnkbutton_Click">View</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Employee Name" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lblemployeename" runat="server" Text='<%# Eval("Name") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Designation" ItemStyle-Width="3%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lbldesignation" runat="server" Text='<%# Eval("Designation") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Department" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lbldepartment" runat="server" Text='<%#Eval("Department") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="HOD Approval" ItemStyle-Width="2%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblprincipalapproval" runat="server" Text='<%# Eval("Status") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="University Librarian" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lbldeputylibrarian" runat="server" Text='<%#Eval("Approval_status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" HeaderText="Select" ItemStyle-CssClass="text-center">
                   
                    <ItemTemplate>
                        <asp:CheckBox ID="Chkemployee" Enabled='<%# Eval("txtMarksEnableDesable").ToString().Equals("true") %>'  runat="server" AutoPostBack="true"  />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
        </asp:GridView>
        
               
          <asp:Panel ID="pnlGridViewDetails" CssClass="modalPopup" Width="65%" runat="server"  Style="display: none;" ScrollBars="Vertical" Height="950px">
            <div class="header">
                <b>

                    <asp:Label ID="lblNotification" runat="server" Text="Library Membership Form"></asp:Label></b>
                <div class="close">
                    <asp:Button ID="btnclose" OnClick="btnclose_Click" runat="server" Text="X" />
                </div>
            </div>
               <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
        <div style="width: 90%;">
                     <div style="text-align: right; width: 100%; margin-bottom: -30px;">
                         <asp:Button ID="BtnPrint" OnClientClick="javascript:CallPrint('bill')" runat="server" Width="10%" Style="margin-top: 5px;" Text="Print" Font-Bold="true" BorderColor="WhiteSmoke" />
                     </div>

              
                    <%--<div style="width: 100%; margin-bottom: 10px; margin-left: 1%; margin-right: 1%; margin-top: 5px;">--%>
               <div id="bill">    
            <table>
                <tr>
                    <td style="width: 1%"></td>
                    <td style="width: 12%; text-align: left">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/UPDATEDLOGO.jpg" Width="55%" />

                    </td>
                    <td style="width: 65%; text-align: center">
                        <strong>
                            <asp:Label ID="lblcentral" Font-Size="Large" Text="CENTRAL LIBRARY:GENERAL" runat="server"></asp:Label></strong>
                        <br />
                        <strong>
                            <asp:Label ID="lblCName" Font-Size="Large" Text="TEERTHANKER MAHAVEER UNIVERSITY" runat="server"></asp:Label></strong>
                        <br />
                        <strong>
                            <asp:Label ID="lblAC" runat="server" Text="(Established under Govt. of U. P. Act No. 30, 2008)"></asp:Label></strong>

                        <br />
                        <strong>
                            <asp:Label ID="LblType" runat="server" Text="Delhi Road,Moradabad(U.P) India"></asp:Label>
                        </strong>
                        <br />
                    </td>
                    <td style="width: 10%; text-align: center"></td>
                </tr>
                </table>
       
  
    <fieldset class="boxBody" style="text-align: center; border-color: black; background-color: black;">
        <asp:Label ID="Label1" runat="server" Text="LIBRARY MEMBERSHIP FORM FOR TEACHING/NON-TEACHING EMPLOYEES" Font-Size="15pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
             
    <div id="divGeneralBody">
        <fieldset class="boxBodyInner">
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">

                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="width: 200px">Employee Code:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtemployeecode" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="col-md-2">
                                    <label>Name:</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtemployeename" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div>
                                    <asp:UpdatePanel ID="pnlpic" runat="server">
                                        <ContentTemplate>
                                            <asp:Image ID="ImgPrv" Height="100px" Width="100px" runat="server" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <%-- <%--<asp:FileUpload ID="FileUpload2" runat="server" Width="150px"/>
                                       <asp:Button ID="btnUpload" runat="server" Text="Upload"/>--%>
                                <%--<asp:FileUpload ID="FileUpload2" runat="server" Width="150px" onchange="ShowImagePreview(this);" />--%>
                            </div>
                            <div class="form-group" style="margin-top: -70px">
                                <div class="col-md-2">
                                    <label style="width: 200px">Designation:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtdesignation" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px">Department:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtdepartment" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="width: 200px">Date Of Birth:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtdateofbirth" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px">Contact No:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtmobile" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="width: 200px">Local Address:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtlocaladdress" runat="server" TextMode="MultiLine" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px">Permanent Address:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtpermentaddress" runat="server" Enabled="false" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="width: 200px">Upload Photo:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:FileUpload ID="FileUpload2" runat="server" Enabled="false" CssClass="form-control" onchange="ShowImagePreview(this);" />
                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px">Email:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtemail" runat="server" Enabled="false" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                                </div>
                            <br />
                            <br />
                            <table>

                          <tr>
                              <td style="width:850px;"></td style="width:1100px">
                                <td style="width:1100px; text-align:center ">
                        <strong>
                            <asp:Label ID="Label14" Font-Size="Large" Text="Librarian" runat="server"></asp:Label></strong>
                        <br />
                        <strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label15" Font-Size="Large" Text="Teerthanker Mahaveer University" runat="server"></asp:Label>
                                    <br />
                            <asp:Label ID="Label16" runat="server" Text="Moradabad"></asp:Label></strong>


                        <br />
                                    </td>
</tr>
                                      </table>
                            <fieldset class="boxBody" style="text-align: center; border-color: white;">
                                <asp:Label ID="Label6" runat="server" Text="RULES AND REGULATIONS OF LIBRARY" Font-Underline="true" Font-Size="15pt" ForeColor="black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                            </fieldset>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label style="font-size: medium">1. Entry to the library is through Identity Card</label>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label style="font-size: medium">2. Three books will be issued for period of 90 Days. The Book shall have to be returned to the library on or before the due date.</label>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label style="font-size: medium">3. Borrower has to maintain the book in good condition, failing which she/he can asked to pay the current price/purchases price of the book as fine.</label>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label style="font-size: medium">4. Bags, Food items are not allowed in Library.</label>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label style="font-size: medium">
                                        5. Library books are for the use and benefit of not only the present but  also future members of the library. Therefore, all library books should be handled   with due care and 
                                        and consideration. Member should not use markers,pen,pencil, or disfigure the books in any way.
                                    </label>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label style="font-size: medium">6. Members should satisfy themselves abount the physical condition of the books they wish to borrow before getting them issued; otherwise they will be held responsible for any damage or multilation noticed at the time of return.</label>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label style="font-size: medium">7. You are advised to take due care of electronic gadgets, jewellery etc, including cash and other valuables. The library shall not be responsible for any loos of theft in any way.</label>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label style="font-size: medium">8. All items taken into and out of the library are subject to security check.</label>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label style="font-size: medium">9. All users are requested to maintain silence in the library. Smoking, eating, sleeping and using mobile phones etc. are strictly prohibited in the library premises. Users are expected to behave decently and maintain decorum.</label>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label style="font-size: medium">10. Faculty/Staff Members can suggest standard books/journals for procurement in the library.</label>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label style="font-size: medium">
                                        <asp:CheckBox ID="chkaccept" Enabled="false" runat="server" />I have read the Library Rules and agree to abide by them and shall obtain "Clearance Certificate" from the Library at the time of transfer/leaving the University.</label>

                                </div>
                                </div>
                               <div class="form-group">
                                <div class="col-md-12" style="text-align:left">                                   
                                    <label style="font-size: medium">Date:__________________.</label>
                                </div>
                            </div>
                               <div class="form-group">
                                <div class="col-md-12" style="text-align:right">                                   
                                    <label style="font-size: medium"> Signature of the Applicant</label>
                                    
                                </div>                                   
                            </div>  
                              </div>                   
                        </div>                   
                    </div>                
                </div>
            
          </div>
            </div>
        </fieldset>
       
                 
        <%--<asp:Button ID="Button1" runat="server" Text="Print" OnClientClick = "return PrintPanel();" />--%>
              
               </asp:Panel>
       

        <asp:Button ID="btnDummy" runat="server" Style="display: none;" />
        <asp:ModalPopupExtender ID="GridViewDetails" runat="server" TargetControlID="btnDummy"
            PopupControlID="pnlGridViewDetails" BackgroundCssClass="modalBackground" />
        
        </fieldset>
   
</asp:Content>

