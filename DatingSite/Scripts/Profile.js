function SendMessage(message, date) {
    var MessageApiObject = {
        Message: message,
        Date: date
    };

    $.ajax({
        url: 'http://localhost:50856/api/message',
        type: 'POST',
        async: false,
        data: JSON.stringify(MessageApiObject),
        dataType: "json",
        contentType: "application/json; charset = utf-8",
        success: function (data) {
            alert(data);
            return true;
        },
        error: function () {
            alert('Failed to deliver message :(');
        }
    });
}