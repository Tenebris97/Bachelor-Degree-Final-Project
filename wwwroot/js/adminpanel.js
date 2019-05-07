$(document).ready(function () {
    $(".menu li").addClass('openul').children('ul').show();

    $(".menu li.mainli > a").on('click', function () {
        var getli = $(this).parent('li');
        if (getli.hasClass('openul')) {//beband
            getli.removeClass('openul');
            getli.find('ul').slideUp(200);
        }
        else {//baz kon
            getli.addClass('openul');
            getli.children('ul').slideDown(200);
        }
    })
})