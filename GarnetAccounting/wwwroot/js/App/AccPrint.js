var ploder = document.getElementById("loader");


$(document).on('click', '.getprint', function () {

   // ploder.style.display = "block";
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
            , BalanceColumnsQty : balanceColumnsQty
        }
    }).done(function (data) {
        var newWindow = window.open();
        newWindow.document.write(data);
        newWindow.document.close();
    }).always(function () {
        ploder.style.display = "none";
    });
});

$(document).on('click', '.page-link', function () {

    let frm = $(this).parents('.card').find('form');
    let page = $(this).data('currentpage');
    frm.find('input[name="filter.CurrentPage"]').val(page);
   await frm.submitAsync();
});

//.............................................
$(document).on('click', '.tafsilPrint', function () {

    // ploder.style.display = "block";
    let frm = $(this).parents('form');
    let actionUrl = $(this).data('url');

    let groups = frm.find('select[name="TafsilReport.filter.selectTafsilGroup"]').val();
    let tafsils = frm.find('select[name="TafsilReport.filter.Tafsil4Ids"]').val();
    let tafsils5 = frm.find('select[name="TafsilReport.filter.Tafsil5Ids"]').val();
    let kols = frm.find('select[name="TafsilReport.filter.Kols"]').val();
    let moeins = frm.find('select[name="TafsilReport.filter.Moeins"]').val();
    let project = frm.find('select[name="TafsilReport.filter.ProjectId"]').val();
    let fromDate = frm.find('input[name="TafsilReport.filter.strStartDate"]').val();
    let toDate = frm.find('input[name="TafsilReport.filter.strEndDate"]').val();
    let reportLevel = frm.find('input[name="TafsilReport.filter.ReportLevel"]').val();

    $.ajax({
        url: actionUrl,
        type: 'get',
        data: {
            TafsilGroup: groups
            , Tafsil4Ids: tafsils
            , Tafsil5Ids: tafsils5
            , Kols: kols
            , Moeins: moeins
            , ProjectId: project
            , strStartDate: fromDate
            , strEndDate: toDate
            , ReportLevel=reportLevel
        }
    }).done(function (data) {
        var newWindow = window.open();
        newWindow.document.write(data);
        newWindow.document.close();
    }).always(function () {
        ploder.style.display = "none";
    });
});