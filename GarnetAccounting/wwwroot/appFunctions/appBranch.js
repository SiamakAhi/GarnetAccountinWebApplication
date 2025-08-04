
$('#themechange').change(function () {
    let uiId = $(this).val();

    $.get("/Home/uitoggle/id=" + uiId).done(function (ui) {
        alert();
    });
});

$(document).on('click', '.callform[data-url]', function () {

    let actionUrl = $(this).data('url');

    $.get(actionUrl).done(function (data) {

        appModal = $('#appmodal');
        appModal.html(data);
        appModal.modal('show');

        $(document).find('.flatpickr-input').flatpickr({
            locale: 'fa',
            altInput: true,
            altFormat: 'Y/m/d',
            allowInput: true,
            disableMobile: true
        });
    });
});
