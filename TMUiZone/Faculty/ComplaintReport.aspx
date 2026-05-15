<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="ComplaintReport.aspx.cs" Inherits="Faculty_ComplaintReport" %>

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
    <div style="width:500px">
        <asp:Button ID="btnBack" runat="server" Width="120px" ForeColor="White" Font-Size="Larger" Font-Bold="true" BackColor="Blue" OnClick="btnBack_Click" Text="Back to list" />
        <asp:Button ID="btnExport" runat="server" Width="130px" ForeColor="White" Font-Size="Larger" Font-Bold="true" BackColor="Green" OnClick="btnExport_Click" Text="Export to Excel" />
    </div>

    <asp:DropDownList ID="drpHostel" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="drpHostel_SelectedIndexChanged">
    </asp:DropDownList>

    <asp:UpdatePanel ID="Orderlist" runat="server">
        <ContentTemplate>
            <div class="box box-default" style="background-color: rgba(255, 255, 255, 0.80);">





                <%--DataKeyNames="Complaint_No"--%>

                <asp:GridView ID="OrderList1" DataKeyNames="Department" EmptyDataText="No Record Found.." CssClass="footable" CellPadding="5" CellSpacing="0" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Sl. No.">

                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle Width="2%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Department" ItemStyle-Width="180px" HeaderText="Department" />

                        <asp:TemplateField HeaderText="Total Complaint">
                            <ItemTemplate>

                                <asp:LinkButton ID="lnkT" ForeColor="Blue" Text='<%# Eval("Total Complaint") %>' runat="server" OnClick="lnkT_Click"></asp:LinkButton>

                            </ItemTemplate>

                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Pending at Warden">
                            <ItemTemplate>

                                <asp:LinkButton ID="lnkPW" ForeColor="Blue" Text='<%# Eval("Pending at Warden") %>' runat="server" OnClick="lnkPW_Click"></asp:LinkButton>

                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pending at Department">
                            <ItemTemplate>

                                <asp:LinkButton ID="lnkPD" ForeColor="Blue" Text='<%# Eval("Pending at Department") %>' runat="server" OnClick="lnkPD_Click"></asp:LinkButton>

                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rejected by Warden">
                            <ItemTemplate>

                                <asp:LinkButton ID="lnkRD" ForeColor="Blue" Text='<%# Eval("Rejected by Warden") %>' runat="server" OnClick="lnkRD_Click"></asp:LinkButton>

                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Resolve by Department">
                            <ItemTemplate>

                                <asp:LinkButton ID="lnkResolved" ForeColor="Blue" Text='<%# Eval("Resolved from Department") %>' runat="server" OnClick="lnkResolved_Click"></asp:LinkButton>

                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Close">
                            <ItemTemplate>

                                <asp:LinkButton ID="lnkClose" ForeColor="Blue" Text='<%# Eval("Close") %>' runat="server" OnClick="lnkClose_Click"></asp:LinkButton>

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
                            <div class="box box-default" style="background-color: rgba(255, 255, 255, 0.80);">







                                <asp:GridView ID="GridView1" DataKeyNames="Complaint_No" EmptyDataText="No Record Found.." CssClass="footable" CellPadding="5" CellSpacing="0" runat="server" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl. No.">

                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle Width="2%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Complaint_No" ItemStyle-Width="80px" HeaderText="Complaint No" />
                                        <asp:BoundField DataField="Department" HeaderText="Department" ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="Area" HeaderText="Area" />

                                        <asp:BoundField DataField="Status" HeaderText="Status" />
                                        <asp:BoundField DataField="Room" HeaderText="Room No" ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="CreateDate" HeaderText="CreateDate" />
                                        <asp:BoundField DataField="AttemptDate" HeaderText="Warden Approval Date" />
                                        <asp:BoundField DataField="DRD" HeaderText="Resolve Date" />
                                        <asp:BoundField DataField="CloseDate" HeaderText="Close Date" />
                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" />



                                    </Columns>
                                </asp:GridView>

                                <!-- /.col -->

                                <div class="box-footer">
                                </div>
                            </div>

                        </ContentTemplate>

                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

    </fieldset>









</asp:Content>

