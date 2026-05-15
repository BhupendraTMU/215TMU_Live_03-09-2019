<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="UpdateProfile.aspx.cs" Inherits="Faculty_UpdateProfile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <div class="text-right" style="padding-left: 250px">

            <asp:Button ID="BtnSubmit" runat="server" Text="Approved" OnClick="BtnSubmit_Click" ForeColor="White" CssClass="btn" BackColor="#ff9900" />
            <asp:Button ID="BtnRejected" runat="server" Text="Rejected" ForeColor="White" OnClick="BtnRejected_Click" CssClass="btn" BackColor="#ff9900" />

        </div>
    </fieldset>
    <br />
    <asp:GridView ID="grdUpdateProfileData" runat="server" DataKeyNames="No_" OnPageIndexChanging="grdUpdateProfileData_PageIndexChanging" AlternatingRowStyle-CssClass="danger" PageSize="50"
        AllowPaging="true" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" Visible="true">
        <PagerSettings Mode="NumericFirstLast" />
        <PagerStyle CssClass="csspager" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <%# Container.DataItemIndex +1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Student Number" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:Label ID="lblemployeecode" runat="server" Text='<%# Bind("No_") %>'></asp:Label>
                    <asp:HiddenField ID="Hfemployeecode" Value='<%# Eval("No_") %>' runat="server" />
                    <asp:HiddenField ID="Hfhodname" Value='<%# Eval("No_") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="View" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkbutton" OnClick="lnkbutton_Click" runat="server">View</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                <ItemTemplate>
                    <asp:Label ID="lblstudentName" runat="server" Text='<%# Eval("[Student Name]") %>' Style="text-transform: uppercase;"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Father's Name" ItemStyle-Width="3%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                <ItemTemplate>
                    <asp:Label ID="lblfathername" runat="server" Text='<%# Eval("[Fathers Name]") %>' Style="text-transform: uppercase;"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Mother Name" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:Label ID="lblmothername" runat="server" Text='<%#Eval("[Mothers Name]") %>' Style="text-transform: uppercase;"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date Of Birth" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:Label ID="lbldateofbirth" runat="server" Text='<%#Eval("[Dob]") %>' Style="text-transform: uppercase;"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <%--  <asp:TemplateField HeaderText="Remark" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:TextBox ID="txtRemark" runat="server" Enabled='<%# Eval("txtMarksEnableDesable").ToString().Equals("true") %>' Text='<%#Eval("RejectRemarks") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="Gender" ItemStyle-Width="2%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:Label ID="lblgender" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Course Name" ItemStyle-Width="2%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:Label ID="lblcoursename" runat="server" Text='<%# Eval("[Course Name]") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="College Name" ItemStyle-Width="2%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:Label ID="lblcollegename" runat="server" Text='<%# Eval("[Global Dimension 1 Code] ") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" HeaderText="Select" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:CheckBox ID="Chkemployee" runat="server" AutoPostBack="true" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
    </asp:GridView>
    <asp:Panel ID="pnlGridViewDetails" CssClass="modalPopup" Width="65%" runat="server" Style="display: none;" ScrollBars="Vertical" Height="900px">
        <div class="header">
            <b>

                <asp:Label ID="lblNotification" runat="server" Text="Update Student Data"></asp:Label></b>
            <div class="close">
                <asp:Button ID="btnclose" OnClick="btnclose_Click" runat="server" Text="X" />
            </div>
        </div>
        <fieldset class="boxBodyInner">
            <table cellpadding="0px" cellspacing="0px">
                <tr>
                    <td colspan="15" style="height: 10px">
                        <fieldset class="boxBodyHeader">
                            <asp:Label ID="Label3" runat="server"
                                Text="Personal Information" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                        </fieldset>
                        <br />
                        <fieldset class="boxBodyInner">
                            <table cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td>
                                        <table cellpadding="0px" cellspacing="0px">

                                            <tr>
                                                <td>
                                                    <label>Student Name</label></td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtName" runat="server" Enabled="false" Width="250px"></asp:TextBox>

                                                </td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <label>Father's Name </label>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtfathername" runat="server" Enabled="false" Width="250px"></asp:TextBox></td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <label style="width: 100px; float: right;">Mother's Name</label></td>
                                                <td></td>
                                                <td>
                                                    <asp:TextBox ID="txtMothername" runat="server" Enabled="false" Width="250px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td colspan="11" style="height: 10px"></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Student Name (हिन्दी में)</label></td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtstudendtnameHindi" runat="server" Enabled="false" Width="250px"></asp:TextBox>

                                                </td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <label>Father's Name (हिन्दी में) </label>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtFathernameHindi" runat="server" Enabled="false" Width="250px"></asp:TextBox></td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <label style="width: 100px; float: right;">Mother's Name (हिन्दी में)</label></td>
                                                <td></td>
                                                <td>
                                                    <asp:TextBox ID="txtMotherNameHindi" runat="server" Enabled="false" Width="250px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td colspan="11" style="height: 10px"></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Programme </label>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtCourse" runat="server" Enabled="False" Width="250px"></asp:TextBox>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <label>Enrolment No </label>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td>

                                                    <asp:TextBox ID="txtRollNo" runat="server" Width="250px" Enabled="false"></asp:TextBox>

                                                </td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <label>Date of Birth </label>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtDOB" runat="server" Enabled="false" Width="250px"></asp:TextBox></td>

                                            </tr>
                                            <tr>
                                                <td colspan="11" style="height: 10px"></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Name Of College </label>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtnameofcollege" runat="server" Enabled="False" Width="250px"></asp:TextBox>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <label>Religion </label>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td>

                                                    <asp:TextBox ID="txtreligion" runat="server" Enabled="false" Width="250px"></asp:TextBox>

                                                </td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <label>Category </label>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtcategory" runat="server" Enabled="false" Width="250px"></asp:TextBox></td>

                                            </tr>

                                            <tr>
                                                <td colspan="11" style="height: 10px"></td>
                                            </tr>

                                            <tr>
                                                <td>
                                                    <label>Year of Addmission</label>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtyearofaddmission" runat="server" Enabled="False" Width="250px"></asp:TextBox>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <label>Gender </label>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td>

                                                    <asp:TextBox ID="txtgender" runat="server" Enabled="false" Width="250px"></asp:TextBox>

                                                </td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <label>Nationality</label>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtnationality" runat="server" Enabled="False" Width="250px"></asp:TextBox></td>

                                            </tr>
                                            <tr>
                                                <td colspan="11" style="height: 10px"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="11" style="height: 10px">&nbsp; 
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>

                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td colspan="15" style="height: 10px">
                        <fieldset class="boxBodyHeader">
                            <asp:Label ID="Label2" runat="server"
                                Text="Contact Information" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                        </fieldset>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <fieldset class="boxBodyInner">
                                    <table cellpadding="0px" cellspacing="0px" class="auto-style1">

                                        <tr>
                                            <td colspan="12">
                                                <table cellpadding="0px" cellspacing="0px">

                                                    <tr>
                                                        <td>
                                                            <label>Student Mob. </label>
                                                        </td>
                                                        <td class="auto-style2"></td>
                                                        <td>
                                                            <asp:TextBox ID="txtmobileno" runat="server" Width="200px " Enabled="false" BorderColor="Black" placeholder="Mobile" onkeypress="return numeric(event)" MaxLength="10" TargetControlID="txtmobileno" FilterType="Numbers, Custom"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtmobileno" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps" ValidationExpression='(^([0-9]*|\d*\d{1}?\d*)$)' Display='Dynamic'></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="width: 10px"></td>
                                                        <td style="width: 10px"></td>
                                                        <td style="width: 10px"></td>
                                                        <td>
                                                            <label>Student Email-Id </label>
                                                        </td>
                                                        <td class="auto-style3"></td>
                                                        <td>
                                                            <asp:TextBox ID="txtsudentEmailID" Enabled="false" runat="server" Width="250px"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="sudentEmailID" Display="Dynamic" ControlToValidate="txtsudentEmailID" Text="Please fill the valid Email address!" ForeColor="Red" ValidationGroup="g1"
                                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="7" style="height: 10px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Parents Mob. </label>
                                                        </td>
                                                        <td class="auto-style2"></td>
                                                        <td>
                                                            <asp:TextBox ID="txtparentsmob" runat="server" Width="200px" Enabled="false" BorderColor="Black" placeholder="Mobile" onkeypress="return numeric(event)" MaxLength="10" TargetControlID="txtmobileno" FilterType="Numbers, Custom"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtparentsmob" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps" ValidationExpression='(^([0-9]*|\d*\d{1}?\d*)$)' Display='Dynamic'></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="width: 10px"></td>
                                                        <td style="width: 10px"></td>
                                                        <td style="width: 10px"></td>
                                                        <td>
                                                            <label>Parents Email-Id </label>
                                                        </td>
                                                        <td class="auto-style3"></td>
                                                        <td>
                                                            <asp:TextBox ID="txtEmailID" runat="server" Enabled="false" Width="250px"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="regEmail" Display="Dynamic" ControlToValidate="txtEmailID" Text="Please fill the valid Email address!" ForeColor="Red" ValidationGroup="g1"
                                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server" />
                                                        </td>

                                                    </tr>

                                                    <tr>
                                                        <td>&nbsp;</td>



                                                    </tr>
                                                </table>
                                        </tr>
                                    </table>



                                </fieldset>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <fieldset class="boxBodyHeader">
                            <asp:Label ID="Label4" runat="server"
                                Text="Address Information" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                        </fieldset>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>

                                <fieldset class="boxBodyInner">
                                    <table cellpadding="0px" cellspacing="0px" class="auto-style1">

                                        <tr>
                                            <td colspan="12">
                                                <table cellpadding="0px" cellspacing="0px">

                                                    <tr>
                                                        <td>
                                                            <label>Correspondence Address </label>
                                                        </td>
                                                        <td class="auto-style2"></td>
                                                        <td>
                                                            <asp:TextBox ID="txtcorrespondence" runat="server" Enabled="false" MaxLength="256" Width="280px"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 10px"></td>
                                                        <td>
                                                            <label>District </label>
                                                        </td>
                                                        <td class="auto-style3"></td>
                                                        <td>
                                                            <asp:TextBox ID="txtdistrictcorres" runat="server" Enabled="false" Width="250px"></asp:TextBox></td>
                                                        <td class="auto-style5"></td>
                                                        <td>
                                                            <label>State </label>
                                                        </td>
                                                        <td style="width: 10px"></td>
                                                        <td>
                                                            <asp:TextBox ID="txtstatecorres" runat="server" Enabled="false" Width="250px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="7" style="height: 10px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Pin Code </label>
                                                        </td>
                                                        <td class="auto-style2"></td>
                                                        <td>
                                                            <asp:TextBox ID="txtpincodecorre" runat="server" Enabled="false" Width="200px" BorderColor="Black" placeholder="Pin Code" onkeypress="return numeric(event)" MaxLength="6" TargetControlID="txtmobileno" FilterType="Numbers, Custom"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtpincodecorre" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps" ValidationExpression='(^([0-9]*|\d*\d{1}?\d*)$)' Display='Dynamic'></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="width: 10px"></td>
                                                        <td>
                                                            <label>Country </label>
                                                        </td>
                                                        <td class="auto-style3"></td>
                                                        <td>
                                                            <asp:TextBox ID="txtcountrycorre" runat="server" Enabled="false" Width="250px"></asp:TextBox></td>
                                                        <td class="auto-style5"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="7" style="height: 10px"></td>
                                                    </tr>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="true" Enabled="false" Text="Same As Above" Font-Size="15pt" ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" />
                                                    </td>
                                                    <tr>
                                                        <td colspan="11" style="height: 10px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Permanent Address </label>
                                                        </td>
                                                        <td class="auto-style2"></td>
                                                        <td>
                                                            <asp:TextBox ID="txtperaddress" runat="server" Enabled="false" MaxLength="256" Height="28px" Width="280px"></asp:TextBox>
                                                            <%-- <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars"
                                                            ValidChars="zxcvbnmlkjhgfdsaqwertyuiopQWERTYUIOPLKJHGFDSAZXCVBNM"
                                                            TargetControlID="txtCity">--%>
                                                            <%-- </asp:FilteredTextBoxExtender>--%>
                                                        </td>
                                                        <td style="width: 10px"></td>

                                                        <td>District
                                                        </td>
                                                        <td class="auto-style3"></td>
                                                        <td>
                                                            <asp:TextBox ID="txtperdistrict" runat="server" Enabled="false" Width="250px"></asp:TextBox>
                                                            <%--<asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterMode="ValidChars"
                                                            ValidChars="zxcvbnmlkjhgfdsaqwertyuiopQWERTYUIOPLKJHGFDSAZXCVBNM"
                                                            TargetControlID="txtCountry">
                                                        </asp:FilteredTextBoxExtender>--%>
                                                            <%-- <asp:RegularExpressionValidator ID="regEmail" Display="Dynamic" ControlToValidate="txtEmailID" Text="Please fill the valid Email address!" ForeColor="Red" ValidationGroup="g1"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server" />--%>
                                                            <%-- <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtMobileNo" FilterType="Numbers, Custom"></asp:FilteredTextBoxExtender>--%>
                                                        </td>
                                                        <td class="auto-style5"></td>
                                                        <td>
                                                            <label>State </label>
                                                        </td>
                                                        <td style="width: 10px"></td>
                                                        <td>
                                                            <asp:TextBox ID="txtperstate" runat="server" Enabled="false" Width="250px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="7" style="height: 10px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Pin Code </label>
                                                        </td>
                                                        <td class="auto-style2"></td>
                                                        <td>
                                                            <asp:TextBox ID="txtperpincode" runat="server" Width="200px" Enabled="false" BorderColor="Black" placeholder="Pin Code" onkeypress="return numeric(event)" MaxLength="6" TargetControlID="txtmobileno" FilterType="Numbers, Custom"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtperpincode" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps" ValidationExpression='(^([0-9]*|\d*\d{1}?\d*)$)' Display='Dynamic'></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="width: 10px"></td>
                                                        <td>
                                                            <label>Country </label>
                                                        </td>
                                                        <td class="auto-style3"></td>
                                                        <td>
                                                            <asp:TextBox ID="txtpercountry" runat="server" Enabled="false" Width="250px"></asp:TextBox></td>
                                                        <td class="auto-style5"></td>


                                                        <tr>
                                                            <td colspan="9" style="height: 10px;"></td>
                                                        </tr>
                                            </td>
                                        </tr>
                                    </table>


                                    </tr>
                                </table>

                             
                                
                                </fieldset>



                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <asp:Button ID="btnDummy" runat="server" Style="display: none;" />
    <asp:ModalPopupExtender ID="GridViewDetails" runat="server" TargetControlID="btnDummy" PopupControlID="pnlGridViewDetails" BackgroundCssClass="modalBackground" />



</asp:Content>

