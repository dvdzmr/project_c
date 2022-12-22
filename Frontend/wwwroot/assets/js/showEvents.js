function events(currentevents){
    if (currentevents < 5){
        currentevents = 5;
    }
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