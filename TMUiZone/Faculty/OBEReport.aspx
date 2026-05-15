<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="OBEReport.aspx.cs" Inherits="Faculty_OBEReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        .column {
            border: 1px solid #000;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 500px;
            height: 200px;
        }
    </style>
    <style type="text/css">
        .GridPager a, .GridPager span {
            display: block;
            height: 15px;
            width: 15px;
            font-weight: bold;
            text-align: center;
            text-decoration: none;
        }

        .GridPager a {
            background-color: #f5f5f5;
            border: 1px solid #969696;
        }

        .GridPager span {
            background-color: #A1DCF2;
            border: 1px solid #3AC0F2;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="OBE Report" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>


    <div class="col-sm-2 p-0">
        <div class="form-group clearfix">
            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Academic Year</label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpAcademicYear" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
            <div>
                <asp:DropDownList ID="drpAcademicYear" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpAcademicYear_SelectedIndexChanged" Width="180px"></asp:DropDownList>

            </div>
        </div>
    </div>

    <div class="col-sm-2 p-0">
        <div class="form-group clearfix">
            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Program</label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpCourse" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
            <div>
                <asp:DropDownList ID="drpCourse" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged" Width="180px">
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="col-sm-2 p-0">
        <div class="form-group clearfix">
            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Semester/Year</label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpSemester" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
            <div>
                <asp:DropDownList ID="drpSemester" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="drpSemester_SelectedIndexChanged" Width="180px">
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="col-sm-2 p-0">
        <div class="form-group clearfix">
            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Course</label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="ddlSubject" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
            <div>
                <asp:DropDownList ID="ddlSubject" runat="server" AutoPostBack="true" CssClass="form-control" Width="180px">
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="col-sm-2 p-0">
        <div class="form-group clearfix">
            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Percentage</label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtPer" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
            <div>
                <asp:TextBox ID="txtPer" runat="server" AutoPostBack="true" CssClass="form-control" Width="180px">
                </asp:TextBox>
            </div>
        </div>
    </div>
    <div class="col-sm-2 p-0">
        <div class="form-group clearfix">
            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px; visibility: hidden">&nbsp&nbsp&nbsp Course</label>

            <div>
                <asp:Button ID="btnShow" runat="server" class="btn btn-info" ValidationGroup="g1" OnClick="btnShow_Click" Height="30px" Width="200px" Text="Generate Report" />
            </div>
        </div>
    </div>
    <div class="col-sm-12 p-0" align="center">
        <div id="pdfContainer">
            <iframe id="pdfViewer" width="100%" height="600px" src="<%= ViewState["PDFFilePath"] %>"></iframe>
        </div>
        <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
    </div>



</asp:Content>

