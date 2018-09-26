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
using TrainingIS.Entities.Resources.SpecialtyResources;
using TrainingIS.Entities.Resources.MetierResources;
using TrainingIS.Entities.Resources.YearStudyResources;
using GApp.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.ModuleTrainingResources;
using TrainingIS.Entities;

namespace TrainingIS.Models.ModuleTrainings
{
    [IndexView(typeof(ModuleTraining))]
    public class Index_ModuleTraining_Model : BaseModel
    {
        [Display(Name = "SingularName", ResourceType = typeof(msg_Specialty))]
        [GAppDataTable(AutoGenerateFilter = true, FilterBy = "Specialty.Id", SearchBy = "Specialty.Reference", OrderBy = "Specialty.Reference", PropertyPath = "Specialty")]
        public Specialty Specialty { set; get; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_Metier))]
        [GAppDataTable(AutoGenerateFilter = true, FilterBy = "Metier.Id", SearchBy = "Metier.Reference", OrderBy = "Metier.Reference", PropertyPath = "Metier")]
        public Metier Metier { set; get; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_YearStudy))]
        [GAppDataTable(AutoGenerateFilter = true, FilterBy = "YearStudy.Id", SearchBy = "YearStudy.Reference", OrderBy = "YearStudy.Reference", PropertyPath = "YearStudy")]
        public YearStudy YearStudy { set; get; }

        [Required]
        [Display(Name = "Name", ResourceType = typeof(msg_app))]
        [GAppDataTable(AutoGenerateFilter = true, FilterBy = "Name", SearchBy = "Name", OrderBy = "Name", PropertyPath = "Name")]
        public String Name { set; get; }

        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "Code", SearchBy = "Code", OrderBy = "Code", PropertyPath = "Code")]
        public String Code { set; get; }

        [Display(Name = "HourlyMass", ResourceType = typeof(msg_ModuleTraining))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "HourlyMass", SearchBy = "HourlyMass", OrderBy = "HourlyMass", PropertyPath = "HourlyMass")]
        public Single HourlyMass { set; get; }

    }
}
