
var CodingPlace = $(document).find('#codingPlace');
var loder = document.getElementById("loader");
$('#sumRowsPlace').hide();
$(document).find('#doclistPlace').hide();

//Show Tafsils Culomns in Doc Page
var tafsils = $(document).find('.hide');
var showTafsils = Cookies.get('tafsilVis');

var showTafsilsCheckbox = $(document).find('#tafsilVis');
showTafsilsCheckbox.prop('checked', showTafsils === 'true');
if (showTafsils === 'true') {
    tafsils.show();
}
else {
    tafsils.hide();
}
//....................................

//....................................

document.addEventListener('dblclick', event => {
    const target = event.target.closest('tr[data-url].accCoding');

    if (target) {
        const actionUrl = target.dataset.url;
        fetch(actionUrl)
            .then(respons => {
                return respons.text();
            })
            .then(data => {
                document.querySelector('#codingPlace').innerHTML = data;
                $(document).find('.select2').select2();
            })
    }
});


document.addEventListener('click', event => {
    const target = event.target.closest('.codingNav[data-url]');
    if (target) {
        const actionUrl = target.dataset.url;
        fetch(actionUrl)
            .then(respons => {
                return respons.text();
            })
            .then(data => {
                document.querySelector('#codingPlace').innerHTML = data;
            })
    }
});



document.addEventListener('click', event => {
    const target = event.target.closest('.callformwhithid[data-url]');
    if (target) {
        const actionUrl = target.dataset.url;
        let Id = target.dataset.docid;
        let docNumber = target.dataset.docnumber;
        let url = actionUrl + "?id=" + Id + '&docNo=' + docNumber;
        fetch(url)
            .then(response => {
                return response.text();
            })
            .then(data => {
                var modalTarget = $(document).find('#accmodal');
                modalTarget.html(data);

                $(document).find('.flatpickr-input').flatpickr({
                    locale: 'fa',
                    altInput: true,
                    altFormat: 'Y/m/d',
                    allowInput: true,
                    disableMobile: true,
                    disableMobile: true
                });
                modalTarget.modal('show');
            })
            .catch(error => {
                console.error('Error:', error);
            });
    }
});
document.addEventListener('click', event => {
    const target = event.target.closest('.callformwhithItemid[data-url]');
    if (target) {
        const actionUrl = target.dataset.url;
        let ItemId = target.dataset.itemid;
        let url = actionUrl + "?Id=" + ItemId;
        fetch(url)
            .then(response => {
                return response.text();
            })
            .then(data => {
                var modalTarget = $(document).find('#accmodal');
                modalTarget.html(data);

                $(document).find('.flatpickr-input').flatpickr({
                    locale: 'fa',
                    altInput: true,
                    altFormat: 'Y/m/d',
                    allowInput: true,
                    disableMobile: true
                });
                modalTarget.modal('show');
            })
            .catch(error => {
                console.error('Error:', error);
            });
    }
});

document.addEventListener('dblclick', event => {
    const target = event.target.closest('.dblc_callform[data-url]');
    if (target) {
        const actionUrl = target.dataset.url;

        fetch(actionUrl)
            .then(respons => {
                return respons.text();
            })
            .then(data => {
                var modalTarget = $(document).find('#accmodal');
                modalTarget.html(data);

                $(document).find('.flatpickr-input').flatpickr({
                    locale: 'fa',
                    altInput: true,
                    altFormat: 'Y/m/d',
                    allowInput: true,
                    disableMobile: true
                });
                modalTarget.modal('show');
                //$(document).find('.select2').select2();
            })
    }
});

$(document).on('click', '.addAccountByAjax', function () {
    var loder = document.getElementById("loader");
    if (loder) loder.style.display = "block";

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

        if (result['Success'] == true) {

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

            if (result['returnUrl']) {
                var getUrl = result['returnUrl'];

                $.get(getUrl).done(function (newData) {
                    $(document).find('#codingPlace').html(newData);
                    var modalTarget = $(document).find('#accmodal');
                    modalTarget.modal('hide');
                    $(document).find('.select2').select2();
                });
            }
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
});

$(document).on('click', '.submitCustomAction', function () {
    loder.style.display = "block";

    let frm = $(this).parents('form');
    let actionUrl = $(this).data('url');
    let dataToSend = new FormData(frm.get(0));

    $.ajax({
        url: actionUrl,
        type: 'post',
        data: dataToSend,
        processData: false,
        contentType: false
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

            $(this).delay(200).queue(function () {
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
});
$(document).on('click', '.accAjaxSubmit', function () {
    loder.style.display = "block";
    let frm = $(this).closest('form');

    /*let frm = $(this).parents('form');*/
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

            $(this).delay(200).queue(function () {
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
});

$(document).on('click', '.accAjaxPost', function () {
    if (loder) loder.style.display = "block";

    let actionUrl = $(this).data('url');
    let Id = $(this).data('target');

    $.ajax({
        url: actionUrl,
        type: 'post',
        data: { id: Id },
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
});

$(document).on('click', '.accAjaxSaveDoc', function () {
    loder.style.display = "block";

    let formid = $(this).data('target');
    let frm = $(document).find(formid);
    let actionUrl = $(this).data('url');
    let dataToSend = new FormData(frm.get(0));

    $.ajax({
        url: actionUrl,
        type: 'post',
        data: dataToSend,
        processData: false,
        contentType: false
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
});

//removeArticle
$(document).on('click', '.removeById', function () {
    loder.style.display = "block";

    let actionUrl = $(this).data('url');
    let Id = $(this).data('target');
    var tr = $(this).closest('tr');
    var confirmDialogText = $(this).attr('title');

    if (confirm(confirmDialogText)) {
        $.ajax({
            url: actionUrl,
            type: 'POST',
            data: { itemId: Id }
        }).done(function (data) {
            let result = JSON.parse(data);

            if (result['Success'] === true) {

                tr.hide(300);
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

document.addEventListener('click', event => {
    const target = event.target.closest('.callmodal[data-url]');
    if (target) {
        const actionUrl = target.dataset.url;

        fetch(actionUrl)
            .then(respons => {
                return respons.text();
            })
            .then(data => {
                var modalTarget = $(document).find('#accmodal');
                modalTarget.html(data);

                $(document).find('.flatpickr-input').flatpickr({
                    locale: 'fa',
                    altInput: true,
                    altFormat: 'Y/m/d',
                    allowInput: true,
                    disableMobile: true
                });
                modalTarget.modal('show');
                //$(document).find('.select2').select2();
            })
    }
});

$(document).on('click', '.renumberRows', function () {
    let actionUrl = $(this).data('url');
    let DocId = $(this).data('docid');

    let tblId = $(this).data('target');
    let tableTr = $(document).find(tblId + ' tr[id]');
    var dataToSend = new Array();

    tableTr.each(function (index, row) {
        dataToSend.push({
            Id: $(this).attr('id'),
            RowNumber: index + 1
        });
    });

    $.ajax({
        url: actionUrl,
        type: 'post',
        data: { jsonData: JSON.stringify(dataToSend), docId: DocId },
        dataType: 'json'
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
        loderr.css('display', 'none');
    });
});

$(document).on('dblclick', '.accountBrowser_item', function () {

    var ldr = $(document).find('#loader');
    ldr.css('display', 'block');
    let place = $(document).find('#accountBrowswePlace');

    let frm = $(this).parents('.card').find('form');
    let actionUrl = $(this).data('url');
    let id = $(this).data('target');
    let longid = $(this).data('longid');
    let formact = frm.attr('action');

    let stDate = frm.find('input[name=strStartDate]').val();
    let edDate = frm.find('input[name=strEndDate]').val();
    let fromNumber = frm.find('input[name=FromDocNumer]').val();
    let toDocNumer = frm.find('input[name=ToDocNumer]').val();
    let DocType = frm.find('input[name=docType]').val();


    $.ajax({
        url: actionUrl,
        type: 'post',
        data: {
            strStartDate: stDate
            , strEndDate: edDate
            , FromDocNumer: fromNumber
            , ToDocNumer: toDocNumer
            , docType: DocType
            , targetId: id
            , longId: longid
        }
    }).done(function (data) {
        place.html(data);
    }).always(function () {
        ldr.css('display', 'none');
    });
});
$(document).on('dblclick', '.accountBrowser_itemLink', function () {
    var ldr = $(document).find('#loader');
    ldr.css('display', 'block');

    let frm = $(this).closest('.card').find('form');
    let actionUrl = $(this).data('url');
    let id = $(this).data('target');

    frm.attr('action', actionUrl);
    frm.find('input[name="filter.targetId"]').val(id);
    frm.submit();
});

$(document).on('dblclick', '.accountBrowser_tafsil', function () {
    var ldr = $(document).find('#loader');
    ldr.css('display', 'block');

    let frm = $(document).find('#frm_AccountBrowser');
    let actionUrl = $(this).data('url');
    let moeinId = $(this).data('moeinid');
    let tafsilId = $(this).data('tafsilid');
    let levelup = true;
    frm.attr('action', actionUrl);
    frm.find('input[name="filter.MoeinId"]').val(moeinId);
    frm.find('input[name="filter.CurrentTafsilId"]').val(tafsilId);
    frm.find('input[name="filter.LevelUp"]').val("true");

    frm.submit();
});


$(document).on('click', '.accountBrowser_nav', function () {

    var ldr = $(document).find('#loader');
    ldr.css('display', 'block');
    let place = $(document).find('#accountBrowswePlace');

    let frm = $(this).parents('.card').find('form');
    let actionUrl = $(this).data('url');
    let id = $(this).data('target');
    let longid = $(this).data('longid');
    let formact = frm.attr('action');

    let stDate = frm.find('input[name=strStartDate]').val();
    let edDate = frm.find('input[name=strEndDate]').val();
    let fromNumber = frm.find('input[name=FromDocNumer]').val();
    let toDocNumer = frm.find('input[name=ToDocNumer]').val();
    let DocType = frm.find('input[name=docType]').val();


    $.ajax({
        url: actionUrl,
        type: 'post',
        data: {
            strStartDate: stDate
            , strEndDate: edDate
            , FromDocNumer: fromNumber
            , ToDocNumer: toDocNumer
            , docType: DocType
            , targetId: id
            , longId: longid
        }
    }).done(function (data) {
        place.html(data);
    }).always(function () {
        ldr.css('display', 'none');
    });
});

$(document).off('dblclick', '.dblc-link').on('dblclick', '.dblc-link', function () {
    var actionUrl = $(this).data('url');
    window.open(actionUrl, '_blank');
});
$('tr[data-url].opentab').on('dblclick', function () {
    var actionUrl = $(this).data('url');
    window.location.href = actionUrl;
    window.open(actionUrl);
});

$(document).on('click', '.submitFormById', function () {
    var selectedForm = document.getElementById("frm_accBrowser");
    selectedForm.submit();
});

//===========================================

//$(document).on('select2:open', '.select2', function () {
//    var nam = $(this).attr('name');
//    var searchField = $(document).find('select[name="'+nam+'"]').find('input[type="search"]');
//    searchField.focus().select();
//});

$(document).on('select2:open', function (e) {
    // پیدا کردن فیلد جستجوی Select2
    var searchField = $(e.target).data('select2').dropdown.$search;
    if (searchField) {
        // تمرکز و انتخاب متن در فیلد جستجو
        searchField.focus().select();
    }
});


//================================================================================================
// Add Article
$(document).on('keypress', '#artRowNumber', function (e) {
    let key = e.which;
    if (key == 13) {
        let archiveCode = $(document).find('#artArchiveCode');
        archiveCode.select();
    }
})
$(document).on('keypress', '#artArchiveCode', function (e) {
    let key = e.which;
    if (key == 13) {
        $(document).find('#frmAddArticle_MoeinId').select2('open');
    }
});
$(document).on('change', '#frmAddArticle_MoeinId[data-url]', function () {

    var tafsilPlcae = $(document).find('#frmAddArticle_TafsilPlace');
    tafsilPlcae.empty();
    var id = $(this).val();
    var actionUrl = $(this).data('url') + "/" + id;
    $.get(actionUrl).done(function (data) {
        tafsilPlcae.html(data);
        $(document).find('.select2').select2();
        setTimeout(function () {
            let tafsil4 = $(document).find('#frmArticle_Tafsil4');
            let tafsil5 = $(document).find('#frmArticle_Tafsil5');
            let tafsil6 = $(document).find('#frmArticle_Tafsil6');
            let tafsil7 = $(document).find('#frmArticle_Tafsil7');
            let tafsil8 = $(document).find('#frmArticle_Tafsil8');
            let articleComment = $(document).find('input[name="Article.Comment"]');
            if (tafsil4.length) {
                tafsil4.select2();
                tafsil4.select2('open');
            }
            else if (tafsil5.length) {
                tafsil5.select2();
                tafsil5.select2('open');
            }
            else if (tafsil6.length) {
                tafsil6.select2();
                tafsil6.select2('open');
            }
            else if (tafsil7.length) {
                tafsil7.select2();
                tafsil7.select2('open');
            }
            else if (tafsil8.length) {
                tafsil8.select2();
                tafsil8.select2('open');
            }
            else {
                articleComment.focus();
                articleComment.select();
            }
        }, 0);

    });
});
$(document).on('change', '#frmArticle_Tafsil4', function () {

    let tafsil5 = $(document).find('#frmArticle_Tafsil5');
    let tafsil6 = $(document).find('#frmArticle_Tafsil6');
    let tafsil7 = $(document).find('#frmArticle_Tafsil7');
    let tafsil8 = $(document).find('#frmArticle_Tafsil8');
    var articleComment = $(document).find('input[name="Article.Comment"]');

    if (tafsil5.length) {
        tafsil5.select2();
        tafsil5.select2('open');
    }
    else if (tafsil6.length) {
        tafsil6.select2();
        tafsil6.select2('open');
    }
    else if (tafsil7.length) {
        tafsil7.select2();
        tafsil7.select2('open');
    }
    else if (tafsil8.length) {
        tafsil8.select2();
        tafsil8.select2('open');
    }
    else {
        setTimeout(function () {
            articleComment.focus();
            articleComment.select();
        }, 100);
    }
});
$(document).on('change', '#frmArticle_Tafsil5', function () {

    let tafsil6 = $(document).find('#frmArticle_Tafsil6');
    let tafsil7 = $(document).find('#frmArticle_Tafsil7');
    let tafsil8 = $(document).find('#frmArticle_Tafsil8');
    var articleComment = $(document).find('input[name="Article.Comment"]');

    if (tafsil6.length) {
        tafsil6.select2();
        tafsil6.select2('open');
    }
    else if (tafsil7.length) {
        tafsil7.select2();
        tafsil7.select2('open');
    }
    else if (tafsil8.length) {
        tafsil8.select2();
        tafsil8.select2('open');
    }
    else {
        setTimeout(function () {
            articleComment.focus();
            articleComment.select();
        }, 100);
    }
});
$(document).on('change', 'select[name="Article.Tafsil6Id"]', function () {

    let tafsil7 = $(document).find('#frmArticle_Tafsil7');
    let tafsil8 = $(document).find('#frmArticle_Tafsil8');
    var articleComment = $(document).find('input[name="Article.Comment"]');
    if (tafsil7.length) {
        tafsil7.select2();
        tafsil7.select2('open');
    }
    else if (tafsil8.length) {
        tafsil8.select2();
        tafsil8.select2('open');
    }
    else {
        setTimeout(function () {
            articleComment.focus();
            articleComment.select();
        }, 100);
    }
});
$(document).on('change', 'select[name="Article.Tafsil7Id"]', function () {

    let tafsil8 = $(document).find('#frmArticle_Tafsil8');
    let articleComment = $(document).find('input[name="Article.Comment"]');

    if (tafsil8.length) {
        tafsil8.select2();
        tafsil8.select2('open');
    }
    else {
        setTimeout(function () {
            articleComment.focus();
            articleComment.select();
        }, 100);
    }
});
$(document).on('change', 'select[name="Article.Tafsil8Id"]', function () {

    var articleComment = $(document).find('input[name="Article.Comment"]');
    articleComment.select();
});
$(document).on('keypress', '#artDesc', function (e) {

    let key = e.which;
    if (key == 13) {
        let bed = $(document).find('input[name="Article.strBed"]');
        bed.select();
    }
});
$(document).on('keypress', 'input[name="Article.strBed"]', function (e) {

    let key = e.which;
    if (key == 13) {
        let bes = $(document).find('input[name="Article.strBes"]');
        bes.select();
    }
});
$(document).on('keypress', 'input[name="Article.strBes"]', function (e) {

    let key = e.which;
    if (key == 13) {
        let btnAdd = $(document).find('#addArt');
        btnAdd.focus();
        btnAdd.select();
    }
});


//================================================================================================
// Edit Article 
$(document).on('change', '#frmEditArticle_MoeinId[data-url]', function () {
    var tafsilPlace = $(document).find('#frmEditArticle_TafsilPlace');
    tafsilPlace.empty();
    var id = $(this).val();
    var actionUrl = $(this).data('url') + "/" + id;

    $.get(actionUrl).done(function (data) {
        tafsilPlace.html(data);
        setTimeout(function () {
            var e_tafsil4 = $(document).find('#frmEdit_Tafsil4');
            let e_tafsil5 = $(document).find('#frmEdit_Tafsil5');
            let tafsil6 = $(document).find('#frmEdit_Tafsil6');
            let tafsil7 = $(document).find('#frmEdit_Tafsil7');
            let tafsil8 = $(document).find('#frmEdit_Tafsil8');
            let articleComment = $(document).find('#frmEdit_artDesc');
            if (e_tafsil4.length) {
                e_tafsil4.select2('open');
            }
            else if (e_tafsil5.length) {
                e_tafsil5.select2('open');
            }
            else if (tafsil6.length) {
                tafsil6.select2('open');
            }
            else if (tafsil7.length) {
                tafsil7.select2('open');
            }
            else if (tafsil8.length) {
                tafsil8.select2('open');
            }
            else {
                articleComment.select();
            }
        }, 200);
    });
});
$(document).on('change', '#frmEdit_Tafsil4', function () {
    let e_tafsil5 = $(document).find('#frmEdit_Tafsil5');
    let tafsil6 = $(document).find('#frmEdit_Tafsil6');
    let tafsil7 = $(document).find('#frmEdit_Tafsil7');
    let tafsil8 = $(document).find('#frmEdit_Tafsil8');
    let articleComment = $(document).find('#frmEdit_artDesc');

    if (e_tafsil5.length) {
        e_tafsil5.select2('open');
    }
    else if (tafsil6.length) {
        tafsil6.select2('open');
    }
    else if (tafsil7.length) {
        tafsil7.select2('open');
    }
    else if (tafsil8.length) {
        tafsil8.select2('open');
    }
    else {
        setTimeout(function () {
            articleComment.select();
        }, 100);
    }
});
$(document).on('change', '#frmEdit_Tafsil5', function () {

    let tafsil6 = $(document).find('#frmEdit_Tafsil6');
    let tafsil7 = $(document).find('#frmEdit_Tafsil7');
    let tafsil8 = $(document).find('#frmEdit_Tafsil8');
    let articleComment = $(document).find('#frmEdit_artDesc');
    if (tafsil6.length) {
        tafsil6.select2('open');
    }
    else if (tafsil7.length) {
        tafsil7.select2('open');
    }
    else if (tafsil8.length) {
        tafsil8.select2('open');
    }
    else {
        setTimeout(function () {
            articleComment.select();
        }, 100);
    }
});
$(document).on('change', '#frmEdit_Tafsil6', function () {

    let tafsil7 = $(document).find('#frmEdit_Tafsil7');
    let tafsil8 = $(document).find('#frmEdit_Tafsil8');
    let articleComment = $(document).find('#frmEdit_artDesc');

    if (tafsil7.length) {
        tafsil7.select2('open');
    }
    else if (tafsil8.length) {
        tafsil8.select2('open');
    }
    else {
        setTimeout(function () {
            articleComment.select();
        }, 100);
    }
});
$(document).on('change', '#frmEdit_Tafsil7', function () {

    let tafsil8 = $(document).find('#frmEdit_Tafsil8');
    let articleComment = $(document).find('#frmEdit_artDesc');
    if (tafsil8.length) {
        tafsil8.select2('open');
    }
    else {
        setTimeout(function () {
            articleComment.select();
        }, 100);
    }
});
$(document).on('change', '#frmEdit_Tafsil8', function () {

    let articleComment = $(document).find('#frmEdit_artDesc');
    articleComment.select();
});

$(document).on('change', 'input[type="number"]', function () {
    var inputValue = $(this).val();
    if (!inputValue)
        $(this).val(0);
});
$(document).on('keypress', '.gonext', function (e) {
    if (e.which === 13) {  // Enter key
        e.preventDefault();
        var inputs = $(this).parents('form').find(':input:visible');
        var idx = inputs.index(this);

        while (idx < inputs.length - 1) {
            idx++;  // حرکت به کنترل بعدی
            var nextInput = inputs[idx];

            // چک کردن اینکه کنترل بعدی readonly نباشد
            if (!$(nextInput).prop('readonly')) {
                $(nextInput).focus().select();
                break; // خروج از حلقه در صورت یافتن کنترل بدون readonly
            }
        }

        // اگر به آخرین کنترل رسید، دکمه ثبت را فشار دهد
        if (idx === inputs.length - 1) {
            $('.accAjaxSubmit').click();
        }
    }
});


//=======================================================================================
$('#accmodal').on('shown.bs.modal', function () {

    $(this).find('.firstSelect').focus();
    $(this).find('.firstSelect').select();
});
$(document).on('load', function () {

    $(this).find('.firstSelect').focus();
    $(this).find('.firstSelect').select();
});
$('#model-general-larg').on('shown.bs.modal', function () {

    $(this).find('.firstSelect').focus();
    $(this).find('.firstSelect').select();
});

$(document).on('change', 'input[name="articleRow"]', function () {
    var place = $('#sumRowsPlace');
    var totalBed = 0;
    var totalBes = 0;

    var checkedRows = $('input[name="articleRow"]:checked');
    if (checkedRows.length > 1) {
        if (!place.hasClass('open')) {
            place.slideDown(300, function () {
                place.addClass('open');
            });
        }
        checkedRows.each(function () {
            var row = $(this).closest('tr');
            var bed = parseFloat(row.data('deb')) || 0;
            var bes = parseFloat(row.data('bes')) || 0;

            totalBed += bed;
            totalBes += bes;
        });
    } else {
        if (place.hasClass('open')) {
            place.slideUp(300, function () {
                place.removeClass('open');
            });
        }
    }
    var mande = totalBed - totalBes;
    $('#totalBed').text(totalBed.toLocaleString());
    $('#totalBes').text(totalBes.toLocaleString());
    $('#ekhtelaf').text(mande.toLocaleString());
});

$(document).on('input', '.numberInput', function () {
    var inputVal = $(this).val();
    // حذف هر کاراکتر غیر عددی
    inputVal = inputVal.replace(/[^\d]/g, '');
    // اضافه کردن جداکننده‌های هزارگان
    var formattedInputVal = inputVal.replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    $(this).val(formattedInputVal);
});
$(document).on('change', '.numberInput', function () {
    var inputVal = $(this).val();
    if (inputVal == null || inputVal == '')
        $(this).val(0);
});

$(document).on('click', '#docSearchToggle', function () {
    $('#doclistPlace').toggle(500);
})


$(document).on('click', '.bulkAction', function () {
    loder.style.display = "block";

    let actionUrl = $(this).data('url');
    let allerText = $(this).data('allerttext');
    let tblId = $(this).data('tableid');
    var tbl = $(document).find('#' + tblId);
    var dataToSend = [];

    tbl.find('.tblRow:checked').each(function () {
        dataToSend.push($(this).val());
    })
    if (dataToSend.length == 0) {
        loder.style.display = "none";
        return;
    }
    if (confirm(allerText)) {
        $.ajax({
            url: actionUrl,
            type: 'post',
            data: { items: dataToSend },
        }).done(function (response) {
            let result = JSON.parse(response);

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
$(document).on('click', '.bulkAction-callmodal', function () {
    loder.style.display = "block";

    let actionUrl = $(this).data('url');
    let tblId = $(this).data('tableid');
    var tbl = $(this).parents('.card').find('#' + tblId);
    var dataToSend = [];
    tbl.find('.tblRow:checked').each(function () {
        dataToSend.push($(this).val());
    })
    if (dataToSend.length == 0) {
        loder.style.display = "none";
        return;
    }
    $.ajax({
        url: actionUrl,
        type: 'post',
        data: { items: dataToSend },
    }).done(function (response) {
        var modalTarget = $(document).find('#accmodal');
        modalTarget.html(response);

        $(document).find('.flatpickr-input').flatpickr({
            locale: 'fa',
            altInput: true,
            altFormat: 'Y/m/d',
            allowInput: true,
        });
        modalTarget.modal('show');
    }).always(function () {
        loder.style.display = "none";
    });

});
$(document).on('click', '.bulkActionformdata-callmodal', function () {
    

    let actionUrl = $(this).data('url');
    let tblId = $(this).data('tableid');
    let tbl = $(this).parents('.card').find('#' + tblId);

    let formId = $(this).data('formid');
    let frm = $(document).find('#' + formId);
    var tType = frm.find('select[name="filter.Transactiontype"]').val();
    var bankAccount = frm.find('select[name="filter.AccountId"]').val();

    let dataToSend = [];
    tbl.find('.tblRow:checked').each(function () {
        dataToSend.push($(this).val());
    });

    if (dataToSend.length === 0) {
        alert("ابتدا یک یا چند ردیف را انتخاب کنید.");
        return;
    }
    loder.style.display = "block";

    $.ajax({
        url: actionUrl,
        type: 'POST',
        data: {
            items: dataToSend,
            transactionsType: tType,
            BankId: bankAccount
        },

    }).done(function (response) {
        let modalTarget = $(document).find('#accmodal');
        modalTarget.empty();
        modalTarget.html(response);

        $(document).find('.flatpickr-input').flatpickr({
            locale: 'fa',
            altInput: true,
            altFormat: 'Y/m/d',
            allowInput: true,
        });

        modalTarget.modal('show');
    }).fail(function () {
        alert("خطایی رخ داده است. لطفاً دوباره تلاش کنید.");
    }).always(function () {
        loder.style.display = "none";
    });
});



$(document).on('click', '.bulk-callAction', function () {
    let actionUrl = $(this).data('url');
    let tblId = $(this).data('tableid');
    var tbl = $(this).parents('.card').find('#' + tblId);
    var dataToSend = [];

    tbl.find('.tblRow:checked').each(function () {
        dataToSend.push($(this).val());
    });

    if (dataToSend.length === 0) {
        return;
    }
    // var jsonData = JSON.stringify(dataToSend);
    $.ajax({
        url: actionUrl,
        type: 'POST',
        data: { ids: dataToSend },

        success: function (response) {
            var newWindow = window.open();
            newWindow.document.write(response);
            newWindow.document.close();
        },
        error: function (xhr, status, error) {
            console.error('An error occurred:', error);
        }
    });
});

$('.bulk-DocPrint').click(function () {
    let actionUrl = $(this).data('url');
    let tblId = $(this).data('tableid');
    var tbl = $(this).parents('.card').find('#' + tblId);
    var dataToSend = [];

    tbl.find('.tblRow:checked').each(function () {
        dataToSend.push($(this).val());
    });

    if (dataToSend.length === 0) {
        return;

    }
    // var jsonData = JSON.stringify(dataToSend);
    $.ajax({
        url: actionUrl,
        type: 'post',
        data: { ids: dataToSend },

        success: function (response) {
            var modalTarget = $(document).find('#accmodal');
            modalTarget.html(response);
            modalTarget.modal('show');
        },
        error: function (xhr, status, error) {
            console.error('An error occurred:', error);
        }
    });
});

$(document).on('change', '.electRows', function () {
    var isChecked = this.checked;
    $(document).find('input[name="docId"]').prop('checked', isChecked);

    if (isChecked) {
        $(document).find('#select-all-label').text('عدم انتخاب اسناد');
    } else {
        $(document).find('#select-all-label').text('انتخاب همه اسناد');
    }
});
$(document).on('change', '.selectAllRows', function () {
    var isChecked = this.checked;
    $(this).parents('table').find('input[name="itemId"]').prop('checked', isChecked);
});

$(document).on('click', '.callAction', function () {
    var frm = $(this).parents('form');
    var actionUrl = frm.attr('action');
    var rowsCount = frm.find('input[name=RowsCount]').val();
    var url = actionUrl + '?RowsCount=' + rowsCount;
    window.open(url, '_blank');
})
$(document).on('click', '.pringDoc', function () {
    var frm = $(this).closest('form'); // یافتن فرم مرتبط
    var actionUrl = frm.attr('action');
    var DocId = frm.find('input[name="id"]').val();
    var level = frm.find('input[name="level"]:checked').val();
    var url = actionUrl + '?id=' + DocId + '&level=' + level;

    window.open(url, '_blank');
});

$(document).on('change', '#selectTblRows', function () {
    var isChecked = this.checked;
    $(document).find('input[name="tableRow"]').prop('checked', isChecked);
});

$(document).on('change', '.mdsa', function () {

    let frm = $(this).parents('form');
    let actionUrl = $(this).data('url');
    let targetId = $(this).data('target');
    let target = $(document).find(targetId);
    let dataToSend = new FormData(frm.get(0));
    $.ajax({
        url: actionUrl,
        type: 'post',
        data: dataToSend,
        processData: false,
        contentType: false
    }).done(function (data) {
        target.html(data);
    }).always(function () {
        loder.style.display = "none";
    });
});
$(document).on('click', '.AjaxGetData', function () {
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

            $(this).delay(200).queue(function () {
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
});

// Account Browser Navigation
$(document).on('click', '.accountBrowserNav_tafsil', function () {
    var formId = '#' + $(this).data('formid');
    var actionUrl = $(this).data('url');
    var rpLevel = $(this).data('reportlevel');
    var selectedForm = $(document).find(formId);
    selectedForm.attr('action', actionUrl);
    selectedForm.find('input[name="filter.ReportLevel"]').val(rpLevel);

    selectedForm.submit();
});

$(document).on('change', '.callDocWithNumber', function () {
    let docNumber = $(this).val();
    let actionUrl = "/OpAccoucnting/DocViaNumber"
    var url = actionUrl + '?number=' + docNumber;
    window.open(url, '_blank');
})

$(document).on('click', '.submitCustomForm', function () {

    var formId = '#' + $(this).data('formid');
    var actionUrl = $(this).data('url');
    var selectedForm = $(document).find(formId);
    selectedForm.attr('action', actionUrl);
    selectedForm.submit();
    loder.style.display = "none";

});
$(document).on('click', '.getcustomerInvoices', function () {
    var formId = '#' + $(this).data('formid');
    var partyId = $(this).data('partyid');
    var actionUrl = $(this).data('url');
    var selectedForm = $(document).find(formId);
    selectedForm.find('select[name="filter.Party"]').val(partyId);
    selectedForm.attr('action', actionUrl);
    selectedForm.submit();
    loder.style.display = "none";

});



$('#tafsilVis').change(function () {
    var isChecked = this.checked;
    var tafsilEl = $(this).parents('.card').find('.hide');
    Cookies.set('tafsilVis', isChecked, { expires: 7 });
    if (isChecked) {
        tafsilEl.show();
    }
    else {
        tafsilEl.hide();
    }
});


$(document).on('click', '.getprint', function () {

    loder.style.display = "block";
    let frm = $(this).parents('form');
    let actionUrl = $(this).data('url');

    let fromDoc = frm.find('input[name="filter.FromDocNumer"]').val();
    let toDoc = frm.find('input[name="filter.ToDocNumer"]').val();
    let startDate = frm.find('input[name="filter.strStartDate"]').val();
    let endDate = frm.find('input[name="filter.strEndDate"]').val();
    let doctype = frm.find('select[name="filter.docType"]').val();
    let reportLevel = frm.find('input[name="filter.ReportLevel"]:checked').val();
    let balanceColumnsQty = frm.find('input[name="filter.BalanceColumnsQty"]:checked').val();

    $.ajax({
        url: actionUrl,
        type: 'get',
        data: {
            FromDocNumer: fromDoc
            , ToDocNumer: toDoc
            , strStartDate: startDate
            , strEndDate: endDate
            , docType: doctype
            , ReportLevel: reportLevel
            , BalanceColumnsQty: balanceColumnsQty
        }
    }).done(function (data) {
        var newWindow = window.open();
        newWindow.document.write(data);
        newWindow.document.close();
    }).always(function () {
        loder.style.display = "none";
    });
});

$(document).on('click', '.getBrowserprint', function () {

    loder.style.display = "block";
    let frm = $(this).parents('form');
    let actionUrl = $(this).data('url');

    let fromDoc = frm.find('input[name="filter.FromDocNumer"]').val();
    let toDoc = frm.find('input[name="filter.ToDocNumer"]').val();
    let startDate = frm.find('input[name="filter.strStartDate"]').val();
    let endDate = frm.find('input[name="filter.strEndDate"]').val();
    let doctype = frm.find('select[name="filter.docType"]').val();

    let kolId = frm.find('input[name="filter.KolId"]').val();
    let moeinId = frm.find('input[name="filter.MoeinId"]').val();
    let currentTafsilId = frm.find('input[name="filter.CurrentTafsilId"]').val();
    let tafsil4 = frm.find('input[name="filter.tafsil4Id"]').val();
    let tafsil5 = frm.find('input[name="filter.tafsil5Id"]').val();


    let reportLevel = frm.find('input[name="filter.ReportLevel"]').val();
    let balanceColumnsQty = frm.find('input[name="filter.BalanceColumnsQty"]:checked').val();

    $.ajax({
        url: actionUrl,
        type: 'get',
        data: {
            FromDocNumer: fromDoc
            , ToDocNumer: toDoc
            , strStartDate: startDate
            , strEndDate: endDate
            , docType: doctype
            , KolId : kolId
            , MoeinId: moeinId
            , CurrentTafsilId: currentTafsilId
            , tafsil4Id: tafsil4
            , tafsil5Id: tafsil4
            , ReportLevel: reportLevel
            , BalanceColumnsQty: balanceColumnsQty
        }
    }).done(function (data) {
        var newWindow = window.open();
        newWindow.document.write(data);
        newWindow.document.close();
    }).always(function () {
        loder.style.display = "none";
    });
});
$(document).on('click', '.tafsilPrint', function () {

    // ploder.style.display = "block";
    let frm = $(this).parents('form');
    let actionUrl = $(this).data('url');

    let groups = $('#selectTafsilGroup').val();
    let tafsils = $('#selectTafsil').val();
    let tafsils5 = $('#selectTafsil5').val();
    let kols = $('#selectKol').val();
    let moeins = $('#selectMoein').val();
    let project = $('#selectProject').val();
    let fromDate = $('#tafsil_fromDate').val();
    let toDate = $('#tafsil_toDate').val();
    let onlyHasValue = $('#onlyHasValue').is(':checked');
    let reportLvl = $('input[name="TafsilReport.filter.ReportLevel"]:checked').val();

    $.ajax({
        url: actionUrl,
        type: 'get',
        traditional: true,
        data: {
            TafsilGroup: groups
            , Tafsil4Ids: tafsils
            , Tafsil5Ids: tafsils5
            , Kols: kols
            , Moeins: moeins
            , ProjectId: project
            , strStartDate: fromDate
            , strEndDate: toDate
            , ShowJustWithValue: onlyHasValue
            , ReportLevel:reportLvl
        }
    }).done(function (data) {
        var newWindow = window.open();
        newWindow.document.write(data);
        newWindow.document.close();
    }).always(function () {
        ploder.style.display = "none";
    });
});

$(document).on('click', '.printArticles', function () {

    // ploder.style.display = "block";
    let frm = $(this).parents('form');
    let actionUrl = $(this).data('url');

    let tafsils = $('#selectTafsil').val();
    let tafsils5 = $('#selectTafsil5').val();
    let moeins = $('#selectMoein').val();
    //let project = $('#selectProject').val();
    let fromDate = $('#tafsil_fromDate').val();
    let toDate = $('#tafsil_toDate').val();

    $.ajax({
        url: actionUrl,
        type: 'get',
        traditional: true,
        data: {
             Tafsil4Ids: tafsils
            , Tafsil5Ids: tafsils5
            , Moeins: moeins
            , strStartDate: fromDate
            , strEndDate: toDate
        }
    }).done(function (data) {
        var newWindow = window.open();
        newWindow.document.write(data);
        newWindow.document.close();
    }).always(function () {
        ploder.style.display = "none";
    });
});

// print browser Accounts
$(document).on('click', '.printBrowser', function () {

    // ploder.style.display = "block";
    let frm = $(this).parents('form');
    let actionUrl = $(this).data('url');

    let kolId = $('#ba-kolId').val();
    let moeinId = $('#ba_moeinId').val();
    let reportLevel = $('#ba-level').val();
    let fromDoc = $('#ba-fromDoc').val();
    let ToDoc = $('#ba-toDoc').val();
    let fromDate = $('#ba-fromDate').val();
    let toDate = $('#ba-toDate').val();

    $.ajax({
        url: actionUrl,
        type: 'get',
        traditional: true,
        data: {
            Tafsil4Ids: tafsils
            , Tafsil5Ids: tafsils5
            , Moeins: moeins
            , strStartDate: fromDate
            , strEndDate: toDate
        }
    }).done(function (data) {
        var newWindow = window.open();
        newWindow.document.write(data);
        newWindow.document.close();
    }).always(function () {
        ploder.style.display = "none";
    });
});
//==============================================

$(document).on('click', '.delByAjax', function () {

    loder.style.display = "block";
    let actionUrl = $(this).data('url');
    let Id = $(this).data('target');
    var confirmDialogText = $(this).attr('title');

    if (confirm(confirmDialogText)) {

        $.ajax({
            url: actionUrl,
            type: 'post',
            data: { id: Id },
        }).done(function (jsonResult) {
            let result = JSON.parse(jsonResult);

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

                $(this).delay(200).queue(function () {
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


$(document).on('click', '.callaction-bulk', function () {
    let actionUrl = $(this).data('url');
    let allerText = $(this).data('allerttext');
    let tblId = $(this).data('tableid');
    var tbl = $(document).find('#' + tblId);
    var dataToSend = [];

    tbl.find('.tblRow:checked').each(function () {
        dataToSend.push($(this).val());
    });

    if (dataToSend.length == 0) {
        return;
    }

    if (confirm(allerText)) {
        // ایجاد یک فرم موقت برای ارسال داده‌ها به اکشن
        let form = $('<form>', {
            method: 'POST',
            action: actionUrl
        });

        dataToSend.forEach(id => {
            form.append($('<input>', {
                type: 'hidden',
                name: 'items',
                value: id
            }));
        });

        // افزودن فرم به صفحه و ارسال آن
        $('body').append(form);
        form.submit();
    }
});

$(document).on('click', '.callform[data-url]', function () {

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
    });
});

//Sale 
$('input[name="itemId"]').change(function () {

    const place = $('#sumRowPlace');
    let totalTaxable = 0;
    let totalNoTaxable = 0;
    let totalVat = 0;
    let totalFinal = 0;
    let totalDiscount = 0;
    let totalAfterDiscount = 0;

    var checkedRows = $('input[name="itemId"]:checked');
   
    if (checkedRows.length > 1) {
        if (!place.hasClass('open')) {
            place.slideDown(300, function () {
                place.addClass('open');
            });
        }
        checkedRows.each(function () {
            var row = $(this).closest('tr');
            var taxable = parseFloat(row.data('tax')) || 0;
            var noTaxable = parseFloat(row.data('notax')) || 0;
            var discount = parseFloat(row.data('dis')) || 0;
            var vat = parseFloat(row.data('vat')) || 0;
            var finalPrice = parseFloat(row.data('due')) || 0;
            var afterDescount = parseFloat(row.data('afdis')) || 0;

            totalTaxable += taxable;
            totalNoTaxable += noTaxable;
            totalDiscount += discount;
            totalVat += vat;
            totalFinal += finalPrice;
            totalAfterDiscount += afterDescount;

        });
    } else {
        if (place.hasClass('open')) {
            place.slideUp(300, function () {
                place.removeClass('open');
            });
        }
    }
    $('#taxable').html(totalTaxable.toLocaleString());
    $('#noTaxable').html(totalNoTaxable.toLocaleString());
    $('#discount').html(totalDiscount.toLocaleString());
    $('#vat').html(totalVat.toLocaleString());
    $('#finalPrice').html(totalFinal.toLocaleString());
    $('#aftardiscount').html(totalAfterDiscount.toLocaleString());
});                                                                             

$('.fillMoeinSelect').change(function () {
    const actionUrl = $(this).data('url');
    const targetId = $(this).data('filltarget');
    const target = $(targetId);
    const dataToSend = $(this).val();

    $.ajax({
        url: actionUrl,
        type: 'POST',
        data: { items: dataToSend },
        dataType: 'json',
    }).done(function (response) {
        target.empty();
        $.each(response, function (index, lst) {
            target.append($('<option>', {
                value: lst.id,
                text: lst.moeinName
            }));
        });
    }).fail(function (xhr, status, error) {
        console.error('درخواست با خطا مواجه شد:', error);
    });
});

$("#addArtBed").change(function () {
    let val = parseFloat($(this).val());
    if (val > 0) {
        $("#addArtBes").val("0");
    }

});
$("#addArtBes").change(function () {
    let val = parseFloat($(this).val());
    if (val > 0) {
        $("#addArtBed").val("0");
    }

});

$('#delArts').click(function () {
    loder.style.display = "block";

    let actionUrl = $(this).data('url');
    let allerText = $(this).data('allerttext');
    let tblId = $(this).data('tableid');
    var tbl = $(document).find('#' + tblId);
    var dataToSend = [];

    tbl.find('.tblRow:checked').each(function () {
        dataToSend.push($(this).val());
    })
    if (dataToSend.length == 0) {
        loder.style.display = "none";
        return;
    }
    if (confirm(allerText)) {
        $.ajax({
            url: actionUrl,
            type: 'post',
            data: { items: dataToSend },
        }).done(function (response) {
            let result = JSON.parse(response);

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