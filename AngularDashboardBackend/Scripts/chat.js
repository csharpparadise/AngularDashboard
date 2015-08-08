(function ($) {
    'use strict';
    var username;

    var chatHub = $.connection.chatHub;

    chatHub.client.hello = function (message) {
        addMessage(message);
    };

    chatHub.client.send = function (ts, user, message) {
        var displayedMessage = ts + ' - ' + user + ': ' + message;
        addMessage(displayedMessage);
    };

    $.connection.hub.start().done(function () {
        // Wire up Send button to call NewContosoChatMessage on the server.
        $("#btnJoin").click(function () {
            username = prompt("Please enter your username.");
            chatHub.server.hello(username);
            $('#message').val('').focus();
            $(this).hide();
            $('#send').prop("disabled", false);
        });

        $("#send").click(function (event) {
            chatHub.server.send(username, $('#message').val());
            $('#message').val('').focus();
            event.preventDefault();
        });
    });

    function addMessage(msg) {
        var msgContainer = $("<span/>");
        msgContainer.html(msg);
        $('#chat').prepend(msgContainer);
    }

})(jQuery);