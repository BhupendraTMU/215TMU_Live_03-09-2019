<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="JobWork.aspx.cs" Inherits="Student_JobWork" %>


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
            Text="Maintenance List" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>



    </fieldset>
    <asp:Button ID="btnAddNew" OnClick="btnAddNew_Click" runat="server" ForeColor="White" Font-Size="Larger" Font-Bold="true" BackColor="Blue" Width="120px" Text="Add New" />
    <asp:Button ID="btnBack" runat="server" Width="120px" ForeColor="White" Font-Size="Larger" Font-Bold="true" BackColor="Blue" OnClick="btnBack_Click" Text="Back to list" />

    <asp:UpdatePanel ID="Orderlist" runat="server">
        <ContentTemplate>
            <div class="box box-default" style="background-color: rgba(255, 255, 255, 0.80);">







                <asp:GridView ID="OrderList1" DataKeyNames="Complaint_No" EmptyDataText="No Record Found.." CssClass="footable" CellPadding="5" CellSpacing="0" runat="server" OnRowDataBound="OrderList1_RowDataBound" AutoGenerateColumns="false">
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
                         <asp:BoundField DataField="CustomerMobile" HeaderText="Mobile No" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />

                        <asp:BoundField DataField="CreateDate" HeaderText="CreateDate" />
                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" />



                        <asp:BoundField DataField="CloseDate" HeaderText="CloseDate" />
                      
                        <asp:TemplateField HeaderText="Attachment">
                            <ItemTemplate>

                                <asp:LinkButton ID="lnkAttachment" CommandArgument='<%# Eval("Complaint_No") %>' ForeColor="Blue" Text="Download" Visible='<%# (Eval("AttachmentFilename")).ToString().ToUpper() == "" ? false:true %>' runat="server" OnClick="lnkAttachment_Click"></asp:LinkButton>

                            </ItemTemplate>

                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Resolved Attchment">
                            <ItemTemplate>

                                <asp:LinkButton ID="lnkAttachment1" CommandArgument='<%# Eval("Complaint_No") %>' ForeColor="Blue" Text="Download" Visible='<%# (Eval("AttachmentFilename1")).ToString().ToUpper() == "" ? false:true %>' runat="server" OnClick="lnkAttachment1_Click"></asp:LinkButton>

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
                                        <label style="width: 200px">Department Name :</label>
                                    </div>
                                    <div class="col-md-4">
                                         <asp:DropDownList ID="ddlDepartment" ForeColor="Black" Width="320px" Font-Bold="true" runat="server" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true" ></asp:DropDownList>
                                        
                                       <asp:HiddenField ID="hfWardenMobile" runat="server" />
                                    </div>
                                      <div class="col-md-2" style="width: 150px">
                                        <label>Room No :</label>
                                    </div>
                                    <div class="col-md-4">
                                         <asp:TextBox ID="txtRoomNo" ForeColor="Black" Font-Bold="true" Width="320px" Enabled="false" runat="server"  CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <%--<div class="col-md-2" style="width: 150px">
                                        <label>Department Name :</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlDepartment" ForeColor="Black" Width="320px" Font-Bold="true" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="ddlDepartment_TextChanged"></asp:DropDownList>
                                    </div>--%>
                                </div>

                                 <div class="form-group">
                                    <div class="col-md-2" style="width: 200px">
                                        <label style="width: 200px">Warden Name :</label>
                                    </div>
                                    <div class="col-md-4">
                                         <asp:TextBox ID="txtWarden" ForeColor="Black" Font-Bold="true" Width="320px" Enabled="false" runat="server"  CssClass="form-control"></asp:TextBox>
                                         <asp:HiddenField ID="hfWardenEmployee" runat="server" />
                                    </div>                                     
                                    <div class="col-md-2" style="width: 150px">
                                        <label>Area/Building :</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtArea" ForeColor="Black" Font-Bold="true" Enabled="false" Width="320px" runat="server"  CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <div class="col-md-2" style="width: 200px">
                                        <label style="width: 200px">Remark :</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtRemark" ForeColor="Black" Font-Bold="true" Width="320px" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>




                                    </div>

                                    <div class="col-md-2" style="width: 150px">
                                        <label>Attachment :</label>
                                    </div>
                                    <div class="col-md-4" style="margin-bottom: 20px">
                                        <asp:FileUpload ID="flUpload" runat="server" ForeColor="Black" Width="320px" Font-Bold="true"  CssClass="form-control"></asp:FileUpload>
                                           &nbsp&nbsp&nbsp
                                        <asp:Label ID="fileName" runat="server" Width="200px"  ForeColor="Red" Font-Size="Larger"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-2" style="width: 200px">
                                        <label style="width: 200px"></label>
                                    </div>
                                    <div class="col-md-4" style="margin-bottom: 20px">
                                        <asp:TextBox ID="txtOTP" ForeColor="Black" Font-Bold="true" Width="120px" runat="server" CssClass="form-control"></asp:TextBox>
                                        &nbsp&nbsp&nbsp
                                        <asp:LinkButton ID="lnkGenerateOTP" runat="server" ForeColor="Green" Font-Size="Larger"  OnClick="lnkGenerateOTP_Click" Text="Generate OTP"></asp:LinkButton>
                                    </div>

                                    <div class="col-md-2" style="width: 150px">
                                        <label style="width: 200px"></label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Button ID="btnSubmit" runat="server" ForeColor="White" Width="320px" Font-Size="Larger" Font-Bold="true" BackColor="Blue" Text="Submit" OnClick="btnSubmit_Click" CssClass="form-control"></asp:Button>
                                    </div>
                                </div>

                            </div>
                        </ContentTemplate>
                          <Triggers>

                            <asp:PostBackTrigger ControlID="btnSubmit" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

    </fieldset>
</asp:Content>

