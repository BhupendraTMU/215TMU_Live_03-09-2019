<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="EnquiryList.aspx.cs" Inherits="EnquiryList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="js/jquery-1.11.0.min.js" type="text/javascript"></script>
    <script src="js/jquery-1.11.0.min.js" type="text/javascript"></script>
        <script src="js/bootstrap.min.js" type="text/javascript"></script>
        <script src="js/wow.min.js" type="text/javascript"></script>
    <link href="bootstrap/css/bootstrap.css" rel="stylesheet" />

    <style type="text/css">
    body
    {
        font-family: Arial;
        font-size: 10pt;
    }
    .GridPager a, .GridPager span
    {
        display: block;
        height: 25px;
        width: 40px;
        font-weight: bold;
        text-align: center;
        text-decoration: none;
    }
    .GridPager a
    {
        background-color: #f5f5f5;
        color: #969696;
        border: 1px solid #969696;
    }
    .GridPager span
    {
        background-color: #A1DCF2;
        color: #ff6a00;
        border: 1px solid #3AC0F2;
    }
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <script type="text/javascript">
          function preventBackspace(e) {
              var evt = e || window.event;
              if (evt) {
                  var keyCode = evt.charCode || evt.keyCode;
                  if (keyCode === 8) {
                      if (evt.preventDefault) {
                          evt.preventDefault();
                      } else {
                          evt.returnValue = false;
                      }
                  }
              }
          }

          function EnterEvent(e) {
              if (e.keyCode == 13) {
                  $('[id$=btnSearch]').focus();
                  $('[id$=btnSearch]').click();
                  return false;
        }
    }
    </script>
   <style type="text/css">
    body
    {
        font-family: Arial;
        font-size: 10pt;
    }
    .modalPopup
  {
    background-color: #ffffdd;
    border-width: 3px;
    border-style: solid;
    border-color: Gray;
    padding: 3px;
    width: 50%;
  }
    
    .modalPopup .header
    {
        background-color: #2FBDF1;
        height: 30px;
        color: White;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
    }
    .modalPopup .body
    {
        min-height: 50px;
        line-height: 30px;
        text-align: center;
        padding:5px
    }
    .modalPopup .footer
    {
        padding: 3px;
    }
    .modalPopup .button
    {
        height: 30px;
        color: White;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
        cursor: pointer;
        background-color: red;
        border: 1px solid #5C5C5C;
    }
    .modalPopup td
    {
        text-align:left;
    }
        .redBorder {
            border:1px solid red;
        }
</style>
    <script type="text/javascript">
        $(document).ready(function () {
           debugger
            $('[id$=btnSave]').click(function () {
                $('[id$=txtFollowUpDate]').addClass("redBorder");
                $('[id$=ddlFollowUpStatus]').addClass("redBorder");
                var d=$('[id$=txtFollowUpDate]').val();
                
                if ($('[id$=txtFollowUpDate]').val() == '') {
                    return false;
                }
                else
                    $('[id$=txtFollowUpDate]').removeClass("redBorder");

                if ($('[id$=ddlFollowUpStatus]').val() == '0') {
                    return false;
                }
                else
                    $('[id$=ddlFollowUpStatus]').removeClass("redBorder");
            });

        });
    </script>

      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updmain">
        <ContentTemplate>
    <asp:Panel ID="pnlList" runat="server" BorderWidth="2px" BorderColor="#ACE9FB" ScrollBars="Vertical">
    
        <div class="panel-heading" style="background-color: #ACE9FB">
            <center>
                <div class="panel-title" style="fit-position: center;">
                    <b>
                        <p style="color: white; font-size: 25px">Enquiry List</p>
                    </b>
                </div>
            </center>
        </div>
        <asp:Panel ID="pnl" runat="server" BorderWidth="2px" BorderColor="#f5f5f5" Height="65px"  ><%--ScrollBars="Vertical"  >--%>
           <asp:UpdatePanel>
               <ContentTemplate >
                    <div class="col-lg-12">
                <div class="col-lg-4" style="padding-right: 0px; padding-top: 12px;">
                    <div class="form-group">
                        <div class="input-group">
                            <span class="input-group-addon"><span class="glyphicon glyphicon-copyright-mark"></span></span>
                            <asp:DropDownList ID="ddlSearch" CssClass="form-control input-sm" placeholder="Search" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4" style="padding-left: 0px; padding-top: 12px;">
                    <div class="form-group">
                        <div class="input-group">
                            <span class="input-group-addon"><span class="glyphicon glyphicon-education"></span></span>
                            <asp:TextBox ID="txtSearch" runat="server" onkeypress="return EnterEvent(event)"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4" style="padding-left: 0px; font-size: 20px; padding-top: 4px;">
                    <div class="col-lg-4">
                        <button id="btnSearch" type="button" class="btn btn-info btn-lg" runat="server" onserverclick="btnSearch_Click">
                            <span class="glyphicon glyphicon-search"></span>Search 
                        </button>
                    </div>
                    <div class="col-lg-4">
                        <button id="btnRefresh" type="button" class="btn btn-info btn-lg" runat="server" onserverclick="btnRefresh_Click">
                            <span class="glyphicon glyphicon-refresh"></span>Refresh
                        </button>

                    </div>
                    <div class="col-lg-4" style="padding-left: 50px;">
                        <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/images/excel.jpg" OnClick="btnExport_Click" Width="50px" Height="40px"></asp:ImageButton>
                    </div>
                </div>

            </div>
               </ContentTemplate>
           </asp:UpdatePanel>
           
            </asp:Panel>
          
    <div class="table-responsive">
                    <asp:GridView ID="grdEnquiry" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="No_" EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="50" OnPageIndexChanging="grdEnquiry_PageIndexChanging" OnSelectedIndexChanged="OnSelectedIndexChanged" AllowSorting="true" OnSorting="grdEnquiry_Sorting">
                        <Columns>
                            <asp:ButtonField Text="Follow UP" CommandName="Select" />
                           <asp:HyperLinkField DataTextField="No_" DataNavigateUrlFields="No_" DataNavigateUrlFormatString="~/Enquiry/Enquiry.aspx?EnquiryNo={0}"
            HeaderText="Enquiry No" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" SortExpression="No_"  />
                            <asp:BoundField DataField="EnquiryDate" HeaderText="Enquiry Date" SortExpression="EnquiryDate" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" />
                            <asp:BoundField DataField="ApplicantName" HeaderText="Applicant Name" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />                            
                            <asp:BoundField DataField="CourseCode" HeaderText="Course Code" SortExpression="CourseCode" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />                           
                            <asp:BoundField DataField="MobileNumber" HeaderText="Mobile No." SortExpression="MobileNumber"   HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="EnquiryType" HeaderText="Enquiry Type" SortExpression="EnquiryType" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="CollegeInterested" HeaderText="College" SortExpression="CollegeInterested" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="City" HeaderText="Place" SortExpression="City" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="EMailAddress" SortExpression="EMailAddress" HeaderText="E-mail ID" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                        </Columns>                       
                        <AlternatingRowStyle CssClass="danger" />
                        <PagerStyle HorizontalAlign = "Right" CssClass = "GridPager" />
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" PreviousPageText="Previous" NextPageText="Next" FirstPageText="First" LastPageText="Last" Position="TopAndBottom"  />
                    </asp:GridView>
        <asp:LinkButton Text="" ID = "lnkFollowUP" runat="server" />
<asp:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="lnkFollowUP"
CancelControlID="btnClose" BackgroundCssClass="modalBackground"  Drag="true" PopupDragHandleControlID="pnlPopup" >
</asp:ModalPopupExtender>
<asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
    
     <div class="panel-heading" style="background-color: #ACE9FB">
                            <center>
                                <div class="panel-title" style="fit-position: center;">
                                    <b>
                                        <p style="color: white; font-size: 25px">Follow UP</p>
                                    </b>
                                </div>
                            </center>
         
     </div>
    <div >
       <table width="100%">
           <tr>
             <td  style="width:90%"><div style="text-align:center"><asp:Label runat="server" ID="lblMsg" ForeColor="Red" Font-Bold="true"></asp:Label></div> </td>  
             <td  style="width:10%"><div style="text-align:right"><asp:Button ID="btnClose" runat="server" Text="X" Width="30px" CssClass="button" /></div></td>
           </tr>
       </table> 
    </div>
    <div class="body">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style = "width:20%">  <b> <b>Enquiry No </b></td>
                <td style = "width:20%"> <b> <asp:Label ID="lblFollowEnquiryNo" runat="server" ForeColor="#ff9900" /></b> </td>                   
                <td style = "width:20%"></td>
               <td style = "width:40%"></td>
            </tr>
             <tr>
                <td >
                    <b>Follow Up Status </b>
                </td>
                <td>
                    <asp:DropDownList ID="ddlFollowUpStatus" runat="server" />       
     <asp:RequiredFieldValidator ControlToValidate="ddlFollowUpStatus" ID="rvfFollowUpStatus" SetFocusOnError="true" ErrorMessage="*" InitialValue="-- Follow Up --" runat="server" ValidationGroup="vgFollowup"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <b>Next Folow Up Date </b>
                </td>
                <td>
                   <asp:TextBox ID="txtFollowUpDate" runat="server" onkeypress="return false;" onKeyDown="preventBackspace();"  Width="150px"
                       oncopy="return false;" onpaste="return false;" autocomplete="off"  ></asp:TextBox>
                     <asp:Image runat="server" ID="imgFollowUpDate" Width="25px" Height="30px" ImageUrl="~/images/calendar.png" />
                                                <asp:CalendarExtender ID="cleFollowUpDate" Format="dd MMM yyyy" runat="server"
                                                    CssClass="cal_Theme1" PopupButtonID="imgFollowUpDate" Enabled="true" TargetControlID="txtFollowUpDate">
                                                </asp:CalendarExtender>
     <asp:RequiredFieldValidator ControlToValidate="txtFollowUpDate" ID="rfvFollowUpDate" SetFocusOnError="true" ErrorMessage="*" runat="server" ValidationGroup="vgFollowup"></asp:RequiredFieldValidator>
       
                </td>
                </tr>
                <tr>
                <td>   Remarks </td>
                <td colspan="2"><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Width="300px" ></asp:TextBox></td>
                <td>
                 <asp:Button ID="btnSave" runat="server" ValidationGroup="vgFollowup" Text="Submit" CssClass="btn-lg btn-primary btn-block" Height="43px" Width="93px" OnClick="btnSave_Click" /></td>
                   
               
                
            </tr>
        </table>
        
    </div>

    
    <div>
 <asp:GridView ID="grdFollowUp" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="No_" EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="10" OnPageIndexChanging="grdFollowUp_PageIndexChanging" OnSelectedIndexChanged="OnSelectedIndexChanged">
                        <Columns>
               <asp:BoundField DataField="No_" HeaderText="Enquiry No" SortExpression="N_o" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" />
               <asp:BoundField DataField="LineNo" HeaderText="Line No" SortExpression="LineNo" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
               <asp:BoundField DataField="FollowUpStatusName" HeaderText="Follow Up Status" SortExpression="FollowUpStatusName" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
               <asp:BoundField DataField="NextFollowUpDate" HeaderText="Next Follow Up Date" SortExpression="NextFollowUpDate" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
               <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                      </Columns>                       
                        <AlternatingRowStyle CssClass="danger" />
                    </asp:GridView>
    </div>
</asp:Panel>
 
                </div>
      </asp:Panel>
  </ContentTemplate>
        <Triggers >
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

