var counter = 1;
var counterEdit = 1;

$().ready(function () {
    var currentTime = new Date();
    var month = currentTime.getMonth()+1;
    var day = currentTime.getDate();
    var year = currentTime.getFullYear();

    if (day < 10) {
        day = '0' + day;
    }
    if (month < 10) {
        month = '0' + month;
    }

    $("#txtMeetingDate").datepicker();
    $("#txtMeetingDate").val(month + "/" + day + "/" + year);
    $("div.ui-datepicker").css({ "font-size": "12px" });

    //syncPost("GetSymbolList", "");
    //$("#ddlSymbols").html(syncPostResult);

    //$("#ddlSymbols").select2({

    //});

    //syncPost("GetICMeetings", "");
    //$("#ddlICMeetingSymbols").html(syncPostResult);

    //$("#ddlICMeetingSymbols").select2({

    //});
    syncPost("GetICMeetings", "");
    $("#ddlICMeetings").html(syncPostResult);

    $("#ddlICMeetings").select2({

    });

    $("#ddlICMeetingsEmail").html(syncPostResult);

    $("#ddlICMeetingsEmail").select2({

    });

    syncPost("GetSymbolList", "");
    $("#ddlSymbols1").html(syncPostResult);

    $("#ddlSymbols1").select2({

    });

    //syncPost("GetICMeetingEmployeeList", "");
    //$("#ddlCandidatesMeeting1").html(syncPostResult);
    //$('#ddlCandidatesMeeting1 option').prop('selected', true);

    syncPost("GetICMeetingEmployeeListHtml", "");
    $("#divAttendees1").append(syncPostResult);
    // $("#tblAttendees").html(syncPostResult);

    $('#txtMeetingStartTime').timepicker({ 'minTime': '3:00pm' });
    $('#txtMeetingEndTime').timepicker({ 'minTime': '4:00pm' });
    
    $('#txtOrigNumOfPages').numeric({min:0,allowMinus:false});
    $('#txtFinNumOfPages').numeric({ min: 0, allowMinus: false });
});

function CreateMeeting() {
   
    //$("#divPopUp").height(450);
    $("#divPopUp").load('AddICMeeting.aspx', function () {
        var currentTime = new Date();
        var month = currentTime.getMonth() + 1;
        var day = currentTime.getDate();
        var year = currentTime.getFullYear();

        if (day < 10) {
            day = '0' + day;
        }
        if (month < 10) {
            month = '0' + month;
        }

        $("#txtMeetingDate").datepicker();
        $("#txtMeetingDate").val(month + "/" + day + "/" + year);
        $("div.ui-datepicker").css({ "font-size": "12px" });

        syncPost("GetSymbolList", "");
        $("#ddlSymbols1").html(syncPostResult);

        $("#ddlSymbols1").select2({

        });

        syncPost("GetICMeetingEmployeeList", "");
        $("#ddlCandidatesMeeting1").html(syncPostResult);
        $('#ddlCandidatesMeeting1 option').prop('selected', true);
        //var selectedPeople = ["bjohnson","JKakkar"];

        //$("#ddlCandidatesMeeting1").val(selectedPeople);
      
        //$("#divPopUp").width(525);
        //$("#divPopUp").height(550);
        $("#divPopUp").width(525);
        $("#divPopUp").height(650);
        $("#divPopUp").show().center();

        $("#hiddenCreateUpdate").val("Create");

        showOverlay();
    });


}

function hideForm() {
    $("#divPopUp").fadeOut("fast");
    $("#divPopUp").html("");
   // counter = 1;
    hideOverlay();
}

function AssignMeetingAttributes()
{
    $("#divPopUp").load('SymbolAttributes.aspx', function () {
      

        syncPost("GetSymbolList", "");
        $("#ddlSymbols").html(syncPostResult);

        $("#ddlSymbols").select2({

        });

        $("#divPopUp").width(510);
        $("#divPopUp").height(300);
        $("#divPopUp").show().center();

        showOverlay();
    });

}

function SaveMeetingAttributes()
{
    syncPost("InsertMeetingAttributes", JSON.stringify({
        symbol: $("#ddlSymbols").val(),
        meetingId: $('#ddlICMeetingSymbols').val(), attendees: $("#ddlCandidatesMeeting").val().toString(),
        requiredPositionRecommendation: $('input[name=poistionReco]:checked').val()
    }));

    $("#divMeetingAttributes").css("display", "inline-block");
    $("#divPopUp").fadeOut("fast");
    hideOverlay();

}




function SaveMeeting()
{
    //syncPost("InsertICMeeting", JSON.stringify({
    //    meetingDate: $("#txtMeetingDate").val(),
    //    meetingStart3PM: $('input[name=meetStart3Pm]:checked').val(), meetingStartTime: $("#txtMeetingStartTime").val(),
    //    meetingEndTime: $("#txtMeetingEndTime").val(), origNotes1PageLength: $('input[name=origNotes1Page]:checked').val(),
    //    origLengthOfNotes: $("#txtOrigNumOfPages").val(),finalLengthOfNotes:$("#txtFinNumOfPages").val(),
    //    linkToMeetingDoc:$("#txtLinkMeetingDoc").val()
    //}));

    //syncPost("GetICMeetings", "");
    //$("#ddlICMeetingSymbols").html(syncPostResult);

    //$("#ddlICMeetingSymbols").select2({

    //});

    //$("#ddlICMeetings").html(syncPostResult);

    //$("#ddlICMeetings").select2({

    //});

    //$("#ddlICMeetingsEmail").html(syncPostResult);

    //$("#ddlICMeetingsEmail").select2({

    //});
    //hideForm();
   ;

    var meetingAttributes = [];
   
    for (var i = 1; i <= counter; i++) {
        var meetingAttribute = {};
        meetingAttribute.Symbol = $("#ddlSymbols"+i).val();
        meetingAttribute.RequiredRecommendation = $("input[name=poistionReco" + i + "]:checked").val();
        var selectedCheckboxes = [];
        $("#divAttendees"+i+" input:checked").each(function () {
            selectedCheckboxes.push($(this).attr('id'));
        });
        // meetingAttribute.Attendee = $("#ddlCandidatesMeeting" + i).val().toString();
        meetingAttribute.Attendees = selectedCheckboxes;

        var unSelectedCheckboxes = [];
        $("#divAttendees" + i + " input:not(:checked)").each(function () {
            unSelectedCheckboxes.push($(this).attr('id'));
        });
        // meetingAttribute.Attendee = $("#ddlCandidatesMeeting" + i).val().toString();
        meetingAttribute.NonAttendees = unSelectedCheckboxes;


        meetingAttribute.Securities = $("#ddlSecurities" + i).val();
        if (meetingAttribute.Symbol !== undefined && meetingAttribute.Symbol != '-1') {
            meetingAttributes.push(meetingAttribute);
        }
    }

    // if ($("#hiddenCreateUpdate").val() == "Create") {
    var meetingLinkDoc = ($("#txtLinkMeetingDoc").val().length == 0) ? $("#lblLinkMeetingDoc").text() : $("#txtLinkMeetingDoc").val()
        syncPost("InsertICMeeting", JSON.stringify({
            meetingDate: $("#txtMeetingDate").val(),
            meetingStart3PM: ($('input[name=meetStart3Pm]:checked').val() === undefined) ? null : $('input[name=meetStart3Pm]:checked').val(),
            meetingStartTime: $("#txtMeetingStartTime").val(),
            meetingEndTime: $("#txtMeetingEndTime").val(),
            origNotes1PageLength: ($('input[name=origNotes1Page]:checked').val() === undefined) ? null : $('input[name=origNotes1Page]:checked').val(),
            origLengthOfNotes: $("#txtOrigNumOfPages").val(),
            finalLengthOfNotes: $("#txtFinNumOfPages").val(),
            linkToMeetingDoc: meetingLinkDoc,
            isLocked: ($('input[name=lockMeeting]:checked').val() === undefined) ? null : $('input[name=lockMeeting]:checked').val(),
            meetingAttributesList: meetingAttributes
        }));
        if (syncPostSuccess)
        {
            alert("Meeting Successfully Saved");
        }
        else
        {
            alert("There was a problem saving your meeting "+syncPostResult);
        }

   // }
    //else if ($("#hiddenCreateUpdate").val() == "Update")
    //{
    //    syncPost("UpdateICMeeting", JSON.stringify({
    //        meetingId: $("#ddlICMeetings").val(), meetingDate: $("#txtMeetingDate").val(),
    //        meetingStart3PM: $('input[name=meetStart3Pm]:checked').val(), meetingStartTime: $("#txtMeetingStartTime").val(),
    //        meetingEndTime: $("#txtMeetingEndTime").val(), origNotes1PageLength: $('input[name=origNotes1Page]:checked').val(),
    //        origLengthOfNotes: $("#txtOrigNumOfPages").val(), finalLengthOfNotes: $("#txtFinNumOfPages").val(),
    //        linkToMeetingDoc: $("#txtLinkMeetingDoc").val(),
    //        meetingAttributesList: meetingAttributes
    //    }));
    //}

       // counter = 1;//resetting the counter to 0//commented 10/27/2016
        syncPost("GetICMeetings", "");
        $("#ddlICMeetings").html(syncPostResult);

        $("#ddlICMeetings").select2({

        });


    

   // hideForm();


}

function AddSymbol() {

    $("#divAddNewSymbol").css("display", "inline-block");

    $("#divAddNewSymbol").html("<input type='text' id='txtNewSymbol' />  <label style='cursor:pointer;width:10px;' onclick='InsertNewSymbol()'><u style='color:deepskyblue;'>Add</u></label>");
    
}

function AddMeetingAtributes()
{
    counter = counter + 1;
    var meetingAttributes = "";
    meetingAttributes += "<div style='border-radius: 10px; border: 1px solid grey; border-image: none;padding-top:5px;'>";
    meetingAttributes += "<div style='padding-top:5px;'>";
    meetingAttributes += "<label>Choose A Company</label>";
    meetingAttributes += "<select id=" + "ddlSymbols" + counter + " style='width:215px;'" + "onchange=PopulateStocks(" + counter + ") ></select>";
    meetingAttributes += "</div>";

 

    meetingAttributes += "<div style='padding-top:5px;'>";
    meetingAttributes += "<label style='vertical-align:top;'>Choose A Security</label>";
    meetingAttributes += "<select id=" + "ddlSecurities" + counter + " style='width:215px;height:100px;' multiple></select>";
    meetingAttributes += "</div>";
   
    meetingAttributes += "<div id=divAttendees" + counter + " style='padding-top:5px;'>";
    meetingAttributes += "<label style='vertical-align:top;'>Attendees</label>";
    meetingAttributes += "</div>";

   // meetingAttributes += "</div>";

    meetingAttributes += "<div style='padding-top:5px;padding-left:20px;'>";
    meetingAttributes += "<label>Position Recommendation</label>";
    meetingAttributes += "<input name=" + "poistionReco" + counter + " type='radio' value='true' />";
    meetingAttributes += "<span style='width:25px;padding-left:0px'>Yes</span>";
    meetingAttributes += "<input name=" + "poistionReco" + counter + " type='radio' value='false' />";
    meetingAttributes += "<span style='width:25px;padding-left:0px'>No</span>";
    meetingAttributes += "</div>";
    meetingAttributes += "</div>";
    $("#divMeetingAttributes").append(meetingAttributes);


    //$("#divMeetingAttributes").append("<div style='border-radius: 10px; border: 1px solid grey; border-image: none;'>");
    //$("#divMeetingAttributes").append("<div style='padding-top:5px;'>");
    //$("#divMeetingAttributes").append("<label>Choose A Symbol</label>");
    //$("#divMeetingAttributes").append("<select id=" + "ddlSymbols" + counter + " style='width:215px;'></select>");
    //$("#divMeetingAttributes").append("</div>");

    syncPost("GetSymbolList", "");
    $("#ddlSymbols" + counter).html(syncPostResult);

    $("#ddlSymbols" + counter).select2({

    });

    //$("#divMeetingAttributes").append("<div style='padding-top:5px;'>");
    //$("#divMeetingAttributes").append("<label style='vertical-align:top;'>Attendees</label>");
    //$("#divMeetingAttributes").append("<select id=" + "ddlCandidatesMeeting" + counter + " style='width:215px;height:150px;' multiple></select>");
    //$("#divMeetingAttributes").append("</div>");

    //syncPost("GetICMeetingEmployeeList", "");
    //$("#ddlCandidatesMeeting"+counter).html(syncPostResult);
    //$("#ddlCandidatesMeeting" + counter + " option").prop('selected', true);


    syncPost("GetICMeetingEmployeeListHtml", "");
    $("#divAttendees"+counter).append(syncPostResult);

    //$("#divMeetingAttributes").append("<div style='padding-top:5px;'>");
    //$("#divMeetingAttributes").append("<label>Position Recommendation</label>");
    //$("#divMeetingAttributes").append("<input name=" + "poistionReco" + counter + " type='radio' value='true' />");
    //$("#divMeetingAttributes").append("<label style='width:25px;padding-left:0px'>Yes</label>");
    //$("#divMeetingAttributes").append("<input name=" + "poistionReco" + counter + " type='radio' value='false' />");
    //$("#divMeetingAttributes").append("<label style='width:25px;padding-left:0px'>No</label>");
    //$("#divMeetingAttributes").append("</div>");
    //$("#divMeetingAttributes").append('</div>');

}

//function EditMeeting()
//{
//    //$("#divPopUp").height(450);
//    $("#divPopUp").load('AddICMeeting.aspx', function () {
//        syncPost("GetMeetingInfo", JSON.stringify({ meetingId: $('#ddlICMeetings').val() }));


//        $("#txtMeetingDate").val(syncPostResult.MeetingDate);

//        if (syncPostResult.MeetingStartAt3PM)
//        {
//            $('input:radio[name=meetStart3Pm]')[0].checked = true;
//        }
//        else
//        {
//            $('input:radio[name=meetStart3Pm]')[1].checked = true;
//        }
//        $("#txtMeetingStartTime").val(syncPostResult.MeetingStartTime);
//        $("#txtMeetingEndTime").val(syncPostResult.MeetingEndTime);

//        if (syncPostResult.OrigNotes1PageInLength) {
//            $('input:radio[name=origNotes1Page]')[0].checked = true;
//        }
//        else {
//            $('input:radio[name=origNotes1Page]')[1].checked = true;
//        }
//        $("#txtOrigNumOfPages").val(syncPostResult.OrigLengthOfNotes);
//        $("#txtFinNumOfPages").val(syncPostResult.FinalLengthOfNotes);
//        $("#txtLinkMeetingDoc").val(syncPostResult.MeetingDocLocation);


//        var meetingAttributes = syncPostResult.MeetingAttributes;
//        syncPost("GetSymbolList", "");
//        var symbolList = syncPostResult;
//        syncPost("GetICMeetingEmployeeList", "");
//        var employeeList = syncPostResult;

//        if (meetingAttributes.length == 0) {
//            $("#ddlSymbols" + counter).html(symbolList);
//            $("#ddlSymbols" + counter).select2({

//            });
//            $("#ddlCandidatesMeeting" + counter).html(employeeList);
//        }

//        for (var i = 0; i < meetingAttributes.length; i++) {//here we are populating the controls which are rendered on the initial load
           

//            if (i == 0) {
//                $("#ddlSymbols" + counter).html(symbolList);
//                $("#ddlSymbols" + counter).val(meetingAttributes[i].Symbol);
//                $("#ddlSymbols" + counter).select2({

//                });

//                $("#ddlCandidatesMeeting" + counter).html(employeeList);
//                $("#ddlCandidatesMeeting" + counter).val(meetingAttributes[i].Attendee.split(","));

//                if (meetingAttributes[i].RequiredRecommendation) {
//                    $("input:radio[name=poistionReco" + counter + "]")[0].checked = true;
//                }
//                else {
//                    $("input:radio[name=poistionReco" + counter + "]")[1].checked = true;
//                }
//            }
//            else {
//                counter = counter + 1;
//                var meetingAttributesHtml = "";
//                meetingAttributesHtml += "<div style='border-radius: 10px; border: 1px solid grey; border-image: none;padding-top:5px;'>";
//                meetingAttributesHtml += "<div style='padding-top:5px;'>";
//                meetingAttributesHtml += "<label>Choose A Symbol</label>";
//                meetingAttributesHtml += "<select id=" + "ddlSymbols" + counter + " style='width:215px;'></select>";
//                meetingAttributesHtml += "</div>";

//                meetingAttributesHtml += "<div style='padding-top:5px;'>";
//                meetingAttributesHtml += "<label style='vertical-align:top;'>Attendees</label>";
//                meetingAttributesHtml += "<select id=" + "ddlCandidatesMeeting" + counter + " style='width:215px;height:150px;' multiple></select>";
//                meetingAttributesHtml += "</div>";

//                meetingAttributesHtml += "<div style='padding-top:5px;'>";
//                meetingAttributesHtml += "<label>Position Recommendation</label>";
//                meetingAttributesHtml += "<input name=" + "poistionReco" + counter + " type='radio' value='true' />";
//                meetingAttributesHtml += "<label style='width:25px;padding-left:0px'>Yes</label>";
//                meetingAttributesHtml += "<input name=" + "poistionReco" + counter + " type='radio' value='false' />";
//                meetingAttributesHtml += "<label style='width:25px;padding-left:0px'>No</label>";
//                meetingAttributesHtml += "</div>";
//                meetingAttributesHtml += "</div>";
//                $("#divMeetingAttributes").append(meetingAttributesHtml);

//                //$("#divMeetingAttributes").append("<div style='padding-top:5px;'>");
//                //$("#divMeetingAttributes").append("<label>Choose A Symbol</label>");
//                //$("#divMeetingAttributes").append("<select id=" + "ddlSymbols" + counter + " style='width:215px;'></select>");
//                //$("#divMeetingAttributes").append("</div>");

             


//                $("#ddlSymbols" + counter).html(symbolList);
//                $("#ddlSymbols" + counter).val(meetingAttributes[i].Symbol);
//                $("#ddlSymbols" + counter).select2({

//                });



//                //$("#divMeetingAttributes").append("<div style='padding-top:5px;'>");
//                //$("#divMeetingAttributes").append("<label style='vertical-align:top;'>Attendees</label>");
//                //$("#divMeetingAttributes").append("<select id=" + "ddlCandidatesMeeting" + counter + " style='width:215px;height:150px;' multiple></select>");
//                //$("#divMeetingAttributes").append("</div>");

               
//                $("#ddlCandidatesMeeting" + counter).html(employeeList);
//                $("#ddlCandidatesMeeting" + counter).val(meetingAttributes[i].Attendee.split(","));

//                //$("#divMeetingAttributes").append("<div style='padding-top:5px;'>");
//                //$("#divMeetingAttributes").append("<label>Position Recommendation</label>");
//                //$("#divMeetingAttributes").append("<input name=" + "poistionReco" + counter + " type='radio' value='true' />");
//                //$("#divMeetingAttributes").append("<label style='width:25px;padding-left:0px'>Yes</label>");
//                //$("#divMeetingAttributes").append("<input name=" + "poistionReco" + counter + " type='radio' value='false' />");
//                //$("#divMeetingAttributes").append("<label style='width:25px;padding-left:0px'>No</label>");
//                //$("#divMeetingAttributes").append("</div>");

//                if (meetingAttributes[i].RequiredRecommendation) {
//                    $("input:radio[name=poistionReco" + counter + "]")[0].checked = true;
//                }
//                else {
//                    $("input:radio[name=poistionReco" + counter + "]")[1].checked = true;
//                }
                
//            }
           
           
//        }

//        //syncPost("GetSymbolList", "");
//        //$("#ddlSymbols1").html(syncPostResult);

//        //$("#ddlSymbols1").select2({

//        //});

//        //syncPost("GetICMeetingEmployeeList", "");
//        //$("#ddlCandidatesMeeting1").html(syncPostResult);

//        //$("#ddlCandidatesMeeting1").val("bJohnson,JKakkar");

//        $("#divPopUp").width(525);
//        //$("#divPopUp").height(550);
//        $("#divPopUp").height(650);
//        $("#divPopUp").show().center();
//        $("#hiddenCreateUpdate").val("Update");
//        showOverlay();
//    });


//}

function EditMeeting()
{
       counter = 1;
    
        $("#divMeetingAttributes").html("");
        syncPost("GetMeetingInfo", JSON.stringify({ meetingId: $('#ddlICMeetings').val() }));


        $("#txtMeetingDate").val(syncPostResult.MeetingDate);

        if (syncPostResult.MeetingStartAt3PM) {
            $('input:radio[name=meetStart3Pm]')[0].checked = true;
        }
        else {
            $('input:radio[name=meetStart3Pm]')[1].checked = true;
        }
        $("#txtMeetingStartTime").val(syncPostResult.MeetingStartTime);
        $("#txtMeetingEndTime").val(syncPostResult.MeetingEndTime);

        if (syncPostResult.OrigNotes1PageInLength) {
            $('input:radio[name=origNotes1Page]')[0].checked = true;
        }
        else {
            $('input:radio[name=origNotes1Page]')[1].checked = true;
        }
        $("#txtOrigNumOfPages").val(syncPostResult.OrigLengthOfNotes);
        $("#txtFinNumOfPages").val(syncPostResult.FinalLengthOfNotes);
        $("#txtLinkMeetingDoc").val(syncPostResult.MeetingDocLocation);
        $("#lblLinkMeetingDoc").text(syncPostResult.MeetingDocLocation);
        if (syncPostResult.IsLocked) {
            $('input:radio[name=lockMeeting]')[0].checked = true;
        }
        else {
            $('input:radio[name=lockMeeting]')[1].checked = true;
        }

        var meetingAttributes = syncPostResult.MeetingAttributes;
        syncPost("GetSymbolList", "");
        var symbolList = syncPostResult;
    //syncPost("GetICMeetingEmployeeList", "");
        syncPost("GetICMeetingEmployeeListHtml", "");
        var employeeList = syncPostResult;

        if (meetingAttributes.length == 0) {
            $("#ddlSymbols" + counter).html(symbolList);
            $("#ddlSymbols" + counter).select2({

            });
            $("#ddlCandidatesMeeting" + counter).html(employeeList);
        }

        for (var i = 0; i < meetingAttributes.length; i++) {//here we are populating the controls which are rendered on the initial load


            //if (i == 0) {
            //    $("#ddlSymbols" + counter).html(symbolList);
            //    $("#ddlSymbols" + counter).val(meetingAttributes[i].Symbol);
            //    $("#ddlSymbols" + counter).select2({

            //    });

            //    PopulateStocks(1);
            //    $("#ddlSecurities" + counter).val(meetingAttributes[i].Securities);

            //   // $("#ddlCandidatesMeeting" + counter).html(employeeList);
            //    //$("#ddlCandidatesMeeting" + counter).val(meetingAttributes[i].Attendee.split(","));
            //    $('input:checkbox').removeAttr('checked');//clear all checkboxes which are checked

            //    var attendees = meetingAttributes[i].Attendee.split(",");
            //    for (var k = 0; k < attendees.length; k++)
            //    {
            //        $("div#divAttendees1 :checkbox#" + attendees[k]).attr('checked', 'checked');

            //    }

            //    if (meetingAttributes[i].RequiredRecommendation) {
            //        $("input:radio[name=poistionReco" + counter + "]")[0].checked = true;
            //    }
            //    else {
            //        $("input:radio[name=poistionReco" + counter + "]")[1].checked = true;
            //    }
            //}
            //else
            {
                //counter = counter + 1;
                var meetingAttributesHtml = "";
                meetingAttributesHtml += "<div style='border-radius: 10px; border: 1px solid grey; border-image: none;padding-top:5px;'>";
                meetingAttributesHtml += "<div style='padding-top:5px;'>";
                meetingAttributesHtml += "<label>Choose A Symbol</label>";
                meetingAttributesHtml += "<select id=" + "ddlSymbols" + counter + " style='width:215px;'></select>";
                meetingAttributesHtml += "</div>";

            
              meetingAttributesHtml += "<div style='padding-top:5px;'>";
              meetingAttributesHtml += "<label style='vertical-align:top;'>Choose A Security</label>";
              meetingAttributesHtml += "<select id=" + "ddlSecurities" + counter + " style='width:215px;height:100px;' multiple></select>";
              meetingAttributesHtml += "</div>";
              
                meetingAttributesHtml += "<div id=divAttendees"+counter +" style='padding-top:5px;'>";
                meetingAttributesHtml += "<label style='vertical-align:top;'>Attendees</label>";
                //meetingAttributesHtml += "<select id=" + "ddlCandidatesMeeting" + counter + " style='width:215px;height:150px;' multiple></select>";
                meetingAttributesHtml += "</div>";

                meetingAttributesHtml += "<div style='padding-top:5px;padding-left:20px;'>";
                meetingAttributesHtml += "<label>Position Recommendation</label>";
                meetingAttributesHtml += "<input name=" + "poistionReco" + counter + " type='radio' value='true' />";
                meetingAttributesHtml += "<span style='width:25px;padding-left:0px'>Yes</span>";
                meetingAttributesHtml += "<input name=" + "poistionReco" + counter + " type='radio' value='false' />";
                meetingAttributesHtml += "<span style='width:25px;padding-left:0px'>No</span>";
                meetingAttributesHtml += "</div>";
                meetingAttributesHtml += "</div>";
                $("#divMeetingAttributes").append(meetingAttributesHtml);

                //$("#divMeetingAttributes").append("<div style='padding-top:5px;'>");
                //$("#divMeetingAttributes").append("<label>Choose A Symbol</label>");
                //$("#divMeetingAttributes").append("<select id=" + "ddlSymbols" + counter + " style='width:215px;'></select>");
                //$("#divMeetingAttributes").append("</div>");




                $("#ddlSymbols" + counter).html(symbolList);
                $("#ddlSymbols" + counter).val(meetingAttributes[i].Symbol);
                $("#ddlSymbols" + counter).select2({

                });
                PopulateStocks(counter);
                $("#ddlSecurities" + counter).val(meetingAttributes[i].Securities);


                //$("#divMeetingAttributes").append("<div style='padding-top:5px;'>");
                //$("#divMeetingAttributes").append("<label style='vertical-align:top;'>Attendees</label>");
                //$("#divMeetingAttributes").append("<select id=" + "ddlCandidatesMeeting" + counter + " style='width:215px;height:150px;' multiple></select>");
                //$("#divMeetingAttributes").append("</div>");


                //$("#ddlCandidatesMeeting" + counter).html(employeeList);
                //$("#ddlCandidatesMeeting" + counter).val(meetingAttributes[i].Attendee.split(","));

                //syncPost("GetICMeetingEmployeeListHtml", "");
                $("#divAttendees" + counter).append(employeeList);
                $("#divAttendees" + counter + " input:checkbox").removeAttr('checked');

                    var attendees = meetingAttributes[i].Attendee.split(",");
                    for (var k = 0; k < attendees.length; k++)
                    {
                        $("#divAttendees" + counter + " #" + attendees[k]).attr('checked', 'checked');

                    }

                //$("#divMeetingAttributes").append("<div style='padding-top:5px;'>");
                //$("#divMeetingAttributes").append("<label>Position Recommendation</label>");
                //$("#divMeetingAttributes").append("<input name=" + "poistionReco" + counter + " type='radio' value='true' />");
                //$("#divMeetingAttributes").append("<label style='width:25px;padding-left:0px'>Yes</label>");
                //$("#divMeetingAttributes").append("<input name=" + "poistionReco" + counter + " type='radio' value='false' />");
                //$("#divMeetingAttributes").append("<label style='width:25px;padding-left:0px'>No</label>");
                //$("#divMeetingAttributes").append("</div>");

                if (meetingAttributes[i].RequiredRecommendation) {
                    $("input:radio[name=poistionReco" + counter + "]")[0].checked = true;
                }
                else {
                    $("input:radio[name=poistionReco" + counter + "]")[1].checked = true;
                }
               
                    counter = counter + 1;
              

            }


        }

     





}

function PopulateStocks(counter)
{
    syncPost("GetSecurityList", JSON.stringify({
        stockSymbol: $("#ddlSymbols"+counter).val()
    }));
    $("#ddlSecurities"+counter).html(syncPostResult);


}

function SendPositionRecommendations() {
    $("#divPreviewEmail").html("");
    var positionRecoPreviewHtml = "";
 

  //  positionRecoPreviewHtml += "<div id='subTitle' class='subtitle'>Add Email Subject <span style='margin-left:735px;cursor:pointer' onclick='hideForm();'>X</span></div>";
    positionRecoPreviewHtml += "<div><textarea id='txtEmailAddenddum' style ='width:650px; height:100px; margin-top:10px; margin-left:10px;'></textarea></div>";
    positionRecoPreviewHtml += "<div><input style='margin-top:10px; margin-left:10px;margin-bottom:10px;'  type='button' value='Send' onclick='SendPositionRecommendationsEmail()'/></div>";
    //syncPost("GetPositionRecommendationsHistoryHtmlEmail", JSON.stringify({
    //    tagId: 42, addendumText: ""
    //}));
    syncPost("GetPositionRecommendationsHistoryHtmlEmail", JSON.stringify({
        tagId: $('#ddlMeetingSymbolsPopup').val(), addendumText: ""
    }));
    positionRecoPreviewHtml += syncPostResult;

    $("#divPreviewEmail").append(positionRecoPreviewHtml);
   // $("#divPopUp").width(900);
   // $("#divPopUp").height(400);
   // $("#divPopUp").show().center();
   // showOverlay();
  
}

function OpenPositionRecommendationsPopup() {

    $("#divPopUp").html("");
    var positionRecoPreviewHtml = "";
    positionRecoPreviewHtml += "<div id='subTitle' class='subtitle'>Add Email Subject <span style='margin-left:735px;cursor:pointer' onclick='hideForm();'>X</span></div>";
   // positionRecoPreviewHtml += "<div><input style='margin-top:10px; margin-left:10px;margin-bottom:10px;'  type='button' value='GeneratePreview' onclick='SendPositionRecommendations()'/></div>";
    syncPost("GetSymbolListForMeeting", JSON.stringify({ meetingDate: $("#txtMeetingDate").val() }));
    positionRecoPreviewHtml += "<div style='margin-top:10px; margin-left:10px;margin-bottom:10px;'><select id='ddlMeetingSymbolsPopup' style='width:100px;height:20px;'>" + syncPostResult + "</select></div>";
    positionRecoPreviewHtml += "<div><input style='margin-left:10px;margin-bottom:10px;'  type='button' value='GeneratePreview' onclick='SendPositionRecommendations()'/></div>";
    positionRecoPreviewHtml += "<div id='divPreviewEmail'></div>";
    $("#divPopUp").append(positionRecoPreviewHtml);
    $("#divPopUp").width(900);
    $("#divPopUp").height(800);
    $("#divPopUp").show().center();
    showOverlay();

}

function SendPositionRecommendationsEmail() {

    //syncPost("SendPositionRecommendationsEmail", JSON.stringify({
    //    tagId: 42, addendumText: $("#txtEmailAddenddum").val()
    //}));
    syncPost("SendPositionRecommendationsEmail", JSON.stringify({
        tagId:  $('#ddlMeetingSymbolsPopup').val(), addendumText: $("#txtEmailAddenddum").val()
    }));
    alert("Email Sent");
    hideForm();
}

function SendICMeetingNotesEmail()
{
    var meetingLinkDoc = ($("#txtLinkMeetingDoc").val().length == 0) ? $("#lblLinkMeetingDoc").text() : $("#txtLinkMeetingDoc").val()

    syncPost("SendICMeetingNotesEmail", JSON.stringify({
        meetingId: $('#ddlICMeetings').val(), meetingDate: $("#txtMeetingDate").val(), meetingNotesDoc: meetingLinkDoc
    }));
    if (syncPostSuccess) {
        alert("Email Sent");
    }
    else {
        alert("There was a problem sending your email " + syncPostResult);
    }

}