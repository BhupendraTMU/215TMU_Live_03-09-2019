<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="AttendenceNew.aspx.cs" Inherits="AttendenceNew" EnableEventValidation="false"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <fieldset class="boxBody"  >
 <asp:Label ID="Label1" runat="server" 
            Text="Attendance" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 </fieldset>


    <fieldset class="boxBody"> 

<table cellpadding="0px" cellspacing="0px">
<tr>
<td  style="width:10px">  </td>
<td  style="width:200px" valign="top">
    <table cellpadding="0px" cellspacing="0px" class="leftbg1" style="width: 220px; height: 430px">
        <tr>
            <td style="width: 10px"></td>
            <td>
                <table cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td class="leftmMenu">
                            <img src="../logo/Star.png" id="img2" runat="server" />
                            <asp:LinkButton ID="lnkViewAttendance" runat="server">View Attendance </asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                </table>
            </td>
            <td style="width: 10px"></td>
        </tr>
    </table>    
</td> 
<td style="width:30px" >  </td>
 <td valign="top" style="width:100%"> 
  <asp:Panel ID="pnlviewAttendence" runat="server" CssClass="leftBackground" Visible="true" >
    <table cellpadding="0px" cellspacing="0px" width="100%">
         <tr> 
             <td style="width:10px"> </td>
        <td>    
    <table cellpadding="0px" cellspacing="0px" width="100%">
      <tr> <td >   
    <br />
    <asp:Label ID="Label3" runat="server" 
            Text="View Attendance" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
           
  </td></tr>
    <tr> <td style="height:13px"> </td></tr>
    <tr> <td class="leftm">
        <table cellpadding="0px" cellspacing="0px" style="width: 100%">

            <tr>
                <td>Month :  </td>
                <td style="width: 10px"></td>
                <td>
                    <asp:DropDownList ID="ddlMonth" runat="server" Height="29px">
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 10px"></td>
                <td>Year :  </td>
                <td style="width: 10px"></td>
                <td>
                    <asp:DropDownList ID="ddlYear" runat="server" Height="29px"></asp:DropDownList>
                </td>
                <td style="width: 10px"></td>
                <td>
                    <asp:Button ID="btnPreview" runat="server" Text="View Attendance" OnClick="btnPreview_Click" CssClass="btnLogin" />
                    &nbsp;
<asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="return PrintPanel();" Visible="False" OnClick="btnPrint_Click" CssClass="btnLogin" />
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />
         </td></tr>   
    <tr>
         <td class="leftm"> </td>
    </tr>
    <tr> <td style="height:13px"> </td></tr>
    <tr>
         <td> 
        <table cellpadding="0px" cellspacing="0px">
       <tr> <td>
            <asp:Panel ID="pnlworkingday" runat="server" Visible="false">
            <table cellpadding="0px" cellspacing="0px"> <tr> <td style="color:red"> <label> Working Days&nbsp;&nbsp;&nbsp;&nbsp; </label> </td> <td style="color:red"> <asp:Label ID="lblworkingdays" runat="server" Text=""></asp:Label>  </td> <td style="width:100px">  </td>  <td style="color:red"> <label > Present&nbsp;&nbsp;&nbsp;&nbsp;</label> </td> <td style="color:red"> <asp:Label ID="lblpresent" runat="server" Text=""></asp:Label>  </td><td style="width:100px">  </td>  <td style="color:red"> <label > Leave &nbsp;&nbsp;&nbsp;&nbsp; </label> </td> <td style="color:red"> <asp:Label ID="lblLeave" runat="server"></asp:Label>  </td> </tr>  </table>
            </asp:Panel>
            </td></tr> 
             <tr> <td> 
                 <%-- <div id="grdViewLeaveStatuswrap"> --%>
                  <asp:GridView ID="grdAttendance" runat="server" AutoGenerateColumns="False" 
             CellPadding="4" ForeColor="#333333" GridLines="None" Width="866px" AllowPaging="True" PageSize="31" OnPageIndexChanging="grdAttendance_PageIndexChanging" OnRowDataBound="grdViewdetail_RowDataBound">
             <AlternatingRowStyle BackColor="White" />
             <Columns>                 
                 <asp:BoundField DataField="Date" HeaderText="Date"  />
                 <asp:BoundField DataField="Day" HeaderText="Day"  />
                 <asp:BoundField DataField="Shift Timing" HeaderText="Shift Timing" />
                 <asp:BoundField DataField="Time In" HeaderText="Time In" />
                  <asp:BoundField DataField="Time Out" HeaderText="Time Out" />
                 <asp:BoundField DataField="Late BY" HeaderText="Late BY" />
                 <asp:BoundField DataField="Early BY" HeaderText="Early BY" />
                 <asp:BoundField DataField="Status" HeaderText="Status"/>
               
             </Columns>
             <EditRowStyle BackColor="#2461BF" />
                      <EmptyDataTemplate>
                          There are no records found
                      </EmptyDataTemplate>
             <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
             <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="White" CssClass="cssGridheaderfont" HorizontalAlign="Left"/>
             <PagerStyle BackColor="#ed7600" ForeColor="White" HorizontalAlign="Left" />
             <RowStyle BackColor="#EFF3FB"  HorizontalAlign="Left" />
             <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
             <SortedAscendingCellStyle BackColor="#F5F7FB" />
             <SortedAscendingHeaderStyle BackColor="#6D95E1" />
             <SortedDescendingCellStyle BackColor="#E9EBEF" />
             <SortedDescendingHeaderStyle BackColor="#4870BE" />
         </asp:GridView> 
                      <%--</div>--%> </td></tr>
        </table>                  
       </td>
    </tr>
     <tr> <td style="height:13px"> </td></tr>   
    </table>
     </td> <td style="width:10px"> </td>
     </tr>
    </table>    
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
 </td>

</tr>

</table>


   



</fieldset>

</asp:Content>

