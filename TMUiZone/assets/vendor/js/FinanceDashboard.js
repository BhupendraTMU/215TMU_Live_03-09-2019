google.load("visualization", "1", { packages: ["corechart"] });
google.setOnLoadCallback(drawCharts);
function drawCharts() {
    var Session = document.getElementById("ContentPlaceHolder1_ddlSession").value;  
    var Acdyear = document.getElementById("ContentPlaceHolder1_ddlAcademicYear").value;
    var Programe = document.getElementById("ContentPlaceHolder1_ddlProgram").value;
    var InstCode = document.getElementById("ContentPlaceHolder1_listacademicname").value;
    var dataScholarshipprogram = new google.visualization.DataTable();
    var dataScholarshipcategory = new google.visualization.DataTable();
    //alert(Session + Acdyear + Programe);
    $(function () {
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            url: 'FinanceDashboard.aspx/ScholarshipProgramwise',
            data: "{instcode:'" + InstCode
            + "', session:'" + Session
            + "', graduation:'" + Programe
            + "', academic:'" + Acdyear
            + "'}",
            contentType: "application/json; charset=utf-8", dataType: "json",
            success: function (response) { Scholarshipprogram(response.d, dataScholarshipprogram); // calling method  
            }//, error: function () { alert("Error loading data Students Attendance! Please try again."); }
        });

    })
    $(function () {

        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            url: 'FinanceDashboard.aspx/ScholarshipCategorywise',
            data: "{instcode:'" + InstCode
            + "', session:'" + Session
            + "', graduation:'" + Programe
            + "', academic:'" + Acdyear
            + "'}",
            contentType: "application/json; charset=utf-8", dataType: "json",
            success: function (response) {Scholarshipcategory(response.d, dataScholarshipcategory); // calling method  
            }//,error: function () {  alert("Error loading data Students Attendance! Please try again.");  }
        });
    })
}

//Scholorship Programe
function Scholarshipprogram(dataValues, dataScholarshipprogram) {
    //var dataStudentsAttendance = new google.visualization.DataTable();

    dataScholarshipprogram.addColumn('string', 'Programe');
    dataScholarshipprogram.addColumn('number', 'Male');
    dataScholarshipprogram.addColumn({ type: 'string', role: 'annotation' }); // annotation role col.
    dataScholarshipprogram.addColumn('number', 'Female');
    dataScholarshipprogram.addColumn({ type: 'string', role: 'annotation' }); // annotation role col.

    for (var i = 0; i < dataValues.length; i++) {
        dataScholarshipprogram.addRow([dataValues[i].Program, dataValues[i].Male, dataValues[i].Mannotation, dataValues[i].Female, dataValues[i].Fannotation]);
    }

    ScholarProgram(dataScholarshipprogram);
}

//Scholorship Programe
function ScholarProgram(dataScholarshipprogram) {
    var optionsScholarshipprogram = {
        backgroundColor: 'transparent',
        colors: ['cornflowerblue', 'tomato'],
        fontName: 'Open Sans',
        chartArea: {
            left: 200,
            top: 10,
            width: '40%',
            height: '80%'
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
    var chartScholarshipprogram = new google.visualization.BarChart(document.getElementById('chartScholarshipFeeProgramWise'));
    chartScholarshipprogram.draw(dataScholarshipprogram, optionsScholarshipprogram);
}

//Scholorship Category
function Scholarshipcategory(dataValues, dataScholarshipcategory) {
    //var dataStudentsAttendance = new google.visualization.DataTable();

    dataScholarshipcategory.addColumn('string', 'Category');
    dataScholarshipcategory.addColumn('number', 'Male');
    dataScholarshipcategory.addColumn({ type: 'string', role: 'annotation' }); // annotation role col.
    dataScholarshipcategory.addColumn('number', 'Female');
    dataScholarshipcategory.addColumn({ type: 'string', role: 'annotation' }); // annotation role col.

    for (var i = 0; i < dataValues.length; i++) {
        dataScholarshipcategory.addRow([dataValues[i].Category, dataValues[i].Male,
        dataValues[i].Mannotation, dataValues[i].Female, dataValues[i].Fannotation]);
    }

    Scholarcategory(dataScholarshipcategory);
}

//Scholorship Programe
function Scholarcategory(dataScholarshipcategory) {
    var optionsScholarshipcategory = {
        backgroundColor: 'transparent',
        colors: ['cornflowerblue', 'tomato'],
        fontName: 'Open Sans',
        chartArea: {
            left: 150,
            top: 10,
            width: '40%',
            height: '80%'
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
    var chartScholarshipcategory = new google.visualization.BarChart(document.getElementById('chartScholarshipFeeCategoryWise'));
    chartScholarshipcategory.draw(dataScholarshipcategory, optionsScholarshipcategory);
}