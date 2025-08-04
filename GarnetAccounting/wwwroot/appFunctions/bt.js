
$('#bank').change(function () {
    let selectbankAccount = $('#bankAccount');
    let actionUrl = $(this).data('url');
    let bankId = $(this).val();

    // اعتبارسنجی اولیه
    if (!actionUrl || !bankId) {
        console.error('URL یا شناسه بانک وجود ندارد.');
        return;
    }

    $.ajax({
        url: actionUrl,
        type: 'POST',
        data: { id: bankId },
        dataType: 'json',
    }).done(function (response) {
        // خالی کردن لیست حساب‌ها
        selectbankAccount.empty();

        // اضافه کردن گزینه پیش‌فرض
        selectbankAccount.append($('<option>', {
            value: '',
            text: 'لطفاً انتخاب کنید'
        }));

        // پر کردن لیست حساب‌ها
        $.each(response, function (index, account) {
            selectbankAccount.append($('<option>', {
                value: account.id,
                text: account.accountName
            }));
        });

    }).fail(function (xhr, status, error) {
        console.error('درخواست با خطا مواجه شد:', error);
        alert('خطایی رخ داده است. لطفاً دوباره امتحان کنید.');
    });
});