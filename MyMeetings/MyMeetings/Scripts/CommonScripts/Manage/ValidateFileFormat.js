$(function() {
    $("#upload-image-button").click(function (e) {
        $("div[id='status-message']").remove();
        var inputFile = document.getElementById("imagefile").files;
        var file = inputFile.item(0);

        if ((file.type == "image/png") || (file.type == "image/jpeg") || (file.type == "image/bmp")) OnSucces(); else {onFailed(e);}
        
        function OnSucces() {
            $(".status-message").append("<div id='status-message' class='alert alert-success'>File will be uploaded</div>");

        }
        function onFailed(event) {
            console.log(file.type);
            $(".status-message").append("<div id='status-message' class='alert alert-danger'>File is not have image extension </div>");
            event.preventDefault();
        }
    });
});
