<%@ Page Title="" Language="C#" MasterPageFile="~/IndexMaster.master" AutoEventWireup="true" CodeFile="viewpreviousprofile.aspx.cs" Inherits="viewpreviousprofile" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table cellpadding="0px" cellspacing="0px" style="width:100%">

        <tr> <td> 
             <fieldset class="boxBody">
 <asp:Label ID="Label1" runat="server" 
            Text="View Previous Profile Changes Detail" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 </fieldset>



             </td></tr>

        <tr> <td>

            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display:none" />
            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server"  PopupControlID="pnlchangestatus1" TargetControlID="Button1" BackgroundCssClass="modelbgcolor">



            </asp:ModalPopupExtender>
            
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

      <table cellpadding="0px" cellspacing="0px"> <%-- Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" --%>
          <tr style="background-color:#E0F2F7; height:20px"> <td style="height:20px" class="cssheadermenuformodel">  Old Profile Photo</td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td> <td style="height:20px" class="cssheadermenuformodel"><label>   New Profile Photo</label></td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td> <div class="cssInnermenuformodel">    <asp:Image ID="imgOldprofile" runat="server" Width="120px" Height="110px"/></div></td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td> <div class="cssInnermenuformodel">  <asp:Image ID="imgNewProfilePhoto" runat="server" Width="120px" Height="110px"/></div></td> </tr>
            

               <tr> <td colspan="5"> </td> </tr>


   
         
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
 

             <%-- <tr> <td colspan="5"> </td> </tr>
              <tr style="background-color:#E0F2F7;height:20px"> <td style="height:20px" class="cssheadermenuformodel"> Old Mother Name </td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td style="height:20px" class="cssheadermenuformodel"> New Mother Name</td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td><div class="cssInnermenuformodel">  <asp:Label ID="lblOldMotherName" runat="server" Text=""></asp:Label> </div></td> <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td> <div class="cssInnermenuformodel">  <asp:Label ID="lblNewMotherName" runat="server" Text=""></asp:Label> </div></td> </tr>
 --%>

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
 
             <%--  <tr> <td colspan="5"> </td> </tr>
              <tr style="background-color:#E0F2F7;height:20px"> <td style="height:20px" class="cssheadermenuformodel"> Old Country</td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td style="height:20px" class="cssheadermenuformodel"> New Country</td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td><div class="cssInnermenuformodel">  <asp:Label ID="lblOldCountryPermanet" runat="server" Text=""></asp:Label> </div> </td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td><div class="cssInnermenuformodel">   <asp:Label ID="lblNewCountryPermanet" runat="server" Text=""></asp:Label> </div></td> </tr>
 --%>

             <%--<tr> <td colspan="5"> </td> </tr>
              <tr style=" background-color:#E0F2F7;height:20px"> <td style="height:20px" class="cssheadermenuformodel"> Old Pin Code</td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td style="height:20px" class="cssheadermenuformodel"> New Pin Code</td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td> <div class="cssInnermenuformodel"> <asp:Label ID="lblOldPincodepermanet" runat="server" Text=""></asp:Label> </div></td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td> <div class="cssInnermenuformodel">  <asp:Label ID="lblnewPincodepermanet" runat="server" Text=""></asp:Label> </div></td> </tr>
 --%>
              <tr> <td colspan="3"> </td> </tr>
              <tr style="background-color:#E0F2F7;height:20px"> <td style="height:20px" class="cssheadermenuformodel"> Old PAN No</td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td style="height:20px" class="cssheadermenuformodel"> New PAN No</td> </tr>
             <tr> <td colspan="3"> </td> </tr>
             <tr> <td><div class="cssInnermenuformodel">  <asp:Label ID="lblOldPanNo" runat="server" Text=""></asp:Label></div> </td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td> <div class="cssInnermenuformodel">  <asp:Label ID="lblNewPanNo" runat="server" Text=""></asp:Label> </div></td> </tr>
 

              <tr> <td colspan="5"> </td> </tr>
              <tr style="background-color:#E0F2F7;height:20px"> <td style="height:20px" class="cssheadermenuformodel"> Old A/C No</td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td style="height:20px" class="cssheadermenuformodel"> New A/C No</td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td> <div class="cssInnermenuformodel"> <asp:Label ID="lblOldACNo" runat="server" Text=""></asp:Label> </div> </td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td><div class="cssInnermenuformodel">   <asp:Label ID="lblNewACNO" runat="server" Text=""></asp:Label> </div></td> </tr>
 


         </table>


     <%--<table cellpadding="0px" cellspacing="0px">


         <tr style="background-color:#E0F2F7; height:20px"> <td style="height:20px" class="cssheadermenuformodel">  Old Profile Photo</td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td> <td style="height:20px" class="cssheadermenuformodel"><label>   New Profile Photo</label></td> </tr>
             <tr> <td colspan="5"> </td> </tr>
             <tr> <td> <div class="cssInnermenuformodel">    <asp:Image ID="imgOldprofile" runat="server" Width="120px" Height="110px"/></div></td>  <td style="width:10px"> </td> <td style="width:1px;background-color :black" > </td><td style="width:10px"> </td>  <td> <div class="cssInnermenuformodel">  <asp:Image ID="imgNewProfilePhoto" runat="server" Width="120px" Height="110px"/></div></td> </tr>
            

               <tr> <td colspan="5"> </td> </tr>





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
 


         </table>--%>

    </td></tr>

                            </table>

                               


                                                                                                                   </td> <td style="vertical-align:top">

                                                                                                                  

                                                                                                                         </td></tr></table>

       

     </asp:Panel>

                       </td></tr>

              </table> 

            <asp:GridView ID="grdViewApproval" runat="server" AutoGenerateColumns="False" 
                  CellPadding="4" Width="100%" ForeColor="#333333" GridLines="None" 
                     AllowPaging="True" onpageindexchanging="grdViewApproval_PageIndexChanging" 
                     PageSize="50" 
                     onselectedindexchanged="grdViewApproval_SelectedIndexChanged" OnRowCommand="grdViewApproval_RowCommand">
                  <AlternatingRowStyle BackColor="White" />
                  <Columns>
                      <asp:TemplateField>
                          <ItemTemplate>
                              <asp:Label ID="lblNoofchange" runat="server" Text='<%#Bind("id") %>' Visible="false"></asp:Label>
                              <asp:Button ID="btnchangestatus" runat="server" Text="Change Status" OnCommand="btnchangestatus_Command" CommandArgument='<%#Bind("UserID") %>'/>
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


                      <asp:BoundField DataField="CountStatusHR" HeaderText="HR Approval Status" Visible="False" />
                      <asp:BoundField DataField="CountStatusHOD" HeaderText="HOD Approval Status" Visible="False" />


                      <asp:BoundField DataField="title" HeaderText="Title" />
                      <asp:BoundField DataField="Fname" HeaderText="First Name" />
                      <asp:BoundField DataField="SName" HeaderText="Second Name" />
                      <asp:BoundField DataField="LName" HeaderText="Last Name" />
                     <%-- <asp:BoundField DataField="ApprovalStatus" HeaderText="Approval Status" />--%>
                     <%-- <asp:BoundField DataField="ProfileUpdateDate" HeaderText="Profile UpdateDate" />--%>
                      <asp:BoundField DataField="CompanyName" HeaderText="Company Name" Visible="False" />
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


             </td></tr>

    </table>

</asp:Content>

