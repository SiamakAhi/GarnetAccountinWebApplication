
var formModal = $(document).find('#formmodal');

$(document).on('click', '.submitajax', function () {

    var form = $(this).parents('form');
    var actionUrl = form.attr('action');
    var dataToSend = new FormData(form.get(0));
    let title = $(this).data('confirmtext');

    if (confirm(title)) {
        $.ajax({
            url: actionUrl,
            type: "post",
            data: dataToSend,
            processData: false,
            contentType: false,

        }).done(function (data) {
            var res = JSON.parse(data);
            if (res['Success']) {
                alert(res['Message']);
                if (res['returnUrl'].length > 0) {
                    window.location.href = res['returnUrl'];
                }
            }
            else {
                alert(res['Message']);
                if (res['returnUrl'].length > 0) {
                    window.location.href = res['returnUrl'];
                }
            }

        });
    }
});

$(document).on('click', '.deleteAjax', function () {

   
    var actionUrl = $(this).data('url');
    let title = $(this).data('confirmtext');

    if (confirm(title)) {
        $.ajax({
            url: actionUrl,
            type: "post",
            processData: false,
            contentType: false,

        }).done(function (data) {
            var res = JSON.parse(data);
            if (res['Success']) {
                alert(res['Message']);
                if (res['returnUrl'].length > 0) {
                    window.location.href = res['returnUrl'];
                }
            }
            else {
                alert(res['Message']);
                if (res['returnUrl'].length > 0) {
                    window.location.href = res['returnUrl'];
                }
            }

        });
    }
});




$(document).on('click', ".callModalForm", function () {

    var actionUrl = $(this).data('url');
    $.get(actionUrl).done(function (data) {
        var formModal = $(document).find('#formmodal');
        formModal.html(data);
        formModal.modal('show');
    });

});