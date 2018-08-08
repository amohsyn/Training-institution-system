using System;
using System.ComponentModel.DataAnnotations;
using TrainingIS.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.TrainingYearResources;
using TrainingIS.Entities.Resources.ModuleTrainingResources;
using TrainingIS.Entities.Resources.FormerResources;
using TrainingIS.Entities.Resources.GroupResources;
using TrainingIS.Entities.Resources.SpecialtyResources;
using GApp.WebApp.Manager.Views.Attributes;
using System.Collections.Generic;
using GApp.Core.Entities.ModelsViews;
using GApp.Entities;

namespace TrainingIS.Entities.ModelsViews.Trainings
{
    public class TrainingFormView : BaseModelView
    {
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_TrainingYear))]
        public Int64 TrainingYearId { set; get; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_Specialty))]
        [ComboBox(DataFrom = typeof(Specialty))]
        public Int64 SpecialtyId { set; get; }

        [Display(AutoGenerateField = false)]
        public List<BaseEntity>  Specialities { set; get; }

        [Required]
        [SelectFilter(Filter_HTML_Id = "SpecialtyId" , PropertyType = typeof(ModuleTraining))]
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
