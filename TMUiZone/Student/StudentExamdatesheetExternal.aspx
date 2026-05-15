<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentExamdatesheetExternal.aspx.cs" Inherits="Student_StudentExamdatesheetExternal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="External Date Sheet" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>

    <fieldset class="boxBodyHeader">
    </fieldset>
   

       <asp:ScriptManager ID="ty" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="fe" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <br />
             <fieldset class="boxBodyInner">
  <asp:GridView ID="GrdExamTimeSheet" runat="server" CssClass="table table-striped table-bordered table-hover"  Style="width: 95%; margin-left: 2%; margin-right: 2%" EmptyDataText="No Data to display">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.no">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex +1 %>


                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
            </fieldset>

        </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

