//function getOverlays(callback){
//    var url = 'myServerUrl';
//    overlays = [];

//    $.ajax({
//        url: url,
//        dataType: 'jsonp',
//        jsonpCallback: 'getJson',
//        success: function(response) {
//            overlays.push({
//                name: "Something",
//                layer: L.Proj.geoJson(response, {
//                ...
//                }
//                });
//            callback(overlays)
//        }
//    });
//    return overlays;
//}

//var map = L.map('map', {
//    layers: layers[0].layer
//});
//var layers = getBaseLayers();
//getOverlays(function(overlays){
//    var panelLayers = new L.Control.PanelLayers(layers,overlays);
//    map.addControl(panelLayers);
//});


//$.ajax({
//    type: "post",
//    url: "DashboardChart.aspx/GetChartProgramewiseFilter",
//    contentType: "application/json; charset=utf-8",
//    dataType: "json",
//    success: function (response) {
//        var resources = [];
//        var xmlDoc = $.parseXML(response.d);
//        var xml = $(xmlDoc);
//        var customers = xml.find("StateTb");
//        if (customers.length > 0) {
//            $(customers).each(function () {
//                resources.push({
//                    id: $(this).find("Id").text(),
//                    name: $(this).find("name").text(),
//                    userlevel: $(this).find("UserLevel").text(),
//                    group: $(this).find("Group").text(),
//                    email: $(this).find("email").text(),
//                    gender: $(this).find("gender").text(),
//                    birthday: $(this).find("birthday").text(),
//                    zipcode: $(this).find("zipCode").text(),
//                    address: $(this).find("address").text(),
//                    times: $(this).find("times").text()
//                });
//            });
//        }

//        callbacks(resources);
//        $('.loader').hide();
//    },
//    error: function () {
//        alert('could not get the data');
//    },
//});




//Final
google.load("visualization", "1", {
    packages: ["corechart"]
});
//google.charts.load('current', { packages: ['map'] });
google.setOnLoadCallback(drawCharts);
function drawCharts() {
    var selectedVal = this.value
      , Session = document.getElementById("SelectSession").value
      , Acdyear = document.getElementById("SelectAcd").value
      , Programe = document.getElementById("SelectPrograme").value;
    $(function () {
        $.ajax({
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            url: "DashboardChart.aspx/GetChartProgramewiseFilter",
            data: "{session:'All', graduation:'All', academicyear:'2017-2018'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (a) {
                drawchartprogramechart(a.d)
            },
            error: function () {
                alert("Error loading data Programe wise! Please try again.")
            }
        })
    });

    function drawchartprogramechart(dataValues) {
        // Callback that creates and populates a data table,  
        // instantiates the pie chart, passes in the data and  
        // draws it.  
        var dataProgram = new google.visualization.DataTable();
       
        dataProgram.addColumn("string", "Program"),
        dataProgram.addColumn({
            type: "string",
            role: "Code"
        }),
        dataProgram.addColumn("number", "Male"),
        dataProgram.addColumn({
            type: "string",
            role: "annotation"
        }),
        dataProgram.addColumn("number", "Female"),
        dataProgram.addColumn({
            type: "string",
            role: "annotation"
        });
       
      
        for (var i = 0; i < dataValues.length; i++)
            dataProgram.addRow([dataValues[i].Program, dataValues[i].Code, dataValues[i].Male, dataValues[i].Mannotation, dataValues[i].Female, dataValues[i].Fannotation]);
       
        // 
        //data.addRow(['s', 1, 1]);
        var optionsProgram = {
            backgroundColor: 'transparent',
            colors: ['cornflowerblue', 'tomato'],
            fontName: 'Open Sans',
            //isStacked: 'percent',
            chartArea: {
                left: 300,
                top: 20,
                width: '55%',
                height: '95%'
            },
            bar: {
                groupWidth: '80%'
            },
            hAxis: {
                textStyle: {
                    fontSize: 11
                }
            },
            vAxis: {
                minValue: 0,
                maxValue: 1500,
                baselineColor: '#DDD',
                gridlines: {
                    color: '#DDD',
                    count: 4
                },
                textStyle: {
                    fontSize: 11
                }
            },
            legend: {
                textStyle: {
                    fontSize: 12
                }
            },
            animation: {
                duration: 1200,
                easing: 'out',
                startup: true
            }

        };
        var chartProgram = new google.visualization.BarChart(document.getElementById('mychartProgram'));
        
        chartProgram.draw(dataProgram, optionsProgram);
        google.visualization.events.addListener(chartProgram, 'select', selectHandler);
        function selectHandler() {
            var selectedItem = chartProgram.getSelection();
            var row = selectedItem[0].row;
            var course = dataProgram.getValue(row, 0);
            var code = dataProgram.getValue(row, 1);
            var male = dataProgram.getValue(row, 2);
           // $("[id$=hfCustomerId]").val(male);
            var female = dataProgram.getValue(row, 4);
            window.open("Popup.aspx?Code=" + code + "&Male=" + male + "&Female=" + female + "&Session=All&Graduation=All&Academicyear=2017-2018", "List", "toolbar=no, location=no,status=yes,menubar=no,scrollbars=yes,resizable=no, width=900,height=500,left=250,top=100");
            //window.location.href = "Popup.aspx?Code=" + code + "&Male=" + male + "&Female=" + female + "&Session=All&Graduation=All&Academicyear=2017-2018";
           // $('.para').html('The user selected ' + course + ', Code is ' + code + ', Male are ' + male + ' and Female are ' + female);
           // Fill_Male_FemaleData()
          // barchartDetails();
        }

    }
}