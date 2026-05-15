<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="SubjectAllocationReport.aspx.cs" Inherits="Enquiry_SubjectAllocationReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

    <fieldset class="boxBody">
 <asp:Label ID="Label3" runat="server" 
            Text="Subject Choice/Allocation Report" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
   
 </fieldset>
    <fieldset class="boxBodyHeader"> 
  
 </fieldset>

    <fieldset class="boxBodyInner" id="SubAllocation" runat="server">        
            
<center>
            <table width="87%"  >
                <tr>
                   <td align="left" width="20%">
                     <asp:CheckBox ID="chkassign" runat="server" Text="Assign" AutoPostBack="True"  Visible="false" />
                </td> 
                <td align="center">
                    <table>
                            <tr>                        
                    <td align="right">  Subject Type : </td>
                    <td>&nbsp;&nbsp;&nbsp;</td>                    
                    <td> <asp:RadioButton ID="RbtnTHEORY" runat="server" Text="THEORY" GroupName="SubjectGroup" AutoPostBack="True" Checked="True" /></td>
                    <td>&nbsp;&nbsp;&nbsp;</td>
                    <td>  <asp:RadioButton ID="RbtnLAB" runat="server" Text="LAB" GroupName="SubjectGroup" AutoPostBack="True" /></td>                                
                            </tr>
                        </table>

                </td>
                    <td align="right" width="20%">
                          <asp:ImageButton ID="btnExport" Visible="false" runat="server" ImageUrl="~/images/excel.jpg" Height="25px" OnClick="btnExport_Click" ></asp:ImageButton>
                    </td>
                </tr>
               
            </table>
            
            <br />
            <table >
                <tr>
                    <td>
                         <asp:GridView ID="grdSubjectAllocation" runat="server" Width="1000px" EmptyDataText="There are no data records to display." CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" >
                   
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#ff9900" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" ForeColor="#003399" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
                    </td>
                </tr>
            </table>

        </center>
        </fieldset>
            
        </ContentTemplate>
         <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>
    </asp:UpdatePanel>
</asp:Content>

