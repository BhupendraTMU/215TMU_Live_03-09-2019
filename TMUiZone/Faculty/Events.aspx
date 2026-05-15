<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="Events.aspx.cs" Inherits="Student_Events" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
iiii</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <fieldset class="boxBody">
 <asp:Label ID="Label3" runat="server" 
            Text="Events" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
 </fieldset>
 <fieldset class="boxBodyHeader"> 
  
 </fieldset>
    <fieldset  style="background:#fefefe; border-top:1px solid #dde0e8; border-bottom:1px solid #dde0e8; padding:10px 20px; height:100%">
    <br />
            <div class="navbar-inverse" style="height:40px; background-color:#1ECCF3" >
            <marquee ><asp:Label id="lblEvent" style="font-size:20px; line-height:45px"  ForeColor="White"  runat="server" ></asp:Label></marquee>
                </div>
              <center>
                  <br />
               <asp:GridView ID="grdEvents" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" EmptyDataText="There are no data records to display." BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" OnPageIndexChanging="grdEvents_PageIndexChanging">
               <AlternatingRowStyle BackColor="#F7F7F7" />      
                  <Columns>                            
                            <asp:BoundField DataField="Date1" HeaderText="Event Date" SortExpression="ApplicantName"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" >
                               
                            </asp:BoundField>
                            <asp:BoundField DataField="Event" HeaderText="Events" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" >

                            </asp:BoundField>
                            <asp:BoundField DataField="Campus" HeaderText="Campus" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" >

                            </asp:BoundField>
                          </Columns>                       
                     <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
               <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
               <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
               <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont"   />
               <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
               <SortedAscendingCellStyle BackColor="#F4F4FD" />
               <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
               <SortedDescendingCellStyle BackColor="#D8D8F0" />
               <SortedDescendingHeaderStyle BackColor="#3E3277" />
                    </asp:GridView>
                  </center>
        </fieldset>
</asp:Content>

