$(function () {
    'use strict';

    var sideNav = $('.side-nav'),
      burgerToggle = $('.burger-toggle'),
      accordianToggle = $('.accordian-toggle', sideNav);

    burgerToggle.on('click', function () {
        $(this).toggleClass('active');

        if ($(this).hasClass('active')) {
            sideNav.addClass('open');
        } else {
            sideNav.removeClass('open');
            accordianToggle.removeClass('active');
        }
    });

    accordianToggle.on('click', function () {
        $(this).toggleClass('active');
    });

    $(document).on('click', function (e) {
        var targets = sideNav.add(burgerToggle);
        if (!targets.is(e.target) && targets.has(e.target).length === 0) {
            sideNav.removeClass('open');
            burgerToggle.removeClass('active');
            accordianToggle.removeClass('active');
        }
    });

});