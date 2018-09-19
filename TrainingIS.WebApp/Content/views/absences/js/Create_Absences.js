//
// Bind a Select with All Select tag wtih specefique html data
// and Load Create_Group_Absences Forms
//
function Bind_Select(select_id, data_bind, isLoadTrainee) {

    var select_id_selector = "#" + select_id;

    var onChangeFunction = function () {

        var slected_option = $(select_id_selector).find(":selected");
        var seance_planing_id = slected_option.data(data_bind)

        var lisg_option = $("select option").not(slected_option).filter(function () {
            return ($(this).data(data_bind) == seance_planing_id)
        })
        for (var i = 0; i < lisg_option.length; i++) {
            // alert(lisg_option[i].value);
            // lisg_option.select = "selected";
            // alert("#" + lisg_option[i].parentNode.id);

            $("#" + lisg_option[i].parentNode.id).val(lisg_option[i].value);
            // we use init_select2 insteand of .trigger('change') for because 
            // it generate overstackflow
            Init_Select2();


        }
        if (isLoadTrainee) {
            var SeanceDate = Current_SeanceDate;
            Load_Trainees_And_Create_SeanceTraining_If_NotExist(seance_planing_id, SeanceDate);
        }
       
    };

    // selected = 'selected'

    $(select_id_selector).change(onChangeFunction);
}

//
// DataTable
//
function Init_DataTable() {
    $(".GAppDataTable").DataTable({
        language: dataTable_language_fr,
        "order": [],
        select: true,
        paging: false,
        "pageLength": 100
    });
}


// 
// Load Trainees
//
function Load_Trainees(SeanceTainingId) {
    var url = GAppContext.URL_Root + "Absences/Get_Absences_Forms?SeanceTainingId=" + SeanceTainingId;
  
    $('#Absences_Trainees').load(url, function () {
        Init_DataTable();
    });

}
function Load_Trainees_And_Create_SeanceTraining_If_NotExist(SeancePlanningId, SeanceDate) {
    $('#Absences_Trainees').load(GAppContext.URL_Root + "Absences/Get_Absences_Forms_With_Create_SeanceTraining", { SeancePlanningId: SeancePlanningId, SeancePlanningId: SeancePlanningId, SeanceDate: SeanceDate }, function () {
        Init_DataTable();
    });

}

var Current_SeanceDate = Date.now();;
function Create_Absence(TraineeId, SeanceTainingId) {
    var trainee_line = $('#Trainee_' + TraineeId);
    $('#Trainee_' + TraineeId + ' .present_icon').css('display', "none");
    trainee_line.load(GAppContext.URL_Root + "Absences/Create_Absence", { TraineeId: TraineeId, SeanceTainingId: SeanceTainingId }, function () {
        Realod_JS();
    });
}
function Delete_Absence(TraineeId, SeanceTainingId) {
    
    var trainee_line = $('#Trainee_' + TraineeId);
    $('#Trainee_' + TraineeId + ' .present_icon').css('display', "none");
    trainee_line.load(GAppContext.URL_Root + "Absences/Delete_Absence", { TraineeId: TraineeId, SeanceTainingId: SeanceTainingId }, function () {
        Realod_JS();
    });
}


function Delete_SeanceTraining(SeanceTainingId) {
    var url = GAppContext.URL_Root + "Absences/Get_Absences_Forms?SeanceTainingId=" + SeanceTainingId;

    $('#Absences_Trainees').load(url, function () {
        Init_DataTable();
    });

}