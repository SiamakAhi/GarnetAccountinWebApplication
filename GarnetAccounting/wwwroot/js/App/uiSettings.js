
// تابعی برای ارسال درخواست به سرور
function keepSessionAlive() {
    fetch('/Home/KeepSessionAlive', { method: 'GET' })
        .then(response => {
            if (!response.ok) {
                console.error('Error keeping session alive');
            }
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}

// اجرای تابع هر 3 دقیقه یک بار
setInterval(keepSessionAlive, 120000); // 120000 میلی‌ثانیه = 2 دقیقه


$(document).find('#doc-Articles').DataTable({
    "paging": false,
    "info": false,

});
let deferredPrompt;

// گوش دادن به رویداد beforeinstallprompt
window.addEventListener('beforeinstallprompt', (e) => {
    // جلوگیری از نمایش خودکار اعلان نصب
    e.preventDefault();
    // ذخیره رویداد برای استفاده بعدی
    deferredPrompt = e;

    // نمایش دکمه یا پیام نصب به کاربر
    const installButton = document.getElementById('installPWAButton');
    installButton.style.display = 'block'; // دکمه نصب را نمایش دهید

    installButton.addEventListener('click', () => {
        // وقتی کاربر روی دکمه نصب کلیک کرد
        installButton.style.display = 'none'; // دکمه نصب را مخفی کنید
        deferredPrompt.prompt(); // نمایش دیالوگ نصب
        deferredPrompt.userChoice.then((choiceResult) => {
            if (choiceResult.outcome === 'accepted') {
                console.log('User accepted the install prompt');
            } else {
                console.log('User dismissed the install prompt');
            }
            deferredPrompt = null; // پاکسازی رویداد نصب
        });
    });
});

$('#avatarFileInput').change(function (event) {
    const file = event.target.files[0];

    // چک می‌کنیم که فایل یک تصویر باشد
    if (file && file.type.startsWith('image/')) {
        const reader = new FileReader();
        reader.onload = function (e) {
            document.getElementById('avatarPreview').src = e.target.result;
            document.getElementById('removeImageBtn').style.display = 'inline-block'; // نمایش دکمه حذف
        };
        reader.readAsDataURL(file);
    } else {
        alert('لطفا یک فایل تصویر انتخاب کنید.');
        // بازگرداندن تصویر به پیش‌فرض
        document.getElementById('avatarPreview').src = '~/img/avatars/avatar1.svg';
        // پاک کردن فایل انتخاب شده
        event.target.value = '';
        document.getElementById('removeImageBtn').style.display = 'none'; // مخفی کردن دکمه حذف
    }
});

// حذف تصویر و بازگرداندن به حالت پیش‌فرض
$('#removeImageBtn').click(function () {
    $('#avatarPreview').src = '~/img/avatars/avatar1.svg'; // بازگرداندن تصویر پیش‌فرض
    $('#avatarFileInput').value = ''; // پاک کردن فایل انتخاب شده
    this.style.display = 'none'; // مخفی کردن دکمه حذف
});

$('.pagination a.page-link').on('click', function (e) {
    e.preventDefault(); // جلوگیری از رفتار پیش‌فرض لینک
    var currentPage = $(this).data('currentpage');
    var formId = $(this).data('formid');
    var form = $('#' + formId);
    form.find('input[name="filter.CurrentPage"]').val(currentPage);
    form.submit();
});

$('.getRepDetail').on('click', function (e) {
    e.preventDefault();

    let representativeName = $(this).data('repname');
    let formId = $(this).data('frmfilterid');
    let form = $('#' + formId);

    if (form.length === 0) {
        console.error('Form not found with ID:', formId);
        return;
    }

    form.find('select[name="filter.DestinationRepresentative"]').val(representativeName);

    let actionUrl = $(this).data('url');
    if (!actionUrl) {
        console.error('Action URL not specified.');
        return;
    }

    form.attr('action', actionUrl);
    form.submit();
});



        