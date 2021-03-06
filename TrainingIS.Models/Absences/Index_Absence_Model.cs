﻿using GApp.Core.Entities.ModelsViews;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Models.DataAnnotations;
using GApp.Models;
using GApp.Entities;
using TrainingIS.Entities.Resources.TraineeResources;
using TrainingIS.Entities.Resources.AbsenceResources;
using TrainingIS.Entities.Resources.SeanceTrainingResources;
using TrainingIS.Entities;
using TrainingIS.Entities.Resources.SeancePlanningResources;
using TrainingIS.Entities.Resources.GroupResources;
using GApp.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.StateOfAbseceResources;
using TrainingIS.Entities.enums;

namespace TrainingIS.Models.Absences
{
    [IndexView(typeof(Absence))]
    [SearchBy("Reference")]
    public class Index_Absence_Model : BaseModel
    {
        public bool Absent { set; get; } = true;

        [Display(Name = "AbsenceDate", ResourceType = typeof(msg_Absence))]
        [GAppDataTable(AutoGenerateFilter = true, FilterBy = "AbsenceDate", SearchBy = "AbsenceDate", OrderBy = "AbsenceDate", PropertyPath = "AbsenceDate")]
        [DataType(DataType.Date)]
        public DateTime AbsenceDate { set; get; }

        [SearchBy("Trainee.FirstName")]
        [SearchBy("Trainee.LastName")]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Trainee))]
        [GAppDataTable(AutoGenerateFilter = true, FilterBy = "Trainee.Id", SearchBy = "Trainee.Reference", OrderBy = "Trainee.Reference", PropertyPath = "Trainee")]
        public Trainee Trainee { set; get; }

        [GAppDataTable(AutoGenerateFilter = true,FilterBy = "SeanceTraining.SeancePlanning.Training.Group.Id", SearchBy = "SeanceTraining.SeancePlanning.Training.Group.Code", OrderBy = "SeanceTraining.SeancePlanning.Training.Group.Code", PropertyPath = "SeanceTraining.SeancePlanning.Training.Group.Code")]
        [Display(Name = "SingularName", AutoGenerateFilter = true, Order = 40, ResourceType = typeof(msg_Group))]
        public Group Group { set; get; }

        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        [GAppDataTable(AutoGenerateFilter = false, isSeachBy = false, isOrderBy =false, isColumn =true)]
        public string Description { set; get; }

        [SearchBy("SeanceTraining.SeancePlanning.SeanceNumber.Code")]
        [SearchBy("SeanceTraining.SeancePlanning.SeanceDay.Code")]
        [SearchBy("SeanceTraining.SeancePlanning.Training.ModuleTraining.Code")]
        [SearchBy("SeanceTraining.SeancePlanning.Training.ModuleTraining.Name")]
        [SearchBy("SeanceTraining.SeancePlanning.Training.Former.FirstName")]
        [SearchBy("SeanceTraining.SeancePlanning.Training.Former.LastName")]
        [Display(Name = "SingularName", ResourceType = typeof(msg_SeanceTraining))]
        public SeanceTraining SeanceTraining { set; get; }

        [Display(Name = "Contained", ResourceType = typeof(msg_SeanceTraining))]
        [GAppDataTable(AutoGenerateFilter = true, FilterBy = "SeanceTraining.Contained", SearchBy = "SeanceTraining.Contained", OrderBy = "SeanceTraining.Contained", PropertyPath = "SeanceTraining.Contained")]
        public String Contained { set; get; }
 
        public int Number_Absences_In_This_Module { set; get; }
        public int Number_Absences_In_This_DayOfWeek { set; get; }
        public int Number_Absences_In_This_Week { set; get; }
        public int Number_Absences_In_This_Month { set; get; }
        public int Number_Absences_In_This_Year { set; get; }



        [Display(Name = "SingularName", ResourceType = typeof(msg_StateOfAbsece))]
        public string StateOfAbsence { set; get; }

        [Display(Name = "AbsenceState", GroupName = "States", Order = 3, ResourceType = typeof(msg_Absence))]
        [GAppDataTable(PropertyPath = "AbsenceState", FilterBy = "AbsenceState", SearchBy = "AbsenceState", OrderBy = "AbsenceState", AutoGenerateFilter = true, isColumn = true)]
        public AbsenceStates AbsenceState { set; get; }


 
    }
}
