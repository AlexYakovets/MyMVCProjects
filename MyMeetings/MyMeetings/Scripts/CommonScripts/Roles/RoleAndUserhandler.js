$(function () {
    $(".user-id").click(function (e) {
        $.get("/API/RoleForUsers/" + $("#" + e.currentTarget.id).val(),displayRoles);
        
        function displayRoles(data) {
            console.log(data);
            var role = JSON.parse(data);
            $('#list-of-roles li').remove();
            role.forEach(function(elem) {
                $("#list-of-roles").append("<li><input type='checkbox' data-userId="+elem.userId+" name='roles' " + (elem.IsAvaible ? "checked" : " ") + " value=" + elem.Role.Id + "/>" + elem.Role.Name + "</li>");
            });

        }
    });
    $(".btn-save").click(function (e) {
        var arr = [];
        $("input[name='roles']").each(function(index, element) {
            var role = {};
            role.IsAvailible = element.checked;
            role.roleId = element.value;
            //role.userId = 
            arr.push(role);
        });
        var data = JSON.stringify(arr);
        console.log(data);
        console.log(arr);
    });
});
