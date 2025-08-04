 $(document).find('#addDays').hide();


$(document).on('change', 'input[name="GenerateDataType"]', function () {
    let selectedValue = $(this).val();
    if (selectedValue == 4) {
        $(document).find('#addDays').show(300);
        $(document).find('#selectDate').hide(300);
    }
    else if (selectedValue == 5) {
        $(document).find('#selectDate').show(300);
        $(document).find('#addDays').hide(300);
    }
    else {
        $(document).find('#addDays').hide(300);
        $(document).find('#selectDate').hide(300);
    }
});