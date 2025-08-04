var InvoiceArchivePlace = $(document).find('#invc-archive');
var InvoiceToAcceptPlace = $(document).find('#invc-ToAccept');
var InvoiceNewPlace = $(document).find('#navs-top-home');



$(document).on('click', '.delStuff', function () {
    loder.style.display = "block";
    let actionUrl = $(this).data('url');
    let stuffId = $(this).data('target');
    if (confirm('شماره در حال حذف اطلاعات کالا یا خدمت هستید، آیا ادامه می دهید ؟')) {

        $.ajax({
            url: actionUrl,
            type: 'post',
            data: { id: stuffId },
        }).done(function (result) {
            let jsonResult = JSON.parse(result);

            if (jsonResult['Success'] == true) {
                Swal.fire({
                    title: 'کاربـر گـرامی !',
                    html: jsonResult['Message'],
                    icon: 'success',
                    customClass: {
                        confirmButton: 'btn btn-primary'
                    },
                    confirmButtonText: 'باشه',
                    buttonsStyling: false
                });
                $(this).delay(2000).queue(function () {
                    window.location.href = jsonResult['ReturnUrl'];
                    $(this).dequeue();
                });
            }
            else {
                Swal.fire({
                    title: 'کاربـر گـرامی !',
                    html: jsonResult['Message'],
                    icon: 'warning',
                    customClass: {
                        confirmButton: 'btn btn-primary'
                    },
                    confirmButtonText: 'باشه',
                    buttonsStyling: false
                });
            }
        }).always(function () {
            loder.style.display = "none";
        });
    }
    else {
        loder.style.display = "none";
    }
});

$(document).on('click', ".StuffSearchForm", function () {
    let actionUrl = $(this).data('url');
    $.get(actionUrl).done(function (data) {

        generalModal.html(data);
        generalModal.modal('show');
    });
});

$(document).on('click', '.getStuffInfo', function () {
    loder.style.display = "block";
    let frm = $(this).parents('form');
    let actionUrl = frm.attr('action');
    let dataToSend = new FormData(frm.get(0));

    $.ajax({
        url: actionUrl,
        type: 'post',
        data: dataToSend,
        processData: false,
        contentType: false
    }).done(function (data) {
        /* var generalModal = $(document).find('#model-general-larg');*/
        generalModal.html(data);
        generalModal.modal('show');
    }).always(function () {
        loder.style.display = "none";
    });
});

$(document).on('click', ".setForStuff", function () {
    loder.style.display = "block";
    let actionUrl = $(this).data('url');
    let stuffId = $(this).data('target');
    let Code = $(this).data('bind');
    let Vat = $(this).data('vat');
    $.ajax({
        url: actionUrl,
        type: 'post',
        data: { id: stuffId, code: Code, vat: Vat },
    }).done(function (result) {
        let jsonResult = JSON.parse(result);

        if (jsonResult['Success'] == true) {
            //var generalModal = $(document).find('#model-general-larg');
            generalModal.modal('hide');
            window.location.href = jsonResult['ReturnUrl'];
        }
        else {
            Swal.fire({
                title: 'کاربـر گـرامی !',
                html: jsonResult['Message'],
                icon: 'warning',
                customClass: {
                    confirmButton: 'btn btn-primary'
                },
                confirmButtonText: 'باشه',
                buttonsStyling: false
            });
        }
    }).always(function () {
        loder.style.display = "none";
    });

});

$(document).on('click', '.invoiceArchvePageLink', function () {
    let actionUrl = $(this).data('url');
    loder.style.display = "block";
    $.get(actionUrl).done(function (data) {
        InvoiceArchivePlace.html(data);
    }).always(function () {
        loder.style.display = "none";
    });
});
$(document).on('click', '.invoiceToAcceptPageLink', function () {
    let actionUrl = $(this).data('url');
    loder.style.display = "block";
    $.get(actionUrl).done(function (data) {
        InvoiceToAcceptPlace.html(data);
    }).always(function () {
        loder.style.display = "none";
    });
});
$(document).on('click', '.invoiceNewPageLink', function () {
    let actionUrl = $(this).data('url');
    loder.style.display = "block";
    $.get(actionUrl).done(function (data) {
        InvoiceNewPlace.html(data);
    }).always(function () {
        loder.style.display = "none";
    });
});
