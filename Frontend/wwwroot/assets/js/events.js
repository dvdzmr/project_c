function getview(id){
    // var url = "@Html.Raw(Url.Action('ViewDetailsPartial', 'Home', new {test = "-parameter"}))";
    // url = url.replace("-parameter", id)
    // console.log("works");

    // $.ajax({
    //     url: "/Home/ViewDetailsPost",
    //     type: "POST",
    //     data: JSON.stringify(MapItems),
    //     dataType: "json",

    //      url: url,

    $.ajax({
        url: "/Home/ViewDetailsPartial?test="+id,
        type: "GET",
        cache: false,
        success: function (result){
            $("#viewdetailsofevent").html(result)
        },
        error: function (){
            console.log("error")
        }
    });
}
function moreEvents(currentevents){
    var addevent = parseInt(currentevents) + 25;
    $.ajax({
        url: "/Home/Map?addevent="+addevent,
        type: "GET",
        cache: false,
        success: function (result){
            $("#events").html(result)
        },
        error: function (){
            console.log("error")
        }
    });
}