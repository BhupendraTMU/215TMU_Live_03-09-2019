<%@ Page Language="C#" AutoEventWireup="true" CodeFile="surl.aspx.cs" Inherits="surl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    
    <div align="right" >
    <%-- <asp:LinkButton ID="LinkButtonPayFee" runat="server" 
                         onclick="LinkButtonPayFee_Click">Back</asp:LinkButton> &nbsp;&nbsp;--%>
                      <%--    <asp:LinkButton ID="LinkButtonStuLogin" runat="server" 
                         onclick="LinkButtonSigOut_Click">Sign Out</asp:LinkButton>--%>
     </div>
     <div align="center">





         <asp:Label ID="lbltxnid" runat="server" Text=""></asp:Label>
         <asp:Label ID="lblmihpayid" runat="server" Text=""></asp:Label>
         <asp:Label ID="lblmode" runat="server" Text=""></asp:Label>
         <asp:Label ID="lblstatus" runat="server" Text=""></asp:Label>
         <asp:Label ID="lblamount" runat="server" Text=""></asp:Label>
         <asp:Label ID="lblproductinfo" runat="server" Text=""></asp:Label>
          <asp:Label ID="lbldiscount" runat="server" Text=""></asp:Label>
          <asp:Label ID="lblbank_ref_num" runat="server" Text=""></asp:Label>
          <asp:Label ID="lblPG_TYPE" runat="server" Text=""></asp:Label>
          <asp:Label ID="lblpayuMoneyId" runat="server" Text=""></asp:Label>



<asp:label runat="server" text="" ID="LabelError"></asp:label>
     
     </div>
         <div style=" float:left; width:750px; margin-left:20px;  min-height:201px; margin-top:1px; margin-bottom:10px; padding:21px;">
    <div style="float:left; width:750px; height:500px; background-color:White;">
    
        
        <div style="float:left; text-align:center; width:750px; color: #2e568e; font-family: Arial,sans-serif;font-size:30px; margin-top:11px; ">
          <h2>   Payment Status</h2>
        </div>
        <%--<p style="Float:left; color: #153643; font-family: Arial,sans-serif; margin-bottom:21px; font-size: 16px; line-height: 20px;  padding:11px 51px 11px 51px;">Prices of the essential cooking ingredient have surged by over 140 per cent as of August 13 at the Nashik market when compared with Rs 15 per kg at the start of July, as per the National Horticultural Research and Development Foundation (NHRDF) data.</p>--%>

        <div style="float:left; width:749px; ">
            <div style="float:left; margin-left:61px; width:200px; height:400px;  border-top:2px solid #ee4c50; border-bottom:2px solid #ee4c50; ">
                <div style="float:left; padding-left:21px; margin-left:11px; background:#f7f7f7; width:200px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <b>Status:</b>               
                </div>
                <div style="float:left; padding-left:21px; margin-left:11px; background:#f7f7f7; width:200px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <b>Transaction ID:</b>               
                </div>
                <div style="float:left; padding-left:21px; margin-left:11px; background:#f7f7f7; width:200px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <b>Amount:  </b>               
                </div>
                <div style="float:left; padding-left:21px; margin-left:11px; background:#f7f7f7; width:200px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <b>PayuMoney ID:</b>               
                </div>
                <div style="float:left; padding-left:21px; margin-left:11px; background:#f7f7f7; width:200px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <b>Payment Mode:</b>               
                </div>

                   <div style="float:left; padding-left:21px; margin-left:11px; background:#f7f7f7; width:200px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <b>Payment Date:</b>               
                </div>

                <div style="float:left; padding-left:21px; margin-left:11px; background:#f7f7f7; width:200px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <b>Bank Refrence No:</b>               
                </div>
                <div style="float:left; padding-left:21px; margin-left:11px; background:#f7f7f7; width:200px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <b>Student No:</b>               
                </div> 
                <div style="float:left; padding-left:21px; margin-left:11px; background:#f7f7f7; width:200px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <b>Student Name:</b>               
                </div>                
                <div style="float:left; padding-left:21px; margin-left:11px; background:#f7f7f7; width:200px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <b>Date of Birth:</b>               
                </div>
                <div style="float:left; padding-left:21px; margin-left:11px; background:#f7f7f7; width:200px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <b>Father's Name:</b>               
                </div>
                        
            </div>

            <div style="float:left; width:411px; height:400px;  border-top:2px solid #ee4c50; border-bottom:2px solid #ee4c50;">
                
                <div style="float:left; padding-left:11px; margin-left:31px; margin-top:1px; width:350px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    
                    <asp:Panel ID="pnlSuccess" runat="server" Visible="False">
                        <asp:Label ID="lblStatustext" runat="server" ForeColor="#009900" >Success</asp:Label>
                  

                    </asp:Panel>             
                    <asp:Panel ID="pnlFalure" runat="server" Visible="False">
                         <asp:Label ID="lblstatusfauluere" runat="server" ForeColor="Red">Failure     </asp:Label>
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <img src="images/Delete-icon.png" height="10px" />
                         </asp:Panel>
                </div>
                <div style="float:left; padding-left:11px; margin-left:31px; margin-top:1px; width:350px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <asp:Label ID="lbltxtid" runat="server" Text=""></asp:Label>               
                </div>
                <div style="float:left; padding-left:11px; margin-left:31px; margin-top:1px; width:350px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                  <%--<img src="icon/rupess.png" />--%>  &nbsp;&nbsp;    <asp:Label ID="lblAmt" runat="server"></asp:Label>               
                </div>
                <div style="float:left; padding-left:11px; margin-left:31px; margin-top:1px; width:350px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <asp:Label ID="lblOrderid" runat="server"></asp:Label>               
                </div>
                <div style="float:left; padding-left:11px; margin-left:31px; margin-top:1px; width:350px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <asp:Label ID="lblPaymentMode" runat="server"></asp:Label>              
                </div>
                  <div style="float:left; padding-left:11px; margin-left:31px; margin-top:1px; width:350px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <asp:Label ID="lblPaymentDate" runat="server"></asp:Label>               
                </div>

                <div style="float:left; padding-left:11px; margin-left:31px; margin-top:1px; width:350px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <asp:Label ID="lblBankName" runat="server"></asp:Label>               
                </div>
                <div style="float:left; padding-left:11px; margin-left:31px; margin-top:1px; width:350px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <asp:Label ID="lblStudentNo" runat="server"></asp:Label>              
                </div>
                <div style="float:left; padding-left:11px; margin-left:31px; margin-top:1px; width:350px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <asp:Label ID="lblStudentName" runat="server"></asp:Label>              
                </div>
                 <div style="float:left; padding-left:11px; margin-left:31px; margin-top:1px; width:350px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <asp:Label ID="lblDob" runat="server"></asp:Label>              
                </div>
                 <div style="float:left; padding-left:11px; margin-left:31px; margin-top:1px; width:350px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <asp:Label ID="lblFatherName" runat="server"></asp:Label>              
                </div>
                
                
                
                
                
            </div>
        </div>
        
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblStatusCode" runat="server" Visible="False"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server" ForeColor="#009900" Visible="False"></asp:Label>  
                  

                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     
        
        
    </div>
   
</div>
    </form>
</body>
</html>