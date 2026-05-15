google.load("visualization", "1", { packages: ["corechart"] });
google.setOnLoadCallback(drawCharts);
function drawCharts() {
    var Session = document.getElementById("ContentPlaceHolder1_ddlsession").value,
        Acdyear = document.getElementById("ContentPlaceHolder1_ddlacadmicyear").value,
        Programe = document.getElementById("ContentPlaceHolder1_ddlprogram").value,
        InstCode = document.getElementById("ContentPlaceHolder1_listacademicname").value,
        Semester = document.getElementById("SelectSemester").value, 
        Programgpa = document.getElementById("SelectProgram").value, 
        ProgramCGPA = document.getElementById("SelectCGpa").value,
        programeeligible = document.getElementById("SelectEligiblePrograme").value,
        programeeligiblecategory = document.getElementById("SelectEligibleCategory").value,
        programeeligiblesss = document.getElementById("SelectSSSExamination").value,
        dataStudentsAttendance = new google.visualization.DataTable,
        dataEligibleListProgram = new google.visualization.DataTable,
        dataEligibleListCategory = new google.visualization.DataTable,
        dataEligibleListReevalution = new google.visualization.DataTable,
        dataListGpaReport = new google.visualization.DataTable,
        dataListCGpaReport = new google.visualization.DataTable;
    $(function ()
    {
        $.ajax({
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            url: "AcademicDashboard.aspx/StudentsAttendance",
            data: "{instcode:'" + InstCode + "',session:'" + Session + "', graduation:'" + Programe + "', academic:'" + Acdyear + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (e) { studentAttendance(e.d, dataStudentsAttendance) },
            error: function () { alert("Error loading data Students Attendance! Please try again.") }
        })
    }), $(function () {
        $.ajax({
            type: "POST", dataType: "json",
            contentType: "application/json",
            url: "AcademicDashboard.aspx/EligibleListProgram",
            data: "{instcode:'" + InstCode + "',session:'" + Session + "', graduation:'" + Programe + "', academic:'" + Acdyear + "', programe:'" + programeeligible + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (e)
            {
                EligibleListProgram(e.d, dataEligibleListProgram)
            },
            error: function ()
            { alert("Error loading data Students Attendance! Please try again.") }
        })
        }), $(function () {
            $.ajax({
                type: "POST",
                dataType: "json",
                contentType: "application/json", 
                url: "AcademicDashboard.aspx/EligibleListCategory",
                data: "{instcode:'" + InstCode + "',session:'" + Session + "', graduation:'" + Programe + "', academic:'" + Acdyear + "', programe:'" + programeeligiblecategory + "'}",
                contentType: "application/json; charset=utf-8", dataType: "json",
                success: function (e) { EligibleListCategory(e.d, dataEligibleListCategory) },
                error: function () { alert("Error loading data Eligible List Category Wise! Please try again.") }
            })
        }), $(function () {
            $.ajax({
                type: "POST",
                dataType: "json",
                contentType: "application/json",
                url: "AcademicDashboard.aspx/EligibleListReevalution",
                data: "{instcode:'" + InstCode + "',session:'" + Session + "', graduation:'" + Programe + "', academic:'" + Acdyear + "', programe:'" + programeeligiblesss + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (e) { EligibleListReevalution(e.d, dataEligibleListReevalution) },
                error: function () { alert("Error loading data Eligible List Reevalution List! Please try again.") }
            })
        }), $(function ()
        {
            $.ajax({
                type: "POST",
                dataType: "json",
                contentType: "application/json",
                url: "AcademicDashboard.aspx/GPA_Report",
                data: "{instcode:'" + InstCode + "',session:'" + Session + "', graduation:'" + Programe + "', academic:'" + Acdyear + "', semester:'" + Semester + "', program:'" + Programgpa + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (e) { GPA_Report(e.d, dataListGpaReport) },
                error: function () { alert("Error loading data GPA Report! Please try again.") }
            })
        }), $(function () {
            $.ajax({
                type: "POST",
                dataType: "json", contentType: "application/json",
                url: "AcademicDashboard.aspx/CGPA_Report",
                data: "{instcode:'" + InstCode + "',session:'" + Session + "', graduation:'" + Programe + "', academic:'" + Acdyear + "', program:'" + ProgramCGPA + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (e) { CGPA_Report(e.d, dataListCGpaReport) },
                error: function () { alert("Error loading data GPA Report! Please try again.") }
            })
        });
}




//Min
function studentAttendance(n, o) {
    o.addColumn("string", "Attendance"),
        o.addColumn("number", "Male"),
        o.addColumn({type: "string",role: "annotation"}),
        o.addColumn("number", "Female"),
        o.addColumn({ type: "string", role: "annotation" });
    for (var t = 0; t < n.length; t++)
        o.addRow([n[t].Attendance,
            n[t].Male, n[t].Mannotation,
            n[t].Female, n[t].Fannotation]); a(o)
} function EligibleListProgram(n, o)
{
    o.addColumn("string", "Program"),
        o.addColumn("number", "Male"),
        o.addColumn({ type: "string", role: "annotation" }),
        o.addColumn("number", "Female"),
        o.addColumn({ type: "string", role: "annotation" });
    for (var a = 0; a < n.length; a++)
        o.addRow([n[a].Programe, n[a].Male,
            n[a].Mannotation, n[a].Female, n[a].Fannotation]);
    optionEligibleListProgram(o)
} function EligibleListCategory(n, o)
{
    o.addColumn("string", "Category"),
        o.addColumn("number", "Male"), o.addColumn({ type: "string", role: "annotation" }),
        o.addColumn("number", "Female"), o.addColumn({ type: "string", role: "annotation" });
    for (var a = 0; a < n.length; a++) o.addRow([n[a].Category, n[a].Male, n[a].Mannotation,
    n[a].Female, n[a].Fannotation]); optionEligibleListCategory(o)
} function EligibleListReevalution(n, o)
{
    o.addColumn("string", "Category"),
        o.addColumn("number", "Male"),
        o.addColumn({ type: "string", role: "annotation" }),
        o.addColumn("number", "Female"),
        o.addColumn({ type: "string", role: "annotation" });
    for (var a = 0; a < n.length; a++)
        o.addRow([n[a].Revaluation, n[a].Male,
            n[a].Mannotation, n[a].Female, n[a].Fannotation]);
    optionEligibleListReevalution(o)
} function GPA_Report(n, o)
{
    o.addColumn("string", "Consolidated Report"),
        o.addColumn("number", "Male"),
        o.addColumn({ type: "string", role: "annotation" }),
        o.addColumn("number", "Female"),
        o.addColumn({ type: "string", role: "annotation" });
    for (var a = 0; a < n.length; a++)
        o.addRow([n[a].Gpareport, n[a].Male, n[a].Mannotation, n[a].Female, n[a].Fannotation]); 
    optionGPA_Report(o)
} function CGPA_Report(n, o)
{
    o.addColumn("string", "Malpractice"),
        o.addColumn("number", "Male"),
        o.addColumn({ type: "string", role: "annotation" }),
        o.addColumn("number", "Female"),
        o.addColumn({ type: "string", role: "annotation" });
    for (var a = 0; a < n.length; a++)
        o.addRow([n[a].CGpareport, n[a].Male, n[a].Mannotation, n[a].Female, n[a].Fannotation]);
    optionCGPA_Report(o)
}

//Students Attendance
function a(dataStudentsAttendance) {
    var optionsStudentsAttendance = {
        backgroundColor: 'transparent',
        colors: ['cornflowerblue', 'tomato'],
        fontName: 'Open Sans',
        chartArea: {
            left: 150,
            top: 10,
            width: '40%',
            height: '90%'
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
    var chartStudentsAttendance = new google.visualization.BarChart(document.getElementById('chartStudentsAttendance'));
    chartStudentsAttendance.draw(dataStudentsAttendance, optionsStudentsAttendance);
}
//Eligible List Program Wise
function optionEligibleListProgram(dataEligibleListProgram) {
    var optionsEligibleProgram = {
        backgroundColor: 'transparent',
        colors: ['cornflowerblue', 'tomato'],
        fontName: 'Open Sans',
        chartArea: {
            left: 200,
            top: 10,
            width: '33%',
            height: '90%'
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
    var chartEligibleProgram = new google.visualization.BarChart(document.getElementById('chartEligibleProgram'));
    chartEligibleProgram.draw(dataEligibleListProgram, optionsEligibleProgram);
}
//Eligible List Category Wise
function optionEligibleListCategory(dataEligibleListCategory) {
    var optionsEligibleCategory = {
        backgroundColor: 'transparent',
        colors: ['cornflowerblue', 'tomato'],
        fontName: 'Open Sans',
        chartArea: {
            left: 100,
            top: 10,
            width: '45%',
            height: '85%'
        },
        bar: {
            groupWidth: '80%'
        },
        hAxis: {
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
    var chartEligibleCategory = new google.visualization.BarChart(document.getElementById('chartEligibleCategory'));
    chartEligibleCategory.draw(dataEligibleListCategory, optionsEligibleCategory);
}
//SSS Eligible List Reevalution List
function optionEligibleListReevalution(dataEligibleListReevalution) {
    var optionsSSSApplication = {
        backgroundColor: 'transparent',
        colors: ['cornflowerblue', 'tomato'],
        fontName: 'Open Sans',
        chartArea: {
            left: 200,
            top: 10,
            width: '33%',
            height: '90%'
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
    var chartSSSApplication = new google.visualization.BarChart(document.getElementById('ChartSSSApplication'));
    chartSSSApplication.draw(dataEligibleListReevalution, optionsSSSApplication);
}
//GPA Report
function optionGPA_Report(dataListGpaReport) {
    var optionsGPA = {
        backgroundColor: 'transparent',
        colors: ['cornflowerblue', 'tomato'],
        fontName: 'Open Sans',
        chartArea: {
            left: 100,
            top: 10,
            width: '55%',
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
    var chartGPA = new google.visualization.ColumnChart(document.getElementById('chartGPA'));
    chartGPA.draw(dataListGpaReport, optionsGPA);
}
//CGPA Report
function optionCGPA_Report(dataListCGpaReport) {
    var optionsCGPA = {
        backgroundColor: 'transparent',
        colors: ['cornflowerblue', 'tomato'],
        fontName: 'Open Sans',
        chartArea: {
            left: 100,
            top: 10,
            width: '60%',
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
    var chartCGPA = new google.visualization.ColumnChart(document.getElementById('chartCGPA'));
    chartCGPA.draw(dataListCGpaReport, optionsCGPA);
}
//Drop down

//Min
$("#SelectEligiblePrograme").change(function ()
{
    var e = document.getElementById("ContentPlaceHolder1_ddlsession").value,
        a = document.getElementById("ContentPlaceHolder1_ddlacadmicyear").value,
        t = document.getElementById("ContentPlaceHolder1_ddlprogram").value,
        n = document.getElementById("SelectEligiblePrograme").value,
        i = document.getElementById("ContentPlaceHolder1_listacademicname").value,   
        o = new google.visualization.DataTable;
    $(function () {
        $.ajax({
            type: "POST",
            dataType: "json", contentType: "application/json",
            url: "AcademicDashboard.aspx/EligibleListProgram",
            data: "{instcode:'" + i + "',session:'" + e + "', graduation:'" + t + "', academic:'" + a + "', programe:'" + n + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (e) { EligibleListProgram(e.d, o) },
            error: function ()
            { alert("Error loading data Students Attendance! Please try again.") }
        })
    })
}), $("#SelectEligibleCategory").change(function ()
    {
        var e = document.getElementById("ContentPlaceHolder1_ddlsession").value,
        a = document.getElementById("ContentPlaceHolder1_ddlacadmicyear").value,
        t = document.getElementById("ContentPlaceHolder1_ddlprogram").value,
            n = document.getElementById("SelectEligibleCategory").value,
            i = document.getElementById("ContentPlaceHolder1_listacademicname").value,  
        o = new google.visualization.DataTable;
    $(function () {
        $.ajax({
            type: "POST", dataType: "json",
            contentType: "application/json",
            url: "AcademicDashboard.aspx/EligibleListCategory",
            data: "{instcode:'" + i + "',session:'" + e + "', graduation:'" + t + "', academic:'" + a + "', programe:'" + n + "'}",
            contentType: "application/json; charset=utf-8", dataType: "json",
            success: function (e) { EligibleListCategory(e.d, o) },
            error: function () { alert("Error loading data Eligible List Category Wise! Please try again.") }
        })
    })
    }), $("#SelectSSSExamination").change(function ()
    {
        var e = document.getElementById("ContentPlaceHolder1_ddlsession").value,
        a = document.getElementById("ContentPlaceHolder1_ddlacadmicyear").value,
        t = document.getElementById("ContentPlaceHolder1_ddlprogram").value,
            n = document.getElementById("SelectSSSExamination").value,
            In = document.getElementById("ContentPlaceHolder1_listacademicname").value,  
        o = new google.visualization.DataTable; $.ajax({
            type: "POST",
            dataType: "json", contentType: "application/json",
            url: "AcademicDashboard.aspx/EligibleListReevalution",
            data: "{instcode:'" + In + "',session:'" + e + "', graduation:'" + t + "', academic:'" + a + "', programe:'" + n + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json", success: function (e) { EligibleListReevalution(e.d, o) },
            error: function () { alert("Error loading data Eligible List Reevalution List! Please try again.") }
        })
    }), $("#SelectProgram").change(function ()
    {
        var e = document.getElementById("ContentPlaceHolder1_ddlsession").value,
        a = document.getElementById("ContentPlaceHolder1_ddlacadmicyear").value,
        t = document.getElementById("ContentPlaceHolder1_ddlprogram").value,
        n = document.getElementById("SelectProgram").value,
            o = document.getElementById("SelectSemester").value,
            In = document.getElementById("ContentPlaceHolder1_listacademicname").value,
        i = new google.visualization.DataTable;
    $.ajax({
        type: "POST", dataType: "json", contentType: "application/json",
        url: "AcademicDashboard.aspx/GPA_Report",
        data: "{instcode:'" + In + "',session:'" + e + "', graduation:'" + t + "', academic:'" + a + "', semester:'" + o + "', program:'" + n + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json", success: function (e) { GPA_Report(e.d, i) },
        error: function () { alert("Error loading data GPA Report! Please try again.") }
    })
    }), $("#SelectSemester").change(function ()
    {
        var e = document.getElementById("ContentPlaceHolder1_ddlsession").value,
        a = document.getElementById("ContentPlaceHolder1_ddlacadmicyear").value,
        t = document.getElementById("ContentPlaceHolder1_ddlprogram").value,
        n = document.getElementById("SelectProgram").value,
            o = document.getElementById("SelectSemester").value,
            In = document.getElementById("ContentPlaceHolder1_listacademicname").value,
        i = new google.visualization.DataTable; $.ajax({
            type: "POST", dataType: "json",
            contentType: "application/json", url: "AcademicDashboard.aspx/GPA_Report",
            data: "{instcode:'" + In + "',session:'" + e + "', graduation:'" + t + "', academic:'" + a + "', semester:'" + o + "', program:'" + n + "'}",
            contentType: "application/json; charset=utf-8", dataType: "json",
            success: function (e) { GPA_Report(e.d, i) }, error: function ()
            { alert("Error loading data GPA Report! Please try again.") }
        })
    }), $("#SelectCGpa").change(function () {
        var e = document.getElementById("ContentPlaceHolder1_ddlsession").value,
            a = document.getElementById("ContentPlaceHolder1_ddlacadmicyear").value,
            t = document.getElementById("ContentPlaceHolder1_ddlprogram").value,
            n = (document.getElementById("SelectSSSExamination").value,
                In = document.getElementById("ContentPlaceHolder1_listacademicname").value,
                document.getElementById("SelectCGpa").value),
        o = new google.visualization.DataTable; $(function () {
            $.ajax({
                type: "POST", dataType: "json",
                contentType: "application/json",
                url: "AcademicDashboard.aspx/CGPA_Report",
                data: "{instcode:'" + In + "',session:'" + e + "', graduation:'" + t + "', academic:'" + a + "', program:'" + n + "'}",
                contentType: "application/json; charset=utf-8", dataType: "json",
                success: function (e) { CGPA_Report(e.d, o) },
                error: function () { alert("Error loading data GPA Report! Please try again.") }
            })
        })
    }), $("#btnshow").click(function () {
        var e = document.getElementById("ContentPlaceHolder1_ddlsession").value,
        a = document.getElementById("ContentPlaceHolder1_ddlacadmicyear").value,
        t = document.getElementById("ContentPlaceHolder1_ddlprogram").value,
        n = document.getElementById("SelectSemester").value,
        o = document.getElementById("SelectProgram").value,
        i = document.getElementById("SelectCGpa").value,
        l = document.getElementById("SelectEligiblePrograme").value,
        c = document.getElementById("SelectEligibleCategory").value,
            r = document.getElementById("SelectSSSExamination").value,
            In = document.getElementById("ContentPlaceHolder1_listacademicname").value,
        s = new google.visualization.DataTable,
        d = new google.visualization.DataTable,
        u = new google.visualization.DataTable,
        g = new google.visualization.DataTable,
        p = new google.visualization.DataTable,
        m = new google.visualization.DataTable;
    $(function () {
        $.ajax({
            type: "POST", dataType: "json",
            contentType: "application/json",
            url: "AcademicDashboard.aspx/StudentsAttendance",
            data: "{instcode:'" + In + "',session:'" + e + "', graduation:'" + t + "', academic:'" + a + "'}",
            contentType: "application/json; charset=utf-8", dataType: "json",
            success: function (e) { studentAttendance(e.d, s) },
            error: function () { alert("Error loading data Students Attendance! Please try again.") }
        })
    }), $(function () {
        $.ajax({
            type: "POST", dataType: "json",
            contentType: "application/json",
            url: "AcademicDashboard.aspx/EligibleListProgram",         
            data: "{instcode:'" + In + "',session:'" + e + "', graduation:'" + t + "', academic:'" + a + "', programe:'" + l + "'}",
            contentType: "application/json; charset=utf-8", dataType: "json",
            success: function (e) { EligibleListProgram(e.d, d) },
            error: function () { alert("Error loading data Students Attendance! Please try again.") }
        })
        }), $(function () {
            $.ajax({
                type: "POST", dataType: "json",
                contentType: "application/json", url: "AcademicDashboard.aspx/EligibleListCategory",
                data: "{instcode:'" + In + "',session:'" + e + "', graduation:'" + t + "', academic:'" + a + "', programe:'" + c + "'}",
                contentType: "application/json; charset=utf-8", dataType: "json",
                success: function (e) { EligibleListCategory(e.d, u) },
                error: function () { alert("Error loading data Eligible List Category Wise! Please try again.") }
            })
        }), $(function () {
            $.ajax({
                type: "POST",
                dataType: "json", contentType: "application/json",
                url: "AcademicDashboard.aspx/EligibleListReevalution",
                data: "{instcode:'" + In + "',session:'" + e + "', graduation:'" + t + "', academic:'" + a + "', programe:'" + r + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (e) { EligibleListReevalution(e.d, g) },
                error: function () { alert("Error loading data Eligible List Reevalution List! Please try again.") }
            })
        }), $(function ()
        {
            $.ajax({
                type: "POST", dataType: "json",
                contentType: "application/json",
                url: "AcademicDashboard.aspx/GPA_Report",
                data: "{instcode:'" + In + "',session:'" + e + "', graduation:'" + t + "', academic:'" + a + "', semester:'" + n + "', program:'" + o + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (e) { GPA_Report(e.d, p) },
                error: function () { alert("Error loading data GPA Report! Please try again.") }
            })
        }), $(function () {
            $.ajax({
                type: "POST", dataType: "json",
                contentType: "application/json",
                url: "AcademicDashboard.aspx/CGPA_Report",
                data: "{instcode:'" + In + "',session:'" + e + "', graduation:'" + t + "', academic:'" + a + "', program:'" + i + "'}",
                contentType: "application/json; charset=utf-8", dataType: "json",
                success: function (e) { CGPA_Report(e.d, m) }, error: function ()
                { alert("Error loading data GPA Report! Please try again.") }
            })
        })
    });