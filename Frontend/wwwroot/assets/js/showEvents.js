function events(currentevents, value1, value2){
    if (currentevents < 5){
        currentevents = 5;
    }
    var addevent = parseInt(currentevents) - 5;
    $('#events').load('Events?addevent='+addevent+'&value1='+value1+'&value2='+value2);
}
function getnotifs(value1,value2){
    $.ajax({
        type: "GET",
        url: "/Home/GiveNotification?value1="+value1+"&value2="+value2,
        dataType: "json",
    }).done(function (data){
        }
    )
}
function getmapdata(currentevents, value1, value2){
    if (currentevents < 5){
        currentevents = 5;
    }
    $.ajax({
        type: "GET",
        url: "/Home/GetData?addevent="+currentevents+"&value1="+value1+"&value2="+value2, // + currentevents
        dataType: "json",
    }).done(function (data) {
        addmarkers(data);
    })
}