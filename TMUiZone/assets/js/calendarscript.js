var currentUpdateEvent;
var addStartDate;
var addEndDate;
var globalAllDay;

function updateEvent(event, element) {

    $.ajax({
        type: "post",
        url: "StudentTimeTable.aspx/getStatusApdateCancel",
        data: "{EventId:'" + event.id + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d !== "") {
                debugger;
                var vResult = msg.d;
                //alert(vResult);
                var ChkAttendance = vResult.split(";");
                vResult = ChkAttendance[0];
                var vAttendance = ChkAttendance[1];
                if (vResult == "No Mark") {
                    //alert("Entered No Mark");
                    $('#lblMsgForAttendance').hide();
                    $('#lblCancelled').hide();
                    $('#btnAttendance').show();
                    $('#btnEditAttendance').hide();
                    $('#btnCancel').show();
                    //$('#btnResedule').show(); //Commented by Manju M 26-02-2019
                }
                else if (vResult == "Attendance") {
                    //alert("Entered Attendance");
                    $('#btnAttendance').hide();
                    $('#btnCancel').hide();
                    $('#lblCancelled').hide();
                    $('#lblMsgForAttendance').show();
                    $('#btnCancel').hide();
                    $('#btnEditAttendance').show();
                    //$('#btnResedule').hide(); //Commented by Manju M 26-02-2019
                }
                else if (vResult == "Cancelled") {
                    $('#btnAttendance').hide();
                    $('#btnCancel').hide();
                    $('#lblMsgForAttendance').hide();
                    $('#btnEditAttendance').hide();
                    $('#lblCancelled').show();
                    //$('#btnResedule').hide(); //Commented by Manju M 26-02-2019
                }
                else if (vResult == "Interval") {
                    $('#updatedialog').dialog('close');
                    //$('#lblMsgForAttendance').hide();
                    //$('#lblCancelled').hide();
                    //$('#btnAttendance').hide();
                    //$('#btnEditAttendance').hide();
                    //$('#btnCancel').hide();
                    //$('#btnResedule').hide();

                    //document.getElementById("updatedialog").id = "nupdatedialog";
                    //return false;
                }

                //.toLocaleString()
                var date = new Date();
                date.setHours(date.getHours() + 5);
                date.setMinutes(date.getMinutes() + 30);
                var currentDate = date.getTime();
                var sdate = new Date(event.start); //Manju Unikul 22-09-2018
                var ndate = new Date(event.end);
                var EventStartDate = sdate.getTime(); //Manju Unikul 22-09-2018
                var EventEndDate = ndate.getTime();
                if (EventEndDate < currentDate) {
                    //alert("calandar is less than currentdate");
                    //$('#btnResedule').hide(); //Commented by Manju M 26-02-2019
                }
                //Manju Unikul 22-09-2018 Added to allow attendance while class is going on
                else if (EventStartDate <= currentDate && EventEndDate >= currentDate) {
                    //alert("Entered between condition");
                    //$('#btnResedule').hide(); //Commented by Manju M 26-02-2019
                    $('#btnAttendance').show();
                }
                else {
                    //alert("entered else condition");
                    $('#btnAttendance').hide();
                }

                if (vAttendance == "No Attendance") {
                    $('#btnAttendance').hide();
                }
            }
            else {
                customAlert("e", "Error.");
            }
        }
    });

    //old 

    if ($(this).data("qtip")) $(this).qtip("destroy");

    currentUpdateEvent = event;

    $('#updatedialog').dialog('open');
    $("#eventName").val(event.title);
    $("#eventDesc").val(event.description);
    $("#eventId").val(event.id);
    $("#eventStart").text("" + event.start.toLocaleString());

    if (event.end === null) {
        $("#eventEnd").text("");
    }
    else {
        $("#eventEnd").text("" + event.end.toLocaleString());
    }
    return false;
}

function updateSuccess(updateResult) {
    //alert(updateResult);
}

function deleteSuccess(deleteResult) {
    //alert(deleteResult);
}

function addSuccess(addResult) {
    // if addresult is -1, means event was not added
    //    alert("added key: " + addResult);

    if (addResult != -1) {
        $('#calendar').fullCalendar('renderEvent',
						{
						    title: $("#addEventName").val(),
						    start: addStartDate,
						    end: addEndDate,
						    id: addResult,
						    description: $("#addEventDesc").val(),
						    allDay: globalAllDay
						},
						true // make the event "stick"
					);
        $('#calendar').fullCalendar('unselect');
    }
}

function UpdateTimeSuccess(updateResult) {
    //alert(updateResult);
}

function selectDate(start, end, allDay) {

    var monthName = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
                 "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    var DayHour = new Array(21);
    DayHour[13] = "01";
    DayHour[14] = "02";
    DayHour[15] = "03";
    DayHour[16] = "04";
    DayHour[17] = "05";
    DayHour[18] = "06";
    DayHour[19] = "07";
    DayHour[20] = "08";


    var d1 = new Date(start),
     d = d1.getDate(),
     m = d1.getMonth(),
     y = d1.getFullYear();
    var dateString = d + " " + monthName[m] + " " + y;

    //----------------------
    var milliseconds = parseInt((start % 1000) / 100)
            , seconds = parseInt((start / 1000) % 60)
            , minutes = parseInt((start / (1000 * 60)) % 60)
            , hours = parseInt((start / (1000 * 60 * 60)) % 24);

    shours = (hours < 10) ? "0" + hours : hours;
    sminutes = (minutes < 10) ? "0" + minutes : minutes;
    sseconds = (seconds < 10) ? "0" + seconds : seconds;

    if (shours > 12) {
        shours = DayHour[shours];
    }

    var FromTime = shours + ":" + sminutes;

    //-------------------------

    var millisecondsTo = parseInt((end % 1000) / 100)
            , seconds = parseInt((end / 1000) % 60)
            , minutes = parseInt((end / (1000 * 60)) % 60)
            , hours = parseInt((end / (1000 * 60 * 60)) % 24);

    ehours = (hours < 10) ? "0" + hours : hours;
    eminutes = (minutes < 10) ? "0" + minutes : minutes;
    eseconds = (seconds < 10) ? "0" + seconds : seconds;

    if (ehours > 12) {
        ehours = DayHour[ehours];
    }

    var ToTime = ehours + ":" + eminutes;

    //alert(dateString+'  '+FromTime + '-' + ToTime);
    //alert(m);
    //alert(y);
    $("#startDate").text(dateString);
    $("#startTime").text(FromTime);
    $("#endTime").text(ToTime);

    $("#addEventStartDate").text(dateString);
    $("#addEventEndDate").text(FromTime + '-' + ToTime);

    $('#addDialog').dialog('open');

    //$("#addEventStartDate").text("" + start.toLocaleString());
    //$("#addEventEndDate").text("" + end.toLocaleString());

    addStartDate = start;
    addEndDate = end;
    globalAllDay = allDay;


}

function updateEventOnDropResize(event, allDay) {

    //alert("allday: " + allDay);
    var eventToUpdate = {
        id: event.id,
        start: event.start
    };

    if (event.end === null) {
        eventToUpdate.end = eventToUpdate.start;
    }
    else {
        eventToUpdate.end = event.end;
    }

    var endDate;
    if (!event.allDay) {
        endDate = new Date(eventToUpdate.end + 60 * 60000);
        endDate = endDate.toJSON();
    }
    else {
        endDate = eventToUpdate.end.toJSON();
    }

    eventToUpdate.start = eventToUpdate.start.toJSON();
    eventToUpdate.end = eventToUpdate.end.toJSON(); //endDate;
    eventToUpdate.allDay = event.allDay;

    PageMethods.UpdateEventTime(eventToUpdate, UpdateTimeSuccess);
}

function eventDropped(event, dayDelta, minuteDelta, allDay, revertFunc) {
    if ($(this).data("qtip")) $(this).qtip("destroy");

    updateEventOnDropResize(event);
}

function eventResized(event, dayDelta, minuteDelta, revertFunc) {
    if ($(this).data("qtip")) $(this).qtip("destroy");

    updateEventOnDropResize(event);
}

function checkForSpecialChars(stringToCheck) {
    var pattern = /[^A-Za-z0-9 ]/;
    return pattern.test(stringToCheck);
}

function isAllDay(startDate, endDate) {
    var allDay;

    if (startDate.format("HH:mm:ss") == "00:00:00" && endDate.format("HH:mm:ss") == "00:00:00") {
        allDay = true;
        globalAllDay = true;
    }
    else {
        allDay = false;
        globalAllDay = false;
    }

    return allDay;
}

function qTipText(start, end, description) {
    var text;

    if (end != null && description.value != "")
        //text = "<strong>Start:</strong> " + start.format("MM/DD/YYYY hh:mm T") + "<br/><strong>End:</strong> " + end.format("MM/DD/YYYY hh:mm T") + "<br/><br/>" + description;
        text = "<strong>Start:</strong> " + start.format("DD MMMM, YYYY hh:mm A") + "<br/><strong>End:</strong> " + end.format("DD MMMM, YYYY hh:mm A") + "<br/><br/>" + description;  
    else 
        text = "<strong>Start:</strong> " + start.format("DD MMMM, YYYY hh:mm A") + "<br/><strong>End:</strong><br/><br/>";

    return text;
}

$(document).ready(function () {
    //update Dialog
    $('#updatedialog').dialog({
        autoOpen: false,
        show: 'fade',
        hide: 'fade',
        width: 300,
        title: 'Student Attendance',
        open: function (event, ui) {
            $(".ui-widget-overlay").css({
                opacity: 0.42,
                filter: "Alpha(Opacity=40)",
                backgroundColor: "black"
            });
        },
        modal: true,
        //buttons: {
        //    "update": function () {

        //        var eventToUpdate = {
        //            id: currentUpdateEvent.id,
        //            title: $("#eventName").val(),
        //            description: $("#eventDesc").val()
        //        };

        //        if (checkForSpecialChars(eventToUpdate.title) || checkForSpecialChars(eventToUpdate.description)) {
        //            alert("please enter characters: A to Z, a to z, 0 to 9, spaces");
        //        }
        //        else {
        //            PageMethods.UpdateEvent(eventToUpdate, updateSuccess);
        //            $(this).dialog("close");

        //            currentUpdateEvent.title = $("#eventName").val();
        //            currentUpdateEvent.description = $("#eventDesc").val();
        //            $('#calendar').fullCalendar('updateEvent', currentUpdateEvent);
        //        }

        //    },
        //    "delete": function() {

        //        if (confirm("do you really want to delete this event?")) {
        //            PageMethods.deleteEvent($("#eventId").val(), deleteSuccess);
        //            $(this).dialog("close");
        //            $('#calendar').fullCalendar('removeEvents', $("#eventId").val());
        //        }
        //    }
        //}
    });


    //add dialog
    $('#addDialog').dialog({
        autoOpen: false,
        width: 300,
        //buttons: {
        //    "Add": function () {

        //        //alert("sent:" + addStartDate.format("dd-MM-yyyy hh:mm:ss tt") + "==" + addStartDate.toLocaleString());
        //        var eventToAdd = {
        //            title: $("#addEventName").val(),
        //            description: $("#addEventDesc").val(),
        //            start: addStartDate.toJSON(),
        //            end: addEndDate.toJSON(),
        //            allDay: isAllDay(addStartDate, addEndDate)
        //        };
        //        if (checkForSpecialChars(eventToAdd.title) || checkForSpecialChars(eventToAdd.description)) {
        //            alert("please enter characters: A to Z, a to z, 0 to 9, spaces");
        //        }
        //        else {
        //            //alert("sending " + eventToAdd.title);
        //            PageMethods.addEvent(eventToAdd, addSuccess);
        //            $(this).dialog("close");
        //        }
        //    }
        //}
    });

    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();
    var options = {
        weekday: "long",
        year: "numeric",
        month: "short",
        day: "numeric",
        hour: "2-digit",
        minute: "2-digit"
    };

    var vs = true;
    var vUserGroup = document.getElementById("ContentPlaceHolder1_hdnUserGroup").value;
    if (vUserGroup == "STUDENT" || vUserGroup == "PARENT") {
        vs = false;
    }



    $('#calendar').fullCalendar({
        theme: false,
        header: {
            //left: 'prev,next today, select-my',
            left: 'prev,next today',
            center: 'title',
            //right: 'month,agendaWeek,agendaDay'
            right: 'agendaWeek,agendaDay'
        },
        defaultView: 'agendaWeek',
        eventClick: updateEvent,
        selectable: vs,
        selectHelper: true,
        selectConstraint: {
            start: $.fullCalendar.moment().subtract(1, 'days'),
            end: $.fullCalendar.moment().startOf('month').add(12, 'month') 
        },
        select: selectDate,
        editable: false,
        firstDay: 1,
        minTime: "08:00:00",
        maxTime: "20:00:00",
        validRange: {
            start: '2010-01-01',
            end: '2030-12-31'
        },
        viewRender: renderViewColumns,
        //events: "JsonResponse.ashx?nstart=2017-05-01 11:30:00&nend=2017-05-10 17:30:00",        
        events: "JsonResponse.ashx",
        eventRender: function (event, element) {
            element.qtip({
                content: {
                    text: qTipText(event.start, event.end, event.description),
                    title: '<strong>' + event.title + '</strong>'
                },
                position: {
                    my: 'bottom left',
                    at: 'top left'
                },
                style: { classes: 'qtip-shadow qtip-rounded' }
            });
        },
        eventMouseover: function (event, jsEvent, view) {
            var title = event.title;
            var content = event.description;
        },
        eventMouseout: function (event, jsEvent, view) {
            $('#fc_tooltip').remove();
        }
    });

    function renderViewColumns(view, element) {
        element.find('th.fc-day-header.fc-widget-header').each(function () {
            var theDate = moment($(this).data('date')); /* th.data-date="YYYY-MM-DD" */
            $(this).html(buildDateColumnHeader(theDate));
        });

        function buildDateColumnHeader(theDate) {
            var container = document.createElement('div');
            var DDD = document.createElement('div');
            var ddMMM = document.createElement('div');
            DDD.textContent = theDate.format('ddd').toUpperCase();
            ddMMM.textContent = theDate.format('DD MMM');
            container.appendChild(ddMMM);
            container.appendChild(DDD);

            return container;
        }
    }

    var gotoYear = y;
    var gotoMonth = m;

    var currentMonth = date.getMonth();
    var currentYear = date.getFullYear();

    $("#filterCalendarYear").change(function () {
        debugger;
        var selectedVal = this.value;
        if (selectedVal == '') {
            // alert('Please Select a Year');
            return;
        }
        else {
            gotoYear = selectedVal;
            //var setGotoYear = m + " " + d + " " + gotoYear;
            var setGotoYear = gotoMonth + " " + d + " " + gotoYear;
            $('#calendar').fullCalendar('gotoDate', setGotoYear);
        }
    });
    $("#filterCalendarMonth").change(function () {

        var selectedVal = this.value;
        if (selectedVal == '') {
            // alert('Please Select a Year');
            return;
        }
        else {
            gotoMonth = selectedVal;
            var setGotoMonth = gotoMonth + " " + d + " " + gotoYear;
            $('#calendar').fullCalendar('gotoDate', setGotoMonth);
        }
    });

    $(".fc-today-button").click(function () {
        $('#filterCalendarMonth').val(currentMonth + 1);
        $('#filterCalendarYear').val(currentYear);
    });


});
