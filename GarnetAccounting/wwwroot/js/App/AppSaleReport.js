let reportArea = $(document).find('#placeholder-saleReport');
var loder = document.getElementById("loader");

$(document).on('click', '.getReportlink', function () {

    loder.style.display = "block";
    let frm = $(this).parents('form');
    let actionUrl = $(this).data('url');

    let stDate = frm.find('input[name=startDate]').val();
    let edDate = frm.find('input[name=EndDate]').val();
    let buyer = frm.find('select[name=BuyerId]').val();
    let TaxNo = frm.find('input[name=TaxId]').val();
    let NewInvoices = getCheckboxValue(frm.find('input[name="ShowNewInvoices"]'));
    let WaitToAccept = getCheckboxValue(frm.find('input[name=ShowWaitToAccept]'));
    let Accepted = getCheckboxValue(frm.find('input[name=ShowAccepted]'));
    let Canceled = getCheckboxValue(frm.find('input[name=ShowCanceled]'));
    let NeedEdit = getCheckboxValue(frm.find('input[name=ShowNeedEdit]'));


    $.ajax({
        url: actionUrl,
        type: 'get',
        data: {
            startDate: stDate
            , EndDate: edDate
            , BuyerId: buyer
            , TaxId: TaxNo
            , ShowNewInvoices: NewInvoices
            , ShowWaitToAccept: WaitToAccept
            , ShowAccepted: Accepted
            , ShowCanceled: Canceled
            , ShowNeedEdit: NeedEdit
        }
    }).done(function (data) {
        reportArea.html(data);
    }).always(function () {
        loder.style.display = "none";
    });
});

$(document).on('click', '.getReportBtn', function () {

    loder.style.display = "block";
    let frm = $(this).parents('form');
    let actionUrl = frm.attr('action');

    let stDate = frm.find('input[name=startDate]').val();
    let edDate = frm.find('input[name=EndDate]').val();
    let buyer = frm.find('select[name=BuyerId]').val();
    let TaxNo = frm.find('input[name=TaxId]').val();
    let NewInvoices = getCheckboxValue(frm.find('input[name="ShowNewInvoices"]'));
    let WaitToAccept = getCheckboxValue(frm.find('input[name=ShowWaitToAccept]'));
    let Accepted = getCheckboxValue(frm.find('input[name=ShowAccepted]'));
    let Canceled = getCheckboxValue(frm.find('input[name=ShowCanceled]'));
    let NeedEdit = getCheckboxValue(frm.find('input[name=ShowNeedEdit]'));


    $.ajax({
        url: actionUrl,
        type: 'get',
        data: {
            startDate: stDate
            , EndDate: edDate
            , BuyerId: buyer
            , TaxId: TaxNo
            , ShowNewInvoices: NewInvoices
            , ShowWaitToAccept: WaitToAccept
            , ShowAccepted: Accepted
            , ShowCanceled: Canceled
            , ShowNeedEdit: NeedEdit
        }
    }).done(function (data) {
        reportArea.html(data);
    }).always(function () {
        loder.style.display = "none";
    });
});


function getCheckboxValue(checkbox) {
    if (checkbox.is(':checked'))
        return true;
    else
        return false;
}

function getData() {

    let stDate = $(document).find('input[name=startDate]').val();
    let edDate = $(document).find('input[name=EndDate]').val();
    let buyer = $(document).find('select[name=BuyerId]').val();
    let NewInvoices = getCheckboxValue($(document).find('input[name="ShowNewInvoices"]'));
    let WaitToAccept = getCheckboxValue($(document).find('input[name=ShowWaitToAccept]'));
    let Accepted = getCheckboxValue($(document).find('input[name=ShowAccepted]'));
    let Canceled = getCheckboxValue($(document).find('input[name=ShowCanceled]'));
    let NeedEdit = getCheckboxValue($(document).find('input[name=ShowNeedEdit]'));

    var data = {
        startDate: stDate
        , EndDate: edDate
        , BuyerId: buyer
        , ShowNewInvoices: NewInvoices
        , ShowWaitToAccept: WaitToAccept
        , ShowAccepted: Accepted
        , ShowCanceled: Canceled
        , ShowNeedEdit: NeedEdit
    };

    return data;
}