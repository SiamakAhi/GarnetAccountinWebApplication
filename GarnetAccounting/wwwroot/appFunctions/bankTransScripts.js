

$('.tblRow').on('change', function () {
    console.log('Checkbox changed');
    calculateTotals();
});
function calculateTotals() {
    console.log('Calculating totals...');

    let totalDebtor = 0;
    let totalCreditor = 0;
    let selectedCount = 0;

    $('.tblRow:checked').each(function () {
        selectedCount++;
        const $row = $(this).closest('tr');

        const debtorText = $row.find('td:eq(9)').text();
        const creditorText = $row.find('td:eq(10)').text();

        console.log('Row ' + selectedCount + ' - Debtor:', debtorText, 'Creditor:', creditorText);

        totalDebtor += parsePersianNumber(debtorText);
        totalCreditor += parsePersianNumber(creditorText);
    });

    console.log('Total Debtor:', totalDebtor, 'Total Creditor:', totalCreditor);

    $('#input_debtor').val(totalDebtor.toLocaleString('fa-IR'));
    $('#input_crideor').val(totalCreditor.toLocaleString('fa-IR'));
}

function parsePersianNumber(str) {
    if (!str) return 0;

    let result = str.toString().trim();

    // تبدیل اعداد فارسی
    const persianDigits = '۰۱۲۳۴۵۶۷۸۹';
    const englishDigits = '0123456789';

    for (let i = 0; i < 10; i++) {
        const regex = new RegExp(persianDigits[i], 'g');
        result = result.replace(regex, englishDigits[i]);
    }

    // حذف کاما و سایر کاراکترها
    result = result.replace(/[^\d.]/g, '');

    const num = parseFloat(result) || 0;
    console.log('Parsed:', str, '->', result, '->', num);

    return num;
}