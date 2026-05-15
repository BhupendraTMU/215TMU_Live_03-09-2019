<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="NaacDashboard.aspx.cs" Inherits="Faculty_NaacDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .red-border {
            border: 1px solid red;
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

            ./*JainStudentList tr:nth-child(even) {
                background-color: #f2f2f2;
            }*/

            .JainStudentList tr:hover {
                background-color: #ddd;
            }

            .JainStudentList th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #04AA6D;
                color: white;
            }

        .btn {
            padding: 4px 10px;
            border-radius: 4px;
            color: #fff;
            font-size: 13px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="filter-metrics-wrapper"
        style="margin-bottom: 15px; padding: 10px; border: 1px solid #ddd; background: #f9f9f9;">

        <!-- Academic Year -->
        <div style="float: left; margin-right: 20px;">
            <label><b>Academic Year</b></label><br />
            <asp:DropDownList ID="ddlAcademicYear" runat="server" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged" AutoPostBack="true"
                CssClass="form-control" Width="150px">
            </asp:DropDownList>
        </div>

        <!-- Metric No -->
        <div style="float: left; margin-right: 20px;">
            <label><b>Metric No</b></label><br />
            <asp:DropDownList ID="drpMetric" runat="server"
                CssClass="form-control" Width="350px">
            </asp:DropDownList>
        </div>

        <!-- Submit Button -->
        <div style="float: left; margin-right: 20px; margin-top: 22px;">
            <asp:Button ID="btnSubmit" runat="server"
                Text="SHOW"
                CssClass="btn btn-primary"
                Width="150px"
                OnClick="btnSubmit_Click" />
        </div>
        <div style="float: left; margin-right: 20px; margin-top: 22px;">
            <asp:Button ID="btnReport" runat="server"
                Text="Report"
                CssClass="btn btn-primary"
                Width="150px"
                OnClick="btnReport_Click" />
        </div>
        <div style="clear: both;"></div>
    </div>
    <div style="height: 800px; overflow: scroll">

        <asp:GridView ID="JainStudentList" runat="server" CssClass="JainStudentList" AutoGenerateColumns="false" BackColor="White" BorderColor="#0000ff"
            EmptyDataText="There are no data records to display." BorderStyle="Solid" BorderWidth="2px" CellPadding="3" Width="1130px" OnRowDataBound="JainStudentList_RowDataBound"
            GridLines="Horizontal" ShowFooter="true">
            <Columns>
                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <%# Container.DataItemIndex +1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Metric No" ItemStyle-Width="2%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lblMetricNo" runat="server" Text='<%# Bind("[MetricNo]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Metric" ItemStyle-Width="12%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lblMetric" runat="server" Width="400px" Text='<%# Bind("[Metric]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Target Value" ItemStyle-Width="1%" ItemStyle-BackColor="#99ccff" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lblTarget" runat="server" Text='<%# Bind("[Target Value]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Jan" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lblJanuary" runat="server" Text='<%# Bind("[January]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Feb" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lblFebruary" runat="server" Text='<%# Bind("[February]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="March" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lblMarch" runat="server" Text='<%# Bind("[March]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="April" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lblApril" runat="server" Text='<%# Bind("[April]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="May" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lblMay" runat="server" Text='<%# Bind("[May]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="June" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lblJune" runat="server" Text='<%# Bind("[June]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="July" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lblJuly" runat="server" Text='<%# Bind("[July]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Aug" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lblAugust" runat="server" Text='<%# Bind("[August]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Sept" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lblSeptember" runat="server" Text='<%# Bind("[September]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Oct" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lblOctober" runat="server" Text='<%# Bind("[October]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Nov" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lblNovember" runat="server" Text='<%# Bind("[November]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Dec" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lblDecember" runat="server" Text='<%# Bind("[December]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cumulative" ItemStyle-Width="2%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lblCumulative" runat="server" Text='<%# Bind("[Cumulative]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>




            </Columns>
            
        </asp:GridView>
    </div>
</asp:Content>

