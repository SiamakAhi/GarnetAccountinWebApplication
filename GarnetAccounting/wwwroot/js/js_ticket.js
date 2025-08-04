var formModal = $(document).find('#formmodal');

$(document).on('click', '.ownerTicketInfoBtn', function () {
    let infoPad = $(this).parents('.item').find('.overally-userInfo');
    infoPad.slideDown(300);
});
$(document).on('click', '.overally-userInfo-close', function () {
    let infoPad = $(this).parents('.overally-userInfo');;
    infoPad.slideUp(300);
});

$(document).on('click', ".callModalForm", function () {

    var actionUrl = $(this).data('url');
    $.get(actionUrl).done(function (data) {
        var formModal = $(document).find('#formmodal');
        formModal.html(data);
        formModal.modal('show');
    });

});

//-----------------------------------------------------------------------Page Link
$(document).on('click', ".page-link", function () {

    var actionUrl = $(this).data('url');
    var updateTarget = $(this).parents('.card-body');

    $.get(actionUrl).done(function (data) {
        updateTarget.html(data);
    });

});


$(document).on('click', '.ajax-search', function () {

    var form = $(this).parents('form');
    var actionUrl = form.attr('action');
    var dataToSend = new FormData(form.get(0));
    let updateTarget = $(this).parents('.card').find('.card-body');

    $.ajax({
        url: actionUrl,
        type: "post",
        data: dataToSend,
        processData: false,
        contentType: false,

    }).done(function (data) {
        updateTarget.html(data);
    });


});

//---------------------------------------------------------
$(document).on('click', '#showTicketDetails', function () {
    var target = $(this).parents('.item').find('.item-content-ditails');

    if (target.hasClass('show')) {
        target.hide(300, function () {
            target.removeClass('show');
        });
    }
    else {
        target.show(300, function () {
            target.addClass('show');
        });
    }

});

$(document).on('click', '.btn-post', function () {
    let Title = $(this).attr('title');
    let actionUrl = $(this).data('url');

    if (confirm(Title)) {
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


$(document).on('click', '.submitajax-ticket', function () {

    var form = $(this).parents('form');
    var actionUrl = form.attr('action');
    var dataToSend = new FormData(form.get(0));
    let title = $(this).attr('title');
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
                if (res['updateType'] == 1) {

                    if (res['returnUrl'].length > 0) {
                        window.location.href = res['returnUrl'];
                    }
                }
                else if (res['updateType'] == 2) {
                    
                    var updatePlaceHolder = $(document).find(res['updateTargetId']);
                    $.get(res['returnUrl']).done(function (data) {

                        updatePlaceHolder.html(data);

                        var formModal = $(document).find('#formmodal');
                        formModal.modal('hide');
                    });
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

$(document).on('click', '.submitajax-AnswerTicket', function () {

    var form = $(this).parents('form');
    var actionUrl = form.attr('action');
    var dataToSend = new FormData(form.get(0));
    let title = $(this).attr('title');
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

                //alert(res['Message']);
                if (res['updateType'] == 1) {

                    if (res['returnUrl'].length > 0) {
                        window.location.href = res['returnUrl'];
                    }
                }
                else if (res['updateType'] == 2) {

                    var updatePlaceHolder = $(document).find(res['updateTargetId']);
                    $.get(res['returnUrl']).done(function (messages) {

                        updatePlaceHolder.html(messages);

                        if (!updatePlaceHolder.parent().hasClass('show')) {

                            updatePlaceHolder.parent().show(300, function () {
                                updatePlaceHolder.parent().addClass('show');
                            });
                        }

                        var formModal = $(document).find('#formmodal');
                        formModal.modal('hide');
                    });
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