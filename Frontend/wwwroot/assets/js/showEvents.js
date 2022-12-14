function events(currentevents){
    var addevent = parseInt(currentevents) - 5;
    $('#events').load('Events?addevent='+addevent);
}
function getmapdata(currentevents){
    $.ajax({
        type: "GET",
        url: '@Url.Action("GetData", "Home")', // + currentevents
        dataType: "json",
    }).done(function (data) {
        newmmap(data);
    })
}