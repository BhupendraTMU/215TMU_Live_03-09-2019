<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaytmTransactionStatus.aspx.cs" Inherits="PaytmTransactionStatus" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <div style=" float:left; width:750px; margin-left:20px;  min-height:201px; margin-top:1px; margin-bottom:10px; padding:21px;">
    <div style="float:left; width:750px; height:500px; background-color:White;">
    <div style="float:left; text-align:center; width:750px; color: #2e568e; font-family: Arial,sans-serif;font-size:30px; margin-top:11px; ">
        <asp:label runat="server" text="" ID="LabelError"></asp:label>
    </div>        
        <div style="float:left; text-align:center; width:750px; color: #2e568e; font-family: Arial,sans-serif;font-size:30px; margin-top:11px; ">
          <h2>   Payment Status</h2>
        </div>       

        <div style="float:left; width:749px; ">
            <div style="float:left; margin-left:61px; width:200px; height:384px;  border-top:2px solid #ee4c50; border-bottom:2px solid #ee4c50; ">
                <div style="float:left; padding-left:21px; margin-left:11px; background:#f7f7f7; width:200px; height:11px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <b>Status:</b>               
                </div>
                <div style="float:left; padding-left:21px; margin-left:11px; background:#f7f7f7; width:200px; height:11px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <b>Transaction ID:</b>               
                </div>

                  <div style="float:left; padding-left:21px; margin-left:11px; background:#f7f7f7; width:200px; height:11px;  padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <b>Amount:</b>               
                </div>
                  <div style="float:left; padding-left:21px; margin-left:11px; background:#f7f7f7; width:200px; height:11px;  padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <b>Paytm ID: </b>               
                </div>
                  <div style="float:left; padding-left:21px; margin-left:11px; background:#f7f7f7; width:200px; height:11px;  padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <b>Payment Mode:</b>               
                </div>
                  <div style="float:left; padding-left:21px; margin-left:11px; background:#f7f7f7; width:200px; height:11px;  padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <b>Payment Date:</b>               
                </div>
                 <div style="float:left; padding-left:21px; margin-left:11px; background:#f7f7f7; width:200px; height:11px;  padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <b>Bank Name: </b>               
                </div>
                <div style="float:left; padding-left:21px; margin-left:11px; background:#f7f7f7; width:200px; height:11px;  padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <b>Application No:</b></div>
                <%--<div style="float:left; padding-left:21px; margin-left:11px; background:#f7f7f7; width:200px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <b>xx</b>               
                </div>--%>
                <div style="float:left; padding-left:21px; margin-left:11px; background:#f7f7f7; width:200px; height:11px;  padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <b>Applicant Name: </b>               
                </div>

                   <div style="float:left; padding-left:21px; margin-left:11px; background:#f7f7f7; width:200px; height:11px;  padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <b>Date Of Birth:</b>               
                </div>

                <div style="float:left; padding-left:21px; margin-left:11px; background:#f7f7f7; width:200px; height:11px;  padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <b>Father's Name:</b></div>
                <div style="float:left; padding-left:21px; margin-left:11px; background:#f7f7f7; width:200px; height:11px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <b>Enquiry ID:</b>               
                </div>
                
                               
            </div>

            <div style="float:left; width:411px; height:384px;  border-top:2px solid #ee4c50; border-bottom:2px solid #ee4c50;">
                
                <div style="float:left; padding-left:11px; margin-left:31px; margin-top:1px; width:350px;height:10px;  height:10px; padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                  
                    <asp:Panel ID="pnlSuccess" runat="server" Visible="False">
                        <asp:Label ID="lblStatustext" runat="server" ForeColor="#009900" >Success</asp:Label>
                  

                    </asp:Panel>             
                    <asp:Panel ID="pnlFalure" runat="server" Visible="False">
                         <asp:Label ID="lblstatusfauluere" runat="server" ForeColor="Red">Failure</asp:Label>
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <img src="images/Delete-icon.png" height="10px" />
                         </asp:Panel>
                </div>
                <div style="float:left; padding-left:11px; margin-left:31px; margin-top:1px; width:350px; height:10px;  padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <asp:Label ID="lbltxtid" runat="server" Text=""></asp:Label>               
                </div>

                  <div style="float:left; padding-left:11px; margin-left:31px; margin-top:1px; width:350px; height:10px;  padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <img src="logo/Rupee.jpg" style="height:20px ;width:20px"/>  &nbsp;&nbsp;    <asp:Label ID="lblAmount" runat="server" ></asp:Label>               
                </div>
                  <div style="float:left; padding-left:11px; margin-left:31px; margin-top:1px; width:350px; height:10px;  padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <asp:Label ID="lblOrderid" runat="server"></asp:Label>               
                </div>
                  <div style="float:left; padding-left:11px; margin-left:31px; margin-top:1px; width:350px; height:10px;  padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                      <asp:Label ID="lblPaymentMode" runat="server"></asp:Label>             
                </div>
                  <div style="float:left; padding-left:11px; margin-left:31px; margin-top:1px; width:350px; height:10px;  padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <asp:Label ID="lblPaymentDate" runat="server"></asp:Label>              
                </div>

                <div style="float:left; padding-left:11px; margin-left:31px; margin-top:1px; width:350px; height:10px;  padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                  <asp:Label ID="lblBankName" runat="server"></asp:Label>                                
                </div>
                <div style="float:left; padding-left:11px; margin-left:31px; margin-top:1px; width:350px; height:10px;  padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                    <asp:Label ID="lblApplicationNo" runat="server" Text=""></asp:Label>               
                </div>
                <div style="float:left; padding-left:11px; margin-left:31px; margin-top:1px; width:350px; height:10px;  padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                  <asp:Label ID="lblApplicantName" runat="server" Text=""></asp:Label>               
                </div>
                  <div style="float:left; padding-left:11px; margin-left:31px; margin-top:1px; width:350px; height:10px;  padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                     <asp:Label ID="lblDob" runat="server" Text=""></asp:Label>               
                </div>

                <div style="float:left; padding-left:11px; margin-left:31px; margin-top:1px; width:350px; height:10px;  padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                     <asp:Label ID="lblFatherName" runat="server" Text=""></asp:Label>  
                </div>
                <div style="float:left; padding-left:11px; margin-left:31px; margin-top:1px; width:350px; height:10px;  padding-top:10px; padding-bottom:10px; border-bottom:1px solid #e5e5e5;  font-family: Arial,sans-serif; font-size: 11px;">
                                  <asp:Label ID="lblAgentId" runat="server"></asp:Label>              
                </div>     
                
            </div>
        </div>
        
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblStatusCode" runat="server" Visible="False"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblStatus" runat="server" ForeColor="#009900" Visible="False"></asp:Label>  
                  

                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     
        
        
    </div>
   
</div>

    </div>
    </form>
</body>
</html>
