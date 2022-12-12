function getview(id){
    $.ajax({
        url: "/Home/ViewDetailsPartial?test="+id,
        type: "GET",
        cache: false,
        success: function (result){
            $("#viewdetailsofevent").html(result)
            openNav()
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
            $("#events").html(result);
            
            // Code hieronder kan later worden verwijdert (voor nu nog niet) - Min En
            
            // var tmp = document.getElementById("testing");
            // var scripthtml = tmp.innerHTML;
            // tmp.remove();
            // var newscript = document.createElement('script');
            // newscript.id = "testing";
            // newscript.appendChild(document.createTextNode(scripthtml));
            // document.body.appendChild(newscript);
            // console.log("works?")
        },
        error: function (){
            console.log("error")
        }
    });
}
function closing(){
    $.ajax({
        url: "/Home/Main",
        type: "GET",
        cache: false,
        success: function (result){
            closeNav()
        },
        error: function (){
            console.log("error")
        }
    });
}