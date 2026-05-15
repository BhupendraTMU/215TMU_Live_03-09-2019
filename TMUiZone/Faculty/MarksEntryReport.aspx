<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="MarksEntryReport.aspx.cs" Inherits="MarksEntryReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server" 
            Text="Marks Entry Faculty Report" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
    </fieldset>
    <br />
    <div class="row mr-10 ml-10">
        <asp:UpdatePanel ID="mrak" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="Panlehid" class="label-responsive" runat="server">
                    <div class="col-md-12 p-0 clearfix">
                        <div class="col-md-6 p-0">
                            <div class="col-sm-3">
                                <label>Course</label>
                            </div>
                            <div class="col-sm-7">
                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlcourse" OnSelectedIndexChanged="ddlcourse_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlcourse" Display="Dynamic" ErrorMessage="Select Academic Year" InitialValue="--Select--" ForeColor="Red" ValidationGroup="SL1"></asp:RequiredFieldValidator>

                            </div>
                        </div>
                        <div class="col-md-6 p-0">
                            <div class="col-sm-3">
                                <label>Faculty</label>
                            </div>
                            <div class="col-sm-7">
                                <asp:DropDownList ID="drpFaculty" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpFaculty" Display="Dynamic" ErrorMessage="Select Semester"  ForeColor="Red" ValidationGroup="SL1"></asp:RequiredFieldValidator>

                            </div>
                            <div class="col-sm-1 ">
                                <asp:Button ID="btnShow" runat="server" CssClass="btn btn-success" Text="SHOW" />
                            </div>

                        </div>


                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

