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
using TrainingIS.Entities.Resources.SeanceTrainingResources;
using TrainingIS.Entities.Resources.SeancePlanningResources;
using TrainingIS.Entities.Resources.FormerResources;
using TrainingIS.Entities.Resources.GroupResources;
using TrainingIS.Entities.Resources.ModuleTrainingResources;
using TrainingIS.Entities.Resources.ScheduleResources;
using TrainingIS.Entities.Resources.SeanceDayResources;
using TrainingIS.Entities.Resources.SeanceNumberResources;
using TrainingIS.Entities.Resources.ClassroomResources;
using TrainingIS.Entities.Resources.AbsenceResources;

namespace TrainingIS.Entities.ModelsViews
{
    [ExportView(typeof(SeanceTraining))]
    public class SeanceTraining_Export_Model : BaseModel
    {
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_Group))]
        public virtual Group Group { set; get; }

        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_ModuleTraining))]
        public virtual ModuleTraining ModuleTraining { set; get; }
 
        [Display(Name = "SeanceDate", Order = 0, ResourceType = typeof(msg_SeanceTraining))]
        [DataType(DataType.Date)]
        public DateTime SeanceDate { set; get; }

        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_SeanceDay))]
        public virtual SeanceDay SeanceDay { set; get; }

        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_SeanceNumber))]
        public virtual SeanceNumber SeanceNumber { set; get; }

        [Display(Name = "Plurality", AutoGenerateFilter = true, ResourceType = typeof(msg_SeanceTraining))]
        public int Plurality { set; get; }

        [Display(Name = "Contained", Order = 0, ResourceType = typeof(msg_SeanceTraining))]
        public String Contained { set; get; }

        [Display(Name = "PluralName", ResourceType = typeof(msg_Absence))]
        public string Absences_Trainees { set; get; }

 
        [GAppDataTable(PropertyPath = "Absences", FilterBy = "", SearchBy = "", OrderBy = "Absences.Count", AutoGenerateFilter = false, isColumn = true)]
        public List<Absence> Absences { set; get; }

        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_Classroom))]
        public virtual Classroom Classroom { set; get; }

        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_Former))]
        public virtual Former Former { set; get; }

        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_Schedule))]
        public virtual Schedule Schedule { set; get; }

    }
}
