using GApp.Core.Entities.ModelsViews;
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

namespace TrainingIS.Models.Absences
{
    [IndexView(typeof(Absence))]
    public class Index_Absence_Model : BaseModel
    {
        public bool Absent { set; get; } = true;

       
        [GAppDataTable(AutoGenerateFilter =true, PropertyPath = "AbsenceDate")]
        [Display(Name = "AbsenceDate", AutoGenerateFilter =true, ResourceType = typeof(msg_Absence))]
        [DataType(DataType.Date)]
        public DateTime AbsenceDate { set; get; }

        [SearchBy("Trainee.LastName")]
        [GAppDataTable(AutoGenerateFilter = true, PropertyPath = "Trainee.FirstName")]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Trainee))]
        public Trainee Trainee { set; get; }

        [GAppDataTable(AutoGenerateFilter = true, PropertyPath = "SeanceTraining.SeancePlanning.Training.Group.Code")]
        [Display(Name = "SingularName", AutoGenerateFilter = true, Order = 40, ResourceType = typeof(msg_Group))]
        public Group Group { set; get; }


        [GAppDataTable(PropertyPath = "isHaveAuthorization")]
        [Required]
        [Display(Name = "isHaveAuthorization", ResourceType = typeof(msg_Absence))]
        public Boolean isHaveAuthorization { set; get; }

        [SearchBy("SeanceTraining.SeancePlanning.SeanceNumber.Code")]
        [Display(Name = "SingularName", ResourceType = typeof(msg_SeanceTraining))]
        public SeanceTraining SeanceTraining { set; get; }

        public int Number_Absences_In_This_Module { set; get; }
        public int Number_Absences_In_This_DayOfWeek { set; get; }
        public int Number_Absences_In_This_Week { set; get; }
        public int Number_Absences_In_This_Month { set; get; }
        public int Number_Absences_In_This_Year { set; get; }

        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_StateOfAbsece))]
        public string StateOfAbsence { set; get; }

        [Required]
        [GAppDataTable(PropertyPath = "Valide")]
        [Display(Name = "Valide", ResourceType = typeof(msg_Absence))]
        public Boolean Valide { set; get; }


    }
}
