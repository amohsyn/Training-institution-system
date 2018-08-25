$.ajaxSetup({
});

$(document).on("ready", function () {
    $("#web_app_loading").hide();
});

$(document).ajaxStart(function () {
    $("#web_app_loading").show();
});

$(document).ajaxStop(function () {
    $("#web_app_loading").hide();
});

$(document).ajaxComplete(function (event, request, settings) {
    $("#web_app_loading").hide();
});

$(document).ajaxError(function (event, request, settings, thrownError) {
    $("#web_app_loading").hide();
    // [Bug] Localization
    var error_msg = "Error " + settings.url   + " : " + thrownError;
    swal("Erreur", error_msg,"error" );
   
});

//function(jqXHR, textStatus, errorThrown) {
//    alert('An error occurred... Look at the console (F12 or Ctrl+Shift+I, Console tab) for more information!');

//    $('#result').html('<p>status code: ' + jqXHR.status + '</p><p>errorThrown: ' + errorThrown + '</p><p>jqXHR.responseText:</p><div>' + jqXHR.responseText + '</div>');
//    console.log('jqXHR:');
//    console.log(jqXHR);
//    console.log('textStatus:');
//    console.log(textStatus);
//    console.log('errorThrown:');
//    console.log(errorThrown);
//}


$(document).ajaxSuccess(function (event, request, settings) {
    $("#web_app_loading").hide();
});

$(document).ajaxSend(function (event, request, settings) {
    
});

