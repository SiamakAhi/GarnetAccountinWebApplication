
$('#tableToPrint').click( function () {
    // دریافت شناسه جدول از data-tableid دکمه
    var selectedTableId = $(this).data('tableid');

    if (!selectedTableId || $(selectedTableId).length === 0) {
        alert('جدولی برای چاپ یافت نشد.');
        return;
    }

    // استخراج محتوای جدول
    var tableContent = $(selectedTableId).prop('outerHTML');

    // ایجاد پنجره چاپ جدید
    var printWindow = window.open('', '', 'height=800,width=1200');

    // استایل‌ها برای تنظیم چاپ در یک صفحه A4 افقی
    printWindow.document.write(`
        <html lang="fa">
            <head>
                <title>چاپ جدول</title>
                <style>
                    @media print {
                        @page {
                            size: A4 landscape; /* تنظیم صفحه A4 افقی */
                            margin: 5mm; /* کاهش حاشیه برای فضای بیشتر */
                        }
                        body {
                            margin:10px auto 10px auto;
                            padding: 0px;
                            font-family: 'Yekan Bakh FaNum', sans-serif;
                            direction: rtl;
                            font-size: 10px; /* کاهش اندازه فونت برای جا شدن در صفحه */
                        }
                        table {
                            margin:auto;
                            max-width: 98%;
                            table-layout: fixed; /* تنظیم عرض ثابت ستون‌ها برای جا شدن در صفحه */
                            border-collapse: collapse;
                        }
                        th, td {
                            border: 1px solid gray;
                            padding: 4px;
                            text-align: center;
                            word-wrap: break-word; /* جلوگیری از خروج متن از ستون‌ها */
                            overflow: hidden;
                            font-size: 10px;
                        }
                        th {
                            background-color: whitesmoke;
                        }
                        input[type="checkbox"] {
                            display: none;
                        }
                    }
                </style>
            </head>
            <body>
                ${tableContent}
            </body>
        </html>
    `);

    // آماده‌سازی و ارسال پنجره به چاپگر
    printWindow.document.close();
    printWindow.focus();
    printWindow.print();
    printWindow.close();
});

$('#exportToExcel').click( function () {
    // تعیین شناسه جدول‌ها
    var tableId1 = '#tbl-accountBrowserTafsil';
    var tableId2 = '#tbl-accountBrowserArt';
    var tableData = [];

    // بررسی وجود جدول و انتخاب جدول مناسب
    var selectedTableId = $(tableId1).length ? tableId1 : ($(tableId2).length ? tableId2 : null);

    if (!selectedTableId) {
        alert('جدولی برای خروجی یافت نشد.');
        return;
    }

    // استخراج هدرهای جدول به صورت پویا
    $(selectedTableId + ' thead tr').each(function (rowIndex) {
        var headerRow = [];
        $(this).find('th').each(function (colIndex) {
            var colspan = $(this).attr('colspan') || 1;
            var text = $(this).text().trim();

            // بررسی ردیف دوم هدر برای پرش از ستون اول
            if (rowIndex === 1 && colIndex === 0) {
                headerRow.push(''); // اضافه کردن ستون خالی برای ستون ردیف در ردیف دوم
            }

            // افزودن هدرها با توجه به colspan
            headerRow.push(text);
            for (var i = 1; i < colspan; i++) {
                headerRow.push(''); // افزودن ستون‌های خالی برای colspan
            }
        });
        tableData.push(headerRow);
    });

    // استخراج اطلاعات سطرهای جدول
    var columnSums = []; // آرایه برای ذخیره مجموع هر ستون
    $(selectedTableId + ' tbody tr').each(function () {
        var rowData = [];
        $(this).find('td').each(function (colIndex) {
            var cellValue = $(this).text().trim();
            rowData.push(cellValue);

            // محاسبه مجموع هر ستون (فرض بر این است که داده‌های عددی فقط در ستون‌های بدهکار و بستانکار قرار دارند)
            var numericValue = parseFloat(cellValue.replace(/,/g, '')) || 0;
            columnSums[colIndex] = (columnSums[colIndex] || 0) + numericValue;
        });
        tableData.push(rowData);
    });

    // افزودن ردیف جمع به جدول
    var sumRow = [];
    for (var i = 0; i < tableData[0].length; i++) {
        if (columnSums[i]) {
            sumRow.push(columnSums[i].toLocaleString('en-US')); // افزودن مجموع به صورت عددی با کاما
        } else {
            sumRow.push(''); // ستون‌های غیرعددی خالی باقی می‌مانند
        }
    }
    tableData.push(sumRow);

    // ایجاد ورک‌بوک اکسل با استفاده از SheetJS
    var worksheet = XLSX.utils.aoa_to_sheet(tableData);

    // تنظیم عرض ستون‌ها به‌صورت خودکار
    const wscols = [];
    for (let i = 0; i < tableData[0].length; i++) {
        wscols.push({ wpx: 100 }); // تنظیم عرض پیش‌فرض ستون‌ها به 100 پیکسل
    }
    worksheet['!cols'] = wscols;

    // خط‌کشی جدول (مرزبندی سلول‌ها) و راست‌چین کردن متن سلول‌ها
    for (let cell in worksheet) {
        if (cell[0] === '!') continue; // نادیده گرفتن متادیتاهای شیت
        worksheet[cell].s = {
            border: {
                top: { style: "thin", color: { rgb: "A9A9A9" } },
                right: { style: "thin", color: { rgb: "A9A9A9" } },
                bottom: { style: "thin", color: { rgb: "A9A9A9" } },
                left: { style: "thin", color: { rgb: "A9A9A9" } }
            },
            alignment: {
                vertical: 'center',
                horizontal: 'center', // مرکز چین کردن هدرها و داده‌ها
            }
        };
    }

    // تنظیم ورک‌شیت به حالت راست‌به‌چپ (RTL)
    var workbook = XLSX.utils.book_new();
    workbook.Workbook = { Views: [{ RTL: true }] }; // اعمال تنظیمات RTL به ورک‌بوک
    XLSX.utils.book_append_sheet(workbook, worksheet, 'Report');

    // خروجی گرفتن از ورک‌بوک به فایل اکسل
    XLSX.writeFile(workbook, 'Report.xlsx');
});

$('#printTable').click( function () {
    // دریافت شناسه جدول از data-tableid دکمه
    // تعیین شناسه جدول‌ها
    var tableId1 = '#tbl-accountBrowserTafsil';
    var tableId2 = '#tbl-accountBrowserArt';
    var tableData = [];

    // بررسی وجود جدول و انتخاب جدول مناسب
    var selectedTableId = $(tableId1).length ? tableId1 : ($(tableId2).length ? tableId2 : null);

    if (!selectedTableId || $(selectedTableId).length === 0) {
        alert('جدولی برای چاپ یافت نشد.');
        return;
    }

    // استخراج محتوای جدول
    var tableContent = $(selectedTableId).prop('outerHTML');

    // ایجاد پنجره چاپ جدید
    var printWindow = window.open('', '', 'height=800,width=1200');

    // استایل‌ها برای تنظیم چاپ در یک صفحه A4 افقی
    printWindow.document.write(`
        <html lang="fa">
            <head>
                <title>چاپ جدول</title>
                <style>
                    @media print {
                        @page {
                            size: A4 landscape; /* تنظیم صفحه A4 افقی */
                            margin: 10mm; /* کاهش حاشیه برای فضای بیشتر */
                           
                        }
                        body {
                            margin:10px auto 10px auto;
                            padding:8px 0px;
                            font-family: 'Yekan Bakh FaNum', sans-serif;
                            direction: rtl;
                            font-size: 10px; /* کاهش اندازه فونت برای جا شدن در صفحه */
                        }
                        table {
                            margin:auto;
                            max-width: 98%;
                            table-layout: fixed; /* تنظیم عرض ثابت ستون‌ها برای جا شدن در صفحه */
                            border-collapse: collapse;
                        }
                        th, td {
                            border: 1px solid gray;
                            padding: 4px;
                            text-align: center;
                            word-wrap: break-word; /* جلوگیری از خروج متن از ستون‌ها */
                            overflow: hidden;
                            font-size: 10px;
                        }
                        th {
                            background-color: whitesmoke;
                        }
                        input[type="checkbox"] {
                            display: none;
                        }
                        .reportTitle{
                            width:100%;
                            text-align:center;
                            padding:8px;
                            font-size:18px;
                        }
                    }
                </style>
            </head>
            <body>
            <div class="reportTitle"> چاپ مرورگر </div>
                ${tableContent}
            </body>
        </html>
    `);

    // آماده‌سازی و ارسال پنجره به چاپگر
    printWindow.document.close();
    printWindow.focus();
    printWindow.print();
    printWindow.close();
});




document.getElementById('exportToExcelById').addEventListener('click', function () {
    // دریافت شناسه جدول از data-tableid دکمه
    var selectedTableId = $(this).data('tableid');
    var tableData = [];

    if (!selectedTableId || $(selectedTableId).length === 0) {
        alert('جدولی برای خروجی یافت نشد.');
        return;
    }

    // استخراج هدرهای جدول به صورت پویا
    $(selectedTableId + ' thead tr').each(function (rowIndex) {
        var headerRow = [];
        $(this).find('th').each(function (colIndex) {
            var colspan = $(this).attr('colspan') || 1;
            var text = $(this).text().trim();

            // بررسی ردیف دوم هدر برای پرش از ستون اول
            if (rowIndex === 1 && colIndex === 0) {
                headerRow.push(''); // اضافه کردن ستون خالی برای ستون ردیف در ردیف دوم
            }

            // افزودن هدرها با توجه به colspan
            headerRow.push(text);
            for (var i = 1; i < colspan; i++) {
                headerRow.push(''); // افزودن ستون‌های خالی برای colspan
            }
        });
        tableData.push(headerRow);
    });

    // استخراج اطلاعات سطرهای جدول
    var columnSums = []; // آرایه برای ذخیره مجموع هر ستون
    $(selectedTableId + ' tbody tr').each(function () {
        var rowData = [];
        $(this).find('td').each(function (colIndex) {
            var cellValue = $(this).text().trim();
            rowData.push(cellValue);

            // محاسبه مجموع هر ستون (فرض بر این است که داده‌های عددی فقط در ستون‌های مشخصی قرار دارند)
            var numericValue = parseFloat(cellValue.replace(/,/g, '')) || 0;
            columnSums[colIndex] = (columnSums[colIndex] || 0) + numericValue;
        });
        tableData.push(rowData);
    });

    // افزودن ردیف جمع به جدول
    var sumRow = [];
    for (var i = 0; i < tableData[0].length; i++) {
        if (columnSums[i]) {
            sumRow.push(columnSums[i].toLocaleString('en-US')); // افزودن مجموع به صورت عددی با کاما
        } else {
            sumRow.push(''); // ستون‌های غیرعددی خالی باقی می‌مانند
        }
    }
    tableData.push(sumRow);

    // ایجاد ورک‌بوک اکسل با استفاده از SheetJS
    var worksheet = XLSX.utils.aoa_to_sheet(tableData);

    // تنظیم عرض ستون‌ها به‌صورت خودکار
    const wscols = [];
    for (let i = 0; i < tableData[0].length; i++) {
        wscols.push({ wpx: 100 }); // تنظیم عرض پیش‌فرض ستون‌ها به 100 پیکسل
    }
    worksheet['!cols'] = wscols;

    // خط‌کشی جدول (مرزبندی سلول‌ها) و راست‌چین کردن متن سلول‌ها
    for (let cell in worksheet) {
        if (cell[0] === '!') continue; // نادیده گرفتن متادیتاهای شیت
        worksheet[cell].s = {
            border: {
                top: { style: "thin", color: { rgb: "A9A9A9" } },
                right: { style: "thin", color: { rgb: "A9A9A9" } },
                bottom: { style: "thin", color: { rgb: "A9A9A9" } },
                left: { style: "thin", color: { rgb: "A9A9A9" } }
            },
            alignment: {
                vertical: 'center',
                horizontal: 'center', // مرکز چین کردن هدرها و داده‌ها
            }
        };
    }

    // تنظیم ورک‌شیت به حالت راست‌به‌چپ (RTL)
    var workbook = XLSX.utils.book_new();
    workbook.Workbook = { Views: [{ RTL: true }] }; // اعمال تنظیمات RTL به ورک‌بوک
    XLSX.utils.book_append_sheet(workbook, worksheet, 'Report');

    // خروجی گرفتن از ورک‌بوک به فایل اکسل
    XLSX.writeFile(workbook, 'Report.xlsx');
});






