$().ready(function () {



    //var meetingId = getParameterByName('meetingId');
    //var meetingDate = getParameterByName('meetingDate');
    //syncPost("GetPositionRecommendationsHtml", JSON.stringify({
    //    meetingId: meetingId, meetingDate: meetingDate
    //}));
    //$("#divPositionRecommendations").html(syncPostResult);



    if(confirm("Are you sure you want to mark yourself as having reviewed the meeting notes?"))
    {
        var meetingId = getParameterByName('meetingId');
        syncPost("UpdateMeetingNotesChecked", JSON.stringify({
            meetingId: meetingId
        }));

    }
    window.open('', '_self', '');
    window.close();


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