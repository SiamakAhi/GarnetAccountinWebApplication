
$('#selectTblRows').change(function () {
    alert();
})

$(document).on('change', '#selectTblRows', function () {
    var isChecked = this.checked;
    $(document).find('input[name="tableRow"]').prop('checked', isChecked);
    alert();
});