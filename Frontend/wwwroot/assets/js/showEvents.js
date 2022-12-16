function events(currentevents){
    var addevent = parseInt(currentevents) - 5;
    $('#events').load('Events?addevent='+addevent);
}
function getmapdata(currentevents){
    $.ajax({
        type: "GET",
        url: "/Home/GetData?addevent="+currentevents, // + currentevents
        dataType: "json",
    }).done(function (data) {
        addmarkers(data);
    })
}