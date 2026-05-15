<%@ Page Title="" Language="C#" MasterPageFile="~/IndexMaster.master" AutoEventWireup="true" CodeFile="AttendenceDetail.aspx.cs" Inherits="AttendenceDetail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
  <fieldset class="boxBody">
 <asp:Label ID="Label1" runat="server" 
            Text="View Team Attendance" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 </fieldset>

<fieldset class="boxBody"> 

<table cellpadding="0px" cellspacing="0px" style="width:100%"> <tr> <td> 

    <asp:Panel ID="pnlviewAttendence" runat="server" CssClass="leftBackground" 
         Visible="true">

    <table cellpadding="0px" cellspacing="0px"  style="width:100%"> <tr> <td style="width:10px"> </td> <td>
    
    <table cellpadding="0px" cellspacing="0px"  style="width:100%">  <tr> <td > 
    
   
 <%--   <br />--%>
    <%--<asp:Label ID="Label3" runat="server" 
            Text="View Attendance / Leave Detail" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>--%>
           
  </td></tr>
   <%-- <tr> <td style="height:13px"> </td></tr>--%>

   <%-- <tr> <td class="leftm"> </td></tr>--%>





        <tr> <td style="height:13px"> </td></tr>


        <tr> <td>


          <%--  <asp:Label ID="lblhead" runat="server" 
            Text="You can view attendance only upto " Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>   <asp:Label ID="Label7" runat="server" 
            Text="You can view attendance only upto " Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>--%>
             </td></tr>




    <tr> <td style="height:13px"> </td></tr>
  


        <tr> <td align="center">   

              <table cellpadding="0px" cellspacing="0px"> <tr> <td> <label> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; From Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   </label> </td> <td> <asp:TextBox ID="txtfromDate" runat="server" Width="100px"></asp:TextBox>
      <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfromDate" Format="yyyy-MM-dd">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtfromDate">
      </cc1:TextBoxWatermarkExtender></td>  <td style="width:100px"> </td> <td> <label> To Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   </label> </td> <td> <asp:TextBox ID="txtTodate" runat="server" Width="100px"></asp:TextBox>
      <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTodate" Format="yyyy-MM-dd">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtTodate">
      </cc1:TextBoxWatermarkExtender></td> <td style="width:100px"> </td> <td>  <asp:Button ID="Button2" runat="server" Text="Search" OnClick="Button2_Click" CssClass="btnLogin"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td></tr>  </table>

             </td></tr>


            <tr> <td style="height:20px"> </td></tr>

        <tr> <td >

            <asp:Panel ID="pnlFilter" runat="server" Visible="False" BackColor="#f2f2f2">



            <table cellpadding="0px" cellspacing="0px">
                
                <tr> <td colspan="10" style="height:10px">  </td></tr>
                <tr> <td>  Filter By : </td> <td style="width:10px"> </td> <td> <asp:RadioButton ID="rdEmployeeid" runat="server" Checked="True" GroupName="vteamAtt" Text="Employee ID" /> </td> <td style="width:10px"> </td> <td> <asp:RadioButton ID="rdEmployeeName" runat="server" GroupName="vteamAtt" Text="Employee Name" /> </td> <td style="width:50px"> </td> <td >  Enter Employee id / Employee Name </td> <td style="width:10px">  </td> <td>  <asp:TextBox ID="txtNameid" runat="server"></asp:TextBox></td> <td style="width:10px"> </td> <td>  <asp:Button ID="btnSearch" runat="server" Text="Get" OnClick="btnSearch_Click" /></td></tr> </table>
            </asp:Panel>

            <asp:GridView ID="grdViewAttendancehod" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" OnRowDataBound="grdViewAttendancehod_RowDataBound">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <%--<asp:BoundField DataField="No_" HeaderText="Employee Code" />--%>

                      <asp:TemplateField HeaderText="Employee Code">
                        <ItemTemplate>
                            <asp:Label ID="lblEmpCodeGrid" runat="server" Text='<%#Bind("No_") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="First Name" HeaderText="Name" />

                     <asp:TemplateField HeaderText="Working Days">
                        <ItemTemplate>
                            <asp:Label ID="lblworkingDaysGrid" runat="server" Text='<%#Bind("HOD") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Present">
                        <ItemTemplate>
                            <asp:Label ID="lblPresentgrid" runat="server" Text='<%#Bind("HOD") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Leave">
                        <ItemTemplate>
                            <asp:Label ID="lblLeaveGrid" runat="server" Text='<%#Bind("HOD") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnviewAttendanced" runat="server" Text="View Detail" OnCommand="btnviewAttendanced_Command" CommandArgument='<%#Bind("No_") %>'  />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="White" CssClass="cssGridheaderfont" HorizontalAlign="Left"/>
             <PagerStyle BackColor="#C5122F" ForeColor="White" HorizontalAlign="Left" />
             <RowStyle BackColor="#EFF3FB" CssClass="cssGridheaderfont" HorizontalAlign="Left" />
             <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                <SortedDescendingHeaderStyle BackColor="#820000" />
            </asp:GridView>


             </td></tr>



        <tr><td>  </td></tr>


 
    </table>
     </td> <td style="width:10px"> </td> </tr></table>

    
    </asp:Panel>

</td></tr></table>

</fieldset>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel2" TargetControlID="Button1" BackgroundCssClass="modelbgcolor">



    </cc1:ModalPopupExtender>
     <asp:Button ID="Button1" runat="server" Text="Button" Style="display:none" />
    <%--Style="display:none"--%>

    <asp:Panel ID="Panel2" runat="server"  >


        <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px"> </td> 
             <td >


        


              <table cellpadding="0px" cellspacing="0px" > 

            <tr> <td align="right" style="color:white">  <asp:LinkButton ID="lnkClose" runat="server" OnClick="lnkClose_Click" ForeColor="White">[X]</asp:LinkButton></td></tr>
            <tr> <td style="height:10px"> </td></tr>


            <tr> <td style="background-color:White"> 


                <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:20px">  </td><td>  

                     <table cellpadding="0px" cellspacing="0px">


             <tr> <td >
   <fieldset class="boxBody">
 <asp:Label ID="Label6" runat="server" 
            Text="View Attendance" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 </fieldset>
  
   </td></tr>


                 

    
    

                     <tr> <td style="height:13px"> </td></tr>


                     <tr> <td align="center">

                       <table cellpadding="0px" cellspacing="0px">  

                           <tr><td style="color:red ; font-size:20px"> <label> Attendance of  </label> </td> <td style="width:20px"> </td>  <td> <asp:Label ID="lblDaterange" runat="server" Font-Bold="False" Font-Size="18pt" ForeColor="#003366"></asp:Label> </td></tr>

                       </table>

                          </td></tr>


                     <tr> <td style="height:17px"> </td></tr>


                    <tr> <td>
                        <asp:Panel ID="pnlactivitydata" runat="server">
                        <table cellpadding="0px" cellspacing="0px">

                             <tr> <td>  


                              <table cellpadding="0px" cellspacing="0px"> <tr> <td style="color:red"> <label> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Working Days&nbsp;&nbsp;&nbsp;&nbsp; </label> </td> <td style="color:red"> <asp:Label ID="lblworkingdays" runat="server" Text=""></asp:Label>  </td> <td style="width:100px">  </td>  <td style="color:red"> <label > Present&nbsp;&nbsp;&nbsp;&nbsp;</label> </td> <td style="color:red"> <asp:Label ID="lblpresent" runat="server" Text=""></asp:Label>  </td><td style="width:100px">  </td>  <td style="color:red"> <label > Leave &nbsp;&nbsp;&nbsp;&nbsp; </label> </td> <td style="color:red"> <asp:Label ID="lblLeave" runat="server"></asp:Label>  </td> </tr>  </table>


                          </td></tr>

                     <tr> <td style="height:13px"> </td></tr>

  <tr> <td align="center"> 

      <asp:GridView ID="grdviewApprovalofnav" runat="server" AutoGenerateColumns="False" 
             CellPadding="4" ForeColor="#333333" GridLines="None" Width="1024px" AllowPaging="True" PageSize="6" OnPageIndexChanging="grdviewApprovalofnav_PageIndexChanging" OnRowDataBound="grdviewApprovalofnav_RowDataBound" >
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


         
               
               <%--  <asp:BoundField DataField="[Updated In Time]" HeaderText="Changed In Time" />
                 <asp:BoundField DataField="Updated Out Time" HeaderText="Changed Out Time" />
                 <asp:BoundField DataField="Update_Date" HeaderText="Changed Date" />
--%>

         
               
             </Columns>
             <EditRowStyle BackColor="#2461BF" />
                      <EmptyDataTemplate>
                               There are no records found
                      </EmptyDataTemplate>
             <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
             <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="White" CssClass="cssGridheaderfont" HorizontalAlign="Left"/>
             <PagerStyle BackColor="#C5122F" ForeColor="White" HorizontalAlign="Left" />
             <RowStyle BackColor="#EFF3FB" CssClass="cssGridheaderfont" HorizontalAlign="Left" />
             <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
             <SortedAscendingCellStyle BackColor="#F5F7FB" />
             <SortedAscendingHeaderStyle BackColor="#6D95E1" />
             <SortedDescendingCellStyle BackColor="#E9EBEF" />
             <SortedDescendingHeaderStyle BackColor="#4870BE" />
         </asp:GridView>
       </td></tr>


  <tr> <td style="height:10px"> </td></tr>

                            <tr> <td> 


                                  <asp:GridView ID="grdcardtimechanged" runat="server" AutoGenerateColumns="False" 
             CellPadding="4" ForeColor="#333333" GridLines="None" Width="1024px" AllowPaging="True" PageSize="5" OnPageIndexChanging="grdcardtimechanged_PageIndexChanging" OnRowCreated="grdcardtimechanged_RowCreated" >
             <AlternatingRowStyle BackColor="White" />
             <Columns>
              
                
                
               
              
                 <asp:BoundField DataField="Atte_Date" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" />


                     <asp:TemplateField HeaderText="Default In Time">
                     <ItemTemplate>
                         <asp:Label ID="lblintime" runat="server" Text='<%# Eval("fromTime","{0:t}") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>


                   <asp:TemplateField HeaderText="Default Out Time">
                     <ItemTemplate>
                         <asp:Label ID="lblouttime" runat="server" Text='<%# Eval("ToTime","{0:t}") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
             

                 <%-- <asp:TemplateField HeaderText="Status">
                     <ItemTemplate>
                         <asp:Label ID="lblStatusStatus" runat="server" Text='<%#Bind("Status") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>--%>


         
               
                 <asp:BoundField DataField="Updated In Time" HeaderText="Changed In Time" />
                 <asp:BoundField DataField="Updated Out Time" HeaderText="Changed Out Time" />
                 <asp:BoundField DataField="Update_Date" HeaderText="Changed Date" />


         
               
             </Columns>
             <EditRowStyle BackColor="#2461BF" />
                    <%--  <EmptyDataTemplate>
                               There are no records found
                      </EmptyDataTemplate>--%>
             <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
             <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="White" CssClass="cssGridheaderfont" HorizontalAlign="Left"/>
             <PagerStyle BackColor="#C5122F" ForeColor="White" HorizontalAlign="Left" />
             <RowStyle BackColor="#EFF3FB" CssClass="cssGridheaderfont" HorizontalAlign="Left" />
             <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
             <SortedAscendingCellStyle BackColor="#F5F7FB" />
             <SortedAscendingHeaderStyle BackColor="#6D95E1" />
             <SortedDescendingCellStyle BackColor="#E9EBEF" />
             <SortedDescendingHeaderStyle BackColor="#4870BE" />
         </asp:GridView>

                                 </td></tr>



  <tr> <td style="height:10px"> </td></tr>


          <tr> <td> 
          
          <table cellpadding="0px" cellspacing="0px"  style="width:100%"> 
              
              
              <tr> <td valign="top" style="background-color:white">   

             
                  <table cellpadding="0px" cellspacing="0px"> 
                     
                           <tr> <td style="height:5px;background-color:#C5122F"> </td></tr>
        <tr> <td  style="background-color:#C5122F ">&nbsp;&nbsp;<asp:Label ID="Label5" runat="server" 
            Text=" Current Month Status" Font-Size="14pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label> </td></tr>
         <tr> <td style="height:5px;background-color:#C5122F"> </td></tr>
        <tr> <td style="height:5px" > </td></tr>
            <tr> <td class="leftm" > </td></tr>
                     
                     
                        <tr> <td style="height:5px" > </td></tr>
                      <tr> <td>     <asp:Calendar ID="Calendar1" runat="server" BackColor="White" 
          BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" 
           DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" 
           ForeColor="Black" Height="150px" Width="330px"  OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChanged" Enabled="False">
           <DayHeaderStyle BackColor="#ffccff" ForeColor="#336666" Height="1px" />
           <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
           <OtherMonthDayStyle ForeColor="#999999" />
           <SelectedDayStyle BackColor="#C5122F" Font-Bold="True" ForeColor="#CCFF99" />
           <SelectorStyle BackColor="#C5122F" ForeColor="Black" />
           <TitleStyle BackColor="#C5122F" BorderColor="#3366CC" BorderWidth="1px" 
               Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
           <TodayDayStyle BackColor="White" ForeColor="Black" />

           <WeekendDayStyle  />
       </asp:Calendar></td></tr>
                  </table>

          
                                                           </td> <td style="width:20px;background-color:white"> </td> 
               <td style="background-color:white">

                  <table cellpadding="0px" cellspacing="0px"> 
  <tr> <td style="height:5px;"> </td></tr>
                <tr> <td style="">

        <asp:Label ID="Label4" runat="server" 
            Text=" Color represents in calender" Font-Size="14pt" ForeColor="#C5122F" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

         </td></tr>
  

                      
     <tr> <td style="height:5px"> 
         &nbsp;</td></tr>

     <tr> <td>
       <table cellpadding="0px" cellspacing="0px"><tr> <td> <div style="background-color:Gray; width:5px; color:Gray" >c </div></td> <td>  <label >&nbsp;&nbsp;&nbsp;Days Blocked</label></td></tr> </table>  

         </td></tr>
     <tr> <td style="height:6px"> 
        </td></tr>
    <tr> <td>
       <table cellpadding="0px" cellspacing="0px"><tr> <td> <div style="background-color:lightblue ; width:5px; color:lightblue" >c </div></td> <td> <label>&nbsp;&nbsp;&nbsp;Leave</label></td></tr> </table>  

         </td></tr>

     <%--<tr> <td style="height:2px"> 
        </td></tr>

    <tr> <td>
       <table cellpadding="0px" cellspacing="0px"><tr> <td> <div style="background-color:Aqua; width:5px; color:Aqua" >c </div></td> <td>  <label>&nbsp;&nbsp;Approved Half Leave</label></td></tr> </table>  

         </td></tr>--%>

 

     <tr> <td style="height:6px"> 
        </td></tr>


    <tr> <td>
       <table cellpadding="0px" cellspacing="0px"><tr> <td> <div style="background-color:lawngreen ; width:5px ; color:lawngreen" >c </div></td> <td> <label> &nbsp;&nbsp;&nbsp;Marked</label ></td></tr> </table>  

         </td></tr>

   <tr> <td style="height:6px"> 
        </td></tr>

    <tr> <td>
       <table cellpadding="0px" cellspacing="0px"><tr> <td> <div style="background-color:white ; width:5px; color:white" >c </div></td> <td> <label>&nbsp;&nbsp;&nbsp;To be Marked </label></td></tr> </table>  

         </td></tr>
    <tr> <td style="height:6px"> 
        </td></tr>

     <tr> <td>
       <table cellpadding="0px" cellspacing="0px"><tr> <td>  <div style="background-color:SandyBrown ; width:5px; color:SandyBrown" >c </div></td> <td> <label>&nbsp;&nbsp;&nbsp;Posted Attendance </label></td></tr> </table>  

         </td></tr>
    <tr> <td style="height:6px"> 
        </td></tr>

    <tr> <td style="height:12px">  

     

         </td></tr>



            </table>


                                                                                                                                                                  </td>

            

              <td style="width:20px;background-color:white""> </td> 

            
              <td valign="top" style="background-color:white"> 

                  <table cellpadding="0px" cellspacing="0px">

                      
                           <tr> <td style="height:5px;background-color:#C5122F" > </td></tr>
        <tr> <td style="background-color:#C5122F">&nbsp; &nbsp; <asp:Label ID="Label3" runat="server" 
            Text="Leave Status" Font-Size="14pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label> </td></tr>
         
         <tr> <td style="height:5px;background-color:#C5122F" > </td></tr>
            <tr> <td class="leftm"  style="height:5px" > </td></tr>

                        <tr> <td style="height:5px"> </td></tr>
                           
       <tr> <td> 
           <asp:GridView ID="grdleave" runat="server" AutoGenerateColumns="False" Width="400px">
                  <Columns>
                      <asp:BoundField DataField="Leave code" HeaderText="&nbsp; &nbsp;   Leave Code" DataFormatString="{0:F2}"/>
                      <%--<asp:BoundField HeaderText="No Of Leave" DataField="No of Leaves Credited" DataFormatString="{0:F2}"/>--%>
                      <asp:BoundField DataField="Leave Balance" HeaderText="Leave Balance" DataFormatString="{0:F2}" />
                     <%-- <asp:TemplateField HeaderText="Taken Leave"></asp:TemplateField>--%>
                     <%-- <asp:BoundField HeaderText="Taken Leave" DataField="No of Leaves Availed (Full)" DataFormatString="{0:F2}"/>--%>

                      <asp:BoundField DataField="Unapproved Leave" HeaderText="Not Approved Leave" DataFormatString="{0:F2}" />
                  </Columns>
                  <EmptyDataTemplate>
                      There is no leave detail
                  </EmptyDataTemplate>
                  <HeaderStyle HorizontalAlign="Left" BackColor="#C5122F" ForeColor="White" CssClass="cssGridheaderfont" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"/>
                      <RowStyle  CssClass="cssGridheaderfont"  Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" HorizontalAlign="Center"/>
              </asp:GridView>

            </td></tr>

                  </table>

              </td> 

                                                      </tr></table>
          </td></tr>
      <tr> <td style="height:10px"> </td></tr>

          


        
         

        <tr> <td>
            <asp:Panel ID="pnlsetupattendance" runat="server" Visible="False" BackColor="#ffffff">


                      <table cellpadding="0px" cellspacing="0px" style="width:100%"> 
                            <tr> <td class="leftm" colspan="6" style="background-color:#C5122F "> </td></tr>
                           <tr> <td style="height:5px;background-color:#C5122F" colspan="6"> </td></tr>
                           <tr> <td  colspan="6" style="background-color:#C5122F "> &nbsp;&nbsp; <asp:Label ID="Label2" runat="server" 
            Text="Attendance Status of " Font-Size="14pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>    <asp:Label ID="lblselecteddate" runat="server" Font-Size="14pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label></td></tr>


                           <tr> <td style="height:5px;background-color:#C5122F" colspan="6"> </td></tr>
                     <tr> <td style="height:5px;" colspan="6"> </td></tr>
                          
                          <tr> <td><%--&nbsp;&nbsp; Can mark attendance upto past&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  --%></td> <td>  <asp:Label ID="lblMarkupto" runat="server" Font-Bold="True" ForeColor="#006699" Visible="False"></asp:Label> </td>  <td style="width:100px">  </td>  <td> <%--In Time / Out Time  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; --%> </td> <td style="width:10px">  </td> <td>  <asp:Label ID="lblfromTimetotime" runat="server" Font-Bold="True" ForeColor="#006699" Visible="False"></asp:Label></td></tr>



            <tr> <td colspan="6" style="height:5px"> </td> </tr>

              <tr> <td>&nbsp;&nbsp; In Time  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  </td> <td>  <asp:Label ID="lblFromTime" runat="server" Font-Bold="True" ForeColor="#006699"></asp:Label> </td>  <td style="width:100px">  </td>  <td> Out Time &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  </td> <td style="width:10px">  </td> <td>  <asp:Label ID="lbltoTime" runat="server" Font-Bold="True" ForeColor="#006699"></asp:Label></td></tr>
                <tr> <td colspan="6" style="height:5px"> </td> </tr>

                              <tr> <td colspan="6">   <table cellpadding="0px" cellspacing="0px"> <tr> <td> &nbsp;&nbsp; Remarks</td> <td style="width:120px"> </td>  <td>  <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="700px" Enabled="False"></asp:TextBox></td></tr>  </table></td> </tr>

                   </table> 


            </asp:Panel>
         </td></tr>


                        </table>
                        </asp:Panel>
                         </td></tr>



        </table>


                                                                                               </td><td style="width:20px"></td></tr>  </table>

                
                 </td></tr>

        </table>








                                                                                        </td> 
            
            
            <td style="width:10px"> </td> </tr> </table>

      



         
    </asp:Panel>
</asp:Content>

