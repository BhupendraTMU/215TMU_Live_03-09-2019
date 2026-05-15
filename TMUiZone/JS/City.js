
var countries = Object();
countries['Asia'] = 'India|Bangladesh|Nepal|Thailand';

////////////////////////////////////////////////////////////////////////////

var states = Object();
//Asia
states['India'] = 'New Delhi|Andaman/Nicobar Islands|Andhra Pradesh|Arunachal Pradesh|Assam|Bihar|Chandigarh|Chhattisgarh|Dadra/Nagar Haveli|Daman/Diu|Goa|Gujarat|Haryana|Himachal Pradesh|Jammu/Kashmir|Jharkhand|Karnataka|Kerala|Lakshadweep|Madhya Pradesh|Maharashtra|Manipur|Meghalaya|Mizoram|Nagaland|Orissa|Pondicherry|Punjab|Rajasthan|Sikkim|Tamil Nadu|Tripura|Uttaranchal|Uttar Pradesh|West Bengal';
states['Bangladesh'] = 'Dhaka|Barisal|Chittagong|Khulna|Rajshahi|Sylhet';
states['Nepal'] = 'Kathmandu|Bagmati|Bheri|Dhawalagiri|Gandaki|Janakpur|Karnali|Kosi|Lumbini|Mahakali|Mechi|Narayani|Rapti|Sagarmatha|Seti';
states['Thailand'] = 'Bangkok|Amnat Charoen|Ang Thong|Buriram|Chachoengsao|Chai Nat|Chaiyaphum|Chanthaburi|Chiang Mai|Chiang Rai|Chon Buri|Chumphon|Kalasin|Kamphaeng Phet|Kanchanaburi|Khon Kaen|Krabi|Lampang|Lamphun|Loei|Lop Buri|Mae Hong Son|Maha Sarakham|Mukdahan|Nakhon Nayok|Nakhon Pathom|Nakhon Phanom|Nakhon Ratchasima|Nakhon Sawan|Nakhon Si Thammarat|Nan|Narathiwat|Nong Bua Lamphu|Nong Khai|Nonthaburi|Pathum Thani|Pattani|Phangnga|Phatthalung|Phayao|Phetchabun|Phetchaburi|Phichit|Phitsanulok|Phra Nakhon Si Ayutthaya|Phrae|Phuket|Prachin Buri|Prachuap Khiri Khan|Ranong|Ratchaburi|Rayong|Roi Et|Sa Kaeo|Sakon Nakhon|Samut Prakan|Samut Sakhon|Samut Songkhram|Sara Buri|Satun|Sing';

////////////////////////////////////////////////////////////////////////////
var district = Object();
district['Delhi'] = 'New Delhi|North Delhi|North West Delhi|West Delhi|South West Delhi|South Delhi|Central Delhi|Noth East Delhi||Shahdra|East Delhi';
district['Uttar Pradesh']='Allahabad|Kanpur|Lucknow|Gorakhpur|Noida|Gaziabad|Varanshi|Mooradabad'
////////////////////////////////////////////////////////////////////////////

function FillCountryDropDown(country) {
    for (region in states) {
        $('<option />', { value: region, text: region }).appendTo(country);
    }

    // Logic to Sort the Country Options
    // Keep track of the selected option.
    var selectedValue = $(country).val();

    // Sort all the options by text. I could easily sort these by val.
    $(country).html($("option", $(country)).sort(function (a, b) {
        return a.text == b.text ? 0 : a.text < b.text ? -1 : 1
    }));

    // Select one option.
    $(country).val(selectedValue);
}

function FillStatesDropDown(oCountrySel, oStateSel) {
    var countryArr;
    $('#' + oStateSel + ' > option').remove();
    $('<option />', { value: '', text: '--- Select/Specify State ---' }).appendTo('#' + oStateSel);
    if (oCountrySel.selectedIndex >= 0) {
        var region = oCountrySel.options[oCountrySel.selectedIndex].text;
        if (city_states[region]) {
            countryArr = states[region].split('|');
            for (var i = 0; i < countryArr.length; i++) {
                $('<option />', { value: countryArr[i], text: countryArr[i] }).appendTo('#' + oStateSel);
            }
        }
    }
}

function ReloadCountryStateValue(oStateSel, CustomStateFieldID) {
    $('select[id*=ddlCountry]').val($('input[id*=hdnCountry]').val());
    FillStatesDropDown($('select[id*=ddlCountry]')[0], oStateSel);
    $('select[id*=ddlstate]').val($('input[id*=hdnState]').val());
    if ($('select[id*=ddlstate]').val() == null) {
        $('#' + CustomStateFieldID).show();
        $('input[id*=txtState]').val($('input[id*=hdnState]').val());
        $('select[id*=ddlstate]')[0].selectedIndex = 0;
    }
    else {
        $('#' + CustomStateFieldID).hide();
    }
}

function SetCountryStateValue() {
    $('input[id*=hdnCountry]').val($('select[id*=ddlCountry]').val());
    if ($('select[id*=ddlstate]').val() == "") {
        $('input[id*=hdnState]').val($('input[id*=txtState]').val());
    }
    else {
        $('input[id*=hdnState]').val($('select[id*=ddlstate]').val());
    }
}