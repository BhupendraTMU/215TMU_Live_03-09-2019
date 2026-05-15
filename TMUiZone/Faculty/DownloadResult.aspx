<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="DownloadResult.aspx.cs" Inherits="Faculty_DownloadResult" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .upCase {
            text-transform: uppercase;
        }

        .myTableClass tr th {
            padding: 1px;
        }

        tr td {
            padding: 1px;
        }



        .style1 {
            height: 83px;
        }

        a.greenButton {
            color: #000;
            text-decoration: none;
            margin: 20px;
            padding: 10px 20px 10px 20px;
            display: inline-block;
        }

            a.greenButton:hover {
                background-color: #5078B3;
            }

        .modalbackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .modalPopupWhite {
            background-color: #ffffff;
            border-width: 3px;
            border-style: solid;
            border-color: Gray;
            padding: 3px;
            width: 500px;
        }

        .modalprogress {
            opacity: 0.7;
            filter: alpha(opacity=60);
            background-color: #ededed;
        }
 </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <fieldset class="boxBodyInner">
        <table cellpadding="0px" cellspacing="0px">
            <caption>
                <tr>
                    <td style="width: 25%">
                        <asp:Label ID="Label1" runat="server"
                            Text="Download Results" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                    </td>

                </tr>
                <tr>

                    <td>Academic Year  </td>
                    <td style="width: 10px"></td>
                    <td>
                        <asp:DropDownList ID="drpAcademicYear" Width="100px" Height="20px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpAcademicYear_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td style="width: 20px"></td>
                    <td>Program
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpCourse" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 10px"></td>
                    <td>
                        <asp:DropDownList ID="drpCourse" runat="server" AutoPostBack="true" Height="20px" Width="150px">
                        </asp:DropDownList>
                    </td>


                    <td style="width: 10px"></td>
                    <td>
                        <asp:Button ID="btnShow" runat="server" CssClass="btn-sm btn-primary btn-block"
                            ValidationGroup="g1" OnClick="btnShow_Click" Height="30px" Width="90px" Text="SHOW / BACK" />

                    </td>





                </tr>
                <tr >
                    <asp:GridView ID="grdResult" runat="server" DataKeyNames="No_" BackColor="White" AutoGenerateColumns="false" ShowFooter="true" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                        GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                        <AlternatingRowStyle BackColor="#F7F7F7" />

                        <Columns>
                              <asp:TemplateField HeaderText="Sr. No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle Width="3%" />
                            </asp:TemplateField>
                          

                            <asp:TemplateField HeaderText="Result No." ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="grdNo" runat="server" Text='<%#Eval("No_")%>'></asp:Label>                                   

                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Course Code" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblCourse" runat="server" Text='<%#Eval("Course Code")%>'></asp:Label>                                   

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Course Name" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblCourseName" runat="server" Text='<%#Eval("Course Name")%>'></asp:Label>                                   

                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Semester" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="grdSem" runat="server" Text='<%#Eval("Semester")%>'></asp:Label>                                   

                                </ItemTemplate>
                            </asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="Result Published Date" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="grdPublishDate" runat="server" Text='<%#Eval("Published Date")%>'></asp:Label>                                   

                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Tebulation Report" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkTeb" runat="server" Text="Download" Enabled='<%# Eval("TR").ToString() == "0" ? false : true %>'  OnClick="lnkTeb_Click"></asp:LinkButton>                                   

                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Result Analysis" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkResAn" runat="server" Text="Download" Enabled='<%# Eval("RAP").ToString() == "0" ? false : true %>' OnClick="lnkResAn_Click"></asp:LinkButton>                                   

                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="MOOC" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkMooc" runat="server" Text="Download" Enabled='<%# Eval("MOOC").ToString() == "0" ? false : true %>'  OnClick="lnkMooc_Click"></asp:LinkButton>                                   

                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="CTLD" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkCTLD" runat="server" Text="Download" Enabled='<%# Eval("CTLD").ToString() == "0" ? false : true %>' OnClick="lnkCTLD_Click"></asp:LinkButton>                                   

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bridge Course" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkBridge" runat="server" Text="Download" Enabled='<%# Eval("Bridge").ToString() == "0" ? false : true %>' OnClick="lnkBridge_Click"></asp:LinkButton>                                   

                                </ItemTemplate>
                            </asp:TemplateField>

















                        </Columns>
                         <footerstyle backcolor="#B5C7DE" forecolor="#4A3C8C" />
                    <headerstyle backcolor="#ed7600" font-bold="True" forecolor="#F7F7F7" horizontalalign="Left" cssclass="cssGridheaderfont" />
                    <pagerstyle backcolor="#E7E7FF" forecolor="#4A3C8C" horizontalalign="Right" />
                    <rowstyle forecolor="#4A3C8C" backcolor="#E7E7FF" cssclass="cssGridheaderfont" />
                    <selectedrowstyle backcolor="#88dde3" font-bold="True" forecolor="#F7F7F7" />
                    <sortedascendingcellstyle backcolor="#F4F4FD" />
                    <sortedascendingheaderstyle backcolor="#5A4C9D" />
                    <sorteddescendingcellstyle backcolor="#D8D8F0" />
                    <sorteddescendingheaderstyle backcolor="#3E3277" />
                    </asp:GridView>
                   
                </tr>

            </caption>
        </table>
        <asp:Button ID="BtnHideQuotation" runat="server" Style="display: none" />
            <asp:ModalPopupExtender runat="server" ID="ModalPopupMsg" TargetControlID="BtnHideQuotation"
                PopupControlID="PanelQuotation" BackgroundCssClass="modalbackground" RepositionMode="RepositionOnWindowScroll"
                PopupDragHandleControlID="PanelQuotation">
            </asp:ModalPopupExtender>
            <asp:Panel runat="server" ID="PanelQuotation" CssClass="modalPopupWhite">
                <br /><asp:Button ID="btnclose" Text="Close" runat="server" OnClick="btnclose_Click" />
                <div id="divmsg" runat="server" style="text-align: center; color: Red; margin-bottom: 20px;">

                    <asp:GridView ID="grdOpenElective" runat="server" DataKeyNames="SubjectCode,No_" BackColor="White" AutoGenerateColumns="false" ShowFooter="true" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                        GridLines="Horizontal" EmptyDataText="There are no data records to display.">

                        <Columns>
                             <asp:TemplateField HeaderText="Sr. No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle Width="3%" />
                            </asp:TemplateField>
                          

                              <asp:BoundField DataField="SubjectCode" ItemStyle-HorizontalAlign="Left"  HeaderText="Subject Code" />
                              <asp:BoundField DataField="SubjectName" ItemStyle-HorizontalAlign="Left"  HeaderText="Subject Name" />
                            <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkTeb" runat="server" Text="Select"   OnClick="lnkTeb_Click1"></asp:LinkButton>                                   

                                </ItemTemplate>
                                </asp:TemplateField>
                            
                        </Columns>
                         <FooterStyle BackColor="#B5C7DE" ForeColor="LightBlue" />
                        <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="LightBlue" CssClass="cssGridheaderfont" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                        <RowStyle ForeColor="#4A3C8C" BackColor="LightBlue" CssClass="cssGridheaderfont" />
                        <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
                        <SortedAscendingCellStyle BackColor="#F4F4FD" />
                        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                        <SortedDescendingCellStyle BackColor="#D8D8F0" />
                        <SortedDescendingHeaderStyle BackColor="#3E3277" />
                    </asp:GridView>
                </div>
               
               
            </asp:Panel>
    </fieldset>
</asp:Content>

