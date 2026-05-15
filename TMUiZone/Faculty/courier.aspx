<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="courier.aspx.cs" Inherits="Faculty_courier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script type="text/javascript" src="dropdowneditable/jquery.min.js"></script>
 <script type="text/javascript" src="dropdowneditable/jquery.searchabledropdown-1.0.8.min.js"></script>
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
 <style type="text/css">
     .ScrollStyle
{
    max-height: 500px;
    overflow-y: scroll;
}
 </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="COURIER MASTER" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <table cellpadding="0px" cellspacing="0px" style="margin-left:50px">
        <tr>
            <td colspan="15">
                <table cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td colspan="11" style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td>
                            <label style="line-height: 25px">BAR CODE</label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="txtBarcode" runat="server" Width="220px"></asp:TextBox>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <label style="line-height: 25px">REF</label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="txtRef" runat="server"  Width="220px"></asp:TextBox>
                        </td>
                        <td style="width: 10px"></td>

                         <td>
                            <label style="line-height: 25px">PIN CODE</label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="txtPin" runat="server" AutoPostBack="true" Width="220px" onkeypress="return isNumberKey(event)" OnTextChanged="txtPin_TextChanged"></asp:TextBox>
                        </td>
                       
                    </tr>
                    <tr>
                        <td colspan="11" style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td>
                            <label style="line-height: 25px">CITY</label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="txtCity" runat="server"  Width="220px"></asp:TextBox>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <label style="line-height: 25px">NAME</label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server"  Width="220px"></asp:TextBox>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <label style="line-height: 25px">ADD1</label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="txtADD1" runat="server"  Width="220px"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="11" style="height: 10px"></td>
                    </tr>
                     <tr>
                        <td>
                            <label style="line-height: 25px">ADD2</label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="txtAdd2" runat="server" Width="220px"></asp:TextBox>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <label style="line-height: 25px">ADD3</label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="txtAdd3" runat="server"  Width="220px"></asp:TextBox>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <label style="line-height: 25px">ADDREMAIL</label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="txtMail" runat="server"  Width="220px"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="11" style="height: 10px"></td>
                    </tr>
                     <tr>
                        <td>
                            <label style="line-height: 25px">ADDRMOBILE</label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="TextAddMobile" runat="server" Width="220px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <label style="line-height: 25px">SENDERMOBILE</label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="TextSenMobile" runat="server"  Width="220px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <label style="line-height: 25px">WEIGHT</label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="TextWeight" runat="server"  Width="220px"></asp:TextBox>
                        </td>

                    </tr>
                     <tr>
                        <td colspan="11" style="height: 10px"></td>
                    </tr>
                     <tr>
                        <td>
                            <label style="line-height: 25px">COD</label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="TextCOD" runat="server" Width="220px"></asp:TextBox>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <label style="line-height: 25px">INSVAL</label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="TextINSVAL" runat="server"  Width="220px"></asp:TextBox>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <label style="line-height: 25px">VPP</label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="TextVPP" runat="server"  Width="220px"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="11" style="height: 10px"></td>
                    </tr>
                     <tr>
                        <td>
                            <label style="line-height: 25px">L</label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="TextL" runat="server" Width="220px"></asp:TextBox>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <label style="line-height: 25px">B</label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="TextB" runat="server"  Width="220px"></asp:TextBox>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <label style="line-height: 25px">H</label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="TextH" runat="server"  Width="220px"></asp:TextBox>
                        </td>

                    </tr>
                     <tr>
                        <td colspan="11" style="height: 10px"></td>
                    </tr>
                     <tr>
                        <td>
                            <label style="line-height: 25px">CONTENT TYPE</label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="TextContType" runat="server" Width="220px"></asp:TextBox>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <label style="line-height: 25px">DEPARTMENT</label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                          <asp:DropDownList ID="ddIssueid" runat="server" AutoPostBack="true"  Width="218px" Height="26px">
              </asp:DropDownList>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                           
                        </td>
                        <td style="width: 10px;text-align:right">
                              <asp:Button ID="btnSave" runat="server" Width="100px" OnClick="btnSave_Click" BackColor="Green" ForeColor="White" Text="SAVE" />

                            
                        </td>

                        <td>
                            <asp:Button ID="btnExport" runat="server" Width="140px" OnClick="btnExport_Click" BackColor="Green" ForeColor="White" Text="EXPORT TO EXCEL" />

                        </td>

                    </tr>
                    </table>
                </td>
            </tr>

        </table>
    <br />
    <br />
    <table>
        <tr>
            <td>
                <div class="ScrollStyle" style="width:1200px;height:500px" >
                <asp:GridView ID="grddata" runat="server" Style="margin-left:2%; margin-right: 2%;height:50%; width: 80%" Visible="true" 
                    CssClass="table table-striped table-bordered table-hover" AlternatingRowStyle-CssClass="danger"></asp:GridView>
           </div>
                     </td>
        </tr>
    </table>
</asp:Content>

