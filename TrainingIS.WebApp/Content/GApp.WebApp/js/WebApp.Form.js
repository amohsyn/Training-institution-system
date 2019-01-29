//
// Select Filter 
//

// Params
// filter_id : Filter "Select" id
// filtered_id : Select multi id 
function SelectFilter(filter_id, filtedred_id) {

    var filter_selector = "#" + filter_id;
    var filtedred_selector = "#" + filtedred_id;
    var data_filter_name = filter_id.toLowerCase();

    // On Filter Change

    var onChangeFunction = function () {

        var filter_selected_value = $(filter_selector).find(":selected").val();

        // Clear Filter
        $(filtedred_selector + " span option").unwrap('<span/>');

        // Apply Filter
        $(filtedred_selector + " option").filter(function () {
            return ($(this).data(data_filter_name) != filter_selected_value)
        }).wrap('<span/>');
    };

    $(filter_selector).change(onChangeFunction);
    onChangeFunction();


}

// 
// ReadFrom 
//
function ReadFrom(PropertyId, ReadFromId) {

    var ReadFromId_Selector = "#" + ReadFromId;
    var PropertyId_Selector = "#" + PropertyId;
    $(ReadFromId_Selector).change(function () {
        $(PropertyId_Selector).val($(ReadFromId_Selector).val())
    });
}

//
// Cascad_DropDownList
//
