<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="LeaveM.aspx.cs" Inherits="Faculty_LeaveM" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        #grdLeavedetailwrap
{
width:650px;
height:100%;
overflow:scroll;
}

          #grdViewLeaveStatuswrap
{
width:650px;
height:100%;
overflow:scroll;
}


    </style>
    <script type="text/javascript">
        function Confirm_Delete() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Cancel  this leave ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

        function Confirm_Apply() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            var Value = '';
            if ($('[id$=txtNoOfLeavePriod]').val() == '')
                Value = '0';
            else
                Value = $('[id$=txtNoOfLeavePriod]').val();
            if (confirm("Do you want to Apply ? " + Value + " days leave")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

</script>

    <script type = "text/javascript">
        function RadioCheck(rb) {
            var gv = document.getElementById("<%=grdViewLeaveStatus.ClientID%>");
            var rbs = gv.getElementsByTagName("input");

            var row = rb.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
        }
</script>
    <script type="text/javascript">

        function CheckBoxCount() {
            //var nooflea = document.getElementById('txtNoOfLeavePriod');
            var value = document.getElementById('<%=txtNoOfLeavePriod.ClientID%>').value;

            var gv = document.getElementById("<%= grdCOLeave.ClientID %>");
            var inputList = gv.getElementsByTagName("input");
            var numChecked = 0;

            for (var i = 0; i < inputList.length; i++) {
                if (inputList[i].type == "checkbox" && inputList[i].checked) {
                    numChecked = numChecked + 1;

                }
            }
            //alert(textnol);
            if (numChecked == value) {

                var grd = document.getElementById("<%= grdCOLeave.ClientID %>");
                var inputList2 = grd.getElementsByTagName("input");

                for (var j = 0; j < inputList.length; j++) {
                    if (inputList2[j].type == "checkbox") {

                        inputList2[j].disabled = true;
                    }
                }


            }
        }


    </script>

    
    <script type="text/javascript" language="javascript">
        function CheckOne(me) {
            debugger
            $('[id$=chkArrangement]').attr('checked', false);
            $('[id$=chkWithoutArrangement]').attr('checked', false);
            me.checked = true;
            if (me.id == 'ContentPlaceHolder1_chkArrangement') {
                document.getElementById("<%=rfvtxtReason.ClientID%>").enabled = false
                // alert("y");
            }
            else {
                document.getElementById("<%=rfvtxtReason.ClientID%>").enabled = true
                // alert("N")
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
 <fieldset class="boxBody" >
          <asp:Label ID="Label7" runat="server" 
            Text="Leave balances displayed here are provisional and subject to scrutiny and revision as per leave policy." Font-Size="10pt" Font-Bold="true" ForeColor="Red" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
     
         </fieldset>
    <fieldset class="boxBody" >
 <asp:Label ID="Label1" runat="server" 
            Text="Leave" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
           
 </fieldset>


    <fieldset class="boxBody"> 

<table cellpadding="0px" cellspacing="0px">  <tr>  <td  style="width:10px">  </td>
<td  style="width:200px" valign="top"> 

<table cellpadding="0px" cellspacing="0px" class="leftbg1" style="width:180px; height:430px">  <tr> <td style="width:10px"> </td> <td>


<table cellpadding="0px" cellspacing="0px" >
 <tr> <td style="height:10px"> </td></tr>
 <tr> <td class="leftmMenu">   &nbsp;<asp:LinkButton ID="lnkProfileview" runat="server" onclick="lnkleaveview_Click" 
        >Leave Application</asp:LinkButton>
     </td></tr>
    <tr> <td style="height:10px"> </td></tr>
     <tr> <td class="leftmMenu">  &nbsp;<asp:LinkButton ID="lnkRejectProfileDetail" runat="server" 
             onclick="lnkRejectLeaveDetail_Click" >View Leave Status</asp:LinkButton></td></tr>
     <tr> <td style="height:10px"> 
        </td></tr>


     <tr > <td class="leftmMenu1">  &nbsp;<asp:LinkButton ID="lnkLeaveApprovalforHOD" runat="server" 
            Visible="false" PostBackUrl="~/Faculty/LeaveApproval.aspx"  >Leave Approval</asp:LinkButton>&nbsp;&nbsp; 
         <asp:Label ID="lblLeaveCount" runat="server"  ForeColor="Red" Visible="false"></asp:Label> </td></tr>
     <tr> <td style="height:10px"> 
         &nbsp;</td></tr>


    <tr> <td style="visibility:hidden">

        <asp:Label ID="Label4" runat="server" 
            Text=" Color Represent in Calender" Font-Size="10pt" ForeColor="#093A62" Font-Names="Open Sans" ></asp:Label>

         </td></tr>
    <%--<asp:BoundField DataField="Time Out" HeaderText="Time Out" DataFormatString="{0:t}" />
                     <asp:BoundField DataField="Hours Present" HeaderText="Hours Present" DataFormatString="{0:0.00}"/>--%>


     <tr> <td style="height:5px"> 
         &nbsp;</td></tr>

     <tr> <td style="visibility:hidden">
       <table cellpadding="0px" cellspacing="0px"><tr> <td> <div style="background-color:Gray; width:5px; color:Gray" >c </div></td> <td>  <label >&nbsp;&nbsp;&nbsp;Days Blocked</label></td></tr> </table>  

         </td></tr>
     <tr> <td style="height:2px"> 
        </td></tr>
    <tr> <td style="visibility:hidden">
       <table cellpadding="0px" cellspacing="0px"><tr> <td> <div style="background-color:lightblue ; width:5px; color:lightblue" >c </div></td> <td> <label>&nbsp;&nbsp; Leave</label></td></tr> </table>  

         </td></tr>

     <%--<div style="color:red">  Whether holiday to be counted if it falls between continuous leaves</div>--%>

 

     <tr> <td style="height:2px"> 
        </td></tr>


    <tr> <td style="visibility:hidden">
       <table cellpadding="0px" cellspacing="0px"><tr> <td> <div style="background-color:lawngreen ; width:5px ; color:lawngreen" >c </div></td> <td> <label> &nbsp;&nbsp;Marked</label ></td></tr> </table>  

         </td></tr>

   <tr> <td style="height:2px"> 
        </td></tr>

    <tr> <td style="visibility:hidden">
       <table cellpadding="0px" cellspacing="0px"><tr> <td> <div style="background-color:white ; width:5px; color:white" >c </div></td> <td> <label>&nbsp;&nbsp;To be Marked </label></td></tr> </table>  

         </td></tr>
    <tr> <td style="height:2px"> 
        </td></tr>
     <tr> <td style="visibility:hidden">
         <%--<div style="color:red">Whether off days to be counted if it falls between continuous leaves </div>--%>
         <table  runat="server" id="tblPostedAttendance" visible="false">
           <tr>
                <td>
                    <div style="background-color:SandyBrown ; width:5px; color:SandyBrown" >c </div>

                </td> <td> <label >&nbsp;&nbsp;Posted Attendance </label></td></tr> </table>  

         </td></tr>
    <tr> <td style="height:2px"> 
        </td></tr>

    <tr> <td >  
       

         </td></tr>




    </table>
 </td> <td style="width:10px"> </td></tr></table>



    
</td>  <td style="width:30px">  </td>
 <td valign="top"> 
 
 
    <asp:Panel ID="pnlLeaveApplication" runat="server" CssClass="leftBackground">

    <table cellpadding="0px" cellspacing="0px">
         <tr> <td style="width:10px"> </td> <td>
    
    <table cellpadding="0px" cellspacing="0px">  <tr> <td > 
        <br />
   
        <table cellpadding="0px" cellspacing="0px"> <tr> <td>    <asp:Label ID="Label2" runat="server" 
            Text="Leave Application" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
            </td> <td style="width:50px">  </td> 
            <td>  
                
                <table cellpadding="0px" cellspacing="0px"> 
                
                <tr> <td> <asp:Label ID="Label6" runat="server" Text=" Note : Approval Authority : 1 ) " ForeColor="Green" Font-Size="12pt"></asp:Label> <asp:Label ID="lblfirstApproval" runat="server" ForeColor="#FF3300" Font-Size="10pt"></asp:Label>  .&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 2) </td> <td>  <asp:Label ID="lblSecondApproval" runat="server" ForeColor="#FF3300" Font-Size="10pt"></asp:Label></td></tr> 
                    <tr> <td colspan="2">  <asp:Label ID="lblApprovalAuthority1" runat="server" Text="Approval authority not tag . Please contact system admin ,otherwise leave can't apply." Visible="False" Font-Bold="True" ForeColor="Red" Font-Size="11pt"></asp:Label></td></tr>
                     <tr> <td colspan="2">  <asp:Label ID="lblHRAuthority" runat="server" Text="HR authority not tag . Please contact system admin ,otherwise leave can't apply." Visible="False" Font-Bold="True" ForeColor="Red" Font-Size="11pt"></asp:Label></td></tr>

                </table>

                


                                                  </td> </tr> 



        </table>


 
  </td></tr>
    <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>

        <tr> <td>  

          <table> <tr> <td>


                       </td> <td style="width:30px">  </td> <td>  <asp:Label ID="lblholiday" runat="server" Font-Bold="True" ForeColor="#990000" Visible="false"></asp:Label></td></tr>

              <tr> <td>


                   </td> <td style="width:30px">  </td> <td>  <asp:Label ID="lbloffday" runat="server" Font-Bold="True" ForeColor="#990000" Visible="false"></asp:Label></td></tr> 


          </table> 


             </td></tr>
        
         <tr> <td style="height:5px"> 
             <asp:Label ID="lblleaveUpto" runat="server" Visible="False"></asp:Label>
             </td>

         </tr>   
        <tr> 
            <td>
                <table cellpadding="0px" cellspacing="0px" >
                    <tr>
                        <td style="width: 70px">
                            <label>Leave Type</label>
                        </td>
                        <td style="width: 180px">
                            <asp:DropDownList ID="ddLeaveTypen" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddLeaveTypen_SelectedIndexChanged" Height="29px" Width="80px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddLeaveTypen" ErrorMessage="*" ForeColor="#CC0000" InitialValue="-------" SetFocusOnError="True" ValidationGroup="lapply"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblLeaveBalance_CL" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td>
                         <table runat="server" id="tblArrangement">
                            <tr>
                                <td style="width: 100px">&nbsp&nbsp  Arrangement  &nbsp&nbsp</td>
                        <td style="width: 70px">
                            <asp:CheckBox ID="chkArrangement" runat="server" Onclick="CheckOne(this)"  Text="Yes"></asp:CheckBox>&nbsp&nbsp

                        </td>
                        <td style="width: 60px">
                            <asp:CheckBox ID="chkWithoutArrangement" runat="server" Checked="true" Text="No" Onclick="CheckOne(this)" />

                        </td>
                                <td style="width:10px"> </td> <td id="tdEmployee" runat="server" visible="false"> 
                                    
                                   Employee List :&nbsp<asp:DropDownList ID="drpEmployee" runat="server"></asp:DropDownList>
                                     </td>

                            </tr>
                        </table>
                        
                        </td>
                        

                    </tr>
                </table>
              </td>  </tr>        
                       
    <tr> <td style="height:10px"> </td></tr>


        <tr><td align="right"> 
            
            <table cellpadding="0px" cellspacing="0px">
                 <tr> <td>             
                       <asp:Panel ID="pnlCLApplyLeave" runat="server" Visible="false"> 
                           <table cellpadding="0px" cellspacing="0px" style="color:red">
                                <tr> <td> CL Leave can apply not more than</td> <td style="width:10px"> </td>
                                    <td> </td> <td style="width:10px"> </td>
                                    <td> <asp:Label ID="lblNonof_Cl_leave" runat="server" Font-Bold="True"></asp:Label></td></tr> </table> </asp:Panel>



                      </td><td style="width:10px"> </td> <td>No Of Leave  </td> <td style="width:10px"> </td>   <td>   <asp:TextBox ID="txtNoOfLeavePriod" runat="server" Enabled="False" Font-Bold="True" ForeColor="#CC0000" Width="80px">0</asp:TextBox>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtNoOfLeavePriod" ErrorMessage="*" ForeColor="#CC0000" SetFocusOnError="True" ValidationGroup="lapply"></asp:RequiredFieldValidator>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNoOfLeavePriod" ErrorMessage="*" ForeColor="#CC0000" InitialValue="0" SetFocusOnError="True" ValidationGroup="lapply"></asp:RequiredFieldValidator></td></tr> </table>
</td> </tr>
   
                       
    <tr> <td style="height:10px"> 
        <asp:Label ID="lblCLErrorRemarks" runat="server" ForeColor="#FF3300" Font-Bold="True"></asp:Label>
        </td></tr>


        <tr> <td>   <asp:GridView ID="grdCOLeave" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" Width="866px" OnRowCreated="grdCOLeave_RowCreated" OnRowDataBound="grdCOLeave_RowDataBound" >
                 <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                 <Columns>

                     

                     <asp:TemplateField HeaderText="Select">
                         <ItemTemplate>
                             <asp:CheckBox ID="chkSelectCO" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelectCO_CheckedChanged"   />
                         </ItemTemplate>
                     </asp:TemplateField>

                        <asp:TemplateField HeaderText="Date">
                         <ItemTemplate>
                             <asp:Label ID="lblAttendacedate" runat="server" Text='<%#Bind("[Co_Date]","{0:dd MMM yyyy}") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>


                      <asp:TemplateField HeaderText="Time in">
                         <ItemTemplate>
                             <asp:Label ID="lblTimeingrd" runat="server" Text='<%#Bind("[In time]","{0:t}") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                       <asp:TemplateField HeaderText="Time Out">
                         <ItemTemplate>
                             <asp:Label ID="lblTimeinoutgrd" runat="server" Text='<%#Bind("[Out Time]","{0:t}") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                      <asp:TemplateField HeaderText="Hours Present">
                         <ItemTemplate>
                             <asp:Label ID="lblHoursPresent" runat="server" Text='<%#Bind("[Present Hour]","{0:0.00}") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>

                     <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left">
                         <ItemTemplate>
                             <div style="width: 200px; overflow:auto; white-space: nowrap; text-overflow:clip">
                                 <%# Eval("Remarks") %>
                             </div>
                         </ItemTemplate>
                         <ItemStyle HorizontalAlign="Left" />
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Expire On">
                         <ItemTemplate>
                             <asp:Label ID="lblExpireCO" runat="server" Text='<%#Bind("[Expire on]","{0:dd MMM yyyy}") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="ID" Visible="false">
                         <ItemTemplate>
                             <asp:Label ID="lblidff" runat="server" Text='<%#Bind("id") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                 </Columns>
                 <EditRowStyle BackColor="#999999" />
                 <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                 <HeaderStyle HorizontalAlign="Left" BackColor="#ed7600" ForeColor="White" CssClass="cssGridheaderfont" />
                 <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                 <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                 <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                 <SortedAscendingCellStyle BackColor="#E9E7E2" />
                 <SortedAscendingHeaderStyle BackColor="#506C8C" />
                 <SortedDescendingCellStyle BackColor="#FFFDF8" />
                 <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
             </asp:GridView>
 </td></tr>
         <tr> <td style="height:10px">

             <asp:Label ID="lblerrorthanthree" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Red"></asp:Label>

              </td></tr>
   


   <tr> <td> 
       <table cellpadding="0px" cellspacing="0px"> 
           
          
           <tr> <td> 

               <table cellpadding="0px" cellspacing="0px"> 
                    <tr>
                 <td>   <label> From Date </label></td> <td style="width:5px"> </td> <td> 
       <asp:TextBox ID="txtfromDate" runat="server" AutoPostBack="True" autocomplete="off" OnTextChanged="txtfromDate_TextChanged" Width="100px" onkeydown="return false;" 
           oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox> 
       
        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfromDate" Format="dd MMM yyyy">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" WatermarkText="dd MMM yyyy" TargetControlID="txtfromDate">
      </cc1:TextBoxWatermarkExtender>

       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfromDate" ErrorMessage="*" ForeColor="#CC0000" SetFocusOnError="True" ValidationGroup="lapply">*</asp:RequiredFieldValidator>

       </td> <td style="width:15px"> </td> <td> <asp:CheckBox ID="chkPostlunch" runat="server" Text="Post Lunch" Visible="False" AutoPostBack="True" OnCheckedChanged="chkPostlunch_CheckedChanged" /> </td>
                        <td style="width:15px"> </td>  <td> <label> To Date</label></td> <td style="width:10px"> </td> <td>
           <asp:TextBox ID="txtTodate" runat="server" AutoPostBack="True" OnTextChanged="txtTodate_TextChanged" Width="100px" autocomplete="off" onkeydown="return false;" 
           oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox> 
             <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTodate" Format="dd MMM yyyy">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" WatermarkText="dd MMM yyyy" TargetControlID="txtTodate">
      </cc1:TextBoxWatermarkExtender>

           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTodate" ErrorMessage="*" ForeColor="#CC0000" SetFocusOnError="True" ValidationGroup="lapply"></asp:RequiredFieldValidator>

           </td>  <td style="width:10px">  </td> <td> 
               <asp:CheckBox ID="chkPrelunch" runat="server" Text="Pre Lunch" Visible="False" AutoPostBack="True" OnCheckedChanged="chkPrelunch_CheckedChanged" /> </td> 
                        <td style="width:10px">  </td> <td> <asp:Label ID="lblPeriodText" runat="server" Text="Period"></asp:Label> </td>  <td style="width:10px"> </td> 
                         <td><asp:DropDownList ID="ddLeavePeriod" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddLeavePeriod_SelectedIndexChanged" Visible="true" Width="80px">
                     <asp:ListItem Selected="True">(Full-Day)</asp:ListItem>
                     <asp:ListItem>(Half-Day)</asp:ListItem>
                 </asp:DropDownList> </td>  <td style="width:10px"> </td> <td>
                     <asp:Label ID="lblDayModeText" runat="server" Text="Day Mode"></asp:Label></td> <td style="width:10px"> </td><td> 
                         <asp:DropDownList ID="ddShiftType" runat="server" Enabled="False" OnSelectedIndexChanged="ddShiftType_SelectedIndexChanged">
       <asp:ListItem Value="0">-</asp:ListItem>
       <asp:ListItem Value="1">1 st Half</asp:ListItem>
       <asp:ListItem Value="2">2 nd Half</asp:ListItem>
       </asp:DropDownList>       </td> </tr>

               </table>

                </td></tr>


       
       <tr> <td style="height:5px"> </td></tr>
     <tr> <td> 

         <table cellpadding="0px" cellspacing="0px">

               <tr>
           <td>
               <label>Reason </label>
           </td>
           <td style="width: 10px"></td>
           <td colspan="12">
               <asp:TextBox ID="txtReason" runat="server" Width="700px" Height="50px" MaxLength="250"
                   TextMode="MultiLine"></asp:TextBox>
           </td>
           <td>
               <asp:RequiredFieldValidator ID="rfvtxtReason" runat="server" ControlToValidate="txtReason" 
                   ErrorMessage="Required" ForeColor="#CC0000" SetFocusOnError="True" ValidationGroup="lapply" Enabled="false"></asp:RequiredFieldValidator>
           </td>
       </tr>
         </table>
          </td></tr>
      
       <tr> <td style="height:5px"> </td></tr>
         <tr>
             <td colspan="7" align="left"><%--Attachment for SL more than 3--%>
                
                       <asp:FileUpload ID="flUpload" runat="server"  Visible="false"/>
                 <asp:RequiredFieldValidator ID="rfvflUpload" runat="server" Font-Bold="true" ForeColor="Red" ControlToValidate="flUpload" ValidationGroup="lapply"
                     ErrorMessage="* Upload Attachment" Enabled="false" ></asp:RequiredFieldValidator>

             <%--Attachment for SL more than 3--%>
             </td>
              <td colspan="10" align="right">
            <%-- onclick="CheckBoxCount();"--%>             
             

              <asp:TextBox ID="txtPhoneNo" runat="server" Width="120px" Visible="False" Height="5px"></asp:TextBox>
 
           <asp:Button ID="btnSave" runat="server" CssClass="btnLogin"  Text="Apply" ValidationGroup="lapply" OnClick = "OnConfirm" OnClientClick = "Confirm_Apply()" />
              </td></tr>




<tr> <td colspan="17" style="height:10px"> 
    <asp:Label ID="lblHolidayexpect" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblOffdayexpect" runat="server" Visible="False"></asp:Label><asp:Label ID="lbllwpleaveprevbalance" runat="server"></asp:Label>
    </td></tr>
   


       </table> </td></tr>


       <tr> <td style="height:13px" align="center"> 

            <asp:Label ID="lblerrorMessage" runat="server" Font-Bold="True" ForeColor="#CC0000" Font-Size="14pt"></asp:Label>

           <asp:Label ID="lblerror" runat="server" Font-Bold="True" ForeColor="Maroon" Visible="False"></asp:Label>
           <asp:Label ID="lblTotalLeave" runat="server" Visible="False"></asp:Label>
           <asp:Label ID="lblcountholiday" runat="server" Visible="False"></asp:Label>
           <asp:Label ID="lblCountOffDay" runat="server" Visible="False"></asp:Label>
           <asp:Label ID="lblTotalBalance" runat="server" Font-Bold="True" ForeColor="#CC0000" Visible="False"></asp:Label>
            <asp:Label ID="lblabalance" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblHalfDay" runat="server" Visible="False"></asp:Label>
           </td></tr>


      <tr> <td>  
          <table cellpadding="0px" cellspacing="0px"> <tr> <td>
              
              <asp:GridView ID="grdleave" runat="server" AutoGenerateColumns="False" Width="866px" OnRowCreated="grdleave_RowCreated">
                  <Columns>
                      <asp:BoundField DataField="Leave code" HeaderText="Leave Code" DataFormatString="{0:F2}"/>
                      <%--<asp:BoundField DataField="Leave Balance" HeaderText="Leave Balance" DataFormatString="{0:F2}" /> ----commment by Ashu 14-07-2016 --%>
                       <asp:BoundField DataField="Leave Balance" HeaderText="Leave Balance" DataFormatString="{0:F2}" />
                      <asp:BoundField DataField="Unapproved Leave" HeaderText="Pending Approval" DataFormatString="{0:F2}"  />
                  </Columns>
                  <EmptyDataTemplate>
                      There is no leave detail
                  </EmptyDataTemplate>
                  <HeaderStyle HorizontalAlign="Left" BackColor="#ed7600" ForeColor="White" CssClass="cssGridheaderfont" />
                      <RowStyle  CssClass="cssGridheaderfont"  />
              </asp:GridView>


          </td></tr>

                  <tr> <td style="height:13px" align="right"> 
                      
                      <asp:Label ID="lblUnapprovedLeave" runat="server" Visible="False"></asp:Label>
                      </td></tr>

    <tr> <td class="leftm"> </td></tr>

        <tr> <td>


           <%-- <div id="grdLeavedetailwrap"> --%>
            
            <asp:GridView ID="grdLeaveDetail" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanging="grdLeaveDetail_PageIndexChanging" PageSize="4" Width="900px" OnRowCreated="grdLeaveDetail_RowCreated" OnRowDataBound="grdLeaveDetail_RowDataBound" >
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btndelete" runat="server" Text="Cancel Leave" OnCommand="btndelete_Command"  CommandArgument='<%#Bind("id") %>'  OnClientClick="Confirm_Delete()" Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField>
                         <ItemTemplate>
                             <asp:Button ID="btnworkingDetails" runat="server" Text="Working Details" CommandArgument='<%#Bind("AutoNo") %>' OnCommand="btnworkingDetails_Command" />
                         </ItemTemplate>
                    </asp:TemplateField>
                     <asp:BoundField DataField="Status" HeaderText="Status" Visible="False" />

                      <asp:TemplateField HeaderText="Leave">
                         <ItemTemplate>
                             <asp:Label ID="lblLeavetypeApplied" runat="server" Text='<%#Bind("Leave_Type") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>

                      

<%--                    <asp:BoundField DataField="Leave_Type" HeaderText="Leave Type" />--%>
                    <asp:BoundField DataField="F_Date" HeaderText="From Date"   />

                      <asp:TemplateField HeaderText="Post Lunch">
                         <ItemTemplate>
                             <asp:Label ID="lblPostLunch_GRID" runat="server" Text='<%#Bind("PostLunch") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>
                        
                    <asp:BoundField DataField="T_Date" HeaderText="To Date"   />
                       <asp:TemplateField HeaderText="Pre Lunch">
                         <ItemTemplate>
                             <asp:Label ID="lblPreLunch_GRID" runat="server" Text='<%#Bind("PreLunch") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Half_Day_type_Desc" HeaderText="Day Mode" />
                     <asp:BoundField DataField="No_Of_Days_Leave_Period" HeaderText="Days" />
                                         <asp:BoundField DataField="Arrangement" HeaderText="Arrangement" />

                      <asp:TemplateField HeaderText="Reason" ItemStyle-HorizontalAlign="Left">
                          <ItemTemplate>
                                <div style="width: 200px;">
                                       <%# Eval("Reason") %>
                                </div>
                          </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                       </asp:TemplateField>



                    <asp:BoundField DataField="Create_Date" HeaderText="Date" Visible="False" />
                   
                    <asp:BoundField DataField="UserID" HeaderText="User ID" Visible="false"/>
                           <asp:TemplateField HeaderText="Attachment" ItemStyle-HorizontalAlign="Left">
                          <ItemTemplate>
                              <asp:Label ID="lblleaveAttachmentFilename" runat="server" Text='<%#Bind("AttachmentFilename") %>' Visible="false"></asp:Label>
                              <asp:LinkButton ID="lnkDownloadgrid" runat="server" Text="Download" OnClick="DownloadFile"
                    CommandArgument='<%# Eval("AutoNo") %>'></asp:LinkButton>


                           
                              
                               </div>
                          </ItemTemplate>
                          <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Left" />
                      </asp:TemplateField>

                </Columns>
                <EditRowStyle BackColor="#7C6F57" />
                <EmptyDataTemplate>
                    There is no leave detail
                </EmptyDataTemplate>
                <FooterStyle BackColor="#ed7600" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="White" 
                      HorizontalAlign="Left" CssClass="cssGridheaderfont" Font-Names="Open Sans" />
                <PagerStyle BackColor="#ed7600" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#E3EAEB" CssClass="cssGridheaderfont" Font-Names="Open Sans" Font-Size="10px"/>
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                <SortedAscendingHeaderStyle BackColor="#246B61" />
                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                <SortedDescendingHeaderStyle BackColor="#15524A" />
            </asp:GridView>
           <%-- </div>--%>
             </td></tr>

     <tr> <td style="height:13px"> </td></tr>

          <tr> <td> 
          
          <table cellpadding="0px" cellspacing="0px"> <tr> <td>  </td></tr></table>
          </td></tr>
   
    </table>
     </td> <td style="width:10px"> </td> </tr></table>

    </td></tr></table>
     </asp:Panel>



      <asp:Panel ID="pnlViewleaveDetail" runat="server" CssClass="leftBackground" 
         Visible="false">

    <table cellpadding="0px" cellspacing="0px" style="width:100%"> <tr> <td style="width:10px"> </td> <td>
    
    <table cellpadding="0px" cellspacing="0px">  <tr> <td > 
    
   
    <br />
    <asp:Label ID="Label3" runat="server" 
            Text="View Leave Status" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
           
  </td></tr>
    <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>
    <tr> <td style="height:13px"> </td></tr>
  
  <tr> <td>
  <table cellpadding="0px" cellspacing="0px" style="width:866px"> <tr> <td> <label>From Date  </label> </td> <td style="width:10px"> </td> <td> 
      <asp:TextBox ID="txtFromDateView" runat="server" Width="100px"></asp:TextBox>
      
      <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFromDateView" Format="dd MMM yyyy">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" WatermarkText="dd MMM yyyy" TargetControlID="txtFromDateView">
      </cc1:TextBoxWatermarkExtender>

      </td> <td style="width:10px"> </td> <td><label>To Date </label> </td> <td style="width:10px"> </td> <td>
          <asp:TextBox ID="txtToDateView" runat="server" Width="100px"></asp:TextBox> 
          
            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtToDateView" Format="dd MMM yyyy">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" WatermarkText="dd MMM yyyy" TargetControlID="txtToDateView">
      </cc1:TextBoxWatermarkExtender>

          </td> <td> 
               <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px"> </td> <td> <label> Select Status</label> </td> <td style="width:10px"> </td> <td> <asp:DropDownList ID="ddstatus" runat="server" Height="29px">
          <asp:ListItem>All</asp:ListItem>
          <asp:ListItem>Pending</asp:ListItem>
          <asp:ListItem>Approved</asp:ListItem>
                   <asp:ListItem>Recommend</asp:ListItem>
          <asp:ListItem>Rejected</asp:ListItem>
          </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td> </tr> </table> </td> <td align="right">
              <asp:Button ID="btnLeaveViewSearch" runat="server" Text="Search" CssClass="btnLogin" OnClick="btnLeaveViewSearch_Click" /> </td></tr></table>
  
   </td></tr>


                  <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>
        <tr> <td>     <%--<div id="grdViewLeaveStatuswrap"> --%>
            
            <asp:GridView ID="grdViewLeaveStatus" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanging="grdLeaveDetail_PageIndexChanging" PageSize="30" Width="866px" OnRowDataBound="grdViewLeaveStatus_RowDataBound" >
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                
                    <%-- <asp:BoundField DataField="To_Date" HeaderText="To Date" />--%><%-- <asp:BoundField DataField="Reason" HeaderText="Reason" />--%>
                
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnleavecancApproved" runat="server" Text="Cancel Leave" CommandArgument='<%#Bind("id") %>' OnCommand="btnleavecancApproved_Command" Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnWorkingdetailsview" runat="server" OnCommand="btnWorkingdetailsview_Command" Text="Working Details" CommandArgument='<%#Bind("AutoNo") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Select" Visible="false">
                        <ItemTemplate>
                            <asp:RadioButton ID="rdSelect" runat="server" GroupName="lca" onclick = "RadioCheck(this);" />
                        </ItemTemplate>
                    </asp:TemplateField>
                   <%-- <asp:BoundField DataField="Status" HeaderText="Status" />--%>
                    <%--<asp:BoundField DataField="Leave_Type" HeaderText="Leave Type" />--%>
                      <asp:TemplateField HeaderText="Leave">
                        <ItemTemplate>
                            <asp:Label ID="lblleavetypeG" runat="server" Text='<%#Bind("Leave_Type") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="F_Date" HeaderText="From Date"  />

                       <asp:TemplateField HeaderText="Post Lunch">
                         <ItemTemplate>
                             <asp:Label ID="lblPostLunch_GRID_View" runat="server" Text='<%#Bind("PostLunch") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="To Date">
                        <ItemTemplate>
                            <asp:Label ID="lblTodate" runat="server" Text='<%#Bind("T_Date") %>'  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
               <%--     <asp:BoundField DataField="Leave_Period" HeaderText="Leave Period" />--%>

                      <asp:TemplateField HeaderText="Pre Lunch">
                         <ItemTemplate>
                             <asp:Label ID="lblPreLunch_GRID_View" runat="server" Text='<%#Bind("PreLunch") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>
                     

                     <%--<asp:BoundField DataField="Leave_Period" HeaderText="Period" />--%>
                    <asp:BoundField DataField="Half_Day_type_Desc" HeaderText="Day Mode" />
                     <asp:BoundField DataField="No_Of_Days_Leave_Period" HeaderText="Days" />
                                         <asp:BoundField DataField="Arrangement" HeaderText="Arrangement" />

<%--                    <asp:BoundField DataField="No_Of_Days_Leave_Period" HeaderText="No Of Leave" />--%>
                    <asp:TemplateField HeaderText="Reason" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <div style="width: 100px; overflow:auto; white-space: nowrap; text-overflow:clip">
                                <%# Eval("Reason") %>
                            </div>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                <%--    <asp:BoundField DataField="Address_Phone_No" HeaderText="Alternate Phone No" />--%>
                    <asp:BoundField DataField="Total_Balance" HeaderText="Total Balance" Visible="False" />
                    <asp:BoundField DataField="Create_Date" HeaderText="Date" Visible="False" />
                    <asp:BoundField DataField="UserID" HeaderText="User ID" Visible="false" />
                      <asp:TemplateField HeaderText="Attachment" ItemStyle-HorizontalAlign="Left">
                          <ItemTemplate>
                              <asp:Label ID="lblleaveAttachmentFilenamView" runat="server" Text='<%#Bind("AttachmentFilename") %>' Visible="false"></asp:Label>
                              <asp:LinkButton ID="lnkDownloadgridView" runat="server" Text="Download" OnClick="DownloadFile"
                    CommandArgument='<%# Eval("AutoNo") %>'></asp:LinkButton>
                           <%--   <asp:Button ID="btnViewAttachment" runat="server" CommandArgument='<%#Bind("AutoNo") %>' OnCommand="btnViewAttachment_Command" Text='<%# Eval("Upload") %>' />--%>
                              </div>
                          </ItemTemplate>
                          <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Left" />
                      </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblrejectedappr" runat="server" Text='<%#Bind("[Rejected Approval]") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Status1" HeaderText="Status" />
                     <asp:BoundField DataField="ApprovedBy" HeaderText="Approved By" />
                   <%-- <asp:BoundField DataField="ApprovalDate" HeaderText="Approval Date" />--%>

                </Columns>
                <EditRowStyle BackColor="#7C6F57" />
                <EmptyDataTemplate>
                    There is no leave detail
                </EmptyDataTemplate>
                <FooterStyle BackColor="#ed7600" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="White" 
                      HorizontalAlign="Left" CssClass="cssGridheaderfont" Font-Size="10px" />
                <PagerStyle BackColor="#ed7600" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#E3EAEB" CssClass="cssGridheaderfont" Font-Size="9px" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                <SortedAscendingHeaderStyle BackColor="#246B61" />
                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                <SortedDescendingHeaderStyle BackColor="#15524A" />
            </asp:GridView>
            <%--</div>--%> </td></tr>
     <tr> <td style="height:13px"> </td></tr>

          <tr> <td> 
          
          <table cellpadding="0px" cellspacing="0px"> <tr> <td>  </td></tr></table>
          </td></tr>
   
    </table>
     </td> <td style="width:10px"> </td> </tr></table>

    
    </asp:Panel>



       
     <asp:Panel ID="pnlMain" runat="server" Visible="false" >
     <table cellpadding="0px" cellspacing="0px"> <tr> <td>
     <table cellpadding="0px" cellspacing="0px"> <tr> <td>
      <p>&nbsp;&nbsp;&nbsp;&nbsp; </p>
         <p>
             <strong>Functions Of Leave System </strong>
         </p>
         <p>
             &nbsp;</p>
         <strong>
     </strong>
      </td></tr>

      <tr> <td class="leftm"> </td></tr>
     <tr> <td>
     
      <br />
         <br />
        
         ..... This is an online system facilitating availability information at the 
         touch of a button.
         <br />
         <br />
         ..... The Leave Management System is a Web based application that can be used 
         over the internet<br /> <strong>&nbsp;</strong><br /> ..... It standardizes a common 
         universal process for entire organization.
         <br />
         <br />
         ..... It uses the standard leave approval process of Initiating Officer and 
         reviewing officer.
         <br />
         <br />
         ..... Generates required Reports.
         <br />
         <br />
         <strong>.</strong>.... Online Leave Approval – Different Stages
         <br />
         <br />
         ..... Intimation of Approved/Rejected Leaves on E-mail
         <br />
         <br />
    <br /> 
      </td></tr>
     <tr> <td class="leftm"> </td></tr>
     </table>

         
    
        
      </td></tr></table>

     </asp:Panel>

 
 </td></tr></table>


   



</fieldset>
    <asp:Button ID="Button1" runat="server" Text="Button" Style="display:none" />
    <cc1:ModalPopupExtender ID="mdworkingdetails" runat="server"  TargetControlID="Button1" PopupControlID="pnlworkingDetails" BackgroundCssClass="modalBackgroundforco"></cc1:ModalPopupExtender>
    <asp:Panel ID="pnlworkingDetails" runat="server" BackColor="White" Style="display:none">
        <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:20px">  </td> <td>

            <table cellpadding="0px" cellspacing="0px"> <tr> <td align="right"> <asp:ImageButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" ImageUrl="~/logo/close.png" Width="25px" Height="25px" /> </td></tr> 
           <tr> <td style="height:13px"> </td></tr>
            <tr> <td class="leftm"> </td></tr>
           <tr> <td style="height:13px"> </td></tr>
           <tr> <td> <asp:Label ID="Label5" runat="server" 
            Text="Working Details" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>   ( Days you worked on week off / holiday) </td></tr>

            <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>
    <tr> <td style="height:13px"> </td></tr>

           <tr> <td> <asp:GridView ID="grdworkingdetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="800px">
               <AlternatingRowStyle BackColor="#CCCCCC"  />
               <Columns>
                   <asp:BoundField DataField="Co_Date" HeaderText="Date"  DataFormatString="{0:yyyy-MM-dd}"/>
                   <asp:BoundField DataField="In time" HeaderText="In Time" />
                   <asp:BoundField DataField="Out Time" HeaderText="Out Time" />
                   <asp:BoundField DataField="Present Hour" HeaderText="Present Hour"  />
                   <asp:BoundField DataField="Expire on" HeaderText="Expire on" DataFormatString="{0:yyyy-MM-dd}" />
                   <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
               </Columns>
               <FooterStyle BackColor="#CCCCCC" />
               <HeaderStyle  BackColor="#ed7600" Font-Bold="True" ForeColor="White" 
                      HorizontalAlign="Left" CssClass="cssGridheaderfont" />
               <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
               <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
               <SortedAscendingCellStyle BackColor="#F1F1F1" />
               <SortedAscendingHeaderStyle BackColor="#808080" />
               <SortedDescendingCellStyle BackColor="#CAC9C9" />
               <SortedDescendingHeaderStyle BackColor="#383838" />
               </asp:GridView> </td></tr>

       </table>


        </td> <td style="width:20px"> </td></tr>


            <tr> <td colspan="3" style="height:20px">  </td></tr>

        </table>


       

    </asp:Panel>



</asp:Content>

