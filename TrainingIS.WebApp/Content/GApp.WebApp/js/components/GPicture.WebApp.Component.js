// 
// GPicture - Component
//

 
// Edit
$(".GPicture .update").on("click", function () {
    var property_name = $(this).data("property_name");
    $("#Upload_" + property_name).trigger("click");
});

// Delete
$(".GPicture .Delete").on("click", function () {
    var property_name = $(this).data("property_name");
    $("#" + property_name + "_Preview").attr("src", GAppContext.URL_Root + "Content/GApp.WebApp/images/user.png")
    $("#" + property_name + "_Reference").val("Delete");
});


$(".Upload_GPicture").change(function () {

    var input_file = $(this);

    var picture_name_id = $(this).data("picture_name_id");
    var picture_preview_id = $(this).data("picture_preview_id");

    var data = new FormData();
    var files = input_file.get(0).files;
    if (files.length > 0) {
        data.append("GPictureFile", files[0]);
    }

    $.ajax({
        url: "/UploadImage/UploadFile",
        type: "POST",
        processData: false,
        contentType: false,
        data: data,
        success: function (response) {
            //code after success
            $("#" + picture_name_id).val(response);
            $("#" + picture_preview_id).attr('src', '/Upload_Tmp/' + response + '/Medium.png'  );
        },
        error: function (er) {
            alert(er);
        }

    });
});

//function Save_GPicture() {

//    var data = new FormData();
//    var files = $("#UploadImg").get(0).files;
//    if (files.length > 0) {
//        data.append("MyImages", files[0]);
//    }

//    $.ajax({
//        url: "/UploadImage/SaveFile",
//        type: "POST",
//        processData: false,
//        contentType: false,
//        data: data,
//        success: function (response) {
//            //code after success
//            $("#txtImg").val(response);
//            $("#imgPreview").attr('src', '/Upload/' + response);
//        },
//        error: function (er) {
//            alert(er);
//        }


//    })
//}

// CSS
$("figure.GPicture").mouseleave(
    function () {
        $(this).removeClass("hover");

    }
);