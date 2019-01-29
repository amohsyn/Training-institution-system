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

using TrainingIS.Entities.Resources.SpecialtyResources;
using TrainingIS.Entities.Resources.MetierResources;
using TrainingIS.Entities.Resources.YearStudyResources;
using GApp.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.ModuleTrainingResources;
using GApp.Entities.Resources.BaseEntity;
using TrainingIS.Entities.Resources.TrainingYearResources;
using TrainingIS.Entities.Resources.FormerResources;
using TrainingIS.Entities.Resources.GroupResources;

namespace TrainingIS.Entities.ModelsViews
{
    [FormView(typeof(Training))]
    public class Form_Training_Model : BaseModel
    {
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_TrainingYear))]
        public Int64 TrainingYearId { set; get; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_Specialty))]
        [ComboBox(DataFrom = typeof(Specialty))]
        public Int64 SpecialtyId { set; get; }

        //[Display(AutoGenerateField = false)]
        //public List<BaseEntity> Specialities { set; get; }

        [Required]
        // Specilize Code for [SelectFilter(Filter_HTML_Id = "SpecialtyId", PropertyType = typeof(ModuleTraining))]
        [Display(Name = "SingularName", ResourceType = typeof(msg_ModuleTraining))]
        public Int64 ModuleTrainingId { set; get; }


        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Former))]
        public Int64 FormerId { set; get; }

        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Group))]
        public Int64 GroupId { set; get; }

        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        public String Code { set; get; }

        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public String Description { set; get; }

    }
}
