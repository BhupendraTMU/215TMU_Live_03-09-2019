<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="ExtraDuty.aspx.cs" Inherits="Faculty_ExtraDuty" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="text-align: center; width: 100%" id="logoDiv">
        <table style="width: 80%; margin-left: 10%;">
            <tr>
                <td style="width: 200px;" align="left">
                    <img src="~/images/rightlogo.png" id="Image1" runat="server" width="100" height="102" visible="true" />
                </td>
                <td style="width: 80%; vertical-align: middle" align="left">
                    <asp:Label ID="LblTitle" runat="server" Text="Teerthanker Mahaveer University,Moradabad" Style="font-size: 25px;"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="panel-heading" style="background-color: #2b5b69">
        <center>
            <div class="panel-title" style="fit-position: center;">
                <b>
                    <p style="color: white; font-size: 22px">
                        Form to Claim Extra Duty &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    </p>
                </b>
            </div>
        </center>
    </div>
    <fieldset class="boxBodyInner">

        <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
            <div class="form-group">
                <asp:Label ID="Label1" Style="line-height: 30px" runat="server" Text="Label"><b>Name</b></asp:Label>
            </div>
        </div>
        <div class="col-lg-4" style="padding-right: 0px;">
            <div class="form-group">
                <div class="input-group">

                    <asp:TextBox ID="txtName" CssClass="form-control input-sm" placeholder="Applicant Name" Width="300px" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtName" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
            <div class="form-group">
                <asp:Label ID="Label2" Style="line-height: 30px" runat="server" Text="Label"><b>Designation</b></asp:Label>
            </div>
        </div>
        <div class="col-lg-4" style="padding-right: 0px;">
            <div class="form-group">
                <div class="input-group">

                    <asp:TextBox ID="txtDesignation" CssClass="form-control input-sm" placeholder="Designation" runat="server" Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDesignation" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
            <div class="form-group">
                <asp:Label ID="Label3" Style="line-height: 30px" runat="server" Text="Label"><b>College/Branch of posting</b></asp:Label>
            </div>
        </div>
        <div class="col-lg-4" style="padding-right: 0px;">
            <div class="form-group">
                <div class="input-group">

                    <asp:TextBox ID="txtBranch" CssClass="form-control input-sm" placeholder="State" runat="server" Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBranch" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
            <div class="form-group">
                <asp:Label ID="Label4" Style="line-height: 30px" runat="server" Text="Label"><b>Duty Type</b></asp:Label>
            </div>
        </div>
        <div class="col-lg-4" style="padding-right: 0px;">
            <div class="form-group">
                <div class="input-group">

                    <asp:DropDownList ID="drpDutyType" runat="server" Width="300px" Height="30px" CssClass="form-control input-sm">
                        <asp:ListItem Text="Duty Based" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Hours Based" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
            <div class="form-group">
                <asp:Label ID="Label5" Style="line-height: 30px" runat="server" Text="Label"><b>Date on Which Extra duty Performed </b></asp:Label>
            </div>
        </div>
        <div class="col-lg-4" style="padding-right: 0px;">
            <div class="form-group">
                <div class="input-group">

                      <asp:TextBox ID="txtExtraDuty" CssClass="form-control input-sm" placeholder="State" runat="server" Width="300px"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtExtraDuty" Format="dd MMM yyyy">
                                </cc1:CalendarExtender>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtExtraDuty" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                

                </div>
            </div>
        </div>
         <div class="col-lg-2" style="padding-right: 0px; font-size: 12px;">
            <div class="form-group">
                <asp:Label ID="Label6" Style="line-height: 30px" runat="server" Text="Label"><b>Scheduled Duty</b></asp:Label>
            </div>
        </div>
        <div class="col-lg-4" style="padding-right: 0px;">
            <div class="form-group">
                <div class="input-group">

                    <asp:TextBox ID="txtScheduleDuty" CssClass="form-control input-sm" placeholder="State" runat="server" Width="300px"></asp:TextBox>
                 
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtScheduleDuty" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                
                </div>
            </div>
        </div>







    </fieldset>




</asp:Content>

