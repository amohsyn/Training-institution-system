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

$(document).ready(function () {
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
    $('.datetimepicker').datetimepicker({
        keepOpen: true,
        format: 'DD/MM/YYYY',
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