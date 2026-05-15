<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="FacultyLoad.aspx.cs" Inherits="Faculty_FacultyLoad" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:UpdatePanel runat="server" ID="updmain">
        <ContentTemplate>
            <asp:Panel ID="pnlList" runat="server" BorderWidth="2px" BorderColor="#ACE9FB" ScrollBars="Vertical">
                 <div class="parent" style="background-color: #ff6a00">
        <br />
        <table style="fit-position: left;   padding-top:15px;">
            <tr>
                <td width="15%" align="left">
                 &nbsp&nbsp&nbsp&nbsp<asp:Label ID="Label1" runat="server"  Visible="true" Text="Faculty Load" Font-Size="15pt" ForeColor="#093A62" 
                     Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>                  
                </td>
                <td width="15%" align="left">                  
                </td>
                <td width="20%">
                    <b>
                                <asp:TextBox runat="server" ID="txtFromtDate" CssClass="form-control input-sm" Width="150px" ToolTip="From Date" onkeypress="return false"
                                    onKeyDown="preventBackspace();" placeholder="From Date"></asp:TextBox>&nbsp&nbsp&nbsp&nbsp
                                <asp:Image src="../Images/Calendar.png" runat="server" Height="30px" Width="30px" alt="" ID="fdate" />
                               <asp:CalendarExtender ID="cleFromDate" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDate" CssClass="cal_Theme1"
                                    PopupButtonID="fdate" Enabled="true" TargetControlID="txtFromtDate" />&nbsp&nbsp&nbsp&nbsp
                        

                            </b>
                </td>
                <td width="20%">
                    <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control input-sm" Width="150px" ToolTip="To Date" onkeypress="return false" 
                                    onKeyDown="preventBackspace();" placeholder="To Date"></asp:TextBox>&nbsp&nbsp&nbsp&nbsp
                                <asp:Image src="../Images/Calendar.png" runat="server" Height="30px" Width="30px" alt="" ID="tdate" />
                               <asp:CalendarExtender ID="cleToDate" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDate" CssClass="cal_Theme1"
                                    PopupButtonID="tdate" Enabled="true" TargetControlID="txtToDate" />&nbsp&nbsp&nbsp&nbsp                    
                </td>
                <td width="20%">                    
                <asp:DropDownList runat="server" ID="ddlFaculty" Width="250px" Height="30px" ></asp:DropDownList>                   
                </td>
                <td width="10%">                    
                     <asp:Button id="BtnShow" type="button" Text="Show" class="btn btn-info btn" runat="server" OnClick="BtnShow_Click" >                                                          
                                                            </asp:Button>
                </td>
            </tr>
        </table>
        <br />
    </div>
                </div>
                </center>
                        </div>

         <div class="table-responsive">
             <asp:GridView ID="grdLoad" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="true"
                 EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="50" OnPageIndexChanging="grdLoad_PageIndexChanging" OnDataBound="grdLoad_DataBound" OnRowDataBound="grdLoad_RowDataBound" OnRowCreated="grdLoad_RowCreated">
                 <%--<AlternatingRowStyle CssClass="danger" />--%>
                 <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                 <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" PreviousPageText="Previous" NextPageText="Next" FirstPageText="First" LastPageText="Last" Position="Bottom" />
             </asp:GridView>
             <br />

         </div>
            </asp:Panel>
        </ContentTemplate>
        <%--<Triggers >
            <asp:PostBackTrigger ControlID ="ddlFaculty" />
              <asp:AsyncPostBackTrigger ControlID ="BtnShow" EventName="Click" />
        </Triggers>--%>
    </asp:UpdatePanel>
    
</asp:Content>

