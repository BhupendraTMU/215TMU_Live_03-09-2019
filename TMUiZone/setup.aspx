<%@ Page Title="TMU HRM" Language="C#" MasterPageFile="~/IndexMaster.master" AutoEventWireup="true" CodeFile="setup.aspx.cs" Inherits="createlevel" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link href="css/structure.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
        #grdLeavedetailwrap
{
width:866px;
height:100%;
overflow:scroll;
}

          #grdViewLeaveStatuswrap
{
width:866px;
height:100%;
overflow:scroll;
}

             #grdremSetupswrap
{
width:866px;
height:100%;
overflow:scroll;
}
                     #grdAttenSetupswrap
{
width:866px;
height:100%;
overflow:scroll;
}


                       #grdRembursset
{
width:866px;
height:100%;
overflow:scroll;
}
    </style>


     <script type="text/javascript">
         function Confirm_Unblock() {
             var confirm_value = document.createElement("INPUT");
             confirm_value.type = "hidden";
             confirm_value.name = "confirm_value";
             if (confirm("Do you want to Unblock ? ")) {
                 confirm_value.value = "Yes";
             } else {
                 confirm_value.value = "No";
             }
             document.forms[0].appendChild(confirm_value);
         }

         function Confirm_Block() {
             var confirm_value = document.createElement("INPUT");
             confirm_value.type = "hidden";
             confirm_value.name = "confirm_value";
             if (confirm("Aru you sure ?")) {
                 confirm_value.value = "Yes";
             } else {
                 confirm_value.value = "No";
             }
             document.forms[0].appendChild(confirm_value);
         }

</script>
       <script type="text/JavaScript">
           function validateHhMm(inputField) {
               var isValid = /^([0-1]?[0-9]|2[0-3]):([0-5][0-9])(:[0-5][0-9])?$/.test(inputField.value);

               if (isValid) {
                   //inputField.style.backgroundColor = '#bfa';
               } else {
                   //inputField.style.backgroundColor = '#fba';

                   //alert("Accept only Time format .. (HH:MM)!  ");

                   alert(" Invalid time .. Please enter valid time in 24 hrs formate ");
                   inputField.value = "";
               }

               return isValid;


           }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
  <asp:Label ID="Label1" runat="server" Text="Setup" 
            Font-Size="15pt" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" 
            ForeColor="#093A62"></asp:Label>

 </fieldset>

  <fieldset class="boxBody"> 

 <table cellpadding="0px" cellspacing="0px">  <tr>  <td  style="width:10px">  </td><td  style="width:210px; height:430px; " valign="top"> 

<table cellpadding="0px" cellspacing="0px" class="leftbg1" style="width:210px; height:430px">  <tr> <td style="width:10px"> </td> <td >


<table cellpadding="0px" cellspacing="0px" >
 <tr> <td style="height:10px"> </td></tr>
 <tr> <td class="leftmMenu">  <img src="logo/Star.png" /> 
    <asp:LinkButton ID="lnkCreateLevel" runat="server" 
         onclick="lnkCreateLevel_Click">Approver</asp:LinkButton></td></tr>
    <tr> <td style="height:10px"> </td></tr>
     <tr> <td class="leftmMenu">  <img src="logo/Star.png" /> 
    <asp:LinkButton ID="lnkSetattendenceExpiry" runat="server" OnClick="lnkSetattendenceExpiry_Click" 
        >Attendance </asp:LinkButton></td></tr>
     <tr> <td style="height:10px"> </td></tr>
     <tr> <td class="leftmMenu">  <img src="logo/Star.png" /> 
    <asp:LinkButton ID="lnkleaveSetup" runat="server" OnClick="lnkleaveSetup_Click"  
        >Leave</asp:LinkButton></td></tr>

     <tr> <td style="height:10px"> </td></tr>
     <tr> <td class="leftmMenu">  <img src="logo/Star.png" /> 
    <asp:LinkButton ID="lnkMailSetup" runat="server" OnClick="lnkMailSetup_Click"  
        >E-Mail </asp:LinkButton></td></tr>
     <tr> <td style="height:10px"> </td></tr>
        <tr> <td class="leftmMenu"> <%--  <img src="logo/Star.png"  />--%>
    <asp:LinkButton ID="lnkReimbursementApproval" runat="server"  OnClick="lnkReimbursementApproval_Click" Visible="False" >Approval Type</asp:LinkButton></td></tr>
     <tr> <td style="height:10px"> </td></tr>
    <tr> <td class="leftmMenu"> <%-- <img src="logo/Star.png" /> --%>
    <asp:LinkButton ID="lnkReimTypeMaster" runat="server" OnClick="lnkReimTypeMaster_Click" Visible="False"  
        >Reimbursement Type Master </asp:LinkButton></td></tr>
     <tr> <td style="height:10px"> </td></tr>
     <tr> <td class="leftmMenu">   <img src="logo/Star.png"  style="visibility:hidden"/>
    <asp:LinkButton ID="lnkreimbursment" runat="server" onclick="lnkreimbursment_Click" Visible="False">Expense</asp:LinkButton></td></tr>
     <tr> <td style="height:10px"> </td></tr>
        <tr> <td class="leftmMenu">   <img src="logo/Star.png"  style="visibility:hidden" />
    <asp:LinkButton ID="lnkrembursmenttype" runat="server" OnClick="lnkrembursmenttype_Click" Visible="False" >Reimbursement Type </asp:LinkButton></td></tr>
     <tr> <td style="height:10px"> </td></tr>

   



    </table>
 </td> <td style="width:10px"> </td></tr></table>



    
</td>  <td style="width:20px">  </td> <td valign="top"> 
  






    <asp:Panel ID="pnlCreate" runat="server" CssClass="leftBackground" >
   


   <table cellpadding="0px" cellspacing="0px"> <tr> <td> 
  <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px"> </td> <td>
  <br />
    <asp:Label ID="Label3" runat="server" 
            Text="Approver" Font-Size="15pt" ForeColor="#093A62" 
          Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
    
   </td></tr></table>
          
   </td></tr>
   
   
 <tr> <td style="height:13px"  > </td></tr>

    <tr> <td class="leftm"  > </td></tr>

   <tr> <td>
   
   <table cellpadding="0px" cellspacing="0px"> 
   
            
   <tr> <td style="width:10px"> </td> <td> 
   
   <table cellpadding="0px" cellspacing="0px"> 
 
 <tr> <td colspan="11" style="height:13px">
 
  </td></tr>

   



       
 <tr> <td> Approval For </td> <td style="width:20px"> </td> <td colspan="6">  
 <asp:DropDownList ID="ddType" runat="server" OnSelectedIndexChanged="ddType_SelectedIndexChanged" AutoPostBack="True" Height="29px">
             <asp:ListItem Value="For Profile">Profile</asp:ListItem>
             <asp:ListItem Value="For Leave">Leave</asp:ListItem>
             <asp:ListItem Value="For Attendance">Attendance</asp:ListItem>
            <%-- <asp:ListItem Value="For Reimbursment">Reimbursment</asp:ListItem>--%>
         </asp:DropDownList>    
 </td> <%--<td style="width:20px"> </td> <td>  
     </td> <td style="width:20px"> </td>
     <td>  
    </td><td style="width:20px"> </td> <td> 
          </td>--%>
     </tr>
         <tr> <td colspan="11" style="height:20px"> </td></tr>

 <tr> <td> Approving Authority</td> <td style="width:20px"> </td> <td>  
     <asp:CheckBox ID="chkBlank" runat="server" Text="Blank" AutoPostBack="True" OnCheckedChanged="chkBlank_CheckedChanged" />
     <asp:CheckBox ID="CHKHOD" runat="server" AutoPostBack="True" OnCheckedChanged="CHKHOD_CheckedChanged" Text="Reporting Manager" />
     </td> <td style="width:20px"> </td> <td>  
     <asp:CheckBox ID="chkHR" runat="server" Text="HR" AutoPostBack="True" OnCheckedChanged="chkHR_CheckedChanged" /></td> <td style="width:20px"> </td>
     <td>  
         &nbsp;</td><td style="width:20px"> </td> <td> 
          </td>
     </tr>
       
        <tr> <td colspan="11" style="height:20px"> </td></tr>
         
        <tr> <td> Email </td> <td style="width:20px"> </td> <td>  
            <asp:CheckBox ID="chkNONEemail" runat="server" AutoPostBack="True"  Text="Blank" Enabled="False" OnCheckedChanged="chkNONEemail_CheckedChanged" Visible="False" />
             <asp:CheckBox ID="chkHREmail" runat="server" AutoPostBack="True" Text="HR" Enabled="False" OnCheckedChanged="chkHREmail_CheckedChanged" />
            </td> <td style="width:20px"> </td> <td>  
           <asp:CheckBox ID="chkHODEmail" runat="server" AutoPostBack="True" Text="Reporting Manager" Enabled="False" OnCheckedChanged="chkHODEmail_CheckedChanged" />
            </td> <td style="width:20px"> </td>
     <td>  
         
            </td><td style="width:20px"> </td> <td> 
          </td>
     </tr>
       <tr> <td colspan="11" style="height:20px"> </td></tr>
 <tr> <td> <asp:Label ID="Label13" runat="server" Text="First Approval By" Visible="false"></asp:Label>  </td> <td style="width:20px"> </td> <td>  
     <asp:DropDownList ID="ddPriority" runat="server" Visible="False" Height="29px">
         <asp:ListItem Selected="True">-----</asp:ListItem>
         <asp:ListItem>HOD</asp:ListItem>
        <%-- <asp:ListItem>HR</asp:ListItem>--%>
     </asp:DropDownList></td> <td style="width:20px"> </td> <td>  
     </td> <td style="width:20px"> </td>
     <td>  
         
     </td><td style="width:20px"> </td> <td> 
         <asp:Button ID="btnsetup" runat="server" Text="Save" CssClass="btnLogin" OnClick="btnsetup_Click" /> </td>
     </tr>
     <tr> <td colspan="11" style="height:20px"> </td></tr>
     </table>
    </td> <td style="width:10px"> </td></tr></table>
    </td></tr>
    <tr> <td class="leftm"  > </td></tr>

       <tr> <td >

           <asp:GridView ID="grdPriority" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" OnRowDataBound="grdPriority_RowDataBound">
               <AlternatingRowStyle BackColor="#F7F7F7" />
               <Columns>
                   <asp:BoundField DataField="HOD" HeaderText="Reporting Manager" />
                   <asp:BoundField DataField="HR" HeaderText="HR"  Visible="false"/>
                   <asp:BoundField DataField="Blank" HeaderText="Blank" />
                   <asp:BoundField DataField="PriorityHOD" HeaderText="First Approval By - HOD" Visible="false" />
                   <asp:BoundField DataField="PriorityHR" HeaderText="First Approval By - HR " Visible="false" />
                   <asp:BoundField DataField="Type" HeaderText="Approval For" />
                   <asp:BoundField DataField="EmailBlank" HeaderText="Email - Blank" Visible="false"/>
                   <asp:BoundField DataField="EmailHR" HeaderText="Email - HR" />
                   <asp:BoundField DataField="EmailHOD" HeaderText="Email - Reporting Manager" />
               </Columns>
               <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
               <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
               <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
               <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont"   />
               <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
               <SortedAscendingCellStyle BackColor="#F4F4FD" />
               <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
               <SortedDescendingCellStyle BackColor="#D8D8F0" />
               <SortedDescendingHeaderStyle BackColor="#3E3277" />
           </asp:GridView>

            </td></tr>
    <tr> <td style="height:13px"  > </td></tr>

    


   </table>

   


 

 

    </asp:Panel>





    
  


         


      <asp:Panel ID="pnlForAttendencesetup" runat="server" CssClass="leftBackground"  Visible="false">
   


   <table cellpadding="0px" cellspacing="0px"> <tr> <td> 
  <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px"> </td> <td>
  <br />
    <asp:Label ID="Label4" runat="server" 
            Text="Attendance" Font-Size="15pt" ForeColor="#093A62" 
          Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
    
   </td></tr></table>
          
   </td></tr>
   
   
 <tr> <td style="height:13px"  > </td></tr>

    <tr> <td class="leftm"  > </td></tr>

   <tr> <td>
   
   <table cellpadding="0px" cellspacing="0px"> 
   
            
   <tr> <td style="width:10px"> </td> <td> 
   
   <table cellpadding="0px" cellspacing="0px" style="width:100%"> 
 
 <tr> <td colspan="11" style="height:13px">
 
  </td></tr>

   

 <tr> <td> Can mark attendance upto past  </td> <td style="width:20px"> </td> <td colspan="5">  
  
     <asp:TextBox ID="txtNoOdays" runat="server" Width="100px"></asp:TextBox> &nbsp;days.
     <br /> 
     <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtNoOdays" ErrorMessage="Please fill attendance mark upto past" ValidationGroup="attsex" ForeColor="Red"></asp:RequiredFieldValidator>
 </td> 
     </tr>
      
         <tr> <td colspan="11" style="height:7px"> </td></tr>

          <tr> <td>
              <asp:Label ID="Label245" runat="server" Text="In Time and Out time required" Visible="False"></asp:Label>  </td> <td style="width:20px"> </td> <td>  
  
     <asp:DropDownList ID="ddIntimeOuttimerequired" runat="server" Width="105px" AutoPostBack="True" OnSelectedIndexChanged="ddIntimeOuttimerequired_SelectedIndexChanged" Height="29px" Visible="False">
         <asp:ListItem Selected="True">Yes</asp:ListItem>
     </asp:DropDownList>
 </td> <td style="width:20px"> </td>
     <td>  
         <asp:Label ID="Label2" runat="server" Text="Allow time change" Visible="False"></asp:Label>
     </td><td style="width:20px"> 
         
     </td> <td> 
       
                  <asp:DropDownList ID="ddOption" runat="server" Height="29px" Width="105px" Visible="False">
                      <asp:ListItem Selected="True">Enable</asp:ListItem>
                  </asp:DropDownList>
       
          </td>
     </tr>
 

       <tr> <td colspan="11" style="height:7px"> </td></tr>

       <tr> <td>   <asp:Label ID="Label8" runat="server" Text="Allow Changing Card Attendance"></asp:Label></td> <td style="width:20px"> </td> <td>  
  
           <asp:DropDownList ID="ddCardAttendanceChanging" runat="server" Height="29px" Width="105px">
               <asp:ListItem Value="1">Yes</asp:ListItem>
               <asp:ListItem Value="2">No</asp:ListItem>
           </asp:DropDownList>
           </td> <td style="width:20px"> </td>
     <td>  
       
     </td><td style="width:20px"> 
         
     </td> <td> 
               &nbsp;</td>
     </tr>
     

       <tr> <td colspan="11" style="height:7px"> </td></tr>
       <tr> <td>Default In Time  </td> <td style="width:20px"> </td> <td>  
  
     <asp:TextBox ID="txtFromTime" runat="server" Width="100px" onchange="validateHhMm(this);"></asp:TextBox>
           <br />
 </td> <td style="width:20px"> 
             
           </td>
     <td>  
       Default Out Time
     </td><td style="width:20px"> 
         
     </td> <td> <asp:TextBox ID="txtTotime" runat="server" Width="100px" onchange="validateHhMm(this);"></asp:TextBox>
                    <br />
          </td>
     </tr>




        <tr> <td colspan="11" style="height:7px"> </td></tr>

       <tr> <td colspan="11">

           &nbsp;</td></tr>


        <tr> <td colspan="11" style="height:7px"> </td></tr>





           <tr> <td> Applicable for  </td> <td style="width:20px"></td> <td colspan="9">  
  
              <table cellpadding="0px" cellspacing="0px" style="width:100px">  <tr> <td> <asp:RadioButton ID="rdAll" runat="server" Text="All" AutoPostBack="True" GroupName="attdur" OnCheckedChanged="rdAll_CheckedChanged" Width="120px" /> 
      </td> <td style="width:10px">  </td> <td> <asp:RadioButton ID="rdEmployeeidwise" runat="server" Text="Employee Wise" AutoPostBack="True" GroupName="attdur" OnCheckedChanged="rdEmployeeidwise_CheckedChanged" Width="120px" />
 </td>  <td style="width:10px">  </td> <td> <asp:DropDownList ID="txtempidduration" runat="server"  AppendDataBoundItems="true" Height="29px" >

             <asp:ListItem Text="" Value="" />
         </asp:DropDownList> </td><td style="width:10px">  </td> <td>  <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" ValidationGroup="attsex" /> </td> </tr></table>

              
              
                                                                         </td>  <%--<td>  
  
                   </td> <td style="width:20px"> </td>--%>
     
     </tr>

         <tr> <td colspan="11" style="height:7px"> </td></tr>
  


      
 <tr> <td>  </td> <td style="width:20px"> </td> <td>  
  
    
 </td> <td style="width:20px"> </td>
     <td>  
        
     </td><td style="width:20px"> 
         
     </td> <td align="right"> 
       
          <asp:Button ID="btnattendenceno" runat="server" CssClass="btnLogin" OnClick="btnattendenceno_Click" Text="Save" ValidationGroup="attsex" />
       
          </td>
     </tr>

 

     </table>
    </td> <td style="width:10px"> </td></tr></table>
    </td></tr>
    <tr> <td class="leftm"  > </td></tr>

       <tr> <td>   <asp:Label ID="Label10" runat="server" Text="Applicable for all" Font-Bold="True" Font-Size="10pt" ForeColor="#CC0000"></asp:Label> </td></tr>

       <tr> <td >

          <%-- <div id="grdAttenSetupswrap"> --%>
           <asp:GridView ID="grdSetATDExpiry" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="866px">
           <AlternatingRowStyle BackColor="White" />
           <Columns>
               <asp:BoundField DataField="No_of_Days" HeaderText="Can mark attendance upto past" />
               <asp:BoundField DataField="From_Time" HeaderText="Default In Time" />
               <asp:BoundField DataField="To_Time" HeaderText="Default Out Time" />
               <asp:BoundField DataField="Option_fromTime_toime_All" HeaderText="Allow time change" Visible="False" />

               <asp:BoundField DataField="In_time_Out_TimeRquired" HeaderText="In Time and Out time required " Visible="False" />

               <asp:BoundField DataField="Card_Attendance_changing" HeaderText="Allow Changing Card Attendance" />

           </Columns>
           <EditRowStyle BackColor="#2461BF" />
           <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
           <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
           <RowStyle BackColor="#EFF3FB" CssClass="cssGridheaderfont" />
           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
           <SortedAscendingCellStyle BackColor="#F5F7FB" />
           <SortedAscendingHeaderStyle BackColor="#6D95E1" />
           <SortedDescendingCellStyle BackColor="#E9EBEF" />
           <SortedDescendingHeaderStyle BackColor="#4870BE" />
           </asp:GridView>

             <%--  </div>--%>
         </td></tr>



    <tr> <td style="height:13px"  > </td></tr>

    
       <tr> <td style="height:13px"  >


           <asp:Label ID="lblsetupAtt" runat="server" Text="Applicable for employee" Font-Bold="True" Font-Size="10pt" ForeColor="#CC0000"></asp:Label>

            </td></tr>

       <tr> <td> 

              <asp:GridView ID="grdAttendINDExpiry" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="866px" AllowPaging="True" OnPageIndexChanging="grdAttendINDExpiry_PageIndexChanging" PageSize="7">
           <AlternatingRowStyle BackColor="White" />
           <Columns>
               <asp:BoundField DataField="EmployeeID" HeaderText="Employee ID" />
               <asp:BoundField DataField="Duration" HeaderText="Can mark attendance upto past" />
               <asp:BoundField DataField="From_Time" HeaderText="Default In Time" />
               <asp:BoundField DataField="To_Time" HeaderText="Default Out Time" />
               <asp:BoundField DataField="Option_fromTime_toime_Individual" HeaderText="Allow time change" Visible="False" />
               <asp:BoundField DataField="In_time_Out_TimeRquired" HeaderText="In Time and Out time required " Visible="False" />
               <asp:BoundField DataField="Card_Attendance_changing" HeaderText="Allow card changing Attendance" />
               <asp:TemplateField>
                   <ItemTemplate>
                       <asp:Button ID="btnDel" runat="server" CommandArgument='<%#Bind("id") %>' OnCommand="btnDel_Command" Text="Delete" />
                   </ItemTemplate>
               </asp:TemplateField>
           </Columns>
           <EditRowStyle BackColor="#2461BF" />
           <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" />
           <PagerStyle BackColor="#C5122F" ForeColor="White" HorizontalAlign="Center" />
           <RowStyle BackColor="#EFF3FB" CssClass="cssGridheaderfont" />
           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
           <SortedAscendingCellStyle BackColor="#F5F7FB" />
           <SortedAscendingHeaderStyle BackColor="#6D95E1" />
           <SortedDescendingCellStyle BackColor="#E9EBEF" />
           <SortedDescendingHeaderStyle BackColor="#4870BE" />
           </asp:GridView>

            </td></tr>


   </table>

   


 

 

    </asp:Panel>


     <asp:Panel ID="pnlLeavesetup" runat="server" CssClass="leftBackground"  Visible="false">
   


   <table cellpadding="0px" cellspacing="0px"> <tr> <td> 
  <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px"> </td> <td>
  <br />
    <asp:Label ID="Label5" runat="server" 
            Text="Leave" Font-Size="15pt" ForeColor="#093A62" 
          Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
    
   </td></tr></table>
          
   </td></tr>
   
   
 <tr> <td style="height:13px"  > </td></tr>

    <tr> <td class="leftm"  > </td></tr>

   <tr> <td>
   
   <table cellpadding="0px" cellspacing="0px"> 
   
            
   <tr> <td style="width:10px"> </td> <td> 
   
   <table cellpadding="0px" cellspacing="0px"> 
 

        <tr> <td colspan="11" style="height:10px">  </td></tr>


        
       <tr> <td colspan="11">

           <table cellpadding="0px" cellspacing="0px"> <tr> <td>  Can apply for Compensatory Leave upto </td> <td style="width:10px">  </td> <td>
               <asp:TextBox ID="txtLeaveupto" runat="server" Width="100px"></asp:TextBox> </td> <td style="width:10px">  </td> <td> Days </td> </tr> 


               <tr> <td colspan="5" style="height:10px"> </td></tr>


               <tr> <td colspan="5">

                   <table cellpadding="0px" cellspacing="0px" style="width:100%"> <tr> <td> Applicable For </td> <td style="width:10px">  </td>  <td>  <asp:RadioButton ID="rdAllCOupto" runat="server" Text="All" Checked="True" GroupName="couptol" AutoPostBack="True" OnCheckedChanged="rdAllCOupto_CheckedChanged" /></td> <td style="width:5px"> </td><td>  <asp:RadioButton ID="rdINDCoupto" runat="server" Text="Individual" GroupName="couptol" AutoPostBack="True" OnCheckedChanged="rdINDCoupto_CheckedChanged" /> </td> <td style="width:10px"> </td> <td>Employee ID  </td> <td style="width:10px">  </td> <td> <asp:DropDownList ID="txtEmployeeidcoupto" runat="server" Width="300px"  AppendDataBoundItems="true"  Height="29px" Enabled="False">

                       <asp:ListItem Text="" Value="" />

                                                                                                               </asp:DropDownList> <%--<asp:TextBox ID="txtEmployeeidcoupto" runat="server" Enabled="False"></asp:TextBox>--%></td> <td style="width:10px">  </td> <td> <asp:Button ID="btnSaveleaveupto" runat="server" Text="Save" OnClick="btnSaveleaveupto_Click" /></td></tr></table>

                    </td></tr>

                 <tr> <td colspan="5" style="height:10px"> </td></tr>

                 <tr> <td colspan="5" >
                    
                     <asp:GridView ID="grdLeaveuptodetails" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="800px">
         <AlternatingRowStyle BackColor="White" />
         <Columns>
             <asp:BoundField DataField="Comp Leave Upto" HeaderText="Compensatory Leave upto for All" />
             <asp:BoundField DataField="Individual Duration" HeaderText="Compensatory Leave upto for Employee" />
             <asp:BoundField DataField="Employee ID" HeaderText="Employee" />
         </Columns>
         <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
         <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
         <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
         <RowStyle BackColor="#FFFBD6" Font-Size="13.2px" ForeColor="#333333" HorizontalAlign="Left" />
         <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
         <SortedAscendingCellStyle BackColor="#FDF5AC" />
         <SortedAscendingHeaderStyle BackColor="#4D0000" />
         <SortedDescendingCellStyle BackColor="#FCF6C0" />
         <SortedDescendingHeaderStyle BackColor="#820000" />
     </asp:GridView>

                      </td></tr>


           </table>

            </td></tr>

 
 <tr> <td colspan="11" style="height:20px">  </td></tr>


 <tr> <td colspan="11">
 
     <asp:GridView ID="grdLeaveSetup" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCancelingEdit="grdLeaveSetup_RowCancelingEdit" OnRowEditing="grdLeaveSetup_RowEditing" OnRowUpdating="grdLeaveSetup_RowUpdating" Width="500px">
         <AlternatingRowStyle BackColor="White" />
         <Columns>
             <asp:TemplateField HeaderText="Leave Type">
                 <ItemTemplate>
                     <asp:Label ID="lblLeaveTypedd" runat="server" Text='<%# Eval("Leave Type") %>'></asp:Label>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="Club Holidays">
                 <EditItemTemplate>
                     <asp:DropDownList ID="ddClubHolidays" runat="server">
                         <asp:ListItem>Yes</asp:ListItem>
                         <asp:ListItem>No</asp:ListItem>
                     </asp:DropDownList>
                 </EditItemTemplate>
                 <ItemTemplate>
                     <asp:Label ID="Label15" runat="server" Text='<%# Eval("Club Holiday") %>'></asp:Label>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:CommandField ShowEditButton="True" />
         </Columns>
         <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
         <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
         <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
         <RowStyle BackColor="#FFFBD6" Font-Size="13.2px" ForeColor="#333333" HorizontalAlign="Left" />
         <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
         <SortedAscendingCellStyle BackColor="#FDF5AC" />
         <SortedAscendingHeaderStyle BackColor="#4D0000" />
         <SortedDescendingCellStyle BackColor="#FCF6C0" />
         <SortedDescendingHeaderStyle BackColor="#820000" />
     </asp:GridView>
 
  </td></tr>

   

      

 <tr style="visibility:hidden"> <td> <%--Leave will be counted with holiday if leave will be continuously--%>

     <%--Whether holiday to be counted if it falls between continuous leaves--%>

      </td> <td style="width:20px"> </td> <td>  
  
     <asp:DropDownList ID="ddHolidayCount" runat="server" Visible="False" Height="29px">
         <asp:ListItem Value="-------">-------</asp:ListItem>
         <asp:ListItem Value="0" Selected="True">False</asp:ListItem>
         <asp:ListItem Value="1">True</asp:ListItem>
     </asp:DropDownList>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddHolidayCount" ErrorMessage="****" ForeColor="#CC0000" InitialValue="-------" SetFocusOnError="True" ValidationGroup="lsetup"></asp:RequiredFieldValidator>
 </td> <td style="width:20px"> </td>
     <td>  
     </td><td style="width:20px"> 
         
     </td> <td> 
          </td>
     </tr>
      
     <tr> <td colspan="11" style="height:20px"> </td></tr>

       <tr style="visibility:hidden"> <td>   <%--Leave will be counted with Off Days if leave will be continuously--%>
         Whether off days to be counted if it falls between continuous leaves

            </td> <td style="width:20px"> </td> <td>  
  
          <asp:DropDownList ID="ddOffDaycount" runat="server" Height="29px">
              <asp:ListItem>-------</asp:ListItem>
              <asp:ListItem Value="0">False</asp:ListItem>
              <asp:ListItem Value="1">True</asp:ListItem>
           </asp:DropDownList> 
           <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddOffDaycount" ErrorMessage="****" ForeColor="#CC0000" InitialValue="-------" SetFocusOnError="True" ValidationGroup="lsetup"></asp:RequiredFieldValidator>
 </td> <td style="width:20px"> </td>
     <td>  
     </td><td style="width:20px"> 
               &nbsp;</td> <td> 
          </td>
     </tr>

       



         <tr> <td colspan="11" style="height:20px"> </td></tr>
        <tr> <td>   </td> <td style="width:20px"> </td> <td align="right">  
  
            &nbsp;</td> <td style="width:20px"> </td>
     <td>  
     </td><td style="width:20px"> 
               &nbsp;</td> <td> 
          </td>
     </tr>

        <tr> <td colspan="11" style="height:13px">
 
  </td></tr>

   

 <%--<tr> <td>  Whether holiday to be counted if it falls between continuous leaves
 </td> <td style="width:20px"> </td> <td>  
  
     <asp:DropDownList ID="ddholidayView" runat="server" Enabled="False">
         <asp:ListItem>------</asp:ListItem>
         <asp:ListItem Value="0">False</asp:ListItem>
         <asp:ListItem Value="1">True</asp:ListItem>
     </asp:DropDownList>
 </td> <td style="width:20px"> </td>
     <td>  
     </td><td style="width:20px"> 
         
     </td> <td> 
          </td>
     </tr>--%>
      
    <%-- <tr> <td colspan="11" style="height:20px"> </td></tr>

       <tr> <td>
            Whether off days to be counted if it falls between continuous leaves


            </td> <td style="width:20px"> </td> <td>  
  
          <asp:DropDownList ID="ddOffDayView" runat="server" Enabled="False">
              <asp:ListItem>-------</asp:ListItem>
              <asp:ListItem Value="0">False</asp:ListItem>
              <asp:ListItem Value="1">True</asp:ListItem>
           </asp:DropDownList> 
 </td> <td style="width:20px"> </td>
     <td>  
     </td><td style="width:20px"> 
               &nbsp;</td> <td> 
          </td>
     </tr>--%>


     </table>
    </td> <td style="width:10px"> </td></tr></table>
    </td></tr>
    <tr> <td class="leftm"  > </td></tr>

       <tr> <td >

           &nbsp;</td></tr>
    <tr> <td style="height:13px"  > </td></tr>

    


   </table>

   


 

 

    </asp:Panel>

    <asp:Panel ID="pnlMailSetup" runat="server" CssClass="leftBackground"  Visible="false">
   


   <table cellpadding="0px" cellspacing="0px"> <tr> <td> 
  <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px"> </td> <td>
  <br />
    <asp:Label ID="Label6" runat="server" 
            Text="E-Mail" Font-Size="15pt" ForeColor="#093A62" 
          Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
    
   </td></tr></table>
          
   </td></tr>
   
   
 <tr> <td style="height:13px"  > </td></tr>

    <tr> <td class="leftm"  > </td></tr>

   <tr> <td>
   
   <table cellpadding="0px" cellspacing="0px"> 
   
            
   <tr> <td style="width:10px"> </td> <td> 
   
   <table cellpadding="0px" cellspacing="0px"> 
 
 <tr> <td colspan="11" style="height:10px">
 
  </td></tr>


       <tr> <td> SMTP For  </td>  <td style="width:20px"> </td> <td colspan="7"> 
           <asp:DropDownList ID="ddsmtpfor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddsmtpfor_SelectedIndexChanged" Height="29px">
               <asp:ListItem>Attendance</asp:ListItem>
               <asp:ListItem>Leave</asp:ListItem>
               <asp:ListItem>Profile</asp:ListItem>
               <asp:ListItem>Reimbursement</asp:ListItem>
           </asp:DropDownList>
     </td> </tr>


   <tr> <td colspan="11" style="height:5px">
 
  </td></tr>

 <tr> <td> Default email-id for mail setup</td> <td style="width:20px"> </td> <td > 
     <asp:TextBox ID="txtFromEMailid" runat="server" Width="200px"></asp:TextBox>
      
     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFromEMailid" ErrorMessage="***" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="setupM"></asp:RegularExpressionValidator>
      
     </td> 

     <td style="width:5px">  </td>  <td> SMTP for mail </td> <td style="width:5px"> </td> <td colspan="4"> <asp:TextBox ID="txtSMTP" runat="server" Width="100px"></asp:TextBox> </td>

     </tr>
       <%-- <tr> <td colspan="11" style="height:5px"> </td></tr>
         
       <tr> <td>  </td> <td style="width:20px"> </td> <td colspan="7"> 
    
      
     </td> 
     </tr>--%>

     <tr> <td colspan="11" style="height:5px"> </td></tr>

          <tr> <td>Password  </td> <td style="width:20px"> </td> <td > 
     <asp:TextBox ID="txtPassword" runat="server" Width="150px"></asp:TextBox>
      
     </td>  

              <td style="width:5px"> </td> <td> PORT No</td> <td style="width:5px"> </td> <td colspan="4">  <asp:TextBox ID="txtPortNo" runat="server" Width="100px"></asp:TextBox> </td>

     </tr>

      <%--  <tr> <td colspan="11" style="height:10px"> </td></tr>

          <tr> <td>   </td> <td style="width:20px"> </td> <td colspan="7"> 
    
      
     </td> 
     </tr>--%>

        <tr> <td colspan="11" style="height:5px"> </td></tr>

          <tr> <td> CC-mail (with multiple
              <br />
              email ID comma(,) seperator)</td> <td style="width:20px"> </td> <td colspan="7"> 
     <asp:TextBox ID="txtCCMail" runat="server" Width="300px" Height="25px" TextMode="MultiLine"></asp:TextBox>
      
     </td> 
     </tr>

        <tr> <td colspan="11" style="height:5px"> </td></tr>

          <tr> <td> Mail configuration through portal </td> <td style="width:20px"> </td> <td colspan="7"> 
              <asp:DropDownList ID="ddPortalMail" runat="server" Height="29px">
                  <asp:ListItem Selected="True" Value="0">False</asp:ListItem>
                  <asp:ListItem Value="1">True</asp:ListItem>
              </asp:DropDownList>
              
     </td> 
     </tr>

          <tr> <td colspan="11" style="height:5px"> </td></tr>


    <tr> <td colspan="11">

        <table cellpadding="0px" cellspacing="0px" style="width:100%"> <tr> <td align="center"><asp:Label ID="Label7" runat="server" 
            Text="Select option to configure mails " Font-Size="12pt" ForeColor="#093A62" 
          Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label> </td></tr>
               <tr> <td style="height:10px"> </td></tr>
             <tr> <td class="leftm"  > </td></tr>
            <tr> <td style="height:10px"> </td></tr>
            <tr> <td>   <table cellpadding="0px" cellspacing="0px" style="width:100%"> 
                 <tr> <td> <asp:CheckBox ID="chkProfilechange" runat="server" Text="Profile Change"/> </td> <td>   <asp:CheckBox ID="chkProfileApproval" runat="server" Text="Profile Approval" /></td> <td>
                <asp:CheckBox ID="chkAttendenceMark" runat="server" Text="Mark Attendance " />  </td>  <td>
                    <asp:CheckBox ID="chkAttendenceApproval" runat="server" Text="Attendance Approval" />  </td></tr>


                <tr> <td colspan="4" style="height:5px">  </td>  </tr>
                <tr> <td>

                        <asp:CheckBox ID="chkLeaveApply" runat="server" Text="Leave Apply" />
                                                                                                        </td> <td>
                                                                                                            <asp:CheckBox ID="chkLeaveApproval" runat="server" Text="Leave Approval" />  </td><td>
                    <asp:CheckBox ID="chkReimbursmentApply" runat="server" Text="Reimbursement Apply"/> </td> <td><asp:CheckBox ID="chkReimbusrApproval" runat="server" Text="Reimbursement Approval"/>  </td></tr>
                        </table> </td></tr>


        </table>


         </td></tr>





          <tr> <td>  </td> <td style="width:20px"> </td> <td colspan="9" align="right">

              <asp:Button ID="btnmailsetup" runat="server" Text="Save" CssClass="btnLogin" OnClick="btnmailsetup_Click" ValidationGroup="setupM" />

               
     </td> 
     </tr>
      

       <tr> <td colspan="11">

           <div id="grdLeavedetailwrap">  

           <asp:GridView ID="grdMailSetup" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="2000px">
               <AlternatingRowStyle BackColor="White" />
               <Columns>
                   <asp:BoundField DataField="SMTPFor" HeaderText="SMTP For" />
                   <asp:BoundField DataField="from_Email" HeaderText="Default email-id for mail setup" />
                   <asp:BoundField DataField="smtp" HeaderText="SMTP for mail" />
                   <asp:BoundField DataField="Password_From" HeaderText="Password " />
                   <asp:BoundField DataField="Port_No" HeaderText="PORT No" />
                   <asp:BoundField DataField="Mail_Sending_Option" HeaderText="Mail configuration through portal" />
                   <asp:BoundField DataField="Profile_Change" HeaderText="Profile Change" />
                   <asp:BoundField DataField="Profile_Approval" HeaderText="Profile Approval" />
                   <asp:BoundField DataField="Attendence_Mark" HeaderText="Mark Attendance" />
                   <asp:BoundField DataField="Attendence_Approval" HeaderText="Attendance Approval" />
                   <asp:BoundField DataField="Leave_Apply" HeaderText="Leave Apply" />
                   <asp:BoundField DataField="Leave_Approval" HeaderText="Leave Approval" />
                   <asp:BoundField DataField="CCMail" HeaderText="CC Mail" />
                   <asp:BoundField DataField="Reimbursment_Apply" HeaderText="Reimbursement Apply" />
                   <asp:BoundField DataField="Reimbursment_Approval" HeaderText="Reimbursement Approval" />
               </Columns>
               <EditRowStyle BackColor="#2461BF" />
               <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
               <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont"/>
               <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
               <RowStyle BackColor="#EFF3FB" CssClass="cssGridheaderfont" />
               <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
               <SortedAscendingCellStyle BackColor="#F5F7FB" />
               <SortedAscendingHeaderStyle BackColor="#6D95E1" />
               <SortedDescendingCellStyle BackColor="#E9EBEF" />
               <SortedDescendingHeaderStyle BackColor="#4870BE" />
           </asp:GridView>

           </div>
            </td></tr>

     </table>
    </td> <td style="width:10px"> </td></tr></table>
    </td></tr>
    <tr> <td class="leftm"  > </td></tr>

     
    


   </table>

   


 

 

    </asp:Panel>




    <asp:Panel ID="pnlReimbursementApproval" runat="server" Visible="false" CssClass="leftBackground">

        <table cellpadding="0px" cellspacing="0px" style="width:100%">

            <tr> <td style="width:10px">  </td> <td>


                  <table cellpadding="0px" cellspacing="0px" style="width:100%">
                      
                      <tr> <td>

             <asp:Label ID="Label12" runat="server" 
            Text="Approval Type" Font-Size="15pt" ForeColor="#093A62" 
          Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
    

                                                        </td></tr> 

            <tr> <td style="height:13px"  > </td></tr>

    <tr> <td class="leftm"  > </td></tr>
            
            <tr> <td style="height:13px"  > </td></tr>

            <tr> <td>   <table cellpadding="0px" cellspacing="0px" >
                
                  <tr> <td>Type </td> <td style="width:10px"> </td> <td> <asp:TextBox ID="txtTypeRemAPR" runat="server"></asp:TextBox></td>  <td style="width:30px"> </td> <td> Description</td> <td style="width:10px"> </td> <td> <asp:TextBox ID="txtDescriptionREmAPP" runat="server" Width="200px"></asp:TextBox></td> </tr>


                <tr> <td colspan="7" style="height:10px"> </td></tr>

                 <tr> <td>Self Approval  </td> <td style="width:10px"> </td> <td>  <asp:CheckBox ID="chkSelfAPP" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelfAPP_CheckedChanged" /></td>  <td style="width:30px"> </td> <td> Limit</td> <td style="width:10px"> </td> <td> <asp:TextBox ID="txtLimitSelfREmAPR" runat="server" Enabled="False" AutoPostBack="True" OnTextChanged="txtLimitSelfREmAPR_TextChanged">0</asp:TextBox></td> </tr>

                <tr> <td colspan="7" style="height:10px"> </td></tr>

                 <tr> <td>Reporting Manager 1  </td> <td style="width:10px"> </td> <td>  <asp:CheckBox ID="chkRM1" runat="server" AutoPostBack="True" OnCheckedChanged="chkRM1_CheckedChanged" /></td>  <td style="width:30px"> </td> <td> Limit</td> <td style="width:10px"> </td> <td> <asp:TextBox ID="txtLimitRM1" runat="server" Enabled="False" AutoPostBack="True" OnTextChanged="txtLimitRM1_TextChanged">0</asp:TextBox></td> </tr>

                <tr> <td colspan="7" style="height:10px"> </td></tr>

                 <tr> <td>Reporting Manager 2  </td> <td style="width:10px"> </td> <td>  <asp:CheckBox ID="chkRM2" runat="server" AutoPostBack="True" OnCheckedChanged="chkRM2_CheckedChanged" /></td>  <td style="width:30px"> </td> <td> Limit</td> <td style="width:10px"> </td> <td> <asp:TextBox ID="txtLimitRM2" runat="server" Enabled="False" AutoPostBack="True" OnTextChanged="txtLimitRM2_TextChanged">0</asp:TextBox></td> </tr>



                        </table>  </td></tr>


            
            <tr> <td style="height:13px"  > </td></tr>
            <tr> <td >  <table cellpadding="0px" cellspacing="0px" > <tr> <td> Applicable For  </td> <td style="width:50px"> </td> <td> <asp:RadioButton ID="rdALLRemAPpr" runat="server"  Text="All" GroupName="remapr_App" AutoPostBack="True" OnCheckedChanged="rdALLRemAPpr_CheckedChanged" Checked="True"/> </td> <td style="width:10px"> </td> <td><asp:RadioButton ID="rdDepartmentRemAPr" runat="server"  Text="Department" AutoPostBack="True" GroupName="remapr_App" OnCheckedChanged="rdDepartmentRemAPr_CheckedChanged"/>  </td> <td style="width:10px"> </td> <td> <asp:DropDownList ID="ddDepartmentRemApr" runat="server" Visible="False" Height="29px"></asp:DropDownList> </td> <td style="width:10px"> </td> <td> <asp:RadioButton ID="rdIndividualremAll" runat="server"  Text="Individual" GroupName="remapr_App" AutoPostBack="True" OnCheckedChanged="rdIndividualremAll_CheckedChanged"/> </td> <td style="width:10px">  </td> <td>  
                <asp:DropDownList ID="ddIND_Employee" runat="server" Width="100px"  AppendDataBoundItems="true"  AutoPostBack="True" Visible="False" Height="29px">

                       <asp:ListItem Text="" Value="" />

                                                                                                               </asp:DropDownList></td> </tr> </table>  </td></tr>

            
            <tr> <td style="height:13px"  > </td></tr>


              <tr> <td >  <table cellpadding="0px" cellspacing="0px"> <tr> <td> Limit  </td> <td style="width:103px"> </td> <td> <asp:RadioButton ID="rdDaily" runat="server"  Text="Daily" GroupName="remapr_App_limit"/> </td> <td style="width:10px"> </td> <td> <asp:RadioButton ID="rdMonthlyRemApr" runat="server"  Text="Monthly" GroupName="remapr_App_limit"/> </td> <td style="width:10px"> </td> <td><asp:RadioButton ID="rdYearlyRemApp" runat="server"  Text="Yearly" GroupName="remapr_App_limit" Checked="True"/>  </td> <td style="width:20px"> </td> <td>  Display Limit  </td> <td style="width:10px"> </td> <td> <asp:DropDownList ID="dddisplaylimit" runat="server" Height="29px">
                  <asp:ListItem>No</asp:ListItem>
                  <asp:ListItem>Yes</asp:ListItem>
                  </asp:DropDownList> </td>  <td style="width:20px"> </td> <td>  Display Balance  </td> <td style="width:10px"> </td> <td> <asp:DropDownList ID="ddDisplayBalance" runat="server" Height="29px">
                  <asp:ListItem>No</asp:ListItem>
                  <asp:ListItem>Yes</asp:ListItem>
                  </asp:DropDownList> </td>  </tr> </table>  </td></tr>

           <tr> <td style="height:13px"  > </td></tr>

             <tr> <td>  <table cellpadding="0px" cellspacing="0px"> <tr> <td>  </td> <td style="width:10px"> </td> <td> <asp:RadioButton ID="rdPercent_of_basic_Salary" runat="server"  Text="Percent of Basic Salary" GroupName="remapr_App_limit_Per" AutoPostBack="True" OnCheckedChanged="rdPercent_of_basic_Salary_CheckedChanged"/> </td> <td style="width:10px"> </td>  <td> </td> <td><asp:RadioButton ID="rdfixed_amountRemApr" runat="server"  Text="Fixed Amount" GroupName="remapr_App_limit_Per" AutoPostBack="True" OnCheckedChanged="rdfixed_amountRemApr_CheckedChanged" Checked="True"/>  </td> <td style="width:10px"> </td> <td> <asp:TextBox ID="txtFixedAmountRemApr" runat="server">0</asp:TextBox>  <asp:Label ID="lblperfixed" runat="server" Text="Rs/- "></asp:Label> </td> </tr> </table>  </td></tr>

              <tr> <td style="height:3px"  > </td></tr>

                  <%--       <tr> <td> 

                             <table cellpadding="0px" cellspacing="0px"> <tr> <td>  Effective Date </td> <td style="width:10px"> </td> <td> <asp:TextBox ID="txteffectivedate" runat="server"></asp:TextBox> </td></tr></table>

                              </td></tr>--%>


<tr> <td align="right"> <asp:Button ID="btnSaveRemApr" runat="server" Text="Save" CssClass="btnLogin" OnClick="btnSaveRemApr_Click"/>

    <asp:Button ID="btnClearREmapr" runat="server" Text="Cancel" Visible="false" OnClick="btnClearREmapr_Click" CssClass="btnLogin"/><asp:Button ID="btnUpdateRemApr" runat="server" Text="Update" Visible="false" OnClick="btnUpdateRemApr_Click" CssClass="btnLogin" />

     </td></tr>


<tr> <td style="height:1px"  > </td></tr>
<tr> <td>
     <asp:Panel ID="pnlsearchdataremApproval" runat="server">

    <table cellpadding="0px" cellspacing="0px"> <tr> <td> &nbsp;&nbsp;&nbsp;&nbsp; Enter Approval Type</td> <td style="width:10px"> </td> <td>
        <asp:TextBox ID="txtTypeapr" runat="server"></asp:TextBox> </td> <td style="width:10px"> </td> <td> <asp:Button ID="btn_Approval_search" runat="server" Text="Search" OnClick="btn_Approval_search_Click" /></td> <td  style="width:50px"> </td> <td> <asp:Button ID="btnexporttoexcel" runat="server" Text="Export To Excel" OnClick="btnexporttoexcel_Click" /></td></tr> </table>

          </asp:Panel>  </td></tr>

                      <tr> <td>

                          <div id="grdRembursset">

                              <asp:GridView ID="grdNewreimApprosetup" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"  Width="1500px" AllowPaging="True" OnPageIndexChanging="grdNewreimApprosetup_PageIndexChanging" PageSize="4">
                                  <AlternatingRowStyle BackColor="White"  />
                              <Columns>

                                   <asp:TemplateField>
                                      <ItemTemplate>
                                          <asp:Button ID="btnEditnewApproRem" runat="server" Text="Edit"  CommandArgument='<%#Bind("id") %>' OnCommand="btnEditnewApproRem_Command"  />
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sl No.">
                                  <ItemTemplate>
                                     <%#Container.DataItemIndex+1 %>
                                  </ItemTemplate>
                              </asp:TemplateField>
                                  <asp:BoundField DataField="Type" HeaderText="Type" />
                                  <asp:BoundField DataField="Description" HeaderText="Description" />
                                  <asp:BoundField DataField="Self_Approval" HeaderText="Self Approval" />
                                  <asp:BoundField DataField="Self_Limit" HeaderText="Limit" />
                                  <asp:BoundField DataField="RM1_Approval" HeaderText="RM 1" />
                                  <asp:BoundField DataField="RM1_Limit" HeaderText="Limit" />
                                  <asp:BoundField DataField="RM2_Approval" HeaderText="RM 2" />
                                  <asp:BoundField DataField="RM2_Limit" HeaderText="Limit" />
                                  <asp:BoundField DataField="Applicable_For" HeaderText="Applicable For" />
                                  <asp:BoundField DataField="Department" HeaderText="Department" />
                                  <asp:BoundField DataField="Individual_Userid" HeaderText="No_" />
                                  <asp:BoundField DataField="Limit" HeaderText="Limit" />
                                   <asp:BoundField DataField="Display_limit" HeaderText="Display Limit" />
                                  <asp:BoundField DataField="Percentage_Of_Basic_Pay" HeaderText="Percentage Of Basic Pay" />
                                  <asp:BoundField DataField="Percentage" HeaderText="Percentage" />
                                  <asp:BoundField DataField="Fixed_Amount" HeaderText="Fixed Amount" />
                                  <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                 
                              </Columns>
                                  <EditRowStyle BackColor="#2461BF" />
                                      <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
               <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
               <PagerStyle  BackColor="#C5122F" ForeColor="White" HorizontalAlign="Center" />
               <RowStyle BackColor="#EFF3FB" CssClass="cssGridheaderfont" />
                                  <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                  <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                  <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                  <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                  <SortedDescendingHeaderStyle BackColor="#4870BE" />
                          </asp:GridView>



                                         <asp:GridView ID="grdApprovalreimsetup" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                                  <AlternatingRowStyle BackColor="White"  />
                              <Columns>

                                
                                    <asp:TemplateField HeaderText="Sl No.">
                                  <ItemTemplate>
                                     <%#Container.DataItemIndex+1 %>
                                  </ItemTemplate>
                              </asp:TemplateField>
                                  <asp:BoundField DataField="Type" HeaderText="Type" />
                                  <asp:BoundField DataField="Description" HeaderText="Description" />
                                  <asp:BoundField DataField="Self_Approval" HeaderText="Self Approval" />
                                  <asp:BoundField DataField="Self_Limit" HeaderText="Limit" />
                                  <asp:BoundField DataField="RM1_Approval" HeaderText="RM 1" />
                                  <asp:BoundField DataField="RM1_Limit" HeaderText="Limit" />
                                  <asp:BoundField DataField="RM2_Approval" HeaderText="RM 2" />
                                  <asp:BoundField DataField="RM2_Limit" HeaderText="Limit" />
                                  <asp:BoundField DataField="Applicable_For" HeaderText="Applicable For" />
                                  <asp:BoundField DataField="Department" HeaderText="Department" />
                                  <asp:BoundField DataField="Individual_Userid" HeaderText="No_" />
                                  <asp:BoundField DataField="Limit" HeaderText="Limit" />
                                   <asp:BoundField DataField="Display_limit" HeaderText="Display Limit" />
                                  <asp:BoundField DataField="Percentage_Of_Basic_Pay" HeaderText="Percentage Of Basic Pay" />
                                  <asp:BoundField DataField="Percentage" HeaderText="Percentage" />
                                  <asp:BoundField DataField="Fixed_Amount" HeaderText="Fixed Amount" />
                                  <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                 
                              </Columns>
                                  <EditRowStyle BackColor="#2461BF" />
                                      <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
               <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
               <PagerStyle  BackColor="#C5122F" ForeColor="White" HorizontalAlign="Center" />
               <RowStyle BackColor="#EFF3FB" CssClass="cssGridheaderfont" />
                                  <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                  <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                  <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                  <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                  <SortedDescendingHeaderStyle BackColor="#4870BE" />
                          </asp:GridView>
                          </div>

                          


                           </td></tr>


        </table>

                                                </td> <td style="width:10px">  </td></tr>

        </table>


      

    </asp:Panel>




    <asp:Panel ID="pnlReimTypeMaster" runat="server" Visible="false" CssClass="leftBackground">


        <table cellpadding="0px" cellspacing="0px"   >
           
            <tr> <td style="width:10px"> </td> <td> <table cellpadding="0px" cellspacing="0px">     
             <tr> <td style="height:13px"  > </td></tr>
               <tr> <td>

             <asp:Label ID="Label14" runat="server" 
            Text="Reimbursement Type Master" Font-Size="15pt" ForeColor="#093A62" 
          Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
    

                                                        </td></tr> 

            <tr> <td style="height:13px"  > </td></tr>

    <tr> <td class="leftm"  > </td></tr>
            
            <tr> <td style="height:13px"  > </td></tr>


<tr> <td> 

    <table cellpadding="0px" cellspacing="0px"> 
        
        
        <tr> <td>  Type : </td>  <td style="width:10px"> </td> <td>  <asp:TextBox ID="txtRemTypeMaster" runat="server"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required Type" ControlToValidate="txtRemTypeMaster" ForeColor="Red" SetFocusOnError="True" ValidationGroup="reimtypemaster"></asp:RequiredFieldValidator>

                                                               </td> <td style="width:10px">  </td> <td> Description : </td>  <td style="width:10px"> </td> <td>  <asp:TextBox ID="txtDescriptionRemTypeMaster" runat="server"></asp:TextBox></td></tr> 

        <tr> <td colspan="7" style="height:2px">  </td></tr>

         <tr> <td> Approval Type : </td>  <td style="width:10px"> </td> <td> <asp:DropDownList ID="ddApprovalTypeMaster" runat="server" Width="150px" Height="29px"></asp:DropDownList>  <br />
             <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddApprovalTypeMaster" ErrorMessage="Required Approval Type" ForeColor="Red" SetFocusOnError="True" ValidationGroup="reimtypemaster"></asp:RequiredFieldValidator>
             </td> <td style="width:10px">  </td> <td> Active : </td>  <td style="width:10px"> </td> <td>  <asp:CheckBox ID="chkActiveReimMaster" runat="server" Checked="True" Enabled="False" /></td></tr> 

         <tr> <td colspan="7" style="height:2px">  </td></tr>
         <tr> <td>G/L Account Dr.: </td>  <td style="width:10px"> </td> <td colspan="5"> <asp:DropDownList ID="ddGLAccountDr" runat="server" Width="400px"  AppendDataBoundItems="true" Height="29px">

              <asp:ListItem Text="" Value="" />

                                                                                         </asp:DropDownList>  
             </td> </tr> 
        <tr> <td colspan="7" style="height:12px">  </td></tr>
         <tr> <td>G/L Account Cr.: </td>  <td style="width:10px"> </td> <td colspan="5">  <asp:DropDownList ID="ddGLAccountCr" runat="server" Width="400px"  AppendDataBoundItems="true" Height="29px">

              <asp:ListItem Text="" Value="" />

                                                                                          </asp:DropDownList>   
             </td> </tr> 
<%--        <td style="width:10px">  </td> <td> G/L AccountCr. : </td>  <td style="width:10px"> </td> <td>  </td>--%>

          <tr> <td colspan="7" style="height:10px">  </td></tr>
        <tr> <td> Effective Date : </td>  <td style="width:10px"> </td> <td> <asp:TextBox ID="txtEffectiveReimMaster" runat="server"></asp:TextBox>
            <br />
           <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Required Effective Date " ControlToValidate="txtEffectiveReimMaster" ForeColor="Red" SetFocusOnError="True" ValidationGroup="reimtypemaster"></asp:RequiredFieldValidator>
            
            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtEffectiveReimMaster" Format="yyyy-MM-dd"></asp:CalendarExtender>


                                                                        </td> <td style="width:10px">  </td> <td colspan="3">   <asp:Button ID="btnCancelReimMaster" runat="server" Text="Cancel" CssClass="btnLogin" Visible="false" OnClick="btnCancelReimMaster_Click" /> <asp:Button ID="btnUpdate_ReimMaster" runat="server" Text="Update" CssClass="btnLogin" Visible="false" OnClick="btnUpdate_ReimMaster_Click" ValidationGroup="reimtypemaster"/> <asp:Button ID="btnSaveReim_Master" runat="server" Text="Save" OnClick="btnSaveReim_Master_Click" CssClass="btnLogin" ValidationGroup="reimtypemaster" /></td></tr> 

    </table>


     </td></tr>
<tr> <td style="height:13px"  > </td></tr>


                <tr> <td>  

                      <asp:Panel ID="pnlReimTypeserach" runat="server">

    <table cellpadding="0px" cellspacing="0px"> <tr> <td> &nbsp;&nbsp;&nbsp;&nbsp; Enter Type</td> <td style="width:10px"> </td> <td>
        <asp:TextBox ID="txtReimTypeSearch" runat="server"></asp:TextBox> </td> <td style="width:10px"> </td> <td> <asp:Button ID="btnReimtypesearch" runat="server" Text="Search" OnClick="btnReimtypesearch_Click" /></td> <td  style="width:50px"> </td> <td> <asp:Button ID="btnExporttoexcel_reimserch" runat="server" Text="Export To Excel" OnClick="btnExporttoexcel_reimserch_Click"  /></td></tr> </table>

          </asp:Panel>

                     </td></tr>
            <tr> <td>
                
                  <asp:GridView ID="grdReimTypeMaster" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="866px" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="grdReimTypeMaster_PageIndexChanging" PageSize="6">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                      <asp:TemplateField HeaderText="Sl No.">
                                  <ItemTemplate>
                                     <%#Container.DataItemIndex+1 %>
                                  </ItemTemplate>
                              </asp:TemplateField>
                    <asp:BoundField DataField="Reim_Type" HeaderText="Type" />
                    <asp:BoundField DataField="Reim_Description" HeaderText="Description" />
                    <asp:BoundField DataField="Approval_type" HeaderText="Approval Type" />
                    <asp:BoundField DataField="Active" HeaderText="Active" />
                    <asp:BoundField DataField="Effective_date" HeaderText="Effective Date" DataFormatString="{0:yyyy-MM-dd}" />
                      <asp:BoundField DataField="Gl_Account_Dr_Id" HeaderText="GL Account Dr." />
                      <asp:BoundField DataField="Gl_Account_CR_ID" HeaderText="GL Account Cr." />
                      <asp:TemplateField>
                          <ItemTemplate>
                              <asp:Button ID="btneditreim_master" runat="server" CommandArgument='<%#Bind("id") %>' OnCommand="btneditreim_master_Command" Text="Edit" />
                          </ItemTemplate>
                      </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
             <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
               <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont"/>
               <PagerStyle  BackColor="#C5122F" ForeColor="White" HorizontalAlign="Center" />
               <RowStyle BackColor="#EFF3FB" CssClass="cssGridheaderfont" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>

                 <asp:GridView ID="grdReimtype_excel" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"  AutoGenerateColumns="False" Width="866px" >
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                      <asp:TemplateField HeaderText="Sl No.">
                                  <ItemTemplate>
                                     <%#Container.DataItemIndex+1 %>
                                  </ItemTemplate>
                              </asp:TemplateField>
                    <asp:BoundField DataField="Reim_Type" HeaderText="Type" />
                    <asp:BoundField DataField="Reim_Description" HeaderText="Description" />
                    <asp:BoundField DataField="Approval_type" HeaderText="Approval Type" />
                    <asp:BoundField DataField="Active" HeaderText="Active" />
                    <asp:BoundField DataField="Effective_date" HeaderText="Effective Date" DataFormatString="{0:yyyy-MM-dd}" />

                       <asp:BoundField DataField="Gl_Account_Dr_Id" HeaderText="GL Account Dr." />
                      <asp:BoundField DataField="Gl_Account_CR_ID" HeaderText="GL Account Cr." />


                <%--    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btneditreim_master" runat="server" Text="Edit" OnCommand="btneditreim_master_Command" CommandArgument='<%#Bind("id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
             <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
               <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
               <PagerStyle  BackColor="#C5122F" ForeColor="White" HorizontalAlign="Center" />
               <RowStyle BackColor="#EFF3FB" CssClass="cssGridheaderfont" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>

            </td></tr>


        </table>
  </td> <td style="width:10px">  </td></tr>

        </table>

       

    </asp:Panel>



</td></tr></table>

</fieldset>

</asp:Content>

