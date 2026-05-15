<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="BackPaper.aspx.cs" Inherits="BackPaper" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="width:830px">

<table cellpadding="0px" cellspacing="0px"> <tr> <td><p class="Navigator">TMU &gt;&gt; Examination&gt;&gt; Back Paper Payment </p>
      <div id="shadetabs0">
        <ul id="countrytabs" class="shadetabsPage">
          
          <li><a href="StuExamResult.aspx" id="ctl00_ContentPlaceHolder1_linkExamResult" rel="countrycontainer" class="selected"><span>Exam Result</span></a></li>
          
          
          <li><a href="BackPaper.aspx" rel="countrycontainer" ><span>Back Papers Payment</span></a></li>           
          
          <li><a href="ExamSchedule.aspx" rel="countrycontainer" ><span>Exam Schedule</span></a></li>           
          
                    
          
        </ul>
      </div>  </td></tr>
      

      <tr> <td style="height:10px"> </td></tr>
      <tr> <td>
      
        
            <asp:Panel ID="pnlNotBlank" runat="server">

         <%-- <table cellpadding="0px" cellspacing="0px"> <tr> <td>  Exam Fee  </td> <td style="width:10px">  </td> <td>  </td> </tr> </table>
--%>


                <asp:GridView ID="grdCourse" runat="server" BackColor="White" BorderColor="#3366CC" Width="823px" BorderStyle="None" BorderWidth="1px" CellPadding="4" AutoGenerateColumns="False">
                    <Columns>

                        <asp:BoundField DataField="Semester" HeaderText="Semester" />
                        <asp:BoundField DataField="Backpaper" HeaderText="No Of Back Paper" />
                        <asp:TemplateField HeaderText="Exam Fee">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text="3500"></asp:Label>
                                .00
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkPay" runat="server" oncommand="lnkPay_Command">Pay</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        No back Paper(s) .
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <HeaderStyle BackColor="#CAD3D8" Font-Bold="True" ForeColor="Gray" Height="35px" HorizontalAlign="Left"/>
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" ForeColor="Gray" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                    <SortedDescendingHeaderStyle BackColor="#002876" />
                </asp:GridView>
          </asp:Panel>
        </td></tr>
      
      
       </table>

   
 </div>
</asp:Content>

