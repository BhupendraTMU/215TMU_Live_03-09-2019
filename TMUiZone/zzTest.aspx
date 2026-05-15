<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="zzTest.aspx.cs" Inherits="zzTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="js/jquery-1.11.0.min.js" type="text/javascript"></script>
        <script src="js/bootstrap.min.js" type="text/javascript"></script
        <script src="js/wow.min.js" type="text/javascript"></script>
    <link href="bootstrap/css/bootstrap.css" rel="stylesheet" />

    <style>
     .disable {
            border:1px solid red;
        }
        </style>
      <script type="text/javascript">
          $(document).ready(function () {
              debugger;
              $('[id$=btnSave]').click(function () {
                  $('[id$=btnSave]').hide;
                 // $('[id$=btnSave]').addClass("redBorder");
                 
              });

          });
    </script>   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:TextBox ID="txtname" runat="server"></asp:TextBox>
    <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="Save" />
</asp:Content>

