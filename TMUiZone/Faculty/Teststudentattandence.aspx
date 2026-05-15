<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="Teststudentattandence.aspx.cs" Inherits="Faculty_Teststudentattandence" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
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
          if (sender._selectedDate > today) {
              alertify.error("You cannot select less than current date!");
              sender._selectedDate = new Date();
              sender._textbox.set_Value(sender._selectedDate.format(sender._format))
          }

          else {
              var f = new Date($('[id$=txtDateTo]').val());
              if (sender._selectedDate > f) {
                  alertify.error("You cannot select Greater than To date!");
                  sender._textbox.set_Value('');
              }
          }


      }
      function checkDate1(sender, args) {

          var today = new Date();
          if (sender._selectedDate > today) {
              alertify.error("You cannot select less than current date!");
              sender._selectedDate = new Date();
              sender._textbox.set_Value(sender._selectedDate.format(sender._format))
          }
          if ($('[id$=txtDateFrom]').val() == '') {
              alertify.error('First select the from date!');
              sender._textbox.set_Value('');
              return false;
          }
          else {
              var f = new Date($('[id$=txtDateFrom]').val());

              if (sender._selectedDate < f) {
                  alertify.error("You cannot select less than from date!");
                  sender._textbox.set_Value('');
              }
          }
      }
    </script>
    <script type="text/javascript">
        function myFunction() {
            var arr = { StudentName: $('[id$=txtStudentNo]').val() };
            $.ajax({
                type: "POST",
                url: "EditStudentAttendance.aspx/GetCustomers",
                data: JSON.stringify(arr),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    alert('Error');
                }
            });
        }


        function OnSuccess(response) {

            $('[id$=pnlGrid]').show();
            if (response.d.length == 0) {
                $('[id$=btnSave]').attr("disabled", true);
                $('[id$=txtStudentNo]').focus();
                return false;
            }
            else {
                for (var i = 0; i < response.d.length; i++) {
                    $('[id$=grdAttendanceDetails]').append("<tr><td>" + response.d[i].EnrollmentNo + "</td><td>" + response.d[i].StudentNo + "</td><td>" + response.d[i].StudentName + "</td><td>" + response.d[i].FatherName + "</td><td>"
                         + response.d[i].CourseCode + "</td><td>" + response.d[i].Semester + "</td><td>" + response.d[i].Section + "</td><td><img src='../logo/close.png'  height='20px' width='20px'  class='btnDelete'/></td></tr>");
                }
                $('[id$=txtStudentNo]').val('');
                $('[id$=btnSave]').attr("disabled", false);
            }

            $(".btnDelete").bind("click", Delete);
        }
        function Delete() {
            var par = $(this).parent().parent();
            par.remove();
            var grid = document.getElementById("");
            var rowsCount = grid.rows.length;


            if (rowsCount == 2) {
                //  $('[id$=grdAttendanceDetails]').hide();
                $('[id$=pnlGrid]').hide();


            }
        }


        function ShowHide1() {
            $('[id$=btnSave]').attr("disabled", true);
            $('[id$=lblDateCommited]').show();
            $('[id$=txtDateCommited]').show();
            $('[id$=lblDateFrom]').hide();
            $('[id$=txtDateFrom]').hide();
            $('[id$=lblDateTo]').hide();
            $('[id$=txtDateTo]').hide();
            $('[id$=pnlGrid]').hide();
            $('[id$=chkManyDays]').prop("checked", false);
            $('[id$=txtDateCommited]').val('');
            $('[id$=txtDateFrom]').val('');
            $('[id$=txtDateTo]').val('');

        }


        $(document).ready(function () {

            ShowHide1();

            $('[id$=btnSave]').click(function () {

                if ($('[id$=chkManyDays]').is(':checked')) {
                    if ($('[id$=txtDateFrom]').val() == '' || $('[id$=txtDateTo]').val() == '') {
                        alertify.error('Please select the "From Date" and "To Date"');
                        return false;
                    }
                }
                else if ($('[id$=txtDateCommited]').val() == '') {
                    alertify.error('Please select the commited Date');
                    return false;
                }
                //veerendra ddlEventTypetxtRemarks

                if ($('[id$=txtRemarks]').val() == '') {

                    alertify.error('Please write Remarks');
                    return false;

                }

                var grid = document.getElementById("");
                 var Count1 = grid.rows.length;

                 if (Count1 < 2) {
                     alertify.error('Please Add Student');
                     return false;
                 }

                 var StudentNolist = new Array();
                 for (var j = 1; j < Count1; j++) {
                     var MName = grid.rows[j].cells[1].outerText;
                     StudentNolist[j] = MName;
                 };

                 var chkManyDays = $('[id$=chkManyDays]').is(':checked') ? "yes" : "no";
                 var chkPresent = $('[id$=chkPresent]').is(':checked') ? "yes" : "no";
                 var remarks = $('[id$=txtRemarks]').val();
                //  var Lecture
                 var Lecture = $('[id$=ddlLecture]').val();
                // Lecture = "ALL";
                // alert($('[id$=ddlLecture]').val());
                 var arr1 = {
                     StudentNolist: StudentNolist, DateCommited: $('[id$=txtDateCommited]').val(), DateFrom: $('[id$=txtDateFrom]').val(),
                     DateTo: $('[id$=txtDateTo]').val(), chkManyDays: chkManyDays, chkPresent: chkPresent, Lecture: Lecture, remarks: remarks, ddlEventType: $('[id$=ddlEventType]').val()
                 };
                 $.ajax({
                     type: "POST",
                     url: "EditStudentAttendance.aspx/Save",
                     data: JSON.stringify(arr1),
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     success: OnSuccess1,
                     error: function (xmlHttpRequest, textStatus, errorThrown) {
                         alert('Error');
                     }
                 });
            });
        });
         function OnSuccess1() {
             debugger
             $('[id$=grdAttendanceDetails]').find('tr:has(td)').each(function () {
                 $(this).remove();
             });
             alertify.success('Update Successfully');
             ShowHide1();
             return false;
         }

         function ShowHide(me) {
             if (me.checked == true) {
                 $('[id$=lblDateCommited]').hide();
                 $('[id$=txtDateCommited]').hide();
                 $('[id$=lblDateFrom]').show();
                 $('[id$=txtDateFrom]').show();
                 $('[id$=lblDateTo]').show();
                 $('[id$=txtDateTo]').show();
             }
             else {
                 $('[id$=lblDateCommited]').show();
                 $('[id$=txtDateCommited]').show();
                 $('[id$=lblDateFrom]').hide();
                 $('[id$=txtDateFrom]').hide();
                 $('[id$=lblDateTo]').hide();
                 $('[id$=txtDateTo]').hide();
             }
         }
         function ShowP(me) {
             if (me.checked == true) {
                 $('[id$=chkPresent]').prop("checked", true);
                 $('[id$=chkAbsent]').prop("checked", false);
             }
             else {
                 $('[id$=chkPresent]').prop("checked", false);
                 $('[id$=chkAbsent]').prop("checked", true);

             }
         }
         function ShowA(me) {
             if (me.checked == true) {
                 $('[id$=chkPresent]').prop("checked", false);
                 $('[id$=chkAbsent]').prop("checked", true);
             }
             else {
                 $('[id$=chkPresent]').prop("checked", true);
                 $('[id$=chkAbsent]').prop("checked", false);

             }
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="elist" runat="server">
        <ContentTemplate>
                     <fieldset class="boxBody">
<asp:HiddenField ID="hfStudentId" runat="server" />
                         <asp:Label ID="Label1" runat="server"  Visible="true" Text="Edit Student Attendance" Font-Size="15pt" ForeColor="#093A62" 
                     Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
                                     
                     </fieldset>
             <fieldset  style="background:#fefefe; border-top:1px solid #dde0e8; border-bottom:1px solid #dde0e8; padding:10px 20px; height:100%">
     <asp:Panel ID="Panel1" runat="server" BorderColor="#E8E8E8" BorderWidth="1px" Width="100%">
                    
                     <fieldset class="boxBodyInner">
                            
  <div class="panel-body" style="border-color:#337ab7; width:100%;border-bottom:groove;"   id="body2">
                            <div class="pull-left">
                                <asp:CheckBox runat="server" ID="chkManyDays" Onclick="ShowHide(this);" Text="Click for more then one days" Font-Bold="true"/>
                            &nbsp&nbsp&nbsp&nbsp  &nbsp&nbsp&nbsp&nbsp  &nbsp&nbsp&nbsp&nbsp  &nbsp&nbsp&nbsp&nbsp  </div>
                            
                             <div class="pull-left">
                                         <asp:CheckBox runat="server" ID="chkPresent" Onclick="ShowP(this);" Text="Present" Font-Bold="true" Checked="true" />
                                       &nbsp&nbsp&nbsp&nbsp 
                                      </div>
                                  <div class="pull-left">
                                  <asp:CheckBox runat="server" ID="chkAbsent" Onclick="ShowA(this);" Text="Absent" Font-Bold="true"/>
                                       </div>
                                  <div class="pull-right">
                                  <asp:DropDownList runat="server" ID="ddlLecture"  Visible="true">
                                       <asp:ListItem Value="">ALL</asp:ListItem>
                                       <asp:ListItem Value="1">I</asp:ListItem>
                                       <asp:ListItem Value="2">II</asp:ListItem>
                                       <asp:ListItem Value="3">III</asp:ListItem>
                                       <asp:ListItem Value="4">IV</asp:ListItem>
                                       <asp:ListItem Value="5">V</asp:ListItem>
                                       <asp:ListItem Value="6">VI</asp:ListItem>
                                       <asp:ListItem Value="7">VII</asp:ListItem>
                                       <asp:ListItem Value="8">VIII</asp:ListItem>
                                       <asp:ListItem Value="9">IX</asp:ListItem>
                                       <asp:ListItem Value="10">X</asp:ListItem>                                       
                                      </asp:DropDownList>
                                       </div>
                                 <div class="pull-right">
                                   <asp:Label runat="server" ID="lblLecture"  Text="Lecture" Font-Bold="true"   Visible="true"/>
                                       &nbsp&nbsp&nbsp&nbsp 
                                      </div>
                 <div class="pull-right">
                     <div style="text-align:left">
</div>
                 </div>                           

                            <div class="clearfix"></div>
                            <table class="table">
                            <br />
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblDateCommited" runat="server" Text="Date Commited:"></asp:Label>
                                        </td>
                                        <td>                              
                                          <asp:TextBox ID="txtDateCommited"  runat="server"  Width="150px"  Height="22px"  placeholder="Date" onkeypress="return false" onKeyDown="preventBackspace();" 
                                                               oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                                   <asp:CalendarExtender ID="CalendarExtender1" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDate"
                                                        CssClass="cal_Theme1" Enabled="true" TargetControlID="txtDateCommited"></asp:CalendarExtender>
                                         </td>
                                        <td>
                                               <asp:Label ID="lblDateFrom" runat="server" Text="From:"></asp:Label>
                                          
                                        </td>
                                        <td>
                                                 <asp:TextBox ID="txtDateFrom"  runat="server"  Width="150px"  Height="22px"  placeholder="From Date" onkeypress="return false" onKeyDown="preventBackspace();" 
                                                               oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                                   <asp:CalendarExtender ID="CalendarExtender4" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDate"
                                                        CssClass="cal_Theme1" Enabled="true" TargetControlID="txtDateFrom"></asp:CalendarExtender>
                                        </td>
                                        <td>
                                               <asp:Label ID="lblDateTo" runat="server" Text="To:"></asp:Label>
                                          
                                        </td>
                                        <td>
                                                 <asp:TextBox ID="txtDateTo"  runat="server"  Width="150px"  Height="22px"  placeholder="To Date" onkeypress="return false" onKeyDown="preventBackspace();" 
                                                               oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                                   <asp:CalendarExtender ID="CalendarExtender5" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDate1"
                                                        CssClass="cal_Theme1" Enabled="true" TargetControlID="txtDateTo"></asp:CalendarExtender>
                                        </td>
                                        <td>
                                            
                                        </td>
                                         
                                    </tr>

                                         
                                      <tr>
                                        <td>
                                             <asp:Label ID="eventtyp" runat="server">Reason</asp:Label>
                                        </td>
                                        <td>                              
                                           <asp:DropDownList ID="ddlEventType" runat="server" onchange='CheckColors(this.value);' Width="150px" CssClass="form-control input-sm">
                                                
                                            </asp:DropDownList>
                                            
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlEventType" ErrorMessage="**" SetFocusOnError="True" ValidationGroup="g1" InitialValue="--Select--"></asp:RequiredFieldValidator>        
                                        
                                             </td>
                                        <td>
                                                <asp:Label ID="txtr"  runat="server">Remarks</asp:Label>
                                          
                                        </td>
                                        <td colspan="3">
                                               <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" Width="420px" MaxLength="250" placeholder="Enter Maximum 250 Character"  ></asp:TextBox>
                     
 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRemarks" ErrorMessage="**" SetFocusOnError="True" ValidationGroup="g1" InitialValue=""></asp:RequiredFieldValidator>        
                              
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                                             </div> 
                         <br />
                             <table width="98%"><tr>
                                 <td align="left" width="25%"> <div>
                                 
                                 <div class="form-group" style="margin-left: 1%;"> 
                                       <div class="input-group">
                                       <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>                      
                                       <asp:TextBox ID="txtStudentNo" runat="server" Width="100%"  Height="1%"  autocomplete="off" placeholder="Student Name" onchange="myFunction()" ></asp:TextBox>                   
                                   </div>
                                    </div>                                  
                                 </div></td>
                                 <td>
                                    <div class="form-group" style="margin-left: 1%;"> 
                                       <div class="input-group">
                                       <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>                      
                                       <asp:TextBox ID="TextBox1" runat="server" Width="100%"  Height="1%"  autocomplete="off" placeholder="Student Name" onchange="myFunction()" ></asp:TextBox>                   
                                   </div>                                

                                 </td>
                                 <td align="right" width="10%"><asp:Button ID="btnsearch"  Text="Add" class="btn btn-info btn"  OnClick="btnsearch_Click" runat="server"></asp:Button>
                  </td>
                             </tr>


                             </table>
                           <asp:AutoCompleteExtender ServiceMethod="SearchCustomers" MinimumPrefixLength="1" CompletionInterval="0" EnableCaching="false" CompletionSetCount="6" 
                              TargetControlID="txtStudentNo"  CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                CompletionListHighlightedItemCssClass="itemHighlighted" ID="AutoCompleteExtender2" runat="server" FirstRowSelected = "true"></asp:AutoCompleteExtender>
                     
            <br />
         <div class="table-responsive">
            <asp:GridView ID="GridView1" runat="server"  DataKeyNames="No_" CssClass="table table-striped table-bordered table-hover"  AutoGenerateColumns="false" BackColor="White" EmptyDataText="There are no data records to display." BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Horizontal" AllowPaging="true"  OnRowDeleting="GridView1_RowDeleting"
    PageSize="55">
              <AlternatingRowStyle BackColor="#F7F7F7" />
     <Columns>
<asp:TemplateField HeaderText="Sr. No.">
                           <ItemTemplate >
                          <%# Container.DataItemIndex + 1 %>
                                     </ItemTemplate>
                                       <ItemStyle Width="6%" />
                                    </asp:TemplateField>

         <asp:templatefield>
             <HeaderTemplate>
       Select All: <asp:CheckBox ID="chkboxSelectAll"  AutoPostBack="true" checked="true"  OnCheckedChanged="chkboxSelectAll_CheckedChanged" 
       runat="server"/>
           </HeaderTemplate>
                    <ItemTemplate>
            <asp:CheckBox ID="chkEmp" checked="true" runat="server"></asp:CheckBox>
            </ItemTemplate>
               <ItemStyle Width="10%" />
             </asp:TemplateField>
        <%-- <asp:BoundField ItemStyle-Width="150px" DataField="Enrollment No_" HeaderText="Enrollment No">
                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:BoundField>--%>

         <asp:BoundField ItemStyle-Width="150px" DataField="No_" HeaderText="Student No">
                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:BoundField>
                      
         <asp:BoundField ItemStyle-Width="150px" DataField="Student Name" HeaderText="Student Name">
                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:BoundField>
         
         <asp:BoundField ItemStyle-Width="150px" DataField="Fathers Name" HeaderText="Student Name">
                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:BoundField>
        
         <asp:BoundField ItemStyle-Width="150px" DataField="Course Code" HeaderText="Course Name">
                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:BoundField>
         <asp:BoundField ItemStyle-Width="150px" DataField="Section" HeaderText="Section">
                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:BoundField>
         <asp:BoundField ItemStyle-Width="150px" DataField="Semester" HeaderText="Semester/Year">
                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:BoundField>
         
         <asp:BoundField ItemStyle-Width="150px" DataField="Global Dimension 1 Code" HeaderText="College Code">
                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:BoundField>
        <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />


         <asp:TemplateField>
                               <ItemTemplate> 
                   <asp:LinkButton  ID="gdlbtnRemove" OnClick="gdlbtnRemove_Click"  runat="server" Text="<img src='../logo/close.png'  height='20px' width='20px'  class='btnDelete'/>"></asp:LinkButton> 
                                 </ItemTemplate>
                                 <HeaderTemplate>Remove</HeaderTemplate>
                   </asp:TemplateField>
      
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
             <br />
         </div>
                     
                       <table width="98%"><tr>
                           <td align="right">
                                                                                        
                                <asp:Button ID="Btnupdate" type="button" Text="update"   class="btn btn-success" OnClick="Btnupdate_Click" runat="server"  ValidationGroup="g1"></asp:Button>
                  
                                        </td>
                              </tr>

                       </table>      
              </fieldset>
                            
                </asp:Panel>
              


</fieldset>

    </ContentTemplate>
        <Triggers>
           <%-- <asp:PostBackTrigger ControlID="btnExportToexcel" />
            <asp:PostBackTrigger ControlID="BtnExportpdf" />--%>
            
        </Triggers>
    </asp:UpdatePanel>
  
</asp:Content>

