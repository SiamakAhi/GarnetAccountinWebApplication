
var CodingPlace = $(document).find('#codingPlace');
var loder = document.getElementById("loader");
$('#sumRowsPlace').hide();
$(document).find('#doclistPlace').hide();

document.addEventListener('dblclick', async event => {
    const target = event.target.closest('tr[data-url].accCoding');

    if (target) {
        const actionUrl = target.dataset.url;

        try {
            const response = await fetch(actionUrl);
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const data = await response.text();
            document.querySelector('#codingPlace').innerHTML = data;
            $(document).find('.select2').select2();
        } catch (error) {
            console.error('There was a problem with the fetch operation:', error);
        }
    }
});


document.addEventListener('click', async event => {
    const target = event.target.closest('.codingNav[data-url]');
    if (target) {
        const actionUrl = target.dataset.url;

        try {
            const response = await fetch(actionUrl);
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const data = await response.text();
            document.querySelector('#codingPlace').innerHTML = data;
        } catch (error) {
            console.error('There was a problem with the fetch operation:', error);
        }
    }
});


document.addEventListener('click', async event => {
    const target = event.target.closest('.callform[data-url]');
    if (target) {
        const actionUrl = target.dataset.url;

        try {
            const response = await fetch(actionUrl);
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const data = await response.text();
            let modalTarget = document.querySelector('#accmodal');
            modalTarget.innerHTML = data;
            $(modalTarget).modal('show');
            // Ensure flatpickr is initialized only once
            $(modalTarget).find('.flatpickr-input').flatpickr({
                locale: 'fa',
                altInput: true,
                altFormat: 'Y/m/d',
                allowInput: true,
            });
           
            $('.select2').select2();
        } catch (error) {
            console.error('There was a problem with the fetch operation:', error);
        }
    }
});

document.addEventListener('dblclick', async event => {
    const target = event.target.closest('.dblc_callform[data-url]');
    if (target) {
        const actionUrl = target.dataset.url;

        try {
            const response = await fetch(actionUrl);
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const data = await response.text();
            const modalTarget = document.querySelector('#accmodal');
            modalTarget.innerHTML = data;

            // Ensure flatpickr is initialized only once
            $(modalTarget).find('.flatpickr-input').flatpickr({
                locale: 'fa',
                altInput: true,
                altFormat: 'Y/m/d',
                allowInput: true,
            });

            $(modalTarget).modal('show');
        } catch (error) {
            console.error('There was a problem with the fetch operation:', error);
        }
    }
});


$(document).on('click', '.addAccountByAjax', async function (event) {
    event.preventDefault(); // جلوگیری از رفتار پیش‌فرض فرم

    const loader = document.getElementById("loader");
    if (loader) loader.style.display = "block";

    const frm = $(this).closest('form');
    const actionUrl = frm.attr('action');
    const dataToSend = new FormData(frm[0]);

    try {
        const response = await $.ajax({
            url: actionUrl,
            type: 'post',
            data: dataToSend,
            processData: false,
            contentType: false
        });

        const result = JSON.parse(response);

        if (result['Success']) {
            if (result['ShowMessage']) {
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
                const newData = await $.get(result['returnUrl']);
                $('#codingPlace').html(newData);
                const modalTarget = $('#accmodal');
                modalTarget.modal('hide');
                $('.select2').select2();
            }
        } else {
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
    } catch (error) {
        console.error('There was an error with the AJAX request:', error);
        Swal.fire({
            title: 'خطا!',
            html: 'در ارسال درخواست مشکلی پیش آمده است.',
            icon: 'error',
            customClass: {
                confirmButton: 'btn btn-danger'
            },
            confirmButtonText: 'باشه',
            buttonsStyling: false
        });
    } finally {
        if (loader) loader.style.display = "none";
    }
});

$(document).on('click', '.submitCustomAction', async function (event) {
    event.preventDefault(); // جلوگیری از رفتار پیش‌فرض

    const loader = document.getElementById("loader");
    if (loader) loader.style.display = "block";

    const frm = $(this).closest('form');
    const actionUrl = $(this).data('url');
    const dataToSend = new FormData(frm[0]);

    try {
        const response = await $.ajax({
            url: actionUrl,
            type: 'post',
            data: dataToSend,
            processData: false,
            contentType: false
        });

        const result = JSON.parse(response);

        if (result['Success']) {
            if (result['ShowMessage']) {
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

            // استفاده از `setTimeout` به جای `delay` برای تأخیر
            setTimeout(() => {
                if (parseInt(result['updateType']) === 1) {
                    if (result['returnUrl']) window.location.href = result['returnUrl'];
                } else {
                    if (result['returnUrl']) {
                        const getUrl = result['returnUrl'];
                        const target = result['updateTargetId'];

                        $.get(getUrl).done(newData => {
                            $(target).html(newData);
                            $('#accmodal').modal('hide');
                            $('.select2').select2();
                        });
                    }
                }
            }, 200);
        } else {
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
    } catch (error) {
        console.error('There was an error with the AJAX request:', error);
        Swal.fire({
            title: 'خطا!',
            html: 'در ارسال درخواست مشکلی پیش آمده است.',
            icon: 'error',
            customClass: {
                confirmButton: 'btn btn-danger'
            },
            confirmButtonText: 'باشه',
            buttonsStyling: false
        });
    } finally {
        if (loader) loader.style.display = "none";
    }
});

$(document).on('click', '.accAjaxSubmit', async function (event) {
    event.preventDefault(); // جلوگیری از رفتار پیش‌فرض

    const loader = document.getElementById("loader");
    if (loader) loader.style.display = "block";

    const frm = $(this).closest('form');
    const actionUrl = frm.attr('action');
    const dataToSend = new FormData(frm[0]);

    try {
        const response = await $.ajax({
            url: actionUrl,
            type: 'post',
            data: dataToSend,
            processData: false,
            contentType: false
        });

        const result = JSON.parse(response);

        if (result['Success']) {
            if (result['ShowMessage']) {
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

            // استفاده از `setTimeout` به جای `delay` برای تأخیر
            setTimeout(() => {
                if (parseInt(result['updateType']) === 1) {
                    if (result['returnUrl']) window.location.href = result['returnUrl'];
                } else {
                    if (result['returnUrl']) {
                        const getUrl = result['returnUrl'];
                        const target = result['updateTargetId'];

                        $.get(getUrl).done(newData => {
                            $(target).html(newData);
                            $('#accmodal').modal('hide');
                            $('.select2').select2();
                        });
                    }
                }
            }, 200);
        } else {
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
    } catch (error) {
        console.error('There was an error with the AJAX request:', error);
        Swal.fire({
            title: 'خطا!',
            html: 'در ارسال درخواست مشکلی پیش آمده است.',
            icon: 'error',
            customClass: {
                confirmButton: 'btn btn-danger'
            },
            confirmButtonText: 'باشه',
            buttonsStyling: false
        });
    } finally {
        if (loader) loader.style.display = "none";
    }
});


$(document).on('click', '.accAjaxPost', async function (event) {
    event.preventDefault(); // جلوگیری از رفتار پیش‌فرض اگر دکمه باشد

    const loader = document.getElementById("loader");
    if (loader) loader.style.display = "block";

    const actionUrl = $(this).data('url');
    const id = $(this).data('target');

    try {
        const response = await $.ajax({
            url: actionUrl,
            type: 'post',
            data: { id: id }
        });

        const result = JSON.parse(response);

        if (result['Success']) {
            if (result['ShowMessage']) {
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

            setTimeout(() => {
                if (parseInt(result['updateType']) === 1) {
                    if (result['returnUrl']) window.location.href = result['returnUrl'];
                } else {
                    if (result['returnUrl']) {
                        const getUrl = result['returnUrl'];
                        const target = result['updateTargetId'];

                        $.get(getUrl).done(newData => {
                            $(target).html(newData);
                            $('#accmodal').modal('hide');
                            $('.select2').select2();
                        });
                    }
                }
            }, 1000);
        } else {
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
    } catch (error) {
        console.error('There was an error with the AJAX request:', error);
        Swal.fire({
            title: 'خطا!',
            html: 'در ارسال درخواست مشکلی پیش آمده است.',
            icon: 'error',
            customClass: {
                confirmButton: 'btn btn-danger'
            },
            confirmButtonText: 'باشه',
            buttonsStyling: false
        });
    } finally {
        if (loader) loader.style.display = "none";
    }
});


$(document).on('click', '.accAjaxSaveDoc', async function (event) {
    event.preventDefault(); // جلوگیری از رفتار پیش‌فرض اگر دکمه باشد

    const loader = document.getElementById("loader");
    if (loader) loader.style.display = "block";

    const formId = $(this).data('target');
    const frm = $(formId);
    const actionUrl = $(this).data('url');
    const dataToSend = new FormData(frm[0]);

    try {
        const response = await $.ajax({
            url: actionUrl,
            type: 'post',
            data: dataToSend,
            processData: false,
            contentType: false
        });

        const result = JSON.parse(response);

        if (result['Success']) {
            if (result['ShowMessage']) {
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

            // استفاده از `setTimeout` به جای `delay` برای تأخیر
            setTimeout(() => {
                if (parseInt(result['updateType']) === 1) {
                    if (result['returnUrl']) window.location.href = result['returnUrl'];
                } else {
                    if (result['returnUrl']) {
                        const getUrl = result['returnUrl'];
                        const target = result['updateTargetId'];

                        $.get(getUrl).done(newData => {
                            $(target).html(newData);
                            $('#accmodal').modal('hide');
                            $('.select2').select2();
                        });
                    }
                }
            }, 1000);
        } else {
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
    } catch (error) {
        console.error('There was an error with the AJAX request:', error);
        Swal.fire({
            title: 'خطا!',
            html: 'در ارسال درخواست مشکلی پیش آمده است.',
            icon: 'error',
            customClass: {
                confirmButton: 'btn btn-danger'
            },
            confirmButtonText: 'باشه',
            buttonsStyling: false
        });
    } finally {
        if (loader) loader.style.display = "none";
    }
});


//removeArticle
$(document).on('click', '.removeById', async function (event) {
    event.preventDefault(); // جلوگیری از رفتار پیش‌فرض اگر دکمه باشد

    const loader = document.getElementById("loader");
    if (loader) loader.style.display = "block";

    const actionUrl = $(this).data('url');
    const id = $(this).data('target');
    const tr = $(this).closest('tr');
    const confirmDialogText = $(this).attr('title');

    if (confirm(confirmDialogText)) {
        try {
            const response = await $.ajax({
                url: actionUrl,
                type: 'POST',
                data: { itemId: id }
            });

            const result = JSON.parse(response);

            if (result['Success']) {
                tr.hide(300);
                if (result['ShowMessage']) {
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

                setTimeout(() => {
                    if (parseInt(result['updateType']) === 1) {
                        if (result['returnUrl']) window.location.href = result['returnUrl'];
                    } else {
                        if (result['returnUrl']) {
                            const getUrl = result['returnUrl'];
                            const target = result['updateTargetId'];

                            $.get(getUrl).done(newData => {
                                $(target).html(newData);
                                $('#accmodal').modal('hide');
                                $('.select2').select2();
                            });
                        }
                    }
                }, 1000);
            } else {
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
        } catch (error) {
            console.error('There was an error with the AJAX request:', error);
            Swal.fire({
                title: 'خطا!',
                html: 'در ارسال درخواست مشکلی پیش آمده است.',
                icon: 'error',
                customClass: {
                    confirmButton: 'btn btn-danger'
                },
                confirmButtonText: 'باشه',
                buttonsStyling: false
            });
        } finally {
            if (loader) loader.style.display = "none";
        }
    } else {
        if (loader) loader.style.display = "none";
    }
});


document.addEventListener('click', async event => {
    const target = event.target.closest('.callmodal[data-url]');
    if (target) {
        event.preventDefault(); // جلوگیری از رفتار پیش‌فرض در صورت نیاز

        const actionUrl = target.dataset.url;

        try {
            const response = await fetch(actionUrl);
            const data = await response.text();

            const modalTarget = $('#accmodal');
            modalTarget.html(data);

            // استفاده از `flatpickr`
            $('.flatpickr-input').flatpickr({
                locale: 'fa',
                altInput: true,
                altFormat: 'Y/m/d',
                allowInput: true,
            });

            // نمایش مدال
            modalTarget.modal('show');

            // فعال کردن `select2` در صورت نیاز
            // $('.select2').select2();
        } catch (error) {
            console.error('Error fetching data:', error);
            // نمایش پیام خطا به کاربر در صورت نیاز
            Swal.fire({
                title: 'خطا!',
                html: 'در بارگذاری اطلاعات مشکلی پیش آمده است.',
                icon: 'error',
                customClass: {
                    confirmButton: 'btn btn-danger'
                },
                confirmButtonText: 'باشه',
                buttonsStyling: false
            });
        }
    }
});


$(document).on('click', '.renumberRows', async function (event) {
    event.preventDefault(); // جلوگیری از رفتار پیش‌فرض در صورت نیاز

    const loader = $('#loader'); // اطمینان از وجود عنصر
    if (loader.length) loader.css('display', 'block');

    const actionUrl = $(this).data('url');
    const docId = $(this).data('docid');
    const tblId = $(this).data('target');
    const tableTr = $(tblId + ' tr[id]');
    const dataToSend = [];

    tableTr.each(function (index) {
        dataToSend.push({
            Id: $(this).attr('id'),
            RowNumber: index + 1
        });
    });

    try {
        const response = await $.ajax({
            url: actionUrl,
            type: 'post',
            data: { jsonData: JSON.stringify(dataToSend), docId: docId },
            dataType: 'json'
        });

        const result = response; // داده‌ها از نوع JSON هستند

        if (result['Success']) {
            if (result['ShowMessage']) {
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

            setTimeout(() => {
                if (parseInt(result['updateType']) === 1) {
                    if (result['returnUrl']) window.location.href = result['returnUrl'];
                } else {
                    if (result['returnUrl']) {
                        const getUrl = result['returnUrl'];
                        const target = result['updateTargetId'];

                        $.get(getUrl).done(newData => {
                            $(target).html(newData);
                            $('#accmodal').modal('hide');
                            $('.select2').select2();
                        });
                    }
                }
            }, 1000);
        } else {
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
    } catch (error) {
        console.error('Error with AJAX request:', error);
        Swal.fire({
            title: 'خطا!',
            html: 'در ارسال درخواست مشکلی پیش آمده است.',
            icon: 'error',
            customClass: {
                confirmButton: 'btn btn-danger'
            },
            confirmButtonText: 'باشه',
            buttonsStyling: false
        });
    } finally {
        if (loader.length) loader.css('display', 'none');
    }
});


$(document).on('dblclick', '.accountBrowser_item', async function (event) {
    event.preventDefault(); // جلوگیری از رفتار پیش‌فرض در صورت نیاز

    const loader = $('#loader');
    const place = $('#accountBrowswePlace');
    const card = $(this).closest('.card');
    const form = card.find('form');
    const actionUrl = $(this).data('url');
    const id = $(this).data('target');
    const longid = $(this).data('longid');

    const stDate = form.find('input[name=strStartDate]').val();
    const edDate = form.find('input[name=strEndDate]').val();
    const fromNumber = form.find('input[name=FromDocNumer]').val();
    const toDocNumer = form.find('input[name=ToDocNumer]').val();
    const docType = form.find('input[name=docType]').val();

    loader.css('display', 'block');

    try {
        const response = await $.ajax({
            url: actionUrl,
            type: 'post',
            data: {
                strStartDate: stDate,
                strEndDate: edDate,
                FromDocNumer: fromNumber,
                ToDocNumer: toDocNumer,
                docType: docType,
                targetId: id,
                longId: longid
            }
        });
        place.html(response);
    } catch (error) {
        console.error('Error with AJAX request:', error);
        Swal.fire({
            title: 'خطا!',
            html: 'در ارسال درخواست مشکلی پیش آمده است.',
            icon: 'error',
            customClass: {
                confirmButton: 'btn btn-danger'
            },
            confirmButtonText: 'باشه',
            buttonsStyling: false
        });
    } finally {
        loader.css('display', 'none');
    }
});

$(document).on('dblclick', '.accountBrowser_itemLink', function (event) {
    event.preventDefault(); // جلوگیری از رفتار پیش‌فرض در صورت نیاز

    const loader = $('#loader');
    const card = $(this).closest('.card');
    const form = card.find('form');
    const actionUrl = $(this).data('url');
    const id = $(this).data('target');

    loader.css('display', 'block');

    try {
        form.attr('action', actionUrl);
        form.find('input[name="filter.targetId"]').val(id);
        form.submit();
    } catch (error) {
        console.error('Error:', error);
        Swal.fire({
            title: 'خطا!',
            html: 'در ارسال فرم مشکلی پیش آمده است.',
            icon: 'error',
            customClass: {
                confirmButton: 'btn btn-danger'
            },
            confirmButtonText: 'باشه',
            buttonsStyling: false
        });
    } finally {
        loader.css('display', 'none');
    }
});

$(document).on('dblclick', '.accountBrowser_tafsil', function (event) {
    event.preventDefault(); // جلوگیری از رفتار پیش‌فرض در صورت نیاز

    const loader = $('#loader');
    const form = $('#frm_AccountBrowser');
    const actionUrl = $(this).data('url');
    const moeinId = $(this).data('moeinid');
    const tafsilId = $(this).data('tafsilid');

    loader.css('display', 'block');

    form.attr('action', actionUrl);
    form.find('input[name="filter.MoeinId"]').val(moeinId);
    form.find('input[name="filter.CurrentTafsilId"]').val(tafsilId);
    form.submit();
});

$(document).on('click', '.accountBrowser_nav', function () {
    const loader = $('#loader');
    const place = $('#accountBrowswePlace');

    loader.css('display', 'block');

    const $card = $(this).closest('.card');
    const form = $card.find('form');
    const actionUrl = $(this).data('url');
    const id = $(this).data('target');
    const longId = $(this).data('longid');

    const stDate = form.find('input[name="strStartDate"]').val();
    const edDate = form.find('input[name="strEndDate"]').val();
    const fromNumber = form.find('input[name="FromDocNumer"]').val();
    const toDocNumer = form.find('input[name="ToDocNumer"]').val();
    const docType = form.find('input[name="docType"]').val();

    $.ajax({
        url: actionUrl,
        type: 'POST',
        data: {
            strStartDate: stDate,
            strEndDate: edDate,
            FromDocNumer: fromNumber,
            ToDocNumer: toDocNumer,
            docType: docType,
            targetId: id,
            longId: longId
        }
    }).done(function (data) {
        place.html(data);
    }).always(function () {
        loader.css('display', 'none');
    });
});


$(document).on('dblclick', '.dblc-link', function () {
    const actionUrl = $(this).data('url');
    window.open(actionUrl, '_blank');
});

$(document).on('click', '.submitFormById', function () {
    const selectedForm = document.getElementById("frm_accBrowser");

    if (selectedForm) {
        selectedForm.submit();
    } else {
        console.error('Form with id "frm_accBrowser" not found.');
    }
});

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
    // دریافت کد کلید فشرده شده
    const key = e.which;

    // بررسی اینکه آیا کلید Enter (کد 13) فشرده شده است
    if (key === 13) {
        e.preventDefault(); // جلوگیری از عمل پیش‌فرض (مثل ارسال فرم)

        // انتخاب فیلد با شناسه 'artArchiveCode'
        const archiveCode = $('#artArchiveCode');
        archiveCode.select();
    }
});

$(document).on('keypress', '#artArchiveCode', function (e) {
    let key = e.which;
    if (key == 13) {
        $(document).find('#frmAddArticle_MoeinId').select2('open');
    }
});
$(document).on('change', '#frmAddArticle_MoeinId[data-url]', function () {
    var tafsilPlace = $(document).find('#frmAddArticle_TafsilPlace');
    tafsilPlace.empty(); // پاک‌سازی محتوای قبلی

    var id = $(this).val(); // گرفتن مقدار انتخاب‌شده
    var actionUrl = $(this).data('url') + "/" + id; // ساختن URL

    $.get(actionUrl).done(function (data) {
        tafsilPlace.html(data); // قرار دادن داده‌های دریافتی در بخش مقصد

        $(document).find('.select2').select2(); // بازنشانی select2 برای فیلدهای جدید

        setTimeout(function () {
            // انتخاب فیلد‌هایی که ممکن است دارای select2 باشند و باز کردن آنها
            var tafsils = [
                '#frmArticle_Tafsil4',
                '#frmArticle_Tafsil5',
                '#frmArticle_Tafsil6',
                '#frmArticle_Tafsil7',
                '#frmArticle_Tafsil8'
            ];

            var selected = tafsils.find(selector => $(document).find(selector).length > 0);

            if (selected) {
                $(document).find(selected).select2().select2('open');
            } else {
                var articleComment = $(document).find('textarea[name="Article.Comment"]');
                if (articleComment.length) {
                    articleComment.select();
                }
            }
        }, 0);
    });
});

//$(Document).on('change', '#frmAddArticle_MoeinId[data-url]', function () {

//    var tafsilPlcae = $(document).find('#frmAddArticle_TafsilPlace');
//    tafsilPlcae.empty();
//    var id = $(this).val();
//    var actionUrl = $(this).data('url') + "/" + id;
//    $.get(actionUrl).done(function (data) {
//        tafsilPlcae.html(data);
//        $(document).find('.select2').select2();
//        setTimeout(function () {
//            let tafsil4 = $(document).find('#frmArticle_Tafsil4');
//            let tafsil5 = $(document).find('#frmArticle_Tafsil5');
//            let tafsil6 = $(document).find('#frmArticle_Tafsil6');
//            let tafsil7 = $(document).find('#frmArticle_Tafsil7');
//            let tafsil8 = $(document).find('#frmArticle_Tafsil8');
//            let articleComment = $(document).find('textarea[name="Article.Comment"]');
//            if (tafsil4.length) {
//                tafsil4.select2();
//                tafsil4.select2('open');
//            }
//            else if (tafsil5.length) {
//                tafsil5.select2();
//                tafsil5.select2('open');
//            }
//            else if (tafsil6.length) {
//                tafsil6.select2();
//                tafsil6.select2('open');
//            }
//            else if (tafsil7.length) {
//                tafsil7.select2();
//                tafsil7.select2('open');
//            }
//            else if (tafsil8.length) {
//                tafsil8.select2();
//                tafsil8.select2('open');
//            }
//            else {
//                articleComment.select();
//            }
//        }, 0);

//    });
//});
$(document).on('change', '#frmArticle_Tafsil4', function () {

    let tafsil5 = $(document).find('#frmArticle_Tafsil5');
    let tafsil6 = $(document).find('#frmArticle_Tafsil6');
    let tafsil7 = $(document).find('#frmArticle_Tafsil7');
    let tafsil8 = $(document).find('#frmArticle_Tafsil8');
    var articleComment = $(document).find('textarea[name="Article.Comment"]');

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
    var articleComment = $(document).find('textarea[name="Article.Comment"]');

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
    var articleComment = $(document).find('textarea[name="Article.Comment"]');
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
    let articleComment = $(document).find('textarea[name="Article.Comment"]');

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

    var articleComment = $(document).find('textarea[name="Article.Comment"]');
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

        if (idx === inputs.length - 1) {
            $('.accAjaxSubmit').click();
        } else {
            var nextInput = inputs[idx + 1];
            $(nextInput).focus().select();
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
    var tbl = $(this).parents('.card').find('#' + tblId);
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

$(document).on('change', '.SelectRows', function () {
    var isChecked = this.checked;
    $(document).find('input[name="docId"]').prop('checked', isChecked);

    if (isChecked) {
        $(document).find('#select-all-label').text('عدم انتخاب اسناد');
    } else {
        $(document).find('#select-all-label').text('انتخاب همه اسناد');
    }
});

$(document).on('click', '.callAction', function () {
    var frm = $(this).parents('form');
    var actionUrl = frm.attr('action');
    var rowsCount = frm.find('input[name=RowsCount]').val();
    var url = actionUrl + '?RowsCount=' + rowsCount;
    window.open(url, '_blank');
})
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



















//$(document).on('click', '.callform[data-url]', function () {

//    let actionUrl = $(this).data('url');

//    $.get(actionUrl).done(function (data) {

//        var targetmodal = $(document).find('#accmodal');
//        targetmodal.html(data);
//        targetmodal.modal('show');
//    })
//});
 //$(document).on('dblclick', 'tr[data-url]', '.codingNav[data-url]', function () {

//    var actionUrl = $(this).data('url');
//    $.get(actionUrl).done(function (data) {
//        $(document).find('#codingPlace').html(data);
//    })
//})

//$(document).on('click', '.codingNav[data-url]', function () {

//    var actionUrl = $(this).data('url');
//    $.get(actionUrl).done(function (data) {
//        $(document).find('#codingPlace').html(data);
//    })
//})


//var loder = document.find("#loader");

//$(document).on('dblclick', 'tr[data-url]', '.codingNav[data-url]', function () {

//    var actionUrl = $(this).data('url');
//    $.get(actionUrl).done(function (data) {
//        $(document).find('#codingPlace').html(data);
//    })
//})