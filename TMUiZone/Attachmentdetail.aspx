<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Attachmentdetail.aspx.cs" Inherits="Attachmentdetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ashoka University</title>
    <link href="css/mainpage.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <table cellpadding="0px" cellspacing="0px" style="width:100%">


            <tr> <td> 


                  <div id="wrap"> 

    <table cellpadding="0px" cellspacing="0px" style="width:100%" >  

         <tr> <td >

            
                               <fieldset class="boxBodyHeader"> 
 <asp:Label ID="Label7" runat="server" 
            Text="Attachment Details " Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>  

 
 </fieldset>


                                                                             </td></tr>


        <tr> <td align="center"> 

            <asp:GridView ID="grdAttachmentcsv" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
    RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000"
    AutoGenerateColumns="false" Width="962px"   PageSize="30" >
                <AlternatingRowStyle BackColor="White" ForeColor="#000000" />
                <Columns>
                    <asp:BoundField DataField="File_Attachment_Name" HeaderStyle-HorizontalAlign="Left" HeaderText="Attachment">
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDownloadcsv" runat="server" CommandArgument='<%# Eval("id") %>' OnClick="DownloadFile" Text="Download"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDeketeattachementcsv" runat="server" CommandArgument='<%# Eval("id") %>'  Text="Delete" OnCommand="lnkDeketeattachementcsv_Command"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="White" 
                      HorizontalAlign="Left" Font-Size="8pt" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" />
                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Font-Size="7pt" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"/>
                 <PagerStyle BackColor="#C5122F" ForeColor="#4A3C8C" HorizontalAlign="Left" />
</asp:GridView>

             </td></tr>

        </table>

            </div>

                 </td></tr>
        </table>

      
    </div>
    </form>
</body>
</html>
