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
        public Specialty Specialty { set; get; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_Metier))]
        public Metier Metier { set; get; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_YearStudy))]
        public YearStudy YearStudy { set; get; }

        [Required]
        [Display(Name = "Name", ResourceType = typeof(msg_app))]
        public String Name { set; get; }

        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        public String Code { set; get; }

        [Display(Name = "HourlyMass", ResourceType = typeof(msg_ModuleTraining))]
        public Int32 HourlyMass { set; get; }

    }
}
