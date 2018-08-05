//
// Select Filter 
//

// Params
// filter_id : Filter "Select" id
// filtered_id : Select multi id 
function SelectFilter(filter_id, filtedred_id) {

    var filter_selector = "#" + filter_id;
    var filtedred_selector = "#" + filtedred_id;

    // On Filter Change
    $(filter_selector).change(function () {

        var filter_selected_value = $(filter_selector).find(":selected").val();

        // Clear Filter
        $(filtedred_selector + " span option").unwrap('<span/>');

        // Apply Filter
        $(filtedred_selector + " option").filter(function () {
            return ($(this).data("controllerappid") != filter_selected_value)
        }).wrap('<span/>');
    });
}


$(document).ready(function () {

    $('.datetimepicker').datetimepicker({
        keepOpen: true,
        format: 'DD/MM/YYYY',
        showClose: true

    });

    $('.datetimepicker').on('dp.change', function (e) {
        $(this).datetimepicker('hide');
    });

    // 
    // Select2 init
    //
    $.fn.select2.defaults.set("theme", "bootstrap");
    $('select').select2({
        theme: "bootstrap"
    });

  
   
  
     


});  

