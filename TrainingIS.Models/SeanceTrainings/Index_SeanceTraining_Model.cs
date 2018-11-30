using GApp.Models;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Entities.Resources.AbsenceResources;
using TrainingIS.Entities.Resources.GroupResources;
using TrainingIS.Entities.Resources.ModuleTrainingResources;
using TrainingIS.Entities.Resources.SeanceNumberResources;
using TrainingIS.Entities.Resources.SeanceTrainingResources;

namespace TrainingIS.Models.SeanceTrainings
{
    [IndexView(typeof(SeanceTraining))]
    [SearchBy("Reference")]
    public class Index_SeanceTraining_Model : BaseModel
    {

        [GAppDataTable(AutoGenerateFilter = true, FilterBy = "SeancePlanning.Training.Group.Id", SearchBy = "SeancePlanning.Training.Group.Reference", OrderBy = "SeancePlanning.Training.Group.Reference", PropertyPath = "SeancePlanning.Training.Group")]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Group))]
        public Group Group { set; get; }

        [GAppDataTable(AutoGenerateFilter = true, FilterBy = "SeancePlanning.Training.ModuleTraining.Id", SearchBy = "SeancePlanning.Training.ModuleTraining.Reference", OrderBy = "SeancePlanning.Training.ModuleTraining.Reference", PropertyPath = "SeancePlanning.Training.ModuleTraining")]
        [Display(Name = "SingularName", ResourceType = typeof(msg_ModuleTraining))]
        public ModuleTraining ModuleTraining { set; get; }

        [Required]
        [Display(Name = "SeanceDate",ShortName ="Date", ResourceType = typeof(msg_SeanceTraining))]
        [GAppDataTable(FilterBy = "SeanceDate", SearchBy = "SeanceDate", OrderBy = "SeanceDate", PropertyPath = "SeanceDate")]
        [DataType(DataType.Date)]
        public DateTime SeanceDate { set; get; }


        [GAppDataTable(SearchBy = "SeancePlanning.SeanceNumber.Reference", OrderBy = "SeancePlanning.SeanceNumber.Reference", PropertyPath = "SeancePlanning.SeanceNumber.Reference")]
        [Display(Name = "SingularName", ShortName = "S", ResourceType = typeof(msg_SeanceNumber))]
        public SeanceNumber SeanceNumber { set; get; }

        [Display(Name = "Contained", ResourceType = typeof(msg_SeanceTraining))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "Contained", SearchBy = "Contained", OrderBy = "Contained", PropertyPath = "Contained")]
        public String Contained { set; get; }

        [Display(Name = "FormerValidation", ShortName ="V", ResourceType = typeof(msg_SeanceTraining))]
        [GAppDataTable(AutoGenerateFilter = true, FilterBy = "FormerValidation", SearchBy = "FormerValidation", OrderBy = "FormerValidation", PropertyPath = "FormerValidation")]
        public Boolean FormerValidation { set; get; }

        [Display(Name = "PluraleName", ResourceType = typeof(msg_Absence))]
        public String Absences_Description { set; get; }

        [GAppDataTable(PropertyPath = "Absences.Count")]
        [Display(Name = "Absences_Count",  ResourceType = typeof(msg_SeanceTraining))]
        public int Absences_Count { set; get; }

        [Display(AutoGenerateField =false)]
        public List<Absence> Absences { set; get; }

    }
}
