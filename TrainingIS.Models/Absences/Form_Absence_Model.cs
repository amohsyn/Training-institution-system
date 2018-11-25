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
using TrainingIS.Entities.enums;
using System.ComponentModel.DataAnnotations;

using TrainingIS.Entities.Resources.AbsenceResources;
using TrainingIS.Entities.Resources.SeanceTrainingResources;
using TrainingIS.Entities.Resources.TraineeResources;
using GApp.Entities.Resources.BaseEntity;
using System.ComponentModel;

namespace TrainingIS.Entities.ModelsViews
{
    [FormView(typeof(Absence))]
    public class Form_Absence_Model : BaseModel
    {
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Trainee))]
        public Int64 TraineeId { set; get; }

        [Display(Name = "AbsenceState", GroupName = "States", Order = 3, ResourceType = typeof(msg_Absence))]
        [GAppDataTable(PropertyPath = "AbsenceState", FilterBy = "AbsenceState", SearchBy = "AbsenceState", OrderBy = "AbsenceState", AutoGenerateFilter = true, isColumn = true)]
        public AbsenceStates AbsenceState { set; get; }

        [ReadOnly(true)]
        [Display(Name = "SingularName", ResourceType = typeof(msg_SeanceTraining))]
        public SeanceTraining SeanceTraining { set; get; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_SeanceTraining))]
        public Int64 SeanceTrainingId { set; get; }


        [Display(Name = "FormerComment", ResourceType = typeof(msg_Absence))]
        public String FormerComment { set; get; }

        [Display(Name = "TraineeComment", ResourceType = typeof(msg_Absence))]
        public String TraineeComment { set; get; }

        [Display(Name = "SupervisorComment", ResourceType = typeof(msg_Absence))]
        public String SupervisorComment { set; get; }

        [Display(AutoGenerateField = false)]
        public JustificationAbsence JustificationAbsence { set; get; }

    }
}
