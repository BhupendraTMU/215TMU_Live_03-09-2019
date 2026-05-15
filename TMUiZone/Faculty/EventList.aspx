<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="EventList.aspx.cs" Inherits="Faculty_EventList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <style type="text/css">
        .parent {
            text-align: center;
            display: block;
            border: 1px solid outset;
        }

        .child {
            display: inline-block;
            width: 200px;
        }

      


    </style>
    <script type="text/javascript">

        function preventBackspace(e) {
            var evt = e || window.event;
            if (evt) {
                var keyCode = evt.charCode || evt.keyCode;
                if (keyCode === 8) {
                    if (evt.preventDefault) {
                        evt.preventDefault();
                    } else {
                        evt.returnValue = false;
                    }
                }
            }
        }
        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select greater than current date!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
        function checkDate1(sender, args) {
            if (sender._selectedDate < new Date()) {
                alert("You cannot select Less than current date!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }

        </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:ToolkitScriptManager ID="ScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="elist" runat="server">
        <ContentTemplate>


        
    
            <asp:Panel ID="pnlList" runat="server" BorderWidth="2px" BorderColor="#ACE9FB" ScrollBars="Vertical">
                 <div class="navbar-inverse" style="height:40px; background-color:#1ECCF3" >
            <marquee ><asp:Label id="lblEvent" style="font-size:20px; line-height:45px"  ForeColor="White"  runat="server" ></asp:Label></marquee>
                </div>
                 <div class="parent" style="background-color: #ff6a00">
        <br />
        <table style="fit-position: left;   padding-top:25px;" width="100%">
            <tr>
                <td width="10%" align="left">
                 &nbsp&nbsp&nbsp&nbsp<asp:Label ID="Label1" runat="server"  Visible="true" Text="Event List" Font-Size="15pt" ForeColor="#093A62" 
                     Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>&nbsp&nbsp&nbsp&nbsp                  
                </td>
                <td width="90%" >                   
                   <table id="tblSearch" runat="server" >
                       <tr>
                           <td width="20%" align="right">
                               <asp:DropDownList runat="server" ID="ddleventl" Width="200px" CssClass="form-control input-sm" Height="30px"></asp:DropDownList>
                           </td>

                           <td width="15%" align="right" style="padding-left:3%">
                               <b>
                               <asp:TextBox runat="server" ID="txtFromtDate" CssClass="form-control input-sm" Width="120px" ToolTip="From Date" onkeypress="return false"
                                       onKeyDown="preventBackspace();" placeholder="From Date"></asp:TextBox>
                                <asp:Image src="../Images/Calendar.png" runat="server" Height="30px" Width="30px" alt="" ID="fdate" />
                                   <asp:CalendarExtender ID="cleFromDate" Format="dd MMM yyyy" runat="server" CssClass="cal_Theme1"
                                       PopupButtonID="fdate" Enabled="true" TargetControlID="txtFromtDate" />
                                   &nbsp&nbsp&nbsp&nbsp
                        

                               </b>
                           </td>
                           <td width="15%" align="left">
                               <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control input-sm" Width="120px" ToolTip="To Date" onkeypress="return false"
                                   onKeyDown="preventBackspace();" placeholder="To Date"></asp:TextBox>
                                <asp:Image src="../Images/Calendar.png" runat="server" Height="30px" Width="30px" alt="" ID="tdate" />
                               <asp:CalendarExtender ID="cleToDate" Format="dd MMM yyyy" runat="server" CssClass="cal_Theme1"
                                   PopupButtonID="tdate" Enabled="true" TargetControlID="txtToDate" />
                                                 
                           </td>
                           <td width="15%" align="center">
                               <asp:DropDownList runat="server" ID="ddlcollege" Width="200px" CssClass="form-control input-sm" Height="30px" Enabled="false"></asp:DropDownList>
                           </td>
                           <td width="10%" align="left">
                               <asp:Button ID="BtnShow" type="button" Text="Show" class="btn btn-info btn" runat="server" OnClick="BtnShow_Click"></asp:Button>
                            <asp:ImageButton ID="btnExportToExcel" runat="server" ImageUrl="~/images/excel.jpg"  OnClick="btnExportToExcel_Click" Width="40px" Height="30px" Visible="true" ValidationGroup="show"></asp:ImageButton>                  
                     <asp:ImageButton ID="BtnExportpdf" runat="server" ImageUrl="~/images/pdf.jpg" OnClick="BtnExportpdf_Click"  Width="40px" Height="30px" Visible="true" ValidationGroup="show"></asp:ImageButton>                  
                  
                           
                           
                           </td>
                       </tr>
                   </table>
                </td>
            </tr>
        </table>
        <br />
    </div>

         <div class="table-responsive">
            <asp:GridView ID="GridView1" runat="server"   DataKeyNames="Code" AutoGenerateColumns="false" BackColor="White" 
                EmptyDataText="There are no data records to display." BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" 
                CellPadding="3" Width="100%" GridLines="Horizontal" AllowPaging="true"
    PageSize="55">
              <AlternatingRowStyle BackColor="#F7F7F7" />
     <Columns>
         <asp:TemplateField HeaderText="Sr. No.">
                           <ItemTemplate >
                          <%# Container.DataItemIndex + 1 %>
                                     </ItemTemplate>
                                       <ItemStyle Width="3%" />
                                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="Event"> <ItemStyle Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblEvent" runat="server" Text='<%#Eval("Event") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Campus"> <ItemStyle Width="7%" />
                                <ItemTemplate>
                   <asp:Label ID="lblcollege" runat="server" Text='<%#Eval("Campus") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
        <asp:TemplateField HeaderText="Faculty"> <ItemStyle Width="15%" />
                                <ItemTemplate>
                   <asp:Label ID="lblfaculty" runat="server" Text='<%#Eval("[Name of Guest Faculty]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
        <asp:TemplateField HeaderText="Organization"> <ItemStyle Width="15%" />
                                <ItemTemplate>
                   <asp:Label ID="lblorganization" runat="server" Text='<%#Eval("[Organization]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
        <asp:TemplateField HeaderText="Objective"> <ItemStyle Width="25%" />
                                <ItemTemplate>
                   <asp:Label ID="lblobjEvent" runat="server" Text='<%#Eval("[Objective of Event]") %>' Width="100%" ReadOnly="true"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
          <asp:TemplateField HeaderText="Event Type"> <ItemStyle Width="10%" />
                                <ItemTemplate>
                   <asp:Label ID="lblEventType" runat="server" Text='<%#Eval("[EventType]") %>'></asp:Label>
                   <asp:HiddenField ID="hfEventType" runat="server" value='<%#Eval("[Type of Event]") %>'></asp:HiddenField>
                                </ItemTemplate>
                            </asp:TemplateField>
   
          <asp:TemplateField HeaderText="Date"> <ItemStyle Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lbldate" runat="server"  Text='<%#Eval("Date","{0:dd MMM yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                         
                            

         <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="Btnpop" runat="server" Text='<%# Eval("Img").ToString() == "0" ? "" : "Attachment" %>' OnClick="Btnpop_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
      
    </Columns>             
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
               <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
               <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Left" />
               <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF"   />
               <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
               
</asp:GridView>
             <br />




         </div>
            </asp:Panel>



    <asp:Button ID="btnShowPopup" runat="server" style="display:none" />
 <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
 BackgroundCssClass="modalBackground">
</asp:ModalPopupExtender>
<asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="350px" Width="700px" style="display:none">

<table width="100%" style="border:Solid 3px #D55500; width:100%; height:100%;" cellpadding="0" cellspacing="0">
<tr style="background-color:#D55500">
<td style=" height:10%; color:White; font-weight:bold; font-size:larger;width:90%" align="center" >EVENT DETAILS</td>
    <td style=" height:10%; color:White; font-weight:bold; font-size:larger;width:10%" align="right" ><asp:Button ID="btnCancel" runat="server" Text="X" BackColor="#D55500" /></td>
</tr>     


    <tr>
 <td  colspan="2" style="height:2px" align="right"> <asp:Label ID="lblpop" runat="server" Visible="false"></asp:Label>

     
     
 </td>       

    </tr>
<tr>

<td colspan="2" align="center">
    <br />
<asp:GridView ID="GridViewpop" runat="server"  CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" 
    BackColor="White" EmptyDataText="There are no data records to display." BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" 
    CellPadding="3" Width="94%"  GridLines="Horizontal" AllowPaging="false" ShowHeader="false" >
              <AlternatingRowStyle BackColor="#F7F7F7" />
    <Columns>      
        <asp:TemplateField HeaderText="IMAGE1" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>  
             <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("Image1") %>' Height="240px" Width="200px" AlternateText="NO IMAGE"/><br />
                                    
           <asp:LinkButton ID="lnkDownload" Text ='<%# Eval("Image1").ToString() == "" ? "" : "Download" %>' CommandArgument = '<%# Eval("Image1") %>' runat="server" OnClick="lnkDownload_Click"></asp:LinkButton>
                                       <br />
    
                                </ItemTemplate>
                            </asp:TemplateField>

          <asp:TemplateField HeaderText="IMAGE2" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>                   
                   <asp:Image ID="Image2" runat="server" ImageUrl='<%# Eval("Image2") %>' Height="240px" Width="200px" ImageAlign="Middle" AlternateText="NO IMAGE" /><br />
                                    
           <asp:LinkButton ID="lnkDownload2" Text ='<%# Eval("Image2").ToString() == "" ? "" : "Download" %>' CommandArgument = '<%# Eval("Image2") %>' runat="server" OnClick="lnkDownload2_Click"></asp:LinkButton> <br />
                                </ItemTemplate>
                            </asp:TemplateField>
   
          <asp:TemplateField HeaderText="IMAGE3" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
         <asp:Image ID="Image3" runat="server" ImageUrl='<%# Eval("Image3") %>' Height="240px" Width="200px" ImageAlign="Middle"  AlternateText="NO IMAGE"/>
                                    <br />                                   
   <asp:LinkButton ID="lnkDownload3" Text ='<%# Eval("Image3").ToString() == "" ? "" : "Download" %>'
 CommandArgument = '<%# Eval("Image3") %>' runat="server" OnClick="lnkDownload3_Click"></asp:LinkButton>
 <br />
     
                                </ItemTemplate>
                            </asp:TemplateField> 
 
    </Columns> 
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
              <%-- <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />--%>               
               <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont"   />
               <%--<SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />--%>
               
</asp:GridView>
</td>
</tr>
 
</table>
</asp:Panel>
    </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportToexcel" />
            <asp:PostBackTrigger ControlID="BtnExportpdf" />
            <asp:PostBackTrigger  ControlID="GridViewpop"/>
        </Triggers>
    </asp:UpdatePanel>
    
 
  


</asp:Content>

