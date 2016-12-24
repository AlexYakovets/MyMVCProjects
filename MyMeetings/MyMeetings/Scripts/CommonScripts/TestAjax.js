$(function () {
    $(".test-ajax").click(function(e) {
        $.get("/api/Users", display);
        function display(data) {
            var obj = JSON.parse(data);
            obj.forEach(function(elem) {
                $("#userList").append("<li>"+elem["UserName"]+"</li>");
            });
            console.log(obj);
           // alert(data);
        };
    }); 
});