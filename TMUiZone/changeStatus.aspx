<%@ Page Language="C#" AutoEventWireup="true" CodeFile="changeStatus.aspx.cs" Inherits="changeStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Status</title>
     <link href="css/mainpage.css" rel="stylesheet" type="text/css" />
    <link href="dropdown_one.css" rel="stylesheet" type="text/css" />
     <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link href="css/structure.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:200px">  </td> <td>  <table cellpadding="0px" cellspacing="0px"><tr><td align="right">    </td></tr>

                  <tr> <td >  

                       <asp:Panel ID="pnlchangestatus1" runat="server" Style="" CssClass="modelpanelcolor">

                   
                          <table cellpadding="0px" cellspacing="0px">  <tr> <td style="vertical-align:top">  </td> <td > 


                            <table cellpadding="0px" cellspacing="0px" style="height:300px;"> <tr> <td style="height:30px" >

                               <fieldset class="boxBodyHeader"> 
 <asp:Label ID="Label7" runat="server" 
            Text="Personal Information Change Status" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>  of  <asp:Label ID="lblID" runat="server" Text="" ForeColor="Red"></asp:Label>

 
 </fieldset>


                                                                             </td></tr>


<tr> <td style="height:30px">

      <div class="cssInnermenuformodel">                         
 <asp:Label ID="lblchangestatusmodel" runat="server" 
            Text="Personal Information Change Status" Font-Size="12pt" ForeColor="Red" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 
          </div>


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

    </td></tr>

                            </table>

                               


                                                                                                                   </td> <td style="vertical-align:top">

                                                                                                                     &nbsp;</td></tr></table>

       


     </asp:Panel>

                       </td></tr>

              </table>    </td> <td  style="width:100px">  </td></tr></table>

      
              


       

    </div>
    </form>
</body>
</html>
