$().ready(function () {
    

    
    //var meetingId = getParameterByName('meetingId');
    //var meetingDate = getParameterByName('meetingDate');
    //syncPost("GetPositionRecommendationsHtml", JSON.stringify({
    //    meetingId: meetingId, meetingDate: meetingDate
    //}));
    //$("#divPositionRecommendations").html(syncPostResult);
   

  
    GetInitData();


});


function SaveRecommendations(recommendationIds)
{
    var arrRecommendationIds = recommendationIds.split(",");

    var positionRecommendations = [];

    for (var i = 0; i < arrRecommendationIds.length; i++) {
        var positionRecommendation = {};
        positionRecommendation.TagSecurityId = arrRecommendationIds[i];
        positionRecommendation.Pros = $("#pros" + arrRecommendationIds[0]).val();
        positionRecommendation.Cons = $("#cons" + arrRecommendationIds[0]).val();
        positionRecommendation.ConvertAllocation = $("#alloc" + arrRecommendationIds[i]).val();
        positionRecommendations.push(positionRecommendation);
    }

    syncPost("InsertPositionRecommendations", JSON.stringify({
        recommendations: positionRecommendations
    }));

   // GetInitData();

}


function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

function GetHistory(tagId,meetingId)
{
    $("#divPopUp").load('SymbolRecommendations.aspx', function () {


        syncPost("GetPositionRecommendationsHistoryHtml", JSON.stringify({
            tagId: tagId
        }));
        $("#divPositionRecommendationHistory").html(syncPostResult);

        $("#divPopUp").width(1200);
        $("#divPopUp").height(600);
        $("#divPopUp").show().center();

        showOverlay();
    });
}

function hideForm() {
    $("#divPopUp").fadeOut("fast");
    counter = 1;
    hideOverlay();
}


function GetInitData()
{
    var meetingId = getParameterByName('meetingId');
    var meetingDate = getParameterByName('meetingDate');
    syncPost("GetPositionRecommendationsHtml", JSON.stringify({
        meetingId: meetingId, meetingDate: meetingDate
    }));
    $("#divPositionRecommendations").html(syncPostResult);


}