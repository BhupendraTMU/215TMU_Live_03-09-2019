<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="Attendence.aspx.cs" Inherits="LeaveApproval" EnableEventValidation="false"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/JavaScript">
    function validateHhMm(inputField) {
        var isValid = /^([0-1]?[0-9]|2[0-3]):([0-5][0-9])(:[0-5][0-9])?$/.test(inputField.value);

        if (isValid) {
            
        } else {

            alert(" Invalid time .. Please enter valid time in 24 hrs format ");
            inputField.value = "";
        }
        return isValid;
    }
</script>
      <style type="text/css">
        #grdAttendencewrap
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

        <style type="text/css">

#GridScrollProfile 
{
width:650px;
height:100%;
overflow:scroll;
}


#grdRejectedwrap 
{
width:650px;
height:100%;
overflow:scroll;
}


#grdResolvedwrap 
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
              if (confirm("Do you want to Delete  this Attendence ?")) {
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
              if (confirm("Do you want to Save ?")) {
                  confirm_value.value = "Yes";
              } else {
                  confirm_value.value = "No";
              }
              document.forms[0].appendChild(confirm_value);
          }

</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    
    <fieldset class="boxBody"  >
 <asp:Label ID="Label1" runat="server" 
            Text="Attendance" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 </fieldset>


    <fieldset class="boxBody"> 

<table cellpadding="0px" cellspacing="0px">  <tr>  <td  style="width:10px">  </td>
<td  style="width:200px" valign="top"> 

<table cellpadding="0px" cellspacing="0px" class="leftbg1" style="width:220px; height:430px">  <tr> <td style="width:10px"> </td> <td>


<table cellpadding="0px" cellspacing="0px" >
 <tr> <td style="height:10px"> </td></tr>
 <tr runat="server" id="trMarkAttendance" visible="false"> <td class="leftmMenu">  
     <img src="../logo/Star.png" />
    <asp:LinkButton ID="lnkProfileview" runat="server" onclick="lnkleaveview_Click" > Mark Attendance</asp:LinkButton></td></tr>
    <tr> <td style="height:10px"> </td></tr>
     <tr> <td class="leftmMenu"> <img src="../logo/Star.png" id="img2" runat="server"/>
    <asp:LinkButton ID="lnkRejectProfileDetail" runat="server" 
             onclick="lnkRejectLeaveDetail_Click" >View Attendance </asp:LinkButton></td></tr>
     <tr> <td style="height:10px"> </td></tr>
  

     
     <tr runat="server" id="trPostAttendance" visible="false" > <td class="leftmMenu">  <img src="../logo/Star.png" id="imgStar" runat="server"/>
    <asp:LinkButton ID="lnkpostAttendance" runat="server"  OnClick="lnkpostAttendance_Click"
              >Post Attendance </asp:LinkButton> <asp:Label ID="lblPostAttendanceNotify" runat="server" Text="" ForeColor="Red"></asp:Label></td></tr>
     <tr> <td style="height:10px">  </td></tr>


    <tr runat="server" id="trViewTeamAttendance" visible="false"> <td class="leftmMenu">  <img src="../logo/Star.png" id="img1" runat="server"/>
    <asp:LinkButton ID="lnkViewTeamAttendance" runat="server"
            PostBackUrl="~/AttendenceDetail.aspx" Visible="false"  >View Team Attendance </asp:LinkButton> </td></tr>
     <tr> <td style="height:10px">  </td></tr>



   <%--<tr> <td style="height:13px"> </td></tr>--%>


    <tr> <td >

        <asp:Panel ID="pnlviewattend" runat="server">
            <table cellpadding="0px" cellspacing="0px"> 

                <tr> <td>

        <%--<asp:Label ID="Label4" runat="server" 
            Text=" Color represents in calender" Font-Size="10pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>--%>

         </td></tr>
    <tr> <td style="height:5px"> 
         &nbsp;</td></tr>

     <tr> <td>
       <table cellpadding="0px" cellspacing="0px"><tr> <td> <div style="background-color:Gray; width:5px; color:Gray" >c </div></td> <td>  <label >&nbsp;&nbsp;&nbsp;Days Blocked</label></td></tr> </table>  

         </td></tr>
     <tr> <td style="height:2px"> 
        </td></tr>
    <tr> <td>
       <table cellpadding="0px" cellspacing="0px"><tr> <td> <div style="background-color:lightblue ; width:5px; color:lightblue" >c </div></td> <td> <label>&nbsp;&nbsp; Leave</label></td></tr> </table>  

         </td></tr>

     <%--<tr> <td style="height:2px"> 
        </td></tr>

    <tr> <td>
       <table cellpadding="0px" cellspacing="0px"><tr> <td> <div style="background-color:Aqua; width:5px; color:Aqua" >c </div></td> <td>  <label>&nbsp;&nbsp;Approved Half Leave</label></td></tr> </table>  

         </td></tr>--%>

 

     <tr> <td style="height:2px"> 
        </td></tr>


    <tr> <td>
       <table cellpadding="0px" cellspacing="0px"><tr> <td> <div style="background-color:lawngreen ; width:5px ; color:lawngreen" >c </div></td> <td> <label> &nbsp;&nbsp;Marked</label ></td></tr> </table>  

         </td></tr>

   <tr> <td style="height:2px"> 
        </td></tr>

    <tr> <td>
       <table cellpadding="0px" cellspacing="0px"><tr> <td> <div style="background-color:white ; width:5px; color:white" >c </div></td> <td> <label>&nbsp;&nbsp;To be Marked </label></td></tr> </table>  

         </td></tr>
    <tr> <td style="height:2px"> 
        </td></tr>

     <tr> <td>
       <table cellpadding="0px" cellspacing="0px"><tr> <td>  <div style="background-color:SandyBrown ; width:5px; color:SandyBrown" >c </div></td> <td> <label>&nbsp;&nbsp;Posted Attendance </label></td></tr> </table>  

         </td></tr>
    <tr> <td style="height:2px"> 
        </td></tr>

    <tr> <td>  
       
        <asp:Calendar ID="clndview" runat="server" BackColor="White" 
           BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" 
           DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" 
           ForeColor="Black" Height="150px" Width="220px" OnDayRender="clndview_DayRender" Enabled="False" OnSelectionChanged="clndview_SelectionChanged" >
           <DayHeaderStyle BackColor="#ffccff" ForeColor="#336666" Height="1px" />
           <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
           <OtherMonthDayStyle ForeColor="#999999" />
           <SelectedDayStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#CCFF99" />
           <SelectorStyle BackColor="#ed7600" ForeColor="Black" />
           <TitleStyle BackColor="#ed7600" BorderColor="#3366CC" BorderWidth="1px" 
               Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
           <TodayDayStyle BackColor="White" ForeColor="Black" />
       </asp:Calendar>


         </td></tr>



            </table>



        </asp:Panel>


         </td></tr>


    </table>
 </td> <td style="width:10px"> </td></tr></table>



    
</td>  <td style="width:30px">  </td>
 <td valign="top"> 
 
 
    <asp:Panel ID="pnlAttendenceMark" runat="server" CssClass="leftBackground" Visible="false">

    <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px"> </td> <td>
    
    <table cellpadding="0px" cellspacing="0px">  <tr> <td > 
    
   
    <br />
    <asp:Label ID="Label2" runat="server" 
            Text="Mark Attendance" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
           
  </td></tr>
         <tr> <td style="height:6px"> </td></tr>
        <tr> <td class="leftm"> </td></tr>
    <tr> <td style="height:6px"> </td></tr>

        <tr> <td>
            <table cellpadding="0px" cellspacing="0px"> 

                <tr> <td> 
                    
                     <table cellpadding="0px" cellspacing="0px" style="width:100%"> <tr> <td> <label> <asp:Label ID="Label5" runat="server" Text=" Color represents in calender" Font-Bold="True" Font-Size="12pt" ForeColor="#CC6699" Visible="false"></asp:Label> </label> </td> <td style="width:50px">  </td>    <td>  </td>  </tr></table> </td></tr>
                   <tr> <td style="height:10px"> </td></tr>
                <tr><td>
                     <table> <tr> <td>   <asp:Panel ID="Panel1" runat="server" Width="5px" BackColor="Gray" ForeColor="Gray" Visible="false"> c </asp:Panel></td> <td><%--<label >&nbsp;Days Blocked</label> --%></td><td style="width:40px"> </td>  <td>  <div style="background-color:lawngreen ; width:5px ; color:lawngreen" >c </div></td> <td><label> &nbsp;Marked</label > </td> <td style="width:40px">  </td> <td>  <%--<div style="background-color:white ; width:5px; color:white" >c </div>--%></td> <td> <%--<label>&nbsp;To be Marked </label>--%></td> <td style="width:40px">  </td> <td>  <div style="background-color:SandyBrown ; width:5px; color:SandyBrown" >c </div></td> <td> <label>&nbsp;Posted Attendance </label></td> <td style="width:40px"></td> <td align="right">  <div style="background-color:lightblue ; width:5px; color:lightblue" >c </div></td> <td> <label>&nbsp;Leave</label></td>  <td  style="width:2px"> </td><%--<td align="right">  <div style="background-color:Aqua; width:5px; color:Aqua" >c </div></td> <td> <label>&nbsp;Approved Half Leave</label></td>--%></tr></table>   </td> </tr>

                                <tr><td style="height:10px">



                                    </td> </tr>


                 

            </table>


             </td></tr>
            <tr> <td style="height:3px"> </td></tr>
    <tr> <td class="leftm"> </td></tr>
    <tr> <td style="height:13px"> </td></tr>
   

   <tr> <td>
   
   <table cellpadding="0px" cellspacing="0px"> <tr> <td>  <label> Select Date </label></td> <td style="width:10px"> </td> <td> 
      
       <table cellpadding="0px" cellspacing="0px">   <tr> <td style="vertical-align:top"> <asp:Calendar ID="Calendar1" runat="server" BackColor="White" 
           BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" 
           DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" 
           ForeColor="Black" Height="150px" Width="220px" OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChanged">
           <DayHeaderStyle BackColor="#ffccff" ForeColor="#336666" Height="1px" />
           <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
           <OtherMonthDayStyle ForeColor="#999999" />
           <SelectedDayStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#CCFF99" />
           <SelectorStyle BackColor="#ed7600" ForeColor="Black" />
           <TitleStyle BackColor="#ed7600" BorderColor="#3366CC" BorderWidth="1px" 
               Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
           <TodayDayStyle BackColor="White" ForeColor="Black" />
           <WeekendDayStyle  />
       </asp:Calendar></td> <td style="width:10px"> &nbsp;</td> <td style="vertical-align:top">   
         <table cellpadding="0px" cellspacing="0px">  
     
     <tr> <td> <label >In Time  </label></td> <td style="width:5px"> </td> <td > 
         <asp:TextBox ID="txtFromTime" runat="server" onchange="validateHhMm(this);" Width="50px"></asp:TextBox> 
         </td> <td style="width:10px"> </td> <td><label>Out Time </label> </td> <td style="width:20px"> </td><td>
             <asp:TextBox ID="txtToTime" runat="server" onchange="validateHhMm(this);" Width="50px"></asp:TextBox> 
         </td> </tr>

             <tr> <td colspan="7" style="height:13px"> </td></tr>

             <tr> <td>Location </td><td style="width:5px"> </td> <td > 
         <asp:TextBox ID="txtJobLocation" runat="server" Width="200px"></asp:TextBox> </td><td style="width:10px"> </td> <td>Date</td> <td style="width:20px"> </td><td>
                 <asp:TextBox ID="txtSelectedDate" runat="server" Width="70px" Enabled="false"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSelectedDate" ErrorMessage="Date Required" ForeColor="Red" SetFocusOnError="True" ValidationGroup="calend"></asp:RequiredFieldValidator>
                 </td> </tr>
          <tr> <td colspan="7" style="height:13px"> </td></tr>

             <tr> <td>Remarks </td><td style="width:20px"> </td> <td colspan="5"> 
         <asp:TextBox ID="txtRemarks" runat="server" Width="400px" TextMode="MultiLine" Height="70px"></asp:TextBox> </td> </tr>


     </table>
           


                             </td></tr> </table>
        
   </td></tr> </table>
    </td></tr>

     <tr> <td style="height:3px"> </td></tr>

    
      <tr> <td align="right">

          <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="btnLogin" OnClick="btnClose_Click" Visible="false"/>
          <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" CssClass="btnLogin" OnClick="btnUpdate_Click" ValidationGroup="calend" />
            
          <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnLogin" 
              onclick="btnSave_Click" ValidationGroup="calend" /> 
          <asp:Label ID="lblDateForUpdate" runat="server" Visible="False"></asp:Label>
          </td></tr>
           <tr> <td style="height:13px"> 
               <asp:Label ID="lblCO" runat="server" Text="0" Visible="False"></asp:Label>
               </td></tr>
              <tr> <td>  
                <%--  <div id="grdAttendencewrap">  --%>
                  <asp:GridView ID="grdAttendence" runat="server" AutoGenerateColumns="False" 
             CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" AllowPaging="True" OnPageIndexChanging="grdAttendence_PageIndexChanging" PageSize="3" OnRowDataBound="grdAttendence_RowDataBound">
             <AlternatingRowStyle BackColor="White" />
             <Columns>
                 <asp:BoundField DataField="Userid" HeaderText="User Id" Visible="false" />
                 <asp:BoundField DataField="ApprovalStatus" HeaderText="Status" Visible="False" />
                  <%-- <asp:BoundField DataField="Status" HeaderText="Status" />--%>
                 <asp:BoundField DataField="Atte_Date" HeaderText="Date" />
                 <asp:BoundField DataField="fromTime" HeaderText="In Time" />
                 <asp:BoundField DataField="ToTime" HeaderText="Out Time" />
                 <asp:BoundField DataField="Work_period" HeaderText="Work Period" Visible="False" />
                 <asp:BoundField DataField="Job_location" HeaderText="Location" />
                  <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left">
                                                                                                <ItemTemplate>
                                                                                                    <div style="width: 150px; overflow:auto; white-space: nowrap; text-overflow:clip">
                                                                                                        <%# Eval("Remarks") %>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>
                <%-- <asp:BoundField DataField="Remarks" HeaderText="Remarks" />--%>
               
                 <asp:BoundField DataField="Working_Duration" HeaderText="Working Hours" Visible="False" />
                 <asp:TemplateField HeaderText="Through" Visible="False">
                     <ItemTemplate>
                         <asp:Label ID="lblAttendanceType" runat="server" Text='<%#Bind("[Attendance Type]") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField>
                     <ItemTemplate>
                         <%--<asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Bind("id") %>' OnCommand="lnkDelete_Command" OnClientClick="Confirm_Delete()">Delete</asp:LinkButton>--%>
                         <asp:Button ID="lnkDelete" runat="server" CommandArgument='<%#Bind("id") %>' OnClientClick="Confirm_Delete()" OnCommand="lnkDelete_Command" Text="Delete" Visible="false" />
                         <asp:Button ID="btnEdit" runat="server" CommandArgument='<%#Bind("id") %>' OnCommand="btnEdit_Command" Text="Edit" />
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>
             <EditRowStyle BackColor="#2461BF" />
                      <EmptyDataTemplate>
                          There are no records found
                      </EmptyDataTemplate>
             <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
             <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="White" CssClass="cssGridheaderfont" HorizontalAlign="Left"/>
             <PagerStyle BackColor="#ed7600" ForeColor="White" HorizontalAlign="Left" />
             <RowStyle BackColor="#EFF3FB" CssClass="cssGridheaderfont" HorizontalAlign="Left" />
             <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
             <SortedAscendingCellStyle BackColor="#F5F7FB" />
             <SortedAscendingHeaderStyle BackColor="#6D95E1" />
             <SortedDescendingCellStyle BackColor="#E9EBEF" />
             <SortedDescendingHeaderStyle BackColor="#4870BE" />
         </asp:GridView> 
                      
                     <%-- </div>--%></td></tr>

                  <%--<tr> <td style="height:13px"> </td></tr>--%>




    <tr> <td class="leftm"> </td></tr>

     <tr> <td style="height:10px"> </td></tr>

    


     <tr> <td>  
         </td></tr>


          <tr> <td> 
          
          <table cellpadding="0px" cellspacing="0px"> <tr> <td>  </td></tr></table>
          </td></tr>
   
    </table>
     </td> <td style="width:10px"> </td> </tr></table>

    
    </asp:Panel>



      <asp:Panel ID="pnlviewAttendence" runat="server" CssClass="leftBackground" 
         Visible="true">

    <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px"> </td> <td>
    
    <table cellpadding="0px" cellspacing="0px">  <tr> <td > 
    
   
    <br />
    <asp:Label ID="Label3" runat="server" 
            Text="View Attendance" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
           
  </td></tr>
    <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>
    <tr> <td style="height:13px"> </td></tr>
  
  <tr> <td>
  <table cellpadding="0px" cellspacing="0px" style="width:866px"> <tr> <td> <label>From Date  </label> </td> <td style="width:10px"> </td> <td> 
      <asp:TextBox ID="txtfromDate" runat="server" Width="100px"></asp:TextBox>
      <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfromDate" Format="yyyy-MM-dd">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtfromDate">
      </cc1:TextBoxWatermarkExtender>

      </td> <td style="width:10px"> </td> <td><label>To Date </label> </td> <td style="width:10px"> </td> <td>
          <asp:TextBox ID="txtTodate" runat="server" Width="100px"></asp:TextBox> </td> <td style="width:10px"> 
          
           <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTodate" Format="yyyy-MM-dd">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtTodate">
      </cc1:TextBoxWatermarkExtender>

          </td> <td>

               <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px"> </td> <td> &nbsp;</td> <td style="width:10px"> </td> <td> <asp:DropDownList ID="ddstatus" runat="server" Visible="False" Height="29px">
          <asp:ListItem Selected="True">All</asp:ListItem>
          <asp:ListItem>Pending</asp:ListItem>
          <asp:ListItem Value="Approved">Posted</asp:ListItem>
          </asp:DropDownList> </td>   <td>      <asp:Button ID="btnLeaveViewSearch" runat="server" Text="Search" 
                  CssClass="btnLogin" onclick="btnLeaveViewSearch_Click" Visible="False" /> <asp:Button ID="btnsearchnavdata" runat="server" Text="Search" 
                  CssClass="btnLogin" OnClick="btnsearchnavdata_Click"  /></td></tr> </table>

           </td></tr></table>
  
   </td></tr>


                  <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>
          <tr> <td style="height:13px"> </td></tr>

    <tr> <td> 
        

        <table cellpadding="0px" cellspacing="0px"> <tr> <td>

            <asp:Panel ID="pnlworkingday" runat="server" Visible="false">
            <table cellpadding="0px" cellspacing="0px"> <tr> <td style="color:red"> <label> Working Days&nbsp;&nbsp;&nbsp;&nbsp; </label> </td> <td style="color:red"> <asp:Label ID="lblworkingdays" runat="server" Text=""></asp:Label>  </td> <td style="width:100px">  </td>  <td style="color:red"> <label > Present&nbsp;&nbsp;&nbsp;&nbsp;</label> </td> <td style="color:red"> <asp:Label ID="lblpresent" runat="server" Text=""></asp:Label>  </td><td style="width:100px">  </td>  <td style="color:red"> <label > Leave &nbsp;&nbsp;&nbsp;&nbsp; </label> </td> <td style="color:red"> <asp:Label ID="lblLeave" runat="server"></asp:Label>  </td> </tr>  </table>
            </asp:Panel>
                                                         </td></tr> 




             <tr> <td> 
                 <%-- <div id="grdViewLeaveStatuswrap"> --%>
                 
                 
                 
                 
                  
                  <asp:GridView ID="grdViewdetail" runat="server" AutoGenerateColumns="False" 
             CellPadding="4" ForeColor="#333333" GridLines="None" Width="866px" AllowPaging="True" PageSize="15" OnPageIndexChanging="grdViewdetail_PageIndexChanging" OnRowDataBound="grdViewdetail_RowDataBound">
             <AlternatingRowStyle BackColor="White" />
             <Columns>
                <%-- <asp:BoundField DataField="ApprovalStatus" HeaderText="Status" />--%>
                 <%-- <asp:BoundField DataField="Status" HeaderText="Attendance Status" />--%>

                 <%--<asp:BoundField DataField="Remarks" HeaderText="Remarks" />--%>
                
                 <asp:BoundField DataField="Userid" HeaderText="User Id" Visible="false" />
               
                 <asp:TemplateField HeaderText="Status" Visible="False">
                     <ItemTemplate>
                         <asp:Label ID="lblApprovalStatusgrid" runat="server" Text='<%#Bind("ApprovalStatus") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:BoundField DataField="Atte_Date" HeaderText="Date" />
                 <asp:BoundField DataField="fromTime" HeaderText="In Time" />
                 <asp:BoundField DataField="ToTime" HeaderText="Out Time" />
                 <asp:TemplateField HeaderText="Status">
                     <ItemTemplate>
                         <asp:Label ID="lblapprovalstatusgridAttend" runat="server" Text='<%#Bind("ApprovalStatus") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:BoundField DataField="Work_period" HeaderText="Work Period" Visible="False" />
                 <asp:BoundField DataField="Job_location" HeaderText="Location" />
                 <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left">
                     <ItemTemplate>
                         <div style="width: 300px; overflow:auto; white-space: nowrap; text-overflow:clip">
                             <%# Eval("Remarks") %>
                         </div>
                     </ItemTemplate>
                     <ItemStyle HorizontalAlign="Left" />
                 </asp:TemplateField>
                 <asp:BoundField DataField="Working_Duration" HeaderText="Working Hours" Visible="False" />
               
             </Columns>
             <EditRowStyle BackColor="#2461BF" />
                      <EmptyDataTemplate>
                          There are no records found
                      </EmptyDataTemplate>
             <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
             <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="White" CssClass="cssGridheaderfont" HorizontalAlign="Left"/>
             <PagerStyle BackColor="#ed7600" ForeColor="White" HorizontalAlign="Left" />
             <RowStyle BackColor="#EFF3FB" CssClass="cssGridheaderfont" HorizontalAlign="Left" />
             <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
             <SortedAscendingCellStyle BackColor="#F5F7FB" />
             <SortedAscendingHeaderStyle BackColor="#6D95E1" />
             <SortedDescendingCellStyle BackColor="#E9EBEF" />
             <SortedDescendingHeaderStyle BackColor="#4870BE" />
         </asp:GridView> 
                      

                     <asp:GridView ID="grdviewApprovalofnav" runat="server" AutoGenerateColumns="False" 
             CellPadding="4" ForeColor="#333333" GridLines="None" Width="866px" AllowPaging="True" PageSize="13" OnPageIndexChanging="grdviewApprovalofnav_PageIndexChanging" OnRowDataBound="grdviewApprovalofnav_RowDataBound" >
             <AlternatingRowStyle BackColor="White" />
             <Columns>
              
                
                
               
              
                 <asp:BoundField DataField="Attendance Date" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" />


                     <asp:TemplateField HeaderText="In Time">
                     <ItemTemplate>
                         <asp:Label ID="lbltimeinnav" runat="server" Text='<%# Eval("Time in","{0:t}") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>


                   <asp:TemplateField HeaderText="Out Time">
                     <ItemTemplate>
                         <asp:Label ID="lbltimeoutnav" runat="server" Text='<%# Eval("Time Out","{0:t}") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
             

                  <asp:TemplateField HeaderText="Status">
                     <ItemTemplate>
                         <asp:Label ID="lblStatusStatus" runat="server" Text='<%#Bind("Status") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>


         
               
             </Columns>
             <EditRowStyle BackColor="#2461BF" />
                      <EmptyDataTemplate>
                          There are no records found
                      </EmptyDataTemplate>
             <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
             <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="White" CssClass="cssGridheaderfont" HorizontalAlign="Left"/>
             <PagerStyle BackColor="#ed7600" ForeColor="White" HorizontalAlign="Left" />
             <RowStyle BackColor="#EFF3FB" CssClass="cssGridheaderfont" HorizontalAlign="Left" />
             <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
             <SortedAscendingCellStyle BackColor="#F5F7FB" />
             <SortedAscendingHeaderStyle BackColor="#6D95E1" />
             <SortedDescendingCellStyle BackColor="#E9EBEF" />
             <SortedDescendingHeaderStyle BackColor="#4870BE" />
         </asp:GridView> 


                      <%--</div>--%> </td></tr>
        </table>




           
       </td></tr>

     <tr> <td style="height:13px"> </td></tr>

         
   
    </table>
     </td> <td style="width:10px"> </td> </tr></table>

    
    </asp:Panel>




       <asp:Panel ID="pnlProfileView" runat="server" CssClass="leftBackground" 
         Visible="false">

    <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px"> </td> <td>
    
    <table cellpadding="0px" cellspacing="0px">  <tr> <td > 
    
   
    <br />
    <asp:Label ID="Label6" runat="server" 
            Text="Post Attendance" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
          
  </td></tr>
    <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>
    <tr> <td style="height:13px"> </td></tr>
    <tr> <td>  
    

    <table cellpadding="0px" cellspacing="0px" style="width:866px"> <tr> <td> <label>Select Records by </label> </td> <td style="width:90px"> </td> <td> 
        &nbsp;</td> <td style="width:30px"> </td> <td> 
        &nbsp;</td> <td style="width:30px"> </td> <td> 
        <asp:RadioButton ID="rdDatewise" runat="server" Text="Date Wise " 
            GroupName="ATTDC" oncheckedchanged="rdDatewise_CheckedChanged" 
            AutoPostBack="True"/> </td><td style="width:30px"> </td> <td> 
        <asp:RadioButton ID="CHKAllPending" runat="server" Text="All" 
            GroupName="ATTDC" 
            AutoPostBack="True" OnCheckedChanged="CHKAllPending_CheckedChanged"/> </td>
        </tr></table>

   
        
        
        
         </td></tr>
         <tr> <td style="height:13px"> </td></tr>
         <tr> <td class="leftm"> </td></tr>
          <tr> <td style="height:20px"> </td></tr>

          <tr> <td >  
              <table cellpadding="0px" cellspacing="0px"> <tr> <td>  
              <asp:Panel ID="pnlEmployeeidName" runat="server" Visible="false">

               <table cellpadding="0px" cellspacing="0px" > <tr> <td> <label>Enter Employee Id/ Name </label> </td> <td style="width:110px"> </td><td> 
                   <asp:TextBox ID="txtSearchName" runat="server" Width="200px"></asp:TextBox></td> </tr></table>

              </asp:Panel>

                 <asp:Panel ID="pnlDate" runat="server" Visible="false">

               <table cellpadding="0px" cellspacing="0px" > <tr> <td> <label>From Date</label> </td> <td style="width:10px"> </td><td> 
                   <asp:TextBox ID="txtfromDatePost" runat="server"></asp:TextBox>
                   
                     <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtfromDatePost" Format="yyyy-MM-dd">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtfromDatePost">
      </cc1:TextBoxWatermarkExtender>

                   </td>  <td style="width:30px"> </td> <td>To Date </td>  <td style=" width:10px"> </td><td>
                       <asp:TextBox ID="txtTodatePost" runat="server"></asp:TextBox> 
                       
                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtTodatePost" Format="yyyy-MM-dd">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtTodatePost">
      </cc1:TextBoxWatermarkExtender>

                       </td>  </tr></table>

              </asp:Panel>

          </td>   <td style="width:40px"> </td> <td>  
              <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btnLogin" 
                  Visible="False" onclick="btnSearch_Click"/></td></tr></table> </td></tr>

                  <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>

     <tr> <td style="height:13px"> </td></tr>

          <tr> <td>
          <table cellpadding="0px" cellspacing="0px">
          <tr> <td align="right"> 
              <asp:Button ID="btnApprove" runat="server" Text="Post" 
                  Visible="False" onclick="btnApprove_Click" CssClass="btnLogin" />   </td>
             
           

               </tr>

              <tr> <td style="height:10px" colspan="3"> </td></tr>

          <tr> <td colspan="3">

             


         
             

              

       
          <%--   <div id="GridScrollProfile">--%>
 

             <table cellpadding="0px" cellspacing="0px"><tr><td align="right">    </td></tr>

                  <tr> <td >  

                      

                      
                       <asp:Button ID="btnselectchecked" runat="server" OnClick="btnselectchecked_Click" Text="Select All" Visible="False" />
                       <asp:Button ID="btnuncheked" runat="server" OnClick="btnuncheked_Click" Text="Un Select" Visible="False" />

                       </td></tr>

              </table> 





              <asp:GridView ID="grdViewApproval" runat="server" AutoGenerateColumns="False" Width="866px" 
                     AllowPaging="True" onpageindexchanging="grdViewApproval_PageIndexChanging" 
                     PageSize="6" 
                    >
                  <Columns>
                      <%--<asp:TemplateField>
                          <ItemTemplate>
                              <asp:Button ID="btnchangestatus" runat="server" Text="Change Status" CommandArgument='<%#Bind("Userid") %>' OnCommand="btnchangestatus_Command" />
                             
                          </ItemTemplate>
                      </asp:TemplateField>--%>
                     
                      <%-- <asp:BoundField DataField="ApprovalStatus" HeaderText="Approval Status" />--%><%-- <asp:BoundField DataField="ProfileUpdateDate" HeaderText="Profile UpdateDate" />--%>
                  <%--    <asp:BoundField DataField="Remarks" HeaderText="Remarks" />--%>


                     <%-- <asp:BoundField DataField="Status" HeaderText="Status" />--%>
                    <%--  <asp:BoundField DataField="CountStatusHR" HeaderText="HR Approval Status" />
                      <asp:BoundField DataField="CountStatusHOD" HeaderText="HOD Approval Status" />--%>
                    
                      <asp:TemplateField HeaderText="">
                          <ItemTemplate>
                              <asp:CheckBox ID="chkMark" runat="server" ForeColor="White" Text='<%#Bind("Userid") %>' Width="20px" />
                          </ItemTemplate>
                      </asp:TemplateField>
                    
                      <asp:BoundField DataField="Uname" HeaderText="Name" Visible="False"/>
                      <asp:TemplateField HeaderText="Date">
                          <ItemTemplate>
                              <asp:Label ID="lblProfilechangedate" runat="server" Text='<%#Bind("Atte_Date") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="fromTime" HeaderText="In Time" />
                      <asp:BoundField DataField="ToTime" HeaderText="Out Time" />
                      <asp:TemplateField HeaderText="Status">
                          <ItemTemplate>
                              <asp:Label ID="lblApprovalSTatus" runat="server" Text='<%#Bind("ApprovalStatus") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="Working_Duration" HeaderText="Working Hours" Visible="False" />
                      <asp:BoundField DataField="Job_location" HeaderText="Location" />
                      <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left">
                          <ItemTemplate>
                              <div style="width: 300px; overflow:auto; white-space: nowrap; text-overflow:clip">
                                  <%# Eval("Remarks") %>
                              </div>
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Left" />
                      </asp:TemplateField>
                      <asp:BoundField DataField="CompanyName" HeaderText="Company Name" Visible="false" />
                  </Columns>
                  <EmptyDataTemplate>
                      There is no record found..............
                  </EmptyDataTemplate>
                  <HeaderStyle BackColor="#ed7600" Font-Bold="True"  ForeColor="White"  HorizontalAlign="Left" CssClass="cssGridheaderfont" Font-Names="Open Sans"/>
                  <RowStyle CssClass="cssGridheaderfont" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" BackColor="White"/>

                  
              </asp:GridView><%-- </div>--%>
          
           </td></tr>
          
           </table>

                
       
          </td></tr>
   
    </table>
     </td> <td style="width:10px"> </td> </tr></table>

    
    </asp:Panel>





      <asp:Panel ID="pnlMain" runat="server" Visible="false">
     <table cellpadding="0px" cellspacing="0px"> <tr> <td>
     <table cellpadding="0px" cellspacing="0px"> <tr> <td>
      <p>&nbsp;&nbsp;&nbsp;&nbsp; </p>
         <p>
             <strong>Functions Of Attendance System </strong>
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
         ..... The Attendance Management System is a Web based application that can be used 
         over the internet<br /> <strong>&nbsp;</strong><br /> ..... It standardizes a common 
         universal process for entire organization.
       
         <br />
         <br />
         ..... Generates required Reports.
         <br />
         <br />
         <strong>.</strong>.... View Attendance Detail
         <br />
         <br />
        
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

</asp:Content>

