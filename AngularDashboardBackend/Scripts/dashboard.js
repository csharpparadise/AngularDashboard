(function ($) {
    'use strict';
    var connection = $.connection("/signalr-db");

    connection.received(function (data) {
        console.log('data received');
        console.log(data);
        $('#dbData').html(data);
    });

    connection.start().done(function () {
        console.log("Connected!");
    });

})(jQuery);