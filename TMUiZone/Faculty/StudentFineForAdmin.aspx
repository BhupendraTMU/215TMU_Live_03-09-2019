<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="~/Faculty/StudentFineForAdmin.aspx.cs" Inherits="StudentFineForAdmin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="bootstrap/js/jquery-1.11.2.min.js"></script>
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="bootstrap/js/bootstrap.min.js"></script>

    <style>
        .red-border {
            border: 1px solid red;
        }
    </style>
    <style type="text/css"> 
        .completionList {
        border:solid 1px Gray;
        margin:0px;
        padding:3px;
        height: 140px;
        overflow:scroll;
        background-color: #FFFFFF;     
        } 
        .listItem {
        color: #191919;
        } 
        .itemHighlighted {
        background-color: #ADD6FF;       
        }
    </style>
    <script type="text/javascript">
       
        function validateLength(oSrc, args) {
            args.IsValid = (($('[id$=txtAmount]').val() == '0') && ($('[id$=drpActionTaken]').val() != 'Fine') || ($('[id$=txtAmount]').val() != '0') && ($('[id$=drpActionTaken]').val() == 'Fine'));
         }
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
        //Open Date --Back date can select--so comment it on 12 jan 2018
        function checkDate(sender, args) {
            if (sender._selectedDate < new Date()) {
                alert("You cannot select Less than current date!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset  style="background:#fefefe; border-top:1px solid #dde0e8; border-bottom:1px solid #dde0e8; padding:10px 20px; height:100%">
     <asp:UpdatePanel ID="updatepanel1" runat="server">
                        <ContentTemplate>
    <fieldset class="boxBodyHeader"> 
          <asp:Label ID="Label1" runat="server" 
            Text="Student Fine Details" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
              
          </fieldset>
                            <br />
                            <center>
                                <asp:Panel runat="server" BorderColor="#E8E8E8" BorderWidth="1px" Width="100%">
          <fieldset class="boxBodyInner"> 
           <table cellpadding="0px" cellspacing="0px"> 

          <tr> <td colspan="15" > 
      
       <table cellpadding="0px" cellspacing="0px">       
       <br />
       <tr> <td><label style="line-height:25px">  Enrollment No </label> </td> <td style="width:10px"> </td> <td>
         <asp:TextBox ID="txtStudentNo" runat="server" Width="250px" autocomplete="off" placeholder="Enrollment No/Student Name/Student No" AutoPostBack="true" OnTextChanged="txtStudentNo_TextChanged" ></asp:TextBox>                   
           <asp:HiddenField ID="hfStudentId" runat="server" />
                               <asp:AutoCompleteExtender ServiceMethod="SearchCustomers" MinimumPrefixLength="1" CompletionInterval="0" EnableCaching="false"
                                    CompletionSetCount="6" TargetControlID="txtStudentNo"  CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                     CompletionListHighlightedItemCssClass="itemHighlighted" ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "true"></asp:AutoCompleteExtender>

        </td> <td style="width:10px"> </td> 
           <td><label style="line-height:25px"> Name </label> </td> <td style="width:10px"> </td><td> 
        <asp:TextBox ID="txtName" runat="server" Enabled="False" Width="250px"></asp:TextBox></td> <td style="width:10px"> </td> 
           <td><label style="line-height:25px"> Course</label> </td> <td style="width:10px"> </td><td> 
        <asp:TextBox ID="txtCourse" runat="server" Enabled="False" Width="250px"></asp:TextBox></td></tr>
           <tr> <td colspan="11" style="height:10px"> </td></tr>
                      
            <tr>  <td>
                
                <label style="line-height:25px">  Semester/Year</label></td> <td style="width:10px"> </td><td> 
             <asp:TextBox ID="txtSemester" runat="server" Enabled="False" Width="250px"></asp:TextBox>
                </td> <td style="width:10px"> </td>
                <td><label style="line-height:25px"> Academic Year </label> </td> <td style="width:10px"> </td><td> 
        <asp:TextBox ID="txtAcademicYear" runat="server" Enabled="False" Width="250px"></asp:TextBox></td> <td style="width:10px"> </td> 
           <td><label style="line-height:25px"> Section </label> </td> <td style="width:10px"> </td><td> 
        <asp:TextBox ID="txtSection" runat="server" Enabled="False" Width="250px"></asp:TextBox></td></tr>
           <tr> <td colspan="11" style="height:10px"> </td>
            </tr>

          <tr> 
             <td> <label style="line-height:25px"> College </label></td>
             <td style="width:10px"> </td>
             <td> <asp:TextBox ID="txtCollege" runat="server" Enabled="False" Width="250px"></asp:TextBox> </td>
             <td style="width:10px"> </td>
             <td colspan="7" style="height:10px"></td>

          </tr>
           </table>
       
       </td></tr>
       </table>

          </fieldset>
                                    </asp:Panel>
                    </center>
                             <br />     
                            <center>
                    <div class="panel-body" style="border-color:#337ab7; width:80%; border-top:groove"  id="body2">
                        <table class="table" width="80%">
                            <thead>
                                <tr>
                                    <th style="font-size:15px">Date Committed</th>
                                    <th style="font-size:15px">Action Taken</th>
                                    <th style="font-size:15px">Fine Amount</th>
                                    <th style="font-size:15px">Remarks</th>
                                    <th style="font-size:15px">Save</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                               
                                   <asp:TextBox ID="txtDateCommited" CssClass="form-control input-sm" runat="server" Width="120px" onkeypress="return false" onKeyDown="preventBackspace();" ></asp:TextBox>
                                        
                                    <asp:Image src="../Images/Calendar.png" runat="server" Height="30px" Width="25px" alt="" id="fdate" />
                                   <%-- <asp:CalendarExtender ID="CalendarExtender2" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDate"
                                        CssClass="cal_Theme1" PopupButtonID="fdate" Enabled="true" TargetControlID="txtDateCommited">
                                   </asp:CalendarExtender>--%>
                                  <asp:CalendarExtender ID="CalendarExtender2" Format="dd MMM yyyy" runat="server" CssClass="cal_Theme1" PopupButtonID="fdate" Enabled="true" TargetControlID="txtDateCommited">
                                   </asp:CalendarExtender>

                                        </td>
                                    <td>
                                            <asp:DropDownList ID="drpActionTaken" AutoPostBack="true" OnSelectedIndexChanged="drpActionTaken_SelectedIndexChanged" CssClass="form-control input-sm" Width="120px" runat="server"></asp:DropDownList>
                                          
                                    </td>
                                    <td>
                                             <asp:TextBox ID="txtAmount" Width="120px" Text="0" Enabled="false" runat="server" CssClass="form-control input-sm" placeholder="Amount"></asp:TextBox>
                                             <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtAmount" FilterType="Numbers, Custom"></asp:FilteredTextBoxExtender>
                                             
                                    </td>
                                     <td>
                                             <asp:TextBox ID="txtRemarks" Width="250px"  runat="server" CssClass="form-control input-sm" placeholder="Remarks Max 250 characters" MaxLength="250"></asp:TextBox>
                                             <asp:RequiredFieldValidator ID="rfvtxtRemarks" ForeColor="Red" ControlToValidate="txtRemarks" ValidationGroup="g1" runat="server" Display="Dynamic" ErrorMessage="Please enter Remarks!"></asp:RequiredFieldValidator>
                                             
                                    </td>
                                     <td>
                                             <asp:Button ID="btnSave" runat="server" Enabled="false"  Height="30px" ValidationGroup="g1" class="btn btn-success" Text="Save" OnClick="btnSave_Click"  />                                             
                                             
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" ControlToValidate="txtDateCommited" ValidationGroup="g1" runat="server" Display="Dynamic" ErrorMessage="Please enter the date!"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" ControlToValidate="drpActionTaken" InitialValue="-- Select --" ValidationGroup="g1" runat="server" Display="Dynamic" ErrorMessage="Please select the action!"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:CustomValidator id="CustomValidator2" runat="server" ValidationGroup="g1" ControlToValidate = "txtAmount" ErrorMessage = "Please enter the amount!"
                                            ClientValidationFunction="validateLength" ForeColor="red" ></asp:CustomValidator>
                                    </td>
                                    <td>

                                    </td>
                                    <td></td>

                                </tr>
                            </tbody>
                        </table>
                        
                     <div class="table-responsive">

                          <asp:GridView ID="grdFineInfo" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" OnPageIndexChanging="grdFineInfo_PageIndexChanging"  EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="10">
               <AlternatingRowStyle BackColor="#F7F7F7" />
                        <Columns>                            
                            <asp:BoundField DataField="Enrollment No_" HeaderText="Enrollment No" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                           
                             <asp:BoundField DataField="Staff Code" HeaderText="Staff Code" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Action Taken" HeaderText="Action Taken" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Fine Amount" HeaderText="Amount" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="createdate" HeaderText="Imposed Date" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="DateCommited" HeaderText="Date Commited" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                           
                        
                        </Columns>                       
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
               <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
               <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
               <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont"   />
               <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
               <SortedAscendingCellStyle BackColor="#F4F4FD" />
               <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
               <SortedDescendingCellStyle BackColor="#D8D8F0" />
               <SortedDescendingHeaderStyle BackColor="#3E3277" />
                    </asp:GridView>
                                         </div>
                    </div>
                                </center>                             
                         </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtStudentNo" EventName="TextChanged"/>
                            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click"/>
                        </Triggers>
                    </asp:UpdatePanel>                    
               
              </fieldset>
            
</asp:Content>

