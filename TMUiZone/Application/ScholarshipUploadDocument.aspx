<%@ Page Title="" Language="C#" MasterPageFile="~/Application/IndexMaster.master" AutoEventWireup="true" CodeFile="ScholarshipUploadDocument.aspx.cs" Inherits="Application_ScholarshipUploadDocument" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            display: block;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
        }
    </style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
        <div style="width: 100%; margin-bottom: 10px; margin-left: 1%; margin-right: 1%; margin-top: 5px;">

            <table>
                <tr>
                    <td style="width: 1%"></td>
                    <td style="width: 12%; text-align: left">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/UPDATEDLOGO.jpg" Width="55%" />

                    </td>
                    <td style="width: 65%; text-align: center">
                        <strong>
                            <asp:Label ID="lblCName" Font-Size="Large" Text="Teerthanker Mahaveer University, Moradabad" runat="server"></asp:Label></strong>
                        <br />
                        <strong>
                            <asp:Label ID="lblAC" runat="server" Text="(Established under Govt. of U. P. Act No. 30, 2008)"></asp:Label></strong>

                        <br />
                        <strong>
                            <asp:Label ID="LblType" runat="server" Text="Delhi Road,(146 Kms from Delhi on N.H. 24) Moradabad(U.P) India"></asp:Label>
                        </strong>
                        <br />
                        <strong>
                            <asp:Label ID="lbltel" runat="server" Text=" Tel.:+91-2360222 , 2360777"></asp:Label>
                        </strong>
                        <br />
                        <strong>
                            <asp:Label ID="lblemail" runat="server" Text="Email:university@tmu.ac.in;  hr@tmu.ac.in;  Website:www.tmu.ac.in"></asp:Label>
                        </strong>
                    </td>
                    <td style="width: 10%; text-align: center"></td>
                </tr>
            </table>
        </div>
    </fieldset>
    <fieldset class="boxBody" style="text-align: center; border-color: black; background-color: black;">
        <asp:Label ID="Label1" runat="server" Text="STUDENT UPLOAD DOCUMENT" Font-Size="15pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
    </fieldset>
     <fieldset class="boxBody" style="text-align: center;">

        <asp:Label ID="lblDocumentUpload" runat="server" ForeColor="Red"  Font-Size="Medium" Text="Document Upload Status:"></asp:Label>
        <asp:Label ID="lblDocumentUploadStatu" runat="server" ForeColor="Red"  Font-Size="Medium" Text=""></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label13" runat="server" ForeColor="Red" Font-Size="Medium"  Text="Document Verify Status:"></asp:Label>
        <asp:Label ID="lbldocumentverifystatus" runat="server" ForeColor="Red" Font-Size="Medium" Text=""></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Size="Medium"  Text="Rejection Reasons:"></asp:Label>
        <asp:Label ID="lblRejectionReasons" runat="server" ForeColor="Red" Font-Size="Medium" Text=""></asp:Label>

    </fieldset>

    
    <div id="divGeneralBodyUploaddocument">
        <fieldset class="boxBodyInner">
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <div class="col-md-2">
                                </div>

                                <div class="col-md-2">
                                    <label style="font-size: large; font: bold">Upload Document</label>
                                </div>
                                <div class="col-md-2">
                                    <asp:UpdatePanel ID="updatePanel5" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="drpuploaddpocument" runat="server" AutoPostBack="true" CssClass="form-control">
                                                <asp:ListItem Text="---SELECT---" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="10th Marksheet" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="12th MArksheet" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Diploma Marksheet Final Year" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="UG Marksheet Final Year" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="Transfer Certificate Original" Value="5"></asp:ListItem>
                                                <%--<asp:ListItem Text="Character Certificate Original" Value="6"></asp:ListItem>--%>
                                                <asp:ListItem Text="Migration Original" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="Anti Ragging" Value="8"></asp:ListItem>
                                                <asp:ListItem Text="Domicile" Value="9"></asp:ListItem>
                                                <asp:ListItem Text="Student Aadhar" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="Guardian Aadhar" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="ABC ID" Value="12"></asp:ListItem>

                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-md-2">
                                    <asp:FileUpload ID="FileUpload2" CssClass="auto-style1" runat="server" Width="195px" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="UploadBtn" Text="Upload File" runat="server" class="btn btn-success btn-sm form-control" Height="30px" OnClick="UploadBtn_Click" />
                                </div>
                            </div>
                            

                        </div>
                    </div>
                </div>
            </div>
        </fieldset>
    </div>
    <div id="divGeneralBodyUploaddocument1">
        <fieldset class="boxBodyInner">
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">                      
                        <div class="form-group">
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-10">
                                <asp:GridView ID="grdAttachment" runat="server" BackColor="White" Font-Size="Large" EmptyDataText="There are no data records to display." CssClass="myTableClass" AutoGenerateColumns="false" ShowFooter="true" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Horizontal">
                                    <Columns>
                                        <asp:TemplateField ControlStyle-BorderStyle="Solid">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbl10th" runat="server" Enabled='<%# (Eval("HighSchoolMarksheet").ToString() == "" ?  false : true) %>' Text="10th Marksheet"  OnClientClick="form1.target='_blank';" OnClick="lbl10th_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-BorderStyle="Solid">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbl12th" runat="server" Enabled='<%# (Eval("InterMarksheet").ToString() ==  "" ?  false : true) %>' Text="12th Marksheet"  OnClientClick="form1.target='_blank';" OnClick="lbl12th_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-BorderStyle="Solid">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbldipthe" runat="server" Enabled='<%# (Eval("Diploma_final_Year").ToString() ==  "" ?  false : true) %>' Text="Diploma Marksheet"  OnClientClick="form1.target='_blank';" OnClick="lbldipthe_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-BorderStyle="Solid">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblUG" runat="server" Enabled='<%# (Eval("UG_Final_Year").ToString() ==  "" ?  false : true) %>' Text="UG Marksheet" OnClick="lblUG_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-BorderStyle="Solid">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblTran" runat="server" Enabled='<%# (Eval("Transfer_Certificate").ToString() ==  "" ?  false : true) %>' Text="Transfer Certificate" OnClick="lblTran_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-BorderStyle="Solid" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblCharacter" runat="server" Enabled='<%# (Eval("Character_Certificate").ToString() ==  "" ?  false : true) %>' Text="Character Certificate" OnClick="lblCharacter_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-BorderStyle="Solid">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblMigration" runat="server" Text="Migration" Enabled='<%# (Eval("Migration").ToString() ==  "" ?  false : true) %>' Height="42px" OnClick="lblMigration_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-BorderStyle="Solid">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblGap" runat="server" Text="Anti-Ragging" Enabled='<%# (Eval("Anti Ragging").ToString() ==  "" ?  false : true) %>' OnClick="lblGap_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-BorderStyle="Solid">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblDomicile" runat="server" Text="Domicile" Height="42px" Enabled='<%# (Eval("Domicile").ToString() ==  "" ?  false : true) %>' OnClick="lblDomicile_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-BorderStyle="Solid">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblAadhar" runat="server" Text="Aadhar" Height="42px" Enabled='<%# (Eval("Student_Aadhar").ToString() ==  "" ?  false : true ) %>' OnClick="lblAadhar_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-BorderStyle="Solid" >
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblGuardian" runat="server" Text="Guardian Aadhar" Enabled='<%# (Eval("Guardian_Aadhar").ToString() ==  "" ?  false : true) %>' OnClick="lblGuardian_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField ControlStyle-BorderStyle="Solid">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblABCID" runat="server" Text="ABC ID" Enabled='<%# (Eval("Guardian_Aadhar").ToString() ==  "" ?  false : true) %>' OnClick="lblGuardian_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                         <asp:TemplateField ControlStyle-BorderStyle="Solid" ControlStyle-Width="150px" ControlStyle-Height="42px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblAdmission" runat="server" Text="ABC ID" Enabled='<%# (Eval("ABC_ID").ToString() ==  "" ?  false : true) %>' OnClick="lblAdmission_Click" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                    </Columns>
                                    <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />

                                </asp:GridView>
                                <br />
                                  <div class="form-group">
                                 
                                  <div class="col-md-10">
                                       <asp:CheckBox ID="Chkemployee" Font-Bold="true"  OnCheckedChanged="Chkemployee_CheckedChanged"  AutoPostBack="true"  Width="937px" Text="Please confirm that all the required documents are uploaded. There will be no document upload or changes possible after Final Submission." runat="server" />
                                      </div>
                                  </div>

                            <div class="form-group">
                                
                                <div class="col-md-5" style="text-align: center">
                                    <asp:Button ID="btn_submit" Text="Final Submit" runat="server" Font-Size="Large" OnClick="btn_submit_Click" Enabled="false" Width="966px" Height="40px" class="btn btn-success btn-sm form-control" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-2" style="width: 200px">
                                    <label style="width: 200px"></label>
                                </div>
                                <div class="col-md-4" style="visibility: hidden">
                                    <asp:TextBox ID="txtID" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>
                            </div>
                        </div>
                    </div>
                        </div>
                </div>
            </div>
        </fieldset>
    </div>


</asp:Content>

