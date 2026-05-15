<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentGrievances.aspx.cs" Inherits="Student_StudentGrievances" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript" src="../bootstrap/js/jquery-1.11.2.min.js"></script>
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../bootstrap/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
                      <fieldset  style="background:#fefefe; border-top:1px solid #dde0e8; border-bottom:1px solid #dde0e8; padding:10px 20px; height:100%">
                    <asp:GridView ID="grdActionTakenReport" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" OnPageIndexChanging="grdActionTakenReport_PageIndexChanging"  EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="10">
               <AlternatingRowStyle BackColor="#F7F7F7" />
                        <Columns>                            
                            <asp:BoundField DataField="Date Commited" HeaderText="Date Commited" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Staff Code" HeaderText="Staff Code" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Action Taken" HeaderText="Action Taken" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Fine Amount" HeaderText="Fine Amount" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                        </Columns>                       
                        <AlternatingRowStyle CssClass="danger" />
                    </asp:GridView>
    </fieldset>
</asp:Content>

