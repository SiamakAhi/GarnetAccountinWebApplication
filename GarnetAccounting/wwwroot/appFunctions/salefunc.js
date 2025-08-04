
$(document).ready(function () {

    $('#frminvc_Product').select2();

    $('#frminvc_Product').on('select2:select', function () {
        let actionUrl = $(this).data('url'); 
        let productId = $(this).val(); 

        if (productId !== "null") { 
            $.get(actionUrl, { id: productId })
                .done(function (result) {
                    if (result) {
                      
                        $('#frminvc_QuantityInPerPakage').val(result.quantityInPakage);
                        $('#frminvc_QuantityInPakageUnit').focus().select();
                        updateItemControls();
                    } 
                })
                .fail(function () {
                    alert("خطا در دریافت اطلاعات محصول.");
                });
        } 
    });

    // رویداد تغییر برای سایر فیلدهای ورودی که تابع updateItemControls را صدا می‌زنند
    $('#frminvc_QuantityInPakageUnit, #frminvc_QuantityInBaseUnit, #frminvc_UnitPrice, #frminvc_VatRate, #frminvc_Discount').change(function () {
        updateItemControls();
    });



    $('tr[data-url].opentab').on('dblclick', function () {
        var actionUrl = $(this).data('url');
        window.location.href = actionUrl;
        window.open(actionUrl, '_blank');
    });
});



// تابع برای پاکسازی جداکننده‌های هزارگان
function cleanNumber(value) {
    return parseFloat(value.replace(/,/g, '')) || 0;
}

// تابع برای به‌روزرسانی کنترل‌های آیتم فاکتور
function updateItemControls() {
    let item_input_PackQty = $('#frminvc_QuantityInPakageUnit');
    let item_input_UnitQty = $('#frminvc_QuantityInBaseUnit');
    let item_input_QtyPerPakage = $('#frminvc_QuantityInPerPakage');
    let item_input_TotalQty = $('#frminvc_TotalQuantity');
    let item_input_UnitPrice = $('#frminvc_UnitPrice');
    let item_input_SumBeforeDiscount = $('#frminvc_PriceBeForDescount');
    let item_input_Discount = $('#frminvc_Discount');
    let item_input_SumAfterDiscount = $('#frminvc_PriceAfterDiscount');
    let item_input_VatRate = $('#frminvc_VatRate');
    let item_input_VatPrice = $('#frminvc_VatPrice');
    let item_input_FinalPrice = $('#frminvc_FinalPrice');

    let item_PackQty = cleanNumber(item_input_PackQty.val());
    let item_UnitQty = cleanNumber(item_input_UnitQty.val());
    let item_QtyPerPakage = cleanNumber(item_input_QtyPerPakage.val());
    let item_UnitPrice = cleanNumber(item_input_UnitPrice.val());
    let item_Discount = cleanNumber(item_input_Discount.val());
    let item_VatRate = cleanNumber(item_input_VatRate.val());

    // محاسبه تعداد کل
    let item_TotalQty = (item_PackQty * item_QtyPerPakage + item_UnitQty).toFixed(2);

    // محاسبه مجموع قبل از تخفیف
    let item_SumBeforeDiscount = (item_UnitPrice * item_TotalQty).toFixed(2);

    // محاسبه مجموع بعد از تخفیف
    let item_SumAfterDiscount = (item_SumBeforeDiscount - item_Discount).toFixed(2);

    // محاسبه مالیات
    let item_VatPrice = ((item_SumAfterDiscount * item_VatRate) / 100).toFixed(2);

    // محاسبه قیمت نهایی
    let item_FinalPrice = (parseFloat(item_SumAfterDiscount) + parseFloat(item_VatPrice)).toFixed(2);

    // به‌روزرسانی مقادیر ورودی‌ها با فرمت جداکننده
    item_input_TotalQty.val(Number(item_TotalQty).toLocaleString());
    item_input_SumBeforeDiscount.val(Number(item_SumBeforeDiscount).toLocaleString());
    item_input_SumAfterDiscount.val(Number(item_SumAfterDiscount).toLocaleString());
    item_input_VatPrice.val(Number(item_VatPrice).toLocaleString());
    item_input_FinalPrice.val(Number(item_FinalPrice).toLocaleString());
}


//Edit Invoice Item
$(document).on('change', 'input[name="QuantityInPakageUnit"], input[name="QuantityInBaseUnit"], input[name="UnitPrice"], input[name="VatRate"], input[name="Discount"]', function () {
    update_EditItemControls();
});
function update_EditItemControls() {
    let item_input_PackQty = $(document).find('input[name="QuantityInPakageUnit"]'); 
    let item_input_UnitQty = $(document).find('input[name="QuantityInBaseUnit"]'); 
    let item_input_QtyPerPakage = $(document).find('input[name="QuantityInPerPakage"]'); 
    let item_input_TotalQty = $(document).find('input[name="TotalQuantity"]');
    let item_input_UnitPrice = $(document).find('input[name="UnitPrice"]'); 
    let item_input_SumBeforeDiscount = $(document).find('input[name="PriceBeForDescount"]');
    let item_input_Discount = $(document).find('input[name="Discount"]');
    let item_input_SumAfterDiscount = $(document).find('input[name="PriceAfterDiscount"]'); 
    let item_input_VatRate = $(document).find('input[name="VatRate"]');
    let item_input_VatPrice = $(document).find('input[name="VatPrice"]');
    let item_input_FinalPrice = $(document).find('input[name="FinalPrice"]'); 

    let item_PackQty = cleanNumber(item_input_PackQty.val());
    let item_UnitQty = cleanNumber(item_input_UnitQty.val());
    let item_QtyPerPakage = cleanNumber(item_input_QtyPerPakage.val());
    let item_UnitPrice = cleanNumber(item_input_UnitPrice.val());
    let item_Discount = cleanNumber(item_input_Discount.val());
    let item_VatRate = cleanNumber(item_input_VatRate.val());

    // محاسبه تعداد کل
    let item_TotalQty = (item_PackQty * item_QtyPerPakage + item_UnitQty).toFixed(2);

    // محاسبه مجموع قبل از تخفیف
    let item_SumBeforeDiscount = (item_UnitPrice * item_TotalQty).toFixed(2);

    // محاسبه مجموع بعد از تخفیف
    let item_SumAfterDiscount = (item_SumBeforeDiscount - item_Discount).toFixed(2);

    // محاسبه مالیات
    let item_VatPrice = ((item_SumAfterDiscount * item_VatRate) / 100).toFixed(2);

    // محاسبه قیمت نهایی
    let item_FinalPrice = (parseFloat(item_SumAfterDiscount) + parseFloat(item_VatPrice)).toFixed(2);

    // به‌روزرسانی مقادیر ورودی‌ها با فرمت جداکننده
    item_input_TotalQty.val(Number(item_TotalQty).toLocaleString());
    item_input_SumBeforeDiscount.val(Number(item_SumBeforeDiscount).toLocaleString());
    item_input_SumAfterDiscount.val(Number(item_SumAfterDiscount).toLocaleString());
    item_input_VatPrice.val(Number(item_VatPrice).toLocaleString());
    item_input_FinalPrice.val(Number(item_FinalPrice).toLocaleString());
}