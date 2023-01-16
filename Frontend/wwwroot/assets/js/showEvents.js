function events(currentevents, value1, value2, value3){
    if (currentevents < 5){
        currentevents = 5;
    }
    var addevent = parseInt(currentevents) - 5;
    $('#events').load('Events?addevent='+addevent+'&value1='+value1+'&value2='+value2+"&value3="+value3);
}
function getnotifs(){
    $.ajax({
        type: "GET",
        url: "/Home/GiveNotification",
        dataType: "json",
    }).done(function (data){
        }
    )
}
function getmapdata(currentevents, value1, value2, value3){
    if (currentevents < 5){
        currentevents = 5;
    }
    $.ajax({
        type: "GET",
        url: "/Home/GetData?addevent="+currentevents+"&value1="+value1+"&value2="+value2+"&value3="+value3, // + currentevents
        dataType: "json",
    }).done(function (data) {
        addmarkers(data);
    })
}