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
using TrainingIS.Entities.Resources.FormerSpecialtyResources;
using TrainingIS.Entities.Resources.TrainingResources;

namespace TrainingIS.Entities.ModelsViews
{
    [FormView(typeof(Training))]
    public class Form_Training_Model : BaseModel
    {
        #region Group
        [Required]
        [Display(Name = "TrainingYear", GroupName = "Group", ResourceType = typeof(msg_Training))]
        public Int64 TrainingYearId { set; get; }

        [Display(Name = "Specialty", GroupName = "Group", ResourceType = typeof(msg_Training))]
        [ComboBox(DataFrom = typeof(Specialty))]
        public Int64? SpecialtyId { set; get; }

        [Required]
        [Display(Name = "Group", GroupName = "Group", ResourceType = typeof(msg_Training))]
        public Int64 GroupId { set; get; }

        [Display(Name = "Description", GroupName = "Group", ResourceType = typeof(msg_Training))]
        public String Description { set; get; }
        #endregion

        #region Module 
        // Module
        [Required]
        [Display(Name = "ModuleTraining", GroupName = "Module", ResourceType = typeof(msg_Training))]
        public long ModuleTrainingId { set; get; }

        [Display(Name = "Hourly_Mass_To_Teach", GroupName = "Module", ResourceType = typeof(msg_Training))]
        [GAppDataTable(PropertyPath = "Hourly_Mass_To_Teach", FilterBy = "Hourly_Mass_To_Teach", isSeachBy = false, OrderBy = "Hourly_Mass_To_Teach", AutoGenerateFilter = false, isColumn = true)]
        public float Hourly_Mass_To_Teach { get; set; }
        #endregion


        #region Former
        [Display(Name = "FormerSpecialty", GroupName = "Former", ResourceType = typeof(msg_Training))]
        [ComboBox(DataFrom = typeof(FormerSpecialty))]
        public long? FormerSpecialtyId { set; get; }
        // Former
        [Required]
        [Display(Name = "Former", GroupName = "Former", ResourceType = typeof(msg_Training))]
        public long FormerId { set; get; }
        #endregion

  

    }
}
