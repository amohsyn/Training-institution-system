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
// Dependent_DropDownList
// Exemple : Dependent_DropDownList("SpecialtyId", "ModuleTrainingId", "@Url.Content("~/")" + "Trainings/Get_ModuleTraining_By_SpecialtyId");
//
function Dependent_DropDownList(Id_ddl_master, Id_ddl_slave, server_data_action) {

    var $ddl_master = $('#' + Id_ddl_master);
    var $ddl_slave = $('#' + Id_ddl_slave);
    var server_data_action = server_data_action + "/";
    $ddl_master.change(function () {
        var master_object_id = $ddl_master.val();
        if ($ddl_master.val()) {
            url_data = server_data_action + $ddl_master.val();
            $.getJSON(url_data, function (data) {
                var html = $.map(data.list, function (selectListItem) {
                    return '<option value="' + selectListItem.Value + '">' + selectListItem.Text + '</option>'
                }).join('');
                $ddl_slave.html(html);
                $ddl_slave.change();
            });
        }
        else {
            $ddl_slave.html('');
            $ddl_slave.change();
        }
    });
}

function Dependent_DropDownList_Show_All_If_Master_Empty(Id_ddl_master, Id_ddl_slave, server_data_action) {

    var $ddl_master = $('#' + Id_ddl_master);
    var $ddl_slave = $('#' + Id_ddl_slave);
    var server_data_action = server_data_action + "/";
    $ddl_master.change(function () {
        var master_object_id = $ddl_master.val();
        url_data = server_data_action + $ddl_master.val();
        $.getJSON(url_data, function (data) {
            var html = $.map(data.list, function (selectListItem) {
                return '<option value="' + selectListItem.Value + '">' + selectListItem.Text + '</option>'
            }).join('');
            $ddl_slave.html(html);
            $ddl_slave.change();
        });
    });
}

//
// Dependant_DropDownList_TextBox
//
// Master   : DropDownList
// Salve    : TextBox
// PropertyName : PropertyName of Object Data geted from Server
function Dependant_DropDownList_TextBox(Id_ddl_master, Id_ddl_slave, server_data_action, PropertyName) {

    var $ddl_master = $('#' + Id_ddl_master);
    var $ddl_slave = $('#' + Id_ddl_slave);
    var server_data_action = server_data_action + "/";
    $ddl_master.change(function () {
        var master_object_id = $ddl_master.val();
        url_data = server_data_action + $ddl_master.val();
        $.getJSON(url_data, function (data) {
            $ddl_slave.val(data[PropertyName]);
        });
    });
}