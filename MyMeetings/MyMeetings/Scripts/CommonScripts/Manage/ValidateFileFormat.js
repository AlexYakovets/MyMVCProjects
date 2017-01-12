$(function() {
    $("#upload-image-button").click(function (e) {
        $("div[id='status-message']").remove();
        var inputFile = document.getElementById("imagefile").files;
        var file = inputFile.item(0);
   
        ((file.type == "image/png")||( file.type == "image/jpg") || (file.type == "image/bmp")) ? OnSucces() : onFailed(e);

        function OnSucces() {
            $(".status-message").append("<div id='status-message' class='alert alert-success'>File will be uploaded</div>");

        }
        function onFailed(event) {
            $(".status-message").append("<div id='status-message' class='alert alert-danger'>File is not have image extension </div>");
            event.preventDefault();
        }
    });
});
