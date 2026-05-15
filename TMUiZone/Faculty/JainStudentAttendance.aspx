<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="JainStudentAttendance.aspx.cs" Inherits="Faculty_JainStudentAttendance" %>

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
   
.JainStudentList {
  font-family: Arial, Helvetica, sans-serif;
  border-collapse: collapse;
  width: 100%;
}

.JainStudentList td, .JainStudentList th {
  border: 1px solid #ddd;
  padding: 8px;
}

.JainStudentList tr:nth-child(even){background-color: #f2f2f2;}

.JainStudentList tr:hover {background-color: #ddd;}

.JainStudentList th {
  padding-top: 12px;
  padding-bottom: 12px;
  text-align: left;
  background-color: #04AA6D;
  color: white;
}
</style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBodyHeader">
        <asp:Label ID="Label1" runat="server"
            Text="Jain Students Attendance List" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
    </fieldset>
      <fieldset id="status" runat="server" style="text-align: center;margin-bottom:10px">
    <div class="form-group" style="margin-top:20px;">

         <div class="col-md-2">
<div class="col-md-6" style="margin-top: 8px;">
    <asp:Label ID="Label2" runat="server" Text="Session:" Font-Bold="true" Font-Size="Larger"></asp:Label>
</div>
<div class="col-md-6" style="text-align: right">
    <asp:DropDownList ID="ddlSession" runat="server" Height="29px">
        <asp:ListItem Text="22-23" Value="22-23"></asp:ListItem>
        <asp:ListItem Text="23-24" Value="23-24"></asp:ListItem>
        <asp:ListItem Text="24-25" Value="24-25"></asp:ListItem>
        <asp:ListItem Text="25-26" Value="25-26" Selected="True"></asp:ListItem>
    </asp:DropDownList>
</div> 
 </div>
         <div class="col-md-2">
        <div class="col-md-6" style="margin-top: 8px;">
            <asp:Label ID="Label222" runat="server" Text="Month:" Font-Bold="true" Font-Size="Larger"></asp:Label>
        </div>
        <div class="col-md-6" style="text-align: right">
            <asp:DropDownList ID="ddlMonth" runat="server" Height="29px">
                <asp:ListItem Text="January" Value="1"></asp:ListItem>
                <asp:ListItem Text="February" Value="2"></asp:ListItem>
                <asp:ListItem Text="March" Value="3"></asp:ListItem>
                <asp:ListItem Text="April" Value="4"></asp:ListItem>
                <asp:ListItem Text="May" Value="5"></asp:ListItem>
                <asp:ListItem Text="June" Value="6"></asp:ListItem>
                <asp:ListItem Text="July" Value="7"></asp:ListItem>
                <asp:ListItem Text="August" Value="8"></asp:ListItem>
                <asp:ListItem Text="September" Value="9"></asp:ListItem>
                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                <asp:ListItem Text="December" Value="12"></asp:ListItem>
               </asp:DropDownList>
        </div> 
         </div>
         <div class="col-md-2">
<div class="col-md-6" style="margin-top: 8px;">
    <asp:Label ID="Label3" runat="server" Text="Year:" Font-Bold="true" Font-Size="Larger"></asp:Label>
</div>
<div class="col-md-6" style="text-align: right">
    <asp:DropDownList ID="ddlYear" runat="server" Height="29px">
   </asp:DropDownList>
</div> 
 </div>

        <div class="col-md-1">
            <asp:Button ID="BtnShow" runat="server" Text="Search" ForeColor="White" OnClick="BtnShow_Click" CssClass="form-control" BackColor="#ff9900" />
        </div>
         <div class="col-md-5">
             <asp:Button ID="btnexporttoexcel" runat="server" Text="Export To Excel" OnClick="btnexporttoexcel_Click" style="float: right;" />
        </div>
</fieldset> 
    <%--<asp:Button ID="btnBack" runat="server" Width="120px" ForeColor="White" Font-Size="Larger" Font-Bold="true" BackColor="Blue" OnClick="btnBack_Click" Text="Back to list" />
    --%>
    <asp:UpdatePanel ID="Orderlist" runat="server">
        <ContentTemplate>
            <div class="box box-default" style="background-color: rgba(255, 255, 255, 0.80);overflow: auto;height: 720px;">
                    <asp:GridView ID="JainStudentList"  EmptyDataText="No Record Found.." CssClass="footable JainStudentList" CellPadding="5" CellSpacing="0" runat="server" style="white-space:nowrap;">
                   <Columns>
                        <asp:TemplateField HeaderText="No" ItemStyle-Width="100">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# (Container.DataItemIndex + 1)%>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                        </asp:GridView>

                <!-- /.col -->

                <div class="box-footer">
                </div>
            </div>
        </ContentTemplate>
        </asp:UpdatePanel>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>
  </asp:Content>

