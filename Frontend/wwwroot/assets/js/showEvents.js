function events(currentevents){
    var addevent = parseInt(currentevents) - 5;
    $('#events').load('Events?addevent='+addevent);
}
