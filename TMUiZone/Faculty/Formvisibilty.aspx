<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Faculty/IndexMaster.master" CodeFile="Formvisibilty.aspx.cs" Inherits="Formvisibilty" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script type="text/javascript">
         function preventBackspace(e) {
             var evt = e || window.event;
             if (evt) {
                 var keyCode = evt.charCode || evt.keyCode;
                 if (keyCode === 8) {
                     if (evt.preventDefault) {
                         evt.preventDefault();
                     } else {
                         evt.returnValue = false;
                     }
                 }
             }
         }
         
        //function checkDate(sender, args) {
        //    if (sender._selectedDate > new Date()) {
        //        alert("You cannot select greater than current date!");
        //        sender._selectedDate = new Date();
        //        sender._textbox.set_Value(sender._selectedDate.format(sender._format))
        //    }
        //}
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Form Visibility Setup" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <fieldset class="boxBodyHeader">
    </fieldset>
    <fieldset class="boxBodyInner">
        <center>
            <div>
                <asp:GridView ID="grdFormvisibilty" runat="server" AutoGenerateColumns="false"  BackColor="White" BorderColor="#E7E7FF" BorderStyle="None"
                                    BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Horizontal" ShowFooter="true"
                    OnRowEditing="grdFormvisibilty_RowEditing" OnRowCancelingEdit="grdFormvisibilty_RowCancelingEdit"
                    OnRowUpdating="grdFormvisibilty_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="id"  Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>' Enabled="false" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblEditid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "id") %>'></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Form Name" >
                            <ItemTemplate>
                                <asp:Label ID="lblFormName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FormName") %>' Enabled="false" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblEditFormName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "FormName") %>'></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Academic Year" >
                            <ItemTemplate>
                                <asp:Label ID="lblAcademicYear" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Academic Year") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="lblEditAcademicYear" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Academic Year") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Odd Sem" >
                            <ItemTemplate>
                                <asp:CheckBox ID="chkOddSem" runat="server" Checked='<%# Eval("Odd Sem") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="chkEditOddSem" runat="server" Checked='<%# Eval("Odd Sem") %>'></asp:CheckBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Even Sem" >
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEvenSem" runat="server" Checked='<%# Eval("Even Sem") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="chkEditEvenSem" runat="server" Checked='<%# Eval("Even Sem") %>'></asp:CheckBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Year" >
                            <ItemTemplate>
                                <asp:CheckBox ID="chkYear" runat="server" Checked='<%# Eval("Year") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="chkEditYear" runat="server" Checked='<%# Eval("Year") %>'></asp:CheckBox>
                            </EditItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Open Date" >
                            <ItemTemplate>
                                <asp:Label ID="lblOpenDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Open Date") %>' Enabled="false" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="lblEditOpenDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Open Date") %>'
                                     onkeypress="return false" onKeyDown="preventBackspace();" oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                 <asp:CalendarExtender ID="cleOpenDate" Format="dd MMM yyyy" runat="server"
                                        CssClass="cal_Theme1" PopupButtonID="lblEditOpenDate"  Enabled="true" TargetControlID="lblEditOpenDate">
                                   </asp:CalendarExtender>                                     
                            </EditItemTemplate>
                            
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Close Date" >
                            <ItemTemplate>
                                <asp:Label ID="lblCloseDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Close Date") %>' Enabled="false" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="lblEditCloseDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Close Date") %>'
                                     onkeypress="return false" onKeyDown="preventBackspace();" oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                 <asp:CalendarExtender ID="cleCloseDate" Format="dd MMM yyyy" runat="server"
                                        CssClass="cal_Theme1" PopupButtonID="lblEditCloseDate"  Enabled="true" TargetControlID="lblEditCloseDate">
                                   </asp:CalendarExtender>                                     
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Active" >
                            <ItemTemplate>
                                <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("Active") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="chkEditActive" runat="server" Checked='<%# Eval("Active") %>'></asp:CheckBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="SubmissionAllow" >
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSubmissionAllow" runat="server" Checked='<%# Eval("SubmissionAllow") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="chkEditSubmissionAllow" runat="server" Checked='<%# Eval("SubmissionAllow") %>'></asp:CheckBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/icon-edit.png" Height="32px" Width="32px" ToolTip="EDIT" />

                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" ImageUrl="~/Images/icon-update.png" Height="32px" Width="32px" ToolTip="UPDATE" />
                                <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/icon-Cancel.png" Height="32px" Width="32px" ToolTip="CANCEL" />
                            </EditItemTemplate>

                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                        <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />                                                    
                                        <RowStyle ForeColor="#4A3C8C" Font-Bold="false"  />                                                   
                </asp:GridView>

            </div>
        </center>
    </fieldset>
</asp:Content>
