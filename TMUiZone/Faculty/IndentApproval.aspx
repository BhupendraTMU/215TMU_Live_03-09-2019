<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="IndentApproval.aspx.cs" Inherits="Faculty_IndentApproval" EnableEventValidation="false"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
       <fieldset class="boxBody" >
 <asp:Label ID="Label1" runat="server" 
            Text="Indent Approval" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 </fieldset>

    <table cellpadding="0px" cellspacing="0px" style="width:100%"> <tr> <td style="width:10px">  </td> <td>   <table cellpadding="0px" cellspacing="0px" style="width:100%"> 
        <tr> <td style="height:20px" colspan="2"> </td></tr>
         <tr>            
              <td align="left"> 
        <table cellpadding="0px" cellspacing="0px"> <tr>
             <td style="width:20%"> </td> 
          <td >
            <asp:Panel ID="pnlDatewisefilter" runat="server" Visible="true"> <table cellpadding="0px" cellspacing="0px"> <tr> <td> From Date  </td> <td style="width:10px">  </td> <td> <asp:TextBox ID="txtFromDate" runat="server" onkeydown="return false;" 
           oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" Width="120px"></asp:TextBox> 
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate" Format="dd MMM yyyy"></asp:CalendarExtender>
                                                                                                                                                                                    </td> <td style="width:10px">  </td> <td>Till Date </td> <td style="width:10px"> </td> <td>
                <asp:TextBox ID="txtTillDate" runat="server" onkeydown="return false;" 
           oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" Width="120px"></asp:TextBox> 

<asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTillDate" Format="dd MMM yyyy"></asp:CalendarExtender>
                                                                                                                                                                                                                                                                           </td> <td style="width:10px">  </td> <td> Status </td> <td style="width:10px">  </td> <td>  
             <asp:DropDownList ID="ddStatus" runat="server" Height="30px" >
                <asp:ListItem Value="1">Processed for Approval</asp:ListItem>
                <asp:ListItem Value="2">Approved</asp:ListItem>
                <asp:ListItem Value="4">Rejected</asp:ListItem>
                </asp:DropDownList></td> <td style="width:10px"> </td> <td> <asp:Button ID="btnSearch" runat="server" Text="Get" OnClick="btnSearch_Click" /> </td></tr> </table> </asp:Panel>                                                                                                                                                                                                                                                                     </td> <td> </td> </tr> </table>

                                                      </td></tr> 

        <tr> <td style="height:20px"> </td></tr>
        <tr><td style="border-color:#ff9900;margin-bottom: 12px;">
            <input type="text"  id="txtSearch"  name="off" autocomplete="off" placeholder="Search Data" style="margin-right: 10px;margin-bottom: 12px;" />
            <input id="btnClickMe" type="button" value="Search" style="height:31px;" onclick="return SearchGrid('txtSearch', '<%=grdApproval.ClientID%>')" />
         </td></tr>
        <tr> <td align="center">
            <script type="text/javascript">

                function SearchGrid(txtSearch, grdApproval) {
                    if ($("[id *=" + txtSearch + " ]").val() != "") {
                        $("[id *=" + grdApproval + " ]").children
     ('tbody').children('tr').each(function () {
         $(this).show();
     });
                        $("[id *=" + grdApproval + " ]").children
     ('tbody').children('tr').each(function () {
         var match = false;
         $(this).children('td').each(function () {
             if ($(this).text().toUpperCase().indexOf($("[id *=" +
         txtSearch + " ]").val().toUpperCase()) > -1) {
                 match = true;
                 return false;
             }
         });
         if (match) {
             $(this).show();
             $(this).children('th').show();
         }
         else {
             $(this).hide();
             $(this).children('th').show();
         }
     });


                        $("[id *=" + grdApproval + " ]").children('tbody').
             children('tr').each(function (index) {
                 if (index == 0)
                     $(this).show();
             });
                    }
                    else {
                        $("[id *=" + grdApproval + " ]").children('tbody').
             children('tr').each(function () {
                 $(this).show();
             });
                    }
                }

            </script>



            <asp:GridView ID="grdApproval" runat="server"   AutoGenerateColumns="False" ForeColor="#333333" GridLines="None"  DataKeyNames="DocumentNo"
                CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4" OnRowDataBound="grdApproval_RowDataBound" OnPageIndexChanging="grdApproval_PageIndexChanging" AllowPaging="True" 
                >
                <Columns>
                     <asp:TemplateField HeaderText="Indent No">
                        <ItemTemplate>
                            <asp:LinkButton ID="lblDoumnentNo" ForeColor="#ff3300" runat="server" Text='<%#Bind("DocumentNo") %>' OnCommand="lblDoumnentNo_Command" CommandArgument='<%#Bind("DocumentNo") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                  <%--  <asp:BoundField DataField=" DocumentNo" HeaderText="Indent No" />--%>
                    <asp:BoundField DataField="Issue Date" HeaderText="Issue Date" DataFormatString="{0:dd MMM yyyy}" />
                    <asp:BoundField DataField="Issue For" HeaderText="Issue For" />
                    <%--<asp:BoundField DataField="Status" HeaderText="Status" />--%>

                       <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("Status") %>'></asp:Label>
                            
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Issue Id" HeaderText="Issue Id" />
                    <asp:BoundField DataField="Issue Name" HeaderText="Issue Name" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnApprove" runat="server" Text="Approve" OnCommand="btnApprove_Command" CommandArgument='<%# Eval("DocumentNo") %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    

                     <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnReject" runat="server" Text="Reject" CommandArgument='<%# Eval("DocumentNo") %>' OnCommand="btnReject_Command" />
                            <asp:HiddenField ID="AllApprove" runat="server" Value='<%# Eval("AllIssue") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    There are no record found.....
                </EmptyDataTemplate>
                <FooterStyle BackColor="#ff9900" Font-Bold="True" ForeColor="White" />
                <HeaderStyle  BackColor="#ff9900" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#ff9900" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#000066" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>

            <%--<script type="text/javascript" src="../jquery.min.js"></script>
                            <script type="text/javascript" src="../quicksearch.js"></script>
                            <script type="text/javascript">
                                $(function () {
                                    $('.search_textbox').each(function (i) {
                                        $(this).quicksearch("[id*=grdApproval] tr:not(:has(th))", {
                                            'testQuery': function (query, txt, row) {
                                                return $(row).children(":eq(" + i + ")").text().toLowerCase().indexOf(query[0].toLowerCase()) != -1;
                                            }
                                        });
                                    });
                                });
                            </script>--%>
             </td></tr>

        <tr> <td style="height:90px">  </td></tr>

    </table> </td> <td style="width:10px">  </td></tr>  </table>

  

     <asp:Button ID="Button1" runat="server" Text="Button" Style="display:none" />
    <asp:ModalPopupExtender ID="mdIndentLine" runat="server" TargetControlID="Button1" PopupControlID="PnlIndentLineData" BackgroundCssClass="modalBackgroundforco"></asp:ModalPopupExtender>


    <asp:Panel ID="PnlIndentLineData" runat="server" BackColor="White" Style="display:none"  >


        <table cellpadding="0px" cellspacing="0px"> 
            <tr> <td style="height:20px"> </td></tr>
            <tr> <td align="right">  <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" /> &nbsp;&nbsp;</td></tr>

             <tr> <td style="height:20px"> </td></tr>
             <tr> <td style="background-color:#e5e3e3">
            <table cellpadding="0px" cellspacing="0px"> 
                <tr> <td style="height:10px"> </td></tr>
                 <tr> <td > &nbsp; &nbsp; <asp:Label ID="Label2" runat="server" Text="Indent Sub Form" Font-Bold="True" Font-Size="10pt" ForeColor="Black"></asp:Label>  </td></tr>
                 <tr> <td style="height:10px">  </td></tr>
            </table>
             </td></tr>
            <tr> <td>  
                <asp:GridView ID="grdViewIndentLine" runat="server"  AutoGenerateColumns="False" ForeColor="#333333" GridLines="None" 
                    CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#3366CC" BorderStyle="None" 
                    BorderWidth="1px" CellPadding="4" DataKeyNames="Line No_" >
                <Columns>
                    <asp:BoundField DataField="Document No" HeaderText="Indent No" />
                    <asp:BoundField HeaderText="No_" DataField="No_" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:TemplateField HeaderText="Item No.">
                        <ItemTemplate>
                            <asp:Label ID="lblItemNo_Grid" runat="server" Text='<%#Bind("[Item No]") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                            <asp:Label ID="lblItemNoDescription_Grid" runat="server" Text='<%#Bind("[Description]") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit of Measure">

                           <ItemTemplate>
                            <asp:Label ID="lblUnitofMeasure_Grid" runat="server" Text='<%#Bind("[Unit of Measure]") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="Request Quantity">
                          <ItemTemplate>
                            <asp:TextBox ID="lblQuantity_Grid" runat="server" Text='<%#Bind("Quantity","{0:n}" ) %>'></asp:TextBox>
                            <asp:HiddenField ID="hfQuantity" runat="server" value='<%#Bind("Quantity","{0:n}" ) %>'></asp:HiddenField>
                        </ItemTemplate>
                    </asp:TemplateField>
                  

                    <%--<asp:TemplateField HeaderText="Variance Code"></asp:TemplateField>--%>
                   
                     
                    <asp:TemplateField HeaderText="Issue Qty">

                           <ItemTemplate>
                            <asp:Label ID="lblishuquty_Grid" runat="server" Text='<%#Bind("[Issued Qty]","{0:n}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Pending/Reject Quantity">

                           <ItemTemplate>
                            <asp:Label ID="lblqiai_Grid" runat="server" Text='<%#Bind("Rem_Qty","{0:n}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Reason of Pending/Rejection">
                           <ItemTemplate>
                            <asp:Label ID="lblremark" runat="server" Text='<%#Bind("[Remarks]") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
               <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle  BackColor="#ff9900" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>

                 </td></tr>


             <tr> <td style="height:120px"> </td></tr>

        </table>

   


    </asp:Panel>

</asp:Content>

