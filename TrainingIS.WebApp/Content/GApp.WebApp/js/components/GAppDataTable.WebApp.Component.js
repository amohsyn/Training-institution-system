var FilterBy_String = "";
var OrderBy_String = "";
var GetDataTable_URL = "";
var GAppDataTable_Id = ""
var Current_Page = 1;

// Init GAppDataTable 
function Init_GAppDataTable(gAppDataTable_Id, getDataTable_URL) {
    GetDataTable_URL = getDataTable_URL;
    GAppDataTable_Id = gAppDataTable_Id
}

// Get the from the server

function Update_GAppDataTable_Data(URL) {

    if (URL == undefined) {
        URL = GetDataTable_URL
    }
    alert(Current_Page);
    jQuery.ajax({
        type: "POST",
        url: URL,
        data: {
            OrderBy: OrderBy_String,
            FilterBy: FilterBy_String,
            SearchBy: jQuery("#Search_GAppDataTable").val(),
            currentPage: Current_Page,
            pageSize: parseInt(jQuery("#SelectPageSize").children('option:selected').val())
        },
        success: function (data) {
            jQuery("#" + GAppDataTable_Id).html("").html(data);
            GAppContext.Init_After_Ajax_Request();
        }
    });
}

//
function Update_FilterByString() {

    var Filters = [];
    $(".GAppDataTable_Filter").each(function () {

        var Property = $(this).attr("id").replace("_Filter", "");
        var value = "";

        // Read filter value
        value = $(this).val();
        if ($(this).prop("tagName") == "SELECT") {
            if (value == "0") value = "";
        }
        if (value != "" && value != null) {
            var Filter = "[" + Property + "," + value + "]";
            Filters.push(Filter);
        }
    })

    FilterBy_String = Filters.join(";");
    // alert(FilterBy_String);
    Current_Page = 1;
    Update_GAppDataTable_Data();
}

function Update_OrderByString() {
    var SortCriterias = [];
    $("th.header-column").each(function () {

        var self = $(this);
        var SortCriteria = "";
        if ($(this).find("i").hasClass('fa-sort')) {
            SortCriteria = "";
        }
        if ($(this).find("i").hasClass('fa-sort-asc')) {
            SortCriteria = jQuery(this).attr("id");
        }
        if ($(this).find("i").hasClass('fa-sort-desc')) {
            SortCriteria = jQuery(this).attr("id") + " " + "Desc";
        }

        if (SortCriteria != "") {
            SortCriterias.push(SortCriteria);
        }


    })
    OrderBy_String = SortCriterias.join(";");

    Current_Page = 1;
    Update_GAppDataTable_Data();
}


function GAppDataTable_Init_After_Ajax_Request() {

}
jQuery(document).ready(function () {

    // Filter  
    jQuery(document).on("change", ".GAppDataTable_Filter", function () {
        Update_FilterByString();
    });

    $('.GAppDataTable_Filter').on('dp.change', function (e) {
        Update_FilterByString();
    });

    // Order  
    jQuery(document).on("click", "th.header-column", function () {
        var self = $(this);


        if ($(this).find("i").hasClass('fa-sort')) {
            $(this).find("i").removeClass('fa-sort');
            $(this).find("i").addClass('fa-sort-asc');
            Update_OrderByString();
            return;
        }
        if ($(this).find("i").hasClass('fa-sort-asc')) {
            $(this).find("i").removeClass('fa-sort-asc');
            $(this).find("i").addClass('fa-sort-desc');
            Update_OrderByString();
            return;
        }
        if ($(this).find("i").hasClass('fa-sort-desc')) {
            $(this).find("i").removeClass('fa-sort-desc');
            $(this).find("i").addClass('fa-sort');
            Update_OrderByString();
            return;
        }


    });

    // Pagination
    jQuery(document).on("click", "li.currentPage", function () {
        if ($(this).hasClass("disabled")) {
            event.preventDefault();
            return false;
        }
        Current_Page = parseInt($(this).attr("pagenumber"));
        Update_GAppDataTable_Data();
    });

    jQuery(document).on("keyup", "#Search_GAppDataTable", function () {
        Update_GAppDataTable_Data();
    });

    jQuery(document).on("change", "#SelectPageSize", function () {
        Update_GAppDataTable_Data();
    });

    GAppDataTable_Init_After_Ajax_Request();
});