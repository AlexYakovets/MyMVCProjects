
$(function () {
    var elements = document.getElementsByClassName("timer")
    for (var i = 0; i < elements.length; i++) {
        var timer = elements[i];
        var id = timer.getAttribute("value");
        var date = document.getElementById(id).getAttribute("value");
        $(timer).flipcountdown({
            size: "sm",
            beforeDateTime: date

            //beforeDateTime: '1/01/' + (dt.getFullYear() + 1) + ' 00:00:01'
        });
        //    console.log(date);
        //}
        //{
        //    console.log(element);
        //    var date = $('#PublicationId[value=' + element + '] .DateTimeOfMeet');
        //    console.log(date);
        //}
        //var dt = new Date();
        //$('.timer').flipcountdown({
        //    size: "sm",
        //    //beforeDateTime: ('05/01/2017 00:00:01')

        //    //beforeDateTime: '1/01/' + (dt.getFullYear() + 1) + ' 00:00:01'
        //});
    }
});