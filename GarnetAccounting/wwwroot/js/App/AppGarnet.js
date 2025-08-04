
let generalModal = $(document).find('#model-general-larg');
var loder = document.getElementById("loader");


$(document).on('submit', 'form', function (event) {
    loder.style.display = "block";

});
$(document).on('click', '.loaderOff', function (event) {
    loder.style.display = "none";
});


$(document).on('click', 'table > tbody > tr', function () {
    $('table tr').removeClass('selectedrow');
    $(this).addClass('selectedrow');
});

$(document).on('click', '.bymodal[data-url]', function () {

    let actionUrl = $(this).data('url');

    $.get(actionUrl).done(function (data) {

        generalModal.html(data);
        generalModal.modal('show');

        $(document).find('.flatpickr-input').flatpickr({
            locale: 'fa',
            altInput: true,
            altFormat: 'Y/m/d',
            allowInput: true,
        });
        $(document).find('.select2').select2();
    })
});
$(document).on('click', '.newdoc[data-url]', function () {

    let actionUrl = $(this).data('url');

    $.get(actionUrl).done(function (data) {

        generalModal.html(data);
        generalModal.modal('show');

        $(document).find('.flatpickr-input').flatpickr({
            locale: 'fa',
            altInput: true,
            altFormat: 'Y/m/d',
            allowInput: true,
            disableMobile: true
        });
        $(document).find('input[name="DocNumber"]').select();
    })
});

$(document).on('click', '.sellerName[data-url]', function () {

    let actionUrl = $(this).data('url');
    $.get(actionUrl).done(function (data) {

        generalModal.html(data);
        generalModal.modal('show');
    })
});

//................................
$(document).on('click', '.SubmitByAjax', function () {

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
        let result = JSON.parse(data);
        let alerticon = result['Success'] == true ? 'success' : 'warning';

        Swal.fire({
            title: 'کاربـر گـرامی !',
            html: result['Message'],
            icon: alerticon,
            customClass: {
                confirmButton: 'btn btn-primary'
            },
            confirmButtonText: 'باشه',
            buttonsStyling: false
        });
        if (result['ReturnUrl']) {
            $(this).delay(2000).queue(function () {
                window.location.href = result['ReturnUrl'];
                $(this).dequeue();
            });
        }
    }).always(function () {
        loder.style.display = "none";
    });
});
$(document).on('click', '.SubmitAjax', function () {

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
        let result = JSON.parse(data);
        let alerticon = result['Success'] == true ? 'success' : 'warning';

        Swal.fire({
            title: 'کاربـر گـرامی !',
            html: result['Message'],
            icon: alerticon,
            customClass: {
                confirmButton: 'btn btn-primary'
            },
            confirmButtonText: 'باشه',
            buttonsStyling: false
        });
        if (result['ReturnUrl']) {
            $(this).delay(2000).queue(function () {
                window.location.href = result['ReturnUrl'];
                $(this).dequeue();
            });
        } else {
            generalModal.modal('hide');
        }
    }).always(function () {
        loder.style.display = "none";
    });
});

$(document).on('click', '.ajaxSubmit', function () {
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
        let result = JSON.parse(data);
        let alerticon = result['Success'] == true ? 'success' : 'warning';

        Swal.fire({
            title: 'کاربـر گـرامی !',
            html: result['Message'],
            icon: alerticon,
            customClass: {
                confirmButton: 'btn btn-primary'
            },
            confirmButtonText: 'باشه',
            buttonsStyling: false
        });

        if (result['ReturnUrl']) {
            $(this).delay(2000).queue(function () {
                window.location.href = result['ReturnUrl'];
                $(this).dequeue();
            });
        }
    }).always(function () {
        loder.style.display = "none";
    });
});



//============================== Add Stuff
var stuffExportFields = $(document).find('#exportFields');
stuffExportFields.hide();

$(document).on('click', 'input[name="AddTemp"]', function () {
    let valu = $(this).val();
    if (valu == 2) {
        stuffExportFields.show();
    }
    else {
        stuffExportFields.hide();
    }
});

//=========================================================== End add stuff

//===== Add Buyer =====================================

$(document).on('click', '.getEcco', function () {
    loder.style.display = "block";
    let actionUrl = $(this).data('url');
    let validtext = $(document).find('#ecconumber').val();
    if (validtext == null || validtext == 'undefined' || validtext == '')
        return;

    $.ajax({
        url: actionUrl,
        type: 'post',
        data: { ecode: validtext }

    }).done(function (data) {
        /* let generalModal = $(document).find('#model-general-larg');*/
        generalModal.html(data);
        generalModal.modal('show');
    }).always(function () {
        loder.style.display = "none";
    });

});

//...
$(document).on('click', '.delBuyer', function () {

    loder.style.display = "block";
    let actionUrl = $(this).data('url');
    let buyerId = $(this).data('target');

    if (confirm('شماره در حال حذف اطلاعات مشتری هستید، آیا ادامه می دهید ؟')) {

        $.ajax({
            url: actionUrl,
            type: 'post',
            data: { id: buyerId },
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

$(document).on('click', '.btnaddtobuyer', function () {
    let name = $(document).find('#bname').html();
    let natcode = $(document).find('#bnat').text();
    //
    $(document).find('#buyernat').val(natcode);
    $(document).find('#buyerTradeName').val(name);
    /*let generalModal = $(document).find('#model-general-larg');*/
    generalModal.modal('hide');

});

//=================================================== End Add buyer
//-------------------------------------------------------------------------
//============ Invoic list  ===============================================

$(document).on('change', '#SelectRows', function () {

    $(document).find('input[name="docId"]').prop('checked', this.checked);
})

$(document).on('click', '#sendToMoadian', function () {

    let selectedValues = $(document).find('input[name="chb-InvoiceId"]:checked').map(function () {
        return this.value;
    }).get();

    if (selectedValues == null) return;

    let ActionUrl = $(this).data('url');
    $.ajax({
        url: ActionUrl,
        type: 'post',
        data: { invoceIds: selectedValues }
    }).done(function (data) {
        $('#place').html(data);
    });

});

$('#yourButtonId').click(function () {
    var selectedIds = $('input[name="chb-InvoiceId"]:checked').map(function () {
        return this.value;
    }).get();

    $.ajax({
        url: '@Url.Action("ActionName", "ControllerName")',
        type: 'post',
        data: { ids: selectedIds },
        success: function (response) {

        },
        error: function () {

        }
    });
});


$(document).on('click', '.sentInvc', function () {
    loder.style.display = "block";
    let actionUrl = $(this).data('url');
    let InvoiceId = $(this).data('target');
    $.ajax({
        url: actionUrl,
        type: 'post',
        data: { invoceId: InvoiceId },
    }).done(function (result) {
        let jsonResult = JSON.parse(result);

        if (jsonResult['Success'] == true) {

            window.location.href = jsonResult['ReturnUrl'];
        }
        else {
            Swal.fire({
                title: 'کاربـر گـرامی !',
                text: jsonResult['Message'],
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

$(document).on('click', '#invoiceInquery', function () {
    loder.style.display = "block";
    let actionUrl = $(this).data('url');
    let InvoiceId = $(this).data('target');
    let refrenc = $(this).data('bind');
    $.ajax({
        url: actionUrl,
        type: 'post',
        data: { invoiceId: InvoiceId, referenceNumber: refrenc },

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
            $(this).delay(4000).queue(function () {
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
            if (jsonResult['ReturnUrl']) {
                $(this).delay(4000).queue(function () {

                    window.location.href = jsonResult['ReturnUrl'];
                    $(this).dequeue();
                });
            }

        }
    }).always(function () {
        loder.style.display = "none";
    });
});

$(document).on('click', '#btnCancelationInvoice', function () {
    let actionUrl = $(this).data('url');
    $.get(actionUrl).done(function (data) {
        let modalTarget = $(document).find('#model-general-larg');
        modalTarget.html(data);
        modalTarget.modal('show');
    });
});

$(document).on('click', '.delInvoice', function () {
    loder.style.display = "block";
    let actionUrl = $(this).data('url');
    let InvoiceId = $(this).data('target');

    if (confirm('شماره در حال حذف کردن یک صورتحساب هستید، آیا ادامه می دهید ؟')) {

        $.ajax({
            url: actionUrl,
            type: 'post',
            data: { invoceId: InvoiceId },
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
                $(this).delay(3000).queue(function () {
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

$(document).on('click', '.delRow', function () {
    loder.style.display = "block";
    let actionUrl = $(this).data('url');
    let id = $(this).data('target');
    let UserName = $(this).data('bind');

    if (confirm('ردیف موردنظر حذف شود ؟')) {

        $.ajax({
            url: actionUrl,
            type: 'post',
            data: { Id: id, userName: UserName },
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
                $(this).delay(3000).queue(function () {
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

$(document).on('click', '.cususerDel', function () {
    loder.style.display = "block";
    let actionUrl = $(this).data('url');
    let UserName = $(this).data('target');

    if (confirm('کاربر موردنظر حذف شود ؟')) {

        $.ajax({
            url: actionUrl,
            type: 'post',
            data: { userName: UserName },
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
                $(this).delay(3000).queue(function () {
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

//=== Home Page ==========================================================



$(document).on('click', 'button.postajax', function () {
    loder.style.display = "block";
    let frm = $(this).parents('form');
    let actionUrl = frm.attr('action');
    let dataToSend = new FormData(frm.get(0));
    let validtext = $('input[name="EconomicCode"]').val();
    if (validtext == null || validtext == 'undefined' || validtext == '')
        return;

    $.ajax({
        url: actionUrl,
        type: 'post',
        data: dataToSend,
        processData: false,
        contentType: false
    }).done(function (data) {
        let generalModal = $(document).find('#model-general-larg');
        generalModal.html(data);
        generalModal.modal('show');
    }).always(function () {
        loder.style.display = "none";
    });

});

$(document).on('click', '.searchAjax', function () {
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
        let generalModal = $(document).find('#model-general-larg');
        generalModal.html(data);
        generalModal.modal('show');
    }).always(function () {
        loder.style.display = "none";
    });

});


//==== Check seller Connection ==========================

$(document).on('click', '.checkconnection', function () {
    loder.style.display = "block";
    let actionUrl = $(this).data('url');
    $.get(actionUrl).done(function (result) {
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

$(document).on('click', '.sendMessage', function () {
    let actionUrl = $(this).data('url');
    $.get(actionUrl).done(function (frmMessage) {

        generalModal.html(frmMessage);
        generalModal.modal('show');
    });

});

$(document).on('click', 'button.downloadbyform', function () {
    loder.style.display = "block";
    let frm = $(this).parents('form');
    let actionUrl = frm.attr('action');
    let dataToSend = new FormData(frm.get(0));
    let filaName = $(this).data('target');

    $.ajax({
        url: actionUrl,
        type: 'post',
        data: dataToSend,
        processData: false,
        contentType: false
    }).done(function (response) {

    }).always(function () {
        loder.style.display = "none";
    });

});

function separate(Number) {
    Number += '';
    Number = Number.replace(',', '');
    x = Number.split('.');
    y = x[0];
    z = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(y))
        y = y.replace(rgx, '$1' + ',' + '$2');
    return y + z;
}
function itpro(Number) {
    Number += '';
    Number = Number.replace(',', ''); Number = Number.replace(',', ''); Number = Number.replace(',', '');
    Number = Number.replace(',', ''); Number = Number.replace(',', ''); Number = Number.replace(',', '');
    x = Number.split('.');
    y = x[0];
    z = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(y))
        y = y.replace(rgx, '$1' + ',' + '$2');
    return y + z;
}

//Person

$(document).on('click', '.removeRepresentative', function () {

    loder.style.display = "block";
    let actionUrl = $(this).data('url');
    let partyId = $(this).data('party');
    let representativeId = $(this).data('representative');

    if (confirm('ردیف مورد نظر از لیست رابطین حذف شود ؟')) {

        $.ajax({
            url: actionUrl,
            type: 'post',
            data: { partyId: partyId, repId: representativeId },
        }).done(function (data) {
            let result = JSON.parse(data);

            if (result['Success'] === true) {

                if (result['ShowMessage'] === true) {
                    Swal.fire({
                        title: 'کاربـر گـرامی !',
                        html: result['Message'],
                        icon: 'success',
                        customClass: {
                            confirmButton: 'btn btn-success'
                        },
                        confirmButtonText: 'متوجه شدم',
                        buttonsStyling: false
                    });
                }

                $(this).delay(1000).queue(function () {
                    if (parseInt(result['updateType']) === 1) {
                        if (result['returnUrl']) window.location.href = result['returnUrl'];
                    }
                    else {
                        if (result['returnUrl']) {
                            var getUrl = result['returnUrl'];
                            var target = result['updateTargetId'];

                            $.get(getUrl).done(function (newData) {
                                $(document).find(target).html(newData);
                                var modalTarge = $(document).find('#accmodal');
                                modalTarge.modal('hide');
                                $(document).find('.select2').select2();
                            });
                        }
                    }
                })
            }
            else {
                Swal.fire({
                    title: 'کاربـر گـرامی !',
                    html: result['Message'],
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

$(document).on('click', '.ajaxAction', function () {
    loder.style.display = "block";

    let actionUrl = $(this).data('url');

    if (confirm('عملیات موردنظر انجام شود ؟')) {
        $.ajax({
            url: actionUrl,
            type: 'POST'
        }).done(function (data) {
            let result = JSON.parse(data);

            if (result['Success'] === true) {

                if (result['ShowMessage'] === true) {
                    Swal.fire({
                        title: 'کاربـر گـرامی !',
                        html: result['Message'],
                        icon: 'success',
                        customClass: {
                            confirmButton: 'btn btn-success'
                        },
                        confirmButtonText: 'متوجه شدم',
                        buttonsStyling: false
                    });
                }

                $(this).delay(1000).queue(function () {
                    if (parseInt(result['updateType']) === 1) {
                        if (result['returnUrl']) window.location.href = result['returnUrl'];
                    }
                    else {
                        if (result['returnUrl']) {
                            var getUrl = result['returnUrl'];
                            var target = result['updateTargetId'];

                            $.get(getUrl).done(function (newData) {
                                $(document).find(target).html(newData);
                                var modalTarge = $(document).find('#accmodal');
                                modalTarge.modal('hide');
                                $(document).find('.select2').select2();
                            });
                        }
                    }
                })
            }
            else {
                Swal.fire({
                    title: 'کاربـر گـرامی !',
                    html: result['Message'],
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
});

$(document).on('change', '#SelectRows', function () {
    $(this).parents('table').find('input[name="articleRow"]').prop('checked', this.checked);
})


//Utility
$('.move-up').click(function () {
    var row = $(this).closest('tr');
    if (row.prev().length > 0) { // بررسی وجود ردیف قبلی
        row.prev().before(row);
    }
});

// جابجایی به پایین
$('.move-down').click(function () {
    var row = $(this).closest('tr');
    if (row.next().length > 0) { // بررسی وجود ردیف بعدی
        row.next().after(row);
    }
});


