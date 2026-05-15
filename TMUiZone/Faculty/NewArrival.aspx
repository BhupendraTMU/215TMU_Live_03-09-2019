<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="NewArrival.aspx.cs" Inherits="Faculty_NewArrival" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="//code.jquery.com/jquery-1.11.2.min.js" type="text/javascript"></script>
    <%--<script type="text/javascript">
        function ShowImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=ImgPrv.ClientID%>').prop('src', e.target.result)
                        .width(100)
                        .height(100);
                };
                reader.readAsDataURL(input.files[0]);
                }
            }
    </script>--%>
  
    
     <style>
        #confirmModalB .modal-dialog.modalPopup {
            width: 95%;
        }

        table thead tr th:first-child, .table > tbody > tr > th:first-child {
            border-left: 1px solid #60594f;
            padding: 5px 8px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody" runat="server" id="fieldset1" visible="false" style="text-align: center; border-color: black;  background-color: black;">
        <asp:Label ID="Label1" runat="server" Text=" New Arrival Form " Visible="false" Font-Size="15pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <div id="divGeneralBody" runat="server" visible="false">
        <fieldset class="boxBodyInner">
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">

                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="width: 200px">College Code:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpcollegecode" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px">Academic Year:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpacademicyear" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Select"  Value="0"></asp:ListItem>
                                        <asp:ListItem Text="2024-2025" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="2023-2024" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="2022-2023" Value="3"></asp:ListItem>
                                       
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="width: 200px">CAS:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpcas" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Journal" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Book" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Magazine" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px">Remark:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="width: 200px">Attachment:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:FileUpload ID="FileUpload2" runat="server" CssClass="form-control" onchange="ShowImagePreview(this);" />
                                </div>
                            <div class="col-md-3">
                                <asp:Button ID="btn_save" runat="server" Text="Save" Width="70px" OnClick="btn_save_Click" CssClass="form-control" />
                                </div>

                            </div>
                            <%--<div>
                                    <asp:UpdatePanel ID="pnlpic" runat="server">
                                        <ContentTemplate>
                                            <asp:Image ID="ImgPrv" Height="100px" Width="100px" runat="server" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>--%>
                        </div>
                        </div>
                    </div>
                </div>
        </fieldset>

    </div>

    <fieldset class="boxBody" runat="server" id="fieldset2"  style="text-align: center; border-color: black;  background-color: black;">
        <asp:Label ID="Label2" runat="server" Text=" New Arrival list " Visible="false" Font-Size="15pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
    </fieldset>
   <br />
     <fieldset>
        <div class="text-right"  style="padding-left:-100px">
     <asp:Button ID="BtnRejected" runat="server" Text="Delete" ForeColor="White" CssClass="btn" OnClick="BtnRejected_Click"  BackColor="#ff9900"/>
    <asp:Button ID="Btnexporttoexel" runat="server" Text="Export To Excel" ForeColor="White"  CssClass="btn" OnClick="Btnexporttoexel_Click" BackColor="#ff9900"/>
            </div>
        </fieldset>
    <br />
   
    <div style="overflow:scroll">
        <asp:GridView ID="grdnewarrivallist" runat="server" Visible="false"  allowpaging="true"    DataKeyNames="ID" PageSize="10"  OnPageIndexChanging="grdnewarrivallist_PageIndexChanging"  AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
            BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
            GridLines="Horizontal" EmptyDataText="There are no data records to display." 
            AllowSorting="true">           
            <PagerSettings Mode="Numeric" Position="Bottom"  />
            <pagerstyle backcolor="LightBlue" height="30px" verticalalign="Bottom" horizontalalign="Center" CssClass="csspager"/>
            <%--<PagerStyle CssClass="csspager" />--%>
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

            <AlternatingRowStyle BackColor="#F7F7F7" />
            <Columns>
         
                <asp:TemplateField HeaderText="Sl. No.">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate> 
                     <ItemStyle Width="7%" />        
                </asp:TemplateField>
               <asp:TemplateField HeaderText="ID" Visible="false" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblemployeecode" runat="server" Text='<%# Bind("[ID]") %>'></asp:Label>
                        <asp:HiddenField ID="Hfemployeecode" Value='<%# Eval("[ID]") %>' runat="server" />
                        <asp:HiddenField ID="Hfhodname" Value='<%# Eval("[ID]") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
              <%--  <asp:TemplateField HeaderText="View" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkview" runat="server" OnClick="lnkview_Click">View</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>


                <asp:TemplateField HeaderText="College Code" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("College_code") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Academic Year" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label runat="server"  Text='<%# Eval("Academic_Year ") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />

                </asp:TemplateField>
                <asp:TemplateField HeaderText="CAS" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label runat="server"  Text='<%# Eval("CAS") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Remarks" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label runat="server"  Text='<%# Eval("Remark") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                   <asp:TemplateField HeaderText="Uploaded By" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label runat="server"  Text='<%# Eval("Uploaded_by") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

              
                <asp:TemplateField HeaderText="Attachment Download " ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <%--   <asp:Label runat="server" Width="100px" Text='<%# Eval("Upload_Photo") %>'></asp:Label>--%>
                        <asp:LinkButton ID="lnkPhoto" runat="server" ForeColor="Red" Font-Underline="true" Enabled='<%# Eval("Attachment").ToString() == "0" ? false : true %>' OnClick="lnkPhoto_Click"> View Photo</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                  <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" HeaderText="Select" ItemStyle-CssClass="text-center">
           
                    <ItemTemplate>
                        <asp:CheckBox ID="Chkemployee"   runat="server" AutoPostBack="true"  />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
           <EmptyDataTemplate>
                            There is no record found
                        </EmptyDataTemplate>
                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                        <HeaderStyle HorizontalAlign="Center" Height="20px" BackColor="#ff9900"  ForeColor="White" />
                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" Font-Size="Large" Font-Bold="true" VerticalAlign="Middle" />
                        <RowStyle BackColor="White" ForeColor="#330099" HorizontalAlign="Left"/>
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                        <SortedAscendingCellStyle BackColor="#FEFCEB" />
                        <SortedAscendingHeaderStyle BackColor="#AF0101" />
                        <SortedDescendingCellStyle BackColor="#F6F0C0" />
                        <SortedDescendingHeaderStyle BackColor="#7E0000" />
        </asp:GridView>
        <asp:Panel ID="pnlGridViewDetails" CssClass="modalPopup"  Width="65%" runat="server"  Style="display: none;">
            <fieldset class="boxBody" runat="server"  style="text-align: center; border-color: black;  background-color: black;">
        <asp:Label ID="Viewlabel" runat="server" Text=" New Arrival Form "  Font-Size="15pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
               <div class="close">
                    <asp:Button ID="btnclose" runat="server" OnClick="btnclose_Click" Text="X" />
                </div>
    </fieldset>
            
    <div id="div1" runat="server">
        <fieldset class="boxBodyInner">
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">

                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="width: 200px">College Code:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px">Academic Year:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Select"  Value="0"></asp:ListItem>
                                        <asp:ListItem Text="2024-2025" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="2023-2024" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="2022-2023" Value="3"></asp:ListItem>
                                       
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="width: 200px">CAS:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Journal" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Book" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Magazine" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px">Remark:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="width: 200px">Attachment:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" onchange="ShowImagePreview(this);" />
                                </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </fieldset>
            </div>
            

            </asp:Panel>
         <asp:Button ID="btnDummy" runat="server" Style="display: none;" />
        <asp:ModalPopupExtender ID="GridViewDetails" runat="server" TargetControlID="btnDummy"
            PopupControlID="pnlGridViewDetails" BackgroundCssClass="modalBackground" />
  <br />
    </div>
    
  
</asp:Content>

