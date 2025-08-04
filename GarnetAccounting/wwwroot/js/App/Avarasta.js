let avaModal = $(document).find('#model-general-larg');

$(document).on('click', '.delCustomer', function () {
    let actionUrl = $(this).data('url');
    let Id = $(this).data('target');
   
    if (confirm('شماره در حال حذف اطلاعات مشتری هستید، آیا ادامه می دهید ؟')) {
        $.ajax({
            url: actionUrl,
            type: 'POST',
            data: {id:Id}
        }).done(function (data) {
            let jsonResult = JSON.parse(data);

            if (jsonResult['Success'] == true) {
                Swal.fire({
                    title: 'کاربـر گـرامی !',
                    html: jsonResult['Message'],
                    icon: 'success',
                    customClass: {
                        confirmButton: 'btn btn-primary'
                    },
                    confirmButtonText: 'باشه',
                    buttonsStyling: false
                });
                $(this).delay(2000).queue(function () {
                    window.location.href = jsonResult['ReturnUrl'];
                    $(this).dequeue();
                });
            }
            else {
                Swal.fire({
                    title: 'کاربـر گـرامی !',
                    html: jsonResult['Message'],
                    icon: 'warning',
                    customClass: {
                        confirmButton: 'btn btn-primary'
                    },
                    confirmButtonText: 'باشه',
                    buttonsStyling: false
                });
            }
        })
    }
});

//$(document).on('click', '.delCustomer', function () {
//    let actionUrl = $(this).data('url');
//    let Id = $(this).data('target');

//    if (confirm('شماره در حال حذف اطلاعات مشتری هستید، آیا ادامه می دهید ؟')) {

//        $.ajax({
//            url: actionUrl,
//            type: 'post',
//            data: { id: Id },
//        }).done(function (result) {
//            let jsonResult = JSON.parse(result);

//            if (jsonResult['Success'] == true) {
//                Swal.fire({
//                    title: 'کاربـر گـرامی !',
//                    html: jsonResult['Message'],
//                    icon: 'success',
//                    customClass: {
//                        confirmButton: 'btn btn-primary'
//                    },
//                    confirmButtonText: 'باشه',
//                    buttonsStyling: false
//                });
//                $(this).delay(2000).queue(function () {
//                    window.location.href = jsonResult['ReturnUrl'];
//                    $(this).dequeue();
//                });
//            }
//            else {
//                Swal.fire({
//                    title: 'کاربـر گـرامی !',
//                    html: jsonResult['Message'],
//                    icon: 'warning',
//                    customClass: {
//                        confirmButton: 'btn btn-primary'
//                    },
//                    confirmButtonText: 'باشه',
//                    buttonsStyling: false
//                });
//            }
//        });
//    }
//});