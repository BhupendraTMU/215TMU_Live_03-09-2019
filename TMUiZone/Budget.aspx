<%@ Page Title="" Language="C#" MasterPageFile="~/IndexMaster.master" AutoEventWireup="true" CodeFile="Budget.aspx.cs" Inherits="Budget" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
function divexpandcollapse(divname) {
        var div = document.getElementById(divname);
        var img = document.getElementById('img' + divname);
        if (div.style.display == "none") {
            div.style.display = "block";
            img.src = "minus.gif";
        } else {
            div.style.display = "none";
            img.src = "plus.gif";
        }
    }
</script>
<style  type="text/css">
#GridScrollProfile 
{
width:950px;
height:100%;
overflow:scroll;
}
#rptBudgetGridDiv
{
width:950px;
height:100%;
overflow:scroll;
}
div {
    margin-bottom:10px;
    margin-top:10px;
}
table tr td.Middle  {
    vertical-align:middle;
}
table tr td.Margin  {
    margin:18px;
}
.HeadGridHeader {
    min-width:70px;
}
.ChildGridHeader {
    min-width:70px;
}
.TextBoxTextAlign {
    text-align:right;
    width:90%;
}
.radio {
display:inline-block;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<fieldset class="boxBody">
 <asp:Label ID="Label1" runat="server" Text="Budget" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
</fieldset>
<fieldset class="boxBody"> 
<table cellpadding="0px" cellspacing="0px">  <tr> <td  style="width:200px" valign="top"> 

    <table cellpadding="0px" cellspacing="0px" style="width:200px" >  <tr>  <td>

        <table cellpadding="0px" cellspacing="0px" >
             <tr> <td style="height:10px"> </td></tr>
             <tr> <td class="leftmMenu">  <img src="logo/Star.png" /> <asp:LinkButton ID="lnkBudgetEntry" runat="server" OnClick="lnkBudgetEntry_Click" > Enter Budget </asp:LinkButton></td></tr>
    
            <tr> <td style="height:10px"> </td></tr>
            <tr> <td class="leftmMenu">   <img src="logo/Star.png" /> <asp:LinkButton ID="lnkReview" runat="server" OnClick="lnkReview_Click" >Review Budget </asp:LinkButton></td></tr>

            <tr> <td style="height:10px"> </td></tr>
            <tr> <td class="leftmMenu">   <img src="logo/Star.png"/> <asp:LinkButton ID="lnkApproveBudget" runat="server" OnClick="lnkApproveBudget_Click"  >Approve Budget </asp:LinkButton></td></tr>
            
            <tr> <td style="height:10px"> </td></tr>
            <tr> <td class="leftmMenu">   <img src="logo/Star.png"/> <asp:LinkButton ID="lnkBudgetReport" runat="server" OnClick="lnkBudgetReport_Click"  >Budget Report </asp:LinkButton></td></tr>
        </table>
    </td> </tr></table>
    
</td>  <td valign="top" style="border-left:1px solid #dde0e8; padding-left:10px">
<asp:Panel ID ="pnlReview" runat="server" Visible="false">
    <table><tr>
            <td><asp:RadioButton ID="rbtnReView" runat="server" Checked="true" GroupName="ReviewType" Text="Review" AutoPostBack="true" OnCheckedChanged="rbtnReView_CheckedChanged"/></td><td>&nbsp;&nbsp;</td>
            <td><asp:RadioButton ID="rbtnReport" runat="server" GroupName="ReviewType" Text="Report" AutoPostBack="true" OnCheckedChanged="rbtnReView_CheckedChanged"/></td>
   </tr></table>
    <div class="leftm"> </div>
    <div style="height:13px"> </div>
</asp:Panel>
<asp:Panel ID="pnlEntry" runat="server" Visible="false">

    <asp:Label ID="Label2" runat="server" 
            Text="Enter Budget" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
  
    <div class="leftm"> </div>
    <div style="height:13px"> </div>
    <div style="width:100%">
        <span style="margin-right:1%">Budget</span> <asp:DropDownList ID="ddlBudget" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBudget_SelectedIndexChanged" ></asp:DropDownList>
        <span style="margin-left:1%; margin-right:1%"> Department </span><asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
        <span id="lblBudgetStatus" runat="server" style="margin-left:45%; ">BudgetStatus</span>
    </div>
    <div id="GridScrollProfile">
    <asp:GridView ID="GridViewHead" runat="server" ShowFooter="true" AutoGenerateColumns="false"  ForeColor="#333333" GridLines="None" Width="1800px" OnRowDataBound="GridViewHead_RowDataBound" OnRowCreated="GridViewHead_RowCreated" >
    <Columns>
        <asp:TemplateField ItemStyle-Width="10px" >
            <ItemTemplate>
                <a href="JavaScript:divexpandcollapse('div<%# Eval("GLRange") %>');">
                    <img id='imgdiv<%# Eval("GLRange") %>' alt="Img" width="18px" border="0" src="plus.gif"/>
                </a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Code" HeaderText="Code" ItemStyle-Font-Bold="true" SortExpression="Code" />
        <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Font-Bold="true" SortExpression="Name" />
        <asp:BoundField DataField="PYB" HeaderText="PYB"  HeaderStyle-CssClass="HeadGridHeader"/>
        <asp:BoundField DataField="YTDB" HeaderText="YTDB" HeaderStyle-CssClass="HeadGridHeader"/>
        <asp:BoundField DataField="YTDA" HeaderText="YTDA" HeaderStyle-CssClass="HeadGridHeader"/>
        <asp:BoundField DataField="BudgetedAmount" HeaderText="Budgeted Amount" HeaderStyle-CssClass="HeadGridHeader"/>
        <asp:BoundField DataField="April" HeaderText="April" HeaderStyle-CssClass="HeadGridHeader"/>
        <asp:BoundField DataField="May" HeaderText="May" HeaderStyle-CssClass="HeadGridHeader"/>
        <asp:BoundField DataField="June" HeaderText="June" HeaderStyle-CssClass="HeadGridHeader"/>
        <asp:BoundField DataField="July" HeaderText="July" HeaderStyle-CssClass="HeadGridHeader"/>
        <asp:BoundField DataField="August" HeaderText="August" HeaderStyle-CssClass="HeadGridHeader"/>
        <asp:BoundField DataField="Sept" HeaderText="September" HeaderStyle-CssClass="HeadGridHeader"/>
        <asp:BoundField DataField="Oct" HeaderText="October" HeaderStyle-CssClass="HeadGridHeader"/>
        <asp:BoundField DataField="Nov" HeaderText="November" HeaderStyle-CssClass="HeadGridHeader"/>
        <asp:BoundField DataField="Dec" HeaderText="December" HeaderStyle-CssClass="HeadGridHeader"/>
        <asp:BoundField DataField="Jan" HeaderText="January" HeaderStyle-CssClass="HeadGridHeader"/>
        <asp:BoundField DataField="Feb" HeaderText="February" HeaderStyle-CssClass="HeadGridHeader"/>
        <asp:BoundField DataField="March" HeaderText="March" HeaderStyle-CssClass="HeadGridHeader"/>
        <asp:TemplateField>
            <ItemTemplate>
                <tr><td colspan="100%">
                    <div id='div<%# Eval("GLRange") %>' style="width:90%;left:15px;display:none;position:relative;">
                        <asp:GridView ID="GridViewBudget" runat="server" ShowFooter="true" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None" Width="1780px" OnRowEditing="GridViewBudget_RowEditing" OnRowCancelingEdit="GridViewBudget_RowCancelingEdit" OnRowUpdating="GridViewBudget_RowUpdating" OnRowDataBound="GridViewBudget_RowDataBound" >
                            <Columns>
                                <asp:BoundField DataField="Code" HeaderText="Code" ReadOnly="true" ItemStyle-Font-Bold="true" SortExpression="Code" />
                                <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="true" ItemStyle-Font-Bold="true" SortExpression="Name" />
                                <asp:BoundField DataField="PYB" HeaderText="PYB"  HeaderStyle-CssClass="HeadGridHeader" ReadOnly="true" />
                                <asp:BoundField DataField="YTDB" HeaderText="YTDB" HeaderStyle-CssClass="HeadGridHeader" ReadOnly="true" />
                                <asp:BoundField DataField="YTDA" HeaderText="YTDA" HeaderStyle-CssClass="HeadGridHeader" ReadOnly="true" />
                                <asp:BoundField DataField="BudgetedAmount" HeaderText="Budgeted Amount" ReadOnly="true" />
                                <asp:TemplateField ControlStyle-ForeColor="Blue">
                                    <ItemTemplate >
                                        <asp:LinkButton ID="lnkEdit" CommandName="Edit" runat="server" Text="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton id="lnkCopy" runat="server" Text="Copy" OnClick="lnkCopy_Click"></asp:LinkButton>
                                        <asp:LinkButton id="lnkUpdate" CommandName="Update" runat="server" Text="Update"></asp:LinkButton>
                                        <asp:LinkButton id="lnkCancel" CommandName="Cancel" runat="server" Text="Cancel"></asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:CommandField ShowEditButton ="True" ItemStyle-ForeColor ="Blue" />--%>
                                <asp:TemplateField HeaderText="April" HeaderStyle-CssClass="ChildGridHeader" >
                                    <ItemTemplate  > <asp:Label ID="lblApril" runat="server" Text='<% #Eval("April") %>' ></asp:Label> </ItemTemplate>
                                    <EditItemTemplate >
                                            <asp:TextBox ID="txtApril" runat="server" Width="90%" CssClass="TextBoxTextAlign" Text='<% #Bind("April") %>' AutoPostBack="true" OnTextChanged="txtJan_TextChanged" onFocus="this.select()"  ></asp:TextBox> 
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtApril" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="May" HeaderStyle-CssClass="ChildGridHeader">
                                    <ItemTemplate> <asp:Label ID="lblMay" runat="server" Text='<% #Eval("May") %>' ></asp:Label> </ItemTemplate>
                                    <EditItemTemplate> 
                                        <asp:TextBox ID="txtMay" runat="server" Width="90%" CssClass="TextBoxTextAlign" Text='<% #Bind("May") %>' AutoPostBack="true" OnTextChanged="txtJan_TextChanged" onFocus="this.select()"></asp:TextBox> 
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtMay" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="June" HeaderStyle-CssClass="ChildGridHeader">
                                    <ItemTemplate> <asp:Label ID="lblJune" runat="server" Text='<% #Eval("June") %>' ></asp:Label> </ItemTemplate>
                                    <EditItemTemplate> 
                                        <asp:TextBox ID="txtJune" runat="server" Width="90%" CssClass="TextBoxTextAlign" Text='<% #Bind("June") %>' AutoPostBack="true" OnTextChanged="txtJan_TextChanged" onFocus="this.select()"></asp:TextBox> 
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtJune" />
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="July" HeaderStyle-CssClass="ChildGridHeader">
                                    <ItemTemplate> <asp:Label ID="lblJuly" runat="server" Text='<% #Eval("July") %>' ></asp:Label> </ItemTemplate>
                                    <EditItemTemplate> 
                                        <asp:TextBox ID="txtJuly" runat="server" Width="90%" CssClass="TextBoxTextAlign" Text='<% #Bind("July") %>' AutoPostBack="true" OnTextChanged="txtJan_TextChanged" onFocus="this.select()"></asp:TextBox> 
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers" TargetControlID="txtJuly" />
                                    </EditItemTemplate>
                                </asp:TemplateField> 

                                <asp:TemplateField HeaderText="August" HeaderStyle-CssClass="ChildGridHeader"> 
                                    <ItemTemplate> <asp:Label ID="lblAug" runat="server" Text='<% #Eval("Aug") %>' ></asp:Label> </ItemTemplate>
                                    <EditItemTemplate> 
                                        <asp:TextBox ID="txtAug" runat="server" Width="90%" CssClass="TextBoxTextAlign"  Text='<% #Bind("Aug") %>' AutoPostBack="true" OnTextChanged="txtJan_TextChanged" onFocus="this.select()"></asp:TextBox> 
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers" TargetControlID="txtAug" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="September" HeaderStyle-CssClass="ChildGridHeader">
                                    <ItemTemplate> <asp:Label ID="lblSept" runat="server" Text='<% #Eval("Sept") %>' ></asp:Label> </ItemTemplate>
                                    <EditItemTemplate> 
                                        <asp:TextBox ID="txtSept" runat="server" Width="90%" CssClass="TextBoxTextAlign" Text='<% #Bind("Sept") %>' AutoPostBack="true" OnTextChanged="txtJan_TextChanged" onFocus="this.select()"></asp:TextBox> 
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers" TargetControlID="txtSept"/>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="October" HeaderStyle-CssClass="ChildGridHeader">
                                    <ItemTemplate> <asp:Label ID="lblOct" runat="server" Text='<% #Eval("Oct") %>' ></asp:Label> </ItemTemplate>
                                    <EditItemTemplate> 
                                        <asp:TextBox ID="txtOct" runat="server" Width="90%" CssClass="TextBoxTextAlign" Text='<% #Bind("Oct") %>' AutoPostBack="true" OnTextChanged="txtJan_TextChanged" onFocus="this.select()"></asp:TextBox> 
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers" TargetControlID="txtOct" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="November" HeaderStyle-CssClass="ChildGridHeader">
                                    <ItemTemplate> <asp:Label ID="lblNov" runat="server" Text='<% #Eval("Nov") %>' ></asp:Label> </ItemTemplate>
                                    <EditItemTemplate> 
                                        <asp:TextBox ID="txtNov" runat="server" Width="90%" CssClass="TextBoxTextAlign" Text='<% #Bind("Nov") %>' AutoPostBack="true" OnTextChanged="txtJan_TextChanged" onFocus="this.select()"></asp:TextBox> 
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers" TargetControlID="txtNov" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="December" HeaderStyle-CssClass="ChildGridHeader">
                                    <ItemTemplate> <asp:Label ID="lblDec" runat="server" Text='<% #Eval("Dec") %>' ></asp:Label> </ItemTemplate>
                                    <EditItemTemplate> 
                                        <asp:TextBox ID="txtDec" runat="server" Width="90%" CssClass="TextBoxTextAlign" Text='<% #Bind("Dec") %>' AutoPostBack="true" OnTextChanged="txtJan_TextChanged" onFocus="this.select()"></asp:TextBox> 
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Numbers" TargetControlID="txtDec" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="January" HeaderStyle-CssClass="ChildGridHeader">
                                    <ItemTemplate> <asp:Label ID="lblJan" runat="server" Text='<% #Eval("Jan") %>' ></asp:Label> </ItemTemplate>
                                    <EditItemTemplate > 
                                        <asp:TextBox ID="txtJan" runat="server" Width="90%" CssClass="TextBoxTextAlign" Text='<% #Bind("Jan") %>'  AutoPostBack="true" OnTextChanged="txtJan_TextChanged" onFocus="this.select()"></asp:TextBox> 
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Numbers" TargetControlID="txtJan" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
            
                                <asp:TemplateField HeaderText="February" HeaderStyle-CssClass="ChildGridHeader">
                                    <ItemTemplate> <asp:Label ID="lblFeb" runat="server" Text='<% #Eval("Feb") %>' ></asp:Label> </ItemTemplate>
                                    <EditItemTemplate> 
                                        <asp:TextBox ID="txtfeb" runat="server" Width="90%" CssClass="TextBoxTextAlign" Text='<% #Bind("Feb") %>'  AutoPostBack="true" OnTextChanged="txtJan_TextChanged" onFocus="this.select()"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Numbers" TargetControlID="txtfeb" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="March" HeaderStyle-CssClass="ChildGridHeader">
                                    <ItemTemplate> <asp:Label ID="lblMarch" runat="server" Text='<% #Eval("March") %>' ></asp:Label> </ItemTemplate>
                                    <EditItemTemplate> 
                                        <asp:TextBox ID="txtMarch" runat="server" Width="90%" CssClass="TextBoxTextAlign" Text='<% #Bind("March") %>'  AutoPostBack="true" OnTextChanged="txtJan_TextChanged" onFocus="this.select()"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterType="Numbers" TargetControlID="txtMarch" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>
                            <FooterStyle BackColor="#C5122F" ForeColor="White" Font-Bold="True"  Font-Size="13.2px" />
                            <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="White" Font-Size="13.2px" />
                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" Font-Size="13.2px" />
                        </asp:GridView>
                    </div>
                </td></tr>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <FooterStyle BackColor="#C5122F" ForeColor="White" Font-Bold="True"  Font-Size="13.2px" />
    <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="White" Font-Size="13.2px" />
    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" Font-Size="13.2px" />
    </asp:GridView>
    </div>
    <div >
        <table>
            <tr>
                <td class="Middle"><span id="lblRemark" runat="server"> Remarks :</span>&nbsp;</td>
                <td ><textarea id="txtRemark" runat ="server" maxlength ="250" rows="4" cols="70" style=" width:430px; overflow:hidden"></textarea></td>
                <td class="Middle"><asp:Button ID="btnSendForReview" runat="server" Text="Send For Review" OnClick="btnSendForReview_Click" UseSubmitBehavior ="true"/>
                                    <asp:Button ID="btnReview" runat="server" Visible ="true" Text="Send For Approval" OnClick="btnReview_Click" UseSubmitBehavior ="true"/>
                                    <asp:Button ID="btnApprove" runat="server" Visible ="false" Text="Approve" OnClick="btnApprove_Click"  UseSubmitBehavior ="true"/></td>
                <td>&nbsp;</td>
                <td class="Middle" ><asp:Button ID="btnReject" runat="server" Visible ="false" Text="SendBack" OnClick="btnReject_Click" UseSubmitBehavior ="true"/></td>
            </tr> 
        </table>
    </div>
</asp:Panel>
<asp:Panel ID="pnlReport" runat="server" Visible="false">

    <asp:Label ID="Label3" runat="server" 
            Text="Budget Report" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
  
    <div class="leftm"> </div>
    <div style="height:13px"> </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <table><tr>
            <td><asp:RadioButton ID="rbtnDepartmentWise" runat="server" Checked="true" GroupName="ReportType" Text="Department Wise" AutoPostBack="true" OnCheckedChanged="rbtnHeadWise_CheckedChanged" /></td><td>&nbsp;&nbsp;</td>
            <td><asp:RadioButton ID="rbtnHeadWise" runat="server" GroupName="ReportType" Text="Head Wise" AutoPostBack="true" OnCheckedChanged="rbtnHeadWise_CheckedChanged" /></td>
            <td>&nbsp;&nbsp;&nbsp;</td>    
            <td><asp:CheckBox ID="chkQuaterly" Text="Quarterly" runat="server" OnCheckedChanged="chkQuaterly_CheckedChanged" AutoPostBack="true" /></td>
   </tr></table>
    <div>
        
    <asp:CheckBox ID="chkSelect" Text="SelectAll" OnCheckedChanged="chkSelect_CheckedChanged" runat="server" Checked="true"  AutoPostBack="true"/>
    <div style="width:866px; max-height:129px; overflow:auto;">
        <asp:DataList ID="DataListDepartment" runat="server" RepeatColumns="7">
            <ItemTemplate>

                <table> <tr> <td><asp:CheckBox ID="Chk" runat="server" Text='<% #Eval("DepartmentCode") %>' AutoPostBack="true" Checked="true" OnCheckedChanged="Chk_CheckedChanged" /> </td> <td style="width:20px"> </td></tr></table>
                
            </ItemTemplate>
        </asp:DataList>
    </div>
    <table cellpadding ="20px" cellspacing="30px"> 
        <tr> 
            <td class="Middle"> Budget &nbsp;&nbsp;</td> <td class="Middle"> <asp:DropDownList ID="ddlrptBudget" runat="server" OnSelectedIndexChanged="ddlrptBudget_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList> </td>
            <td class="Middle">&nbsp;&nbsp;&nbsp;&nbsp; Campus &nbsp;&nbsp;</td> <td class="Middle"> <asp:DropDownList ID="ddlrptCampus" runat="server" OnSelectedIndexChanged="ddlrptCampus_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td ><asp:Button ID="btnGet" runat="server" Font-Bold="True" OnClick="btnGet_Click" Text="Get" Width="80px" /></td>
        </tr>
    </table>
    </div>
    <div id="rptBudgetGridDiv">
        <asp:GridView ID="GridViewBudgetrpt" runat="server" ShowFooter="true" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="#333333" GridLines="None" OnRowDataBound="GridViewBudgetrpt_RowDataBound">
            <FooterStyle BackColor="#C5122F" ForeColor="White" Font-Bold="True"  Font-Size="13.2px" />
            <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="White" Font-Size="13.2px" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" Font-Size="13.2px" />
        </asp:GridView>
    </div>
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>
</td></tr></table>
</fieldset>
</asp:Content>



