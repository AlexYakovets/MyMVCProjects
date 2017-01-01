$(function () {
    $(".user-id").click(function (e) {
        alert($("#" + e.currentTarget.id).val());
        $.get("/API/RoleForUsers/" + $("#" + e.currentTarget.id).val());
    });

});