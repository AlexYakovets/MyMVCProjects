$(function() {
    $("#upload-image-button").click(function(e) {
        var inputFile = document.getElementById("imagefile").files;
        var file = inputFile.item(0);
        file.type == "image/png" ? OnSucces() : onFailed(e);

        function OnSucces() {
            $(".status-message").append("<div class='alert alert-success'>File will be uploaded to the server </div>");

        }
        function onFailed(e) {
            $(".status-message").append("<div class='alert alert-warning'>File is not have image extension </div>");
            e.preventDefault();
        }
    });
});
