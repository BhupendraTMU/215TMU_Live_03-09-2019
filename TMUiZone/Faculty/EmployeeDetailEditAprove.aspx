<%@ Page Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="EmployeeDetailEditAprove.aspx.cs" Inherits="Faculty_EmployeeDetailEditAprove" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function ViewUserDetails(No) {
            $.ajax({
                url: 'EmployeeDetailEditAprove.aspx/GetEmployeeDetailList',
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ employeeNo: No }),
                success: function (result) {
                    const data = result.d;

                    if (!data) {
                        alert("No data found.");
                        return;
                    }

                    $("#ContentPlaceHolder1_hidEmpID").val(data.CreatedBy);

                    $("#empNo").text(data.New_EmployeeNo);
                    $("#empName").text(data.Old_Name);
                    $("#empBirthDate").text(data.Old_BirthDate);
                    $("#empGender").text(data.Old_Gender);
                    $("#empMobile").text(data.Old_MobileNo);
                    $("#empEmail").text(data.Old_Email);

                    $("#empDesignationName").text(data.Old_DesignationName);

                    $("#empFatherName").text(data.Old_FatherName);
                    $("#empAdharCard").text(data.Old_AdharCard);
                    $("#empPANCard").text(data.Old_PANCard);

                    if (data.New_FileUpload != "") {
                        $("#nextPageLink").attr("href", "../Uploads/doc/" + data.New_FileUpload);
                    }
                    else {
                        $("#nextPageLink").hide();
                    }

                    $("#empNo1").text(data.New_EmployeeNo);
                    $("#empName1").text(data.New_Name);
                    $("#empBirthDate1").text(data.New_BirthDate);
                    $("#empGender1").text(data.New_Gender);
                    $("#empMobile1").text(data.New_MobileNo);
                    $("#empEmail1").text(data.New_Email);

                    $("#empFatherName1").text(data.New_FatherName);
                    $("#empAdharCard1").text(data.New_AdharCard);
                    $("#empPANCard1").text(data.New_PANCard);

                    $("#empDesignationName1").text(data.New_DesignationName);

                    $("#empStatus").text(data.New_Status);
                    $("#empCreatedBy").text(data.CreatedBy);
                    $("#empCreatedAt").text(data.CreatedAt);
                    // Show the modal
                    $("#userDetailModal").modal("show");
                },
                error: function () {
                    alert('Error loading employee details.');
                }
            });
        }
    </script>
    <style>
        /* ===== Custom Modal Styling ===== */
        #userDetailModal .modal-content {
            border-radius: 16px;
            border: none;
            box-shadow: 0 6px 25px rgba(0, 0, 0, 0.2);
            background: #ffffff;
            overflow: hidden;
        }

        #userDetailModal .modal-header {
            /*background: linear-gradient(135deg, #0d6efd, #4dabf7);*/
            background-color: #ed7600;
            color: #fff;
            padding: 15px 20px;
            border-bottom: none;
        }

        #userDetailModal .modal-title {
            font-size: 1.25rem;
            font-weight: 600;
        }

        #userDetailModal .btn-close {
            filter: brightness(0) invert(1); /* make close button white */
            opacity: 0.9;
        }

        /* ===== Table Styling ===== */
        #userDetailModal table {
            margin: 0;
            border-collapse: separate;
            border-spacing: 0 6px;
            width: 100%;
        }

        #userDetailModal th {
            width: 30%;
            background: #f8f9fa;
            color: #333;
            font-weight: 600;
            padding: 10px;
            border: none;
            border-radius: 6px 0 0 6px;
        }

        #userDetailModal td {
            background: #fff;
            padding: 10px;
            border: none;
            border-radius: 0 6px 6px 0;
            font-weight: 500;
        }

        /* Zebra effect with soft shadow */
        #userDetailModal tr {
            box-shadow: 0 2px 6px rgba(0,0,0,0.05);
            margin-bottom: 5px;
            display: table-row;
        }

        /* ===== Mobile Responsive ===== */
        @media (max-width: 576px) {
            #userDetailModal .modal-dialog {
                margin: 10px;
            }

            #userDetailModal th {
                width: 40%;
                font-size: 0.9rem;
            }

            #userDetailModal td {
                font-size: 0.9rem;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Employee Detail Edit Aprove" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <table>
        <tr style="height: 20px">
            <td></td>
        </tr>
        <tr>
            <td style="width: 20px">
                <asp:Button ID="btnExportExcel" runat="server"
                    Text="Export To Excel"
                    CssClass="btn btn-primary"
                    OnClick="btnExportExcel_Click" />
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="grdmemberapprovallist" runat="server" AlternatingRowStyle-CssClass="danger" PageSize="50"
        AllowPaging="true" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" OnPageIndexChanging="grdmemberapprovallist_PageIndexChanging" Visible="true">
        <PagerSettings Mode="NumericFirstLast" />
        <PagerStyle CssClass="csspager" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <%# Container.DataItemIndex +1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Employee Code">
                <ItemTemplate>
                    <a href='#' onclick="ViewUserDetails('<%# Eval("CreatedBy") %>');">
                        <%# Eval("CreatedBy") %>
                    </a>
                    <asp:HiddenField ID="Hfemployeecode" Value='<%# Eval("CreatedBy") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Employee Name">
                <ItemTemplate>
                    <asp:Label ID="lblemployeename" runat="server" Text='<%# Eval("Name") %>' Style="text-transform: uppercase;"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Gender">
                <ItemTemplate>
                    <asp:Label ID="lblgender" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Aadhar Card">
                <ItemTemplate>
                    <asp:Label ID="lblAadharcard" runat="server" Text='<%# Eval("AdharCard") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Mobile No">
                <ItemTemplate>
                    <asp:Label ID="lblmobileno" runat="server" Text='<%# Eval("MobileNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <asp:Label ID="lblemail" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>


        </Columns>
        <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
    </asp:GridView>
    <asp:GridView ID="gvDetails" CellPadding="5" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Text" HeaderText="FileName" />
        </Columns>
        <HeaderStyle BackColor="#df5015" Font-Bold="true" ForeColor="White" />


    </asp:GridView>
    <div class="modal fade" id="userDetailModal" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Employee Details</h5>
                </div>
                <div class="modal-body">
                    <div style="max-height: 350px; overflow-y: auto;">
                        <table class="table table-striped">
                            <tr>
                                <th></th>
                                <th>Existing Record</th>
                                <th>New Record</th>
                            </tr>
                            <tr>
                                <th>Employee No</th>
                                <td id="empNo"></td>
                                <td id="empNo1"></td>
                            </tr>
                            <tr>
                                <th>Name</th>
                                <td id="empName"></td>
                                <td id="empName1"></td>
                            </tr>
                            <tr>
                                <th>DOB</th>
                                <td id="empBirthDate"></td>
                                <td>
                                    <p id="empBirthDate1" style="display: inline; margin-right: 10px;"></p>
                                    <a id="nextPageLink" target="_blank" style="float: right;">DOB verify</a></td>
                            </tr>
                            <tr>
                                <th>Gender</th>
                                <td id="empGender"></td>
                                <td id="empGender1"></td>
                            </tr>
                            <tr>
                                <th>Mobile</th>
                                <td id="empMobile"></td>
                                <td id="empMobile1"></td>
                            </tr>
                            <tr>
                                <th>Email</th>
                                <td id="empEmail"></td>
                                <td id="empEmail1"></td>
                            </tr>

                            <tr>
                                <th>Father Name</th>
                                <td id="empFatherName"></td>
                                <td id="empFatherName1"></td>
                            </tr>
                            <tr>
                                <th>Adhar Card</th>
                                <td id="empAdharCard"></td>
                                <td id="empAdharCard1"></td>
                            </tr>
                            <tr>
                                <th>PAN Card</th>
                                <td id="empPANCard"></td>
                                <td id="empPANCard1"></td>
                            </tr>


                            <tr>
                                <th>Designation</th>
                                <td id="empDesignationName"></td>
                                <td id="empDesignationName1"></td>
                            </tr>
                            <tr>
                                <th>Status</th>
                                <td></td>
                                <td id="empStatus"></td>
                            </tr>
                            <tr>
                                <th>Created By</th>
                                <td></td>
                                <td id="empCreatedBy"></td>
                            </tr>
                            <tr>
                                <th>Created At</th>
                                <td></td>
                                <td id="empCreatedAt"></td>
                            </tr>
                        </table>

                    </div>
                </div>
                <div class="modal-footer">
                    <asp:HiddenField runat="server" ID="hidEmpID" />
                    <asp:Button ID="btnAccept" runat="server" Text="Accept"
                        CssClass="btn btn-primary" OnClick="btnAccept_Click" />

                    <asp:Button ID="btnReject" runat="server" Text="Reject"
                        CssClass="btn btn-danger" OnClick="btnReject_Click" />

                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>

                </div>
            </div>
        </div>
    </div>

</asp:Content>
