<%@ Page Language="C#" AutoEventWireup="true" CodeFile="City.aspx.cs" Inherits="ZZZZZ_City" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    

<head runat="server">
    <title></title>
    <script src="../JS/City.js"></script>
    <script type="text/javascript">
        //function print_country(country) {
        //    //given the id of the <select> tag as function argument, it inserts <option> tags
        //    var option_str = document.getElementById(country);
        //    option_str.length = 0;
        //    option_str.options[0] = new Option('Select Country', '');
        //    option_str.selectedIndex = 0;
        //    for (var i = 0; i < country_arr.length; i++) {
        //        option_str.options[option_str.length] = new Option(country_arr[i], country_arr[i]);
        //    }
        //}

        //function print_state(state, selectedIndex) {
        //    var option_str = document.getElementById(state);
        //    option_str.length = 0;    // Fixed by Julian Woods
        //    option_str.options[0] = new Option('Select State', '');
        //    option_str.selectedIndex = 0;
        //    var state_arr = s_a[selectedIndex].split("|");
        //    for (var i = 0; i < state_arr.length; i++) {
        //        option_str.options[option_str.length] = new Option(state_arr[i], state_arr[i]);
        //    }
        //}
        //function FillCountryDropDown(country) {
        //    for (region in city_states) {
        //        $('<option />', { value: region, text: region }).appendTo(country);
        //    } 
        //    // Logic to Sort the Country Options
        //    // Keep track of the selected option.
        //    var selectedValue = $(country).val();
 
        //    // Sort all the options by text. I could easily sort these by val.
        //    $(country).html($("option", $(country)).sort(function (a, b) {
        //        return a.text == b.text ? 0 : a.text < b.text ? -1 : 1
        //    }));
 
        //    // Select one option.
        //    $(country).val(selectedValue);
        //}
        //function FillStatesDropDown(oCountrySel, oStateSel) {
        //    var countryArr;    
        //    $('#' + oStateSel + ' > option').remove();
        //    $('<option />', { value: '', text: '--- Select/Specify State ---' }).appendTo('#' + oStateSel);
        //    if (oCountrySel.selectedIndex >= 0) {
        //        var region = oCountrySel.options[oCountrySel.selectedIndex].text;
        //        if (city_states[region]) {
        //            countryArr = city_states[region].split('|');
        //            for (var i = 0; i < countryArr.length; i++) {                
        //                $('<option />', { value: countryArr[i], text: countryArr[i] }).appendTo('#' + oStateSel);
        //            }
        //        }
        //    }
        //}
 
        
</script>

</head>

<body>
    <form id="form1" runat="server">
    <div>  
<table>
<tr>
<td style="text-align: left;">Country:</td>
<td style="text-align: left;">
<select name="country" id="ddlCountry" onchange="FillStatesDropDown('country','India')">
  <option value="Bangladesh">Bangladesh</option>
  <option value="India">India</option>  
</select>
</td>
</tr><tr>
<td style="text-align: left;">State:</td>
<td style="text-align: left;">
<select name="state" id="ddlstate" onchange="setCities();">
  <option value="">Please select a Country</option>
</select>
</td>
</tr><tr>
<td style="text-align: left;">City:</td>
<td style="text-align: left;">
<select name="city"  id="ddlcity">
  <option value="">Please select a Country</option>
</select>
</td>
</tr>
</table>
    </div>
    </form>
</body>
</html>
