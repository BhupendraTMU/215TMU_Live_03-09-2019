<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true"  EnableEventValidation="false"  CodeFile="ViewGrievances.aspx.cs" Inherits="Faculty_ViewGrievances" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
   
    .rbl input[type="radio"] {
            margin-left: 10px;
            margin-right: 1px;
        }
    .parent
    {
        text-align:center;
        display: block;
        border: 1px solid outset;
    }
    .child
    {
        display: inline-block;       
        width: 200px;
    }
</style>
        <style type="text/css">

        
     .GridPager a, .GridPager span
    {
        display: block;
        height: 20px;
        width: 15px;
        font-weight: bold;
        text-align: center;
        text-decoration: none;
    }
    .GridPager a
    {
        background-color: #f5f5f5;
       
        border: 1px solid #969696;
    }
    .GridPager span
    {
        background-color: #A1DCF2;
       
        border: 1px solid #3AC0F2;
    }   


    </style>

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
            function checkDate(sender, args) {
                var today = new Date();
                today.getHours(0, 0, 0, 0);
                if ($('[id$=txtFromtDate]').val() == '') {
                    alertify.error('First select the from date!');
                    sender._textbox.set_Value('');
                    return false;
                }
                else if (sender._selectedDate > today) {
                    alertify.error("You cannot select greater than current date!");
                    sender._selectedDate = new Date();
                    sender._textbox.set_Value('');
                }
                else {
                    var f = new Date($('[id$=txtFromtDate]').val());

                    if (sender._selectedDate < f) {
                        alertify.error("You cannot select less than from date!");
                        sender._textbox.set_Value('');
                    }
                }
            }

            function checkDate1(sender, args) {
                var today = new Date();
                //today.getHours(0, 0, 0, 0);
                if ($('[id$=txtFromtDate]').val() == '') {
                    alertify.error('First select the From date!');
                    sender._textbox.set_Value('');
                    return false;
                }
                else if (sender._selectedDate > today) {
                    alertify.error("You cannot select less than current date!");
                    sender._selectedDate = new Date();
                    sender._textbox.set_Value('');
                }
                else {
                    var f = new Date($('[id$=txtFromtDate]').val());

                    if (sender._selectedDate < f) {
                        alertify.error("You cannot select less than From date!");
                        sender._textbox.set_Value('');
                    }
                }
            }

        </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updmain">
        <ContentTemplate>

    
  <fieldset class="boxBodyInner">
      <center>         <div>
                                    <b>
                                        <p style="color:black; font-size: 25px; text-align:center;">Grievances List</p>
                                    </b>
                                    </div>
          </center>

      </fieldset>
                <div class="row tmu-form ml-5 mr-5">
                          
                             
                    <div>
                            
                                    <b>
                                        <asp:RadioButtonList id="rblType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"  OnSelectedIndexChanged="rblType_SelectedIndexChanged" CssClass="rbl" >
                                            <asp:ListItem Text="STUDENT" Value="STUDENT" Selected="True"/>
                                            <asp:ListItem Text="FACULTY" Value="FACULTY"/>                                            
                                        </asp:RadioButtonList>
                                    </b>
                          <br />
                                </div>
                          
                        </div>
             <div>

                   <table id="tblSearch" runat="server" style="width:100%;" >
                       <tr>
                          
                           <td style="width:50px;"></td>
                           <td>
                               <asp:TextBox runat="server" ID="txtCollege" Width="150px" placeholder="College Code" style="text-transform:uppercase" MaxLength="20" CssClass="form-control input-sm" Height="30px"></asp:TextBox>
                           </td>
                              <td>
                               <asp:TextBox runat="server" ID="txtCourse" Width="150px" placeholder="Course Code" style="text-transform:uppercase"   MaxLength="20" CssClass="form-control input-sm" Height="30px"></asp:TextBox>
                              <asp:TextBox runat="server" ID="txtFacultyCode" Width="150px" placeholder="Faculty Code" style="text-transform:uppercase" Visible="false"  MaxLength="20" CssClass="form-control input-sm" Height="30px"></asp:TextBox>

                              
                              </td>
                           <td>
                               <b>
                               <asp:TextBox runat="server" ID="txtFromtDate" CssClass="form-control input-sm" Width="120px" ToolTip="From Date" onkeypress="return false"
                                       onKeyDown="preventBackspace();" placeholder="From Date"></asp:TextBox>
                                <asp:Image src="../Images/Calendar.png" runat="server" Height="30px" Width="30px" alt="" ID="fdate" />
                                   <asp:CalendarExtender ID="cleFromDate" Format="dd MMM yyyy" runat="server" CssClass="cal_Theme1" OnClientDateSelectionChanged="checkDate"
                                       PopupButtonID="fdate" Enabled="true" TargetControlID="txtFromtDate" />
                                   &nbsp&nbsp&nbsp&nbsp
                        

                               </b>
                           </td>
                           <td>
                               <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control input-sm" Width="120px" ToolTip="To Date" onkeypress="return false"
                                   onKeyDown="preventBackspace();" placeholder="To Date"></asp:TextBox>
                                <asp:Image src="../Images/Calendar.png" runat="server" Height="30px" Width="30px" alt="" ID="tdate" />
                               <asp:CalendarExtender ID="cleToDate" Format="dd MMM yyyy" runat="server" CssClass="cal_Theme1"
                                   PopupButtonID="tdate" Enabled="true" TargetControlID="txtToDate" OnClientDateSelectionChanged="checkDate1" />
                                                 
                           </td>
                         
                           <td >


                               <asp:Button ID="BtnShow" type="button" Text="Show" class="btn btn-info btn" runat="server" OnClick="BtnShow_Click" ></asp:Button>
                            
                           </td>
                       </tr>
                   </table>

             </div>
      
                <asp:Panel ID="pnlList" runat="server" BorderWidth="2px" BorderColor="#ACE9FB" ScrollBars="Vertical">

         <div class="table-responsive">
                    <asp:GridView ID="grdGrievances" runat="server" 
                        GridLines="None" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                         AutoGenerateColumns="False" DataKeyNames="SerialNo" CssClass="table table-striped table-bordered table-hover"
                        EmptyDataText="There are no data records to display." AllowPaging="True" PageSize="10" 
                        OnPageIndexChanging="grdGrievances_PageIndexChanging" AllowSorting="True" 
                        OnRowDataBound="grdGrievances_RowDataBound" OnRowCommand="grdGrievances_RowCommand"  OnRowEditing="grdGrievances_RowEditing" 
                        OnRowUpdating="grdGrievances_RowUpdating" OnRowCancelingEdit="grdGrievances_RowCancelingEdit">
                        <Columns>                          
                            <asp:TemplateField HeaderText="Name" HeaderStyle-Width="12%"  >
                                <ItemTemplate>
                                    <asp:HiddenField ID="snoHidden" Value='<%# Eval("SerialNo") %>' runat="server" />
                            <asp:Label ID="lblName" runat="server"  Text='<%# Eval("Name") %>'   />
                                    
                                    </ItemTemplate>
                                <HeaderStyle  />
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="Enrollment No" HeaderStyle-Width="8%" > 
                             <ItemTemplate>
                            <asp:Label  ID="lblEnrollmentNo" runat="server"   Text='<%# Eval("EnrollmentNo") %>'   />  
                                    </ItemTemplate>
                                <HeaderStyle Width="175px" />
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="College Code" HeaderStyle-Width="8%" > 
                             <ItemTemplate>
                            <asp:Label  ID="lblCollege" runat="server"   Text='<%# Eval("CollegeCode") %>'   />  
                                    </ItemTemplate>
                                <HeaderStyle Width="175px" />
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Course Code" HeaderStyle-Width="8%" > 
                             <ItemTemplate>
                            <asp:Label  ID="lblCourse" runat="server"   Text='<%# Eval("CourseCode") %>'   />  
                                    </ItemTemplate>
                                <HeaderStyle Width="175px" />
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="Grounds For Appeal" HeaderStyle-Width="12%" >
                             <ItemTemplate>
                         
                                 <asp:TextBox ID="GroundsForAppeal" runat="server" Width="140px" Height="30px"   Text='<%# Eval("GroundsForAppeal") %>' TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                <HeaderStyle Width="175px" />
                                </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Appeal1" HeaderStyle-Width="9%" >
                             <ItemTemplate>                           
                                 <asp:TextBox ID="txtAppeal1" runat="server" Width="140px" Height="30px"  Enabled="false" TextMode="MultiLine" Text='<%# Eval("Appeal1") %>'></asp:TextBox>
                                    </ItemTemplate>                                
                                </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Appeal2" HeaderStyle-Width="9%" >
                             <ItemTemplate>
                           
                                 <asp:TextBox ID="txtAppeal2" runat="server" Width="140px" Height="30px" Enabled="false" TextMode="MultiLine" Text='<%# Eval("Appeal2") %>'></asp:TextBox>
                                    </ItemTemplate>
                                <HeaderStyle Width="175px" />
                                </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Date" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                             <ItemTemplate>
                            <asp:Label ID="lblGrievancesDate" runat="server"  Text='<%# Eval("GrievancesDate") %>'   />  
                                    </ItemTemplate>
                                <HeaderStyle Width="175px" />
                                </asp:TemplateField>                         
                            
                             <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="24%">
                                <ItemTemplate>
                            <div style="width: 200px; overflow: auto; white-space: nowrap; text-overflow: clip">
                                                                <%# Eval("ActionRemarks") %>
                                                            </div>
                                    </ItemTemplate>
                                  <EditItemTemplate>
                                   <table>
                                       <tr>
                                           <td>
                                                <asp:TextBox runat="server" ID="txtRemarks" Text='<%# Eval("ActionRemarks") %>' ></asp:TextBox>
                                           </td>
                                           <td>&nbsp&nbsp </td>
                                           <td align="right">
                                               &nbsp&nbsp  <asp:CheckBox runat="server" ID="chkResolve" Text='Resolve' Checked='<%# Eval("CloseGrievance") %>' ></asp:CheckBox>
                                           </td>
                                       </tr>
                                   </table> 
                                    
                                </EditItemTemplate>
                                 <HeaderStyle Width="400px" />
                            </asp:TemplateField>
                            
                             <asp:TemplateField  HeaderText="Update" HeaderStyle-Width="15%" >  
                    <ItemTemplate>  
                        <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" />  
                    </ItemTemplate>  
                    <EditItemTemplate  >  
                        <table>
                            <tr>
                                <td> <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update"/> </td>
                                <td>&nbsp;&nbsp;&nbsp; </td>
                                <td><asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel"/> </td>
                            </tr>
                        </table>
                        
                         
                    </EditItemTemplate>  
                                 <HeaderStyle Width="300px" />
                </asp:TemplateField>  

                             <asp:TemplateField HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="btnDownload" Text='<%# Eval("AttachmentD") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Download"  ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>                       
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle  BackColor="#ff9900" Font-Bold="True" ForeColor="White" Font-Size="11px" />
                <PagerStyle CssClass = "GridPager" HorizontalAlign="Center" BackColor="#ff9900"/>
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Size="10.1px" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" PreviousPageText="Previous" NextPageText="Next" FirstPageText="First" LastPageText="Last" Position="Bottom"  />
                    </asp:GridView>
             <br />
                 <asp:GridView ID="GrdFacultyGrievances" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                      AutoGenerateColumns="False" DataKeyNames="SerialNo" 
                        EmptyDataText="There are no data records to display." 
                     AllowPaging="true" PageSize="10" OnPageIndexChanging="GrdFacultyGrievances_PageIndexChanging" 
                     AllowSorting="true" OnRowDataBound="GrdFacultyGrievances_RowDataBound" OnRowCommand="GrdFacultyGrievances_RowCommand" 
                     OnRowEditing="GrdFacultyGrievances_RowEditing" OnRowCancelingEdit="GrdFacultyGrievances_RowCancelingEdit"
                      OnRowUpdating="GrdFacultyGrievances_RowUpdating"
                     
                     BorderStyle="None" BorderWidth="1px" CellPadding="4">
                        <Columns>    
                            
                            
                              <asp:TemplateField HeaderText="Name" HeaderStyle-Width="12%" >
                                <ItemTemplate>
                                    <asp:HiddenField ID="snoHidden" Value='<%# Eval("SerialNo") %>' runat="server" />
                            <asp:Label ID="lblName" runat="server"  Text='<%# Eval("Name") %>'   />  
                                    </ItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="Faculty Code" HeaderStyle-Width="8%" > 
                             <ItemTemplate>
                            <asp:Label  ID="lblEnrollmentNo" runat="server"  Text='<%# Eval("EnrollmentNo") %>'   />  
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="College Code" HeaderStyle-Width="8%" > 
                             <ItemTemplate>
                            <asp:Label  ID="lblCollege" runat="server"   Text='<%# Eval("CollegeCode") %>'   />  
                                    </ItemTemplate>
                                <HeaderStyle Width="175px" />
                                </asp:TemplateField>

                              

                            <asp:TemplateField HeaderText="Grounds For Appeal" HeaderStyle-Width="12%" >
                             <ItemTemplate>
                            
                                  <asp:TextBox ID="GroundsForAppeal" runat="server" Width="140px" Height="30px"   Text='<%# Eval("GroundsForAppeal") %>' TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Appeal1" HeaderStyle-Width="9%"  >
                             <ItemTemplate>
                            
                                 <asp:TextBox ID="txtAppeal1" runat="server" Width="140px" Height="30px"  Enabled="false" TextMode="MultiLine" Text='<%# Eval("Appeal1") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Appeal2" HeaderStyle-Width="9%" >
                             <ItemTemplate>
                           
                                  <asp:TextBox ID="txtAppeal2" runat="server" Width="140px" Height="30px" Enabled="false" TextMode="MultiLine" Text='<%# Eval("Appeal2") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Date" HeaderStyle-Width="5%" ItemStyle-Width="5%" >
                             <ItemTemplate>
                            <asp:Label ID="lblGrievancesDate" runat="server"  Text='<%# Eval("GrievancesDate") %>'   />  
                                    </ItemTemplate>
                                </asp:TemplateField> 
                            
                            
                            
                            
                            
                            
                            
                            
                                                  
                           <%-- <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="btnDownload" Text='<%# Eval("AttachmentD") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Download"  ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Old Remarks" >
                                <ItemTemplate>
                            <div style="width: 150px; overflow: auto; white-space: nowrap; text-overflow: clip">
                                                                <%# Eval("ActionRemarks") %>
                                                            </div>

                                    </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Remarks">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" ID="txtRemarks"  ></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>--%>
                             <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="24%">
                                <ItemTemplate>
                            <div style="width: 200px; overflow: auto; white-space: nowrap; text-overflow: clip">
                                                                <%# Eval("ActionRemarks") %>
                                                            </div>
                                    </ItemTemplate>
                                  <EditItemTemplate>
                                   <table>
                                       <tr>
                                           <td>
                                                <asp:TextBox runat="server" ID="txtRemarks" Text='<%# Eval("ActionRemarks") %>' ></asp:TextBox>
                                           </td>
                                           <td>&nbsp&nbsp </td>
                                           <td align="right">
                                               &nbsp&nbsp  <asp:CheckBox runat="server" ID="chkResolve" Text='Resolve' Checked='<%# Eval("CloseGrievance") %>' ></asp:CheckBox>
                                           </td>
                                       </tr>
                                   </table> 
                                    
                                </EditItemTemplate>
                                 <HeaderStyle Width="400px" />
                            </asp:TemplateField>
                            
                            
                           
                              <asp:TemplateField  HeaderText="Update" HeaderStyle-Width="15%">  
                    <ItemTemplate>  
                        <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" />  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <table>
                            <tr>
                                <td> <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update"/>  </td>
                                <td>&nbsp;&nbsp;&nbsp;</td>
                                <td> <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel"/>  </td>
                            </tr>
                        </table>
                       
                       
                    </EditItemTemplate>  
                </asp:TemplateField> 

                        </Columns>   
                     
                     <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle  BackColor="#ff9900" Font-Bold="True" ForeColor="White" Font-Size="11px" />
                <PagerStyle CssClass = "GridPager" HorizontalAlign="Center" BackColor="#ff9900"/>
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Size="10.1px" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        <AlternatingRowStyle CssClass="danger" />

                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" PreviousPageText="Previous" NextPageText="Next" FirstPageText="First" LastPageText="Last" Position="Bottom"  />
                    </asp:GridView>
                </div>
      </asp:Panel>
  </ContentTemplate>
        <Triggers >
           <%-- <asp:PostBackTrigger ControlID="grdGrievances" />--%>
             <asp:PostBackTrigger ControlID="rblType"  />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

