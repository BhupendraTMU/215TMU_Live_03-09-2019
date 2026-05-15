<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" EnableEventValidation = "false" CodeFile="CRCReport.aspx.cs" Inherits="Faculty_CRCReport" %>

<script runat="server">


</script>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .grdcrcreport tr td {
            padding: 5px;
            border: 1px solid #ddd;
        }

        .grdcrcreport tr th {
            padding: 5px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="CRC Report" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <table>

        <tr style="height: 20px">
            <td></td>
        </tr>
        <tr>
            <td style="width: 20px"></td>
            <td style="font-size: larger">Admitted Year:</td>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="drpadmittedyear" Height="30px" Width="100px" runat="server">
                    <asp:ListItem Text="--Select--"></asp:ListItem>
                    <asp:ListItem Text="22-23"></asp:ListItem>
                    <asp:ListItem Text="21-22"></asp:ListItem>
                    <asp:ListItem Text="20-21"></asp:ListItem>
                    <asp:ListItem Text="19-20"></asp:ListItem>
                    <asp:ListItem Text="18-19"></asp:ListItem>
                    <asp:ListItem Text="17-18"></asp:ListItem>
                    <asp:ListItem Text="16-17"></asp:ListItem>
                    <asp:ListItem Text="15-16"></asp:ListItem>
                    <asp:ListItem Text="14-15"></asp:ListItem>
                    <asp:ListItem Text="13-14"></asp:ListItem>
                    <asp:ListItem Text="12-13"></asp:ListItem>
                    <asp:ListItem Text="11-12"></asp:ListItem>
                    <asp:ListItem Text="10-11"></asp:ListItem>
                    <asp:ListItem Text="09-10"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button runat="server" ID="show" Text="Show" Height="30px" Width="120px" ForeColor="White" BackColor="Green" OnClick="show_Click" /></td>
            <td>&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button runat="server" Text="Export to Excel" Height="30px" Width="120px" ForeColor="White" BackColor="Green" ID="btnexporttoexel" OnClick="btnexporttoexel_Click" />
            </td>
        </tr>

    </table>
    <br />
    <div style="height: 300px; width: 1200px; overflow: scroll;">
        <asp:GridView ID="grdcrcreport" DataKeyNames="ID" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
            BorderStyle="None" BorderWidth="1px"
            GridLines="Horizontal" EmptyDataText="There are no data records to display."
            AllowSorting="true" Height="200px">
            <AlternatingRowStyle BackColor="#F7F7F7" />

            <Columns>
                <asp:TemplateField HeaderText="Sl. No.">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Student Name">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("[Student Name]") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Enrollment No" HeaderStyle-Width="20px">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("[Enrollment No_]") %>'></asp:Label>
                    </ItemTemplate>

                </asp:TemplateField>

                <asp:TemplateField HeaderText="Gender">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="100px" Text='<%# Eval("Gender") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Category">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="100px" Text='<%# Eval("Category") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="DOB">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="100px" Text='<%# Eval("DOB") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Mobile No">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="100px" Text='<%# Eval("Mobile") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Alternate Mobile No">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="100px" Text='<%# Eval("AlterMobile") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Official Email ID " HeaderStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="200px"  Text='<%# Eval("PersonalEmail") %>'></asp:Label>
                    </ItemTemplate>
                   
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Personal Email ID " HeaderStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="200px" Text='<%# Eval("PersonalEmail") %>'></asp:Label>
                    </ItemTemplate>
                   
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Permanent Address">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="300px" Text='<%# Eval("PerAddress") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Current Address">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="300px" Text='<%# Eval("CurrAddress") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="State">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="100px" Text='<%# Eval("State") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Any Disabilty">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="100px" Text='<%# Eval("Disability ") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PG Degree">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="100px" Text='<%# Eval("PGDegree") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="PG Branch">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="100px" Text='<%# Eval("PGBranch") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="PG College Name">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="200px" Text='<%# Eval("PGCollege") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="PG University Name">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="200px" Text='<%# Eval("PGUniversity") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="PG Percentage(Aggr) ">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="100px" Text='<%# Eval("PGPer") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PG Year Of Passing">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="100px" Text='<%# Eval("PGPassingYear") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="UG Degree">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="100px" Text='<%# Eval("UGDegree") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UG Branch">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="100px" Text='<%# Eval("UGBranch") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="UG College Name">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="200px" Text='<%# Eval("UGCollege") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UG University Name">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="200px" Text='<%# Eval("UGUniversity") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="UG Percentage(Aggr)">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="100px" Text='<%# Eval("UGPer") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="UG Year of Passing">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="100px" Text='<%# Eval("UGPassingYear") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="12th Board Name">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="200px" Text='<%# Eval("12BoardName") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="12th Percentage">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="100px" Text='<%# Eval("12thPer") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="12th Year of Passing">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="100px" Text='<%# Eval("12thPassingYear") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="10th Board Name">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="200px" Text='<%# Eval("10BoardName") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="10th Percentage">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="100px" Text='<%# Eval("10thper") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="10th Year of Passing">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="100px" Text='<%# Eval("10thPassingYear") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

      <asp:TemplateField HeaderText="Backlog Total(In Pursing Course)">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="100px" Text='<%# Eval("TotalBacklog") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Pending Backlog (In Pursing Course)">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="100px" Text='<%# Eval("Pendingbacklog") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
               <asp:TemplateField HeaderText="VAC1">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Width="100px" Text='<%# Eval("VAC1") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                  <asp:TemplateField HeaderText="VAC2">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Width="100px" Text='<%# Eval("VAC2") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                  <asp:TemplateField HeaderText="VAC3">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Width="100px" Text='<%# Eval("VAC3") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                  <asp:TemplateField HeaderText="VAC4">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Width="100px" Text='<%# Eval("VAC4") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                  <asp:TemplateField HeaderText="VAC5">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" width="100px" Text='<%# Eval("VAC5") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Need Placement Assitance">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="100px" Text='<%# Eval("NeedPlacementAssistance") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Open to Relocation (Pan India )">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="100px" Text='<%# Eval("OpentoRelocate") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Preferred University">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Width="100px" Text='<%# Eval("PUniversity") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Preferred Course">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Width="100px" Text='<%# Eval("PCourse") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Name of Enterprise">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Width="100px" Text='<%# Eval("NameOfEnterPrise") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Location/Address">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Width="100px" Text='<%# Eval("NLocation") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                 <asp:TemplateField HeaderText="GSTIN NO.">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Width="150px" Text='<%# Eval("GSTINNo") %>'></asp:Label>
                                                        
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                  <asp:TemplateField HeaderText="Family Business">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Width="100px" Text='<%# Eval("FamilyBusiness") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Type Of StartUp/Industry">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Width="200px" Text='<%# Eval("TypeOfStartUP") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>

                <asp:TemplateField HeaderText="Startup">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Width="100px" Text='<%# Eval("StartUP") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                <asp:TemplateField HeaderText="Option to Download Resume">
                    <ItemTemplate>
                      <%--  <asp:Label runat="server" Width="100px" Text='<%# Eval("Resume") %>'></asp:Label>--%>
                        <asp:LinkButton ID="lnkResume" runat="server" ForeColor="Red" Font-Underline="true" Enabled='<%# Eval("EnableR").ToString() == "0" ? false : true %>'  Width="200px" OnClick="lnkResume_Click"> View Resume</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="15%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Option to Download Photo">
                    <ItemTemplate>
                        <%--<asp:Label runat="server" Width="100px" Text='<%# Eval("Photo") %>'></asp:Label>--%>
                        <asp:LinkButton ID="lnkPhoto" runat="server" ForeColor="Red" Font-Underline="true" Enabled='<%# Eval("EnableP").ToString() == "0" ? false : true %>' OnClick="lnkPhoto_Click"> View Photo</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
            <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
            <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
            <SortedAscendingCellStyle BackColor="#F4F4FD" />
            <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
            <SortedDescendingCellStyle BackColor="#D8D8F0" />
            <SortedDescendingHeaderStyle BackColor="#3E3277" />






        </asp:GridView>
    </div>

</asp:Content>

