
$(function () {
    $('.timer').each(function (index, element)
    {
        var date = $('#' + element.id + ' .DateTimeOfMeet').val;
        Console.log(date);
    })
    var dt = new Date();
    $('.timer').flipcountdown({
        size: "sm",
        //beforeDateTime: ('05/01/2017 00:00:01')
        beforeDateTime: $('.timer .DateTimeOfMeet')
        //beforeDateTime: '1/01/' + (dt.getFullYear() + 1) + ' 00:00:01'
    });
});