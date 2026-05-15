<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="ITIssueTrackerList.aspx.cs" Inherits="Faculty_IssueTrackerList" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
    <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <%--<script type="text/javascript">
    $(function () {
        $('[id*=CheckBoxList1]').multiselect({
            includeSelectAllOption: true
        });
    });
</script>--%>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js">
    </script>


    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script type="text/jscript">
        function Search() {

            var t = document.getElementById("txtSearching").value;

            $('#cphContentBody_chkJudges tbody tr').each(function () {

                var str = $(this).text();
                if (str.toUpperCase().indexOf(t.toUpperCase()) >= 0) {

                    $(this).show();
                }
                else {
                    $(this).hide();
                }
            }
            );

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server" Text="Issue Tracker" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
        <asp:Label ID="Label2" runat="server" Text=":" Font-Size="Large" Font-Underline="true" ForeColor="Red"></asp:Label>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
           
          <asp:Label ID="Label3" runat="server" Text="Issue Date" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
        <asp:Label ID="Label4" runat="server" Text=":" Font-Size="Large" Font-Underline="true" ForeColor="Red"></asp:Label>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
           
          <asp:Label ID="Label5" runat="server" Text="Issue Status" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
        <asp:Label ID="Label6" runat="server" Text=":" Font-Size="Large" Font-Underline="true" ForeColor="Red"></asp:Label>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 
           
          <asp:Label ID="Label7" runat="server" Text="Assign To" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
        <asp:Label ID="Label8" runat="server" Text=":" Font-Size="Large" Font-Underline="true" ForeColor="Red"></asp:Label>

    </fieldset>
    <div id="divGeneralBody">
        <fieldset class="boxBodyInner">
            <div class="form-horizontal">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <div class="col-md-2">
                                <label style="width: 200px">Employee Name.</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtemployeename" runat="server" Enabled="false" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label style="width: 200px">Employee Code.</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtemployeeceode" runat="server" Enabled="false" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label style="width: 200px">Department Name.</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtemployeedept" runat="server" Enabled="false" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-2">
                                <label style="width: 200px">College</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtcollege" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label style="width: 200px">Complain Type</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtcomplaintype" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                          <div class="col-md-2">
                                <label style="width: 200px">Complaint Detail</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="TextBox1" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-2">
                                <label style="width: 200px">Involve Dept..</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtinvolvedept" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label style="width: 200px">Status.</label>
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="drpstatus" runat="server"  AutoPostBack="true" BorderColor="Black" CssClass="form-control">
                                    <asp:ListItem Text="SELECT" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Open" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Assign To" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Process" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Completed" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Closed" Value="5"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-2">
                                <label style="width: 200px">Assign To.</label>
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="drpassignto" runat="server" AutoPostBack="true" BorderColor="Black" CssClass="form-control">
                                   
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">

                               <div class="col-md-2">                               
                                <label style="width: 200px">Mobile No./Ext. No.</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtmobile" runat="server" BorderColor="Black" MaxLength="256" CssClass="form-control"></asp:TextBox>
                                                                   
                                </div>                                                   
                            <div class="col-md-2">
                                <label style="width: 200px">Email. Id.</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtemail" runat="server"  BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                              <div class="col-md-2">
                                <label style="width: 200px">Remarks.</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtremark" runat="server" TextMode="MultiLine" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                            </div>

                        </div>
                                <div class="form-group">
                                    <div class="col-md-2" style="width: 200px">
                                        <label style="width: 200px"></label>
                                    </div>
                                    <div class="col-md-4" style="visibility: hidden">
                                        <asp:TextBox ID="txtID" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>           
            <div>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;               
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" Text="Export To Exel" OnClick="Button1_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="btn_Reset" runat="server" Text="Reset" OnClick="btn_Reset_Click" />
           
                 </div>   
    <br />    
    <div style="height: 300px; width: 1190px; overflow: scroll;">
        <asp:GridView ID="grdissuetracker" runat="server" DataKeyNames="ID" PageSize="10" AllowPaging="true" OnPageIndexChanging="grdissuetracker_PageIndexChanging" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
            BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
            GridLines="Horizontal" Font-Bold="true" EmptyDataText="There are no data records to display."
            AllowSorting="true">
            <PagerSettings Mode="Numeric" Position="Bottom" />
            <PagerStyle BackColor="LightBlue" Height="30px" VerticalAlign="Bottom" HorizontalAlign="Center" CssClass="csspager" />

            <AlternatingRowStyle BackColor="#F7F7F7" />
            <Columns>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>

                        <asp:LinkButton ID="btnselect" runat="server" Text="Select" Width="80px" OnClick="btnselect_Click" />
                    </ItemTemplate>

                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sl. No.">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Employee Code">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="200px" Text='<%# Eval("Employee_Code") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Employee Name">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="200px" Text='<%# Eval("Employee_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />

                </asp:TemplateField>
               <%-- <asp:TemplateField HeaderText="Priority">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="200px" Text='<%# Eval("Priority") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="Employee Department">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="200px" Text='<%# Eval("Employee_Dept") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Complaint Detail">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="200px" Text='<%# Eval("Query") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Involve Department">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="200px" Text='<%# Eval("Involve_Dept") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Email_Id">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="200px" Text='<%# Eval("Email_ID") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Issue Status">
                    <ItemTemplate>

                        <asp:Label runat="server" Width="200px" Text='<%# Eval("IssueStatus") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Assign To">
                    <ItemTemplate>

                        <asp:Label runat="server" Width="200px" Text='<%# Eval("AssignTo") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remarks">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="200px" Text='<%# Eval("Remarks") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Attachment1">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkPhoto" runat="server" ForeColor="Red" Font-Underline="true" Enabled='<%# Eval("Attachment_1").ToString() == "0" ? false : true %>' OnClick="lnkPhoto_Click"> View Photo</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
            <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
            <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
            <SortedAscendingCellStyle BackColor="#F4F4FD" />
            <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
            <SortedDescendingCellStyle BackColor="#D8D8F0" />
            <SortedDescendingHeaderStyle BackColor="#3E3277" />

        </asp:GridView>
    </div>
</asp:Content>

