<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="ComplaintList.aspx.cs" Inherits="Faculty_ComplaintList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="js/36d/jquery-1.4.2.min.js"></script>

    <meta name="viewport" content="width = device-width, initial-scale = 1.0, minimum-scale = 1.0, maximum-scale = 1.0, user-scalable = no" />
    <!-- Bootstrap 3.3.5 -->
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
    <!-- Font Awesome -->
    <link href="css/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />

    <!-- Theme style -->
    <link rel="stylesheet" href="dist/css/AdminLTE.min.css">
    <link rel="stylesheet" href="dist/css/skins/_all-skins.min.css">
    <link href="css/CloudeStyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="dist/css/style.css" rel="stylesheet" />
    <script src="js/CloudeJScript.js" type="text/javascript"></script>
    <link href="css/gridstyle.css" rel="stylesheet" />
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBodyHeader">
        <asp:Label ID="Label1" runat="server"
            Text="Complaint List" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
    </fieldset>
    <asp:Button ID="btnBack" runat="server" Width="120px" ForeColor="White" Font-Size="Larger" Font-Bold="true" BackColor="Blue" OnClick="btnBack_Click" Text="Back to list" />
      <fieldset id="status" runat="server" style="text-align: center">
    <div class="form-group" style="margin-top:20px;">
        <div class="col-md-2" style="margin-top: 8px;">
            <asp:Label ID="Label222" runat="server" Text="Department:" Font-Bold="true" Font-Size="Larger"></asp:Label>
        </div>
        <div class="col-md-2" style="text-align: right">
            <asp:HiddenField ID="getval" Value="0"  runat="server"/>
           <asp:DropDownList ID="ddlDept" runat="server" Height="29px">
               </asp:DropDownList>
        </div>
        <div class="col-md-1">
            <asp:Button ID="BtnShowReport" runat="server" Text="Search" ForeColor="White" OnClick="lnkbutton_Click1xx" CssClass="form-control" BackColor="#ff9900" />
        </div> 
         <div class="col-md-3">

         </div>
          <div class="col-md-1">
             <asp:Button ID="Button1" runat="server" Text="Done" ForeColor="White" OnClick="Donebutton_Click" CssClass="form-control" BackColor="Green" />
         </div> 
                 <div class="col-md-1">
             <asp:Button ID="Button2" runat="server" Text="Pending" ForeColor="White" OnClick="Pendingbutton_Click" CssClass="form-control" BackColor="Red" />
         </div> 
        <div class="col-md-2" style="text-align: right">
            <asp:Button ID="btnexporttoexcel" runat="server" Text="Export To Excel" OnClick="btnexporttoexcel_Click" />
        </div>
    </div>
</fieldset> 
    <%--<asp:Button ID="btnBack" runat="server" Width="120px" ForeColor="White" Font-Size="Larger" Font-Bold="true" BackColor="Blue" OnClick="btnBack_Click" Text="Back to list" />
    --%>
    <asp:UpdatePanel ID="Orderlist" runat="server">
        <ContentTemplate>
            <div class="box box-default" style="background-color: rgba(255, 255, 255, 0.80);">







                <asp:GridView ID="OrderList1" OnPageIndexChanging="OrderList1_PageIndexChanging1" DataKeyNames="Complaint_No" EmptyDataText="No Record Found.." CssClass="footable" CellPadding="5" CellSpacing="0" OnRowDataBound="OrderList1_RowDataBound" runat="server" AutoGenerateColumns="false">
                                <PagerSettings Mode="NumericFirstLast" />
                                <PagerStyle CssClass="csspager" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl. No.">

                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle Width="2%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Complaint_No" ItemStyle-Width="180px" HeaderText="Complaint No" />
                        <asp:BoundField DataField="Department" HeaderText="Department" ItemStyle-Width="25%" />
                        <asp:BoundField DataField="Area" HeaderText="Area" />

                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:BoundField DataField="Room" HeaderText="Room No" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="CreateDate" HeaderText="CreateDate" />
                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" />



                        <%--   <asp:BoundField DataField="CloseDate" HeaderText="CloseDate" />--%>

                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>

                                <asp:LinkButton ID="lnkSelect" CommandArgument='<%# Eval("Complaint_No") %>' ForeColor="Blue" Text="Action" runat="server" OnClick="lnkSelect_Click"></asp:LinkButton>

                            </ItemTemplate>

                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>

                <!-- /.col -->

                <div class="box-footer">
                </div>
            </div>
        </ContentTemplate>
        <Triggers>

            <asp:PostBackTrigger ControlID="OrderList1" />
        </Triggers>
    </asp:UpdatePanel>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>
    <script type="text/javascript">
        $(function () {

            $('[id*=OrderList1]').footable();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        $("[id*=OrderList1]").footable();
                    }
                });
            };
        });
    </script>




    <fieldset class="boxBodyInner" id="main" runat="server" visible="false">
        <div class="form-horizontal">
            <div class="box-body">
                <div class="row">
                    <asp:UpdatePanel ID="Panel" runat="server">
                        <ContentTemplate>


                            <div class="col-md-12">

                                <div class="form-group">
                                    <div class="col-md-2" style="width: 200px">
                                        <label style="width: 200px">Customer Code :</label>
                                        <asp:HiddenField ID="hfCo_Head" runat="server" />
                                        <asp:HiddenField ID="hfCustNAme" runat="server" />
                                        <asp:HiddenField ID="hfWarden" runat="server" />
                                        <asp:HiddenField ID="hfHead" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtCustCode" ForeColor="Black" Width="320px" Font-Bold="true" runat="server" Enabled="false" CssClass="form-control" AutoPostBack="true"></asp:TextBox>

                                    </div>

                                    <div class="col-md-2" style="width: 150px">
                                        <label>Customer Name :</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtCustName" ForeColor="Black" Font-Bold="true" Width="320px" runat="server" Enabled="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                        <asp:HiddenField ID="hdfCustomerMobile" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-2" style="width: 200px">
                                        <label style="width: 200px">Room No :</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtRoom" ForeColor="Black" Width="320px" Font-Bold="true" runat="server" Enabled="false" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                    </div>

                                    <div class="col-md-2" style="width: 150px">
                                        <label>Complaint No.</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtComplaint" ForeColor="Black" Font-Bold="true" Width="320px" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>



                                <div class="form-group">
                                    <div class="col-md-2" style="width: 200px">
                                        <label style="width: 200px">Department Name :</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtDept" ForeColor="Black" Width="320px" Font-Bold="true" runat="server" Enabled="false" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                        <asp:HiddenField ID="CoHeadNumber" runat="server" />
                                        <asp:HiddenField ID="HeadNumber" runat="server" />
                                    </div>

                                    <div class="col-md-2" style="width: 150px">
                                        <label>Area/Building :</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtArea" ForeColor="Black" Font-Bold="true" Width="320px" runat="server" Enabled="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                        <asp:HiddenField ID="hdfWardenMobile" runat="server" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-2" style="width: 200px">
                                        <label style="width: 200px">Customer Remark :</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtRemark" ForeColor="Black" Font-Bold="true" Width="320px" runat="server" Enabled="false" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="col-md-2" style="width: 150px">
                                        <label>Remark :</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtRemarks" ForeColor="Black" Font-Bold="true" Width="320px" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-2" style="width: 200px" runat="server" id="ATTACH">
                                        <label style="width: 200px">Attachment :</label>
                                    </div>
                                    <div class="col-md-4" runat="server" id="ATTACHUPLOAD">

                                        <asp:FileUpload ID="flUpload" runat="server" ForeColor="Black" Width="320px" Font-Bold="true" CssClass="form-control"></asp:FileUpload>

                                    </div>

                                    <div class="col-md-2" style="width: 200px">
                                        Customer Attachment : 
                                    </div>
                                    <div class="col-md-4">

                                        <asp:LinkButton ID="lnkAttachment" runat="server" ForeColor="Blue" Text="Download" Width="320px" Font-Bold="true" OnClick="lnkAttachment_Click"></asp:LinkButton>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-2" style="width: 200px">
                                        <label style="width: 200px"></label>
                                    </div>
                                    <div class="col-md-4" style="margin-bottom: 20px">
                                    </div>

                                    <div class="col-md-2" style="width: 150px">
                                        <label style="width: 200px"></label>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-5">
                                            <asp:Button ID="btnResolve" runat="server" ForeColor="White" Width="140px" Font-Size="Larger" Visible="false" Font-Bold="true" BackColor="Green" Text="Resolve Case" OnClick="btnResolve_Click" CssClass="form-control"></asp:Button>
                                            <asp:Button ID="btnSubmit" runat="server" ForeColor="White" Width="140px" Font-Size="Larger" Visible="false" Font-Bold="true" BackColor="Blue" Text="Approve Case" OnClick="btnSubmit_Click" CssClass="form-control"></asp:Button>
                                            <asp:Button ID="btnClose" runat="server" ForeColor="White" Width="140px" Font-Size="Larger" Visible="false" Font-Bold="true" BackColor="Green" Text="Close Case" OnClick="btnClose_Click" CssClass="form-control"></asp:Button>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Button ID="btnReject" runat="server" ForeColor="White" Width="140px" Font-Size="Larger" Visible="false" Font-Bold="true" BackColor="Red" Text="Reject" OnClick="btnReject_Click" CssClass="form-control"></asp:Button>
                                        </div>



                                    </div>
                                </div>

                            </div>
                        </ContentTemplate>
                        <Triggers>

                            <asp:PostBackTrigger ControlID="lnkAttachment" />
                            <asp:PostBackTrigger ControlID="btnSubmit" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

    </fieldset>





</asp:Content>

