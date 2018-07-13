$(function () {
    $('.datetimepicker').datetimepicker({
        keepOpen: true,
        format: 'DD/MM/YYYY',
        showClose: true

    });

    $('.datetimepicker').on('dp.change', function (e) {
        $(this).datetimepicker('hide');
    });
});