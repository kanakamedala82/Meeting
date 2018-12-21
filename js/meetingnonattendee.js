$().ready(function () {



    


    GetInitData();


});
function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

function GetInitData() {
    //var meetingId = getParameterByName('meetingId');
    var meetingDate = getParameterByName('meetingDate');
    //syncPost("GetPositionRecommendationsHtml", JSON.stringify({
    //    meetingId: meetingId, meetingDate: meetingDate
    //}));
    $("#divMeetingHeading").html("Please enter the reason for not attending IC meeting on " + meetingDate);


}

function SaveReason()
{

    var tagId = getParameterByName('tagId');
    syncPost("InsertNonAttendeeReason", JSON.stringify({
        tagId: tagId, reason: $("#txtNonAttendeeReason").val()
    }));
    window.open('', '_self', '');
    window.close();

}