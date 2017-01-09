$(function() {
    $("#upload-image-button").click(function(e) {
        var inputFile = document.getElementById("imagefile").files;
        var file = inputFile.item(0);
        file.type == 'image/png' ? console.log("Все ОК") : alert("Файл не того формата");

    });
});
