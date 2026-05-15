<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="facultyFileUpload.aspx.cs" Inherits="Faculty_facultyFileUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset class="boxBody">
 <asp:Label ID="Label3" runat="server" 
            Text="Traning/Dissertation" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
   
 </fieldset>
    <fieldset class="boxBodyHeader"> 
  
 </fieldset>
    <fieldset class="boxBodyInner"> 
        <center>
            <table border="4">
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:FileUpload ID="FacultyFileUpload" runat="server" CssClass="form-control" /></td>
                    <td>
                        <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-info btn" OnClick="btnUpload_Click" /></td>
                    <td>
                        <asp:Label ID="StatusLabel" runat="server" Text=""></asp:Label></td>
                </tr>
               
            </table>
            <br />
            <br />

            
               <script type="text/javascript">

                   function SearchGrid(txtSearch, Filedownload) {
                       if ($("[id *=" + txtSearch + " ]").val() != "") {
                           $("[id *=" + Filedownload + " ]").children
        ('tbody').children('tr').each(function () {
            $(this).show();
        });
                           $("[id *=" + Filedownload + " ]").children
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


                           $("[id *=" + Filedownload + " ]").children('tbody').
                children('tr').each(function (index) {
                    if (index == 0)
                        $(this).show();
                });
                       }
                       else {
                           $("[id *=" + Filedownload + " ]").children('tbody').
                children('tr').each(function () {
                    $(this).show();
                });
                       }
                   }

               </script>
     <%--   <asp:Textbox runat="server" ID="txtSearch" ></asp:Textbox>--%>
            
      
            
                    <table>
                        <tr>
                            <td><input type="text"  id="txtSearch"  placeholder="Type Your Search Text  " class="form-control"  onkeyup=" SearchGrid('txtSearch', '<%=Filedownload.ClientID%>')"/></td>
                        </tr>
                <tr>

                    <td>
                         <asp:GridView ID="Filedownload" runat="server" Width="1000px"
                              EmptyDataText="There are no data records to display." CssClass="table table-striped table-bordered table-hover"
                              BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="Sno"
                              OnRowCommand="Filedownload_RowCommand" AutoGenerateColumns="False" OnDataBound="Filedownload_DataBound">
                    <Columns>
                       
                      <asp:BoundField DataField="FacultyCode" HeaderText="Faculty Code" />
                        <asp:BoundField DataField="FacultyCollegeCode" HeaderText="College Code" />
                        <asp:BoundField DataField="FacultyFileName" HeaderText="File Name" />
                        <asp:BoundField DataField="CreateDate" HeaderText="Upload Date" DataFormatString="{0:d}" />
                       <%-- <asp:BoundField DataField="FacultyFilePath" HeaderText="File" />--%>
                       <asp:TemplateField HeaderText="Delete" >
                           <ItemTemplate>
                                 <asp:LinkButton ID="delelink" runat="server"  CommandName="btnDelete" CommandArgument='<%# Eval("Sno") %>' OnClientClick="return confirm('Are you sure you want to delete this event?');" >Delete</asp:LinkButton>
                           </ItemTemplate>
                       </asp:TemplateField>
                      <asp:TemplateField HeaderText="File">
                            <ItemTemplate>

                                <asp:LinkButton ID="LinkButton1" runat="server"  CommandName="download" CommandArgument='<%# Eval("Sno") %>' >Download</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                    </Columns>
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
</asp:Content>

