<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="Hostel.aspx.cs" Inherits="Faculty_Hostel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <asp:GridView ID="grdHostel" runat="server" AutoGenerateColumns="False">
    <Columns>
        <asp:BoundField DataField="StudentNo" HeaderText="Student No" />
<asp:BoundField DataField="StudentName" HeaderText="Student Name" />
<asp:BoundField DataField="PunchTime" HeaderText="Punch Time" />
<asp:BoundField DataField="RoomNo" HeaderText="Room No" />
    </Columns>
</asp:GridView>

</asp:Content>

