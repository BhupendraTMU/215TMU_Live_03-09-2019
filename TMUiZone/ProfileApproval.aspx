<%@ Page Title="" Language="C#" MasterPageFile="~/IndexMaster.master" AutoEventWireup="true" CodeFile="ProfileApproval.aspx.cs" Inherits="LeaveApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
        function Confirm_Approva() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Approve this record?")) {
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
    <fieldset class="boxBody">
 <asp:Label ID="Label1" runat="server" 
            Text="Profile Approval Detail" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 </fieldset>


    <fieldset class="boxBody"> 

<table cellpadding="0px" cellspacing="0px">  <tr>  <td  style="width:10px">  </td>
<td  style="width:200px" valign="top"> 

<table cellpadding="0px" cellspacing="0px" class="leftbg1" style="width:200px; height:430px">  <tr> <td style="width:10px"> </td> <td>


<table cellpadding="0px" cellspacing="0px" >
 <tr> <td style="height:10px"> </td></tr>
 <tr> <td class="leftmMenu">   <img src="logo/Star.png" />
    <asp:LinkButton ID="lnkProfileview" runat="server" onclick="lnkleaveview_Click" 
        >Pending Profile</asp:LinkButton></td></tr>
    <tr> <td style="height:10px"> </td></tr>
     <tr> <td class="leftmMenu"> <img src="logo/Star.png" />  
    <asp:LinkButton ID="lnkRejectProfileDetail" runat="server" 
             onclick="lnkRejectLeaveDetail_Click" >Rejected Profile</asp:LinkButton></td></tr>
     <tr> <td style="height:10px"> </td></tr>
     <tr> <td class="leftmMenu">  <img src="logo/Star.png" /> 
    <asp:LinkButton ID="lnkApprovedProfiledetail" runat="server" 
             onclick="lnkApprovedApproveddetail_Click" >Approved Profile</asp:LinkButton></td></tr>
     <tr> <td style="height:10px"> </td></tr>

    </table>
 </td> <td style="width:10px"> </td></tr></table>



    
</td>  <td style="width:30px">  </td>
 <td valign="top"> 
 
 
    <asp:Panel ID="pnlProfileView" runat="server" CssClass="leftBackground" 
         Visible="false">

    <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px"> </td> <td>
    
    <table cellpadding="0px" cellspacing="0px">  <tr> <td > 
    
   
    <br />
    <asp:Label ID="Label2" runat="server" 
            Text="Pending Profile" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
          
  </td></tr>
    <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>
    <tr> <td style="height:13px"> </td></tr>
    <tr> <td>  
    

    <table cellpadding="0px" cellspacing="0px"> <tr> <td> <label>Select Records by </label> </td> <td style="width:90px"> </td> <td> 
        <asp:RadioButton ID="rdEmployeeID" runat="server" Text="Employee Id" 
            GroupName="pA" oncheckedchanged="rdEmployeeID_CheckedChanged" 
            AutoPostBack="True"/> </td> <td style="width:30px"> </td> <td> 
        <asp:RadioButton ID="rdEmployeeName" runat="server" Text="Employee Name" 
            GroupName="pA" oncheckedchanged="rdEmployeeName_CheckedChanged" 
            AutoPostBack="True"/> </td> <td style="width:30px"> </td> <td> 
        <asp:RadioButton ID="rdDatewise" runat="server" Text="Date Wise " 
            GroupName="pA" oncheckedchanged="rdDatewise_CheckedChanged" 
            AutoPostBack="True"/> </td><td style="width:30px"> </td> <td> 
        <asp:RadioButton ID="CHKAllPending" runat="server" Text="All" 
            GroupName="pA" 
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
                   <asp:TextBox ID="txtfromDate" runat="server"></asp:TextBox>
                   
                     <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfromDate" Format="yyyy-MM-dd">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtfromDate">
      </cc1:TextBoxWatermarkExtender>

                   </td>  <td style="width:30px"> </td> <td>To Date </td>  <td style=" width:10px"> </td><td>
                       <asp:TextBox ID="txtTodate" runat="server"></asp:TextBox> 
                       
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTodate" Format="yyyy-MM-dd">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtTodate">
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
          <tr> <td > 
              <asp:Button ID="btnApprove" runat="server" Text="Approve" 
                  Visible="False" onclick="btnApprove_Click" CssClass="btnLogin" OnClientClick="Confirm_Approva()"  />   <asp:Button ID="btnreject" runat="server" Text="Reject" Visible="False" 
                  onclick="btnreject_Click" CssClass="btnLogin" />  </td>
             
           

               </tr>

              <tr> <td style="height:10px" colspan="3"> </td></tr>

          <tr> <td colspan="3">

             


         
             

              

       
             <div id="GridScrollProfile">
 

                  <asp:Button ID="Button1" runat="server" Text="Button" Style="display:none" />
            <cc1:ModalPopupExtender ID="mdaprovalchangestatus" runat="server"  PopupControlID="pnlchangestatus1" TargetControlID="Button1" BackgroundCssClass="modelbgcolor">



            </cc1:ModalPopupExtender>
            
             <table cellpadding="0px" cellspacing="0px"><tr><td align="right">    </td></tr>

                  <tr> <td >  

                       <asp:Panel ID="pnlchangestatus1" runat="server" Style="display: none;height:550px;overflow:scroll;" CssClass="modelpanelcolor">

                      <%--<fieldset class="boxBody"> --%>

                          <table cellpadding="0px" cellspacing="0px">  <tr> <td style="vertical-align:top">  </td> <td > 


                            <table cellpadding="0px" cellspacing="0px"> <tr> <td style="height:30px" >

                               <fieldset class="boxBodyHeader"> 
                                   <table cellpadding="0px" cellspacing="0px" style="width:100%"> <tr> <td>
 <asp:Label ID="Label7" runat="server" 
            Text="Personal Information Change Status" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

  </td> <td align="right">   <asp:ImageButton ID="IMGClose" runat="server" ImageUrl="~/logo/close.png" Width="15px" />
</td></tr></table>

 </fieldset>


                                                                             </td></tr>


<tr> <td style="height:30px">

                               <fieldset> 
 <asp:Label ID="lblchangestatusmodel" runat="server" 
            Text="Personal Information Change Status" Font-Size="12pt" ForeColor="Red" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 
 </fieldset>


                                                                             </td></tr>


<tr><td>

     <table cellpadding="0px" cellspacing="0px">
             <tr style="background-color:#E0F2F7; height:20px"> <td style="height:20px" class="cssheadermenuformodel">  Old Address</td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td> <td style="height:20px" class="cssheadermenuformodel"><label>   New Address</label></td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td> <div class="cssInnermenuformodel">  <asp:Label ID="lblOldAddress" runat="server" Text=""></asp:Label>  </div></td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td> <div class="cssInnermenuformodel">  <asp:Label ID="lblNewAddress" runat="server" Text=""></asp:Label></div></td> </tr>
             <tr> <td colspan="5"> </td> </tr>
              <tr style="background-color:#E0F2F7; height:20px"> <td style="height:20px" class="cssheadermenuformodel"><fieldset> Old Address2 </fieldset> </td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td> <td style="height:20px" class="cssheadermenuformodel"><fieldset> New Address2  </fieldset></td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td>  <div class="cssInnermenuformodel"> <asp:Label ID="lblOldAddress1" runat="server" Text=""></asp:Label></div> </td> <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td><div class="cssInnermenuformodel">   <asp:Label ID="lblNewAddress1" runat="server" Text=""></asp:Label> </div></td> </tr>


               <tr> <td colspan="5"> </td> </tr>
              <tr style=" background-color:#E0F2F7;height:20px"> <td style="height:20px" class="cssheadermenuformodel"> Old City </td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td> <td style="height:20px" class="cssheadermenuformodel"> New City</td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td> <div class="cssInnermenuformodel"> <asp:Label ID="lblOldCity" runat="server" Text=""></asp:Label> </div></td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td> <div class="cssInnermenuformodel">  <asp:Label ID="lblNewCity" runat="server" Text=""></asp:Label> </div></td> </tr>


               <tr> <td colspan="5"> </td> </tr>
              <tr style=" background-color:#E0F2F7;height:20px"> <td style="height:20px" class="cssheadermenuformodel"> Old State </td> <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td> <td style="height:20px" class="cssheadermenuformodel"> New State</td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td> <div class="cssInnermenuformodel"> <asp:Label ID="lblOldState" runat="server" Text=""></asp:Label></div> </td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td> <div class="cssInnermenuformodel">  <asp:Label ID="lblNewState" runat="server" Text=""></asp:Label> </div></td> </tr>

               <tr> <td colspan="5"> </td> </tr>
              <tr style="background-color:#E0F2F7;height:20px"> <td style="height:20px" class="cssheadermenuformodel"> Old Country </td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td style="height:20px" class="cssheadermenuformodel"> New Country</td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td><div class="cssInnermenuformodel">  <asp:Label ID="lblOldCountry" runat="server" Text=""></asp:Label> </div></td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td> <div class="cssInnermenuformodel">  <asp:Label ID="lblNewCountry" runat="server" Text=""></asp:Label> </div></td> </tr>

               <tr> <td colspan="5"> </td> </tr>
              <tr style="background-color:#E0F2F7; height:20px"> <td style="height:20px" class="cssheadermenuformodel"> Old PinCode </td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td style="height:20px" class="cssheadermenuformodel"> New PinCode</td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td><div class="cssInnermenuformodel">  <asp:Label ID="lblOldPincode" runat="server" Text=""></asp:Label></div> </td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td> <div class="cssInnermenuformodel">  <asp:Label ID="lblNewPincode" runat="server" Text=""></asp:Label> </div></td> </tr>

              <tr> <td colspan="5"> </td> </tr>
              <tr style=" background-color:#E0F2F7;height:20px"> <td style="height:20px" class="cssheadermenuformodel"> Old Email ID </td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td style="height:20px" class="cssheadermenuformodel"> New Email ID</td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td><div class="cssInnermenuformodel">  <asp:Label ID="lblOldEmailID" runat="server" Text=""></asp:Label></div> </td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td> <td> <div class="cssInnermenuformodel">  <asp:Label ID="lblNewEmailID" runat="server" Text=""></asp:Label> </div></td> </tr>
 
             <tr> <td colspan="5"> </td> </tr>
              <tr style="background-color:#E0F2F7;height:20px"> <td style="height:20px" class="cssheadermenuformodel"> Old Date Of Birth </td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td style="height:20px" class="cssheadermenuformodel"> New Date Of Birth</td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td><div class="cssInnermenuformodel">  <asp:Label ID="lblOldDateOfBirth" runat="server" Text=""></asp:Label></div> </td> <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td> <td> <div class="cssInnermenuformodel">  <asp:Label ID="lblNewDateofBirth" runat="server" Text=""></asp:Label> </div></td> </tr>
 
              <tr> <td colspan="5"> </td> </tr>
              <tr style="background-color:#E0F2F7; height:20px"> <td style="height:20px" class="cssheadermenuformodel"> Old Father Name </td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td style="height:20px" class="cssheadermenuformodel"> New Father Name</td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td><div class="cssInnermenuformodel">  <asp:Label ID="lblOldFatherName" runat="server" Text=""></asp:Label></div> </td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td> <div class="cssInnermenuformodel">  <asp:Label ID="lblNewFatherName" runat="server" Text=""></asp:Label> </div></td> </tr>
 
             <tr> <td colspan="5"> </td> </tr>
              <tr style="background-color:#E0F2F7;height:20px"> <td style="height:20px" class="cssheadermenuformodel"> Old Husband Name </td> <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td style="height:20px" class="cssheadermenuformodel"> New Husband Name</td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td><div class="cssInnermenuformodel">  <asp:Label ID="lblOldHusbandName" runat="server" Text=""></asp:Label> </div></td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td><div class="cssInnermenuformodel">   <asp:Label ID="lblNewHusbandName" runat="server" Text=""></asp:Label></div></td> </tr>
 

              <tr> <td colspan="5"> </td> </tr>
              <tr style="background-color:#E0F2F7;height:20px"> <td style="height:20px" class="cssheadermenuformodel"> Old Mother Name </td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td style="height:20px" class="cssheadermenuformodel"> New Mother Name</td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td><div class="cssInnermenuformodel">  <asp:Label ID="lblOldMotherName" runat="server" Text=""></asp:Label> </div></td> <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td> <div class="cssInnermenuformodel">  <asp:Label ID="lblNewMotherName" runat="server" Text=""></asp:Label> </div></td> </tr>
 

              <tr> <td colspan="5"> </td> </tr>
              <tr style="background-color:#E0F2F7;height:20px"> <td style="height:20px" class="cssheadermenuformodel"> Old Marital Status </td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td> <td style="height:20px" class="cssheadermenuformodel"> New Marital Status</td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td> <div class="cssInnermenuformodel"> <asp:Label ID="lblOldMaritalStatus" runat="server" Text=""></asp:Label> </div></td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td><div class="cssInnermenuformodel">   <asp:Label ID="lblNewMaritalStatus" runat="server" Text=""></asp:Label> </div></td> </tr>
 

              <tr> <td colspan="5"> </td> </tr>
              <tr style="background-color:#E0F2F7;height:20px"> <td style="height:20px" class="cssheadermenuformodel"> Old Contact No </td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td> <td style="height:20px" class="cssheadermenuformodel"> New  Contact No</td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td><div class="cssInnermenuformodel">  <asp:Label ID="lblOLDContactNo" runat="server" Text=""></asp:Label> </div></td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td> <div class="cssInnermenuformodel">  <asp:Label ID="lblNewContactNo" runat="server" Text=""></asp:Label> </div></td> </tr>
 

               <tr> <td colspan="5"> </td> </tr>
              <tr style="background-color:#E0F2F7;height:20px"> <td style="height:20px" class="cssheadermenuformodel"> Old Phone No</td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td> <td style="height:20px" class="cssheadermenuformodel"> New  Phone No</td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td><div class="cssInnermenuformodel">  <asp:Label ID="lblOldPhoneNo" runat="server" Text=""></asp:Label></div> </td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td><div class="cssInnermenuformodel">   <asp:Label ID="lblNewPhoneNo" runat="server" Text=""></asp:Label> </div></td> </tr>
 

              <tr> <td colspan="5" class="cssheadermenuformodel">  <h2>Permanent Address </h2></td> </tr>

                <tr> <td colspan="5"> </td> </tr>
              <tr style="background-color:#E0F2F7;height:20px"> <td style="height:20px" class="cssheadermenuformodel"> Old Address</td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td> <td style="height:20px" class="cssheadermenuformodel"> New Address</td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td><div class="cssInnermenuformodel">  <asp:Label ID="lblOldAddressPERMAnent" runat="server" Text=""></asp:Label> </div></td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td><div class="cssInnermenuformodel">   <asp:Label ID="lblNewAddressPERMAnent" runat="server" Text=""></asp:Label> </div></td> </tr>
 
                 <tr> <td colspan="5"> </td> </tr>
              <tr style="background-color:#E0F2F7;height:20px"> <td style="height:20px" class="cssheadermenuformodel"> Old Address2</td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td> <td style="height:20px" class="cssheadermenuformodel"> New Address2</td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td> <div class="cssInnermenuformodel"> <asp:Label ID="lblOldAddressPERMAnent2" runat="server" Text=""></asp:Label> </div> </td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td> <div class="cssInnermenuformodel">  <asp:Label ID="lblNewAddressPERMAnent2" runat="server" Text=""></asp:Label> </div></td> </tr>
 
              <tr> <td colspan="5"> </td> </tr>
              <tr style="background-color:#E0F2F7;height:20px"> <td style="height:20px" class="cssheadermenuformodel"> Old City</td> <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td> <td style="height:20px" class="cssheadermenuformodel"> New City</td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td><div class="cssInnermenuformodel">  <asp:Label ID="lblOldCitypermanet" runat="server" Text=""></asp:Label> </div> </td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td><div class="cssInnermenuformodel">   <asp:Label ID="lblNewCitypermanet" runat="server" Text=""></asp:Label> </div> </td> </tr>
 
             <tr> <td colspan="5"> </td> </tr>
              <tr style="background-color:#E0F2F7;height:20px"> <td style="height:20px" class="cssheadermenuformodel"> Old State</td> <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td style="height:20px" class="cssheadermenuformodel"> New State</td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td> <div class="cssInnermenuformodel"> <asp:Label ID="lblOldStatePermanet" runat="server" Text=""></asp:Label> </div> </td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td> <div class="cssInnermenuformodel">  <asp:Label ID="lblNewStatePermanet" runat="server" Text=""></asp:Label> </div></td> </tr>
 
               <tr> <td colspan="5"> </td> </tr>
              <tr style="background-color:#E0F2F7;height:20px"> <td style="height:20px" class="cssheadermenuformodel"> Old Country</td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td style="height:20px" class="cssheadermenuformodel"> New Country</td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td><div class="cssInnermenuformodel">  <asp:Label ID="lblOldCountryPermanet" runat="server" Text=""></asp:Label> </div> </td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td><div class="cssInnermenuformodel">   <asp:Label ID="lblNewCountryPermanet" runat="server" Text=""></asp:Label> </div></td> </tr>
 

             <tr> <td colspan="5"> </td> </tr>
              <tr style=" background-color:#E0F2F7;height:20px"> <td style="height:20px" class="cssheadermenuformodel"> Old Pin Code</td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td style="height:20px" class="cssheadermenuformodel"> New Pin Code</td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td> <div class="cssInnermenuformodel"> <asp:Label ID="lblOldPincodepermanet" runat="server" Text=""></asp:Label> </div></td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td> <div class="cssInnermenuformodel">  <asp:Label ID="lblnewPincodepermanet" runat="server" Text=""></asp:Label> </div></td> </tr>
 
              <tr> <td colspan="3"> </td> </tr>
              <tr style="background-color:#E0F2F7;height:20px"> <td style="height:20px" class="cssheadermenuformodel"> Old PAN No</td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td style="height:20px" class="cssheadermenuformodel"> New PAN No</td> </tr>
             <tr> <td colspan="3"> </td> </tr>
             <tr> <td><div class="cssInnermenuformodel">  <asp:Label ID="lblOldPanNo" runat="server" Text=""></asp:Label></div> </td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td> <div class="cssInnermenuformodel">  <asp:Label ID="lblNewPanNo" runat="server" Text=""></asp:Label> </div></td> </tr>
 

              <tr> <td colspan="5"> </td> </tr>
              <tr style="background-color:#E0F2F7;height:20px"> <td style="height:20px" class="cssheadermenuformodel"> Old A/C No</td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td style="height:20px" class="cssheadermenuformodel"> New A/C No</td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td> <div class="cssInnermenuformodel"> <asp:Label ID="lblOldACNo" runat="server" Text=""></asp:Label> </div> </td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td><div class="cssInnermenuformodel">   <asp:Label ID="lblNewACNO" runat="server" Text=""></asp:Label> </div></td> </tr>
 


         </table>

    </td></tr>

                            </table>

                               


                                                                                                                   </td> <td style="vertical-align:top">

                                                                                                                  

                                                                                                                         </td></tr></table>

       

     </asp:Panel>

                       <asp:Button ID="btnselectchecked" runat="server" OnClick="btnselectchecked_Click" Text="Select All" Visible="False" />
                       <asp:Button ID="btnuncheked" runat="server" OnClick="btnuncheked_Click" Text="UN Select" Visible="False" />

                       </td></tr>

              </table> 





              <asp:GridView ID="grdViewApproval" runat="server" AutoGenerateColumns="False" 
                  CellPadding="4" Width="1200px" ForeColor="#333333" GridLines="None" 
                     AllowPaging="True" onpageindexchanging="grdViewApproval_PageIndexChanging" 
                     PageSize="12" 
                     onselectedindexchanged="grdViewApproval_SelectedIndexChanged" OnRowCommand="grdViewApproval_RowCommand">
                  <AlternatingRowStyle BackColor="White" />
                  <Columns>
                      <asp:TemplateField HeaderText="Select Userid">
                          <ItemTemplate>
                              <asp:CheckBox ID="chkMark" runat="server"  Text='<%#Bind("UserID") %>'/>
                          </ItemTemplate>
                      </asp:TemplateField>

                       <asp:BoundField DataField="title" HeaderText="Title" />
                      <asp:BoundField DataField="Fname" HeaderText="First Name" />
                      <asp:BoundField DataField="SName" HeaderText="Second Name" />
                      <asp:BoundField DataField="LName" HeaderText="Last Name" />

                      <asp:TemplateField>
                          <ItemTemplate>
                              <asp:Button ID="btnchangestatus" runat="server" Text="Change Status" CommandArgument='<%#Bind("UserID") %>' OnCommand="btnchangestatus_Command" />
                             
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Profile Changed Date">
                          <ItemTemplate>
                              <asp:Label ID="lblProfilechangedate" runat="server" Text='<%#Bind("ProfileUpdateDate") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                       <asp:TemplateField HeaderText="Approval Status">
                          <ItemTemplate>
                              <asp:Label ID="lblApprovalSTatus" runat="server" Text='<%#Bind("ApprovalStatus") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>


                     <%-- <asp:BoundField DataField="CountStatusHR" HeaderText="HR Approval Status" />
                      <asp:BoundField DataField="CountStatusHOD" HeaderText="HOD Approval Status" />
--%>

                      <asp:TemplateField HeaderText="No Of Changes" Visible="False">
                          <ItemTemplate>
                              <asp:Label ID="lblNoofchange" runat="server" Text='<%#Bind("id") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>


                     
                      <asp:BoundField DataField="Phone_No" HeaderText="Phone No" Visible="False" />
                      <asp:BoundField DataField="Mobile_No" HeaderText="Mobile No" Visible="False" />
                      <asp:BoundField DataField="DOB" HeaderText="DOB" />
                      <asp:BoundField DataField="FatherName" HeaderText="Father Name" Visible="False" />
                      <asp:BoundField DataField="HusbandName" HeaderText="Husband Name" Visible="False" />
                     <%-- <asp:BoundField DataField="ApprovalStatus" HeaderText="Approval Status" />--%>
                     <%-- <asp:BoundField DataField="ProfileUpdateDate" HeaderText="Profile UpdateDate" />--%>
                      <asp:BoundField DataField="CompanyName" HeaderText="Company Name" Visible="false" />
                  </Columns>
                  <EditRowStyle BackColor="#2461BF" />
                  <EmptyDataTemplate>
                      There is no record found..............
                  </EmptyDataTemplate>
                  <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                  <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="White" 
                      HorizontalAlign="Left" Font-Size="8pt" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"/>
                  <PagerStyle BackColor="#C5122F" ForeColor="White" HorizontalAlign="Left" />
                  <RowStyle BackColor="#EFF3FB" Font-Size="8pt" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"/>
                  <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                  <SortedAscendingCellStyle BackColor="#F5F7FB" />
                  <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                  <SortedDescendingCellStyle BackColor="#E9EBEF" />
                  <SortedDescendingHeaderStyle BackColor="#4870BE" />
              </asp:GridView> </div>
          
           </td></tr>
          
           </table>

                
       
          </td></tr>
   
    </table>
     </td> <td style="width:10px"> </td> </tr></table>

    
    </asp:Panel>



      <asp:Panel ID="pnlProfileRejected" runat="server" CssClass="leftBackground" 
         Visible="false">

    <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px"> </td> <td>
    
    <table cellpadding="0px" cellspacing="0px">  <tr> <td > 
    
   
    <br />
    <asp:Label ID="Label3" runat="server" 
            Text="Rejected Profile" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
           
  </td></tr>
    <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>
    <tr> <td style="height:13px"> </td></tr>
    <tr> <td>  
    

    <table cellpadding="0px" cellspacing="0px"> <tr> <td> <label>Select Records by</label> </td> <td style="width:90px"> </td> <td> 
        <asp:RadioButton ID="rdProfileRectedEMPID" runat="server" Text="Employee Id" 
            GroupName="pAR" 
            AutoPostBack="True" oncheckedchanged="rdLeaveRectedEMPID_CheckedChanged"/> </td> <td style="width:30px"> </td> <td> 
        <asp:RadioButton ID="rdrdProfileRectedName" runat="server" Text="Employee Name" 
            GroupName="pAR" 
            AutoPostBack="True" oncheckedchanged="rdrdLeaveRectedName_CheckedChanged"/> </td> <td style="width:30px"> </td> <td> 
        <asp:RadioButton ID="rdProfileRectedDatewise" runat="server" Text="Date Wise " 
            GroupName="pAR" 
            AutoPostBack="True" 
            oncheckedchanged="rdLeaveRectedDatewise_CheckedChanged"/> </td> <td style="width:30px"> </td> <td> 
        <asp:RadioButton ID="rdAllReject" runat="server" Text="All" 
            GroupName="pAR" 
            AutoPostBack="True" OnCheckedChanged="rdAllReject_CheckedChanged" 
           /> </td>
        </tr></table>

   
        
        
        
         </td></tr>
         <tr> <td style="height:13px"> </td></tr>
         <tr> <td class="leftm"> </td></tr>
          <tr> <td style="height:20px"> </td></tr>

          <tr> <td >  <table cellpadding="0px" cellspacing="0px"> <tr> <td>  
              <asp:Panel ID="pnlLeaveRejectedEMPID" runat="server" Visible="false">

               <table cellpadding="0px" cellspacing="0px" > <tr> <td> <label>Enter Employee Id/ Name </label> </td> <td style="width:110px"> </td><td> 
                   <asp:TextBox ID="txtRejectedSearch" runat="server" Width="200px"></asp:TextBox></td> </tr></table>

              </asp:Panel>

                 <asp:Panel ID="pnlLeaveRejectedDatewise" runat="server" Visible="false">

               <table cellpadding="0px" cellspacing="0px" > <tr> <td> <label>From Date</label> </td> <td style="width:10px"> </td><td> 
                   <asp:TextBox ID="txtRejectedFromDate" runat="server"></asp:TextBox>
                   
                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtRejectedFromDate" Format="yyyy-MM-dd">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtRejectedFromDate">
      </cc1:TextBoxWatermarkExtender>

                   </td>  <td style="width:30px"> </td> <td>To Date </td>  <td style=" width:10px"> </td><td>
                       <asp:TextBox ID="txtRejectedToDate" runat="server"></asp:TextBox> 
                       
                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtRejectedToDate" Format="yyyy-MM-dd">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtRejectedToDate">
      </cc1:TextBoxWatermarkExtender>
                       </td>  </tr></table>

              </asp:Panel>

          </td>   <td style="width:40px"> </td> <td>  
              <asp:Button ID="btnrejectedsearch" runat="server" Text="Search" CssClass="btnLogin" 
                  Visible="False" onclick="btnrejectedsearch_Click"/></td></tr></table> </td></tr>

                  <tr> <td style="height:13px"> </td></tr>


                  <tr> <td>
                  <table cellpadding="0px" cellspacing="0px"> 
                  

                  <tr> <td> 
                  <div id="grdRejectedwrap">  
                    <asp:GridView ID="grdRejected" runat="server" AutoGenerateColumns="False" 
                  CellPadding="4" Width="1500px" ForeColor="#333333" GridLines="None" 
                          AllowPaging="True" onpageindexchanging="grdRejected_PageIndexChanging" OnRowCommand="grdRejected_RowCommand">
                  <AlternatingRowStyle BackColor="White" />
                  <Columns>
                      <asp:TemplateField HeaderText="Userid">
                          <ItemTemplate>
                              <asp:Label ID="Label6" runat="server" Text='<%#Bind("UserID") %>'></asp:Label>
                              
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField>
                          <ItemTemplate>
                              <asp:Button ID="btnchangestusrejected" runat="server" Text="Change Status" CommandArgument='<%#Bind("UserID") %>' OnCommand="btnchangestusrejected_Command" />
                              <asp:Label ID="lblNoofchange" runat="server" Text='<%#Bind("id") %>' Visible="false"></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Profile Image">
                          <ItemTemplate>
                              <asp:Image ID="imgProfile" runat="server" Height="22px" Width="20px" ImageUrl='<%# "~/ProfileImage/"+ Eval("Photo1") %>' />
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Profile Changed Date">
                          <ItemTemplate>
                              <asp:Label ID="lblProfilechangedate" runat="server" Text='<%#Bind("ProfileUpdateDate") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                       <%-- <asp:BoundField DataField="Approval_HOD_Date" HeaderText="Rejected HOD Date" />
                      <asp:BoundField DataField="Approval_HR_Date" HeaderText="Rejected HR Date" />
                      <asp:BoundField DataField="Rejected_User_Date" HeaderText="Rejected User Date" />--%>
                      <asp:BoundField DataField="RejectedByUserRemarks" HeaderText="Rejected Remarks" />
                     <%-- <asp:BoundField DataField="RejectedByHRRemarks" HeaderText="HR Remarks" />
                      <asp:BoundField DataField="RejectedByHODRemarks" HeaderText="HOD Remarks" />--%>
                      <asp:BoundField DataField="title" HeaderText="Title" />
                      <asp:BoundField DataField="Fname" HeaderText="First Name" />
                      <asp:BoundField DataField="SName" HeaderText="Second Name" />
                      <asp:BoundField DataField="LName" HeaderText="Last Name" />
                      <asp:BoundField DataField="Address" HeaderText="Address" Visible="False" />
                      <asp:BoundField DataField="Address2" HeaderText="Address 2" Visible="False" />
                      <asp:BoundField DataField="City" HeaderText="City" Visible="False" />
                      <asp:BoundField DataField="State" HeaderText="State" Visible="False" />
                      <asp:BoundField DataField="Country" HeaderText="Country" Visible="False" />
                      <asp:BoundField DataField="Pin_Code" HeaderText="Pin Code" Visible="False" />
                      <asp:BoundField DataField="Email_ID" HeaderText="Email" Visible="False" />
                      <asp:BoundField DataField="Sex" HeaderText="Sex" Visible="False" />
                      <asp:BoundField DataField="Marital_Status" HeaderText="Marital Status" Visible="False" />
                      <asp:BoundField DataField="Phone_No" HeaderText="Phone No" Visible="False" />
                      <asp:BoundField DataField="Mobile_No" HeaderText="Mobile No" Visible="False" />
                      <asp:BoundField DataField="DOB" HeaderText="DOB" Visible="False" />
                      <asp:BoundField DataField="FatherName" HeaderText="Father Name" Visible="False" />
                      <asp:BoundField DataField="HusbandName" HeaderText="Husband Name" Visible="False" />
                      <asp:BoundField DataField="MotherName" HeaderText="Mother Name" Visible="False" />
                      <asp:BoundField DataField="PAddress" HeaderText="Permanent Address" Visible="False" />
                      <asp:BoundField DataField="PAddress2" HeaderText="Permanent Address 2" Visible="False" />
                      <asp:BoundField DataField="PCity" HeaderText="Permanent City" Visible="False" />
                      <asp:BoundField DataField="PState" HeaderText="Permanent State" Visible="False" />
                      <asp:BoundField DataField="PCountry" HeaderText="Permanent Country" Visible="False" />
                      <asp:BoundField DataField="PPinCode" HeaderText="Permanent PinCode" Visible="False" />
                      <asp:BoundField DataField="PanNo" HeaderText="Pan No" Visible="False" />
                      <asp:BoundField DataField="Ac_No" HeaderText="Ac_No" Visible="False" />
                      <asp:BoundField DataField="ApprovalStatus" HeaderText="Approval Status" />
                      <asp:BoundField DataField="Rejected Approval" HeaderText="Rejected" />
                     <%-- <asp:BoundField DataField="ProfileUpdateDate" HeaderText="Profile UpdateDate" />--%>
                      <asp:BoundField DataField="CompanyName" HeaderText="Company Name" Visible="false" />
                  </Columns>
                  <EditRowStyle BackColor="#2461BF" />
                  <EmptyDataTemplate>
                      There is no record found..............
                  </EmptyDataTemplate>
                  <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                  <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="White" 
                      HorizontalAlign="Left" Font-Size="8pt" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"/>
                  <PagerStyle BackColor="#C5122F" ForeColor="White" HorizontalAlign="Left" />
                  <RowStyle BackColor="#EFF3FB" Font-Size="8pt" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"/>
                  <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                  <SortedAscendingCellStyle BackColor="#F5F7FB" />
                  <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                  <SortedDescendingCellStyle BackColor="#E9EBEF" />
                  <SortedDescendingHeaderStyle BackColor="#4870BE" />
              </asp:GridView>
                  
                  </div>
                  </td></tr>

                  </table>
                  
                   </td></tr>


    <tr> <td class="leftm"> </td></tr>

     <tr> <td style="height:13px"> </td></tr>

          <tr> <td> 
          
          <table cellpadding="0px" cellspacing="0px"> <tr> <td>  </td></tr></table>
          </td></tr>
   
    </table>
     </td> <td style="width:10px"> </td> </tr></table>

    
    </asp:Panel>



         <asp:Panel ID="pnlApprovedDetail" runat="server" CssClass="leftBackground" Visible="false">

    <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px"> </td> <td>
    
    <table cellpadding="0px" cellspacing="0px">  <tr> <td > 
    
   
    <br />
    <asp:Label ID="Label4" runat="server" 
            Text="Approved Profile" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
           
  </td></tr>
    <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>
    <tr> <td style="height:13px"> </td></tr>
    <tr> <td>  
    

    <table cellpadding="0px" cellspacing="0px"> <tr> <td> <label>Select Records by </label> </td> <td style="width:90px"> </td> <td> 
        <asp:RadioButton ID="rdApprovedEmpid" runat="server" Text="Employee Id" 
            GroupName="pAA" 
            AutoPostBack="True" oncheckedchanged="rdApprovedEmpid_CheckedChanged" /> </td> <td style="width:30px"> </td> <td> 
        <asp:RadioButton ID="rdApprovedEMPName" runat="server" Text="Employee Name" 
            GroupName="pAA" 
            AutoPostBack="True" oncheckedchanged="rdApprovedEMPName_CheckedChanged" /> </td> <td style="width:30px"> </td> <td> 
        <asp:RadioButton ID="rdApprovedDatewise" runat="server" Text="Date Wise " 
            GroupName="pAA" 
            AutoPostBack="True" oncheckedchanged="rdApprovedDatewise_CheckedChanged" 
           /> </td>
        <td style="width:30px"> </td> <td> 
        <asp:RadioButton ID="rdAllApprove" runat="server" Text="All " 
            GroupName="pAA" 
            AutoPostBack="True" OnCheckedChanged="rdAllApprove_CheckedChanged" 
           /> </td>

        </tr></table>

   
        
        
        
         </td></tr>
         <tr> <td style="height:13px"> </td></tr>
         <tr> <td class="leftm"> </td></tr>
          <tr> <td style="height:20px"> </td></tr>

          <tr> <td >  <table cellpadding="0px" cellspacing="0px"> <tr> <td>  
              <asp:Panel ID="pnlLeaveApprovedDetailEmpid" runat="server" Visible="false">

               <table cellpadding="0px" cellspacing="0px" > <tr> <td> <label>Enter Employee Id/ Name </label> </td> <td style="width:110px"> </td><td> 
                   <asp:TextBox ID="txtResolvedSearch" runat="server" Width="200px"></asp:TextBox></td> </tr></table>

              </asp:Panel>

                 <asp:Panel ID="pnlLeaveApprovedDetailDatewise" runat="server" Visible="false">

               <table cellpadding="0px" cellspacing="0px" > <tr> <td> <label>From Date</label> </td> <td style="width:10px"> </td><td> 
                   <asp:TextBox ID="txtApprovedFromDate" runat="server"></asp:TextBox>
                   
                   <cc1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtApprovedFromDate" Format="yyyy-MM-dd">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtApprovedFromDate">
      </cc1:TextBoxWatermarkExtender>
                   </td>  <td style="width:30px"> </td> <td>To Date </td>  <td style=" width:10px"> </td><td>
                       <asp:TextBox ID="txtApprovedToDate" runat="server"></asp:TextBox>
                       <cc1:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtApprovedToDate" Format="yyyy-MM-dd">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtApprovedToDate">
      </cc1:TextBoxWatermarkExtender>
                       
                        </td>  </tr></table>

              </asp:Panel>

          </td>   <td style="width:40px"> </td> <td>  
              <asp:Button ID="btnApprovedSearch" runat="server" Text="Search" CssClass="btnLogin" 
                  Visible="False" onclick="btnApprovedSearch_Click"/></td></tr></table> </td></tr>

                  <tr> <td style="height:13px"> </td></tr>


                  <tr> <td>  
                  <div id="grdResolvedwrap"> 
                   <asp:GridView ID="grdResolved" runat="server" AutoGenerateColumns="False" 
                  CellPadding="4" Width="1500px" ForeColor="#333333" GridLines="None" 
                          AllowPaging="True" onpageindexchanging="grdResolved_PageIndexChanging" OnRowCommand="grdResolved_RowCommand">
                  <AlternatingRowStyle BackColor="White" />
                  <Columns>
                     <%-- <asp:BoundField DataField="ProfileUpdateDate" HeaderText="Profile UpdateDate" />--%>
                      <asp:TemplateField HeaderText="Userid">
                          <ItemTemplate>
                              <asp:Label ID="Label6" runat="server" Text='<%#Bind("UserID") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField>
                          <ItemTemplate>
                              <asp:Button ID="btnresolved" runat="server" OnCommand="btnresolved_Command" Text="Change Status" CommandArgument='<%#Bind("UserID") %>'/>
                               <asp:Label ID="lblNoofchange" runat="server" Text='<%#Bind("id") %>' Visible="false"></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>

                       <asp:BoundField DataField="title" HeaderText="Title" />
                      <asp:BoundField DataField="Fname" HeaderText="First Name" />
                      <asp:BoundField DataField="SName" HeaderText="Second Name" />
                      <asp:BoundField DataField="LName" HeaderText="Last Name" />
                      <asp:TemplateField HeaderText="Profile Image">
                          <ItemTemplate>
                              <asp:Image ID="imgProfile" runat="server" Height="22px" ImageUrl='<%# "~/ProfileImage/"+ Eval("Photo1") %>' Width="20px" />
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Profile Changed Date">
                          <ItemTemplate>
                              <asp:Label ID="lblProfilechangedate" runat="server" Text='<%#Bind("ProfileUpdateDate") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="Approval_HOD_Date" HeaderText="Approval Date" />
                      <asp:BoundField DataField="ApprovalStatus" HeaderText="Approval Status" />
                <%--      <asp:BoundField DataField="CountStatusHOD" HeaderText="HOD Remarks" />
                      <asp:BoundField DataField="CountStatusHR" HeaderText="HR Remarks" />--%>
                     
                      <asp:BoundField DataField="Address" HeaderText="Address" Visible="False" />
                      <asp:BoundField DataField="Address2" HeaderText="Address 2" Visible="False" />
                      <asp:BoundField DataField="City" HeaderText="City" Visible="False" />
                      <asp:BoundField DataField="State" HeaderText="State" Visible="False" />
                      <asp:BoundField DataField="Country" HeaderText="Country" Visible="False" />
                      <asp:BoundField DataField="Pin_Code" HeaderText="Pin Code" Visible="False" />
                      <asp:BoundField DataField="Email_ID" HeaderText="Email" Visible="False" />
                      <asp:BoundField DataField="Sex" HeaderText="Sex" Visible="False" />
                      <asp:BoundField DataField="Marital_Status" HeaderText="Marital Status" Visible="False" />
                      <asp:BoundField DataField="Phone_No" HeaderText="Phone No" Visible="False" />
                      <asp:BoundField DataField="Mobile_No" HeaderText="Mobile No" Visible="False" />
                      <asp:BoundField DataField="DOB" HeaderText="DOB" Visible="False" />
                      <asp:BoundField DataField="FatherName" HeaderText="Father Name" Visible="False" />
                      <asp:BoundField DataField="HusbandName" HeaderText="Husband Name" Visible="False" />
                      <asp:BoundField DataField="MotherName" HeaderText="Mother Name" Visible="False" />
                      <asp:BoundField DataField="PAddress" HeaderText="Permanent Address" Visible="False" />
                      <asp:BoundField DataField="PAddress2" HeaderText="Permanent Address 2" Visible="False" />
                      <asp:BoundField DataField="PCity" HeaderText="Permanent City" Visible="False" />
                      <asp:BoundField DataField="PState" HeaderText="Permanent State" Visible="False" />
                      <asp:BoundField DataField="PCountry" HeaderText="Permanent Country" Visible="False" />
                      <asp:BoundField DataField="PPinCode" HeaderText="Permanent PinCode" Visible="False" />
                      <asp:BoundField DataField="PanNo" HeaderText="Pan No" Visible="False" />
                      <asp:BoundField DataField="Ac_No" HeaderText="Ac_No" Visible="False" />
                     <%-- <asp:BoundField DataField="Rejected Approval" HeaderText="Rejected" />--%>
                      <asp:BoundField DataField="CompanyName" HeaderText="Company Name" Visible="false" />
                  </Columns>
                  <EditRowStyle BackColor="#2461BF" />
                  <EmptyDataTemplate>
                      There is no record found..............
                  </EmptyDataTemplate>
                  <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                  <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="White" 
                      HorizontalAlign="Left" Font-Size="8pt" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"/>
                  <PagerStyle BackColor="#C5122F" ForeColor="White" HorizontalAlign="Left" />
                  <RowStyle BackColor="#EFF3FB" Font-Size="8pt" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"/>
                  <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                  <SortedAscendingCellStyle BackColor="#F5F7FB" />
                  <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                  <SortedDescendingCellStyle BackColor="#E9EBEF" />
                  <SortedDescendingHeaderStyle BackColor="#4870BE" />
              </asp:GridView>
              </div>
                  </td></tr>

    <tr> <td class="leftm"> </td></tr>

     <tr> <td style="height:13px"> </td></tr>

          <tr> <td> 
          
          <table cellpadding="0px" cellspacing="0px"> <tr> <td>  </td></tr></table>
          </td></tr>
   
    </table>
     </td> <td style="width:10px"> </td> </tr></table>

    
    </asp:Panel>

       <asp:Panel ID="pnlMain" runat="server" >

      <table cellpadding="0px" cellspacing="0px" style="width:100%"> <tr> <td align="center"> <br /> <br /> 
         &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;  <img src="h.jpg" />    </td></tr></table>
      

     </asp:Panel>
 

                       <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnreject" PopupControlID="pnlrejectedDetail" BackgroundCssClass="modalBackground">

                      </cc1:ModalPopupExtender>
                      <asp:Panel ID="pnlrejectedDetail" runat="server" CssClass="modalPopup" Style="display: none">
                      <table cellpadding="0px" cellspacing="0px" >
                      <tr> <td>  
                     <div class="header">
       
   <table cellpadding="0px" cellspacing="0px" style="width:400px"> <tr> <td>   <label><asp:Label ID="Label5" runat="server" 
            Text="Reject Approval Detail" Font-Size="12pt"  ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 </label>   </td> <td align="right"> 
     
     
     <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/logo/close.png" Width="25px" Height="25px" />
     </td></tr></table>
                    </div></td></tr>
                      
                      <tr> <td style="height:10px"> </td></tr>
                      <tr> <td>
                      <div class="body">
                        <table cellpadding="0px" cellspacing="0px"> <tr>  <td style="width:20px"> </td> <td><label>  Remarks </label></td> <td style="width:20px"> </td> <td>
                          <asp:TextBox ID="txtRemarksRejected" runat="server" Height="150px" TextMode="MultiLine" 
                              Width="320px"></asp:TextBox> </td> <td style="width:20px"> </td> </tr> </table>
                              </div>
                               </td></tr>
 <tr> <td style="height:10px"> </td></tr>

  <tr> <td >  
  <div class="footer" align="right">
  <asp:Button ID="btnRejectProfile1" runat="server" Text="Save" CssClass="btnLogin" 
            onclick="btnRejectProfile1_Click" />
     
      </div>
       </td></tr>
      <tr> <td style="height:10px"> </td></tr>
                       </table>
                         
                      </asp:Panel>


    
    


 </td></tr></table>


   
    

         
       
                           




</fieldset>
   

     

</asp:Content>

