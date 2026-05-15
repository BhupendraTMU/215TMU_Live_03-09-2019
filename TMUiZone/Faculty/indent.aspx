<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="indent.aspx.cs" Inherits="Faculty_Requisition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script language="Javascript">
       <!--
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode != 46 && charCode > 31
          && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    //-->
    </script>
  <script type="text/javascript" src="dropdowneditable/jquery.min.js"></script>
 <script type="text/javascript" src="dropdowneditable/jquery.searchabledropdown-1.0.8.min.js"></script>
	
	
	<script type="text/javascript">
	    $(document).ready(function () {
	        $("select").searchable();
	    });


	    // demo functions
	    $(document).ready(function () {
	        $("#value").html($("#ddPointReport :selected").text() + " (VALUE: " + $("#ddPointReport").val() + ")");
	        $("select").change(function () {
	            $("#value").html(this.options[this.selectedIndex].text + " (VALUE: " + this.value + ")");
	        });
	    });

	    function modifySelect() {
	        $("select").get(0).selectedIndex = 5;
	    }

	    function appendSelectOption(str) {
	        $("select").append("<option value=\"" + str + "\">" + str + "</option>");
	    }

	    function applyOptions() {
	        $("select").searchable({
	            maxListSize: $("#maxListSize").val(),
	            maxMultiMatch: $("#maxMultiMatch").val(),
	            latency: $("#latency").val(),
	            exactMatch: $("#exactMatch").get(0).checked,
	            wildcards: $("#wildcards").get(0).checked,
	            ignoreCase: $("#ignoreCase").get(0).checked
	        });

	        alert(
				"OPTIONS\n---------------------------\n" +
				"maxListSize: " + $("#maxListSize").val() + "\n" +
				"maxMultiMatch: " + $("#maxMultiMatch").val() + "\n" +
				"exactMatch: " + $("#exactMatch").get(0).checked + "\n" +
				"wildcards: " + $("#wildcards").get(0).checked + "\n" +
				"ignoreCase: " + $("#ignoreCase").get(0).checked + "\n" +
				"latency: " + $("#latency").val()
			);
	    }
	</script>
	


    <script type="text/javascript">
        $(document).ready(function () {
            $("select").searchable();
        });


        // demo functions
        $(document).ready(function () {
            $("#value").html($("#ddSubPointReport :selected").text() + " (VALUE: " + $("#ddSubPointReport").val() + ")");
            $("select").change(function () {
                $("#value").html(this.options[this.selectedIndex].text + " (VALUE: " + this.value + ")");
            });
        });

        function modifySelect() {
            $("select").get(0).selectedIndex = 5;
        }

        function appendSelectOption(str) {
            $("select").append("<option value=\"" + str + "\">" + str + "</option>");
        }

        function applyOptions() {
            $("select").searchable({
                maxListSize: $("#maxListSize").val(),
                maxMultiMatch: $("#maxMultiMatch").val(),
                latency: $("#latency").val(),
                exactMatch: $("#exactMatch").get(0).checked,
                wildcards: $("#wildcards").get(0).checked,
                ignoreCase: $("#ignoreCase").get(0).checked
            });

            alert(
				"OPTIONS\n---------------------------\n" +
				"maxListSize: " + $("#maxListSize").val() + "\n" +
				"maxMultiMatch: " + $("#maxMultiMatch").val() + "\n" +
				"exactMatch: " + $("#exactMatch").get(0).checked + "\n" +
				"wildcards: " + $("#wildcards").get(0).checked + "\n" +
				"ignoreCase: " + $("#ignoreCase").get(0).checked + "\n" +
				"latency: " + $("#latency").val()
			);
        }
	</script>

      <script type="text/javascript">
          $(document).ready(function () {
              $("select").searchable();
          });


          // demo functions
          $(document).ready(function () {
              $("#value").html($("#ddStaffNameDepartment :selected").text() + " (VALUE: " + $("#ddStaffNameDepartment").val() + ")");
              $("select").change(function () {
                  $("#value").html(this.options[this.selectedIndex].text + " (VALUE: " + this.value + ")");
              });
          });

          function modifySelect() {
              $("select").get(0).selectedIndex = 5;
          }

          function appendSelectOption(str) {
              $("select").append("<option value=\"" + str + "\">" + str + "</option>");
          }

          function applyOptions() {
              $("select").searchable({
                  maxListSize: $("#maxListSize").val(),
                  maxMultiMatch: $("#maxMultiMatch").val(),
                  latency: $("#latency").val(),
                  exactMatch: $("#exactMatch").get(0).checked,
                  wildcards: $("#wildcards").get(0).checked,
                  ignoreCase: $("#ignoreCase").get(0).checked
              });

              alert(
                  "OPTIONS\n---------------------------\n" +
                  "maxListSize: " + $("#maxListSize").val() + "\n" +
                  "maxMultiMatch: " + $("#maxMultiMatch").val() + "\n" +
                  "exactMatch: " + $("#exactMatch").get(0).checked + "\n" +
                  "wildcards: " + $("#wildcards").get(0).checked + "\n" +
                  "ignoreCase: " + $("#ignoreCase").get(0).checked + "\n" +
                  "latency: " + $("#latency").val()
              );
          }
	</script>

      <script type="text/javascript">
          $(document).ready(function () {
              $("select").searchable();
          });


          // demo functions
          $(document).ready(function () {
              $("#value").html($("#ddAirlinefileter :selected").text() + " (VALUE: " + $("#ddAirlinefileter").val() + ")");
              $("select").change(function () {
                  $("#value").html(this.options[this.selectedIndex].text + " (VALUE: " + this.value + ")");
              });
          });

          function modifySelect() {
              $("select").get(0).selectedIndex = 5;
          }

          function appendSelectOption(str) {
              $("select").append("<option value=\"" + str + "\">" + str + "</option>");
          }

          function applyOptions() {
              $("select").searchable({
                  maxListSize: $("#maxListSize").val(),
                  maxMultiMatch: $("#maxMultiMatch").val(),
                  latency: $("#latency").val(),
                  exactMatch: $("#exactMatch").get(0).checked,
                  wildcards: $("#wildcards").get(0).checked,
                  ignoreCase: $("#ignoreCase").get(0).checked
              });

              alert(
                  "OPTIONS\n---------------------------\n" +
                  "maxListSize: " + $("#maxListSize").val() + "\n" +
                  "maxMultiMatch: " + $("#maxMultiMatch").val() + "\n" +
                  "exactMatch: " + $("#exactMatch").get(0).checked + "\n" +
                  "wildcards: " + $("#wildcards").get(0).checked + "\n" +
                  "ignoreCase: " + $("#ignoreCase").get(0).checked + "\n" +
                  "latency: " + $("#latency").val()
              );
          }
	</script>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <fieldset class="boxBody" >
 <asp:Label ID="Label1" runat="server" 
            Text="Indent" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 </fieldset>


    <table cellpadding="0px" cellspacing="0px" style="width:100%"> <tr> <td style="background-color:#e5e3e3">

             <table cellpadding="0px" cellspacing="0px" style="width:100%"> 
                <tr> <td style="height:10px">  </td></tr>
                 <tr> <td >   <table cellpadding="0px" cellspacing="0px" style="width:100%"> <tr> <td> &nbsp; &nbsp;<asp:Label ID="Label3" runat="server" Text="General" Font-Bold="True" Font-Size="10pt" ForeColor="Black"></asp:Label>  </td> <td align="right">  <asp:Button ID="btnback" runat="server" Text="Back for Details" OnClick="btnback_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr> </table>  </td></tr>

                 <tr> <td style="height:10px">  </td></tr>
            </table>

                                                     </td></tr>

        <tr> <td>   <table cellpadding="0px" cellspacing="0px"> <tr> <td colspan="8">   </td></tr>
 
        <tr> <td colspan="8" style="height:5px"> </td></tr>

        <tr> <td style="width:10px"> </td> <td> Indent No </td> <td style="width:10px">  </td> <td>
            <asp:TextBox ID="txtIndentno" runat="server" Enabled="False" Width="150px"></asp:TextBox>  
            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtIndentno" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="indentdh"></asp:RequiredFieldValidator>--%>
            </td> <td style="width:100px">  </td> <td> Issue Date </td> <td style="width:10px">  </td> <td>  <asp:TextBox ID="txtIssueDate" runat="server" Enabled="False"></asp:TextBox></td></tr>
         <tr> <td colspan="8" style="height:5px"> </td></tr>

        <tr><td style="width:10px"> </td> <td> Issue For </td> <td style="width:10px">  </td> <td>
            <asp:DropDownList ID="ddIssueFor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddIssueFor_SelectedIndexChanged" Width="150px" Height="25px">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="1">Department</asp:ListItem>
             <%--   <asp:ListItem Value="2">Employee</asp:ListItem>--%>
            </asp:DropDownList>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddIssueFor" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="indentdh"></asp:RequiredFieldValidator>
              </td> <td style="width:100px">  </td> <td> User ID </td> <td style="width:10px">  </td> <td>  <asp:TextBox ID="txtIssueUserid" runat="server" Enabled="False"></asp:TextBox></td></tr>

         <tr> <td colspan="8" style="height:5px"> </td></tr>
          <tr> <td style="width:10px"> </td><td> Issue ID </td> <td style="width:10px">  </td> <td>
              <asp:DropDownList ID="ddIssueid" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddIssueid_SelectedIndexChanged" Width="150px" Height="25px">
              </asp:DropDownList>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddIssueid" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="indentdh"></asp:RequiredFieldValidator>
              </td> <td style="width:100px">  </td> <td> Status </td> <td style="width:10px">  </td> <td>  
              <asp:DropDownList ID="ddStatus" runat="server" Enabled="False" Width="150px" Height="25px">
                  <asp:ListItem Value="1">Open</asp:ListItem>
              </asp:DropDownList>
              </td></tr>
         <tr> <td colspan="8" style="height:5px"> </td></tr>
          <tr><td style="width:10px"> </td> <td> Issue Name </td> <td style="width:10px">  </td> <td>
              <asp:TextBox ID="txtIssueName" runat="server" Enabled="False" Width="400px"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtIssueName" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="indentdh"></asp:RequiredFieldValidator>
              </td> <td style="width:100px">  </td> <td>  </td> <td style="width:10px">  </td> <td>  </td> </tr>
         <tr> <td colspan="8" style="height:5px"> </td></tr>



    </table> </td></tr>


        <tr> <td style="background-color:#e5e3e3">
            <table cellpadding="0px" cellspacing="0px"> 
                <tr> <td style="height:10px"> </td></tr>
                 <tr> <td > &nbsp; &nbsp; <asp:Label ID="Label2" runat="server" Text="Indent Sub Form" Font-Bold="True" Font-Size="10pt" ForeColor="Black"></asp:Label>  </td></tr>

                 <tr> <td style="height:10px">  </td></tr>
            </table>

             </td></tr>


<tr> <td>  <table cellpadding="0px" cellspacing="0px" style="width:100%"> 
    
    <tr> <td style="width:10px"> </td> <td> No.</td> <td> Name </td> <td>  Item No </td> <td> Description </td> <td> Unit of Measure</td> <td>  Quantity</td> <td> </td> <td style="width:10px"> </td></tr>
    
     <tr> <td style="width:10px"> </td> <td> <asp:TextBox ID="txtDepartmentCode" runat="server" Enabled="False" Width="100px"></asp:TextBox>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDepartmentCode" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="indentdh"></asp:RequiredFieldValidator>
         </td> <td> <asp:TextBox ID="txtDepartmentName" runat="server" Enabled="False" Width="250px"></asp:TextBox> 
             <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtDepartmentName" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="indentdh"></asp:RequiredFieldValidator>
         </td> <td>  <asp:DropDownList ID="ddItemNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddItemNo_SelectedIndexChanged" Width="150px" Height="25px"></asp:DropDownList> 
             <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddItemNo" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="indentdh"></asp:RequiredFieldValidator>
         </td> <td> <asp:TextBox ID="txtDescription" runat="server" Enabled="false" Width="300px"></asp:TextBox> 
             <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtDescription" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="indentdh"></asp:RequiredFieldValidator>
         </td> <td> <asp:TextBox ID="txtUnitofMeasure" runat="server" Enabled="false" Width="80px"></asp:TextBox></td>  <td>  <asp:TextBox ID="txtQuantityforRequistion" runat="server" Width="80px"  onkeypress="return isNumberKey(event)">0</asp:TextBox>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtQuantityforRequistion" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="indentdh"></asp:RequiredFieldValidator>
         </td> <td> <%-- <asp:TextBox ID="txtInventory" runat="server" Enabled="false" Width="80px">0</asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtInventory" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="indentdh"></asp:RequiredFieldValidator>--%>
         </td> <td style="width:10px"> </td></tr>



     




           </table></td></tr>         

        <tr> <td style="height:5px">  </td></tr>

        <tr> <td align="right">  
            <asp:Label ID="lblAcademicyrs" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblGenpostingGroup" runat="server" Visible="False"></asp:Label>
            <asp:TextBox ID="txtOldIndent" runat="server" Enabled="False" Visible="False"></asp:TextBox>
            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" ValidationGroup="indentdh" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td></tr>

          <tr> <td style="height:5px">  </td></tr>

       <tr> <td> 

            <asp:GridView ID="grdViewIndentLine" runat="server"  AutoGenerateColumns="False" ForeColor="#333333" GridLines="None" CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" >
                <Columns>
                    <asp:BoundField HeaderText="No_" DataField="No_" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:TemplateField HeaderText="Item No.">
                        <ItemTemplate>
                            <asp:Label ID="lblItemNo_Grid" runat="server" Text='<%#Bind("[Item No]") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                            <asp:Label ID="lblItemNoDescription_Grid" runat="server" Text='<%#Bind("[Description]") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit of Measure">

                           <ItemTemplate>
                            <asp:Label ID="lblUnitofMeasure_Grid" runat="server" Text='<%#Bind("[Unit of Measure]") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Variance Code"></asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Qty. for Requsition">
                          <ItemTemplate>
                            <asp:Label ID="lblQuantity_Grid" runat="server" Text='<%#Bind("Quantity","{0:n}" ) %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>

                    
                  
                   
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnDeleted" runat="server" Text="Delete" CommandArgument='<%# Eval("[Line No_]") %>' OnCommand="btnDeleted_Command"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                    
                  
                   
                </Columns>
               <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle  BackColor="#ff9900" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            </td></tr>
        <tr> <td style="height:5px">  </td></tr>

         <tr> <td align="right"> <asp:Button ID="btnSendforApproval" runat="server" Text="Send For Approval" Visible="false" OnClick="btnSendforApproval_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td></tr>

         <tr> <td style="height:95px">  </td></tr>

    </table>


  








</asp:Content>

