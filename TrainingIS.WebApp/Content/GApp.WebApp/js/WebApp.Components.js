//
// WebApp Components is the integration of Library by WebApp, as DataTable.js, Select2, Bootstrap datetimepicker
//

//
// DataTable
//
var dataTable_language_fr = {
    processing: "Traitement en cours...",
    search: "Rechercher&nbsp;:",
    lengthMenu: "Afficher _MENU_ &eacute;l&eacute;ments",
    info: "Affichage de l'&eacute;lement _START_ &agrave; _END_ sur _TOTAL_ &eacute;l&eacute;ments",
    infoEmpty: "Affichage de l'&eacute;lement 0 &agrave; 0 sur 0 &eacute;l&eacute;ments",
    infoFiltered: "(filtr&eacute; de _MAX_ &eacute;l&eacute;ments au total)",
    infoPostFix: "",
    loadingRecords: "Chargement en cours...",
    zeroRecords: "Aucun &eacute;l&eacute;ment &agrave; afficher",
    emptyTable: "Aucune donnée disponible dans le tableau",
    paginate: {
        first: "Premier",
        previous: "Pr&eacute;c&eacute;dent",
        next: "Suivant",
        last: "Dernier"
    },
    aria: {
        sortAscending: ": activer pour trier la colonne par ordre croissant",
        sortDescending: ": activer pour trier la colonne par ordre décroissant"
    }
};


function Init_DataTable () {
    $(".GAppDataTable").DataTable({
        language: dataTable_language_fr,
        "order": [],
        select: true
    });
    $(".GAppDataTable_NotPagination").DataTable({
        language: dataTable_language_fr,
        "order": [],
        select: true,
        paging: false
    });
}

$(document).ready(function () {
    Init_DataTable();
    GAppContext.Add_Init_After_Ajax_Request_Function(Init_DataTable);
});

//
// Init_Select2
//
function Init_Select2() {
    $.fn.select2.defaults.set("theme", "bootstrap");
    $('select').select2({
        theme: "bootstrap",
        placeholder: "",
        allowClear: true,
    });
}

$(document).ready(function () {
    Init_Select2();
});  
//
// datetimepicker
//
$(document).ready(function () {

    // Localization
    moment.locale(); 

    // DatePicker
    $('.datepicker').datetimepicker({
        keepOpen: true,
        format: 'DD/MM/YYYY',
        showClose: true
    });

    $('.datepicker').on('dp.change', function (e) {
        $(this).datetimepicker('hide');
    });

    // DateTimePicker
    $('.datetimepicker').datetimepicker({
        keepOpen: true,
        showClose: true
    });

    $('.datetimepicker').on('dp.change', function (e) {
        $(this).datetimepicker('hide');
    });
});  

//
// tooltip
//


$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip({
        container: 'body',
        trigger: 'hover'
    });
    $('[data-toggle="tooltip"]').on('click', function () {
        $(this).tooltip('hide')
    })
}); 


// GAppCheckBox 3 State
/*  */
   
function Init_GAppCheckBox(control) {
    tristate(control, '\u2753', '\u2705', '\u274C');
}


/**
 *  loops thru the given 3 values for the given control
 */
function tristate(control, value1, value2, value3) {
    switch (control.value.charAt(0)) {
        case value1:
            control.value = value2;
            break;
        case value2:
            control.value = value3;
            break;
        case value3:
            control.value = value1;
            break;
        default:
            // display the current value if it's unexpected
            alert(control.value);
    }
}

function GAppCheckBox_Value(control) {
    var element = $("#" + control);
    switch ( element.attr("value").charAt(0)) {
        case '\u2753':
            return "null";
            break;
        case '\u2705':
            return "true";
            break;
        case '\u274C':
            return "false";
            break;
    }
}

function GAppCheckBox_Char(value) {
    var element = $("#" + control);
    switch (value) {
        case "null":
            return "\u2753";
            break;
        case "'true":
            return "\u2705";
            break;
        case "false":
            return "\u274C";
            break;
    }
}

// GApp Image Upload
function myFunction() {

    var data = new FormData();
    var files = $("#UploadImg").get(0).files;
    if (files.length > 0) {
        data.append("MyImages", files[0]);
    }

    $.ajax({
        url: "/UploadImage/SaveFile",
        type: "POST",
        processData: false,
        contentType: false,
        data: data,
        success: function (response) {
            //code after success
            $("#txtImg").val(response);
            $("#imgPreview").attr('src', '/Upload/' + response);
        },
        error: function (er) {
            alert(er);
        }


    })
}

$("#UploadImg").change(function () {
    var data = new FormData();
    var files = $("#UploadImg").get(0).files;
    if (files.length > 0) {
        data.append("MyImages", files[0]);
    }

    $.ajax({
        url: "/UploadImage/UploadFile",
        type: "POST",
        processData: false,
        contentType: false,
        data: data,
        success: function (response) {
            //code after success
            $("#txtImg").val(response);
            $("#imgPreview").attr('src', '/Upload/Temp/' + response);
        },
        error: function (er) {
            alert(er);
        }

    });
});